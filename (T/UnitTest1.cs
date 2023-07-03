using System;
using System.Diagnostics;

#if DEBUG
namespace nilnul.app.t
{

	public static class A
	{
		public static string ConnString;
	}

	[Serializable]
	public class T
	{
		// Accesing A's field;
		public string ConnString { get { return A.ConnString; } set { A.ConnString = value; } }
	}


}

#endif

