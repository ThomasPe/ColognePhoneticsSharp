using System;
using System.Linq;
using System.Text;

namespace ColognePhoneticsSharp
{
    /// <summary>
    /// Transforms a word into the respective Cologne Phontetics notation
    /// </summary>
    public class ColognePhonetics
    {

        /// <summary>
        /// Group of letters with value "0"
        /// </summary>
        private static char[] group0 = new char[] { 'A', 'E', 'I', 'J', 'O', 'U', 'Y', 'Ä', 'Ö', 'Ü' };

        /// <summary>
        /// Group of letters with value "3"
        /// </summary>
        private static char[] group3 = new char[] { 'F', 'V', 'W' };

        /// <summary>
        /// Group of letters with value "4"
        /// </summary>
        private static char[] group4 = new char[] { 'G', 'K', 'Q', };

        /// <summary>
        /// Group of letters with value "6"
        /// </summary>
        private static char[] group6 = new char[] { 'M', 'N' };

        /// <summary>
        /// Group of letters with value "8"
        /// </summary>
        private static char[] group8 = new char[] { 'S', 'Z', 'ß' };

        /// <summary>
        /// Group of letters following an initial "C"
        /// </summary>
        private static char[] groupCFirst = new char[] { 'A', 'H', 'K', 'L', 'O', 'Q', 'R', 'U', 'X' };

        /// <summary>
        /// Group of letters following a "C" within the string
        /// </summary>
        private static char[] groupCNoFirst = new char[] { 'A', 'H', 'K', 'O', 'Q', 'U', 'X' };

        /// <summary>
        /// Group of letters that can precede a "C"
        /// </summary>
        private static char[] groupCPrevious = new char[] { 'S', 'Z' };

        /// <summary>
        /// Group of letters that can precede a "D" or "T"
        /// </summary>
        private static char[] groupDTPrevious = new char[] { 'C', 'S', 'Z' };

        /// <summary>
        /// Group of letters following an "X"
        /// </summary>
        private static char[] groupXFollow = new char[] { 'C', 'K', 'Q' };

        /// <summary>
        /// Process all three steps of encoding the Cologene Phonetics
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string GetPhonetics(string input)
        {
            var output = GetEncoding(input);
            output = CleanDoubles(output);
            output = CleanZeros(output);
            return output;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string GetEncoding(string input)
        {
            char[] content = input.ToUpperInvariant().ToCharArray();

            // hold result in StringBuilder
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < content.Length; i++)
            {
                char entry = content[i];

                // ignore non-letters
                if (!Char.IsLetter(entry))
                {
                    continue;
                }

                // ignore H
                if (entry.Equals('H'))
                {
                    continue;
                }

                // check if entry is part of the standard value groups

                if (group0.Contains(entry))
                {
                    sb.Append("0");
                    continue;
                }

                if (group3.Contains(entry))
                {
                    sb.Append("3");
                    continue;
                }

                if (group4.Contains(entry))
                {
                    sb.Append("4");
                    continue;
                }

                if (group6.Contains(entry))
                {
                    sb.Append("6");
                    continue;
                }

                if (group8.Contains(entry))
                {
                    sb.Append("8");
                    continue;
                }

                if (entry.Equals('B'))
                {
                    sb.Append("1");
                    continue;
                }

                if (entry.Equals('L'))
                {
                    sb.Append("5");
                    continue;
                }

                if (entry.Equals('R'))
                {
                    sb.Append("7");
                    continue;
                }

                // check if character is in a special value group

                if (entry.Equals('P'))
                {
                    // last letter in array?
                    if (i + 1 >= content.Length)
                    {
                        sb.Append("1");
                        continue;
                    }

                    char next = content[i + 1];

                    // if followed by "H"
                    if (next.Equals('H'))
                    {
                        sb.Append("3");
                        continue;
                    }

                    sb.Append("1");
                    continue;
                }

                if (entry.Equals('X'))
                {
                    // if first letter
                    if (i == 0)
                    {
                        sb.Append("48");
                        continue;
                    }

                    char previous = content[i - 1];

                    // compare with previous
                    if (groupXFollow.Contains(previous))
                    {
                        sb.Append("8");
                        continue;
                    }

                    sb.Append("48");
                    continue;
                }

                if (entry.Equals('D') || entry.Equals('T'))
                {
                    // last letter in array?
                    if (i + 1 >= content.Length)
                    {
                        sb.Append("2");
                        continue;
                    }

                    char next = content[i + 1];

                    // is next value in special value group?
                    if (groupDTPrevious.Contains(next))
                    {
                        sb.Append("8");
                        continue;
                    }

                    sb.Append("2");
                    continue;
                }

                if (entry.Equals('C'))
                {
                    // if first letter
                    if (i == 0)
                    {
                        char next = content[i + 1];

                        // if next letter is in same group
                        if (groupCFirst.Contains(next))
                        {
                            sb.Append("4");
                            continue;
                        }

                        sb.Append("8");
                        continue;
                    }
                    else // not first letter
                    {
                        // last letter?
                        if (i + 1 >= content.Length)
                        {
                            continue;
                        }

                        char next = content[i + 1];
                        char previous = content[i - 1];

                        // is previous letter in same group?
                        if (groupCPrevious.Contains(previous))
                        {
                            sb.Append("8");
                            continue;
                        }
                        else
                        {
                            // is next letter in same group?
                            if (groupCNoFirst.Contains(next))
                            {
                                sb.Append("4");
                                continue;
                            }

                            sb.Append("8");
                            continue;
                        }
                    }
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// Remove duplicate, adjacent values 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string CleanDoubles(string input)
        {
            StringBuilder sb = new StringBuilder();
            char[] content = input.ToCharArray();
            char previous = new char();

            for (int i = 0; i < content.Length; i++)
            {
                char entry = content[i];
                if (!entry.Equals(previous))
                {
                    sb.Append(entry);
                }
                previous = entry;
            }
            return sb.ToString();
        }

        /// <summary>
        /// Remove zeros, except at the beginning
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string CleanZeros(string input)
        {
            StringBuilder sb = new StringBuilder();
            char[] content = input.ToCharArray();

            for(int i = 0; i < content.Length; i++)
            {
                char entry = content[i];

                // skip all zeros except in first place
                if(!entry.Equals('0') || i == 0)
                {
                    sb.Append(entry);
                }
            }
            return sb.ToString();
        }
    }
}
