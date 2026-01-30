using System;
using System.Windows.Forms;

namespace IOS.Configuration.TreeControl
{
	///<Authority>
	///this control is created by Syed Md. Abul Bashar Milton
	///Software Engineer 
	///this is Version 3.4.2.4
	///Last Modification Date : 04/09/2006(dd/MM/yyyy)
	
	///</Authority>
	/// <summary>
	/// this is Milton created Button(MiButton) specially used for ButtonTree which contrains its Node information.
	/// </summary>
	public class ContainerNode:XMLControl
	{
		private XMLNode _MiNode;
		/// <summary>
		/// MiButon is Button take argument ButtonNode as Container of Button
		/// </summary>
		/// <param name="btnNode">Node contain this button</param>
		public ContainerNode()
		{
			
			//
			// TODO: Add constructor logic here
			//
		}
		public ContainerNode(XMLNode  btnNode)
		{
			_MiNode=btnNode;
			
			//
			// TODO: Add constructor logic here
			//
		}
/// <summary>
/// Get or Set Node Containing  Button
/// </summary>
		public XMLNode MiNode
		{
			get
			{
				return _MiNode;
			}
			set
			{
				_MiNode=value;
			}
		}
		
	}
}
