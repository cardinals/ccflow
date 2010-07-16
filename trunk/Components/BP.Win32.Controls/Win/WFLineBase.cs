using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Collections;

namespace BP.Win.Controls
{
	/// <summary>
	/// WFLineBase ��ժҪ˵����
	/// </summary>
	public class WFLineBase :IPaint
	{
		#region ���캯��
		public WFLineBase()
		{
		}
		public WFLineBase(Point begin,Point end )
		{
			this.BeginPoint = begin;
			this.EndPoint = end;
		}
		#endregion

		#region ��������
		private string _text="WFLineBase";
		public string Text
		{
			get
			{
				return this._text;
			}
			set
			{
				this._text = value;
			}
		}
		private string _name="";
		public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				this._name = value;
			}
		}
		private bool _focused = false;
		public  bool Focused 
		{
			get
			{
				return this._focused;
			}
			set
			{
				this._focused = value;
				if(this.Parent!=null)
					this.Parent.Refresh();
			}
		}
		public Control  Parent = null;
		public void Focus()
		{
			this.Focused = true;
		}
		public void LostFocus()
		{
			this.Focused = false;
		}

		#endregion 

		#region ����
		/// <summary>
		/// ��ȡ��poΪ���ĵģ�loc���ڵ�����
		/// </summary>
		public  int GetQuadrant( Point po , Point loc )
		{
			if( po.X==loc.X && po.Y==loc.Y)
				return 0;
			int xx = loc.X - po.X ;
			int yy = loc.Y - po.Y ;
			if( xx==0 )
			{
				if( yy>0 )
					return 5;
				else
					return 13;
			}
			else if( yy==0)
			{
				if( xx>0 )
					return 1;
				else
					return 9;
			}
			else
			{
				if( xx>0 )
				{
					if(yy>0) // ++ ,1
					{
						if( xx>yy)
							return 2;
						else if( xx==yy)
							return 3;
						else 
							return 4;
					}
					else // +- 4
					{
						if( xx>Math.Abs(yy))
							return 16;
						else if( xx==Math.Abs(yy))
							return 15;
						else 
							return 14;
					}
				}
				else
				{
					if(yy>0) //-+ 2
					{
						if( Math.Abs(xx)>yy)
							return 8;
						else if( Math.Abs(xx)==yy)
							return 7;
						else 
							return 6;
					}
					else // -- 3
					{
						if( xx>yy)
							return 12;
						else if( xx==yy)
							return 11;
						else 
							return 10;
					}
				}
			}
		}
		#endregion 

		#region ��ƫ��

		protected  virtual  Point GetPoint( bool end )
		{
			return Point.Empty;
		}
		/// <summary>
		/// ��ȡƫ�ƺ�Ŀ�ʼ���������Ի滭��ͷ
		/// </summary>
		public Point GetArrowPoint( Point pt0 , Point begin ,Point end ,Rectangle centerofpt0 )
		{
			Point po = pt0;
			Point p1 = begin;
			Point p2 = end;
			Rectangle rect = centerofpt0;

			int xx = p2.X-p1.X ;
			int yy = p2.Y-p1.Y ;
			double angle = Math.Atan2( yy,xx);

			#region // X ��
			if(yy==0)//yy==0 
			{
				if( xx> 0) 
					p1.Offset( rect.Width/2,0);// ����ƫ��
				else
					p1.Offset( -rect.Width/2,0);// ����ƫ��
			}
				#endregion // X ��

				#region  // Y ��
			else if(xx==0) //xx==0 // Y ��
			{
				if( yy> 0)
					p1.Offset( 0,rect.Height/2); // ����ƫ��
				else
					p1.Offset( 0,-rect.Height/2);// ����ƫ��
			}
				#endregion  // Y ��

				#region  // б��
			else
			{
				if( xx>0 )
				{
					if(yy>0) // ++ ,1
					{
						//po.Offset( rect.Width/2 , rect.Height/2 );
						po.Offset( rect.Width/2 , rect.Height/2 );
						xx = po.X-p1.X ;// p1 center
						yy = po.Y-p1.Y ;
						double rightbottom = Math.Atan2( yy,xx);
						if( angle > rightbottom)
							p1.Offset( 0,rect.Height/2); // ����ƫ��
						else
							p1.Offset( rect.Width/2,0);// ����ƫ��
					}
					else // +- 4
					{
						po.Offset( rect.Width/2 , -rect.Height/2);
						xx = po.X-p1.X ;// p1 center
						yy = po.Y-p1.Y ;
						double righttop = Math.Atan2( yy,xx);
						if( angle < righttop)
							p1.Offset( 0,-rect.Height/2);// ����ƫ��
						else
							p1.Offset( rect.Width/2,0);// ����ƫ��
					}
				}
				else
				{
					if(yy>0) //-+ 2
					{
						po.Offset( -rect.Width/2 , rect.Height/2);
						xx = po.X-p1.X ;// p1 center
						yy = po.Y-p1.Y ;
						double leftbottom = Math.Atan2( yy,xx);
						if( angle < leftbottom)
							p1.Offset( 0,rect.Height/2); // ����ƫ��
						else
							p1.Offset( -rect.Width/2,0);// ����ƫ��
					}
					else // -- 3
					{
						po.Offset( -rect.Width/2 , -rect.Height/2);
						xx = po.X-p1.X ;// p1 center
						yy = po.Y-p1.Y ;
						double lefttop = Math.Atan2( yy,xx);
						if( angle < lefttop)
							p1.Offset( -rect.Width/2,0);// ����ƫ��
						else
							p1.Offset( 0,-rect.Height/2);// ����ƫ��
					}
				}
			}
			#endregion  // б��

			return p1;
		}

		#endregion ��ƫ��
		private   Point _beginPoint = new Point( 0, 0);
		protected Point BeginPoint
		{
			get
			{
				return _beginPoint;
			}
			set
			{
				this._beginPoint = value ;
			}
		}
		private   Point _endPoint = new Point( 10, 10);
		protected Point EndPoint 
		{
			get
			{
				return _endPoint;
			}
			set
			{
				this._endPoint = value ;
			}
		}
		
		#region �滭����
		
		private int _arrowlen = 11;//��ͷ��ռ����
		private int _arrowwidth = 3; //��ͷ�����
		void IPaint.Paint(System.Windows.Forms.PaintEventArgs e)
		{

			Point p1 = this.GetPoint( false );
			Point p2 = this.GetPoint( true );
			if( p1==Point.Empty || p2==Point.Empty )
			{
				throw new Exception( "���ܻ滭ֵΪ�յĵ㣡");
			}
			if( p1.X == p2.X && p1.Y == p2.Y )
			{
				p2.Offset(0 ,50);
			}

			//LinearGradientBrush lbrush = new LinearGradientBrush( p1 ,p2 ,Color.Yellow,Color.Black);
			LinearGradientBrush lbrush = new LinearGradientBrush( p1 ,p2 ,Color.Yellow,Color.Black);

			//SolidBrush lbrush = new SolidBrush(Color.Red);
			Pen pen = new Pen( lbrush );
			if( this.Focused ) 
				pen.Width = 3;
			else
				pen.Width = 1;

			e.Graphics.DrawLine( pen , p1 , p2);

			int xx = p2.X-p1.X ;
			int yy = p2.Y-p1.Y ;
			double angle = Math.Atan2(yy ,xx );

			xx = Convert.ToInt32(this._arrowlen * Math.Cos( angle )) +1;
			yy = Convert.ToInt32(this._arrowlen * Math.Sin( angle )) +1;
			Point p22 = p2;
			p22.Offset( -xx , -yy );
			xx = Convert.ToInt32(this._arrowwidth * Math.Sin( angle )) ;
			yy = Convert.ToInt32(this._arrowwidth * Math.Cos( angle )) ;

			p1 = p22;
			p1.Offset( 1+xx , -yy );
			e.Graphics.DrawLine( pen , p1 , p2);

			p1 = p22;
			p1.Offset( 1-xx , yy );
			e.Graphics.DrawLine( pen , p1 , p2);

			lbrush.Dispose();
			pen.Dispose();
		}
		#endregion
	}

	#region �ߵļ���
	public class WFLineBases : CollectionBase
	{
		public Control Parent = null;
		public WFLineBases()
		{
		}
		public WFLineBase this[int index]
		{
			get
			{
				return this.InnerList[index] as WFLineBase;
			}
		}
		public bool Contains(string name)
		{
			foreach(WFLineBase line in this)
			{
				if(line.Name == name)
					return true;
			}
			return false;
		}

		public void AddLine(WFLineBase line)
		{
			line.Parent = this.Parent;
			this.InnerList.Add( line );
		}

	}
	#endregion

}
