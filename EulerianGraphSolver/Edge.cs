using System;

namespace EulerianGraphConsole
{
    public class Edge<T> where T : IEquatable<T>
    {
        public readonly T A;
        public readonly T B;

        public Edge(T a, T b)
        {
            A = a;
            B = b;
        }

        public override int GetHashCode() => A.GetHashCode() ^ B.GetHashCode();

        public override bool Equals(object obj)
        {
            var other = obj as Edge<T>;

            if (other == null)
                return false;

            return A.Equals(other.A) && B.Equals(other.B) || A.Equals(other.B) && B.Equals(other.A);
        }

        public override string ToString() => $"{A} -> {B}";
    }
}
