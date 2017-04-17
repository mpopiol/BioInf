using System.Linq;
using BioInf.Model;

namespace BioInf.Logic
{
    public static class RandomSolutionLogic
    {
        public static int[] GenerateRandomSolution()
        {
            int[] result = new int[Global.Nucleotids.Count];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = i + 1;
            }
            //now shuffle
            return result.OrderBy(i => Global.Random.Next()).ToArray();
        }
    }
}