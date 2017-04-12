using BioInf.Model;
using System.Linq;

namespace BioInf.Logic
{
    public static class RandomSolutionLogic
    {
        public static int[] GenerateRandomSolution()
        {
            int[] result = new int[Global.Nucleotids.FirstOrDefault().Sequence.Length];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = i;
            }
            //now shuffle
            return result.OrderBy(i => Global.Random.Next()).ToArray();
        }
    }
}