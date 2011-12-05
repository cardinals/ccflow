using System;
using System.Data;
using System.Collections;
using BP.DA;
using BP.En;
namespace BP.Sys
{
    public enum FrmFrom
    {
        Flow,
        Node,
        Dtl
    }
	/// <summary>
	/// ӳ�����
	/// </summary>
    public class MapDataAttr : EntityNoNameAttr
    {
        public const string PTable = "PTable";
        public const string Dtls = "Dtls";
        public const string EnPK = "EnPK";
        public const string SearchKeys = "SearchKeys";
        //public const string CellsFrom = "CellsFrom";
        public const string FrmW = "FrmW";
        public const string FrmH = "FrmH";
        /// <summary>
        /// ��Դ
        /// </summary>
        public const string FrmFrom = "FrmFrom";
        /// <summary>
        /// DBURL
        /// </summary>
        public const string DBURL = "DBURL";

        /// <summary>
        /// �����
        /// </summary>
        public const string Designer = "Designer";
        /// <summary>
        /// ����ߵ�λ
        /// </summary>
        public const string DesignerUnit = "DesignerUnit";
        /// <summary>
        /// �������ϵ��ʽ
        /// </summary>
        public const string DesignerContext = "DesignerContext";
        /// <summary>
        /// �����
        /// </summary>
        public const string FK_FrmSort = "FK_FrmSort";
        /// <summary>
        /// ��ʾ����
        /// </summary>
        public const string ShowAttrs = "ShowAttrs";
    }
	/// <summary>
	/// ӳ�����
	/// </summary>
    public class MapData : EntityNoName
    {
        #region ��������
        private FrmLines _HisFrmLines = null;
        public FrmLines FrmLines
        {
            get
            {
                if (_HisFrmLines == null)
                    _HisFrmLines = new FrmLines(this.No);
                return _HisFrmLines;
            }
        }
        private FrmLabs _FrmLabs = null;
        public FrmLabs FrmLabs
        {
            get
            {
                if (_FrmLabs == null)
                    _FrmLabs = new FrmLabs(this.No);
                return _FrmLabs;
            }
        }
        private FrmImgs _FrmImgs = null;
        public FrmImgs FrmImgs
        {
            get
            {
                if (_FrmImgs == null)
                    _FrmImgs = new FrmImgs(this.No);
                return _FrmImgs;
            }
        }
        private FrmAttachments _FrmAttachments = null;
        public FrmAttachments FrmAttachments
        {
            get
            {
                if (_FrmAttachments == null)
                    _FrmAttachments = new FrmAttachments(this.No);
                return _FrmAttachments;
            }
        }

        private FrmImgAths _FrmImgAths = null;
        public FrmImgAths FrmImgAths
        {
            get
            {
                if (_FrmImgAths == null)
                    _FrmImgAths = new FrmImgAths(this.No);
                return _FrmImgAths;
            }
        }
        private FrmRBs _FrmRBs = null;
        public FrmRBs FrmRBs
        {
            get
            {
                if (_FrmRBs == null)
                    _FrmRBs = new FrmRBs(this.No);
                return _FrmRBs;
            }
        }
        #endregion
        public static Boolean IsEditDtlModel
        {
            get
            {
                string s = BP.Web.WebUser.GetSessionByKey("IsEditDtlModel", "0");
                if (s == "0")
                    return false;
                else
                    return true;
            }
            set
            {
                BP.Web.WebUser.SetSessionByKey("IsEditDtlModel", "1");
            }
        }

        #region ����
        public string PTable
        {
            get
            {
                string s = this.GetValStrByKey(MapDataAttr.PTable);
                if (s == "" || s == null)
                    return this.No;
                return s;
            }
            set
            {
                this.SetValByKey(MapDataAttr.PTable, value);
            }
        }
        public DBUrlType HisDBUrl
        {
            get
            {
                return (DBUrlType)this.GetValIntByKey(MapDataAttr.DBURL);
            }
        }
        public string Dtls
        {
            get
            {
                return this.GetValStrByKey(MapDataAttr.Dtls);
            }
            set
            {
                this.SetValByKey(MapDataAttr.Dtls, value);
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        public string EnPK
        {
            get
            {
                return this.GetValStrByKey(MapDataAttr.EnPK);
            }
            set
            {
                this.SetValByKey(MapDataAttr.EnPK, value);
            }
        }
        public string SearchKeys
        {
            get
            {
                return this.GetValStrByKey(MapDataAttr.SearchKeys);
            }
            set
            {
                this.SetValByKey(MapDataAttr.SearchKeys, value);
            }
        }
        /// <summary>
        /// ��ʾ����
        /// </summary>
        public string ShowAttrs
        {
            get
            {
                return this.GetValStrByKey(MapDataAttr.ShowAttrs);
            }
            set
            {
                this.SetValByKey(MapDataAttr.ShowAttrs, value);
            }
        }
        public float FrmW
        {
            get
            {
                return this.GetValFloatByKey(MapDataAttr.FrmW);
            }
            set
            {
                this.SetValByKey(MapDataAttr.FrmW, value);
            }
        }
        public float FrmH
        {
            get
            {
                return this.GetValFloatByKey(MapDataAttr.FrmH);
            }
            set
            {
                this.SetValByKey(MapDataAttr.FrmH, value);
            }
        }
        #endregion

        #region ���췽��
        //public MapAttrs GenerHisTableCells
        //{
        //    get
        //    {
        //        //if ( this.CellsFrom == null)
        //        //    return null;
        //        MapAttrs mapAttrs = new MapAttrs(this.No+"T");
        //        if (mapAttrs.Count == 0)
        //        {
        //            for (int i = 1; i < 4; i++)
        //            {
        //                MapAttr attr = new MapAttr();
        //                attr.FK_MapData = this.No + "T";
        //                attr.KeyOfEn = "F" + i.ToString();
        //                attr.Name = "Lab" + i;
        //                attr.MyDataType = BP.DA.DataType.AppString;
        //                attr.UIContralType = UIContralType.TB;
        //                attr.LGType = FieldTypeS.Normal;
        //                attr.UIVisible = true;
        //                attr.UIIsEnable = true;
        //                attr.UIWidth = 30;
        //                attr.UIIsLine = true;
        //                attr.MinLen = 0;
        //                attr.MaxLen = 600;
        //                attr.IDX = i;
        //                attr.Insert();
        //            }
        //            mapAttrs = new MapAttrs(this.No + "T");
        //        }
        //        return mapAttrs;
        //    }
        //}
        public Map GenerHisMap()
        {
            MapAttrs mapAttrs = new MapAttrs(this.No);
            Map map = new Map(this.PTable);

            DBUrl u = new DBUrl(this.HisDBUrl);
            map.EnDBUrl = u;

            map.EnDesc = this.Name;
            map.EnType = EnType.App;
            map.DepositaryOfEntity = Depositary.None;
            map.DepositaryOfMap = Depositary.Application;

            Attrs attrs = new Attrs();
            foreach (MapAttr mapAttr in mapAttrs)
                map.AddAttr(mapAttr.HisAttr);

            if (this.SearchKeys.Contains("@") == true)
            {
                string[] strs = this.SearchKeys.Split('@');
                foreach (string s in strs)
                {
                    if (s == "" || s == null)
                        continue;
                    try
                    {
                        map.AddSearchAttr(s);
                    }
                    catch
                    {
                    }
                }
            }

            // ������ϸ��
            MapDtls dtls = new MapDtls(this.No);
            foreach (MapDtl dtl in dtls)
            {
                BP.Sys.GEDtls dtls1 = new GEDtls(dtl.No);
                map.AddDtl(dtls1, "RefPK");
            }
            return map;
        }
        private GEEntity _HisEn = null;
        public GEEntity HisGEEn
        {
            get
            {
                if (this._HisEn == null)
                    _HisEn = new GEEntity(this.No);
                return _HisEn;
            }
        }
        public static Map GenerHisMap(string no)
        {
            if (SystemConfig.IsDebug)
            {
                MapData md = new MapData();
                md.No = no;
                md.Retrieve();

                return md.GenerHisMap();
            }
            else
            {
                Map map = BP.DA.Cash.GetMap(no);
                if (map == null)
                {
                    MapData md = new MapData();
                    md.No = no;
                    md.Retrieve();

                    map = md.GenerHisMap();
                    BP.DA.Cash.SetMap(no, map);
                }
                return map;
            }
        }
        /// <summary>
        /// ӳ�����
        /// </summary>
        public MapData()
        {
        }
        /// <summary>
        /// ӳ�����
        /// </summary>
        /// <param name="no"></param>
        public MapData(string no):base(no)
        {
        }
        /// <summary>
        /// EnMap
        /// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;
                Map map = new Map("Sys_MapData");
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = "ӳ�����";
                map.EnType = EnType.Sys;
                map.CodeStruct = "4";

                map.AddTBStringPK(MapDataAttr.No, null, "���", true, false, 1, 20, 20);
                map.AddTBString(MapDataAttr.Name, null, "����", true, false, 0, 500, 20);
                map.AddTBString(MapDataAttr.EnPK, null, "ʵ������", true, false, 0, 10, 20);
                map.AddTBString(MapDataAttr.SearchKeys, null, "��ѯ��", true, false, 0, 500, 20);
                map.AddTBString(MapDataAttr.PTable, null, "�����", true, false, 0, 500, 20);
                map.AddTBString(MapDataAttr.Dtls, null, "��ϸ��", true, false, 0, 500, 20);

                map.AddTBFloat(MapDataAttr.FrmW, 900, "FrmW", true, true);
                map.AddTBFloat(MapDataAttr.FrmH, 1200, "FrmH", true, true);
                map.AddTBInt(MapDataAttr.DBURL, 0, "DBURL", true, false);

                map.AddTBString(MapDataAttr.Designer, null, "�����", true, false, 0, 500, 20);
                map.AddTBString(MapDataAttr.DesignerUnit, null, "��λ", true, false, 0, 500, 20);
                map.AddTBString(MapDataAttr.DesignerContext, null, "��ϵ��ʽ", true, false, 0, 500, 20);

                // ����Ϊ������ֶΡ�
                map.AddTBString(MapDataAttr.FK_FrmSort, null, "�����", true, false, 0, 500, 20);
                map.AddTBString(MapDataAttr.ShowAttrs, null, "��ʾ����", true, false, 0, 3800, 20);

                // map.AddTBInt(MapDataAttr.FrmFrom, 0, "��Դ", true, true);

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion

        public static void ImpMapData(string fk_mapdata, DataSet ds)
        {
            MapData mdOld = new MapData();
            mdOld.No = fk_mapdata;
            mdOld.Delete();

            // ���dataset��map.
            string oldMapID = "";
            DataTable dtMap = ds.Tables["Sys_MapData"];
            foreach (DataRow dr in dtMap.Rows)
            {
                if (dr["No"].ToString().Contains("Dtl"))
                    continue;
                oldMapID = dr["No"].ToString();
            }

            string timeKey = DateTime.Now.ToString("yyMMddhhmm");
            foreach (DataTable dt in ds.Tables)
            {
                int idx = 0;
                switch (dt.TableName)
                {
                    case "Sys_MapDtl":
                        foreach (DataRow dr in dt.Rows)
                        {
                            MapDtl dtl = new MapDtl();
                            foreach (DataColumn dc in dt.Columns)
                            {
                                string val = dr[dc.ColumnName] as string;
                                if (val == null)
                                    continue;
                                dtl.SetValByKey(dc.ColumnName, val.ToString().Replace(oldMapID, fk_mapdata));
                            }
                            dtl.Insert();
                        }
                        break;
                    case "Sys_MapData":
                        foreach (DataRow dr in dt.Rows)
                        {
                            MapData md = new MapData();
                            foreach (DataColumn dc in dt.Columns)
                            {
                                string val = dr[dc.ColumnName] as string;
                                if (val == null)
                                    continue;
                                md.SetValByKey(dc.ColumnName, val.ToString().Replace(oldMapID, fk_mapdata));
                                //md.SetValByKey(dc.ColumnName, val);
                            }
                            md.Insert();
                        }
                        break;
                    case "Sys_FrmBtn":
                        foreach (DataRow dr in dt.Rows)
                        {
                            idx++;
                            FrmBtn en = new FrmBtn();
                            foreach (DataColumn dc in dt.Columns)
                            {
                                string val = dr[dc.ColumnName] as string;
                                if (val == null)
                                    continue;
                                en.SetValByKey(dc.ColumnName, val.ToString().Replace(oldMapID, fk_mapdata));
                            }

                            en.MyPK = "Btn" + timeKey + "_" + idx;
                            en.Insert();
                        }
                        break;
                    case "Sys_FrmLine":
                        foreach (DataRow dr in dt.Rows)
                        {
                            idx++;
                            FrmLine en = new FrmLine();
                            foreach (DataColumn dc in dt.Columns)
                            {
                                string val = dr[dc.ColumnName] as string;
                                if (val == null)
                                    continue;

                                en.SetValByKey(dc.ColumnName, val.ToString().Replace(oldMapID, fk_mapdata));
                            }
                            //   en.FK_MapData = fk_mapdata;
                            en.MyPK = "LE" + timeKey + "_" + idx;
                            en.Insert();
                        }
                        break;
                    case "Sys_FrmLab":
                        foreach (DataRow dr in dt.Rows)
                        {
                            idx++;
                            FrmLab en = new FrmLab();
                            foreach (DataColumn dc in dt.Columns)
                            {
                                string val = dr[dc.ColumnName] as string;
                                if (val == null)
                                    continue;
                                en.SetValByKey(dc.ColumnName, val.ToString().Replace(oldMapID, fk_mapdata));
                            }
                            //  en.FK_MapData = fk_mapdata;
                            en.MyPK = "LB" + timeKey + "_" + idx;
                            en.Insert();
                        }
                        break;
                    case "Sys_FrmLink":
                        foreach (DataRow dr in dt.Rows)
                        {
                            idx++;
                            FrmLink en = new FrmLink();
                            foreach (DataColumn dc in dt.Columns)
                            {
                                string val = dr[dc.ColumnName] as string;
                                if (val == null)
                                    continue;
                                en.SetValByKey(dc.ColumnName, val.ToString().Replace(oldMapID, fk_mapdata));
                            }
                            //en.FK_MapData = fk_mapdata;
                            en.MyPK = "LK" + timeKey + "_" + idx;
                            en.Insert();
                        }
                        break;
                    case "Sys_FrmImg":
                        foreach (DataRow dr in dt.Rows)
                        {
                            idx++;
                            FrmImg en = new FrmImg();
                            foreach (DataColumn dc in dt.Columns)
                            {
                                string val = dr[dc.ColumnName] as string;
                                if (val == null)
                                    continue;
                                en.SetValByKey(dc.ColumnName, val.ToString().Replace(oldMapID, fk_mapdata));
                            }
                            //en.FK_MapData = fk_mapdata;
                            en.MyPK = "Img" + timeKey + "_" + idx;
                            en.Insert();
                        }
                        break;
                    case "Sys_FrmImgAth":
                        foreach (DataRow dr in dt.Rows)
                        {
                            idx++;
                            FrmImgAth en = new FrmImgAth();
                            foreach (DataColumn dc in dt.Columns)
                            {
                                string val = dr[dc.ColumnName] as string;
                                if (val == null)
                                    continue;
                                en.SetValByKey(dc.ColumnName, val.ToString().Replace(oldMapID, fk_mapdata));
                            }
                            // en.FK_MapData = fk_mapdata;
                            en.MyPK = "ImgA" + timeKey + "_" + idx;
                            en.Insert();
                        }
                        break;
                    case "Sys_FrmRB":
                        foreach (DataRow dr in dt.Rows)
                        {
                            idx++;
                            FrmRB en = new FrmRB();
                            foreach (DataColumn dc in dt.Columns)
                            {
                                string val = dr[dc.ColumnName] as string;
                                if (val == null)
                                    continue;
                                en.SetValByKey(dc.ColumnName, val.ToString().Replace(oldMapID, fk_mapdata));
                            }
                            //en.FK_MapData = fk_mapdata;
                            try
                            {
                                en.Save();
                            }
                            catch
                            {

                            }
                        }
                        break;
                    case "Sys_FrmAttachment":
                        foreach (DataRow dr in dt.Rows)
                        {
                            idx++;
                            FrmAttachment en = new FrmAttachment();
                            foreach (DataColumn dc in dt.Columns)
                            {
                                string val = dr[dc.ColumnName] as string;
                                if (val == null)
                                    continue;
                                en.SetValByKey(dc.ColumnName, val.ToString().Replace(oldMapID, fk_mapdata));
                            }
                            // en.FK_MapData = fk_mapdata;
                            en.MyPK = "Ath" + timeKey + "_" + idx;
                            en.Insert();
                        }
                        break;
                    case "Sys_MapM2M":
                        foreach (DataRow dr in dt.Rows)
                        {
                            idx++;
                            MapM2M en = new MapM2M();
                            foreach (DataColumn dc in dt.Columns)
                            {
                                string val = dr[dc.ColumnName] as string;
                                if (val == null)
                                    continue;
                                en.SetValByKey(dc.ColumnName, val.ToString().Replace(oldMapID, fk_mapdata));
                            }
                            // en.FK_MapData = fk_mapdata;
                            en.No = "D" + timeKey + "_" + idx;
                            en.Insert();
                        }
                        break;
                    case "Sys_MapFrame":
                        foreach (DataRow dr in dt.Rows)
                        {
                            idx++;
                            MapFrame en = new MapFrame();
                            foreach (DataColumn dc in dt.Columns)
                            {
                                string val = dr[dc.ColumnName] as string;
                                if (val == null)
                                    continue;
                                en.SetValByKey(dc.ColumnName, val.ToString().Replace(oldMapID, fk_mapdata));
                            }
                            // en.FK_MapData = fk_mapdata;
                            en.No = "Fra" + timeKey + "_" + idx;
                            en.Insert();
                        }
                        break;
                    case "Sys_MapExt":
                        foreach (DataRow dr in dt.Rows)
                        {
                            idx++;
                            MapExt en = new MapExt();
                            foreach (DataColumn dc in dt.Columns)
                            {
                                string val = dr[dc.ColumnName] as string;
                                if (val == null)
                                    continue;
                                en.SetValByKey(dc.ColumnName, val.ToString().Replace(oldMapID, fk_mapdata));
                            }
                            //en.FK_MapData = fk_mapdata;
                            en.MyPK = "Ext" + timeKey + "_" + idx;
                            en.Insert();
                        }
                        break;
                    case "Sys_MapAttr":
                        foreach (DataRow dr in dt.Rows)
                        {
                            idx++;
                            MapAttr en = new MapAttr();
                            foreach (DataColumn dc in dt.Columns)
                            {
                                string val = dr[dc.ColumnName] as string;
                                if (val == null)
                                    continue;
                                en.SetValByKey(dc.ColumnName, val.ToString().Replace(oldMapID, fk_mapdata));
                            }
                            //  en.FK_MapData = fk_mapdata;
                            en.Insert();
                        }
                        break;
                    case "Sys_GroupField":
                        foreach (DataRow dr in dt.Rows)
                        {
                            idx++;
                            GroupField en = new GroupField();
                            foreach (DataColumn dc in dt.Columns)
                            {
                                string val = dr[dc.ColumnName] as string;
                                if (val == null)
                                    continue;
                                en.SetValByKey(dc.ColumnName, val.ToString().Replace(oldMapID, fk_mapdata));
                            }
                            en.OID = 0;
                            en.Insert();
                        }
                        break;
                    default:
                        break;
                }
            }
        }
        protected override bool beforeDelete()
        {
            MapAttrs attrs = new MapAttrs();
            string sql = "";
            attrs.Delete(MapAttrAttr.FK_MapData, this.No);
            try
            {
                BP.DA.DBAccess.RunSQL("DROP TABLE " + this.PTable);
            }
            catch
            {
            }

            // Sys_MapDtl.
            sql = "SELECT * FROM Sys_MapDtl WHERE FK_MapData ='" + this.No + "'";
            DataTable Sys_MapDtl = DBAccess.RunSQLReturnTable(sql);
            string ids = "'" + this.No + "'";
            foreach (DataRow dr in Sys_MapDtl.Rows)
            {
                ids += ",'" + dr["No"] + "'";
            }

            string where = " FK_MapData IN (" + ids + ")";
            sql += "@DELETE Sys_FrmLine WHERE " + where;
            sql += "@DELETE Sys_FrmBtn WHERE " + where;
            sql += "@DELETE Sys_FrmLab WHERE " + where;
            sql += "@DELETE Sys_FrmLink WHERE " + where;
            sql += "@DELETE Sys_FrmImg WHERE " + where;
            sql += "@DELETE Sys_FrmImgAth WHERE " + where;
            sql += "@DELETE Sys_FrmRB WHERE " + where;
            sql += "@DELETE Sys_FrmAttachment WHERE " + where;
            sql += "@DELETE Sys_MapM2M WHERE " + where;
            sql += "@DELETE Sys_MapFrame WHERE " + where;
            sql += "@DELETE Sys_MapExt WHERE " + where;
            sql += "@DELETE Sys_MapAttr WHERE " + where;
            sql += "@DELETE Sys_GroupField WHERE EnName IN (" + ids + ")";
            sql += "@DELETE Sys_MapData WHERE No IN (" + ids + ")";
            DBAccess.RunSQLs(sql);
            return base.beforeDelete();
        }
        public System.Data.DataSet GenerHisDataSet()
        {
            DataSet ds = new DataSet();
            string sql = "";

            // Sys_MapDtl.
            sql = "SELECT * FROM Sys_MapDtl WHERE FK_MapData ='" + this.No + "'";
            DataTable Sys_MapDtl = DBAccess.RunSQLReturnTable(sql);
            Sys_MapDtl.TableName = "Sys_MapDtl";
            ds.Tables.Add(Sys_MapDtl);
            string ids = "'" + this.No + "'";
            foreach (DataRow dr in Sys_MapDtl.Rows)
            {
                ids += ",'" + dr["No"] + "'";
            }
            string where = " FK_MapData IN (" + ids + ")";

            // Sys_MapData.
            sql = "SELECT * FROM Sys_MapData WHERE No IN (" + ids + ")";
            DataTable Sys_MapData = DBAccess.RunSQLReturnTable(sql);
            Sys_MapData.TableName = "Sys_MapData";
            ds.Tables.Add(Sys_MapData);

            // line.
            sql = "SELECT * FROM Sys_FrmLine WHERE " + where;
            DataTable dtLine = DBAccess.RunSQLReturnTable(sql);
            dtLine.TableName = "Sys_FrmLine";
            ds.Tables.Add(dtLine);

            // link.
            sql = "SELECT * FROM Sys_FrmLink WHERE " + where;
            DataTable dtLink = DBAccess.RunSQLReturnTable(sql);
            dtLink.TableName = "Sys_FrmLink";
            ds.Tables.Add(dtLink);

            // btn.
            sql = "SELECT * FROM Sys_FrmBtn WHERE " + where;
            DataTable dtBtn = DBAccess.RunSQLReturnTable(sql);
            dtBtn.TableName = "Sys_FrmBtn";
            ds.Tables.Add(dtBtn);

            // Sys_FrmImg.
            sql = "SELECT * FROM Sys_FrmImg WHERE " + where;
            DataTable dtFrmImg = DBAccess.RunSQLReturnTable(sql);
            dtFrmImg.TableName = "Sys_FrmImg";
            ds.Tables.Add(dtFrmImg);

            // Sys_FrmLab.
            sql = "SELECT * FROM Sys_FrmLab WHERE " + where;
            DataTable Sys_FrmLab = DBAccess.RunSQLReturnTable(sql);
            Sys_FrmLab.TableName = "Sys_FrmLab";
            ds.Tables.Add(Sys_FrmLab);

            // Sys_FrmLab.
            sql = "SELECT * FROM Sys_FrmRB WHERE " + where;
            DataTable Sys_FrmRB = DBAccess.RunSQLReturnTable(sql);
            Sys_FrmRB.TableName = "Sys_FrmRB";
            ds.Tables.Add(Sys_FrmRB);

            // Sys_MapAttr.
            sql = "SELECT * FROM Sys_MapAttr WHERE " + where + " AND KeyOfEn NOT IN('WFState','WFLog')";
            DataTable Sys_MapAttr = DBAccess.RunSQLReturnTable(sql);
            Sys_MapAttr.TableName = "Sys_MapAttr";
            ds.Tables.Add(Sys_MapAttr);

            // Sys_MapM2M.
            sql = "SELECT * FROM Sys_MapM2M WHERE " + where;
            DataTable Sys_MapM2M = DBAccess.RunSQLReturnTable(sql);
            Sys_MapM2M.TableName = "Sys_MapM2M";
            ds.Tables.Add(Sys_MapM2M);

            // Sys_FrmAttachment.
            sql = "SELECT * FROM Sys_FrmAttachment WHERE " + where;
            DataTable Sys_FrmAttachment = DBAccess.RunSQLReturnTable(sql);
            Sys_FrmAttachment.TableName = "Sys_FrmAttachment";
            ds.Tables.Add(Sys_FrmAttachment);

            // Sys_FrmImgAth.
            sql = "SELECT * FROM Sys_FrmImgAth WHERE " + where;
            DataTable Sys_FrmImgAth = DBAccess.RunSQLReturnTable(sql);
            Sys_FrmImgAth.TableName = "Sys_FrmImgAth";
            ds.Tables.Add(Sys_FrmImgAth);

            // Sys_MapExt.
            sql = "SELECT * FROM Sys_MapExt WHERE " + where;
            DataTable Sys_MapExt = DBAccess.RunSQLReturnTable(sql);
            Sys_MapExt.TableName = "Sys_MapExt";
            ds.Tables.Add(Sys_MapExt);

            // Sys_GroupField.
            sql = "SELECT * FROM Sys_GroupField WHERE  EnName IN (" + ids + ")";
            DataTable Sys_GroupField = DBAccess.RunSQLReturnTable(sql);
            Sys_GroupField.TableName = "Sys_GroupField";
            ds.Tables.Add(Sys_GroupField);
            return ds;
        }
        /// <summary>
        /// �����Զ��ģ�����
        /// </summary>
        /// <param name="pk"></param>
        /// <param name="attrs"></param>
        /// <param name="attr"></param>
        /// <param name="tbPer"></param>
        /// <returns></returns>
        public static string GenerAutoFull(string pk, MapAttrs attrs, MapAttr attr, string tbPer)
        {
            string left = "\n document.forms[0]." + tbPer + "_TB" + attr.KeyOfEn + "_" + pk + ".value = ";
            string right = attr.AutoFullDoc;
            foreach (MapAttr mattr in attrs)
            {
                right = right.Replace("@" + mattr.KeyOfEn, " parseFloat( document.forms[0]." + tbPer + "_TB_" + mattr.KeyOfEn + "_" + pk + ".value) ");
            }
            return " alert( document.forms[0]." + tbPer + "_TB" + attr.KeyOfEn + "_" + pk + ".value ) ; \t\n " + left + right;
        }
    }
	/// <summary>
	/// ӳ�����s
	/// </summary>
    public class MapDatas : EntitiesMyPK
	{		
		#region ����
        /// <summary>
        /// ӳ�����s
        /// </summary>
		public MapDatas()
		{
		}
		/// <summary>
		/// �õ����� Entity
		/// </summary>
		public override Entity GetNewEntity 
		{
			get
			{
				return new MapData();
			}
		}
		#endregion
	}
}
