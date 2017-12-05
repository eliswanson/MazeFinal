using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CSharpMaze
{
	public static class Commands
	{
		public static readonly RoutedCommand Save = new RoutedCommand();
		public static readonly RoutedCommand Load = new RoutedCommand();
		public static readonly RoutedCommand Exit = new RoutedCommand();
		public static readonly RoutedCommand About = new RoutedCommand();
		public static readonly RoutedCommand How = new RoutedCommand();
		public static readonly RoutedCommand Game = new RoutedCommand();
		public static readonly RoutedCommand Settings = new RoutedCommand();
	}
}
