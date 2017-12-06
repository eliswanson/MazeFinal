using System.Windows;

namespace CSharpMaze
{
    /// <summary>
    /// Interaction logic for WMainMenu.xaml
    /// </summary>
    public partial class WMainMenu : Window
    {
        
        public WMainMenu()
        {            
            InitializeComponent();
            GridHelp.Visibility = System.Windows.Visibility.Hidden;
            GridLoadGame.Visibility = System.Windows.Visibility.Hidden;
            GridSettings.Visibility = System.Windows.Visibility.Hidden;
        }

        private void ImgStartGame_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            MainWindow myMain = new MainWindow();
            this.Hide();
			myMain.NewGame();
            //myMain.Owner = this;
            myMain.ShowDialog();
            this.Close();
        }

        private void ImgLoadGame_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            GridLoadGame.Visibility = System.Windows.Visibility.Visible;
            GridMainMenu.Visibility = System.Windows.Visibility.Hidden;
            GridSettings.Visibility = System.Windows.Visibility.Hidden;
            GridHelp.Visibility = System.Windows.Visibility.Hidden;
            
        }

        private void ImgExitGame_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
           MessageBoxResult res =  MessageBox.Show("Do you want to exit this game!!!", "Exit", MessageBoxButton.YesNo);
            if(res == MessageBoxResult.Yes)
                this.Close();
        }

        private void ImgSettingsGame_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            GridLoadGame.Visibility = System.Windows.Visibility.Hidden;
            GridMainMenu.Visibility = System.Windows.Visibility.Hidden;
            GridSettings.Visibility = System.Windows.Visibility.Visible;
            GridHelp.Visibility = System.Windows.Visibility.Hidden;
            
        }

        private void ImgAboutGame_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            GridLoadGame.Visibility = System.Windows.Visibility.Hidden;
            GridMainMenu.Visibility = System.Windows.Visibility.Hidden;
            GridSettings.Visibility = System.Windows.Visibility.Hidden;
            GridHelp.Visibility = System.Windows.Visibility.Visible;
            AboutGame();
        }

        private void AboutGame()
        {
            string strInfo = "";
            strInfo += "The Maze Game\n";
            strInfo += "Version 1.0.0\n";
            strInfo += ".Net Framework 6.1\n";
            strInfo += " 32/64 bit\n";
            strInfo += "Programmed/Developer:\n" +
                "   Andrew Combs\n" +
                "   Eli Swanson\n" +
                "   Tuan Dang";
            this.txbAbout.Text = strInfo;
        }

        //This method used to back to main menu
        private void BtnBackHelp_Click(object sender, RoutedEventArgs e)
        {
            this.BackToMainMenu();
        }

        private void BackToMainMenu()
        {
            GridLoadGame.Visibility = System.Windows.Visibility.Hidden;
            GridMainMenu.Visibility = System.Windows.Visibility.Visible;
            GridSettings.Visibility = System.Windows.Visibility.Hidden;
            GridHelp.Visibility = System.Windows.Visibility.Hidden;
        }
        private void BtnDefault_Click(object sender, RoutedEventArgs e)
        {
            this.BackToMainMenu();
        }

        private void BtnSaveSettings_Click(object sender, RoutedEventArgs e)
        {
            this.BackToMainMenu();
        }
    }
}
