

using System;
using System.Data;
using BP.DA;
using BP.En;

namespace BP.GE
{
    /// <summary>
    /// ����
    /// </summary>
    public class ImgLinkSortAttr : EntityNoNameAttr
    {
        public const string IsShowDtl = "IsShowDtl";
    }
	/// <summary>
	/// ���
	/// </summary>
    public class ImgLinkSort : EntityNoName
    {
        #region ���캯��
        /// <summary>
        /// ���
        /// </summary>
        public ImgLinkSort()
        {
        }
        public ImgLinkSort(string no)
        {
            this.No = no;
            try
            {
                this.Retrieve();
            }
            catch
            {

            }
        }
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;
                Map map = new Map("GE_ImgLinkSort");
                map.EnDesc = "���";
                map.TitleExt = " - <a href='Batch.aspx?EnsName=BP.GE.ImgLinks' >" + BP.Sys.EnsAppCfgs.GetValString("BP.GE.ImgLinks", "AppName") + "</a>";

                map.IsAutoGenerNo = false;
                map.IsAllowRepeatName = false; 
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.CodeStruct = "2";
                map.IsAutoGenerNo = true;

                map.AddTBStringPK(ImgLinkSortAttr.No, null, "���", true, true, 2, 2, 2);
                map.AddTBString(ImgLinkSortAttr.Name, null, "����", true, false, 0, 50, 300);
                //  map.AddBoolean(ImgLinkSortAttr.IsShowDtl,true, "���÷�", true,false);

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
	/// <summary>
	/// ��� 
	/// </summary>
    public class ImgLinkSorts : EntitiesNoName
    {
        /// <summary>
        /// ��ȡ���
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new ImgLinkSort();
            }
        }

        #region ���캯��
        /// <summary>
        /// ���
        /// </summary>
        public ImgLinkSorts()
        {
        }
        #endregion

    }
}
 

