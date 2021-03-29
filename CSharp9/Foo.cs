using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp9
{
    public class Foo
    {
        public Foo()
        {
            this.Bar = 10;
            this.DateTime = new DateTime(2021, 1, 1);
        }

        public int Bar { get; init; }
        public DateTime DateTime { get; init; }

        public bool IsInRange(int i) =>
            i is (>= 1 and <= 10) or (>= 100 and <= 200);
    }

    public record Person
    {
        public string LastName { get; }
        public string FirstName { get; }

        public Person(string first, string last) => (FirstName, LastName) = (first, last);
    }

    public record Teacher : Person
    {
        public string Subject { get; }

        public Teacher(string first, string last, string sub)
            : base(first, last) => Subject = sub;
    }
}
