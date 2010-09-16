using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.En.Base;

namespace BP.GTS
{
	
	/// <summary>
	/// �ж������
	/// </summary>
	public class WorkRandomVSJudgeThemeAttr  :WorkVSBaseAttr
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
	public class WorkRandomVSJudgeTheme :WorkVSBase
	{
		#region ��������
		/// <summary>
		///�ж���
		/// </summary>
		public string FK_JudgeTheme
		{
			get
			{
				return this.GetValStringByKey(WorkRandomVSJudgeThemeAttr.FK_JudgeTheme);
			}
			set
			{
				SetValByKey(WorkRandomVSJudgeThemeAttr.FK_JudgeTheme,value);
			}
		}		  
		 
		#endregion

		#region ��չ����
		 
		#endregion		

		#region ���캯��
		/// <summary>
		/// ������Ա��λ
		/// </summary> 
		public WorkRandomVSJudgeTheme()
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
				
				Map map = new Map("GTS_WorkRandomVSJudgeTheme");
				map.EnDesc="�ж������";	
				map.EnType=EnType.Dot2Dot;
		 
				map.AddDDLEntitiesPK(WorkRandomVSJudgeThemeAttr.FK_Work,"0001","�Ծ�",new WorkFixDesigns(),true);
				map.AddDDLEntitiesPK(WorkRandomVSJudgeThemeAttr.FK_JudgeTheme,null,"�ж���",new JudgeThemes(),true);
				map.AddTBInt(WorkRandomVSJudgeThemeAttr.Cent,1,"��",true,true);

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
	public class WorkRandomVSJudgeThemes : WorkVSBases
	{
		#region ����
		/// <summary>
		/// �ж������
		/// </summary>
		public WorkRandomVSJudgeThemes(){}
		#endregion

		#region ����
		/// <summary>
		/// �õ����� Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new WorkRandomVSJudgeTheme();
			}
		}	
		#endregion 

		#region ��ѯ����
		 
		#endregion
	}
	
}
