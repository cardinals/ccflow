using System;
using System.Collections;
using BP.DA;
using BP.En;


namespace BP.TA
{
	/// <summary>
	/// ����
	/// </summary>
    public class WorkTemplateType : SimpleNoNameFix
    {
        #region ʵ�ֻ����ķ���
        /// <summary>
        /// PhysicsTable
        /// </summary>
        public override string PhysicsTable
        {
            get
            {
                return "TA_WorkTemplateType";
            }
        }
        /// <summary>
        /// Desc
        /// </summary>
        public override string Desc
        {
            get
            {
                return "����";
            }
        }
        #endregion

        #region ���췽��
        /// <summary>
        /// ����
        /// </summary>
        public WorkTemplateType() { }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="_No">���</param>
        public WorkTemplateType(string _No) : base(_No) { }
        #endregion
    }
	 
	/// <summary>
	/// ����s
	/// </summary>
	public class WorkTemplateTypes :SimpleNoNameFixs
	{
		#region ����
		/// <summary>
		/// ����s
		/// </summary>
		public WorkTemplateTypes(){}
		/// <summary>
		/// ����
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new WorkTemplateType();
			}
		}
		#endregion
	}
}
