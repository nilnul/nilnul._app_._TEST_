using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nilnul.app.task_
{
	class Completed
	{
		protected async virtual Task OverridableMethod1() {
		//	await Task.Yield();
		}
		protected virtual Task OverridableMethod() {
			return Task.CompletedTask;
		}
	}
}
