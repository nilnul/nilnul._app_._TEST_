using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace nilnul._app_._TEST_.nilnul0.app.task.kill
{
#if TRIAL

	[TestClass]
#endif
	public class UnitTest1
	{
		[TestMethod]
		public void TestMethod1()
		{

			//Only way I know of is to use old Thread stuff and call Thread.Abort() or better -have the slow 3pty piece of code being loaded into separate application domain and unload the whole application domain on timeout. This won't save you if you're calling unmanaged code from the slow 3pty piece -only way around is then separate process.

			/*
			 Putting the code in a separate process, and terminating that process on cancellation. This is the only fully safe but most difficult solution.
Putting the code in a separate app domain, and unloading that app domain on cancellation. This is not fully safe; terminated app domains can cause process-level resource leaks.
Putting the code in a separate thread, and terminating that thread on cancellation. This is even less safe; terminated threads can corrupt program memory.
			 */

			/*
		At this point, the question is "how do I cancel uncancelable code?" And the answer to that depends on how stable you want your system to be:

Run the code in a separate Thread and Abort the thread when it is no longer necessary. This is the easiest to implement but the most dangerous in terms of application instability. To put it bluntly, if you ever call Abort anywhere in your app, you should regularly restart that app, in addition to standard practices like heartbeat/smoketest checks.
Run the code in a separate AppDomain and Unload that AppDomain when it is no longer necessary. This is harder to implement (you have to use remoting), and isn't an option in the Core world. And it turns out that AppDomains don't even protect the containing application like they were supposed to, so any apps using this technique also need to be regularly restarted.
Run the code in a separate Process and Kill that process when it is no longer necessary. This is the most complex to implement, since you'll also need to implement some form of inter-process communication. But it is the only reliable solution to cancel uncancelable code.	 
			If you discard the unstable solutions (1) and (2), then the only remaining solution (3) is a ton of work - way, way more than making the code cancelable.

TL;DR: Just use the cancellation APIs the way they were designed to be used. That is the simplest and most effective solution. */
		}
	}
}
