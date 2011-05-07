using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data;
using System.Collections;
using System.Reflection;
using System.ComponentModel;
using BP.DA;


namespace BP.Win.Controls
{
	public enum ItemsType
	{
		Year,
		Month,
		Week,
		Day,
		Time,
		Minute,
		Compare,
		None
	}

	[ToolboxBitmap(typeof(System.Windows.Forms.ComboBox))]
	public class DDL : System.Windows.Forms.ComboBox
	{
		

		public DDL()
		{
			this.DropDownWidth=200;
			this.MaxDropDownItems=20;
		}

		private void InitializeComponent()
		{
		
		}
		#region ��д���� 
		protected override bool ProcessDialogKey(Keys keyData)
		{
			if(keyData==Keys.Return)
				return base.ProcessDialogKey(Keys.Tab);
			else
				return base.ProcessDialogKey(keyData);
		}
		#endregion ��д����

		#region ������Լ�����

		private ItemsType itemsType = ItemsType.None;
		public ItemsType ItemsType 
		{
			get { return this.itemsType ;} 
			set { this.itemsType = value ;} 
		}
		public void FillItemsByItemsType( ItemsType typ)
		{
			this.ItemsType = typ;
			this.FillItemsByItemsType();
		}
		public void FillItemsByItemsType()
		{
			int selectid = this.SelectedIndex;

			this.DataSource = null;
			this.Items.Clear();
			this.DropDownStyle = ComboBoxStyle.DropDownList;
			ListItems  items = new ListItems();

			switch(this.itemsType)
			{
				case ItemsType.None:
					return;
				case ItemsType.Time:
					this.DropDownStyle = ComboBoxStyle.DropDown;
					for(int i=0 ;i<24;i++)
					{
						for(int j=0;j<4;j++)
						{
							string str = i.ToString().PadLeft(2,'0');
							str += ":"+(j*15).ToString().PadLeft(2,'0');
							items.Add(new ListItem(str,str));
						}
					}
					break;
				case ItemsType.Minute:
					this.DropDownStyle = ComboBoxStyle.DropDown;
					for(int i=0 ;i<60;i++)
					{
						string str = i.ToString().PadLeft(2,'0');
						items.Add(new ListItem(str,str));
					}
					break;
				case ItemsType.Day:
					for(int i=1 ;i<=28;i++)
					{
						string str = i.ToString().PadLeft(2,'0');
						items.Add(new ListItem(str,i.ToString()));
					}
					break;
				case ItemsType.Month:
					for(int i=1 ;i<=12;i++)
					{
						//string str = i.ToString().PadLeft(2,'0');
						items.Add(new ListItem(i.ToString(),i.ToString()));
					}
					break;
				case ItemsType.Week:
					items.Add(new ListItem(DayOfWeek.Sunday ,"��"));
					items.Add(new ListItem(DayOfWeek.Monday ,"һ"));
					items.Add(new ListItem(DayOfWeek.Tuesday , "��"));
					items.Add(new ListItem(DayOfWeek.Wednesday ,"��"));
					items.Add(new ListItem(DayOfWeek.Thursday ,"��"));
					items.Add(new ListItem(DayOfWeek.Friday, "��"));
					items.Add(new ListItem(DayOfWeek.Saturday ,"��"));
					break;
				case ItemsType.Compare:
					items.Add(new ListItem("=","����"));
					items.Add(new ListItem(">=","���ڵ���"));
					items.Add(new ListItem("<=", "С�ڵ���"));
					items.Add(new ListItem(">","����"));
					items.Add(new ListItem("<","С��"));
					items.Add(new ListItem("!=","������"));
					items.Add(new ListItem(" like ", "����"));
					break;
				default:
					return;
			}
			this.BindDataListItems( items );

			if( selectid <0 || selectid>this.Items.Count )
				this.SelectedIndex = 0;
			else 
				this.SelectedIndex = selectid;
		}
		
		private Hashtable ItemTagTable = new Hashtable();
		public  object SelectedItemTag
		{
			get
			{
				if(this.SelectedValue==null)
					return null;
				else
					return ItemTagTable[this.SelectedValue];
			}
		}


		private bool _stopEvent = false ;
		protected override void OnSelectedValueChanged(EventArgs e)
		{
			if( !_stopEvent )
			{
				this.CurrentEditIndex = this.SelectedIndex;
				base.OnSelectedValueChanged (e);
			}
		}

		public int GetIndexByText(string text)
		{
			if(this.DVSource!=null)
			{
				if(DVSource.Sort!=this.DisplayMember)
					DVSource.Sort = this.DisplayMember;
				int ri = DVSource.Find(text);
				if(ri==-1)
				{
					DVSource.Sort = this.ValueMember;
					return -1;
				}
				object val = DVSource[ri][ValueMember];
				DVSource.Sort = this.ValueMember;
				ri = DVSource.Find(val);
				return ri;
			}
			Color co = this.ForeColor;
			this.ForeColor = this.BackColor ;
			int pos = -1;
			for(int i=0;i<this.Items.Count;i++)
			{
				this.SelectedIndex = i;
				if(this.Text == text)
					pos = i;
			}
			this.ForeColor = co;
			return pos;
		}
		public int GetIndexByValue(object val)
		{
			if(this.DVSource!=null)
			{
				if(DVSource.Sort!=this.ValueMember)
					DVSource.Sort = this.ValueMember;
				int ri = DVSource.Find(val);
				return ri;
			}
			else if(this.SelectedItem!=null)
			{
				_stopEvent = true ;
				if(val==null)
					return -1;
				int pos = -1;
				for(int i=0;i<this.Items.Count;i++)
				{
					this.SelectedIndex = i;
					if(this.SelectedValue.ToString() == val.ToString())
					{
						pos = i ;
						break;
					}
				}
				if(pos==-1)
					this.SelectedIndex = -1 ;

				_stopEvent = false ;
				this.OnSelectedValueChanged (null);
				return pos ;
			}
			return -1;
		}
		public string GetDisplayText( object val)
		{
			if(val.Equals(DBNull.Value))
				return "";
			if(this.DVSource!=null)
			{
				if(DVSource.Sort!=this.ValueMember)
					DVSource.Sort = this.ValueMember;
				int ri = DVSource.Find(val);
				if(ri>-1)
					return DVSource[ri][this.DisplayMember].ToString();
			}
			else if(this.SelectedItem!=null)
			{
				object text = this.SelectedItem.GetType().GetProperty(this.ValueMember).GetValue(this.SelectedItem ,null);
				if(text!=null)
					return text.ToString();
			}
			else
			{
				this.SelectedIndex = GetIndexByValue(val);
				if(SelectedIndex>-1)
					return this.Text;
			}
			return "";
		}
		public  DataView DVSource
		{
			get {return this.DataSource as DataView;}
		}
		public  DataRowView CurrentRow
		{
			get {return this.BindingContext[this.DataSource].Current as DataRowView;}
		}
		public  int  CurrentEditIndex
		{
			get{
				if(this.DataSource!=null)
					return this.BindingContext[this.DataSource].Position;
				else
					return -1;
			}
			set{
				if(this.DataSource!=null)
					this.BindingContext[this.DataSource].Position =value;
			}
		}
		#endregion ������Լ�����

		#region ���ݰ�
		public void BindEntitiesNoName(string Name)
		{
			object ds = ClassFactory.GetEns( Name);
			Type tp = ds.GetType();
			MethodInfo method = tp.GetMethod("RetrieveAll",new Type[0]);
			method.Invoke(ds,null);
			this.BindData( ds );
		}
		public void BindData(object DataSource)
		{
			this.BindData(DataSource ,"No","Name");
		}
		public void BindData(object DataSource,string valName,string textName)
		{
			IListSource ls = DataSource as IListSource;
			if( ls!=null && !ls.ContainsListCollection )//DataSource �Ǳ����ͼ
                this.BindDataDataView(ls.GetList() as DataView, valName, textName );
			else
                this.BindDataIList(DataSource as IList, valName, textName );
		}
		public void BindDataIList( IList list,string valName,string textName)
		{
			this.DataSource = null;
			this.Items.Clear();
			this.ItemTagTable.Clear();
			if( list==null || list.Count==0)
			{
				return;
			}
			PropertyInfo p1 = list[0].GetType().GetProperty(valName);
			PropertyInfo p2 = list[0].GetType().GetProperty(textName);

			MethodInfo mp = list.GetType().GetMethod("ToDataTable",new Type[0]);

			DataTable tb = null;
			if(p1!=null && p2!=null)
			{
				tb = new DataTable("IList");
				tb.Columns.Add(valName ,typeof(string));
				tb.Columns.Add(textName ,typeof(string));
				foreach(object en in list)
				{
					tb.Rows.Add(new object[]{p1.GetValue(en ,null) ,p2.GetValue(en ,null)});
				}
			}
			else if(mp!=null)
			{
				tb = mp.Invoke( list ,null) as DataTable;
				valName = tb.Columns[0].ColumnName;
				textName = tb.Columns[1].ColumnName;
			}
			else if(p1==null )
				MessageBox.Show("�Ҳ������ԡ�"+valName+"������������Դû��ToDataTable()������","��ʧ�ܣ�");
			else if(p2==null )
				MessageBox.Show("�Ҳ������ԡ�"+textName+"������������Դû��ToDataTable()������","��ʧ�ܣ�");

			if(tb!=null)
				this.BindDataDataView( tb.DefaultView ,valName,textName);
			else
			{
				MessageBox.Show("��ͼ�󶨵���֧�ֵ�����Դ������ToDataTable()��������null��","��ʧ�ܣ�");
			}
		}

		public void BindDataDataTable(DataTable dt,string valName,string textName)
		{
			if( dt !=null)
				this.BindDataDataView( dt.DefaultView ,valName,textName);
			else
				this.BindDataDataView( null ,valName,textName);
		}
		public void BindDataDataView(DataView dv,string valName,string textName)
		{
			this.DataSource = null;
			this.Items.Clear();
			if(dv ==null || dv.Count==0)
				return;

			if(dv.Table.Columns.IndexOf(valName)==-1)
			{
				MessageBox.Show("û���ҵ���Ӧ����["+valName+"]" ,"��ʧ�ܣ�");
				return;
			}
			if(dv.Table.Columns.IndexOf(textName)==-1)
			{
				MessageBox.Show("û���ҵ���Ӧ����["+textName+"]" ,"��ʧ�ܣ�");
				return;
			}
			this.DisplayMember = textName;
			this.ValueMember = valName;
			dv.Sort = ValueMember;
			this.DataSource = dv;
		}
	
		
		public void BindDataListItems( ListItems its)
		{
			ItemTagTable.Clear();
			this.DataSource = null ;
			this.Items.Clear();

			DataTable tb = new DataTable( "ListItems");
			tb.Columns.Add( "Value" ,typeof( string ));
			tb.Columns.Add( "Text" ,typeof( string ));
			foreach( ListItem li in its)
			{
				tb.Rows.Add( new object[]{li.Value ,li.Text});
				ItemTagTable.Add( li.Value , li.Tag );
			}
			this.DisplayMember = "Text";
			this.ValueMember = "Value";
			tb.DefaultView.Sort = ValueMember;
			this.DataSource = tb.DefaultView;    // ������ ValueMember ֮��
		}
		
		public void BindDataTableSchema( DataTable tb)
		{
			ListItems its = new ListItems();
			its.FillTableSchema( tb );
			this.BindDataListItems( its );
		}
		#endregion 
	
	}
}
