using System;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic.CompilerServices;

namespace Cbase
{
    public class RegexTest
    {
        static void Main(string[] args)
        {
            var str = "ssss{aaaa}ssssss{bbbbbbb}sssssss{ddddd.eeeee}sssssss{fff_ggg.hhh}ssss{}";
            var regex = new Regex("{[\\w|.|_]*}");

            var matches = regex.Matches(str);
            foreach (Match match in matches)
            {
                Console.WriteLine(match.Value);
            }
            
        }
    }
}