using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data;
using System.ComponentModel;
using System.Collections;
using System.Reflection;

namespace BP.Win.Controls
{		
	public delegate void OnFirstExpandEventHandler(object sender,TreeViewCancelEventArgs e);

	[ToolboxBitmap(typeof(System.Windows.Forms.TreeView))]
	public class Tree : System.Windows.Forms.TreeView
	{
		public Tree()
		{
			this.HideSelection=false;
			this.HotTracking=true;
		}

		#region ��д����
		protected  override void OnMouseDown(System.Windows.Forms.MouseEventArgs e)
		{
			if(e.Button ==MouseButtons.Right)
				this.SelectedNode=this.GetNodeAt(e.X,e.Y);			
			base.OnMouseDown(e);
		}
		[Category("��Ϊ"),Description("�״�չ���ڵ�ʱ����")]
		public event OnFirstExpandEventHandler FirstExpand;

		/// <summary>
		/// ���� FirstExpand �¼�
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnFirstExpand(TreeViewCancelEventArgs e)
		{
			if( FirstExpand!=null )
			{
				FirstExpand(this,e);
			}
		}
		/// <summary>
		/// �ڵ�չ��֮ǰ
		/// </summary>
		/// <param name="e"> Ҫչ���Ľڵ�</param>
		protected  override void OnBeforeExpand(TreeViewCancelEventArgs e)
		{
			base.OnBeforeExpand(e);
			if( e.Node.FirstNode.Text.Equals("_temp"))
			{
				e.Node.FirstNode.Remove();//�״�չ��ʱɾ����ʱ�Ľڵ�"_temp"
				this.OnFirstExpand(e);
			}
		}
		protected override void OnBeforeCheck(TreeViewCancelEventArgs e)
		{
			base.OnBeforeCheck (e);
		}


		#endregion ��д����
		
		private bool _readOnly = false;
		public  bool ReadOnly  
		{
			get
			{
				return this._readOnly;
			}
			set
			{
				this._readOnly = value;
			}
		}

		public bool TagVisible = false;
		public string TagPropertyName ="";
		public void TreeNodeShow( TreeNode tn )
		{
			if( this.TagVisible )
			{
				if( tn.Tag !=null )
				{
					object val = "null";
					Type tp = tn.Tag.GetType();

					PropertyInfo ptag = tp.GetProperty( TagPropertyName );
					if( ptag != null )
					{
						val = ptag.GetValue( tn.Tag , null );
					}
					else
						val = tn.Tag.ToString();

					if( val != null )
						tn.Text += "["+ val.ToString() +"]";
				}
				else
					tn.Text += "[Null]";
			}
		}


		#region ��ӷ��� 
		public int SelectedNodeLevel
		{
			get
			{
				if(this.SelectedNode ==null)
					return -1;
				else if(this.SelectedNode == this.TopNode)
					return 0;
				else 
				{
					if(this.PathSeparator.Length!=1)
						this.PathSeparator ="\\";
					return this.SelectedNode.FullPath.Split( this.PathSeparator[0] ).Length-1;
				}
			}
		}


		public int AddNode(TreeNode parent,object tag,string text,int imageIndex,int selectedImage,bool isParent)
		{
			TreeNode temp=new TreeNode(text);
			temp.Tag=tag;
			TreeNodeShow( temp );

			temp.ImageIndex=imageIndex;
			temp.SelectedImageIndex=selectedImage;			
			if(isParent) 
			{
				temp.Nodes.Add("_temp");
			}
			if( parent !=null)
			{
				parent.Nodes.Add(temp);
				return parent.Nodes.Count;
			}
			else
			{
				this.Nodes.Add(temp);
				return this.Nodes.Count;
			}
		}
        public int AddNodes(object DataSource, TreeNode parent, string tagName, string textName, int imageIndex, int selectedImage, bool isParent)
        {
            if (parent != null)
                parent.Nodes.Clear();
            else
                this.Nodes.Clear();

            #region ��������Դ
            IListSource ls = DataSource as IListSource;
            IList list;
            if (ls != null && !ls.ContainsListCollection)//DataSource �Ǳ����ͼ
                list = ls.GetList();
            else
                list = DataSource as IList;

            if (list == null || list.Count == 0)
                return 0;

            Type tp = null;
            PropertyInfo tag = null, text = null;
            DataView dv = null;

            if (ls == null)
            {
                tp = list[0].GetType();
                tag = tp.GetProperty(tagName);
                text = tp.GetProperty(textName);
                if (text == null)
                    throw new Exception("δ�ҵ�����'" + textName + "'��");
            }
            else
                dv = list as DataView;
            #endregion

            #region ��ӽڵ㼯��
            if (ls == null)
            {
                foreach (object en in list)
                {
                    TreeNode temp = new TreeNode(text.GetValue(en, null).ToString());
                    if (tag == null)
                        temp.Tag = en;
                    else
                        temp.Tag = tag.GetValue(en, null);
                    TreeNodeShow(temp);

                    temp.ImageIndex = imageIndex;
                    temp.SelectedImageIndex = selectedImage;
                    if (isParent)
                        temp.Nodes.Add("_temp");
                    if (parent != null)
                        parent.Nodes.Add(temp);
                    else
                        this.Nodes.Add(temp);
                }
            }
            else
            {
                for (int i = 0; i < dv.Count; i++)
                {
                    TreeNode temp = new TreeNode(dv[i][textName].ToString());
                    temp.Tag = dv[i][tagName];
                    TreeNodeShow(temp);

                    temp.ImageIndex = imageIndex;
                    temp.SelectedImageIndex = selectedImage;
                    if (isParent)
                        temp.Nodes.Add("_temp");
                    if (parent != null)
                        parent.Nodes.Add(temp);
                    else
                        this.Nodes.Add(temp);
                }
                dv.Dispose();
            }
            #endregion ��ӽڵ㼯��

            if (parent != null)
                return parent.Nodes.Count;
            else
                return this.Nodes.Count;
        }
		
//		public void FillTree(string topNodeText, DataView dv,bool isParent)
//		{
//			TreeNode node=new TreeNode(topNodeText);
//			node.ImageIndex=0;
//			node.ImageIndex=2;
//			this.Nodes.Clear();
//			this.Nodes.Add(node);
//			try
//			{
//				this.BeginUpdate();
//				for(int i=0;i<dv.Count;i++)
//				{
//					this.AddNode(node,dv[i][0],dv[i][1].ToString(),1,2,isParent);
//				}
//				this.EndUpdate();
//				node.Expand();
//				this.SelectedNode=node;
//			}
//			catch(System.Exception ex)
//			{
//				MessageBox.Show(ex.Message);
//			}
//		}
//		public void FillChild(TreeNode parent, DataView dv,bool isParent)
//		{
//			try
//			{
//				this.BeginUpdate();
//				for(int i=0;i<dv.Count;i++)
//				{
//					this.AddNode(parent,dv[i][0],dv[i][1].ToString(),1,2,isParent);
//				}
//				this.EndUpdate();
//			}
//			catch(System.Exception ex)
//			{
//				MessageBox.Show(ex.Message);
//			}
//		}
		#endregion ��ӷ��� ��������
	}
}
