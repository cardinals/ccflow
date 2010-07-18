using System;
using System.Data;
using BP.DA;
using BP.En;

namespace BP.GTS
{
	
	/// <summary>
	/// ��������
	/// </summary>
	public class WorkVSFillBlankAttr:WorkVSBaseAttr
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
	public class WorkVSFillBlank :WorkVSBase
	{
		#region ��������
		/// <summary>
		///�����
		/// </summary>
		public string FK_FillBlank
		{
			get
			{
				return this.GetValStringByKey(WorkVSFillBlankAttr.FK_FillBlank);
			}
			set
			{
				SetValByKey(WorkVSFillBlankAttr.FK_FillBlank,value);
			}
		}		  
		#endregion

		#region ��չ����
		 
		#endregion		

		#region ���캯��
		/// <summary>
		/// ������Ա��λ
		/// </summary> 
		public WorkVSFillBlank()
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
				
				Map map = new Map("GTS_WorkVSFillBlank");
				map.EnDesc="��������";	
				map.EnType=EnType.Dot2Dot;
		 
				map.AddDDLEntitiesPK(WorkVSFillBlankAttr.FK_Work,"0001","�Ծ�", new WorkFixDesigns(),true);
				map.AddDDLEntitiesPK(WorkVSFillBlankAttr.FK_FillBlank,null,"�����",new FillBlanks(),true);
				map.AddTBInt(WorkVSFillBlankAttr.Cent,1,"��",true,true);

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
	public class WorkVSFillBlanks : WorkVSBases
	{
		#region ����
		/// <summary>
		/// ��������
		/// </summary>
		public WorkVSFillBlanks(){}
		#endregion

		#region ����
		/// <summary>
		/// �õ����� Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new WorkVSFillBlank();
			}
		}	
		#endregion 

		#region ��ѯ����
		 
		#endregion
	}
	
}
