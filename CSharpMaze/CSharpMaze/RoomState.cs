using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CSharpMaze
{
	[Serializable]
	class RoomState
	{

		public int Door1State { get; set; }
		public int Door2State { get; set; }
		public int Door3State { get; set; }
		public int Door4State { get; set; }
	}
}
