using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gramma.Configuration
{
	/// <summary>
	/// Interface expected to be implemented by a configuration section
	/// specifying a settings instance in XAML.
	/// </summary>
	public interface IXamlSettingsSection
	{
		/// <summary>
		/// The filename of the settings XAML representation.
		/// </summary>
		string SettingsXamlPath { get; set; }
	}
}
