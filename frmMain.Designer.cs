
namespace MissingC
{
    partial class frmMain
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
            this.checkedListBoxChains = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnDeleteChain = new System.Windows.Forms.Button();
            this.btnEditChain = new System.Windows.Forms.Button();
            this.btnNewChain = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.checkedListBoxBands = new System.Windows.Forms.ListBox();
            this.btnDeleteBand = new System.Windows.Forms.Button();
            this.btnEditBand = new System.Windows.Forms.Button();
            this.btnNewBand = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnEditTour = new System.Windows.Forms.Button();
            this.btnEditTicket = new System.Windows.Forms.Button();
            this.cmbBoxPPMYear = new System.Windows.Forms.ComboBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnUpdateClubs = new System.Windows.Forms.Button();
            this.btnSendInvites = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.labelStatus = new System.Windows.Forms.Label();
            this.btnReset = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // checkedListBoxChains
            // 
            this.checkedListBoxChains.DisplayMember = "nameChain";
            this.checkedListBoxChains.FormattingEnabled = true;
            this.checkedListBoxChains.Location = new System.Drawing.Point(6, 23);
            this.checkedListBoxChains.Name = "checkedListBoxChains";
            this.checkedListBoxChains.Size = new System.Drawing.Size(230, 121);
            this.checkedListBoxChains.TabIndex = 1;
            this.checkedListBoxChains.SelectedIndexChanged += new System.EventHandler(this.checkedListBoxChains_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnDeleteChain);
            this.groupBox1.Controls.Add(this.btnEditChain);
            this.groupBox1.Controls.Add(this.btnNewChain);
            this.groupBox1.Controls.Add(this.checkedListBoxChains);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(560, 160);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Chains";
            // 
            // btnDeleteChain
            // 
            this.btnDeleteChain.Location = new System.Drawing.Point(247, 109);
            this.btnDeleteChain.Name = "btnDeleteChain";
            this.btnDeleteChain.Size = new System.Drawing.Size(86, 23);
            this.btnDeleteChain.TabIndex = 4;
            this.btnDeleteChain.Text = "Delete Chain";
            this.btnDeleteChain.UseVisualStyleBackColor = true;
            this.btnDeleteChain.Click += new System.EventHandler(this.btnDeleteChain_Click);
            // 
            // btnEditChain
            // 
            this.btnEditChain.Location = new System.Drawing.Point(247, 71);
            this.btnEditChain.Name = "btnEditChain";
            this.btnEditChain.Size = new System.Drawing.Size(86, 23);
            this.btnEditChain.TabIndex = 3;
            this.btnEditChain.Text = "Edit Chain";
            this.btnEditChain.UseVisualStyleBackColor = true;
            this.btnEditChain.Click += new System.EventHandler(this.btnEditChain_Click);
            // 
            // btnNewChain
            // 
            this.btnNewChain.Location = new System.Drawing.Point(247, 33);
            this.btnNewChain.Name = "btnNewChain";
            this.btnNewChain.Size = new System.Drawing.Size(86, 23);
            this.btnNewChain.TabIndex = 2;
            this.btnNewChain.Text = "New Chain";
            this.btnNewChain.UseVisualStyleBackColor = true;
            this.btnNewChain.Click += new System.EventHandler(this.btnNewChain_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.btnReset);
            this.groupBox2.Controls.Add(this.checkedListBoxBands);
            this.groupBox2.Controls.Add(this.btnDeleteBand);
            this.groupBox2.Controls.Add(this.btnEditBand);
            this.groupBox2.Controls.Add(this.btnNewBand);
            this.groupBox2.Location = new System.Drawing.Point(12, 181);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(560, 160);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Bands";
            // 
            // checkedListBoxBands
            // 
            this.checkedListBoxBands.DisplayMember = "nameClub";
            this.checkedListBoxBands.FormattingEnabled = true;
            this.checkedListBoxBands.Location = new System.Drawing.Point(6, 21);
            this.checkedListBoxBands.Name = "checkedListBoxBands";
            this.checkedListBoxBands.Size = new System.Drawing.Size(230, 121);
            this.checkedListBoxBands.TabIndex = 5;
            this.checkedListBoxBands.SelectedIndexChanged += new System.EventHandler(this.checkedListBoxBands_SelectedIndexChanged);
            // 
            // btnDeleteBand
            // 
            this.btnDeleteBand.Location = new System.Drawing.Point(247, 106);
            this.btnDeleteBand.Name = "btnDeleteBand";
            this.btnDeleteBand.Size = new System.Drawing.Size(86, 23);
            this.btnDeleteBand.TabIndex = 4;
            this.btnDeleteBand.Text = "Delete Band";
            this.btnDeleteBand.UseVisualStyleBackColor = true;
            this.btnDeleteBand.Click += new System.EventHandler(this.btnDeleteBand_Click);
            // 
            // btnEditBand
            // 
            this.btnEditBand.Location = new System.Drawing.Point(247, 68);
            this.btnEditBand.Name = "btnEditBand";
            this.btnEditBand.Size = new System.Drawing.Size(86, 23);
            this.btnEditBand.TabIndex = 3;
            this.btnEditBand.Text = "Edit Band";
            this.btnEditBand.UseVisualStyleBackColor = true;
            this.btnEditBand.Click += new System.EventHandler(this.btnEditBand_Click);
            // 
            // btnNewBand
            // 
            this.btnNewBand.Location = new System.Drawing.Point(247, 30);
            this.btnNewBand.Name = "btnNewBand";
            this.btnNewBand.Size = new System.Drawing.Size(86, 23);
            this.btnNewBand.TabIndex = 2;
            this.btnNewBand.Text = "New Band";
            this.btnNewBand.UseVisualStyleBackColor = true;
            this.btnNewBand.Click += new System.EventHandler(this.btnNewBand_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.btnEditTour);
            this.groupBox3.Controls.Add(this.btnEditTicket);
            this.groupBox3.Controls.Add(this.cmbBoxPPMYear);
            this.groupBox3.Location = new System.Drawing.Point(12, 347);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(560, 62);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Tour";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(192, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(157, 16);
            this.label1.TabIndex = 7;
            this.label1.Text = "Select the year of the tour";
            // 
            // btnEditTour
            // 
            this.btnEditTour.Location = new System.Drawing.Point(441, 22);
            this.btnEditTour.Name = "btnEditTour";
            this.btnEditTour.Size = new System.Drawing.Size(75, 23);
            this.btnEditTour.TabIndex = 6;
            this.btnEditTour.Text = "Edit Tour";
            this.btnEditTour.UseVisualStyleBackColor = true;
            this.btnEditTour.Click += new System.EventHandler(this.btnEditTour_Click);
            // 
            // btnEditTicket
            // 
            this.btnEditTicket.Location = new System.Drawing.Point(11, 22);
            this.btnEditTicket.Name = "btnEditTicket";
            this.btnEditTicket.Size = new System.Drawing.Size(122, 23);
            this.btnEditTicket.TabIndex = 5;
            this.btnEditTicket.Text = "Edit Ticket Price";
            this.btnEditTicket.UseVisualStyleBackColor = true;
            this.btnEditTicket.Click += new System.EventHandler(this.btnEditTicket_Click);
            // 
            // cmbBoxPPMYear
            // 
            this.cmbBoxPPMYear.FormattingEnabled = true;
            this.cmbBoxPPMYear.Location = new System.Drawing.Point(355, 24);
            this.cmbBoxPPMYear.Name = "cmbBoxPPMYear";
            this.cmbBoxPPMYear.Size = new System.Drawing.Size(80, 21);
            this.cmbBoxPPMYear.TabIndex = 0;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnUpdateClubs);
            this.groupBox4.Controls.Add(this.btnSendInvites);
            this.groupBox4.Location = new System.Drawing.Point(12, 467);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(560, 44);
            this.groupBox4.TabIndex = 7;
            this.groupBox4.TabStop = false;
            // 
            // btnUpdateClubs
            // 
            this.btnUpdateClubs.Location = new System.Drawing.Point(154, 14);
            this.btnUpdateClubs.Name = "btnUpdateClubs";
            this.btnUpdateClubs.Size = new System.Drawing.Size(100, 24);
            this.btnUpdateClubs.TabIndex = 1;
            this.btnUpdateClubs.Text = "Update Clubs";
            this.btnUpdateClubs.UseVisualStyleBackColor = true;
            this.btnUpdateClubs.Click += new System.EventHandler(this.btnUpdateClubs_Click);
            // 
            // btnSendInvites
            // 
            this.btnSendInvites.Location = new System.Drawing.Point(8, 13);
            this.btnSendInvites.Name = "btnSendInvites";
            this.btnSendInvites.Size = new System.Drawing.Size(100, 25);
            this.btnSendInvites.TabIndex = 0;
            this.btnSendInvites.Text = "Send Invites";
            this.btnSendInvites.UseVisualStyleBackColor = true;
            this.btnSendInvites.Click += new System.EventHandler(this.btnSendInvites_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.labelStatus);
            this.groupBox5.Location = new System.Drawing.Point(12, 415);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(560, 46);
            this.groupBox5.TabIndex = 8;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Status";
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelStatus.Location = new System.Drawing.Point(11, 17);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(0, 20);
            this.labelStatus.TabIndex = 0;
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(450, 30);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 23);
            this.btnReset.TabIndex = 6;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(429, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(125, 16);
            this.label2.TabIndex = 8;
            this.label2.Text = "Resets bands date ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(425, 75);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(129, 16);
            this.label4.TabIndex = 10;
            this.label4.Text = "of the last invite sent.";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 521);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(600, 560);
            this.Name = "frmMain";
            this.ShowIcon = false;
            this.Shown += new System.EventHandler(this.frmMain_Shown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox checkedListBoxChains;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnDeleteChain;
        private System.Windows.Forms.Button btnEditChain;
        private System.Windows.Forms.Button btnNewChain;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnDeleteBand;
        private System.Windows.Forms.Button btnEditBand;
        private System.Windows.Forms.Button btnNewBand;
        private System.Windows.Forms.ListBox checkedListBoxBands;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnEditTour;
        private System.Windows.Forms.Button btnEditTicket;
        private System.Windows.Forms.ComboBox cmbBoxPPMYear;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnSendInvites;
        private System.Windows.Forms.Button btnUpdateClubs;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnReset;
    }
}