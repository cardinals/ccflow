using System;
using System.Collections;
using BP.DA;
using BP.En;
namespace BP.Sys
{
    /// <summary>
    /// �༭����
    /// </summary>
    public enum EditType
    {
        /// <summary>
        /// �ɱ༭
        /// </summary>
        Edit,
        /// <summary>
        /// ����ɾ��
        /// </summary>
        UnDel,
        /// <summary>
        /// ֻ��
        /// </summary>
        Readonly
    }
    /// <summary>
    /// �Զ���䷽ʽ
    /// </summary>
    public enum AutoFullWay
    {
        /// <summary>
        /// ������
        /// </summary>
        Way0,
        /// <summary>
        /// ��ʽ1
        /// </summary>
        Way1_JS,
        /// <summary>
        /// sql ��ʽ��
        /// </summary>
        Way2_SQL,
        /// <summary>
        /// ���
        /// </summary>
        Way3_FK,
        /// <summary>
        /// ��ϸ
        /// </summary>
        Way4_Dtl
    }
    /// <summary>
    /// ʵ������
    /// </summary>
    public class MapAttrAttr : EntityOIDAttr
    {
        /// <summary>
        /// ʵ���ʶ
        /// </summary>
        public const string FK_MapData = "FK_MapData";
        /// <summary>
        /// �����
        /// </summary>
        public const string KeyOfEn = "KeyOfEn";
        /// <summary>
        /// ʵ������
        /// </summary>
        public const string Name = "Name";
        /// <summary>
        /// Ĭ��ֵ
        /// </summary>
        public const string DefVal = "DefVal";
        /// <summary>
        /// �ֶ�
        /// </summary>
        public const string Field = "Field";
        /// <summary>
        /// ��󳤶�
        /// </summary>
        public const string MaxLen = "MaxLen";
        /// <summary>
        /// ��С����
        /// </summary>
        public const string MinLen = "MinLen";
        /// <summary>
        /// �󶨵�ֵ
        /// </summary>
        public const string UIBindKey = "UIBindKey";
        /// <summary>
        /// �ռ�����
        /// </summary>
        public const string UIContralType = "UIContralType";
        /// <summary>
        /// ���
        /// </summary>
        public const string UIWidth = "UIWidth";
        /// <summary>
        /// �Ƿ�ֻ��
        /// </summary>
        public const string UIIsEnable = "UIIsEnable";
        /// <summary>
        /// �����ı��Key
        /// </summary>
        public const string UIRefKey = "UIRefKey";
        /// <summary>
        /// �����ı��Lab
        /// </summary>
        public const string UIRefKeyText = "UIRefKeyText";
        /// <summary>
        /// �Ƿ�ɼ���
        /// </summary>
        public const string UIVisible = "UIVisible";
        /// <summary>
        /// �Ƿ񵥶�����ʾ
        /// </summary>
        public const string UIIsLine = "UIIsLine";
        /// <summary>
        /// ���
        /// </summary>
        public const string IDX = "IDX";
        /// <summary>
        /// ��ʶ�������ʱ���ݣ�
        /// </summary>
        public const string Tag = "Tag";
        /// <summary>
        /// MyDataType
        /// </summary>
        public const string MyDataType = "MyDataType";
        /// <summary>
        /// �߼�����
        /// </summary>
        public const string LGType = "LGType";
        /// <summary>
        /// �༭����
        /// </summary>
        public const string EditType = "EditType";
        /// <summary>
        /// �Զ���д����
        /// </summary>
        public const string AutoFullDoc = "AutoFullDoc";
        /// <summary>
        /// �Զ���д��ʽ
        /// </summary>
        public const string AutoFullWay = "AutoFullWay";
        /// <summary>
        /// GroupID
        /// </summary>
        public const string GroupID = "GroupID";
    }
    /// <summary>
    /// ʵ������
    /// </summary>
    public class MapAttr : EntityOID
    {
        #region ����
        public EntitiesNoName HisEntitiesNoName
        {
            get
            {
                if (this.UIBindKey.Contains("."))
                {
                    EntitiesNoName ens = (EntitiesNoName)BP.DA.ClassFactory.GetEns(this.UIBindKey);
                    ens.RetrieveAll();
                    return ens;
                }

                GENoNames myens = new GENoNames(this.UIBindKey, this.Name);
                myens.RetrieveAll();
                return myens;
            }
        }
        public bool IsTableAttr
        {
            get
            {
                return DataType.IsNumStr( this.KeyOfEn.Replace("F", ""));
            }
        }
        public Attr HisAttr
        {
            get
            {
                Attr attr = new Attr();
                attr.Key = this.KeyOfEn;
                attr.Desc = this.Name;
                attr.DefaultVal = this.DefVal;
                attr.Field = this.Field;
                attr.MaxLength = this.MaxLen;
                attr.MinLength = this.MinLen;
                attr.UIBindKey = this.UIBindKey;

                attr.UIIsLine = this.UIIsLine;

                attr.UIHeight = 0;
                if (this.MaxLen > 3000)
                    attr.UIHeight = 10;

                attr.UIWidth = this.UIWidth;
                attr.MyDataType = this.MyDataType;
                attr.UIRefKeyValue = this.UIRefKey;
                attr.UIRefKeyText = this.UIRefKeyText;
                attr.UIVisible = this.UIVisible;
                if (this.IsPK)
                    attr.MyFieldType = FieldType.PK;

                switch (this.LGType)
                {
                    case FieldTypeS.Enum:
                        attr.UIContralType = UIContralType.DDL;
                        attr.MyFieldType = FieldType.Enum;
                        attr.UIDDLShowType = BP.Web.Controls.DDLShowType.SysEnum;
                        attr.UIIsReadonly = this.UIIsEnable;
                        break;
                    case FieldTypeS.FK:
                        attr.UIContralType = UIContralType.DDL;
                        attr.MyFieldType = FieldType.FK;
                        attr.UIDDLShowType = BP.Web.Controls.DDLShowType.Ens;
                        attr.UIRefKeyValue = "No";
                        attr.UIRefKeyText = "Name";
                        attr.UIIsReadonly = this.UIIsEnable;
                        break;
                    default:
                        attr.UIContralType = UIContralType.TB;
                        if (this.IsPK)
                            attr.MyFieldType = FieldType.PK;

                        attr.UIIsReadonly = !this.UIIsEnable;
                        switch (this.MyDataType)
                        {
                            case DataType.AppBoolean:
                                attr.UIContralType = UIContralType.CheckBok;
                                attr.UIIsReadonly = this.UIIsEnable;
                                break;
                            case DataType.AppDate:
                                if (this.Tag == "1")
                                    attr.DefaultVal = DataType.CurrentData;
                                break;
                            case DataType.AppDateTime:
                                if (this.Tag == "1")
                                    attr.DefaultVal = DataType.CurrentData;
                                break;
                            default:
                                break;
                        }
                        break;
                }

                attr.AutoFullWay = this.HisAutoFull;
                attr.AutoFullDoc = this.AutoFullDoc;

                //attr.MyFieldType = FieldType
                //attr.UIDDLShowType= BP.Web.Controls.DDLShowType.Self
                return attr;
            }
        }

        /// <summary>
        /// �Ƿ�����
        /// </summary>
        public bool IsPK
        {
            get
            {
                switch (this.KeyOfEn)
                {
                    case "OID":
                    case "No":
                    case "MyPK":
                        return true;
                    default:
                        return false;
                }
            }
        }
        public EditType HisEditType
        {
            get
            {
                return (EditType)this.GetValIntByKey(MapAttrAttr.EditType);
            }
            set
            {
                this.SetValByKey(MapAttrAttr.EditType, (int)value);
            }
        }
        public string FK_MapData
        {
            get
            {
                return this.GetValStrByKey(MapAttrAttr.FK_MapData);
            }
            set
            {
                this.SetValByKey(MapAttrAttr.FK_MapData, value);
            }
        }
        /// <summary>
        /// AutoFullWay
        /// </summary>
        public AutoFullWay HisAutoFull
        {
            get
            {
                return (AutoFullWay)this.GetValIntByKey(MapAttrAttr.AutoFullWay);
            }
            set
            {
                this.SetValByKey(MapAttrAttr.AutoFullWay, (int)value);
            }
        }
        /// <summary>
        /// �Զ���д
        /// </summary>
        public string AutoFullDoc
        {
            get
            {
                return this.GetValStrByKey(MapAttrAttr.AutoFullDoc);
            }
            set
            {
                this.SetValByKey(MapAttrAttr.AutoFullDoc, value);
            }
        }
        public string KeyOfEn
        {
            get
            {
                return this.GetValStrByKey(MapAttrAttr.KeyOfEn);
            }
            set
            {
                this.SetValByKey(MapAttrAttr.KeyOfEn, value);
            }
        }
        public FieldTypeS LGType
        {
            get
            {
                return (FieldTypeS)this.GetValIntByKey(MapAttrAttr.LGType);
            }
            set
            {
                this.SetValByKey(MapAttrAttr.LGType, (int)value);
            }
        }
        public string LGTypeT
        {
            get
            {
                return this.GetValRefTextByKey(MapAttrAttr.LGType);
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        public string Name
        {
            get
            {
                string s = this.GetValStrByKey(MapAttrAttr.Name);
                if (s == "" || s == null)
                    return this.KeyOfEn;
                return s;
            }
            set
            {
                this.SetValByKey(MapAttrAttr.Name, value);
            }
        }
        public bool IsNum
        {
            get
            {
                switch (this.MyDataType)
                {
                    case BP.DA.DataType.AppString:
                    case BP.DA.DataType.AppDate:
                    case BP.DA.DataType.AppDateTime:
                    case BP.DA.DataType.AppBoolean:
                        return false;
                    default:
                        return true;
                }
            }
        }
        public decimal DefValDecimal
        {
            get
            {
                return decimal.Parse(this.DefVal);
            }
        }
        public string DefValReal
        {
            get
            {
                return this.GetValStrByKey(MapAttrAttr.DefVal);
            }
        }
        /// <summary>
        /// Ĭ��ֵ
        /// </summary>
        public string DefVal
        {
            get
            {
                string s = this.GetValStrByKey(MapAttrAttr.DefVal);
                if (this.IsNum)
                {
                    if (s == "")
                        return "0";
                }

                switch (this.MyDataType)
                {
                    case BP.DA.DataType.AppDate:
                        if (this.Tag == "1")
                            return DataType.CurrentData;
                        else
                            return "          ";
                        break;
                    case BP.DA.DataType.AppDateTime:
                        if (this.Tag == "1")
                            return DataType.CurrentDataTime;
                        else
                            return "               ";
                        //return "    -  -    :  ";

                        break;
                    default:
                        break;
                }


                if (s.Contains("@") == false)
                    return s;

                switch (s)
                {
                    case "@WebUser.No":
                        return BP.Web.WebUser.No;
                        break;
                    case "@WebUser.Name":
                        return BP.Web.WebUser.Name;
                        break;
                    case "@WebUser.FK_Dept":
                        return BP.Web.WebUser.FK_Dept;
                    case "@WebUser.FK_Station":
                        return BP.Web.WebUser.FK_Station;
                    case "@FK_NY":
                        return DataType.CurrentYearMonth;
                    case "@FK_ND":
                        return DataType.CurrentYear;
                    case "@FK_YF":
                        return DataType.CurrentMonth;
                    default:
                        throw new Exception("û��Լ���ı���Ĭ��ֵ����" + s);
                }
                return this.GetValStrByKey(MapAttrAttr.DefVal);
            }
            set
            {
                this.SetValByKey(MapAttrAttr.DefVal, value);
            }
        }
        public bool DefValOfBool
        {
            get
            {
                return this.GetValBooleanByKey(MapAttrAttr.DefVal, false);
            }
            set
            {
                this.SetValByKey(MapAttrAttr.DefVal, value);
            }
        }
        /// <summary>
        /// �ֶ�
        /// </summary>
        public string Field
        {
            get
            {
                return this.KeyOfEn;
                //string s= this.GetValStrByKey(MapAttrAttr.Field);
                //if (s == null || s == "" || s.Trim()=="" )
                //    return this.Key;
                //else
                //    return s;
            }
        }
        public BP.Web.Controls.TBType HisTBType
        {
            get
            {
                switch (this.MyDataType)
                {
                    case BP.DA.DataType.AppRate:
                    case BP.DA.DataType.AppMoney:
                        return BP.Web.Controls.TBType.Moneny;
                    case BP.DA.DataType.AppInt:
                    case BP.DA.DataType.AppFloat:
                    case BP.DA.DataType.AppDouble:
                        return BP.Web.Controls.TBType.Num;
                    default:
                        return BP.Web.Controls.TBType.TB;
                }
            }
        }
        public int MyDataType
        {
            get
            {
                return this.GetValIntByKey(MapAttrAttr.MyDataType);
            }
            set
            {
                this.SetValByKey(MapAttrAttr.MyDataType, value);
            }
        }
        public string MyDataTypeS
        {
            get
            {
                switch (this.MyDataType)
                {
                    case DataType.AppString:
                        return "String";
                    case DataType.AppInt:
                        return "Int";
                    case DataType.AppFloat:
                        return "Float";
                    case DataType.AppMoney:
                        return "Money";
                    case DataType.AppDate:
                        return "Date";
                    case DataType.AppDateTime:
                        return "DateTime";
                    case DataType.AppBoolean:
                        return "Bool";
                    default:
                        throw new Exception("sdsdsd");
                }
            }
            set
            {

                switch (value)
                {
                    case "String":
                        this.SetValByKey(MapAttrAttr.MyDataType, DataType.AppString);
                        break;
                    case "Int":
                        this.SetValByKey(MapAttrAttr.MyDataType, DataType.AppInt);
                        break;
                    case "Float":
                        this.SetValByKey(MapAttrAttr.MyDataType, DataType.AppFloat);
                        break;
                    case "Money":
                        this.SetValByKey(MapAttrAttr.MyDataType, DataType.AppMoney);
                        break;
                    case "Date":
                        this.SetValByKey(MapAttrAttr.MyDataType, DataType.AppDate);
                        break;
                    case "DateTime":
                        this.SetValByKey(MapAttrAttr.MyDataType, DataType.AppDateTime);
                        break;
                    case "Bool":
                        this.SetValByKey(MapAttrAttr.MyDataType, DataType.AppBoolean);
                        break;
                    default:
                        throw new Exception("sdsdsd");
                }

            }
        }
        public string MyDataTypeStr
        {
            get
            {
                return DataType.GetDataTypeDese(this.MyDataType);
            }
        }
        /// <summary>
        /// ��󳤶�
        /// </summary>
        public int MaxLen
        {
            get
            {
                int i = this.GetValIntByKey(MapAttrAttr.MaxLen);
                if (i > 4000)
                    i = 400;
                return i;
            }
            set
            {
                this.SetValByKey(MapAttrAttr.Field, value);
            }
        }
        /// <summary>
        /// ��С����
        /// </summary>
        public int MinLen
        {
            get
            {
                return this.GetValIntByKey(MapAttrAttr.MinLen);
            }
            set
            {
                this.SetValByKey(MapAttrAttr.MinLen, value);
            }
        }
        public int GroupID
        {
            get
            {
                return this.GetValIntByKey(MapAttrAttr.GroupID);
            }
            set
            {
                this.SetValByKey(MapAttrAttr.GroupID, value);
            }
        }
        public bool IsBigDoc
        {
            get
            {
                if (this.MaxLen > 3000)
                    return true;
                return false;
            }
        }
        /// <summary>
        /// ���
        /// </summary>
        public int UIWidth
        {
            get
            {
                switch (this.MyDataType)
                {
                    case DataType.AppString:
                        return this.GetValIntByKey(MapAttrAttr.UIWidth);
                    case DataType.AppFloat:
                    case DataType.AppInt:
                    case DataType.AppMoney:
                    case DataType.AppRate:
                    case DataType.AppDouble:
                    case DataType.AppDate:
                    case DataType.AppDateTime:
                        return 40;
                    default:
                        return 40;
                }
            }
            set
            {
                this.SetValByKey(MapAttrAttr.UIWidth, value);
            }
        }
        public int UIWidthOfLab
        {
            get
            {
                return 0;

                //Graphics2D g2 = (Graphics2D)g;
                //g2.setRenderingHint(RenderingHints.KEY_ANTIALIASING,
                //                        RenderingHints.VALUE_ANTIALIAS_ON);

                //int textWidth = getFontMetrics(g2.getFont()).bytesWidth(str.getBytes(), 0, str.getBytes().length); 

            }
        }
        /// <summary>
        /// �Ƿ�ֻ��
        /// </summary>
        public bool UIVisible
        {
            get
            {
                return this.GetValBooleanByKey(MapAttrAttr.UIVisible);
            }
            set
            {
                this.SetValByKey(MapAttrAttr.UIVisible, value);
            }
        }
        /// <summary>
        /// �Ƿ����
        /// </summary>
        public bool UIIsEnable
        {
            get
            {
                return this.GetValBooleanByKey(MapAttrAttr.UIIsEnable);
            }
            set
            {
                this.SetValByKey(MapAttrAttr.UIIsEnable, value);
            }
        }
        /// <summary>
        /// �Ƿ񵥶�����ʾ
        /// </summary>
        public bool UIIsLine
        {
            get
            {
                return this.GetValBooleanByKey(MapAttrAttr.UIIsLine);
            }
            set
            {
                this.SetValByKey(MapAttrAttr.UIIsLine, value);
            }
        }
        /// <summary>
        /// �󶨵�ֵ
        /// </summary>
        public string UIBindKey
        {
            get
            {
                return this.GetValStrByKey(MapAttrAttr.UIBindKey);
            }
            set
            {
                this.SetValByKey(MapAttrAttr.UIBindKey, value);
            }
        }
        /// <summary>
        /// �����ı��Key
        /// </summary>
        public string UIRefKey
        {
            get
            {
                return this.GetValStrByKey(MapAttrAttr.UIRefKey);
            }
            set
            {
                this.SetValByKey(MapAttrAttr.UIRefKey, value);
            }
        }
        /// <summary>
        /// �����ı��Lab
        /// </summary>
        public string UIRefKeyText
        {
            get
            {
                return this.GetValStrByKey(MapAttrAttr.UIRefKeyText);
            }
            set
            {
                this.SetValByKey(MapAttrAttr.UIRefKeyText, value);
            }
        }
        /// <summary>
        /// ��ʶ
        /// </summary>
        public string Tag
        {
            get
            {
                return this.GetValStrByKey(MapAttrAttr.Tag);
            }
            set
            {
                this.SetValByKey(MapAttrAttr.Tag, value);
            }
        }
        /// <summary>
        /// �ؼ�����
        /// </summary>
        public UIContralType UIContralType
        {
            get
            {
                return (UIContralType)this.GetValIntByKey(MapAttrAttr.UIContralType);
            }
            set
            {
                this.SetValByKey(MapAttrAttr.UIContralType, (int)value);
            }
        }

        public string UIContralTypeT
        {
            get
            {
                return this.GetValRefTextByKey(MapAttrAttr.UIContralType);
            }
        }
        public string F_Desc
        {
            get
            {
                switch (this.MyDataType)
                {
                    case DataType.AppString:
                        if (this.UIVisible == false)
                            return "����" + this.MinLen + "-" + this.MaxLen + "���ɼ�";
                        else
                            return "����" + this.MinLen + "-" + this.MaxLen;
                    case DataType.AppDate:
                    case DataType.AppDateTime:
                    case DataType.AppInt:
                    case DataType.AppFloat:
                    case DataType.AppMoney:
                        if (this.UIVisible == false)
                            return "���ɼ�";
                        else
                            return "";
                    default:
                        return "";
                }
            }
        }
        /// <summary>
        /// ���
        /// </summary>
        public int IDX
        {
            get
            {
                return this.GetValIntByKey(MapAttrAttr.IDX);
            }
            set
            {
                this.SetValByKey(MapAttrAttr.IDX, value);
            }
        }
        #endregion

        #region ���췽��
        /// <summary>
        /// ʵ������
        /// </summary>
        public MapAttr()
        {
        }
        public MapAttr(int oid)
        {
            this.OID = oid;
            this.Retrieve();
        }
        public MapAttr(string fk_mapdata, string key)
        {
            this.FK_MapData = fk_mapdata;
            this.KeyOfEn = key;
            this.Retrieve(MapAttrAttr.FK_MapData, this.FK_MapData, MapAttrAttr.KeyOfEn, this.KeyOfEn);
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

                Map map = new Map("Sys_MapAttr");
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = "ʵ������";
                map.EnType = EnType.Sys;

                map.AddTBIntPKOID();

                map.AddTBString(MapAttrAttr.FK_MapData, null, "ʵ���ʶ", true, true, 1, 30, 20);
                map.AddTBString(MapAttrAttr.KeyOfEn, null, "����", true, true, 1, 30, 20);
                map.AddTBString(MapAttrAttr.Name, null, "����", true, false, 0, 100, 20);
                map.AddTBString(MapAttrAttr.DefVal, null, "Ĭ��ֵ", false, false, 0, 30, 20);

                map.AddDDLSysEnum(MapAttrAttr.UIContralType, 0, "�ռ�����", true, false, MapAttrAttr.UIContralType, "@0=�ı���@1=������");
                map.AddDDLSysEnum(MapAttrAttr.MyDataType, 0, "��������", true, false, MapAttrAttr.MyDataType,
                    "@1=�ı�(String)@2=����(Int)@3=����(Float)@4=����@5=Double@6=AppDate@7=AppDateTime@8=AppMoney@9=AppRate");

                map.AddDDLSysEnum(MapAttrAttr.LGType, 0, "�߼�����", true, false, MapAttrAttr.LGType, "@0=��ͨ@1=ö��@2=���");

                map.AddTBInt(MapAttrAttr.UIWidth, 10, "���", true, false);
                map.AddTBInt(MapAttrAttr.MinLen, 0, "��С����", true, false);
                map.AddTBInt(MapAttrAttr.MaxLen, 500, "��󳤶�", true, false);

                map.AddTBString(MapAttrAttr.UIBindKey, null, "�󶨵���Ϣ", true, false, 0, 100, 20);
                map.AddTBString(MapAttrAttr.UIRefKey, null, "�󶨵�Key", true, false, 0, 30, 20);
                map.AddTBString(MapAttrAttr.UIRefKeyText, null, "�󶨵�Text", true, false, 0, 30, 20);

                map.AddBoolean(MapAttrAttr.UIVisible, true, "�Ƿ�ɼ�", true, true);
                map.AddBoolean(MapAttrAttr.UIIsEnable, false, "�Ƿ�ֻ��", true, true);
                map.AddBoolean(MapAttrAttr.UIIsLine, false, "�Ƿ񵥶�����ʾ", true, true);

                map.AddTBString(MapAttrAttr.Tag, null, "��ʶ�������ʱ���ݣ�", true, false, 0, 100, 20);
                map.AddTBInt(MapAttrAttr.EditType, 0, "�༭����", true, false);

                map.AddTBString(MapAttrAttr.AutoFullDoc, null, "�Զ���д����", false, false, 0, 500, 20);
                map.AddDDLSysEnum(MapAttrAttr.AutoFullWay, 0, "�Զ���д��ʽ", true, false, MapAttrAttr.AutoFullWay,
                    "@0=������@1=���������ݼ���@2=����SQL�Զ����@3=�����������@4=����ϸ�������ֵ");

                map.AddTBInt(MapAttrAttr.IDX, 0, "���", true, false);
                map.AddTBInt(MapAttrAttr.GroupID, 0, "GroupID", true, false);

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion

        /// <summary>
        /// ����Ƿ񸴺�Ҫ��
        /// </summary>
        /// <returns></returns>
        public string DoCheckFullWay()
        {
            string doc = "";
            switch (this.HisAutoFull)
            {
                case AutoFullWay.Way0: //������
                    break;
                case AutoFullWay.Way1_JS: //JS ��ʽ����
                    break;
                case AutoFullWay.Way2_SQL:
                    break;
                case AutoFullWay.Way3_FK:
                    break;
                case AutoFullWay.Way4_Dtl:
                    break;
                default:
                    break;
            }
            return "���ɹ�������";
        }


        /// <summary>
        /// �ȵ�������
        /// </summary>
        private void DoOrder()
        {
            MapAttrs attrs = new MapAttrs();
            attrs.SearchMapAttrsYesVisable(this.FK_MapData);

            int i = 0;
            foreach (MapAttr attr in attrs)
            {
                i++;
                attr.IDX = i;
                attr.Update(MapAttrAttr.IDX, attr.IDX);
            }
        }
        public string DoUp()
        {
            this.DoOrder();

            this.RetrieveFromDBSources();
            if (this.IDX == 1)
                return null;

            MapAttrs attrs = new MapAttrs();
            attrs.SearchMapAttrsYesVisable(this.FK_MapData);


            MapAttr attrUp = (MapAttr)attrs[this.IDX - 1 - 1];
            attrUp.Update(MapAttrAttr.IDX, this.IDX);
            this.Update(MapAttrAttr.IDX, this.IDX - 1);
            return null;
        }
        public string DoDown()
        {
            this.DoOrder();
            this.RetrieveFromDBSources();

            MapAttrs attrs = new MapAttrs();
            attrs.SearchMapAttrsYesVisable(this.FK_MapData);


            if (this.IDX == attrs.Count)
                return null;

            MapAttr attrDown = (MapAttr)attrs[this.IDX];
            attrDown.Update(MapAttrAttr.IDX, this.IDX);
            this.Update(MapAttrAttr.IDX, this.IDX + 1);
            return null;
        }
        public string DoJump(MapAttr attrTo)
        {
            this.IDX = attrTo.IDX;
            this.GroupID = attrTo.GroupID;
            this.Update();

            string sql = "UPDATE Sys_MapAttr SET IDX=IDX-1 WHERE IDX <="+attrTo.IDX+" AND FK_MapData='"+this.FK_MapData+"' AND KeyOfEn<>'"+this.KeyOfEn+"'";
            DBAccess.RunSQL(sql);
            return null;


            //this.DoOrder();
            //this.RetrieveFromDBSources();

            //MapAttrs attrs = new MapAttrs();
            //attrs.SearchMapAttrsYesVisable(this.FK_MapData);

            //if (this.IDX == attrs.Count)
            //    return null;

            //MapAttr attrDown = (MapAttr)attrs[this.IDX];
            //attrDown.Update(MapAttrAttr.IDX, this.IDX);
            //this.Update(MapAttrAttr.IDX, this.IDX + 1);
            //return null;
        }
        protected override bool beforeDelete()
        {
            try
            {
                BP.DA.DBAccess.RunSQL("alter table " + this.FK_MapData + " drop column " + this.KeyOfEn);
            }
            catch
            {
            }
            return base.beforeDelete();
        }
        protected override bool beforeInsert()
        {
            if (this.KeyOfEn == null || this.KeyOfEn.Trim() == "")
            {
                try
                {
                    this.KeyOfEn = BP.DA.chs2py.convert(this.Name);
                    if (this.KeyOfEn.Length > 20)
                        this.KeyOfEn = BP.DA.chs2py.ConvertWordFirst(this.Name);

                    if (this.KeyOfEn == null || this.KeyOfEn.Trim() == "")
                        throw new Exception("@�������ֶ����������ֶ����ơ�");
                }
                catch (Exception ex)
                {
                    throw new Exception("@�������ֶ��������ֶ����ơ��쳣��Ϣ:"+ex.Message);
                }

                int i = 0;
                while (this.IsExit(BP.Sys.MapAttrAttr.KeyOfEn, this.KeyOfEn, BP.Sys.MapAttrAttr.FK_MapData, this.FK_MapData))
                {
                    this.KeyOfEn = this.KeyOfEn + i;
                    i++;
                }
            }
            else
            {
                if (this.KeyOfEn.Contains("_") == false)
                {
                    System.Text.RegularExpressions.Regex reg1 = new System.Text.RegularExpressions.Regex(@"^[A-Za-z0-9]+$");
                    if (reg1.IsMatch(this.KeyOfEn) == false)
                    {
                        throw new Exception("��������ֶ�Ӣ����[" + this.KeyOfEn + "]������Ҫ���밴Ӣ�ġ�ƴ����д��д������ϵͳд��ɡ�");
                    }
                }
            }

            this.IDX = BP.DA.DBAccess.RunSQLReturnValInt("SELECT COUNT(*) FROM Sys_MapAttr WHERE FK_MapData='" + this.FK_MapData + "'") + 1;
            return base.beforeInsert();
        }
    }
    /// <summary>
    /// ʵ������s
    /// </summary>
    public class MapAttrs : EntitiesOID
    {
        #region ����
        /// <summary>
        /// ʵ������s
        /// </summary>
        public MapAttrs()
        {
        }
        /// <summary>
        /// ʵ������s
        /// </summary>
        public MapAttrs(string fk_map)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(MapAttrAttr.FK_MapData, fk_map);
            qo.addOrderBy(MapAttrAttr.IDX);
            qo.DoQuery();
        }
        public int SearchMapAttrsYesVisable(string fk_map)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(MapAttrAttr.FK_MapData, fk_map);
            qo.addAnd();
            qo.AddWhere(MapAttrAttr.UIVisible, 1);
            qo.addOrderBy(MapAttrAttr.IDX);
            return qo.DoQuery();

        }
        /// <summary>
        /// �õ����� Entity
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new MapAttr();
            }
        }
        public int WithOfCtl
        {
            get
            {
                int i = 0;
                foreach (MapAttr item in this)
                {
                    if (item.UIVisible == false)
                        continue;

                    i += item.UIWidth;
                }
                return i;
            }
        }
        #endregion
    }
}
