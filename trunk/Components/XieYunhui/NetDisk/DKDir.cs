using System;
using System.Data;
using BP.DA;
using BP.En;

namespace BP.GE
{
    /// <summary>
    /// ����
    /// </summary>
    public class DKDirAttr : EntityOIDNoNameAttr
    {
        /// <summary>
        /// ������
        /// </summary>
        public const string GradeNo = "GradeNo";
        /// <summary>
        /// ����Ա
        /// </summary>
        public const string FK_Emp = "FK_Emp";
        /// <summary>
        /// �Ƿ���
        /// </summary>
        public const string IsShare = "IsShare";
    }

    /// <summary>
    /// �û�Ŀ¼
    /// </summary>
    public class DKDir : EntityOIDName
    {
        #region  ����
        /// <summary>
        /// �û�
        /// </summary>
        public string FK_Emp
        {
            get
            {
                return this.GetValStringByKey(DKDirAttr.FK_Emp);
            }
            set
            {
                this.SetValByKey(DKDirAttr.FK_Emp, value);
            }
        }
        /// <summary>
        /// ���
        /// </summary>
        public string GradeNo
        {
            get
            {
                return this.GetValStringByKey(DKDirAttr.GradeNo);
            }
            set
            {
                this.SetValByKey(DKDirAttr.GradeNo, value);
            }
        }

        // ��ȡ���ڵ����к���
        public DKDirs HisChilds
        {
            get
            {
                DKDirs ens = new DKDirs();
                QueryObject qo = new QueryObject(ens);
                qo.AddWhere(DKDirAttr.GradeNo, " LIKE ", this.GradeNo + "%");
                qo.addAnd();
                qo.AddWhere(DKDirAttr.FK_Emp, this.FK_Emp);
                qo.addAnd();
                qo.AddWhere(DKDirAttr.GradeNo, "<>", this.GradeNo);
                qo.addOrderBy(DKDirAttr.GradeNo);
                qo.DoQuery();
                return ens;
            }
        }

        // ��ȡ���ڵ��� 
        public string GradeNoOfParent
        {
            get
            {
                if (this.GradeNo.Length == 2)
                    return "";
                return this.GradeNo.Substring(0, this.GradeNo.Length - 2);
            }
        }

        // ��ȡ���ڵ�����к��ӽڵ�
        public DKDirs HisParentChilds
        {
            get
            {
                DKDirs ens = new DKDirs();
                QueryObject qo = new QueryObject(ens);
                qo.AddWhere(DKDirAttr.GradeNo, " LIKE ", this.GradeNoOfParent + "%");
                qo.addAnd();
                qo.AddWhere(DKDirAttr.FK_Emp, this.FK_Emp);
                qo.addOrderBy(DKDirAttr.GradeNo);
                qo.DoQuery();
                return ens;
            }
        }

        // ��ȡ���е��ֵܽڵ�
        public DKDirs HisBrotherNodes
        {
            get
            {
                DKDirs ens = new DKDirs();
                QueryObject qo = new QueryObject(ens);
                qo.AddWhere(DKDirAttr.GradeNo, " LIKE ", this.GradeNoOfParent + "%");
                qo.addAnd();
                qo.AddWhere(DKDirAttr.FK_Emp, this.FK_Emp);
                qo.addAnd();
                qo.AddWhereLen(DKDirAttr.GradeNo, "=", this.GradeNo.Length, DBType.SQL2000);
                qo.addOrderBy(DKDirAttr.GradeNo);
                qo.DoQuery();
                return ens;
            }
        }

        // ��ȡ���е��ֵܽڵ�(����)
        public DKDirs HisBrotherNodesDescOrder
        {
            get
            {
                DKDirs ens = new DKDirs();
                QueryObject qo = new QueryObject(ens);
                qo.AddWhere(DKDirAttr.GradeNo, " LIKE ", this.GradeNoOfParent + "%");
                qo.addAnd();
                qo.AddWhere(DKDirAttr.FK_Emp, this.FK_Emp);
                qo.addAnd();
                qo.AddWhereLen(DKDirAttr.GradeNo, "=", this.GradeNo.Length, DBType.SQL2000);
                qo.addOrderByDesc(DKDirAttr.GradeNo);
                qo.DoQuery();
                return ens;
            }
        }
        #endregion

        #region ���캯��
        /// <summary>
        /// �û�Ŀ¼
        /// </summary>
        public DKDir()
        {
        }
        /// <summary>
        /// �û�Ŀ¼
        /// </summary>
        /// <param name="no"></param>
        public DKDir(int no)
        {
            this.OID = no;
            this.Retrieve();
        }
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;
                Map map = new Map("GE_DKDir");
                map.EnDesc = "�������Ŀ¼";
                //  map.TitleExt = " - <a href='Batch.aspx?EnsName=BP.GE.Infos' >��Ϣ����</a>";
                map.IsAutoGenerNo = false;
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;

                map.AddTBIntPKOID();
                map.AddTBString(DKDirAttr.GradeNo, null, "���", true, true, 100, 100, 100);
                map.AddTBString(DKDirAttr.Name, null, "����", true, false, 0, 50, 300);
                map.AddTBString(DKDirAttr.FK_Emp, null, "�û�", false, false, 0, 50, 300);
                map.AddTBInt(DKDirAttr.IsShare, 0, "�����?", false, false);

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
        protected override bool beforeInsert()
        {
            //this.FK_Emp = Web.WebUser.No;
            return base.beforeInsert();
        }
    }
    /// <summary>
    /// �û�Ŀ¼ 
    /// </summary>
    public class DKDirs : EntitiesOID
    {
        /// <summary>
        /// ��ȡ�û�Ŀ¼
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new DKDir();
            }
        }

        #region ���캯��
        /// <summary>
        /// �û�Ŀ¼
        /// </summary>
        public DKDirs()
        {
        }
        /// <summary>
        /// ��ѯȫ��
        /// </summary>
        /// <returns></returns>
        public override int RetrieveAll()
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(DKDirAttr.FK_Emp, Web.WebUser.No);
            int i = qo.DoQuery();
            if (i == 0)
            {
                DKDir en = new DKDir();
                en.FK_Emp = Web.WebUser.No;
                en.GradeNo = "01";
                en.Name = "�ҵ��ĵ�";
                en.Insert();

                en = new DKDir();
                en.FK_Emp = Web.WebUser.No;
                en.GradeNo = "02";
                en.Name = "�ҵĿμ�";
                en.InsertAsNew();


                en = new DKDir();
                en.FK_Emp = Web.WebUser.No;
                en.GradeNo = "03";
                en.Name = "�ҵ��ز�";
                en.InsertAsNew();

                en = new DKDir();
                en.FK_Emp = Web.WebUser.No;
                en.GradeNo = "0301";
                en.Name = "ͼƬ";
                en.InsertAsNew();

                en = new DKDir();
                en.FK_Emp = Web.WebUser.No;
                en.GradeNo = "0302";
                en.Name = "��Ƶ";
                en.InsertAsNew();

                en = new DKDir();
                en.FK_Emp = Web.WebUser.No;
                en.GradeNo = "0303";
                en.Name = "��Ƶ";
                en.InsertAsNew();

                en = new DKDir();
                en.FK_Emp = Web.WebUser.No;
                en.GradeNo = "0304";
                en.Name = "����";
                en.InsertAsNew();


                en = new DKDir();
                en.FK_Emp = Web.WebUser.No;
                en.GradeNo = "04";
                en.Name = "�ҵ���Ƭ";
                en.InsertAsNew();


                en = new DKDir();
                en.FK_Emp = Web.WebUser.No;
                en.GradeNo = "05";
                en.Name = "�ҵ���Ƶ";
                en.InsertAsNew();
                return qo.DoQuery();
            }
            return i;
        }
        #endregion

        #region ��ѯ
        /// <summary>
        /// ��õ�һ���ڵ�
        /// </summary>
        /// <param name="fk_emp">�û���</param>
        /// <returns></returns>
        public int ReGrade1(string fk_emp)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhereLen(DKDirAttr.GradeNo, "=", 2, DBType.SQL2000);
            qo.addAnd();
            qo.AddWhere(DKDirAttr.FK_Emp, fk_emp);

            qo.addOrderBy(DKDirAttr.GradeNo);
            return qo.DoQuery();
        }

        /// <summary>
        /// �����һ���ڵ㣨���������ڵ㣩
        /// </summary>
        /// <param name="fk_emp">�û���</param>
        /// <param name="pNo">GradeNo</param>
        /// <returns></returns>
        public int ReNextChild(string fk_emp, string pNo)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(DKDirAttr.GradeNo, " LIKE ", pNo + "%");
            qo.addAnd();
            qo.AddWhereLen(DKDirAttr.GradeNo, "=", pNo.Length + 2,
                DBType.SQL2000);
            qo.addAnd();
            qo.AddWhere(DKDirAttr.FK_Emp, fk_emp);
            qo.addOrderBy(DKDirAttr.GradeNo);
            return qo.DoQuery();
        }

        /// <summary>
        /// ��ȡ���������ӽڵ㣨�������ڵ㣩��
        /// </summary>
        /// <param name="fk_emp">�û���</param>
        /// <param name="pNo">GradeNo</param>
        /// <returns></returns>
        public int ReHisChilds(string fk_emp, string pNo)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(DKDirAttr.GradeNo, " LIKE ", pNo + "%");
            qo.addAnd();
            qo.AddWhere(DKDirAttr.FK_Emp, fk_emp);
            qo.addOrderBy(DKDirAttr.GradeNo);
            return qo.DoQuery();
        }
        #endregion
    }
}
 

