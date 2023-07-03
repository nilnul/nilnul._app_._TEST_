using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nilnul._app_._TEST_.nilnul0.app.task.scheduler_
{
	class Default
	{
		//The default task scheduler is based on the .NET Framework 4 thread pool, which provides work-stealing for load-balancing, thread injection/retirement for maximum throughput, and overall good performance. It should be sufficient for most scenarios.
		//The default scheduler for the Task Parallel Library and PLINQ uses the .NET thread pool, which is represented by the ThreadPool class, to queue and execute work. The thread pool uses the information that is provided by the Task type to efficiently support the fine-grained parallelism (short-lived units of work) that parallel tasks and queries often represent.

		//The default task scheduler, Default, uses the ThreadPool class to queue and execute work.You can override the default task scheduler by setting the TaskScheduler property when you construct a dataflow block object.

		/*ThreadPoolTaskScheduler()*/
	}
}
