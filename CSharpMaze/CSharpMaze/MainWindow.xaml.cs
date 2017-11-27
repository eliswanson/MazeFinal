using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Drawing;
using System.Windows.Media.Imaging;
using System.Windows.Interop;
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
			
			/***Remove these later once look and feel is established. Set them under properties ****/
            gbTFQues.Visibility = System.Windows.Visibility.Hidden;
            gbSAQues.Visibility = System.Windows.Visibility.Hidden;
            gbMCQues.Visibility = System.Windows.Visibility.Hidden;
			/*************************** End Remove *************************************/
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

		//Generate door Hitboxes
		private void GenerateHitBoxes()
		{
			doorsHitBoxes = new Rect[4];
			doorsHitBoxes[0] = new Rect(new System.Windows.Point((double)PlayerRoom.Children[1].GetValue(Canvas.LeftProperty), (double)PlayerRoom.Children[1].GetValue(Canvas.TopProperty)), new System.Windows.Size((double)PlayerRoom.Children[1].GetValue(Canvas.WidthProperty), (double)PlayerRoom.Children[1].GetValue(Canvas.HeightProperty) - 10));
			doorsHitBoxes[1] = new Rect(new System.Windows.Point(0, (double)PlayerRoom.Children[4].GetValue(Canvas.TopProperty)), new System.Windows.Size((double)PlayerRoom.Children[4].GetValue(Canvas.HeightProperty) - 10, (double)PlayerRoom.Children[4].GetValue(Canvas.WidthProperty)));
			doorsHitBoxes[2] = new Rect(new System.Windows.Point((double)PlayerRoom.Children[7].GetValue(Canvas.LeftProperty), (double)PlayerRoom.Children[7].GetValue(Canvas.TopProperty) + 10), new System.Windows.Size((double)PlayerRoom.Children[7].GetValue(Canvas.WidthProperty), (double)PlayerRoom.Children[7].GetValue(Canvas.HeightProperty) - 10));
			doorsHitBoxes[3] = new Rect(new System.Windows.Point((double)PlayerRoom.Children[10].GetValue(Canvas.LeftProperty) - ((double)PlayerRoom.Children[10].GetValue(Canvas.HeightProperty) - 10), (double)PlayerRoom.Children[10].GetValue(Canvas.TopProperty)), new System.Windows.Size((double)PlayerRoom.Children[10].GetValue(Canvas.HeightProperty), (double)PlayerRoom.Children[10].GetValue(Canvas.WidthProperty)));
		}

		//Controls the speed of how fast player moves and holds what key was pressed last
		private int SPEED = 3;
		private Key prevKey = new Key();
		private int AtCurrentDoor;
		private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			TriggerPlayerGif(1);
			if (e.Key == Key.Right)
			{
				if (prevKey != Key.Right)
				{
					UpdatePlayersDirection(0);
				}

				else if (Canvas.GetLeft(this.Player) + 2 < this.PlayerRoom.Width - this.Player.Width )
				{
					Canvas.SetLeft(this.Player, Canvas.GetLeft(this.Player) + SPEED);	
				}

				else if (PlayerHitTarget() )
				{
                    //Eli's testing for MazeDriver
                    engine.CurrentDoor = "Door4";
                    //engine.Answered(true);

                    //Testing for QuestionDriver
                    myQuestionDriver.Display();
					//Disable Keys
					this.PreviewKeyDown -= Window_PreviewKeyDown;

					AtCurrentDoor = 3;
					// Used to test whether user hit door
					Console.WriteLine("Hit");
				}
            }

			else if (e.Key == Key.Left)
			{
				if (prevKey != Key.Left)
				{
					UpdatePlayersDirection(180);
				}

				else if (Canvas.GetLeft(this.Player) - 2 > 0 )
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

					AtCurrentDoor = 1;
					// Used to test whether user hit door
					Console.WriteLine("Hit");
				}
			

            }

			else if (e.Key == Key.Up)
			{

				if (prevKey != Key.Up)
				{
					UpdatePlayersDirection(-90);
				}

				else if (Canvas.GetTop(this.Player) - 4 > 0)
				{

					Canvas.SetTop(this.Player, Canvas.GetTop(this.Player) - SPEED);

				}
				else if (PlayerHitTarget())
				{
                    //Eli's testing for MazeDriver
                    engine.CurrentDoor = "Door1";
                    //engine.Answered(true);

                    //Testing for QuestionDriver
                    myQuestionDriver.Display();

					//Disable Keys
					this.PreviewKeyDown -= Window_PreviewKeyDown;

					AtCurrentDoor = 0;
					// Used to test whether user hit door
					Console.WriteLine("Hit");
				}
            }

			else if (e.Key == Key.Down)
			{
				if (prevKey != Key.Down)
				{
					UpdatePlayersDirection(90);
				}
				else if (Canvas.GetTop(this.Player) + 2 < this.PlayerRoom.Height - this.Player.Height )
				{
					Canvas.SetTop(this.Player, Canvas.GetTop(this.Player) + SPEED);
				}
				else if (PlayerHitTarget())
				{
                    //Eli's testing for MazeDriver
                    engine.CurrentDoor = "Door3";
                    //engine.Answered(true);

                    //Testing for QuestionDriver
                    myQuestionDriver.Display();

					//Disable Keys
					this.PreviewKeyDown-=Window_PreviewKeyDown;

					AtCurrentDoor = 2;
					// Used to test whether user hit door
					Console.WriteLine("Hit");

				}
                
            }

			//Used to gather the previous key pressed
			prevKey = e.Key;
		}
		// Updates player direction based on the key pressed.
		private void UpdatePlayersDirection(double direction)
		{
			Player.RenderTransform = new RotateTransform(direction);
		}
		//Checks to see if player has hit any objects on board to trigger event.
		private bool PlayerHitTarget()
		{ 
			Rect playerdetect = new Rect(new System.Windows.Point((double)Player.GetValue(Canvas.LeftProperty) + 5, (double)Player.GetValue(Canvas.TopProperty) + 5), new System.Windows.Size((double)Player.Width, (double)Player.Height - 5));
			for (int i = 0; i < doorsHitBoxes.Length; i++)
				if (playerdetect.IntersectsWith(doorsHitBoxes[i]))
					if(PlayerHitSpecificTarget(i))
						return true;
			
			return false;
		}
		// Specifies if user has hit any specific door based.
		private bool PlayerHitSpecificTarget(int door)
		{
			Rect playerdetect = new Rect(new System.Windows.Point((double)Player.GetValue(Canvas.LeftProperty) + 5, (double)Player.GetValue(Canvas.TopProperty) + 5), new System.Windows.Size((double)Player.Width, (double)Player.Height - 5));

			if (playerdetect.IntersectsWith(doorsHitBoxes[door])) {

				switch (door)
				{
					case 0:
						return engine.CurrentRoom.Door1State != 3 && engine.CurrentRoom.Door1State != 2;		
				
					case 1:
						return engine.CurrentRoom.Door2State != 3 && engine.CurrentRoom.Door1State != 2;
				
					case 2:
						return engine.CurrentRoom.Door3State != 3 && engine.CurrentRoom.Door1State != 2;
						
					case 3:
						return engine.CurrentRoom.Door4State != 3 && engine.CurrentRoom.Door1State != 2;	
				}
			}

			return false;
		}

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
#endregion

		#region gather user information
		private void btnTF_OK_Click(object sender, RoutedEventArgs e)
        {
			if (rdTFAns1.IsChecked == true)
			{ engine.Answered(myQuestionDriver.IsCorrect(rdTFAns1.Content.ToString()));
				this.PreviewKeyDown += Window_PreviewKeyDown;
				gbTFQues.Visibility = System.Windows.Visibility.Hidden;
			}

			else if (rdTFAns2.IsChecked == true)
			{ engine.Answered(myQuestionDriver.IsCorrect(rdTFAns2.Content.ToString()));
				this.PreviewKeyDown += Window_PreviewKeyDown;
				gbTFQues.Visibility = System.Windows.Visibility.Hidden;
			}

			else
			{ MessageBox.Show("Please input a valid input "); }

        }

		private void btnMC_OK_Click(object sender, RoutedEventArgs e)
		{
			if (rdMCAns1.IsChecked == true)
			{
				engine.Answered(myQuestionDriver.IsCorrect(rdMCAns1.Content.ToString()));
				this.PreviewKeyDown += Window_PreviewKeyDown;
				gbMCQues.Visibility = System.Windows.Visibility.Hidden;
			}

			else if (rdMCAns2.IsChecked == true)
			{
				engine.Answered(myQuestionDriver.IsCorrect(rdMCAns2.Content.ToString()));
				this.PreviewKeyDown += Window_PreviewKeyDown;
				gbMCQues.Visibility = System.Windows.Visibility.Hidden;
			}

			else if (rdMCAns3.IsChecked == true)
			{
				engine.Answered(myQuestionDriver.IsCorrect(rdMCAns3.Content.ToString()));
				this.PreviewKeyDown += Window_PreviewKeyDown;
				gbMCQues.Visibility = System.Windows.Visibility.Hidden;
			}

			else if (rdMCAns4.IsChecked == true)
			{
				engine.Answered(myQuestionDriver.IsCorrect(rdMCAns4.Content.ToString()));
				this.PreviewKeyDown += Window_PreviewKeyDown;
				gbMCQues.Visibility = System.Windows.Visibility.Hidden;
			}

			else
			{ MessageBox.Show("Please input a valid input "); }
        }

        private void btnSA_OK_Click(object sender, RoutedEventArgs e)
        {
			if (txtSA.Text.Length != 0)
			{
				engine.Answered(myQuestionDriver.IsCorrect(txtSA.Text));
				this.PreviewKeyDown += Window_PreviewKeyDown;
				gbSAQues.Visibility = System.Windows.Visibility.Hidden;
			}

			else
			{ MessageBox.Show("Please input a valid input "); }

		}
#endregion
	}
}
