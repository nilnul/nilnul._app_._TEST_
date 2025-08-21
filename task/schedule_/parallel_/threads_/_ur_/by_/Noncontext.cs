using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace nilnul.app.task.schedule_.parallel_.threads_._ur_.by_
{
	/// youtube.com/watch?v=R-z2Hv-7nxk
	/// <summary>
	/// 
	/// </summary>
	static public class Noncontext
	{
		private static readonly BlockingCollection<Action> _workitems = new BlockingCollection<Action>();

		public static void QueueUserWorkItem(Action action) => _workitems.Add(action);
		static Noncontext()
		{
			for (int i = 0; i < Environment.ProcessorCount; i++)
			{
				new Thread(
					() =>
					{
						while (true)
						{
							var workitem = _workitems.Take();
							workitem();
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
