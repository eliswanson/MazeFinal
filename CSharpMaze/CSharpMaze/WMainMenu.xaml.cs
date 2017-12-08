using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media.Imaging;

namespace CSharpMaze
{
    /// <summary>
    /// Interaction logic for WMainMenu.xaml
    /// </summary>
    public partial class WMainMenu : Window
    {
	    private string difficultyLevel;
		private MainWindow myMain = new MainWindow();
	    private int loadState = 0;

	    /// <summary>
	    /// Determines what state user should be seeing on loading or saving
	    /// </summary>
	    public int LoadState
	    {
		    get => loadState;
		    set => loadState = value;
	    }

		public WMainMenu()
        {            
            InitializeComponent();
            GridHelp.Visibility = System.Windows.Visibility.Hidden;
            GridLoadGame.Visibility = System.Windows.Visibility.Hidden;
            GridSettings.Visibility = System.Windows.Visibility.Hidden;
            GridLost.Visibility = System.Windows.Visibility.Hidden;
            GridWin.Visibility = System.Windows.Visibility.Hidden;
            GridNewGame.Visibility = System.Windows.Visibility.Hidden;
	        PlayBackgroundMusic();
			difficultyLevel = QuestionDriver.MediumString;
		}

		#region win/lose game
		//This method use for losting game or winning game
		public WMainMenu(bool flag)
        {
            InitializeComponent();
            GridHelp.Visibility = System.Windows.Visibility.Hidden;
            GridLoadGame.Visibility = System.Windows.Visibility.Hidden;
            GridSettings.Visibility = System.Windows.Visibility.Hidden;
            GridLost.Visibility = System.Windows.Visibility.Hidden;
            GridMainMenu.Visibility = System.Windows.Visibility.Hidden;
            GridNewGame.Visibility = System.Windows.Visibility.Hidden;
            PlayBackgroundMusic();
            difficultyLevel = QuestionDriver.MediumString;
            if (flag == true)//You Win
            {                
                GridWin.Visibility = System.Windows.Visibility.Visible;
                GridLost.Visibility = System.Windows.Visibility.Hidden;
            }
            else//You Lost
            {
                GridWin.Visibility = System.Windows.Visibility.Hidden;
                GridLost.Visibility = System.Windows.Visibility.Visible;
            }           
        }

        public WMainMenu(int iNewGame)
        {
            InitializeComponent();
            GridHelp.Visibility = System.Windows.Visibility.Hidden;
            GridLoadGame.Visibility = System.Windows.Visibility.Hidden;
            GridSettings.Visibility = System.Windows.Visibility.Hidden;
            GridLost.Visibility = System.Windows.Visibility.Hidden;
            GridMainMenu.Visibility = System.Windows.Visibility.Hidden;
            GridWin.Visibility = System.Windows.Visibility.Hidden;
            GridLost.Visibility = System.Windows.Visibility.Hidden;
          //  PlayBackgroundMusic();
            difficultyLevel = QuestionDriver.MediumString;
            GridNewGame.Visibility = System.Windows.Visibility.Visible;
        }
		#endregion

	    #region Game Save Mechanics




	    /// <summary>
	    /// Enables all radio buttons for user to select the same and changes the content of button to save
	    /// </summary>
	    public void SetUpSaving()
	    {
		    RdGameOne.IsEnabled = true;
		    RdGameTwo.IsEnabled = true;
		    RdGameThree.IsEnabled = true;
		    SaveLoad.Content = "Save";
	    }

	    /// <summary>
	    /// Runs saving the game mechanic. Grabs what string user has selected and saves to the file based off of user selection
	    /// </summary>
	    private void SaveGameMainMenu()
	    {
		    if (RdGameOne.IsChecked == true)
			    myMain.userSave = RdGameOne.Content.ToString();
		    else if (RdGameTwo.IsChecked == true)
			    myMain.userSave = RdGameTwo.Content.ToString();
		    else if (RdGameThree.IsChecked == true)
			    myMain.userSave = RdGameThree.Content.ToString();

		    myMain.SaveGame();
		    InGameMenuHide();

	    }

		#endregion

		#region Loading Functions

		/// <summary>
		/// Grabs all content from radio buttons
		/// </summary>
		/// <returns>a list of names from the content of the radio buttons</returns>
		public List<string> GameSaveNames()
		{
			List<string> GameSaveNames = new List<string>();

			for (int i = 0; i < GridLoadGame.Children.Count; i++)
			{
				if (GridLoadGame.Children[i] is RadioButton)
				{
					var name = (RadioButton)GridLoadGame.Children[i];
					GameSaveNames.Add(name.Content.ToString());
				}
			}

			return GameSaveNames;

		}

		/// <summary>
		/// checks to see if game saves exists
		/// </summary>
		/// <param name="GameSaveNames">liste of gamesaves names</param>
		/// <returns></returns>
		/// 
		public List<bool> CheckGameLoad(List<string> gameSaveNames)
		{
			var existing = new List<bool>();
			foreach (var item in gameSaveNames)
				existing.Add(File.Exists(item + ".txt"));
			return existing;
		}

		/// <summary>
		/// Enables Radio buttons if file is existing
		/// </summary>
		public void SetupLoading()
		{
			List<bool> namesExist = CheckGameLoad(GameSaveNames());

			RdGameOne.IsEnabled = namesExist[0];
			RdGameTwo.IsEnabled = namesExist[1]; ;
			RdGameThree.IsEnabled = namesExist[2];

			SaveLoad.Content = "Load";
		}

		/// <summary>
		/// Controls all mechanics to load the game 
		/// </summary>
		private void LoadGameMainMenu()
		{
			if (RdGameOne.IsChecked == true)
				myMain.userSave = RdGameOne.Content.ToString();
			else if (RdGameTwo.IsChecked == true)
				myMain.userSave = RdGameTwo.Content.ToString();
			else if (RdGameThree.IsChecked == true)
				myMain.userSave = RdGameThree.Content.ToString();

			if (myMain.MenuReference == null)
			{
				myMain.LoadGame();
				Hide();
				myMain.MenuReference = this;
				BackToMainMenu();
                TurnRadioOff();
				myMain.ShowDialog();
				myMain.MenuReference = null;
				Close();
			}
			else
			{
				myMain.LoadGame();
                TurnRadioOff();
				Hide();
				BackToMainMenu();
			}

		}
		#endregion

		private void ImgStartGame_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
			myMain.UserDifficulty = difficultyLevel;
	        myMain.MenuReference = this;
	        Hide();
	        TurnRadioOff();
			myMain.NewGame();
            myMain.ShowDialog();
	        myMain.MenuReference = null;
			Close();
        }

	    #region Disable difficulty
	    /// <summary>
	    /// Disables all radio buttons for difficulty. Can only be selected once when user is in main menu
	    /// </summary>
	    private void TurnRadioOff()
	    {
		    rdHard.IsEnabled = false;
		    rdEasy.IsEnabled = false;
		    rdMedium.IsEnabled = false;
	    }
	    #endregion

		private void ImgLoadGame_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            GridLoadGame.Visibility = System.Windows.Visibility.Visible;
            GridMainMenu.Visibility = System.Windows.Visibility.Hidden;
            GridSettings.Visibility = System.Windows.Visibility.Hidden;
            GridHelp.Visibility = System.Windows.Visibility.Hidden;
	        SetupLoading();

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

	    #region About and Instructions for game

	    /// <summary>
	    /// Call to switch display about the game text to user
	    /// </summary>
	    public void AboutGame()
	    {
		    string strInfo = "";
		    strInfo += "    The C# Trivia Maze Game\n\n";
		    strInfo += "Test your C# knowledge while making your way through a dungeonesque maze filled with over 150 questions!" +
		               "\nSelect the difficulty of questions and make your way to the token of graditude.\n" +
		               "Think you have what it takes?\n\n";
		    strInfo += "Version 1.0.0\n";
		    strInfo += ".Net Framework 6.1\n";
		    strInfo += "32/64 bit\n\n";
		    strInfo += "Developers:\n" +
		               "	Andrew Combs\n" +
		               "	Eli Swanson\n" +
		               "	Tuan Dang";
		    txbHowToPlay.Text = strInfo;
	    }
	    /// <summary>
	    /// Call to switch display Instructions the game text to user
	    /// </summary>
	    public void HowToPlayGame()
	    {
		    string strInfo = "How to Play:" +
		                     "\nUse the arrow keys to move your character. " +
		                     "\nWalk to a door and answer the questions appropriately based on the question given." +
		                     "\n\nQuestions given are either Short Answer, True or False, or Multiple Choice." +
		                     "\nIf your answer is correct the door will be opened and you'll proceed to the next room." +
                             "\nOtherwise, the door will be locked and you'll have to find another path through the maze." +		                     
		                     "\nContinue on until to reach the golden token and make it out of the maze!";
		    txbHowToPlay.Text = strInfo;
	    }

	    #endregion

		//This method used to back to main menu
		private void BtnBackHelp_Click(object sender, RoutedEventArgs e)
        {
            if (myMain.MenuReference == null)
            {
                this.BackToMainMenu();
                ResetSaveBtns();
            }
            else
            {
                Hide();
                BackToMainMenu();
                ResetSaveBtns();
            }
        }

        private void BackToMainMenu()
        {
            GridLoadGame.Visibility = System.Windows.Visibility.Hidden;
            GridMainMenu.Visibility = System.Windows.Visibility.Visible;
            GridSettings.Visibility = System.Windows.Visibility.Hidden;
            GridHelp.Visibility = System.Windows.Visibility.Hidden;
            GridLost.Visibility = System.Windows.Visibility.Hidden;
            GridWin.Visibility = System.Windows.Visibility.Hidden;
            GridNewGame.Visibility = System.Windows.Visibility.Hidden;
        }

        private void BtnDefault_Click(object sender, RoutedEventArgs e)
        {
			if (myMain.MenuReference == null)
			{
				BackGroundMusic.Volume = 0.5;
				SldVolume.Value = 0.5;
				rdMedium.IsChecked = true;
				difficultyLevel = rdMedium.Content.ToString();
				this.BackToMainMenu();
			    

			}

			else
			{
				BackGroundMusic.Volume = 0.5;
				SldVolume.Value = 0.5;
				InGameMenuHide(); 
			   

			}
		}

        private void ResetSaveBtns()
        {
            RdGameOne.IsChecked = false;
            RdGameTwo.IsChecked = false;
            RdGameThree.IsChecked = false;
        }

        private void BtnSaveSettings_Click(object sender, RoutedEventArgs e)
        {
			if (myMain.MenuReference == null)
			{
				if (rdEasy.IsChecked == true)
					difficultyLevel = QuestionDriver.EasyString;
				else if (rdHard.IsChecked == true)
					difficultyLevel = QuestionDriver.HardString;
				this.BackToMainMenu();
			}
			else
			{
				InGameMenuHide();
			}
		}

	    #region Background Music
	    /// <summary>
	    /// Plays the background music for the game
	    /// </summary>
	    private void PlayBackgroundMusic()
	    {
		    try
		    {
			    BackGroundMusic.Source = new Uri("Is Anybody Home_.wav", UriKind.Relative);
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
	    private void SldVolume_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
	    {
		    BackGroundMusic.Volume = (double)SldVolume.Value;
	    }
		#endregion

		/// <summary>
		/// Closing event for application. Checks if the other window is open and closes it if it is closed
		/// </summary>
		/// <param name="sender">Object that is triggering the event</param>
		/// <param name="e">Event to stop the closing of the application</param>
		private void WMainMenu_OnClosing(object sender, CancelEventArgs e)
	    {
		    if (myMain.MenuReference != null)
		    {
			    e.Cancel = true;
			    InGameMenuHide();
			}
		    else
			    myMain.Close();
	    }

	    private void SaveLoad_OnClick(object sender, RoutedEventArgs e)
	    {
			if (LoadState == 0)
				LoadGameMainMenu();
			else
				SaveGameMainMenu();
		}

	    /// <summary>
	    /// Hides the main menu form and returns all content back to mainmenu
	    /// </summary>
	    private void InGameMenuHide()
	    {
		    Hide();
		    BackToMainMenu();
	    }

		#region Game Save Load enable button 
		private void RdGameOne_OnChecked(object sender, RoutedEventArgs e)
	    {
		    SaveLoad.IsEnabled = true;
	    }

	    private void RdGameTwo_OnChecked(object sender, RoutedEventArgs e)
	    {
		    SaveLoad.IsEnabled = true;
	    }

	    private void RdGameThree_OnChecked(object sender, RoutedEventArgs e)
	    {
		    SaveLoad.IsEnabled = true;
	    }
		#endregion

	    #region RollOverEffects

	    


	    
		private void BtnMainMenu_Click(object sender, RoutedEventArgs e)
        {
            this.BackToMainMenu();
        }

        private void ImgStartGame_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            ImgStartGame.Source = new BitmapImage(new Uri("playclickablebutton.png", UriKind.Relative));

        }
        private void ImgStartGame_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            ImgStartGame.Source = new BitmapImage(new Uri("playbutton.png", UriKind.Relative));

        }

        private void ImgLoadGame_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            ImgLoadGame.Source = new BitmapImage(new Uri("loadclickablebutton.png", UriKind.Relative));

        }

        private void ImgLoadGame_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            ImgLoadGame.Source = new BitmapImage(new Uri("loadbutton.png", UriKind.Relative));

        }

        private void ImgSettingsGame_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            ImgSettingsGame.Source = new BitmapImage(new Uri("settingsclickablebutton.png", UriKind.Relative));

        }

        private void ImgSettingsGame_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            ImgSettingsGame.Source = new BitmapImage(new Uri("settingsbutton.png", UriKind.Relative));

        }

        private void ImgAboutGame_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            ImgAboutGame.Source = new BitmapImage(new Uri("aboutclickablebutton.png", UriKind.Relative));

        }

        private void ImgAboutGame_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            ImgAboutGame.Source = new BitmapImage(new Uri("aboutbutton.png", UriKind.Relative));

        }

        private void ImgExitGame_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            ImgExitGame.Source = new BitmapImage(new Uri("exitclickablebutton.png", UriKind.Relative));

        }

        private void ImgExitGame_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            ImgExitGame.Source = new BitmapImage(new Uri("exitbutton.png", UriKind.Relative));
        }

        #endregion

        private void BtnBackNewGame_Click(object sender, RoutedEventArgs e)
        {
            if (myMain.MenuReference == null)
                this.BackToMainMenu();
            else
            {
                Hide();
                BackToMainMenu();
            }
        }

        private void BtnNewGame_OnClick(object sender, RoutedEventArgs e)
        {
            myMain.NewGame();
            Hide();
            BackToMainMenu();
            myMain.UserDifficulty = difficultyLevel;
            if (rbEasy.IsChecked == true)
                myMain.UserDifficulty = QuestionDriver.EasyString;
            else if (rbMedium.IsChecked == true)
                myMain.UserDifficulty = QuestionDriver.MediumString;
            else if (rbHard.IsChecked == true)
                myMain.UserDifficulty = QuestionDriver.HardString;                                        
        }
    }
}
