using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee_Store_Management
{
    class Ultils
    {
        public static string FormatText(string text, int numberChar = 15)
        {
            if (text == null)
            {
                return "";
            }
            else
            {
                if (text.Length > numberChar)
                {
                    return text;
                }
                else
                {
                    int countSpace = numberChar - text.Length;
                    for (int i = 0; i < countSpace; i++)
                    {
                        text = text + ' ';
                    }
                    return text;
                }
            }
        }
    }
}
