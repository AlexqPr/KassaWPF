using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
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

namespace KassaWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
      
        public MainWindow()
        {
            InitializeComponent();
            string[] types = Myconv.MyDeserialize<string[]>("Типы.json");
            ComboBox.ItemsSource = types;

            DatePicker.SelectedDate = DateTime.Today; //Выставление изначальной даты
            Read();
        }

        private void NewType_Click(object sender, RoutedEventArgs e)
        {
            CreateType window = new CreateType();
            window.ShowDialog();
            string[] types = Myconv.MyDeserialize<string[]>("Типы.json");
            ComboBox.ItemsSource = types;
        }

        private void Create()
        {
            Zapis newzap = new Zapis(NewName.Text, (string)ComboBox.SelectedItem, Convert.ToInt32(Summa.Text), Convert.ToDateTime(DatePicker.Text));
           
           
            List<Zapis> zapis = Myconv.MyDeserialize<List<Zapis>>("Заметки.json");
            zapis.Add(newzap);
            Myconv.Myserialize(zapis, "Заметки.json");


            //Сброс полей
            NewName.Text = "";
            Summa.Text = "";
            ComboBox.SelectedItem = null;

            Read();
        }


        private void Read()
        {
            List<Zapis> newlist = Myconv.MyDeserialize<List<Zapis>>("Заметки.json");

            List<Zapis> foread = new List<Zapis>();

            DateTime data = Convert.ToDateTime(DatePicker.Text);
            foreach (var item in newlist) //Цикл для поиска заметок на эту дату
            {
                if(item.data == data)
                {
                    foread.Add(item);
                }
            }

            MyData.ItemsSource = null;
            MyData.ItemsSource = foread;
            int sum = 0;
            foreach (var item in newlist)
            {
                if (item.data == data)
                {
                    if(item.isPOs == true)
                    {
                        sum += item.Sum;
                    }
                    else
                    {
                        sum -= item.Sum;
                    }
                }
            }
            Itog.Content = sum.ToString();

         
        }


        private void Update()
        {
            List<Zapis> newlist = Myconv.MyDeserialize<List<Zapis>>("Заметки.json");


            foreach (var item in newlist)
            {
                if(item.data == Convert.ToDateTime(DatePicker.Text))
                {
                    MessageBox.Show(MyData.SelectedIndex.ToString());
                    newlist[MyData.SelectedIndex].Name = NewName.Text;
                    if (Convert.ToInt32(Summa.Text) < 0)
                    {
                        newlist[MyData.SelectedIndex].Sum = Convert.ToInt32(Summa.Text) * -1;
                        newlist[MyData.SelectedIndex].isPOs = false;
                    }
                    else
                    {
                        newlist[MyData.SelectedIndex].Sum = Convert.ToInt32(Summa.Text);
                        newlist[MyData.SelectedIndex].isPOs = true;
                    }


                    newlist[MyData.SelectedIndex].TypeOf = ComboBox.SelectedItem.ToString();
                    Myconv.Myserialize(newlist, "Заметки.json");

                    break;
                   
                }
               
                
            }
            NewName.Text = "";
            Summa.Text = "";
            ComboBox.SelectedItem = null;
            Read();



        }

        private void Delete()
        {
            
            List<Zapis> fordel = Myconv.MyDeserialize<List<Zapis>>("Заметки.json");

            foreach (var item in fordel)
            {
                if(item.data == Convert.ToDateTime(DatePicker.Text))
                {
                    fordel.RemoveAt(MyData.SelectedIndex);
                    break;
                }
            }

           

            Myconv.Myserialize(fordel, "Заметки.json");

           
            NewName.Text = "";
            Summa.Text = "";
            ComboBox.SelectedItem = null;
            Read();
        }

        private void CrreateZP_Click(object sender, RoutedEventArgs e)
        {
            Create();
        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            NewName.Text = "";
            Summa.Text = "";
            ComboBox.SelectedItem = null;

            Read();
        }

        private void DeleteBt_Click(object sender, RoutedEventArgs e)
        {
            Delete();
        }

        private void MyData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
            
            Zapis selected = MyData.SelectedItem as Zapis;
            if(selected == null)
            {
                //тут ничего не происходит но мне нужна обработка того, что если чувак выбрал что-то в DataGrid а потом поменял дату, то у меня вылезает ошибка (((((((
            }
            else
            {
                NewName.Text = selected.Name;
                if (selected.isPOs == false)
                {
                    Summa.Text = (selected.Sum * -1).ToString();
                }
                else
                {
                    Summa.Text = selected.Sum.ToString();
                }
                ComboBox.SelectedItem = selected.TypeOf;
            }
          
            
        }

        private void UpdateBT_Click(object sender, RoutedEventArgs e)
        {
            Update();
        }
    }
}
