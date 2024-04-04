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
using System.Windows.Shapes;

namespace WpfApp6
{
    /// <summary>
    /// Логика взаимодействия для Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        private TableScore1 _currentScr = new TableScore1();
        public string name;
        public Window2()
        {
            InitializeComponent();

            Slovar.ItemsSource = SankeEntities.GetContext().TableScore1.ToList();
            DataContext = _currentScr;
            
        }


        private void ButtonClickD(object sender, RoutedEventArgs e)
        {
            _currentScr.Login = name;
            _currentScr.score = Score.Text;
          
            if (_currentScr.id == 0) SankeEntities.GetContext().TableScore1.Add(_currentScr);

            try
            {
                SankeEntities.GetContext().SaveChanges();
                MessageBox.Show("Данные успешно сохранены!");
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            System.Windows.Application.Current.Shutdown();
        }

    }
}
