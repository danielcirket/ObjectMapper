using System;
using System.Reflection;

namespace DanielCirket.ObjectMapper
{
    public class Null
    {
        public static short NullShort
        {
            get
            {
                return -1;
            }
        }
        public static int NullInteger
        {
            get
            {
                return -1;
            }
        }
        public static byte NullByte
        {
            get
            {
                return 255;
            }
        }
        public static float NullSingle
        {
            get
            {
                return float.MinValue;
            }
        }
        public static double NullDouble
        {
            get
            {
                return double.MinValue;
            }
        }
        public static decimal NullDecimal
        {
            get
            {
                return decimal.MinValue;
            }
        }
        public static DateTime NullDate
        {
            get
            {
                return DateTime.MinValue;
            }
        }
        public static string NullString
        {
            get
            {
                return "";
            }
        }
        public static bool NullBoolean
        {
            get
            {
                return false;
            }
        }
        public static Guid NullGuid
        {
            get
            {
                return Guid.Empty;
            }
        }

        public static object SetNull(PropertyInfo objPropertyInfo)
        {
            object returnValue = null;
            switch (objPropertyInfo.PropertyType.ToString())
            {
                case "System.Int16":
                    returnValue = NullShort;
                    break;
                case "System.Int32":
                case "System.Int64":
                    returnValue = NullInteger;
                    break;
                case "system.Byte":
                    returnValue = NullByte;
                    break;
                case "System.Single":
                    returnValue = NullSingle;
                    break;
                case "System.Double":
                    returnValue = NullDouble;
                    break;
                case "System.Decimal":
                    returnValue = NullDecimal;
                    break;
                case "System.DateTime":
                    returnValue = NullDate;
                    break;
                case "System.String":
                case "System.Char":
                    returnValue = NullString;
                    break;
                case "System.Boolean":
                    returnValue = NullBoolean;
                    break;
                case "System.Guid":
                    returnValue = NullGuid;
                    break;
                default:
                    //Enumerations default to the first entry
                    Type pType = objPropertyInfo.PropertyType;
                    if (pType.BaseType.Equals(typeof(Enum)))
                    {
                        Array objEnumValues = Enum.GetValues(pType);
                        Array.Sort(objEnumValues);
                        returnValue = Enum.ToObject(pType, objEnumValues.GetValue(0));
                    }
                    else //complex object
                    {
                        returnValue = null;
                    }
                    break;
            }
            return returnValue;
        }
        public static object GetNull(object objField, object objDBNull)
        {
            object returnValue = objField;
            if (objField == null)
            {
                returnValue = objDBNull;
            }
            else if (objField is byte)
            {
                if (Convert.ToByte(objField) == NullByte)
                {
                    returnValue = objDBNull;
                }
            }
            else if (objField is short)
            {
                if (Convert.ToInt16(objField) == NullShort)
                {
                    returnValue = objDBNull;
                }
            }
            else if (objField is int)
            {
                if (Convert.ToInt32(objField) == NullInteger)
                {
                    returnValue = objDBNull;
                }
            }
            else if (objField is float)
            {
                if (Convert.ToSingle(objField) == NullSingle)
                {
                    returnValue = objDBNull;
                }
            }
            else if (objField is double)
            {
                if (Convert.ToDouble(objField) == NullDouble)
                {
                    returnValue = objDBNull;
                }
            }
            else if (objField is decimal)
            {
                if (Convert.ToDecimal(objField) == NullDecimal)
                {
                    returnValue = objDBNull;
                }
            }
            else if (objField is DateTime)
            {
                // compare the Date part of the DateTime with the DatePart of the NullDate ( this avoids subtle time differences )
                if (Convert.ToDateTime(objField).Date == NullDate.Date)
                {
                    returnValue = objDBNull;
                }
            }
            else if (objField is string)
            {
                if (objField == null)
                {
                    returnValue = objDBNull;
                }
                else
                {
                    if (objField.ToString() == NullString)
                    {
                        returnValue = objDBNull;
                    }
                }
            }
            else if (objField is bool)
            {
                if (Convert.ToBoolean(objField) == NullBoolean)
                {
                    returnValue = objDBNull;
                }
            }
            else if (objField is Guid)
            {
                if (((Guid)objField).Equals(NullGuid))
                {
                    returnValue = objDBNull;
                }
            }
            return returnValue;
        }
    }
}
