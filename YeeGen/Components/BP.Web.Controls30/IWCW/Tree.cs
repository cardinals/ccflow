using System;
using System.Data;
using Microsoft.Web.UI.WebControls;
using System.Web.UI.WebControls;
using BP.En;
using System.ComponentModel;
using BP.DA;
using Microsoft.Web.UI.WebControls.Design;

namespace BP.Web.Controls
{
	/// <summary>
	/// BPGrid ��ժҪ˵����
	/// </summary>
    public class Tree : Microsoft.Web.UI.WebControls.TreeView
    {
        /// <summary>
        /// ��ѡ��Ľڵ㣬������Ϊsql��ģʽ��
        /// </summary>
        /// <returns>in sql</returns>
        public string ToStringOfSQLModel()
        {
            string pk = "";
            foreach (Node nd in this.Nodes)
            {
                if (nd.Checked == false)
                    continue;

                pk += "'" + nd.SelfNo + "',";
            }
            if (pk == "")
                return "''";

            pk = pk.Substring(0, pk.Length - 1);
            return pk;
        }
        /// <summary>
        /// ����ȫ��ѡ��
        /// </summary>
        /// <param name="isChecked">isChecked</param>
        public void SetChecked(bool isChecked)
        {
            foreach (Microsoft.Web.UI.WebControls.TreeNode nd in this.Nodes)
            {
                SetChecked(isChecked, nd);
            }
        }
        public void SetChecked(bool isChecked, Microsoft.Web.UI.WebControls.TreeNode mytn)
        {
            foreach (Microsoft.Web.UI.WebControls.TreeNode tn in mytn.Nodes)
            {
                tn.Checked = isChecked;
                SetChecked(isChecked, tn);
            }
        }

        public void SetSelectedNode(string selectedNo)
        {

        }

        #region ��ʵ��Bind
        /// <summary>
        /// EntitiesNoName
        /// </summary>
        /// <param name="ens"></param>
        public void BindEntities(Entities ens, bool isCheckBox, bool isShowKey, string refVal, string refText)
        {
            this.Nodes.Clear();
            foreach (Entity en in ens)
            {
                Node nd1 = new Node(en.GetValStringByKey(refVal), en.GetValStringByKey(refText));
                nd1.CheckBox = isCheckBox;
                if (isShowKey == false)
                {
                    nd1.Text = en.GetValStringByKey(refText);
                }

                this.Nodes.Add(nd1);
                continue;
            }
        }
        /// <summary>
        /// EntitiesNoName
        /// </summary>
        /// <param name="ens"></param>
        public void BindEntities(EntitiesNoName ens, bool isCheckBox, bool isShowKey)
        {
            this.Nodes.Clear();
            foreach (EntityNoName en in ens)
            {
                Node nd1 = new Node(en.No, en.Name);
                nd1.CheckBox = isCheckBox;
                if (isShowKey == false)
                {
                    nd1.Text = en.Name;
                }
                this.Nodes.Add(nd1);
                continue;
            }
        }

        public void BindEntitiesNew(GradeEntitiesNoNameBase gen, bool Expanded)
        {
            this.Nodes.Clear();
            // Bind ��1����
            foreach (GradeEntityNoNameBase en in gen)
            {
                if (en.Grade != 1)
                    continue;
                Node nd1 = new Node(en.No, en.Name);
                nd1.SelfLevel = en.Grade;
                nd1.SelfIsDtl = en.IsDtl;
                nd1.Expanded = Expanded;
                this.Nodes.Add(nd1);
            }
            // Bind ��2����
            foreach (Node nd1 in this.Nodes)
            {
                if (nd1.SelfIsDtl == true)
                    continue;
                foreach (GradeEntityNoNameBase en in gen)
                {
                    if (en.Grade != 2)
                        continue;

                    if (nd1.SelfNo != en.NoOfParent)
                        continue;
                    Node nd2 = new Node(en, Expanded);
                    nd2.Expanded = Expanded;
                    nd1.Nodes.Add(nd2);
                }
            }
            // Bind ��3����
            foreach (Node nd1 in this.Nodes)
            {
                if (nd1.SelfIsDtl == true)
                    continue;

                foreach (Node nd2 in nd1.Nodes)
                {
                    if (nd2.SelfIsDtl == true)
                        continue;

                    foreach (GradeEntityNoNameBase en in gen)
                    {
                        if (en.Grade != 3)
                            continue;
                        if (nd2.SelfNo != en.NoOfParent)
                            continue;

                        Node nd3 = new Node(en, Expanded);
                        nd3.Expanded = Expanded;
                        nd2.Nodes.Add(nd3);
                    }
                }
            }

            // Bind ��4����
            foreach (Node nd1 in this.Nodes)
            {
                if (nd1.SelfIsDtl == true)
                    continue;
                foreach (Node nd2 in nd1.Nodes)
                {
                    if (nd2.SelfIsDtl == true)
                        continue;
                    foreach (Node nd3 in nd2.Nodes)
                    {
                        if (nd3.SelfIsDtl == true)
                            continue;
                        foreach (GradeEntityNoNameBase en in gen)
                        {
                            if (en.Grade != 4)
                                continue;
                            if (nd3.SelfNo != en.NoOfParent)
                                continue;

                            Node nd4 = new Node(en, Expanded);
                            nd4.Expanded = Expanded;

                            nd3.Nodes.Add(nd4);
                        }
                    }
                }
            }

            // Bind ��5����
            foreach (Node nd1 in this.Nodes)
            {
                if (nd1.SelfIsDtl == true)
                    continue;

                foreach (Node nd2 in nd1.Nodes)
                {
                    if (nd2.SelfIsDtl == true)
                        continue;
                    foreach (Node nd3 in nd2.Nodes)
                    {
                        if (nd3.SelfIsDtl == true)
                            continue;

                        foreach (Node nd4 in nd3.Nodes)
                        {
                            if (nd4.SelfIsDtl == true)
                                continue;
                            foreach (GradeEntityNoNameBase en in gen)
                            {
                                if (en.Grade != 5)
                                    continue;
                                if (nd4.SelfNo != en.NoOfParent)
                                    continue;

                                Node nd5 = new Node(en, Expanded);
                                nd5.Expanded = Expanded;

                                nd4.Nodes.Add(nd5);
                            }
                        }
                    }
                }
            }
            // Bind ��6����
            foreach (Node nd1 in this.Nodes)
            {
                if (nd1.SelfIsDtl == true)
                    continue;

                foreach (Node nd2 in nd1.Nodes)
                {
                    if (nd2.SelfIsDtl == true)
                        continue;
                    foreach (Node nd3 in nd2.Nodes)
                    {
                        if (nd3.SelfIsDtl == true)
                            continue;

                        foreach (Node nd4 in nd3.Nodes)
                        {
                            if (nd4.SelfIsDtl == true)
                                continue;
                            foreach (Node nd5 in nd4.Nodes)
                            {
                                if (nd5.SelfIsDtl == true)
                                    continue;

                                foreach (GradeEntityNoNameBase en in gen)
                                {
                                    if (en.Grade != 6)
                                        continue;
                                    if (nd5.SelfNo != en.NoOfParent)
                                        continue;

                                    Node nd6 = new Node(en, Expanded);
                                    nd6.Expanded = Expanded;
                                    nd5.Nodes.Add(nd6);
                                }
                            }
                        }
                    }
                }
            }


            // Bind ��7����
            foreach (Node nd1 in this.Nodes)
            {
                if (nd1.SelfIsDtl == true)
                    continue;

                foreach (Node nd2 in nd1.Nodes)
                {
                    if (nd2.SelfIsDtl == true)
                        continue;
                    foreach (Node nd3 in nd2.Nodes)
                    {
                        if (nd3.SelfIsDtl == true)
                            continue;

                        foreach (Node nd4 in nd3.Nodes)
                        {
                            if (nd4.SelfIsDtl == true)
                                continue;
                            foreach (Node nd5 in nd4.Nodes)
                            {
                                if (nd5.SelfIsDtl == true)
                                    continue;

                                foreach (Node nd6 in nd5.Nodes)
                                {
                                    if (nd6.SelfIsDtl == true)
                                        continue;

                                    foreach (GradeEntityNoNameBase en in gen)
                                    {
                                        if (en.Grade != 7)
                                            continue;
                                        if (nd6.SelfNo != en.NoOfParent)
                                            continue;

                                        Node nd7 = new Node(en, Expanded);
                                        nd7.Expanded = Expanded;
                                        nd6.Nodes.Add(nd7);
                                    }
                                }
                            }
                        }
                    }
                }
            }


            // Bind ��8����
            foreach (Node nd1 in this.Nodes)
            {
                if (nd1.SelfIsDtl == true)
                    continue;

                foreach (Node nd2 in nd1.Nodes)
                {
                    if (nd2.SelfIsDtl == true)
                        continue;
                    foreach (Node nd3 in nd2.Nodes)
                    {
                        if (nd3.SelfIsDtl == true)
                            continue;

                        foreach (Node nd4 in nd3.Nodes)
                        {
                            if (nd4.SelfIsDtl == true)
                                continue;
                            foreach (Node nd5 in nd4.Nodes)
                            {
                                if (nd5.SelfIsDtl == true)
                                    continue;

                                foreach (Node nd6 in nd5.Nodes)
                                {
                                    if (nd6.SelfIsDtl == true)
                                        continue;

                                    foreach (Node nd7 in nd6.Nodes)
                                    {
                                        if (nd7.SelfIsDtl == true)
                                            continue;

                                        foreach (GradeEntityNoNameBase en in gen)
                                        {
                                            if (en.Grade != 8)
                                                continue;
                                            if (nd7.SelfNo != en.NoOfParent)
                                                continue;

                                            Node nd8 = new Node(en, Expanded);
                                            nd8.Expanded = Expanded;

                                            nd7.Nodes.Add(nd8);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
         

        /// <summary>
        /// ��GradeEntitiesNoNameBase Bind.���ܼ���
        /// ���磺���ܼ���
        /// </summary>
        /// <param name="gen"> GradeEntitiesNoNameBase </param>
        public void BindEntities(GradeEntitiesNoNameBase gen, bool isCheckBox)
        {
            this.Nodes.Clear();
            foreach (GradeEntityNoNameBase en in gen)
            {
                //#region �����һ����
                if (en.Grade == 1)  // ����ǵ�һ���ġ�
                {
                    Node nd1 = new Node(en.No, en.Name);
                    nd1.SelfLevel = en.Grade;
                    nd1.SelfIsDtl = en.IsDtl;
                    nd1.SelfIsDir = true;
                    this.Nodes.Add(nd1);
                    continue;
                }
                //#endregion

                //#region �������
                if (en.Grade == 2)  /// ����ǵ�2���ġ�
                {
                    Node nd2 = new Node(en.No, en.Name); /// new ���ʵ����
                    nd2.SelfLevel = en.Grade;
                    nd2.SelfIsDtl = en.IsDtl;
                    nd2.SelfIsDir = true;
                    //bool isHave=false ; 
                    foreach (Node nd_1 in this.Nodes) /// �ӵ�һ����ȥ�ҡ�
                    {
                        if (nd_1.SelfNo == en.NoOfParent)
                        {
                            nd_1.Nodes.Add(nd2);
                            //isHave=true;
                            break;
                        }
                    }

                    //					if (isHave==false) // ���û�дӵ�һ���ҵ����ĸ��ڵ㡣 ���½���һ��.
                    //					{
                    //						Node nd_1 = new Node( en.Parent.No,en.Parent.Name );
                    //						nd_1.SelfLevel =en.Grade ;
                    //						nd_1.SelfIsDtl =en.IsDtl ;
                    //						nd_1.SelfIsDir =true ;
                    //						this.Nodes.Add(  nd_1 ) ;
                    //						nd_1.Nodes.Add( nd2 ) ; 
                    //					}
                }

                if (en.Grade == 3)  //����ǵ�2���ġ�
                {
                    Node nd3 = new Node(en.No, en.Name); /// new ���ʵ����
                    nd3.SelfLevel = en.Grade;
                    nd3.SelfIsDtl = en.IsDtl;
                    nd3.SelfIsDir = false;
                    //bool isHave=false ;
                    foreach (Node nd_2 in this.Nodes) /// �ӵڶ�����ȥ�ҡ�
                    {
                        foreach (Node nd33 in nd_2.Nodes)
                        {
                            if (nd33.SelfNo == en.NoOfParent)
                            {
                                nd33.Nodes.Add(nd3);
                                //isHave=true;
                                break;
                            }
                        }
                    }
                }
            }
        }
         
        /// <summary>
        /// ��������빦�ܵĹ���ģʽ��
        /// </summary>
        /// <param name="gen">�������</param>
        /// <param name="ens">���ܡ�</param>
        public void BindEntities(GradeEntitiesNoNameBase gen, EntitiesNoName ens, string refKey)
        {
            this.BindEntities(gen, false);

            foreach (Node nd in this.Nodes)   /// ��������Ϊdir.
            {
                nd.SelfIsDir = true;
            }


            foreach (Node nd in this.Nodes)
            {
                // ��1����.
                foreach (EntityNoName en in ens)
                {
                    if (en.GetValStringByKey(refKey) == nd.SelfNo)
                    {
                        Node mynd = new Node();
                        mynd.SelfNo = en.No;
                        mynd.SelfName = en.Name;
                        mynd.SelfIsDir = false;
                        mynd.SelfLevel = 2;
                        mynd.Text = en.No + " " + en.Name;
                        nd.Nodes.Add(mynd);
                    }
                }
                // ��2����.
                if (nd.Nodes.Count == 0)
                    continue;
                foreach (Node nd2 in nd.Nodes)
                {
                    if (nd2.SelfIsDir == false)
                        continue;
                    foreach (EntityNoName en in ens)
                    {
                        if (en.GetValStringByKey(refKey) == nd2.SelfNo)
                        {
                            Node mynd = new Node();
                            mynd.SelfNo = en.No;
                            mynd.SelfName = en.Name;
                            mynd.SelfIsDir = false;
                            mynd.SelfLevel = 3;
                            mynd.Text = en.No + " " + en.Name;
                            nd2.Nodes.Add(mynd);
                        }
                    }

                    if (nd2.Nodes.Count == 0)
                        continue;
                    foreach (Node nd3 in nd2.Nodes)
                    {
                        if (nd2.SelfIsDir == false)
                            continue;
                        foreach (EntityNoName en in ens)
                        {
                            if (en.GetValStringByKey(refKey) == nd3.SelfNo)
                            {
                                Node mynd = new Node();
                                mynd.SelfNo = en.No;
                                mynd.SelfName = en.Name;
                                mynd.SelfIsDir = false;
                                mynd.SelfLevel = 5;
                                mynd.Text = en.No + " " + en.Name;
                                nd2.Nodes.Add(mynd);
                            }
                        }
                        if (nd3.Nodes.Count == 0)
                            continue;

                        foreach (Node nd4 in nd3.Nodes)
                        {
                            if (nd4.SelfIsDir == false)
                                continue;
                            foreach (EntityNoName en in ens)
                            {
                                if (en.GetValStringByKey(refKey) == nd4.SelfNo)
                                {
                                    Node mynd = new Node();
                                    mynd.SelfNo = en.No;
                                    mynd.SelfName = en.Name;
                                    mynd.SelfIsDir = false;
                                    mynd.SelfLevel = 5;
                                    mynd.Text = en.No + " " + en.Name;
                                    nd3.Nodes.Add(mynd);
                                }
                            }
                            if (nd4.Nodes.Count == 0)
                                continue;
                            throw new Exception("ss");
                        }
                    }
                }
            }
        }
        /// <summary>
        /// ��
        /// </summary>
        /// <param name="ens"></param>
        public void BindDeptsType(EntitiesNoName ens)
        {
            this.Nodes.Clear();
            this.BindDeptsType(ens, 2);
            this.BindDeptsType(ens, 4);
            this.BindDeptsType(ens, 6);
            this.BindDeptsType(ens, 8);
            this.BindDeptsType(ens, 10);
        }
        private void BindDeptsType(EntitiesNoName ens, int len)
        {
            foreach (EntityNoName en in ens)
            {
                if (en.No.Length != len)
                    continue;


                Microsoft.Web.UI.WebControls.TreeNode tn = new Microsoft.Web.UI.WebControls.TreeNode();
                tn.Text = en.Name;
                tn.ID = "TN" + en.No;
                if (len == 2)
                    this.Nodes.Add(tn);


                Microsoft.Web.UI.WebControls.TreeNode ptn = this.GetNodeByID("TN" + en.No.Substring(0, en.No.Length - 2));
                if (ptn != null)
                {
                    ptn.Nodes.Add(tn);
                }
                else
                    this.Nodes.Add(tn);
            }
        }
        #endregion

        #region ���췽��
        public Tree()
        {
            //this.InitTree();
            //this.CssClass="Tree"+WebUser.Style;		

            //this.ExpandedImageUrl=altopen.gif
            //this.ExpandedImageUrl=System.Web.HttpContext.Current.Request.ApplicationPath + "/images/System/altopen.gif";
            //this.SystemImagesPath=System.Web.HttpContext.Current.Request.ApplicationPath+"/images/TreeImages/";
            //this.ImageUrl
            //this.ImageUrl=System.Web.HttpContext.Current.Request.ApplicationPath + "/images/System/altclose.gif";
            //this.SelectedImageUrl=System.Web.HttpContext.Current.Request.ApplicationPath + "/images/System/06.gif";
            //this.ExpandedImageUrl=System.Web.HttpContext.Current.Request.ApplicationPath + "/images/sys/openFload.ico" ;
            //this.ImageUrl=System.Web.HttpContext.Current.Request.ApplicationPath + "/images/sys/arror.ico";

        }
        private void InitTree()
        {
            //this.SystemImagesPath=System.Web.HttpContext.Current.Request.ApplicationPath+"/images/TreeImages/";
            this.CssClass = "Tree" + WebUser.Style;
        }
        #endregion

        #region ����������
        /// <summary>
        /// �õ�ѡ��� SelfNo ���ݷָ���š�
        /// </summary>
        /// <param name="spt">�ָ����</param>
        /// <returns>��ɵ��ַ�</returns>
        public string GetSelfNosBySpt(string spt)
        {
            string str = "";
            foreach (Node nd in this.Nodes)
            {
                str += spt + nd.SelfNo;
            }
            return str;
        }
        public Microsoft.Web.UI.WebControls.TreeNode GetNodeByID(string id)
        {
            foreach (Microsoft.Web.UI.WebControls.TreeNode tn in this.Nodes)
            {
                if (tn.ID == id)
                    return tn;

                Microsoft.Web.UI.WebControls.TreeNode mytn = this.GetNodeByID(id, tn);
                if (mytn != null)
                    return mytn;
            }
            return null;
        }

        public Microsoft.Web.UI.WebControls.TreeNode GetNodeByID(string id, Microsoft.Web.UI.WebControls.TreeNode tn)
        {
            foreach (Microsoft.Web.UI.WebControls.TreeNode mytn in tn.Nodes)
            {
                if (mytn.ID == id)
                    return mytn;
                Microsoft.Web.UI.WebControls.TreeNode tnXXX = GetNodeByID(id, mytn);
                if (tnXXX == null)
                    continue;
                else
                    return tnXXX;
            }
            return null;
        }


        /// <summary>
        /// �õ���ǰѡ��������
        /// </summary>
        /// <returns></returns>
        public Node GetCurrSelectedNode
        {
            get
            {
                string index = this.SelectedNodeIndex;
                try
                {
                    return (Node)this.GetNodeFromIndex(index);
                }
                catch
                {
                    return null;
                }
            }
        }
        public int GetCurrSelectedNodeLevel
        {
            get
            {
                if (this.GetCurrSelectedNode.Nodes.Count > 0)
                    return 1;
                else
                    return 2;
            }
        }
        #endregion

        #region Bind ������
        /// <summary>
        ///  DT col OID, No, Name .
        ///  SeleDt col OID .
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="SeleDt"></param>
        public void BindSelected(DataTable SeleDt)
        {
            foreach (DataRow dr in SeleDt.Rows)
            {
                int OID = (int)dr["OID"];
                foreach (Node nd in this.Nodes)
                {
                    if (nd.SelfOID == OID)
                    {
                        nd.Checked = true;
                    }
                }
            }
        }
        /// <summary>
        /// OID, No , Name
        /// </summary>
        /// <param name="dt"></param>
        public void BindByTable(DataTable dt)
        {
            this.BindByTable(dt, false);
        }
        /// <summary>
        /// OID, No , Name
        /// </summary>
        /// <param name="dt"></param>
        public void BindByTableNoName(DataTable dt, bool IsCheckBox)
        {
            this.Nodes.Clear();
            foreach (DataRow dr in dt.Rows)
            {
                Node nd = new Node();
                nd.SelfNo = dr[0].ToString();
                nd.SelfName = dr[1].ToString();
                nd.Text = nd.SelfNo + nd.SelfName;
                nd.CheckBox = IsCheckBox;
                this.Nodes.Add(nd);
            }
        }
        /// <summary>
        /// OID, No , Name . �ǲ��� CheckBox
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="IsCheck"></param>
        public void BindByTable(DataTable dt, bool IsCheckBox)
        {
            this.Nodes.Clear();
            foreach (DataRow dr in dt.Rows)
            {
                Node nd = new Node(dr);
                nd.CheckBox = IsCheckBox;
                this.Nodes.Add(nd);
            }
        }
        #endregion

        //		/ <summary>
        //		/ �û�����, �޸��� 2003-10-21
        //		/ </summary> 
        //		public void BindAppFunc()
        //		{
        //			FuncCates HisFuncCates = BP.Web.WebUser.HisFuncCates;
        //			Funcs HisFuncs = BP.Web.WebUser.HisFuncs;
        //			Emp en = new Emp(WebUser.No);
        //			foreach(FuncCate fc in HisFuncCates)
        //			{				
        //				Node nd = new Node(fc.No,fc.Name);
        //				nd.Expanded=true;
        //				if (en.IsShowICO)				
        //					nd.ImageUrl=System.Web.HttpContext.Current.Request.ApplicationPath+fc.Ico;				 
        //				
        //				this.Nodes.Add(nd);
        //				Funcs  funcs = WebUser.GetHisFuncsByCateNo(fc.No);
        //				foreach(Func f in funcs)
        //				{	 		
        //					if (f.IsShow==false) continue;				 
        //					Node n = new Node(f.No,f.Name);
        //					n.NavigateUrl=f.Url;
        //					if (en.IsShowICO)
        //						n.ImageUrl=System.Web.HttpContext.Current.Request.ApplicationPath+f.Ico;
        //					n.Target="mainfrm";
        //					nd.Nodes.Add( n );
        //				}	
        //			}
        //		}
        //		/// <summary>
        //		/// �û�����
        //		/// </summary> 
        //		public void BindAppUserFunc(string userNo)
        //		{
        //			FuncCates HisFuncCates = new FuncCates();
        //			Funcs HisFuncs = BP.Web.WebUser.HisFuncs;
        //			Emp en = new Emp(userNo );
        //			foreach(FuncCate fc in HisFuncCates)
        //			{
        //				Node nd = new Node(fc.No,fc.Name);
        //				nd.Expanded=true;
        //				if (en.IsShowICO)
        //				{
        //					nd.ImageUrl=System.Web.HttpContext.Current.Request.ApplicationPath+fc.Ico;
        //					//nd.ExpandedImageUrl=System.Web.HttpContext.Current.Request.ApplicationPath+"/images/sys/OPENFOLD.ICO";
        //				}
        //				this.Nodes.Add(nd);
        //
        //				Funcs  funcs = WebUser.GetHisFuncsByCateNo(fc.No);
        //				foreach(Func f in funcs)
        //				{	
        //					Node n = new Node(f.No,f.Name) ;
        //					n.NavigateUrl=f.Url;
        //					if (en.IsShowICO)
        //						n.ImageUrl=System.Web.HttpContext.Current.Request.ApplicationPath+f.Ico;
        //					n.Target="mainfrm";
        //					nd.Nodes.Add( n );
        //				}	
        //			}
        //		}
        //		/// <summary>
        //		/// bind ��˰�˵Ĺ���
        //		/// </summary>
        //		public void BindAppAllNSRFunc()
        //		{
        //			NSRFuncSorts ens = new  NSRFuncSorts();
        //			ens.QueryAll();
        //			foreach(NSRFuncSort fc in ens)
        //			{
        //				Node nd = new Node( fc.No,fc.Name);
        //				nd.Expanded=true;
        //				this.Nodes.Add(nd);
        //				NSRFuncs  funcs =fc.HisFuncs;
        //				foreach(NSRFunc f in funcs)
        //				{
        //					Node n = new Node(f.No,f.Name) ;
        //					n.NavigateUrl=System.Web.HttpContext.Current.Request.ApplicationPath +f.Url;
        //					n.Target="mainfrm";
        //					nd.Nodes.Add( n );
        //				}				
        //			}
        //		}
    }
	public class Node : Microsoft.Web.UI.WebControls.TreeNode 
	{
		private void SetText()
		{			 
			//this.Text=this.SelfNo+" "+this.SelfName;
			this.Text= this.SelfName;
			//this.ID="_"+this.SelfOID.ToString()+this.SelfNo;
		}
		/// <summary>
		/// Node
		/// </summary>
		public Node()
		{			
			this.InitNode();
		}

        public void InitNode()
        {
            this.DefaultStyle.CssText = "TreeDefaultStyle" + WebUser.Style;
            this.HoverStyle.CssText = "TreeHoverStyle" + WebUser.Style;
            this.SelectedStyle.CssText = "TreeSelectedStyle" + WebUser.Style;
            this.ExpandedImageUrl = System.Web.HttpContext.Current.Request.ApplicationPath + "/Images/Pub/BillOpen.gif";
            this.ImageUrl = System.Web.HttpContext.Current.Request.ApplicationPath + "/Images/Pub/Bill.gif";
        }

//		public Node(EntityOIDName   en )
//		{			 
//			SelfOID=en.OID;
//			SelfNo=en.Name;
//			this.SetText();
//		}

		public Node(GradeEntityNoNameBase   en, bool  Expanded )
		{
			Text=en.NoOfThisGrade+en.Name;
			SelfLevel = en.Grade;
			SelfIsDtl =en.IsDtl;
			SelfNo=en.No;
			this.Expanded = Expanded;
			this.InitNode();

		}

//		public Node(Dict en )
//		{
//			SelfOID=en.OID;
//			SelfNo=en.No;
//			SelfName=en.Name ;			
//			this.SetText();
//		}
//		public Node(EntityNoName en )
//		{			 
//			SelfNo=en.No;
//			SelfName=en.Name;
//			this.SetText();
//		}
		public Node(int OID, string No, string Name)
		{
			SelfOID=OID;
			SelfNo=No;
			SelfName=Name;
			this.SetText();
			this.InitNode();
		}
		/// <summary>
		/// No, Name
		/// </summary>
		/// <param name="No">����</param>
		/// <param name="Name">����</param>
		public Node(string No, string Name)
		{
			SelfNo=No;
			SelfName=Name;
			this.SetText();
			this.InitNode();

		}
		public Node(DataRow dr)
		{
			SelfOID=(int)dr["OID"];
			SelfNo=(string)dr["No"];
			SelfName=(string)dr["Name"];
			this.SetText();

			this.InitNode();

		}		 
		/// <summary>
		/// �����ı������
		/// </summary>
		public string SelfNo
		{
			get
			{
				if (ViewState["Self_GradeNo"]==null)				
					return "";
				else			
					return (string)ViewState["Self_GradeNo"];			
			}
			set
			{
				ViewState["Self_GradeNo"] = value;
			}
		}
		/// <summary>
		/// �����ı������
		/// </summary>
		public int SelfLevel
		{
			get
			{
				if (ViewState["SelfLevel"]==null)
					return 1;
				else			
					return (int)ViewState["SelfLevel"];			
			}
			set 
			{
				ViewState["SelfLevel"] = value;
			}
		}
		/// <summary>
		/// �ǲ�������
		/// </summary>
		public bool SelfIsDtl
		{
			get
			{
				if (ViewState["SelfIsDtl"]==null)
					return true;
				else			
					return (bool)ViewState["SelfIsDtl"];			
			}
			set 
			{
				ViewState["SelfIsDtl"] = value;
			}
		}
		/// <summary>
		/// ����������
		/// </summary>
		public string SelfName
		{
			get 
			{
				object obj = ViewState["Self_Name"];
				if (obj != null)
					return(string)obj;
				return null;
			}
			set 
			{
				ViewState["Self_Name"] = value;
			}
		}
		/// <summary>
		/// ������Ψһ��ʶOID
		/// </summary>
		public int SelfOID
		{
			get 
			{				
				if (ViewState["Self_OID"] == null)
					return -1;
				else
					return int.Parse(ViewState["Self_OID"].ToString()); 
			}
			set 
			{
				ViewState["Self_OID"] = value;
			}
		}
		/// <summary>
		/// �ǲ���Ŀ¼?
		/// </summary>
		public bool SelfIsDir
		{
			get
			{
				if (ViewState["SelfIsDir"]==null)
					return true;
				else			
					return (bool)ViewState["SelfIsDir"];			
			}
			set 
			{
				ViewState["SelfIsDir"] = value;
			}
		}
		 
	}
}
