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

    public partial class frmChain : MetroSetForm
    {
        private SortedDictionary<string, int> validGenreSlots = new SortedDictionary<string, int>{
            {"African Music", 18}, {"Blues", 15}, {"Classical", 16}, {"Country & Western", 13}, {"Electronica", 7}, {"Flamenco", 19},
            {"Heavy Metal", 5}, {"Hip Hop", 9}, {"Jazz", 14}, {"Latin Music", 17}, {"Modern Rock", 4}, {"Pop", 8}, {"Punk Rock", 6},
            {"R&B", 10}, {"Reggae", 11}, {"Rock", 3}, {"World Music", 12}
        }; //PPM Genres and respective IDs
        List<Club> addedClubs = new List<Club>();
        List<Club> removedClubs = new List<Club>();
        Chain editingChain = new Chain();
        bool edit;
        string idUser;

        public frmChain(bool edit, string idUser, Chain chain = null)
        {
            InitializeComponent();
            
            listBoxClubPool.DisplayMember = "Name";
            listBoxClubChain.DisplayMember = "Name";
            cmbBoxGenre.DataSource = new BindingSource(validGenreSlots, null);
            cmbBoxGenre.DisplayMember = "Key";
            cmbBoxGenre.ValueMember = "Key";
            this.idUser = idUser;
            this.edit = edit;
            if (chain != null)
                editingChain = chain;
        }

        private void frmChain_Load(object sender, EventArgs e)
        {
            PopulateClubPool();

            if(edit)
            {
                txtChainName.Text = editingChain.Name.ToString();
                cmbBoxGenre.SelectedItem = editingChain.Genre.ToString();

                PopulateClubChain();
            }
        }

        private void PopulateClubPool()
        {
            List<Club> temp = new List<Club>(SqliteDataAccess.LoadClubsPerChain(this.idUser));

            foreach(Club club in temp)
            {
                listBoxClubPool.Items.Add(club);
            }
        }
        private void PopulateClubChain()
        {
            List<Club> temp = new List<Club>(SqliteDataAccess.LoadClubsPerChain(idUser, editingChain.Id));

            foreach (Club club in temp)
            {
                listBoxClubChain.Items.Add(club);
            }
        }

        private void btnAddClub_Click(object sender, EventArgs e)
        {
            if (listBoxClubPool.SelectedItems.Count > 0)
            {
                if (edit)
                {
                    Club c = listBoxClubPool.SelectedItem as Club;
                    addedClubs.Add(c);
                }

                listBoxClubChain.Items.Add(listBoxClubPool.SelectedItem);
                listBoxClubPool.Items.Remove(listBoxClubPool.SelectedItem);
            }
        }



        private void btnSubClub_Click(object sender, EventArgs e)
        {
            if (listBoxClubChain.SelectedItems.Count > 0)
            {
                if (edit)
                {
                    Club c = listBoxClubChain.SelectedItem as Club;
                    removedClubs.Add(c);
                }

                listBoxClubPool.Items.Add(listBoxClubChain.SelectedItem);
                listBoxClubChain.Items.Remove(listBoxClubChain.SelectedItem);
            }
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtChainName.Text.ToString()) && !String.IsNullOrEmpty(cmbBoxGenre.GetItemText(this.cmbBoxGenre.SelectedItem)))
            {
                if (edit)
                {
                    Chain c = new Chain();
                    c.Name = txtChainName.Text.ToString();
                    c.Genre = this.cmbBoxGenre.GetItemText(this.cmbBoxGenre.SelectedItem);
                    c.UserId = this.idUser;
                    c.Id = editingChain.Id;

                    SqliteDataAccess.UpdateChain(c);

                    List<Club> temp = new List<Club>();

                    foreach (Club club in addedClubs)
                    {
                        club.ChainId = editingChain.Id;
                        temp.Add(club);
                    }

                    foreach (Club club in removedClubs)
                    {
                        club.ChainId = null;
                        temp.Add(club);
                    }

                    SqliteDataAccess.UpdateChaininClubs(temp);

                    this.Close();
                }
                else
                {
                    Chain c = new Chain();
                    c.Name = txtChainName.Text.ToString();
                    c.Genre = this.cmbBoxGenre.GetItemText(this.cmbBoxGenre.SelectedItem);
                    c.UserId = this.idUser;

                    SqliteDataAccess.SaveChain(c);

                    String chainId = SqliteDataAccess.LoadChainByName(idUser, c.Name).Id.ToString();

                    List<Club> temp = new List<Club>();

                    for (int i = 0; i < listBoxClubChain.Items.Count; i++)
                    {
                        Club club = listBoxClubChain.Items[i] as Club;
                        club.ChainId = chainId;

                        temp.Add(club);
                    }

                    SqliteDataAccess.UpdateChaininClubs(temp);

                    this.Close();
                }
            }
        }

       
    }
}
