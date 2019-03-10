using System;
using System.Collections.Generic;
using System.Text;

namespace VsExamples.Standard
{
    public class MethodExample
    {

        public void Add(ref int value, Person person, out int result2)
        {
            // value = value + 20;
            value = value + 20;
            person.Age += 20;
            result2 = value + 20;
        }

        public string Join(string joiner, params string[] values)
        {
            return string.Join(joiner, values);
        }
    }
}
