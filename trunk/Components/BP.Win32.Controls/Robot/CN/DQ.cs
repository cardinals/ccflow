using System;
using System.Data;
using BP.DA;
using BP.En.Base;
using BP.En;
using BP.Port;
//using BP.ZHZS.DS;


namespace BP.CN
{
	/// <summary>
	/// �޵���
	/// </summary>
	public class ZDSAttr: EntityNoNameAttr
	{
		#region ��������
		public const  string FK_PQ="FK_PQ";
        public const string FK_ZDS = "FK_ZDS";
		#endregion
	}
	/// <summary>
    /// �޵���
	/// </summary>
	public class ZDS :EntityNoName
	{	
		#region ��������
		 
		#endregion 

		#region ���캯��
		public override UAC HisUAC
		{
			get
			{
				UAC uac = new UAC();
				uac.OpenForSysAdmin();
				return uac;
			}
		}
		/// <summary>
		/// �޵���
		/// </summary>		
		public ZDS(){}
		public ZDS(string no):base(no)
		{
		}

		
		/// <summary>
		/// Map
		/// </summary>
		public override Map EnMap
		{
            get
            {
                if (this._enMap != null)
                    return this._enMap;
                Map map = new Map();

                #region ��������
                map.EnDBUrl = new DBUrl(DBUrlType.AppCenterDSN);
                map.PhysicsTable = "CN_ZDS";
                map.AdjunctType = AdjunctType.AllType;
                map.DepositaryOfMap = Depositary.Application;
                map.DepositaryOfEntity = Depositary.None;
                map.IsAllowRepeatNo = false;
                map.IsCheckNoLength = false;
                map.EnDesc = "�޵���";
                map.EnType = EnType.App;
                map.CodeStruct = "4";
                #endregion

                #region �ֶ�
                map.AddTBStringPK(ZDSAttr.No, null, "���", true, false, 0, 50, 50);
                map.AddTBString(ZDSAttr.Name, null, "����", true, false, 0, 50, 200);

                map.AddDDLEntities(ZDSAttr.FK_PQ, null, "Ƭ��", new PQs(), true);
                map.AddDDLEntities(ZDSAttr.FK_ZDS, null, "ʡ��", new SFs(), true);
                #endregion

                this._enMap = map;
                return this._enMap;
            }
		}
		#endregion
		 
	}
	/// <summary>
	/// �޵���
	/// </summary>
	public class ZDSs : EntitiesNoName
	{
		#region 
		/// <summary>
		/// �õ����� Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new ZDS();
			}
		}	
		#endregion 

		#region ���췽��
		/// <summary>
		/// �޵���s
		/// </summary>
		public ZDSs(){}
		#endregion
	}
	
}
