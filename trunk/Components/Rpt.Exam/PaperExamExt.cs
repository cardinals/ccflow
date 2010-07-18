using System;
using System.Collections;
using BP.DA;
using BP.En;
using BP.Port;


namespace BP.GTS
{
	/// <summary>
	/// �Ծ��attr
	/// </summary>
	public class PaperExamExtAttr:EntityNoAttr
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
		/// �ɼ��ȼ�
		/// </summary>
		public const string FK_Level="FK_Level";
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
		/// <summary>
		/// ��׼��
		/// </summary>
		public const string RightRate="RightRate";
		/// <summary>
		/// �Ƿ��ǿ���
		/// </summary>
		public const string IsExam="IsExam";
	}
 
	/// <summary>
	/// �Ծ��
	/// </summary>
	public class PaperExamExt :EntityNo
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
		/// ��д���෽��
		/// </summary>
		public override Map EnMap
		{
			get
			{
				if (this._enMap!=null) 
					return this._enMap;
				
				Map map = new Map("V_GTS_PaperExam");
				map.EnDesc="����";
				map.CodeStruct="5";
				map.EnType= EnType.View;
				map.AddTBStringPK(PaperExamAttr.No,null,"���",false,false,1,30,10);
				map.AddDDLSysEnum(PaperExamAttr.ExamState,0,"״̬",true,false);
				map.AddDDLEntities(PaperExamAttr.FK_Paper,null,"�Ծ�",new PaperExts(),false);
				map.AddDDLEntities(PaperExamAttr.FK_Emp,Web.WebUser.No,"ѧ��",new Emps(),false);
                map.AddDDLEntities(PaperExamAttr.FK_Dept, null, "����", new BP.Port.Depts(), false);

				map.AddTBDecimal(PaperExamAttr.CentOfChoseOne,0,"��ѡ��",true,true);
                map.AddTBDecimal(PaperExamAttr.CentOfChoseM, 0, "��ѡ��", true, true);
                map.AddTBDecimal(PaperExamAttr.CentOfFillBlank, 0, "�����", true, true);
                map.AddTBDecimal(PaperExamAttr.CentOfJudgeTheme, 0, "�ж���", true, true);
                map.AddTBDecimal(PaperExamAttr.CentOfEssayQuestion, 0, "�ʴ���", true, true);
                map.AddTBDecimal(PaperExamAttr.CentOfRC, 0, "�Ķ������", true, true);
                map.AddTBDecimal(PaperExamAttr.CentOfSum, 0, "�ܷ�", true, true);
				map.AddTBDecimal(PaperExamAttr.RightRate,0,"��ȷ��%",true,true);
				map.AddDDLEntities(PaperExamAttr.FK_Level,"1","�ɼ��ȼ�",new Levels(),false);

				map.AddSearchAttr(PaperExamAttr.FK_Paper);
				map.AddSearchAttr(PaperExamAttr.FK_Dept);
				map.AddSearchAttr(PaperExamAttr.ExamState);
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
		public PaperExamExt()
		{
		}
		#endregion 

	}
	/// <summary>
	///  �Ծ��
	/// </summary>
	public class PaperExamExts :EntitiesNo
	{
		/// <summary>
		/// PaperExamExts
		/// </summary>
		public PaperExamExts(){}
		/// <summary>
		/// PaperExamExt
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new PaperExamExt();
			}
		}
	}
}
