namespace Ejercicio_1
{
    partial class ShowingFile
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShowingFile));
            this.txtFileShow = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtFileShow
            // 
            this.txtFileShow.Location = new System.Drawing.Point(1, 2);
            this.txtFileShow.Multiline = true;
            this.txtFileShow.Name = "txtFileShow";
            this.txtFileShow.Size = new System.Drawing.Size(798, 444);
            this.txtFileShow.TabIndex = 0;
            // 
            // ShowingFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.txtFileShow);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ShowingFile";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ShowingFile";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ShowingFile_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox txtFileShow;
    }
}