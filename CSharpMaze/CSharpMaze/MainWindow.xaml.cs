using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Drawing;
using System.Windows.Media.Imaging;
using System.Windows.Interop;

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
			RoomState Testroom = new RoomState
			{
				Door1State = 0,
				Door2State = 0,
				Door3State = 0,
				Door4State = 0
			};

			InitializeComponent();
            engine = new MazeDriver(this.MiniMap, this.PlayerRoom);

            gbTFQues.Visibility = System.Windows.Visibility.Hidden;
            gbSAQues.Visibility = System.Windows.Visibility.Hidden;
            gbMCQues.Visibility = System.Windows.Visibility.Hidden;
            myQuestionDriver = new QuestionDriver(gbMCQues, gbTFQues, gbSAQues);

            doorsHitBoxes = new Rect[4];

			doorsHitBoxes[0] = new Rect(new System.Windows.Point((double)PlayerRoom.Children[1].GetValue(Canvas.LeftProperty), (double)PlayerRoom.Children[1].GetValue(Canvas.TopProperty)), new System.Windows.Size((double)PlayerRoom.Children[1].GetValue(Canvas.WidthProperty), (double)PlayerRoom.Children[1].GetValue(Canvas.HeightProperty) - 10));
			doorsHitBoxes[1] = new Rect(new System.Windows.Point(0, (double)PlayerRoom.Children[4].GetValue(Canvas.TopProperty)), new System.Windows.Size((double)PlayerRoom.Children[4].GetValue(Canvas.HeightProperty) - 10, (double)PlayerRoom.Children[4].GetValue(Canvas.WidthProperty)));
			doorsHitBoxes[2] = new Rect(new System.Windows.Point((double)PlayerRoom.Children[7].GetValue(Canvas.LeftProperty), (double)PlayerRoom.Children[7].GetValue(Canvas.TopProperty) + 10), new System.Windows.Size((double)PlayerRoom.Children[7].GetValue(Canvas.WidthProperty), (double)PlayerRoom.Children[7].GetValue(Canvas.HeightProperty) - 10));
			doorsHitBoxes[3] = new Rect(new System.Windows.Point((double)PlayerRoom.Children[10].GetValue(Canvas.LeftProperty) - ((double)PlayerRoom.Children[10].GetValue(Canvas.HeightProperty) - 10), (double)PlayerRoom.Children[10].GetValue(Canvas.TopProperty)), new System.Windows.Size((double)PlayerRoom.Children[10].GetValue(Canvas.HeightProperty), (double)PlayerRoom.Children[10].GetValue(Canvas.WidthProperty)));
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

		private int SPEED = 4;
		Key prevKey = new Key();
		private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Right)
			{
				if (prevKey != Key.Right)
				{
					UpdatePlayersDirection(0);
				}

				else if (Canvas.GetLeft(this.Player) + 2 < this.PlayerRoom.Width - this.Player.Width && !PlayerHitTarget())
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
                }
				prevKey = e.Key;
                
            }

			else if (e.Key == Key.Left)
			{
				if (prevKey != Key.Left)
				{
					UpdatePlayersDirection(180);
				}

				else if (Canvas.GetLeft(this.Player) - 2 > 0 && !PlayerHitTarget())
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
                }
				prevKey = e.Key;

            }

			else if (e.Key == Key.Up)
			{

				if (prevKey != Key.Up)
				{
					UpdatePlayersDirection(-90);
				}

				else if (Canvas.GetTop(this.Player) - 2 > 0 && !PlayerHitTarget())
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
                }
				prevKey = e.Key;
                
            }

			else if (e.Key == Key.Down)
			{
				if (prevKey != Key.Down)
				{
					UpdatePlayersDirection(90);
				}
				else if (Canvas.GetTop(this.Player) + 2 < this.PlayerRoom.Height - this.Player.Height && !PlayerHitTarget())
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
                }
                
            }

		}

		private void UpdatePlayersDirection(double direction)
		{
			Player.RenderTransform = new RotateTransform(direction);
		}

		private bool PlayerHitTarget()
		{
			Rect playerdetect = new Rect(new System.Windows.Point((double)Player.GetValue(Canvas.LeftProperty) + 5, (double)Player.GetValue(Canvas.TopProperty) + 5), new System.Windows.Size((double)Player.Width, (double)Player.Height - 5));
			for (int i = 0; i < doorsHitBoxes.Length; i++)
			{
				if (playerdetect.IntersectsWith(doorsHitBoxes[i]))
					return true;
			}

			return false;
		}

        private void btnTF_OK_Click(object sender, RoutedEventArgs e)
        {
            engine.Answered(true);
            //engine.Answered(myQuestionDriver.IsCorrect());
            gbTFQues.Visibility = System.Windows.Visibility.Hidden;


        }

        private void btnMC_OK_Click(object sender, RoutedEventArgs e)
        {
            engine.Answered(true);
            //engine.Answered(myQuestionDriver.IsCorrect());
            gbMCQues.Visibility = System.Windows.Visibility.Hidden;
        }

        private void btnSA_OK_Click(object sender, RoutedEventArgs e)
        {
            engine.Answered(true);
            //engine.Answered(myQuestionDriver.IsCorrect());
            gbSAQues.Visibility = System.Windows.Visibility.Hidden;
        }

    }
}
