using System.Collections.Generic;

namespace ConsoleThreading
{
    public class ThreadingObject
    {
        public string Name;
        private readonly int Number;
        public bool MyBool;
        private readonly IReadOnlyCollection<KeyValuePair<int, string>> SomeKVP;

        public ThreadingObject(string n, int i, bool b, IReadOnlyCollection<KeyValuePair<int, string>> irc)
        {
            this.Name = n;
            this.Number = i;
            this.MyBool = b;
            this.SomeKVP = irc;
        }
    }
}
