using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grammophone.Configuration
{
	/// <summary>
	/// Configuration section containing a 'providers' collection of 
	/// elements of type <typeparamref name="E"/> which supply instances of
	/// type <typeparamref name="B"/>.
	/// </summary>
	/// <typeparam name="B">The base type of the instances being provided.</typeparam>
	/// <typeparam name="E">The type of the providers configuration elements.</typeparam>
	public class ProviderSection<B, E> : ConfigurationSection
		where E : TypeElement<B>, new()
	{
		#region Private fields

		private const string providersKey = "providers";

		#endregion

		#region Construction

		/// <summary>
		/// Create.
		/// </summary>
		public ProviderSection()
		{
			this[providersKey] = new NamedElementsCollection<E>();
		}

		#endregion

		#region Public properties

		/// <summary>
		/// The 
		/// </summary>
		[ConfigurationProperty(providersKey, IsDefaultCollection = false)]
		public NamedElementsCollection<E> Providers
		{
			get
			{
				return (NamedElementsCollection<E>)this[providersKey];
			}
			set
			{
				if (value == null) throw new ArgumentNullException(nameof(value));
				this[providersKey] = value;
			}
		}

		#endregion
	}
}
