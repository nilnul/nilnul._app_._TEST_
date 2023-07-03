using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace nilnul._app_._TEST_.nilnul0.app.asyn
{
	class YieldBack
	{
		public static void MethodName()
		{

			//how can it be asynchronouse operation and stay in the same thread ? ..As a simple example: WinForms app:

			async void Form_Load(object s, object e)
			{
				await Task.Yield();
				MessageBox.Show("Async message!");
			}
			//Form_Load will return to the caller(the WinFroms framework code which has fired Load event), and then the message box will be shown asynchronously, upon some future iteration of the message loop run by
			Application.Run();
			//.
			//The continuation callback is queued with
			//WinForms
				//SynchronizationContext.Post
			//, which internally posts a private Windows message to the UI thread's message loop. The callback will be executed when this message gets pumped, still on the same thread.

//In a console app, you can run a similar serializing loop with 

		}
	}
}
