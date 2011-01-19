
using System;
 

namespace BP.En.Base
{
	#region �ֵ�����������
	/// <summary>
	/// �ֵ��������
	/// </summary>
	public class GradeDictBaseAttr : DictAttr
	{
		/// <summary>
		/// ����
		/// </summary>
		public const string Grade = "Grade";
		/// <summary>
		/// �Ƿ���ϸ
		/// </summary>
		public const string IsDetail = "IsDetail";
	}
	#endregion

	#region �ּ��ֵ�Ļ���
	/// <summary>
	/// �ּ��ֵ�Ļ���
	/// </summary>
	public abstract class GradeDictBase : Dict
	{
		/// <summary>
		/// ���캯��
		/// </summary>
		protected GradeDictBase()
		{
		}
		/// <summary>
		/// �����������OIDС�ڵ���0���Զ�����һ��OID���������0���ѯ�ö�������������򴴽�һ��OIDΪ0�Ŀն���
		/// </summary>
		/// <param name="oid"></param>
		protected GradeDictBase(int oid) : base(oid){}
		protected GradeDictBase(string _No ) : base(_No){}

		#region ����
		/// <summary>
		/// ���
		/// </summary>
		public override string No
		{
			get
			{
				 return this.GetValStringByKey(GradeDictBaseAttr.No);
				 
			}
			set
			{
				this.SetValByKey(GradeDictBaseAttr.No,value); 
//				setValue(GradeDictBaseAttr.Grade,this.GetGrade());
			}
		}
		/// <summary>
		/// ����
		/// </summary>
		public int Grade
		{
			get
			{
				//return int.Parse(getValue(GradeDictBaseAttr.Grade));
				return 0 ;
			}
			set
			{
				///setValue(GradeDictBaseAttr.Grade,value);
			}
		}
		/// <summary>
		/// �Ƿ���ϸ
		/// </summary>
		public bool IsDetail
		{
			get
			{
				///return bool.Parse(getValue(GradeDictBaseAttr.IsDetail));
				return true;
			}
			set
			{
				//setValue(GradeDictBaseAttr.IsDetail,value);
			}
		}
		private string _parentNo;
		/// <summary>
		/// ͨ�����ȡ����
		/// </summary>
		/// <returns></returns>
		private int GetGrade()
		{
			int leng=this.No.Length,i=0;
			do 
			{
				leng = leng - int.Parse(this.EnMap.CodeStruct.Substring(i,1));
				i++;
			}while(leng!=0);
			return i;
			 
		}
		/// <summary>
		/// �ϼ���No
		/// </summary>
		public string ParentNo
		{
			get 
			{
				int parentPos = this.No.Length - int.Parse(this.EnMap.CodeStruct.Substring(this.Grade - 1,1));
				_parentNo = this.No.Substring(0,parentPos);
				return _parentNo;
				 
			}
		}
		#endregion

		/// <summary>
		/// ȡ�ñ���������ţ�û�ҵ����ؿմ���
		/// </summary>
		/// <returns>�����</returns>
		public string GetGradeMaxNo()
		{
			string maxNo = "";
			if(this.Grade>0)
			{
				QueryObject qo = new QueryObject(this);
//				qo.AddSelect(new SqlField(DictBaseAttr.No,new MaxModifier()));
//				qo.AddWhere(new SqlField(GradeDictBaseAttr.Grade),this.Grade);
//				DictBase dict = (DictBase)CreateInstance();
//				maxNo = DataAccess.Query(qo,"").ToString();
			}
			return maxNo;
		}
		/// <summary>
		/// ͨ�����ȡ������������
		/// </summary>
		/// <returns></returns>
		public GradeDictBase GetParent()
		{

//			GradeDictBase dict = (GradeDictBase)CreateInstance();
//			dict = (GradeDictBase)dict.QueryByNo( this.ParentNo );
			//return dict;
			return null;
			 
		}
		/// <summary>
		/// ͨ�����ȡ���ӽ��
		/// </summary>
		/// <returns></returns>
		public GradeDictBases GetChildren()
		{
//			SubStrModifier subModifier = new SubStrModifier();
//			subModifier.Place = 1;
//			subModifier.Count = this.No.Length;
//			GradeDictBases dicts = (GradeDictBases)NewCollection();
//			QueryObject qo = GetQueryObject();
//			qo.AddWhere(new SqlField(GradeDictBaseAttr.Grade),">",this.Grade);
//			qo.AddWhere(new SqlField(GradeDictBaseAttr.No,subModifier),this.No);
//
//			qo.AddOrder(GradeDictBaseAttr.No,true);
//
//			DataAccess.Query(qo,dicts);
			//return dicts;
			return null;

		}
		
		/// <summary>
		/// ͨ����š�����ȡ���ӽ��
		/// </summary> 
		public GradeDictBases GetChildren(int grade)
		{
//			SubStrModifier subModifier = new SubStrModifier();
//			subModifier.Place = 1;
//			subModifier.Count = this.No.Length;
//			GradeDictBases dicts = (GradeDictBases)NewCollection();
//			QueryObject qo = GetQueryObject();
//			qo.AddWhere(new SqlField(GradeDictBaseAttr.No,subModifier),this.No);
//			qo.AddWhere(new SqlField(GradeDictBaseAttr.Grade),grade);
//
//			qo.AddOrder(GradeDictBaseAttr.No,true);
//
//			DataAccess.Query(qo,dicts);
			//return dicts;
			return null;

		}

		/// <summary>
		/// ͨ���Ƿ���ϸȡ���ӽ��
		/// </summary> 
		public GradeDictBases GetChildren(bool isDtl)
		{
////			SubStrModifier subModifier = new SubStrModifier();
////			subModifier.Place = 1;
////			subModifier.Count = this.No.Length;
////			GradeDictBases dicts = (GradeDictBases)NewCollection();
////			QueryObject qo = GetQueryObject();
////			qo.AddWhere(new SqlField(GradeDictBaseAttr.No,subModifier),this.No);
////			qo.AddWhere(new SqlField(GradeDictBaseAttr.IsDetail),isDtl);
////
////			qo.AddOrder(GradeDictBaseAttr.No,true);
////
////			DataAccess.Query(qo,dicts);
//			return dicts;
			return null;
		}
		/// <summary>
		/// ͨ����š��������Ƿ���ϸȡ���ӽ��
		/// </summary> 
		public GradeDictBases GetChildren(int grade,bool isDtl)
		{
//			SubStrModifier subModifier = new SubStrModifier();
//			subModifier.Place = 1;
//			subModifier.Count = this.No.Length;
//			GradeDictBases dicts = (GradeDictBases)NewCollection();
//			QueryObject qo = GetQueryObject();
//			qo.AddWhere(new SqlField(GradeDictBaseAttr.No,subModifier),this.No);
//			qo.AddWhere(new SqlField(GradeDictBaseAttr.Grade),grade);
//			qo.AddWhere(new SqlField(GradeDictBaseAttr.IsDetail),isDtl);
//
//			qo.AddOrder(GradeDictBaseAttr.No,true);
//
//			DataAccess.Query(qo,dicts);
//			return dicts;
			return null;

		}

		/// <summary>
		/// �޸�ǰ�Ĵ���
		/// </summary>
		protected override bool beforeUpdate()
		{
//			if (!base.beforeUpdate())
//				return false;
//
//			//����һ���¶���
//			GradeDictBase olddict = (GradeDictBase)CreateInstance();
//			//����һ������
//			GradeDictBases dicts = (GradeDictBases)NewCollection();
//			//�ж��Ƿ�����ϸ
//			if( this.IsDetail == false )
//			{   
//				olddict = (GradeDictBase)olddict.QueryByOID( this.OID );
//				if( this.No !=olddict.No )
//				{
//					dicts = olddict.GetChildren();
//					for( int i=0;i<dicts.Count;i++ )
//					{
//						int pos = this.No.Length;
//
//						((GradeDictBase)dicts[i]).No = this.No + ((GradeDictBase)dicts[i]).No.Substring(pos);
//					}
//					dicts.Update();
//				}
//			}
			return true;	
		}	
		
		/// <summary>
		/// ����ǰ�Ĵ���
		/// </summary>
		protected override bool beforeInsert()
		{
//			if (!base.beforeInsert())
//				return false;
//
//			//�ж����ϼ��Ƿ����
//			if( Grade != 1 )
//			{
//				GradeDictBase dict = this.GetParent();
//				if( dict == null )
//				{
//					throw new PanException("@L01000001",this.ParentNo);
//				}
//				dict.IsDetail = false;
//				dict.Update();
//			}
//			this.IsDetail=true;

			return true;
		}
		/// <summary>
		/// ɾ��ǰ�Ĵ���
		/// </summary>
		protected override bool beforeDelete()
		{   
//			if (!base.beforeDelete())
//				return false;
//
//			//����Ƿ���ڴ˶�������������Ƿ�����ϸ
//			if ( this.QueryByOID( this.OID )==null|| this.IsDetail ==false )
//			{    
//				throw new PanException("@L01000002");
//			}
			return true;
		}
		/// <summary>
		/// ɾ������
		/// </summary>
		/// <returns></returns>
		protected override void afterDelete()
		{
			base.afterDelete();

			GradeDictBase dict = this.GetParent();

			if (dict == null)
				return;

			GradeDictBases gdbs = dict.GetChildren();
			if(gdbs == null || gdbs.Count <= 0)
			{
				dict.IsDetail = true;
				dict.Update();
			}
		}		

//		/// <summary>
//		/// ɾ��ǰ�ж��Ƿ��Ѿ���ʹ�� add by bluesky
//		/// </summary>
//		/// <returns></returns>
//		public override bool beforeDelete()
//		{
//			return  beforeDelete();
//		}
	}
	#endregion

	#region �ּ��ֵ伯�ϵĻ���
	/// <summary>
	/// �ּ����ֵ伯�ϻ���
	/// </summary>
	public abstract class GradeDictBases : Dicts
	{
		
		
		#region ��ѯ
		/// <summary>
		/// ��ѯ�����еķ���ϸֵ
		/// </summary>
		/// <returns></returns>
		public void RetrieveNotDetail()
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(GradeDictBaseAttr.IsDetail,false);
			qo.addOrderBy(GradeDictBaseAttr.No );
			qo.DoQuery();
			 
		}
		/// <summary>
		/// ��ѯ�����еķ���ϸֵ��ָ������
		/// </summary>
		/// <returns></returns>
		public void RetrieveNotDetail(int grade)
		{
//			QueryObject qo = GetQueryObject();
//			qo.AddWhere(new SqlField(GradeDictBaseAttr.IsDetail),false);
//			qo.AddWhere(new SqlField(GradeDictBaseAttr.Grade),grade);
//			qo.AddOrder(DictBaseAttr.No,true);
//			retrieveBy(qo);
		}

		 
		/// <summary>
		/// ��ѯ��ָ��������������
		/// </summary>
		/// <param name="no">ָ���ĸ��ڵ���</param>
		public void RetrieveChildrens(string no)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(GradeDictBaseAttr.No, " like ", no+"%" );
			qo.DoQuery();
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
//				qo.AddWhere(new SqlField(GradeDictBaseAttr.No,modifier),gradeNo);
//			}
//			//����������գ���ֻͳ�Ƹü��ļ�¼
//			if (grade > 0)
//			{
//				qo.AddWhere(new SqlField(GradeDictBaseAttr.Grade),grade);
//			}
//			//ֻͳ����ϸ��¼
//			if (detailFlag)
//			{
//				qo.AddWhere(new SqlField(GradeDictBaseAttr.IsDetail),true);
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