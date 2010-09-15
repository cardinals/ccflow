using System;
using System.Collections;
using BP.DA;
using BP.En;

namespace BP.GE
{
    /// <summary>
    /// ��Ϣ����
    /// </summary>
    public class Info1Attr : InfoBaseAttr
    {
    }
    /// <summary>
    /// ��Ϣ����
    /// </summary>
    public class Info1 : InfoBase
    {
        #region ����
        /// <summary>
        /// ���ݿ�����
        /// </summary>
        public override string PTable
        {
            get
            {
                return "GE_Info1";
            }
        }
        /// <summary>
        /// ���ݿ����ʵ����
        /// </summary>
        public override string SortEntity
        {
            get
            {
                return "BP.GE.InfoSort1";
            }
        }
        /// <summary>
        /// ���DDLEntitees
        /// </summary>
        public override EntitiesNoName SortDDLEntities
        {
            get
            {
                return new InfoSort1s();
            }
        }

        /// <summary>
        /// ��һƪ����(ĳһ�����)
        /// </summary>
        public Info1 PreviousInfo
        {
            get
            {
                Info1 en = new Info1();
                QueryObject qo = new QueryObject(en);
                qo.AddWhere(Info1Attr.FK_Sort, this.FK_Sort);
                qo.addAnd();
                qo.AddWhere(Info1Attr.No, ">", this.No);
                qo.addAnd();
                qo.AddWhereNotIn(Info1Attr.InfoSta, "0");
                qo.Top = 1;
                qo.addOrderBy(Info1Attr.No);
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
        public Info1 NextInfo
        {
            get
            {
                Info1 en = new Info1();
                QueryObject qo = new QueryObject(en);
                qo.AddWhere(Info1Attr.FK_Sort, this.FK_Sort);
                qo.addAnd();
                qo.AddWhere(Info1Attr.No, "<", this.No);
                qo.addAnd();
                qo.AddWhereNotIn(Info1Attr.InfoSta, "0");
                qo.Top = 1;
                qo.addOrderByDesc(Info1Attr.No);
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
        public Info1 PreviousInfoOfAll
        {
            get
            {
                Info1 en = new Info1();
                QueryObject qo = new QueryObject(en);
                qo.AddWhere(Info1Attr.No, ">", this.No);
                qo.addAnd();
                qo.AddWhereNotIn(Info1Attr.InfoSta, "0");
                qo.Top = 1;
                qo.addOrderBy(Info1Attr.No);
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
        public Info1 NextInfoOfAll
        {
            get
            {
                Info1 en = new Info1();
                QueryObject qo = new QueryObject(en);
                qo.AddWhere(Info1Attr.No, "<", this.No);
                qo.addAnd();
                qo.AddWhereNotIn(Info1Attr.InfoSta, "0");
                qo.Top = 1;
                qo.addOrderByDesc(Info1Attr.No);
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
        public Info1(string no)
            : base(no)
        {

        }
        /// <summary>
        /// ��Ϣ����
        /// </summary>
        public Info1()
        {
        }
        #endregion
    }
    /// <summary>
    /// ��Ϣ����s
    /// </summary>
    public class Info1s : InfoBases
    {
        /// <summary>
        /// ��Ϣ����s
        /// </summary>
        public Info1s()
        {
        }
        /// <summary>
        /// �õ����� Entity
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new Info1();
            }
        }
    }
}
