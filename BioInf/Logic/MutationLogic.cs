using BioInf.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
