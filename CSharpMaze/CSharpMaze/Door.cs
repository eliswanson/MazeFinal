using System;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CSharpMaze
{
	class Door
	{
		private Image doorOpened;
		private Image doorClosed;
		private Image doorLocked;

		private double doorWidth;
		private double doorHeight;

		public Door()
		{

			doorOpened = new Image{Stretch = Stretch.Fill};
			doorClosed = new Image{Stretch = Stretch.Fill};
			doorLocked = new Image{Stretch = Stretch.Fill};

			ImageSource closedDoorsrc = new BitmapImage(new Uri("closedDoor.png", UriKind.Relative));
			doorClosed.Source = closedDoorsrc;
			doorClosed.Width = closedDoorsrc.Width;
			doorClosed.Height = closedDoorsrc.Height;

			ImageSource openDoorsrc = new BitmapImage(new Uri("DoorOpen.png", UriKind.Relative));
			doorOpened.Source = openDoorsrc;
			doorOpened.Width = openDoorsrc.Width;
			doorOpened.Height = openDoorsrc.Height;

			ImageSource lockedDoorsrc = new BitmapImage(new Uri("Lock.png", UriKind.Relative));
			doorLocked.Source = lockedDoorsrc;
			doorLocked.Width = lockedDoorsrc.Width;
			doorLocked.Height = lockedDoorsrc.Height;

			this.doorWidth = closedDoorsrc.Width;
			this.doorHeight = closedDoorsrc.Height;

		}

		public double GetDoorWidth()
		{
			return this.doorWidth;
		}

		public double GetDoorHeight()
		{
			return this.doorHeight;
		}

		public void RotateImages()
		{
			this.doorClosed.RenderTransform = new RotateTransform(90);
		}

		public void HideAll()
		{
			doorClosed.Visibility = System.Windows.Visibility.Hidden;
			doorOpened.Visibility = System.Windows.Visibility.Hidden;
			doorLocked.Visibility = System.Windows.Visibility.Hidden;
		}

		public void Closed()
		{
			doorClosed.Visibility = System.Windows.Visibility.Visible;
		}
		public void Opened()
		{
			doorOpened.Visibility = System.Windows.Visibility.Visible;
		}
		public void Locked()
		{
			doorLocked.Visibility = System.Windows.Visibility.Visible;
		}

		public Image DoorClosed()
		{
			return this.doorClosed;
		}

		public Image DoorOpened()
		{
			return this.doorOpened;
		}

		public Image DoorLocked()
		{
			return this.doorLocked;
		}
	}
}
