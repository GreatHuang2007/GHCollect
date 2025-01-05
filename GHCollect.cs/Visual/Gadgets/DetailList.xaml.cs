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

namespace GHCollect.Visual.Gadgets {
    /// <summary>
    /// DetailList.xaml 的交互逻辑
    /// </summary>
    public partial class DetailList : UserControl {
        public DetailList() {
            InitializeComponent();
        }

        private int count = 0;

        public void AddItem(string Name, string Value) {
            gd_main.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            var dpName = new TextBlock { Text = Name, Margin = new Thickness(5), HorizontalAlignment = HorizontalAlignment.Stretch, VerticalAlignment = VerticalAlignment.Stretch, TextWrapping = TextWrapping.Wrap };
            var dpValue = new TextBlock { Text = Value, Margin = new Thickness(5), HorizontalAlignment = HorizontalAlignment.Stretch, VerticalAlignment = VerticalAlignment.Stretch, TextWrapping = TextWrapping.Wrap };
            Grid.SetColumn(dpName, 0);
            Grid.SetColumn(dpName, 1);
            Grid.SetRow(dpName, count);
            Grid.SetRow(dpValue, count);
            gd_main.Children.Add(dpName);
            gd_main.Children.Add(dpValue);
            count++;
        }

        public void Clear() {
            count = 0;
            var textBlocks = gd_main.Children.OfType<TextBlock>().ToList();
            foreach (var textBlock in textBlocks) {
                gd_main.Children.Remove(textBlock);
            }
            gd_main.RowDefinitions.Clear();
        }
    }
}
