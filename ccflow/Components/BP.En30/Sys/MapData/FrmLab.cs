using System;
using System.Collections;
using BP.DA;
using BP.En;
namespace BP.Sys
{
      
    /// <summary>
    /// ��ǩ
    /// </summary>
    public class FrmLabAttr : EntityOIDNameAttr
    {
        /// <summary>
        /// ����
        /// </summary>
        public const string FK_MapData = "FK_MapData";
        /// <summary>
        /// X
        /// </summary>
        public const string X = "X";
        /// <summary>
        /// Y
        /// </summary>
        public const string Y = "Y";
        /// <summary>
        /// X2
        /// </summary>
        public const string X2 = "X2";
        /// <summary>
        /// Y2
        /// </summary>
        public const string Y2 = "Y2";
        /// <summary>
        /// ���
        /// </summary>
        public const string FrontSize = "FrontSize";
        /// <summary>
        /// ��ɫ
        /// </summary>
        public const string FrontColor = "FrontColor";
        /// <summary>
        /// ���
        /// </summary>
        public const string FrontName = "FrontName";
        /// <summary>
        /// ������
        /// </summary>
        public const string FrontWeight = "FrontWeight";
    }
    /// <summary>
    /// ��ǩ
    /// </summary>
    public class FrmLab : EntityOIDName
    {
        #region ����
        public string FrontWeight
        {
            get
            {
                return this.GetValStringByKey(FrmLabAttr.FrontWeight);
            }
            set
            {
                this.SetValByKey(FrmLabAttr.FrontWeight, value);
            }
        }
        public string FrontColor
        {
            get
            {
                return this.GetValStringByKey(FrmLabAttr.FrontColor);
            }
            set
            {
                this.SetValByKey(FrmLabAttr.FrontColor, value);
            }
        }
 
       
        public string FrontName
        {
            get
            {
                return this.GetValStringByKey(FrmLabAttr.FrontName);
            }
            set
            {
                this.SetValByKey(FrmLabAttr.FrontName, value);
            }
        }
        /// <summary>
        /// �Ƿ�����Ա��Ȩ��
        /// </summary>
        public int Y
        {
            get
            {
                return this.GetValIntByKey(FrmLabAttr.Y);
            }
            set
            {
                this.SetValByKey(FrmLabAttr.Y, value);
            }
        }
        public int X
        {
            get
            {
                return this.GetValIntByKey(FrmLabAttr.X);
            }
            set
            {
                this.SetValByKey(FrmLabAttr.X, value);
            }
        }
        public int FrontSize
        {
            get
            {
                return this.GetValIntByKey(FrmLabAttr.FrontSize);
            }
            set
            {
                this.SetValByKey(FrmLabAttr.FrontSize, value);
            }
        }
        public string FK_MapData
        {
            get
            {
                return this.GetValStrByKey(FrmLabAttr.FK_MapData);
            }
            set
            {
                this.SetValByKey(FrmLabAttr.FK_MapData, value);
            }
        }
        #endregion

        #region ���췽��
        /// <summary>
        /// ��ǩ
        /// </summary>
        public FrmLab()
        {
        }
        public FrmLab(int oid)
        {
            this.OID = oid;
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
                Map map = new Map("Sys_FrmLab");
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = "��ǩ";
                map.EnType = EnType.Sys;

                map.AddTBIntPKOID();
                map.AddTBString(FrmLabAttr.FK_MapData, null, "����", true, false, 1, 30, 20);
                map.AddTBString(FrmLabAttr.Name, "�½���ǩ", "����", true, false, 0, 3900, 20);

                map.AddTBInt(FrmLabAttr.X, 5, "X", true, false);
                map.AddTBInt(FrmLabAttr.Y, 5, "Y", false, false);

                map.AddTBInt(FrmLabAttr.FrontSize, 12, "��С", false, false);
                map.AddTBString(FrmLabAttr.FrontColor, "black", "��ɫ", true, false, 0, 30, 20);
                map.AddTBString(FrmLabAttr.FrontName, "����", "��������", true, false, 0, 30, 20);
                map.AddTBString(FrmLabAttr.FrontWeight, "normal", "������", true, false, 0, 30, 20);

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
    /// <summary>
    /// ��ǩs
    /// </summary>
    public class FrmLabs : EntitiesNoName
    {
        #region ����
        /// <summary>
        /// ��ǩs
        /// </summary>
        public FrmLabs()
        {
        }
        /// <summary>
        /// ��ǩs
        /// </summary>
        /// <param name="fk_mapdata">s</param>
        public FrmLabs(string fk_mapdata)
        {
            this.Retrieve(FrmLabAttr.FK_MapData, fk_mapdata);
        }
        /// <summary>
        /// �õ����� Entity
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new FrmLab();
            }
        }
        #endregion
    }
}
