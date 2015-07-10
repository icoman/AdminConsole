/*
 * Created by SharpDevelop.
 * User: Ioan.Coman
 * Date: 11/11/2012
 * Time: 8:09 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace AdminConsole
{


	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		ConsoleItem[][] array = new ConsoleItem[100][];
		public static string cmdresult = null;
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}

		
		void LoadConfig_PopulateTree()
		{
			//
			//read config and populate treeView1
			//
			treeView1.Nodes.Clear();
			TreeNode tn = null;
			string configFilename = "adminconsole.cfg";  //name of config file
			int cntlinie = 0;
			int pos = -1;
			int subpos = 0;
			string line = null;
			string defaultConfig = @";
;
; Default admin console configuration file
;
; +Nume of grup of computers (workstations or servers)
; Title, type, PC_name_or_IP, RDP_user, RDP_or_VNC_password, any other comments visible later on console tree
;
; type has value: VNC or RDP
;
;
;
;


;Example:
+SQL Servers
	Server1 Administrator,RDP,192.168.10.100,Administrator,Alfa.123,This is server1 with RDP and Administrator
	Server1 User1,RDP,192.168.10.100,User1,Alfa.123,Server 1 with RDP and User1
	Server2 with VNC,VNC,192.168.10.101,ioan,123
	Server3 with VNC,VNC,192.168.10.102,ioan,123

+Web Servers
	Server4 Administrator,RDP,192.168.10.103,Administrator,Alfa.123,This is server4 with RDP and Administrator
	Server4 User1,RDP,192.168.10.103,User1,Alfa.123,Server 4 with RDP and User1
	Server5 with VNC,VNC,192.168.10.104,ioan,123,Server 5 user:ioan passwords:xxxx
	Server6 with VNC,VNC,192.168.10.105,ioan,123,Server 6 user:dan password:****, SQL:sa:123

";
			if (!File.Exists(configFilename)) {
				//create default config file
				File.WriteAllText(configFilename, defaultConfig);
			}

			try {
				StreamReader sr = new StreamReader(configFilename);
				while (true) {
					line = sr.ReadLine();
					cntlinie = cntlinie + 1;
					if (line == null)
						break;
					int first = line.IndexOf(";");	//find comments
					if (first != -1)
						//found a ';'
						line = line.Remove(first, line.Length - first);
					
					line = line.Trim();	//remove all spaces from begin and from end
					if (line == "")
						continue;	//skip empty lines
					if (line[0] == '+') {
						line = line.Replace("+", "");
						//add previous node
						if (tn != null)
							treeView1.Nodes.Add(tn);
						//create a new node
						tn = new TreeNode(line);
						pos = pos + 1;
						subpos = 0;
						array[pos] = new ConsoleItem[100];
					} else {
						//add subitems to node
						//  0     1      2      3      4            5
						//Title, type, PC_name, user, password, comments
						ConsoleItem ci = new ConsoleItem();
						IList<string> ll = line.Split(',').ToList<string>();
						ci.title = ll[0];
						if (ll[1].ToUpper() == "VNC")
							ci.type = 0;
						else
							ci.type = 1;
						ci.computername = ll[2];
						ci.user = ll[3];
						ci.password = ll[4];
						//assert the rest is a comment and add it to tree
						string s = "";
						for (int i = 5; i < ll.Count; i++)
							s = s + ll[i] + " ";
						ci.comments = s;
						array[pos][subpos] = ci;
						subpos = subpos + 1;
						tn.Nodes.Add(new TreeNode(ci.repr()));
					}
				}
				if (tn != null)
					treeView1.Nodes.Add(tn);
				sr.Close();
			} catch (Exception ex) {
				string msg = string.Format("Error in config line {0}:\n{1}\n\n\n", cntlinie, line) + ex.Message;
				MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			
		}
		
		void MainFormLoad(object sender, EventArgs e)
		{
			LoadConfig_PopulateTree();
			label2.Text = "";
		}

		
		
		//
		// ExecuteCommandSync si ExecuteCommandAsync de la:
		// http://www.codeproject.com/Articles/25983/How-to-Execute-a-Command-in-C
		//
		/// <summary>
		/// Executes a shell command synchronously.
		/// </summary>
		/// <param name="command">string command</param>
		/// <returns>string, as output of the command.</returns>
		public static void ExecuteCommandSync(object command)
		{
			cmdresult = "error";
			try {
				// create the ProcessStartInfo using "cmd" as the program to be run,
				// and "/c " as the parameters.
				// Incidentally, /c tells cmd that we want it to execute the command that follows,
				// and then exit.
				System.Diagnostics.ProcessStartInfo procStartInfo =
					new System.Diagnostics.ProcessStartInfo("cmd", "/c " + command);
				
				// The following commands are needed to redirect the standard output.
				// This means that it will be redirected to the Process.StandardOutput StreamReader.
				procStartInfo.RedirectStandardOutput = true;
				procStartInfo.UseShellExecute = false;
				// Do not create the black window.
				procStartInfo.CreateNoWindow = true;
				// Now we create a process, assign its ProcessStartInfo and start it
				System.Diagnostics.Process proc = new System.Diagnostics.Process();
				proc.StartInfo = procStartInfo;
				proc.Start();
				// Get the output into a string
				cmdresult = proc.StandardOutput.ReadToEnd();
				// Display the command output.
				//Console.WriteLine(result);
			} catch {
				// Log the exception
			}
		}
		/// <summary>
		/// Execute the command Asynchronously.
		/// </summary>
		/// <param name="command">string command.</param>
		public static void ExecuteCommandAsync(string command)
		{
			try {
				//Asynchronously start the Thread to process the Execute command request.
				Thread objThread = new Thread(new ParameterizedThreadStart(ExecuteCommandSync));
				//Make the thread as background thread.
				objThread.IsBackground = true;
				//Set the Priority of the thread.
				objThread.Priority = ThreadPriority.AboveNormal;
				//Start the thread.
				objThread.Start(command);
			} catch {
				// Log the exception
			}
		}


		
		void TreeView1MouseDoubleClick(object sender, MouseEventArgs e)
		{
			TreeNode node = treeView1.SelectedNode;
			TreeNode parent = node.Parent;
			if (parent != null) {
				int pos = parent.Index;
				int subpos = node.Index;
				ConsoleItem ci = array[pos][subpos];
				//MessageBox.Show(ci.repr());
				if (ci.type == 0) {
					//VNC - http://www.tightvnc.com/
					//ex: vncviewer.exe /shared epowxpprod01 /password 123
					//string cmd = string.Format("vncviewer.exe /shared {0} /password {1}",ci.computername,ci.password);
					string cmd = string.Format("tvnviewer.exe -host={0} -password={1}", ci.computername, ci.password);
					ExecuteCommandAsync(cmd);
				} else {
					//RDP - http://www.donkz.nl/
					//ex: rdp.exe /shared /v:computername /u:username /p:password /noprinters /nodrives /w:1024 /h:768
					string cmd = string.Format("rdp.exe /v:{0} /u:{1} /p:{2} /noprinters /nodrives /w:1024 /h:768", ci.computername, ci.user, ci.password);
					ExecuteCommandAsync(cmd);
				}
				
			}
			//the two indexes (node and parent) are used on selection from global list
			
		}
		
		void Button1Click(object sender, EventArgs e)
		{
			LoadConfig_PopulateTree();
		}
		
		void TreeView1AfterSelect(object sender, TreeViewEventArgs e)
		{
			TreeNode node = treeView1.SelectedNode;
			TreeNode parent = node.Parent;
			if (parent != null) {
				int pos = parent.Index;
				int subpos = node.Index;
				ConsoleItem ci = array[pos][subpos];
				string s = null;
				if (ci.type == 0)
					s = "VNC";
				else
					s = "RDP";
				s = s + string.Format(": {0}   {1}", ci.computername, ci.comments);
				label2.Text = s;
			} else
				label2.Text = "";
			
		}
		
		void Button2Click(object sender, EventArgs e)
		{
			new About().ShowDialog();
		}
		
		void Button3Click(object sender, EventArgs e)
		{
			this.Close();
		}


		void Button4Click(object sender, System.EventArgs e)
		{
			//Not working - not implemented
			//new InstallVNC().ShowDialog();
			MessageBox.Show("Not working.", "Not working.", MessageBoxButtons.RetryCancel, MessageBoxIcon.Question);
		}
		
		void Button5Click(object sender, EventArgs e)
		{
			TreeView1MouseDoubleClick(sender, null);
		}

	}
	



	public class ConsoleItem
	{
		public string title;
		public int type;
		//0=VNC 1=RDP
		public string computername, user, password, comments;
		public string repr()
		{
			return string.Format("{0} ({1}) {2}", title, computername, comments);
		}
	}
	
	

}
