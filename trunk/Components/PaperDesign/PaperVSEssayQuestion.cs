using System;
using System.Data;
using BP.DA;
using BP.En;

namespace BP.GTS
{
	
	/// <summary>
	/// �ʴ������
	/// </summary>
	public class PaperVSEssayQuestionAttr  
	{
		#region ��������
		/// <summary>
		/// �Ծ�
		/// </summary>
		public const  string FK_Paper="FK_Paper";
		/// <summary>
		/// �ʴ���
		/// </summary>
		public const  string FK_EssayQuestion="FK_EssayQuestion";
		/// <summary>
		/// ����
		/// </summary>
		public const  string Cent="Cent";
		#endregion	
	}
	/// <summary>
	/// �ʴ������ ��ժҪ˵����
	/// </summary>
	public class PaperVSEssayQuestion :Entity
	{
		#region ��������
		/// <summary>
		/// HisUAC
		/// </summary>
		public override UAC HisUAC
		{
			get
			{
				UAC uc = new UAC();
				uc.OpenForSysAdmin();
               // uc.IsInsert = false;
              //  uc.IsDelete = false;
				return uc;
			}
		}

		/// <summary>
		///�ʴ���
		/// </summary>
		public string FK_EssayQuestion
		{
			get
			{
				return this.GetValStringByKey(PaperVSEssayQuestionAttr.FK_EssayQuestion);
			}
			set
			{
				SetValByKey(PaperVSEssayQuestionAttr.FK_EssayQuestion,value);
			}
		}		  
		/// <summary>
		///���
		/// </summary>
		public string FK_Paper
		{
			get
			{
				return this.GetValStringByKey(PaperVSEssayQuestionAttr.FK_Paper);
			}
			set
			{
				SetValByKey(PaperVSEssayQuestionAttr.FK_Paper,value);
			}
		}
		/// <summary>
		/// ����
		/// </summary>
		public decimal Cent
		{
			get
			{
				return this.GetValDecimalByKey(PaperVSEssayQuestionAttr.Cent);
			}
			set
			{
				SetValByKey(PaperVSEssayQuestionAttr.Cent,value);
			}
		}
		#endregion

		#region ��չ����
		 
		#endregion		

		#region ���캯��
		/// <summary>
		/// �ʴ���������
		/// </summary> 
		public PaperVSEssayQuestion()
		{
		}
		public PaperVSEssayQuestion(string paper,string Equestion)
		{
			this.FK_Paper = paper;
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
				
				Map map = new Map("GTS_PaperVSEssayQuestion");
				map.EnDesc="�ʴ���������";	
				map.EnType=EnType.Dot2Dot;
		 
				map.AddDDLEntitiesPK(PaperVSEssayQuestionAttr.FK_Paper,"0001","�Ծ�",new Papers(),false);
				map.AddDDLEntitiesPK(PaperVSEssayQuestionAttr.FK_EssayQuestion,null,"�ʴ���",new EssayQuestions(),false);
				map.AddTBDecimal(PaperVSEssayQuestionAttr.Cent,5,"��",true,false);

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
	public class PaperVSEssayQuestions : Entities
	{
		#region ����
		/// <summary>
		/// �ʴ������
		/// </summary>
		public PaperVSEssayQuestions(){}
		#endregion

		#region ����
		/// <summary>
		/// �õ����� Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new PaperVSEssayQuestion();
			}
		}	
		#endregion 

		#region ��ѯ����
		 
		#endregion
	}
	
}
