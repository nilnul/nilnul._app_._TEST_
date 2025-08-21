using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nilnul.app.task_.faulty
{
	///learn.microsoft.com/zh-cn/dotnet/csharp/asynchronous-programming/
	/// <summary>
	/// The most common scenario for a faulted task is that the Exception property contains exactly one exception. When code awaits a faulted task, the first exception in the AggregateException.InnerExceptions collection is rethrown. That's why the output from this example shows an InvalidOperationException instead of an AggregateException.
	/// </summary>
	/// <remarks>
	///  You can examine the Exception property in your code when your scenario may generate multiple exceptions.
	/// </remarks>
	internal class IAwaiter
	{
	}
}
