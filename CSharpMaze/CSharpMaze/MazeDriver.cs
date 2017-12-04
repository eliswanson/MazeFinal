using System;
using System.Linq.Expressions;
using System.Windows;
using System.Windows.Controls;

//use int constants for doorstate? 0 closed, 1 open etc.
namespace CSharpMaze
{
    class MazeDriver
    {
        #region fields and properties
        private Grid griMiniMap;
        private Canvas canBoard;

        private RoomState[][] rooms;
        private Point location;

        private Board board;
        private MiniMap map;
        private Graph<Point> mazeGraph;

        public RoomState CurrentRoom
        {
            get; private set;
        }
        public string CurrentDoor { get; set; }
#endregion
        public MazeDriver(Grid griMiniMap, Canvas canBoard)
        {         
            this.griMiniMap = griMiniMap;
            this.canBoard = canBoard;

            this.rooms = new RoomState[5][];
            this.mazeGraph = new Graph<Point>();
            this.location = new Point(0, 0);

            this.board = new Board(canBoard);
            this.map = new MiniMap(griMiniMap);

            for (int col = 0; col < rooms.Length; col++)
            {
                rooms[col] = new RoomState[5];

                for (int row = 0; row < rooms[col].Length; row++)
                {
                    Point curPoint = new Point(row, col);
                    int leftCol = 0;
                    int rightCol = rooms[col].Length - 1;
                    int topRow = 0;
                    int botRow = rooms.Length - 1;

                    RoomState curRoom = new RoomState
                    {
                        Door1 = RoomState.Closed,
                        Door2 = RoomState.Closed,
                        Door3 = RoomState.Closed,
                        Door4 = RoomState.Closed
                    };

                    mazeGraph.AddVertex(curPoint);

                    if (col == leftCol) //Sets doors to hidden for rooms that are on edge of map
                    {
                        curRoom.Door1 = RoomState.Hidden;
                    }
                    else 
                    {
                        Point above = new Point(row - 1, col);
                            if (mazeGraph.Contains(above))
                                mazeGraph.Connect(curPoint, above);
                    }
                    if (row == topRow)
                    {
                        curRoom.Door2 = RoomState.Hidden;
                    }
                    else
                    {
                        Point left = new Point(row - 1, col);
                        if (mazeGraph.Contains(left))
                            mazeGraph.Connect(curPoint, left);
                    }
                    if (col == rightCol)
                    {
                        curRoom.Door3 = RoomState.Hidden;
                    }
                    else
                    {
                        Point bot = new Point(row - 1, col);
                        if (mazeGraph.Contains(bot))
                            mazeGraph.Connect(curPoint, bot);
                    }
                    if (row == botRow)
                    {
                        curRoom.Door4 = RoomState.Hidden;
                    }
                    else
                    {
                        Point right = new Point(row - 1, col);
                        if (mazeGraph.Contains(right))
                            mazeGraph.Connect(curPoint, right);
                    }

                    rooms[col][row] = curRoom;
                }
            }

            CurrentRoom = rooms[0][0];

			this.board.CurrentRoom(rooms[0][0]);
        }

        #region Updating minimap and board
        public void Answered(bool answer)
        {
            if (answer) //open door and move player
            {
                UpdateRoom(1, rooms[(int)location.Y][(int)location.X], CurrentDoor); //open door in the current room
                map.UpdateMap((int)location.Y * 5 + (int)location.X, rooms[(int)location.Y][(int)location.X]); //update minimap  

                location = UpdateLocation(location, CurrentDoor); //change location of player                                              

                CurrentDoor = OppositeDoor(CurrentDoor); //get door opposite of one the one opened      

                UpdateRoom(1, rooms[(int)location.Y][(int)location.X], CurrentDoor); //open door in room player moved to
                map.UpdateMap(rooms[(int)location.Y][(int)location.X], location); //update minimap and moves star
                board.CurrentRoom(rooms[(int)location.Y][(int)location.X]);

                IsWinner(UpdateCurrentRoom(location));
            }
            else //lock door and run maze algorithm
            {
                RoomState otherRoom; //used for room on other side of door
                Point otherPoint;

                UpdateRoom(2, rooms[(int)location.Y][(int)location.X], CurrentDoor);
                map.UpdateMap((int)location.Y * 5 + (int)location.X, rooms[(int)location.Y][(int)location.X]); //update current room

                otherPoint = UpdateLocation(location, CurrentDoor); //get point for other door
                otherRoom = UpdateRoom(2, rooms[(int)otherPoint.Y][(int)otherPoint.X], OppositeDoor(CurrentDoor)); //update room on other side of door    

                map.UpdateMap((int)otherPoint.Y * 5 + (int)otherPoint.X, otherRoom); //update minimap for room on other side of the door
                board.CurrentRoom(rooms[(int)location.Y][(int)location.X]);

                IsLoser();
            }
        }
        public bool IsLoser()
        {

            return true;
        }

        public bool IsWinner(RoomState room)
        {
            if (room.Equals(rooms[4][4]))
            {
                MessageBox.Show("Winner!");
                return true;
            }
            return false;
        }

        public void OpenDoor(string door)
        {
            location = UpdateLocation(location, door); //change location of player 
            map.UpdateMap(location);                    
            board.CurrentRoom(UpdateCurrentRoom(location));
            CurrentDoor = OppositeDoor(door);
        }
        #endregion

        #region Helper methods for updating room and player location on minimap
        private RoomState UpdateCurrentRoom(Point newLocation) //Updates CurrentRoom and returns it
        {
            return CurrentRoom = rooms[(int)newLocation.Y][(int)newLocation.X];
        }

        private RoomState UpdateRoom(int newState, Point point, string door)
        {
            if (newState < 0 || newState > 3)
                throw new Exception("newState is out of bounds");

            int x = (int)point.X, y = (int)point.Y;

            switch (door)
            {
                case ("Door1"):
                    rooms[y][x].Door1 = newState;
                    return rooms[y][x];
                case ("Door2"):
                    rooms[y][x].Door2 = newState;
                    return rooms[y][x];
                case ("Door3"):
                    rooms[y][x].Door3 = newState;
                    return rooms[y][x];
                case ("Door4"):
                    rooms[y][x].Door4 = newState;
                    return rooms[y][x];
            }

            throw new Exception("Bad door string passed");
        }
        private RoomState UpdateRoom(int newState, RoomState room, string door)
        {
            if (newState < 0 || newState > 3)
                throw new Exception("newState is out of bounds");

            switch (door)
            {
                case ("Door1"):
                    room.Door1 = newState;
                    return room;
                case ("Door2"):
                    room.Door2 = newState;
                    return room;
                case ("Door3"):
                    room.Door3 = newState;
                    return room;
                case ("Door4"):
                    room.Door4 = newState;
                    return room;
            }

            throw new Exception("Bad door string passed");
        }
        private Point UpdateLocation(Point point, string door)
        {
            switch (door)
            {
                case ("Door1"):
                    return new Point(point.X, point.Y - 1);
                case ("Door2"):
                    return new Point(point.X - 1, point.Y);
                case ("Door3"):
                    return new Point(point.X, point.Y + 1);
                case ("Door4"):
                    return new Point(point.X + 1, point.Y);
            }

            throw new Exception("Bad door string passed");
        }
#endregion

        public string OppositeDoor(string door)
        {
            switch (door)
            {
                case ("Door1"):
                    return "Door3";

                case ("Door2"):
                    return "Door4";

                case ("Door3"):
                    return "Door1";

                case ("Door4"):
                    return "Door2";
            }

            throw new Exception("Bad door");
        }    
    }

}

