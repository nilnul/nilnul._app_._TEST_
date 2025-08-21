using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace nilnul.app.task_
{
	/// <summary>
	/// 
	/// </summary>
	internal class Ur
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
					///devblogs.microsoft.com/dotnet/how-async-await-really-works/
					///The whole implementation doesn’t support multiple waiters / continuations. This is by design for simplicity of discussion (and is called out in the next paragraph when talking about what Task provides that MyTask doesn’t: “with support for any number of continuations”). Just adding a ManualResetEventSlim as a field wouldn’t help with that, since ContinueWith will explicitly throw an exception in my example if it’s used multiple times.
					/// 
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

		public Ur continueWith(Action action)
		{
			var t = new Ur();

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
		public Ur continueWith(Func<Ur> action)
		{
			var t = new Ur();

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

		static public Ur Run(Action action)
		{
			var t = new Ur();

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
		static public Ur WhenAll(IEnumerable<Ur> tasks)
		{
			var t = new Ur();

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

		static public Ur Delay(int milliseconds)
		{
			var t = new Ur();

			new Timer(
				_ => t.setResult()
			).Change(milliseconds, -1);


			return t;
		}
		static public Ur Iterate(IEnumerable<Ur> tasks)
		{
			var t = new Ur();

			var enumtor = tasks.GetEnumerator();
			void moveNext()
			{
				try
				{
					if (enumtor.MoveNext())
					{
						var c = enumtor.Current;
						c.continueWith(moveNext);

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

		public struct Awaiter(Ur u) : INotifyCompletion
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
