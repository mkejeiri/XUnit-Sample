using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GameEngine.Tests
{
    public class InternalHealthDamageData
    {
        //public static readonly List<object> Data = new List<object>
        //{
        //    new object[] {0, 100},
        //    new object[] {1, 99},
        //    new object[] {50, 50},
        //    new object[] {101, 1},
        //    new object[] {0, 100}
        //};
        //public static IEnumerable<object> TestData => Data;

        public static IEnumerable<object[]> TestData {
            get
            {
                yield return new object[] {0, 100};
                yield return new object[] {1, 99};
                yield return new object[] {50, 50};
                yield return new object[] { 75, 25 };
                yield return new object[] {101, 1};
                yield return new object[] {0, 100};
            }
        }
    }
}
