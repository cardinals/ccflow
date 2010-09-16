using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.En.Base;

namespace BP.GTS
{
	
	/// <summary>
	/// �ʴ������
	/// </summary>
	public class WorkRandomVSEssayQuestionAttr  :WorkVSBaseAttr
	{
		#region ��������
		/// <summary>
		/// �ʴ���
		/// </summary>
		public const  string FK_EssayQuestion="FK_EssayQuestion";
		#endregion	
	}
	/// <summary>
	/// �ʴ������ ��ժҪ˵����
	/// </summary>
	public class WorkRandomVSEssayQuestion :WorkVSBase
	{
		#region ��������
		/// <summary>
		///�ʴ���
		/// </summary>
		public string FK_EssayQuestion
		{
			get
			{
				return this.GetValStringByKey(WorkRandomVSEssayQuestionAttr.FK_EssayQuestion);
			}
			set
			{
				SetValByKey(WorkRandomVSEssayQuestionAttr.FK_EssayQuestion,value);
			}
		}		  
		#endregion

		#region ��չ����
		 
		#endregion		

		#region ���캯��
		/// <summary>
		/// �ʴ���������
		/// </summary> 
		public WorkRandomVSEssayQuestion()
		{
		}
		public WorkRandomVSEssayQuestion(string Work,string Equestion)
		{
			this.FK_Work = Work;
			this.FK_EssayQuestion = Equestion;
			
			try
			{
				this.Retrieve();
			}
			catch
			{
				this.Insert();
			}
		}
		/// <summary>
		/// ��д���෽��
		/// </summary>
		public override Map EnMap
		{
			get
			{
				if (this._enMap!=null) 
					return this._enMap;
				
				Map map = new Map("GTS_WorkRandomVSEssayQuestion");
				map.EnDesc="�ʴ���������";	
				map.EnType=EnType.Dot2Dot;
		 
				map.AddDDLEntitiesPK(WorkRandomVSEssayQuestionAttr.FK_Work,"0001","�Ծ�",new WorkFixDesigns(),false);
				map.AddDDLEntitiesPK(WorkRandomVSEssayQuestionAttr.FK_EssayQuestion,null,"�ʴ���",new EssayQuestions(),false);
				map.AddTBInt(WorkRandomVSEssayQuestionAttr.Cent,1,"��",true,false);

				//map.AddSearchAttr(EmpDutyAttr.FK_Emp);
				//map.AddSearchAttr(EmpDutyAttr.FK_Duty);

				this._enMap=map;
				return this._enMap;
			}
		}
		#endregion

		#region ���ػ��෽��

		#endregion 
	
	}
	/// <summary>
	/// �ʴ������ 
	/// </summary>
	public class WorkRandomVSEssayQuestions : WorkVSBases
	{
		#region ����
		/// <summary>
		/// �ʴ������
		/// </summary>
		public WorkRandomVSEssayQuestions(){}
		#endregion

		#region ����
		/// <summary>
		/// �õ����� Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new WorkRandomVSEssayQuestion();
			}
		}	
		#endregion 

		#region ��ѯ����
		 
		#endregion
	}
	
}
