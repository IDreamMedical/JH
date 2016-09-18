using System;
using System.Collections.Generic;
//  using System.Linq;
using System.Text;
using System.Collections;

namespace UniGuy.Core.Helpers
{
    public class StringHelper
    {
        /// <summary>
        /// String processing functions that use the primitive data type string. Effective for small-to-medium strings.
        /// </summary>
        public class StringProcessing
        {
            /// <summary>
            /// Returns a string representation of an array. Each element is written on a new line.
            /// </summary>
            public static string ArrayToString(IList array)
            {
                //Validate input
                if (array == null || array.Count == 0)
                    return string.Empty;

                string output = string.Empty;

                for (int i = 0; i < array.Count; i++)
                {
                    output += array[i].ToString();

                    if (i != array.Count - 1) //don't add separator at the end of the list
                        output += Environment.NewLine;
                }

                return output;
            }

            /// <summary>
            /// Returns a string representation of an array. Each element is separated by the specified string.
            /// </summary>
            /// <param name="array">The collection of objects.</param>
            /// <param name="separator">The string sequence to separate each element in the collection</param>
            /// <returns></returns>
            public static string ArrayToString(IList array, string separator)
            {
                //Validate input
                if (array == null || array.Count == 0)
                    return string.Empty;

                string output = string.Empty;

                for (int i = 0; i < array.Count; i++)
                {
                    output += array[i].ToString();

                    if (i != array.Count - 1) //don't add separator at the end of the list
                        output += separator;
                }

                return output;
            }
        }

        /// <summary>
        /// String processing functions that use the the StringBuilder class. Effective for large strings.
        /// </summary>
        public class StringBuilderProcessing
        {
            ////Processing with StringBuilder
            //Use for small to medium strings

            //Swaps the cases in a string
            //word -> WORD
            //Word -> wORD
            //WoRd -> wOrD
            public static string SwapCases(string input)
            {
                StringBuilder ret = new StringBuilder();
                for (int i = 0; i < input.Length; i++)
                {
                    if (string.Compare(input.Substring(i, 1), input.Substring(i, 1).ToUpper(), false) == 0)
                        ret.Append(input.Substring(i, 1).ToLower());
                    else
                        ret.Append(input.Substring(i, 1).ToUpper());
                }
                return ret.ToString();
            }

            //Alternates cases between letters of a string, letting the user pick if the first letter is capitalized
            public static string AlternateCases(string input, bool firstIsUpper)
            {
                StringBuilder ret = new StringBuilder();
                for (int i = 0; i < input.Length; i++)
                {
                    if (firstIsUpper)
                        ret.Append(input.Substring(i, 1).ToUpper());
                    else
                        ret.Append(input.Substring(i, 1).ToLower());

                    firstIsUpper = !firstIsUpper;
                }

                return ret.ToString();
            }

            //Removes vowels from a word
            //remove -> rmv
            public static string RemoveVowels(string input)
            {
                StringBuilder ret = new StringBuilder();
                string currentLetter;
                for (int i = 0; i < input.Length; i++)
                {
                    currentLetter = input.Substring(i, 1);

                    if (string.Compare(currentLetter, "a", true) != 0 &&
                        string.Compare(currentLetter, "e", true) != 0 &&
                        string.Compare(currentLetter, "i", true) != 0 &&
                        string.Compare(currentLetter, "o", true) != 0 &&
                        string.Compare(currentLetter, "u", true) != 0)
                    {
                        //Not a vowel, add it
                        ret.Append(currentLetter);
                    }
                }
                return ret.ToString();
            }

            //Removes vowels from a word
            //remove -> eoe
            public static string KeepVowels(string input)
            {
                StringBuilder ret = new StringBuilder();
                string currentLetter;
                for (int i = 0; i < input.Length; i++)
                {
                    currentLetter = input.Substring(i, 1);

                    if (string.Compare(currentLetter, "a", true) == 0 ||
                        string.Compare(currentLetter, "e", true) == 0 ||
                        string.Compare(currentLetter, "i", true) == 0 ||
                        string.Compare(currentLetter, "o", true) == 0 ||
                        string.Compare(currentLetter, "u", true) == 0)
                    {
                        //A vowel, add it
                        ret.Append(currentLetter);
                    }
                }
                return ret.ToString();
            }

            //Returns an array converted into a string
            public static string ArrayToString(Array input, string separator)
            {
                StringBuilder ret = new StringBuilder();
                for (int i = 0; i < input.Length; i++)
                {
                    ret.Append(input.GetValue(i).ToString());
                    if (i != input.Length - 1)
                        ret.Append(separator);
                }
                return ret.ToString();
            }

            //Inserts a separator after every letter
            //hello, - -> h-e-l-l-o
            public static string InsertSeparator(string input, string separator)
            {
                StringBuilder ret = new StringBuilder();
                for (int i = 0; i < input.Length; i++)
                {
                    ret.Append(input.Substring(i, 1));
                    if (i != input.Length - 1)
                        ret.Append(separator);
                }
                return ret.ToString();
            }

            //Inserts a separator after every Count letters
            //hello, -, 2 -> he-ll-o
            public static string InsertSeparatorEvery(string input, string separator, int count)
            {
                StringBuilder ret = new StringBuilder();
                for (int i = 0; i < input.Length; i++)
                {
                    if (i + count < input.Length)
                        ret.Append(input.Substring(i, count));
                    else
                        ret.Append(input.Substring(i));

                    if (i != input.Length - 1)
                        ret.Append(separator);
                }
                return ret.ToString();
            }

            //Reverses a string
            //Hello -> olleH
            public static string Reverse(string input)
            {
                StringBuilder ret = new StringBuilder();
                for (int i = input.Length - 1; i >= 0; i--)
                {
                    ret.Append(input.Substring(i, 1));
                }
                return ret.ToString();
            }
            /// <summary>
            /// Returns a string representation of an array. Each element is written on a new line.
            /// </summary>
            public static string ArrayToString(IList array)
            {
                //Validate input
                if (array == null || array.Count == 0)
                    return string.Empty;

                StringBuilder output = new StringBuilder(array.Count * 2);

                for (int i = 0; i < array.Count; i++)
                {
                    output.Append(array[i].ToString());

                    if (i != array.Count - 1) //don't add separator at the end of the list
                        output.Append(Environment.NewLine);
                }

                return output.ToString();
            }

            /// <summary>
            /// Returns a string representation of an array. Each element is separated by the specified string.
            /// </summary>
            /// <param name="array">The collection of objects.</param>
            /// <param name="separator">The string sequence to separate each element in the collection</param>
            /// <returns></returns>
            public static string ArrayToString(IList array, string separator)
            {
                //Validate input
                if (array == null || array.Count == 0)
                    return string.Empty;

                StringBuilder output = new StringBuilder(array.Count * 2);

                for (int i = 0; i < array.Count; i++)
                {
                    output.Append(array[i].ToString());

                    if (i != array.Count - 1) //don't add separator at the end of the list
                        output.Append(separator);
                }

                return output.ToString();
            }
        }

        /// <summary>
        /// Returns a string with characters in reverse order.
        /// </summary>
        public static string Reverse(string input)
        {
            //Validate input
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            char[] outputChars = input.ToCharArray();

            //Reverse
            Array.Reverse(outputChars);

            //build a string from the processed characters and return it
            return new string(outputChars);
        }

        /// <summary>
        /// Returns a string with a given seperator inserted after every character.
        /// </summary>
        /// <param name="input">The original string.</param>
        /// <param name="separator">The separator to insert.</param>
        public static string InsertSeparator(string input, string separator)
        {
            //Validate string
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            List<char> outputChars = new List<char>(input.ToCharArray());
            char[] separatorChars = separator.ToCharArray();

            int i = 1;
            while (i < outputChars.Count)
            {
                if (i != outputChars.Count) //don't add separator to the end of string
                    outputChars.InsertRange(i, separatorChars);

                i += 1 + separator.Length; //go up the interval amount plus separator
            }

            return new string(outputChars.ToArray());
        }

        /// <summary>
        /// Returns a string with a given seperator inserted after a specified interval of characters.
        /// </summary>
        /// <param name="input">The original string.</param>
        /// <param name="separator">The separator to insert.</param>
        /// <param name="interval">The number of characters between separators.</param>
        public static string InsertSeparator(string input, string separator, int interval)
        {
            //Validate string
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            List<char> outputChars = new List<char>(input.ToCharArray());
            char[] separatorChars = separator.ToCharArray();

            int i = interval;
            while (i < outputChars.Count)
            {
                if (i != outputChars.Count) //don't add separator to the end of string
                    outputChars.InsertRange(i, separatorChars);

                i += interval + separator.Length; //go up the interval amount plus separator
            }

            return new string(outputChars.ToArray());
        }

        /// <summary>
        /// Returns a string with any vowel character removed.
        /// </summary>
        public static string RemoveVowels(string input)
        {
            //Validate input
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            List<char> outputChars = new List<char>(input.ToCharArray());

            //Iterate backwards to avoid problems from removing characters
            for (int i = outputChars.Count - 1; i >= 0; i--)
            {
                if (outputChars[i] == 'a' || outputChars[i] == 'A' ||
                    outputChars[i] == 'e' || outputChars[i] == 'E' ||
                    outputChars[i] == 'i' || outputChars[i] == 'I' ||
                    outputChars[i] == 'o' || outputChars[i] == 'O' ||
                    outputChars[i] == 'u' || outputChars[i] == 'U')
                    //not a vowel, remove it
                    outputChars.RemoveAt(i);
            }

            return new string(outputChars.ToArray());
        }

        /// <summary>
        /// Returns a string with only the vowel characters.
        /// </summary>
        public static string KeepVowels(string input)
        {
            //Validate input
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            List<char> outputChars = new List<char>(input.ToCharArray());

            //Iterate backwards to avoid problems from removing characters
            for (int i = outputChars.Count - 1; i >= 0; i--)
            {
                if (!(outputChars[i] == 'a' || outputChars[i] == 'A' ||
                      outputChars[i] == 'e' || outputChars[i] == 'E' ||
                      outputChars[i] == 'i' || outputChars[i] == 'I' ||
                      outputChars[i] == 'o' || outputChars[i] == 'O' ||
                      outputChars[i] == 'u' || outputChars[i] == 'U'))
                    //a vowel, remove it
                    outputChars.RemoveAt(i);
            }

            return new string(outputChars.ToArray());
        }

        /// <summary>
        /// Returns a string with alternated letter casing (upper/lower). First character of the string stays the same.
        /// </summary>
        public static string AlternateCases(string input)
        {
            if (string.IsNullOrEmpty(input)) return string.Empty;
            if (input.Length == 1) return input; //Nothing to alternate

            char[] inputChars = input.ToCharArray(); //character array representing the string
            bool toUpper = !char.IsUpper(inputChars[0]);

            for (int i = 1; i < inputChars.Length; i++) //start with the section character
            {
                if (toUpper)
                    inputChars[i] = char.ToUpper(inputChars[i]);
                else
                    inputChars[i] = char.ToLower(inputChars[i]);

                toUpper = !toUpper; //alternate
            }

            return new string(inputChars);
        }

        /// <summary>
        /// Returns a string with the opposite letter casing for each character.
        /// </summary>
        public static string SwapCases(string input)
        {
            //Validate input
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            char[] inputChars = input.ToCharArray();

            for (int i = 0; i < inputChars.Length; i++)
            {
                //Apply the opposite letter casing
                if (char.IsUpper(inputChars[i]))
                    inputChars[i] = char.ToLower(inputChars[i]);
                else
                    inputChars[i] = char.ToUpper(inputChars[i]);
            }

            return new string(inputChars);
        }

        /// <summary>
        /// Capitalizes the first character in a string.
        /// </summary>
        public static string Capitalize(string input)
        {
            if (string.IsNullOrEmpty(input)) return string.Empty;
            if (input.Length == 1) return input.ToUpper();

            return input[0].ToString().ToUpper() + input.Substring(1);
        }

        /// <summary>
        /// Returns the initials of each word in a string. Words must be separated with spaces.
        /// </summary>
        /// <param name="input">The original string.</param>
        /// <param name="capitalizeInitials">True to capitalize each initial in the output string.</param>
        /// <param name="preserveSpaces">True to preserver the spaces between initials in the output string.</param>
        /// <param name="includePeriod">True to include a '.' after each intiali</param>
        public static string GetInitials(string input, bool capitalizeInitials, bool preserveSpaces, bool includePeriod)
        {
            //Verify input
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            string[] words = input.Split(' ');

            for (int i = 0; i < words.Length; i++)
            {
                if (words[i].Length > 0)
                {
                    if (capitalizeInitials)
                        words[i] = char.ToUpper(words[i][0]).ToString(); //only keep the first letter
                    else
                        words[i] = words[i][0].ToString(); //only keep the first letter

                    if (includePeriod)
                        words[i] += ".";
                }
            }

            if (preserveSpaces)
                return string.Join(" ", words);
            else
                return string.Join("", words);
        }

        /// <summary>
        /// Returns the initials of each word in a string. Words are separated according to the sepecified string sequence.
        /// </summary>
        /// <param name="input">The original string.</param>
        /// <param name="separator">The string sequence that separates words.</param>
        /// <param name="capitalizeInitials">True to capitalize each initial in the output string.</param>
        /// <param name="preserveSeparator">True to preserver the spaces between initials in the output string.</param>
        public static string GetInitials(string input, string separator, bool capitalizeInitials, bool preserveSeparator, bool includePeriod)
        {
            //Verify input
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            string[] words = input.Split(separator.ToCharArray());

            for (int i = 0; i < words.Length; i++)
            {
                if (words[i].Length > 0)
                {
                    if (capitalizeInitials)
                        words[i] = char.ToUpper(words[i][0]).ToString(); //only keep the first letter
                    else
                        words[i] = words[i][0].ToString(); //only keep the first letter

                    if (includePeriod)
                        words[i] += ".";
                }
            }

            if (preserveSeparator)
                return string.Join(separator, words);
            else
                return string.Join("", words);
        }

        /// <summary>
        /// Returns a string with each word's first character capitalized. Words must be separated by spaces.
        /// </summary>
        public static string GetTitle(string input)
        {
            //Verify input
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            string[] words = input.Split(' ');

            for (int i = 0; i < words.Length; i++)
            {
                //Capitalize each word
                if (words[i].Length > 0)
                    words[i] = char.ToUpper(words[i][0]).ToString() + words[i].Substring(1);
            }

            return string.Join(" ", words);
        }

        /// <summary>
        /// Returns a string with each word's first character capitalized. Words are separated according to the sepecified string sequence.
        /// </summary>
        /// <param name="input">The original string.</param>
        /// <param name="separator">The string sequence that separates words.</param>
        public static string GetTitle(string input, string separator)
        {
            //Verify input
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            string[] words = input.Split(separator.ToCharArray());

            for (int i = 0; i < words.Length; i++)
            {
                //Capitalize each word
                if (words[i].Length > 0)
                    words[i] = char.ToUpper(words[i][0]).ToString() + words[i].Substring(1);
            }

            return string.Join(separator, words);
        }

        /// <summary>
        /// Returns a segment of a string, marked by the start and end index (exclusive).
        /// </summary>
        /// <param name="input">The original string.</param>
        /// <param name="start">The start index position.</param>
        /// <param name="end">The end index position. (exclusive)</param>
        public static string SubstringEnd(string input, int start, int end)
        {
            //Verify input
            if (string.IsNullOrEmpty(input) || start == end)
                return string.Empty;

            if (start == 0 && end == input.Length)
                return input; //entire string

            if (start < 0)
                throw new IndexOutOfRangeException("start index cannot be less than zero.");

            if (start > input.Length)
                throw new IndexOutOfRangeException("start index cannot be greater than the length of the string.");

            if (end < 0)
                throw new IndexOutOfRangeException("end index cannot be less than zero.");

            if (end > input.Length)
                throw new IndexOutOfRangeException("end index cannot be greater than the length of the string.");

            if (start > end)
                throw new IndexOutOfRangeException("start index cannot be greater than the end index.");

            return input.Substring(start, end - start);
        }

        /// <summary>
        /// Returns the character in a string at a given index counting from the right.
        /// </summary>
        /// <param name="input">The original string.</param>
        /// <param name="index">The starting position from the right. (Index 0 = last character)</param>
        public static char CharRight(string input, int index)
        {
            //Verify input
            if (string.IsNullOrEmpty(input))
                return new char();

            if (input.Length - index - 1 >= input.Length)
                throw new IndexOutOfRangeException("Index cannot be less than zero.");

            if (input.Length - index - 1 < 0)
                throw new IndexOutOfRangeException("Index cannot be larger than the length of the string");

            return input[input.Length - index - 1];
        }

        /// <summary>
        /// Returns the character at a position given by the startingIndex plus the given count.
        /// </summary>
        /// <param name="input">The original string.</param>
        /// <param name="startingIndex">The starting index position.</param>
        /// <param name="countIndex">The number of characters to count from the starting position.</param>
        /// <returns></returns>
        public static char CharMid(string input, int startingIndex, int count)
        {
            //Verify input
            if (string.IsNullOrEmpty(input))
                return new char();

            if (startingIndex < 0)
                throw new IndexOutOfRangeException("startingIndex cannot be less than zero.");

            if (startingIndex >= input.Length)
                throw new IndexOutOfRangeException("startingIndex cannot be greater than the length of the string.");

            if (startingIndex + count < 0)
                throw new IndexOutOfRangeException("startingIndex + count cannot be less than zero.");

            if (startingIndex + count >= input.Length)
                throw new IndexOutOfRangeException("startingIndex + count cannot be greater than the length of the string.");

            return input[startingIndex + count];
        }

        /// <summary>
        /// Returns the total number of times a given sequence appears in a string.
        /// </summary>
        /// <param name="input">The original string.</param>
        /// <param name="sequence">The string sequence to count.</param>
        /// <param name="ignoreCase">True, to ignore the difference in case between the sequence and the original string.</param>
        public static int CountString(string input, string sequence, bool ignoreCase)
        {
            //Verify input
            if (string.IsNullOrEmpty(input) || string.IsNullOrEmpty(sequence))
                return 0;

            int count = 0;
            string inputSegment = null; //holds the current segment being compared

            for (int i = 0; i < input.Length; i++)
            {
                if (i + sequence.Length > input.Length)
                    break; //sequence doesn't fit anymore

                inputSegment = input.Substring(i, sequence.Length);

                if (string.Compare(inputSegment, sequence, ignoreCase) == 0)
                    count++; //another match found
            }
            return count;
        }

        /// <summary>
        /// Returns an array of every index where a sequence is found on the specified string. Note: Overlaps will be counted.
        /// </summary>
        /// <param name="input">The original string.</param>
        /// <param name="sequence">The string sequence to seek.</param>
        /// <param name="ignoreCase">True, to ignore the difference in case between the sequence and the original string.</param>
        public static int[] IndexOfAll(string input, string sequence, bool ignoreCase)
        {
            //Verify input
            if (string.IsNullOrEmpty(input))
                return new int[0]; //empty array

            List<int> indices = new List<int>();

            string inputSegment = null; //holds the current segment being compared

            for (int i = 0; i < input.Length; i++)
            {
                if (i + sequence.Length > input.Length)
                    break; //sequence doesn't fit anymore

                inputSegment = input.Substring(i, sequence.Length);

                if (string.Compare(inputSegment, sequence, ignoreCase) == 0)
                    indices.Add(i);
            }

            //Copy entries over to an array
            int[] output = indices.ToArray();
            indices.Clear();

            return output;
        }

        /// <summary>
        /// Returns an array of every index where a sequence is found on the specified string. Note: Overlaps will be counted.
        /// </summary>
        /// <param name="input">The original string.</param>
        /// <param name="sequence">The string sequence to seek.</param>
        /// <param name="startIndex">Index from which to start seeking.</param>
        /// <param name="ignoreCase">True, to ignore the difference in case between the sequence and the original string.</param>
        public static int[] IndexOfAll(string input, string sequence, int startIndex, bool ignoreCase)
        {
            //Verify input
            if (string.IsNullOrEmpty(input))
                return new int[0]; //empty array

            List<int> indices = new List<int>();

            string inputSegment = null; //holds the current segment being compared

            for (int i = startIndex; i < input.Length; i++)
            {
                if (i + sequence.Length > input.Length)
                    break; //sequence doesn't fit anymore

                inputSegment = input.Substring(i, sequence.Length);

                if (string.Compare(inputSegment, sequence, ignoreCase) == 0)
                    indices.Add(i);
            }

            //Copy entries over to an array
            int[] output = indices.ToArray();
            indices.Clear();

            return output;
        }

        /// <summary>
        /// Returns whether the letter casing in a string is alternating.
        /// </summary>
        public static bool IsAlternateCases(string input)
        {
            //Verify input
            if (string.IsNullOrEmpty(input) || input.Length == 1) return false;

            bool isLastUpper = char.IsUpper(input[0]);

            for (int i = 1; i < input.Length; i++)
            {
                if (isLastUpper)
                {
                    if (char.IsUpper(input[i]))
                        return false; //two upper-cases in a row
                }
                else
                {
                    if (char.IsLower(input[i]))
                        return false; //two lower-cases in a row
                }

                isLastUpper = !isLastUpper; //alternate
            }

            return true;
        }

        /// <summary>
        /// Returns true if the first character in a string is upper case.
        /// </summary>
        public static bool IsCapitalized(string input)
        {
            //Verify input
            if (string.IsNullOrEmpty(input)) return false;

            return char.IsUpper(input[0]);
        }

        /// <summary>
        /// Returns whether a string is in all lower case.
        /// </summary>
        public static bool IsLowerCase(string input)
        {
            //Verify input
            if (string.IsNullOrEmpty(input)) return false;

            for (int i = 0; i < input.Length; i++)
            {
                //A single non-lower case character makes function false,
                //unless it is a chracter other than a letter
                if (!char.IsLower(input[i]) && char.IsLetter(input[i]))
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Returns whether a string is in all upper case.
        /// </summary>
        public static bool IsUpperCase(string input)
        {
            //Verify input
            if (string.IsNullOrEmpty(input)) return false;

            for (int i = 0; i < input.Length; i++)
            {
                //A single non-upper case character makes function false,
                //unless it is a chracter other than a letter
                if (!char.IsUpper(input[i]) && char.IsLetter(input[i]))
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Returns whether a string contains any vowel letters
        /// </summary>
        public static bool HasVowels(string input)
        {
            //Verify input
            if (string.IsNullOrEmpty(input)) return false;

            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == 'a' || input[i] == 'A' ||
                    input[i] == 'e' || input[i] == 'E' ||
                    input[i] == 'i' || input[i] == 'I' ||
                    input[i] == 'o' || input[i] == 'O' ||
                    input[i] == 'u' || input[i] == 'U')
                    return true; //a single vowel makes function true
            }

            return false;
        }

        /// <summary>
        /// Returns whether a string is all empty spaces
        /// </summary>
        public static bool IsSpaces(string input)
        {
            return string.IsNullOrEmpty(input) || input.Replace(" ", "").Length == 0;
        }

        /// <summary>
        /// Returns whether a string is composed of only a single character.
        /// </summary>
        public static bool IsRepeatedChar(string input)
        {
            return string.IsNullOrEmpty(input) || input.Replace(input[0].ToString(), "").Length == 0;
        }

        /// <summary>
        /// Returns whether a string is composed of only numeric characters.
        /// </summary>
        public static bool IsNumeric(string input)
        {
            //Verify input
            if (string.IsNullOrEmpty(input))
                return false;

            for (int i = 0; i < input.Length; i++)
            {
                if (!char.IsNumber(input[i]))
                    return false; //single non-numeric integer makes function false
            }
            return true;
        }

        /// <summary>
        /// Returns whether a string contains any numberic characters.
        /// </summary>
        public static bool HasNumeric(string input)
        {
            //Verify input
            if (string.IsNullOrEmpty(input))
                return false;

            for (int i = 0; i < input.Length; i++)
            {
                if (char.IsNumber(input[i]))
                    return true; //single numeric integer makes function true
            }
            return false;
        }

        /// <summary>
        /// Returns whether a string is composed of only letter and number characters.
        /// </summary>
        public static bool IsAlphaNumeric(string input)
        {
            //Verify input
            if (string.IsNullOrEmpty(input)) return false;

            for (int i = 0; i < input.Length; i++)
            {
                if (!char.IsLetter(input[i]) && !char.IsNumber(input[i]))
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Returns whether a string is composed of all letter characters.
        /// </summary>
        public static bool IsLetters(string input)
        {
            //Verify input
            if (string.IsNullOrEmpty(input)) return false;

            for (int i = 0; i < input.Length; i++)
            {
                if (!char.IsLetter(input[i]))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Returns whether a string is formatted like a title, ie the first chracter of each word is capitalized. 
        /// Words must be separated by spaces.
        /// </summary>
        public static bool IsTitle(string input)
        {
            //Verify input
            if (string.IsNullOrEmpty(input))
                return false;

            string[] words = input.Split(' ');

            for (int i = 0; i < words.Length; i++)
            {
                if (words[i].Length > 0)
                    if (!char.IsUpper(words[i][0]))
                        return false;
            }
            return true;
        }

        /// <summary>
        /// Returns whether a string is formatted like a title, ie the first chracter of each word is capitalized. 
        /// Words are separated according to the sepecified string sequence.
        /// </summary>
        /// <param name="input">The original string.</param>
        /// <param name="separator">The string sequence that separates words.</param>
        public static bool IsTitle(string input, string separator)
        {
            //Verify input
            if (string.IsNullOrEmpty(input))
                return false;

            string[] words = input.Split(separator.ToCharArray());

            for (int i = 0; i < words.Length; i++)
            {
                if (words[i].Length > 0)
                    if (!char.IsUpper(words[i][0]))
                        return false;
            }
            return true;
        }

        /// <summary>
        /// Returns whether a string is in a valid email address format.
        /// </summary>
        public static bool IsEmailAddress(string input)
        {
            //Validate input
            if (string.IsNullOrEmpty(input))
                return false;

            if (input.IndexOf('@') != -1 &&
                input.Length >= 5) //any email address will be at least 5 characters (a@a.a)
            {
                int indexOfDot = input.LastIndexOf('.');
                if (indexOfDot > input.IndexOf('@')) //last period must be after the @ 
                    return true;
            }

            return false;
        }

        //Counts total number of a char or chars in a string
        //hello, l -> 2
        //hello, el -> 1
        public static int CountTotal(string input, string chars, bool ignoreCases)
        {
            int count = 0;
            for (int i = 0; i < input.Length; i++)
            {
                if (!(i + chars.Length > input.Length) &&
                    string.Compare(input.Substring(i, chars.Length), chars, ignoreCases) == 0)
                {
                    count++;
                }
            }
            return count;
        }

        //Checks if the string contains numbers
        //hello -> False
        //h3llo -> True
        public static bool HasNumbers(string input)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(input, "\\d+");
        }

        //Checks if string is numbers and letters
        //Test1254 -> True
        //$chool! -> False
        public static bool IsAlphaNumberic(string input)
        {
            char currentLetter;
            for (int i = 0; i < input.Length; i++)
            {
                currentLetter = input[i];

                if (!(Convert.ToInt32(currentLetter) >= 48 && Convert.ToInt32(currentLetter) <= 57) &&
                    !(Convert.ToInt32(currentLetter) >= 65 && Convert.ToInt32(currentLetter) <= 90) &&
                    !(Convert.ToInt32(currentLetter) >= 97 && Convert.ToInt32(currentLetter) <= 122))
                {
                    //Not a number or a letter
                    return false;
                }
            }
            return true;
        }

        //Checks if a string contains only letters
        //Hi -> True
        //Hi123 -> False
        public static bool isLetters(string input)
        {
            char currentLetter;
            for (int i = 0; i < input.Length; i++)
            {
                currentLetter = input[i];

                if (!(Convert.ToInt32(currentLetter) >= 65 && Convert.ToInt32(currentLetter) <= 90) &&
                    !(Convert.ToInt32(currentLetter) >= 97 && Convert.ToInt32(currentLetter) <= 122))
                {
                    //Not a letter
                    return false;
                }
            }
            return true;
        }

        // Returns the initials of a name or sentence
        //capitalize - whether to make intials capitals
        //includeSpace - to return intials separated (True - J. S. or False - J.S.)
        //John Smith -> J. S. or J.S.
        public static string GetInitials(string input, bool capitalize, bool includeSpace)
        {
            string[] words = input.Split(new char[] { ' ' });

            for (int i = 0; i < words.Length; i++)
            {
                if (words[i].Length > 0)
                    if (capitalize)
                        words[i] = words[i].Substring(0, 1).ToUpper() + ".";
                    else
                        words[i] = words[i].Substring(0, 1) + ".";
            }

            if (includeSpace)
                return string.Join(" ", words);
            else
                return string.Join("", words);
        }

        //Very much like the GetTitle function, capitalizes the first letter of every word
        //Additional function is, mcdonald -> McDonald, machenry -> MacHenry
        //Credits to ShutlOrbit (http://www.thirdstagesoftware.com) from CodeProject
        public static string GetNameCasing(string input)
        {
            string[] words = input.Split(new char[] { ' ' });

            for (int i = 0; i < words.Length; i++)
            {
                if (words[i].Length > 0)
                {
                    words[i] = words[i].Substring(0, 1).ToUpper() + words[i].Substring(1);
                    if (words[i].StartsWith("Mc") && words[i].Length > 2)
                        words[i] = words[i].Substring(0, 2) + words[i].Substring(2, 1).ToUpper() + words[i].Substring(3);
                    else if (words[i].StartsWith("Mac") && words[i].Length > 3)
                        words[i] = words[i].Substring(0, 3) + words[i].Substring(3, 1).ToUpper() + words[i].Substring(4);
                }
            }
            return string.Join(" ", words);
        }

        //Returns all the locations of a char in a string
        //Hello, l -> 2, 3
        //Hello, o -> 4
        //Bob, 1 -> -1
        public static int[] IndexOfAll(string input, string chars)
        {
            List<int> indices = new List<int>();
            for (int i = 0; i < input.Length; i++)
            {
                if (input.Substring(i, 1) == chars)
                    indices.Add(i);
            }

            if (indices.Count == 0)
                indices.Add(-1);

            return indices.ToArray();
        }

        //Return a rating for how strong the string is as a password
        //Max rating is 100
        //Credits for original function to D. Rijmenants
        //If there are problems with copyright contact us and we will delete it
        public static int PasswordStrength(string input)
        {
            double total = 0;
            bool hasUpperCase = false;
            bool hasLowerCase = false;

            total = input.Length * 3;

            char currentLetter;
            for (int i = 0; i < input.Length; i++)
            {
                currentLetter = input[i];
                if (Convert.ToInt32(currentLetter) >= 65 && Convert.ToInt32(currentLetter) <= 92)
                    hasUpperCase = true;

                if (Convert.ToInt32(currentLetter) >= 97 && Convert.ToInt32(currentLetter) <= 122)
                    hasLowerCase = true;
            }

            if (hasUpperCase && hasLowerCase) total *= 1.2;

            for (int i = 0; i < input.Length; i++)
            {
                currentLetter = input[i];
                if (Convert.ToInt32(currentLetter) >= 48 && Convert.ToInt32(currentLetter) <= 57) //Numbers
                    if (hasUpperCase && hasLowerCase) total *= 1.4;
            }

            for (int i = 0; i < input.Length; i++)
            {
                currentLetter = input[i];
                if ((Convert.ToInt32(currentLetter) <= 47 && Convert.ToInt32(currentLetter) >= 123) ||
                    (Convert.ToInt32(currentLetter) >= 58 && Convert.ToInt32(currentLetter) <= 64)) //symbols
                {
                    total *= 1.5;
                    break;
                }
            }

            if (total > 100.0) total = 100.0;

            return (int)total;
        }

        //Splits strings, but leaves anything within quotes
        //(Has issues with nested quotes
        //This is a "very long" string ->
        //This
        //is
        //a
        //very long
        //string
        public static string[] SplitQuotes(string input, bool ignoreQuotes, string separator)
        {
            if (ignoreQuotes)
                return input.Split(separator.ToCharArray());
            else
            {
                string[] words = input.Split(separator.ToCharArray());
                List<string> newWords = new List<string>();

                for (int i = 0; i < words.Length; i++)
                {
                    //  这里可能有问题, 必须正好切分到["]之前, 这个["]才起作用, 也就是(This is a "very long" string)可以正确划分["]内的内容, 而(This is a "very long" string)不行
                    if (words[i].StartsWith('"'.ToString()))
                    {
                        List<string> linked = new List<string>();
                        for (int y = i; y < words.Length; y++)
                        {
                            if (words[y].EndsWith('"'.ToString()))
                            {
                                linked.Add(words[y].Substring(0, words[y].Length - 1));
                                i = y;
                                break;
                            }
                            else
                            {
                                if (words[y].StartsWith('"'.ToString()))
                                    linked.Add(words[y].Substring(1));
                            }
                        }
                        newWords.Add(string.Join(separator, linked.ToArray()));
                        linked.Clear();
                    }
                    else
                        newWords.Add(words[i]);
                }
                return newWords.ToArray();
            }
        }
    }
}
