using System.Windows.Input;
using System.Windows;

namespace WpfApp6
{
    public partial class Window1 : Window
    {
    
        public void baza(object sender ,KeyEventArgs e)
        {
      
            switch (e.Key)
            {
                case Key.Enter:
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.name1 = Name.Text;
                    mainWindow.Show();
                    
                    break;
            }
        }
        public Window1()
        {
            InitializeComponent();
           
            

        }
    }
}