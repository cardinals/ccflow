using System;
using System.Collections.Generic;
using System.Text;

namespace Tax666.WebControls
{
    #region Ȩ������
    /// <summary>
    /// Ȩ������
    /// </summary>
    public enum PopedomType
    {
        /// <summary>
        /// ���
        /// </summary>
        List = 2,
        /// <summary>
        /// ���
        /// </summary>
        New = 4,
        /// <summary>
        /// �༭
        /// </summary>
        Edit = 8,
        /// <summary>
        /// ɾ��
        /// </summary>
        Delete = 16,
        /// <summary>
        /// ����
        /// </summary>
        Orderby = 32,
        /// <summary>
        /// ��ӡ
        /// </summary>
        Print = 64,
        /// <summary>
        /// ��Ч/��Ч
        /// </summary>
        Valid = 128,
        /// <summary>
        /// ���
        /// </summary>
        Audit = 256,
        /// <summary>
        /// ����
        /// </summary>
        Search = 512,
        /// <summary>
        /// �ƶ�
        /// </summary>
        Move = 1024,
        /// <summary>
        /// ����
        /// </summary>
        Download = 2048,
        /// <summary>
        /// �ϴ�
        /// </summary>
        Upload = 4096,
        /// <summary>
        /// ����
        /// </summary>
        Output = 8192,
        /// <summary>
        /// ����
        /// </summary>
        Input = 16384,
        /// <summary>
        /// ����
        /// </summary>
        Report = 32768,
        /// <summary>
        /// ����
        /// </summary>
        Backup = 65536,
        /// <summary>
        /// �ָ�
        /// </summary>
        Restore = 131072,
        /// <summary>
        /// ��Ȩ
        /// </summary>
        Grint = 262144,
        /// <summary>
        /// ����
        /// </summary>
        Other = 1
    }
    #endregion
}
