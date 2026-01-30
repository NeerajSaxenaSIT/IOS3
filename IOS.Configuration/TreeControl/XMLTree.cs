using System;
using System.Windows.Forms ;
using System.Collections;
using System.Drawing;
using System.ComponentModel;

namespace IOS.Configuration.TreeControl
{
	///<Authority>
	///this control is created by Syed Md. Abul Bashar Milton
	///Software Engineer
	///this is Version 3.4.2.4
	///Last Modification Date : 04/09/2006(dd/MM/yyyy)
	///</Authority>
	/// <summary>
	///
	/// This is Milton Created TreeView like Tree where Nodes contains Button Control  
	/// </summary>
	public class XMLTree:System.Windows.Forms.Panel 
	{

		#region member variable
		public XMLNode ButtonNodes;
		public Point _RootLocation;
		public int XRootLocation=0;
		private  int _NodeVerticalSeparator;
		private Size _ButtonNodeSize; 
		private Size _IndicatorNodeSize; 
		private int NextXLocation=0;
		private int NextYLocation=0;
		private int _NodeHorizontalSeparator;
		private int _IndicatorButtonSeparator=5;
		private Image _IndicatorCloseImage;
		private Image _IndicatorOpenImage;
		private Image _MyImage;
		#endregion
		#region constructor
		public XMLTree()
		{
			
			
			_RootLocation=new Point(this.Left+5,this.Top+5 );
			XRootLocation=this.Left+5;
			_NodeHorizontalSeparator=5;
			_NodeVerticalSeparator=5;
			ButtonNodes=new XMLNode();
			_ButtonNodeSize=new Size(70,20);
			_IndicatorNodeSize=new Size(15,15);
			NextXLocation=_RootLocation.X ;
			NextYLocation=_RootLocation.Y;
			//
			// TODO: Add constructor logic here
			//
		}

		#endregion
		

		#region public member function

		#region TreeProperty
		[Category("TreeProperty"),Description("Set or Get Node HorizontalSeparator Separator"),DefaultValue(5)]
		public int NodeHorizontalSeparator
		{
			get
			{
				return _NodeHorizontalSeparator;
			}
			set
			{
				_NodeHorizontalSeparator  =value;
			}
		}

		[Category("TreeProperty"),Description("Set or Get Node HorizontalSeparator Separator"),DefaultValue(5)]
		public int NodeVerticalSeparator
		{
			get
			{
				return _NodeVerticalSeparator;
			}
			set
			{
				_NodeVerticalSeparator  =value;
			}
		}

		#endregion
		
		#region TreeNode Property
		
		

		[Category("TreeNodeProperty"),Description("Set or Get Button Node Size"),Browsable(true)]
		public Size ButtonNodeSize
		{
			get
			{
				return _ButtonNodeSize;
			}
			set
			{
				_ButtonNodeSize=value;
			}
		}

		
		[Category("TreeNodeProperty"),Description("Set or Get Indicator Node Size"),Browsable(true)]
		public Size IndicatorButtonSize
		{
			get
			{
				return _IndicatorNodeSize;
			}
			set
			{
				_IndicatorNodeSize =value;
			}
		}
		[Category("TreeNodeProperty"),Description("Set or Get IndicatorButton and NodeButton Spacing"),DefaultValue(10)]
		public int IndicatorButtonSeparator
		{
			get
			{
				return _IndicatorButtonSeparator ;
			}
			set
			{
				_IndicatorButtonSeparator =value;
			}
		}
		[Category("MyImage"),Description("Set or Get Indicator Close State Image"),Browsable(true)]
		public Image MyImage
		{
			get
			{
				return _MyImage;
			}
			set
			{
				_MyImage=value;
			}
		}

		[Category("TreeNodeProperty"),Description("Set or Get Indicator Close State Image"),Browsable(true)]
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

		[Category("TreeNodeProperty"),Description("Set or Get Indicator Open State Image"),Browsable(true)]
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


		public void SetNextLocation(Point sp)
		{
			NextXLocation=sp.X;
			NextYLocation=sp.Y;
		}
		public  void SetNextXLocation(int Level)
		{
			NextXLocation=_RootLocation.X +Level*(this.IndicatorButtonSize.Width +this.NodeHorizontalSeparator); 
			
		}


		private void SetPosition(XMLNode btnNode)
		{
			
				btnNode.NodePosition =ButtonNodes.Count;
			
		}
		

		public void LoadNode(XMLNode btnNode)
		{
			foreach(XMLNode btn in btnNode)
			{
				this.Controls.Add(btn.IndicatorButton);
				this.Controls.Add(btn.NodeButton);
				LoadNode(btn);
			}

			
		}
		public void Add( XMLNode btnNode)
		{
			this.SetPosition(btnNode);
			ButtonNodes.Add(btnNode);
			btnNode.Parent=ButtonNodes;
			
			btnNode.IndicatorButton.Size =this.IndicatorButtonSize;
			btnNode.NodeButton.Size=this.ButtonNodeSize;
			btnNode.IndicatorButtonSeparator=this.IndicatorButtonSeparator;
			btnNode.RootPanel=this;
			btnNode.IndicatorButton.Image=this.IndicatorOffImage;
			btnNode.IndicatorOffImage =this.IndicatorOffImage;
			
			btnNode.IndicatorOnImage =this.IndicatorOnImage;
			SetButtonNodeProperty(btnNode);
											
			ShowControls(btnNode );
			LoadNode(btnNode);		
		}

	
	
		public void RemoveControls()
		{
			this.Controls.Clear();
			SetNextLocation(_RootLocation);
		

		}
		public void ShowControls(XMLNode btnNodes)
		{
			this.SetNextXLocation(btnNodes.Level);
			btnNodes.SetLocation(GetNextLocation);
			if(btnNodes.Count>0)
			{
				btnNodes.NodeButton.Visible=true;
				this.Controls.Add(btnNodes.IndicatorButton);
			}
			btnNodes.IndicatorButton.Visible=true;
			this.Controls.Add(btnNodes.NodeButton);
			this.SetNextYLocation(GetNextLocation);
//			foreach(ButtonNode btnNodes in btnNodes)
//			{
//				if(btnNodes.Expanded==true)
//					ShowControls(btn);
//				
//			}
				

			
		}

//		public void ReLoad()
//		{
//			RemoveControls();
//			ShowControls(ButtonNodes);
//
//		}
		#endregion
		#region private member function

		#region Property
		private Point GetNextLocation
		{
			get
			{

				return	new Point(NextXLocation,NextYLocation);

			}
		}
		#endregion

		private void SetButtonNodeProperty(XMLNode btnNode)
		{
			foreach(XMLNode btn in btnNode)
			{
				//btn.Parent=ButtonNodes;
				btn.IndicatorButton.Size =this.IndicatorButtonSize;
				btn.NodeButton.Size =this.ButtonNodeSize;
				btn.IndicatorButtonSeparator=this.IndicatorButtonSeparator;
				btn.RootPanel=this;
				btn.IndicatorButton.Image=this.IndicatorOffImage;
				btn.IndicatorOffImage =this.IndicatorOffImage;
				
				btn.IndicatorOnImage =this.IndicatorOnImage;
		
				SetButtonNodeProperty(btn);
			}
		}
		private void SetNextYLocation(Point sp)
		{
			NextYLocation=sp.Y+this.NodeVerticalSeparator+this.ButtonNodeSize.Height ; 

		}

		#endregion
		

	
	}
}
