using System;

namespace Debugger
{
    partial class DebugForm
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
            ListStack = new System.Windows.Forms.ListBox();
            ListCode = new System.Windows.Forms.ListBox();
            Continue = new System.Windows.Forms.Button();
            btnExit = new System.Windows.Forms.Button();
            SuspendLayout();
            // 
            // ListStack
            // 
            ListStack.FormattingEnabled = true;
            ListStack.ItemHeight = 20;
            ListStack.Location = new System.Drawing.Point(335, 42);
            ListStack.Name = "ListStack";
            ListStack.Size = new System.Drawing.Size(169, 224);
            ListStack.TabIndex = 0;
            // 
            // ListCode
            // 
            ListCode.FormattingEnabled = true;
            ListCode.ItemHeight = 20;
            ListCode.Location = new System.Drawing.Point(23, 42);
            ListCode.Name = "ListCode";
            ListCode.Size = new System.Drawing.Size(178, 224);
            ListCode.TabIndex = 1;
            // 
            // Continue
            // 
            Continue.Location = new System.Drawing.Point(217, 199);
            Continue.Name = "Continue";
            Continue.Size = new System.Drawing.Size(94, 29);
            Continue.TabIndex = 2;
            Continue.Text = "Continue";
            Continue.UseVisualStyleBackColor = true;
            Continue.Click += Continue_Click;
            // 
            // btnExit
            // 
            btnExit.ForeColor = System.Drawing.Color.Red;
            btnExit.Location = new System.Drawing.Point(217, 237);
            btnExit.Name = "btnExit";
            btnExit.Size = new System.Drawing.Size(94, 29);
            btnExit.TabIndex = 3;
            btnExit.Text = "Exit";
            btnExit.UseVisualStyleBackColor = true;
            btnExit.Click += button1_Click;
            // 
            // DebugForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(525, 291);
            Controls.Add(btnExit);
            Controls.Add(Continue);
            Controls.Add(ListCode);
            Controls.Add(ListStack);
            Name = "DebugForm";
            Text = "DebugForm";
            Load += DebugForm_Load;
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.ListBox ListStack;
        private System.Windows.Forms.ListBox ListCode;
        private System.Windows.Forms.Button Continue;
        private System.Windows.Forms.Button btnExit;
    }
}