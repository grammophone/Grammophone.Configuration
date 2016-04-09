using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xaml;

namespace Grammophone.Configuration
{
	/// <summary>
	/// Loader of a settinigs instance persisted in XAML.
	/// </summary>
	/// <typeparam name="T">The type of the settings instance in the XAML.</typeparam>
	public class XamlConfiguration<T>
		where T : class
	{
		#region Private fields

		private Lazy<T> lazySettings;

		private string configurationSectionName;

		#endregion

		#region Construction

		/// <summary>
		/// Create.
		/// </summary>
		/// <param name="configurationSectionName">The name of the configuration section implementing <see cref="IXamlSettingsSection"/>.</param>
		public XamlConfiguration(string configurationSectionName)
		{
			if (configurationSectionName == null) throw new ArgumentNullException(nameof(configurationSectionName));

			this.configurationSectionName = configurationSectionName;
			this.lazySettings = new Lazy<T>(LoadSettings, System.Threading.LazyThreadSafetyMode.ExecutionAndPublication);
		}

		#endregion

		#region Public properties

		/// <summary>
		/// Get the settings instance specified in the XAML file.
		/// </summary>
		/// <exception cref="ConfigurationException">
		/// Thrown when the configuration section doesn't exist or doesn't implement <see cref="IXamlSettingsSection"/>
		/// or the XAML file does not specify a descendant of <typeparamref name="T"/>.
		/// </exception>
		public T Settings
		{
			get
			{
				return lazySettings.Value;
			}
		}

		#endregion

		#region Private methods

		private T LoadSettings()
		{
			var configurationSection = System.Configuration.ConfigurationManager.GetSection(configurationSectionName);

			if (configurationSection == null)
				throw new ConfigurationException(
					$"No configuration section has been defined having name '{configurationSectionName}'.");

			var xamlSettingsSection = configurationSection as IXamlSettingsSection;

			if (xamlSettingsSection == null)
				throw new ConfigurationException(
					$"No configuration section having name '{configurationSectionName}' doesn't implement IXamlSettingsSection.");

			using (var reader = new XamlXmlReader(xamlSettingsSection.SettingsXamlPath))
			{
				var settings = XamlServices.Load(reader) as T;

				if (settings == null)
					throw new ConfigurationException(
						$"The instance specified in the XAML file is not of type '{typeof(T).FullName}'.");

				var xamlLoadListener = settings as IXamlLoadListener;

				if (xamlLoadListener != null) xamlLoadListener.OnPostLoad(this);

				return settings;
			}

		}

		#endregion
	}
}
