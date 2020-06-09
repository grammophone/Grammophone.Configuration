using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xaml.Schema;
using System.Reflection;

namespace Grammophone.Configuration.ReadOnlyModels
{
	/// <summary>
	/// Adds an element to a collection using its private/protected/internal/public method "AddItem".
	/// </summary>
	public class ReadOnlyXamlTypeInvoker : XamlTypeInvoker
	{
		#region Construction

		/// <summary>
		/// Create.
		/// </summary>
		/// <param name="xamlType">The <see cref="ReadOnlyXamlType"/> instance.</param>
		public ReadOnlyXamlTypeInvoker(ReadOnlyXamlType xamlType)
			: base(xamlType)
		{

		}

		#endregion

		#region Protected methods

		/// <summary>
		/// Invokes the private/protected/internal/public "AddItem" method to add an <paramref name="item"/> to an <paramref name="instance"/>.
		/// </summary>
		public override void AddToCollection(object instance, object item)
		{
			if (instance == null) throw new ArgumentNullException("instance");

			var addItemMethodInfo = instance.GetType().GetMethod("AddItem", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

			if (addItemMethodInfo != null)
			{
				if (addItemMethodInfo.GetParameters().Length == 1)
				{
					addItemMethodInfo.Invoke(instance, new object[] { item });

					return;
				}
			}

			base.AddToCollection(instance, item);
		}

		#endregion
	}
}
