using System;
using System.Collections.Generic;
using System.IO;

namespace CSharpMaze
{
    [Serializable]
    public class Graph<T>
    {
        private Dictionary<T, Vertex> Maze { get; } = new Dictionary<T, Vertex>();
        /// <summary>
        /// Creates an undirected and unweighted graph that can contain vertices of an user specified type.
        /// </summary>
        /// <summary>
        /// Add a new vertex to the graph.
        /// </summary>
        /// <param name="v"></param>
        #region Vertex Connections
        public void AddVertex(T v) => Maze.Add(v, new Vertex()); // needs null check?
        /// <summary>
        /// Check to see if vertex is contained in the graph.
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public bool Contains(T v)
        {
            return Maze.ContainsKey(v);
        }
        /// <summary>
        /// Connects vertices. Does not add duplicates. Throws exception if either vertix isn't in the graph.
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        public void Connect(T v1, T v2) //return false if failed?
        {
            if (!Maze.TryGetValue(v1, out Vertex first))
                throw new NullReferenceException("v1 does not exist in the graph");

            if (!Maze.TryGetValue(v2, out Vertex second))
                throw new NullReferenceException("v2 does not exist in the graph");

            if(!first.neighbors.Contains(second))
                first.neighbors.AddLast(second);
            if(!second.neighbors.Contains(first))
                second.neighbors.AddLast(first);
        }
        /// <summary>
        /// Returns true if vertices are connected. Throws exception if either vertex isn't in the graph.
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
        /// <summary>
        /// Disconnects vertices. Returns false if they weren't connected. Throws exception either vertex isn't in the graph.
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
        }
        #endregion
        #region BFS Operations
        /// <summary>
        /// Returns true if a path is found from start to finish. Throws exception if start or finish are not in the graph.
        /// </summary>
        /// <param name="start">BFS starts at this vertex</param>
        /// <param name="finish">BFS looks for this vertex</param>
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
        [Serializable]
        private class Vertex
        {
            public readonly LinkedList<Vertex> neighbors = new LinkedList<Vertex>();
            public bool Visited { get; set; }
        }
    }       
}
