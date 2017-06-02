using BioInf.Logic;
using BioInf.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BioInf
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            int iterations = 10000;
            int repetitionsPerFile = 3;

            int instanceSize = 200;
            int populationSize = 50;
            //float maxMutationPercentage = 0.85f;
            float mutationPercentage = 0.5f;
            int mutationsPlusCrossing = instanceSize - populationSize;
            int mutations = (int)(mutationsPlusCrossing * mutationPercentage);

            var streamWriter = new StreamWriter("output.csv");
            var directoryInfo = new DirectoryInfo(Environment.CurrentDirectory += "\\Data");
            var textFiles = directoryInfo.GetFiles("*.txt");

            int fileCount = 0;
            foreach (var textFile in textFiles)
            {
                Regex rgx = new Regex("^([0-9]{1,2}).([0-9]{3})");
                MatchCollection matches = rgx.Matches(textFile.Name);
                int windowLength = int.Parse(matches[0].Groups[2].ToString()) + 9;

                InitData(textFile.FullName, windowLength);

                //var streamWriter = new StreamWriter("output.txt");

                double[] results = new double[iterations];

                var dateBefore = DateTime.Now;

                for (int i = 0; i < repetitionsPerFile; i++)
                {
                    Result[] population = new Result[instanceSize];

                    for (int j = 0; j < instanceSize; j++)
                    {
                        population[j] = RandomSolutionLogic.GenerateRandomSolution();
                    }

                    for (int j = 0; j < iterations; j++)
                    {
                        //if (j  == 800)
                        //    population[25] = RandomSolutionLogic.GenerateGreedySolution();

                        Parallel.For(populationSize, populationSize + mutations, k =>
                        {
                            population[k] = MutationLogic.GreedMutate(population[StaticRandom.Rand(populationSize - 1)]);
                        });

                        Parallel.For(populationSize + mutations, instanceSize, k =>
                        {
                            if (StaticRandom.Rand() % 2 == 0)
                                population[k] = CrossingLogic.Cross(population[StaticRandom.Rand(populationSize - 1)], population[StaticRandom.Rand(populationSize - 1)]);
                            else
                                population[k] = CrossingLogic.Cross2Points(population[StaticRandom.Rand(populationSize - 1)], population[StaticRandom.Rand(populationSize - 1)]);
                        });

                        Parallel.For(0, instanceSize, k =>
                        {
                            population[k].EvaluationPoints = EvaluationLogic.Evaluate(population[k]);
                            population[k].TotalLength = EvaluationLogic.GetTotalLength(population[k]);
                        });

                        TournamentLogic.Execute(ref population, population.Length);

                        var item = population.Where(p => p.EvaluationPoints == population.Max(x => x.EvaluationPoints)).First();
                        System.Console.WriteLine(String.Format("File {0}/{1} Iteration: {2}, Max: {3}/{4}, MinLength: {5}, BadGuys: {6}", fileCount, textFiles.Count(), j, population.Max(p => p.EvaluationPoints), Global.MaxLength, population.Min(p => p.TotalLength), EvaluationLogic.GetWeakConnectedNucleotidIndexes(item).Count));

                        results[j] += population.Max(p => p.EvaluationPoints);
                    }
                }

                fileCount++;

                TimeSpan duration = new TimeSpan((DateTime.Now - dateBefore).Ticks / 5);

                streamWriter.WriteLine(textFile.FullName);
                for (int i = 0; i < iterations; i++)
                {
                    streamWriter.Write(i.ToString() + ";");
                }
                streamWriter.WriteLine();
                for (int i = 0; i < iterations; i++)
                {
                    streamWriter.Write((results[i] / repetitionsPerFile).ToString() + ";");
                }
                streamWriter.WriteLine();
                streamWriter.WriteLine(String.Format("{0}:{1}:{2}", duration.Minutes, duration.Seconds, duration.Milliseconds));
                streamWriter.Flush();
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

        private static void InitData(string fileName, int windowLength)
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
            Global.MaxLength = windowLength;
            Global.Nucleotids = nucleotidList;
        }
    }
}