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

        

    }
}