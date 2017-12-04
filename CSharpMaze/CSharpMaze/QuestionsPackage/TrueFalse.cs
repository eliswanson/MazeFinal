using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CSharpMaze.QuestionsPackage
{
    class TrueFalse : Ques_Ans
    {
        private string strAns1;
        private string strAns2;
        private GroupBox myGB;
        private TextBlock txbQues;
        private RadioButton[] rdAns = new RadioButton[2];

        #region get/set
        public string Ans1
        {
            get { return this.strAns1; }
            set { this.strAns1 = value; }
        }

        public string Ans2
        {
            get { return this.strAns2; }
            set { this.strAns2 = value; }
        }

        public GroupBox MyGB
        {
            get { return myGB; }
            set { this.myGB = value; }
        }

        #endregion

        #region constructor
        public TrueFalse(GroupBox gb)
        {
            for (int i = 0; i < 2; i++)
                rdAns[i] = new RadioButton();
            this.myGB = gb;

        }
        public TrueFalse()
        {

        }
        #endregion

        public override string Display()
        {
            Grid myGrid = myGB.Content as Grid;

            //Display question
            txbQues = new TextBlock();
            txbQues = myGrid.Children[0] as TextBlock;
            txbQues.Text = Ques;

            //Display answer
            rdAns[0] = myGrid.Children[1] as RadioButton;
            rdAns[0].Content = Ans1;

            rdAns[1] = myGrid.Children[2] as RadioButton;
            rdAns[1].Content = Ans2;

            myGrid.Visibility = Visibility.Visible;

            return this.Final;
        }
    }
}
