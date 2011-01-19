using System; 
using System.Collections;
using BP.DA; 
using BP.Web.Controls;
using System.Reflection;
using BP.Port;


namespace BP.En
{
	/// <summary>
	/// Method ��ժҪ˵��
	/// </summary>
    public class MethodExample1 : Method
    {
        /// <summary>
        /// �����в����ķ���
        /// </summary>
        public MethodExample1()
        {
            this.Title = "�޸�����";
            this.Help = "�޸��Լ��ĵ�¼���룬Ϊ���������ݰ�ȫ�����������ڵ��޸����룬���벻Ҫ���ڼ򵥡�";
        }
        /// <summary>
        /// ����ִ�б���
        /// </summary>
        /// <returns></returns>
        public override void Init()
        {
            this.Warning = "��ȷ��Ҫִ����";
            HisAttrs.AddTBString("P1", null, "ԭ����", true, false, 0, 10, 10);
            HisAttrs.AddTBString("P2", null, "������", true, false, 0, 10, 10);
            HisAttrs.AddTBString("P3", null, "ȷ��", true, false, 0, 10, 10);
        }
        /// <summary>
        /// ��ǰ�Ĳ���Ա�Ƿ����ִ���������
        /// </summary>
        public override bool IsCanDo
        {
            get
            {
                return true;
            }
        }
        /// <summary>
        /// ִ��
        /// </summary>
        /// <returns>����ִ�н��</returns>
        public override object Do()
        {
            string p1 = this.GetValStrByKey("P1");
            string p2 = this.GetValStrByKey("P2");
            string p3 = this.GetValStrByKey("P3");

            if (p2 != p3)
                return "�����벻һ�¡�";

            Emp emp = new Emp();
            emp.No = BP.Web.WebUser.No;
            if (emp.Pass == p1)
            {
                emp.Update("Pass",p2);
                return "ִ�гɹ�����Ǻ����������롣";
            }
            else
            {
                return "�����벻��ȷ��";
            }
        }
    }
}
