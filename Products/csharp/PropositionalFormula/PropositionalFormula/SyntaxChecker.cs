using System;
using System.Collections.Generic;
using System.Text;

namespace PropositionalFormula
{
    public class SyntaxChecker
    {

        public static bool CheckBrackets(string str, string open = "(", string close = ")")
        {
            int nestCount = 0;

            while (str.Length > 0)
            {
                if (str.StartsWith(open))
                {
                    nestCount++;
                    str = str.Substring(open.Length);
                }
                else if (str.StartsWith(close))
                {
                    nestCount--;
                    str = str.Substring(close.Length);
                }
                else
                {
                    str = str.Substring(1);
                }

                if (nestCount < 0)
                {
                    return false;
                }
            }

            return nestCount == 0;
        }
    }
}
