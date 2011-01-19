namespace Tax666.Common
{
    #region ö������

    /// <summary>
    /// ���ݿ��������
    /// </summary>
    public enum DbProviderEnum
    {
        Access = 0,
        SqlServer = 1,
        Oracle = 2
    }

    /// <summary>
    /// QueryStringֵ�ļ��
    /// </summary>
    public enum CheckGetEnum
    {
        Int = 0,
        Safety = 1
    }

    /// <summary>
    /// ʹ�ñ�ǩ
    /// </summary>
    public enum UsingTagEnum
    {
        None = 0,
        Ubb = 1,
        Html = 2
    }

    /// <summary>
    /// ɾ���ļ�·������
    /// </summary>
    public enum DeleteFilePathType
    {
        /// <summary>
        /// ����·��
        /// </summary>
        PhysicsPath = 1,
        /// <summary>
        /// ����·��
        /// </summary>
        DummyPath = 2,
        /// <summary>
        /// ��ǰĿ¼
        /// </summary>
        NowDirectoryPath = 3
    }

    /// <summary>
    /// ��ȡ��ʽ
    /// </summary>
    public enum MethodType
    {
        /// <summary>
        /// Post��ʽ
        /// </summary>
        Post = 1,
        /// <summary>
        /// Get��ʽ
        /// </summary>
        Get = 2
    }

    /// <summary>
    /// ��ȡ��������
    /// </summary>
    public enum DataType
    {
        /// <summary>
        /// �ַ�
        /// </summary>
        Str = 1,
        /// <summary>
        /// ����
        /// </summary>
        Dat = 2,
        /// <summary>
        /// ����
        /// </summary>
        Int = 3,
        /// <summary>
        /// ������
        /// </summary>
        Long = 4,
        /// <summary>
        /// ˫����С��
        /// </summary>
        Double = 5,
        /// <summary>
        /// ֻ���ַ�������
        /// </summary>
        CharAndNum = 6,
        /// <summary>
        /// ֻ���ʼ���ַ
        /// </summary>
        Email = 7,
        /// <summary>
        /// ֻ���ַ������ֺ�����
        /// </summary>
        CharAndNumAndChinese = 8

    }

    /// <summary>
    /// ���������
    /// </summary>
    public enum DataTable_Action
    {
        /// <summary>
        /// ����
        /// </summary>
        Insert = 0,
        /// <summary>
        /// ����
        /// </summary>
        Update = 1,
        /// <summary>
        /// ɾ��
        /// </summary>
        Delete = 2
    }
    /// <summary>
    /// ��������
    /// </summary>
    public enum OnlineCountType
    {
        /// <summary>
        /// ����
        /// </summary>
        Cache = 0,
        /// <summary>
        /// ���ݿ�
        /// </summary>
        DataBase = 1
    }

    /// <summary>
    /// ��ʾIcon����
    /// </summary>
    public enum Icon_Type : byte
    {
        /// <summary>
        /// �����ɹ�
        /// </summary>
        OK,
        /// <summary>
        /// ������ʾ
        /// </summary>
        Alert,
        /// <summary>
        /// ����ʧ��
        /// </summary>
        Error
    }

    /// <summary>
    /// ��ť��������
    /// </summary>
    public enum UrlType : byte
    {
        /// <summary>
        /// ��������
        /// </summary>
        Href,
        /// <summary>
        /// JavaScript �ű�
        /// </summary>
        JavaScript,
        /// <summary>
        /// VBScript �ű�
        /// </summary>
        VBScript
    }

    #endregion
}
