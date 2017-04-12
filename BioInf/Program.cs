using BioInf.Logic;
using BioInf.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioInf
{
    class Program
    {
        static void Main(string[] args)
        {
            //get input data and set parameters
            
            Result[] population = new Result[200];

            //randomize solution
            for (int i=0; i<50; i++)
            {
                population[i] = new Result()
                {
                    sequenceIndexes = RandomSolutionLogic.GenerateRandomSolution()
                };
            }
            //mutate
            for (int i = 50; i<100; i++)
            {
                population[i] = MutationLogic.Mutate(population[Global.Random.Next(49)]);
            }
            //cross
            for (int i = 100; i < 200; i++)
            {
                population[i] = CrossingLogic.Cross(population[Global.Random.Next(49)], population[Global.Random.Next(49)]);
            }
            //evluate
            for (int i=0; i<200; i++)
            {
                population[i].EvaluationPoints = EvaluationLogic.Evaluate(population[i]);
            }

            //first 50 are the best ones
            population = population.OrderBy(p => p.EvaluationPoints).ToArray();
        }
    }
}
