using System;
using System.Data;
using BP.DA;
using BP.En;

namespace BP.GTS
{
	
	/// <summary>
	/// �ʴ������
	/// </summary>
	public class WorkVSEssayQuestionAttr  :WorkVSBaseAttr
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
	public class WorkVSEssayQuestion :WorkVSBase
	{
		#region ��������
		/// <summary>
		///�ʴ���
		/// </summary>
		public string FK_EssayQuestion
		{
			get
			{
				return this.GetValStringByKey(WorkVSEssayQuestionAttr.FK_EssayQuestion);
			}
			set
			{
				SetValByKey(WorkVSEssayQuestionAttr.FK_EssayQuestion,value);
			}
		}		  
		#endregion

		#region ��չ����
		 
		#endregion		

		#region ���캯��
		/// <summary>
		/// �ʴ���������
		/// </summary> 
		public WorkVSEssayQuestion()
		{
		}
		public WorkVSEssayQuestion(string Work,string Equestion)
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
				
				Map map = new Map("GTS_WorkVSEssayQuestion");
				map.EnDesc="�ʴ���������";	
				map.EnType=EnType.Dot2Dot;
		 
				map.AddDDLEntitiesPK(WorkVSEssayQuestionAttr.FK_Work,"0001","�Ծ�",new WorkFixDesigns(),false);
				map.AddDDLEntitiesPK(WorkVSEssayQuestionAttr.FK_EssayQuestion,null,"�ʴ���",new EssayQuestions(),false);
				map.AddTBDecimal(WorkVSEssayQuestionAttr.Cent,1,"��",true,false);

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
	public class WorkVSEssayQuestions : WorkVSBases
	{
		#region ����
		/// <summary>
		/// �ʴ������
		/// </summary>
		public WorkVSEssayQuestions(){}
		#endregion

		#region ����
		/// <summary>
		/// �õ����� Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new WorkVSEssayQuestion();
			}
		}	
		#endregion 

		#region ��ѯ����
		 
		#endregion
	}
	
}
