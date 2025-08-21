using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace nilnul.app.task_.ur_
{
	/// <summary>
	/// 
	/// </summary>
	internal class Wrong
	{
		private bool _completed;

		private Exception? _exception;

		private Action? _continuation;
		private ExecutionContext? _context;
		public bool isCompleted { get {
				lock (this)
				{
				return _completed;

				}
				//throw new NotImplementedException();
			} }


		public void setResult()  {
			complete(null);
		}

		public void setException(Exception exception) {
			complete(exception);
		}

		 void complete(Exception? exception) {
			lock (this)
			{
				if (_completed)
				{
					throw new InvalidOperationException("Stop messing up with my code.");
				}
				_completed = true;
				_exception = exception;
				if (_continuation is not null)
				{
					task.schedule_.parallel_.threads_.Ur.QueueUserWorkItem(
						delegate {
							if (_context is  null)
							{
								_continuation();
							}
							else
							{
								ExecutionContext.Run(
									_context
									,
									(object? state) => ((Action)state!).Invoke()
									,
									_continuation
								);
							}
						}
					);
				}
			}
		}

		public void wait() {
			ManualResetEventSlim? mres = null;
			lock (this) {
				if (!_completed)
				{
					mres = new ManualResetEventSlim();
					continueWith(mres.Set);
				}
			}

			mres?.Wait();
			if (_exception is not null)
			{
				// ExceptionDispatchInfo.Capture(_exception).Throw();

				throw new AggregateException( _exception );
			}
		}

		public Wrong continueWith(Action action) {
			var t = new Wrong();

			Action callback = () => {
				try
				{
					action();

				}
				catch (Exception x)
				{
					t.setException(x);
					return;
					//throw;
				}
				t.setResult();
			};

			lock (this)
			{
				if (_completed)  /// if the previous is completed.
				{
					task.schedule_.parallel_.threads_.Ur.QueueUserWorkItem(callback);
				}
				else
				{
					_continuation = action;
					_context = ExecutionContext.Capture();
				}
			}

			return t;
		}

		static public Wrong Run(Action action) {
			var t = new Wrong();

			task.schedule_.parallel_.threads_.Ur.QueueUserWorkItem(
				() => {
					try
					{
						action();
					}
					catch (Exception e)
					{
						t.setException(e);
						return;
						//throw;
					}
					t.setResult();
				}
			);

			return t;
		}
		static public Wrong WhenAll(IEnumerable<Wrong> tasks) {
			var t = new Wrong();

			if (tasks.Count() == 0)
			{
				t.setResult();
			}
			else
			{
				var remained = tasks.Count();

				Action conti = () => {
					if (Interlocked.Decrement(ref remained) == 0)
					{
						t.setResult(); /// without the above, we might do this twice.
					}
				};

				foreach (var t1 in tasks)
				{
					t1.continueWith(conti);
				}

			}


			return t;
		}

		static public Wrong Delay(int milliseconds) {
			var t = new Wrong();

			new Timer(
				_ => t.setResult()
			).Change(milliseconds, -1);


			return t;
		}

	}
}
