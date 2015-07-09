/*
 * Created by SharpDevelop.
 * User: ioan.coman
 * Date: 11/15/2012
 * Time: 10:19 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;

namespace AdminConsole
{
	/// <summary>
	/// Description of InstallVNC.
	/// </summary>
	public partial class InstallVNC : Form
	{
		public InstallVNC()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		void Button2Click(object sender, EventArgs e)
		{
			this.Close();
		}
		
		void log(string s)
		{
			//Environment.NewLine = \r\n in windows si = \n in Unix
			this.textBox2.Text += (s+Environment.NewLine);
		}
		void Button1Click(object sender, EventArgs e)
		{
			string numepc = this.textBox1.Text;
			//string s;
			numepc = "epowxp1gdf15j";
			log("Atentie, dureaza ...");
			log("Nu merge");

			/*
			s = "NET USE \\\\"+numepc+"\\IPC$";
			log(s);	MainForm.ExecuteCommandSync(s);log("rezultat:"+MainForm.cmdresult);

			s = "psexec \\\\"+numepc+" -s -d net stop winvnc";
			log(s);	MainForm.ExecuteCommandSync(s);log("rezultat:"+MainForm.cmdresult);

			s = "NET USE \\\\"+numepc+"\\IPC$";
			log(s);	MainForm.ExecuteCommandSync(s);log("rezultat:"+MainForm.cmdresult);

			s = "psexec \\\\"+numepc+" -s  taskkill /F /IM winvnc*";
			log(s);	MainForm.ExecuteCommandSync(s);log("rezultat:"+MainForm.cmdresult);
			
			s = "xcopy /y winvnc.exe \\\\"+numepc+"\\c$\\windows";
			log(s);	MainForm.ExecuteCommandSync(s);log("rezultat:"+MainForm.cmdresult);

			s = "xcopy /y UltraVNC.ini \\\\"+numepc+"\\c$\\windows";
			log(s);	MainForm.ExecuteCommandSync(s);log("rezultat:"+MainForm.cmdresult);
			*/
		}
	}
}
