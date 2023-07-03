using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace nilnul.app.evt_
{
	class Idle
	{
		// await IdleYield();

		public static Task IdleYield()
		{
			var idleTcs = new TaskCompletionSource<bool>();
			// subscribe to Application.Idle
			EventHandler handler = null;
			handler = (s, e) =>
			{
				Application.Idle -= handler;
				idleTcs.SetResult(true);
			};
			Application.Idle += handler;
			return idleTcs.Task;

//It is recommended that you do not exceed 50ms for each iteration of such background operation running on the UI thread.

		}
	}
}
