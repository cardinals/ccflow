using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using BP.WF;
using BP.Sys;

namespace BP.Web.WF.WF
{
	/// <summary>
	/// DeleteZF ��ժҪ˵����
	/// </summary>
    partial class DeleteZF : WebPage
    {

        private void Page_Load(object sender, System.EventArgs e)
        {
            if (WebUser.No != "admin")
            {
                this.ToErrorPage("�Ƿ��û���");
                return;
            }
        }

        #region Web ������������ɵĴ���
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: �õ����� ASP.NET Web ���������������ġ�
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        /// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
        /// �˷��������ݡ�
        /// </summary>
        private void InitializeComponent()
        {
            this.Button1.Click += new System.EventHandler(this.Button1_Click);
            this.Load += new System.EventHandler(this.Page_Load);
        }
        #endregion

        private void Button1_Click(object sender, System.EventArgs e)
        {
            if (WebUser.No != "admin")
            {
                this.ToErrorPage("�Ƿ��û���");
                return;
            }

        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            if (WebUser.No != "admin")
            {
                this.ToErrorPage("�Ƿ��û���");
                return;
            }

            DA.DBAccess.RunSQL("delete WF_CHOfNode");
            DA.DBAccess.RunSQL("delete WF_CHOfFlow");
            DA.DBAccess.RunSQL("delete WF_Book");
            DA.DBAccess.RunSQL("delete WF_GENERWORKERLIST");
            DA.DBAccess.RunSQL("delete WF_GENERWORKFLOW");
            DA.DBAccess.RunSQL("delete WF_WORKLIST");
            DA.DBAccess.RunSQL("delete WF_ReturnWork");
            DA.DBAccess.RunSQL("delete WF_ReturnWork");
            DA.DBAccess.RunSQL("delete WF_SelectAccper");

            Nodes nds = new Nodes();
            foreach (Node nd in nds)
            {
                try
                {
                    Work wk = nd.HisWork;
                    DA.DBAccess.RunSQL("DELETE " + wk.EnMap.PhysicsTable);
                }
                catch
                {
                }
            }

            MapDatas mds = new MapDatas();
            mds.RetrieveAll();
            foreach (MapData nd in mds)
            {
                try
                {
                    DA.DBAccess.RunSQL("DELETE " + nd.PTable );
                }
                catch
                {
                }
            }

            MapDtls dtls = new MapDtls();
            dtls.RetrieveAll();
            foreach (MapDtl dtl in dtls)
            {
                try
                {
                    DA.DBAccess.RunSQL("DELETE " + dtl.PTable);
                }
                catch
                {
                }
            }

            this.Alert("clear ok.");

        }
        protected void Button3_Click(object sender, EventArgs e)
        {

            if (WebUser.No != "admin")
            {
                this.ToErrorPage("�Ƿ��û���");
                return;
            }



        }
}
}
