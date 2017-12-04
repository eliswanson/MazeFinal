using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpMaze.QuestionsPackage
{
	[Serializable]
	abstract class Ques_Ans
    {
        protected string strQues;
        protected string strFinal;

        public string Ques
        {
            get { return this.strQues; }
            set { this.strQues = value; }
        }

        public string Final
        {
            get { return this.strFinal; }
            set { this.strFinal = value; }
        }
        public abstract string Display();
    }
}
