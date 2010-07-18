using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.En.Base;

namespace BP.GTS
{
	
	/// <summary>
	/// ��������
	/// </summary>
	public class WorkRandomVSFillBlankAttr:WorkVSBaseAttr
	{
		#region ��������
		/// <summary>
		/// �����
		/// </summary>
		public const  string FK_FillBlank="FK_FillBlank";
		#endregion	
	}
	/// <summary>
	/// �������� ��ժҪ˵����
	/// </summary>
	public class WorkRandomVSFillBlank :WorkVSBase
	{
		#region ��������
		/// <summary>
		///�����
		/// </summary>
		public string FK_FillBlank
		{
			get
			{
				return this.GetValStringByKey(WorkRandomVSFillBlankAttr.FK_FillBlank);
			}
			set
			{
				SetValByKey(WorkRandomVSFillBlankAttr.FK_FillBlank,value);
			}
		}		  
		#endregion

		#region ��չ����
		 
		#endregion		

		#region ���캯��
		/// <summary>
		/// ������Ա��λ
		/// </summary> 
		public WorkRandomVSFillBlank()
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
				
				Map map = new Map("GTS_WorkRandomVSFillBlank");
				map.EnDesc="��������";	
				map.EnType=EnType.Dot2Dot;
		 
				map.AddDDLEntitiesPK(WorkRandomVSFillBlankAttr.FK_Work,"0001","�Ծ�", new WorkFixDesigns(),true);
				map.AddDDLEntitiesPK(WorkRandomVSFillBlankAttr.FK_FillBlank,null,"�����",new FillBlanks(),true);
				map.AddTBInt(WorkRandomVSFillBlankAttr.Cent,1,"��",true,true);

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
	/// �������� 
	/// </summary>
	public class WorkRandomVSFillBlanks : WorkVSBases
	{
		#region ����
		/// <summary>
		/// ��������
		/// </summary>
		public WorkRandomVSFillBlanks(){}
		#endregion

		#region ����
		/// <summary>
		/// �õ����� Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new WorkRandomVSFillBlank();
			}
		}	
		#endregion 

		#region ��ѯ����
		 
		#endregion
	}
	
}
