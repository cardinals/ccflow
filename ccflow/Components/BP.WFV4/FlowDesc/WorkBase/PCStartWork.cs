
using System;
using System.Data;
using System.Collections;
using BP.DA;
using BP.En.Base;
using BP.En;

namespace BP.WF
{

	/// <summary>
	/// ��ʼ������������
	/// </summary>
	public class PCStartWorkAttr:StartWorkAttr
	{ 
	}
	/// <summary>	 
	/// PC��ʼ��������,���п�ʼPC������Ҫ������̳�
	/// ����������ص���:
	/// 1, �������������.
	/// 2, 
	/// </summary>
	abstract public class PCStartWork:StartWork 
	{

		#region ��Ҫ����ʵ�ֵķ���
		
		#endregion 		 		

		#region ���캯��
		/// <summary>
		/// ��������
		/// </summary>
		protected PCStartWork(){}
			 
		#endregion
		
		#region  ��д����ķ�����
		 
		/// <summary>
		/// �߼�����
		/// </summary>
		/// <returns></returns>
		protected override bool beforeInsert()
		{			 
			base.beforeInsert();
			return true;
		}
		/// <summary>
		/// �߼�����
		/// </summary>
		/// <returns></returns>
		protected override bool beforeUpdate()
		{			
			base.beforeUpdate();
			return true;
		}
		/// <summary>
		/// �߼�����
		/// </summary>
		/// <returns></returns>
		protected override bool beforeDelete()
		{
			base.beforeDelete();
			return true;
		}
		/// <summary>
		/// �߼�����
		/// </summary>
		protected override void afterDelete()
		{
			base.afterDelete();
			return;
		}
		/// <summary>
		/// �߼�����,�ڽ���֮��Ҫ���Ĺ���.
		/// 1, ������������.
		/// 2, ������������Ϣ������������������. 
		/// </summary>
		protected override  void afterInsert()
		{
			/*try
			{
				//������������Ϣ������������������.
				WFInfo wf = new WFInfo();
				wf.WFOID =this.OID;
				wf.Insert();
			}
			catch(Exception ex)
			{
				this.Delete();
				throw new Exception("@�������̳��ִ���:"+ex.Message);
			}*/
			base.afterInsert();
			return ;
		}
		/// <summary>
		/// �߼�����
		/// </summary>
		protected override void afterUpdate()
		{
			base.afterUpdate();
			return ;
		}
		#endregion

		#region ����
		 
		#endregion 
		 
	}
	/// <summary>
	/// �������̲ɼ���Ϣ�Ļ��� ����
	/// </summary>
	abstract public class PCStartWorks : StartWorks
	{
		/// <summary>
		/// �����Զ�������������.
		/// </summary>
		public virtual string AutoGenerWorkFlow()
		{
			return "";
		}	 

		#region ���췽��
		/// <summary>
		/// ��Ϣ�ɼ�����
		/// </summary>
		public PCStartWorks()
		{
		}
		#endregion

		 

	
	}
}
