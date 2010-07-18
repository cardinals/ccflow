using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.Port;

namespace BP.GTS
{
	/// <summary>
	/// ѧϰ
	/// </summary>
	public class StudyAttr  
	{
		#region ��������
		/// <summary>
		/// ѧ��
		/// </summary>
		public const  string FK_Emp="FK_Emp";
		/// <summary>
		/// ����
		/// </summary>
		public const  string FK_ThemeType="FK_ThemeType";
		/// <summary>
		/// ���
		/// </summary>
		public const  string FK_ThemeSort="FK_ThemeSort";
		/// <summary>
		/// ����
		/// </summary>
		public const  string FK_Theme="FK_Theme";
		/// <summary>
		/// ��Ϥ�̶�
		/// </summary>
		public const  string SXCD="SXCD";
		public const  string Docs="Docs";

		#endregion	
	}
	/// <summary>
	/// ѧϰ ��ժҪ˵��
	/// </summary>
	public class Study :Entity
	{
		#region ��������
		/// <summary>
		///�Ķ���
		/// </summary>
		public string FK_Emp
		{
			get
			{
				return this.GetValStringByKey(StudyAttr.FK_Emp);
			}
			set
			{
				SetValByKey(StudyAttr.FK_Emp,value);
			}
		}
		public string Docs
		{
			get
			{
				return this.GetValStringByKey(StudyAttr.Docs);
			}
			set
			{
				SetValByKey(StudyAttr.Docs,value);
			}
		}
		/// <summary>
		///���
		/// </summary>
		public string FK_ThemeType
		{
			get
			{
				return this.GetValStringByKey(StudyAttr.FK_ThemeType);
			}
			set
			{
				SetValByKey(StudyAttr.FK_ThemeType,value);
			}
		}
		public string FK_ThemeSort
		{
			get
			{
				return this.GetValStringByKey(StudyAttr.FK_ThemeSort);
			}
			set
			{
				SetValByKey(StudyAttr.FK_ThemeSort,value);
			}
		}
		/// <summary>
		/// FK_Theme
		/// </summary>
		public string FK_Theme
		{
			get
			{
				return this.GetValStringByKey(StudyAttr.FK_Theme);
			}
			set
			{
				SetValByKey(StudyAttr.FK_Theme,value);
			}
		}
		/// <summary>
		/// ��Ϥ�̶�
		/// </summary>
		public int SXCD
		{
			get
			{
				return this.GetValIntByKey(StudyAttr.SXCD);
			}
			set
			{
				SetValByKey(StudyAttr.SXCD,value);
			}
		}
		public string SXCDText
		{
			get
			{
				return this.GetValRefTextByKey(StudyAttr.SXCD);
			}
		}
		#endregion

		#region ���캯��
		public Study()
		{

		}
		/// <summary>
		/// �Ķ���������
		/// </summary> 
		public Study(string fk_emp, string fk_themtype, string fk_theme, string themSort)
		{
			this.FK_Emp = fk_emp;
			this.FK_ThemeType=fk_themtype;
			this.FK_Theme=fk_theme;
			this.FK_ThemeSort=themSort;
//			if (themSort==null || themSort=="undefined" )
//			{
//				throw new  Exception("error themSort ");
//			}
			try
			{
				this.Retrieve();
			}
			catch
			{
				this.Insert();
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
				
				Map map = new Map("GTS_Study");
				map.EnDesc="ѧϰ";	
				map.EnType=EnType.Dtl;
				map.AddDDLEntitiesPK(StudyAttr.FK_Emp,Web.WebUser.No,"ѧ��",new Emps(),false);
				map.AddDDLEntitiesPK(StudyAttr.FK_ThemeType,"ChoseOne","����",new ThemeTypes(),false);
				map.AddDDLEntities(StudyAttr.FK_ThemeSort,"01","���",new ThemeSorts(),false);
				map.AddTBStringPK(StudyAttr.FK_Theme,null,"��Ŀ���",true,true,0,90,30);
				map.AddDDLSysEnum(StudyAttr.SXCD,1,"��Ϥ�̶�",true,false);
				map.AddTBStringDoc(StudyAttr.Docs,null,"��ע",true,true);

				this._enMap=map;
				return this._enMap;
			}
		}
		#endregion

		#region ���ػ��෽��

		 

		#endregion 
	
	}
	/// <summary>
	/// ѧϰ 
	/// </summary>
	public class Studys : Entities
	{
		#region ����
		/// <summary>
		/// ѧϰ
		/// </summary>
		public Studys(){}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="fk_themetype"></param>
		/// <param name="fk_emp"></param>
		public Studys(string fk_themetype, string fk_emp, int sxcd)
		{
			if (sxcd==4)
				return;

			QueryObject qo = new QueryObject(this);
			qo.AddWhere(StudyAttr.FK_ThemeType, fk_themetype);
			qo.addAnd();
			qo.AddWhere(StudyAttr.FK_Emp, fk_emp);
			qo.addAnd();
			qo.AddWhere(StudyAttr.SXCD, sxcd);
			qo.DoQuery();

		}

		#endregion

		#region ����
		/// <summary>
		/// �õ����� Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new Study();
			}
		}	
		#endregion 

		#region ��ѯ����
		 
		#endregion
	}
	
}
