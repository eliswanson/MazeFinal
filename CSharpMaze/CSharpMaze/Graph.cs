using System;
using System.Collections.Generic;
using System.IO;

namespace CSharpMaze
{
    public class Graph<T> //undirected
    {
        private Dictionary<T, Vertex> Maze { get; }
        public Graph()
        {
            Maze = new Dictionary<T, Vertex>();       
        }
        public void AddVertex(T v) => Maze.Add(v, new Vertex()); // needs null check?

        public bool Contains(T v)
        {
            return Maze.ContainsKey(v);
        }

        #region Vertex Connections
        /// <summary>
        /// Connects v1 and v2. Throws exception if v1 or v2 isn't in the graph.
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        public void Connect(T v1, T v2) //return false if failed?
        {
            if (!Maze.TryGetValue(v1, out Vertex first))
                throw new NullReferenceException("v1 does not exist in the graph");

            if (!Maze.TryGetValue(v2, out Vertex second))
                throw new NullReferenceException("v2 does not exist in the graph");

            first.neighbors.AddLast(second);
            second.neighbors.AddLast(first);
            //AddEdge(first, second);
        }
        /// <summary>
        /// Returns true if v1 and v2 are connected. Throws exception if v1 or v2 isn't in the graph.
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public bool IsConnected(T v1, T v2)
        {
            if (!Maze.TryGetValue(v1, out Vertex first))
                throw new NullReferenceException("v1 does not exist in the graph");      
            
            if (!Maze.TryGetValue(v2, out Vertex second))
                throw new NullReferenceException("v2 does not exist in the graph");

            return first.neighbors.Contains(second);
        }
       /* private void AddEdge(Vertex v1, Vertex v2)
        {
            v1.neighbors.AddLast(new Edge(v1, v2));
            v2.neighbors.AddLast(new Edge(v2, v1));
        }*/
        /// <summary>
        /// Disconnects v1 and v2. Returns false if they weren't connected. Throws exception if v1 or v2 isn't in the graph.
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public bool Disconnect(T v1, T v2)
        {
            if (!Maze.TryGetValue(v1, out Vertex first))
                throw new NullReferenceException("v1 does not exist in the graph");

            if (!Maze.TryGetValue(v2, out Vertex second))
                throw new NullReferenceException("v2 does not exist in the graph");

            return (first.neighbors.Remove(second) && second.neighbors.Remove(first));

            //RemoveEdge(first, second);
        }
        #endregion
        #region BFS Operations
        /// <summary>
        /// Returns true if a path is found from start to finish. Throws exception if start or finish are not in the graph.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="finish"></param>
        /// <returns></returns>
        public bool BFS(T start, T finish)
        {
            if (!Maze.TryGetValue(start, out Vertex first))
                throw new NullReferenceException("start is not in the graph");

            if (!Maze.TryGetValue(finish, out Vertex find))
                throw new NullReferenceException("finish is not in the graph");            

            bool found = false;
            Queue<Vertex> queue = new Queue<Vertex>();
            queue.Enqueue(first);

            while (!found && queue.Count > 0)
            {
                Vertex cur = queue.Dequeue();

                foreach (Vertex n in cur.neighbors)
                {
                    if (n.Equals(find))
                    {
                        found = true;
                        break;
                    }
                    if (n.Visited) continue;
                    queue.Enqueue(n);
                    n.Visited = true;
                }
            }

            ResetVisited();

            return found;
        }
        private void ResetVisited()
        {
            foreach(Vertex v in Maze.Values)
            {
                v.Visited = false;
            }
        }
        #endregion
        /*private void RemoveEdge(Vertex v1, Vertex v2)
        {
            v1.neighbors.Remove(new Edge(v1, v2));
            v2.neighbors.Remove(new Edge(v2, v1));
        }*/
        /*class Edge
        {
            private Vertex node1;
            private Vertex node2;
            public Edge(Vertex node1, Vertex node2)
            {
                this.node1 = node1;
                this.node2 = node2;
            }
            public override bool Equals(object obj)
            {
                if (!(obj is Edge))
                    return false;

                Edge e = obj as Edge;

                if (this.node1 == e.node1 && this.node1 == e.node1)
                    return true;

                else if (this.node1 == e.node2 && this.node2 == e.node1)
                    return true;

                return false;
            }
        }*/
        class Vertex
        {
            public LinkedList<Vertex> neighbors = new LinkedList<Vertex>();

            public bool Visited { get; set; }
            //bool visited;
            //refactor bool to a property set by user? mazedriver sets property of Vertex to a bool and
            //initializes as false
            //mazedriver resets bool to false after maze algorithm runs (since it sets bool to true for rooms visited)
        }
    }       
}
