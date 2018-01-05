using System;
using System.Collections.Generic;
using System.IO;

namespace CSharpMaze
{
    [Serializable]
    public class Graph<T>
    {
        #region Fields/Constructors
        private Dictionary<T, Vertex> mazeDictionary { get; } = new Dictionary<T, Vertex>();

        /// <summary>
        /// Creates an undirected and unweighted graph that can contain vertices of an user specified type. Does not allow duplicate vertices.
        /// </summary>
        public Graph()
        {
        }
        #endregion
        #region Vertex Connections
        /// <summary>
        /// Add a new vertex to the graph. Duplicates not allowed, thows an exception.
        /// </summary>
        /// <param name="v"></param>
        public void AddVertex(T v)
        {
            if(v == null)
                throw new ArgumentNullException();

            if (mazeDictionary.ContainsKey(v))
                throw new ArgumentException("Duplicate vertices not allowed; vertices must be unique");

            mazeDictionary.Add(v, new Vertex());
        }

        /// <summary>
        /// Check to see if vertex is contained in the graph.
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public bool Contains(T v)
        {
            return mazeDictionary.ContainsKey(v);
        }
        /// <summary>
        /// Connects vertices. Returns true if succesful, false if failed. Does not add duplicate connections. Throws exception if either vertix isn't in the graph.
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        public void Connect(T v1, T v2) //return false if failed?
        {
            if (!mazeDictionary.TryGetValue(v1, out Vertex firstVertex))
                throw new ArgumentOutOfRangeException("v1 does not exist in the graph");

            if (!mazeDictionary.TryGetValue(v2, out Vertex secondVertex))
                throw new ArgumentOutOfRangeException("v2 does not exist in the graph");

            if (!firstVertex.neighborsList.Contains(secondVertex))
                firstVertex.neighborsList.AddLast(secondVertex);

            if (!secondVertex.neighborsList.Contains(firstVertex))
                secondVertex.neighborsList.AddLast(firstVertex);

            if (!IsConnected(v1, v2))
                throw new Exception("v1 was not succesfully connected to v2; this should never happen");
        }
        /// <summary>
        /// Returns true if vertices are connected. Throws exception if either vertex isn't in the graph.
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public bool IsConnected(T v1, T v2)
        {
            if (!mazeDictionary.TryGetValue(v1, out Vertex firstVertex))
                throw new ArgumentOutOfRangeException("v1 does not exist in the graph");      
            
            if (!mazeDictionary.TryGetValue(v2, out Vertex secondVertex))
                throw new ArgumentOutOfRangeException("v2 does not exist in the graph");

            return firstVertex.neighborsList.Contains(secondVertex) && secondVertex.neighborsList.Contains(firstVertex);
        }
        /// <summary>
        /// Disconnects vertices. Returns false if they weren't connected. Throws exception either vertex isn't in the graph.
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public bool Disconnect(T v1, T v2)
        {
            if (!mazeDictionary.TryGetValue(v1, out Vertex firstVertex))
                throw new NullReferenceException("v1 does not exist in the graph");

            if (!mazeDictionary.TryGetValue(v2, out Vertex secondVertex))
                throw new NullReferenceException("v2 does not exist in the graph");

            return (firstVertex.neighborsList.Remove(secondVertex) && secondVertex.neighborsList.Remove(firstVertex));
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
            if (!mazeDictionary.TryGetValue(start, out Vertex firstVertex))
                throw new NullReferenceException("start is not in the graph");

            if (!mazeDictionary.TryGetValue(finish, out Vertex findVertex))
                throw new NullReferenceException("finish is not in the graph");            

            bool found = false;
            Queue<Vertex> queue = new Queue<Vertex>();
            queue.Enqueue(firstVertex);

            while (!found && queue.Count > 0)
            {
                Vertex cur = queue.Dequeue();

                foreach (Vertex v in cur.neighborsList)
                {
                    if (v.Equals(findVertex))
                    {
                        found = true;
                        break;
                    }
                    if (v.Visited) continue;
                    queue.Enqueue(v);
                    v.Visited = true;
                }
            }

            ResetVisited();

            return found;
        }
        private void ResetVisited()
        {
            foreach(Vertex v in mazeDictionary.Values)
            {
                v.Visited = false;
            }
        }
        #endregion       
        [Serializable]
        private class Vertex
        {
            public readonly LinkedList<Vertex> neighborsList = new LinkedList<Vertex>();
            public bool Visited { get; set; }
        }
    }       
}
