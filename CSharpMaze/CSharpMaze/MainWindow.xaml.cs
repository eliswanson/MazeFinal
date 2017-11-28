using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using WpfAnimatedGif;

namespace CSharpMaze
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private Rect[] doorsHitBoxes;
		private MazeDriver engine;
        private QuestionDriver myQuestionDriver;
        public MainWindow()
		{
			RoomState testRoom = new RoomState() { Door1State = 3, Door2State = 3, Door3State = 2, Door4State=2 };

			InitializeComponent();
            engine = new MazeDriver(this.MiniMap, this.PlayerRoom);

			this.ResetGrids();

			myQuestionDriver = new QuestionDriver(gbMCQues, gbTFQues, gbSAQues);
			GenerateHitBoxes();
		}

		void Save_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = true;
		}

		void Save_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			MessageBox.Show("Game Saved");
		}

		void Load_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = true;
		}

		void Load_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			MessageBox.Show("Game load");
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
		}

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

		#region Move player around room
		//Controls the speed of how fast player moves and holds what key was pressed last
		private int SPEED = 3;
		private Key prevKey = new Key();
		private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			TriggerPlayerGif(1);
			if (e.Key == Key.Right)
			{
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

					//Eli's testing for MazeDriver
					engine.CurrentDoor = "Door4";
					//engine.Answered(true);

					//Testing for QuestionDriver
					myQuestionDriver.Display();

					//Disable Keys
					this.PreviewKeyDown -= Window_PreviewKeyDown;

				}
            }

			else if (e.Key == Key.Left)
			{
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
					//Eli's testing for MazeDriver
					engine.CurrentDoor = "Door2";
					//engine.Answered(true);

					//Testing for QuestionDriver
					myQuestionDriver.Display();

					//Disable Keys
					this.PreviewKeyDown -= Window_PreviewKeyDown;
				}
            }

			else if (e.Key == Key.Up)
			{

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
					//Eli's testing for MazeDriver
					engine.CurrentDoor = "Door1";
					//engine.Answered(true);

					//Testing for QuestionDriver
					myQuestionDriver.Display();

					//Disable Keys
					this.PreviewKeyDown -= Window_PreviewKeyDown;
				}
            }

			else if (e.Key == Key.Down)
			{
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
					//Eli's testing for MazeDriver
					engine.CurrentDoor = "Door3";
					//engine.Answered(true);

					// Displays Question
					myQuestionDriver.Display();

					//Disable Keys
					this.PreviewKeyDown -= Window_PreviewKeyDown;
				}
            }
			//Used to gather the previous key pressed
			prevKey = e.Key;
		}
		
		//Center player to the middle of the room if user answers question correctly
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
					if(!DoorLockedOrShown(i))// If user has hit the door checks that current door to see if it is locked or shown. else false.
						return true;
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
		int frames;
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
			/*engine.Answered(true);
			CenterPlayer();
			ResetGrids();*/
			if (rdTFAns1.IsChecked == true)
			{
				engine.Answered(myQuestionDriver.IsCorrect(rdTFAns1.Content.ToString()));

				if (myQuestionDriver.IsCorrect(rdTFAns2.Content.ToString()))
					CenterPlayer();

				ResetGrids();
			}

			else if (rdTFAns2.IsChecked == true)
			{
				engine.Answered(myQuestionDriver.IsCorrect(rdTFAns2.Content.ToString()));

				if (myQuestionDriver.IsCorrect(rdTFAns2.Content.ToString()))
					CenterPlayer();

				ResetGrids();
			}

			else
			{ MessageBox.Show("Please input a valid input "); }

		}

		private void BtnMC_OK_Click(object sender, RoutedEventArgs e)
		{
			//Code for Testing
			/*engine.Answered(true);
			CenterPlayer();
			ResetGrids();*/
			if (rdMCAns1.IsChecked == true)
			{
				engine.Answered(myQuestionDriver.IsCorrect(rdMCAns1.Content.ToString()));

				if (myQuestionDriver.IsCorrect(rdMCAns1.Content.ToString()))
					CenterPlayer();

				ResetGrids();

			}

			else if (rdMCAns2.IsChecked == true)
			{
				engine.Answered(myQuestionDriver.IsCorrect(rdMCAns2.Content.ToString()));

				if (myQuestionDriver.IsCorrect(rdMCAns2.Content.ToString()))
					CenterPlayer();

				ResetGrids();

			}

			else if (rdMCAns3.IsChecked == true)
			{
				engine.Answered(myQuestionDriver.IsCorrect(rdMCAns3.Content.ToString()));

				if (myQuestionDriver.IsCorrect(rdMCAns3.Content.ToString()))
					CenterPlayer();

				ResetGrids();
			}

			else if (rdMCAns4.IsChecked == true)
			{
				engine.Answered(myQuestionDriver.IsCorrect(rdMCAns4.Content.ToString()));

				if (myQuestionDriver.IsCorrect(rdMCAns4.Content.ToString()))
					CenterPlayer();

				ResetGrids();
			}

			else
			{ MessageBox.Show("Please input a valid input "); }

	
		}

        private void BtnSA_OK_Click(object sender, RoutedEventArgs e)
        {
			//Code for Testing
			/*engine.Answered(true);
			CenterPlayer();
			ResetGrids();*/
			if (txtSA.Text.Length != 0)
			{
				engine.Answered(myQuestionDriver.IsCorrect(txtSA.Text));

				if (myQuestionDriver.IsCorrect(txtSA.Text))
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
	}
}
