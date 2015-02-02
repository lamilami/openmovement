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
            buttonOK.Enabled = false;
        }


        private void AppendText(string data)
        {
            if (InvokeRequired) { this.Invoke(new MethodInvoker(() => { AppendText(data); })); return; }

            const int max = 8192;
            if (textBoxProgress.Text.Length + data.Length > max)
            {
                string txt = textBoxProgress.Text + data;
                txt = txt.Substring(txt.Length - max);
                textBoxProgress.Text = txt;
                textBoxProgress.Select(textBoxProgress.Text.Length, 0);
                textBoxProgress.ScrollToCaret();
            }
            else
            {
                textBoxProgress.Select(textBoxProgress.Text.Length, 0);
                textBoxProgress.SelectedText = data;
                textBoxProgress.Select(textBoxProgress.Text.Length, 0);
                textBoxProgress.ScrollToCaret();
            }
            Console.Out.Write(data);
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
            processInformation.CreateNoWindow = true;
            processInformation.RedirectStandardError = true;
            //processInformation.RedirectStandardOutput = true;

            Process conversionProcess = new Process();
            conversionProcess.EnableRaisingEvents = true;
            conversionProcess.StartInfo = processInformation;
            /*
            conversionProcess.OutputDataReceived += (sender, e) => {
                AppendText(e.Data);
            };
            conversionProcess.ErrorDataReceived += (sender, e) => {
                AppendText(e.Data);
            };
            */

            AppendText("<<<START: " + conversionProcess.StartInfo.FileName + " " + conversionProcess.StartInfo.Arguments + ">>>\n");
            try
            {
                conversionProcess.Start();
            }
            catch (Exception ex)
            {
                AppendText("<<<ERROR: " + ex.Message+ ">>>\n");
                Trace.WriteLine("ERROR: Problem running conversion process: " + ex.Message);
                MessageBox.Show(this, "Problem running conversion process " + ex.Message + ".", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                return false;
            }

            bool cancel = false;
            StringBuilder sb = new StringBuilder();
            while (!conversionProcess.StandardError.EndOfStream)
            {
                if (conversionProcess.StandardError.Peek() < 0)
                {
                    AppendText(sb.ToString()); sb.Remove(0, sb.Length);
                    System.Threading.Thread.Sleep(100);
                }
                else
                {
                    int ic = conversionProcess.StandardError.Read();
                    if (ic < 0) { break; }
                    char cc = (char)ic;
                    sb.Append(cc);

                    if (cc == 13 || sb.Length > 32)
                    {
                        AppendText(sb.ToString()); sb.Remove(0, sb.Length);
                    }
                }
                if (backgroundWorker.CancellationPending) 
                {
                    cancel = true;
                    conversionProcess.Kill();
                    break; 
                }
            }
            AppendText(sb.ToString()); sb.Remove(0, sb.Length);

            AppendText("<<<WAIT>>>\n");
            conversionProcess.WaitForExit();

            int exitCode = conversionProcess.ExitCode;
            AppendText("<<<END: " + exitCode + ">>>\n");

            if (cancel) { AppendText("<<<CANCELLED>>>\n"); }
            return !cancel;
        }



        private bool cancelClose = true;

        private void ProcessingForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = cancelClose;
            //if (e.Cancel) { backgroundWorker.CancelAsync(); }
        }

        private void ProcessingForm_Load(object sender, EventArgs e)
        {
            backgroundWorker.DoWork += (bws, bwe) =>
            {
                if (Execute())
                {
                    if (InvokeRequired) { this.Invoke(new MethodInvoker(() => { buttonOK.Enabled = true; })); }                    
                }
            };
            backgroundWorker.RunWorkerCompleted += (bws, bwe) =>
            {
                cancelClose = false;
                //this.Close();
            };

            pictureBoxProgress.Visible = true;
            backgroundWorker.RunWorkerAsync();
        }


        private void timerUpdate_Tick(object sender, EventArgs e)
        {
            pictureBoxProgress.Visible = backgroundWorker.IsBusy;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

    }
}
