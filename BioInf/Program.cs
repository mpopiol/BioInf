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
            int iterations = 1000;
            int instanceSize = 200;
            int populationSize = 50;
            //float maxMutationPercentage = 0.85f;
            float mutationPercentage = 0.6f;
            int mutationsPlusCrossing = instanceSize - populationSize;
            int mutations = (int)(mutationsPlusCrossing * mutationPercentage);

            var streamWriter = new StreamWriter("output.csv");
            var directoryInfo = new DirectoryInfo(Environment.CurrentDirectory += "\\Data");
            var textFiles = directoryInfo.GetFiles("*.txt");

            foreach (var textFile in textFiles)
            {
                InitData(textFile.FullName);

                //var streamWriter = new StreamWriter("output.txt");

                double[] results = new double[iterations];

                var dateBefore = DateTime.Now;

                for (int i = 0; i < 5; i++)
                {
                    Result[] population = new Result[instanceSize];

                    for (int j = 0; j < instanceSize; j++)
                    {
                        population[j] = RandomSolutionLogic.GenerateRandomSolution();
                    }

                    for (int j = 0; j < iterations; j++)
                    {
                        if (j  == 800)
                            population[25] = RandomSolutionLogic.GenerateGreedySolution();

                        Parallel.For(populationSize, populationSize + mutations, k =>
                        {
                            population[k] = MutationLogic.GreedMutate(population[Global.Random.Next(populationSize - 1)]);
                        });

                        Parallel.For(populationSize + mutations, instanceSize, k =>
                        {
                            population[k] = CrossingLogic.Cross(population[Global.Random.Next(populationSize - 1)], population[Global.Random.Next(populationSize - 1)]);
                        });

                        Parallel.For(0, instanceSize, k =>
                        {
                            population[k].EvaluationPoints = EvaluationLogic.Evaluate(population[k]);
                            population[k].TotalLength = EvaluationLogic.GetTotalLength(population[k]);
                        });

                        TournamentLogic.Execute(ref population, population.Length);

                        var item = population.Where(p => p.EvaluationPoints == population.Max(x => x.EvaluationPoints)).First();
                        System.Console.WriteLine(String.Format("Iteration: {0}, Max: {1}, MinLength: {2}, BadGuys: {3}", j, population.Max(p => p.EvaluationPoints), population.Min(p => p.TotalLength), EvaluationLogic.GetWeakConnectedNucleotidIndexes(item).Count));

                        results[j] += population.Max(p => p.EvaluationPoints);
                    }
                }

                TimeSpan duration = new TimeSpan((DateTime.Now - dateBefore).Ticks / 5);

                streamWriter.WriteLine(textFile.FullName);
                for (int i = 0; i < iterations; i++)
                {
                    streamWriter.Write(i.ToString() + ";");
                }
                streamWriter.WriteLine();
                for (int i = 0; i < iterations; i++)
                {
                    streamWriter.Write((results[i]/5).ToString() + ";");
                }
                streamWriter.WriteLine();
                streamWriter.WriteLine(String.Format("{0}:{1}:{2}", duration.Minutes, duration.Seconds, duration.Milliseconds));
                //streamWriter.WriteLine("************ Result ************");
                //WriteResult(streamWriter, population[0]);
            }
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

        private static void InitData(string fileName)
        {
            var nucleotids = File.ReadAllLines(fileName);
            var nucleotidList = new List<Nucleotid>();
            foreach (var item in nucleotids)
            {
                Nucleotid nucl = new Nucleotid()
                {
                    Sequence = item
                };
                nucleotidList.Add(nucl);
            }
            Global.ErrorToleration = 0;
            Global.MaxLength = 209;
            Global.Nucleotids = nucleotidList;
        }
    }
}