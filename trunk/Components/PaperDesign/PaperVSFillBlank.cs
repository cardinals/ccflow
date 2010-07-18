using System;
using System.Data;
using BP.DA;
using BP.En;

namespace BP.GTS
{
	
	/// <summary>
	/// ��������
	/// </summary>
	public class PaperVSFillBlankAttr  
	{
		#region ��������
		/// <summary>
		/// �Ծ�
		/// </summary>
		public const  string FK_Paper="FK_Paper";
		/// <summary>
		/// �����
		/// </summary>
		public const  string FK_FillBlank="FK_FillBlank";
		/// <summary>
		/// ����
		/// </summary>
		public const  string Cent="Cent";
		#endregion	
	}
	/// <summary>
	/// �������� ��ժҪ˵����
	/// </summary>
	public class PaperVSFillBlank :Entity
	{
		#region ��������
		/// <summary>
		///�����
		/// </summary>
		public string FK_FillBlank
		{
			get
			{
				return this.GetValStringByKey(PaperVSFillBlankAttr.FK_FillBlank);
			}
			set
			{
				SetValByKey(PaperVSFillBlankAttr.FK_FillBlank,value);
			}
		}		  
		/// <summary>
		///���
		/// </summary>
		public string FK_Paper
		{
			get
			{
				return this.GetValStringByKey(PaperVSFillBlankAttr.FK_Paper);
			}
			set
			{
				SetValByKey(PaperVSFillBlankAttr.FK_Paper,value);
			}
		}
		/// <summary>
		/// ����
		/// </summary>
		public int Cent
		{
			get
			{
				return this.GetValIntByKey(PaperVSFillBlankAttr.Cent);
			}
			set
			{
				SetValByKey(PaperVSFillBlankAttr.Cent,value);
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
		public PaperVSFillBlank()
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
				
				Map map = new Map("GTS_PaperVSFillBlank");
				map.EnDesc="��������";	
				map.EnType=EnType.Dot2Dot;
		 
				map.AddDDLEntitiesPK(PaperVSFillBlankAttr.FK_Paper,"0001","�Ծ�",new Papers(),true);
				map.AddDDLEntitiesPK(PaperVSFillBlankAttr.FK_FillBlank,null,"�����",new FillBlanks(),true);
				map.AddTBInt(PaperVSFillBlankAttr.Cent,1,"��",true,true);

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
	public class PaperVSFillBlanks : Entities
	{
		#region ����
		/// <summary>
		/// ��������
		/// </summary>
		public PaperVSFillBlanks(){}
		#endregion

		#region ����
		/// <summary>
		/// �õ����� Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new PaperVSFillBlank();
			}
		}	
		#endregion 

		#region ��ѯ����
		 
		#endregion
	}
	
}
