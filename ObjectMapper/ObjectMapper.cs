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

        public static ICacheHelper CacheHelper
        {
            get
            {
                return _cacheHelper ?? new CacheHelper();
            }
            set
            {
                _cacheHelper = value;
            }
        }

        #endregion

        #region Methods

        public static T FillObject<T>(object obj)
        {
            return (T)Convert.ChangeType(obj, typeof(T));
        }

        public static T FillObject<T>(IDataReader datareader) where T : class, new()
        {
            T fillObject;

            bool shouldContinue = false;

            if (datareader != null && datareader.Read())
            {
                shouldContinue = true;
            }

            if (shouldContinue)
            {
                fillObject = CreateObject<T>(datareader);
            }
            else
            {
                fillObject = default(T);
            }

            if (datareader != null)
            {
                datareader.Close();
            }

            return fillObject;
        }
        public static T FillObject<T>(IDataReader datareader, Action<T> callback) where T : class, new()
        {
            var result = FillObject<T>(datareader);

            if (result != null)
                callback(result);

            return result;
        }
        public static T FillObject<T>(IDataReader datareader, T instance, bool overwriteExstingProperties) where T : class, new()
        {
            T fillObject;

            bool shouldContinue = false;

            if (datareader != null && datareader.Read())
            {
                shouldContinue = true;
            }

            if (shouldContinue)
            {
                fillObject = CreateObject<T>(datareader, instance, overwriteExstingProperties);
            }
            else
            {
                fillObject = default(T);
            }

            if (datareader != null)
            {
                datareader.Close();
            }

            return fillObject;
        }
        public static T FillObject<T>(IDataReader datareader, T instance, bool overwriteExstingProperties, Action<T> callback) where T : class, new()
        {
            var result = FillObject<T>(datareader, instance, overwriteExstingProperties);

            if (result != null)
                callback(result);
            
            return result;
        }

        public static T FillObject<T>(DataTable dataTable) where T : class, new()
        {
            return FillObject<T>(dataTable.CreateDataReader());
        }
        public static T FillObject<T>(DataTable dataTable, Action<T> callback) where T : class, new()
        {
            var result = FillObject<T>(dataTable.CreateDataReader());

            if (result != null)
                callback(result);

            return result;
        }
        public static T FillObject<T>(DataTable dataTable, T instance, bool overwriteExstingProperties) where T : class, new()
        {
            return FillObject<T>(dataTable.CreateDataReader(), instance, overwriteExstingProperties);
        }
        public static T FillObject<T>(DataTable dataTable, T instance, bool overwriteExstingProperties, Action<T> callback) where T : class, new()
        {
            var result = FillObject<T>(dataTable.CreateDataReader(), instance, overwriteExstingProperties);

            if (result != null)
                callback(result);

            return result;
        }

        public static List<T> FillCollection<T>(IDataReader datareader) where T : class, new()
        {
            List<T> listObjects = new List<T>();

            if (datareader != null)
            {
                while (datareader.Read())
                {
                    T fillObject = CreateObject<T>(datareader);

                    listObjects.Add(fillObject);
                }

                datareader.Close();
            }

            return listObjects;
        }
        public static List<T> FillCollection<T>(IDataReader datareader, Action<T> callback) where T : class, new()
        {
            List<T> listObjects = new List<T>();

            if (datareader != null)
            {
                while (datareader.Read())
                {
                    T fillObject = CreateObject<T>(datareader);

                    if (fillObject != null)
                        callback(fillObject);

                    listObjects.Add(fillObject);
                }

                datareader.Close();
            }

            return listObjects;
        }

        public static List<T> FillCollection<T>(DataTable dataTable) where T : class, new()
        {
            return FillCollection<T>(dataTable.CreateDataReader());
        }
        public static List<T> FillCollection<T>(DataTable dataTable, Action<T> callback) where T : class, new()
        {
            return FillCollection<T>(dataTable.CreateDataReader(), callback);
        }

        private static T CreateObject<T>(IDataReader datareader, T instance = null, bool overwriteProperties = false) where T : class, new()
        {
            Type objectPropertyType = null;

            T createObject = instance;

            if (createObject == null)
                createObject = new T();

            // get properties for type
            List<PropertyInfo> objProperties = GetPropertyInfo(createObject.GetType());

            // get ordinal positions in datareader
            int[] ordinals = GetOrdinals(objProperties, datareader);

            // fill object with values from 
            for (int i = 0; i <= objProperties.Count - 1; i++)
            {
                PropertyInfo objPropertyInfo = (PropertyInfo)objProperties[i];

                if (objPropertyInfo.CanWrite)
                {
                    object objectValue = Null.SetNull(objPropertyInfo);

                    // Check if object passed to populate.
                    if (instance != null && objPropertyInfo.CanRead)
                    {
                        // Check if the property is set (Not NullValue).
                        var currentValue = Convert.ChangeType(objPropertyInfo.GetValue(instance), objPropertyInfo.PropertyType);

                        // Get type.
                        var propType = objPropertyInfo.PropertyType;

                        // Get the default value
                        var defaultValue = objPropertyInfo.PropertyType.IsValueType 
                            ? Convert.ChangeType(Activator.CreateInstance(objPropertyInfo.PropertyType), objPropertyInfo.PropertyType) 
                            : null;

                        if (!object.Equals(currentValue, defaultValue) && !overwriteProperties)
                            continue;
                    }

                    if (ordinals[i] != -1)
                    {
                        if (datareader.GetValue(ordinals[i]) == DBNull.Value)
                        {
                            // translate Null value
                            objPropertyInfo.SetValue(createObject, objectValue, null);
                        }
                        else
                        {
                            try
                            {
                                // try implicit conversion first
                                objPropertyInfo.SetValue(createObject, datareader.GetValue(ordinals[i]), null);
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
                                        if (Information.IsNumeric(datareader.GetValue(ordinals[i])))
                                        {
                                            ((PropertyInfo)objProperties[i]).SetValue(createObject, Enum.ToObject(objectPropertyType, Convert.ToInt32(datareader.GetValue(ordinals[i]))), null);
                                        }
                                        else
                                        {
                                            ((PropertyInfo)objProperties[i]).SetValue(createObject, Enum.ToObject(objectPropertyType, datareader.GetValue(ordinals[i])), null);
                                        }
                                    }
                                    else
                                    {
                                        // try explicit conversion.
                                        objPropertyInfo.SetValue(createObject, Convert.ChangeType(datareader.GetValue(ordinals[i]), objectPropertyType), null);
                                    }
                                }
                                catch (Exception)
                                {
                                    objPropertyInfo.SetValue(createObject, Convert.ChangeType(datareader.GetValue(ordinals[i]), objectPropertyType), null);
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
        private static int[] GetOrdinals(List<PropertyInfo> objProperties, IDataReader datareader)
        {
            int[] listOrdinals = new int[objProperties.Count + 1];

            if (datareader != null)
            {
                for (int i = 0; i < objProperties.Count; i++)
                {
                    listOrdinals[i] = -1;
                    try
                    {
                        listOrdinals[i] = datareader.GetOrdinal(((PropertyInfo)objProperties[i]).Name);
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
