using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompletKitInstall.Data
{
    public interface IFormatter
    {
        Task<List<string>> ArrangeDescription(string inputString);
    }
    public class Formatter : IFormatter
    {
        public int maxCapacity = 100;
        public Task<List<string>> ArrangeDescription(string inputString)
        {
            var word = new StringBuilder(maxCapacity);
            var line = new StringBuilder();
            var text = new List<string>();

            foreach (char letter in inputString)
            {
                if (!char.IsWhiteSpace(letter)) 
                {
                    word.Append(letter);
                }
                else
                {
                    if ((line.Length + word.Length) <= maxCapacity)
                    {
                        switch (letter.ToString())
                        {
                            case " ":
                                line.Append(word);
                                line.Append(" ");
                                word.Clear();
                                break;
                            case "\n":
                                text.Add(line.ToString());
                                line.Clear();
                                break;
                            case ":":
                                text.Add(line.ToString());
                                line.Clear();
                                break;
                        }
                        //if (letter.ToString() == " ")
                        //{
                        //    line.Append(word);
                        //    line.Append(" ");
                        //    word.Clear();
                        //}
                        //if (letter.ToString()=="\n")
                        //{
                        //    text.Add(line.ToString());
                        //    line.Clear();
                        //}
                    }
                    else
                    {
                        text.Add(line.ToString());
                        line.Clear();
                    }    
                }
            }
            return Task.FromResult(text);
        }
    }
}
