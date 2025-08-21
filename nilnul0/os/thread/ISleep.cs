using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nilnul.os.thread
{
	internal class ISleep
	{
		/*google.com/search?q=threads+scheduler&oq=threads+schedule&gs_lcrp=EgZjaHJvbWUqBwgAEAAYgAQyBwgAEAAYgAQyBwgBEAAYgAQyBwgCEAAYgAQyBggDEEUYOTIHCAQQABiABDIICAUQABgWGB4yCAgGEAAYFhgeMggIBxAAGBYYHjIICAgQABgWGB4yCAgJEAAYFhge0gEINDE5OWowajeoAgiwAgE&sourceid=chrome&ie=UTF-8
		 * 
		 learn.microsoft.com/en-us/dotnet/api/system.threading.thread.sleep?view=net-9.0 
		 * 
		 when a thread calls "sleep," it effectively signals the operating system that it should not be scheduled for execution for the specified duration, meaning the OS will not actively try to run that thread until the sleep period is over, essentially taking it out of the scheduling pool temporarily.
This method changes the state of the thread to include WaitSleepJoin.



State change: When a thread sleeps, it transitions into a "sleeping" state, indicating to the OS that it is not currently available to run.

Sleep queue: While sleeping, the thread is placed in a special queue managed by the OS scheduler, waiting to be re-added to the runnable queue once the sleep period ends.

No CPU usage: During the sleep period, the thread does not consume any CPU resources.




You can specify Timeout.Infinite for the millisecondsTimeout parameter to suspend the thread indefinitely. However, we recommend that you use other System.Threading classes such as Mutex, Monitor, EventWaitHandle, or Semaphore instead to synchronize threads or manage resources.

		 */
	}
}
