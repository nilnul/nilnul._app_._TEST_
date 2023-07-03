using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace nilnul.app.task.scheduler_.current
{
	class Context0current
	{
		public static void MethodName()
		{
			var x = SynchronizationContext.Current != null
				?
				TaskScheduler.FromCurrentSynchronizationContext()
				:
				TaskScheduler.Current
			;
			//stackoverflow.com/questions/23431595/task-yield-real-usages
			//
			//So, is await Task.Yield() useful? IMO, not much. It can be used as a shortcut to run the continuation via
			//SynchronizationContext.Current.Post
			//or
			//ThreadPool.QueueUserWorkItem
			//, if you really need to impose asynchrony upon a part of your method.

			//most often the TaskScheduler.Current is TaskScheduler.Default and SynchronizationContext is not set


		}
	}
}
