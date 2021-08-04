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
    public partial class frmTouring : MetroSetForm
    {
        private int year;
        private Band band;
        bool edit;
        int idutilizador;
        string tourType;
        DateTime firstDay;
        List<Control> layoutListDates = new List<Control>();
        List<Control> layoutListSingleTour = new List<Control>();
        List<Control> layoutListDoubleTour = new List<Control>();

        public frmTouring(int year, bool edit, int idUser, Band band)
        {
            InitializeComponent();
            this.edit = edit;
            this.idutilizador = idUser;
            this.band = band;
            this.year = year;
            this.layoutListDates.Add(tableLayoutPanel14);
            this.layoutListDates.Add(tableLayoutPanel15);
            this.layoutListDates.Add(tableLayoutPanel16);
            this.layoutListDates.Add(tableLayoutPanel17);
            this.layoutListDates.Add(tableLayoutPanel18);
            this.layoutListDates.Add(tableLayoutPanel19);
            this.layoutListDates.Add(tableLayoutPanel20);
            this.layoutListDates.Add(tableLayoutPanel21);

            this.layoutListSingleTour.Add(tableLayoutPanel22);
            this.layoutListSingleTour.Add(tableLayoutPanel24);
            this.layoutListSingleTour.Add(tableLayoutPanel26);
            this.layoutListSingleTour.Add(tableLayoutPanel28);
            this.layoutListSingleTour.Add(tableLayoutPanel30);
            this.layoutListSingleTour.Add(tableLayoutPanel32);
            this.layoutListSingleTour.Add(tableLayoutPanel34);
            this.layoutListSingleTour.Add(tableLayoutPanel36);

            this.layoutListDoubleTour.Add(tableLayoutPanel23);
            this.layoutListDoubleTour.Add(tableLayoutPanel25);
            this.layoutListDoubleTour.Add(tableLayoutPanel27);
            this.layoutListDoubleTour.Add(tableLayoutPanel29);
            this.layoutListDoubleTour.Add(tableLayoutPanel31);
            this.layoutListDoubleTour.Add(tableLayoutPanel33);
            this.layoutListDoubleTour.Add(tableLayoutPanel35);
            this.layoutListDoubleTour.Add(tableLayoutPanel37);



        }

        private void frmTouring_Load(object sender, EventArgs e)
        {


            firstDay = Helper.GetInitialDateByYear(this.year);
            FillLabelDates(firstDay, layoutListDates);
            AddAutoCompleteToTextbox(layoutListSingleTour);
            AddAutoCompleteToTextbox(layoutListDoubleTour);

            FillTimeComboBox(layoutListSingleTour);
            FillTimeComboBox(layoutListDoubleTour);

            lblYear.Text = "Year: " + this.year.ToString();
            lblBand.Text = "Band: " + this.band.Name;

            if (edit)
            {
                FillControlsFromDB(layoutListSingleTour, layoutListDoubleTour);
            }
            else
            {
                foreach (Control ctrl in tableLayoutPanel1.Controls)
                {

                    if (ctrl.Name == "tableLayoutPanel11")
                    {
                        ctrl.Enabled = true;
                    }
                    else
                    {
                        ctrl.Enabled = false;
                    }

                    foreach (Control Children in ctrl.Controls)
                    {
                        if (Children.Name == "tableLayoutPanel38" || Children.Name == "tableLayoutPanel39")
                        {
                            Children.Enabled = false;
                        }
                    }

                }
            }
        }

        private void FillLabelDates(DateTime day, List<Control> ctrl)
        {
            IEnumerable<Control> queryControl = ctrl.OrderBy(c => c.Name);

            foreach (Control control in queryControl)
            {
                var labels = control.Controls.OfType<Label>()
                          .Where(c => c.Name.StartsWith("label"))
                          .ToList();

                IEnumerable<Label> queryLabel = labels.OrderBy(label => label.Name);

                foreach (Label label in queryLabel)
                {
                    label.Text = day.ToString().Split(' ')[0];
                    day = day.AddDays(1);
                }
            }

        }

        private void AddAutoCompleteToTextbox(List<Control> ctrl)
        {
            IEnumerable<Control> queryControl = ctrl.OrderBy(c => c.Name);

            foreach (Control control in queryControl)
            {
                var txtboxes = control.Controls.OfType<TextBox>()
                          .Where(c => c.Name.StartsWith("text"))
                          .ToList();

                IEnumerable<TextBox> querytxtBoxes = txtboxes.OrderBy(txtbox => txtbox.Name);

                foreach (TextBox txtbox in querytxtBoxes)
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
        }

        private void FillTimeComboBox(List<Control> ctrl)
        {
            IEnumerable<Control> queryControl = ctrl.OrderBy(c => c.Name);

            foreach (Control control in queryControl)
            {
                var cmbBoxes = control.Controls.OfType<ComboBox>()
                          .Where(c => c.Name.StartsWith("metro"))
                          .ToList();

                IEnumerable<ComboBox> querycmbBoxes = cmbBoxes.OrderBy(cmbBox => cmbBox.Name);

                foreach (ComboBox cmbbox in querycmbBoxes)
                {
                    List<string> times = new List<string>();
                    foreach (KeyValuePair<int, string> pair in Helper.validTimeSlots)
                    {
                        times.Add(pair.Value);
                    }

                    cmbbox.DataSource = times;
                }
            }
        }

        private void ChangeTime(string time, List<Control> listEntry)
        {

            foreach (Control control in listEntry)
            {
                var cmbBoxs = control.Controls.OfType<ComboBox>()
                          .Where(c => c.Name.StartsWith("metro"))
                          .ToList();

                foreach (ComboBox cmbbox in cmbBoxs)
                {
                    cmbbox.SelectedItem = time;
                }
            }
        }

        private List<Week> AttributeWeekDay(List<Control> dates, List<Control> singleTour, List<Control> doubleTour = null)
        {
            List<Week> tourDays = new List<Week>();

            IEnumerable<Control> queryDates = dates.OrderBy(c => c.Name);
            IEnumerable<Control> querySingle = singleTour.OrderBy(c => c.Name);


            foreach (Control control in queryDates)
            {
                int i = 0;

                var labels = control.Controls.OfType<Label>()
                          .Where(c => c.Name.StartsWith("label"))
                          .ToList();

                IEnumerable<Label> queryLabel = labels.OrderBy(label => label.Name);

                Week week = new Week();


                foreach (Label label in queryLabel)
                {
                    Day day = new Day();
                    day.Date = label.Text;
                    week.Days[i] = day;
                    i++;
                }
                tourDays.Add(week);
            }

            int j = 0;
            foreach (Control control in querySingle)
            {
                var textBoxs = control.Controls.OfType<TextBox>()
                          .Where(c => c.Name.StartsWith("text"))
                          .ToList();

                var comboBoxs = control.Controls.OfType<ComboBox>()
                          .Where(c => c.Name.StartsWith("metro"))
                          .ToList();

                IEnumerable<TextBox> queryText = textBoxs.OrderBy(textBox => textBox.Name);
                IEnumerable<ComboBox> queryCombo = comboBoxs.OrderBy(comboBox => comboBox.Name);


                int i = 0;

                foreach (TextBox txtBox in queryText)
                {
                    tourDays[j].Days[i].City1 = txtBox.Text.ToString();
                    i++;
                }

                i = 0;

                foreach (ComboBox comboBox in queryCombo)
                {
                    tourDays[j].Days[i].Time1 = comboBox.SelectedItem.ToString();
                    i++;
                }

                j++;
            }

            j = 0;
            if (doubleTour != null)
            {
                IEnumerable<Control> queryDouble = doubleTour.OrderBy(c => c.Name);

                foreach (Control control in queryDouble)
                {
                    var textBoxs = control.Controls.OfType<TextBox>()
                              .Where(c => c.Name.StartsWith("text"))
                              .ToList();

                    var comboBoxs = control.Controls.OfType<ComboBox>()
                              .Where(c => c.Name.StartsWith("metro"))
                              .ToList();

                    IEnumerable<TextBox> queryText = textBoxs.OrderBy(textBox => textBox.Name);
                    IEnumerable<ComboBox> queryCombo = comboBoxs.OrderBy(comboBox => comboBox.Name);


                    int i = 0;

                    foreach (TextBox txtBox in queryText)
                    {
                        tourDays[j].Days[i].City2 = txtBox.Text.ToString();
                        i++;
                    }

                    i = 0;

                    foreach (ComboBox comboBox in queryCombo)
                    {
                        tourDays[j].Days[i].Time2 = comboBox.SelectedItem.ToString();
                        i++;
                    }

                    j++;

                }
            }

            return tourDays;
        }

        private List<Week> ConvertTourDayToDay(List<TourDay> tourDay)
        {
            List<Week> outputList = new List<Week>();
            int cntTourDay = 0;
            int cntDay = 0;

            for (int i = 0; i < 8; i++)
            {
                Week week = new Week();
                foreach (Day day in week.Days)
                {
                    //Checks if the Tour is using a Single or Double setup

                    Day d = new Day
                    {
                        Date = tourDay[cntTourDay].Day,
                        City1 = tourDay[cntTourDay].City,
                        Time1 = tourDay[cntTourDay].Time
                    };


                    if (tourType.Equals("Double"))
                    {
                        d.City2 = tourDay[cntTourDay + 56].City;
                        d.Time2 = tourDay[cntTourDay + 56].Time;
                    }

                    cntTourDay++;
                    week.Days[cntDay] = d;
                    cntDay++;
                }
                cntDay = 0;
                outputList.Add(week);

            }

            return outputList;
        }

        private void FillControlsFromDB(List<Control> singleTour, List<Control> doubleTour)
        {
            tourType = SqliteDataAccess.GetTour(year, Int32.Parse(band.Id), idutilizador).Type;

            if (tourType.Equals("Single"))
            {
                checkedListBoxTourType.SetSelected(0, true);
                checkedListBoxTourType.SetItemChecked(0, true);
                DisableControlsBasedOnTourType();
            }
            else if (tourType.Equals("Double"))
            {
                checkedListBoxTourType.SetSelected(1, true);
                checkedListBoxTourType.SetItemChecked(1, true);
                DisableControlsBasedOnTourType();
            }
            List<Week> week = new List<Week>();
            List<TourDay> td = SqliteDataAccess.GetTourDays(year, Int32.Parse(band.Id));
            if(td.Count > 0 )
            {
                //td = td.OrderBy(day => day.Day).ToList();
                week = ConvertTourDayToDay(td);
            }

            IEnumerable<Control> querySingle = singleTour.OrderBy(c => c.Name);

            int j = 0;
            foreach (Control control in querySingle)
            {
                var textBoxs = control.Controls.OfType<TextBox>()
                          .Where(c => c.Name.StartsWith("text"))
                          .ToList();

                var comboBoxs = control.Controls.OfType<ComboBox>()
                          .Where(c => c.Name.StartsWith("metro"))
                          .ToList();

                IEnumerable<TextBox> queryText = textBoxs.OrderBy(textBox => textBox.Name);
                IEnumerable<ComboBox> queryCombo = comboBoxs.OrderBy(comboBox => comboBox.Name);


                int i = 0;

                foreach (TextBox txtBox in queryText)
                {
                    txtBox.Text = week[j].Days[i].City1;
                        i++;
                }

                i = 0;

                foreach (ComboBox comboBox in queryCombo)
                {
                    comboBox.SelectedItem = week[j].Days[i].Time1;
                        i++;
                }

                j++;
            }

            j = 0;
            if (tourType.Equals("Double"))
            {
                IEnumerable<Control> queryDouble = doubleTour.OrderBy(c => c.Name);

                foreach (Control control in queryDouble)
                {
                    var textBoxs = control.Controls.OfType<TextBox>()
                              .Where(c => c.Name.StartsWith("text"))
                              .ToList();

                    var comboBoxs = control.Controls.OfType<ComboBox>()
                              .Where(c => c.Name.StartsWith("metro"))
                              .ToList();

                    IEnumerable<TextBox> queryText = textBoxs.OrderBy(textBox => textBox.Name);
                    IEnumerable<ComboBox> queryCombo = comboBoxs.OrderBy(comboBox => comboBox.Name);



                    int i = 0;

                    foreach (TextBox txtBox in queryText)
                    {
                        txtBox.Text = week[j].Days[i].City2;
                        i++;
                    }

                    i = 0;

                    foreach (ComboBox comboBox in queryCombo)
                    {
                        comboBox.SelectedItem = week[j].Days[i].Time2;
                        i++;
                    }

                    j++;

                }
            }
        }

        private void DisableControlsBasedOnTourType()
        {
            if (checkedListBoxTourType.SelectedItem.ToString().Equals("Double"))
            {
                tourType = "Double";
                foreach (Control ctrl in tableLayoutPanel1.Controls)
                {
                    if (ctrl.Name == "tableLayoutPanel11")
                    {
                        foreach (Control Children in ctrl.Controls)
                        {
                            if (Children.Name == "tableLayoutPanel38" || Children.Name == "tableLayoutPanel39")
                            {
                                Children.Enabled = true;
                            }
                        }
                    }
                }

                foreach (TableLayoutPanel tblpnl in layoutListDoubleTour)
                {
                    tblpnl.Enabled = true;
                }
            }
            else
            {
                tourType = "Single";
                foreach (Control ctrl in tableLayoutPanel1.Controls)
                {
                    if (ctrl.Name == "tableLayoutPanel11")
                    {
                        foreach (Control Children in ctrl.Controls)
                        {
                            if (Children.Name == "tableLayoutPanel38")
                            {
                                Children.Enabled = true;
                            }
                            if (Children.Name == "tableLayoutPanel39")
                            {
                                Children.Enabled = false;
                            }
                        }
                    }
                }

                foreach (TableLayoutPanel tblpnl in layoutListDoubleTour)
                {
                    tblpnl.Enabled = false;
                }
            }

            foreach (Control ctrl in tableLayoutPanel1.Controls)
            {
                if (ctrl.Name == "tableLayoutPanel12")
                {
                    ctrl.Enabled = true;
                }

            }
        }
        private void checkedListBoxTourType_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisableControlsBasedOnTourType();
        }

        private void checkedListBoxTourType_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            Helper.SingleCheckBoxSelection(checkedListBoxTourType, e);

            foreach (Control ctrl in tableLayoutPanel1.Controls)
            {
                ctrl.Enabled = true;
            }
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
                    ChangeTime("12:00", layoutListSingleTour);
                    break;
                case "14:00":
                    ChangeTime("14:00", layoutListSingleTour);
                    break;
                case "16:00":
                    ChangeTime("16:00", layoutListSingleTour);
                    break;
                case "18:00":
                    ChangeTime("18:00", layoutListSingleTour);
                    break;
                case "20:00":
                    ChangeTime("20:00", layoutListSingleTour);
                    break;
                case "22:00":
                    ChangeTime("22:00", layoutListSingleTour);
                    break;
                default:
                    break;

            }
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
                    ChangeTime("12:00", layoutListDoubleTour);
                    break;
                case "14:00":
                    ChangeTime("14:00", layoutListDoubleTour);
                    break;
                case "16:00":
                    ChangeTime("16:00", layoutListDoubleTour);
                    break;
                case "18:00":
                    ChangeTime("18:00", layoutListDoubleTour);
                    break;
                case "20:00":
                    ChangeTime("20:00", layoutListDoubleTour);
                    break;
                case "22:00":
                    ChangeTime("22:00", layoutListDoubleTour);
                    break;
                default:
                    break;

            }
        }

        private void checkedListBoxTimeSingle_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            Helper.SingleCheckBoxSelection(checkedListBoxTimeSingle, e);
        }

        private void checkedListBoxTimeDouble_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            Helper.SingleCheckBoxSelection(checkedListBoxTimeDouble, e);
        }



        private void btnCheck_Click(object sender, EventArgs e)
        {

            List<Control> layoutListAll = layoutListSingleTour.Concat(layoutListDoubleTour).ToList();

            IEnumerable<Control> queryControl = layoutListAll.OrderBy(c => c.Name);

            foreach (Control control in queryControl)
            {
                var txtboxes = control.Controls.OfType<TextBox>()
                          .Where(c => c.Name.StartsWith("text"))
                          .ToList();

                IEnumerable<TextBox> querytxtBoxes = txtboxes.OrderBy(txtbox => txtbox.Name);

                foreach (TextBox txtbox in querytxtBoxes)
                {
                    txtbox.ForeColor = Color.Black;
                }
            }

            List<Week> calendar = new List<Week>();

            if (checkedListBoxTourType.GetItemCheckState(0) == CheckState.Checked)
            {
                calendar = AttributeWeekDay(layoutListDates, layoutListSingleTour);
            }
            else if (checkedListBoxTourType.GetItemCheckState(1) == CheckState.Checked)
            {
                calendar = AttributeWeekDay(layoutListDates, layoutListSingleTour, layoutListDoubleTour);
            }

            List<TourDay> Calendar = new List<TourDay>();

            foreach (Week week in calendar)
            {
                foreach (Day day in week.Days)
                {
                    TourDay tourDay = new TourDay();
                    tourDay.City = day.City1;
                    tourDay.Day = day.Date;
                    tourDay.Time = day.Time1;
                    tourDay.UserId = idutilizador;

                    Calendar.Add(tourDay); 
                }
            }

            if (checkedListBoxTourType.GetItemCheckState(1) == CheckState.Checked) //Checks if Double option is picked in ListBoxTourType
            {
                foreach (Week week in calendar)
                {
                    foreach (Day day in week.Days)
                    {
                        TourDay tourDay = new TourDay();
                        tourDay.City = day.City2;
                        tourDay.Day = day.Date;
                        tourDay.Time = day.Time2;
                        tourDay.UserId = idutilizador;

                        Calendar.Add(tourDay);
                    }
                }
            }

            //Calendar.OrderBy(c => c.Day);

            List<bool> temp = new List<bool>();
            foreach (TourDay day in Calendar) //Checks if day is already on the database
            {
                if (SqliteDataAccess.CheckTourDay(day) && !String.IsNullOrEmpty(day.City))
                {
                    temp.Add(true);
                }
                else
                {
                    temp.Add(false);
                }
            }

            
            int i = 0;

            foreach (Control control in layoutListSingleTour)
            {
                var txtboxes = control.Controls.OfType<TextBox>()
                          .Where(c => c.Name.StartsWith("text"))
                          .ToList();

                IEnumerable<TextBox> querytxtBoxes = txtboxes.OrderBy(txtbox => txtbox.Name);


                foreach (TextBox txtbox in querytxtBoxes)
                {
                    if (temp[i])
                    {
                        txtbox.ForeColor = Color.Red;
                    }

                    i++;
                }
            }
            
            if (checkedListBoxTourType.GetItemCheckState(1) == CheckState.Checked) //If Tour Type Double
            {
                //i = 0;

                foreach (Control control in layoutListDoubleTour)
                {
                    var txtboxes = control.Controls.OfType<TextBox>()
                              .Where(c => c.Name.StartsWith("text"))
                              .ToList();

                    IEnumerable<TextBox> querytxtBoxes = txtboxes.OrderBy(txtbox => txtbox.Name);


                    foreach (TextBox txtbox in querytxtBoxes)
                    {
                        if (temp[i])
                        {
                            txtbox.ForeColor = Color.Red;
                        }

                        i++;
                    }
                }
            }


        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            Tour tour = new Tour();

            if (!edit)
            {
                SqliteDataAccess.SaveTour(year, tourType, Int32.Parse(band.Id), idutilizador);
            }
            else
            {
                tour = SqliteDataAccess.GetTour(year, Int32.Parse(band.Id), idutilizador);
                tour.Year = year;
                tour.Type = tourType;
                SqliteDataAccess.UpdateTour(tour);
            }
            int TourID = -1;

            TourID = SqliteDataAccess.GetTour(year, Int32.Parse(band.Id), idutilizador).Id;

            if (TourID != -1)
            {
                List<Week> calendar = new List<Week>();

                if (checkedListBoxTourType.GetItemCheckState(0) == CheckState.Checked)
                {
                    calendar = AttributeWeekDay(layoutListDates, layoutListSingleTour);
                }
                else if (checkedListBoxTourType.GetItemCheckState(1) == CheckState.Checked)
                {
                    calendar = AttributeWeekDay(layoutListDates, layoutListSingleTour, layoutListDoubleTour);
                }

                List<TourDay> Calendar = new List<TourDay>();

                foreach (Week week in calendar)
                {
                    foreach (Day day in week.Days)
                    {
                        TourDay tourDay = new TourDay();
                        tourDay.City = day.City1;
                        tourDay.Day = day.Date;
                        tourDay.Time = day.Time1;
                        tourDay.UserId = idutilizador;
                        tourDay.TourId = TourID;

                        Calendar.Add(tourDay);
                    }
                }
                if (checkedListBoxTourType.GetItemCheckState(1) == CheckState.Checked) //Checks if Double option is picked in ListBoxTourType
                {
                    foreach (Week week in calendar)
                    {
                        foreach (Day day in week.Days)
                        {
                            TourDay tourDay = new TourDay();
                            tourDay.City = day.City2;
                            tourDay.Day = day.Date;
                            tourDay.Time = day.Time2;
                            tourDay.UserId = idutilizador;
                            tourDay.TourId = TourID;

                            Calendar.Add(tourDay);
                        }
                    }
                }

                Calendar.OrderBy(c => c.Day);

                if (edit)
                {
                    SqliteDataAccess.DeleteTourDays(tour.Id);
                    SqliteDataAccess.SaveTourDays(Calendar);
                }
                else
                {
                    SqliteDataAccess.SaveTourDays(Calendar);
                }
                    
                    

                this.Close();
            }
        }
    }
}
