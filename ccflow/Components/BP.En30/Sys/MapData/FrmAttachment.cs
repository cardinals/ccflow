using System;
using System.Collections;
using BP.DA;
using BP.En;
namespace BP.Sys
{
    /// <summary>
    /// ����
    /// </summary>
    public class FrmAttachmentAttr : EntityMyPKAttr
    {
        /// <summary>
        /// Name
        /// </summary>
        public const string Name = "Name";
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
        /// W
        /// </summary>
        public const string W = "W";
        /// <summary>
        /// Exts
        /// </summary>
        public const string Exts = "Exts";
        /// <summary>
        /// �������
        /// </summary>
        public const string NoOfAth = "NoOfAth";
        /// <summary>
        /// �Ƿ�����ϴ�
        /// </summary>
        public const string IsUpload = "IsUpload";
        /// <summary>
        /// �Ƿ����ɾ��
        /// </summary>
        public const string IsDelete = "IsDelete";
        /// <summary>
        /// �Ƿ��������
        /// </summary>
        public const string IsDownload = "IsDownload";
        /// <summary>
        /// ���浽
        /// </summary>
        public const string SaveTo = "SaveTo";
    }
    /// <summary>
    /// ����
    /// </summary>
    public class FrmAttachment : EntityMyPK
    {
        #region ����
        /// <summary>
        /// �Ƿ�����ϴ�
        /// </summary>
        public bool IsUpload
        {
            get
            {
                return this.GetValBooleanByKey(FrmAttachmentAttr.IsUpload);
            }
            set
            {
                this.SetValByKey(FrmAttachmentAttr.IsUpload, value);
            }
        }
        /// <summary>
        /// �Ƿ��������
        /// </summary>
        public bool IsDownload
        {
            get
            {
                return this.GetValBooleanByKey(FrmAttachmentAttr.IsDownload);
            }
            set
            {
                this.SetValByKey(FrmAttachmentAttr.IsDownload, value);
            }
        }
        /// <summary>
        /// �Ƿ����ɾ��
        /// </summary>
        public bool IsDelete
        {
            get
            {
                return this.GetValBooleanByKey(FrmAttachmentAttr.IsDelete);
            }
            set
            {
                this.SetValByKey(FrmAttachmentAttr.IsDelete, value);
            }
        }
        /// <summary>
        /// ��������
        /// </summary>
        public string Name
        {
            get
            {
                return this.GetValStringByKey(FrmAttachmentAttr.Name);
            }
            set
            {
                this.SetValByKey(FrmAttachmentAttr.Name, value);
            }
        }
        /// <summary>
        /// Exts
        /// </summary>
        public string Exts
        {
            get
            {
                return this.GetValStringByKey(FrmAttachmentAttr.Exts);
            }
            set
            {
                this.SetValByKey(FrmAttachmentAttr.Exts, value);
            }
        }
        public string SaveTo
        {
            get
            {
                return this.GetValStringByKey(FrmAttachmentAttr.SaveTo);
            }
            set
            {
                this.SetValByKey(FrmAttachmentAttr.SaveTo, value);
            }
        }
        /// <summary>
        /// �������
        /// </summary>
        public string NoOfAth
        {
            get
            {
                return this.GetValStringByKey(FrmAttachmentAttr.NoOfAth);
            }
            set
            {
                this.SetValByKey(FrmAttachmentAttr.NoOfAth, value);
            }
        }
        /// <summary>
        /// Y
        /// </summary>
        public float Y
        {
            get
            {
                return this.GetValFloatByKey(FrmAttachmentAttr.Y);
            }
            set
            {
                this.SetValByKey(FrmAttachmentAttr.Y, value);
            }
        }
        /// <summary>
        /// X
        /// </summary>
        public float X
        {
            get
            {
                return this.GetValFloatByKey(FrmAttachmentAttr.X);
            }
            set
            {
                this.SetValByKey(FrmAttachmentAttr.X, value);
            }
        }
        /// <summary>
        /// W
        /// </summary>
        public float W
        {
            get
            {
                return this.GetValFloatByKey(FrmAttachmentAttr.W);
            }
            set
            {
                this.SetValByKey(FrmAttachmentAttr.W, value);
            }
        }
        /// <summary>
        /// FK_MapData
        /// </summary>
        public string FK_MapData
        {
            get
            {
                return this.GetValStrByKey(FrmAttachmentAttr.FK_MapData);
            }
            set
            {
                this.SetValByKey(FrmAttachmentAttr.FK_MapData, value);
            }
        }
        #endregion

        #region ���췽��
        /// <summary>
        /// ����
        /// </summary>
        public FrmAttachment()
        {
        }
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="mypk"></param>
        public FrmAttachment(string mypk)
        {
            this.MyPK = mypk;
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
                Map map = new Map("Sys_FrmAttachment");

                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = "����";
                map.EnType = EnType.Sys;
                map.AddMyPK();

                map.AddTBString(FrmAttachmentAttr.FK_MapData, null, 
                    "FK_MapData", true, false, 1, 30, 20);

                map.AddTBString(FrmAttachmentAttr.NoOfAth, null, "�������", true, false, 0, 50, 20);

                map.AddTBString(FrmAttachmentAttr.Name, null,"����", true, false, 0, 50, 20);
                map.AddTBString(FrmAttachmentAttr.Exts, null, "��չ", true, false, 0, 50, 20);
                map.AddTBString(FrmAttachmentAttr.SaveTo, null, "���浽", true, false, 0, 50, 20);
                
                
                map.AddTBFloat(FrmAttachmentAttr.X, 5, "X", true, false);
                map.AddTBFloat(FrmAttachmentAttr.Y, 5, "Y", false, false);
                map.AddTBFloat(FrmAttachmentAttr.W, 5, "TBWidth", false, false);


                map.AddTBInt(FrmAttachmentAttr.IsUpload, 1, "�Ƿ�����ϴ�", false, false);
                map.AddTBInt(FrmAttachmentAttr.IsDelete, 1, "�Ƿ����ɾ��", false, false);
                map.AddTBInt(FrmAttachmentAttr.IsDownload, 1, "�Ƿ��������", false, false);


                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
    /// <summary>
    /// ����s
    /// </summary>
    public class FrmAttachments : EntitiesMyPK
    {
        #region ����
        /// <summary>
        /// ����s
        /// </summary>
        public FrmAttachments()
        {
        }
        /// <summary>
        /// ����s
        /// </summary>
        /// <param name="fk_mapdata">s</param>
        public FrmAttachments(string fk_mapdata)
        {
            this.Retrieve(FrmAttachmentAttr.FK_MapData, fk_mapdata);
        }
        /// <summary>
        /// �õ����� Entity
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new FrmAttachment();
            }
        }
        #endregion
    }
}
