using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BP.Web.Controls;
using BP.DA;
using BP.En;

using BP.Web;
using BP.Sys;
using BP.Win32.Comm ; 


namespace BP.Win32.Controls
{
	
	[ToolboxBitmap(typeof(System.Windows.Forms.DataGrid))]
	public class DG : System.Windows.Forms.DataGrid
	{

		#region ����
		/// <summary>
		/// ����
		/// </summary>
		public DG()
		{
		
			this.CurrentCellChanged+=new EventHandler(DG_CurrentCellChanged);
			//this.CurrentCellChanged

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
		public bool IsDGReadonly
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
		//private void 
		public Entities HisEns=null;

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
		/// bind
		/// </summary>
		public void Bind()
		{
			this.BindEnsThisOnly(this.HisEns,this.IsDGReadonly,false);
			this.PivCell=new DataGridCell(-1,-1);
		}
		/// <summary>
		/// ��������һ��ens	 
		/// </summary>
		/// <param name="ens">entity</param>
		public void BindEnsThisOnly(Entities ens, bool IsReadonly, bool IsFirstBind)
		{
			//this.UnSelectAll();
			//	this.CurrentRowIndex=0;
			//this.LastTimeRowIndex=-1;

			this.IsDGReadonly = IsReadonly;
			if (this.IsDGReadonly)
				this.ReadOnly =true;
			else
				this.ReadOnly=false;


			this.DGModel =DGModel.Ens;
			if	(ens.Count < 0 )
				ens.RetrieveAll();

			this.HisEns = ens;
			this.HisEn=ens.GetNewEntity;

			//DataSet ds = new DataSet();
			//DataTable dt = new DataTable();
 			 
			this.CurrentTable= ens.ToDataTableField();
			this.CurrentTable.TableName = this.HisEn.EnDesc;
			//ds.Tables.Add(dt);
			//this.CurrentTable.RowChanging+=new DataRowChangeEventHandler(dt_RowChanging);
			//this.CurrentTable.RowChanged+=new DataRowChangeEventHandler(CurrentTable_RowChanged);
			//this.CurrentTable.
			//			DataView dv = new DataView();
			//			dv.Table=dt;
			//			dv.AllowNew=false;
			//			dv.AllowEdit=false;
			//			dv.AllowDelete=true;
			 
			//this.IsReadonly;
			this.SetDataBinding(this.CurrentTable,"");
			//this.SetDataBinding(dv,"");

			if (IsFirstBind)
			{
				this.TableStyles.Clear();
				this.InitContextMenu();
				this.CaptionText=this.HisEn.EnDesc;
				this.InitColumn(this.HisEns);
			}
		}
		public void ReSetDataSource_del(Entities ens)
		{
			DataSet ds=EnExt.ToDataSet(ens);
			this.SetDataBinding(ds,this.HisEn.EnDesc);
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
		public void InitColumn_del() 
		{
			this.InitColumn(this.HisEns); 

			foreach(BP.En.EnDtl dtl in this.HisEn.EnMap.DtlsAll)
			{
				Entities ens = dtl.Ens;
				Entity en = ens.GetNewEntity;
				this.InitColumn(ens);
				foreach(BP.En.EnDtl dtl1 in en.EnMap.DtlsAll)
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
			Entity newEn= this.HisEns.GetNewEntity;

			// ����һ�� table style.
			DataGridTableStyle dts = new DataGridTableStyle();
			dts.MappingName=en.EnDesc;

			UAC uac = new UAC();
			if (uac.IsView==false)
				throw new Exception("�����ܶ�["+en.EnDesc+"]�в鿴��Ȩ�ޡ�");

			//			if ( !(uac.IsDelete || uac.IsInsert || uac.IsUpdate) )
			//				this.ReadOnly=true;

			Attr prviewAttr= new Attr();		
			foreach(Attr attr in en.EnMap.Attrs)
			{	
				#region �����ж����ǲ��� readonly
				if (this.IsDGReadonly)
				{
					if (attr.MyFieldType==FieldType.Enum
						|| attr.MyFieldType==FieldType.PKEnum 
						|| attr.MyFieldType==FieldType.FK 
						||  attr.MyFieldType==FieldType.PKFK )
					{
						DataGridTextBoxColumn myDataCol = new DataGridTextBoxColumn();
						myDataCol.HeaderText = attr.Desc;
						myDataCol.MappingName =attr.Key;
						//myDataCol.NullText = newEn.GetValStringByKey(attr.Key);
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
						//myDataCol.NullValue = newEn.GetValBooleanByKey(attr.Key);
						dts.GridColumnStyles.Add(myDataCol);
						continue;
					}
					else
					{
						DataGridTextBoxColumn myDataCol = new DataGridTextBoxColumn();
						myDataCol.HeaderText = attr.Desc;
						myDataCol.MappingName =attr.Key; 
						//myDataCol.NullText = newEn.GetValStringByKey(attr.Key);
						myDataCol.ReadOnly = true;
						dts.GridColumnStyles.Add(myDataCol);
						//						if (attr.UIVisible==false)
						//						  myDataCol.Width=0;
						continue;
					}					
				}
				#endregion
			 
				#region  ���Ա༭״̬��
                if (attr.MyDataType == DataType.AppDateTime || attr.MyDataType == DataType.AppDate)
				{  /*ʱ������*/
					if (attr.UIIsReadonly)
					{
						DataGridTextBoxColumn myDataCol = new DataGridTextBoxColumn();
						myDataCol.HeaderText = attr.Desc;
						myDataCol.MappingName =attr.Key;
						//myDataCol.NullText = newEn.GetValStringByKey(attr.Key);
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
					//myDataCol.NullValue = newEn.GetValBooleanByKey(attr.Key);
					dts.GridColumnStyles.Add(myDataCol);
					continue;
				}
				else if ( attr.UIContralType==UIContralType.DDL )
				{
					prviewAttr=attr;
					DataGridTextBoxColumn myDataCol = new DataGridTextBoxColumn();
					myDataCol.HeaderText = attr.Desc;
					myDataCol.MappingName =attr.Key;
					//myDataCol.NullText = newEn.GetValStringByKey(attr.Key);					
					//myDataCol.Width=10; // ���ص�ֵ
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
					//myDataCol.NullText = newEn.GetValStringByKey(attr.Key);
					myDataCol.ReadOnly = attr.UIIsReadonly ;
					dts.GridColumnStyles.Add(myDataCol) ;
					if (attr.UIVisible==false)
					{
						myDataCol.Width=0;
					}
					continue;
				}
				//this.TableStyles.Add(myDataCol);
				#endregion 
			}
			this.TableStyles.Add(dts);			 
		}
		 
		#endregion

		#region ����		
		public void Save123()
		{
			try
			{
				if (this.HisEns.Count==0)
					return;
				this.CurrentCell=new DataGridCell(0,0); // this.PivCell;

				if (this.IsDGReadonly)
					return;

				string errorMsg=null;
				int i=0, okNum=0,errorNum=0;
				foreach(Entity en in this.HisEns)
				{
					try
					{
						en.Save();
						okNum++;
					}
					catch(Exception ex)
					{
						errorNum++;
						errorMsg+="@�ڱ���["+i+"]��¼ʱ��������´���----- .@"+ex.Message;
					}
					i++;
				}
				if (errorMsg!=null)
					throw new Exception("����["+okNum+"]��¼���³ɹ�,["+errorNum+"]����ʧ��."+errorMsg);
				 
				this.Bind();
				//PubClass.Information("����["+okNum+"]��¼���³ɹ�");
			}
			catch(Exception ex)
			{
				PubClass.Alert(ex); 
			}
		}		
		public void Save()
		{
			try
			{
				if (this.HisEns.Count==0)
					return;

				this.CurrentCell=new DataGridCell(0,0);
				if (this.IsDGReadonly)
					return;

				string errorMsg=null;
				int i=0, okNum=0,errorNum=0;
				Entities ens  =this.CurrentEnsInDG;
				foreach(Entity en in ens)
				{
					try
					{
						en.Save();
						okNum++;
					}
					catch(Exception ex)
					{
						errorNum++;
						errorMsg+="@�ڱ���["+i+"]��¼ʱ��������´���----- .@"+ex.Message;
					}
					i++;
				}
				if (errorMsg!=null)
					throw new Exception("����["+okNum+"]��¼���³ɹ�,["+errorNum+"]����ʧ��."+errorMsg);
				 
				this.HisEns  = ens;
				this.Bind(); 
				PubClass.Information("����["+okNum+"]��¼���³ɹ�");
			}
			catch(Exception ex)
			{
				PubClass.Alert(ex); 
			}
		}		
		#endregion

		#region ��д����
		/// <summary>
		/// ��ǰѡ���Ens.
		/// </summary>
		public Entities CurrentSelectedEns
		{
			get
			{
				Entities ens = this.HisEns.CreateInstance();
				int num = this.HisEns.Count ; 
				for(int i=0; i< num; i++)
				{
					if (this.IsSelected(i))
						ens.AddEntity(this.GetEnByRowIndex(i));
				}
				return ens;
			}
		}
		public Entities CurrentEnsInDG
		{
			get
			{
				Entities ens = this.HisEns.CreateInstance();
				int num = this.HisEns.Count ; 
				for(int i=0; i< num; i++)
				{
					ens.AddEntity(this.GetEnByRowIndex(i));
				}
				return ens;
			}
		}
		public Entity GetEnByRowIndex(int rowIndex)
		{
			Entity en = this.HisEns.GetNewEntity;
			int i = -1;
			foreach(Attr attr in en.EnMap.Attrs)
			{
				i++;
				if (attr.MyDataType == DataType.AppBoolean)
				{
					// #warning ��ν���� checkbox ���.
					en.SetValByKey(attr.Key,1);
				}
				else
				{
					en.SetValByKey(attr.Key ,this[rowIndex,i] );
				}	
			}
			return en;
		}
    	/// <summary>
		/// ��ǰѡ���Entity
		/// </summary>
		public Entity CurrentRowEn
		{
			get
			{
				if (this.CurrentRowIndex==-1)
					throw new Exception("û��ѡ���У�����");
				return this.GetEnByRowIndex(this.CurrentRowIndex);
			}
		}		 
		/// <summary>
		/// ��ȡ��ǰ������Table
		/// </summary>
		public DataTable CurrentTable=null;	 
		#region ����ʵ��Ĳ���
		 
		private void NewEn()
		{

		}
		/// <summary>
		/// ɾ��ȫ����
		/// </summary>
		public void DeleteAll()
		{
			if (MessageBox.Show("׼��ɾ�� "+this.HisEns.Count+" �У�\t\r\t\r�������ǣ���������ɾ����Щ�У������޷����������ĸ��ģ�", "ɾ��ȷ��", MessageBoxButtons.YesNo,MessageBoxIcon.Warning,MessageBoxDefaultButton.Button2,MessageBoxOptions.DefaultDesktopOnly)==DialogResult.No)
				return;

			try
			{
				int num = this.HisEns.Count;
				for(int i=0; i< num; i++)
				{
					//this.CurrentTable.Rows[i].Delete();
					this.HisEns[0].Delete();
					this.HisEns.RemoveAt(0);
				}
				this.HisEns.Clear();
				this.Bind();
			}
			catch(Exception ex)
			{
				this.Bind();
				PubClass.Alert(ex);	
			 
			}
		}
		/// <summary>
		/// ɾ����ǰѡ���¼.
		/// </summary>
		public void DeleteSelected()
		{
			try
			{
				Entities ens = this.CurrentSelectedEns;
				if (MessageBox.Show("׼��ɾ�� "+ens.Count+" �У�\t\r\t\r�������ǣ���������ɾ����Щ�У������޷����������ĸ��ģ�","ɾ��ȷ��", MessageBoxButtons.YesNo,MessageBoxIcon.Warning,MessageBoxDefaultButton.Button2,MessageBoxOptions.DefaultDesktopOnly)==DialogResult.No)
					return;

				ens.Delete();

				/*
				int num = this.HisEns.Count;
				for(int i=0; i< num; i++)
				{
					if (this.IsSelected(i))
					{
						if (this.HisEns[i].IsExits)
						{
							this.HisEns[i].Delete();
							this.HisEns.RemoveAt(i);
						}
						else
							this.HisEns.RemoveAt(i);
					}
				}
				*/
				//this.Bind();
			}
			catch(Exception ex)
			{
				//this.Bind();
				PubClass.Alert(ex);				 
			}
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
		private DataGridCell PivCell = new DataGridCell(-1,-1);
		public bool IsActive=true;
		//private int PivColumnNumber=0;
		/// <summary>
		/// �ڼ�������¼�ʱ�ɷ�����
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DG_CurrentCellChanged(object sender, EventArgs e)
		{
			if (this.IsDGReadonly)
				return ;

			if ( PivCell.RowNumber==-1)
			{
				this.PivCell=this.CurrentCell;
				if (this.CurrentRowIndex > this.HisEns.Count-1)
				{
					/* ��������У� ����֮��͵����У���*/
					Entity en=this.HisEns.GetNewEntity;
					int i=0;
					foreach(Attr myattr in en.EnMap.Attrs)
					{
						if (myattr.MyFieldType==FieldType.RefText)
							this[this.CurrentRowIndex,i]=en.GetValRefTextByKey(myattr.Key.Replace("Text","") );
						else
							this[this.CurrentRowIndex,i]=en.GetValByKey(myattr.Key);
						i++;
					}
					this.HisEns.AddEntity(en);
				}
				return;
			}

			if (this.PivCell.RowNumber!=this.CurrentCell.RowNumber)
			{
				/* ������� */
				if (this.CurrentRowIndex > this.HisEns.Count-1)
				{
					/* ��������У�*/
					Entity en=this.HisEns.GetNewEntity;
					int i=0;
					foreach(Attr myattr in en.EnMap.Attrs)
					{
						if (myattr.MyFieldType==FieldType.RefText)
							this[this.CurrentRowIndex,i]=en.GetValRefTextByKey(myattr.Key.Replace("Text","") );
						else
							this[this.CurrentRowIndex,i]=en.GetValByKey(myattr.Key);

						i++;
					}
					this.HisEns.AddEntity(en);
					return;
				}
				else
				{
					/* �ǻ��С� */
					try
					{
						Entity en= this.GetEnByRowIndex(this.CurrentCell.RowNumber);
						en.Update();
						//en.Update();
					}
					catch(Exception ex)
					{
						throw ex;
					}

//					int i=0;
//					foreach(Entity en in this.HisEns)
//					{
//						
//						en = this.GetEnByRowIndex(i);
//						i++;
//					}
				}
 
			}

			try
			{
				// ���� ��Ԫ���ֵ�� 
				object obj =this[PivCell.RowNumber,PivCell.ColumnNumber];
				this.GetEnByRowIndex(PivCell.RowNumber).verifyData();
			 
				// ���õ�ǰ��cell
				this.PivCell =this.CurrentCell;

			}
			catch(Exception ex)
			{
				// ���У�鲻��ͨ���� ���������Ϊԭ���Ľ��㡣
				if (this.CurrentCell.RowNumber==this.PivCell.RowNumber
					&&  this.CurrentCell.ColumnNumber==this.PivCell.ColumnNumber)
				{
					this.CurrentCell=this.PivCell; 
					this.PivCell= new DataGridCell(-1,-1);

				}
				else
				{
					PubClass.Alert(ex);
					this.CurrentCell=this.PivCell; 
					this.PivCell= new DataGridCell(-1,-1);
				}
			}
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
                    miDtl.Tag = enDtl.EnsName;
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
			switch(item.Text)
			{
				case "�½�":
					NewEn();
					break;
				case "ɾ����":
					DeleteSelected();
					break;
				case "��Ƭ":
					this.Card();
					break;
				case "DefaultValues":
					this.InvokeDefaultValues();
					break;
				default:
					UIEnsRelation uir =new UIEnsRelation();
					uir.BindEn(this.CurrentRowEn,this.IsDGReadonly) ;
					uir.ShowDialog();					
					return ;
			}
		}

		public Attr CurrentAttr
		{
			get
			{
				return this.HisEn.EnMap.Attrs[this.CurrentCell.ColumnNumber] ;
			}
		}
		public Attr PivAttr
		{
			get
			{
				return this.HisEn.EnMap.Attrs[this.PivCell.ColumnNumber] ;
			}
		}
		public void InvokeDefaultValues()
		{
			BP.Win32.Controls.UIWinDefaultValues ui = new UIWinDefaultValues(this.HisEn, this.CurrentAttr.Key );
		}
		public void Card()
		{
			BP.Win32.Comm.En en = new BP.Win32.Comm.En() ; 
			en.Bind2(this.CurrentRowEn );
			en.Show();

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
			this.Card();
		}	
		

		#endregion

//		/// <summary>
//		/// ������
//		/// </summary>
//		private string CaptionTextOfShortName
//		{
//			get
//			{
//				if (this.DataMember.IndexOf( "." )==-1)
//					return this.DataMember;
//				int i =this.DataMember.LastIndexOf( "." ) ;
//				return this.DataMember.Substring(i+1  );			 
//			}
//		}
		 
		 

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
