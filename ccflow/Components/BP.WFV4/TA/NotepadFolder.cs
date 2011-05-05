using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.Web;

namespace BP.TA
{
	/// <summary>
	/// ���±�����
	/// </summary>
	public class NotepadFolderAttr:EntityOIDNameAttr
	{
		/// <summary>
		/// ��¼��
		/// </summary>
		public const string Recorder="Recorder";
		/// <summary>
		/// ��¼����
		/// </summary>
		public const string RDT="RDT";
	}
	/// <summary>
	/// ���±�
	/// </summary> 
	public class NotepadFolder : EntityOIDName
	{
		#region ��������
		public string Recorder
		{
			get
			{
				return this.GetValAppDateByKey(NotepadFolderAttr.Recorder); 
			}
			set
			{
				SetValByKey(NotepadFolderAttr.Recorder,value);
			}
		}
		#endregion
 
		#region ���캯��
		public override UAC HisUAC
		{
			get
			{
				UAC uac = new UAC();
				uac.OpenAll();
				return uac;
			}
		}

		/// <summary>
		/// ���±�
		/// </summary>
		public NotepadFolder()
		{
		  
		}
		/// <summary>
		/// ���±�
		/// </summary>
		/// <param name="_No">No</param>
		public NotepadFolder(int oid):base(oid)
		{
		}
		/// <summary>
		/// Map
		/// </summary>
		public override Map EnMap
		{
			get
			{
				if (this._enMap!=null) 
					return this._enMap;

				Map map = new Map("TA_NotepadFolder");
				map.EnDesc="���±��ļ���";
				map.Icon="../TA/Images/Folder.ico";
				//map.Icon="../TA/Images/log_s.ico";

				map.AddTBIntPKOID();

				map.AddTBString(NotepadFolderAttr.Name,null,"�½��ļ���1",true,false,0,4000,300);
				map.AddTBString(NotepadFolderAttr.Recorder,WebUser.No,"��¼��",false,false,0,4000,15);
				map.AttrsOfSearch.AddHidden(NotepadFolderAttr.Recorder,"=",Web.WebUser.No);
				this._enMap=map;
				return this._enMap;
			}
		}
		#endregion 

	}
	/// <summary>
	/// ���±�s
	/// </summary> 
	public class NotepadFolders: EntitiesOIDName
	{
		public override int RetrieveAll()
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere( NotepadFolderAttr.Recorder, WebUser.No);
			return qo.DoQuery();
		}

		/// <summary>
		/// ��ȡentity
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new NotepadFolder();
			}
		}
		/// <summary>
		/// NotepadFolders
		/// </summary>
		public NotepadFolders()
		{
		}
		/// <summary>
		/// NotepadFolders
		/// </summary>
		public NotepadFolders(string Recorder)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(NotepadFolderAttr.Recorder, Recorder);
			qo.DoQuery();			
		}
	}
}
 