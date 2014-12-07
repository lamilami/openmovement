using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OmGui
{
    public partial class ProcessingForm : Form
    {
        private string executableName;
        private List<string> args;

        public ProcessingForm(string executableName, List<string> args)
        {
            InitializeComponent();
            this.executableName = executableName;
            this.args = args;
        }

        public bool Execute()
        {
            // Executable file
            string executableFile = Path.Combine(Application.StartupPath, executableName);
            if (!File.Exists(executableFile))
            {
                MessageBox.Show(this, "This process requires the external executable " + executableName + ".\r\n\r\nThe file was not found at:\r\n\r\n" + executableFile + "\r\n\r\nPlease locate the executable there and try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                return false;
            }

            // Convert the file
            ProcessStartInfo processInformation = new ProcessStartInfo();
            processInformation.FileName = executableFile;

            // Construct arguments
            processInformation.Arguments = string.Join(" ", this.args.ToArray());
            processInformation.UseShellExecute = false;
            //processInformation.CreateNoWindow = true;
            processInformation.RedirectStandardError = true;
            //processInformation.RedirectStandardOutput = true;

            Process conversionProcess = new Process();
            conversionProcess.EnableRaisingEvents = true;
            conversionProcess.StartInfo = processInformation;

            try
            {
                conversionProcess.Start();
            }
            catch (Exception ex)
            {
                Trace.WriteLine("ERROR: Problem running conversion process: " + ex.Message);
                return false;
            }

            char[] buffer = new char[2];
            while (!conversionProcess.StandardError.EndOfStream)
            {
                int offset = 0;
                int length = conversionProcess.StandardError.ReadBlock(buffer, offset, buffer.Length);
                if (length > 0)
                {
                    string st = new string(buffer, offset, length);
                    //Console.Out.Write(st);
                    string txt = textBoxProgress.Text + st;
                    if (txt.Length > 8192)
                    {
                        txt = txt.Substring(txt.Length - 8192);
                    }
                    textBoxProgress.Text = txt;
                    textBoxProgress.Select(textBoxProgress.Text.Length, 0);
                    textBoxProgress.ScrollToCaret();
                }
            }
            conversionProcess.WaitForExit();

            int exitCode = conversionProcess.ExitCode;
            if (exitCode != 0)
            {
                Trace.WriteLine("Conversion error (" + exitCode + ")");
                //return false;
            }

            return true;
        }


        private bool cancelClose = true;

        private void ProcessingForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = cancelClose;
        }

        private void ProcessingForm_Load(object sender, EventArgs e)
        {
            if (!Execute())
            {
                this.DialogResult = DialogResult.Cancel;
                cancelClose = false;
//                this.Close();
            }
            this.DialogResult = DialogResult.OK;
            cancelClose = false;
            this.Close();
        }

        private void timerUpdate_Tick(object sender, EventArgs e)
        {
        }

    }
}
