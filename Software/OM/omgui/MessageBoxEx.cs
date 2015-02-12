using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace OmGui
{
    public enum MessageBoxExButtons
    {
        OK = 0,
        OKCancel = 1,
        AbortRetryIgnore = 2,
        YesNoCancel = 3,
        YesNo = 4,
        RetryCancel = 5,
    }

    public enum MessageBoxExIcon
    {
        None = 0,
        Error = 16,
        Hand = 16,
        Stop = 16,
        Question = 32,
        Exclamation = 48,
        Warning = 48,
        Information = 64,
        Asterisk = 64,
    }

    public enum MessageBoxExDefaultButton
    {
        Button1 = 0,
        Button2 = 256,
        Button3 = 512,
    }

    public partial class MessageBoxEx : Form
    {
        public MessageBoxEx(string text, string caption, MessageBoxExButtons buttons, MessageBoxExIcon icon, MessageBoxExDefaultButton defaultButton)
        {
            InitializeComponent();
            DialogResult = System.Windows.Forms.DialogResult.None;

            // Caption
            Text = caption;

            // Message text
            labelPrompt.Text = text;

            // Buttons
            if (buttons == MessageBoxExButtons.OK)
            {
                button3.Text = "OK"; button3.DialogResult = DialogResult.OK;
            }
            else if (buttons == MessageBoxExButtons.OKCancel)
            {
                button2.Text = "OK"; button2.DialogResult = DialogResult.OK;
                button3.Text = "Cancel"; button3.DialogResult = DialogResult.Cancel;
            }
            else if (buttons == MessageBoxExButtons.AbortRetryIgnore)
            {
                button1.Text = "Abort"; button1.DialogResult = DialogResult.Abort;
                button2.Text = "Retry"; button2.DialogResult = DialogResult.Retry;
                button3.Text = "Ignore"; button3.DialogResult = DialogResult.Ignore;
            }
            else if (buttons == MessageBoxExButtons.YesNoCancel)
            {
                button1.Text = "Yes"; button1.DialogResult = DialogResult.Yes;
                button2.Text = "No"; button2.DialogResult = DialogResult.No;
                button3.Text = "Cancel"; button3.DialogResult = DialogResult.Cancel;
            }
            else if (buttons == MessageBoxExButtons.YesNo)
            {
                button2.Text = "Yes"; button2.DialogResult = DialogResult.Yes;
                button3.Text = "No"; button3.DialogResult = DialogResult.No;
            }
            else if (buttons == MessageBoxExButtons.RetryCancel)
            {
                button2.Text = "Retry"; button2.DialogResult = DialogResult.Retry;
                button3.Text = "Cancel"; button3.DialogResult = DialogResult.Cancel;
            }

            // Button visibility
            button1.Visible = (button1.Text.Length > 0);
            button2.Visible = (button2.Text.Length > 0);
            button3.Visible = (button3.Text.Length > 0);

            // Default button number
            int defaultButtonNum = 0;
            if (defaultButton == MessageBoxExDefaultButton.Button1) { defaultButtonNum = 1; }
            else if (defaultButton == MessageBoxExDefaultButton.Button2) { defaultButtonNum = 2; }
            else if (defaultButton == MessageBoxExDefaultButton.Button3) { defaultButtonNum = 3; }

            // Default cancel button
            if (button1.DialogResult == DialogResult.Cancel) { this.CancelButton = button1; }
            if (button2.DialogResult == DialogResult.Cancel) { this.CancelButton = button2; }
            if (button3.DialogResult == DialogResult.Cancel) { this.CancelButton = button3; }

            // Default button focus
            int b = 1;
            if (button1.Visible && defaultButtonNum == ++b) { this.AcceptButton = button1; button1.Focus(); }
            if (button2.Visible && defaultButtonNum == ++b) { this.AcceptButton = button2; button2.Focus(); }
            if (button3.Visible && defaultButtonNum == ++b) { this.AcceptButton = button3; button3.Focus(); }

            // Determine icon
            Icon ico = null;
            if (icon == MessageBoxExIcon.Error) { ico = SystemIcons.Error; }
            if (icon == MessageBoxExIcon.Hand) { ico = SystemIcons.Hand; }
            if (icon == MessageBoxExIcon.Stop) { ico = SystemIcons.Hand; }
            if (icon == MessageBoxExIcon.Question) { ico = SystemIcons.Question; }
            if (icon == MessageBoxExIcon.Exclamation) { ico = SystemIcons.Exclamation; }
            if (icon == MessageBoxExIcon.Warning) { ico = SystemIcons.Warning; }
            if (icon == MessageBoxExIcon.Information) { ico = SystemIcons.Information; }
            if (icon == MessageBoxExIcon.Asterisk) { ico = SystemIcons.Asterisk; }

            // Set icon
            if (ico != null)
            {
                labelPrompt.Width -= pictureBoxIcon.Width;
                labelPrompt.Left += pictureBoxIcon.Width;
                pictureBoxIcon.Visible = true;
                pictureBoxIcon.Image = ico.ToBitmap();
            }
        }

        public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxExButtons buttons, MessageBoxExIcon icon, MessageBoxExDefaultButton defaultButton)
        {
            MessageBoxEx messageBoxEx = new MessageBoxEx(text, caption, buttons, icon, defaultButton);
            return messageBoxEx.ShowDialog(owner);
        }

        public static DialogResult Show(IWin32Window owner, string text, string caption)
        {
            return MessageBoxEx.Show(owner, text, caption, MessageBoxExButtons.OK, MessageBoxExIcon.None, MessageBoxExDefaultButton.Button1);
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void MessageBoxEx_Load(object sender, EventArgs e)
        {
            // Manually center on parent
            //if (this.Parent != null)
            //{
            //    Rectangle rect = this.Parent.DisplayRectangle;
            //    int x = rect.X + rect.Width / 2 - this.Width / 2;
            //    int y = rect.Y + rect.Height / 2 - this.Height / 2;
            //    this.Location = new Point(x, y);
            //}

            // Resize height to fit
            int additional = this.Height - this.panel1.Height;
            int height = labelPrompt.Height + 50 + additional;
            if (height > this.Height)
            {
                this.Height = height;
            }
        }

        private void MessageBoxEx_Resize(object sender, EventArgs e)
        {
            labelPrompt.MaximumSize = new Size(this.Width - labelPrompt.Left - 30, labelPrompt.MaximumSize.Height);
        }

    }
}
