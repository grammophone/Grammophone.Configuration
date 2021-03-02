using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Portable.Xaml;
using System.Reflection;

namespace Grammophone.Configuration.ReadOnlyModels
{
	/// <summary>
	/// Reports XAML information for a type with private setters and contructors.
	/// </summary>
	public class ReadOnlyXamlType : XamlType
	{
		#region Construction

		/// <summary>
		/// Create.
		/// </summary>
		/// <param name="underlyingType">The underlying type.</param>
		/// <param name="schemaContext">The XAML schema context.</param>
		public ReadOnlyXamlType(Type underlyingType, XamlSchemaContext schemaContext)
			: base(underlyingType, schemaContext)
		{
		}

		/// <summary>
		/// Create.
		/// </summary>
		/// <param name="typeName">The name of the type to create.</param>
		/// <param name="typeArguments">The list of type arguments, if the type is generic, or empty.</param>
		/// <param name="schemaContext">The XAML schema context.</param>
		public ReadOnlyXamlType(String typeName, IList<XamlType> typeArguments, XamlSchemaContext schemaContext)
			: base(typeName, typeArguments, schemaContext)
		{

		}

		#endregion

		#region Protected methods

		/// <summary>
		/// Reports that the type is constructible if there is a default constructor of any access level.
		/// </summary>
		protected override bool LookupIsConstructible()
		{
			if (this.ConstructionRequiresArguments) return true;

			if (this.UnderlyingType.GetConstructor(
				BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance,
				null,
				Type.EmptyTypes,
				null) != null)
			{
				return true;
			}

			return false;
		}

		/// <summary>
		/// Loads a member of any access level.
		/// </summary>
		/// <param name="name">The member's name.</param>
		/// <param name="skipReadOnlyCheck">Ignored.</param>
		/// <returns>Returns a <see cref="ReadOnlyXamlMember"/>.</returns>
		protected override XamlMember LookupMember(string name, bool skipReadOnlyCheck)
		{
			var propertyInfo = this.UnderlyingType.GetProperty(name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

			if (propertyInfo == null) return null;

			return new ReadOnlyXamlMember(propertyInfo, this.SchemaContext);
		}

		/// <summary>
		/// Loads all members of all access levels.
		/// </summary>
		/// <returns>Returns a collection of <see cref="ReadOnlyXamlMember"/> items.</returns>
		protected override IEnumerable<XamlMember> LookupAllMembers()
		{
			foreach (var propertyInfo in this.UnderlyingType.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
			{
				yield return new ReadOnlyXamlMember(propertyInfo, this.SchemaContext);
			}
		}

		/* Uncomment these in order to enable adding elements to collections using their private/protected/internal method "AddItem" */

		//protected override XamlCollectionKind LookupCollectionKind()
		//{
		//  var kind = base.LookupCollectionKind();

		//  if (kind != XamlCollectionKind.None) return kind;

		//  var addItemMethodInfo = this.UnderlyingType.GetMethod("AddItem", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

		//  if (addItemMethodInfo != null)
		//  {
		//    if (addItemMethodInfo.GetParameters().Length == 1)
		//    {
		//      return XamlCollectionKind.Collection;
		//    }
		//  }

		//  return XamlCollectionKind.None;
		//}

		//protected override XamlTypeInvoker LookupInvoker()
		//{
		//  return new ReadOnlyXamlTypeInvoker(this);
		//}

		#endregion
	}
}
