using System;
using System.Collections;
using BP.DA;
using BP.Web.Controls;
using System.Reflection;
using BP.Port;
using BP.En;
namespace BP.WF
{
    /// <summary>
    /// �޸����ݿ� ��ժҪ˵��
    /// </summary>
    public class RepariDB : Method
    {
        /// <summary>
        /// �����в����ķ���
        /// </summary>
        public RepariDB()
        {
            this.Title = "�޸����ݿ�";
            this.Help = "�����µİ汾���뵱ǰ�����ݱ�ṹ����һ���Զ��޸�, �޸����ݣ�ȱ���У�ȱ����ע�ͣ���ע�Ͳ����������б仯��";
        }
        /// <summary>
        /// ����ִ�б���
        /// </summary>
        /// <returns></returns>
        public override void Init()
        {
            //this.Warning = "��ȷ��Ҫִ����";
            //HisAttrs.AddTBString("P1", null, "ԭ����", true, false, 0, 10, 10);
            //HisAttrs.AddTBString("P2", null, "������", true, false, 0, 10, 10);
            //HisAttrs.AddTBString("P3", null, "ȷ��", true, false, 0, 10, 10);
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
            string rpt = BP.PubClass.DBRpt(BP.DBLevel.High);

            // �ֶ�����. 2011-07-08 ����ڵ��ֶη���.
            string sql = "DELETE Sys_EnCfg WHERE No='BP.WF.Ext.NodeO'";
            BP.DA.DBAccess.RunSQL(sql);

            sql = "INSERT INTO Sys_EnCfg(No,GroupTitle) VALUES ('BP.WF.Ext.NodeO','NodeID=��������@WarningDays=��������@SendLab=���ܰ�ť��ǩ��״̬')";
            BP.DA.DBAccess.RunSQL(sql);

            //ɾ��������.
            sql = "DELETE Sys_Enum WHERE EnumKey='FormType'";
            BP.DA.DBAccess.RunSQLs(sql);

            return "ִ�гɹ�...";
        }
    }
}
