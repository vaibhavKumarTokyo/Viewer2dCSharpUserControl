namespace Viewer2D.TestClientApp
{
    partial class Form1
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
            this.viewer2DUserControl1 = new Viewer2D.UC.Viewer2DUserControl();
            this.SuspendLayout();
            // 
            // viewer2DUserControl1
            // 
            this.viewer2DUserControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewer2DUserControl1.Image = null;
            this.viewer2DUserControl1.Location = new System.Drawing.Point(0, 0);
            this.viewer2DUserControl1.Name = "viewer2DUserControl1";
            this.viewer2DUserControl1.Size = new System.Drawing.Size(464, 340);
            this.viewer2DUserControl1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(464, 340);
            this.Controls.Add(this.viewer2DUserControl1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private UC.Viewer2DUserControl viewer2DUserControl1;
    }
}

