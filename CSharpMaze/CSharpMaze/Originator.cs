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
		public void RestoreFromMemento(Memento myMemento, out MazeDriver engine, out QuestionDriver questions, MainWindow mainWindow)
		{
			engine = new MazeDriver(mainWindow.MiniMap, mainWindow.PlayerRoom, myMemento.ReturnRooms(), myMemento.ReturnPlayerLocation(), myMemento.ReturnGraph());
            questions = new QuestionDriver(mainWindow.gbMCQues, mainWindow.gbTFQues, mainWindow.gbSAQues, myMemento.ReturnQuestions(), myMemento.ReturnDifficulty());
		}

	}
}
