using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.Port;

namespace BP.WF
{
	/// <summary>
	/// �����λ
	/// </summary>
	public class RptStationAttr  
	{
		#region ��������
		/// <summary>
		/// ����
		/// </summary>
		public const  string FK_Rpt="FK_Rpt";
		/// <summary>
		/// ������λ
		/// </summary>
		public const  string FK_Station="FK_Station";		 
		#endregion	
	}
	/// <summary>
    /// �����λ ��ժҪ˵����
	/// </summary>
    public class RptStation : Entity
    {

        #region ��������
        /// <summary>
        /// ����
        /// </summary>
        public string FK_Rpt
        {
            get
            {
                return this.GetValStringByKey(RptStationAttr.FK_Rpt);
            }
            set
            {
                SetValByKey(RptStationAttr.FK_Rpt, value);
            }
        }
        /// <summary>
        ///������λ
        /// </summary>
        public string FK_Station
        {
            get
            {
                return this.GetValStringByKey(RptStationAttr.FK_Station);
            }
            set
            {
                SetValByKey(RptStationAttr.FK_Station, value);
            }
        }
        #endregion

        #region ��չ����

        #endregion

        #region ���캯��
        /// <summary>
        /// ���������λ
        /// </summary> 
        public RptStation() { }
        /// <summary>
        /// ������Ա������λ��Ӧ
        /// </summary>
        /// <param name="_empoid">����</param>
        /// <param name="wsNo">������λ���</param> 	
        public RptStation(string _empoid, string wsNo)
        {
            this.FK_Rpt = _empoid;
            this.FK_Station = wsNo;
            if (this.Retrieve() == 0)
                this.Insert();
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

                Map map = new Map("WF_RptStation");
                map.EnDesc = "�����λ";
                map.EnType = EnType.Dot2Dot;

                map.AddDDLEntitiesPK(RptStationAttr.FK_Rpt, null, "����Ա", new WFRpts(), true);
                map.AddDDLEntitiesPK(RptStationAttr.FK_Station, null, "������λ", new Stations(), true);

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
	/// <summary>
    /// �����λ 
	/// </summary>
    public class RptStations : Entities
    {
        #region ����
        /// <summary>
        /// ���������λ
        /// </summary>
        public RptStations()
        {
        }
        /// <summary>
        /// �����λ
        /// </summary>
        public RptStations(string stationNo)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(RptStationAttr.FK_Station, stationNo);
            qo.DoQuery();
        }
        /// <summary>
        /// �����λ
        /// </summary>
        /// <param name="empId">RptID</param>
        public RptStations(int empId)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(RptStationAttr.FK_Rpt, empId);
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
                return new RptStation();
            }
        }
        #endregion
    }
}
