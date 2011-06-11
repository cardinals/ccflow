using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using BP.En.Base;
using BP.WF;
using BP.Port;
using BP.En;
using BP.DTS;
using BP.Tax;
using BP.DA;

namespace BP.WFV2.DTS
{
    public class Tmp : DataIOEn
    {
        public Tmp()
        {
            this.HisDoType = DoType.UnName;
            this.Title = "��ʱ����,���Emps��������ݡ�";
            this.HisRunTimeType = RunTimeType.UnName;
            this.FromDBUrl = DBUrlType.AppCenterDSN;
            this.ToDBUrl = DBUrlType.AppCenterDSN;
        }
        public override void Do()
        {
            string sql = "";
            string sql2 = "";
            Nodes nds = new Nodes();
            nds.RetrieveAll();
            foreach (Node nd in nds)
            {
                try
                {
                    sql = "SELECT COUNT(*) FROM " + nd.PTable + " Where emps like '200%' or emps Is null or emps='' ";
                    int i = DBAccess.RunSQLReturnValInt(sql);
                    if (i == 0)
                        continue;
                    Log.DefaultLogWriteLineInfo("@�ڵ�" + nd.FK_Flow + "" + nd.FlowName + "�д�sql= " + sql);
                }
                catch
                {

                }
            }
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public class Tmp2 : DataIOEn
    {
        public Tmp2()
        {
            this.HisDoType = DoType.UnName;
            this.Title = "�ֹ�ע����˰��(����ע��ʧ�ܣ���Ҫ�ֹ�ע���ĵ���,ϵͳ��ִ�н��д����־�ļ���)";
            this.HisRunTimeType = RunTimeType.UnName;
            this.FromDBUrl = DBUrlType.AppCenterDSN;
            this.ToDBUrl = DBUrlType.AppCenterDSN;
        }

        public override void Do()
        {
            string sql = "select fk_taxpayer, taxpayerName, recorder, fk_xj from nd_20100 where wfstate=1 and fk_taxpayer in (select no from ds_taxpayer) order by recorder ";
            DataTable dt = DBAccess.RunSQLReturnTable(sql);

            Log.DefaultLogWriteLineInfo("��["+dt.Rows.Count+"]���쳣��");


            foreach (DataRow dr in dt.Rows)
            {
                string fk_taxpayer = dr["fk_taxpayer"].ToString();
                string taxpayerName = dr["taxpayerName"].ToString();
                string recorder = dr["recorder"].ToString();

                try
                {
                    Log.DefaultLogWriteLineInfo("��ʼִ��ע��:" + fk_taxpayer + "," + taxpayerName);

                    Paras ps = new Paras();
                    ps.Add("UserNo", recorder); //�����Ҫ���û���ţ��ͼ��ϴ�����
                    ps.Add("DoWhat", "ZX");       //���DoWhat����
                    ps.Add("FK_Taxpayer", fk_taxpayer); //���DoWhat����.
                    DBAccess.RunSP("DSBM.DealBuess", ps);
                }
                catch (Exception ex)
                {
                    Log.DefaultLogWriteLineInfo("ע��:" + fk_taxpayer + "," + taxpayerName + "ʧ��" + ex.Message);
                }
            }
        }
    }
}
