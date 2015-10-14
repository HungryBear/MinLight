namespace MinLight.View
{
    partial class ViewForm
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
            this.ctlCanvas = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.ctlCanvas)).BeginInit();
            this.SuspendLayout();
            // 
            // ctlCanvas
            // 
            this.ctlCanvas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ctlCanvas.Location = new System.Drawing.Point(2, 2);
            this.ctlCanvas.Name = "ctlCanvas";
            this.ctlCanvas.Size = new System.Drawing.Size(512, 512);
            this.ctlCanvas.TabIndex = 0;
            this.ctlCanvas.TabStop = false;
            this.ctlCanvas.Paint += new System.Windows.Forms.PaintEventHandler(this.ctlCanvas_Paint);
            // 
            // ViewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(554, 561);
            this.Controls.Add(this.ctlCanvas);
            this.Name = "ViewForm";
            this.Text = "MinLight";
            ((System.ComponentModel.ISupportInitialize)(this.ctlCanvas)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox ctlCanvas;
    }
}

