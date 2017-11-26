using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CSharpMaze
{
	/// <summary>
	/// Interaction logic for MiniMapRoom.xaml
	/// </summary>
	public partial class MiniMapRoom : UserControl
	{
		public MiniMapRoom()
		{
			InitializeComponent();

		}

		public Visibility Door1
		{
			get { return this.door1.Visibility; }
			set { this.door1.Visibility = value; }
		}

		public Visibility Door2
		{
			get { return this.door2.Visibility; }
			set { this.door2.Visibility = value; }
		}

		public Visibility Door3
		{
			get { return this.door3.Visibility; }
			set { this.door3.Visibility = value; }
		}

		public Visibility Door4
		{
			get { return this.door4.Visibility; }
			set { this.door4.Visibility = value; }
		}

	}
}
