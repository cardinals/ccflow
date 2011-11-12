using System;
using System.Data;
using System.Collections;
using BP.DA;
using BP.Web.Controls;
using System.Reflection;
using BP.Port;
using BP.En;
namespace BP.WF
{
    /// <summary>
    /// �޸���������ֶγ��� ��ժҪ˵��
    /// </summary>
    public class RepariMapAttrMinLen : Method
    {
        /// <summary>
        /// �����в����ķ���
        /// </summary>
        public RepariMapAttrMinLen()
        {
            this.Title = "�޸���������ֶγ���";
            this.Help = "���磺һ����ע�ֶ�ԭ����Ƴ���Ϊ500�ַ�������һ��ʱ�����޸ĳ�2000�ַ������д˹��ܾͻ��Զ�����ֶγ��ȵ��޸���";
        }
        /// <summary>
        /// ����ִ�б���
        /// </summary>
        /// <returns></returns>
        public override void Init()
        {
        }
        /// <summary>
        /// ��ǰ�Ĳ���Ա�Ƿ����ִ���������
        /// </summary>
        public override bool IsCanDo
        {
            get
            {
                return true;
            }
        }
        /// <summary>
        /// ִ��
        /// </summary>
        /// <returns>����ִ�н��</returns>
        public override object Do()
        {
            string sql = "SELECT * FROM Sys_MapAttr WHERE MaxLen >200 AND MyDataType=1";
            string msg = "";
            DataTable dt = DBAccess.RunSQLReturnTable(sql);
            int idx = 0;
            foreach (DataRow dr in dt.Rows)
            {
                string fk_mapdata = dr["FK_MapData"].ToString();
                string field = dr["KeyOfEn"].ToString();
                string MaxLen = dr["MaxLen"].ToString();
                sql = "ALTER TABLE " + fk_mapdata + " ALTER COLUMN " + field + " varchar(" + MaxLen + ")";
                try
                {
                    DBAccess.RunSQL(sql);
                    idx++;
                }
                catch (Exception ex)
                {
                    msg += "@����:��:" + fk_mapdata + " , �ֶ�:" + field + ",����:" + MaxLen + ".<font color=red>" + ex.Message+"</font> @SQL="+sql;
                }
            }
            return "ִ�н��:�ɹ�ִ����" + idx + "����¼��<hr><font color=red>" + msg + "</font>";
        }
    }
}
