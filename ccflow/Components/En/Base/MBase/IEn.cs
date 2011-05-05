using System;
using BP.En;
using System.Collections;
using BP.DA;
using System.Data;
using BP.Sys;
using BP;
 

namespace BP.En
{
	/// <summary>
	/// ������ʵ������
	/// </summary>
	public class IEnAttr
	{
		/// <summary>
		/// ���
		/// </summary>
		public const string No="No";
		/// <summary>
		/// OID
		/// </summary>
		public const string OID="OID";
		/// <summary>
		/// ����
		/// </summary>
		public const string FK_Language="FK_Language";
		

	}
	/// <summary>
	/// �����Ե�ʵ��
	/// </summary>
	[Serializable]
	abstract public class IEn : Entity
	{
		#region ��������
		/// <summary>
		/// OID
		/// </summary>
		public int OID
		{
			get
			{
				return this.GetValIntByKey(IEnAttr.OID) ; 
			}
			set
			{
				this.SetValByKey(IEnAttr.OID,value);				 
			}
		}
		/// <summary>
		/// ���
		/// </summary>
		public string  No
		{
			get
			{
				return this.GetValStringByKey(IEnAttr.No) ; 
			}
			set
			{
				this.SetValByKey(IEnAttr.No,value);
			}
		}
		/// <summary>
		/// ����
		/// </summary>
		public string  FK_Language
		{
			get
			{
				return this.GetValStringByKey(IEnAttr.FK_Language) ; 
			}
			set
			{
				this.SetValByKey(IEnAttr.FK_Language,value);
			}
		}
		
		#endregion

		#region ����
		/// <summary>
		/// ʵ��
		/// </summary>
		public IEn(){}
		/// <summary>
		/// ʵ��
		/// </summary>
		/// <param name="OID"></param>
		public IEn(int OID)
		{
			this.OID=OID;
			this.Retrieve();
		}
		/// <summary>
		/// ʵ��
		/// </summary>
		/// <param name="no"></param>
		/// <param name="fk_language"></param>
		public IEn(string no, string fk_language)
		{
			this.No=no;
			this.FK_Language = fk_language;
		}
		#endregion

		#region ���� Map

		#region ��Ҫ������д�ķ���
		/// <summary>
		/// EntityNo
		/// </summary>
		protected EntityNo _enOfA=null;
		/// <summary>
		/// EntityOIDNo
		/// </summary>
		protected EntityOIDNo _enOfM=null;
		/// <summary>
		/// ʵ��
		/// </summary>
		public EntityNo  EnOfA
		{
			get
			{
				if ( _enOfA==null)
					this._enOfA=(EntityNo)this.GetNewEnsOfA.GetNewEntity;
				return this._enOfA;
			}
		}
		/// <summary>
		/// HisEnOIDNo
		/// </summary>
		public EntityOIDNo EnOfM
		{
			get
			{
				if ( _enOfM==null)
					this._enOfM=(EntityOIDNo)this.GetNewEnsOfM.GetNewEntity;
				return this._enOfM;
			}
		}



		/// <summary>
		/// ������Ҫʵ��
		/// </summary>
		public abstract EntitiesNo GetNewEnsOfA{get;}
		/// <summary>
		/// ������Ҫʵ��
		/// </summary>
		public abstract EntitiesOIDNo GetNewEnsOfM{get;}
		/// <summary>
		/// ʵ��A
		/// </summary>
		public Map MapOfA
		{
			get
			{
				return this.EnOfA.EnMap;
			}
		}
		/// <summary>
		/// ʵ��M
		/// </summary>
		public Map MapOfM
		{
			get
			{
				return this.EnOfM.EnMap;
			}
		}
		/// <summary>
		/// ������Ҫ������ map ������һ��
		/// </summary>
		public override Map EnMap
		{
			get
			{
				if (this._enMap==null)
				{
					Map map= new Map(this.MapOfA.EnDBUrl.DBUrlType, this.PhysicsView);
					map.Attrs = this.MapOfA.Attrs;
					foreach(Attr attr in this.MapOfM.Attrs)					
						map.Attrs.Add(attr);
                    this._enMap = map;
				}
				return this._enMap;
			}
		}
		/// <summary>
		/// ������ͼ
		/// </summary>
		public string PhysicsView
		{
			get
			{
				string viewName="V_"+this.MapOfA.PhysicsTable+"_"+this.MapOfM.PhysicsTable;		
				if (BP.SystemConfig.IsDebug==false)
					return viewName ;
		 
				string sql="SELECT TABLE_NAME FROM INFORMATION_SCHEMA.VIEWS WHERE  TABLE_NAME = '"+viewName+"'";			
				if (DBAccess.IsExits(sql)==false)
				{
					sql ="CREATE VIEW "+viewName+" AS SELECT  a.*, m.* FROM "+this.MapOfA.PhysicsTable+" a , "+this.MapOfM.PhysicsTable+" m WHERE a.No=m."+this.MapOfM.GetFieldByKey("No");
					DBAccess.RunSQL(sql);
				}
				return viewName;
			}
		}
		/// <summary>
		/// ������ͼ
		/// </summary>
		private void CreateView()
		{
			//CreateViewOfMSSQL();
		}
		/// <summary>
		/// ���� ms view 
		/// </summary>
		private void CreateViewOfMSSQL_del()
		{
			if (BP.SystemConfig.IsDebug==false)
				return ;

			string sql="SELECT TABLE_NAME FROM INFORMATION_SCHEMA.VIEWS WHERE  TABLE_NAME = '"+this.PhysicsView+"'";			
			if (DBAccess.IsExits(sql)==false)
			{
				sql ="CREATE VIEW "+PhysicsView+" AS SELECT  a.*, m.* FROM "+this.MapOfA.PhysicsTable+" a , "+this.MapOfM.PhysicsTable+" m WHERE a.No=m.No go ";
				DBAccess.RunSQL(sql);
			}
		}
		/// <summary>
		/// ������ͼ
		/// </summary>
		private void CreateViewOfOracle9i()
		{			
		}
		#endregion		 

		#endregion 
		 
		#region ��������
		/// <summary>
		/// ����OID ��ѯ.
		/// </summary>
		/// <returns></returns>
		public int RetrieveByOID()
		{
			return 0;
		}
		/// <summary>
		/// ����OID ��ѯ.
		/// </summary>
		/// <returns></returns>
		public int RetrieveByLanguageNo(string fk_lang, string no)
		{
			this.FK_Language=fk_lang;
			this.No=no;
			QueryObject qo= new QueryObject(this.EnOfM);			
			return  0;
		}
		/// <summary>
		/// ��������ѯ�����ز�ѯ�����ĸ�����
		/// �����ѯ�������Ƕ��ʵ�壬�ǰѵ�һ��ʵ���ֵ��
		/// </summary>
		/// <returns>��ѯ�����ĸ���</returns>
		public new int Retrieve()
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere("OID",this.OID);
			if (qo.DoQuery()==0)
				throw new Exception("û��������¼"+this.OID.ToString() );
		

//		//	if (
//			if (this.OID==0 || (this.FK_Language=="" && this.No="")  )
//				return 
				
			return 0;
			/*
			int i =EnDA.Retrieve(this,SqlBuilder.Retrieve(this));
			if (i ==0)			 			 
				throw new Exception("û��["+this.EnMap.EnDesc+"  "+this.EnMap.PhysicsTable+", ��["+this.ToString()+"], ʵ����PK = "+this.GetValByKey(this.PK)  );
			return i;
			*/
		}
		/// <summary>
		/// ����������ѯ����ѯ�����Ľ����������ǰ��ʵ�塣
		/// </summary>
		/// <returns>��ѯ�����ĸ���</returns>
		public new int RetrieveNotSetValues()
		{
			return 0 ;
			//return  DBAccess.RunSQLReturnTable(SqlBuilder.Retrieve(this)).Rows.Count ;            		 
		}

		#region delete 
		/// <summary>
		/// ���������
		/// </summary>
		/// <returns>true/false</returns>
		private bool CheckDB()
		{
            return true;

            //CheckDatas  ens=new CheckDatas(this.EnMap.PhysicsTable);
            //if (ens.Count==0)
            //    return true;
            //foreach(CheckData en in ens)
            //{
            //    string sql="SELECT "+en.RefTBFK+" FROM  "+en.RefTBName+"   WHERE  "+en.RefTBFK+" ='"+this.GetValByKey(en.MainTBPK) +"' ";	
            //    DataTable dt= DBAccess.RunSQLReturnTable(sql); 
            //    if(dt.Rows.Count==0)
            //        continue;
            //    else				 				
            //        throw new Exception("["+this.EnDesc+"],ɾ���ڼ���ִ���ԭ���ǣ�["+en.RefEnName+"]����֮��["+dt.Rows.Count.ToString()+"]������������ɾ����");
            //}
            //return true;
		}
		protected override bool beforeDelete() 
		{
			this.CheckDB();
			return true;
		}
		public new void Delete()
		{			  
			if (this.beforeDelete() == false)
				return;

			
			 
			//EnDA.Delete(this);
			this.afterDelete();
		}
		protected   override void afterDelete()  
		{
		 
			return;
		}
		#endregion

		#region insert 
		protected override bool beforeInsert()		
		{
			return true;
		}
		 
		public override void Insert()
		{
			if (this.beforeInsert() == false)
				return;
			if (this.beforeUpdateInsertAction() == false)
				return;

			//EnDA.Insert(this);
			this.afterInsert();
		}
		protected override void afterInsert() 
		{
			return;
		}
		/// <summary>
		/// �ڸ��������֮��Ҫ���Ĺ���.
		/// </summary>
		protected override void afterInsertUpdateAction() 
		{
			if (this.EnMap.HisFKEnumAttrs.Count> 0)
				this.Retrieve();
			return ;
		}
		#endregion

		 

	 
		
		 
		#endregion
	 
		 
	}	
	/// <summary>
	/// �����Ե�ʵ�弯��
	/// </summary>
	[Serializable]
	public abstract class IEns : Entities
	{
		#region ���캯��
		public IEns(  string fk_language) 
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(IEnAttr.FK_Language,fk_language); 
			qo.DoQuery();
		}
		/// <summary>
		/// ���캯��
		/// </summary>
		public IEns(){}
		#endregion
	}
}
