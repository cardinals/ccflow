using System;
using System.Data;
using BP.DA;
using BP.En;

namespace BP.GTS
{
	
	/// <summary>
	/// ����ѡ�������
	/// </summary>
	public class PaperVSChoseMAttr  
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
	public class PaperVSChoseM :Entity
	{
		#region ��������
		/// <summary>
		///�ж���
		/// </summary>
		public string FK_Chose
		{
			get
			{
				return this.GetValStringByKey(PaperVSChoseMAttr.FK_Chose);
			}
			set
			{
				SetValByKey(PaperVSChoseMAttr.FK_Chose,value);
			}
		}		  
		/// <summary>
		///���
		/// </summary>
		public string FK_Paper
		{
			get
			{
				return this.GetValStringByKey(PaperVSChoseMAttr.FK_Paper);
			}
			set
			{
				SetValByKey(PaperVSChoseMAttr.FK_Paper,value);
			}
		}
		/// <summary>
		/// ����
		/// </summary>
		public int Cent
		{
			get
			{
				return this.GetValIntByKey(PaperVSChoseMAttr.Cent);
			}
			set
			{
				SetValByKey(PaperVSChoseMAttr.Cent,value);
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
		public PaperVSChoseM()
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
				
				Map map = new Map("GTS_PaperVSChoseM");
				map.EnDesc="����ѡ�������";	
				map.EnType=EnType.Dot2Dot;
		 
				map.AddDDLEntitiesPK(PaperVSChoseMAttr.FK_Paper,"0001","�Ծ�",new Papers(),true);
				map.AddDDLEntitiesPK(PaperVSChoseMAttr.FK_Chose,null,"����ѡ����",new Choses(),true);
				map.AddTBInt(PaperVSChoseMAttr.Cent,1,"��",true,true);

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
	public class PaperVSChoseMs : Entities
	{
		#region ����
		/// <summary>
		/// ����ѡ�������
		/// </summary>
		public PaperVSChoseMs(){}
		#endregion

		#region ����
		/// <summary>
		/// �õ����� Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new PaperVSChoseM();
			}
		}	
		#endregion 

		#region ��ѯ����
		 
		#endregion
	}
	
}
