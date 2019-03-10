using System;
using System.Diagnostics;
using Xunit;
using VsExamples.Standard;
using Shouldly;

namespace VsExamples.Tests
{
    public class UnitTest1
    {
        
        [Fact(DisplayName = "My First Unit Test")]
        public void Test1()
        {
            var example = new MethodExample();

            int value = 20;
            Person person = new Person()
            {
                Age = 20
            };
            //var key = person.generateKey(2);

            //person.IsVisible((dynamic)key);
            int result2;
            example.Add(ref value, person, out result2);

            int result = value;
            int resultAge = person.Age;

            var resultJoin = example.Join(",", new string[] { "Tom", "Jack" });

            // Debugger.Break();

            result.ShouldBe(20);
            resultAge.ShouldBe(40);
        }

        
        [Theory(DisplayName = "My Test with Parameters")]
        [InlineData(1, "OK")]
        [InlineData(2, "Error")]
        public void TestWithParameters(int value, string name)
        {

        }
    }
}
