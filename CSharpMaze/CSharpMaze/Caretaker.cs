using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace CSharpMaze
{
	class Caretaker
	{
	    readonly List<Memento> saveStates = new List<Memento>();

		///<summary>
		///<para>Adds the newest save state to a list of saves</para>
		///</summary>
		public void AddMemento(Memento m)
		{
			saveStates.Add(m);
		}

		///<summary>
		///<para>returns the "latest" save state of the game</para>
		///</summary>
		public Memento GetLatestMemento ()
		{
			return saveStates.Last();	
		}

		///<summary>
		///<para>Takes saves game state of user to a text file</para>
		///</summary>
		public void SaveGame()
		{
			try
			{
				using (Stream file = File.Open("Game_Save.txt", FileMode.Create))
				{
					BinaryFormatter writeTo = new BinaryFormatter();
					writeTo.Serialize(file, GetLatestMemento());
				}
			}
			catch (FileNotFoundException e)
			{
				Console.WriteLine(e.Message);
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
			

		}
		///<summary>
		///<para>Loads game Information from text file</para>
		///</summary>
		public void LoadGame()
		{
			try
			{
				using (Stream file = File.Open("Game_Save.txt", FileMode.Open))
				{
					BinaryFormatter openFileStream = new BinaryFormatter();
					saveStates.Add((Memento)openFileStream.Deserialize(file));
				}
			}
			catch (FileNotFoundException e)
			{
				Console.WriteLine(e.Message);
			}
			catch(FileLoadException e)
			{
				Console.WriteLine(e.Message);
			}
		}
	}
}
