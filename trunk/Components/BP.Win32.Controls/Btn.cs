using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BP.Web.Controls;


namespace BP.Win32.Controls
{
	/// <summary>
	/// ��ť
	/// </summary>
	[ToolboxBitmap(typeof(System.Windows.Forms.Button))]
	public class Btn : System.Windows.Forms.Button
	{
		#region ��������
		/// <summary>
		/// ����
		/// </summary>
		private BtnType _BtnType=BtnType.Confirm ; 
		/// <summary>
		/// ����
		/// </summary>
		public BtnType BtnType
		{
			get
			{
				return _BtnType; 
			}
			set
			{
				_BtnType=value;
				this.OnLayout();
			}
		}
		#endregion
		
		#region ����
		public Btn()
		{
		//this.OnLayout+=this.OnLayout();
			
		}
		/// <summary>
		/// ������� 
		/// </summary>
		protected   void    OnLayout()
		{
		 
			switch (this.BtnType )
			{
				case BtnType.ApplyTask :
					if (this.Text==null || this.Text=="")
						this.Text="��������(A)";
					break;
				case BtnType.Refurbish :
					if (this.Text==null || this.Text=="") 			 
						this.Text="ˢ��(R)";
					break;
				case BtnType.Back :
					if (this.Text==null || this.Text=="") 			 
						this.Text="����(B)";
					break;
				case BtnType.Edit :
					if (this.Text==null || this.Text=="") 			 
						this.Text="�޸�(E)";
					break;
				case BtnType.Close :
					if (this.Text==null || this.Text=="") 			 
						this.Text="�ر�(Q)"; 
					break;
				case BtnType.Cancel :
					if (this.Text==null || this.Text=="") 			 
						this.Text="ȡ��(C)";					 
					break;				��
				case BtnType.Confirm :
					if (this.Text==null || this.Text=="")
						this.Text="ȷ��(O)";					 
					break;
				case BtnType.Search :
					if (this.Text==null || this.Text=="") 			 
						this.Text="����(F)";					 
					break;
				case BtnType.New :
					if (this.Text==null || this.Text=="") 			 
						this.Text="�½�(N)";					 
					break;
				case BtnType.SaveAndNew :
					if (this.Text==null || this.Text=="") 			 
						this.Text="���沢�½�(R)";					 
					break;
				case BtnType.Delete :
					if (this.Text==null || this.Text=="") 			 
						this.Text="ɾ��(D)";					 
					break;
				case BtnType.Export :
					if (this.Text==null || this.Text=="") 			 
						this.Text="����(G)";					 
					break;
				case BtnType.Insert :
					if (this.Text==null || this.Text=="") 			 
						this.Text="����(I)";					 
					break ;
				case BtnType.Print :
					if (this.Text==null || this.Text=="") 			 
						this.Text="��ӡ(P)";					 
					break ;
				case BtnType.Save :
					if (this.Text==null || this.Text=="") 			 
						this.Text="����(S)";					 
					break;
				case BtnType.View:
					if (this.Text==null || this.Text=="") 			 
						this.Text="���(V)";					 
					break;
				case BtnType.Add:
					if (this.Text==null || this.Text=="") 			 
						this.Text="����(A)";					 
					break;
				case BtnType.SelectAll:
					if (this.Text==null || this.Text=="") 			 
						this.Text="ȫѡ��(A)";					 
					break;
				case BtnType.SelectNone:
					if (this.Text==null || this.Text=="") 			 
						this.Text="ȫ��ѡ(N)";					 
					break;
				case BtnType.Reomve:
					if (this.Text==null || this.Text=="") 			 
						this.Text="�Ƴ�(M)";					 
					break;
				default:
					if (this.Text==null || this.Text=="")
						this.Text="ȷ��(O)";					 
					break; 
			}
		}
		#endregion
	}
}
