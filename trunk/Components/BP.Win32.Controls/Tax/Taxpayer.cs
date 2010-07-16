using System;
using System.Collections;
using System.Data ; 
using BP.DA;
using BP.En.Base;
using BP.En;

namespace BP.Tax
{
	/// <summary>
	/// ��˰������
	/// </summary>
	public class TaxpayerAttr : EntityNoNameAttr
	{	
		#region ����
		/// <summary>
		/// С��
		/// </summary>
		public const string FK_Team="FK_Team";
		/// <summary>
		/// ʶ���
		/// </summary>
		public const string SBH="SBH";
		/// <summary>
		/// ��ַ
		/// </summary>
		public const string Addr="Addr";
		/// <summary>
		/// �绰
		/// </summary>
		public const string Tel="Tel";
		/// <summary>
		/// �ʱ�
		/// </summary>
		public const string PostCard="PostCard";
		/// <summary>
		/// ע���ʽ�
		/// </summary>
		public const string ZCZJ="ZCZJ";
		/// <summary>
		/// ����
		/// </summary>
		public const string ArtificialPerson="ArtificialPerson";
		/// <summary>
		/// ������
		/// </summary>
		public const string FinancePerson="FinancePerson";
		/// <summary>
		/// ������
		/// </summary>
		public const string FK_XZBM="FK_XZBM";
		/// <summary>
		/// ��������
		/// </summary>
		public const string FK_JJXZ="FK_JJXZ";
		/// <summary>
		/// ��ҵ
		/// </summary>
		public const string FK_HY="FK_HY";
		/// <summary>
		/// �Ǽ�״̬
		/// </summary>
		public const string FK_DJZT="FK_DJZT";
		/// <summary>
		/// ��Ӫ��Χ
		/// </summary>
		public const string JJFW="JJFW";
		/// <summary>
		/// ע������
		/// </summary>
		public const string SignDate="SignDate";
		/// <summary>
		/// ���ջ��ر��
		/// </summary>
		public const string FK_ZSJG="FK_ZSJG";
		/// <summary>
		/// �º˶���
		/// </summary>
		public const string CheckTax="CheckTax";
		#endregion 
	}
	/// <summary>
	///  Taxpayer��ժҪ˵����
	/// </summary>
	public class Taxpayer : EntityNoName
	{
		#region ��������
		/// <summary>
		/// FK_Team
		/// </summary>
		public string FK_Team
		{
			get
			{
				return this.GetValStringByKey(TaxpayerAttr.FK_Team);
			}
			set
			{
				this.SetValByKey(TaxpayerAttr.FK_Team,value);
			}
		}		 
		/// <summary>
		/// SBH
		/// </summary>
		public string SBH
		{
			get
			{
				return this.GetValStringByKey(TaxpayerAttr.SBH);
			}
			set
			{
				this.SetValByKey(TaxpayerAttr.SBH,value);
			}
		}
		/// <summary>
		/// ��ַ
		/// </summary>
		public string Addr
		{
			get
			{
				return this.GetValStringByKey(TaxpayerAttr.Addr);
			}
			set
			{
				this.SetValByKey(TaxpayerAttr.Addr,value);
			}
		}
		/// <summary>
		/// ����
		/// </summary>
		public string ArtificialPerson
		{
			get
			{
				return this.GetValStringByKey(TaxpayerAttr.ArtificialPerson);
			}
			set
			{
				this.SetValByKey(TaxpayerAttr.ArtificialPerson,value);
			}
		}
		/// <summary>
		/// ��������
		/// </summary>
		public string FinancePerson
		{
			get
			{
				return this.GetValStringByKey(TaxpayerAttr.FinancePerson);
			}
			set
			{
				this.SetValByKey(TaxpayerAttr.FinancePerson,value);
			}
		}
		/// <summary>
		/// �Ǽ�״̬
		/// </summary>
		public string FK_DJZT
		{
			get
			{
				return this.GetValStringByKey(TaxpayerAttr.FK_DJZT);
			}
			set
			{
				this.SetValByKey(TaxpayerAttr.FK_DJZT,value);
			}
		}
		/// <summary>
		/// ��ҵ
		/// </summary>
		public string FK_HY
		{
			get
			{
				return this.GetValStringByKey(TaxpayerAttr.FK_HY);
			}
			set
			{
				this.SetValByKey(TaxpayerAttr.FK_HY,value);
			}
		}
		/// <summary>
		/// �������ʡ�
		/// </summary>
		public string FK_JJXZ
		{
			get
			{
				return this.GetValStringByKey(TaxpayerAttr.FK_JJXZ);
			}
			set
			{
				this.SetValByKey(TaxpayerAttr.FK_JJXZ,value);
			}
		}
		/// <summary>
		/// �������
		/// </summary>
		public string FK_XZBM
		{
			get
			{
				return this.GetValStringByKey(TaxpayerAttr.FK_XZBM);
			}
			set
			{
				this.SetValByKey(TaxpayerAttr.FK_XZBM,value);
			}
		}
		/// <summary>
		/// ��Ӫ��Χ
		/// </summary>
		public string JJFW
		{
			get
			{
				return this.GetValStringByKey(TaxpayerAttr.JJFW);
			}
			set
			{
				this.SetValByKey(TaxpayerAttr.JJFW,value);
			}
		}
		/// <summary>
		/// �ʱ�
		/// </summary>
		public string PostCard
		{
			get
			{
				return this.GetValStringByKey(TaxpayerAttr.PostCard);
			}
			set
			{
				this.SetValByKey(TaxpayerAttr.PostCard,value);
			}
		}
		/// <summary>
		/// ע������
		/// </summary>
		public string SignDate
		{
			get
			{
				return this.GetValStringByKey(TaxpayerAttr.SignDate);
			}
			set
			{
				this.SetValByKey(TaxpayerAttr.SignDate,value);
			}
		}
		/// <summary>
		/// �绰
		/// </summary>
		public string Tel
		{
			get
			{
				return this.GetValStringByKey(TaxpayerAttr.Tel);
			}
			set
			{
				this.SetValByKey(TaxpayerAttr.Tel,value);
			}
		}
		/// <summary>
		/// ע���ʽ�
		/// </summary>
		public string ZCZJ
		{
			get
			{
				return this.GetValStringByKey(TaxpayerAttr.ZCZJ);
			}
			set
			{
				this.SetValByKey(TaxpayerAttr.ZCZJ,value);
			}
		}
		/// <summary>
		/// �������ջ��ر��
		/// </summary>
		public string FK_ZSJG
		{
			get
			{
				return this.GetValStringByKey(TaxpayerAttr.FK_ZSJG);
			}
			set
			{
				if( value.Length==2)					
					this.SetValByKey(TaxpayerAttr.FK_ZSJG,SystemConfigOfTax.FK_ZSJG+value);
				else
					this.SetValByKey(TaxpayerAttr.FK_ZSJG,value);
			}
		}
		public string FK_ZSJGText
		{
			get
			{
				return this.GetValRefTextByKey(TaxpayerAttr.FK_ZSJG);
			}
		}
		/// <summary>
		/// �º˶���
		/// </summary>
		public float CheckTax
		{
			get
			{
				return this.GetValFloatByKey(TaxpayerAttr.CheckTax);
			}
			set
			{
				this.SetValByKey(TaxpayerAttr.CheckTax,value);
			}
		}		
		#endregion

		#region ��չ����
		 
		 
		#endregion

		#region ���췽��
		/// <summary>
		/// /dfsdf
		/// </summary>
		public Taxpayer(){}		
		/// <summary>
		/// ������˰�˱����ѯ��Ϣ
		/// </summary>
		/// <param name="_No">��˰�˱���</param>
		public Taxpayer(string _No)
		{
			try
			{
				if (_No.Length !=14 )
					throw new Exception("@��˰�˵ı��["+_No+"]����.");
				this.No =_No;
				this.Retrieve();
			}
			catch
			{
				this.Name =_No;
				if (this.RetrieveByName()==1)
					return ;
				if (SystemConfig.SysNo=="FeiCheng")
					throw new Exception();

				string sql="";
				if (SystemConfig.ThirdPartySoftWareKey=="YongYou")
				{
					sql="SELECT SWJG AS DeptNo, SGY AS FK_Team, QYBM AS No,TYBM AS SBH,QYMC AS Name,"+
						" QYDH AS Tel, QYDZ AS Addr,FRXM AS ArtificialPerson,CWFZ AS FinancePerson,SWFZRQ AS SignDate,"+
						" FZCH AS FK_DJZT ,	CZJC AS XZBM, JYFW AS JJFW, JKJJ AS FK_JJXZ,HY AS HY , '' AS PostCard,ZZZJ AS ZCZJ "+
						" FROM DSBM.DJSW "+
						" WHERE QYBM='"+ _No+"'	";
					DataTable dt = DBAccessOfOracle9i.RunSQLReturnTable(sql);
					if(dt.Rows.Count==0)
					{
						throw new Exception("@��˰�˱���["+_No+"]��������!!");
					}
					else if (dt.Rows.Count >=2 )
					{
						throw new Exception("@��˰�˱���["+_No+"]����ϵͳ�в�Ψһ��");
					}
					else
					{
						this.FK_ZSJG=SystemConfigOfTax.FK_ZSJG+dt.Rows[0]["DeptNo"].ToString();
						this.FK_Team=dt.Rows[0]["FK_Team"].ToString();
						this.No=dt.Rows[0]["No"].ToString();
						this.SBH=dt.Rows[0]["SBH"].ToString();
						this.Name=dt.Rows[0]["Name"].ToString();
						this.Tel=dt.Rows[0]["Tel"].ToString();
						this.Addr=dt.Rows[0]["Addr"].ToString();
						this.ArtificialPerson=dt.Rows[0]["ArtificialPerson"].ToString();
						this.FinancePerson=dt.Rows[0]["FinancePerson"].ToString();
						this.SignDate=dt.Rows[0]["SignDate"].ToString();
						this.FK_DJZT=dt.Rows[0]["FK_DJZT"].ToString();
						//  this.XZBM=dt.Rows[0]["XZBM"].ToString(); , , , ... ... ... ... ... 
						this.JJFW=dt.Rows[0]["JJFW"].ToString();
						this.FK_JJXZ=dt.Rows[0]["FK_JJXZ"].ToString();
						//this.HY=dt.Rows[0]["HY"].ToString();
						this.PostCard=dt.Rows[0]["PostCard"].ToString();
						if (dt.Rows[0]["ZCZJ"].ToString()=="")
							this.ZCZJ="0";
						else
							this.ZCZJ=dt.Rows[0]["ZCZJ"].ToString();
						this.Insert();
						this.Retrieve();						 
					}
				}
				else
				{
					throw new Exception("��ʱδ�ṩ�ӿ�!!"); 
				}
			}
		}
		#endregion 

		#region Map
		/// <summary>
		/// Map
		/// </summary>
		public override Map EnMap
		{
			get
			{
				if (this._enMap!=null) return this._enMap;
				Map map = new Map("DS_Taxpayer");
				map.IsAllowRepeatNo=false;
				map.IsAllowRepeatName=true;
				map.EnDesc="��˰��";
				map.IsAdjunct=true;
				map.IsDelete=false;
				map.IsUpdate=false;
				map.IsInsert=false;
				map.IsView=true;
				map.DepositaryOfEntity=Depositary.None;
				map.DepositaryOfMap=Depositary.Application;

				map.EnType=EnType.ThirdPart;
				
				//map.PhysicsTable;
				map.AddTBStringPK(TaxpayerAttr.No,null,"��˰�˱��",true,true,0,20,14);
				map.AddTBString(TaxpayerAttr.SBH,null,"��˰��ʶ���",true,true,0,20,18);
				map.AddTBString(TaxpayerAttr.ArtificialPerson,null,"����",true,true,0,20,14);
				map.AddTBString(TaxpayerAttr.Addr,null,"��˰�˵�ַ",true,true,0,50,14);
				map.AddTBString(TaxpayerAttr.FinancePerson,null,"������",true,true,0,20,14);
				map.AddDDLEntitiesNoName(TaxpayerAttr.FK_DJZT,null,"�Ǽ�״̬",new DJZTs(),false);
				map.AddDDLEntitiesNoName(TaxpayerAttr.FK_HY,null,"��ҵ",new HYs(),false);
				map.AddDDLEntitiesNoName(TaxpayerAttr.FK_JJXZ,null,"��������",new JJXZs(),false);
				map.AddTBString(TaxpayerAttr.JJFW,null,"��Ӫ��Χ",true,false,0,20,14);
				map.AddTBString(TaxpayerAttr.Name,null,"��˰������",true,false,0,20,14);
				map.AddTBString(TaxpayerAttr.SignDate,null,"ע������",true,true,0,20,14);
				map.AddTBString(TaxpayerAttr.Tel,null,"��˰�˵绰",true,false,0,20,14);
				map.AddTBString(TaxpayerAttr.ZCZJ,null,"ע���ʽ�",true,false,0,20,14);
				map.AddTBString(TaxpayerAttr.PostCard,null,"��������",true,false,0,20,14);

				map.AddDDLEntitiesNoName(TaxpayerAttr.FK_ZSJG,null,"���ջ���",new ZSJGs(),false);
				map.AddTBMoney(TaxpayerAttr.CheckTax,0,"�º˶���",true,true);

				//map.AttrsOfSearch.Add("ע���ʽ����","ZCZJ",">",1000);

				map.AddSearchAttr(TaxpayerAttr.FK_DJZT);
				map.AddSearchAttr(TaxpayerAttr.FK_HY);
				map.AddSearchAttr(TaxpayerAttr.FK_JJXZ);
				map.AddSearchAttr(TaxpayerAttr.FK_XZBM);
				map.AddSearchAttr(TaxpayerAttr.FK_Team);

				this._enMap=map;
				return this._enMap; 
			}
		}
		#endregion 				

		#region  ��д����ķ�����
//		protected override bool beforeInsert()
//		{
//			base.beforeInsert();
//			return true;
//		}
//		protected override bool beforeUpdate()
//		{
//			base.beforeUpdate();
//			return true;
//		}
//		protected override bool beforeDelete()
//		{
//			base.beforeDelete();
//			return true;
//		}
//
//		protected override void afterDelete()
//		{
//			base.afterDelete();
//			return ;
//
//		}
//		protected override  void afterInsert()
//		{
//			base.afterInsert();
//			return ;
//		}
//		protected override void afterUpdate()
//		{
//			base.afterUpdate();
//			return ;
//		}
		#endregion

		#region ��̬����
		/// <summary>
		/// GenerNSRString
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public static string GenerNSRString(string key)
		{
			if (key.Length==0)
				throw new Exception("@�ؼ��ֲ���Ϊ��");
			DataTable dt = DBAccess.RunSQLReturnTable( "select No from ds_taxpayer where Name like '%"+key+"%'" ) ;
			string str = "";
			foreach ( DataRow dr in dt.Rows )
			{
				str= str + " '"+dr[0].ToString()+"',";
			}
			if (str!="")
				return str.Substring(0,str.Length-1);
			if (str.Trim().Length==0)
				str="'222222222222222'";
			return str;
		}
		/// <summary>
		/// ����һЩ����������˰�˵�string ��, �ָ�.
		/// </summary>
		/// <param name="from">ȥ����˰���</param>
		/// <param name="to">ȥ����˰�</param>
		/// <param name="ZSJG">���ջ���(all)</param>
		/// <param name="Level">����</param>
		/// <param name="TaxpayerType">��˰������(all)</param>
		/// <param name="HY">��ҵ(all)</param>
		/// <returns></returns>
		public static string GenerNSRString(int from, int to, string ZSJG, string Level, string TaxpayerType, string HY )
		{
			DataTable dt = DBAccess.RunSQLReturnTable( GenerNSRSQL(from,to,ZSJG,Level,TaxpayerType, HY) ) ;
			string str = "";
			foreach ( DataRow dr in dt.Rows )
			{
				str= str + " '"+dr[0].ToString()+"',";
			}
			if (str!="")
				return str.Substring(0,str.Length-1);
			if (str.Trim().Length==0)
				str="'222222222222222'";
			return str;
		}		 
		/// <summary>
		/// ������˰��sql 
		/// </summary>
		/// <param name="from">ȥ����˰���</param>
		/// <param name="to">�� </param>
		/// <param name="ZSJG">���ջ���</param>
		/// <param name="Level">����</param>
		/// <param name="TaxpayerType">����</param>
		/// <param name="HY">��ҵ</param>
		/// <returns>bulider sql</returns>
		public static string GenerNSRSQL(int from, int to, string ZSJG, string Level, string TaxpayerType, string HY )
		{
			//�������ȫ����������
			// from to  ����
			string where1= " 1=1 ";
		    // string having = " ";
			//having = " having sum( NSE) > "+ from.ToString() +" and sum(NSE)  <  " + to.ToString(); 
			//���ջ���
			string where2 = " " ; 
			if ( ZSJG.Equals("all")==false)
			{				
				where2 = " and ( aaa.DeptNo = '"+ ZSJG +"' ) ";
			}
			//����
			string where3 = " " ;
			if ( Level.Equals("all")==false)
			{
			}
			//��ҵ����
			string where4 = " " ; 
			if (TaxpayerType.Equals("all")==false)
			{
				where3 = " and ( aaa.FK_JJXZ ='"+ TaxpayerType+"' ) ";
			}
			//��ҵ
			string where5 = " ";
			if ( HY.Equals("all")==false)
			{
				where4 = " and ( aaa.FK_HY like '"+ HY +"%' ) ";
			}
			string WhereSQL=where1+ where2+where3+where4+where5+" AND ( No in (  SELECT TaxpayerNo FROM DS_TaxpayerYearSE2003 WHERE BenNianYiJiao > "+from + " AND BenNianYiJiao <  "+to +" ) ) ";
			string mysql=" SELECT aaa.No as TaxpayerNo FROM  DS_Taxpayer aaa WHERE " + WhereSQL; //;+ "  group by   aaa.c_qydm " + having;
			return mysql;
		}
		#endregion
	}
	/// <summary>
	///  ��˰��s
	/// </summary>
	public class Taxpayers : EntitiesNoName
	{

		#region Init
//		public static string InitTaxpayer()
//		{
//			 string sql="";
//		}
		#endregion

		#region ��˰��
		/// <summary>
		/// ��˰��s
		/// </summary>
		public Taxpayers()
		{
		}
		/// <summary>
		/// �õ����� Entity
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new Taxpayer();
			}
		}
		#endregion
		
	}
}
