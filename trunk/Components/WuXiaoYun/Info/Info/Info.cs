using System;
using System.Collections;
using BP.DA;
using BP.En;

namespace BP.GE
{
    /// <summary>
    /// ��Ϣ����
    /// </summary>
    public class InfoAttr : InfoBaseAttr
    {
    }
    /// <summary>
    /// ��Ϣ����
    /// </summary>
    public class Info : InfoBase
    {
        #region ����
        /// <summary>
        /// ���ݿ�����
        /// </summary>
        public override string PTable
        {
            get
            {
                return "GE_Info";
            }
        }
        /// <summary>
        /// ���ݿ����ʵ����
        /// </summary>
        public override string SortEntity
        {
            get
            {
                return "BP.GE.InfoSort";
            }
        }
        /// <summary>
        /// ���DDLEntitees
        /// </summary>
        public override EntitiesNoName SortDDLEntities
        {
            get
            {
                return new InfoSorts();
            }
        }

        /// <summary>
        /// ��һƪ����(ĳһ�����)
        /// </summary>
        public Info PreviousInfo
        {
            get
            {
                Info en = new Info();
                QueryObject qo = new QueryObject(en);
                qo.AddWhere(InfoAttr.FK_Sort, this.FK_Sort);
                qo.addAnd();
                qo.AddWhere(InfoAttr.No, ">", this.No);
                qo.addAnd();
                qo.AddWhereNotIn(InfoAttr.InfoSta, "0");
                qo.Top = 1;
                qo.addOrderBy(InfoAttr.No);
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
        public Info NextInfo
        {
            get
            {
                Info en = new Info();
                QueryObject qo = new QueryObject(en);
                qo.AddWhere(InfoAttr.FK_Sort, this.FK_Sort);
                qo.addAnd();
                qo.AddWhere(InfoAttr.No, "<", this.No);
                qo.addAnd();
                qo.AddWhereNotIn(InfoAttr.InfoSta, "0");
                qo.Top = 1;
                qo.addOrderByDesc(InfoAttr.No);
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
        public Info PreviousInfoOfAll
        {
            get
            {
                Info en = new Info();
                QueryObject qo = new QueryObject(en);
                qo.AddWhere(InfoAttr.No, ">", this.No);
                qo.addAnd();
                qo.AddWhereNotIn(InfoAttr.InfoSta, "0");
                qo.Top = 1;
                qo.addOrderBy(InfoAttr.No);
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
        public Info NextInfoOfAll
        {
            get
            {
                Info en = new Info();
                QueryObject qo = new QueryObject(en);
                qo.AddWhere(InfoAttr.No, "<", this.No);
                qo.addAnd();
                qo.AddWhereNotIn(InfoAttr.InfoSta, "0");
                qo.Top = 1;
                qo.addOrderByDesc(InfoAttr.No);
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
        public Info(string no)
            : base(no)
        {

        }
        /// <summary>
        /// ��Ϣ����
        /// </summary>
        public Info()
        {
        }
        #endregion
    }
    /// <summary>
    /// ��Ϣ����s
    /// </summary>
    public class Infos : InfoBases
    {
        /// <summary>
        /// ��Ϣ����s
        /// </summary>
        public Infos()
        {
        }
        /// <summary>
        /// �õ����� Entity
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new Info();
            }
        }
    }
}
