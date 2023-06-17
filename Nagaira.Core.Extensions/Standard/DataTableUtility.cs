using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace Nagaira.Core.Extentions.Standard
{
    public static class DataTableUtility
    {
        /// <summary>
        /// It allows obtaining the first record of a DataTable converted to the entity(abstract data type) specified.
        /// </summary>
        /// <typeparam name="DataTable"></typeparam>
        /// <param name="dataTable"></param>
        /// <returns name="T"></returns>
        public static T? DataTableToObjetFirstOrNull<T>(this DataTable dataTable) where T : class
        {
            if (dataTable == null) return null;
            if (dataTable.Rows.Count == 0) return null;

            DataRow row = dataTable.Rows[0];
            T item = GetItemFromDataRow<T>(row);

            return item;
        }

        /// <summary>
        /// It allows obtaining the record of a DataRow converted into the entity (abstract data type) specified.
        /// </summary>
        /// <typeparam name="DataTable"></typeparam>
        /// <param name="dataTable"></param>
        /// <returns name="T"></returns>
        private static T GetItemFromDataRow<T>(DataRow dataRow)
        {
            Type temporalObject = typeof(T);
            T objectInstance = Activator.CreateInstance<T>();

            foreach (DataColumn column in dataRow.Table.Columns)
            {
                foreach (PropertyInfo property in temporalObject.GetProperties())
                {
                    if (property.Name == column.ColumnName)
                        property.SetValue(objectInstance, DBNull.Value == dataRow[column.ColumnName] ? null : dataRow[column.ColumnName], null);

                }
            }

            return objectInstance;
        }

        /// <summary>
        /// Allows you to convert a DataTable to a list of objects of the type entity(Abstract Data Type) specified.
        /// </summary>
        /// <typeparam name="DataTable"></typeparam>
        /// <param name="dataTable"></param>
        /// <returns name="T"></returns>
        public static List<T> DataTableToObject<T>(this DataTable dataTable)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dataTable.Rows)
            {
                T itemData = GetItemFromDataRow<T>(row);
                data.Add(itemData);
            }
            return data;
        }
    }
}
