using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.Threading;

namespace nilnul.app.task.schedule_.parallel_.threads
{
	[TestClass]
	public class UnitTest1
	{
		[TestMethod]
		public void TestMethod1()
		{

			for (int i = 0; i < 1000; i++)
			{
				int capturedValue = i; ///

				ThreadPool	/// multiple cores
					.QueueUserWorkItem/// one queue per core
				(
					delegate {
						Debug.WriteLine(capturedValue);
						Thread.Sleep(1000);
					}
				);


			}

			Debug.WriteLine("scheduled");

			Thread.Sleep(3000*10);
		}
	}
}
