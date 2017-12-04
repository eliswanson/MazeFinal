using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpMaze
{
	class Executions
	{
		bool canStart = true;
		/// <summary>
		/// <para>ExecutSave property bool that sets the execution of allowing user to save or not to save</para>
		/// </summary>
		public bool ExecuteSave
		{
			get { return canStart; }
			set { canStart = value; }
		}
		
	}
}
