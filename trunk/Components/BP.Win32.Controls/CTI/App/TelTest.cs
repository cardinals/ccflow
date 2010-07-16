using System;
using System.Collections;
using BP.DA;
using BP.En.Base;
using BP.En;
using BP.Port;
using BP.CTI;

namespace BP.CTI.App
{
	 
	/// <summary>
	/// ���Ե绰����
	/// </summary>
	public class TelTestAttr
	{
		#region ����
		/// <summary>
		/// tel
		/// </summary>
		public const string Tel="Tel";
		/// <summary>
		/// 
		/// </summary>
		public const string TelName="TelName";
		/// <summary>
		/// ���
		/// </summary>
		public const string JE="JE";
		/// <summary>
		/// ����ʱ���
		/// </summary>
		public const string FK_TelType="FK_TelType";
    	#endregion
	}
	/// <summary>
	/// ���Ե绰
	/// </summary> 
	public class TelTest :Entity
	{

		#region ��������
		/// <summary>
		/// �绰
		/// </summary>
		public string  Tel
		{
			get
			{
				return  GetValStringByKey(TelTestAttr.Tel);
			}
			set
			{
				SetValByKey(TelTestAttr.Tel,value);
			}
		}	
		
		/// <summary>
		/// TelName
		/// </summary>
		public string  TelName
		{
			get
			{
				return GetValStringByKey(TelTestAttr.TelName);
			}
			set
			{
				SetValByKey(CallListAttr.TelName,value);
			}
		}
		public int FK_TelType 
		{
			get
			{
				return GetValIntByKey(TelTestAttr.FK_TelType);
			}
			set
			{
				SetValByKey(CallListAttr.FK_TelType,value);
			}
		}
		public float JE 
		{
			get
			{
				return GetValFloatByKey(TelTestAttr.JE);
			}
			set
			{
				SetValByKey(CallListAttr.JE,value);
			}
		}
		#endregion 

		#region ���췽��
		/// <summary>
		/// ����
		/// </summary>
		public TelTest()
		{
		}
		#endregion 

		#region Map
		/// <summary>
		/// EnMap
		/// </summary>
		public override Map EnMap
		{
			get
			{
				if (this._enMap!=null) 
					return this._enMap;
				Map map = new Map("CTI_TelTest");
				map.EnDesc="�绰����";
				 
				map.AddTBStringPK(TelTestAttr.Tel,null,"�绰",true,false,5,12,20);
				map.AddTBString(TelTestAttr.TelName,null,"�ͻ�",true,false,0,200,20);
				map.AddTBFloat(TelTestAttr.JE,100,"���",true,false);
				map.AddDDLEntities(TelTestAttr.FK_TelType,1,DataType.AppInt,"�绰����",new TelTypes(),TelTypeAttr.OID,TelTypeAttr.Name,true);
				this._enMap=map;
				return this._enMap;
			}
		}
		#endregion

		protected override void afterInsertUpdateAction()
		{
			this.InitCallList();
			base.afterInsertUpdateAction ();
		}
		protected override void afterDelete()
		{
			CallList cl =new CallList();
			cl.Tel=this.Tel;
			cl.Delete();
			base.afterDelete ();
		}



		/// <summary>
		/// 
		/// </summary>
		public void InitCallList()
		{
			CallList cl = new CallList();
			cl.Tel = this.Tel;
			cl.TelName =this.TelName;
			cl.FK_TelType =this.FK_TelType;
			cl.JE = this.JE;
			cl.CallDegree=-9;
			cl.CallingState=0;
			cl.Save();
		}
	}
	/// <summary>
	/// ���Ե绰
	/// </summary>
	public class TelTests :Entities
	{
		#region ����
		/// <summary>
		/// ��ʼ��
		/// </summary>
		public void InitCallList()
		{
			foreach(TelTest tt in this)
			{
				tt.InitCallList();
			}
		}
		#endregion 

		#region ���췽������
		/// <summary>
		/// TelTests
		/// </summary>
		public TelTests(){}
		#endregion 

		#region ����
		/// <summary>
		/// ���Ե绰
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new TelTest();
			}
		}
		#endregion
		 
	}
}
