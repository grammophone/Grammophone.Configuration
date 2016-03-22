using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gramma.Configuration
{
	/// <summary>
	/// A configuration element specifying a .NET type.
	/// </summary>
	/// <typeparam name="B">The expected base of the type.</typeparam>
	public class TypeElement<B> : NamedElement
	{
		#region Private fields

		private const string typeKey = "type";

		/// <summary>
		/// Cache holding the type specified in <see cref="TypeElement{B}.Type"/>.
		/// </summary>
		private Type instanceType;

		#endregion

		#region Construction

		/// <summary>
		/// Create.
		/// </summary>
		public TypeElement()
		{
			this[typeKey] = String.Empty;
		}

		#endregion

		#region Public properties

		/// <summary>
		/// The assembly qualified type name.
		/// </summary>
		[ConfigurationProperty(typeKey, IsRequired = true)]
		public string Type
		{
			get
			{
				return (string)this[typeKey];
			}
			set
			{
				if (value == null) throw new ArgumentNullException(nameof(value));
				this[typeKey] = value;
				instanceType = null;
			}
		}

		#endregion

		#region Public methods

		/// <summary>
		/// Get the type specified in property <see cref="TypeElement{B}.Type"/>.
		/// </summary>
		/// <returns>Returns the specified type or an exception occurs.</returns>
		/// <exception cref="ConfigurationException">
		/// Thrown when <see cref="TypeElement{B}.Type"/> is not specified or 
		/// when it is not a valid type deriving from <typeparamref name="B"/>.
		/// </exception>
		/// <remarks>
		/// The type is expected to derive from <typeparamref name="B"/>.
		/// </remarks>
		public Type GetInstanceType()
		{
			if (instanceType == null)
			{
				string typeName = this.Type;

				if (String.IsNullOrWhiteSpace(typeName))
					throw new ConfigurationException(
						$"The type is not specified in configuration element with name '{this.Name}'.");

				var type = System.Type.GetType(typeName, false);

				if (type == null)
					throw new ConfigurationException(
						$"The type '{this.Type}' specified in configuration element with name '{this.Name}' is invalid.");

				if (typeof(B).IsAssignableFrom(type))
					throw new ConfigurationException(
						$"The type '{this.Type} specified in configuration element with name '{this.Name}' does not implement '{typeof(B).FullName}'.");

				// Set the cached type.
				instanceType = type;
			}

			return instanceType;
		}

		/// <summary>
		/// Create an instance of the type specified in <see cref="TypeElement{B}.Type"/> property.
		/// </summary>
		/// <param name="parameters">The parameters to the constructor.</param>
		/// <returns>Returns the specified type, which by definition derives from <typeparamref name="B"/>.</returns>
		/// <exception cref="ConfigurationException">
		/// Thrown when <see cref="TypeElement{B}.Type"/> is not specified or 
		/// when it is not a valid type deriving from <typeparamref name="B"/>.
		/// </exception>
		public B CreateInstance(params object[] parameters)
		{
			var type = GetInstanceType();

			return (B)type.TypeInitializer.Invoke(parameters);
		}

		/// <summary>
		/// Create an instance using the parameters provided by
		/// <see cref="GetDefaultInstanceParameters"/>. 
		/// The base implementation has empty
		/// parameters, thus calls the default constructor.
		/// </summary>
		/// <returns>Returns the specified type, which by definition derives from <typeparamref name="B"/>.</returns>
		public B CreateDefaultInstance()
		{
			return CreateInstance(GetDefaultInstanceParameters());
		}

		#endregion

		#region Protected methods

		/// <summary>
		/// Supplies the parameters to be used in <see cref="CreateDefaultInstance"/>.
		/// The base implementation yields empty
		/// parameters, thus the default constructor will be called.
		/// </summary>
		/// <returns>The default implementation returns null.</returns>
		protected virtual object[] GetDefaultInstanceParameters()
		{
			return null;
		}

		#endregion
	}
}
