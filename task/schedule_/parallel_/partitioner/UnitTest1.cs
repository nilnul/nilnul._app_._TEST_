using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Concurrent;

namespace nilnul.app.task.schedule_.parallel_.partitioner
{
	[TestClass]
	public class UnitTest1
	{
		[TestMethod]
		public void TestMethod1()
		{
			
			Partitioner.Create(0, 10,Environment.ProcessorCount);
		}
	}
}
