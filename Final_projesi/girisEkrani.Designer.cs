namespace Final_projesi
{
    partial class girisEkrani
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
            this.nameLabel = new System.Windows.Forms.Label();
            this.ErrorLabel = new System.Windows.Forms.Label();
            this.symbolLabel = new System.Windows.Forms.Label();
            this.uyeSec = new System.Windows.Forms.ComboBox();
            this.UyariLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // nameLabel
            // 
            this.nameLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nameLabel.AutoSize = true;
            this.nameLabel.Font = new System.Drawing.Font("Arial", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.nameLabel.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.nameLabel.Location = new System.Drawing.Point(237, 270);
            this.nameLabel.MaximumSize = new System.Drawing.Size(400, 41);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(164, 41);
            this.nameLabel.TabIndex = 0;
            this.nameLabel.Text = "uye_ismi";
            this.nameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ErrorLabel
            // 
            this.ErrorLabel.AutoSize = true;
            this.ErrorLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.ErrorLabel.ForeColor = System.Drawing.Color.IndianRed;
            this.ErrorLabel.Location = new System.Drawing.Point(292, 352);
            this.ErrorLabel.Name = "ErrorLabel";
            this.ErrorLabel.Size = new System.Drawing.Size(52, 24);
            this.ErrorLabel.TabIndex = 1;
            this.ErrorLabel.Text = "Error";
            this.ErrorLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // symbolLabel
            // 
            this.symbolLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.symbolLabel.Font = new System.Drawing.Font("Arial", 96F, System.Drawing.FontStyle.Bold);
            this.symbolLabel.ForeColor = System.Drawing.Color.Chartreuse;
            this.symbolLabel.Location = new System.Drawing.Point(189, 68);
            this.symbolLabel.Name = "symbolLabel";
            this.symbolLabel.Size = new System.Drawing.Size(274, 166);
            this.symbolLabel.TabIndex = 2;
            this.symbolLabel.Text = "✓";
            this.symbolLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // uyeSec
            // 
            this.uyeSec.FormattingEnabled = true;
            this.uyeSec.Location = new System.Drawing.Point(12, 490);
            this.uyeSec.Name = "uyeSec";
            this.uyeSec.Size = new System.Drawing.Size(191, 21);
            this.uyeSec.TabIndex = 3;
            this.uyeSec.SelectedIndexChanged += new System.EventHandler(this.uyeSec_SelectedIndexChanged);
            // 
            // UyariLabel
            // 
            this.UyariLabel.AutoSize = true;
            this.UyariLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.UyariLabel.ForeColor = System.Drawing.Color.Yellow;
            this.UyariLabel.Location = new System.Drawing.Point(292, 352);
            this.UyariLabel.Name = "UyariLabel";
            this.UyariLabel.Size = new System.Drawing.Size(52, 24);
            this.UyariLabel.TabIndex = 1;
            this.UyariLabel.Text = "Uyari";
            this.UyariLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // girisEkrani
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.WindowText;
            this.ClientSize = new System.Drawing.Size(643, 523);
            this.Controls.Add(this.uyeSec);
            this.Controls.Add(this.symbolLabel);
            this.Controls.Add(this.UyariLabel);
            this.Controls.Add(this.ErrorLabel);
            this.Controls.Add(this.nameLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "girisEkrani";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.girisEkrani_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.Label ErrorLabel;
        private System.Windows.Forms.Label symbolLabel;
        private System.Windows.Forms.ComboBox uyeSec;
        private System.Windows.Forms.Label UyariLabel;
    }
}