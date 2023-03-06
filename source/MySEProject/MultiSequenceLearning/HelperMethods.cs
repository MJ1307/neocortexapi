using System;
using System.Globalization;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeoCortexApi;
using NeoCortexApi.Encoders;
using NeoCortexApi.Entities;
using MultiSequenceLearning;

namespace MultiSequenceLearning
{
    public class HelperMethods
    {
        public HelperMethods()
        {
            //needs no implementation
        }

        /// <summary>
        /// Reads CSV file and pre-processes the data and returns it into List of Dictionary
        /// </summary>
        /// <param name="csvFilePath">CSV file</param>
        /// <returns></returns>
        public static List<Dictionary<string, string>> ReadDataFromCSV(string csvFilePath, string sequenceFormat)
        {
            List<Dictionary<string, string>> sequencesCollection = new List<Dictionary<string, string>>();

            int keyForUniqueIndexes = 0;

            int count = 0, maxCount = 0;

            bool firstTime = true;

            if (File.Exists(csvFilePath))
            {
                using (StreamReader reader = new StreamReader(csvFilePath))
                {

                }

                return sequencesCollection;
            }

            return null;
        }

        /// <summary>
        /// HTM Config for creating Connections
        /// </summary>
        /// <param name="inputBits"></param>
        /// <param name="numColumns"></param>
        /// <returns></returns>
        public static HtmConfig FetchHTMConfig(int inputBits, int numColumns)
        {
            HtmConfig cfg = new HtmConfig(new int[] { inputBits }, new int[] { numColumns })
            {
                Random = new ThreadSafeRandom(42),

                CellsPerColumn = 25,
                GlobalInhibition = true,
                LocalAreaDensity = -1,
                NumActiveColumnsPerInhArea = 0.02 * numColumns,
                PotentialRadius = (int)(0.15 * inputBits),
                //InhibitionRadius = 15,

                MaxBoost = 10.0,
                DutyCyclePeriod = 25,
                MinPctOverlapDutyCycles = 0.75,
                MaxSynapsesPerSegment = (int)(0.02 * numColumns),

                ActivationThreshold = 15,
                ConnectedPermanence = 0.5,

                // Learning is slower than forgetting in this case.
                PermanenceDecrement = 0.25,
                PermanenceIncrement = 0.15,

                // Used by punishing of segments.
                PredictedSegmentDecrement = 0.1,

                //NumInputs = 88
            };

            return cfg;
        }

        /// <summary>
        /// Takes in user input and return encoded SDR for prediction
        /// </summary>
        /// <param name="userInput"></param>
        /// <returns></returns>
        public static int[] EncodeSingleInput(string userInput)
        {
            int[] sdr = new int[0];

            //needs implementation

            return sdr;
        }

        //creating synthetic dataset
        public static List<Sequence> CreateDataset(int count, int size, int startVal, int stopVal)
        {
            List<Sequence> dataset = new List<Sequence>();

            for (int i = 0; i < count; i++)
            {
                Sequence sequence = new Sequence();
                sequence.name = $"S{i}";
                sequence.data = getSyntheticData(size, startVal, stopVal);

            }

            return dataset;
        }

        private static double[] getSyntheticData(int size, int startVal, int stopVal)
        {
            double[] data = new double[size];

            data = randomRemoveDouble(randomDouble(size, startVal, stopVal), getDigits(size) * 10);

            return data;
        }

        private static double[] randomDouble(int size, int startVal, int stopVal)
        {
            double[] array = new double[size];
            int digit = getDigits(size);
            List<double> list = new List<double>();
            double number = 0;
            Random r = new Random(Guid.NewGuid().GetHashCode());
            while(list.Count < size)
            {
                number = r.NextDouble() / Math.Pow(10, digit);
                if (!list.Contains(number)) 
                    list.Add(number);
            }

            array = list.ToArray();
            Array.Sort(array);

            return array;            
        }

        private static double[] randomRemoveDouble(double[] array, int less)
        {
            double[] temp = new double[array.Length - less];
            Random random = new Random(Guid.NewGuid().GetHashCode());
            double number = 0;
            List<double> list = new List<double>();

            while (list.Count < (array.Length - less))
            {
                number = array[random.Next(0, (array.Length))];
                if (!list.Contains(number))
                    list.Add(number);
            }

            temp = list.ToArray();
            Array.Sort(temp);

            return temp;
        }

        private static int getDigits(int n)
        {
            if (n >= 0)
            {
                if (n < 100) return 2;
                if (n < 1000) return 3;
                if (n < 10000) return 4;
                if (n < 100000) return 5;
                if (n < 1000000) return 6;
                if (n < 10000000) return 7;
                if (n < 100000000) return 8;
                if (n < 1000000000) return 9;
                return 10;
            }
            else
            {
                return 2;
            }
        }
    }
}