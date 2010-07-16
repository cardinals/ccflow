using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;

using BP.Web.Controls;
using BP.DA;
using BP.En;

using BP.Web;
using BP.Sys;

namespace BP.Win32.Controls
{
	[ToolboxBitmap(typeof(System.Windows.Forms.ListBox))]
	public class LB : System.Windows.Forms.ListBox
	{
		#region ˽�з���
		/// <summary>
		/// beforeBind
		/// </summary>
		private void beforeBind()
		{
			this.Items.Clear();
			if (AddAllLocation==AddAllLocation.Top || AddAllLocation==AddAllLocation.TopAndEnd )
			{
				Items.Add(new ListItem("all","-=ȫ��=-"));
				 
				 
			}
		}
		/// <summary>
		/// afterBind
		/// </summary>
		private void afterBind()
		{
			BindDataListItems();
			if (AddAllLocation==AddAllLocation.End || AddAllLocation==AddAllLocation.TopAndEnd )
			{
				Items.Add(new ListItem("all","-=ȫ��=-"));				
			}
		}

		#endregion

		#region ��������
		public string SelectedValueString
		{
			get
			{
				return	this.SelectedValue.ToString();
			}
			set
			{
				SetSelectedVal(value);
			}
		}
		public int SelectedValueInt
		{
			get
			{
				return int.Parse(this.SelectedValue.ToString()) ; 
			}
			set
			{
				SetSelectedVal(value.ToString());
			}		
		}
		#endregion 

		#region ��������
		/// <summary>
		/// all ���õ�λ��
		/// </summary>
		private AddAllLocation _AddAllLocation=AddAllLocation.None;
		/// <summary>
		/// all ���õ�λ��
		/// </summary>
		public AddAllLocation AddAllLocation
		{
			get
			{
				return _AddAllLocation;
			}
			set
			{
				_AddAllLocation=value;
			}
		}
		/// <summary>
		/// �������Դ���б�
		/// </summary>
		private  ListItems  _HisListItems= new ListItems();
		/// <summary>
		/// �������Դ���б�
		/// </summary>
		public ListItems HisListItems
		{
			get
			{
				return _HisListItems;
			}
			set
			{
				_HisListItems=value;
			}
		}
		#endregion

		#region ����
		public LB()
		{ 
		}
		#endregion


		#region bind entity
		

		/// <summary>
		/// ����Item
		/// </summary>
		/// <param name="text">text</param>
		/// <param name="val">val</param>
		public void AddItem(string text, string val)
		{
			this.HisListItems.Add( new ListItem(val,text)) ; 
		}
		/// <summary>
		/// ����Item
		/// </summary>
		/// <param name="text">text</param>
		/// <param name="val">val</param>
		/// <param name="tag">tag</param>
		public void AddItem(string text, string val, Object tag)
		{
			this.HisListItems.Add( new ListItem(val,text)) ; 
		}
		 
		/// <summary>
		/// BindEns
		/// </summary>
		/// <param name="ens">Entities</param>
		/// <param name="text">����text</param>
		/// <param name="val">����val</param>
		public void BindEns(Entities ens , string text, string val)
		{
			this.beforeBind();
			foreach(Entity en in ens)
			{
				this.AddItem(en.GetValStringByKey(text),en.GetValStringByKey(val),en) ; 
			}
			this.afterBind();
		}
		/// <summary>
		/// BindEns
		/// </summary>
		/// <param name="ens">Entities</param>
		/// <param name="text">����text</param>
		/// <param name="val">����val</param>
		public void BindItem(  string text, string val)
		{
			this.beforeBind();			 
			this.AddItem( text,val ) ;			 
			this.afterBind();
		}
		/// <summary>
		/// BindEns
		/// </summary>
		/// <param name="ens">Entities</param>
		/// <param name="text">����text</param>
		/// <param name="val">����val</param>
		public void BindEns(Entity en , string text, string val)
		{
			this.beforeBind();
			 
			this.AddItem(en.GetValStringByKey(text),en.GetValStringByKey(val),en) ; 
			 
			this.afterBind();
		}
		/// <summary>
		/// BindEns
		/// </summary>
		/// <param name="ens">Entities</param>
		/// <param name="text">����text</param>
		/// <param name="val">����val</param>
		public void BindEns(SysEnum en )
		{
			this.beforeBind();
			 
			this.AddItem(en.Lab,en.IntKey.ToString(),en) ; 
			 
			this.afterBind();
		}
		/// <summary>
		/// ö��
		/// </summary>
		/// <param name="ens"></param>
		public void BindEns(Sys.SysEnums ens)
		{
			BindEns(ens,SysEnumAttr.Lab,SysEnumAttr.IntKey);
		}
		/// <summary>
		/// EntitiesNoName
		/// </summary>
		/// <param name="ens">EntitiesNoName</param>
		public void BindEns(EntitiesNoName ens)
		{ 
			BindEns(ens,"Name","No");
		}
		/// <summary>
		/// ����ѡ���item
		/// </summary>
		/// <param name="selectedVal">selectedVal</param>
		public void SetSelectedVal(string selectedVal)
		{
			this.SelectedValue = selectedVal;			 
		}
		#endregion


		#region bind
		private Hashtable ItemTagTable = new Hashtable();

		private void BindDataListItems()
		{
			this.DataSource = null;
			DataTable tb = new DataTable( "ListItems");
			tb.Columns.Add( "Value" ,typeof( string ));
			tb.Columns.Add( "Text" ,typeof( string ));
			foreach( ListItem li in this.HisListItems )
			{
				tb.Rows.Add( new object[]{li.Value ,li.Text});
				ItemTagTable.Add( li.Value , li.Tag );
			}
			this.DisplayMember = "Text";
			this.ValueMember = "Value";
			tb.DefaultView.Sort = ValueMember;
			this.DataSource = tb.DefaultView;    // ������ ValueMember ֮��
		}
		#endregion
	}
}
