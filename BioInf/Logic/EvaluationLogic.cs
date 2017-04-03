using BioInf.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioInf.Logic
{
    public static class EvaluationLogic
    {
        public static float Evaluate(Result item)
        {
            int max = 0;
            for (int i = 0; i<item.sequenceIndexes.Length; i++)
            {
                int subResult = EvaluationLogic.HandleFromPositionToMax(item, i);
                if (subResult > max)
                    max = subResult;
            }
            return max;
        }

        private static int HandleFromPositionToMax(Result item, int startingPosition)
        {
            int position = 0;
            int nucleotidsCounter = startingPosition;
            while (nucleotidsCounter < item.sequenceIndexes.Length - 1 || position > Global.MaxLength)
            {
                position += EvaluationLogic.GetSinglePartialSum(Global.Nucleotids[item.sequenceIndexes[nucleotidsCounter]],
                    Global.Nucleotids[item.sequenceIndexes[nucleotidsCounter + 1]]);
                nucleotidsCounter++;
            }
            return nucleotidsCounter;
        }

        private static int GetSinglePartialSum(Nucleotid parentNucleotid, Nucleotid childNucleotid)
        {
            int result = 1;
            while (!childNucleotid.StartsWith(parentNucleotid.Sequence.Substring(result), Global.ErrorToleration))
                result++;

            return result;
        }
    }
}
