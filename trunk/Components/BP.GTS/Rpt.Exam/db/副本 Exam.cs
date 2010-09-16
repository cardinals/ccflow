using System;
using System.Collections;
using BP.DA;
using BP.En.Base;
using BP.En;
using BP.Port;


namespace BP.GTS
{
	/// <summary>
	/// �Ծ��attr
	/// </summary>
	public class ExamAttr:EntityOIDAttr
	{
		/// <summary>
		/// �Ծ�
		/// </summary>
		public const string FK_Paper="FK_Paper";
		/// <summary>
		/// ��
		/// </summary>
		public const string FK_Emp="FK_Emp";
		/// <summary>
		/// ����״̬
		/// </summary>
		public const string ExamState="ExamState";
		/// <summary>
		/// dept
		/// </summary>
		public const string FK_Dept="FK_Dept";
		/// <summary>
		/// from datatime
		/// </summary>
		public const string FromDateTime="FromDateTime";
		/// <summary>
		/// to datetime
		/// </summary>
		public const string ToDateTime="ToDateTime";
		/// <summary>
		/// ����ʱ�����
		/// </summary>
		public const string MM="MM";
		/// <summary>
		/// ����ѡ����
		/// </summary>
		public const string CentOfChoseOne="CentOfChoseOne";
		/// <summary>
		/// ����ѡ����
		/// </summary>
		public const string CentOfChoseM="CentOfChoseM";
		/// <summary>
		/// �����
		/// </summary>
		public const string CentOfFillBlank="CentOfFillBlank";
		/// <summary>
		/// �ж���
		/// </summary>
		public const string CentOfJudgeTheme="CentOfJudgeTheme";
		/// <summary>
		/// �ʴ���
		/// </summary>
		public const string CentOfEssayQuestion="CentOfEssayQuestion";
		/// <summary>
		/// CentOfRC
		/// </summary>
		public const string CentOfRC="CentOfRC";
		/// <summary>
		/// �ϼ�
		/// </summary>
		public const string CentOfSum="CentOfSum";
	}
	 
	/// <summary>
	/// �Ծ��
	/// </summary>
	public class Exam :EntityNo
	{

	 
		#region ʵ�ֻ����ķ���
		/// <summary>
		/// uac
		/// </summary>
		public override UAC HisUAC
		{
			get
			{
				UAC uc = new UAC();
				uc.IsView=true;
				return uc;
			}
		}

		/// <summary>
		/// �Ƿ񳬳���ʱ��
		/// </summary>
		public bool IsOutTime
		{
			get
			{
				return false;
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
				
				Map map = new Map("GTS_Exam");
				map.EnDesc="����";
				map.CodeStruct="5";
				map.EnType= EnType.Admin;

				map.AddTBStringPK(PaperExamRandomAttr.No,null,"ϵͳID",false,true,0,50,20);
				map.AddDDLSysEnum(PaperExamRandomAttr.ExamState,0,"״̬",true,true);
				map.AddDDLEntities(PaperExamRandomAttr.FK_Paper,null,"�Ծ�",new PaperExts(),false);
				map.AddDDLEntities(PaperExamRandomAttr.FK_Emp,Web.WebUser.No,"ѧ��",new EmpExts(),false);
				map.AddDDLEntities(PaperExamRandomAttr.FK_Dept,Web.WebUser.FK_Dept,"����",new Depts(),false);

				map.AddTBInt(PaperExamRandomAttr.CentOfChoseOne,0,"��ѡ���",true,true);
				map.AddTBInt(PaperExamRandomAttr.CentOfChoseM,0,"��ѡ���",true,true);
				map.AddTBInt(PaperExamRandomAttr.CentOfFillBlank,0,"������",true,true);
				map.AddTBInt(PaperExamRandomAttr.CentOfJudgeTheme,0,"�ж����",true,true);
				map.AddTBInt(PaperExamRandomAttr.CentOfEssayQuestion,0,"�ʴ����",true,true);
				map.AddTBInt(PaperExamRandomAttr.CentOfRC,0,"�Ķ�������",true,true);
				map.AddTBInt(PaperExamRandomAttr.CentOfSum,0,"�ܷ�",true,true);
				map.AddTBInt(PaperExamRandomAttr.CentOfSum,0,"�ܷ�",true,true);


				map.AddDDLEntities(PaperExamAttr.FK_Level,"0","�ȼ�",new Levels(),false);
				map.AddTBDateTime(PaperExamRandomAttr.FromDateTime,"��ʱ��",true,true);
				map.AddTBDateTime(PaperExamRandomAttr.ToDateTime,"��",true,true);
				map.AddTBInt(PaperExamRandomAttr.MM,0,"��ʱ(����)",true,false);

				//map.AttrsOfSearch.AddFromTo("���ڴ�",PaperExamRandomAttr.FromDateTime,DateTime.Now.AddDays(-30).ToString(DataType.SysDataFormat) , DataType.CurrentData,8);
				map.AddSearchAttr(ExamAttr.FK_Paper);
				map.AddSearchAttr(ExamAttr.FK_Dept);
				map.AddSearchAttr(ExamAttr.ExamState);
				map.AddSearchAttr(PaperExamAttr.FK_Level);
				 
				this._enMap=map;
				return this._enMap;
			}
		}
		#endregion 

		#region ���췽��
		/// <summary>
		/// �Ծ��
		/// </summary> 
		public Exam()
		{
		}
		#endregion 

		 

		 

	}
	/// <summary>
	///  �Ծ��
	/// </summary>
	public class Exams :EntitiesOID
	{
		/// <summary>
		/// Exams
		/// </summary>
		public Exams(){}
		/// <summary>
		/// Exam
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new Exam();
			}
		}
	}
}
