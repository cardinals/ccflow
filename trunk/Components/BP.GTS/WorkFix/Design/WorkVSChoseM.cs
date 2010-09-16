using System;
using System.Data;
using BP.DA;
using BP.En;

namespace BP.GTS
{
	
	/// <summary>
	/// ����ѡ�������
	/// </summary>
	public class WorkVSChoseMAttr : WorkVSBaseAttr
	{
		#region ��������
		/// <summary>
		/// ѡ����Ŀ
		/// </summary>
		public const  string FK_Chose="FK_Chose";
		#endregion	
	}
	/// <summary>
	/// ����ѡ������� ��ժҪ˵����
	/// </summary>
	public class WorkVSChoseM :WorkVSBase
	{
		#region ��������
		/// <summary>
		///�ж���
		/// </summary>
		public string FK_Chose
		{
			get
			{
				return this.GetValStringByKey(WorkVSChoseMAttr.FK_Chose);
			}
			set
			{
				SetValByKey(WorkVSChoseMAttr.FK_Chose,value);
			}
		}
		 
		#endregion

		#region ��չ����
		 
		#endregion		

		#region ���캯��
		/// <summary>
		/// ������Ա��λ
		/// </summary> 
		public WorkVSChoseM()
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
				
				Map map = new Map("GTS_WorkVSChoseM");
				map.EnDesc="����ѡ�������";	
				map.EnType=EnType.Dot2Dot;
		 
				map.AddDDLEntitiesPK(WorkVSChoseMAttr.FK_Work,"0001","��ҵ",new WorkFixDesigns(),true);
				map.AddDDLEntitiesPK(WorkVSChoseMAttr.FK_Chose,null,"����ѡ����",new Choses(),true);
				map.AddTBInt(WorkVSChoseMAttr.Cent,1,"��",true,true);

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
	public class WorkVSChoseMs : WorkVSBases
	{
		#region ����
		/// <summary>
		/// ����ѡ�������
		/// </summary>
		public WorkVSChoseMs(){}
		#endregion

		#region ����
		/// <summary>
		/// �õ����� Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new WorkVSChoseM();
			}
		}	
		#endregion 

		#region ��ѯ����
		 
		#endregion
	}
	
}
