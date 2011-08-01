using System;
using System.Collections;
using BP.DA;
using BP.En;
namespace BP.Sys
{
    /// <summary>
    /// ��չ
    /// </summary>
    public class MapExtAttr : EntityNoNameAttr
    {
        /// <summary>
        /// ����
        /// </summary>
        public const string FK_MapData = "FK_MapData";
        /// <summary>
        /// ExtType
        /// </summary>
        public const string ExtType = "ExtType";
        /// <summary>
        /// �������λ��
        /// </summary>
        public const string RowIdx = "RowIdx";
        /// <summary>
        /// GroupID
        /// </summary>
        public const string GroupID = "GroupID";
        /// <summary>
        /// �߶�
        /// </summary>
        public const string H = "H";
        /// <summary>
        /// ���
        /// </summary>
        public const string W = "W";
        /// <summary>
        /// �Ƿ��������Ӧ��С
        /// </summary>
        public const string IsAutoSize = "IsAutoSize";
        /// <summary>
        /// ���õ�����
        /// </summary>
        public const string AttrOfOper = "AttrOfOper";
        /// <summary>
        /// ���������
        /// </summary>
        public const string AttrsOfActive = "AttrsOfActive";
        /// <summary>
        /// ִ�з�ʽ
        /// </summary>
        public const string DoWay = "DoWay";
        /// <summary>
        /// Tag
        /// </summary>
        public const string Tag = "Tag";
        public const string Tag1 = "Tag1";
    }
    /// <summary>
    /// ��չ
    /// </summary>
    public class MapExt : EntityMyPK
    {
        #region ����
        public string ExtDesc
        {
            get
            {
                string dec = "";
                switch (this.ExtType)
                {
                    case MapExtXmlList.ActiveDDL:
                        dec += "������Ŀ��"+this.AttrOfOper+" ������Ŀ��"+this.AttrsOfActive;
                        break;
                    case MapExtXmlList.FullCtrl:
                        dec += "������Ŀ�� ������Ŀ��";
                        break;
                    case MapExtXmlList.InputCheck:
                        dec += "������Ŀ��"+this.AttrOfOper+" ������ݣ�"+this.Tag1;
                        break;
                    case MapExtXmlList.PopVal:
                        dec += "������Ŀ��" + this.AttrOfOper + " ������Url��" + this.Tag;
                        break;
                    default:
                        break;
                }
                return dec;
            }
        }
        /// <summary>
        /// �Ƿ�����Ӧ��С
        /// </summary>
        public bool IsAutoSize
        {
            get
            {
                return this.GetValBooleanByKey(MapExtAttr.IsAutoSize);
            }
            set
            {
                this.SetValByKey(MapExtAttr.IsAutoSize, value);
            }
        }
      
        public string ExtType
        {
            get
            {
                return this.GetValStrByKey(MapExtAttr.ExtType);
            }
            set
            {
                this.SetValByKey(MapExtAttr.ExtType, value);
            }
        }
        public int DoWay
        {
            get
            {
                return this.GetValIntByKey(MapExtAttr.DoWay);
            }
            set
            {
                this.SetValByKey(MapExtAttr.DoWay, value);
            }
        }
        /// <summary>
        /// ������attrs
        /// </summary>
        public string AttrOfOper
        {
            get
            {
                return this.GetValStrByKey(MapExtAttr.AttrOfOper);
            }
            set
            {
                this.SetValByKey(MapExtAttr.AttrOfOper, value);
            }
        }
        /// <summary>
        /// �����attrs
        /// </summary>
        public string AttrsOfActive
        {
            get
            {
              //  return this.GetValStrByKey(MapExtAttr.AttrsOfActive).Replace("~", "'");
                return this.GetValStrByKey(MapExtAttr.AttrsOfActive);
            }
            set
            {
                this.SetValByKey(MapExtAttr.AttrsOfActive, value);
            }
        }
        public string FK_MapData
        {
            get
            {
                return this.GetValStrByKey(MapExtAttr.FK_MapData);
            }
            set
            {
                this.SetValByKey(MapExtAttr.FK_MapData, value);
            }
        }
        /// <summary>
        /// Doc
        /// </summary>
        public string Doc
        {
            get
            {
                return this.GetValStrByKey("Doc").Replace("~","'");
            }
            set
            {
                this.SetValByKey("Doc", value);
            }
        }
        public string Tag
        {
            get
            {
                return this.GetValStrByKey("Tag").Replace("~", "'");
            }
            set
            {
                this.SetValByKey("Tag", value);
            }
        }
        public string Tag1
        {
            get
            {
                return this.GetValStrByKey("Tag1").Replace("~", "'");
            }
            set
            {
                this.SetValByKey("Tag1", value);
            }
        }
        public int H
        {
            get
            {
                return this.GetValIntByKey(MapExtAttr.H);
            }
            set
            {
                this.SetValByKey(MapExtAttr.H, value);
            }
        }
        public int W
        {
            get
            {
                return this.GetValIntByKey(MapExtAttr.W);
            }
            set
            {
                this.SetValByKey(MapExtAttr.W, value);
            }
        }
        #endregion

        #region ���췽��
        /// <summary>
        /// ��չ
        /// </summary>
        public MapExt()
        {
        }
        /// <summary>
        /// ��չ
        /// </summary>
        /// <param name="no"></param>
        public MapExt(string mypk)
        {
            try
            {
                this.MyPK = mypk;
                this.Retrieve();
            }
            catch
            {
                this.CheckPhysicsTable();
            }
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

                Map map = new Map("Sys_MapExt");
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = "��չ";
                map.EnType = EnType.Sys;

                map.AddMyPK();
                map.AddTBString(MapExtAttr.FK_MapData, null, "����", true, false, 0, 30, 20);
                map.AddTBString(MapExtAttr.ExtType, null, "����", true, false, 0, 30, 20);

                map.AddTBString(MapExtAttr.AttrOfOper, null, "������Attr", true, false, 0, 30, 20);
                map.AddTBString(MapExtAttr.AttrsOfActive, null, "������ֶ�", true, false, 0, 900, 20);

                map.AddTBInt(MapExtAttr.DoWay, 0, "ִ�з�ʽ", true, false);

                map.AddTBStringDoc();

                map.AddTBString(MapExtAttr.Tag, null, "Tag", true, false, 0, 900, 20);
                map.AddTBString(MapExtAttr.Tag1, null, "Tag1", true, false, 0, 900, 20);

                map.AddTBInt(MapExtAttr.H, 500, "�߶�", false, false);
                map.AddTBInt(MapExtAttr.W, 400, "���", false, false);

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
    /// <summary>
    /// ��չs
    /// </summary>
    public class MapExts : Entities
    {
        #region ����
        /// <summary>
        /// ��չs
        /// </summary>
        public MapExts()
        {
        }
        /// <summary>
        /// ��չs
        /// </summary>
        /// <param name="fk_mapdata">s</param>
        public MapExts(string fk_mapdata)
        {
            this.Retrieve(MapExtAttr.FK_MapData, fk_mapdata);
        }
        /// <summary>
        /// �õ����� Entity
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new MapExt();
            }
        }
        #endregion
    }
}
