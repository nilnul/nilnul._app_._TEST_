using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace nilnul._app_._TEST_.nilnul0.app.task.kill_.thread
{
	[TestClass]
	public class UnitTest1
	{
		[TestMethod]
		public void TestMethod1()
		{
			//Usage example:

			var cts = new CancellationTokenSource();
			cts.CancelAfter(7000);
			var task = RunAbortable(() => dowork(), cts.Token);
			task.Wait();

		}

		int dowork() {
			for (int i = 0; i < 10;)
			{

			}
			return 1;
		}
		private static Task<TResult> RunAbortable<TResult>(
			Func<TResult> function
			,
			CancellationToken cancellationToken
		)
		{
			var tcs = new TaskCompletionSource<TResult>();

			///1) That Stephen Toub quote from .NET Matters was written in March 2006 which predates the .NET 4.0 announcement and 1st .NET 4.0 Public beta in 2008 and 2009 respecively. .NET 4.0 introduced TPL (which introduced async/await and Task). The article refers to the explcit class ThreadPool and ThreadPool.QueueUserWorkItem as per .NET 2 and not "thread pools" as is being described here. Most importantly the article refers to faults that can happen in the ThreadPool if used with QueueUserWorkItem when threads are aborted. –
			var thread = new Thread(() =>
			{
				try
				{
					TResult result;
					using (cancellationToken.Register(Thread.CurrentThread.Abort))
					{
						result = function();
					}
					tcs.SetResult(result);
				}
				catch (ThreadAbortException)
				{
					tcs.TrySetCanceled();
				}
				catch (Exception ex)
				{
					tcs.TrySetException(ex);
				}
			});
			thread.IsBackground = true;
			thread.Start();
			return tcs.Task;
		}

	}
}
