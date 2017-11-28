using System.Windows.Shapes;
using System.Windows.Controls;
using System.Windows.Media;

namespace CSharpMaze
{
    class MiniMap
    {
        private Grid Map;
        private Shape player;
        MiniMapRoom[] theSetOfRooms;

        public MiniMap(Grid theMap)
        {
            this.theSetOfRooms = new MiniMapRoom[25];

            this.Map = theMap;

            for (int i = 0; i < theSetOfRooms.Length; i++)
                theSetOfRooms[i] = (MiniMapRoom)this.Map.Children[i];
            player = (Shape)this.Map.Children[this.Map.Children.Count - 1];

        }

        public void UpdateMap(RoomState doorStatus, System.Windows.Point playerLocation)
        {
            int location = (int)(playerLocation.Y * 5 + playerLocation.X);
            if (doorStatus.Door1State == 1)
                theSetOfRooms[location].door1.Fill = new SolidColorBrush(Colors.Green);
            else if (doorStatus.Door1State == 2)
                theSetOfRooms[location].door1.Fill = new SolidColorBrush(Colors.Red);
            else if (doorStatus.Door1State == 3)
                theSetOfRooms[location].Door1 = System.Windows.Visibility.Hidden;

            if (doorStatus.Door2State == 1)
                theSetOfRooms[location].door2.Fill = new SolidColorBrush(Colors.Green);
            else if (doorStatus.Door2State == 2)
                theSetOfRooms[location].door2.Fill = new SolidColorBrush(Colors.Red);
            else if (doorStatus.Door2State == 3)
                theSetOfRooms[location].Door2 = System.Windows.Visibility.Hidden;

            if (doorStatus.Door3State == 1)
                theSetOfRooms[location].door3.Fill = new SolidColorBrush(Colors.Green);
            else if (doorStatus.Door3State == 2)
                theSetOfRooms[location].door3.Fill = new SolidColorBrush(Colors.Red);
            else if (doorStatus.Door3State == 3)
                theSetOfRooms[location].Door3 = System.Windows.Visibility.Hidden;

            if (doorStatus.Door4State == 1)
                theSetOfRooms[location].door4.Fill = new SolidColorBrush(Colors.Green);
            else if (doorStatus.Door4State == 2)
                theSetOfRooms[location].door4.Fill = new SolidColorBrush(Colors.Red);
            else if (doorStatus.Door4State == 3)
                theSetOfRooms[location].Door4 = System.Windows.Visibility.Hidden;

            this.player.SetValue(Grid.RowProperty, (int)playerLocation.Y);
            this.player.SetValue(Grid.ColumnProperty, (int)playerLocation.X);
        }

        public void UpdateMap(int location, RoomState doorStatus)
        {
            if (doorStatus.Door1State == 1)
                theSetOfRooms[location].door1.Fill = new SolidColorBrush(Colors.Green);
            else if (doorStatus.Door1State == 2)
                theSetOfRooms[location].door1.Fill = new SolidColorBrush(Colors.Red);
            else if (doorStatus.Door1State == 3)
                theSetOfRooms[location].Door1 = System.Windows.Visibility.Hidden;

            if (doorStatus.Door2State == 1)
                theSetOfRooms[location].door2.Fill = new SolidColorBrush(Colors.Green);
            else if (doorStatus.Door2State == 2)
                theSetOfRooms[location].door2.Fill = new SolidColorBrush(Colors.Red);
            else if (doorStatus.Door2State == 3)
                theSetOfRooms[location].Door2 = System.Windows.Visibility.Hidden;

            if (doorStatus.Door3State == 1)
                theSetOfRooms[location].door3.Fill = new SolidColorBrush(Colors.Green);
            else if (doorStatus.Door3State == 2)
                theSetOfRooms[location].door3.Fill = new SolidColorBrush(Colors.Red);
            else if (doorStatus.Door3State == 3)
                theSetOfRooms[location].Door3 = System.Windows.Visibility.Hidden;

            if (doorStatus.Door4State == 1)
                theSetOfRooms[location].door4.Fill = new SolidColorBrush(Colors.Green);
            else if (doorStatus.Door4State == 2)
                theSetOfRooms[location].door4.Fill = new SolidColorBrush(Colors.Red);
            else if (doorStatus.Door4State == 3)
                theSetOfRooms[location].Door4 = System.Windows.Visibility.Hidden;
        }

        public void UpdateMap(System.Windows.Point playerLocation)
        {
            this.player.SetValue(Grid.RowProperty, (int)playerLocation.Y);
            this.player.SetValue(Grid.ColumnProperty, (int)playerLocation.X);
        }
    }
}
