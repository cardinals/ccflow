using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

using System.Resources;
using System.ComponentModel;


using BP.En;
using BP.En ;
using BP.Port;


namespace BP.Win32.Controls
{
	
	[ToolboxBitmap(typeof(System.Windows.Forms.TreeView))]
	public class Tree : System.Windows.Forms.TreeView
	{
		#region ����
		public Entities HisEns=null;
		#endregion 

		public Tree()
		{
		}

		protected void beforeBind()
		{
		}
		protected void endBind()
		{
		}
		/// <summary>
		/// ����ʵ�弯
		/// </summary>
		/// <param name="ens">ʵ�弯</param>
		/// <param name="refText">��ص��ı�</param>
		/// <param name="refKey">��ص�ֵ</param>
		/// <param name="CheckBoxes">�Ƿ�Ӹ�ѡ��</param>
		public void BindEns(Entities ens, string refText, string refKey,bool CheckBoxes)
		{
			this.HisEns = ens ;
			if (ens.IsGradeEntities)
			{
				this.BindEns((GradeEntitiesNoNameBase)ens,CheckBoxes) ;
				return ;
			}			
			this.CheckBoxes = CheckBoxes ;
			this.beforeBind();
			
			foreach(Entity en in ens)
			{
				this.Nodes.Add( new Node(en.GetValStringByKey(refText),en.GetValStringByKey(refKey),en)) ;
			}
			this.endBind();
		}
		/// <summary>
		/// ����ʵ�弯����һ����
		/// </summary>
		/// <param name="ens"></param>
		/// <param name="CheckBoxes"></param>
		public void BindEns(GradeEntitiesNoNameBase ens, bool CheckBoxes)
		{
			this.HisEns = ens ;

			this.CheckBoxes = CheckBoxes ;
			this.beforeBind();

			foreach(GradeEntityNoNameBase en  in ens)
			{				
				if (en.Grade==1)
				{
					Node nd1 = new Node();
					nd1.Text = en.Name ;
					nd1.Value = en.No;
					nd1.Tag =en;
					nd1.Grade=1 ; 		
					//nd1.ExpandAll() ;
					nd1.Expand();
					this.Nodes.Add(nd1);
					if (en.IsDtl)
					continue;
					this.NodeAdd(nd1,ens,en);
				}
			}

			
			this.endBind();

			foreach(Node nd in this.Nodes)
			{
				this.ExpandIt(nd);
				nd.ExpandAll();
			}
		}
		public void ExpandIt(Node nd)
		{
			foreach(Node mynd in nd.Nodes)
			{
				mynd.ExpandAll();
			}
		}
		/// <summary>
		///���ؽڵ� 
		/// </summary>
		/// <param name="nd"></param>
		/// <param name="ens"></param>
		/// <param name="en"></param>
		protected void NodeAdd(Node nd, GradeEntitiesNoNameBase ens,GradeEntityNoNameBase en )
		{
			if (en.IsDtl)
				return ;
			//Node childNode = new Node();
			foreach(GradeEntityNoNameBase childen in ens)
			{
				if (childen.Grade ==en.Grade+1 && childen.NoOfParent ==en.No )
				{					
					Node nd1 = new Node();
					nd1.Text = childen.Name ;
					nd1.Value = childen.No;
					nd1.Tag =childen;
					nd1.Grade=childen.Grade ;
					nd1.ExpandAll() ;
					nd1.Expand();
					nd.Nodes.Add(nd1) ;				 
					if (en.IsDtl)
					continue;
					this.NodeAdd(nd1,ens,childen);					
				}
			}
		}
		/// <summary>
		/// ��õ�ǰѡ��ʵ�弯
		/// </summary>
		/// <returns></returns>
		public Entities GetCurrentSelectedEns()
		{
			Entities ens=this.HisEns.CreateInstance();//����ʵ��
			this.GetCurrentSelectedEns(this.Nodes,ens) ;
			return ens;
		}
		protected void GetCurrentSelectedEns(TreeNodeCollection nds, Entities ens)
		{
			foreach(Node nd in nds)
			{
				if (nd.Checked)
				{
					ens.AddEntity( (Entity)nd.Tag );
				}
				GetCurrentSelectedEns(nd.Nodes,ens);
			}
		}

		/// <summary>
		/// ����ѡ��ʵ��״̬
		/// </summary>
		/// <param name="ens"></param>
		/// <param name="refKey"></param>
		public void SetChecked(Entities ens,  string refKey)
		{
			SetChecked(this.Nodes,ens,refKey);
		}
		protected void SetChecked(TreeNodeCollection nds , Entities ens,  string refKey)
		{
			if (nds.Count==0)
				return ;
			foreach(Node nd in nds)
			{
				foreach(Entity en in ens)
				{
					if ( nd.Value==en.GetValStringByKey(refKey) )					 
						nd.Checked =true;
				}
				SetChecked(nd.Nodes,ens,refKey);
			}
		}
		
		#region ����
		
		/// <summary>
		///  ���ؽڵ��Ŀ¼·����
		/// </summary>
		/// <param name="node"></param>
		/// <returns>�ڵ��Ŀ¼·����</returns>
		private string GetPathFromNode(TreeNode node) 
		{
			if (node.Parent == null) 
			{
				return node.Text;
			}
			return Path.Combine(GetPathFromNode(node.Parent), node.Text);
		}		
		/// <summary>
		/// ˢ���Ի�ȡ�����ڵ��µ�������չ���Ľڵ㡣
		/// </summary>
		/// <param name="Node"></param>
		/// <param name="ExpandedNodes"></param>
		/// <param name="StartIndex"></param>
		/// <returns></returns>
		private int Refresh_GetExpanded(TreeNode Node, string[] ExpandedNodes, int StartIndex) 
		{

			if (StartIndex < ExpandedNodes.Length) 
			{
				if (Node.IsExpanded) 
				{
					ExpandedNodes[StartIndex] = Node.Text;
					StartIndex++;
					for (int i = 0; i < Node.Nodes.Count; i++) 
					{
						StartIndex = Refresh_GetExpanded(Node.Nodes[i],
							ExpandedNodes,
							StartIndex);
					}
				}
				return StartIndex;
			}
			return -1;
		}	
	

		DataSet ds=new DataSet();
		// �ݹ�������Ľڵ�
		public void AddTree(int ParentID,TreeNode pNode) 
		{
			DataView dvTree = new DataView(ds.Tables[0]);
			//����ParentID,�õ���ǰ�������ӽڵ�
			dvTree.RowFilter =  "[PARENTID] = " + ParentID;
			foreach(DataRowView Row in dvTree) 
			{
				if(pNode == null) 
				{    //'?��Ӹ��ڵ�
					//					TreeNode Node = treeView1.Nodes.Add(Row["ConText"].ToString());
					//					AddTree(Int32.Parse(Row["ID"].ToString()),Node);    //�ٴεݹ�
				} 
				else 
				{   //��ӵ�ǰ�ڵ���ӽڵ�
					TreeNode Node =  pNode.Nodes.Add(Row["ConText"].ToString());
					AddTree(Int32.Parse(Row["ID"].ToString()),Node);     //�ٴεݹ�
				}
			}
		}
		#endregion
	}
	/// <summary>
	/// ���ϵĽڵ�
	/// </summary>
	public class Node :TreeNode
	{
		/// <summary>
		/// ֵ
		/// </summary>
		public string Value=null;
		/// <summary>
		///����
		/// </summary>
		public int Grade=0;

		public Node()
		{
			 
		}
		/// <summary>
		///���ϵĽڵ� 
		/// </summary>
		/// <param name="text">�ڵ��ǩ����ʾ���ı�</param>
		/// <param name="val">ֵ</param>
		/// <param name="en">����</param>
		public Node(string text, string val, Entity en)
		{
			this.Text = text;
			this.Value = val;
			this.Tag = en;
			
		}
		/// <summary>
		///���ϵĽڵ� 
		/// </summary>
		/// <param name="text">�ڵ��ǩ����ʾ���ı�</param>
		/// <param name="val">ֵ</param>
		public Node(string text, string val )
		{
			this.Text = text;
			this.Value = val;			 
		}
	}
	 
}
