using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nilnul.app.task_
{
	/// <summary>
	/// On a related note, TPL even has the TaskCreationOptions.LongRunning option that requests to not run the task on a ThreadPool thread (rather, it creates a normal thread with new Thread() behind the scene).
	/// </summary>
	/// To get a test running if that even works at all you may only want to implement the behaviour the ThreadPoolTaskScheduler has for the TaskCreationOptions.LongRunning option. So spawning a new thread for each task.
	class LongRunning
	{
	}
}
