using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xaml;

namespace Grammophone.Configuration
{
	/// <summary>
	/// Loader of a settings instance persisted in XAML. As an instance, it uses
	/// a specified <see cref="IXamlSettingsSection"/> to locate the XAML file,
	/// but also offers the static <see cref="LoadSettings(string, object)"/> method to load
	/// an arbitrary XAML file.
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

		#region Public methods

		/// <summary>
		/// Load and parse an arbitrary XAML file into an instance of <typeparamref name="T"/>.
		/// </summary>
		/// <param name="xamlFilename">The XAML filename.</param>
		/// <param name="postLoadEventSender">
		/// The optional user-specified sender to be supplied when <typeparamref name="T"/>
		/// implements the <see cref="IXamlLoadListener"/> interface.
		/// </param>
		/// <returns>Returns the instance of type <typeparamref name="T"/>.</returns>
		/// <exception cref="ConfigurationException">
		/// Thrown when the XAML file does not specify a descendant of <typeparamref name="T"/>.
		/// </exception>
		public static T LoadSettings(string xamlFilename, object postLoadEventSender = null)
		{
			if (xamlFilename == null) throw new ArgumentNullException(nameof(xamlFilename));

			// Is this an absolute or a relative path?
			if (!System.IO.Path.IsPathRooted(xamlFilename))
			{
				// If not, translate it according to the AppDomain's base. 
				// This is required for web applications, otherwise a relative path would be OK.
				xamlFilename = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, xamlFilename);
			}

			using (var reader = new XamlXmlReader(xamlFilename))
			{
				var settings = XamlServices.Load(reader) as T;

				if (settings == null)
					throw new ConfigurationException(
						$"The instance specified in the XAML file is not of type '{typeof(T).FullName}'.");

				var xamlLoadListener = settings as IXamlLoadListener;

				if (xamlLoadListener != null) xamlLoadListener.OnPostLoad(postLoadEventSender);

				return settings;
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

			string settingsPath = xamlSettingsSection.SettingsXamlPath;

			return LoadSettings(settingsPath, this);
		}

		#endregion
	}
}
