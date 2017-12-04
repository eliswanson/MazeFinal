namespace CSharpMaze
{
	class Originator
	{
		///<summary>
		///<para>CreateMemento(MazeDrive engine, QuestionDriver)</para>
		///<para>Returns a new memento object or "save state" of an object</para>
		///</summary>
		public Memento CreateMemento(MazeDriver engine, QuestionDriver questions)
		{
			return new Memento(engine, questions);
		}

		///<summary>
		///<para>RestoreFromMemento(Memento m, MazeDriver e, QuestionDriver q)</para>
		///<para>Restores the objects saved from text file back to the objects</para>
		///</summary>
		public void RestoreFromMemento(Memento myMemento, MazeDriver engine, QuestionDriver questions)
		{
			engine.rooms = myMemento.ReturnRooms();
			questions.questions = myMemento.ReturnQuestions();
			engine.location = myMemento.ReturnPlayerLocation();
		}

	}
}
