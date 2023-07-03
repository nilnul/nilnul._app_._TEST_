using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System.ComponentModel;
//#if DEBUG

namespace nilnul.app._run._cfg._entry.typ_.custom_._room
{
	/*
	 www.blackwasp.co.uk/CustomAppSettings_2.aspx
	 */
	public class Converter

		:
		//This must be derived from TypeConverter so start by creating the class declaration:
		TypeConverter

	{

		//To create the RoomConverter we need to override three methods from the TypeConverter class. The first of these is the CanConvert method. This simply indicates whether a specific type may be converted into our desired Room type. We know that the configuration values will always be strings so the method returns true for a source type of string and false for any other type. The code for the method is as follows:
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string);
		}
		//The second method to override is ConvertFrom. This converts a value to a Room object. Again, we only want to work with strings so we begin by checking that the value, held in the value parameter, is indeed a string. If it isn't we let the base class handle the process.

		//For strings, we will assume that rooms are held as a string containing the room number followed by a comma and the room location. For example, "1,Reception". We simply extract the two values from the string, apply them to the properties of a new Room instance and return the constructed object.

		public override object ConvertFrom(
			ITypeDescriptorContext context,
			System.Globalization.CultureInfo culture,
			object value
		)
		{
			if (value is string)
			{
				string[] parts = ((string)value).Split(new char[] { ',' });
				Room room = new Room();
				room.RoomNumber = Convert.ToInt32(parts[0]);
				room.Location = parts.Length > 1 ? parts[1] : null;
				return room;
			}
			return base.ConvertFrom(context, culture, value);
		}
		//The third method that must be overridden performs the reverse of the second, converting an object of the Room class to a string that can be stored as a configuration setting. In the ConvertTo method we again check that we are converting between our custom class and a string by checking the destinationType parameter. If the destination type is correct, we use the string.Format method to create the string containing the comma-separated room number and location.

		//Add the following method and compile the code. This build is required before the types can be used in the application settings grid.

		public override object ConvertTo(
			ITypeDescriptorContext context,
			System.Globalization.CultureInfo culture,
			object value, Type destinationType
		)
		{
			if (destinationType == typeof(string))
			{
				Room room = value as Room;
				return string.Format("{0},{1}", room.RoomNumber, room.Location);
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
}
//#endif
