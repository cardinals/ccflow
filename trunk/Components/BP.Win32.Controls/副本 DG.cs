using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BP.Web.Controls;
using BP.DA;
using BP.En;
using BP.En.Base;
using BP.Web;
using BP.Sys;
using BP.Win32.Comm ; 


namespace BP.Win32.Controls
{
	/// <summary>
	/// ��ǰ��DataGride ״̬��
	/// </summary>
	public enum DGModel
	{
		/// <summary>
		/// none
		/// </summary>
		None,
		/// <summary>
		/// �༭Ens
		/// </summary>
		Ens,
		/// <summary>
		/// en
		/// </summary>
		En
	}
	[ToolboxBitmap(typeof(System.Windows.Forms.DataGrid))]
	public class DG : System.Windows.Forms.DataGrid
	{
		#region ����
		/// <summary>
		/// ����
		/// </summary>
		public DG()
		{
			//this.Navigate +=new NavigateEventHandler(DG_Navigate);
			//base.BackButtonClick+=new EventHandler(DG_BackButtonClick);
			_dgColumns = new DGColumns();
			
		}
		private DGColumns _dgColumns ;
		public  DGColumns Columns
		{
			get{ return this._dgColumns;  }
			set{ this._dgColumns = value; }
		}		
		#endregion

		#region �Զ�������
		
		/// <summary>
		/// DataGride ��״̬��
		/// </summary>
		public DGModel _DGModel=DGModel.None;
		/// <summary>
		/// DGModel
		/// </summary>
		public DGModel DGModel
		{
			get
			{
				return _DGModel;
			}
			set
			{
				_DGModel=value;
			}
		}
		private bool _IsReadonly=false;
		public bool IsReadonly
		{
			get
			{
				return _IsReadonly;
			}
			set
			{
				_IsReadonly=value;
			}
		}
		private Entities _HisEns ;
		 
		public Entities HisEns
		{
			get
			{
				return _HisEns;
			}
			set
			{
				_HisEns=value;
			}
		}		 
		private Entity _HisEn ;
		public Entity HisEn
		{
			get
			{
				if (_HisEn==null)
					_HisEn=this.HisEns.GetNewEntity ; 					
				return _HisEn;
			}
			set
			{
				_HisEn=value;
			}
		}		 
		#endregion

		#region Bind	
		/// <summary>
		/// ��������һ��ens	 
		/// </summary>
		/// <param name="ens">entity</param>
		public void BindEnsThisOnly(Entities ens, bool IsReadonly, bool IsFirstBind)
		{
			//this.UnSelectAll();
		//	this.CurrentRowIndex=0;
			//this.LastTimeRowIndex=-1;

			this.IsReadonly = IsReadonly;
			this.ReadOnly =false;		 


			this.DGModel =DGModel.Ens;
			this.HisEns = ens;

			DataSet ds = new DataSet();
			DataTable dt = new DataTable();
 
			if (ens.Count > 0)
			{
				dt= ens.ToDataTable();
			}
			else
			{
				dt= ens.RetrieveAllToTable();
			}

			
			dt.TableName = this.HisEn.EnDesc;
			ds.Tables.Add(dt);
			 
			this.SetDataBinding( ds,dt.TableName);
			if (IsFirstBind)
			{
				this.TableStyles.Clear();
			}
  
			#region ����Ҫ��д���¼���
			if (IsFirstBind)
			{
				this.CurrentCellChanged+=new EventHandler(DG_CurrentCellChanged);
				//this.ParentChanged+=new EventHandler(DG_ParentChanged);
				//this.Navigate+=new NavigateEventHandler(DG_Navigate);
				this.KeyPress+=new KeyPressEventHandler(DG_KeyPress);
			}
			#endregion

			if (IsFirstBind)
			{
				this.InitContextMenu();
				this.CaptionText=this.HisEn.EnDesc;
				this.InitColumn(this.HisEns);
			}
			return ;
		}
		public void ReSetDataSource(Entities ens)
		{
			DataSet ds=EnExt.ToDataSet(ens);
			this.SetDataBinding(ds,this.HisEn.EnDesc);

		}
		/// <summary>
		/// thisOnly
		/// </summary>
		/// <param name="ens">ens</param>
		/// <param name="thisOnly">�ǲ�����ʵ����</param>
		public void BindEns(Entities ens )
		{
			this.HisEns=ens;			 
			/* ���ֻ����һ��ens */

			this.SetDataBinding(EnExt.ToDataSet(ens),this.HisEn.EnDesc);


//			this.DataSource =EnExt.ToDataSet(ens);
//			this.DataMember=this.HisEn.EnDesc;		 

			#region ����Ҫ��д���¼���
			this.CurrentCellChanged+=new EventHandler(DG_CurrentCellChanged);
			this.ParentChanged+=new EventHandler(DG_ParentChanged);
			//this.Navigate+=new NavigateEventHandler(DG_Navigate);
			this.KeyPress+=new KeyPressEventHandler(DG_KeyPress);			
			#endregion

			this.CaptionText = this.HisEn.EnDesc ; 
			this.InitColumn();
			// ���Ի��˵���
			this.InitContextMenu();
		}
		 
		 
		 
		/// <summary>
		/// 
		/// </summary>
		/// <param name="e"></param>
		protected override void OnKeyPress(KeyPressEventArgs e)
		{
			base.OnKeyPress (e);
			MessageBox.Show(e.KeyChar.ToString());
		}
		/// <summary>
		/// ���Ի���
		/// </summary>
		public void InitColumn() 
		{
			this.InitColumn(this.HisEns); 

			foreach(BP.En.Base.EnDtl dtl in this.HisEn.EnMap.DtlsAll)
			{
				Entities ens = dtl.Ens;
				Entity en = ens.GetNewEntity;
				this.InitColumn(ens);
				foreach(BP.En.Base.EnDtl dtl1 in en.EnMap.DtlsAll)
				{
					this.InitColumn(dtl1.Ens);
				}
			}
		}
		/// <summary>
		/// ��ʼ����
		/// </summary> 
		public void InitColumn(Entities ens)
		{

			Entity en =ens.GetNewEntity ;
			this.DGModel = DGModel.Ens;
			Entity newEn= this.HisEns.GetNewEntity ;

			// ����һ�� table style.
			DataGridTableStyle dts = new DataGridTableStyle();
			dts.MappingName=en.EnDesc; 

			SysEnsUAC uac = new SysEnsUAC();

			if (uac.IsView==false)
				//throw new Exception("�����ܶ�["+en.EnDesc+"]�в鿴��Ȩ�ޡ�");

			if ( !(uac.IsDelete || uac.IsInsert || uac.IsUpdate))
				this.ReadOnly=true;		 

			Attr prviewAttr= new Attr();		
			foreach(Attr attr in en.EnMap.Attrs)
			{	
				#region �����ж����ǲ��� readonly
				if (this.IsReadonly)
				{
					if (attr.MyFieldType==FieldType.Enum
						|| attr.MyFieldType==FieldType.PKEnum 
						|| attr.MyFieldType==FieldType.FK 
						||  attr.MyFieldType==FieldType.PKFK )
					{
						DataGridTextBoxColumn myDataCol = new DataGridTextBoxColumn();
						myDataCol.HeaderText = attr.Desc;
						myDataCol.MappingName =attr.Key;
						myDataCol.NullText = newEn.GetValStringByKey(attr.Key);
						//myDataCol.ReadOnly = true;
						if (attr.UIContralType==UIContralType.DDL)
						{
							myDataCol.Width=0;
						}
						else
						{
							myDataCol.Width=attr.UIWidth ;
						}
						dts.GridColumnStyles.Add(myDataCol);
						continue;
					}
					else if(attr.MyDataType ==DataType.AppBoolean)
					{
						DataGridBoolColumn myDataCol = new DataGridBoolColumn();
						myDataCol.HeaderText = attr.Desc;
						myDataCol.MappingName =attr.Key;
						myDataCol.AllowNull =false;
						myDataCol.ReadOnly = true ;
						myDataCol.NullValue = newEn.GetValBooleanByKey(attr.Key);
						dts.GridColumnStyles.Add(myDataCol);
						continue;
					}
					else
					{
						DataGridTextBoxColumn myDataCol = new DataGridTextBoxColumn();
						myDataCol.HeaderText = attr.Desc;
						myDataCol.MappingName =attr.Key; 
						myDataCol.NullText = newEn.GetValStringByKey(attr.Key);
						myDataCol.ReadOnly = true;
						dts.GridColumnStyles.Add(myDataCol);
//						if (attr.UIVisible==false)
//						  myDataCol.Width=0;
						continue;
					}

					
				}

				#endregion
			 
				#region  ���Ա༭״̬��
				if (attr.MyDataType==DataType.AppDatetime || attr.MyDataType==DataType.AppDate )
				{  /*ʱ������*/
					if (attr.UIIsReadonly)
					{
						DataGridTextBoxColumn myDataCol = new DataGridTextBoxColumn();
						myDataCol.HeaderText = attr.Desc;
						myDataCol.MappingName =attr.Key;
						myDataCol.NullText = newEn.GetValStringByKey(attr.Key);
						myDataCol.ReadOnly =true;
						//myDataCol.Format="";
						dts.GridColumnStyles.Add(myDataCol) ;
						continue;
					}
					else
					{
						DGTimePickerColumn timePickerColumnStyle = 
							new DGTimePickerColumn();
						timePickerColumnStyle.MappingName =attr.Key;
						timePickerColumnStyle.HeaderText = attr.Desc;
						timePickerColumnStyle.Width = 100;
						timePickerColumnStyle.ReadOnly = !attr.UIIsReadonly ;
						dts.GridColumnStyles.Add(timePickerColumnStyle);
						continue;
					}
				}
				else if (attr.MyDataType==DataType.AppBoolean)
				{
					DataGridBoolColumn myDataCol = new DataGridBoolColumn();
					myDataCol.HeaderText = attr.Desc;
					myDataCol.MappingName =attr.Key;
					myDataCol.AllowNull =false;
					myDataCol.ReadOnly = !attr.UIIsReadonly;
					myDataCol.NullValue = newEn.GetValBooleanByKey(attr.Key);
					dts.GridColumnStyles.Add(myDataCol);
					continue;
				}
				else if ( attr.UIContralType==UIContralType.DDL )
				{
					prviewAttr=attr;
					DataGridTextBoxColumn myDataCol = new DataGridTextBoxColumn();
					myDataCol.HeaderText = attr.Desc;
					myDataCol.MappingName =attr.Key;
					myDataCol.NullText = newEn.GetValStringByKey(attr.Key);					
					myDataCol.Width=0; // ���ص�ֵ
					myDataCol.ReadOnly = attr.UIIsReadonly;
					dts.GridColumnStyles.Add(myDataCol) ;
					continue;
				}
				else if ( attr.MyFieldType==FieldType.RefText ) 
				{
					if (prviewAttr.UIIsReadonly==false)
					{
						DataGridTextBoxColumn myDataCol = new DataGridTextBoxColumn();
						myDataCol.HeaderText = attr.Desc;
						myDataCol.MappingName =attr.Key;
						//myDataCol.NullText = newEn.GetValStringByKey( myDataCol.MappingName );
						myDataCol.ReadOnly = true;
						dts.GridColumnStyles.Add(myDataCol) ;
					}
					else
					{
						DGEnsColumn myDataCol = new DGEnsColumn(prviewAttr);
						//myDataCol.HisAttr = prviewAttr;
						myDataCol.HeaderText = prviewAttr.Desc;
						myDataCol.MappingName =attr.Key ;
						myDataCol.myDDL.Enabled = true;
						//myDataCol.ReadOnly = attr.UIIsReadonly ; 
						dts.GridColumnStyles.Add(myDataCol);
						continue;
					}
				}
				else 
				{
					DataGridTextBoxColumn myDataCol = new DataGridTextBoxColumn();
					myDataCol.HeaderText = attr.Desc;
					myDataCol.MappingName =attr.Key; 
					myDataCol.NullText = newEn.GetValStringByKey(attr.Key);
					myDataCol.ReadOnly = attr.UIIsReadonly ;
					dts.GridColumnStyles.Add(myDataCol) ;
					continue;
				}
				//this.TableStyles.Add(myDataCol);
				#endregion 
			}
			this.TableStyles.Add(dts);			 
		}
		 
		#endregion

		#region ����
		public void SaveAllEns()
		{
			try
			{
				if (this.IsReadonly)
					return;
				Entities ens = this.CurrentDataEns;
				ens.Update();
				MessageBox.Show("����["+ens.Count+"]��¼���³ɹ�.");
			}
			catch(Exception ex)
			{
				PubClass.Alert(ex);				 
			}
		}
		#endregion

		#region ��д����
		/// <summary>
		/// OnCurrentCellChanged
		/// </summary>
		/// <param name="e"></param>
		protected override void OnCurrentCellChanged(EventArgs e)
		{
			base.OnCurrentCellChanged (e);			  
			if (this.DGModel !=DGModel.Ens) 
				return;
			return; 
		}
//		/// <summary>
//		/// �ڵ�����ť����ʱ�䴥�����¼�
//		/// </summary>
//		/// <param name="sender"></param>
//		/// <param name="e"></param>
//		private void DG_BackButtonClick(object sender, EventArgs e)
//		{
//			LastTimeRowIndex = 0 ;
//		}
		public Entities CurrentSelectedEns
		{
			get
			{
				Entities ens = this.HisEns.CreateInstance();


				int num = this.CurrentTable.Rows.Count ; 
//				for(int i=0; i< num; i++)
//				{
//					if (this.SetDataBindingi);
//				}
// 

				

				//foreach(


				 
			}
		}
		/// <summary>
		/// ��ǰѡ���Entity
		/// </summary>
		public Entity CurrentRowEn
		{
			get
			{
				if (this.CurrentRowIndex==-1)
					throw new Exception("û��ѡ���У�");
 
				Entity en =this.CurrentEns.GetNewEntity ;
				 
				int i = -1;					 
				foreach(Attr attr in en.EnMap.Attrs)
				{
					i++;					
					if (attr.MyDataType == DataType.AppBoolean)
					{
						// #warning ��ν���� checkbox ���.
						en.SetValByKey(attr.Key , 1 );
					}
					else
					{
						en.SetValByKey(attr.Key , this[this.CurrentRowIndex,i]);
					}
				}
				return en;
			}
		}
		/// <summary>
		/// ����ȫ����Ens
		/// </summary>
		public void UpdateAll()
		{
			Entities ens =this.CurrentDataEns;		 
			foreach(Entity en in ens)
			{
				en.Update();
			}
			MessageBox.Show("["+ens.Count+"]���³ɹ�");
		}		
		/// <summary>
		/// 
		/// </summary>
		public Entities CurrentDataEns
		{
			get
			{
				Entities ens =this.CurrentEns;
				ens.Clear();
				int currRowIndex=this.CurrentCell.RowNumber ;				 
				int rowNum = this.CurrentTable.Rows.Count;
				for(int i=0; i< rowNum;i++)
				{
					this.CurrentRowIndex=i;
					ens.AddEntity(this.CurrentRowEn); 
				}
				return ens;
			}
		}
		/// <summary>
		/// ��ȡ��ǰ������Table
		/// </summary>
		public DataTable CurrentTable
		{
			get
			{
				return this.HisDataSet.Tables[this.HisEn.EnDesc] ; 
			}
		}
		/// <summary>
		/// ����Դ
		/// </summary>
		public DataSet HisDataSet
		{
			get
			{
				return (DataSet)this.DataSource ; 
			}
		}

		#region ����ʵ��Ĳ���
		/// <summary>
		/// ����ʵ�壬 �ڻ���ʱ�䱣��ʵ�塣
		/// </summary>
		private void SaveEn()
		{
			//�ж�ʵ����û�б仯��
			if (this.IsReadonly)
				return ;
			// ��ǰ��ʵ����¡�
			this.CurrentRowEn.Update();
			return ;
		}
		private void NewEn()
		{

		}
		/// <summary>
		/// ɾ����ǰ��¼.
		/// </summary>
		public void DeleteAll()
		{
			try
			{
				Entities ens = this.CurrentDataEns;
				ens.Delete();
				MessageBox.Show("����["+ens.Count+"]��¼ɾ���ɹ�.");
			}
			catch(Exception ex)
			{
				PubClass.Alert(ex);				 
			}
		}
		public void DeleteSelectedEns()
		{
			//			if (MessageBox.Show("��Ҫɾ����["+this.CurrentRowIndex+"]�����ݣ�Ҫɾ����","ɾ��ȷ��", MessageBoxButtons.YesNo,MessageBoxIcon.Warning,MessageBoxDefaultButton.Button2,MessageBoxOptions.RightAlign)==DialogResult.No)
			//				return ;
			this.CurrentSelectedEns.Delete();

		}
		/// <summary>
		/// ɾ��ʵ��
		/// </summary>
		public void DeleteCurrentRowEn()
		{
			if (this.CurrentRowIndex == -1)
			{
				MessageBox.Show("û��ѡ���С�");
				return ;
			}

			if (MessageBox.Show("��Ҫɾ����["+this.CurrentRowIndex+"]�����ݣ�Ҫɾ����","ɾ��ȷ��", MessageBoxButtons.YesNo,MessageBoxIcon.Warning,MessageBoxDefaultButton.Button2,MessageBoxOptions.RightAlign)==DialogResult.No)
				return ;

			try
			{
				this.CurrentRowEn.Delete();
			}
			catch(Exception ex)
			{
				MessageBox.Show("ɾ��������������"+ex.Message);
			}
			DataSet ds = (DataSet)this.DataSource ;
			ds.Tables[this.DataMember].Rows[this.CurrentCell.RowNumber].Delete();
			return ;
		}
		#endregion
		 
		public void UnSelectAll()
		{
			int num = this.CurrentTable.Rows.Count ; 
			for(int i=0; i< num; i++)
			{
				this.UnSelect(i);
			}
		}
		/// <summary>
		/// �ڼ�������¼�ʱ�ɷ�����
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DG_CurrentCellChanged(object sender, EventArgs e)
		{
			if (this.DGModel!=DGModel.Ens)
				return ;  
		}
	 

		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown (e);

			if (DGModel!=DGModel.Ens)
				return ; 
			return;

			if ( ! (e.Button==System.Windows.Forms.MouseButtons.Right || e.Button==System.Windows.Forms.MouseButtons.Left) )
			{
				/* �����겻�������Ҽ� ���Ͳ���������¼���*/
				return ;
			}

			System.Windows.Forms.DataGrid.HitTestInfo hti;			 
				hti = this.HitTest(e.X, e.Y);
			try
			{
				CurrentRowIndex = hti.Row ; 
			}
			catch
			{

			}

			string message = "You clicked ";
			 
 
			switch (hti.Type) 
			{
				case System.Windows.Forms.DataGrid.HitTestType.None :
					this.ContextMenu=null;
					message += "the background.";
					break;
				case System.Windows.Forms.DataGrid.HitTestType.Cell :
					// �������cell�ϡ�
					if (e.Button==System.Windows.Forms.MouseButtons.Right)
					{
						if (this.CurrentAttr.IsCanUseDefaultValues)
						    this.SetContextMenuItemEnable("DefaultValues",true);
//						this.ContextMenu.Show(this, new Point(e.X,e.Y));
					}
					message += "cell at row " + hti.Row + ", col " + hti.Column;
					break;
				case System.Windows.Forms.DataGrid.HitTestType.ColumnHeader :
				{
					//this.CurrentCell.RowNumber =hti.Column ;
					this.ContextMenu=null;
					message += "the column header for column " + hti.Column;
					break;
				}
				case System.Windows.Forms.DataGrid.HitTestType.RowHeader :
					// ��row header �ϣ�����ʹ�û�ȡ������Ĭ��ֵ��
					if (e.Button==System.Windows.Forms.MouseButtons.Right)
					{					
						this.SetContextMenuItemEnable("DefaultValues",false);
						this.ContextMenu.Show(this, new Point(e.X,e.Y)) ;					  
					}
					message += "the row header for row " + hti.Row;
					break;
				case System.Windows.Forms.DataGrid.HitTestType.ColumnResize :
					this.ContextMenu=null;
					message += "the column resizer for column " + hti.Column;
					break;
				case System.Windows.Forms.DataGrid.HitTestType.RowResize :
					this.ContextMenu=null;
					message += "the row resizer for row " + hti.Row;
					break;
				case System.Windows.Forms.DataGrid.HitTestType.Caption :
					this.ContextMenu=null;
					message += "the caption";
					break;
				case System.Windows.Forms.DataGrid.HitTestType.ParentRows :
					this.ContextMenu=null;
					 
					message += "the parent row";
					break;
			}

		//	MessageBox.Show(message) ; 


			if (e.Button==System.Windows.Forms.MouseButtons.Right)
			{
				/* ���Ҽ�����ʱ�� */

				//				CMenu menu= new CMenu();
				//				this.ContextMenu = menu;
				//				menu.Popup+=new EventHandler(menu_Popup);
				//				this.ContextMenu = menu;
				

			}
			else if (e.Button==System.Windows.Forms.MouseButtons.Left)
			{
				/*���������ʱ��*/
				

			}	
		}
		 
		/// <summary>
		/// ��ʼ��cell�Ҽ��˵�
		/// </summary>
		private void InitContextMenu()
		{
			/* ��������Ҽ��� 
						 * 1������ ��ȡ��������Ĭ��ֵ��
						 * 2������ ɾ���С�
						 * 3�������ǰ��������fk.����༭xxx. �򿪺��ж�Ȩ�޵����⣬Ϊ����߹���Ч�ʡ�
						 * */
			ContextMenu cm = new ContextMenu();
			this.ContextMenu= cm;

			BPMenuItem midel = new BPMenuItem();
			midel.Click +=new EventHandler(ContextMenu_Click); // �����¼�
			midel.Text="ɾ����";
			midel.Enabled=true;
			midel.Tag="Delete";
			midel.Visible=true;
			midel.ShowShortcut=true;
			midel.Shortcut = Shortcut.CtrlD; // shen
			this.ContextMenu.MenuItems.Add(midel);

			BPMenuItem miDefaultValues = new BPMenuItem();
			miDefaultValues.Click +=new EventHandler(ContextMenu_Click); // �����¼�
			miDefaultValues.Text="��ȡ������Ĭ��ֵ";			 
			miDefaultValues.Enabled=true; 
			miDefaultValues.Tag="DefaultValues";
			miDefaultValues.ShowShortcut=true;
			miDefaultValues.Shortcut = Shortcut.CtrlH;
			this.ContextMenu.MenuItems.Add(miDefaultValues);

			BPMenuItem miCard = new BPMenuItem();
			miCard.Click +=new EventHandler(ContextMenu_Click); // �����¼�
			miCard.Text="��Ϣ�༭";
			miCard.Enabled=true;
			miCard.Tag="Card";
			miCard.ShowShortcut=true;
			miCard.Shortcut = Shortcut.CtrlO;
			this.ContextMenu.MenuItems.Add(miCard);

			BPMenuItem miNew = new BPMenuItem();
			miNew.Click +=new EventHandler(ContextMenu_Click); // �����¼�
			miNew.Text="�½�";
			miNew.Enabled=true;
			miNew.Tag="New";
			miNew.ShowShortcut=true;
			miNew.Shortcut = Shortcut.CtrlN;

			this.ContextMenu.MenuItems.Add(miNew);

			#region ����������ϸ			
			EnDtls enDtls= this.HisEn.EnMap.Dtls;
			if ( enDtls.Count > 0 )
			{								
				foreach(EnDtl enDtl in enDtls)
				{	 
					BPMenuItem miDtl = new BPMenuItem();
					miDtl.Click +=new EventHandler(ContextMenu_Click); // �����¼�
					miDtl.Text=enDtl.Desc;
					miDtl.Enabled=true;
					miDtl.Tag=enDtl.ClassName ;
					miDtl.ShowShortcut=true;
					miDtl.Shortcut=Shortcut.CtrlR ;
					this.ContextMenu.MenuItems.Add(miDtl);
				}
			}
			#endregion

			#region ����һ�Զ��ʵ��༭
			AttrsOfOneVSM oneVsM= this.HisEn.EnMap.AttrsOfOneVSM;
			if ( oneVsM.Count > 0 )
			{
				
				foreach(AttrOfOneVSM vsM in oneVsM)
				{
					BPMenuItem miVsM = new BPMenuItem();
					miVsM.Click +=new EventHandler(ContextMenu_Click); // �����¼�
					miVsM.Text=vsM.Desc;
					miVsM.Enabled=true;
					miVsM.Tag=vsM.EnsOfMM.ToString() ;
					miVsM.ShowShortcut=true;
					miVsM.Shortcut=Shortcut.CtrlR ;
					this.ContextMenu.MenuItems.Add(miVsM);
				}
			}
			#endregion

			 


		}
		/// <summary>
		/// �Ҽ��˵����¼�����
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ContextMenu_Click(object sender, EventArgs e)
		{
			BPMenuItem item = (BPMenuItem)sender; 
			switch(item.Tag)
			{
				case "New":
					NewEn();
					break;
				case "Delete":
					DeleteCurrentRowEn();
					break;
				case "Card":
					this.Card();
					break;
				case "DefaultValues":
					this.InvokeDefaultValues();
					break;
				default:
					UIEnsRelation uir =new UIEnsRelation();
					uir.BindEn(this.CurrentRowEn,this.IsReadonly) ;
					uir.ShowDialog();					
					return ;
			}
		}

		public Attr CurrentAttr
		{
			get
			{
				return this.CurrentEns.GetNewEntity.EnMap.Attrs[this.CurrentCell.ColumnNumber] ;
			}
		}
		public void InvokeDefaultValues()
		{
			BP.Win32.Controls.UIWinDefaultValues ui = new UIWinDefaultValues(this.CurrentEns.GetNewEntity, this.CurrentAttr.Key );
		}
		public void Card()
		{
			UIEn ui = new UIEn();
			ui.SetData(this.CurrentRowEn,this.IsReadonly);
			ui.ShowDialog();
			return ;
		}
		/// <summary>
		/// ����һ���˵��Ƿ����ʹ��
		/// </summary>
		/// <param name="Tag">����</param>
		/// <param name="enable">true/false</param>
		public void SetContextMenuItemEnable(string Tag, bool enable)
		{
			try
			{
				if ( this.ContextMenu.MenuItems==null)
					this.InitContextMenu();
			}
			catch
			{
				this.InitContextMenu();
				this.SetContextMenuItemEnable(Tag,enable) ; 
			}
				
			foreach(BPMenuItem menu in this.ContextMenu.MenuItems)
			{
				if (menu.Tag == Tag)
				{
					menu.Enabled = enable;
					return;
				}
			}
		}
 
		/// <summary>
		/// �����¼�
		/// </summary>
		/// <param name="e"></param>
		protected override void OnClick(EventArgs e)
		{
			base.OnClick (e);
			
		}
		/// <summary>
		/// ˫���¼�
		/// </summary>
		/// <param name="e"></param>
		protected override void OnDoubleClick(EventArgs e)
		{
			base.OnDoubleClick (e);

			if (DGModel==DGModel.Ens)
			{
				if (this.CurrentRowIndex ==-1)
					return ;
				
				this.Card();
				//if (this.CurrentCell==null)
				//	return;
				//UIWinDefaultValues win = new UIWinDefaultValues(this.HisEn, this.HisEn.EnMap.Attrs[this.CurrentCell.ColumnNumber].Key) ;
			}
			
		}	
		
		//protected override void 

		#endregion
		/// <summary>
		/// ������
		/// </summary>
		private string CaptionTextOfShortName
		{
			get
			{
				if (this.DataMember.IndexOf( "." )==-1)
					return this.DataMember;
				int i =this.DataMember.LastIndexOf( "." ) ;
				return this.DataMember.Substring(i+1  );			 
			}
		}		
		/// <summary>
		/// ��õ�ǰ�༭��
		/// </summary>
		private Entities _CurrentEns=null;
		/// <summary>
		/// ��õ�ǰ�༭��Ens
		/// </summary>
		private Entities CurrentEns
		{
			get
			{
				if (_CurrentEns==null)
				{
					if (this.HisEn.EnDesc==this.CaptionTextOfShortName)
					{
						_CurrentEns= this.HisEns ;
						return _CurrentEns;
					}
					foreach(EnDtl en in this.HisEn.EnMap.Dtls)
					{
						Entity entity = en.Ens.GetNewEntity ; 
						if (entity.EnDesc==this.CaptionTextOfShortName)
						{
							_CurrentEns= en.Ens;
							return _CurrentEns ; 
							
						}
						else
						{
							foreach(EnDtl dtl in entity.EnMap.Dtls)
							{
								if (dtl.Ens.GetNewEntity.EnDesc==this.CaptionTextOfShortName)
								{
									_CurrentEns= dtl.Ens ;
									return _CurrentEns ; 
								}								
							}
						}
					}

					foreach(AttrOfOneVSM en in this.HisEn.EnMap.AttrsOfOneVSM)
					{
						if (en.EnsOfMM.GetNewEntity.EnDesc==this.CaptionTextOfShortName)
						{
							_CurrentEns = en.EnsOfMM;
							return _CurrentEns;
						}
					}
				}
				else
				{
					return _CurrentEns;
				}

				throw new Exception("û���ҵ���ǰ������Ens");
			}
		}
//		private void DG_Navigate(object sender, NavigateEventArgs ne)
//		{
//			LastTimeRowIndex = 0 ;
//			this.CaptionText = this.DataMember;		 
//		}

		private void DG_ParentChanged(object sender, EventArgs e)
		{
			MessageBox.Show("DG_ParentChanged: sender  peng, "+sender.ToString()+" sss " + e.ToString() ) ; 
		}

		private void DG_KeyPress(object sender, KeyPressEventArgs e)
		{
			MessageBox.Show(e.KeyChar.ToString()) ;
		}

		#region ȡ�� PK val��
		/// <summary>
		///  ���ҵ�ǰ��ѡ��� OID �� �����ڶ�OID �� key �ġ������
		/// </summary>
		public int CurrendSelectedOID
		{
			get
			{
				return int.Parse(this.CurrendSelectedNo);
			}
		}
		/// <summary>
		///  ���ҵ�ǰ��ѡ��� No �� ������No �� key �������
		/// </summary>
		public string CurrendSelectedNo
		{
			get
			{				
				if (this.CurrentCell.RowNumber < 0  )
					throw new Exception("@û��ѡ���С�");
				throw new Exception("��û��ʵ�֡�");
 
			}
		}
		#endregion
	}
}
