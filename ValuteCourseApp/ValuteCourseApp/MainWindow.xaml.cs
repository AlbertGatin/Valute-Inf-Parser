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
using System.Xml.Linq;

namespace ValuteCourseApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        private void XMLtoDB_Click(object sender, RoutedEventArgs e) //отправляет данные с xml файла в бд
        {
            try
            {
                List<ValuteCourse> valutes = ParserFunc.GetFromXml();
                List<ValuteCourse> valtoGrid = ParserFunc.GetFromXml();
                try
                {
                    ParserFunc.InsertToDB(valutes);
                }
                catch
                {
                    MessageBox.Show("ошибка при добавлении записей в бд");
                }
            }
            catch
            {
                MessageBox.Show("ошибка считывания с web страницы");
            }       
            
        }

        private void DBtoGrid_Click(object sender, RoutedEventArgs e)//заполняте таблицу из бд
        {
            try
            {

                ValuteInfGrid.ItemsSource = ParserFunc.GetFromDB();

            }
            catch
            {
                MessageBox.Show("ошибка при чтении бд");
            }
        }

        private void XMLtoGrid_Click(object sender, RoutedEventArgs e) //заполняет таблицу из xml файла
        {
            try
            {
                ValuteInfGrid.ItemsSource = ParserFunc.GetFromXml();
            }
            catch
            {
                MessageBox.Show("ошибка считывания с web страницы");
            }
        }
    }
}
