using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace nilnul.app.task.schedule_.parallel_.threads_._ur
{
	[TestClass]
	public class UnitTest1
	{
		[TestMethod]
		public void TestMethod1()
		{

			Action a;
			a = null;
			a = ((Action)null);
			(a).Invoke();
		}
	}
}
