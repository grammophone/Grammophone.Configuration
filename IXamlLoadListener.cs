using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gramma.Configuration
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
		void OnPostLoad(object sender);
	}
}
