
using System;
using System.Collections;
using System.Data ; 
using BP.DA;
using BP.En.Base;
using BP.En;
using BP.Web; 

namespace BP.WF
{

	/// <summary>
	/// �����������������
	/// </summary>
	public class PCWorkAttr:WorkAttr
	{		 
	}
	/// <summary>	 
	/// �������������,���м����������Ҫ������̳�
	/// ����������ص���,
	/// 1,�����ͨ�����Ȼ�ȡ�ⲿ������.
	/// 2,��������Ϣ��������Ϣ�б�.
	/// 3,��������������Ա��ϵͳ�Զ��������ȡ��.
	/// 4,
	/// </summary>
	abstract public class PCWork : Work, IDTS
	{
		#region ��������		 
		#endregion

		#region ��Ҫ����ʵ�ֵķ���
		/// <summary>
		/// ִ�л�ȡ�ⲿ����
		/// </summary>
		public abstract void InitData();
		
		#endregion

		#region ���캯��
		/// <summary>
		/// ��������
		/// </summary>
		protected PCWork()
		{
		}
		/// <summary>
		/// ��������
		/// </summary>
		/// <param name="wfID">�������̵�ID</param>
        protected PCWork(Int64 wfID)
            : base(wfID)
		{
		}		 
		#endregion

		#region   beforeUpdate 
		protected override bool beforeUpdate()
		{
			this.InitData();
			return base.beforeUpdate ();
		}
		#endregion

		 
	}
	/// <summary>
	/// �������̲ɼ���Ϣ�Ļ��� ����
	/// </summary>
	abstract public class PCWorks : Works
	{
		#region ��չ����
		
		#endregion

		#region ���췽��
		/// <summary>
		/// ��Ϣ�ɼ�����
		/// </summary>
		public PCWorks()
		{
		}
		#endregion 

		#region ��������
		/// <summary>
		/// ִ�г�ʼ������
		/// </summary>
		public  void DoInitData()
		{
			//PCWorks ens = new PCWorks();
			QueryObject qo = new QueryObject(this);
			qo.AddWhere( WorkAttr.NodeState ,(int) NodeState.Init);
			if (qo.DoQuery()==0)
				return;

			string currEmpId= BP.Web.WebUser.No;
			bool isAu = BP.Web.WebUser.IsAuthorize ; 
			int myempid=0;
		 	foreach(PCWork wk in this)
			{
				try
				{
					
					string empid = wk.Recorder ; //ȡ����ǰ�Ĺ�����Ա��

					if (Web.WebUser.No!=empid)
					{
						/* �����ǰ����Ա�� ���ҵ�����Ա�����. */
						Web.WebUser.Exit();
						Emp mp= new Emp( empid ) ; 
						BP.Web.WebUser.SignInOfWFQH(mp,false);
					}

					wk.Update();
					wk.InitData();


					Node nd = new Node( this.ToString()  ) ;
					WorkNode wn = new WorkNode(wk,nd);
                    string msg = wn.AfterNodeSave(false, false, DateTime.Now);
					 
					Log.DefaultLogWriteLineInfo( msg );
				}
				catch(Exception ex)
				{
					Log.DefaultLogWriteLineError(ex.Message+" : User="+WebUser.No);
				}
			}

			Web.WebUser.Exit();
			Emp emp = new Emp(currEmpId); //��ǰ����Ա�ڵ�¼����.
			BP.Web.WebUser.SignInOfWFQH(emp,false);
			return ;
		}
		#endregion 
	}
}
