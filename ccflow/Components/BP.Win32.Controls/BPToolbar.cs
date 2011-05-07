using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BP.Web.Controls ;

namespace BP.Win32.Controls
{
	/// <summary>
	///������ 
	/// </summary>
	[ToolboxBitmap(typeof(System.Windows.Forms.ToolBar))]
	public class BPToolbar : System.Windows.Forms.ToolBar
	{
		#region �Զ�������		 
//		/// <summary>
//		/// ��������HisBPImageList
//		/// ���������AddBtn���������
//		/// </summary>
//		protected void ReSetHisHisBPImageList()
//		{
//			this.ImageList.Images.Clear();
//			foreach(BPToolbarBtn btn in this.Buttons)
//			{
//				
//				this.ImageList.Images.Add( new Icon("D:\\WebApp\\WF\\images\\ToolBarIcon\\Btn\\Card.gif") );
//			}
//
//		}
		#endregion

		#region ����
		/// <summary>
		/// ���߿ռ�
		/// </summary>
		public BPToolbar()
		{
		}
		#endregion

		#region addbtn
		/// <summary>
		/// 
		/// </summary>
		/// <param name="text"></param>
		public void RemoveBtnByText(string text)
		{
			ToolBarButton btn1 = null;
			foreach(ToolBarButton btn in this.Buttons)
			{
				if (btn.Text== text)
				{
					btn1 = btn;
					break;
				}
			}
			 
			this.Buttons.Remove( btn1);
		}
		public bool Contains(string text)
		{
			foreach(ToolBarButton btn in this.Buttons)
			{
				if (btn.Text== text)
					return true;
			}
			return false;
		}
		/// <summary>
		///���Ӱ�ť 
		/// </summary>
		/// <param name="name"></param>
		/// <param name="text"></param>
		public void AddBtn(string name, string text)
		{
			BPToolbarBtn btn = new BPToolbarBtn(name,text);
			this.AddBtn(btn);
		}
		public void AddBtn(BPToolbarBtn btn)
		{
			foreach(ToolBarButton btn1 in this.Buttons)
			{
				if (btn1.Text == btn.Text )
					return;
			}

			this.Buttons.Add(btn);
		}
		public void AddBtn(string name)
		{
			BPToolbarBtn btn = this._AddBtn(name);
			btn.ImageIndex =this.ImageList.Images.Count ;

			this.Buttons.Add(btn ); 

			AddImage(name) ; 

			//this.ImageList.Images.Add( new Icon("D:\\WebApp\\WF\\images\\ToolBarIcon\\Btn\\"+name+".gif" ))  ; 
		}
		/// <summary>
		/// ����ͼ��
		/// </summary>
		/// <param name="btnName"></param>
		public void AddImage(string btnName)
		{
			 
			
			// Be sure to use an appropriate escape sequence (such as the 
			// @) when specifying the location of the file.
			//System.Drawing.Image myImage = 	Image.FromFile(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal)+ @"\Image.gif");
			btnName=btnName.Substring(4);
			//if (this.ImageList==null)
			//	this.ImageList =this.FindForm().Controls.GetChildIndex(  
			System.Drawing.Image myImage = 	Image.FromFile("E:\\WebApp\\WF\\images\\ToolBarIcon\\Btn\\"+btnName+".gif"  );
			this.ImageList.Images.Add(myImage);
		}
		/// <summary>
		/// ���Ӽ����
		/// </summary>
		/// <param name="id"></param>
		public void AddSpt(string id)
		{
			BPToolbarBtn btn = new BPToolbarBtn();
			btn.Style = ToolBarButtonStyle.Separator ; 			
			this.Buttons.Add(btn);
		}
		/// <summary>
		/// ���ӵ���ʱ��ʾ��ť
		/// </summary>
		/// <param name="name"></param>
		/// <param name="text"></param>
		public void AddDropDownBtnt(string name,string text)
		{
			BPToolbarBtn btn = new BPToolbarBtn(name,text) ; 
			btn.Style = ToolBarButtonStyle.DropDownButton ;
            this.Buttons.Add(btn);
		}

		/// <summary>
		/// ���볣�ð�ť
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		private  BPToolbarBtn  _AddBtn(string name)
		{
			string text="" ;
			switch(name)
			{				
				 
				case NamesOfBtn.Send:
					text="����";
					break;
				case NamesOfBtn.Reply:
					text="�ظ�";
					break;
				case NamesOfBtn.Forward:
					text="ת��";
					break;
				case NamesOfBtn.Next:
					text="��һ��";
					break;
				case NamesOfBtn.Previous:
					text="��һ��";
					break;
				case NamesOfBtn.Selected:
					text="ѡ��";
					break;
				case NamesOfBtn.Add:
					text="����";
					break;
				case NamesOfBtn.Adjunct:
					text="����";
					break;
				case NamesOfBtn.AllotTask:
					text="��������";
					break;
				case NamesOfBtn.Apply:
					text="����";
					break;
				case NamesOfBtn.ApplyTask:
					text="��������";
					break;
				case NamesOfBtn.Back:
					text="����";
					break;
				case NamesOfBtn.Card:
					text="��Ƭ";
					break;
				case NamesOfBtn.Close:
					text="�ر�";
					break;
				case NamesOfBtn.Confirm:
					text="ȷ��";
					break;
				case NamesOfBtn.Delete:
					text="ɾ��";
					break;
				case NamesOfBtn.Edit:
					text="�༭";
					break;
				case NamesOfBtn.EnList:
					text="�б�";
					break;
				case NamesOfBtn.Cancel:
					text="ȡ��";
					break;
				case NamesOfBtn.Export:
					text="����";
					break;
				case NamesOfBtn.FileManager:
					text="�ļ�����";
					break;
				case NamesOfBtn.Help:
					text="����";
					break;
				case NamesOfBtn.Insert:
					text="����";
					break;
				case NamesOfBtn.LogOut:
					text="ע��";
					break;
				case NamesOfBtn.Messagers:
					text="��Ϣ";
					break;
				case NamesOfBtn.New:
					text="�½�";
					break;
				case NamesOfBtn.Print:
					text="��ӡ";
					break;
				case NamesOfBtn.Refurbish:
					text="ˢ��";
					break;
				case NamesOfBtn.Reomve:
					text="�Ƴ�";
					break;
				case NamesOfBtn.Save:
					text="����";
					break;
				case NamesOfBtn.SaveAndClose:
					text="���沢�ر�";
					break;
				case NamesOfBtn.SaveAndNew:
					text="���沢�½�";
					break;
				case NamesOfBtn.SaveAsDraft:
					text="����ݸ�";
					break;
				case NamesOfBtn.Search:
					text="����";
					break;
				case NamesOfBtn.SelectAll:
					text="ѡ��ȫ��";
					break;			 
				case NamesOfBtn.SelectNone:
					text="��ѡ";
					break;
				case NamesOfBtn.View:
					text="�鿴";
					break;
				case NamesOfBtn.Update:
					text="����";
					break;
				default:
					throw new Exception("@û�ж���ToolBarBtn ��� "+name);
			}	
			return  new BPToolbarBtn(name,text);
			 
		}
		#endregion
	}

	/// <summary>
	/// ��������ť
	/// </summary>
	[ToolboxBitmap(typeof(System.Windows.Forms.ToolBarButton))]
	public class BPToolbarBtn : System.Windows.Forms.ToolBarButton
	{
		/// <summary>
		/// ���߿ռ�
		/// </summary>
		public BPToolbarBtn()
		{
		}
		public BPToolbarBtn(string name,string text)
		{
			this.Text=text;
			//this.Name=name;
			
			//
		}
	}
	 
}
