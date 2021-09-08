namespace MissingC
{
    partial class frmBand
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
            this.txtBandId = new System.Windows.Forms.TextBox();
            this.txtBandName = new System.Windows.Forms.TextBox();
            this.txtBandRider = new System.Windows.Forms.TextBox();
            this.txtBandArtistCut = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnDoneBand = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtBandId
            // 
            this.txtBandId.Location = new System.Drawing.Point(98, 16);
            this.txtBandId.Margin = new System.Windows.Forms.Padding(2);
            this.txtBandId.Name = "txtBandId";
            this.txtBandId.Size = new System.Drawing.Size(151, 20);
            this.txtBandId.TabIndex = 0;
            // 
            // txtBandName
            // 
            this.txtBandName.Location = new System.Drawing.Point(98, 46);
            this.txtBandName.Margin = new System.Windows.Forms.Padding(2);
            this.txtBandName.Name = "txtBandName";
            this.txtBandName.Size = new System.Drawing.Size(151, 20);
            this.txtBandName.TabIndex = 1;
            // 
            // txtBandRider
            // 
            this.txtBandRider.Location = new System.Drawing.Point(98, 75);
            this.txtBandRider.Margin = new System.Windows.Forms.Padding(2);
            this.txtBandRider.Name = "txtBandRider";
            this.txtBandRider.Size = new System.Drawing.Size(151, 20);
            this.txtBandRider.TabIndex = 2;
            // 
            // txtBandArtistCut
            // 
            this.txtBandArtistCut.Location = new System.Drawing.Point(98, 103);
            this.txtBandArtistCut.Margin = new System.Windows.Forms.Padding(2);
            this.txtBandArtistCut.Name = "txtBandArtistCut";
            this.txtBandArtistCut.Size = new System.Drawing.Size(61, 20);
            this.txtBandArtistCut.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(7, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 16);
            this.label1.TabIndex = 11;
            this.label1.Text = "Band ID";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(163, 103);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 20);
            this.label2.TabIndex = 12;
            this.label2.Text = "%";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(7, 47);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 16);
            this.label3.TabIndex = 13;
            this.label3.Text = "Band Name";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(7, 76);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 16);
            this.label4.TabIndex = 14;
            this.label4.Text = "Rider";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(7, 106);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 16);
            this.label5.TabIndex = 15;
            this.label5.Text = "Artist Cut";
            // 
            // btnDoneBand
            // 
            this.btnDoneBand.Location = new System.Drawing.Point(184, 103);
            this.btnDoneBand.Name = "btnDoneBand";
            this.btnDoneBand.Size = new System.Drawing.Size(65, 29);
            this.btnDoneBand.TabIndex = 16;
            this.btnDoneBand.Text = "Done";
            this.btnDoneBand.UseVisualStyleBackColor = true;
            this.btnDoneBand.Click += new System.EventHandler(this.btnDoneBand_Click);
            // 
            // frmBand
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 141);
            this.Controls.Add(this.btnDoneBand);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtBandArtistCut);
            this.Controls.Add(this.txtBandRider);
            this.Controls.Add(this.txtBandName);
            this.Controls.Add(this.txtBandId);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(300, 180);
            this.MinimizeBox = false;
            this.Name = "frmBand";
            this.ShowIcon = false;
            this.Load += new System.EventHandler(this.frmBand_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtBandId;
        private System.Windows.Forms.TextBox txtBandName;
        private System.Windows.Forms.TextBox txtBandRider;
        private System.Windows.Forms.TextBox txtBandArtistCut;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnDoneBand;
    }
}