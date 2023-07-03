//#if DEBUG111
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nilnul.app._run._cfg._entry.typ_.custom_
{
	[SettingsSerializeAs(SettingsSerializeAs.Xml)]
	public class Employee
	{
		public string Name { get; set; }
		public string Position { get; set; }
	}


	[TypeConverter(typeof(_room.Converter))]
	[SettingsSerializeAs(SettingsSerializeAs.String)]
	public class Room
	{
		public int RoomNumber { get; set; }
		public string Location { get; set; }
	}


}
//#endif