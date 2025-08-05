using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchEngine
{
    public static class CosineSim
    {
        public static double CosineSimilarity(float[] a, float[] b)
        {
            if (a == null || b == null)
                throw new ArgumentNullException("Input vectors cannot be null.");
            if (a.Length != b.Length)
                throw new ArgumentException("Input vectors must have the same length.");
            double dotProduct = 0.0;
            double normA = 0.0;
            double normB = 0.0;
            for (int i = 0; i < a.Length; i++)
            {
                dotProduct += a[i] * b[i];
                normA += a[i] * a[i];
                normB += b[i] * b[i];
            }
            if (normA == 0 || normB == 0)
                return 0.0; 
            return dotProduct / (Math.Sqrt(normA) * Math.Sqrt(normB));
        }
    }
}
