using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.Port;
namespace BP.YG
{
	/// <summary>
	/// ��Ʒ
	/// </summary>
	public class JiangPinAttr
	{
		#region ��������
		public const  string No="No";
		/// <summary>
		/// ��Ʒ����
		/// </summary>
		public const  string Name="Name";
		/// <summary>
		/// �Ķ���
		/// </summary>
		public const string PayCent="PayCent";
		public const string Cost="Cost";

		public const string MyFileName="MyFileName";
		public const string MyFilePath="MyFilePath";
		public const string MyFileExt="MyFileExt";
		#endregion
		public const string Note="Note";
        public const string KuChun = "KuChun";
	}
	/// <summary>
	/// ��Ʒ
	/// </summary>
	public class JiangPin :EntityNoName
	{	
		#region ��������
		public string MyFileName
		{
			get
			{
				return this.GetValStringByKey(JiangPinAttr.MyFileName);
			}
			set
			{
				this.SetValByKey(JiangPinAttr.MyFileName,value);
			}
		}
		public string MyFilePath
		{
			get
			{
				return this.GetValStringByKey(JiangPinAttr.MyFilePath);
			}
			set
			{
				this.SetValByKey(JiangPinAttr.MyFilePath,value);
			}
		}
		public string MyFileExt
		{
			get
			{
				return this.GetValStringByKey(JiangPinAttr.MyFileExt);
			}
			set
			{
				this.SetValByKey(JiangPinAttr.MyFileExt,value);
			}
		}
        public int KuChun
        {
            get
            {
                return this.GetValIntByKey(JiangPinAttr.KuChun);
            }
            set
            {
                this.SetValByKey(JiangPinAttr.KuChun, value);
            }
        }
		public int PayCent
		{
			get
			{
				return this.GetValIntByKey(JiangPinAttr.PayCent);
			}
			set
			{
				this.SetValByKey(JiangPinAttr.PayCent,value);
			}
		}
		public decimal Cost
		{
			get
			{
				return this.GetValDecimalByKey(JiangPinAttr.Cost);
			}
			set
			{
				this.SetValByKey(JiangPinAttr.Cost,value);
			}
		}
		public string Note
		{
			get
			{
				return this.GetValStringByKey(JiangPinAttr.Note);
			}
			set
			{
				this.SetValByKey(JiangPinAttr.Note,value);
			}
		}
		#endregion 

		#region ���캯��
		public override UAC HisUAC
		{
			get
			{
				UAC uac = new UAC();
				uac.OpenAll();
				return uac;
			}
		}
		/// <summary>
		/// ��Ʒ
		/// </summary>		
		public JiangPin()
		{
		}
		/// <summary>
		/// JiangPinMap
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
                map.PhysicsTable = "YG_JiangPin";
                map.AdjunctType = AdjunctType.AllType;
                map.DepositaryOfMap = Depositary.Application;
                map.DepositaryOfEntity = Depositary.None;
                map.EnDesc = "��Ʒ";
                map.EnType = EnType.App;
                map.IsAutoGenerNo = true;

                map.AddTBStringPK(JiangPinAttr.No, null, "���", true, false, 2, 2, 2);
                map.AddTBString(JiangPinAttr.Name, null, "��Ʒ����", true, false, 0, 50, 200);
                map.AddTBMoney(JiangPinAttr.Cost, 0, "��ֵ(�����)", true, false);
                map.AddTBInt(JiangPinAttr.KuChun, 0, "ʣ��", true, false);
                map.AddTBInt("PayCent", 0, "����", true, false);
                map.AddTBStringDoc(JiangPinAttr.Note, null, "����", true, false);
                map.AddMyFile("��Ʒ��Ƭ");
                #endregion

                this._enMap = map;
                return this._enMap;
            }
		}
		#endregion

		#region ����
		/// <summary>
		/// 
		/// </summary>
		public void UpdateReadTimes()
		{
		}
		protected override bool beforeUpdateInsertAction()
		{
			return base.beforeUpdateInsertAction ();
		}
		#endregion 
		
	}
	/// <summary>
	/// ��Ʒ
	/// </summary>
	public class JiangPins : Entities
	{
		#region Entity
		/// <summary>
		/// �õ����� Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new JiangPin();
			}
		}
		#endregion 

		#region ���췽��
		/// <summary>
		/// ��Ʒ
		/// </summary>
		public JiangPins()
		{
		}
		#endregion
	}
	
}
