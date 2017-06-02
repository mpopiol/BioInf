using BioInf.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioInf.Logic
{
    public class TournamentLogic
    {
        public static void Execute(ref Result[] input, int tournamentSize, int singleTournamentSize = 4)
        {
            var slicedInput = input.Take(tournamentSize).ToArray();

            Result[] randomOrder = slicedInput.OrderBy(s => StaticRandom.Rand()).ToArray();
            Result[] results = new Result[slicedInput.Length / singleTournamentSize];
            for (int i = 0; i < slicedInput.Length - 1; i+=singleTournamentSize)
            {
                var bestResult = randomOrder[i];
                for (int j = 1; j < singleTournamentSize; j++)
                {
                    if (randomOrder[i + j].EvaluationPoints > bestResult.EvaluationPoints || (randomOrder[i + j].EvaluationPoints == bestResult.EvaluationPoints && randomOrder[i + j].TotalLength < bestResult.TotalLength) || (randomOrder[i + j].EvaluationPoints == bestResult.EvaluationPoints && EvaluationLogic.GetWeakConnectedNucleotidIndexes(bestResult).Count > EvaluationLogic.GetWeakConnectedNucleotidIndexes(randomOrder[i + j]).Count))
                    {
                        bestResult = randomOrder[i + j];
                    }
                }
                results[i / singleTournamentSize] = bestResult;
            }

            for(int i = 0; i < slicedInput.Length / singleTournamentSize; i++)
            {
                input[i] = results[i];
            }
        }
    }
}
