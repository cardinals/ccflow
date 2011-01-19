using System;
using System.Collections;
using BP.DA;
using BP.En;

namespace BP.Pub
{
	/// <summary>
	/// ��
	/// </summary>
    public class Week : SimpleNoNameFix
    {
        #region ʵ�ֻ����ķ�����
        /// <summary>
        /// �����
        /// </summary>
        public override string PhysicsTable
        {
            get
            {
                return "Pub_Week";
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        public override string Desc
        {
            get
            {
                return this.ToE("Week", "��"); // "����";
            }
        }
        #endregion

        #region ���췽��
        public Week() { }
        public Week(string _No) : base(_No) { }
        #endregion
    }
	/// <summary>
    /// ��s
	/// </summary>
    public class Weeks : SimpleNoNameFixs
    {
        /// <summary>
        /// �ܼ���
        /// </summary>
        public Weeks()
        {
        }
        /// <summary>
        /// �õ����� Entity 
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new Week();
            }
        }
    }
}
