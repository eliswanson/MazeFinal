using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.IO;
using System.Threading;
using WpfAnimatedGif;

namespace CSharpMaze
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
	{        
		private Rect[] doorsHitBoxes;
		private MazeDriver engine;
        private QuestionDriver myQuestionDriver;
		private Executions executes = new Executions();
		private Originator origin = new Originator();
		private Caretaker care = new Caretaker();
		private int SPEED = 3; // Speed at which player moves
		private int frames; // uses keys 
		private Key prevKey;
		//private Thread BackgroundMusic = new Thread(new ThreadStart())

		public MainWindow()
		{
			RoomState testRoom = new RoomState() { Door1State = 3, Door2State = 3, Door3State = 2, Door4State=2 };
            
			InitializeComponent();
            engine = new MazeDriver(this.MiniMap, this.PlayerRoom);

			/****Remove Later ****/
			gbTFQues.Visibility = System.Windows.Visibility.Hidden;
			gbMCQues.Visibility = System.Windows.Visibility.Hidden;
			gbSAQues.Visibility = System.Windows.Visibility.Hidden;
			 /****End Remove Later ****/

			myQuestionDriver = new QuestionDriver(gbMCQues, gbTFQues, gbSAQues);
			GenerateHitBoxes();

			lblCursorPosition.Text = "Choose the door you want by using keyboard up, left, down, right";
		}

        #region Execute Order 66
        void Save_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = executes.ExecuteSave;
		}

		void Save_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			care.AddMemento(origin.CreateMemento(engine, myQuestionDriver));
			care.SaveGame();
		}

		void Load_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = true;
		}

		void Load_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			care.LoadGame();
			origin.RestoreFromMemento(care.GetLatestMemento(), engine, myQuestionDriver);
			CenterPlayer();
		}

		void Exit_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = true;
		}

		void Exit_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			this.Close();
		}

		void About_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = true;
		}

		void About_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			MenuReference.Show();
			MenuReference.GridMainMenu.Visibility = Visibility.Hidden;
			MenuReference.GridHelp.Visibility = Visibility.Visible;
			MenuReference.AboutGame();
		}

		void How_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = true;
		}

		void How_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			MenuReference.Show();
			MenuReference.GridHelp.Visibility = Visibility.Visible;
			MenuReference.GridMainMenu.Visibility = Visibility.Hidden;
			MenuReference.HowToPlayGame();
		}

		void Game_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = true;
		}

		void Game_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			MessageBox.Show("New Game!");
		}

		void Settings_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = true;
		}

		void Settings_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			MenuReference.GridMainMenu.Visibility = Visibility.Hidden;
			MenuReference.GridSettings.Visibility = Visibility.Visible;
			MenuReference.Show();
		}
#endregion

        #region Generate HitBoxes
        //Generate door Hitboxes
        private void GenerateHitBoxes()
		{
			doorsHitBoxes = new Rect[4];
			doorsHitBoxes[0] = new Rect(new System.Windows.Point((double)PlayerRoom.Children[1].GetValue(Canvas.LeftProperty), (double)PlayerRoom.Children[1].GetValue(Canvas.TopProperty)), new System.Windows.Size((double)PlayerRoom.Children[1].GetValue(Canvas.WidthProperty), (double)PlayerRoom.Children[1].GetValue(Canvas.HeightProperty) - 10));
			doorsHitBoxes[1] = new Rect(new System.Windows.Point(0, (double)PlayerRoom.Children[4].GetValue(Canvas.TopProperty)), new System.Windows.Size((double)PlayerRoom.Children[4].GetValue(Canvas.HeightProperty) - 10, (double)PlayerRoom.Children[4].GetValue(Canvas.WidthProperty)));
			doorsHitBoxes[2] = new Rect(new System.Windows.Point((double)PlayerRoom.Children[7].GetValue(Canvas.LeftProperty), (double)PlayerRoom.Children[7].GetValue(Canvas.TopProperty) + 10), new System.Windows.Size((double)PlayerRoom.Children[7].GetValue(Canvas.WidthProperty), (double)PlayerRoom.Children[7].GetValue(Canvas.HeightProperty) - 10));
			doorsHitBoxes[3] = new Rect(new System.Windows.Point((double)PlayerRoom.Children[10].GetValue(Canvas.LeftProperty) - ((double)PlayerRoom.Children[10].GetValue(Canvas.HeightProperty) - 10), (double)PlayerRoom.Children[10].GetValue(Canvas.TopProperty)), new System.Windows.Size((double)PlayerRoom.Children[10].GetValue(Canvas.HeightProperty), (double)PlayerRoom.Children[10].GetValue(Canvas.WidthProperty)));
		}
		//Generate Player Hit Box
		private Rect PlayerHitBox()
		{
			return new Rect(new System.Windows.Point((double)Player.GetValue(Canvas.LeftProperty) + 5, (double)Player.GetValue(Canvas.TopProperty) + 5), new System.Windows.Size((double)Player.Width, (double)Player.Height - 5));
		}
		#endregion

		#region Player Movement
		private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			switch (e.Key)
			{
				case Key.Right:
					
					TriggerPlayerGif(1);
					if (prevKey != Key.Right)
					{
						UpdatePlayersDirection(0);
					}

					if (HitSideOfBoard("Right") && !PlayerHitTarget())
					{
						Canvas.SetLeft(this.Player, Canvas.GetLeft(this.Player) + SPEED);
					}

					else if (PlayerHitTarget())
					{
						string door = "Door4";

						if (engine.CurrentRoom.Door4State == 0) //Door is currently closed, display question and disable movement. 
						{
							engine.CurrentDoor = door;
							myQuestionDriver.Display();
							this.PreviewKeyDown -= Window_PreviewKeyDown;
						}

						else //Door is open. Move player to next room.
						{
							engine.OpenDoor(door);
							CenterPlayer();
						}					
					}
					break;
				case Key.Left:
					
					TriggerPlayerGif(1);
					if (prevKey != Key.Left)
					{
						UpdatePlayersDirection(180);
					}

					if (HitSideOfBoard("Left") && !PlayerHitTarget())
					{
						Canvas.SetLeft(this.Player, Canvas.GetLeft(this.Player) - SPEED);
					}

					else if (PlayerHitTarget())
					{
						string door = "Door2";

						if (engine.CurrentRoom.Door2State == 0) //Door is currently closed, display question and disable movement. 
						{
							engine.CurrentDoor = door;
							myQuestionDriver.Display();
							this.PreviewKeyDown -= Window_PreviewKeyDown;
						}

						else //Door is open. Move player to next room.
						{
							engine.OpenDoor(door);
							CenterPlayer();
						}
					}
					break;
				case Key.Up:
					
					TriggerPlayerGif(1);
					if (prevKey != Key.Up)
					{
						UpdatePlayersDirection(-90);
					}

					if (HitSideOfBoard("Up") && !PlayerHitTarget())
					{
						Canvas.SetTop(this.Player, Canvas.GetTop(this.Player) - SPEED);
					}
					else if (PlayerHitTarget() )
					{
						string door = "Door1";

						if (engine.CurrentRoom.Door1State == 0) //Door is currently closed, display question and disable movement. 
						{
							engine.CurrentDoor = door;
							myQuestionDriver.Display();
							this.PreviewKeyDown -= Window_PreviewKeyDown;
						}

						else //Door is open. Move player to next room.
						{
							engine.OpenDoor(door);
							CenterPlayer();
						}
					}
					break;
				case Key.Down:

					TriggerPlayerGif(1);
					if (prevKey != Key.Down)
					{
						UpdatePlayersDirection(90);
					}

					if (HitSideOfBoard("Bottom") && !PlayerHitTarget())
					{
						Canvas.SetTop(this.Player, Canvas.GetTop(this.Player) + SPEED);
					}

					else if (PlayerHitTarget())
					{
						string door = "Door3";
                                        
						if(engine.CurrentRoom.Door3State == 0) //Door is currently closed, display question and disable movement. 
						{
							engine.CurrentDoor = door;
							myQuestionDriver.Display();
							this.PreviewKeyDown -= Window_PreviewKeyDown;
						}

						else //Door is open. Move player to next room.
						{                        
							engine.OpenDoor(door);
							CenterPlayer();
						}
					}
					break;
			}
			//Used to gather the previous key pressed
			prevKey = e.Key;
		}
		
		//Center player to the middle of the room if user answers question correctly or goes through open door
		private void CenterPlayer()
		{
			Canvas.SetLeft(this.Player, (this.PlayerRoom.Width - this.Player.Width) / 2);
			Canvas.SetTop(this.Player, (this.PlayerRoom.Height - this.Player.Height) / 2);
		}

#endregion

		#region Hit Detection for Player, Doors, and Canvas walls
		private bool HitSideOfBoard(String side)
		{
			switch (side)
			{
				case "Up":
					return Canvas.GetTop(this.Player) - 4 > 0;

				case "Left":
					return Canvas.GetLeft(this.Player) - 5 > 0;

				case "Bottom":
					return Canvas.GetTop(this.Player) + 2 < this.PlayerRoom.Height - this.Player.Height;

				case "Right":
					return Canvas.GetLeft(this.Player) + 2 < this.PlayerRoom.Width - this.Player.Width;
			}

			return false;
		}

		//Checks to see if player has hit any objects on board to trigger event.
		private bool PlayerHitTarget()
		{ 
			Rect playerdetect = new Rect(new System.Windows.Point((double)Player.GetValue(Canvas.LeftProperty), (double)Player.GetValue(Canvas.TopProperty)), new System.Windows.Size((double)Player.Width, (double)Player.Height - 5));
			for (int i = 0; i < doorsHitBoxes.Length; i++)
				if (this.PlayerHitBox().IntersectsWith(doorsHitBoxes[i]))
					if (!DoorLockedOrShown(i))
					{// If user has hit the door checks that current door to see if it is locked or shown. else false.
						executes.ExecuteSave = false;
						return true;
					}
			return false;
		}

		// Looks to see if the door player has hit is locked of not shown.
		private bool DoorLockedOrShown(int door)
		{
				switch (door)
				{
					case 0:
						return engine.CurrentRoom.Door1State == 2 || engine.CurrentRoom.Door1State == 3;		
				
					case 1:
						return engine.CurrentRoom.Door2State == 2 || engine.CurrentRoom.Door2State == 3;
				
					case 2:
						return engine.CurrentRoom.Door3State == 2 || engine.CurrentRoom.Door3State == 3;
						
					case 3:
						return engine.CurrentRoom.Door4State == 2 || engine.CurrentRoom.Door4State == 3;	
				}
			
			return false;
		}
		#endregion

		#region player frame movement
		//Turns off player movement
		
		private void TriggerPlayerGif(int amount)
		{
			frames += amount;
			if (frames >= ImageBehavior.GetAnimationController(this.Player).FrameCount || frames < 0)
				frames = 0;

			ImageBehavior.GetAnimationController(this.Player).GotoFrame(frames);
		}

		// Updates player direction based on the key pressed.
		private void UpdatePlayersDirection(double direction)
		{
			Player.RenderTransform = new RotateTransform(direction);
		}
		#endregion

		#region gather user information
		private void BtnTF_OK_Click(object sender, RoutedEventArgs e)
        {
            //Code for Testing
            //engine.Answered(true);
			//CenterPlayer();
			//ResetGrids();
            bool correct;
            
			if (rdTFAns1.IsChecked == true)
			{
                correct = myQuestionDriver.IsCorrect(rdTFAns1.Content.ToString());
                engine.Answered(correct);

				if (correct)
					CenterPlayer();

				ResetGrids();
			}

			else if (rdTFAns2.IsChecked == true)
			{
                correct = myQuestionDriver.IsCorrect(rdTFAns2.Content.ToString());
                engine.Answered(correct);

				if (correct)
					CenterPlayer();

				ResetGrids();
			}

			else
			{ MessageBox.Show("Please input a valid input "); }

		}

		private void BtnMC_OK_Click(object sender, RoutedEventArgs e)
		{
            //Code for Testing
            //engine.Answered(true);
			//CenterPlayer();
			//ResetGrids();
            
            bool correct;
            if (rdMCAns1.IsChecked == true)
			{
                correct = myQuestionDriver.IsCorrect(rdMCAns1.Content.ToString());
                engine.Answered(correct);

				if (correct)
					CenterPlayer();

				ResetGrids();

			}

			else if (rdMCAns2.IsChecked == true)
			{
                correct = myQuestionDriver.IsCorrect(rdMCAns2.Content.ToString());
                engine.Answered(correct);

				if (correct)
					CenterPlayer();

				ResetGrids();

			}

			else if (rdMCAns3.IsChecked == true)
			{
                correct = myQuestionDriver.IsCorrect(rdMCAns3.Content.ToString());
                engine.Answered(correct);

				if (correct)
					CenterPlayer();

				ResetGrids();
			}

			else if (rdMCAns4.IsChecked == true)
			{
                correct = myQuestionDriver.IsCorrect(rdMCAns4.Content.ToString());
                engine.Answered(correct);

				if (correct)
					CenterPlayer();

				ResetGrids();
			}

			else
			{ MessageBox.Show("Please input a valid input "); }

	
		}

        private void BtnSA_OK_Click(object sender, RoutedEventArgs e)
        {
            //Code for Testing
           // engine.Answered(true);
			//CenterPlayer();
			//ResetGrids();
            
            bool correct;
			if (txtSA.Text.Length != 0)
			{
                correct = myQuestionDriver.IsCorrect(txtSA.Text);
                engine.Answered(correct);

				if (correct)
					CenterPlayer();

				ResetGrids();
			}

			else
			{ MessageBox.Show("Please input a valid input "); }

		}
		// Used to hide grids on the main screen and allow player to move freely
		private void ResetGrids()
		{
			// Enables keys and sets all visibility of group boxes to hidden
			this.PreviewKeyDown += Window_PreviewKeyDown;
			gbTFQues.Visibility = System.Windows.Visibility.Hidden;
			gbMCQues.Visibility = System.Windows.Visibility.Hidden;
			gbSAQues.Visibility = System.Windows.Visibility.Hidden;
			//Turns saving back on
			executes.ExecuteSave = true;

			//Resets text in text-box
			txtSA.Text = "";
			//Resets all radio buttons for multiple choice back to nothing
			rdMCAns4.IsChecked = false;
			rdMCAns3.IsChecked = false;
			rdMCAns2.IsChecked = false;
			rdMCAns1.IsChecked = false;
			//Resets all radio buttons for true and false back to nothing slected
			rdTFAns2.IsChecked = false;
			rdTFAns1.IsChecked = false;

		}

		/// <summary>
		/// Gets and Sets the user difficulty from the main menu
		/// </summary>
		/// <returns>String has easy,medium, or hard</returns>
		public string UserDifficulty { get; set; }

		public WMainMenu MenuReference { get; set; }
		#endregion

		#region Sounds
		/// <summary>
		/// Allows for footstep sounds as user moves player
		/// </summary>
		/// <param name="run"></param>
		private void PlayFootSteps(int run)
		{
			try
			{
				var sndOpen = new System.Media.SoundPlayer("Footsteps.wav");

				if (run == 1)	
					sndOpen.Play();
				
				else
					sndOpen.Stop();
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				Close();
			}
		}
		#endregion

		#region Status bar
		private void btnTF_OK_MouseEnter(object sender, MouseEventArgs e)
		{
			lblCursorPosition.Text = "Press OK to submit your answer";
		}

		private void btnTF_OK_MouseLeave(object sender, MouseEventArgs e)
		{
			lblCursorPosition.Text = "";
		}

		private void btnSA_OK_MouseEnter(object sender, MouseEventArgs e)
		{
			lblCursorPosition.Text = "Press OK to submit your answer";
		}

		private void btnSA_OK_MouseLeave(object sender, MouseEventArgs e)
		{
			lblCursorPosition.Text = "";
		}

		private void btnMC_OK_MouseEnter(object sender, MouseEventArgs e)
		{
			lblCursorPosition.Text = "Press OK to submit your answer";
		}

		private void btnMC_OK_MouseLeave(object sender, MouseEventArgs e)
		{
			lblCursorPosition.Text = "";
		}

		private void txtSA_MouseEnter(object sender, MouseEventArgs e)
		{
			lblCursorPosition.Text = "Please enter your answer (one to three words)";
		}

		private void txtSA_MouseLeave(object sender, MouseEventArgs e)
		{
			lblCursorPosition.Text = "";
		}

		//This method used to display instruction depending on the type of question: Multiple choice, T/F, or short answer
		private void CheckStatusOfTypeQues()
		{
			if (gbMCQues.Visibility == 0)
			{
				lblCursorPosition.Text = "Answer question by choosing one of them";
			}
			else if (gbTFQues.Visibility == 0)
			{
				lblCursorPosition.Text = "Answer question by choosing one of them";
			}
			else
			{
				lblCursorPosition.Text = "Answer question by typing into text box";
			}
		}

		private void mitmFile_Save_MouseEnter(object sender, MouseEventArgs e)
		{
			lblCursorPosition.Text = "Choose File / Save if you want to save your game.";
		}

		private void mitmFile_Save_MouseLeave(object sender, MouseEventArgs e)
		{
			lblCursorPosition.Text = "";
		}

		private void mitmFile_Load_MouseEnter(object sender, MouseEventArgs e)
		{
			lblCursorPosition.Text = "Choose File / Load if you want to load your game.";
		}

		private void mitmFile_Load_MouseLeave(object sender, MouseEventArgs e)
		{
			lblCursorPosition.Text = "";
		}

		private void mitmFile_Game_MouseEnter(object sender, MouseEventArgs e)
		{
			lblCursorPosition.Text = "Choose File / New if you want to start a new game.";
		}

		private void mitmFile_Game_MouseLeave(object sender, MouseEventArgs e)
		{
			lblCursorPosition.Text = "";
		}

		private void mitmFile_Exit_MouseEnter(object sender, MouseEventArgs e)
		{
			lblCursorPosition.Text = "Choose File / Exit if you want to exit your game.";
		}

		private void mitmFile_Exit_MouseLeave(object sender, MouseEventArgs e)
		{
			lblCursorPosition.Text = "";
		}

		private void mitmFile_About_MouseEnter(object sender, MouseEventArgs e)
		{
			lblCursorPosition.Text = "About the game";
		}

		private void mitmFile_About_MouseLeave(object sender, MouseEventArgs e)
		{
			lblCursorPosition.Text = "";
		}

		private void mitmFile_Instructions_MouseEnter(object sender, MouseEventArgs e)
		{
			lblCursorPosition.Text = "Instructions for the game";
		}

		private void mitmFile_Instructions_MouseLeave(object sender, MouseEventArgs e)
		{
			lblCursorPosition.Text = "";
		}


		private void MitmFile_Settings_OnMouseLeave(object sender, MouseEventArgs e)
		{
			lblCursorPosition.Text = "";
		}
		#endregion

		private void MitmFile_Settings_OnMouseEnter(object sender, MouseEventArgs e)
		{
			lblCursorPosition.Text = "Click to change Settings ";
		}
	}
}
