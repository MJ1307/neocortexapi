using NeoCortexApi;
using NeoCortexApi.Encoders;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using static MultiSequenceLearning.MultiSequenceLearning;

namespace MultiSequenceLearning
{
    class Program
    {
        /// <summary>
        /// This sample shows a typical experiment code for SP and TM.
        /// You must start this code in debugger to follow the trace.
        /// and TM.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            // SE Project: ML22/23-15	Approve Prediction of Multisequence Learning 

            //to create synthetic dataset
            /*string path = HelperMethods.SaveDataset(HelperMethods.CreateDataset());
            Console.WriteLine($"Dataset saved: {path}");*/

            //to read dataset
            string BasePath = AppDomain.CurrentDomain.BaseDirectory;
            string datasetPath = Path.Combine(BasePath, "dataset", "dataset_03.json");
            Console.WriteLine($"Reading Dataset: {datasetPath}");
            List<Sequence> sequences = HelperMethods.ReadDataset(datasetPath);

            string testsetPath = Path.Combine(BasePath, "dataset", "test_01.json");
            Console.WriteLine($"Reading Testset: {testsetPath}");
            List<Sequence> sequencesTest = HelperMethods.ReadDataset(testsetPath);


            //RunSimpleMultiSequenceLearningExperiment(sequences);
            RunMultiSequenceLearningExperiment(sequences, sequencesTest);

            Console.WriteLine("Done...");

        }

        private static void RunSimpleMultiSequenceLearningExperiment(List<Sequence> sequences)
        {
            /*List<Sequence> sequences = new List<Sequence>();
            Sequence S1 = new Sequence();
            S1.name = "S1";
            S1.data = new int[] { 1, 2, 3, 4, 5, 6, 7 };
            Sequence S2 = new Sequence();
            S1.name = "S2";
            S1.data = new int[] { 0, 2, 3, 4, 6, 7, 8 };

            sequences.Add(S1);
            sequences.Add(S2);*/

            //
            // Prototype for building the prediction engine.
            MultiSequenceLearning experiment = new MultiSequenceLearning();
            var predictor = experiment.Run(sequences);
        }


        /// <summary>
        /// This example demonstrates how to learn two sequences and how to use the prediction mechanism.
        /// First, two sequences are learned.
        /// Second, three short sequences with three elements each are created und used for prediction. The predictor used by experiment privides to the HTM every element of every predicting sequence.
        /// The predictor tries to predict the next element.
        /// </summary>
        private static void RunMultiSequenceLearningExperiment(List<Sequence> sequences, List<Sequence> sequencesTest)
        {
            // Prototype for building the prediction engine.
            MultiSequenceLearning experiment = new MultiSequenceLearning();
            var predictor = experiment.Run(sequences);

            // These list are used to see how the prediction works.
            // Predictor is traversing the list element by element. 
            // By providing more elements to the prediction, the predictor delivers more precise result.

            foreach (Sequence item in sequencesTest)
            {
                Console.WriteLine($"Using test sequence: {item.name}");
                predictor.Reset();
                PredictNextElement(predictor, item.data);
            }

        }

        private static void PredictNextElement(Predictor predictor, int[] list)
        {
            Console.WriteLine("------------------------------");

            foreach (var item in list)
            {
                Console.WriteLine($"Input: {item}");
                var res = predictor.Predict(item);

                if (res.Count > 0)
                {
                    foreach (var pred in res)
                    {
                        Console.WriteLine($"Predicted Input: {pred.PredictedInput} - Similarity: {pred.Similarity}");
                    }

                    //needs understanding
                    var sequence = res.First().PredictedInput.Split('_');
                    var prediction = res.First().PredictedInput.Split('-');
                    Console.WriteLine($"Predicted Sequence: {sequence.First()}, predicted next element: {prediction.Last()}");
                }
                else
                    Console.WriteLine("Nothing predicted :(");
            }

            Console.WriteLine("------------------------------");
        }
    }
}
