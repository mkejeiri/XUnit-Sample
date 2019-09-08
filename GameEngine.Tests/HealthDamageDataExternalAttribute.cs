using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Xunit.Sdk;

namespace GameEngine.Tests
{
    public class HealthDamageDataExternalAttribute : DataAttribute
    {
        private readonly string _fileName;

        public HealthDamageDataExternalAttribute(string fileName)
        {
            _fileName = fileName;
        }
        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {
            string[] csvLines = File.ReadAllLines(_fileName);
            var testCases = new List<object[]>();

            foreach (var cvsvline in csvLines)
            {
                IEnumerable<int> values = cvsvline.Split(',').Select(int.Parse);
                object[] testCase = values.Cast<object>().ToArray();
                testCases.Add(testCase);
            }
            return testCases;
            //yield return new object[] { 0, 100 };
            //yield return new object[] { 1, 99 };
            //yield return new object[] { 50, 50 };
            //yield return new object[] { 75, 25 };
            //yield return new object[] { 101, 1 };
            //yield return new object[] { 0, 100 };
        }
    }
}
