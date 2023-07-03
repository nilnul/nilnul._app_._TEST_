using Microsoft.VisualStudio.TestTools.UnitTesting;
using nilnul.app.t;
using System;
using System.Diagnostics;

namespace nilnul._app_._TEST_.domains
{
		

	[TestClass]
	public class UnitTest1
	{
		[TestMethod]
		public void TestMethod1()
		{


		}

			[TestMethod]
	public void Main()
		{
			 A.ConnString = "InitialString"; // I set A.ConnString in the current domain

			var newDomain = AppDomain.CreateDomain("DomNew");
			T TObj = newDomain.CreateInstanceAndUnwrap(typeof(T).Assembly.FullName, typeof(T).FullName) as T;

			TObj.ConnString = "NewDomainString"; // It is supposed to set A.ConnString in the newDomain aka a different instance of A.ConnString

			// Here it is supposed to print two different values
			Debug.WriteLine(A.ConnString);  // "InitialString"
			Debug.WriteLine(TObj.ConnString); // "NewDomainString"
		}
	}
}
