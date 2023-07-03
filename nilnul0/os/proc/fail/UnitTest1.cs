using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace nilnul._app_._TEST_.nilnul0.os.proc.fail
{
	[TestClass]
	public class UnitTest1
	{
		[TestMethod]
		public void TestMethod1()
		{
			try
			{
				Environment.FailFast("fail fast");

			}
			catch (Exception)
			{
				//this will not execute
				throw;
			}
//Calling the Environment.FailFast method to terminate execution of an application running in the Visual Studio debugger throws an ExecutionEngineException and automatically triggers the fatalExecutionEngineError managed debugging assistant (MDA).
				}
	}
}
