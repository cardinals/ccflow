using System;
using System.Data;
using BP.DA;
using BP.En;

namespace BP.GTS
{
	
	/// <summary>
	/// ����ѡ�������
	/// </summary>
	public class WorkVSBaseAttr  
	{
		#region ��������
		/// <summary>
		/// ��ҵ
		/// </summary>
		public const  string FK_Work="FK_Work";
		/// <summary>
		/// ����
		/// </summary>
		public const  string Cent="Cent";
		#endregion	
	}
	/// <summary>
	/// ����ѡ������� ��ժҪ˵����
	/// </summary>
	abstract public class WorkVSBase :Entity
	{
		#region ��������
		/// <summary>
		///���
		/// </summary>
		public string FK_Work
		{
			get
			{
				return this.GetValStringByKey(WorkVSBaseAttr.FK_Work);
			}
			set
			{
				SetValByKey(WorkVSBaseAttr.FK_Work,value);
			}
		}
		/// <summary>
		/// ����
		/// </summary>
		public decimal Cent
		{
			get
			{
				return this.GetValDecimalByKey(WorkVSBaseAttr.Cent);
			}
			set
			{
				SetValByKey(WorkVSBaseAttr.Cent,value);
			}
		}
		#endregion

		 

		#region ���캯��
		/// <summary>
		/// ������Ա��λ
		/// </summary> 
		public WorkVSBase()
		{
		}
		#endregion

		#region ���ػ��෽��
		public override UAC HisUAC
		{
			get
			{
				UAC uac = new UAC();
				uac.OpenForAppAdmin();
				return uac;
				//return base.HisUAC;
			}
		}


		#endregion 
	
	}
	/// <summary>
	/// ����ѡ������� 
	/// </summary>
	abstract public class WorkVSBases : Entities
	{
		#region ����
		/// <summary>
		/// ����ѡ�������
		/// </summary>
		public WorkVSBases(){}
		#endregion

		#region ��ѯ����
		#endregion
	}
	
}
