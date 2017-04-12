using BioInf.Logic;
using BioInf.Model;
using NUnit.Framework;
using System.Collections.Generic;

namespace BioInf.Test.Logic
{
    [TestFixture]
    public class EvaluationLogicTest
    {
        [Test]
        public void Evaluation_BasicExample()
        {
            Global.Nucleotids = new List<Nucleotid>()
            {
                new Nucleotid()
                {
                    Sequence = "ATTTT"
                },
                new Nucleotid()
                {
                    Sequence = "AATTT"
                },
                new Nucleotid()
                {
                    Sequence = "AAATT"
                },
                new Nucleotid()
                {
                    Sequence = "AAAAT"
                }
            };

            Global.MaxLength = 8;
            Global.ErrorToleration = 0;
            Result res = new Result()
            {
                sequenceIndexes = new int[] { 4, 3, 2, 1 }
            };
            var result = EvaluationLogic.Evaluate(res);
            Assert.AreEqual(4, result);
        }

        [Test]
        public void Evaluation_BasicExample2()
        {
            Global.Nucleotids = new List<Nucleotid>()
            {
                new Nucleotid()
                {
                    Sequence = "ATTTT"
                },
                new Nucleotid()
                {
                    Sequence = "AATTT"
                },
                new Nucleotid()
                {
                    Sequence = "AAATT"
                },
                new Nucleotid()
                {
                    Sequence = "AAAAT"
                }
            };

            Global.MaxLength = 7;
            Global.ErrorToleration = 0;
            Result res = new Result()
            {
                sequenceIndexes = new int[] { 4, 3, 2, 1 }
            };
            var result = EvaluationLogic.Evaluate(res);
            Assert.AreEqual(3, result);
        }

        [Test]
        public void Evaluation_BasicExample3()
        {
            Global.Nucleotids = new List<Nucleotid>()
            {
                new Nucleotid()
                {
                    Sequence = "ATTTT"
                },
                new Nucleotid()
                {
                    Sequence = "AATTT"
                },
                new Nucleotid()
                {
                    Sequence = "AAATT"
                },
                new Nucleotid()
                {
                    Sequence = "AAAAT"
                }
            };

            Global.MaxLength = 6;
            Global.ErrorToleration = 0;
            Result res = new Result()
            {
                sequenceIndexes = new int[] { 4, 3, 2, 1 }
            };
            var result = EvaluationLogic.Evaluate(res);
            Assert.AreEqual(2, result);
        }

        [Test]
        public void Evaluation_BasicExample4()
        {
            Global.Nucleotids = new List<Nucleotid>()
            {
                new Nucleotid()
                {
                    Sequence = "GGTTT"
                },
                new Nucleotid()
                {
                    Sequence = "TTAAC"
                },
                new Nucleotid()
                {
                    Sequence = "TAACG"
                },
                new Nucleotid()
                {
                    Sequence = "GATCA"
                },
                new Nucleotid()
                {
                    Sequence = "CAGGG"
                }
            };

            Global.MaxLength = 9;
            Global.ErrorToleration = 0;
            Result res = new Result()
            {
                sequenceIndexes = new int[] { 4, 5, 1, 2, 3 }
            };
            var result = EvaluationLogic.Evaluate(res);
            Assert.AreEqual(3, result);
        }

        [Test]
        public void Evaluation_BasicExample5()
        {
            Global.Nucleotids = new List<Nucleotid>()
            {
                new Nucleotid()
                {
                    Sequence = "GGTTT"
                },
                new Nucleotid()
                {
                    Sequence = "TTAAC"
                },
                new Nucleotid()
                {
                    Sequence = "TAACG"
                },
                new Nucleotid()
                {
                    Sequence = "GATCA"
                },
                new Nucleotid()
                {
                    Sequence = "CAGGG"
                }
            };

            Global.MaxLength = 8;
            Global.ErrorToleration = 0;
            Result res = new Result()
            {
                sequenceIndexes = new int[] { 3, 4, 5, 1, 2 }
            };
            var result = EvaluationLogic.Evaluate(res);
            Assert.AreEqual(2, result);
        }

        [Test]
        public void Evaluation_BasicExample6()
        {
            Global.Nucleotids = new List<Nucleotid>()
            {
                new Nucleotid()
                {
                    Sequence = "QWER"
                },
                new Nucleotid()
                {
                    Sequence = "TYUI"
                },
                new Nucleotid()
                {
                    Sequence = "ASDF"
                },
                new Nucleotid()
                {
                    Sequence = "ZXCV"
                }
            };

            Global.MaxLength = 8;
            Global.ErrorToleration = 0;
            Result res = new Result()
            {
                sequenceIndexes = new int[] { 1, 2, 4, 3 }
            };
            var result = EvaluationLogic.Evaluate(res);
            Assert.AreEqual(2, result);

            Global.MaxLength = 9;
            Global.ErrorToleration = 0;
            res = new Result()
            {
                sequenceIndexes = new int[] { 1, 2, 4, 3 }
            };
            result = EvaluationLogic.Evaluate(res);
            Assert.AreEqual(2, result);

            Global.MaxLength = 7;
            Global.ErrorToleration = 0;
            res = new Result()
            {
                sequenceIndexes = new int[] { 1, 2, 4, 3 }
            };
            result = EvaluationLogic.Evaluate(res);
            Assert.AreEqual(1, result);
        }

        #region DELTA

        [Test]
        public void Evaluation_BasicExample_WithDelta()
        {
            Global.Nucleotids = new List<Nucleotid>()
            {
                new Nucleotid()
                {
                    Sequence = "GCATT"
                },
                new Nucleotid()
                {
                    Sequence = "ATTGC"
                },
                new Nucleotid()
                {
                    Sequence = "TGGCC"
                },
                new Nucleotid()
                {
                    Sequence = "GGCCA"
                },
                new Nucleotid()
                {
                    Sequence = "GCCAA"
                },
                new Nucleotid()
                {
                    Sequence = "CAATT"
                },
                new Nucleotid()
                {
                    Sequence = "ATTTG"
                }
            };

            Global.MaxLength = 8;
            Global.ErrorToleration = 1;
            Result res = new Result()
            {
                sequenceIndexes = new int[] { 1, 2, 3, 4, 5, 6, 7 }
            };
            var result = EvaluationLogic.Evaluate(res);
            Assert.AreEqual(4, result);
        }

        [Test]
        public void Evaluation_BasicExample_WithDelta2()
        {
            Global.Nucleotids = new List<Nucleotid>()
            {
                new Nucleotid()
                {
                    Sequence = "GCATT"
                },
                new Nucleotid()
                {
                    Sequence = "ATTGC"
                },
                new Nucleotid()
                {
                    Sequence = "TGGCC"
                },
                new Nucleotid()
                {
                    Sequence = "TGCCA"
                },
                new Nucleotid()
                {
                    Sequence = "GCCAA"
                },
                new Nucleotid()
                {
                    Sequence = "CAATT"
                },
                new Nucleotid()
                {
                    Sequence = "ATTGG"
                }
            };

            Global.MaxLength = 8;
            Global.ErrorToleration = 1;
            Result res = new Result()
            {
                sequenceIndexes = new int[] { 1, 2, 3, 4, 5, 6, 7 }
            };
            var result = EvaluationLogic.Evaluate(res);
            Assert.AreEqual(3, result);

            Global.MaxLength = 9;
            Global.ErrorToleration = 1;
            res = new Result()
            {
                sequenceIndexes = new int[] { 1, 2, 3, 4, 5, 6, 7 }
            };
            result = EvaluationLogic.Evaluate(res);
            Assert.AreEqual(3, result);
        }

        #endregion DELTA
    }
}