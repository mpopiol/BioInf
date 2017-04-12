using BioInf.Model;
using System.Linq;

namespace BioInf.Logic
{
    public static class MutationLogic
    {
        public static Result Mutate(Result item)
        {
            Result result = new Result()
            {
                sequenceIndexes = item.sequenceIndexes.ToArray()
            };

            int index1 = Global.Random.Next(result.sequenceIndexes.Length - 1);
            int index2 = Global.Random.Next(result.sequenceIndexes.Length - 1);

            int tmp = result.sequenceIndexes[index1];
            result.sequenceIndexes[index1] = result.sequenceIndexes[index2];
            result.sequenceIndexes[index2] = tmp;

            return result;
        }
    }
}