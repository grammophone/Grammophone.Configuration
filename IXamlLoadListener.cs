using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grammophone.Configuration
{
	/// <summary>
	/// Implemented by settings classes to be deserialized from XAML
	/// who need to take actions upon loading.
	/// </summary>
	public interface IXamlLoadListener
	{
		/// <summary>
		/// Called after XAML deserialization.
		/// </summary>
		/// <param name="sender">
		/// The <see cref="XamlConfiguration{T}"/> if loaded through <see cref="XamlConfiguration{T}.Settings"/>,
		/// else it is user-defined when loaded via <see cref="XamlConfiguration{T}.LoadSettings(string, object)"/>.
		/// In the latter case, it can be null.
		/// </param>
		void OnPostLoad(object sender);
	}
}
