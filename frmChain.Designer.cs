namespace MissingC
{
    partial class frmChain
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
            this.listBoxClubChain = new System.Windows.Forms.ListBox();
            this.listBoxClubPool = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtChainName = new System.Windows.Forms.TextBox();
            this.btnAddClub = new System.Windows.Forms.Button();
            this.btnSubClub = new System.Windows.Forms.Button();
            this.btnDone = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listBoxClubChain
            // 
            this.listBoxClubChain.FormattingEnabled = true;
            this.listBoxClubChain.ItemHeight = 16;
            this.listBoxClubChain.Location = new System.Drawing.Point(311, 74);
            this.listBoxClubChain.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.listBoxClubChain.Name = "listBoxClubChain";
            this.listBoxClubChain.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.listBoxClubChain.Size = new System.Drawing.Size(185, 516);
            this.listBoxClubChain.TabIndex = 2;
            // 
            // listBoxClubPool
            // 
            this.listBoxClubPool.FormattingEnabled = true;
            this.listBoxClubPool.ItemHeight = 16;
            this.listBoxClubPool.Location = new System.Drawing.Point(15, 74);
            this.listBoxClubPool.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.listBoxClubPool.Name = "listBoxClubPool";
            this.listBoxClubPool.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.listBoxClubPool.Size = new System.Drawing.Size(185, 516);
            this.listBoxClubPool.Sorted = true;
            this.listBoxClubPool.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(16, 25);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Chain Name";
            // 
            // txtChainName
            // 
            this.txtChainName.Location = new System.Drawing.Point(133, 23);
            this.txtChainName.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtChainName.Name = "txtChainName";
            this.txtChainName.Size = new System.Drawing.Size(169, 22);
            this.txtChainName.TabIndex = 3;
            // 
            // btnAddClub
            // 
            this.btnAddClub.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnAddClub.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddClub.Location = new System.Drawing.Point(216, 183);
            this.btnAddClub.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnAddClub.Name = "btnAddClub";
            this.btnAddClub.Size = new System.Drawing.Size(80, 49);
            this.btnAddClub.TabIndex = 4;
            this.btnAddClub.Text = "►";
            this.btnAddClub.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnAddClub.UseVisualStyleBackColor = true;
            this.btnAddClub.Click += new System.EventHandler(this.btnAddClub_Click);
            // 
            // btnSubClub
            // 
            this.btnSubClub.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSubClub.Location = new System.Drawing.Point(216, 252);
            this.btnSubClub.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSubClub.Name = "btnSubClub";
            this.btnSubClub.Size = new System.Drawing.Size(80, 49);
            this.btnSubClub.TabIndex = 5;
            this.btnSubClub.Text = "◄";
            this.btnSubClub.UseVisualStyleBackColor = true;
            this.btnSubClub.Click += new System.EventHandler(this.btnSubClub_Click);
            // 
            // btnDone
            // 
            this.btnDone.Location = new System.Drawing.Point(216, 590);
            this.btnDone.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnDone.Name = "btnDone";
            this.btnDone.Size = new System.Drawing.Size(80, 37);
            this.btnDone.TabIndex = 6;
            this.btnDone.Text = "Done";
            this.btnDone.UseVisualStyleBackColor = true;
            this.btnDone.Click += new System.EventHandler(this.btnDone_Click);
            // 
            // frmChain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(509, 631);
            this.Controls.Add(this.btnDone);
            this.Controls.Add(this.btnSubClub);
            this.Controls.Add(this.btnAddClub);
            this.Controls.Add(this.txtChainName);
            this.Controls.Add(this.listBoxClubChain);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listBoxClubPool);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximumSize = new System.Drawing.Size(527, 678);
            this.Name = "frmChain";
            this.Load += new System.EventHandler(this.frmChain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ListBox listBoxClubChain;
        private System.Windows.Forms.ListBox listBoxClubPool;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtChainName;
        private System.Windows.Forms.Button btnAddClub;
        private System.Windows.Forms.Button btnSubClub;
        private System.Windows.Forms.Button btnDone;
    }
}