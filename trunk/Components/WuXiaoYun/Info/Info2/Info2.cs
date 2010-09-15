using System;
using System.Collections;
using BP.DA;
using BP.En;

namespace BP.GE
{
	/// <summary>
    /// ��Ϣ����
	/// </summary>
    public class Info2Attr : InfoBaseAttr
    {
    }
	/// <summary>
    /// ��Ϣ����
	/// </summary>
    public class Info2 : InfoBase
    {
        #region ����
        /// <summary>
        /// ���ݿ�����
        /// </summary>
        public override string PTable
        {
            get
            {
                return "GE_Info2";
            }
        }
        /// <summary>
        /// ���ݿ����ʵ����
        /// </summary>
        public override string SortEntity
        {
            get
            {
                return "BP.GE.InfoSort2";
            }
        }
        /// <summary>
        /// ���DDLEntitees
        /// </summary>
        public override EntitiesNoName SortDDLEntities
        {
            get
            {
                return new InfoSort2s();
            }
        }

        /// <summary>
        /// ��һƪ����(ĳһ�����)
        /// </summary>
        public Info2 PreviousInfo
        {
            get
            {
                Info2 en = new Info2();
                QueryObject qo = new QueryObject(en);
                qo.AddWhere(Info2Attr.FK_Sort, this.FK_Sort);
                qo.addAnd();
                qo.AddWhere(Info2Attr.No, ">", this.No);
                qo.addAnd();
                qo.AddWhereNotIn(Info2Attr.InfoSta, "0");
                qo.Top = 1;
                qo.addOrderBy(Info2Attr.No);
                qo.DoQuery();
                if (qo.GetCount() <= 0)
                {
                    return null;
                }
                return en;
            }
        }
        /// <summary>
        /// ��һƪ����(ĳһ�����)
        /// </summary>
        public Info2 NextInfo
        {
            get
            {
                Info2 en = new Info2();
                QueryObject qo = new QueryObject(en);
                qo.AddWhere(Info2Attr.FK_Sort, this.FK_Sort);
                qo.addAnd();
                qo.AddWhere(Info2Attr.No, "<", this.No);
                qo.addAnd();
                qo.AddWhereNotIn(Info2Attr.InfoSta, "0");
                qo.Top = 1;
                qo.addOrderByDesc(Info2Attr.No);
                qo.DoQuery();
                if (qo.GetCount() <= 0)
                {
                    return null;
                }
                return en;
            }
        }

        /// <summary>
        /// ��һƪ����(������Ϣ��)
        /// </summary>
        public Info2 PreviousInfoOfAll
        {
            get
            {
                Info2 en = new Info2();
                QueryObject qo = new QueryObject(en);
                qo.AddWhere(Info2Attr.No, ">", this.No);
                qo.addAnd();
                qo.AddWhereNotIn(Info2Attr.InfoSta, "0");
                qo.Top = 1;
                qo.addOrderBy(Info2Attr.No);
                qo.DoQuery();
                if (qo.GetCount() <= 0)
                {
                    return null;
                }
                return en;
            }
        }
        /// <summary>
        /// ��һƪ����(������Ϣ��)
        /// </summary>
        public Info2 NextInfoOfAll
        {
            get
            {
                Info2 en = new Info2();
                QueryObject qo = new QueryObject(en);
                qo.AddWhere(Info2Attr.No, "<", this.No);
                qo.addAnd();
                qo.AddWhereNotIn(Info2Attr.InfoSta, "0");
                qo.Top = 1;
                qo.addOrderByDesc(Info2Attr.No);
                qo.DoQuery();
                if (qo.GetCount() <= 0)
                {
                    return null;
                }
                return en;
            }
        }

         #endregion ����

        #region ���췽��
        /// <summary>
        /// ��Ϣ����
        /// </summary>
        public Info2(string no)
            : base(no)
        {

        }
        /// <summary>
        /// ��Ϣ����
        /// </summary>
        public Info2()
        {
        }
        #endregion
    }
	/// <summary>
    /// ��Ϣ����s
	/// </summary>
    public class Info2s : InfoBases
    {
        /// <summary>
        /// ��Ϣ����s
        /// </summary>
        public Info2s()
        {
        }
        /// <summary>
        /// �õ����� Entity
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new Info2();
            }
        }
    }
}
