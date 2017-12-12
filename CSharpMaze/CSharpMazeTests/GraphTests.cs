using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSharpMaze;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpMaze.Tests
{
    [TestClass()]
    public class GraphTests
    {
        [TestMethod()]
        public void ContainsFalseTest()
        {
            Graph<int> testGraph = new Graph<int>();            
            Assert.IsFalse(testGraph.Contains(-1));
        }
        [TestMethod()]
        public void ContainsTrueTest()
        {
            Graph<int> testGraph = new Graph<int>();
            testGraph.AddVertex(-1);
            Assert.IsTrue(testGraph.Contains(-1));
        }

        [TestMethod()]
        public void AddVertexNullTest()
        {
            Graph<Object> testGraph = new Graph<Object>();            
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                testGraph.AddVertex(null);
            });
        }
    }
}