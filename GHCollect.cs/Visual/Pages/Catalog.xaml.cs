using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using GHCollect.Data;

namespace GHCollect.Visual.Pages {
    
    public partial class Catalog : UserControl {
        public Catalog() {
            InitializeComponent();
            // Save default resources
            this.DefaultTitle = lb_Title.Text;
            this.DefaultDescription = lb_Description.Text;
            this.DefaultIcon = this.pic_icon.Source;
            // Add triggers
            this.list_files.SelectionChanged += list_files_SelectionChanged;
            this.bt_Refresh.Click += bt_Refresh_Click;
        }

        Collection select;

        public string DefaultTitle;
        public string DefaultDescription;
        ImageSource DefaultIcon;

        private void list_files_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            try {
                select = Collection.Read((string)this.list_files.SelectedItem);  // Get the collection
                this.pic_icon.Source = Functions.Base64.Decode.ToImageSource(select.Icon);  // Change icon
                this.lb_Title.Text = select.Name;  // Display name and description
                this.lb_Description.Text = select.Description;
                this.dl_DetailList.Clear();  // Display collection property
                foreach (var tProp in select.Detail) {
                    this.dl_DetailList.AddItem(tProp[0], tProp[1]);
                }
                this.bt_Open.IsEnabled = true;
                this.bt_Open.Visibility = Visibility.Visible;
            } catch { }
        }

        public string FileDirectory = "/Path/To/Files";

        /// <summary>
        /// Reload the folder
        /// </summary>
        public void Refresh() {
            this.bt_Open.IsEnabled = false;
            this.bt_Open.Visibility = Visibility.Collapsed;
            this.list_files.SelectionChanged -= list_files_SelectionChanged;  // Untrigger listbox because the Collection objects will be deleted.
            lb_Title.Text = this.DefaultTitle;  // Set to default displays
            lb_Description.Text= this.DefaultDescription;
            this.pic_icon.Source = this.DefaultIcon;
            this.dl_DetailList.Clear();
            this.list_files.Items.Clear();
            string[] jsonFiles = Directory.GetFiles(FileDirectory, "*.json");
            foreach (string jsonFile in jsonFiles) {
                this.list_files.Items.Add(jsonFile);
            }
            this.list_files.SelectionChanged += list_files_SelectionChanged; // Pay back the trigger
        }

        private void bt_Refresh_Click(object sender, RoutedEventArgs e) {
            this.Refresh();
        }
    }
}
