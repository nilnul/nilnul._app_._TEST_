using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace nilnul._app_._TEST_.nilnul0.app.task_.timeout.awaited
{
	[TestClass]
	public class UnitTest1
	{
		bool _check = false;

		 public async Task/*<bool>*/ _WaitAsyn_ofAddress( CancellationToken canel, int interval = 3 * 1000)
		{

			while (!_check)
			{
				//canel.ThrowIfCancellationRequested();
				await Task.Delay(interval, canel);
			}
			//return true;

		}

		 public async Task _ForceAsyn_ofAddress( int totalTime = 30 * 1000, int interval = 7 * 1000)
		{

			var cancelSrc = new CancellationTokenSource(totalTime);
			try
			{
				await _WaitAsyn_ofAddress( cancelSrc.Token, interval);
				throw new UnexpectedReachException();


			}
			catch (TaskCanceledException ex)
			{
				return;
			}
			catch (OperationCanceledException e)
			{

				return ;
			}
			throw new UnexpectedReachException();

		}

		[TestMethod]
		public void TestMethod1()
		{
			_ForceAsyn_ofAddress().Wait();

		}
	}
}
