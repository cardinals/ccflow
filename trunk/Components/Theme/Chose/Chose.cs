using System;
using System.Collections;
using BP.DA;
using BP.En;
using BP.Port;

namespace BP.GTS
{
	/// <summary>
	/// ѡ��������
	/// </summary>
	public class ChoseAttr:EntityNoNameAttr
	{
		/// <summary>
		/// ѡ����
		/// </summary>
		public const string FK_ThemeSort="FK_ThemeSort";
		/// <summary>
		/// ѡ��������
		/// </summary>
		public const string ChoseType="ChoseType";
		/// <summary>
		/// ��Ŀ����
		/// </summary>
		public const string ItemNum="ItemNum";
	}
	/// <summary>
	/// ѡ����
	/// </summary>
	public class Chose :EntityNoName
	{
		#region attrs
		public ChoseItems HisChoseItems
		{
			get
			{
				return  new ChoseItems(this.No);
			}
		}
		/// <summary>
		/// ѡ��������
		/// </summary>
		public int ChoseType
		{
			get
			{
				return this.GetValIntByKey(ChoseAttr.ChoseType);
			}
			set
			{
				this.SetValByKey(ChoseAttr.ChoseType,value);
			}
		}
        public string FK_ThemeSort
        {
            get
            {
                return this.GetValStrByKey(ChoseAttr.FK_ThemeSort);
            }
            set
            {
                this.SetValByKey(ChoseAttr.FK_ThemeSort, value);
            }
        }
		#endregion
	 
		#region ʵ�ֻ����ķ���
		/// <summary>
		/// power
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
		/// ��д���෽��
		/// </summary>
		public override Map EnMap
		{
            get
            {
                if (this._enMap != null)
                    return this._enMap;

                Map map = new Map("GTS_Chose");
                map.EnDesc = "ѡ����";
                map.CodeStruct = "5";
                map.EnType = EnType.Admin;

                map.AddTBStringPK(ChoseAttr.No, null, "���", true, true, 0, 50, 20);
                map.AddTBString(ChoseAttr.Name, null, "��Ŀ����", true, false, 0, 1000, 600);

                map.AddDDLEntities(ChoseAttr.FK_ThemeSort, "0001", "����", new ThemeSorts(), true);

                map.AddDDLSysEnum(ChoseAttr.ChoseType, 0, "ѡ������", true, false);

                map.AddDtl(new ChoseItems(), ChoseItemAttr.FK_Chose);
                map.AddSearchAttr(ChoseAttr.FK_ThemeSort);
                map.AddSearchAttr(ChoseAttr.ChoseType);

                this._enMap = map;
                return this._enMap;
            }
		}
		#endregion 

		#region ���췽��
		/// <summary>
		/// ѡ����
		/// </summary> 
		public Chose()
		{
		}
		/// <summary>
		/// ѡ����
		/// </summary>
		/// <param name="_No">ѡ������</param> 
		public Chose(string _No ):base(_No)
		{
		}
		#endregion 

		#region �߼�����
		protected override bool beforeInsert()
		{
			//			this.No=this.GenerNewNo;
			return base.beforeInsert();
		}

		protected override bool beforeUpdate()
		{
			int i = DBAccess.RunSQLReturnValInt("select count(*) from GTS_ChoseItem where FK_Chose='"+this.No+"' and isOk=1 ");
			if (i>1)
			{
				this.ChoseType=1;
				/*����ѡ���� */
			}
			else if (i==1) 
			{
				this.ChoseType = 0;
			}

			this.AutoGenerItems();
			return base.beforeUpdate ();
		}
		protected override void afterDelete()
		{
			this.DeleteHisRefEns();
			base.afterDelete ();
		}
		 
		/// <summary>
		/// �Զ�����4��ѡ����Ŀ��
		/// </summary> 
		protected override void afterInsert()
		{
			// �Զ�����4��ѡ����Ŀ��
			this.AutoGenerItems();
			base.afterInsert ();
		}

		/// <summary>
		/// �Զ�����Item
		/// </summary>
		/// <param name="themeNo"></param>
		public void AutoGenerItems()
		{
			ChoseItems cts = new ChoseItems(this.No);
			if (cts.Count!=0)
				return;

			ChoseItem ctA = new ChoseItem();
			ctA.FK_Chose = this.No;
			ctA.Item="A";
			ctA.Insert();

			ChoseItem ctB = new ChoseItem();
			ctB.FK_Chose = this.No;
			ctB.Item="B";
			ctB.Insert();

			ChoseItem ctC = new ChoseItem();
			ctC.FK_Chose = this.No;
			ctC.Item="C";
			ctC.Insert();

			ChoseItem ctD = new ChoseItem();
			ctD.FK_Chose = this.No;
			ctD.Item="D";
			ctD.Insert();
		}
		#endregion

	}
	/// <summary>
	/// ѡ����
	/// </summary>
	public class Choses :EntitiesNoName
	{
		#region 
		/// <summary>
		/// RetrieveChonseOne
		/// </summary>
		/// <returns></returns>
		public int RetrieveAllChonseOne(int topNum)
		{
			QueryObject qo = new QueryObject(this);
			qo.Top = topNum;
			qo.AddWhere(ChoseAttr.ChoseType, 0 );
			qo.addOrderByRandom();
			return qo.DoQuery();
		}
		/// <summary>
		/// RetrieveChonseM
		/// </summary>
		/// <returns></returns>
		public int RetrieveAllChonseM(int topNum)
		{
			QueryObject qo = new QueryObject(this);
			qo.Top = topNum;
			qo.AddWhere(ChoseAttr.ChoseType,1);
			qo.addOrderByRandom();
			return qo.DoQuery();
		}
		#endregion
		 
		   
		/// <summary>
		/// ѡ����
		/// </summary>
		public Choses()
		{
		}
		/// <summary>
		/// ѡ����
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new Chose();
			}
		}
	}
}
