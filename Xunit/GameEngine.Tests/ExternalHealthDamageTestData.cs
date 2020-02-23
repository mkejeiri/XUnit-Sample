using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GameEngine.Tests
{
    public class ExternalHealthDamageData
    {
       public static IEnumerable<object[]> TestData
        {
            get
            {
                string[] csvLines = File.ReadAllLines("testData.csv");
                var testCases = new List<object[]>();

                foreach (var cvsvline in csvLines)
                {
                    IEnumerable<int> values = cvsvline.Split(',').Select(int.Parse);
                    object[] testCase = values.Cast<object>().ToArray();
                    testCases.Add(testCase);
                }
                return testCases;
            }
        }


    }
}
