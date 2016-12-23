using System;
using System.Collections.Generic;
using System.Linq;

namespace EulerianGraphConsole
{
    public class EulerianGraph<T> where T : IEquatable<T>
    {
        private readonly HashSet<Edge<T>> edges = new HashSet<Edge<T>>();
        private readonly HashSet<T> vertices = new HashSet<T>();

        public readonly Edge<T>[] Edges;

        public bool AllEulerianTrailsAreCircuits { get; private set; }
        public bool IsSemiEulerian { get; private set; }
        public T[] Boundaries { get; private set; }

        public EulerianGraph<T> Connect(T source, params T[] targets)
        {
            foreach (var target in targets)
            {
                if (target.Equals(source) == false)
                    edges.Add(new Edge<T>(source, target));
            }

            vertices.Add(source);

            return this;
        }

        public void Done()
        {
            var boundaries = new HashSet<T>();

            foreach (var v in vertices)
            {
                var edgeCount = edges
                    .Where(x => x.A.Equals(v) || x.B.Equals(v))
                    .Count();

                if ((edgeCount & 1) == 1)
                    boundaries.Add(v);
            }

            AllEulerianTrailsAreCircuits = boundaries.Count == 0;
            IsSemiEulerian = boundaries.Count == 2;

            if (boundaries.Count == 2)
                Boundaries = boundaries.ToArray();
            else
                Boundaries = null;
        }

        public T[] Solve()
        {
            var visitedEdges = new HashSet<Edge<T>>();

            T[] trail;

            if (Boundaries != null)
                trail = CreateTrail(Boundaries[0], Boundaries[1], visitedEdges);
            else
            {
                var v = edges.First().A;
                trail = CreateTrail(v, v, visitedEdges);
            }

            while (true)
            {
                bool isOver = true;
                foreach (var vertex in trail)
                {
                    var edge = GetUnvisitedEdgeOf(vertex, visitedEdges);
                    if (edge == null)
                        continue;

                    var sub = CreateTrail(vertex, vertex, visitedEdges);
                    if (sub == null)
                        continue;

                    trail = Join(trail, sub);

                    isOver = false;
                    break;
                }

                if (isOver)
                    break;
            }

            return trail;
        }

        private T[] CreateTrail(T v1, T v2, HashSet<Edge<T>> visitedEdges)
        {
            var startVertex = v1;
            var result = new List<T>();
            result.Add(v1);

            while (true)
            {
                var edge = GetUnvisitedEdgeOf(startVertex, visitedEdges);

                if (edge == null)
                {
                    if (result.Count >= 4 && result[0].Equals(result[result.Count - 1]))
                        return result.ToArray();
                    return result.ToArray();
                }

                startVertex = GetOtherVertex(startVertex, edge);
                result.Add(startVertex);

                visitedEdges.Add(edge);

                if (result.Count >= 3 && startVertex.Equals(v2))
                    return result.ToArray();
            }
        }

        private T GetOtherVertex(T vertex, Edge<T> edge)
        {
            if (vertex.Equals(edge.A))
                return edge.B;
            return edge.A;
        }

        private Edge<T> GetUnvisitedEdgeOf(T vertex, HashSet<Edge<T>> visitedEdges)
        {
            return edges
                .Where(x => x.A.Equals(vertex) || x.B.Equals(vertex))
                .FirstOrDefault(x => visitedEdges.Contains(x) == false);
        }

        private T[] Join(T[] source, T[] toInsert)
        {
            if (toInsert.Length < 3)
                return null;

            if (toInsert[0].Equals(toInsert[toInsert.Length - 1]) == false)
                return null;

            var insertionIndex = Array.FindIndex(source, x => x.Equals(toInsert[0]));
            if (insertionIndex == -1)
                return null;

            var newArray = new T[source.Length + toInsert.Length - 1];

            Array.Copy(source, 0, newArray, 0, insertionIndex);
            Array.Copy(toInsert, 0, newArray, insertionIndex, toInsert.Length);
            Array.Copy(source, insertionIndex + 1, newArray, insertionIndex + toInsert.Length, source.Length - insertionIndex - 1);

            return newArray;
        }
    }
}
