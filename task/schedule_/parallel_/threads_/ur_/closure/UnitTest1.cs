using Microsoft.VisualStudio.TestTools.UnitTesting;
//using nilnul.bit.expr_.plain.parse_._treeByParen._idiomize._lex.token.str_;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace nilnul.app.task.schedule_.parallel_.threads_.ur_.closure.asynLocal
{
	[TestClass]
	public class UnitTest1
	{
		[TestMethod]
		public void TestMethod1()
		{
			AsyncLocal<int> myValue = new AsyncLocal<int>();

			List<task_.Ur> tasks = new();

			for (int i = 0; i < 1000; i++)
			{
				myValue.Value = i; ///

				Closure	/// multiple cores
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
