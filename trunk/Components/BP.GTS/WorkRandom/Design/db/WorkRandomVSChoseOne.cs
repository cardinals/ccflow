using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.En.Base;

namespace BP.GTS
{
	
	/// <summary>
	/// ����ѡ�������
	/// </summary>
	public class WorkRandomVSChoseOneAttr  
	{
		#region ��������
		/// <summary>
		/// �Ծ�
		/// </summary>
		public const  string FK_Work="FK_Work";
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
	public class WorkRandomVSChoseOne :WorkVSBase
	{
		#region ��������
		/// <summary>
		///�ж���
		/// </summary>
		public string FK_Chose
		{
			get
			{
				return this.GetValStringByKey(WorkRandomVSChoseOneAttr.FK_Chose);
			}
			set
			{
				SetValByKey(WorkRandomVSChoseOneAttr.FK_Chose,value);
			}
		}
		#endregion

		#region ��չ����
		 
		#endregion		

		#region ���캯��
		/// <summary>
		/// ������Ա��λ
		/// </summary> 
		public WorkRandomVSChoseOne()
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
				
				Map map = new Map("GTS_WorkRandomVSChoseOne");
				map.EnDesc="����ѡ�������";	
				map.EnType=EnType.Dot2Dot;
		 
				map.AddDDLEntitiesPK(WorkRandomVSChoseOneAttr.FK_Work,"0001","�Ծ�",new WorkFixDesigns(),true);
				map.AddDDLEntitiesPK(WorkRandomVSChoseOneAttr.FK_Chose,null,"����ѡ����",new Choses(),true);
				map.AddTBInt(WorkRandomVSChoseOneAttr.Cent,1,"��",true,true);

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
	public class WorkRandomVSChoseOnes : WorkVSBases
	{
		#region ����
		/// <summary>
		/// ����ѡ�������
		/// </summary>
		public WorkRandomVSChoseOnes(){}
		#endregion

		#region ����
		/// <summary>
		/// �õ����� Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new WorkRandomVSChoseOne();
			}
		}	
		#endregion 

		#region ��ѯ����
		 
		#endregion
	}
	
}
