using System;
using System.Windows.Forms;
using System.Collections; 
using System.Drawing;

namespace IOS.Configuration.TreeControl
{
	///<Authority>
	///this control is created by Syed Md. Abul Bashar Milton
	///Software Engineer 
	///<Version>
	///3.4.2.4
	///</Version> 
	
	///Last Modification Date : 04/09/2006(dd/MM/yyyy)
	///</Authority>
	/// <summary>
	/// This is Milton created Tree Node like Button Node which  contains MiButton
	/// </summary>
	public class XMLNode:ArrayList
	{

		#region member variable
	
		public ContainerNode NodeButton;
		public Button IndicatorButton;
		public bool _Expanded=false;
		public XMLNode _Parent=null;
		public Point StartPoint;
		public Point EndPoint;
		public Point _Location;
		private int _IndicatorButtonSeparator=15;
		private Image _IndicatorCloseImage;
		private Image _IndicatorOpenImage;
		public int Level;
		public XMLTree  _RootPanel;
		public Int32 _NodePosition=-1;
		private int ExpandCount=0;
		public XMLNode _RootNode;
		private int XLocation=0;
		private int YLocation=0;
		
		#endregion

		#region constructor

		public XMLNode()
		{
			
			NodeButton=new ContainerNode(this);
			NodeButton.Visible=false;
			IndicatorButton=new Button();
			IndicatorButton.Visible=false;
			IndicatorButton.BackColor=Color.White;
			IndicatorButton.TextAlign=ContentAlignment.MiddleLeft ;
			IndicatorButton.FlatStyle=FlatStyle.Popup ;
	

			IndicatorButton.Click+=new EventHandler(IndicatorButton_Click);
			NodeButton.Click+=new EventHandler(IndicatorButton_Click); 
//			//
			// TODO: Add constructor logic here
			//
		}
		#endregion
		#region public memeber function


		#region property

		public Int32 NodePosition
		{
			get
			{
				return _NodePosition;
			}
			set
			{
				_NodePosition=value;
			}

		}

		public XMLNode RootNode
		{
			get
			{
				return _RootNode;
			}
			set
			{
				_RootNode=value;
			}
		}

		public int IndicatorButtonSeparator
		{
			get
			{
				return _IndicatorButtonSeparator;
			}
			set
			{
				_IndicatorButtonSeparator=value;
			}
		}
		public Size NodeButtonSize
		{
			get
			{
				return NodeButton.Size ;
			}
			
		}

		public Size IndicatorButtonSize
		{
			get
			{
				return IndicatorButton.Size ;
			}
			
		}

		public XMLNode  Parent
		{
			get
			{
				return _Parent;
			}
			set
			{
				_Parent=value;

			}
		}

		public XMLTree RootPanel
		{
			get
			{
				return _RootPanel;
			}
			set
			{
				_RootPanel=value;
			}
		}
		public bool Expanded
		{
			get
			{
				return _Expanded;
			}
			set
			{
				_Expanded=value;
			}
		}

		public Image IndicatorOffImage
		{
			get
			{
				return _IndicatorCloseImage  ;
			}
			set
			{
				_IndicatorCloseImage =value;
			}
		}

		public Image IndicatorOnImage
		{
			get
			{
				return _IndicatorOpenImage  ;
			}
			set
			{
				_IndicatorOpenImage =value;
			}
		}
		#endregion
		
		#region function
		
		public void SetLevel(XMLNode btnNode)
		{
			
			foreach(XMLNode btn in btnNode)
			{
				btn.Level=btnNode.Level+1;
				SetLevel(btn);
			}
		}

		private void SetIndicatorButtonYLocation()
		{
			IndicatorButton.Location=new Point(IndicatorButton.Location.X, this.NodeButton.Location.Y+this.NodeButtonSize.Height/2-IndicatorButtonSize.Height /2); 
		}
		
		
		public void ChangeVerticalLocation(XMLNode btnNode,int spacing)
		{
			XLocation=btnNode.RootPanel.XRootLocation +btnNode.Level*(IndicatorButton.Width+this.RootPanel.NodeHorizontalSeparator);
			YLocation=YLocation+NodeButton.Height+RootPanel.NodeVerticalSeparator;
			btnNode.SetLocation(new Point(XLocation,YLocation));
			if(btnNode.Expanded==true)
			{
				foreach(XMLNode btn in btnNode)
				{
					ChangeVerticalLocation(btn,spacing);
				}
			}
		}


		public void CollapseNode(XMLNode btnNode)
		{
			foreach(XMLNode btn in btnNode)
			{
				
				btn.NodeButton.Visible=false;
				btn.IndicatorButton.Visible=false;
				ExpandCount+=1;
				CollapseNode(btn);
				
			}
		}

		public void Collapse()
		{
			Expanded=false;
			this.IndicatorButton.Image=this.IndicatorOffImage;
			ExpandCount=0;
			YLocation=this.NodeButton.Location.Y;
            CollapseNode(this);
			int spacing =ExpandCount *(this.RootPanel.NodeVerticalSeparator+this.NodeButton.Size.Height);
			spacing=-spacing;
			ChangeNextNodeLocation(this,spacing);
			

		}


		public void ExpandNode(XMLNode btnNode)
		{
			for(int i=0;i<btnNode.Count;i++)
			{
				XMLNode btn=(XMLNode)btnNode[i];
				btn.NodeButton.Visible=true;
				if(btn.Count>0)
					btn.IndicatorButton.Visible=true;
				XLocation=btnNode.RootPanel.XRootLocation+btn.Level*(IndicatorButton.Width+this.RootPanel.NodeHorizontalSeparator);
				YLocation=YLocation+NodeButton.Height+RootPanel.NodeVerticalSeparator;
				btn.SetLocation(new Point(XLocation,YLocation));
				if(btn.Expanded==true)
				ExpandNode(btn);
				ExpandCount+=1;
			}

		}
		public void Expand()
		{
			Expanded=true;
			this.IndicatorButton.Image=this.IndicatorOnImage;
			ExpandCount=0;
			YLocation=this.NodeButton.Location.Y;
			ExpandNode(this);

			int spacing =ExpandCount*(this.RootPanel.NodeVerticalSeparator+this.NodeButton.Size.Height);
	
			ChangeNextNodeLocation(this,spacing);
		}
		
		
		public void AddChild(XMLNode btnNode)
		{
			btnNode.Parent=this;
			btnNode.NodePosition=this.Count;
			btnNode.Level=this.Level+1;
			SetLevel(btnNode);
			
			this.Add(btnNode);
		}
        public void AddChild(XMLNode btnNode,bool IsString)
        {
            btnNode.Parent = this;
            btnNode.NodePosition = this.Count;
            btnNode.Level = this.Level + 1;
            SetLevel(btnNode);
            this.Add(btnNode);
        }
		
		public void SetLocation(Point sp)
		{

			this.IndicatorButton.Location=GetIndicatorButtonLocation(sp);
		    this.NodeButton.Location=GetNodeButtonLocation(sp);
			

		}
		
		#endregion

		#endregion
		#region private member function
		private Point GetIndicatorButtonLocation(Point sp)
		{
			int X=sp.X;
			int Y=sp.Y +this.NodeButtonSize.Height/2-IndicatorButtonSize.Height /2;
			return new Point(X,Y);

		}
		private Point GetNodeButtonLocation(Point sp)
		{
			int X=sp.X+IndicatorButtonSize.Width+IndicatorButtonSeparator ;
			int Y=sp.Y;
			return new Point(X,Y);
		}

		


		public void ChangeNodeState()
		{
			if(this.Expanded==false)
			{	
								
				Expand();
			}
			else 
			{
				
							
				Collapse(); 

			}
		}
		private void IndicatorButton_Click(object sender, EventArgs e)
		{
			
			this.ChangeNodeState();

		}
		

		private void ChangeNextNodeLocation(XMLNode btnNode,int spacing)
		{

			
			XMLNode ParentNode=(XMLNode)btnNode.Parent;
			if(ParentNode.Parent==null)
			{
				for(int i=btnNode.NodePosition+1;i<ParentNode.Count;i++)
				{
					XMLNode btn=(XMLNode)ParentNode[i];
						
					ChangeVerticalLocation(btn,spacing);
				}
				return;
			}

			else
			{
			
				for(int i=btnNode.NodePosition+1;i<ParentNode.Count;i++)
				{
					XMLNode btn=(XMLNode)ParentNode[i];
						
					ChangeVerticalLocation(btn,spacing);
				}
				ChangeNextNodeLocation(btnNode.Parent,spacing);
			}

		}
		#endregion
	}
}