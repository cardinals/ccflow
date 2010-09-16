using System;
using System.Collections;
using System.Data;
using BP.DA;
using BP.En;
using BP.Port;


namespace BP.GTS
{
	/// <summary>
	/// ��ҵattr
	/// </summary>
	public class WorkRDAttr:EntityNoNameAttr
	{
		/// <summary>
		/// ��
		/// </summary>
		public const string ValidTimeTo="ValidTimeTo";
		/// <summary>
		/// ����ʱ�����
		/// </summary>
		public const string MM="MM";

		#region  ��Ŀ����
		/// <summary>
		/// ����ѡ����
		/// </summary>
		public const string NumOfChoseOne="NumOfChoseOne";
		/// <summary>
		/// ����ѡ����
		/// </summary>
		public const string NumOfChoseM="NumOfChoseM";
		/// <summary>
		/// �����
		/// </summary>
		public const string NumOfFillBlank="NumOfFillBlank";
		/// <summary>
		/// �ж���
		/// </summary>
		public const string NumOfJudgeTheme="NumOfJudgeTheme";
		/// <summary>
		/// �ʴ���
		/// </summary>
		public const string NumOfEssayQuestion="NumOfEssayQuestion";
		/// <summary>
		/// NumOfRC
		/// </summary>
		public const string NumOfRC="NumOfRC";
		#endregion

		#region cent
		/// <summary>
		/// ����ѡ����
		/// </summary>
		public const string CentOfPerChoseOne="CentOfPerChoseOne";
		/// <summary>
		/// ����ѡ����
		/// </summary>
		public const string CentOfPerChoseM="CentOfPerChoseM";
		/// <summary>
		/// �����
		/// </summary>
		public const string CentOfPerFillBlank="CentOfPerFillBlank";
		/// <summary>
		/// �ж���
		/// </summary>
		public const string CentOfPerJudgeTheme="CentOfPerJudgeTheme";
		/// <summary>
		/// �ʴ���
		/// </summary>
		public const string CentOfPerEssayQuestion="CentOfPerEssayQuestion";
		/// <summary>
		/// CentOfPerRC
		/// </summary>
		public const string CentOfPerRC="CentOfPerRC";
		/// <summary>
		/// �ϼ�
		/// </summary>
		public const string CentOfSum="CentOfSum";
		#endregion
	}
	 
	/// <summary>
	/// ��ҵ
	/// </summary>
	public class WorkRD :EntityNoName
	{

		#region his attrs
		/// <summary>
		/// ���Լ���
		/// </summary>
		public WorkRandoms HisWorkRandoms
		{
			get
			{
				WorkRandoms chs = new WorkRandoms(this.No);
				return chs;
			}
		}
		
		#endregion

		#region attrs
		public bool IsValid
		{
			get
			{
				DateTime dtto=DataType.ParseSysDateTime2DateTime(this.ValidTimeTo+" 10:10");
				DateTime dt=DateTime.Now;
				if ( dtto >= dt )
					return true;
				else
					return false;
			}
		}
		/// <summary>
		/// ��ѡ
		/// </summary>
		public int CentOfPerChoseOne
		{
			get
			{
				return this.GetValIntByKey(WorkRDAttr.CentOfPerChoseOne);
			}
			set
			{
				this.SetValByKey(WorkRDAttr.CentOfPerChoseOne,value);
			}
		}
		/// <summary>
		/// ��ѡ
		/// </summary>
		public int CentOfPerChoseM
		{
			get
			{
				return this.GetValIntByKey(WorkRDAttr.CentOfPerChoseM);
			}
			set
			{
				this.SetValByKey(WorkRDAttr.CentOfPerChoseM,value);
			}
		}
		/// <summary>
		/// �ж�
		/// </summary>
		public int CentOfPerJudgeTheme
		{
			get
			{
				return this.GetValIntByKey(WorkRDAttr.CentOfPerJudgeTheme);
			}
			set
			{
				this.SetValByKey(WorkRDAttr.CentOfPerJudgeTheme,value);
			}
		}
		/// <summary>
		/// �ʴ�
		/// </summary>
		public int CentOfPerEssayQuestion
		{
			get
			{
				return this.GetValIntByKey(WorkRDAttr.CentOfPerEssayQuestion);
			}
			set
			{
				this.SetValByKey(WorkRDAttr.CentOfPerEssayQuestion,value);
			}
		}
		/// <summary>
		/// �Ķ�
		/// </summary>
		public int CentOfPerRC
		{
			get
			{
				return this.GetValIntByKey(WorkRDAttr.CentOfPerRC);
			}
			set
			{
				this.SetValByKey(WorkRDAttr.CentOfPerRC,value);
			}
		}
		/// <summary>
		/// ���
		/// </summary>
		public int CentOfPerFillBlank
		{
			get
			{
				return this.GetValIntByKey(WorkRDAttr.CentOfPerFillBlank);
			}
			set
			{
				this.SetValByKey(WorkRDAttr.CentOfPerFillBlank,value);
			}
		}
		/// <summary>
		/// �ϼ�
		/// </summary>
		public int CentOfSum
		{
			get
			{
				return this.GetValIntByKey(WorkRDAttr.CentOfSum);
			}
			set
			{
				this.SetValByKey(WorkRDAttr.CentOfSum,value);
			}
		}
		/// <summary>
		/// ʱ��
		/// </summary>
		public int MM
		{
			get
			{
				return this.GetValIntByKey(WorkRDAttr.MM);
			}
			set
			{
				this.SetValByKey(WorkRDAttr.MM,value);
			}
		}
		#endregion

		#region Num of 
		/// <summary>
		/// ��ѡ
		/// </summary>
		public int NumOfChoseOne
		{
			get
			{
				return this.GetValIntByKey(WorkRDAttr.NumOfChoseOne);
			}
			set
			{
				this.SetValByKey(WorkRDAttr.NumOfChoseOne,value);
			}
		}
		/// <summary>
		/// ��ѡ
		/// </summary>
		public int NumOfChoseM
		{
			get
			{
				return this.GetValIntByKey(WorkRDAttr.NumOfChoseM);
			}
			set
			{
				this.SetValByKey(WorkRDAttr.NumOfChoseM,value);
			}
		}
		/// <summary>
		/// �ж�
		/// </summary>
		public int NumOfJudgeTheme
		{
			get
			{
				return this.GetValIntByKey(WorkRDAttr.NumOfJudgeTheme);
			}
			set
			{
				this.SetValByKey(WorkRDAttr.NumOfJudgeTheme,value);
			}
		}
		/// <summary>
		/// �ʴ�
		/// </summary>
		public int NumOfEssayQuestion
		{
			get
			{
				return this.GetValIntByKey(WorkRDAttr.NumOfEssayQuestion);
			}
			set
			{
				this.SetValByKey(WorkRDAttr.NumOfEssayQuestion,value);
			}
		}
		/// <summary>
		/// �Ķ�
		/// </summary>
		public int NumOfRC
		{
			get
			{
				return this.GetValIntByKey(WorkRDAttr.NumOfRC);
			}
			set
			{
				this.SetValByKey(WorkRDAttr.NumOfRC,value);
			}
		}
		/// <summary>
		/// ���
		/// </summary>
		public int NumOfFillBlank
		{
			get
			{
				return this.GetValIntByKey(WorkRDAttr.NumOfFillBlank);
			}
			set
			{
				this.SetValByKey(WorkRDAttr.NumOfFillBlank,value);
			}
		}
		#endregion

		#region attrs ext
		public string ValidTimeTo
		{
			get
			{
				return this.GetValStringByKey(WorkRDAttr.ValidTimeTo);
			}
			set
			{
				this.SetValByKey(WorkRDAttr.ValidTimeTo,value);
			}
		}
		#endregion
	 
		#region ʵ�ֻ����ķ���
		/// <summary>
		/// uac
		/// </summary>
		public override UAC HisUAC
		{
			get
			{
				UAC uc = new UAC();
				uc.OpenForSysAdmin();
				return uc;
			}
		}
		/// <summary>
		/// ��д���෽��
		/// </summary>
		public override Map EnMap
		{
			get
			{
				if (this._enMap!=null) 
					return this._enMap;
				
				Map map = new Map("GTS_WorkRD");
				map.EnDesc="�����ҵ���";
				map.CodeStruct="5";
				map.EnType= EnType.Admin;

				map.AddTBStringPK(WorkRDAttr.No,null,"���",true,true,0,50,20);
				map.AddTBString(WorkRDAttr.Name,"�½������ҵ1","����",true,false,0,50,20);

				map.AddTBInt(WorkRDAttr.NumOfChoseOne,30,"��ѡ�����",true,false);
				map.AddTBInt(WorkRDAttr.CentOfPerChoseOne,1,"ÿ��ѡ���",true,false);

				map.AddTBInt(WorkRDAttr.NumOfChoseM,10,"��ѡ�����",true,false);
				map.AddTBInt(WorkRDAttr.CentOfPerChoseM,2,"ÿ��ѡ���",true,false);

				map.AddTBInt(WorkRDAttr.NumOfFillBlank,10,"��������",true,false);
				map.AddTBInt(WorkRDAttr.CentOfPerFillBlank,1,"ÿ������",true,false);

				map.AddTBInt(WorkRDAttr.NumOfJudgeTheme,10,"�ж������",true,false);
				map.AddTBInt(WorkRDAttr.CentOfPerJudgeTheme,1,"ÿ�ж����",true,false);

				map.AddTBInt(WorkRDAttr.NumOfEssayQuestion,5,"�ʴ������",true,false);
				map.AddTBInt(WorkRDAttr.CentOfPerEssayQuestion,4,"ÿ�ʴ����",true,false);

				map.AddTBInt(WorkRDAttr.NumOfRC,1,"�Ķ������",true,false);
				map.AddTBInt(WorkRDAttr.CentOfPerRC,10,"�Ķ����ܷ�",true,false);

				map.AddTBInt(WorkRDAttr.CentOfSum,100,"�ܷ�",true,true);

				DateTime dt = DateTime.Now;
				dt=dt.AddDays(7);
				map.AddTBDateTime(WorkRDAttr.ValidTimeTo,dt.ToString("yyyy-MM-dd")+" 10:00","����ҵʱ��",true,false);


				map.AttrsOfOneVSM.Add( new WorkRandomVSEmps(), new Emps(),WorkRandomVSEmpAttr.FK_Work,WorkRandomVSEmpAttr.FK_Emp, RCAttr.Name,RCAttr.No,"�����õ�ѧ��");
				this._enMap=map;
				return this._enMap;
			}
		}
		protected override bool beforeUpdateInsertAction()
		{
			// �ж��Ǵ˿����Ѿ��ɿ�ʼ�Ŀ�����
			string sql="SELECT COUNT(*) FROM GTS_WorkVSChoseOne WHERE FK_Work IN ( SELECT No FROM GTS_WorkRandom WHERE FK_WorkRD='"+this.No+"')" ;

			int i=DBAccess.RunSQLReturnValInt(sql);
			if (i>=1)
				throw new Exception("��ҵ["+this.Name+"], ���ܱ�������ƣ���Ϊ���Ѿ���һ�����ѧ����ʼ���⡣");

			if (this.CentOfPerChoseOne==0 && this.NumOfChoseOne > 0 )
				throw new Exception("ÿ ��ѡ�� ��������Ϊ0��");

			if (this.CentOfPerChoseM==0 && this.NumOfChoseM > 0 )
				throw new Exception("ÿ ��ѡ�� ��������Ϊ0��");

			if (this.CentOfPerFillBlank==0 && this.NumOfFillBlank > 0 )
				throw new Exception("ÿ ����� ��������Ϊ0��");

			if (this.CentOfPerJudgeTheme==0 && this.NumOfJudgeTheme > 0 )
				throw new Exception("ÿ �ж��� ��������Ϊ0��");

			//			if (this.CentOfPerEssayQuestion==0 && this.PerCentOfPerEssayQuestion > 0 )
			//				throw new Exception("ÿ �ʴ��� ��������Ϊ0��");

			if (this.CentOfPerRC==0 && this.NumOfRC > 0 )
				throw new Exception("ÿ�Ķ����������Ϊ0��");

			if (this.NumOfRC==0)
				this.CentOfPerRC=0;
			 
			if (this.NumOfEssayQuestion==0)
				this.CentOfPerEssayQuestion=0;


			int centsum=0;
			centsum+=this.CentOfPerChoseOne*this.NumOfChoseOne;
			centsum+=this.CentOfPerChoseM*this.NumOfChoseM;
			centsum+=this.CentOfPerFillBlank*this.NumOfFillBlank;
			centsum+=this.CentOfPerJudgeTheme*this.NumOfJudgeTheme;
			centsum+=this.CentOfPerEssayQuestion*this.NumOfEssayQuestion;
			centsum+=this.CentOfPerRC;

			sql="SELECT COUNT(*) FROM V_GTS_ChoseOne";
			if ( DBAccess.RunSQLReturnValInt(sql) <this.NumOfChoseOne)
				throw new Exception("��ѡ�� ��Ŀ̫��");

			sql="SELECT COUNT(*) FROM V_GTS_ChoseM";
			if ( DBAccess.RunSQLReturnValInt(sql) <this.NumOfChoseM)
				throw new Exception("��ѡ�� ��Ŀ̫��");

			sql="SELECT COUNT(*) FROM GTS_EssayQuestion";
			if ( DBAccess.RunSQLReturnValInt(sql) <this.NumOfEssayQuestion)
				throw new Exception("����� ��Ŀ̫��");


			sql="SELECT COUNT(*) FROM GTS_FillBlank";
			if ( DBAccess.RunSQLReturnValInt(sql) <this.NumOfFillBlank)
				throw new Exception("����� ��Ŀ̫��");

			sql="SELECT COUNT(*) FROM GTS_RC";
			if ( DBAccess.RunSQLReturnValInt(sql) <this.NumOfRC)
				throw new Exception("�Ķ���� "+NumOfRC+" ��Ŀ̫��");

			sql="SELECT COUNT(*) FROM GTS_JudgeTheme";
			if ( DBAccess.RunSQLReturnValInt(sql) <this.NumOfJudgeTheme)
				throw new Exception("�ж��� "+NumOfJudgeTheme+"��Ŀ̫��");

			if (this.CentOfPerRC ==0 )
				this.NumOfRC=0;

			if (this.NumOfRC==0)
				this.CentOfPerRC=0;

			/* 1, ��ʼ������, �ڿ���ʱ�����ÿ�����Χ, ��ÿ��������ʼ��������ҵ��ʽ�����ǲ�����������Ŀ��
			 * �ȴ��û����뿼�Ժ��ٸ�����Ŀ��
			 * 2, ���ӿ�������
			 * */
//			sql="DELETE GTS_WorkRandom WHERE FK_Emp NOT IN (SELECT FK_Emp FROM GTS_WorkRandomVSEmp WHERE FK_Work='"+this.No+"') AND FK_WorkRandom='"+this.No+"'";
//			DBAccess.RunSQL(sql); // ɾ��û�С�
//
//			sql="SELECT FK_Emp FROM GTS_WorkRandomVSEmp WHERE FK_WorkRandom='"+this.No+"' AND FK_Emp ";
//			sql+="NOT IN (SELECT FK_Emp FROM GTS_WorkRandom WHERE FK_WorkRD='"+this.No+"')";

			sql="SELECT FK_Emp FROM GTS_WorkRandomVSEmp WHERE FK_Work='"+this.No+"'";
			DataTable dt = DBAccess.RunSQLReturnTable(sql);

			// ɾ������
			DBAccess.RunSQL("DELETE GTS_WorkRandom WHERE FK_WorkRD='"+this.No+"' ");
			foreach(DataRow dr in dt.Rows)
			{
				WorkRandom pe = new WorkRandom();
				pe.FK_Emp=dr[0].ToString();
				pe.FK_WorkRD=this.No;
				pe.Insert(); // ���ӿ�������
			}
			// �ܷ�
			this.CentOfSum=this.CentOfPerChoseOne*this.NumOfChoseOne+this.CentOfPerChoseM*this.NumOfChoseM+this.CentOfPerEssayQuestion*this.NumOfEssayQuestion+this.CentOfPerFillBlank*this.NumOfFillBlank+this.CentOfPerJudgeTheme*this.NumOfJudgeTheme+this.CentOfPerRC;
			return base.beforeUpdateInsertAction();
		}
		#endregion 

		#region ���췽��
		/// <summary>
		/// �Ծ�
		/// </summary> 
		public WorkRD()
		{
		}
		/// <summary>
		/// �Ծ�
		/// </summary>
		/// <param name="_No">���ջ��ر��</param> 
		public WorkRD(string _No ):base(_No)
		{
		}
		#endregion 

		

	}
	/// <summary>
	///  ��ҵ
	/// </summary>
	public class WorkRDs :EntitiesNoName
	{
		/// <summary>
		/// WorkRDs
		/// </summary>
		public WorkRDs(){}
		/// <summary>
		/// WorkRD
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new WorkRD();
			}
		}
		 
		/// <summary>
		/// 
		/// </summary>
		/// <param name="fk_emp"></param>
		/// <returns></returns>
		public int RetrieveWorkRD(string fk_emp)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhereInSQL(PaperFixAttr.No,  "SELECT FK_Work FROM GTS_WorkRandomVSEmp WHERE FK_Emp='"+fk_emp+"'");
			return qo.DoQuery();
		}

		 
	}
}
