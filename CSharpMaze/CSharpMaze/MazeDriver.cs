using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CSharpMaze
{
    class MazeDriver
    {
        private Grid griMiniMap;
        private Canvas canBoard;

        private RoomState[][] rooms;
        private Point location;

        private Board board;
        private MiniMap map;

        private RoomState currentRoom;

        public RoomState CurrentRoom
        {
            get
            {
                return currentRoom;
            }
            private set
            {
                currentRoom = value;
            }
        }
        public string CurrentDoor { get; set; }

        public MazeDriver(Grid griMiniMap, Canvas canBoard)
        {
            this.griMiniMap = griMiniMap;
            this.canBoard = canBoard;

            rooms = new RoomState[5][];
            location = new Point(0, 0);

            board = new Board(canBoard);
            map = new MiniMap(griMiniMap);

            for (int i = 0; i < rooms.Length; i++)
            {
                rooms[i] = new RoomState[5];

                for (int j = 0; j < rooms[i].Length; j++)
                {
                    rooms[i][j] = new RoomState { Door1State = 0, Door2State = 0, Door3State = 0, Door4State = 0 };

                    if (i == 0)
                        rooms[i][j].Door1State = 3;
                    if (j == 0)
                        rooms[i][j].Door2State = 3;
                    if (i == rooms.Length - 1)
                        rooms[i][j].Door3State = 3;
                    if (j == rooms[i].Length - 1)
                        rooms[i][j].Door4State = 3;
                }
            }

            CurrentRoom = rooms[0][0];
        }

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

                //Still need to update player character's location. Data binding?    

                CurrentRoom = rooms[(int)location.Y][(int)location.X];
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

                if (!IsPath())
                    MessageBox.Show("You lose!");
            }
        }
        public bool IsPath()
        {
            return true;
        }

        private RoomState UpdateRoom(int newState, Point point, string door)
        {
            if (newState < 0 || newState > 3)
                throw new Exception("newState is out of bounds");

            int x = (int)point.X, y = (int)point.Y;

            switch (door)
            {
                case ("Door1"):
                    rooms[y][x].Door1State = newState;
                    return rooms[y][x];
                case ("Door2"):
                    rooms[y][x].Door2State = newState;
                    return rooms[y][x];
                case ("Door3"):
                    rooms[y][x].Door3State = newState;
                    return rooms[y][x];
                case ("Door4"):
                    rooms[y][x].Door4State = newState;
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
                    room.Door1State = newState;
                    return room;
                case ("Door2"):
                    room.Door2State = newState;
                    return room;
                case ("Door3"):
                    room.Door3State = newState;
                    return room;
                case ("Door4"):
                    room.Door4State = newState;
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

        private string OppositeDoor(string door)
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

