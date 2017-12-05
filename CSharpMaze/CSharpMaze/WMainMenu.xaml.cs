using System;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace CSharpMaze
{
    /// <summary>
    /// Interaction logic for WMainMenu.xaml
    /// </summary>
    public partial class WMainMenu : Window
    {
	    private string difficultyLevel;
	    MainWindow myMain = new MainWindow();
		public WMainMenu()
        {            
            InitializeComponent();
            GridHelp.Visibility = System.Windows.Visibility.Hidden;
            GridLoadGame.Visibility = System.Windows.Visibility.Hidden;
            GridSettings.Visibility = System.Windows.Visibility.Hidden;
			PlayBackgroundMusic(); // Runs background music
		    difficultyLevel = rdMedium.Content.ToString();
		}

        private void ImgStartGame_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
	        myMain.UserDifficulty = difficultyLevel;
	        myMain.MenuReference = this;
			this.Hide();
            //myMain.Owner = this;
            myMain.ShowDialog();
	        myMain.MenuReference = null;
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
	        BackGroundMusic.Volume = 0.5;
	        SldVolume.Value = 0.5;
	        rdMedium.IsChecked = true;
	        difficultyLevel = rdMedium.Content.ToString();
			this.BackToMainMenu();
        }

        private void BtnSaveSettings_Click(object sender, RoutedEventArgs e)
        {
	        if (rdEasy.IsChecked == true)
		        difficultyLevel = rdEasy.Content.ToString();
			else if (rdHard.IsChecked == true)
		        difficultyLevel = rdHard.Content.ToString();
			this.BackToMainMenu();
        }

		#region Background Music
		/// <summary>
		/// <para>PlayBackgroundMusic()</para>
		/// </summary>
		private void PlayBackgroundMusic()
	    {
		    try
		    {
				BackGroundMusic.Source = new Uri("Is Anybody Home_.mp3", UriKind.Relative);
			    BackGroundMusic.Play();
		    }
		    catch (FileNotFoundException e)
		    {
			    Console.WriteLine(e.Message);
			    Close();
		    }
		    catch (Exception e)
		    {
			    Console.WriteLine(e.Message);
			    Close();
		    }
	    }
		/// <summary>
		/// Once music has ended loops music again
		/// </summary>
		/// <param name="sender"> object that is sending the information</param>
		/// <param name="e">event information</param>
	    private void BackGroundMusic_OnMediaEnded(object sender, RoutedEventArgs e)
	    {
			BackGroundMusic.Position = TimeSpan.Zero;
		    BackGroundMusic.Play();
		}
	    #endregion

	    private void SldVolume_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
	    {
		    BackGroundMusic.Volume = (double)SldVolume.Value;
	    }


	    private void WMainMenu_OnClosing(object sender, CancelEventArgs e)
	    {
		    if (myMain.MenuReference != null)
		    {
			    e.Cancel = true;
			    this.Hide();
		    }
		    else
				myMain.Close();
		    


	    }
    }
}
