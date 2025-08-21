using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace nilnul.app.task_.ur_
{
	/// <summary>
	/// 
	/// </summary>
	internal class Loop
	{
		private bool _completed;

		private Exception? _exception;

		private Action? _continuation;
		private ExecutionContext? _context;
		public bool isCompleted
		{
			get
			{
				lock (this)
				{
					return _completed;

				}
				//throw new NotImplementedException();
			}
		}


		public void setResult()
		{
			complete(null);
		}

		public void setException(Exception exception)
		{
			complete(exception);
		}

		void complete(Exception? exception)
		{
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
						delegate
						{
							if (_context is null)
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

		public void wait()
		{
			ManualResetEventSlim? mres = null;
			lock (this)
			{
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

				throw new AggregateException(_exception);
			}
		}

		public Loop continueWith(Action action)
		{
			var t = new Loop();

			Action callback = () =>
			{
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
					_continuation = callback;
					_context = ExecutionContext.Capture();
				}
			}

			return t;
		}
		public Loop continueWith(Func<Loop> action)
		{
			var t = new Loop();

			Action callback = () =>
			{
				try
				{
					var next = action();
					next.continueWith(
						delegate
						{
							if (next._exception is not null)
							{
								t.setException(
									next._exception
								);
							}
							else
							{
								t.setResult();
							}
						}

					);

				}
				catch (Exception x)
				{
					t.setException(x);
					return;
					//throw;
				}
				//t.setResult();
			};

			lock (this)
			{
				if (_completed)  /// if the previous is completed.
				{
					task.schedule_.parallel_.threads_.Ur.QueueUserWorkItem(callback);
				}
				else
				{
					_continuation = callback;
					_context = ExecutionContext.Capture();
				}
			}

			return t;
		}

		static public Loop Run(Action action)
		{
			var t = new Loop();

			task.schedule_.parallel_.threads_.Ur.QueueUserWorkItem(
				() =>
				{
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
		static public Loop WhenAll(IEnumerable<Loop> tasks)
		{
			var t = new Loop();

			if (tasks.Count() == 0)
			{
				t.setResult();
			}
			else
			{
				var remained = tasks.Count();

				Action conti = () =>
				{
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

		static public Loop Delay(int milliseconds)
		{
			var t = new Loop();

			new Timer(
				_ => t.setResult()
			).Change(milliseconds, -1);


			return t;
		}

		/// <summary>
		///  by loop, instead of recurrsion;
		/// </summary>
		/// <param name="tasks"></param>
		/// <returns></returns>
		static public Loop Iterate(IEnumerable<Loop> tasks)
		{
			var t = new Loop();

			var enumtor = tasks.GetEnumerator();
			void moveNext()
			{
				try
				{
					/// https://gist.github.com/jamesmontemagno/12992547430b85723e997a312f13ddf7
					while(enumtor.MoveNext())
					{
						var c = enumtor.Current;
						if (c.isCompleted)
						{
							c.wait();
							continue;
						}

						c.continueWith(moveNext);	///wrong?

						return;
					}


				}
				catch (Exception e)
				{
					t.setException(e);
					return;
				}
				t.setResult();

			}
			moveNext();

			return t;
		}

		public struct Awaiter(Loop u) : INotifyCompletion
		{
			public Awaiter GetAwaiter() => this;

			public bool IsCompleted => u.isCompleted;
			public void OnCompleted(Action continuation)
			{
				u.continueWith(continuation);
			}

			public void GetResult() => u.wait();
		}

		public Awaiter GetAwaiter() => new Awaiter(this);

	}
}
