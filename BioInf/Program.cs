using BioInf.Logic;
using BioInf.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BioInf
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            InitData();

            Result[] population = new Result[200];

            //randomize solution
            for (int i = 0; i < 50; i++)
            {
                population[i] = new Result()
                {
                    sequenceIndexes = RandomSolutionLogic.GenerateRandomSolution()
                };
            }
            //mutate
            for (int j = 0; j < 10000; j++)
            {
                for (int i = 50; i < 100; i++)
                {
                    population[i] = MutationLogic.Mutate(population[Global.Random.Next(49)]);
                }
                //cross
                for (int i = 100; i < 200; i++)
                {
                    population[i] = CrossingLogic.Cross(population[Global.Random.Next(49)], population[Global.Random.Next(49)]);
                }
                //evluate
                Parallel.For(0, 200, i => 
                {
                    population[i].EvaluationPoints = EvaluationLogic.Evaluate(population[i]);
                });
                //for (int i = 0; i < 200; i++)
                //{
                //    population[i].EvaluationPoints = EvaluationLogic.Evaluate(population[i]);
                //}

                //first 50 are the best ones
                population = population.OrderBy(p => p.EvaluationPoints * -1).ToArray();
                System.Console.WriteLine(String.Format("Iteration: {0}, Max: {1}", j, population[0].EvaluationPoints));
            }
        }

        private static void InitData()
        {
            var nucleotids = File.ReadAllLines("200-40.txt");
            var nucleotidList = new List<Nucleotid>();
            foreach (var item in nucleotids)
            {
                Nucleotid nucl = new Nucleotid()
                {
                    Sequence = item
                };
                nucleotidList.Add(nucl);
            }
            Global.ErrorToleration = 2;
            Global.MaxLength = 209;
            Global.Nucleotids = nucleotidList;
        }
    }
}