using System;
using System.Data;
using BP.DA;
using BP.En;

namespace BP.GTS
{
	
	/// <summary>
	/// �ж������
	/// </summary>
	public class WorkVSJudgeThemeAttr  :WorkVSBaseAttr
	{
		#region ��������
		/// <summary>
		/// �ж���Ŀ
		/// </summary>
		public const  string FK_JudgeTheme="FK_JudgeTheme";
		#endregion	
	}
	/// <summary>
	/// �ж������ ��ժҪ˵����
	/// </summary>
	public class WorkVSJudgeTheme :WorkVSBase
	{
		#region ��������
		/// <summary>
		///�ж���
		/// </summary>
		public string FK_JudgeTheme
		{
			get
			{
				return this.GetValStringByKey(WorkVSJudgeThemeAttr.FK_JudgeTheme);
			}
			set
			{
				SetValByKey(WorkVSJudgeThemeAttr.FK_JudgeTheme,value);
			}
		}		  
		 
		#endregion

		#region ��չ����
		 
		#endregion		

		#region ���캯��
		/// <summary>
		/// ������Ա��λ
		/// </summary> 
		public WorkVSJudgeTheme()
		{
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
				
				Map map = new Map("GTS_WorkVSJudgeTheme");
				map.EnDesc="�ж������";	
				map.EnType=EnType.Dot2Dot;
		 
				map.AddDDLEntitiesPK(WorkVSJudgeThemeAttr.FK_Work,"0001","�Ծ�",new WorkFixDesigns(),true);
				map.AddDDLEntitiesPK(WorkVSJudgeThemeAttr.FK_JudgeTheme,null,"�ж���",new JudgeThemes(),true);
				map.AddTBInt(WorkVSJudgeThemeAttr.Cent,1,"��",true,true);

				//map.AddSearchAttr(EmpDutyAttr.FK_Emp);
				//map.AddSearchAttr(EmpDutyAttr.FK_Duty);

				this._enMap=map;
				return this._enMap;
			}
		}
		#endregion

		#region ���ػ��෽��

		#endregion 
	
	}
	/// <summary>
	/// �ж������ 
	/// </summary>
	public class WorkVSJudgeThemes : WorkVSBases
	{
		#region ����
		/// <summary>
		/// �ж������
		/// </summary>
		public WorkVSJudgeThemes(){}
		#endregion

		#region ����
		/// <summary>
		/// �õ����� Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new WorkVSJudgeTheme();
			}
		}	
		#endregion 

		#region ��ѯ����
		 
		#endregion
	}
	
}
