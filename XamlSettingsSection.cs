using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grammophone.Configuration
{
	/// <summary>
	/// A configuration section
	/// specifying a settings instance in XAML.
	/// </summary>
	public class XamlSettingsSection : ConfigurationSection, IXamlSettingsSection
	{
		#region Private fields

		private const string settingsXamlPathKey = "settingsXamlPath";

		#endregion

		#region Construction

		/// <summary>
		/// Create.
		/// </summary>
		public XamlSettingsSection()
		{
			this.SettingsXamlPath = String.Empty;
		}

		#endregion

		#region Public properties

		/// <summary>
		/// The filename of the settings XAML representation.
		/// </summary>
		[ConfigurationProperty(settingsXamlPathKey, IsRequired = true)]
		public string SettingsXamlPath
		{
			get
			{
				return (string)this[settingsXamlPathKey];
			}
			set
			{
				if (value == null) throw new ArgumentNullException("value");
				this[settingsXamlPathKey] = value;
			}
		}

		#endregion
	}
}
