using System.Windows.Shapes;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System;

namespace CSharpMaze
{
	class Board
	{
		private double playerDoorWidth;
		private double playerDoorHeight;
		private Door[] doorDisplay;
		private Canvas PlayerBoard;

		public Board(Canvas TheBoard)
		{
			PlayerBoard = TheBoard;

			doorDisplay = new Door[4];

			for (int i = 0; i < doorDisplay.Length; i++)
				doorDisplay[i] = new Door();

			doorDisplay[1].RotateImages();
			this.doorDisplay[0].DoorOpened().RenderTransform = new RotateTransform(180);
			this.doorDisplay[1].DoorOpened().RenderTransform = new RotateTransform(90);
			this.doorDisplay[3].DoorOpened().RenderTransform = new RotateTransform(-90);
			this.doorDisplay[1].DoorLocked().RenderTransform = new RotateTransform(-90);
			this.doorDisplay[2].DoorLocked().RenderTransform = new RotateTransform(180);
			this.doorDisplay[3].DoorLocked().RenderTransform = new RotateTransform(90);
			this.doorDisplay[3].RotateImages();

			this.playerDoorWidth = doorDisplay[0].GetDoorWidth();
			this.playerDoorHeight = doorDisplay[0].GetDoorHeight();

			this.DrawPlayerBoard(this.PlayerBoard);

		}

		public void CurrentRoom(RoomState CurrentRoom)
		{
			this.doorDisplay[0].HideAll();
			this.doorDisplay[1].HideAll();
			this.doorDisplay[2].HideAll();
			this.doorDisplay[3].HideAll();

			if (CurrentRoom.Door1State == 0)
				this.doorDisplay[0].Closed();
			else if (CurrentRoom.Door1State == 1)
				this.doorDisplay[0].Opened();
			else if (CurrentRoom.Door1State == 2)
			{
				this.doorDisplay[0].Locked();
				this.doorDisplay[0].Closed();
			}

			if (CurrentRoom.Door2State == 0)
				this.doorDisplay[1].Closed();
			else if (CurrentRoom.Door2State == 1)
				this.doorDisplay[1].Opened();
			else if (CurrentRoom.Door2State == 2)
			{
				this.doorDisplay[1].Locked();
				this.doorDisplay[1].Closed();
			}

			if (CurrentRoom.Door3State == 0)
				this.doorDisplay[2].Closed();
			else if (CurrentRoom.Door3State == 1)
				this.doorDisplay[2].Opened();
			else if (CurrentRoom.Door3State == 2)
			{
				this.doorDisplay[2].Locked();
				this.doorDisplay[2].Closed();
			}

			if (CurrentRoom.Door4State == 0)
				this.doorDisplay[3].Closed();
			else if (CurrentRoom.Door4State == 1)
				this.doorDisplay[3].Opened();
			else if (CurrentRoom.Door4State == 2)
			{
				this.doorDisplay[3].Locked();
				this.doorDisplay[3].Closed();
			}

		}
		/** Draws the board based on what needs to be shown or not shown **/

		private void DrawPlayerBoard(Canvas PlayerRoom)
		{
			/** Adding door one to the top of player room **/
			PlayerRoom.Children.Add(this.doorDisplay[0].DoorClosed());
			PlayerRoom.Children.Add(this.doorDisplay[0].DoorOpened());
			PlayerRoom.Children.Add(this.doorDisplay[0].DoorLocked());
			Canvas.SetLeft(this.doorDisplay[0].DoorClosed(), ((PlayerRoom.Width - this.playerDoorWidth) / 2));
			Canvas.SetTop(this.doorDisplay[0].DoorClosed(), 0);

			Canvas.SetLeft(this.doorDisplay[0].DoorOpened(), (PlayerRoom.Width-this.doorDisplay[0].DoorOpened().Width)/2 + this.doorDisplay[0].DoorOpened().Width);
			Canvas.SetTop(this.doorDisplay[0].DoorOpened(), this.doorDisplay[0].DoorOpened().Height);

			Canvas.SetLeft(this.doorDisplay[0].DoorLocked(), ((PlayerRoom.Width - this.doorDisplay[0].DoorLocked().Width) / 2));

			/** Adding door two to the left of player board **/
			PlayerRoom.Children.Add(this.doorDisplay[1].DoorClosed());
			PlayerRoom.Children.Add(this.doorDisplay[1].DoorOpened());
			PlayerRoom.Children.Add(this.doorDisplay[1].DoorLocked());
			Canvas.SetLeft(this.doorDisplay[1].DoorClosed(), this.playerDoorHeight);
			Canvas.SetTop(this.doorDisplay[1].DoorClosed(), (PlayerRoom.Height - this.playerDoorWidth) / 2);

			Canvas.SetLeft(this.doorDisplay[1].DoorOpened(), this.doorDisplay[1].DoorOpened().Height);
			Canvas.SetTop(this.doorDisplay[1].DoorOpened(), ((PlayerBoard.Height - this.doorDisplay[1].DoorOpened().Width) / 2));

			Canvas.SetTop(this.doorDisplay[1].DoorLocked(), ((this.PlayerBoard.Height - this.doorDisplay[1].DoorLocked().Width) / 2) + this.doorDisplay[1].DoorLocked().Width);

			/** Adding door three to the bottom of player board **/
			PlayerRoom.Children.Add(this.doorDisplay[2].DoorClosed());
			PlayerRoom.Children.Add(this.doorDisplay[2].DoorOpened());
			PlayerRoom.Children.Add(this.doorDisplay[2].DoorLocked());
			Canvas.SetLeft(this.doorDisplay[2].DoorClosed(), ((PlayerRoom.Width - this.playerDoorWidth) / 2));
			Canvas.SetTop(this.doorDisplay[2].DoorClosed(), this.PlayerBoard.Height - this.playerDoorHeight);

			Canvas.SetLeft(this.doorDisplay[2].DoorOpened(), ((PlayerRoom.Width - this.doorDisplay[2].DoorOpened().Width) / 2));
			Canvas.SetTop(this.doorDisplay[2].DoorOpened(), this.PlayerBoard.Height -this.doorDisplay[2].DoorOpened().Height);

			Canvas.SetLeft(this.doorDisplay[2].DoorLocked(), ((PlayerRoom.Width - this.doorDisplay[2].DoorLocked().Width) / 2) + this.doorDisplay[2].DoorLocked().Width);
			Canvas.SetTop(this.doorDisplay[2].DoorLocked(), this.PlayerBoard.Height - this.doorDisplay[2].DoorLocked().Height + this.doorDisplay[2].DoorLocked().Height);

			/** Adding door four to the right of the player board **/
			PlayerRoom.Children.Add(this.doorDisplay[3].DoorClosed());
			PlayerRoom.Children.Add(this.doorDisplay[3].DoorOpened());
			PlayerRoom.Children.Add(this.doorDisplay[3].DoorLocked());
			Canvas.SetTop(this.doorDisplay[3].DoorClosed(), (PlayerRoom.Height - this.playerDoorWidth) / 2);
			Canvas.SetLeft(this.doorDisplay[3].DoorClosed(), PlayerRoom.Width);

			Canvas.SetTop(this.doorDisplay[3].DoorOpened(), ((PlayerBoard.Height - this.doorDisplay[3].DoorOpened().Height) + this.doorDisplay[3].DoorOpened().Height) /2 + this.doorDisplay[3].DoorOpened().Height);
			Canvas.SetLeft(this.doorDisplay[3].DoorOpened(), PlayerRoom.Width - this.doorDisplay[3].DoorOpened().Height);

			Canvas.SetTop(this.doorDisplay[3].DoorLocked(), (PlayerRoom.Height - this.doorDisplay[3].DoorLocked().Height) / 2);
			Canvas.SetLeft(this.doorDisplay[3].DoorLocked(), PlayerRoom.Width);
		}
	}
}
