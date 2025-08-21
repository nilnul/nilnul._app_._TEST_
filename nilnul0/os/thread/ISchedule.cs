using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nilnul.os.thread
{
	/* learn.microsoft.com/en-us/dotnet/standard/threading/scheduling-threads



	 The details of the scheduling algorithm used to determine the order in which threads are executed vary with each operating system.
	

	Under some operating systems, the thread with the highest priority (of those threads that can be executed) is always scheduled to run first. If multiple threads with the same priority are all available, the scheduler cycles through the threads at that priority, giving each thread a fixed time slice in which to execute.

If a higher priority thread becomes runnable, the lower priority thread is preempted and the higher priority thread is allowed to execute once again. On top of all that, the operating system can also adjust thread priorities dynamically as an application's user interface is moved between foreground and background. 

		*/

	
	/// <summary>
	/// 
	/// </summary>
	internal class ISchedule
	{
	}
}
