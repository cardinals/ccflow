using System;
using System.Data;
using BP.DA;
using BP.En;

namespace BP.GTS
{
	
	/// <summary>
	/// �ж������
	/// </summary>
	public class PaperVSJudgeThemeAttr  
	{
		#region ��������
		/// <summary>
		/// �Ծ�
		/// </summary>
		public const  string FK_Paper="FK_Paper";
		/// <summary>
		/// �ж���Ŀ
		/// </summary>
		public const  string FK_JudgeTheme="FK_JudgeTheme";
		/// <summary>
		/// ����
		/// </summary>
		public const  string Cent="Cent";
		#endregion	
	}
	/// <summary>
	/// �ж������ ��ժҪ˵����
	/// </summary>
	public class PaperVSJudgeTheme :Entity
	{
		#region ��������
		/// <summary>
		///�ж���
		/// </summary>
		public string FK_JudgeTheme
		{
			get
			{
				return this.GetValStringByKey(PaperVSJudgeThemeAttr.FK_JudgeTheme);
			}
			set
			{
				SetValByKey(PaperVSJudgeThemeAttr.FK_JudgeTheme,value);
			}
		}		  
		/// <summary>
		///���
		/// </summary>
		public string FK_Paper
		{
			get
			{
				return this.GetValStringByKey(PaperVSJudgeThemeAttr.FK_Paper);
			}
			set
			{
				SetValByKey(PaperVSJudgeThemeAttr.FK_Paper,value);
			}
		}
		/// <summary>
		/// ����
		/// </summary>
		public int Cent
		{
			get
			{
				return this.GetValIntByKey(PaperVSJudgeThemeAttr.Cent);
			}
			set
			{
				SetValByKey(PaperVSJudgeThemeAttr.Cent,value);
			}
		}		 
		#endregion

		#region ��չ����
		 
		#endregion		

		#region ���캯��
		/// <summary>
		/// HisUAC
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
		/// ������Ա��λ
		/// </summary> 
		public PaperVSJudgeTheme()
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
				
				Map map = new Map("GTS_PaperVSJudgeTheme");
				map.EnDesc="�ж������";	
				map.EnType=EnType.Dot2Dot;
		 
				map.AddDDLEntitiesPK(PaperVSJudgeThemeAttr.FK_Paper,"0001","�Ծ�",new Papers(),true);
				map.AddDDLEntitiesPK(PaperVSJudgeThemeAttr.FK_JudgeTheme,null,"�ж���",new JudgeThemes(),true);
				map.AddTBInt(PaperVSJudgeThemeAttr.Cent,1,"��",true,true);

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
	public class PaperVSJudgeThemes : Entities
	{
		#region ����
		/// <summary>
		/// �ж������
		/// </summary>
		public PaperVSJudgeThemes(){}
		#endregion

		#region ����
		/// <summary>
		/// �õ����� Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new PaperVSJudgeTheme();
			}
		}	
		#endregion 

		#region ��ѯ����
		 
		#endregion
	}
	
}
