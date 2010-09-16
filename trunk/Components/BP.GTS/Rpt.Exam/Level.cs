using System;
using System.Collections;
using BP.DA;
using BP.En;

namespace BP.GTS
{
	
	/// <summary>
	/// �ȼ�
	/// </summary>
    public class Level : SimpleNoNameFix
    {

        #region ʵ�ֻ����ķ�����
        /// <summary>
        /// �����
        /// </summary>
        public override string PhysicsTable
        {
            get
            {
                return "GTS_Level";
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        public override string Desc
        {
            get
            {
                return "�ɼ��ȼ�";
            }
        }
        #endregion

        #region ���췽��
        public Level()
        {
        }
        public Level(string _No)
            : base(_No)
        {

        }
        #endregion
    }
	/// <summary>
	/// �ɼ��ȼ�
	/// </summary>
	public class Levels :SimpleNoNameFixs
	{
		/// <summary>
		/// �ȼ�����
		/// </summary>
		public Levels()
		{

		}
		/// <summary>
		/// �õ����� Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new Level();
			}
		}
	}
}
