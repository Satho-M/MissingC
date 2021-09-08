using MetroSet_UI.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MissingC
{
    public partial class frmBand : Form
    {
        private Chain slcChain;
        private int idUser;
        private bool edit;
        private Band editingBand;

        public frmBand(bool edit, int idUser, Chain selectedChain, Band band = null)
        {
            InitializeComponent();

            this.MaximizeBox = false;
            this.ShowIcon = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            this.slcChain = selectedChain;
            this.idUser = idUser;
            this.edit = edit;
            if (band != null)
                editingBand = band;
        }

        private void frmBand_Load(object sender, EventArgs e)
        {
            if (edit)
            {
                txtBandName.Text = editingBand.nameBand;
                txtBandId.Text = editingBand.idBand.ToString();
                this.txtBandId.Enabled = false;
                txtBandRider.Text = editingBand.riderBand.ToString();
                txtBandArtistCut.Text = editingBand.artCutBand.ToString();
            }
        }

        private void btnDoneBand_Click(object sender, EventArgs e)
        {
            if (edit)
            {
                editingBand.nameBand = txtBandName.Text;
                editingBand.riderBand = Int32.Parse(txtBandRider.Text);
                editingBand.artCutBand = Int32.Parse(txtBandArtistCut.Text);

                SqliteDataAccess.UpdateBand(editingBand);
                this.Close();

            }
            else
            {
                Band b = new Band()
                {
                    idBand = Int32.Parse(txtBandId.Text),
                    nameBand = txtBandName.Text,
                    riderBand = Int32.Parse(txtBandRider.Text),
                    artCutBand = Int32.Parse(txtBandArtistCut.Text),
                    idUserBand = this.idUser,
                    idChainBand = this.slcChain.idChain
                };

                SqliteDataAccess.SaveBand(b);

                this.Close();
            }
        }
    }
}
