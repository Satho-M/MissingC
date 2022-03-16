using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MissingC
{
    public static class Helper
    {
        public static SortedDictionary<int, string> Scoring = new SortedDictionary<int, string>
            { { 0, "truly abysmal" }, { 1, "abysmal" }, { 2, "bottom dwelling" }, { 3, "horrendous" }, { 4, "dreadful" }, { 5, "terrible" },
            { 6, "poor" }, { 7, "below average" }, { 8, "mediocre" }, { 9, "above average" }, { 10, "decent" }, { 11, "nice" }, { 12, "pleasant" },
            { 13, "good" }, { 14, "sweet" }, { 15, "splendid" }, { 16, "awesome" }, { 17, "great" }, { 18, "terrific" }, { 19, "wonderful" },
            { 20, "incredible" }, { 21, "perfect" }, { 22, "revolutionary" }, { 23, "mind melting" }, { 24, "earth shaking" }, { 25, "GOD SMACKING" }, { 26, "GOD SMACKINGLY GLORIOUS" } };
        public static SortedDictionary<int, string> validTimeSlots = new SortedDictionary<int, string>
            { { 0, "12:00" }, { 1, "14:00" }, { 2, "16:00" }, { 3, "18:00" }, { 4, "20:00" }, { 5, "22:00" } }; //PPM Time Slots for Shows and respective IDs
        public static SortedDictionary<string, int> validCitySlots = new SortedDictionary<string, int>
        { { "Amsterdam", 8 }, { "Ankara", 35 }, { "Antalya", 61 }, { "Baku", 58 }, { "Barcelona", 9 }, { "Belgrade", 36 },
        { "Berlin", 7 }, { "Brussels", 33 }, { "Bucharest", 46 }, { "Budapest", 42 }, { "Buenos Aires", 17 }, { "Chicago" , 60},
        { "Copenhagen", 22 }, { "Dubrovnik", 29 }, { "Glasgow", 27 }, { "Helsinki", 19 }, { "Istanbul", 30 }, { "Izmir", 47 },
        { "Jakarta", 55 }, { "Johannesburg", 51 }, { "Kyiv", 56 }, { "London", 5 }, { "Los Angeles", 14 }, { "Madrid", 24 },
        { "Manila", 54 }, { "Melbourne", 10 }, { "Mexico City", 32 }, { "Milan", 52 }, { "Montreal", 38 }, { "Moscow", 18 },
        { "Nashville", 11 }, { "New York", 6 }, { "Paris", 20 }, { "Porto", 31 }, { "Rio de Janeiro", 25 }, { "Rome", 23 },
        { "São Paulo", 21 }, { "Sarajevo", 49 }, { "Seattle", 50 }, { "Shanghai", 45 }, { "Singapore", 39 }, { "Sofia", 53 },
        { "Stockholm", 1 }, { "Tallinn", 34 }, { "Tokyo", 62 }, { "Toronto", 16 }, { "Tromsø", 26 }, { "Warsaw", 48 }, { "Vilnius", 28 }
        }; //PPM Cities and respective IDs
        public static IEnumerable<TControl> GetChildControls<TControl>(Control control) where TControl : Control
        {
            var children = (control.Controls != null) ? control.Controls.OfType<TControl>() : Enumerable.Empty<TControl>();
            return children.SelectMany(c => GetChildControls<TControl>(c)).Concat(children);
        }
        public static void SingleCheckBoxSelection(CheckedListBox checkBox, ItemCheckEventArgs e)
        {
            // Ensure that we are checking an item
            if (e.NewValue != CheckState.Checked)
            {
                return;
            }

            // Get the items that are selected
            CheckedListBox.CheckedIndexCollection selectedItems = checkBox.CheckedIndices;

            // Check that we have at least 1 item selected
            if (selectedItems.Count > 0)
            {
                // Uncheck the other item
                checkBox.SetItemChecked(selectedItems[0], false);
            }
        }
        public static DateTime GetInitialDateByYear(int yearPPM)
        {
            DateTime firstPPMDate = new DateTime(2003, 01, 01);

            int DiferenceYear = yearPPM - 1;

            return (firstPPMDate.AddDays(DiferenceYear * 56));
        }
        

    }
}
