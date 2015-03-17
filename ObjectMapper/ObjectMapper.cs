using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace DanielCirket.ObjectMapper
{
    /// <summary>
    /// Hydrates objects with data from a datareader
    /// 
    /// NOTE: Does not work for complex objects (e.g. ArrayLists, HashTables, Custom Objects etc).
    /// IF complex objects need to be filled, more custom code will need to be written.
    /// </summary>
    public class ObjectMapper
    {
        #region Fields

        private static ICacheHelper _cacheHelper = null;

        #endregion

        #region Properties

        private static ICacheHelper CacheHelper
        {
            get
            {
                return _cacheHelper ?? new CacheHelper();
            }
        }

        #endregion

        #region Methods

        public static T FillObject<T>(object obj)
        {
            return (T)Convert.ChangeType(obj, typeof(T));
        }
        public static T FillObject<T>(IDataReader dataReader)
        {
            T fillObject;

            bool shouldContinue = false;

            if (dataReader != null && dataReader.Read())
            {
                shouldContinue = true;
            }

            if (shouldContinue)
            {
                fillObject = CreateObject<T>(dataReader);
            }
            else
            {
                fillObject = default(T);
            }

            if (dataReader != null)
            {
                dataReader.Close();
            }

            return fillObject;
        }
        public static T FillObject<T>(DataTable dataTable)
        {
            return FillObject<T>(dataTable.CreateDataReader());
        }

        public static List<T> FillCollection<T>(IDataReader dataReader)
        {
            List<T> listObjects = new List<T>();

            if (dataReader != null)
            {
                while (dataReader.Read())
                {
                    T fillObject = CreateObject<T>(dataReader);

                    listObjects.Add(fillObject);
                }

                dataReader.Close();
            }

            return listObjects;
        }
        public static List<T> FillCollection<T>(DataTable dataTable)
        {
            return FillCollection<T>(dataTable.CreateDataReader());
        }

        private static T CreateObject<T>(IDataReader dataReader)
        {
            Type objectPropertyType = null;

            T createObject = Activator.CreateInstance<T>();

            // get properties for type
            List<PropertyInfo> objProperties = GetPropertyInfo(createObject.GetType());

            // get ordinal positions in datareader
            int[] ordinals = GetOrdinals(objProperties, dataReader);

            // fill object with values from 
            for (int i = 0; i <= objProperties.Count - 1; i++)
            {
                PropertyInfo objPropertyInfo = (PropertyInfo)objProperties[i];

                if (objPropertyInfo.CanWrite)
                {
                    object objectValue = Null.SetNull(objPropertyInfo);

                    if (ordinals[i] != -1)
                    {
                        if (dataReader.GetValue(ordinals[i]) == DBNull.Value)
                        {
                            // translate Null value
                            objPropertyInfo.SetValue(createObject, objectValue, null);
                        }
                        else
                        {
                            try
                            {
                                // try implicit conversion first
                                objPropertyInfo.SetValue(createObject, dataReader.GetValue(ordinals[i]), null);
                            }
                            catch (Exception)
                            {
                                // business object info class member data type does not match datareader member data type
                                try
                                {
                                    objectPropertyType = objPropertyInfo.PropertyType;

                                    //need to handle enumeration conversions differently than other base types
                                    if (objectPropertyType.BaseType.Equals(typeof(Enum)))
                                    {
                                        // check if value is numeric and if not convert to integer
                                        if (Information.IsNumeric(dataReader.GetValue(ordinals[i])))
                                        {
                                            ((PropertyInfo)objProperties[i]).SetValue(createObject, Enum.ToObject(objectPropertyType, Convert.ToInt32(dataReader.GetValue(ordinals[i]))), null);
                                        }
                                        else
                                        {
                                            ((PropertyInfo)objProperties[i]).SetValue(createObject, Enum.ToObject(objectPropertyType, dataReader.GetValue(ordinals[i])), null);
                                        }
                                    }
                                    else
                                    {
                                        // try explicit conversion.
                                        objPropertyInfo.SetValue(createObject, Convert.ChangeType(dataReader.GetValue(ordinals[i]), objectPropertyType), null);
                                    }
                                }
                                catch (Exception)
                                {
                                    objPropertyInfo.SetValue(createObject, Convert.ChangeType(dataReader.GetValue(ordinals[i]), objectPropertyType), null);
                                }
                            }
                        }
                    }
                    else
                    {
                        // property doesn't exist, do nothing.
                    }
                }
            }

            return createObject;
        }

        private static List<PropertyInfo> GetPropertyInfo(Type objectType)
        {
            List<PropertyInfo> objectProperties = CacheHelper.GetFromCache<List<PropertyInfo>>(objectType.FullName);

            if (objectProperties == null)
            {
                objectProperties = new List<PropertyInfo>();

                foreach (PropertyInfo objectProperty in objectType.GetProperties())
                {
                    objectProperties.Add(objectProperty);
                }

                CacheHelper.SaveToCache(objectType.FullName, objectProperties, DateTime.Now.AddMinutes(10));
            }

            return objectProperties;
        }
        private static int[] GetOrdinals(List<PropertyInfo> objProperties, IDataReader dataReader)
        {
            int[] listOrdinals = new int[objProperties.Count + 1];

            if (dataReader != null)
            {
                for (int i = 0; i < objProperties.Count; i++)
                {
                    listOrdinals[i] = -1;
                    try
                    {
                        listOrdinals[i] = dataReader.GetOrdinal(((PropertyInfo)objProperties[i]).Name);
                    }
                    catch (Exception)
                    {
                        // property does not exist in datareader                        
                    }
                }
            }

            return listOrdinals;
        }

        #endregion
    }
}
