using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gramma.Configuration
{
	/// <summary>
	/// Base class for elements contained in <see cref="NamedElementsCollection{E}"/>.
	/// </summary>
	public abstract class NamedElement : ConfigurationElement
	{
		#region Private fields

		private const string nameKey = "name";

		#endregion

		#region Construction

		/// <summary>
		/// Create.
		/// </summary>
		public NamedElement()
		{
			this[nameKey] = String.Empty;
		}

		#endregion

		#region Public properties

		/// <summary>
		/// The name of the element, serving as a key for the containing element collection.
		/// </summary>
		[ConfigurationProperty(nameKey, IsRequired = true, IsKey = true)]
		public string Name
		{
			get
			{
				return (string)this[nameKey];
			}
			set
			{
				if (value == null) throw new ArgumentNullException(nameof(value));
				this[nameKey] = value;
			}
		}

		#endregion
	}
}
