using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System;
using System.Diagnostics;
using System.Threading;

namespace nilnul._app_._TEST_.trace_.src.switched.listener.filtered.cfgless
{
	[TestClass]
	public class UnitTest1
	{
		[TestMethod]
		public void TestMethod1()
		{
			TraceSourceApp.Program.Main();

		}
	}


	namespace TraceSourceApp
	{
		public class Program
		{
			private static TraceSource mySource =
				new TraceSource("TraceSourceApp");
			static public void Main(params string[] args)
			{
				mySource.Switch = new SourceSwitch("sourceSwitch", "Error");
				mySource.Listeners.Remove("Default");
				TextWriterTraceListener textListener =
					new TextWriterTraceListener("myListener.log");
				ConsoleTraceListener console =
					new ConsoleTraceListener(false);
				console.Filter =
					new EventTypeFilter(SourceLevels.Information);
				console.Name = "console";
				textListener.Filter =
					new EventTypeFilter(SourceLevels.Error);
				mySource.Listeners.Add(console);
				mySource.Listeners.Add(textListener);
				Activity1();

				// Allow the trace source to send messages to
				// listeners for all event types. Currently only
				// error messages or higher go to the listeners.
				// Messages must get past the source switch to
				// get to the listeners, regardless of the settings
				// for the listeners.
				mySource.Switch.Level = SourceLevels.All;

				// Set the filter settings for the
				// console trace listener.
				mySource.Listeners["console"].Filter =
					new EventTypeFilter(SourceLevels.Critical);
				Activity2();

				// Change the filter settings for the console trace listener.
				mySource.Listeners["console"].Filter =
					new EventTypeFilter(SourceLevels.Information);
				Activity3();
				mySource.Close();
				return;
			}
			static void Activity1()
			{
				mySource.TraceEvent(TraceEventType.Error, 1,
					"Error message.");
				mySource.TraceEvent(TraceEventType.Warning, 2,
					"Warning message.");
			}
			static void Activity2()
			{
				mySource.TraceEvent(TraceEventType.Critical, 3,
					"Critical message.");
				mySource.TraceInformation("Informational message.");
			}
			static void Activity3()
			{
				mySource.TraceEvent(TraceEventType.Error, 4,
					"Error message.");
				mySource.TraceInformation("Informational message.");
			}
		}
	}
}
