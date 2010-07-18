using System;
using System.Data;
using BP.DA;
using BP.En;

namespace BP.GTS
{
	
	/// <summary>
	/// ����ѡ�������
	/// </summary>
	public class PaperVSChoseOneAttr  
	{
		#region ��������
		/// <summary>
		/// �Ծ�
		/// </summary>
		public const  string FK_Paper="FK_Paper";
		/// <summary>
		/// ѡ����Ŀ
		/// </summary>
		public const  string FK_Chose="FK_Chose";
		/// <summary>
		/// ����
		/// </summary>
		public const  string Cent="Cent";
		#endregion	
	}
	/// <summary>
	/// ����ѡ������� ��ժҪ˵����
	/// </summary>
	public class PaperVSChoseOne :Entity
	{
		#region ��������
		/// <summary>
		///�ж���
		/// </summary>
		public string FK_Chose
		{
			get
			{
				return this.GetValStringByKey(PaperVSChoseOneAttr.FK_Chose);
			}
			set
			{
				SetValByKey(PaperVSChoseOneAttr.FK_Chose,value);
			}
		}		  
		/// <summary>
		///���
		/// </summary>
		public string FK_Paper
		{
			get
			{
				return this.GetValStringByKey(PaperVSChoseOneAttr.FK_Paper);
			}
			set
			{
				SetValByKey(PaperVSChoseOneAttr.FK_Paper,value);
			}
		}
		/// <summary>
		/// ����
		/// </summary>
		public int Cent_DE
		{
			get
			{
				return this.GetValIntByKey(PaperVSChoseOneAttr.Cent);
			}
			set
			{
				SetValByKey(PaperVSChoseOneAttr.Cent,value);
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
		public PaperVSChoseOne()
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
				
				Map map = new Map("GTS_PaperVSChoseOne");
				map.EnDesc="����ѡ�������";	
				map.EnType=EnType.Dot2Dot;
		 
				map.AddDDLEntitiesPK(PaperVSChoseOneAttr.FK_Paper,"0001","�Ծ�",new Papers(),true);
				map.AddDDLEntitiesPK(PaperVSChoseOneAttr.FK_Chose,null,"����ѡ����",new Choses(),true);
				map.AddTBInt(PaperVSChoseOneAttr.Cent,1,"��",true,true);

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
	/// ����ѡ������� 
	/// </summary>
	public class PaperVSChoseOnes : Entities
	{
		#region ����
		/// <summary>
		/// ����ѡ�������
		/// </summary>
		public PaperVSChoseOnes(){}
		#endregion

		#region ����
		/// <summary>
		/// �õ����� Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new PaperVSChoseOne();
			}
		}	
		#endregion 

		#region ��ѯ����
		 
		#endregion
	}
	
}
