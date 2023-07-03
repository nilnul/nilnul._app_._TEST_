using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Specialized;
using System.Configuration;

namespace nilnul._app_._TEST_._run._cfg.sect_
{
	[TestClass]
	public class UnitTest1
	{
		[TestMethod]
		public void TestMethod1()
		{
			//You can access elements in this collection using



		}
		public string GetName1()
		{
			NameValueCollection section =
				(NameValueCollection)ConfigurationManager.GetSection("MyDictionary");
			return section["name1"];
		}
	}
}
