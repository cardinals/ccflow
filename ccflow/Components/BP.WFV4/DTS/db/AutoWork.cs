using System;
using System.Data;
using System.Collections;
using BP.En;
using BP.En.Base;
using BP.DA;
using BP.DTS;
using BP.Pub;
using BP.Tax;
using BP.Web;
using BP.WF;


namespace BP.WF
{
	/// <summary>
	/// �Զ���������
	/// </summary>
	public class AutoWork:DataIOEn
	{
		public static bool IsDoToday
		{
			get
			{
				string val= System.Web.HttpContext.Current.Application["Today"] as string ;
				if ( val== System.DateTime.Now.ToString("yyyyMMdd") )
				{
					return true;
				}
				else
				{
					System.Web.HttpContext.Current.Application["Today"]=System.DateTime.Now.ToString("yyyyMMdd");
					return false;
				}
			}
		}
		/// <summary>
		/// �Զ���������
		/// </summary>
		public AutoWork()
		{
			this.HisDoType=DoType.Especial;
			this.Title="�Զ���������";
			this.HisRunTimeType=RunTimeType.Day;
			this.HisUserType = Web.UserType.AppAdmin;
			this.DefaultEveryMonth="00";
			this.DefaultEveryDay="00";
			this.DefaultEveryHH="10";
			this.DefaultEveryMin="01";
		}
		/// <summary>
		/// �Զ���������
		/// </summary>
		public override void Do()
		{
			if (AutoWork.IsDoToday )
				return ;

			//Emp myemp = new Emp(WebUser.No);
			Log.DebugWriteInfo("���ȹ����Զ���ʼ**************");

			//�����й��м�ڵ������(������������϶������һ���ڵ�)
			ArrayList als = DA.ClassFactory.GetObjects("BP.WF.PCWorks");
			foreach(PCWorks wks in als)
				wks.DoInitData();

			//�����йص�һ���ڵ�����ݣ�����߽����̵ĵ�һ���ڵ㣩
			als = DA.ClassFactory.GetObjects("BP.WF.PCStartWorks");
			//�߽����̣�
			foreach(PCStartWorks wks in als)
				wks.AutoGenerWorkFlow();

			als = DA.ClassFactory.GetObjects("BP.WF.PCTaxpayerStartWorks");
			foreach(PCStartWorks wks in als)
				wks.AutoGenerWorkFlow();

			Log.DebugWriteInfo("���ȹ����Զ�����**************");
			//TaxUser.SignInOfWFQH(myemp,false,true);
		}
	}
		 
}
