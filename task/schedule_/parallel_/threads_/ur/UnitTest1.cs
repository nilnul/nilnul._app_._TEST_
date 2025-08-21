using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.Threading;

namespace nilnul.app.task.schedule_.parallel_.threads_.ur.asynLocal
{
	[TestClass]
	public class UnitTest1
	{
		[TestMethod]
		public void TestMethod1()
		{
			AsyncLocal<int> myValue = new AsyncLocal<int>();

			for (int i = 0; i < 1000; i++)
			{
				myValue.Value = i; ///

				Ur	/// multiple cores
					.QueueUserWorkItem/// one queue per core
				(
					delegate {
						Debug.WriteLine(myValue.Value);
						Thread.Sleep(1000);
					}
				);


			}

			Debug.WriteLine("scheduled");

			Thread.Sleep(3000*10);
		}
	}
}
