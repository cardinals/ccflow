using System;
using System.Collections;
using BP.DA;
using BP.En;
namespace BP.Sys
{
    /// <summary>
    /// ��Ե�
    /// </summary>
    public class MapM2MAttr : EntityNoNameAttr
    {
        /// <summary>
        /// ����
        /// </summary>
        public const string FK_MapData = "FK_MapData";
        /// <summary>
        /// �������λ��
        /// </summary>
        public const string RowIdx = "RowIdx";
        /// <summary>
        /// GroupID
        /// </summary>
        public const string GroupID = "GroupID";
        public const string Height = "Height";
        public const string Width = "Width";
        /// <summary>
        /// �Ƿ��������Ӧ��С
        /// </summary>
        public const string IsAutoSize = "IsAutoSize";
        public const string DBOfObjs = "DBOfObjs";
        public const string DBOfGroups = "DBOfGroups";
        public const string IsDelete = "IsDelete";
        public const string IsInsert = "IsInsert";


        public const string W = "W";
        public const string H = "H";

        public const string X = "X";
        public const string Y = "Y";

        public const string Cols = "Cols";
    }
    /// <summary>
    /// ��Ե�
    /// </summary>
    public class MapM2M : EntityNoName
    {
        public GEEntity HisGEEntity
        {
            get
            {
                GEEntity en = new GEEntity(this.No);
                return en;
            }
        }

        #region ����
        /// <summary>
        /// �Ƿ�����Ӧ��С
        /// </summary>
        public bool IsAutoSize
        {
            get
            {
                return this.GetValBooleanByKey(MapM2MAttr.IsAutoSize);
            }
            set
            {
                this.SetValByKey(MapM2MAttr.IsAutoSize, value);
            }
        }
        public bool IsDelete
        {
            get
            {
                return this.GetValBooleanByKey(MapM2MAttr.IsDelete);
            }
            set
            {
                this.SetValByKey(MapM2MAttr.IsDelete, value);
            }
        }
        public bool IsInsert
        {
            get
            {
                return this.GetValBooleanByKey(MapM2MAttr.IsInsert);
            }
            set
            {
                this.SetValByKey(MapM2MAttr.IsInsert, value);
            }
        }
        public string DBOfObjs
        {
            get
            {
                string sql = this.GetValStrByKey(MapM2MAttr.DBOfObjs);
                //if (string.IsNullOrEmpty(sql))
                //{
                //    return "SELECT No,Name,FK_Dept FROM Port_Emp ";
                //}
                sql = sql.Replace("~", "'");
                return sql;
            }
            set
            {
                this.SetValByKey(MapM2MAttr.DBOfObjs, value);
            }
        }
        public string DBOfGroups
        {
            get
            {
                string sql = this.GetValStrByKey(MapM2MAttr.DBOfGroups);
                //if (string.IsNullOrEmpty(sql))
                //{
                //    return "SELECT No,Name Port_Dept ";
                //}
                sql = sql.Replace("~", "'");
                return sql;
            }
            set
            {
                this.SetValByKey(MapM2MAttr.DBOfGroups, value);
            }
        }

        public string DBOfObjsRun
        {
            get
            {
                string sql = this.GetValStrByKey(MapM2MAttr.DBOfObjs);
                sql = sql.Replace("~", "'");
                sql = sql.Replace("@WebUser.No", BP.Web.WebUser.No);
                sql = sql.Replace("@WebUser.Name", BP.Web.WebUser.Name);
                sql = sql.Replace("@WebUser.FK_Dept", BP.Web.WebUser.FK_Dept);
                return sql;
            }
            set
            {
                this.SetValByKey(MapM2MAttr.DBOfObjs, value);
            }
        }
        public string DBOfGroupsRun
        {
            get
            {
                string sql = this.GetValStrByKey(MapM2MAttr.DBOfGroups);
                sql = sql.Replace("~", "'");
                sql = sql.Replace("@WebUser.No", BP.Web.WebUser.No);
                sql = sql.Replace("@WebUser.Name", BP.Web.WebUser.Name);
                sql = sql.Replace("@WebUser.FK_Dept", BP.Web.WebUser.FK_Dept);
                return sql;
            }
            set
            {
                this.SetValByKey(MapM2MAttr.DBOfGroups, value);
            }
        }

        public bool IsUse = false;
        public string FK_MapData
        {
            get
            {
                return this.GetValStrByKey(MapM2MAttr.FK_MapData);
            }
            set
            {
                this.SetValByKey(MapM2MAttr.FK_MapData, value);
            }
        }
        public int RowIdx
        {
            get
            {
                return this.GetValIntByKey(MapM2MAttr.RowIdx);
            }
            set
            {
                this.SetValByKey(MapM2MAttr.RowIdx, value);
            }
        }
        public int Cols
        {
            get
            {
                return this.GetValIntByKey(MapM2MAttr.Cols);
            }
            set
            {
                this.SetValByKey(MapM2MAttr.Cols, value);
            }
        }
        
        public int GroupID
        {
            get
            {
                return this.GetValIntByKey(MapM2MAttr.GroupID);
            }
            set
            {
                this.SetValByKey(MapM2MAttr.GroupID, value);
            }
        }
        public string Height
        {
            get
            {
                return this.GetValStringByKey(MapM2MAttr.Height);
            }
            set
            {
                this.SetValByKey(MapM2MAttr.Height, value);
            }
        }
        public string Width
        {
            get
            {
                return this.GetValStringByKey(MapM2MAttr.Width);
            }
            set
            {
                this.SetValByKey(MapM2MAttr.Width, value);
            }
        }
        public float X
        {
            get
            {
                return this.GetValFloatByKey(MapM2MAttr.X);
            }
            set
            {
                this.SetValByKey(MapM2MAttr.X, value);
            }
        }
        public float Y
        {
            get
            {
                return this.GetValFloatByKey(MapM2MAttr.Y);
            }
            set
            {
                this.SetValByKey(MapM2MAttr.Y, value);
            }
        }

        /// <summary>
        /// ��չ����
        /// </summary>
        public int FK_Node
        {
            get
            {
                return int.Parse(this.FK_MapData.Replace("ND", ""));
            }
        }
        #endregion

        #region ���췽��
        /// <summary>
        /// ��Ե�
        /// </summary>
        public MapM2M()
        {
        }
        /// <summary>
        /// ��Ե�
        /// </summary>
        /// <param name="no"></param>
        public MapM2M(string no)
        {
            this.No = no;
            this.Retrieve();
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
                Map map = new Map("Sys_MapM2M");
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = "��Ե�";
                map.EnType = EnType.Sys;

                map.AddTBStringPK(MapM2MAttr.No, null, "���", true, false, 1, 20, 20);
                map.AddTBString(MapM2MAttr.Name, null, "����", true, false, 1, 200, 20);
                map.AddTBString(MapM2MAttr.FK_MapData, null, "����", true, false, 0, 30, 20);

                map.AddTBString(MapM2MAttr.DBOfObjs, null, "DBOfObjs", true, false, 0, 4000, 20);
                map.AddTBString(MapM2MAttr.DBOfGroups, null, "DBOfGroups", true, false, 0, 4000, 20);

                map.AddTBString(MapM2MAttr.Height, "100%", "Height", true, false, 0, 10, 20);
                map.AddTBString(MapM2MAttr.Width, "100%", "Width", true, false, 0, 10, 20);

            

                map.AddBoolean(MapM2MAttr.IsAutoSize, true, "�Ƿ��Զ����ô�С", false, false);

                map.AddTBInt(MapM2MAttr.RowIdx, 99, "λ��", false, false);
                map.AddTBInt(MapM2MAttr.GroupID, 0, "����ID", false, false);

                map.AddTBInt(MapM2MAttr.Cols, 4, "��¼��������", false, false);

                map.AddBoolean(MapM2MAttr.IsDelete, true, "��ɾ����", false, false);
                map.AddBoolean(MapM2MAttr.IsInsert, true, "�ɲ����", false, false);


                map.AddTBFloat(FrmImgAttr.X, 5, "X", true, false);
                map.AddTBFloat(FrmImgAttr.Y, 5, "Y", false, false);


                //map.AddTBFloat(FrmImgAttr.H, 200, "H", true, false);
                //map.AddTBFloat(FrmImgAttr.W, 500, "W", false, false);

                this._enMap = map;
                return this._enMap;
            }
        }
        protected override bool beforeInsert()
        {
            if (this.DBOfObjs.Trim().Length <= 5)
            {
                this.DBOfGroups = "SELECT No,Name FROM Port_Dept";
                this.DBOfObjs = "SELECT No,Name,FK_Dept FROM Port_Emp";
            }
            return base.beforeInsert();
        }
        protected override void afterInsert()
        {
            base.afterInsert();
        }
        #endregion
    }
    /// <summary>
    /// ��Ե�s
    /// </summary>
    public class MapM2Ms : EntitiesNoName
    {
        #region ����
        /// <summary>
        /// ��Ե�s
        /// </summary>
        public MapM2Ms()
        {
        }
        /// <summary>
        /// ��Ե�s
        /// </summary>
        /// <param name="fk_mapdata">s</param>
        public MapM2Ms(string fk_mapdata)
        {
            this.Retrieve(MapM2MAttr.FK_MapData, fk_mapdata, MapM2MAttr.GroupID);
        }
        /// <summary>
        /// �õ����� Entity
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new MapM2M();
            }
        }
        #endregion
    }
}
