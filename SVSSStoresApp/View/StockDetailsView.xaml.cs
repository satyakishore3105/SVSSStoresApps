using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SVSSStoresApp.View
{
    /// <summary>
    /// Interaction logic for StockDetailsView.xaml
    /// </summary>
    public partial class StockDetailsView : UserControl
    {
        public StockDetailsView()
        {
            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "dd-MMM-yyyy";
            Thread.CurrentThread.CurrentCulture = ci;
            InitializeComponent();
            List<String> stringList = new List<String>();
            stringList.Add("From Group");
            stringList.Add("Search by Item");
            this.splBtnItemSearch.ItemsSource = stringList;
            //this.tstValue.SelectedItems
        }
    }
}
