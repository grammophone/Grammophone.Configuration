using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xaml;
using System.Reflection;

namespace Grammophone.Configuration.ReadOnlyModels
{
	/// <summary>
	/// Provides XAML information for a member with private setter.
	/// </summary>
	public class ReadOnlyXamlMember : XamlMember
	{
		#region Construction

		/// <summary>
		/// Create.
		/// </summary>
		/// <param name="propertyInfo">The reflection <see cref="PropertyInfo"/> for the member.</param>
		/// <param name="schemaContext">The XAML schema context.</param>
		public ReadOnlyXamlMember(PropertyInfo propertyInfo, XamlSchemaContext schemaContext)
			: base(propertyInfo, schemaContext)
		{

		}

		#endregion

		#region Protected methods

		/// <summary>
		/// If the property is writable, returns true, irrespective of access level.
		/// </summary>
		protected override bool LookupIsWritePublic()
		{
			var propertyInfo = (PropertyInfo)this.UnderlyingMember;

			if (!propertyInfo.CanWrite) return false;

			return true;
		}

		/// <summary>
		/// If the property has no setter, returns false.
		/// </summary>
		protected override bool LookupIsReadOnly()
		{
			var propertyInfo = (PropertyInfo)this.UnderlyingMember;

			if (!propertyInfo.CanWrite) return true;

			return false;
		}

		/// <summary>
		/// If there is a getter or if it is a readable field irrespective of access level, returns true, else false. 
		/// </summary>
		protected override bool LookupIsReadPublic()
		{
			var propertyInfo = (PropertyInfo)this.UnderlyingMember;

			if (!propertyInfo.CanRead || !propertyInfo.GetGetMethod().IsPublic) return false;

			return true;
		}

		#endregion
	}
}
