using System;
using System.IO;
using System.Collections;
using BP.DA;
using BP.En;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
namespace BP.Sys
{
    public class SysFileAttr : EntityNoNameAttr
    {
        /// <summary>
        /// �ϴ�����
        /// </summary>
        public const string RDT = "RDT";
        /// <summary>
        /// ��¼��
        /// </summary>
        public const string Rec = "Rec";
        /// <summary>
        /// ������Table
        /// </summary>
        public const string EnName = "EnName";
        /// <summary>
        /// ������key
        /// </summary>
        public const string RefVal = "RefVal";
        /// <summary>
        /// ImgH
        /// </summary>
        public const string ImgH = "ImgH";
        /// <summary>
        /// ImgW
        /// </summary>
        public const string ImgW = "ImgW";
        /// <summary>
        /// �ļ���С
        /// </summary>
        public const string FileSize = "FileSize";
        /// <summary>
        /// �ļ�����
        /// </summary>
        public const string FileType = "FileType";
        /// <summary>
        /// ��ע
        /// </summary>
        public const string Note = "Note";
        /// <summary>
        /// FileID
        /// </summary>
        public const string FileID = "FileID";
        public const string FileName = "FileName";
    }
    public class SysFile : BP.En.EntityMyPK
    {
        #region ʵ�ֻ�������
        public string FileID
        {
            get
            {
                return this.GetValStringByKey(SysFileAttr.FileID);
            }
            set
            {
                this.SetValByKey(SysFileAttr.FileID, value);
            }
        }
        public string Name
        {
            get
            {
                return this.GetValStringByKey(SysFileAttr.Name);
            }
            set
            {
                this.SetValByKey(SysFileAttr.Name, value);
            }
        }
        public string Rec
        {
            get
            {
                return this.GetValStringByKey(SysFileAttr.Rec);
            }
            set
            {
                this.SetValByKey(SysFileAttr.Rec, value);
            }
        }
        /// <summary>
        /// EnName  
        /// </summary>
        public string EnName
        {
            get
            {
                return this.GetValStringByKey(SysFileAttr.EnName);
            }
            set
            {
                this.SetValByKey(SysFileAttr.EnName, value);
            }
        }
        public object RefVal
        {
            get
            {
                return this.GetValByKey(SysFileAttr.RefVal);
            }
            set
            {
                this.SetValByKey(SysFileAttr.RefVal, value);
            }
        }
        public int ImgH
        {
            get
            {
                return this.GetValIntByKey(SysFileAttr.ImgH);
            }
            set
            {
                this.SetValByKey(SysFileAttr.ImgH, value);
            }
        }
        public int ImgW
        {
            get
            {
                return this.GetValIntByKey(SysFileAttr.ImgW);
            }
            set
            {
                this.SetValByKey(SysFileAttr.ImgW, value);
            }
        }
        public string FileSize
        {
            get
            {
                return this.GetValStringByKey(SysFileAttr.FileSize);
            }
            set
            {
                this.SetValByKey(SysFileAttr.FileSize, value);
            }
        }
        public string FileName
        {
            get
            {
                return this.GetValStringByKey(SysFileAttr.FileName);
            }
            set
            {
                this.SetValByKey(SysFileAttr.FileName, value);
            }
        }
        public string FileType
        {
            get
            {
                return this.GetValStringByKey(SysFileAttr.FileType);
            }
            set
            {
                string s = value;
                s = s.Replace(".", "");
                this.SetValByKey(SysFileAttr.FileType, s);
            }
        }
        public string RDT
        {
            get
            {
                return this.GetValStringByKey(SysFileAttr.RDT);
            }
            set
            {
                this.SetValByKey(SysFileAttr.RDT, value);
            }
        }
        public string Note
        {
            get
            {
                return this.GetValStringByKey(SysFileAttr.Note);
            }
            set
            {
                this.SetValByKey(SysFileAttr.Note, value);
            }
        }
        #endregion

        #region ���췽��

        public SysFile() { }
        /// <summary>
        /// �ļ�������
        /// </summary>
        /// <param name="_OID"></param>
        public SysFile(string _OID)
            : base(_OID)
        {
        }
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null) return this._enMap;
                Map map = new Map("Sys_File");
                map.EnDesc = "�ļ�������";
                map.CodeStruct = "3";
                map.AddMyPK();
                map.AddTBString(SysFileAttr.Rec, null, "�ϴ���", true, false, 0, 50, 20);
                map.AddTBString(SysFileAttr.Name, null, "����", true, false, 0, 500, 20);
                map.AddTBString(SysFileAttr.EnName, null, "��", false, true, 0, 50, 20);
                map.AddTBString(SysFileAttr.RefVal, null, "����", false, true, 0, 50, 10);
                map.AddTBString(SysFileAttr.FileID, null, "FileID", false, true, 0, 50, 10);

                map.AddTBInt(SysFileAttr.ImgH, 0, "ImgH", false, true);
                map.AddTBInt(SysFileAttr.ImgW, 0, "ImgW", false, true);

                map.AddTBString(SysFileAttr.FileSize, null, "��С", true, true, 0, 20, 10);
                map.AddTBString(SysFileAttr.FileType, null, "�ļ�����", true, true, 0, 50, 20);

                map.AddTBDate(SysFileAttr.RDT, null, "ʱ��", true, true);
                //   map.AddTBString(SysFileAttr.Note, null, "��ע", true, false, 0, 200, 30);

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
	/// <summary>
	/// �ļ������� 
	/// </summary>
	public class SysFiles :EntitiesMyPK
	{
        /// <summary>
        /// ����ͼƬ��
        /// </summary>
        /// <param name="en"></param>
        /// <param name="pk"></param>
        /// <returns></returns>
        public static string GenerImgs(Entity en, string pk)
        {
            string html = "";
            if (en.GetValStrByKey("MyFileName") != "")
                html += "<img src='/Data/" + en.ToString() + "/" + en.PKVal + "." + en.GetValStrByKey("MyFileExt") + "' align=left border=0/>";

            if (en.GetValIntByKey("MyFileNum") == 0)
                return html;

            SysFiles ens = new SysFiles(en.ToString(), pk);
            foreach (SysFile sf in ens)
            {
                html += "<br><img align=left src='/Data/" + en.ToString() + "/" + sf.FileID + "." + sf.FileType + "' width='" + sf.ImgW + "px' height='" + sf.ImgH + "px' border=0/>";
            }
            return html;
        }
        /// <summary>
        /// �ļ�������
        /// </summary>
		public SysFiles(){}
        /// <summary>
        /// �ļ�������
        /// </summary>
        /// <param name="enName"></param>
        /// <param name="key"></param>
        public SysFiles(string enName, string key)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(SysFileAttr.EnName, enName);
            qo.addAnd();
            qo.AddWhere(SysFileAttr.RefVal, key);
            qo.DoQuery();
        }
		/// <summary>
		/// �õ����� Entity
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new SysFile();
			}
		}
	}
}
