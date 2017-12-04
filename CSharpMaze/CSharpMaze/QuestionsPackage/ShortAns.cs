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
	class ShortAns : Ques_Ans, ISerializable
    {
        private GroupBox myGB;
        private Label lblQues;
        private TextBlock txbQues;


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
            txbQues = myGrid.Children[0] as TextBlock;
            txbQues.Text = Ques;
            myGB.Visibility = Visibility.Visible;
            return this.Final;
        }

	    public void GetObjectData(SerializationInfo info, StreamingContext context)
	    {
		    info.AddValue("lblShortQues", lblQues);
	    }

	    public ShortAns(SerializationInfo info, StreamingContext context)
	    {
		    lblQues = (Label)info.GetValue("lblShortQues", typeof(Label));
	    }
	}
}
