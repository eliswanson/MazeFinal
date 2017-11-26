using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CSharpMaze.QuestionsPackage
{
    class ShortAns : Ques_Ans
    {
        private GroupBox myGB;
        private Label lblQues;


        #region constructor
        public ShortAns(GroupBox gb)
        {
            this.myGB = gb;

        }
        public ShortAns()
        {

        }
        #endregion

        public override string Display()
        {
            //Display question
            Grid myGrid = myGB.Content as Grid;
            lblQues = myGrid.Children[0] as Label;
            lblQues.Content = Ques;
            myGB.Visibility = Visibility.Visible;
            return this.Final;
        }
    }
}
