﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BP.En;
using BP.DA;
using System.Configuration;

namespace BP.CCOA
{
    public partial class XQueryTool
    {

        public static DataTable Query<T>(T entity, string[] columnNames,
            string value, int pageIndex, int pageSize,
            IDictionary<string, object> whereValues = null,
            string rowNumFieldName = "No") where T : EntityNoName
        {
            XQueryToolBase queryTool = GetQueryTool();
            return queryTool.Query<T>(entity, columnNames, value, pageIndex, pageSize, whereValues, rowNumFieldName);
        }

        private static XQueryToolBase GetQueryTool()
        {
            string m_DataBaseType = ConfigurationManager.AppSettings["AppCenterDBType"].ToUpper();
            XQueryToolBase queryTool = null;
            switch (m_DataBaseType)
            {
                case "ORACLE":
                    queryTool = new XOracleQueryTool();
                    break;
                default:
                    queryTool = new XSqlServerQueryTool();
                    break;
            }
            return queryTool;
        }

        public static int GetRowCount<T>(T entity, string[] columnNames,
            string value,
            IDictionary<string, object> whereValues = null,
            string rowNumFieldName = "No") where T : EntityNoName
        {
            XQueryToolBase queryTool = GetQueryTool();
            return queryTool.GetRowCount<T>(entity, columnNames, value, whereValues, rowNumFieldName);
        }
    }
}
