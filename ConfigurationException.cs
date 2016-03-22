using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gramma.Configuration
{
	/// <summary>
	/// Exception for config or XAML file errors.
	/// </summary>
	[Serializable]
	public class ConfigurationException : Exception
	{
		/// <summary>
		/// Create.
		/// </summary>
		/// <param name="message">The exception message.</param>
		public ConfigurationException(string message) : base(message) { }

		/// <summary>
		/// Create.
		/// </summary>
		/// <param name="message">The exception message.</param>
		/// <param name="inner">The inner cause of the exception.</param>
		public ConfigurationException(string message, Exception inner) : base(message, inner) { }

		/// <summary>
		/// Used for serialization.
		/// </summary>
		protected ConfigurationException(
		System.Runtime.Serialization.SerializationInfo info,
		System.Runtime.Serialization.StreamingContext context) : base(info, context)
		{ }
	}
}
