using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CSharpMaze.QuestionsPackage
{
	[Serializable]
	class ShortAns : Ques_Ans
    {
		#region constructor

        public ShortAns(GroupBox gb) : base(gb) { } 
        #endregion
        public override string Display()
        {
            //Display question
            Grid myGrid = MyBox.Content as Grid;
            TextBlock txbQues = myGrid.Children[0] as TextBlock;
            txbQues.Text = Ques;
            MyBox.Visibility = Visibility.Visible;
            return this.Final;
        }
	}
}
