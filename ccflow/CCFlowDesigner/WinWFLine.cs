using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Collections;
using BP.Win.Controls;
using BP.WF;

namespace BP.Win.WF
{
	/// <summary>
	/// WinWFLine ��ժҪ˵����
	/// </summary>
	public class WinWFLine : WFLineBase , IPaint
	{
		#region ���캯��
		public WinWFLine()
		{
		}
		public WinWFLine(WinWFNode begin,WinWFNode end )
		{
			this._NodeBegin = begin;
			this._NodeEnd = end;
			this.Text = begin.Text +"��"+end.Text;
		}
		#endregion

		#region ��������
		
		public Direction HisDirection =null;

		private WinWFNode _NodeBegin = null;
		public WinWFNode NodeBegin
		{
			get 
			{
				return this._NodeBegin; 
			}
			set 
			{
				this._NodeBegin = value; 
			}
		}
		private WinWFNode _NodeEnd = null;
		public WinWFNode NodeEnd
		{
			get 
			{
				return this._NodeEnd; 
			}
			set
			{
				this._NodeEnd = value; 
			}
		}

		#endregion 

		#region ��ƫ��
		/// <summary>
		/// ��ȡƫ�ƺ�Ŀ�ʼ��������
		/// </summary>
		protected override Point GetPoint( bool end )
		{
			Point po = this._NodeBegin.CenterPoint;//   Center of _NodeBegin
			Rectangle rect =  this._NodeBegin.Bounds;

			Point p1 = this._NodeBegin.CenterPoint;
			Point p2 = this._NodeEnd.CenterPoint;
			if( end )
			{
				po = this._NodeEnd.CenterPoint;//   Center of _NodeEnd
				rect =  this._NodeEnd.Bounds;

				p1 = this._NodeEnd.CenterPoint;
				p2 = this._NodeBegin.CenterPoint;
			}
			return this.GetArrowPoint( po ,p1 ,p2 ,rect );
		}
		#endregion ��ƫ��

	}

	#region �ߵļ���
	public class WinLines : CollectionBase
	{
		public Control Parent = null;
		public WinLines()
		{
		}
		public WinWFLine this[int index]
		{
			get
			{
				return this.InnerList[index] as WinWFLine;
			}
		}
		public bool Contains(string name)
		{
			foreach(WinWFLine line in this)
			{
				if(line.Name == name)
					return true;
			}
			return false;
		}
		/// <summary>
		/// ����һ����
		/// </summary>
		/// <param name="node1"></param>
		/// <param name="node2"></param>
		public void AddLine(WinWFNode node1 , WinWFNode node2)
		{
			WinWFLine line = new WinWFLine(node1 , node2);
			line.Parent = this.Parent;
			this.AddLine( line );
		}
		public void AddLine(WinWFLine line)
		{
			line.Parent = this.Parent;
			this.InnerList.Add( line );
		}
		public void RemoveLine(WinWFLine line)
		{
			if(line.HisDirection!=null)
			{
				try
				{
					line.HisDirection.Delete();
				}
				catch{}
			}
			this.InnerList.Remove(line);
		}
		public void DeleteLinesByWinNode(WinWFNode node)
		{
			int count = this.Count;
			for(int i=0; i<count ;i++)
			{
				WinWFLine line = this[i];
				if(line.NodeBegin.Name == node.Name ||line.NodeEnd.Name == node.Name)
				{
					this.RemoveLine( this.InnerList[i] as WinWFLine);
					i--;
					count--;
				}
			}
		}
	}
	#endregion

}
