using System.Linq;
using BioInf.Model;

namespace BioInf.Logic
{
    public static class MutationLogic
    {
        public static Result Mutate(Result item)
        {
            Result result = new Result()
            {
                SequenceIndexes = item.SequenceIndexes.ToArray()
            };

            int index1 = Global.Random.Next(result.SequenceIndexes.Length - 1);
            int index2 = Global.Random.Next(result.SequenceIndexes.Length - 1);

            int tmp = result.SequenceIndexes[index1];
            result.SequenceIndexes[index1] = result.SequenceIndexes[index2];
            result.SequenceIndexes[index2] = tmp;

            return result;
        }
    }
}