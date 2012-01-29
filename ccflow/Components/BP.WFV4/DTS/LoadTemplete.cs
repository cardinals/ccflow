using System;
using System.Data;
using System.Collections;
using BP.DA;
using BP.Web.Controls;
using System.Reflection;
using BP.Port;
using BP.En;
using BP.Sys;
namespace BP.WF.DTS
{
    /// <summary>
    /// Method ��ժҪ˵��
    /// </summary>
    public class LoadTemplete : Method
    {
        /// <summary>
        /// �����в����ķ���
        /// </summary>
        public LoadTemplete()
        {
            this.Title = "װ��������ʾģ��";
            this.Help = "Ϊ�˰�����λ������ѧϰ������ccflow, ���ṩһЩ����ģ�����ģ���Է���ѧϰ��";
            this.Help += "@��Щģ���λ��" + SystemConfig.PathOfData + "\\FlowDemo\\";
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
        public override object Do()
        {
            string msg = "";
            string flowPath = SystemConfig.PathOfData + "\\FlowDemo\\Flow\\";
            string[] fls = System.IO.Directory.GetFiles(flowPath);

            string fk_flowsort = "01";
            //DBAccess.RunSQLReturnString("SELECT No FROM WF_FlowSort");

            // ���ȱ��ļ���
            foreach (string f in fls)
            {
                try
                {
                    msg += "@��ʼ�����ļ�:" + f;
                    Flow myflow = BP.WF.Flow.DoLoadFlowTemplate(fk_flowsort, f);
                    msg += "@����:" + myflow.Name + "װ�سɹ���";
                    System.IO.FileInfo info = new System.IO.FileInfo(f);
                    myflow.Name = info.Name.Replace(".xml","");
                    myflow.DirectUpdate();
                }
                catch (Exception ex)
                {
                    msg += "@����ʧ��" + ex.Message;
                }
            }

 
            // ���ȱ��ļ���
            flowPath = SystemConfig.PathOfData + "\\FlowDemo\\Form\\";
            fls = System.IO.Directory.GetFiles(flowPath);
            foreach (string f in fls)
            {
                try
                {
                    msg += "@��ʼ���ȱ�ģ���ļ�:" + f;
                    System.IO.FileInfo info = new System.IO.FileInfo(f);
                    if (info.Extension != ".xml")
                        continue;
                    DataSet ds = new DataSet();
                    ds.ReadXml(f);
                    MapData.ImpMapData(ds);
                }
                catch (Exception ex)
                {
                    msg += "@����ʧ��" + ex.Message;
                }
            }
            return msg;
        }
        /// <summary>
        /// ִ��
        /// </summary>
        /// <returns>����ִ�н��</returns>
        public   object Do11()
        {
            #region 2011-08-02 ������ mapdata �����б仯.
            Nodes nds = new Nodes();
            nds.RetrieveAll();
            foreach (Node nd in nds)
            {
                MapData md = new MapData();
                md.No = "ND" + nd.NodeID;
                if (md.RetrieveFromDBSources() == 0)
                {
                    md.Name = nd.Name;
                    md.EnPK = "OID";
                    md.PTable = md.No;
                    md.Insert();
                }
                else
                {
                    md.Name = nd.Name;
                    md.EnPK = "OID";
                    md.PTable = md.No;
                    md.Update();
                }
            }
            MapDtls dtls = new MapDtls();
            dtls.RetrieveAll();
            foreach (MapDtl dtl in dtls)
            {
                MapData md = new MapData();
                md.No = dtl.No;
                if (md.RetrieveFromDBSources() == 0)
                {
                    md.Name = dtl.Name;
                    md.EnPK = "OID";
                    md.PTable = dtl.PTable;
                    md.Insert();
                }
                else
                {
                    md.Name = dtl.Name;
                    md.EnPK = "OID";
                    md.PTable = dtl.PTable;
                    md.Update();
                }
            }
            #endregion 2011-08-02 ������ mapdata �����б仯.

            return "ִ�гɹ�...";
        }
    }
}
