using System;
using System.Collections;
using BP.DA;
using BP.En;
namespace BP.Sys
{
    /// <summary>
    /// �������ݴ洢 - ����
    /// </summary>
    public class FrmAttachmentDBAttr : EntityMyPKAttr
    {
        /// <summary>
        /// ����
        /// </summary>
        public const string FK_FrmAttachment = "FK_FrmAttachment";
        /// <summary>
        /// ����
        /// </summary>
        public const string FK_MapData = "FK_MapData";
        /// <summary>
        /// RefPKVal
        /// </summary>
        public const string RefPKVal = "RefPKVal";
        /// <summary>
        /// �ļ�����
        /// </summary>
        public const string FileName = "FileName";
        /// <summary>
        /// �ļ���չ
        /// </summary>
        public const string FileExts = "FileExts";
        /// <summary>
        /// �ļ���С
        /// </summary>
        public const string FileSize = "FileSize";
        /// <summary>
        /// ���浽
        /// </summary>
        public const string SaveTo = "SaveTo";
    }
    /// <summary>
    /// �������ݴ洢
    /// </summary>
    public class FrmAttachmentDB : EntityMyPK
    {
        #region ����
        public string SaveTo
        {
            get
            {
                return this.GetValStringByKey(FrmAttachmentDBAttr.SaveTo);
            }
            set
            {
                this.SetValByKey(FrmAttachmentDBAttr.SaveTo, value);
            }
        }
        
        /// <summary>
        /// ��������
        /// </summary>
        public string FileName
        {
            get
            {
                return this.GetValStringByKey(FrmAttachmentDBAttr.FileName);
            }
            set
            {
                this.SetValByKey(FrmAttachmentDBAttr.FileName, value);
            }
        }
        /// <summary>
        /// FileExts
        /// </summary>
        public string FileExts
        {
            get
            {
                return this.GetValStringByKey(FrmAttachmentDBAttr.FileExts);
            }
            set
            {
                this.SetValByKey(FrmAttachmentDBAttr.FileExts, value);
            }
        }
        /// <summary>
        /// ��ظ���
        /// </summary>
        public string FK_FrmAttachment
        {
            get
            {
                return this.GetValStringByKey(FrmAttachmentDBAttr.FK_FrmAttachment);
            }
            set
            {
                this.SetValByKey(FrmAttachmentDBAttr.FK_FrmAttachment, value);
            }
        }
        public string RefPKVal
        {
            get
            {
                return this.GetValStringByKey(FrmAttachmentDBAttr.RefPKVal);
            }
            set
            {
                this.SetValByKey(FrmAttachmentDBAttr.RefPKVal, value);
            }
        }
        
        /// <summary>
        /// �������
        /// </summary>
        public string FK_MapData
        {
            get
            {
                return this.GetValStringByKey(FrmAttachmentDBAttr.FK_MapData);
            }
            set
            {
                this.SetValByKey(FrmAttachmentDBAttr.FK_MapData, value);
            }
        }
        /// <summary>
        /// �ļ���С
        /// </summary>
        public float FileSize
        {
            get
            {
                return this.GetValFloatByKey(FrmAttachmentDBAttr.FileSize);
            }
            set
            {
                this.SetValByKey(FrmAttachmentDBAttr.FileSize, value);
            }
        }
        public string FileFull
        {
            get
            {
                return this.SaveTo + "\\" + this.FileName;
                //return this.GetValStringByKey(FrmAttachmentDBAttr.FK_FrmAttachment);
            }
        }
        #endregion

        #region ���췽��
        /// <summary>
        /// �������ݴ洢
        /// </summary>
        public FrmAttachmentDB()
        {
        }
        /// <summary>
        /// �������ݴ洢
        /// </summary>
        /// <param name="mypk"></param>
        public FrmAttachmentDB(string mypk)
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
                Map map = new Map("Sys_FrmAttachmentDB");

                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = "�������ݴ洢";
                map.EnType = EnType.Sys;
                map.AddMyPK();

                map.AddTBString(FrmAttachmentDBAttr.FK_MapData, null,"FK_MapData", true, false, 1, 30, 20);
                map.AddTBString(FrmAttachmentDBAttr.FK_FrmAttachment, null, "�������", true, false, 1, 50, 20);

                map.AddTBString(FrmAttachmentDBAttr.RefPKVal, null, "ʵ������", true, false, 0, 50, 20);

                map.AddTBString(FrmAttachmentDBAttr.SaveTo, null, "SaveTo", true, false, 0, 200, 20);
                
                map.AddTBString(FrmAttachmentDBAttr.FileName, null,"����", true, false, 0, 50, 20);
                map.AddTBString(FrmAttachmentDBAttr.FileExts, null, "��չ", true, false, 0, 50, 20);
                map.AddTBFloat(FrmAttachmentDBAttr.FileSize, 0, "�ļ���С", true, false);

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
    /// <summary>
    /// �������ݴ洢s
    /// </summary>
    public class FrmAttachmentDBs : EntitiesMyPK
    {
        #region ����
        /// <summary>
        /// �������ݴ洢s
        /// </summary>
        public FrmAttachmentDBs()
        {
        }
        /// <summary>
        /// �������ݴ洢s
        /// </summary>
        /// <param name="fk_mapdata">s</param>
        public FrmAttachmentDBs(string fk_mapdata,string pkval)
        {
            this.Retrieve(FrmAttachmentDBAttr.FK_MapData, fk_mapdata, 
                FrmAttachmentDBAttr.RefPKVal, pkval);
        }
        /// <summary>
        /// �õ����� Entity
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new FrmAttachmentDB();
            }
        }
        #endregion
    }
}
