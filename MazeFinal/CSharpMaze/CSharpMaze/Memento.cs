using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Windows;

namespace CSharpMaze
{
	[Serializable]
	class Memento
	{
		// The state of all rooms to be fed to MazeDriver
		private readonly RoomState[][] roomStates;
		// The list of questions
		private readonly List<QuestionsPackage.Ques_Ans> questionsList;
		// The point location of player
		private readonly Point playerLocation;
	    private readonly Graph<Point> mazeGraph;
	    private string difficultyString;

		///<summary>
		///<para>Creates a new Memento for a game save state Memento(MazeDriver engine, QuestionDriver questions)</para>
		///<para>Instantiates a new memento object</para>
		///</summary>
		public Memento(MazeDriver engine, QuestionDriver questions)
		{
			this.roomStates = engine.RoomStates;			
			this.playerLocation = engine.PlayerPoint;
		    this.mazeGraph = engine.MazeGraph;
		    this.questionsList = questions.QuestionsList;
		    this.difficultyString = questions.DifficultyString;
		}

	public Memento(SerializationInfo info, StreamingContext context)
		{
			try
			{
				roomStates = (RoomState[][])info.GetValue("rooms", typeof(object));
				questionsList = (List<QuestionsPackage.Ques_Ans>)info.GetValue("questions",typeof(object));
				playerLocation = (Point)info.GetValue("PlayerLocation", typeof(Point));

			}
			catch (SerializationException e)
			{
				Console.WriteLine(e.Message);
				throw new SerializationException(e.Message);
			}

			catch(Exception e)
			{
				Console.WriteLine(e.Message);
				throw new Exception(e.Message);
			}
		}

		///<summary>
		///<para>ReturnRooms()</para>
		///<para>Returns a RoomState 2d array to be used</para>
		///</summary>
		public RoomState[][] ReturnRooms()
		{
			return roomStates;
		}		
		///<summary>
		///<para>ReturnPlayerLocation()</para>
		///<para>Returns a Point representing the players location on the minimap</para>
		///</summary>
		public Point ReturnPlayerLocation()
		{
			return playerLocation;
		}

	    public Graph<Point> ReturnGraph()
	    {
	        return mazeGraph;
	    }
	    ///<summary>
	    ///<para>ReturnQuestions()</para>
	    ///<para>Returns a Qyes_Ans list</para>
	    ///</summary>
	    public List<QuestionsPackage.Ques_Ans> ReturnQuestions()
	    {
	        return questionsList;
	    }

	    public string ReturnDifficulty()
	    {
	        return difficultyString;
	    }
    }
}
