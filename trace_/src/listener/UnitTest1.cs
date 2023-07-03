using System;
using System.Diagnostics;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace nilnul._app_._TEST_.trace_.src.listener
{
	[TestClass]
	public class UnitTest1
	{
		[TestMethod]
		public void TestMethod1()
		{
			string address = nilnul.fs.folder_.tmp.denote_.mainVered_._NextX.SpearTxt("", "txt");
			var log = new TraceSource(MethodBase.GetCurrentMethod().DeclaringType.FullName)
				;
			log.Switch = new SourceSwitch("a") { Level = SourceLevels.All };

			log.Listeners.Clear();

			using (TextWriterTraceListener file = new TextWriterTraceListener(address))
			using (ConsoleTraceListener console = new ConsoleTraceListener())
			{
				//The file will likely be in /bin/Debug/log.txt
				log.Listeners.Add(file);
				//So you can see the results in screen
				log.Listeners.Add(console);
				//Now trace, the console trace appears immediately.
				nilnul.fs.folder.docs.dedup_.inSym_._TraceSrcX.Vod(
					@"D:\文件夹"
				, log
				);

				log.TraceInformation("test");
				//File buffers, it flushes on Dispose or when you say so.
				file.Flush();
				log.Flush();
			}

			nilnul.fs.file._ExeX.Exe(address);
		}
	}
}
