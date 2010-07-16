using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using CWAI.En.Base;
using CWAI.DA;
using CWAI.Port;
using CWAI.Portal;

using CWAI.En;
using CWAI;
using CWAI.Web.Controls;

namespace CWAI.Win32.Controls
{
	[ToolboxBitmap(typeof(System.Windows.Forms.Button))]
	public class TB : System.Windows.Forms.TextBox
	{
		#region ��չ����

		#region ���� DataHelpKey ����չ����
		/// <summary>
		/// ������Key. 
		/// </summary>
		public string DataHelpKey =null;		 
		/// <summary>
		/// ����Key
		/// </summary>
		public string AttrKey =null;
		/// <summary>
		/// Ĭ�Ͽ��
		/// </summary>
		public int DefaultWith =10;
	 
		#endregion

		#region ��ͨ����չ����
		/// <summary>
		/// TB����
		/// </summary>
		private TBType _ShowType=TBType.TB;
		/// <summary>
		/// TB����
		/// </summary>
		public TBType ShowType
		{
			get
			{
				return _ShowType;				
			}
			set
			{
				this._ShowType=value;
			}
		}
		/// <summary>
		/// ��������ص�������
		/// </summary>
		private string ClassName=null;		 
		#endregion

		#endregion

		#region �¼�

		//private void TBPreRender(object sender, System.EventArgs e )

		/// <summary>
		/// ���ú���Ϣ������������
		/// </summary>
		private void InitTB()
		{
			string script ,  url ;
			string appPath = "" ;
			switch ( ShowType )
			{
				case TBType.Ens : //�����Ҫ�ƶ���Ens.
					//this.Width=Unit.Pixel(this.DefaultWith);
					//url =appPath+"/Comm/DataHelp.htm?"+appPath+"/Comm/UIEns.aspx?ClassName="+this.DataHelpKey+"&IsDataHelp=1" ;
					//script=" if ( event.button != 2)  return; str="+this.ClientID+".value;str= window.showModalDialog('"+url+"&Key=\'+str, '','dialogHeight: 500px; dialogWidth:800px; dialogTop: 100px; dialogLeft: 100px; center: no; help: no'); if ( str==undefined) return ; "+this.ClientID+".value=str ; ";
					//this.Attributes["onmousedown"]=script;
					//this.ToolTip="�ҽ��߼����Ҳ�ѡ��";
					break;
				case TBType.Self : 
//					if (this.DataHelpKey==null)
//						throw new Exception("@��û��ָ��Ҫ�Key.");
//					DataHelp en = new DataHelp(this.DataHelpKey);					 
//					
//					url =appPath+"/Comm/DataHelp.htm?"+appPath+en.FileUrl;
//					script=" if ( event.button != 2)  return;  val=window.showModalDialog('"+url+"','','dialogHeight: "+en.Height+"px; dialogWidth: "+en.Width+"px; dialogTop: 100px; dialogLeft: 100px; center: no; help: no'); if ( val==undefined) return ; "+this.ClientID+".value=val ; ";
//					this.Attributes["onmousedown"]=script;
//					this.ToolTip=en.ToolTip;					 
//					break;

				case TBType.Date:
					//this.Attributes["size"]="10";					 
					if (this.Text==null || this.Text==null  )				
						this.Text= DataType.CurrentData;
//					url =appPath+"/Comm/Pub/CalendarHelp.htm";
//					script=" if ( event.button != 2)  return;  val=window.showModalDialog('"+url+"','','dialogHeight: 335px; dialogWidth: 340px; dialogTop: 100px; dialogLeft: 150px; center: no; help: no'); if ( val==undefined) return ; "+this.ClientID+".value=val ; ";
//
//					this.Attributes["onmousedown"]=script;				 
//					this.ToolTip="�Ҽ�ѡ�����ڡ�";
					break;
				case TBType.DateTime:					 
					//this.Attributes["size"]="16";					 
					if (this.Text==null || this.Text==null  )				
						this.Text= DataType.CurrentDataTime;									  
					 
//				 
//					if (this.ReadOnly==false)
//					{
//						url =appPath+"/Comm/Pub/CalendarHelp.htm";
//						script=" if ( event.button != 2)  return;  val=window.showModalDialog('"+url+"','','dialogHeight: 335px; dialogWidth: 340px; dialogTop: 100px; dialogLeft: 150px; center: no; help: no'); if ( val==undefined) return ; "+this.ClientID+".value=val ; ";
//						this.Attributes["onmousedown"]=script;				 
//						this.ToolTip="�Ҽ�ѡ������ʱ�䡣";
//					}
					break;
				case TBType.Email:
					if (this.Text==null || this.Text==null  )				
						this.Text="@";
					break;
				 
				case TBType.Key:
					if (this.Text==null)
						//this.Text=WebUser.FK_Dept;
					break;				 
				case TBType.Moneny:
					this.MaxLength=14;
					this.Width=14;
					 
					if (this.Text==null || this.Text==null || this.Text=="0" )				
						this.Text="0.00";
					if (this.Text.IndexOf(".")==-1)
						this.Text=this.Text+".00";					 
					break;
				case TBType.Num:
					this.MaxLength=14;
					this.Width=14;					
					if (this.Text==null || this.Text==null )				
						this.Text="0";					 
					break;
				case TBType.Int : 
					this.MaxLength=14;
					this.Width=14;
				 
					if (this.Text=="" || this.Text==null || this.Text=="0" )				
						this.Text="0";
					 
					break;	
				case TBType.TB :
//					if (this.ReadOnly==false)
//					{
//						if (this.ClassName!=null)
//						{
//							url =appPath+"/Comm/DataHelp.htm?"+appPath+"/Comm/UIUserDefaultValues.aspx?ClassName="+this.ClassName+"&AttrKey="+this.AttrKey ;
//							script=" if ( event.button != 2)  return; str="+this.ClientID+".value;str= window.showModalDialog('"+url+"&Key=\'+str, '','dialogHeight: 500px; dialogWidth:850px; dialogTop: 100px; dialogLeft: 100px; center: no; help: no'); if ( str==undefined) return ; "+this.ClientID+".value=str ; ";
//							this.Attributes["onmousedown"]=script;
//							this.ToolTip="�ҽ�ѡ������ö���Ĭ��ֵ��";
//						}						 
//					}
					break;
				default:
					break;
			}
			if (this.Text=="&nbsp;")
				this.Text=null;			 
		}

		#endregion

		#region ���췽��
		public TB()
		{
			//this.Prender += new System.EventHandler(this.TBPreRender);
		}
		/// <summary>
		/// ����
		/// </summary>
		/// <param name="attr">����</param>
		/// <param name="className">��</param>
		public TB(Attr attr, string className, bool isReadonly)
		{
			this.ClassName = className;
			this.MaxLength =attr.MaxLength;
			//this.Width = Unit.Pixel(attr.UIWidth ); 
			this.DefaultWith = attr.UIWidth;
			this.AttrKey=attr.Key;	
			 
			this.ReadOnly = isReadonly ;
			this.ShowType=attr.UITBShowType ;
			this.Width = attr.UIWidth;		 
		
			this.Visible =attr.UIVisible;
			this.DataHelpKey=attr.UIBindKey;
			this.ShowType = attr.UITBShowType;
			this.DataHelpKey = attr.UIBindKey;
		   
			//this.Style.Clear();
			//this.Style.Add("width",attr.UIWidth.ToString()+"px") ;			
			if (this.ReadOnly)
			{
				//this.CssClass="DGTBReadonly"+WebUser.Style;
				this.BackColor=Color.Coral;
			}
			else
			{
				//this.CssClass="DGTB"+WebUser.Style;
			}
			//this.PreRender += new System.EventHandler(this.TBPreRender);
		}
		#endregion


		#region ȡ����չ�����ԣ����ڷ���ȡ��Ϣ��
		/// <summary>
		/// ȡ����չTextExt����
		/// </summary>
		public object TextExt
		{
			get
			{
				return this.Text;
			}
			set
			{
				this.Text=value.ToString();
			}
		}
		/// <summary>
		/// ȡ����չInt����
		/// </summary>
		public int TextExtInt
		{
			get
			{
				return int.Parse(this.Text);
			}
			set
			{
				this.Text=value.ToString();
			}
		}
		/// <summary>
		/// ȡ����չFloat����
		/// </summary>
		public float TextExtFloat
		{
			get
			{
				return float.Parse(this.Text.Trim());
			}
			set
			{
				this.Text=value.ToString();
			}
		}
		/// <summary>
		/// ȡ����չDecimal����
		/// </summary>
		public decimal TextExtDecimal
		{
			get
			{
				string str = this.Text.Trim() ; 
				if (str.Length==0)
					str="0";
					
				return decimal.Parse(str);
			}
			set
			{				
				this.Text=Decimal.Round(value,2).ToString();
			}
		}
		/// <summary>
		/// ȡ����չ��������
		/// </summary>
		public string TextExtDate
		{
			get
			{
				return DataType.StringToDateStr(this.Text.Trim()) ;
			}
			set
			{				
				this.Text=DataType.StringToDateStr(value);
			}
		} 
		#endregion


	}
}
