using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Portable.Xaml;

namespace Grammophone.Configuration.ReadOnlyModels
{
	/// <summary>
	/// Schema context for read-only models with private property setters.
	/// </summary>
	public class ReadOnlyXamlSchemaContext : XamlSchemaContext
	{
		#region Protected methods

		/// <summary>
		/// Yields a <see cref="ReadOnlyXamlType"/> for the type.
		/// </summary>
		public override XamlType GetXamlType(Type type) => new ReadOnlyXamlType(type, this);

		#endregion
	}
}
