using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace nilnul.app.task.schedule_.parallel_.threads_.ur_
{
	/// youtube.com/watch?v=R-z2Hv-7nxk
	/// <summary>
	/// 
	/// </summary>
	static public class _StaticX
	{
		private static readonly BlockingCollection<(Action action,
			ExecutionContext? context   /// literally a dict

		)> _workitems = new();

		public static void QueueUserWorkItem(Action action) => _workitems.Add((action, ExecutionContext.Capture()));
		static _StaticX()
		{
			for (int i = 0; i < Environment.ProcessorCount; i++)
			{
				new Thread(
					() =>
					{
						while (true)
						{
							(var action, var context) = _workitems.Take();
							if (context is null)
							{
								action();

							}
							else
							{

#if false
								ExecutionContext.Run(
										context
										,
										delegate { action(); }
										,
										null
									);
#endif

#if false

									ExecutionContext.Run(
										workitem.context
										,
										state => ((Action)state!).Invoke()
										,workitem.context
									);
#endif

#if false


								ExecutionContext.Run(
									context
									,
									 (object? state) => ((Action)state!).Invoke()
									,
									 action
								);
#endif

#if true

								ExecutionContext.Run(
										context
										,
										 static (object? state) => ((Action)state!).Invoke()
										,action
									);
#endif

#if true0
///static cannot capture dynamic variable

								ExecutionContext.Run(
										workitem.context
										,
										 static (object? state) => workitem.action()
										,workitem.context
								);


#endif

							}
						}
					}
				)
				{
					IsBackground = true
				}.Start();
			}

		}


	}
}
