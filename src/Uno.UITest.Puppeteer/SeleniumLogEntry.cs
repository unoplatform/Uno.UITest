using System;
using OpenQA.Selenium;

namespace Uno.UITest.Selenium
{
	internal class SeleniumLogEntry : ILogEntry
	{
		private readonly LogEntry _entry;

		public SeleniumLogEntry(LogEntry entry)
			=> _entry = entry;

		DateTime ILogEntry.Timestamp
			=> _entry.Timestamp;

		LogEntryLevel ILogEntry.Level
			=> _entry.Level switch {
			LogLevel.Info => LogEntryLevel.Info,
			LogLevel.All => LogEntryLevel.All,
			LogLevel.Severe => LogEntryLevel.Severe,
			LogLevel.Warning => LogEntryLevel.Warning,
			LogLevel.Off => LogEntryLevel.Off,
			_ => LogEntryLevel.Off,
		};

		string ILogEntry.Message
			=> _entry.Message;
	}
}
