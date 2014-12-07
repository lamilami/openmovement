namespace OmGui
{
    partial class ProcessingForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProcessingForm));
            this.groupBoxProgress = new System.Windows.Forms.GroupBox();
            this.textBoxProgress = new System.Windows.Forms.TextBox();
            this.timerUpdate = new System.Windows.Forms.Timer(this.components);
            this.groupBoxProgress.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxProgress
            // 
            this.groupBoxProgress.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxProgress.Controls.Add(this.textBoxProgress);
            this.groupBoxProgress.Location = new System.Drawing.Point(13, 13);
            this.groupBoxProgress.Name = "groupBoxProgress";
            this.groupBoxProgress.Size = new System.Drawing.Size(750, 396);
            this.groupBoxProgress.TabIndex = 0;
            this.groupBoxProgress.TabStop = false;
            this.groupBoxProgress.Text = "Progress";
            // 
            // textBoxProgress
            // 
            this.textBoxProgress.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxProgress.Location = new System.Drawing.Point(7, 22);
            this.textBoxProgress.Multiline = true;
            this.textBoxProgress.Name = "textBoxProgress";
            this.textBoxProgress.ReadOnly = true;
            this.textBoxProgress.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxProgress.Size = new System.Drawing.Size(737, 368);
            this.textBoxProgress.TabIndex = 0;
            // 
            // timerUpdate
            // 
            this.timerUpdate.Enabled = true;
            this.timerUpdate.Interval = 250;
            this.timerUpdate.Tick += new System.EventHandler(this.timerUpdate_Tick);
            // 
            // ProcessingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(775, 421);
            this.Controls.Add(this.groupBoxProgress);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ProcessingForm";
            this.Text = "Processing";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ProcessingForm_FormClosing);
            this.Load += new System.EventHandler(this.ProcessingForm_Load);
            this.groupBoxProgress.ResumeLayout(false);
            this.groupBoxProgress.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxProgress;
        private System.Windows.Forms.TextBox textBoxProgress;
        private System.Windows.Forms.Timer timerUpdate;
    }
}