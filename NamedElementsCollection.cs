using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grammophone.Configuration
{
	/// <summary>
	/// A container for configuratin elements deriving from <see cref="NamedElement"/>.
	/// </summary>
	/// <typeparam name="E">The type of elements, deriving from <see cref="NamedElement"/>.</typeparam>
	public class NamedElementsCollection<E> : ConfigurationElementCollection
		where E : NamedElement, new()
	{
		#region Public properties

		/// <summary>
		/// Get a contained element by index.
		/// </summary>
		public E this[int index]
		{
			get
			{
				return (E)BaseGet(index);
			}
		}

		/// <summary>
		/// Get a contained element by <see cref="NamedElement.Name"/>
		/// or null if no such element exists.
		/// </summary>
		public new E this[string name]
		{
			get
			{
				return (E)BaseGet(name);
			}
		}

		#endregion

		#region Public methods

		/// <summary>
		/// Add an element to the collection.
		/// </summary>
		public void Add(E element)
		{
			if (element == null) throw new ArgumentNullException(nameof(element));

			BaseAdd(element);
		}

		/// <summary>
		/// Remove an element having a specified <see cref="NamedElement.Name"/>.
		/// </summary>
		public void Remove(string name)
		{
			if (name == null) throw new ArgumentNullException(nameof(name));

			BaseRemove(name);
		}

		/// <summary>
		/// Clear all elements.
		/// </summary>
		public void Clear()
		{
			BaseClear();
		}

		#endregion

		#region Protected methods

		/// <summary>
		/// Creates a new element of type <typeparamref name="E"/>.
		/// </summary>
		protected override ConfigurationElement CreateNewElement()
		{
			return new E();
		}

		/// <summary>
		/// Expects an element of type <typeparamref name="E"/> and 
		/// gets its <see cref="NamedElement.Name"/> property.
		/// </summary>
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((E)element).Name;
		}

		#endregion
	}
}
