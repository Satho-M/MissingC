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
    public partial class frmTouring : Form
    {
        private int year;
        private Band band;
        bool edit;
        int idutilizador;
        groupTextCombo[] groupTextCombos;



        public frmTouring(int year, bool edit, int idUser, Band band)
        {
            InitializeComponent();

            this.MaximizeBox = false;
            this.ShowIcon = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            this.edit = edit;
            this.idutilizador = idUser;
            this.band = band;
            this.year = year;

            InfolblTouringBand.Text = this.band.nameBand;
            InfolblTouringYear.Text = this.year.ToString();
            DisableOrEnableControls(false);

            GroupLabelTextCombo(tableLayoutPanelSingle, tableLayoutPanelDouble);
        }

        private void newfrmTouring_Load(object sender, EventArgs e)
        {
            AddAutoCompleteToTextbox(tableLayoutPanelSingle);
            AddAutoCompleteToTextbox(tableLayoutPanelDouble);
            FillLabelDates(Helper.GetInitialDateByYear(this.year), tableLayoutPanelDates);
            FillTimeComboBox(tableLayoutPanelSingle);
            FillTimeComboBox(tableLayoutPanelDouble);

            if (edit)
            {
                FillControlsFromDB(groupTextCombos, this.year, this.band.idBand);
            }
        }

        private void AddAutoCompleteToTextbox(Control ctrl)
        {

            var txtboxes = ctrl.Controls.OfType<TextBox>()
                      .Where(c => c.Name.StartsWith("text"))
                      .ToList();

            foreach (TextBox txtbox in txtboxes)
            {
                AutoCompleteStringCollection allowedTypes = new AutoCompleteStringCollection();
                foreach (KeyValuePair<string, int> pair in Helper.validCitySlots)
                {
                    allowedTypes.Add(pair.Key);
                }
                txtbox.AutoCompleteCustomSource = allowedTypes;
                txtbox.AutoCompleteMode = AutoCompleteMode.Suggest;
                txtbox.AutoCompleteSource = AutoCompleteSource.CustomSource;
            }


        }
        private void FillLabelDates(DateTime day, Control ctrl)
        {

            var labels = ctrl.Controls.OfType<Label>()
                      .Where(c => c.Name.StartsWith("label"))
                      .ToList()
                      .OrderBy(c => c.Name);

            foreach (Label label in labels)
            {
                label.Text = day.ToString().Split(' ')[0];
                day = day.AddDays(1);
            }

        }
        private void FillTimeComboBox(Control ctrl)
        {
            var cmbBoxes = ctrl.Controls.OfType<ComboBox>()
                      .Where(c => c.Name.StartsWith("combo"))
                      .ToList();

            List<string> times = new List<string>();
            foreach (KeyValuePair<int, string> pair in Helper.validTimeSlots)
            {
                times.Add(pair.Value);
            }
            foreach (ComboBox cmbbox in cmbBoxes)
            {
                cmbbox.DataSource = times.ToList();
            }
        }
        private void DisableOrEnableControls(bool status)
        {
            tableLayoutPanelDouble.Enabled = status;
            tableLayoutPanelSingle.Enabled = status;
            tableLayoutPanelSchedDouble.Enabled = status;
            tableLayoutPanelSchedSingle.Enabled = status;
            btnTouringCheck.Enabled = status;
            btnTouringDone.Enabled = status;
        }
        private void EnableControlsBasedOnTourType()
        {
            if (checkedListBoxTourType.SelectedItem.ToString().Equals("Double"))
            {
                DisableOrEnableControls(true);
            }
            else
            {
                tableLayoutPanelSingle.Enabled = true;
                tableLayoutPanelDouble.Enabled = false;
                tableLayoutPanelSchedSingle.Enabled = true;
                tableLayoutPanelSchedDouble.Enabled = false;
                btnTouringCheck.Enabled = true;
                btnTouringDone.Enabled = true;
            }
        }

        //Saves to a global array the collection of Combo+TextBox for future loading of Tours
        private void GroupLabelTextCombo(Control SingleTour, Control DoubleTour)
        {
            groupTextCombo[] groupLabelTextComboTemp = new groupTextCombo[112];

            int contLTC = 0;

            var textBoxs = SingleTour.Controls.OfType<TextBox>()
                          .Where(c => c.Name.StartsWith("text"))
                          .OrderBy(c => c.Name)
                          .ToList();


            foreach (TextBox txtBox in textBoxs)
            {
                groupTextCombo ltc = new groupTextCombo();

                ltc.TextBox = txtBox;
                groupLabelTextComboTemp[contLTC] = ltc;

                contLTC++;
            }

            var comboBoxs = SingleTour.Controls.OfType<ComboBox>()
                          .Where(c => c.Name.StartsWith("combo"))
                          .OrderBy(c => c.Name)
                          .ToList();

            contLTC = 0;
            foreach (ComboBox cmbBox in comboBoxs)
            {
                groupLabelTextComboTemp[contLTC].ComboBox = cmbBox;

                contLTC++;
            }

            textBoxs = DoubleTour.Controls.OfType<TextBox>()
                          .Where(c => c.Name.StartsWith("text"))
                          .OrderBy(c => c.Name)
                          .ToList();

            contLTC = 56;
            foreach (TextBox txtBox in textBoxs)
            {
                groupTextCombo ltc = new groupTextCombo();

                ltc.TextBox = txtBox;
                groupLabelTextComboTemp[contLTC] = ltc;

                contLTC++;
            }

            comboBoxs = DoubleTour.Controls.OfType<ComboBox>()
                          .Where(c => c.Name.StartsWith("combo"))
                          .OrderBy(c => c.Name)
                          .ToList();

            contLTC = 56;
            foreach (ComboBox cmbBox in comboBoxs)
            {
                groupLabelTextComboTemp[contLTC].ComboBox = cmbBox;

                contLTC++;
            }

            groupTextCombos = groupLabelTextComboTemp;

        }

        private List<TourDay> GetTourDays(Control Dates, Control SingleTour, Control DoubleTour)
        {
            TourDay[] arrayTD;
            List<TourDay> listTD;

            if (checkedListBoxTourType.SelectedItem.ToString().Equals("Double"))
                arrayTD = AssignTourDays(Dates, SingleTour).Concat(AssignTourDays(Dates, DoubleTour)).ToArray();
            else
                arrayTD = AssignTourDays(Dates, SingleTour);

            listTD = arrayTD.OrderBy(c => DateTime.Parse(c.dateTD + " " + c.timeTD))
                .ToList();


            return listTD;
        }
        private TourDay[] AssignTourDays(Control Dates, Control Tour)
        {
            TourDay[] arrayTD = new TourDay[56];


            var labels = Dates.Controls.OfType<Label>()
                          .Where(c => c.Name.StartsWith("label"))
                          .OrderBy(c => c.Name)
                          .ToList();

            int i = 0;

            //Saves Day
            foreach (Label label in labels)
            {
                TourDay tourDay = new TourDay();
                tourDay.dateTD = label.Text;
                arrayTD[i] = tourDay;
                i++;
            }


            var textBoxs = Tour.Controls.OfType<TextBox>()
                          .Where(c => c.Name.StartsWith("text"))
                          .OrderBy(c => c.Name)
                          .ToList();
            i = 0;
            //Saves City
            foreach (TextBox txtBox in textBoxs)
            {
                if (!String.IsNullOrEmpty(txtBox.Text))
                {
                    arrayTD[i].cityTD = txtBox.Text;
                    arrayTD[i].textBoxNameTD = txtBox.Name;
                }

                i++;
            }

            var comboBoxs = Tour.Controls.OfType<ComboBox>()
                          .Where(c => c.Name.StartsWith("combo"))
                          .OrderBy(c => c.Name)
                          .ToList();
            i = 0;
            //Saves Time
            foreach (ComboBox cmbBox in comboBoxs)
            {
                if (arrayTD[i].cityTD != null)
                {
                    arrayTD[i].timeTD = cmbBox.SelectedItem.ToString();
                }

                i++;
            }

            TourDay[] arrayReturn;
            arrayReturn = arrayTD.Where(td => !String.IsNullOrEmpty(td.cityTD)).ToArray();

            return arrayReturn;
        }

        private void FillControlsFromDB(groupTextCombo[] Controls, int year, int idBand)
        {
            var tourType = SqliteDataAccess.GetTour(year, band.idBand, idutilizador).typeTour;

            if (tourType.Equals("Single"))
            {
                checkedListBoxTourType.SetSelected(0, true);
                checkedListBoxTourType.SetItemChecked(0, true);
                EnableControlsBasedOnTourType();
            }
            else if (tourType.Equals("Double"))
            {
                checkedListBoxTourType.SetSelected(1, true);
                checkedListBoxTourType.SetItemChecked(1, true);
                EnableControlsBasedOnTourType();
            }

            List<TourDay> tds = SqliteDataAccess.GetTourDays(year, idBand);

            foreach(TourDay td in tds)
            {
                foreach(groupTextCombo tc in Controls)
                {
                    if (td.textBoxNameTD.Equals(tc.TextBox.Name))
                    {
                        tc.TextBox.Text = td.cityTD;
                        tc.ComboBox.SelectedItem = td.timeTD;
                        break;
                    }
                }
            }
        }

        private void ChangeTime(string time, Control Ctrl)
        {
            var cmbBoxs = Ctrl.Controls.OfType<ComboBox>()
                .ToList();

            foreach (ComboBox cmbBox in cmbBoxs)
            {
                cmbBox.SelectedItem = time;
            }
        }

        private void checkedListBoxTourType_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            Helper.SingleCheckBoxSelection(checkedListBoxTourType, e);
        }

        private void checkedListBoxTourType_SelectedIndexChanged(object sender, EventArgs e)
        {
            EnableControlsBasedOnTourType();
        }

        private void checkedListBoxTimeSingle_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (checkedListBoxTimeDouble.SelectedIndex != -1)
            {
                if (checkedListBoxTimeDouble.SelectedIndex <= checkedListBoxTimeSingle.SelectedIndex)
                {
                    checkedListBoxTimeDouble.SetItemChecked(checkedListBoxTimeDouble.SelectedIndex, false);
                }
            }

            string c = checkedListBoxTimeSingle.SelectedItem.ToString();

            switch (c)
            {
                case "12:00":
                    ChangeTime("12:00", tableLayoutPanelSingle);
                    break;
                case "14:00":
                    ChangeTime("14:00", tableLayoutPanelSingle);
                    break;
                case "16:00":
                    ChangeTime("16:00", tableLayoutPanelSingle);
                    break;
                case "18:00":
                    ChangeTime("18:00", tableLayoutPanelSingle);
                    break;
                case "20:00":
                    ChangeTime("20:00", tableLayoutPanelSingle);
                    break;
                case "22:00":
                    ChangeTime("22:00", tableLayoutPanelSingle);
                    break;
                default:
                    break;

            }
        }

        private void checkedListBoxTimeSingle_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            Helper.SingleCheckBoxSelection(checkedListBoxTimeSingle, e);
        }

        private void checkedListBoxTimeDouble_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (checkedListBoxTimeSingle.SelectedIndex >= checkedListBoxTimeDouble.SelectedIndex)
            {
                checkedListBoxTimeSingle.SetItemChecked(checkedListBoxTimeSingle.SelectedIndex, false);
            }

            string c = checkedListBoxTimeDouble.SelectedItem.ToString();

            switch (c)
            {
                case "12:00":
                    ChangeTime("12:00", tableLayoutPanelDouble);
                    break;
                case "14:00":
                    ChangeTime("14:00", tableLayoutPanelDouble);
                    break;
                case "16:00":
                    ChangeTime("16:00", tableLayoutPanelDouble);
                    break;
                case "18:00":
                    ChangeTime("18:00", tableLayoutPanelDouble);
                    break;
                case "20:00":
                    ChangeTime("20:00", tableLayoutPanelDouble);
                    break;
                case "22:00":
                    ChangeTime("22:00", tableLayoutPanelDouble);
                    break;
                default:
                    break;

            }
        }

        private void checkedListBoxTimeDouble_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            Helper.SingleCheckBoxSelection(checkedListBoxTimeDouble, e);
        }
        
        private void btnTouringCheck_Click(object sender, EventArgs e)
        {    
            foreach(groupTextCombo tc in groupTextCombos)
            {
                tc.TextBox.ForeColor = Color.Black;
            }        

            List<TourDay> tdays;

            tdays = GetTourDays(tableLayoutPanelDates, tableLayoutPanelSingle, tableLayoutPanelDouble);

            List<string> colorRed = new List<string>();

            foreach(TourDay td in tdays)
            {
                if (SqliteDataAccess.CheckTourDay(td, band.idChainBand))
                {
                    colorRed.Add(td.textBoxNameTD);
                }
            }

            foreach(string cr in colorRed)
            {
                foreach (groupTextCombo tc in groupTextCombos)
                {
                    if (cr.Equals(tc.TextBox.Name))
                    {
                        tc.TextBox.ForeColor = Color.Red;
                        break;
                    }
                }
            }


        }
        private void btnTouringDone_Click(object sender, EventArgs e)
        {
            Tour tour = new Tour();
            List<TourDay> tdays;

            if (edit)
            {
                tour = SqliteDataAccess.GetTour(year, band.idBand, idutilizador);
                tour.typeTour = checkedListBoxTourType.SelectedItem.ToString();
                SqliteDataAccess.UpdateTour(tour);
            }
            else
            {
                SqliteDataAccess.SaveTour(year, checkedListBoxTourType.SelectedItem.ToString(), band.idBand, idutilizador);
            }

            int TourID = -1;

            TourID = SqliteDataAccess.GetTour(year, band.idBand, idutilizador).idTour;

            if (TourID != -1)
            {
                tdays = GetTourDays(tableLayoutPanelDates, tableLayoutPanelSingle, tableLayoutPanelDouble);

                //Adds TourID and UserID to TourDays
                foreach(TourDay td in tdays)
                {
                    td.idTourTD = TourID;
                    td.idUserTD = idutilizador;
                }

                //Saves TourDays
                if (edit)
                {
                    SqliteDataAccess.DeleteTourDays(tour.idTour);
                    SqliteDataAccess.SaveTourDays(tdays);
                }
                else
                {
                    SqliteDataAccess.SaveTourDays(tdays);
                }

                this.Close();
            }
        }
    }

    public class groupTextCombo
    {
        public TextBox TextBox { get; set; }
        public ComboBox ComboBox { get; set; }
    }
}
