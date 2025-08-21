using Microsoft.VisualStudio.TestTools.UnitTesting;
using nilnul.bit.expr_.plain.parse_._treeByParen._idiomize._lex.token.str_;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace nilnul.app.task_.ur.delay
{
	[TestClass]
	public class UnitTest1
	{
		[TestMethod]
		public void MyTestMethod()
		{

			var t = Task.Run(
				TestMethod1
			);

			Thread.Sleep(2000 + (1000 + 200) * 10 / 4 + 5000);

			if (t.IsCompleted)
			{
				throw new UnexpectedReachException("it shouldnot complete");
			}



		}
		//[TestMethod]
		//[ExpectedException()]
		public void TestMethod1()
		{
			AsyncLocal<int> myValue = new AsyncLocal<int>();

			List<task_.Ur> tasks = new();

			Debug.WriteLine("hi");

			task_.Ur.Delay(2000).continueWith(
				delegate
				{
					Debug.WriteLine("world");
				}
			).wait();

			//for (int i = 0; i < 100; i++)
			//{
			//	myValue.Value = i; ///



			//	tasks.Add(	/// multiple cores
			//		task_.Ur.Run/// one queue per core
			//	(
			//		delegate {
			//			Debug.WriteLine(myValue.Value);
			//			Thread.Sleep(1000);
			//		}
			//	)
			//	);


			//}
			//foreach (var t in tasks)
			//{
			//	t.wait();

			//}

			task_.Ur.WhenAll(tasks).wait();

			Debug.WriteLine("scheduled");

			Thread.Sleep( (1000+200) *10 /4 );
		}
	}
}
