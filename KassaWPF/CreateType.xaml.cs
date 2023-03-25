using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace KassaWPF
{
    /// <summary>
    /// Логика взаимодействия для CreateType.xaml
    /// </summary>
    public partial class CreateType : Window
    {
        public string[] types = new string[] { };
        public CreateType()
        {
            InitializeComponent();
        }

        private void NewTypeyes_Click(object sender, RoutedEventArgs e)
        {
            types = Myconv.MyDeserialize<string[]>("Типы.json");
            MainWindow window2 = new MainWindow();
            types = types.Append(NewTextBox.Text).ToArray();
            Myconv.Myserialize(types, "Типы.json");
            Close();


        }
    }
}
