using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.En.Base;

namespace BP.GTS
{
	
	/// <summary>
	/// �Ծ��Ķ������
	/// </summary>
	public class WorkRandomVSRCAttr  :WorkVSBaseAttr
	{
		#region ��������
		/// <summary>
		/// �Ķ���
		/// </summary>
		public const  string FK_RC="FK_RC";
		#endregion	
	}
	/// <summary>
	/// �Ծ��Ķ������ ��ժҪ˵��
	/// </summary>
	public class WorkRandomVSRC :WorkVSBase
	{
		#region ��������
		/// <summary>
		///�Ķ���
		/// </summary>
		public string FK_RC
		{
			get
			{
				return this.GetValStringByKey(WorkRandomVSRCAttr.FK_RC);
			}
			set
			{
				SetValByKey(WorkRandomVSRCAttr.FK_RC,value);
			}
		}
		#endregion

		 

		#region ���캯��
		/// <summary>
		/// �Ķ���������
		/// </summary> 
		public WorkRandomVSRC()
		{
		}
		public WorkRandomVSRC(string Work,string Equestion)
		{
			this.FK_Work = Work;
			this.FK_RC = Equestion;
			try
			{
				this.Retrieve();
			}
			catch
			{
				this.Insert();
			}
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
				
				Map map = new Map("GTS_WorkRandomVSRC");
				map.EnDesc="�Ķ���������";	
				map.EnType=EnType.Dot2Dot;
		 
				map.AddDDLEntitiesPK(WorkRandomVSRCAttr.FK_Work,"0001","�Ծ�",new WorkFixDesigns(),false);
				map.AddDDLEntitiesPK(WorkRandomVSRCAttr.FK_RC,null,"�Ķ���",new RCs(),false);
				//map.AddTBInt(WorkRandomVSRCAttr.Cent,1,"��",true,false);
				//map.AddSearchAttr(EmpDutyAttr.FK_Emp);
				//map.AddSearchAttr(EmpDutyAttr.FK_Duty);

				this._enMap=map;
				return this._enMap;
			}
		}
		#endregion

		#region ���ػ��෽��

		protected override void afterInsert()
		{
			RC rc = new RC(this.FK_RC);
			RCDtls rcs = rc.HisRCDtls;
			foreach(RCDtl dtl in rcs)
			{
				WorkRandomVSRCDtl en = new WorkRandomVSRCDtl();
				en.Cent=0;

				QueryObject qo = new QueryObject(en);
				qo.AddWhere(WorkRandomVSRCDtlAttr.FK_Work, this.FK_Work);
				qo.addAnd();
				qo.AddWhere(WorkRandomVSRCDtlAttr.FK_RCDtl,dtl.OID);
				if (qo.DoQuery()==1)
				{
					continue;
				}
				else
				{
					en.FK_Work=this.FK_Work;
					en.FK_RCDtl= dtl.OID;
					en.FK_RC=this.FK_RC;
					en.Insert();
				}
			}

			base.afterInsert ();

		}
		protected override bool beforeDelete()
		{
			DBAccess.RunSQL("delete GTS_WorkRandomVSRCDtl where fk_Work='"+this.FK_Work+"' and fk_rc='"+this.FK_RC+"'");
			return base.beforeDelete ();
		}
		#endregion 
	
	}
	/// <summary>
	/// �Ծ��Ķ������ 
	/// </summary>
	public class WorkRandomVSRCs : WorkVSBases
	{
		#region ����
		/// <summary>
		/// �Ծ��Ķ������
		/// </summary>
		public WorkRandomVSRCs(){}

		/// <summary>
		///  �Ծ��Ķ������
		/// </summary>
		/// <param name="fk_Work"></param>
		public WorkRandomVSRCs(string fk_Work)
		{
			QueryObject qo =new QueryObject(this);
			qo.AddWhere( WorkRandomVSRCAttr.FK_Work, fk_Work);
			qo.DoQuery();
		}
		#endregion

		#region ����
		/// <summary>
		/// �õ����� Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new WorkRandomVSRC();
			}
		}	
		#endregion 

		#region ��ѯ����
		public RCs GetRCs(string fk_Work)
		{

			RCs rcs = new RCs();
			QueryObject qo =new QueryObject(rcs);
			qo.AddWhereInSQL( RCAttr.No , "SELECT FK_RC FROM GTS_WorkRandomVSRCs  WHERE FK_Work='"+fk_Work+"'");
			qo.DoQuery();
			return rcs;
		}
		#endregion
	}
	
}
