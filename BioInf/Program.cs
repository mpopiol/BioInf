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

            for (int i = 0; i < 50; i++)
            {
                population[i] = new Result()
                {
                    sequenceIndexes = RandomSolutionLogic.GenerateRandomSolution()
                };
            }

            for (int j = 1; j < 10000; j++)
            {
                Parallel.For(50, 100, i =>
                {
                    population[i] = MutationLogic.Mutate(population[Global.Random.Next(49)]);
                    for (int k = 0; k < (int)(j / 100); k++)
                    {
                        population[i] = MutationLogic.Mutate(population[i]);
                    }
                });

                Parallel.For(100, 200, i =>
                {
                    population[i] = CrossingLogic.Cross(population[Global.Random.Next(49)], population[Global.Random.Next(49)]);
                    for (int k = 0; k < (int)(j / 100); k++)
                    {
                        population[i] = CrossingLogic.Cross(population[i], population[Global.Random.Next(49)]);
                    }
                });

                Parallel.For(0, 200, i =>
                {
                    population[i].EvaluationPoints = EvaluationLogic.Evaluate(population[i]);
                });

                population = population.OrderBy(p => p.EvaluationPoints * -1).ToArray();
                System.Console.WriteLine(String.Format("Iteration: {0}, Max: {1}", j, population[0].EvaluationPoints));
            }
        }

        private static void InitData()
        {
            var nucleotids = File.ReadAllLines("Data/200+80.txt");
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