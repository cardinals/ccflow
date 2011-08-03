using System;
using System.Collections;
using BP.DA;
using BP.Web.Controls;
using System.Reflection;
using BP.Port;
using BP.En;
using BP.Sys;
namespace BP.WF
{
    /// <summary>
    /// Method ��ժҪ˵��
    /// </summary>
    public class Pack : Method
    {
        /// <summary>
        /// �����в����ķ���
        /// </summary>
        public Pack()
        {
            this.Title = "ccflow ������";
            this.Help = "ccflow����Ʊ仯������������ݴ洢�ṹ�����仯��Ϊ�˿�������ʷ�汾���ݣ��뼰ʱ�����°汾�Ĳ�����";
            this.Help += "@2011-08-02 ���������ϰ汾�����̱���֧�֡�";
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
