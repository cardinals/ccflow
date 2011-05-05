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
    /// Method ��ժҪ˵��
    /// </summary>
    public class MDCheck : Method
    {
        /// <summary>
        /// �����в����ķ���
        /// </summary>
        public MDCheck()
        {
            this.Title = "�޸����еĽڵ����ݡ�";
            this.Help = "���ڽڵ���������";
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
            Nodes nds = new Nodes();
            nds.RetrieveAll();
            // ����ִ�ж��Ρ�
            foreach (Node nd in nds)
                nd.RepariMap();

            foreach (Node nd in nds)
                nd.Update();

            // ����ִ�ж��Ρ�
            foreach (Node nd in nds)
                nd.Update();

            foreach (Node nd in nds)
            {
                

                Work wk = nd.HisWork;
                wk.CheckPhysicsTable();

                Sys.MapDtls dtls = new BP.Sys.MapDtls("ND" + wk.NodeID);
                foreach (Sys.MapDtl dtl in dtls)
                {
                    dtl.HisGEDtl.CheckPhysicsTable();
                }
            }
            return "ִ�гɹ�";
        }
    }
}
