using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace nilnul._app_._TEST_.nilnul0.app.task_.timeout
{
	[TestClass]
	public class UnitTest1
	{
		[TestMethod]
		public void TestMethod1()
		{
			var cts = new CancellationTokenSource(5000);
			var t = cts.Token;
			try
			{
				Task.Run(() =>
					{
						try
						{
							t.Register(
								() => t.ThrowIfCancellationRequested()
								,true
							//throw new TimeoutException()
							);
							for (var i = 0; i < 10;) Console.WriteLine(i);
							///todo? it seems continue to run.

						}
						catch (Exception e)  ///thread is being aborted.
						{

							throw;
						}
					}
					, t
				).Wait( 5000);

			}
			catch (Exception)
			{

				throw;
			}

		}
	}
}
