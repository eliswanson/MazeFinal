﻿using System;
using System.Windows;
using System.Windows.Controls;

namespace CSharpMaze
{
    class MazeDriver
    {
        #region fields and properties
        public RoomState[][] RoomStates { get; set; }         

        private readonly Point endPoint;
        public Point PlayerPoint { get; set; }

        private readonly Board board;
        private readonly MiniMap miniMap;
        private readonly Graph<Point> mazeGraph;

        public RoomState CurrentRoomState { get; private set; }
        public string CurrentDoorString { get; set; }
        #endregion
        public MazeDriver(Grid griMiniMap, Canvas canBoard)
        {
            RoomStates = new RoomState[5][];
            mazeGraph = new Graph<Point>();
            PlayerPoint = new Point(0, 0);

            board = new Board(canBoard);
            miniMap = new MiniMap(griMiniMap);

            for (int col = 0; col < RoomStates.Length; col++)
            {
                RoomStates[col] = new RoomState[5];

                for (int row = 0; row < RoomStates[col].Length; row++)
                {                    
                    int leftCol = 0;
                    int rightCol = RoomStates[col].Length - 1;
                    int topRow = 0;
                    int botRow = RoomStates.Length - 1;
                    Point roomPoint = new Point(row, col);
                    RoomState curRoom = new RoomState
                    {
                        Door1State = RoomState.Closed,
                        Door2State = RoomState.Closed,
                        Door3State = RoomState.Closed,
                        Door4State = RoomState.Closed
                    };

                    mazeGraph.AddVertex(roomPoint);

                    if (col == leftCol) //Sets doors to hidden for RoomStates that are on edge of map
                    {
                        curRoom.Door1State = RoomState.Hidden;
                    }
                    else
                    {
                        Point top = new Point(row, col - 1);
                        if (mazeGraph.Contains(top))
                            mazeGraph.Connect(roomPoint, top);
                    }
                    if (row == topRow)
                    {
                        curRoom.Door2State = RoomState.Hidden;
                    }
                    else
                    {
                        Point left = new Point(row - 1, col);
                        if (mazeGraph.Contains(left))
                            mazeGraph.Connect(roomPoint, left);
                    }
                    if (col == rightCol)
                    {
                        curRoom.Door3State = RoomState.Hidden;
                    }
                    else
                    {
                        Point bot = new Point(row, col + 1);
                        if (mazeGraph.Contains(bot))
                            mazeGraph.Connect(roomPoint, bot);
                    }
                    if (row == botRow)
                    {
                        curRoom.Door4State = RoomState.Hidden;
                    }
                    else
                    {
                        Point right = new Point(row + 1, col);
                        if (mazeGraph.Contains(right))
                            mazeGraph.Connect(roomPoint, right);
                    }

                    RoomStates[col][row] = curRoom;
                }
            }

            endPoint = new Point(4,4); //add class variable that defines number of cols and rows?
            CurrentRoomState = RoomStates[0][0];
            board.CurrentRoom(RoomStates[0][0]);
        }
        /// <summary>
        /// Used for deserialization
        /// </summary>
        /// <param name="griMiniMap">Grid that MiniMap will use</param>
        /// <param name="canBoard">Canvas that Board will use</param>
        /// <param name="roomStates">RoomState[][] deserialized from file</param>
        /// <param name="playerPoint">Point deserialized from file</param>
        public MazeDriver(Grid griMiniMap, Canvas canBoard, RoomState[][] roomStates, Point playerPoint)
        {
            RoomStates = roomStates;
            CurrentRoomState = RoomStates[(int)PlayerPoint.Y][(int)PlayerPoint.X];
            
            PlayerPoint = playerPoint;
            endPoint = new Point(4, 4);

            board = new Board(canBoard);
            miniMap = new MiniMap(griMiniMap);

            mazeGraph = new Graph<Point>();

            miniMap.UpdateMap(CurrentRoomState, PlayerPoint);                        
            board.CurrentRoom(CurrentRoomState);

            for (int col = 0; col < RoomStates.Length; col++)
            {
                for (int row = 0; row < RoomStates[col].Length; row++)
                {
                    int leftCol = 0;
                    int rightCol = RoomStates[col].Length - 1;
                    int topRow = 0;
                    int botRow = RoomStates.Length - 1;
                    Point roomPoint = new Point(row, col);
                    RoomState curRoomState = RoomStates[col][row];
                        
                    mazeGraph.AddVertex(roomPoint);

                    if (col != leftCol) //Sets doors to hidden for RoomStates that are on edge of map
                    {
                        Point top = new Point(row, col - 1);
                        if (mazeGraph.Contains(top))
                            mazeGraph.Connect(roomPoint, top);
                    }
                    if (row != topRow)
                    {
                        Point left = new Point(row - 1, col);
                        if (mazeGraph.Contains(left))
                            mazeGraph.Connect(roomPoint, left);
                    }
                    if (col != rightCol)
                    {
                        Point bot = new Point(row, col + 1);
                        if (mazeGraph.Contains(bot))
                            mazeGraph.Connect(roomPoint, bot);
                    }
                    if (row == botRow)
                    {
                        Point right = new Point(row + 1, col);
                        if (mazeGraph.Contains(right))
                            mazeGraph.Connect(roomPoint, right);
                    }

                    miniMap.UpdateMap(col * 5 + row, curRoomState);
                }                
            }

           
        }

        #region Updating minimap and board
        /// <summary>
        /// Updates state of board, minimap and determine win or loss after player answers a question.
        /// </summary>
        /// <param name="answer"></param>
        public void Answered(bool answer) //should return bool if win or loss? let mainwindow handle resetting.
        {
            if (answer) //open door and move player
            {
                UpdateRoom(1, RoomStates[(int)PlayerPoint.Y][(int)PlayerPoint.X], CurrentDoorString); //open door in the current room
                miniMap.UpdateMap((int)PlayerPoint.Y * 5 + (int)PlayerPoint.X, RoomStates[(int)PlayerPoint.Y][(int)PlayerPoint.X]); //update minimap  

                PlayerPoint = UpdateLocation(PlayerPoint, CurrentDoorString); //change PlayerLocation of player                                              

                CurrentDoorString = OppositeDoor(CurrentDoorString); //get door opposite of the one opened      

                UpdateRoom(1, RoomStates[(int)PlayerPoint.Y][(int)PlayerPoint.X], CurrentDoorString); //open door in room player moved to
                miniMap.UpdateMap(RoomStates[(int)PlayerPoint.Y][(int)PlayerPoint.X], PlayerPoint); //update minimap and moves star
                board.CurrentRoom(RoomStates[(int)PlayerPoint.Y][(int)PlayerPoint.X]); //update board

                UpdateCurrentRoom(PlayerPoint);
                if (IsWinner()) //checks to see if winner
                    MessageBox.Show("Winner!");
            }
            else //lock door and run maze algorithm
            {
                RoomState otherRoom; //used for room on other side of door
                Point otherPoint;

                UpdateRoom(2, RoomStates[(int)PlayerPoint.Y][(int)PlayerPoint.X], CurrentDoorString);
                miniMap.UpdateMap((int)PlayerPoint.Y * 5 + (int)PlayerPoint.X, RoomStates[(int)PlayerPoint.Y][(int)PlayerPoint.X]); //update current room

                otherPoint = UpdateLocation(PlayerPoint, CurrentDoorString); //get point for other door
                otherRoom = UpdateRoom(2, RoomStates[(int)otherPoint.Y][(int)otherPoint.X], OppositeDoor(CurrentDoorString)); //update room on other side of door    

                miniMap.UpdateMap((int)otherPoint.Y * 5 + (int)otherPoint.X, otherRoom); //update minimap for room on other side of the door
                board.CurrentRoom(RoomStates[(int)PlayerPoint.Y][(int)PlayerPoint.X]);

                mazeGraph.Disconnect(PlayerPoint, otherPoint);

                if (IsLoser())
                    MessageBox.Show("You lose!");
            }
        }
        private bool IsLoser()
        {
            return(!mazeGraph.BFS(PlayerPoint, endPoint));
        }

        private bool IsWinner()
        {
            return PlayerPoint.Equals(endPoint);
        }
        /// <summary>
        /// Called if player goes through an open door. Moves player to next room and updates board, 
        /// </summary>
        /// <param name="door"></param>
        public void OpenDoor(string door) //rename method?
        {
            PlayerPoint = UpdateLocation(PlayerPoint, door); //change PlayerLocation of player 
            miniMap.UpdateMap(PlayerPoint);
            board.CurrentRoom(UpdateCurrentRoom(PlayerPoint));
            CurrentDoorString = OppositeDoor(door);
        }
        #endregion

        #region Helper methods for updating room and player location on minimap
        private RoomState UpdateCurrentRoom(Point newLocation) //Updates CurrentRoom and returns it
        {
            return CurrentRoomState = RoomStates[(int)newLocation.Y][(int)newLocation.X];
        }

        private RoomState UpdateRoom(int newState, Point point, string door) //never used, delete?
        {
            if (newState < 0 || newState > 3)
                throw new IndexOutOfRangeException("newState is out of bounds");

            int x = (int)point.X, y = (int)point.Y;

            switch (door)
            {
                case ("Door1"):
                    RoomStates[y][x].Door1State = newState;
                    return RoomStates[y][x];
                case ("Door2"):
                    RoomStates[y][x].Door2State = newState;
                    return RoomStates[y][x];
                case ("Door3"):
                    RoomStates[y][x].Door3State = newState;
                    return RoomStates[y][x];
                case ("Door4"):
                    RoomStates[y][x].Door4State = newState;
                    return RoomStates[y][x];
                default:
                    throw new ArgumentException("Bad door string passed"); //change
            }
        }
        private RoomState UpdateRoom(int newState, RoomState room, string door)
        {
            if (newState < 0 || newState > 3)
                throw new IndexOutOfRangeException("newState is out of bounds");

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
                default:
                    throw new ArgumentException("Bad door string passed");
            }
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
                default:
                    throw new ArgumentException("Bad door string passed");
            }            
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
                default:
                    throw new ArgumentException("Bad door string passed");
            }
        }
    }

}

