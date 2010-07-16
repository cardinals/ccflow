using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.Port;
//using BP.ZHZS.DS;


namespace BP.CN
{
	/// <summary>
	/// IP��ַ
	/// </summary>
    public class IPAttr : EntityNoNameAttr
    {
        #region ��������
        public const string FK_PQ = "FK_PQ";
        public const string FK_Area = "FK_Area";
        public const string Names = "Names";
        public const string S = "S";
        public const string E = "E";
        public const string MyPK = "MyPK";

        #endregion
    }
	/// <summary>
    /// IP��ַ
	/// </summary>
    public class IP : Entity
    {
        #region ��������
        public string MyPK
        {
            get
            {
                return this.GetValStrByKey(IPAttr.MyPK);
            }
            set
            {
                this.SetValByKey(IPAttr.MyPK, value);
            }
        }
        public string Name
        {
            get
            {
                return this.GetValStrByKey(CityAttr.Name);
            }
        }
        public string Names
        {
            get
            {
                return this.GetValStrByKey(CityAttr.Names);
            }
        }
        public string S
        {
            get
            {
                return this.GetValStrByKey(IPAttr.S);
            }
        }
        public string E
        {
            get
            {
                return this.GetValStrByKey(IPAttr.E);
            }
        }
        public string FK_Area
        {
            get
            {
                return this.GetValStrByKey(IPAttr.FK_Area);
            }
        }
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
        /// IP��ַ
        /// </summary>		
        public IP() { }
        //public IP(string ip)
        //{
        //    Int64 myip = this.ParseIP(ip);

        //    QueryObject qo = new QueryObject(this);
        //    qo.AddWhere(IPAttr.S, ">=", myip);
        //    qo.addAnd();
        //    qo.AddWhere(IPAttr.E, "<", myip);
        //    int i = qo.DoQuery();
        //    if (i == 0)
        //        throw new Exception("ϵͳû�и���IP[" + ip + "]��ȡ��ַ��");
        //}
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
                map.PhysicsTable = "CN_IP";
                map.AdjunctType = AdjunctType.AllType;
                map.DepositaryOfMap = Depositary.Application;
                map.DepositaryOfEntity = Depositary.None;
                map.IsAllowRepeatNo = false;
                map.IsCheckNoLength = false;
                map.EnDesc = "IP��ַ";
                map.EnType = EnType.App;
                map.CodeStruct = "4";
                #endregion

                #region �ֶ�
                map.AddMyPK();
                map.AddTBString(IPAttr.Name, null, "����", true, false, 0, 500, 200);
                map.AddTBInt(IPAttr.S, 0, "S", false, false);
                map.AddTBInt(IPAttr.E, 0, "e", false, false);
                map.AddDDLEntities(IPAttr.FK_Area, null, "Areas", new Areas(), false);
                map.AddSearchAttr(IPAttr.FK_Area);
                #endregion

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion

        public static Int64  ParseIPString2Int64(string ip)
        {
            string[] ipTemp2 = ip.Split('.');
            long ipTo = Convert.ToInt64(ipTemp2[0]) * 256 * 256 * 256 + Convert.ToInt64(ipTemp2[1]) * 256 * 256 + Convert.ToInt64(ipTemp2[2]) * 256 + Convert.ToInt64(ipTemp2[3]);
            return ipTo;
        }
        public static string GenerIPString2AreaNo(string ip)
        {
            if (ip == "127.0.0.1")
                return null;

            Int64 ipint = IP.ParseIPString2Int64(ip);
            string sql = "select   fk_area   from cn_ip   where    " + ipint.ToString() + "   between   s   and   e ";
            string s = DBAccess.RunSQLReturnString(sql);
            if (s == null)
                return null;

            return s;
            //Area a = new Area(s);
            //return a;
        }
        /// <summary>
        /// ����areaNo.
        /// </summary>
        public void GenerAreaNo()
        {
            string sql = "select * from cn_ip where name like '% %' and fk_area is null  ";
            DataTable dt = DBAccess.RunSQLReturnTable(sql);

            BP.CN.Citys its = new Citys();
            its.RetrieveAll();

            foreach (DataRow dr in dt.Rows)
            {
                string name = dr["Name"].ToString();
                string MyPK = dr["MyPK"].ToString();

                string fk_area = BP.CN.QXS.GenerQXSNoByName(name, null);
                if (fk_area == null)
                    DBAccess.RunSQL("UPDATE CN_IP SET fk_area='unName' where mypk='" + MyPK + "'");
                else
                    DBAccess.RunSQL("UPDATE CN_IP SET fk_area='" + fk_area + "' where mypk='" + MyPK + "'");
            }
        }

        //public static uint IPtoUint(string strip)//��ʽ��IP 
        //{
        //    uint ip1 = Convert.ToUInt32(strip.Split('.')[0]);//��ȡIP��ַ��0λ����תΪ�޷��ŵ����� 
        //    uint ip2 = Convert.ToUInt32(strip.Split('.')[1]);
        //    uint ip3 = Convert.ToUInt32(strip.Split('.')[2]);
        //    uint ip4 = Convert.ToUInt32(strip.Split('.')[3]);
        //    return ip1 * 256 * 256 * 256 + ip2 * 256 * 256 + ip3 * 256 + ip4;//��������10���ƽ�� 
        //}

        /** 
        ����256,����Ϊ��4������ 
        ��һ�����̳���256������Ϊ��3������ 
        ��һ�����̳���256������Ϊ��2������ 
        ��һ������Ϊ��1���� 
        **/
        //��10����������ʽת����127.0.0.1��ʽ��IP��ַ 
        public static string longToIP(long longIP)
        {
            long ip4 = longIP % 256;
            long ip3 = longIP / 256 % 256;
            long ip2 = longIP / 256 / 256 % 256;
            long ip1 = longIP / 256 / 256 / 256;
            return ip1.ToString() + "." + ip2.ToString() + "." + ip3.ToString() + "." + ip4.ToString();
        }
    }
	/// <summary>
	/// IP��ַ
	/// </summary>
    public class IPs : EntitiesNoName
    {
        #region  �õ����� Entity
        /// <summary>
        /// �õ����� Entity 
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new IP();
            }
        }
        #endregion

        #region ���췽��
        /// <summary>
        /// IP��ַs
        /// </summary>
        public IPs() { }
        /// <summary>
        /// IP��ַs
        /// </summary>
        /// <param name="sf">ʡ��</param>
        public IPs(string sf)
        {
            this.Retrieve(IPAttr.FK_Area, sf);
        }
        #endregion
    }
}
