using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.IO;
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
		Originator origin = new Originator();
		Caretaker care = new Caretaker();
		private int SPEED = 3; // Speed at which player moves
		int frames; // uses keys 
		private Key prevKey;
		public MainWindow()
		{
			RoomState testRoom = new RoomState() { Door1State = 3, Door2State = 3, Door3State = 2, Door4State=2 };
            
			InitializeComponent();            

			/****Remove Later ****/
			gbTFQues.Visibility = System.Windows.Visibility.Hidden;
			gbMCQues.Visibility = System.Windows.Visibility.Hidden;
			gbSAQues.Visibility = System.Windows.Visibility.Hidden;
			 /****End Remove Later ****/
			
			PlayBackgroundMusic();

		}
     

	    #region Save and Loading game	   	    
        public void NewGame()
	    {
	        engine = new MazeDriver(this.MiniMap, this.PlayerRoom);
	        myQuestionDriver = new QuestionDriver(gbMCQues, gbTFQues, gbSAQues);
		    GenerateHitBoxes();
			CenterPlayer();
        }

	    public void LoadGame()
	    {
	        care.LoadGame();
	        origin.RestoreFromMemento(care.GetLatestMemento(), out engine, out myQuestionDriver, this);
	        //GenerateHitBoxes();
            CenterPlayer();
        }
	    #endregion
        
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
            LoadGame();
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
			MessageBox.Show("Some text about the game");
		}

		void How_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = true;
		}

		void How_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			MessageBox.Show("Some text describing how to play the game");
		}

		void Game_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = true;
		}

		void Game_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			MessageBox.Show("New Game!");
            NewGame();
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
			if (e.Key == Key.Right)
			{
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

                    if (engine.CurrentRoomState.Door4State == 0) //Door is currently closed, display question and disable movement. 
                    {
                        engine.CurrentDoorString = door;
                        myQuestionDriver.Display();
                        this.PreviewKeyDown -= Window_PreviewKeyDown;
                    }

                    else //Door is open. Move player to next room.
                    {
                        engine.OpenDoor(door);
                        CenterPlayer();
                    }					
				}
            }

			else if (e.Key == Key.Left)
			{
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

                    if (engine.CurrentRoomState.Door2State == 0) //Door is currently closed, display question and disable movement. 
                    {
                        engine.CurrentDoorString = door;
                        myQuestionDriver.Display();
                        this.PreviewKeyDown -= Window_PreviewKeyDown;
                    }

                    else //Door is open. Move player to next room.
                    {
                        engine.OpenDoor(door);
                        CenterPlayer();
                    }
				}
            }

			else if (e.Key == Key.Up)
			{
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

                    if (engine.CurrentRoomState.Door1State == 0) //Door is currently closed, display question and disable movement. 
                    {
                        engine.CurrentDoorString = door;
                        myQuestionDriver.Display();
                        this.PreviewKeyDown -= Window_PreviewKeyDown;
                    }

                    else //Door is open. Move player to next room.
                    {
                        engine.OpenDoor(door);
                        CenterPlayer();
                    }
				}
            }

			else if (e.Key == Key.Down)
			{
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
                                        
                    if(engine.CurrentRoomState.Door3State == 0) //Door is currently closed, display question and disable movement. 
                    {
                        engine.CurrentDoorString = door;
                        myQuestionDriver.Display();
                        this.PreviewKeyDown -= Window_PreviewKeyDown;
                    }

					else //Door is open. Move player to next room.
                    {                        
                        engine.OpenDoor(door);
                        CenterPlayer();
                    }
				}
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
						return engine.CurrentRoomState.Door1State == 2 || engine.CurrentRoomState.Door1State == 3;		
				
					case 1:
						return engine.CurrentRoomState.Door2State == 2 || engine.CurrentRoomState.Door2State == 3;
				
					case 2:
						return engine.CurrentRoomState.Door3State == 2 || engine.CurrentRoomState.Door3State == 3;
						
					case 3:
						return engine.CurrentRoomState.Door4State == 2 || engine.CurrentRoomState.Door4State == 3;	
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
		#endregion
		private static void PlayBackgroundMusic()
		{
			try
			{
				System.Media.SoundPlayer sndOpen = new System.Media.SoundPlayer("Is Anybody Home_.wav");
				sndOpen.PlayLooping();
			}
			catch (FileNotFoundException e)
			{
				Console.WriteLine(e.Message);
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}

			Console.WriteLine("Sound plays");
		}
	}
}
