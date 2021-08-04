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
    public partial class frmBand : MetroSetForm
    {
        private Chain slcChain;
        private string idUser;
        private bool edit;
        private Band editingBand;

        public frmBand(bool edit, string idUser, Chain selectedChain, Band band = null)
        {
            InitializeComponent();
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
                txtBandName.Text = editingBand.Name;
                txtBandId.Text = editingBand.Id;
                this.txtBandId.Enabled = false;
                txtBandRider.Text = editingBand.Rider.ToString();
                txtBandArtistCut.Text = editingBand.ArtistCut.ToString();
            }
        }

        private void btnDoneBand_Click(object sender, EventArgs e)
        {
            if (edit)
            {
                editingBand.Name = txtBandName.Text;
                editingBand.Rider = Int32.Parse(txtBandRider.Text);
                editingBand.ArtistCut = Int32.Parse(txtBandArtistCut.Text);

                SqliteDataAccess.UpdateBand(editingBand);
                this.Close();

            }
            else
            {
                Band b = new Band()
                {
                    Id = txtBandId.Text,
                    Name = txtBandName.Text,
                    Rider = Int32.Parse(txtBandRider.Text),
                    ArtistCut = Int32.Parse(txtBandArtistCut.Text),
                    UserId = this.idUser,
                    ChainId = this.slcChain.Id
                };

                SqliteDataAccess.SaveBand(b);

                this.Close();
            }
        }
    }
}
