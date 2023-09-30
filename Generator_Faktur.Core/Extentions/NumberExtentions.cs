using System;
using System.Collections.Generic;
using System.Text;

namespace Generator_Faktur.Core.Extentions
{
    public static class NumberExtentions
    {
        public static string NumberToText(this int n)
        {
            if (n < 0)
                return "Minus " + NumberToText(-n);
            else if (n == 0)
                return "";
            else if (n <= 19)
                return new string[] {"Jeden", "Dwa", "Trzy", "Cztery", "Pięć", "Sześć", "Siedem", "Osiem",
         "Dziewięć", "Dziesięć", "Jedenaście", "Dwanaście", "Trzynaście", "Czternaście", "Piętnaście", "Szesnaście",
         "Siedemnaście", "Osiemnaście", "Dziewiętnaście"}[n - 1] + " ";
            else if (n <= 99)
                return new string[] {"Dwadzieścia", "Trzydzieści", "Czterdzieści", "Pięćdziesiąt", "Sześćdziesiąt", "Siedemdziesiąt",
         "Osiemdziesiąt", "Dziewięćdziesiąt"}[n / 10 - 2] + " " + NumberToText(n % 10);
            else if (n <= 499)
                return new string[] { "Sto", "Dwieście", "Trzysta", "Czterysta" }[n / 100 - 1] + " " + NumberToText(n % 100);
            else if (n <= 999)
                return NumberToText(n / 100).Trim() + "set " + NumberToText(n % 100);
            else if (n <= 1999)
                return "Tysiąc " + NumberToText(n % 1000);
            else if (n <= 4999 || ((n / 1000) % 10 > 1 && (n / 1000) % 10 < 5))
                return NumberToText(n / 1000) + "Tysiące " + NumberToText(n % 1000);
            else
                return NumberToText(n / 1000) + "Tysięcy " + NumberToText(n % 1000);
        }

        public static string NumberToWordsEng(this int number)
        {
            if (number == 0)
                return "zero";

            if (number < 0)
                return "minus " + NumberToWordsEng(Math.Abs(number));

            string words = "";

            if ((number / 1000000) > 0)
            {
                words += NumberToWordsEng(number / 1000000) + " Million ";
                number %= 1000000;
            }

            if ((number / 1000) > 0)
            {
                words += NumberToWordsEng(number / 1000) + " Thousand ";
                number %= 1000;
            }

            if ((number / 100) > 0)
            {
                words += NumberToWordsEng(number / 100) + " Hundred ";
                number %= 100;
            }

            if (number > 0)
            {
                if (words != "")
                    words += "and ";

                var unitsMap = new[] { "Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
                var tensMap = new[] { "Zero", "Ten", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };

                if (number < 20)
                    words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0)
                        words += "-" + unitsMap[number % 10];
                }
            }

            return words;
        }
    }
}
