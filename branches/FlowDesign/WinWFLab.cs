using System;
using System.IO;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using BP.WF;
using BP.Win.Controls;

namespace BP.Win.WF
{
	/// <summary>
	/// WinWFLab ��ժҪ˵����
	/// </summary>
	public class WinWFLab :  WFNodeBase  //System.Windows.Forms.UserControl ,IPaint
	{
		public WinWFLab() 
		{

		}
        public WinWFLab(LabNote wfn)
        {
            this.HisNode = wfn;
            //  wfn.sSendToBack
            this.BackColor = Color.White;
            this.BindWFNode();
            this.SendToBack();
        }

		#region ����

		public  WinWFFlow ParentWinFlow
		{
			get
			{
				return this.Parent as WinWFFlow;
			}
		}
        private LabNote _HisNode = null;
        public LabNote HisNode
		{
			get
			{
				return this._HisNode ;
			}
			set
			{
				this._HisNode = value;
			}
		}
		
		public new string Text
		{
			get
			{
				return base.Text;
			} 
			set
			{
				base.Text = value;
				if(this.HisNode!=null)
					this.HisNode.Name = value;
				UpdateSize( Glo.NodeImagePath+  "LabNote.bmp" );
			}
		}
		#endregion ����


		#region ����¼�
		protected override void OnMouseDown(MouseEventArgs e )
		{
            this.SendToBack();

			base.OnMouseDown( e );
            return;

            if (Global.ToolIndex == 4)
			{
				WinWFFlow fcon = this.Parent as WinWFFlow;
               
				if( fcon.CurrentLine == null)
				{
					fcon.CurrentLine = new WinWFLine();
				//	fcon.CurrentLine.NodeBegin = this;
					fcon.CurrentLine.Name = this.Name;
				}
				else if(fcon.CurrentLine.NodeEnd ==null)
				{
                    if (fcon.CurrentLine.Name == this.Name)
                    {
                        fcon.CurrentLine = null;
                        return;
                    }

					//fcon.CurrentLine.NodeEnd = this;
                    if (fcon.CurrentLine.NodeEnd.HisNode.IsStartNode)
                    {
                        MessageBox.Show("��������ʼ�����ڵ㷽�������ߡ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                      //  fcon.CurrentLine.NodeEnd = null;
                        fcon.CurrentLine = null;
                        return;
                    }
                 

					fcon.CurrentLine.Name +="_"+this.Name;
					//fcon.CurrentLine.HisDirection =new Direction();
					fcon.CurrentLine.Text ="";
					if(fcon.CurrentLine.NodeBegin.HisNode!=null)
					{
						//fcon.CurrentLine.HisDirection.Node = fcon.CurrentLine.NodeBegin.HisNode.NodeID;
						fcon.CurrentLine.Text = fcon.CurrentLine.NodeBegin.Text ;
					}
					fcon.CurrentLine.Text += "��"; 
					if(fcon.CurrentLine.NodeEnd.HisNode!=null)
					{
						//fcon.CurrentLine.HisDirection.ToNode = fcon.CurrentLine.NodeEnd.HisNode.NodeID;
						fcon.CurrentLine.Text += fcon.CurrentLine.NodeEnd.Text;
					}
					fcon.AddLine( fcon.CurrentLine);
					fcon.AfterAddline();
				}
				else
					fcon.CurrentLine = null;
			}
		}
		#endregion


		#region �� ����
		public void BindWFNode()
		{
			if(_HisNode!=null && _HisNode.OID >0)
			{
                this.Name = _HisNode.OID.ToString();

				if(!File.Exists(this.MouseLeaveImageUrl) )
				{
					this.MouseLeaveImageUrl = "";
				}

				if(this.MouseLeaveImageUrl !="")  
				{
					this.BackgroundImage = Image.FromFile( this.MouseLeaveImageUrl);
					this.Size = this.BackgroundImage.Size;
					this.UserBackgroundImage = true;
				}
				else
					this.UserBackgroundImage = false;

				this.Text = _HisNode.Name;
				this.Location = new Point(_HisNode.X , _HisNode.Y);
                this.BackColor = Color.White;
                return;

				string err = "";
				try
				{
                    //err = "��ȡ HisNodePosType ����";
                    //this._positionType = _HisNode.HisNodePosType ; 
                    //err = "��ȡ HisNodeWorkType ����";
                    //this._workType = _HisNode.HisNodeWorkType;

                    //err = " ���ýڵ���ʽ Position ����";
					this.SetStyleByPosition();
					//err = " ���ýڵ���ʽ WorkType ����";
                    this.DoReSetNodeImg();
				}
				catch( Exception ex)
				{
					this.SetShowTip( err +"["+_HisNode.Name  +"] :\r\n"+ex.Message);
                    this.BackColor = Color.White; //��ʾ ��ȡHisNodePosTypeʱ����
				}
			}
		}
		private void SetStyleByPosition()
		{
            this.BackColor = Color.White;
            return;
		}
        private void DoReSetNodeImg()
        {
            this.SendToBack();

            string url = Glo.NodeImagePath + "LabNote.bmp";
            this.Image = Image.FromFile(url);
            this.UpdateSize(url);
        }
		#endregion ����
		
	}
}
