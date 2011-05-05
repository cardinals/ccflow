using System;
using System.Collections;
using BP.DA;

namespace BP.En
{
	/// <summary>
	/// EntityOIDNameMyFileAttr
	/// </summary>
	public class EntityOIDNameMyFileAttr:EntityOIDAttr
	{
		/// <summary>
		/// ����
		/// </summary>
		public const string Name="Name";

        public const string MyFileName = "MyFileName";
        public const string MyFileSize = "MyFileSize";
        public const string MyFileH = "MyFileH";
        public const string MyFileW = "MyFileW";
        public const string MyFileExt = "MyFileExt";
	}
	/// <summary>
	/// ���� OID Name ���Ե�ʵ��̳С�	
	/// </summary>
    abstract public class EntityOIDNameMyFile : EntityOID
    {
        #region ����
        /// <summary>
        /// ����
        /// </summary>
        protected EntityOIDNameMyFile() { }
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="oid">OID</param>
        protected EntityOIDNameMyFile(int oid) : base(oid) { }
        #endregion

        #region ���Է���
        /// <summary>
        /// ����
        /// </summary>
        public string Name
        {
            get
            {
                return this.GetValStringByKey(EntityOIDNameMyFileAttr.Name);
            }
            set
            {
                this.SetValByKey(EntityOIDNameMyFileAttr.Name, value);
            }
        }
        public string MyFileExt
        {
            get
            {
                return this.GetValStringByKey("MyFileExt");
            }
            set
            {
                this.SetValByKey("MyFileExt", value);
            }
        }
        public string MyFileName
        {
            get
            {
                return this.GetValStringByKey("MyFileName");
            }
            set
            {
                this.SetValByKey("MyFileName", value);
            }
        }
        public int MyFileSize
        {
            get
            {
                return this.GetValIntByKey("MyFileSize");
            }
            set
            {
                this.SetValByKey("MyFileSize", value);
            }
        }
        public int MyFileH
        {
            get
            {
                return this.GetValIntByKey("MyFileH");
            }
            set
            {
                this.SetValByKey("MyFileH", value);
            }
        }
        public int MyFileW
        {
            get
            {
                return this.GetValIntByKey("MyFileW");
            }
            set
            {
                this.SetValByKey("MyFileW", value);
            }
        }
        public bool IsImg
        {
            get
            {
                return DataType.IsImgExt(this.MyFileExt);
            }
        }
        /// <summary>
        /// �������Ʋ�ѯ��
        /// </summary>
        /// <returns>���ز�ѯ�����ĸ���</returns>
        public int RetrieveByName()
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere("Name", this.Name);
            return qo.DoQuery();
        }
        #endregion
    }
	/// <summary>
	/// ����OID Name ���Ե�ʵ��̳�
	/// </summary>
	abstract public class EntityOIDNameMyFiles : EntitiesOID
	{
		#region ����
		/// <summary>
		/// ����
		/// </summary>
		public EntityOIDNameMyFiles()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}		
		#endregion
	}
}
