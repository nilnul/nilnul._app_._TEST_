using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;

namespace nilnul._app_._TEST_._eg.lib_.assembly.meta
{
	[TestClass]
	public class UnitTest1
	{
		[TestMethod]
		public void TestMethod1()
		{

			var latst = _TEST_.data.latest.UnitTest1.Path();

			var m = nilnul._app.lib_.assembly._MetaX.NameEtc(
				//@"D:\170203\data\nilnul._dev_\_bak_(Git\_WIN_(1(Git\bin\Debug\Octokit.dll"
				//@"C:\Users\wangy\.nuget\packages\octokit\0.50.0\lib\net46\Octokit.dll"

				latst

				//@"D:\170203\data\(packages)\MSTest.TestFramework.3.0.0\lib\net462\Microsoft.VisualStudio.TestPlatform.TestFramework.Extensions.dll"

			);
			Debug.WriteLine(m);
		}
	}
}
