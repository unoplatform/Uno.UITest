using System;
using System.Collections.Generic;
using System.Text;

namespace Uno.UITest
{
	/// <summary>
	/// A system log entry
	/// </summary>
	public interface ILogEntry
	{
		/// <summary>
		/// The timestamp of the entry
		/// </summary>
		DateTime Timestamp { get; }

		/// <summary>
		/// The logging level for the entry
		/// </summary>
		LogEntryLevel Level { get; }

		/// <summary>
		/// The entry message payload
		/// </summary>
		string Message { get; }
	}
}
