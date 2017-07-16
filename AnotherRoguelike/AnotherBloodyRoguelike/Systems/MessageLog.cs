using RLNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnotherRoguelike.Systems
{
    public class MessageLog
    {
        //Define max number of lines to store
        private static readonly int maxLines = 9;

        //Use a FIFO queue to keep track of lines
        private readonly Queue<string> lines;

        public MessageLog()
        {
            lines = new Queue<string>();
        }

        public void Add(string msg)
        {
            lines.Enqueue(msg);

            //Removing oldest line when exceeding the line limit
            if(lines.Count > maxLines)
            {
                lines.Dequeue();
            }
        }

        public void Draw(RLConsole console)
        {
            string[] lins = lines.ToArray();
            for (int i = 0; i < lins.Length; i++)
                console.Print(1, i + 1, lins[i], RLColor.White);
        }
    }
}
