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
		private RoomState[][] rooms;
		// The list of questions
		private List<QuestionsPackage.Ques_Ans> questions;
		// The point location of player
		private Point PlayerLocation;

		///<summary>
		///<para>Creates a new Memento for a game save state Memento(MazeDriver engine, QuestionDriver questions)</para>
		///<para>Instantiates a new memento object</para>
		///</summary>
		public Memento(MazeDriver engine, QuestionDriver questions)
		{
			this.rooms = engine.rooms;
			this.questions = questions.questions;
			this.PlayerLocation = engine.location;
		}

		public Memento(SerializationInfo info, StreamingContext context)
		{
			try
			{
				rooms = (RoomState[][])info.GetValue("rooms", typeof(object));
				questions = (List<QuestionsPackage.Ques_Ans>)info.GetValue("questions",typeof(object));
				PlayerLocation = (Point)info.GetValue("PlayerLocation", typeof(Point));

			}
			catch (SerializationException e)
			{
				Console.WriteLine(e.Message);
			}

			catch(Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}

		///<summary>
		///<para>ReturnRooms()</para>
		///<para>Returns a RoomState 2d array to be used</para>
		///</summary>
		public RoomState[][] ReturnRooms()
		{
			return rooms;
		}
		///<summary>
		///<para>ReturnQuestions()</para>
		///<para>Returns a Qyes_Ans list</para>
		///</summary>
		public List<QuestionsPackage.Ques_Ans> ReturnQuestions()
		{
			return questions;
		}
		///<summary>
		///<para>ReturnPlayerLocation()</para>
		///<para>Returns a Point representing the players location on the minimap</para>
		///</summary>
		public Point ReturnPlayerLocation()
		{
			return PlayerLocation;
		}
	}
}
