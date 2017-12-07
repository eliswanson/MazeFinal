using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpMaze
{
	class Executions
	{
		private bool canStart = true;
		private bool canLoad = true;
		private bool canNewGame = true;
		/// <summary>
		/// <para>ExecutSave property bool that sets the execution of allowing user to save or not to save</para>
		/// </summary>
		public bool ExecuteSave
		{
			get { return canStart; }
			set { canStart = value; }
		}

		/// <summary>
		/// ExecuteLoad propery bool that sets the execution to allow user to load or not
		/// </summary>
		public bool ExecuteLoad
		{
			get { return canLoad; }
			set { canLoad = value; }
		}

		/// <summary>
		/// ExecuteNewGame property bool that sets the execution toe allow user to create a new game or not
		/// </summary>
		public bool ExecuteNewGame
		{
			get { return canNewGame; }
			set { canNewGame = value; }
		}
	}
}
