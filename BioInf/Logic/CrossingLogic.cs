using BioInf.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioInf.Logic
{
    public static class CrossingLogic
    {
        public static Result Cross(Result item1, Result item2)
        {
            Result result = new Result()
            {
                sequenceIndexes = new int[item1.sequenceIndexes.Length]
            };

            int crossingPoint = Global.Random.Next(1, item1.sequenceIndexes.Length - 2);

            FillBeginning(ref result, item1, crossingPoint);
            FillEnding(ref result, item2, crossingPoint);

            return result;
        }

        private static void FillBeginning(ref Result result, Result itemToCross, int crossingPoint)
        {
            for (int i=0; i<crossingPoint; i++)
            {
                result.sequenceIndexes[i] = itemToCross.sequenceIndexes[i];
            }
        }

        private static void FillEnding(ref Result result, Result itemToCross, int crossingPoint)
        {
            int resultCounter = crossingPoint;
            int itemToCrossCounter = 0;
            while(resultCounter < itemToCross.sequenceIndexes.Length)
            {
                if (!result.sequenceIndexes.Take(resultCounter).Contains(itemToCross.sequenceIndexes[itemToCrossCounter]))
                {
                    result.sequenceIndexes[resultCounter++] = itemToCross.sequenceIndexes[itemToCrossCounter];
                }
                itemToCrossCounter++;
            }
        }
    }
}
