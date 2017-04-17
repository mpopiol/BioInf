﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BioInf.Logic;
using BioInf.Model;

namespace BioInf
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            InitData();

            int instanceSize = 200;
            int populationSize = 50;
            float maxMutationPercentage = 0.85f;
            float mutationPercentage = 0.1f;
            int mutationsPlusCrossing = instanceSize - populationSize;

            var streamWriter = new StreamWriter("output.txt");

            Result[] population = new Result[instanceSize];

            for (int i = 0; i < populationSize; i++)
            {
                population[i] = new Result()
                {
                    SequenceIndexes = RandomSolutionLogic.GenerateRandomSolution()
                };
            }

            int j = 0;
            while (!(Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Escape))
            {
                j++;

                if (j % 100 == 0 && mutationPercentage < maxMutationPercentage)
                    mutationPercentage += 0.01f;

                int mutations = (int)(mutationsPlusCrossing * mutationPercentage);

                Parallel.For(populationSize, populationSize + mutations, i =>
                {
                    population[i] = MutationLogic.Mutate(population[Global.Random.Next(populationSize - 1)]);
                    //for (int k = 0; k < (int)(j / 500); k++)
                    //{
                    //    population[i] = MutationLogic.Mutate(population[i]);
                    //}
                });

                Parallel.For(populationSize + mutations, instanceSize, i =>
                {
                    population[i] = CrossingLogic.Cross(population[Global.Random.Next(populationSize - 1)], population[Global.Random.Next(populationSize - 1)]);
                    //for (int k = 0; k < (int)(j / 1500); k++)
                    //{
                    //    population[i] = CrossingLogic.Cross(population[i], population[Global.Random.Next(49)]);
                    //}
                });

                Parallel.For(0, instanceSize, i =>
                {
                    population[i].EvaluationPoints = EvaluationLogic.Evaluate(population[i]);
                    population[i].TotalLength = EvaluationLogic.GetTotalLength(population[i]);
                });

                //if (population[0].sequenceIndexes.Length != population[0].sequenceIndexes.Distinct().Count())
                //{
                //    System.Console.WriteLine("DUPLICATE!");
                //}

                population = population.OrderBy(p => p.EvaluationPoints * -1).ThenBy(p => p.TotalLength).ToArray();
                System.Console.WriteLine(String.Format("Iteration: {0}, Max: {1}, MinLength: {2}", j, population.Max(p => p.EvaluationPoints), population.Min(p => p.TotalLength)));

                //streamWriter.WriteLine(String.Format("{0};{1}", j, population[0].EvaluationPoints)); //for plot
            }
            streamWriter.WriteLine("");
            streamWriter.WriteLine("");
            streamWriter.WriteLine("");
            streamWriter.WriteLine("");

            streamWriter.WriteLine("************ Result ************");
            WriteResult(streamWriter, population[0]);

            streamWriter.Close();
        }

        private static void WriteResult(StreamWriter streamWriter, Result result)
        {
            int offset = 0;
            streamWriter.WriteLine(Global.Nucleotids[result.SequenceIndexes[0] - 1].Sequence);
            for (int i = 0; i < result.SequenceIndexes.Length - 1; i++)
            {
                offset += EvaluationLogic.GetSinglePartialSum(Global.Nucleotids[result.SequenceIndexes[i] - 1], Global.Nucleotids[result.SequenceIndexes[i + 1] - 1]);
                for (int j = 0; j < offset; j++)
                {
                    streamWriter.Write(" ");
                }
                streamWriter.Write(Global.Nucleotids[result.SequenceIndexes[i + 1] - 1].Sequence + "\r\n");
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