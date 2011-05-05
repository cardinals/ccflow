
using System;
using BP.DA;
using System.Data;
using System.Collections ;
using BP.En;
 

namespace BP.En
{
	#region �ֵ�����������
	/// <summary>
	/// �ֵ��������
	/// </summary>
	public class GradeEntityNoNameBaseAttr : EntityNoNameAttr
	{
		/// <summary>
		/// ����
		/// </summary>
		public const string Grade = "Grade";
		/// <summary>
		/// �Ƿ���ϸ
		/// </summary>
		public const string IsDtl = "IsDtl";
	}
	#endregion

	#region �ּ��ֵ�Ļ���
	/// <summary>
	/// �ּ��ֵ�Ļ���
	/// </summary>
	public abstract class GradeEntityNoNameBase : EntityNoName
	{
		#region ���÷���
		/// <summary>
		/// ����ͬ�����
		/// </summary>
		/// <returns></returns>
		public string GenerSameGradeNo()
		{
			string field=this.EnMap.GetAttrByKey("No").Field ;
			int startPos=this.GetNoLengthByGrade(this.Grade-1)+1;
			

			string sql=null;
			switch(SystemConfig.AppCenterDBType)
			{
				case DBType.SQL2000:
					sql="SELECT CONVERT(INT, MAX( SUBSTRING("+field+","+startPos.ToString()+","+this.CodeStueOfThisGrade+"))  )+1 AS No FROM "+this.EnMap.PhysicsTable+" WHERE len( rtrim(ltrim("+field+")) )="+this.GetNoLengthByGrade(this.Grade) +" AND "+field+" like '"+this.No+"%' AND len( rtrim(ltrim("+field+")) )="+this.GetNoLengthByGrade(this.Grade)  ;
					break;
				case DBType.Access:
					sql="SELECT CONVERT(INT, MAX( SUBSTRING("+field+","+startPos.ToString()+","+this.CodeStueOfThisGrade+"))  )+1 AS No FROM "+this.EnMap.PhysicsTable+" WHERE len( rtrim(ltrim("+field+")) )="+this.GetNoLengthByGrade(this.Grade) +" AND "+field+" like '"+this.No+"%' AND len( rtrim(ltrim("+field+")) )="+this.GetNoLengthByGrade(this.Grade)  ;
					break;
				case DBType.Oracle9i:
					sql="SELECT   MAX( SUBSTRING("+field+","+startPos.ToString()+","+this.CodeStueOfThisGrade+"))   +1 AS No FROM "+this.EnMap.PhysicsTable+" WHERE len( rtrim(ltrim("+field+")) )="+this.GetNoLengthByGrade(this.Grade) +" AND "+field+" like '"+this.No+"%' AND len( rtrim(ltrim("+field+")) )="+this.GetNoLengthByGrade(this.Grade)  ;
					break;
				default:
					throw new Exception("error11 ");
			}

			 
		 
			DataTable dt = new DataTable();
			switch (this.EnMap.EnDBUrl.DBUrlType )
			{
				case DBUrlType.AppCenterDSN:
					dt=DBAccess.RunSQLReturnTable(sql);
					break;
				case DBUrlType.DBAccessOfMSSQL2000:
					dt=DBAccessOfMSSQL2000.RunSQLReturnTable(sql);
					break;
				case DBUrlType.DBAccessOfOracle9i:
					dt=DBAccessOfOracle9i.RunSQLReturnTable(sql);
					break;
				default:
					throw new Exception("@sys error ");						
			}
			  
			string str="1";
			if (dt.Rows.Count!=0)
				str=dt.Rows[0][0].ToString();
			if ( str.Length > int.Parse(this.CodeStueOfThisGrade) )
				throw new Exception("@��ǰ�ı����Ҫ�����˱�Ź�����ķ�Χ�������µı��ʧ�ܡ�");
	
			str=this.NoOfParent + str.PadLeft(this.NoOfThisGrade.Length,'0') ;
			return str;
		}
		/// <summary>
		/// �������ӱ��
		/// </summary>
		/// <returns>���ӱ��</returns>
		public string GenerChildGradeNo(string thisNo)
		{
			string field=this.EnMap.GetAttrByKey("No").Field ;
			int startPos=this.GetNoLengthByGrade(this.Grade)+1;
			string sql ="SELECT CONVERT(INT, MAX( SUBSTRING("+field+","+startPos.ToString()+","+this.CodeStueOfChildGrade+"))  )+1 AS No FROM "+this.EnMap.PhysicsTable+" WHERE len( rtrim(ltrim("+field+")) )="+this.GetNoLengthByGrade(this.Grade+1) +" AND "+field+" like '"+this.No+"%' AND len( rtrim(ltrim("+field+")) )="+this.GetNoLengthByGrade(this.Grade+1)  ;
			DataTable dt = DBAccess.RunSQLReturnTable(sql) ; 
			string str="1";
			if (dt.Rows.Count!=0)
				str=dt.Rows[0][0].ToString();

			if ( str.Length > int.Parse(this.CodeStueOfThisGrade) )
				throw new Exception("@���ɵķּ����["+str+"]�����˱�Ź������["+CodeStueOfThisGrade+"]��Χ�������µı��ʧ�ܡ�"+sql);
	
			str= thisNo + str.PadLeft(this.NoOfThisGrade.Length,'0') ;

			return str;
		}
		/// <summary>
		/// ����һ���µ�����ʵ�塣
		/// </summary>
		/// <returns></returns>
		public GradeEntityNoNameBase NewChildEntity()
		{

			// �жϻ��ܲ��������¼���
			if (this.No.Length >=this.NoLengthOfMax)
				throw new Exception("@�˼�������߼����������������¼���");

			int grade=this.Grade;
			string thisNo=this.No;
			string newChildNo= this.GenerChildGradeNo( thisNo);
			this.ResetDefaultVal();
			this.Grade=grade+1 ;
		    this.No =newChildNo; 
			this.IsDtl=true;			
			return this;

		}
		/// <summary>
		/// ͨ��һ������ж����ļ���
		/// </summary>
		/// <param name="No">Ҫ�жϵı��</param>
		/// <returns>����</returns>
		public int GetGradeByNo(string checkNo)
		{
			char[] stru=this.EnMap.CodeStruct.ToCharArray() ; 
			
			int i = 0;
			int len=0;
			foreach(char c in stru)
			{
				i++;
				len=len+int.Parse(c.ToString() ) ; 
				if (len==checkNo.Length)
					return i ;				
			}
			throw new Exception("@�����ж�["+this.EnDesc+"]�У��жϵı��["+checkNo+"]�����Ϸ���");

			
		}
		/// <summary>
		/// ���ռ���ȡ����ŵĳ���
		/// </summary>
		/// <param name="grade">����</param>
		/// <returns>length</returns>
		public int GetNoLengthByGrade(int grade)
		{
			string str = this.EnMap.CodeStruct.Substring(0,grade); 
			char[] stru=str.ToCharArray();		
			 
			int len=0;
			foreach(char c in stru)
			{
				 
				len=len+int.Parse(c.ToString() ) ; 
				 		
			}
			return len;			
		}
		#endregion 

		#region ���캯��
		/// <summary>
		/// ���캯��
		/// </summary>
		protected GradeEntityNoNameBase()
		{
		}
		protected GradeEntityNoNameBase(string _No ) : base(_No){}

		#endregion 

		#region ��������
		/// <summary>
		/// ����ǰ���ļ���
		/// </summary>
		public int GradeMax
		{
			get
			{
				return this.EnMap.CodeStruct.Length;
			}
		}
 		/// <summary>
		/// ����
		/// </summary>
		public int Grade
		{
			get
			{
				//return this.No.Length/2;
			    return 	this.GetValIntByKey(GradeEntityNoNameBaseAttr.Grade) ; 				 
			}
			set
			{
				this.SetValByKey(GradeEntityNoNameBaseAttr.Grade,value) ; 
			}
		}
		/// <summary>
		/// �Ƿ���ϸ
		/// </summary>
		public bool IsDtl
		{
			get
			{
				return 	this.GetValBooleanByKey(GradeEntityNoNameBaseAttr.IsDtl) ; 				 
			}
			set
			{
				this.SetValByKey(GradeEntityNoNameBaseAttr.IsDtl,value) ; 
			}
		}
		
		#endregion

		#region ��������
		/// <summary>
		/// ��ǰ����ı��
		/// </summary>
		public string NoOfThisGrade
		{
			get
			{
				if (this.Grade==1)
					return this.No;

				return this.No.Substring(this.NoLengthOfParent);
			}
		}
		/// <summary>
		/// ��ǰ����ı��롣
		/// </summary>
		public string CodeStueOfThisGrade
		{
			get
			{
				if (this.Grade > this.EnMap.CodeStruct.Length)
					throw new Exception("@["+this.EnDesc+"]����ı���["+this.EnMap.CodeStruct+"]�ṹ̫��.");
				return this.EnMap.CodeStruct.Substring(this.Grade-1,1);
			}
		}
		public string CodeStueOfChildGrade
		{
			get
			{
				if (this.Grade+1 > this.EnMap.CodeStruct.Length)
					throw new Exception("@["+this.EnDesc+"]����ı���["+this.EnMap.CodeStruct+"]�ṹ̫��.");
				return this.EnMap.CodeStruct.Substring(this.Grade,1);
			}
		}
		/// <summary>
		/// ��ǰ������ϻ����롣
		/// </summary>
		public string CodeStueOfParent
		{
			get
			{
				if (this.Grade==1)
					throw new Exception("@��ǰ����󼶱𣬲���ȡ���ϼ����롣"); 
				return this.EnMap.CodeStruct.Substring(this.Grade-1,1);
			}
		}
		/// <summary>
		/// �ϼ���ı��볤�ȡ�
		/// </summary>
		public int NoLengthOfParent
		{
			get
			{
				if (this.Grade==1)
					throw new Exception("@��ǰ����󼶱𣬲���ȡ���ϼ�����ĳ��ȡ�"); 
				return this.No.Length - int.Parse(this.CodeStueOfThisGrade) ; 
			}
		}
		/// <summary>
		/// �ϼ����
		/// </summary>
		public string NoOfParent
		{
			get
			{
				return this.No.Substring(0,NoLengthOfParent);
			}
		}
		public int NoLengthOfMax
		{
			get
			{
				int i = 0 ;
				char[] strs=this.EnMap.CodeStruct.ToCharArray() ; 
				foreach(char s in strs)
				{
					i= i+int.Parse(s.ToString());
				}
				return i ;
			}
		}
		/// <summary>
		/// �ϼ���No
		/// </summary>
		public string ParentNo_del
		{
			get 
			{
				if (this.Grade==1)
					throw new Exception("@�˽ڵ�����߼���");

				int parentPos = this.No.Length - int.Parse(this.EnMap.CodeStruct.Substring(this.Grade - 1,1));
				string _parentNo = this.No.Substring(0,parentPos);
				return _parentNo;				 
			}
		}
		
		#endregion

		/// <summary>
		/// ȡ�����ĸ��ڵ�
		/// </summary>
		public  GradeEntityNoNameBase Parent
		{
			get
			{
				GradeEntityNoNameBase en = (GradeEntityNoNameBase)this.CreateInstance();
				en.No = this.NoOfParent ;
				en.Retrieve();
				return en;
			}
		}
		/// <summary>
		/// ȡ��ȫ���ĺ��ӽڵ�.
		/// </summary>
		/// <returns></returns>
		public GradeEntitiesNoNameBase GetChildren()
		{
			GradeEntitiesNoNameBase ens = (GradeEntitiesNoNameBase)this.GetNewEntities;
			QueryObject qo = new QueryObject(ens) ;
			qo.AddWhere(GradeEntityNoNameBaseAttr.Grade," > ",this.Grade );
			qo.addAnd();
			qo.AddWhere(GradeEntityNoNameBaseAttr.No, "like", this.No +"%"); 
			qo.DoQuery();
			return ens ;
		}
		
		/// <summary>
		/// ͨ����š�����ȡ���ӽ��
		/// </summary> 
		public GradeEntitiesNoNameBase GetChildren(int grade)
		{
			GradeEntitiesNoNameBase ens = (GradeEntitiesNoNameBase)this.GetNewEntities;
			QueryObject qo = new QueryObject(ens) ;
			qo.AddWhere(GradeEntityNoNameBaseAttr.Grade," = ",grade );
			qo.addAnd();
			qo.AddWhere(GradeEntityNoNameBaseAttr.No, "like", this.No+"%"); 
			qo.DoQuery();
			return ens ;
		}
		/// <summary>
		/// ȡ���¼���Ľڵ�.
		/// </summary> 
		public GradeEntitiesNoNameBase GetChildrenOfNextGrade()
		{
			return GetChildren(this.Grade+1);			 
		}
		/// <summary>
		/// ȡ���¼���Ľڵ�
		/// </summary>
		/// <param name="isForDtl">�Ƿ���ϸ</param>
		/// <returns></returns>
		public GradeEntitiesNoNameBase GetChildrenOfNextGrade(bool isForDtl)
		{
			return GetChildren(this.Grade+1,isForDtl);
		}
		/// <summary>
		/// ͨ����š��������Ƿ���ϸȡ���ӽ��
		/// </summary> 
		public GradeEntitiesNoNameBase GetChildren(int grade,bool isDtl)
		{
			GradeEntitiesNoNameBase ens = (GradeEntitiesNoNameBase)this.GetNewEntities;
			QueryObject qo = new QueryObject(ens) ;
			qo.AddWhere(GradeEntityNoNameBaseAttr.Grade," = ",grade );
			qo.addAnd();
			qo.AddWhere(GradeEntityNoNameBaseAttr.No, "like", this.No +"%"); 
			qo.addAnd();
			qo.AddWhere(GradeEntityNoNameBaseAttr.IsDtl, isDtl);
			qo.DoQuery();
			return ens ;
		}
		/// <summary>
		/// �޸�ǰ�Ĵ���
		/// </summary>
        protected override bool beforeUpdate()
        {
            this.Grade = this.No.Length / 2;
            return true;
        }
		/// <summary>
		/// ����ǰ�Ĵ���
		/// </summary>
		protected override bool beforeInsert()
		{
			//return base.beforeInsert();
			if (!base.beforeInsert())
				return false;
			if (this.EnMap.IsCheckNoLength==false)
				return true;
			//�ж����ϼ��Ƿ����
			if( Grade != 1 )
			{
				GradeEntityNoNameBase en =Parent;
				if (en.IsDtl)
				{
					en.IsDtl=false;
					en.Update();
				}
			}
			this.IsDtl=true;
			return true;
		}
		/// <summary>
		/// ɾ��ǰ�Ĵ���
		/// </summary>
		protected override bool beforeDelete()
		{
			//return base.beforeInsert();
			if (!base.beforeDelete())
				return false;
			if (this.GetChildren().Count > 0)
				throw new Exception("@ʵ���Ƿ���ϸ������ɾ����");
			return true;
		}
		/// <summary>
		/// ɾ������, �ж����ĸ���.�������ϸ������.��.
		/// </summary>
		/// <returns></returns>
		protected override void afterDelete()
		{
			base.beforeInsert();
			return ;

			/*
			base.afterDelete();
			GradeEntityNoNameBase  parent = this.Parent;
			if ( parent.GetChildren().Count > 0 )
			{
				parent.IsDtl=true;
				parent.Update();
			}
			*/
		}
	}
	#endregion

	#region �ּ��ֵ伯�ϵĻ���
	/// <summary>
	/// �ּ����ֵ伯�ϻ���
	/// </summary>
	public abstract class GradeEntitiesNoNameBase : EntitiesNoName
	{		 
		#region �ڼ����ڸ��ݼ���ȡ��ʵ�塣
		/// <summary>
		/// 
		/// </summary>
		/// <param name="grade"></param>
		/// <returns></returns>
		public GradeEntitiesNoNameBase GetEntitiesByGrade(int grade)
		{
			return null;	 
		}
		#endregion
		
		/// <summary>
		/// ��ѯ
		/// </summary>
		/// <param name="parntNo">���</param>
		public GradeEntitiesNoNameBase RetrieveByParnt(string parntNo)
		{
			GradeEntityNoNameBase en = (GradeEntityNoNameBase)this.GetNewEntity;
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(GradeEntityNoNameBaseAttr.No, " like ",parntNo+"%");
			qo.addAnd();
			qo.AddWhere(GradeEntityNoNameBaseAttr.Grade, en.GetGradeByNo(parntNo)+1 );
			qo.DoQuery();
			return this; 
		}
		/// <summary>
		/// ��ѯ
		/// </summary>
		/// <param name="parntNo">���</param>
		public GradeEntitiesNoNameBase RetrieveByParnt(string parntNo, bool IsJustFo )
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(GradeEntityNoNameBaseAttr.No, " like ",parntNo+"%");
			qo.addAnd();
			qo.AddWhere(GradeEntityNoNameBaseAttr.No, " like ",parntNo+"%");

			qo.DoQuery();
			return this; 
		}

		#region ��ѯ
		/// <summary>
		/// ��ѯ�����еķ���ϸֵ��ָ������
		/// </summary>
		/// <returns></returns>
		public void QueryGradeEntitiesNoNameBase(int grade)
		{
//			QueryObject qo = GetQueryObject();
//			qo.AddWhere(new SqlField(GradeEntityNoNameBaseAttr.IsDetail),false);
//			qo.AddWhere(new SqlField(GradeEntityNoNameBaseAttr.Grade),grade);
//			qo.AddOrder(DictBaseAttr.No,true);
//			retrieveBy(qo);
		}
		/// <summary>
		/// ��ѯ��ָ��������������
		/// </summary>
		/// <returns></returns>
		public void RetrieveByGrade(int grade)
		{
//			QueryObject qo = GetQueryObject();
//			qo.AddWhere(new SqlField(GradeEntityNoNameBaseAttr.Grade),grade);
//			qo.AddOrder(DictBaseAttr.No,true);
//
//			retrieveBy(qo);
		}

		#endregion
		
		#region Count ͳ��
		/// <summary>
		/// ͳ�Ƹü������м�¼��
		/// </summary>
		/// <param name="grade">����</param>
		/// <returns>��¼��</returns>
		public int CountBy(int grade)
		{	
			return CountBy("",grade,false);
		}
		/// <summary>
		/// ͳ�Ƹñ�ŵ������¼�
		/// </summary>
		/// <param name="gradeNo">���</param>
		/// <returns>��¼��</returns>
		public int CountBy(string gradeNo)
		{	
			return CountBy(gradeNo,0,false);
		}
		/// <summary>
		/// ͳ�Ƹñ���µ�ָ�������ļ�¼��
		/// </summary>
		/// <param name="gradeNo">���</param>
		/// <param name="grade">����</param>
		/// <returns>��¼��</returns>
		public int CountBy(string gradeNo,int grade)
		{	
			return CountBy(gradeNo,grade,false);
		}
		/// <summary>
		/// ͳ�ƶ���ļ�¼��
		/// </summary>
		/// <param name="gradeNo">�ϼ���ţ����Ϊ����ͳ������</param>
		/// <param name="grade">�������������С�ڵ���0��ͳ������</param>
		/// <param name="detailFlag">�Ƿ�ֻͳ����ϸ��¼</param>
		/// <returns>��¼��</returns>
		public int CountBy(string gradeNo,int grade,bool detailFlag)
		{	
//			QueryObject qo = this.GetQueryObject();
//			qo.AddSelect(new SqlString("1",new CountModifier()));
//
//			//����ϼ���Ų��գ���ֻͳ�Ƹñ���µļ�¼
//			if(!Pansoft.ArguIEntCheck.CheckEmptyString(gradeNo))
//			{
//				SubStrModifier modifier = new SubStrModifier();
//				modifier.Count = gradeNo.Length;
//				qo.AddWhere(new SqlField(GradeEntityNoNameBaseAttr.No,modifier),gradeNo);
//			}
//			//����������գ���ֻͳ�Ƹü��ļ�¼
//			if (grade > 0)
//			{
//				qo.AddWhere(new SqlField(GradeEntityNoNameBaseAttr.Grade),grade);
//			}
//			//ֻͳ����ϸ��¼
//			if (detailFlag)
//			{
//				qo.AddWhere(new SqlField(GradeEntityNoNameBaseAttr.IsDetail),true);
//			}
//			object result = DataAccess.Query(qo,0);
//			if(result==DBNull.Value)
//				return 0;
//			else
//				return (int)result;

			return 0 ;
		}
		#endregion
	}
	#endregion
}