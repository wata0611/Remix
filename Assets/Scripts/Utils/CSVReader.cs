using System.IO;
using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    public class CSVReader 
    {
        // ”Ä—p“I‚ÈCSVReader
        public static List<string[]> ReadCSV(string filePath)
        {
            List<string[]> dataList = new List<string[]>();
            TextAsset csv = Resources.Load(filePath) as TextAsset;
            StringReader reader = new StringReader(csv.text);

            while (reader.Peek() != -1)
            {
                string line = reader.ReadLine();
                if (line.StartsWith("#"))
                {
                    continue;
                }
                dataList.Add(line.Split(','));
            }

            return dataList;
        }
    }
}