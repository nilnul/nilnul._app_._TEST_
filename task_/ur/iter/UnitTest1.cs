using Microsoft.VisualStudio.TestTools.UnitTesting;
//using nilnul.bit.expr_.plain.parse_._treeByParen._idiomize._lex.token.str_;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace nilnul.app.task_.ur.delay.iter
{
	[TestClass]
	public class UnitTest1
	{
		[TestMethod]
		public void TestMethod1()
		{
			AsyncLocal<int> myValue = new AsyncLocal<int>();


			Debug.WriteLine("hi");

			Ur.Iterate(
				PrintAsyn()
			).wait();


			static IEnumerable<Ur> PrintAsyn() {

				for (int i = 0; ; i++)
				{
					yield return Ur.Delay(100);
					Debug.WriteLine(i);
				}
			}


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


			Debug.WriteLine("scheduled");

			Thread.Sleep( (1000+200) *10 /4 );
		}
	}
}
