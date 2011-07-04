using System;
using System.Collections;
using BP.DA;

namespace BP.En
{
	/// <summary>
	/// �����б�
	/// </summary>
	public class EntityNoNameAttr : EntityNoAttr
	{	
		/// <summary>
		/// ����
		/// </summary>
		public const string Name="Name";
        public const string NameOfS = "NameOfS";

	}
    public class EntityNoNameMyFileAttr : EntityNoMyFileAttr
    {
        /// <summary>
        /// ����
        /// </summary>
        public const string Name = "Name";
    }
	/// <summary>
	/// NoEntity ��ժҪ˵����
	/// </summary>
    abstract public class EntityNoName : EntityNo
    {
        #region ����

        /// <summary>
        /// ����
        /// </summary>
        public string Name
        {
            get
            {
                return this.GetValStringByKey(EntityNoNameAttr.Name);
            }
            set
            {
                this.SetValByKey(EntityNoNameAttr.Name, value);
            }
        }
        //public string NameE
        //{
        //    get
        //    {
        //        return this.GetValStringByKey("NameE");
        //    }
        //    set
        //    {
        //        this.SetValByKey("NameE", value);
        //    }
        //}
        #endregion

        #region ���캯��
        /// <summary>
        /// 
        /// </summary>
        public EntityNoName()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_No"></param>
        protected EntityNoName(string _No) : base(_No) { }
        #endregion

        #region ҵ���߼�����
        /// <summary>
        /// ������Ƶ�����.
        /// </summary>
        /// <returns></returns>
        protected override bool beforeInsert()
        {
            if (this.No.Trim().Length == 0)
            {
                if (this.EnMap.IsAutoGenerNo)
                    this.No = this.GenerNewNo;

                throw new Exception("@û�и�[" + this.EnDesc + " , " + this.Name + "]��������.");
            }

            if (this.EnMap.IsAllowRepeatName == false)
            {
                if (this.PKCount == 1)
                {
                    if (this.ExitsValueNum("Name", this.Name) >= 1)
                        throw new Exception("@����ʧ��[" + this.EnMap.EnDesc + "] ���[" + this.No + "]����[" + Name + "]�ظ�.");
                }
            }
            return base.beforeInsert();
        }
        protected override bool beforeUpdate()
        {
            if (this.EnMap.IsAllowRepeatName == false)
            {
                if (this.PKCount == 1)
                {
                    if (this.ExitsValueNum("Name", this.Name) >= 2)
                        throw new Exception("@����ʧ��[" + this.EnMap.EnDesc + "] ���[" + this.No + "]����[" + Name + "]�ظ�.");
                }
            }
            return base.beforeUpdate();
        }
        #endregion
    }
	/// <summary>
	/// EntitiesNoName
	/// </summary>
	abstract public class EntitiesNoName : EntitiesNo
	{
		/// <summary>
		/// ��������ӵ�����β������������Ѿ����ڣ������.
		/// </summary>
		/// <param name="entity">Ҫ��ӵĶ���</param>
		/// <returns>������ӵ��ĵط�</returns>
		public virtual int AddEntity(EntityNoName entity)
		{
			foreach(EntityNoName en in this)
			{
				if (en.No==entity.No)
					return 0;
			}
			return this.InnerList.Add(entity);
		}
		/// <summary>
		/// ��������ӵ�����β������������Ѿ����ڣ������
		/// </summary>
		/// <param name="entity">Ҫ��ӵĶ���</param>
		/// <returns>������ӵ��ĵط�</returns>
		public virtual void  Insert(int index , EntityNoName entity)
		{
			foreach(EntityNoName en in this)
			{
				if (en.No==entity.No)
					return ;
			}
			
			 this.InnerList.Insert(index,entity);
		}
		/// <summary>
		/// ����λ��ȡ������
		/// </summary>
		public new EntityNoName this[int index]
		{
			get 
			{
				return (EntityNoName)this.InnerList[index];
			}
		}
		/// <summary>
		/// ����
		/// </summary>
        public EntitiesNoName()
        {
        }
		/// <summary> 
		/// ��������ģ����ѯ
		/// </summary>
		/// <param name="likeName">likeName</param>
		/// <returns>���ز�ѯ��Num</returns>
		public int RetrieveByLikeName(string likeName)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere("Name","like" ," %"+likeName+"% ");
			return qo.DoQuery();
		}
        public override int RetrieveAll()
        {
            return base.RetrieveAll("No");
        }

        #region ���ļ��й�ϵ�Ĳ���
        /// <summary>
        /// ��ָ����Ŀ¼�Ž����ݿ���
        /// </summary>
        /// <param name="dirPath">Ŀ¼�ļ�</param>
        public int ReadDirToData(string dirPath, string parentNo)
        {
          //  string dirName = dirPath.Substring(dirPath.LastIndexOf("\\") + 1);
          //  string dirNo = dirName.Substring(0, 2);
            
            this.Clear();
            string[] dirs = System.IO.Directory.GetDirectories(dirPath); // ��ȡ�ļ��С�
            foreach (string dir in dirs)
            {
                string dirShortName = dir.Substring(dir.LastIndexOf("\\") + 1);
                string no = dirShortName.Substring(0, 2);

                EntityNoName en = this.GetNewEntity as EntityNoName;
                en.No = parentNo + no;
                en.Name = dirShortName.Substring(2);

                en.SetValByKey("FK_Sort", parentNo);
                en.SetValByKey("FK_Item1", parentNo);
                en.SetValByKey("FK_Type", parentNo);

                en.Save();
                this.AddEntity(en);
            }
            return this.Count;
        }
        /// <summary>
        /// ��ȡ�ļ������ݿ�
        /// </summary>
        /// <param name="filePath">�ļ�·��</param>
        public void ReadFileToData(string filePath, string fk_item1, string item2 )
        {
            string[] strs = System.IO.Directory.GetFiles(filePath);

            for (int i = 0; i < strs.Length; i++)
            {
                string file = strs[i];
                string shortName = file.Substring(file.LastIndexOf("\\") + 1);
                string ext = shortName.Substring(shortName.LastIndexOf(".") + 1); // �ļ���׺��
                int paycent = 0;
                if (shortName.IndexOf("@") != -1)
                {
                    string centStr = shortName.Substring(shortName.LastIndexOf("@") + 1); // �õ� xxx.doc ���֡�
                    centStr = centStr.Replace("." + ext, "");
                    paycent = int.Parse(centStr);
                }

                EntityNoName en = this.GetNewEntity as EntityNoName;
                en.No = item2 + i.ToString().PadLeft(3, '0');
                en.Name = shortName;

                en.SetValByKey("FK_Item1", fk_item1);
                en.SetValByKey("FK_Item", item2);

                en.SetValByKey("FK_Sort1", fk_item1);
                en.SetValByKey("FK_Sort", item2);

                System.IO.FileInfo info = new System.IO.FileInfo(file);
                en.SetValByKey("FileSize", info.Length/1000 );
                en.SetValByKey("Ext", info.Extension.Replace(".",""));

                en.Save();
                //fdb.FSize = ;
                //fdb.CDT = info.CreationTime.ToString(DataType.SysDataFormatCN);
                //fdb.PayCent = paycent;
                //fdb.Insert();
            }
 
        }
        #endregion


    }
}
