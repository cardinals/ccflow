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
	public class WorkDtlAttr:EntityNoAttr
	{
		/// <summary>
		/// �Ծ�
		/// </summary>
		public const string FK_Work="FK_Work";
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
	}
	/// <summary>
	/// �Ծ��
	/// </summary>
	public class WorkDtl :EntityNo
	{
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

				Map map = new Map("V_GTS_WorkDtl");
				map.EnDesc="��ҵ";
				map.CodeStruct="5";
				map.EnType= EnType.View;

				map.AddTBStringPK(WorkDtlAttr.No,null,"���",true,false,1,30,20);
				map.AddDDLEntities(WorkDtlAttr.FK_Work,null,"��ҵ",new WorkExts(),false);
				map.AddDDLEntities(WorkDtlAttr.FK_Emp,Web.WebUser.No,"ѧ��",new Emps(),false);
                map.AddDDLEntities(WorkDtlAttr.FK_Dept, null, "����", new BP.Port.Depts(), false);

				map.AddTBInt(WorkDtlAttr.CentOfChoseOne,0,"��ѡ��",true,true);
				map.AddTBInt(WorkDtlAttr.CentOfChoseM,0,"��ѡ��",true,true);
				map.AddTBInt(WorkDtlAttr.CentOfFillBlank,0,"�����",true,true);
				map.AddTBInt(WorkDtlAttr.CentOfJudgeTheme,0,"�ж���",true,true);
				map.AddTBInt(WorkDtlAttr.CentOfEssayQuestion,0,"�ʴ���",true,true);
				map.AddTBInt(WorkDtlAttr.CentOfRC,0,"�Ķ������",true,true);
				map.AddTBInt(WorkDtlAttr.CentOfSum,0,"�ܷ�",true,true);
				map.AddTBDecimal(WorkDtlAttr.RightRate,0,"��ȷ��%",true,true);
				map.AddDDLEntities(WorkDtlAttr.FK_Level,"1","�ɼ��ȼ�",new Levels(),false);

				map.AddSearchAttr(WorkDtlAttr.FK_Work);
				map.AddSearchAttr(WorkDtlAttr.FK_Dept);
				map.AddSearchAttr(WorkDtlAttr.FK_Level);
			 
				this._enMap=map;
				return this._enMap;
			}
		}
	}
	/// <summary>
	///  �Ծ��
	/// </summary>
	public class WorkDtls :EntitiesNo
	{
		/// <summary>
		/// WorkDtls
		/// </summary>
		public WorkDtls(){}
		/// <summary>
		/// WorkDtl
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new WorkDtl();
			}
		}
	}
}
