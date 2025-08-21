using System;
using System.Diagnostics;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace nilnul.app._trac.src
{
	[TestClass]
	public class UnitTest1
	{
		[TestMethod]
		public void TestMethod1()
		{
			var log = new TraceSource(MethodBase.GetCurrentMethod().DeclaringType.FullName)
;
			log.Switch = new SourceSwitch("a") { Level = SourceLevels.All };

			log.Listeners.Clear();

			string address4log = null;

			using (TextWriterTraceListener file = new TextWriterTraceListener(address4log))
			using (ConsoleTraceListener console = new ConsoleTraceListener())
			{
				//The file will likely be in /bin/Debug/log.txt
				log.Listeners.Add(file);
				//So you can see the results in screen
				log.Listeners.Add(console);
				//Now trace, the console trace appears immediately.


				f(
										 log

				);

				log.TraceInformation("test");
				//File buffers, it flushes on Dispose or when you say so.
				file.Flush();
				log.Flush();

			}
		}

		private void f(TraceSource log)
		{
			throw new NotImplementedException();
		}
	}
}
