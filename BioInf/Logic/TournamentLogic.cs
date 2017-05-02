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
        public static void Execute(ref Result[] input, int singleTournamentSize = 4)
        {
            Result[] randomOrder = input.OrderBy(s => Global.Random.Next()).ToArray();
            Result[] results = new Result[input.Length / singleTournamentSize];
            for (int i = 0; i < input.Length - 1; i+=singleTournamentSize)
            {
                var bestResult = randomOrder[i];
                for (int j = 1; j < singleTournamentSize; j++)
                {
                    if (randomOrder[i + j].EvaluationPoints > bestResult.EvaluationPoints || (randomOrder[i + j].EvaluationPoints == bestResult.EvaluationPoints && randomOrder[i + j].TotalLength < bestResult.TotalLength))
                    {
                        bestResult = randomOrder[i + j];
                    }
                }
                results[i / singleTournamentSize] = bestResult;
            }

            for(int i = 0; i < input.Length / singleTournamentSize; i++)
            {
                input[i] = results[i];
            }
        }
    }
}
