using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace OracleLibaryQuery.Collections
{
    public static class FillCollection<T>
    {
        public static List<T> FillCollectionFromDataSet(DataSet ds)
        {
            if (ds.Tables.Count <= 0) return new List<T>();

            List<T> _list_T = new List<T>();

            // get properties for type
            Hashtable objProperties = GetPropertyInfo(typeof(T));

            // get ordinal positions in datareader
            Hashtable arrOrdinals = GetOrdinalsFromDataSet(objProperties, ds);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                // fill business object
                T objFillObject = (T)CreateObjectFromDataSet(typeof(T), dr, objProperties, arrOrdinals);

                // add to collection
                _list_T.Add(objFillObject);
            }

            return _list_T;

        }

        public static List<T> FillCollectionFromDataTable(DataTable dt)
        {
            List<T> _list_T = new List<T>();

            Hashtable objProperties = GetPropertyInfo(typeof(T));

            Hashtable arrOrdinals = GetOrdinalsFromDataTable(objProperties, dt);

            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    // fill business object
                    T objFillObject = (T)CreateObjectFromDataSet(typeof(T), dr, objProperties, arrOrdinals);

                    // add to collection
                    _list_T.Add(objFillObject);
                }
            }
            return _list_T;

        }

        public static T FillObjectFromDataSet(DataSet ds)
        {

            try
            {
                // get properties for type
                Hashtable objProperties = GetPropertyInfo(typeof(T));

                // get ordinal positions in datareader
                Hashtable arrOrdinals = GetOrdinalsFromDataSet(objProperties, ds);

                // read datareader
                if (ds.Tables[0].Rows.Count > 0)
                {
                    // fill business object
                    T _objResult = (T)CreateObjectFromDataSet(typeof(T), ds.Tables[0].Rows[0], objProperties, arrOrdinals);
                    return _objResult;
                }

                return default(T);
            }
            catch
            {
                return default(T);
            }
        }

        public static T FillObjectFromDataTable(DataTable dt)
        {

            try
            {
                // get properties for type
                Hashtable objProperties = GetPropertyInfo(typeof(T));

                // get ordinal positions in datareader
                Hashtable arrOrdinals = GetOrdinalsFromDataTable(objProperties, dt);

                // read datareader
                if (dt.Rows.Count > 0)
                {
                    // fill business object
                    T _objResult = (T)CreateObjectFromDataSet(typeof(T), dt.Rows[0], objProperties, arrOrdinals);
                    return _objResult;
                }

                return default(T);
            }
            catch
            {
                return default(T);
            }
        }

        public static DataTable ConvertToDataTable(IList<T> data)
        {
            try
            {
                PropertyDescriptorCollection properties =
                        TypeDescriptor.GetProperties(typeof(T));

                DataTable table = new DataTable();

                foreach (PropertyDescriptor prop in properties)
                    table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);

                foreach (T item in data)
                {
                    DataRow row = table.NewRow();
                    foreach (PropertyDescriptor prop in properties)
                    {
                        row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                    }
                    table.Rows.Add(row);
                }
                return table;
            }
            catch (Exception)
            {
                return new DataTable();
            }
        }

        #region private

        /// <summary>
        /// Tạo một đối tượng từ datarow
        /// </summary>
        /// <param ></param>
        /// <param ></param>
        /// <param ></param>
        /// <param ></param>
        /// <returns></returns>
        private static object CreateObjectFromDataSet(Type objType, DataRow dr, Hashtable objProperties, Hashtable arrOrdinals)
        {
            string _fieldname = String.Empty;
            try
            {
                object objObject = Activator.CreateInstance(objType);


                int _possition = -1;
                foreach (DictionaryEntry de in arrOrdinals)
                {
                    _fieldname = de.Key.ToString();
                    _possition = (int)arrOrdinals[_fieldname];
                    PropertyInfo _PropertyInfo = (PropertyInfo)objProperties[_fieldname.Replace("_",String.Empty)];

                    if (_PropertyInfo.CanWrite)
                    {
                        if (_possition == -1 || dr[_possition] == System.DBNull.Value)
                        {
                            //LinhNN sua 16/12/2013: không cần vì mặc định Activator.CreateInstance khi tạo đã set các thuộc tính về null
                            //   _PropertyInfo.SetValue(objObject, Null.SetNull(_PropertyInfo), null);
                        }
                        else
                        {
                            #region
                            switch (_PropertyInfo.PropertyType.FullName)
                            {
                                case "System.Enum":
                                    _PropertyInfo.SetValue(objObject, System.Enum.ToObject(_PropertyInfo.PropertyType, dr[_possition]), null);
                                    break;
                                case "System.String":
                                    _PropertyInfo.SetValue(objObject, (string)dr[_possition], null);
                                    break;
                                case "System.Boolean":
                                    _PropertyInfo.SetValue(objObject, (Boolean)dr[_possition], null);
                                    break;
                                case "System.Decimal":
                                    _PropertyInfo.SetValue(objObject, Convert.ToDecimal(dr[_possition]), null);
                                    break;
                                case "System.Int16":
                                    _PropertyInfo.SetValue(objObject, Convert.ToInt16(dr[_possition]), null);
                                    break;
                                case "System.Int32":
                                    _PropertyInfo.SetValue(objObject, Convert.ToInt32(dr[_possition]), null);
                                    break;
                                case "System.Int64":
                                    _PropertyInfo.SetValue(objObject, Convert.ToInt64(dr[_possition]), null);
                                    break;
                                case "System.DateTime":
                                    _PropertyInfo.SetValue(objObject, Convert.ToDateTime(dr[_possition]), null);
                                    break;
                                case "System.Double":
                                    _PropertyInfo.SetValue(objObject, Convert.ToDouble(dr[_possition]), null);
                                    break;
                                case "System.Guid":
                                    _PropertyInfo.SetValue(objObject, new Guid(dr[_possition].ToString()), null);
                                    break;
                                default:
                                    // try explicit conversion
                                    _PropertyInfo.SetValue(objObject, Convert.ChangeType(dr[_possition], _PropertyInfo.PropertyType), null);
                                    break;
                            }
                            #endregion
                        }
                    }
                }

                return objObject;
            }
            catch
            {
                return Activator.CreateInstance(objType);
            }
        }

        /// <summary>
        /// Lấy tên thuộc tính của 1 kiểu dữ liệu
        private static Hashtable GetPropertyInfo(Type objType)
        {
            Hashtable hashProperties = new Hashtable();
            foreach (PropertyInfo objProperty in objType.GetProperties())
            {
                hashProperties[objProperty.Name.ToUpper()] = objProperty;
            }
            return hashProperties;
        }

        /// <summary>
        /// Gán thứ tự cột trong dataset theo tên thuộc tính
        /// </summary>
        private static Hashtable GetOrdinalsFromDataSet(Hashtable hashProperties, DataSet dt)
        {
            Hashtable arrOrdinals = new Hashtable();

            if ((dt.Tables.Count > 0))
            {

                for (int i = 0; i < dt.Tables[0].Columns.Count; i++)
                {
                    string nameColumn = Regex.Replace(dt.Tables[0].Columns[i].ColumnName.ToUpper(), @"[^a-zA-Z0-9]", string.Empty);
                    if (hashProperties.ContainsKey(nameColumn))
                        arrOrdinals[dt.Tables[0].Columns[i].ColumnName.ToUpper()] = i;
                }
            }
            return arrOrdinals;
        }

        /// <summary>
        /// Gán thứ tự cột trong dataTable theo tên thuộc tính
        /// </summary>
        private static Hashtable GetOrdinalsFromDataTable(Hashtable hashProperties, DataTable dt)
        {
            Hashtable arrOrdinals = new Hashtable();

            for (int i = 0; i < hashProperties.Count; i++)
            {
                string nameColumn = Regex.Replace(dt.Columns[i].ColumnName.ToUpper(), @"[^a-zA-Z0-9]", string.Empty);
                if (hashProperties.ContainsKey(nameColumn))
                    arrOrdinals[dt.Columns[i].ColumnName.ToUpper()] = i;
            }

            return arrOrdinals;
        }

        #endregion
    }

}
