/*
 * Created by SharpDevelop.
 * User: ioan.coman
 * Date: 11/12/2012
 * Time: 4:10 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Reflection;
using System.Windows.Forms;

namespace AdminConsole
{
	/// <summary>
	/// Description of About.
	/// </summary>
	public partial class About : Form
	{
		public About()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		void Button1Click(object sender, EventArgs e)
		{
			this.Close();
		}
		
		
		void AboutLoad(object sender, EventArgs e)
		{
			//Get OS version
			string infostr = string.Format("OS: {0}", Environment.OSVersion.Platform.ToString());
			infostr += Environment.NewLine;
			infostr += Environment.NewLine;
			
			//Get path for each assembly used
			Assembly[] loadedAsms = AppDomain.CurrentDomain.GetAssemblies();
			foreach (Assembly asm in loadedAsms) {
				infostr += asm.Location;
				infostr += Environment.NewLine;
			}
			textBox1.Text = infostr;
		}
		

	}
}
