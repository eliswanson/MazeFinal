using Microsoft.VisualStudio.TestTools.UnitTesting;
using SUT = CSharpMaze;
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
        // MethodName_StateUnderTest_ExpectedBehavior
        // Can use nested class called MethodName to reduce repitition
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddVertex_NullVertex_ThrowArgumentNullException()
        {
            //arrange
            Graph<object> graph = new SUT.Graph<object>();

            //act and assert
            graph.AddVertex(null);
        }
        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void AddVertex_DuplicateAdded_ArgumentException()
        {
            //arrange
            var graph = new SUT.Graph<int>();
            graph.AddVertex(0);

            //act and assert
            graph.AddVertex(0);
        }
        [TestMethod()]
        public void Contains_EmptyGraph_False()
        {
            //arrange
            var graph = new SUT.Graph<int>();   
            
            //act
            var result = graph.Contains(0);

            //assert
            Assert.IsFalse(result);
        }
        [TestMethod()]
        public void Contains_InputNotInGraph_False()
        {
            //arrange
            var graph = new SUT.Graph<int>();
            graph.AddVertex(1);

            //act
            var result = graph.Contains(0);

            //and assert
            Assert.IsFalse(result);
        }
        [TestMethod()]
        public void Contains_InputIsInGraph_True()
        {
            //arrange
            var graph = new SUT.Graph<int>();
            graph.AddVertex(0);

            //act
            var result = graph.Contains(0);

            //and assert
            Assert.IsTrue(result);
        }
        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void IsConnected_Vertex1AndVertex2NotInGraph_ArgumentOutOfRangeException()
        {
            //arrange
            var graph = new SUT.Graph<int>();

            //act and assert
            var result = graph.IsConnected(0, 2);
        }
        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void IsConnected_Vertex1NotInGraphButVertex2Is_ArgumentOutOfRangeException()
        {
            //arrange
            var graph = new SUT.Graph<int>();
            graph.AddVertex(2);

            //act and assert
            var result = graph.IsConnected(0, 2);
        }
        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void IsConnected_Vertex1IsInGraphButVertext2IsNot_ArgumentOutOfRangeException()
        {
            //arrange
            var graph = new SUT.Graph<int>();
            graph.AddVertex(0);

            //act and assert
            var result = graph.IsConnected(0, 2);
        }
        [TestMethod()]
        public void IsConnected_VerticesInGraphButNeverConnected_False()
        {
            //arrange
            var graph = new SUT.Graph<int>();
            graph.AddVertex(0);
            graph.AddVertex(2);

            //act
            var result = graph.IsConnected(0, 2);

            //assert
            Assert.IsFalse(result);
        }
        [TestMethod()]
        public void IsConnected_VerticesConnected_True()
        {
            //arrange
            var graph = new SUT.Graph<int>();
            graph.AddVertex(0);
            graph.AddVertex(2);

            //act
            graph.Connect(0, 2);
            var resultOrder1 = graph.IsConnected(0, 2);
            var resultOrder2 = graph.IsConnected(2, 0);

            //assert
            Assert.IsTrue(resultOrder1);
            Assert.IsTrue(resultOrder2);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Connect_Vertex1NotInGraph_ArgumentOutOfRangeException()
        {
            //arrange
            var graph = new SUT.Graph<int>();
            graph.AddVertex(2);

            //act and assert
            graph.Connect(1, 2);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Connect_Vertex2NotInGraph_ArgumentOutOfRangeException()
        {
            //arrange
            var graph = new SUT.Graph<int>();
            graph.AddVertex(1);

            //act and assert
            graph.Connect(1, 2);
        }
        [TestMethod()]
        public void Connect_Vertex1AndVertex2Connected_True()
        {
            //arrange
            var graph = new SUT.Graph<int>();
            graph.AddVertex(1);
            graph.AddVertex(2);

            //act
            graph.Connect(1, 2);
            var result = graph.IsConnected(1, 2);

            //assert
            Assert.IsTrue(result);
        }

        /*
        [TestMethod()]
        public void Method_StateUnderTest_ExpectedBehavior()
        {
            //arrange

            //act

            //assert
            Assert.IsTrue(false);
        }
        */
    }
}