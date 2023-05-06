using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace MissingC
{

    public partial class frmChain : Form
    {
        List<Club> addedClubs = new List<Club>();
        List<Club> removedClubs = new List<Club>();
        Chain editingChain = new Chain();
        bool edit;
        int idUser;

        public frmChain(bool edit, int idUser, Chain chain = null)
        {

            InitializeComponent();

            this.MaximizeBox = false;
            this.ShowIcon = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            listBoxClubPool.DisplayMember = "nameClub";
            listBoxClubChain.DisplayMember = "nameClub";
            this.idUser = idUser;
            this.edit = edit;
            if (chain != null)
                editingChain = chain;
        }

        private void frmChain_Load(object sender, EventArgs e)
        {
            PopulateClubPool();

            if (edit)
            {
                txtChainName.Text = editingChain.nameChain.ToString();
                PopulateClubChain();
            }
        }

        private void PopulateClubPool()
        {
            List<Club> temp = new List<Club>(SqliteDataAccess.LoadClubsPerChain(this.idUser));

            foreach (Club club in temp)
            {
                listBoxClubPool.Items.Add(club);
            }
        }
        private void PopulateClubChain()
        {
            List<Club> temp = new List<Club>(SqliteDataAccess.LoadClubsPerChain(idUser, editingChain.idChain));

            foreach (Club club in temp)
            {
                listBoxClubChain.Items.Add(club);
            }
        }

        private void btnAddClub_Click(object sender, EventArgs e)
        {
            if (listBoxClubPool.SelectedItems.Count > 0)
            {
                if (listBoxClubPool.SelectedItems.Count > 1)
                {

                    List<Club> pool = listBoxClubPool.SelectedItems.Cast<Club>().ToList();

                    foreach (Club selected in pool)
                    {
                        if (edit)
                        {
                            addedClubs.Add(selected);
                        }

                        listBoxClubChain.Items.Add(selected);
                        listBoxClubPool.Items.Remove(selected);
                    }

                }
                else
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
        }

        private void btnSubClub_Click(object sender, EventArgs e)
        {
            if (listBoxClubChain.SelectedItems.Count > 0)
            {
                if (listBoxClubChain.SelectedItems.Count > 1)
                {
                    List<Club> pool = listBoxClubChain.SelectedItems.Cast<Club>().ToList();

                    foreach (Club selected in pool)
                    {
                        if (edit)
                        {
                            addedClubs.Add(selected);
                        }

                        listBoxClubPool.Items.Add(selected);
                        listBoxClubChain.Items.Remove(selected);
                    }
                }
                else
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
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtChainName.Text.ToString()))
            {
                if (edit)
                {
                    Chain c = new Chain();
                    c.nameChain = txtChainName.Text.ToString();
                    c.idUserChain = this.idUser;
                    c.idChain = editingChain.idChain;

                    SqliteDataAccess.UpdateChain(c);

                    List<Club> temp = new List<Club>();

                    foreach (Club club in addedClubs)
                    {
                        club.idChainClub = editingChain.idChain;
                        temp.Add(club);
                    }

                    foreach (Club club in removedClubs)
                    {
                        club.idChainClub = null;
                        temp.Add(club);
                    }

                    SqliteDataAccess.UpdateChainInClubs(temp);

                    this.Close();
                }
                else
                {
                    Chain c = new Chain();
                    c.nameChain = txtChainName.Text.ToString();
                    c.idUserChain = this.idUser;

                    SqliteDataAccess.SaveChain(c);

                    String chainId = SqliteDataAccess.LoadChainByName(idUser, c.nameChain).idChain.ToString();

                    List<Club> temp = new List<Club>();

                    for (int i = 0; i < listBoxClubChain.Items.Count; i++)
                    {
                        Club club = listBoxClubChain.Items[i] as Club;
                        club.idChainClub = chainId;

                        temp.Add(club);
                    }

                    SqliteDataAccess.UpdateChainInClubs(temp);

                    this.Close();
                }
            }
        }


    }
}
