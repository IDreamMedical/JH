using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.XPath;
using UniGuy.Core.DataStructures;

using System.Xml.Linq;
using System.Linq;

namespace UniGuy.Core.Extensions
{
    /// <summary>
    /// 	Extension methods for the string data type
    /// </summary>
    public static class StringExtensions
    {
        #region Common string extensions

        /// <summary>
        /// 	Determines whether the specified string is null or empty.
        /// </summary>
        /// <param name = "value">The string value to check.</param>
        public static bool IsEmpty(this string value)
        {
            return ((value == null) || (value.Length == 0));
        }

        /// <summary>
        /// 	Determines whether the specified string is not null or empty.
        /// </summary>
        /// <param name = "value">The string value to check.</param>
        public static bool IsNotEmpty(this string value)
        {
            return (value.IsEmpty() == false);
        }

        /// <summary>
        /// 	Checks whether the string is empty and returns a default value in case.
        /// </summary>
        /// <param name = "value">The string to check.</param>
        /// <param name = "defaultValue">The default value.</param>
        /// <returns>Either the string or the default value.</returns>
        public static string IfEmpty(this string value, string defaultValue)
        {
            return (value.IsNotEmpty() ? value : defaultValue);
        }

        /// <summary>
        /// 	Formats the value with the parameters using string.Format.
        /// </summary>
        /// <param name = "value">The input string.</param>
        /// <param name = "parameters">The parameters.</param>
        /// <returns></returns>
        public static string FormatWith(this string value, params object[] parameters)
        {
            return string.Format(value, parameters);
        }

        /// <summary>
        /// 	Trims the text to a provided maximum length.
        /// </summary>
        /// <param name = "value">The input string.</param>
        /// <param name = "maxLength">Maximum length.</param>
        /// <returns></returns>
        /// <remarks>
        /// 	Proposed by Rene Schulte
        /// </remarks>
        public static string TrimToMaxLength(this string value, int maxLength)
        {
            return (value == null || value.Length <= maxLength ? value : value.Substring(0, maxLength));
        }

        /// <summary>
        /// 	Trims the text to a provided maximum length and adds a suffix if required.
        /// </summary>
        /// <param name = "value">The input string.</param>
        /// <param name = "maxLength">Maximum length.</param>
        /// <param name = "suffix">The suffix.</param>
        /// <returns></returns>
        /// <remarks>
        /// 	Proposed by Rene Schulte
        /// </remarks>
        public static string TrimToMaxLength(this string value, int maxLength, string suffix)
        {
            return (value == null || value.Length <= maxLength ? value : string.Concat(value.Substring(0, maxLength), suffix));
        }

        /// <summary>
        /// 	Determines whether the comparison value strig is contained within the input value string
        /// </summary>
        /// <param name = "inputValue">The input value.</param>
        /// <param name = "comparisonValue">The comparison value.</param>
        /// <param name = "comparisonType">Type of the comparison to allow case sensitive or insensitive comparison.</param>
        /// <returns>
        /// 	<c>true</c> if input value contains the specified value, otherwise, <c>false</c>.
        /// </returns>
        public static bool Contains(this string inputValue, string comparisonValue, StringComparison comparisonType)
        {
            return (inputValue.IndexOf(comparisonValue, comparisonType) != -1);
        }

        /// <summary>
        /// 	Loads the string into a LINQ to XML XDocument
        /// </summary>
        /// <param name = "xml">The XML string.</param>
        /// <returns>The XML document object model (XDocument)</returns>
        public static XDocument ToXDocument(this string xml)
        {
            return XDocument.Parse(xml);
        }

        /// <summary>
        /// 	Loads the string into a XML DOM object (XmlDocument)
        /// </summary>
        /// <param name = "xml">The XML string.</param>
        /// <returns>The XML document object model (XmlDocument)</returns>
        public static XmlDocument ToXmlDOM(this string xml)
        {
            var document = new XmlDocument();
            document.LoadXml(xml);
            return document;
        }

        /// <summary>
        /// 	Loads the string into a XML XPath DOM (XPathDocument)
        /// </summary>
        /// <param name = "xml">The XML string.</param>
        /// <returns>The XML XPath document object model (XPathNavigator)</returns>
        public static XPathNavigator ToXPath(this string xml)
        {
            var document = new XPathDocument(new StringReader(xml));
            return document.CreateNavigator();
        }

        /// <summary>
        /// 	Reverses / mirrors a string.
        /// </summary>
        /// <param name = "value">The string to be reversed.</param>
        /// <returns>The reversed string</returns>
        public static string Reverse(this string value)
        {
            if (value.IsEmpty() || (value.Length == 1))
                return value;

            var chars = value.ToCharArray();
            Array.Reverse(chars);
            return new string(chars);
        }

        /// <summary>
        /// 	Ensures that a string starts with a given prefix.
        /// </summary>
        /// <param name = "value">The string value to check.</param>
        /// <param name = "prefix">The prefix value to check for.</param>
        /// <returns>The string value including the prefix</returns>
        /// <example>
        /// 	<code>
        /// 		var extension = "txt";
        /// 		var fileName = string.Concat(file.Name, extension.EnsureStartsWith("."));
        /// 	</code>
        /// </example>
        public static string EnsureStartsWith(this string value, string prefix)
        {
            return value.StartsWith(prefix) ? value : string.Concat(prefix, value);
        }

        /// <summary>
        /// 	Ensures that a string ends with a given suffix.
        /// </summary>
        /// <param name = "value">The string value to check.</param>
        /// <param name = "suffix">The suffix value to check for.</param>
        /// <returns>The string value including the suffix</returns>
        /// <example>
        /// 	<code>
        /// 		var url = "http://www.pgk.de";
        /// 		url = url.EnsureEndsWith("/"));
        /// 	</code>
        /// </example>
        public static string EnsureEndsWith(this string value, string suffix)
        {
            return value.EndsWith(suffix) ? value : string.Concat(value, suffix);
        }

        /// <summary>
        /// 	Tests whether the contents of a string is a numeric value
        /// </summary>
        /// <param name = "value">String to check</param>
        /// <returns>
        /// 	Boolean indicating whether or not the string contents are numeric
        /// </returns>
        /// <remarks>
        /// 	Contributed by Kenneth Scott
        /// </remarks>
        public static bool IsNumeric(this string value)
        {
            float output;
            return float.TryParse(value, out output);
        }

        /// <summary>
        /// 	Extracts all digits from a string.
        /// </summary>
        /// <param name = "value">String containing digits to extract</param>
        /// <returns>
        /// 	All digits contained within the input string
        /// </returns>
        /// <remarks>
        /// 	Contributed by Kenneth Scott
        /// </remarks>
        public static string ExtractDigits(this string value)
        {
            return string.Join(null, Regex.Split(value, "[^\\d]"));
        }

        /// <summary>
        /// 	Concatenates the specified string value with the passed additional strings.
        /// </summary>
        /// <param name = "value">The original value.</param>
        /// <param name = "values">The additional string values to be concatenated.</param>
        /// <returns>The concatenated string.</returns>
        public static string ConcatWith(this string value, params string[] values)
        {
            return string.Concat(value, string.Concat(values));
        }

        /// <summary>
        /// 	Convert the provided string to a Guid value.
        /// </summary>
        /// <param name = "value">The original string value.</param>
        /// <returns>The Guid</returns>
        public static Guid ToGuid(this string value)
        {
            return new Guid(value);
        }

        /// <summary>
        /// 	Convert the provided string to a Guid value and returns Guid.Empty if conversion fails.
        /// </summary>
        /// <param name = "value">The original string value.</param>
        /// <returns>The Guid</returns>
        public static Guid ToGuidSave(this string value)
        {
            return value.ToGuidSave(Guid.Empty);
        }

        /// <summary>
        /// 	Convert the provided string to a Guid value and returns the provided default value if the conversion fails.
        /// </summary>
        /// <param name = "value">The original string value.</param>
        /// <param name = "defaultValue">The default value.</param>
        /// <returns>The Guid</returns>
        public static Guid ToGuidSave(this string value, Guid defaultValue)
        {
            if (value.IsEmpty())
                return defaultValue;

            try
            {
                return value.ToGuid();
            }
            catch
            { }

            return defaultValue;
        }

        /// <summary>
        /// 	Gets the string before the given string parameter.
        /// </summary>
        /// <param name = "value">The default value.</param>
        /// <param name = "x">The given string parameter.</param>
        /// <returns></returns>
        public static string GetBefore(this string value, string x)
        {
            var xPos = value.IndexOf(x);
            return xPos == -1 ? String.Empty : value.Substring(0, xPos);
        }

        /// <summary>
        /// 获得某个字符串某个索引之后某个字符串之前的字符串
        /// </summary>
        /// <param name="value"></param>
        /// <param name="x"></param>
        /// <param name="start"></param>
        /// <returns></returns>
        /// <remarks>WJ 201106122304</remarks>
        /// <example>
        /// 获得"2011年06月12日" 中的月份字符串
        /// <code>
        /// "2011年06月12日".GetBefore("月", 5)
        /// </code>
        /// </example>
        public static string GetBefore(this string value, string x, int start)
        {
            var xPos = value.IndexOf(x, start);
            return xPos == -1 ? string.Empty : value.Substring(start, xPos - start);
        }

        /// <summary>
        /// 	Gets the string between the given string parameters.
        /// </summary>
        /// <param name = "value">The default value.</param>
        /// <param name = "x">The left string parameter.</param>
        /// <param name = "y">The right string parameter</param>
        /// <returns></returns>
        public static string GetBetween(this string value, string x, string y)
        {
            var xPos = value.IndexOf(x);
            var yPos = value.LastIndexOf(y);

            if (xPos == -1 || xPos == -1)
                return String.Empty;

            var startIndex = xPos + x.Length;
            return startIndex >= yPos ? String.Empty : value.Substring(startIndex, yPos - startIndex).Trim();
        }

        /// <summary>
        /// 	Gets the string after the given string parameter.
        /// </summary>
        /// <param name = "value">The default value.</param>
        /// <param name = "x">The given string parameter.</param>
        /// <returns></returns>
        public static string GetAfter(this string value, string x)
        {
            var xPos = value.LastIndexOf(x);

            if (xPos == -1)
                return String.Empty;

            var startIndex = xPos + x.Length;
            return startIndex >= value.Length ? String.Empty : value.Substring(startIndex).Trim();
        }

        /// <summary>
        /// 获得某个字符串某个索引之前某个字符串之后的字符串
        /// </summary>
        /// <param name="value"></param>
        /// <param name="x"></param>
        /// <param name="start"></param>
        /// <returns></returns>
        /// <remarks>WJ 201106122304</remarks>
        /// <example>
        /// <code>
        /// Console.WriteLine(GetAfter("abcdefg123kkfd", "fg", 10));
        /// </code>
        /// returns "123";
        /// </example>
        public static string GetAfter(this string value, string x, int end)
        {
            var xPos = value.LastIndexOf(x, end);

            if (xPos == -1)
                return String.Empty;

            var start = xPos + x.Length;
            return start >= value.Length ? String.Empty : value.Substring(start, end - start);
        }

        /// <summary>
        /// 	A generic version of System.String.Join()
        /// </summary>
        /// <typeparam name = "T">
        /// 	The type of the array to join
        /// </typeparam>
        /// <param name = "separator">
        /// 	The separator to appear between each element
        /// </param>
        /// <param name = "value">
        /// 	An array of values
        /// </param>
        /// <returns>
        /// 	The join.
        /// </returns>
        /// <remarks>
        /// 	Contributed by Michael T, http://about.me/MichaelTran
        /// </remarks>
        public static string Join<T>(string separator, T[] value)
        {
            if (value == null || value.Length == 0)
                return string.Empty;
            if (separator == null)
                separator = string.Empty;
            Converter<T, string> converter = o => o.ToString();
            return string.Join(separator, Array.ConvertAll(value, converter));
        }

        /// <summary>
        /// 	Remove any instance of the given character from the current string.
        /// </summary>
        /// <param name = "value">
        /// 	The input.
        /// </param>
        /// <param name = "removeCharc">
        /// 	The remove char.
        /// </param>
        /// <remarks>
        /// 	Contributed by Michael T, http://about.me/MichaelTran
        /// </remarks>
        public static string Remove(this string value, params char[] removeCharc)
        {
            var result = value;
            if (!string.IsNullOrEmpty(result) && removeCharc != null)
                Array.ForEach(removeCharc, c => result = result.Remove(c.ToString()));

            return result;
        }

        /// <summary>
        /// 	Remove any instance of the given string pattern from the current string.
        /// </summary>
        /// <param name = "value">
        /// 	The input.
        /// </param>
        /// <param name = "removeStrings">
        /// 	The remove Strings.
        /// </param>
        /// <remarks>
        /// 	Contributed by Michael T, http://about.me/MichaelTran
        /// </remarks>
        public static string Remove(this string value, params string[] strings)
        {
            return strings.Aggregate(value, (current, c) => current.Replace(c, string.Empty));
            //var result = value;
            //if (!string.IsNullOrEmpty(result) && removeStrings != null)
            //  Array.ForEach(removeStrings, s => result = result.Replace(s, string.Empty));

            //return result;
        }

        /// <summary>Finds out if the specified string contains null, empty or consists only of white-space characters</summary>
        /// <param name = "value">The input string</param>
        public static bool IsEmptyOrWhiteSpace(this string value)
        {
            return (value.IsEmpty() || value.All(t => char.IsWhiteSpace(t)));
        }

        /// <summary>Determines whether the specified string is not null, empty or consists only of white-space characters</summary>
        /// <param name = "value">The string value to check</param>
        public static bool IsNotEmptyOrWhiteSpace(this string value)
        {
            return (value.IsEmptyOrWhiteSpace() == false);
        }

        /// <summary>Checks whether the string is null, empty or consists only of white-space characters and returns a default value in case</summary>
        /// <param name = "value">The string to check</param>
        /// <param name = "defaultValue">The default value</param>
        /// <returns>Either the string or the default value</returns>
        public static string IfEmptyOrWhiteSpace(this string value, string defaultValue)
        {
            return (value.IsEmptyOrWhiteSpace() ? defaultValue : value);
        }

        /// <summary>Uppercase First Letter</summary>
        /// <param name = "value">The string value to process</param>
        public static string ToUpperFirstLetter(this string value)
        {
            if (value.IsEmptyOrWhiteSpace()) return string.Empty;

            char[] valueChars = value.ToCharArray();
            valueChars[0] = char.ToUpper(valueChars[0]);

            return new string(valueChars);
        }

        //todo: xml documentation requires
        //todo: unit test required
        public static byte[] GetBytes(this string data)
        {
            return Encoding.Default.GetBytes(data);
        }

        public static byte[] GetBytes(this string data, Encoding encoding)
        {
            return encoding.GetBytes(data);
        }

        /// <summary>Convert text's case to a title case</summary>
        public static string ToTitleCase(this string value)
        {
            //return IfEmptyOrWhiteSpace(value, CultureInfo.CurrentUICulture.TextInfo.ToTitleCase(value));  //  明显有问题
            return value.IsEmptyOrWhiteSpace() ? value : CultureInfo.CurrentUICulture.TextInfo.ToTitleCase(value);
        }

        public static string ToPlural(this string singular)
        {
            // Multiple words in the form A of B : Apply the plural to the first word only (A)
            int index = singular.LastIndexOf(" of ");
            if (index > 0) return (singular.Substring(0, index)) + singular.Remove(0, index).ToPlural();

            // single Word rules
            //sibilant ending rule
            if (singular.EndsWith("sh")) return singular + "es";
            if (singular.EndsWith("ch")) return singular + "es";
            if (singular.EndsWith("us")) return singular + "es";
            if (singular.EndsWith("ss")) return singular + "es";
            //-ies rule
            if (singular.EndsWith("y")) return singular.Remove(singular.Length - 1, 1) + "ies";
            // -oes rule
            if (singular.EndsWith("o")) return singular.Remove(singular.Length - 1, 1) + "oes";
            // -s suffix rule
            return singular + "s";
        }

        #endregion
        #region Regex based extension methods

        /// <summary>
        /// 	Uses regular expressions to determine if the string matches to a given regex pattern.
        /// </summary>
        /// <param name = "value">The input string.</param>
        /// <param name = "regexPattern">The regular expression pattern.</param>
        /// <returns>
        /// 	<c>true</c> if the value is matching to the specified pattern; otherwise, <c>false</c>.
        /// </returns>
        /// <example>
        /// 	<code>
        /// 		var s = "12345";
        /// 		var isMatching = s.IsMatchingTo(@"^\d+$");
        /// 	</code>
        /// </example>
        public static bool IsMatchingTo(this string value, string regexPattern)
        {
            return IsMatchingTo(value, regexPattern, RegexOptions.None);
        }

        /// <summary>
        /// 	Uses regular expressions to determine if the string matches to a given regex pattern.
        /// </summary>
        /// <param name = "value">The input string.</param>
        /// <param name = "regexPattern">The regular expression pattern.</param>
        /// <param name = "options">The regular expression options.</param>
        /// <returns>
        /// 	<c>true</c> if the value is matching to the specified pattern; otherwise, <c>false</c>.
        /// </returns>
        /// <example>
        /// 	<code>
        /// 		var s = "12345";
        /// 		var isMatching = s.IsMatchingTo(@"^\d+$");
        /// 	</code>
        /// </example>
        public static bool IsMatchingTo(this string value, string regexPattern, RegexOptions options)
        {
            return Regex.IsMatch(value, regexPattern, options);
        }

        /// <summary>
        /// 	Uses regular expressions to replace parts of a string.
        /// </summary>
        /// <param name = "value">The input string.</param>
        /// <param name = "regexPattern">The regular expression pattern.</param>
        /// <param name = "replaceValue">The replacement value.</param>
        /// <returns>The newly created string</returns>
        /// <example>
        /// 	<code>
        /// 		var s = "12345";
        /// 		var replaced = s.ReplaceWith(@"\d", m => string.Concat(" -", m.Value, "- "));
        /// 	</code>
        /// </example>
        public static string ReplaceWith(this string value, string regexPattern, string replaceValue)
        {
            return ReplaceWith(value, regexPattern, replaceValue, RegexOptions.None);
        }

        /// <summary>
        /// 	Uses regular expressions to replace parts of a string.
        /// </summary>
        /// <param name = "value">The input string.</param>
        /// <param name = "regexPattern">The regular expression pattern.</param>
        /// <param name = "replaceValue">The replacement value.</param>
        /// <param name = "options">The regular expression options.</param>
        /// <returns>The newly created string</returns>
        /// <example>
        /// 	<code>
        /// 		var s = "12345";
        /// 		var replaced = s.ReplaceWith(@"\d", m => string.Concat(" -", m.Value, "- "));
        /// 	</code>
        /// </example>
        public static string ReplaceWith(this string value, string regexPattern, string replaceValue, RegexOptions options)
        {
            return Regex.Replace(value, regexPattern, replaceValue, options);
        }

        /// <summary>
        /// 	Uses regular expressions to replace parts of a string.
        /// </summary>
        /// <param name = "value">The input string.</param>
        /// <param name = "regexPattern">The regular expression pattern.</param>
        /// <param name = "evaluator">The replacement method / lambda expression.</param>
        /// <returns>The newly created string</returns>
        /// <example>
        /// 	<code>
        /// 		var s = "12345";
        /// 		var replaced = s.ReplaceWith(@"\d", m => string.Concat(" -", m.Value, "- "));
        /// 	</code>
        /// </example>
        public static string ReplaceWith(this string value, string regexPattern, MatchEvaluator evaluator)
        {
            return ReplaceWith(value, regexPattern, RegexOptions.None, evaluator);
        }

        /// <summary>
        /// 	Uses regular expressions to replace parts of a string.
        /// </summary>
        /// <param name = "value">The input string.</param>
        /// <param name = "regexPattern">The regular expression pattern.</param>
        /// <param name = "options">The regular expression options.</param>
        /// <param name = "evaluator">The replacement method / lambda expression.</param>
        /// <returns>The newly created string</returns>
        /// <example>
        /// 	<code>
        /// 		var s = "12345";
        /// 		var replaced = s.ReplaceWith(@"\d", m => string.Concat(" -", m.Value, "- "));
        /// 	</code>
        /// </example>
        public static string ReplaceWith(this string value, string regexPattern, RegexOptions options, MatchEvaluator evaluator)
        {
            return Regex.Replace(value, regexPattern, evaluator, options);
        }

        /// <summary>
        /// 	Uses regular expressions to determine all matches of a given regex pattern.
        /// </summary>
        /// <param name = "value">The input string.</param>
        /// <param name = "regexPattern">The regular expression pattern.</param>
        /// <returns>A collection of all matches</returns>
        public static MatchCollection GetMatches(this string value, string regexPattern)
        {
            return GetMatches(value, regexPattern, RegexOptions.None);
        }

        /// <summary>
        /// 	Uses regular expressions to determine all matches of a given regex pattern.
        /// </summary>
        /// <param name = "value">The input string.</param>
        /// <param name = "regexPattern">The regular expression pattern.</param>
        /// <param name = "options">The regular expression options.</param>
        /// <returns>A collection of all matches</returns>
        public static MatchCollection GetMatches(this string value, string regexPattern, RegexOptions options)
        {
            return Regex.Matches(value, regexPattern, options);
        }

        /// <summary>
        /// 	Uses regular expressions to determine all matches of a given regex pattern and returns them as string enumeration.
        /// </summary>
        /// <param name = "value">The input string.</param>
        /// <param name = "regexPattern">The regular expression pattern.</param>
        /// <returns>An enumeration of matching strings</returns>
        /// <example>
        /// 	<code>
        /// 		var s = "12345";
        /// 		foreach(var number in s.GetMatchingValues(@"\d")) {
        /// 		Console.WriteLine(number);
        /// 		}
        /// 	</code>
        /// </example>
        public static IEnumerable<string> GetMatchingValues(this string value, string regexPattern)
        {
            return GetMatchingValues(value, regexPattern, RegexOptions.None);
        }

        /// <summary>
        /// 	Uses regular expressions to determine all matches of a given regex pattern and returns them as string enumeration.
        /// </summary>
        /// <param name = "value">The input string.</param>
        /// <param name = "regexPattern">The regular expression pattern.</param>
        /// <param name = "options">The regular expression options.</param>
        /// <returns>An enumeration of matching strings</returns>
        /// <example>
        /// 	<code>
        /// 		var s = "12345";
        /// 		foreach(var number in s.GetMatchingValues(@"\d")) {
        /// 		Console.WriteLine(number);
        /// 		}
        /// 	</code>
        /// </example>
        public static IEnumerable<string> GetMatchingValues(this string value, string regexPattern, RegexOptions options)
        {
            return from Match match in GetMatches(value, regexPattern, options)
                   where match.Success
                   select match.Value;
        }

        /// <summary>
        /// 	Uses regular expressions to split a string into parts.
        /// </summary>
        /// <param name = "value">The input string.</param>
        /// <param name = "regexPattern">The regular expression pattern.</param>
        /// <returns>The splitted string array</returns>
        public static string[] Split(this string value, string regexPattern)
        {
            return value.Split(regexPattern, RegexOptions.None);
        }

        /// <summary>
        /// 	Uses regular expressions to split a string into parts.
        /// </summary>
        /// <param name = "value">The input string.</param>
        /// <param name = "regexPattern">The regular expression pattern.</param>
        /// <param name = "options">The regular expression options.</param>
        /// <returns>The splitted string array</returns>
        public static string[] Split(this string value, string regexPattern, RegexOptions options)
        {
            return Regex.Split(value, regexPattern, options);
        }

        /// <summary>
        /// 	Splits the given string into words and returns a string array.
        /// </summary>
        /// <param name = "value">The input string.</param>
        /// <returns>The splitted string array</returns>
        public static string[] GetWords(this string value)
        {
            return value.Split(@"\W");
        }

        /// <summary>
        /// 	Gets the nth "word" of a given string, where "words" are substrings separated by a given separator
        /// </summary>
        /// <param name = "value">The string from which the word should be retrieved.</param>
        /// <param name = "index">Index of the word (0-based).</param>
        /// <returns>
        /// 	The word at position n of the string.
        /// 	Trying to retrieve a word at a position lower than 0 or at a position where no word exists results in an exception.
        /// </returns>
        /// <remarks>
        /// 	Originally contributed by MMathews
        /// </remarks>
        public static string GetWordByIndex(this string value, int index)
        {
            var words = value.GetWords();

            if ((index < 0) || (index > words.Length - 1))
                throw new IndexOutOfRangeException("The word number is out of range.");

            return words[index];
        }

        /// <summary>
        /// Removed all special characters from the string.
        /// </summary>
        /// <param name="value">The input string.</param>
        /// <returns>The adjusted string.</returns>
        [Obsolete("Please use RemoveAllSpecialCharacters instead")]
        public static string AdjustInput(this string value)
        {
            return string.Join(null, Regex.Split(value, "[^a-zA-Z0-9]"));
        }

        /// <summary>
        /// Removed all special characters from the string.
        /// </summary>
        /// <param name="value">The input string.</param>
        /// <returns>The adjusted string.</returns>
        /// <remarks>
        /// 	Contributed by Michael T, http://about.me/MichaelTran
        /// </remarks>
        public static string RemoveAllSpecialCharacters(this string value)
        {
            var sb = new StringBuilder();
            foreach (var c in value.Where(c => (c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z')))
                sb.Append(c);
            return sb.ToString();
        }

        #endregion
        #region Bytes & Base64

        /// <summary>
        /// 	Converts the string to a byte-array using the default encoding
        /// </summary>
        /// <param name = "value">The input string.</param>
        /// <returns>The created byte array</returns>
        public static byte[] ToBytes(this string value)
        {
            return value.ToBytes(null);
        }

        /// <summary>
        /// 	Converts the string to a byte-array using the supplied encoding
        /// </summary>
        /// <param name = "value">The input string.</param>
        /// <param name = "encoding">The encoding to be used.</param>
        /// <returns>The created byte array</returns>
        /// <example>
        /// 	<code>
        /// 		var value = "Hello World";
        /// 		var ansiBytes = value.ToBytes(Encoding.GetEncoding(1252)); // 1252 = ANSI
        /// 		var utf8Bytes = value.ToBytes(Encoding.UTF8);
        /// 	</code>
        /// </example>
        public static byte[] ToBytes(this string value, Encoding encoding)
        {
            encoding = (encoding ?? Encoding.Default);
            return encoding.GetBytes(value);
        }

        /// <summary>
        /// 	Encodes the input value to a Base64 string using the default encoding.
        /// </summary>
        /// <param name = "value">The input value.</param>
        /// <returns>The Base 64 encoded string</returns>
        public static string EncodeBase64(this string value)
        {
            return value.EncodeBase64(null);
        }

        /// <summary>
        /// 	Encodes the input value to a Base64 string using the supplied encoding.
        /// </summary>
        /// <param name = "value">The input value.</param>
        /// <param name = "encoding">The encoding.</param>
        /// <returns>The Base 64 encoded string</returns>
        public static string EncodeBase64(this string value, Encoding encoding)
        {
            encoding = (encoding ?? Encoding.UTF8);
            var bytes = encoding.GetBytes(value);
            return Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// 	Decodes a Base 64 encoded value to a string using the default encoding.
        /// </summary>
        /// <param name = "encodedValue">The Base 64 encoded value.</param>
        /// <returns>The decoded string</returns>
        public static string DecodeBase64(this string encodedValue)
        {
            return encodedValue.DecodeBase64(null);
        }

        /// <summary>
        /// 	Decodes a Base 64 encoded value to a string using the supplied encoding.
        /// </summary>
        /// <param name = "encodedValue">The Base 64 encoded value.</param>
        /// <param name = "encoding">The encoding.</param>
        /// <returns>The decoded string</returns>
        public static string DecodeBase64(this string encodedValue, Encoding encoding)
        {
            encoding = (encoding ?? Encoding.UTF8);
            var bytes = Convert.FromBase64String(encodedValue);
            return encoding.GetString(bytes);
        }

        #endregion

        #region WJ  //  Created 20110615 LastModified 20120203

        /// <summary>
        /// 大小写转让 aBcD123 -> AbCd123
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string ToggleCase(this string s)
        {
            char[] ar = s.ToCharArray();
            for (int i = 0; i < ar.Length; ++i)
                ar[i] = char.IsLower(ar[i]) ? char.ToUpper(ar[i]) : char.ToLower(ar[i]);
            return new string(ar);
        }

        /// <summary>
        /// 字符串是否表示数字
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static bool IsInt(this string @this)
        {
            int i;
            return int.TryParse(@this, out i);
        }

        /// <summary>
        /// 字符串是否表示双精度
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static bool IsDouble(this string @this)
        {
            double d;
            return double.TryParse(@this, out d);
        }

        /// <summary>
        /// Make a valid filename from a string
        /// 不允许在文件名中出现的字符会被替换成'_'
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string ToValidFileName(this string name)
        {
            string invalidChars = Regex.Escape(new string(Path.GetInvalidFileNameChars()));
            string invalidReStr = string.Format(@"[{0}]+", invalidChars);
            return Regex.Replace(name, invalidReStr, "_");
        }

        /// <summary>
        /// 切分字符串中的单词;
        /// 英文等按照空格,英文标点符号切分(其中标点符号保留);
        /// </summary>
        /// <param name="this"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public static string[] SplitWords(this string @this)
        {
            List<string> ret = new List<string>();

            int pos = -1;

            for (int i = 0; i < @this.Length; i++)
            {
                if (char.IsWhiteSpace(@this[i]))
                {
                    if (pos != -1)
                    {
                        ret.Add(@this.Substring(pos, i - pos));
                        pos = -1;
                    }
                }
                else if (char.IsLetter(@this[i]) || char.IsNumber(@this[i]) || @this[i] == '_')
                {
                    if (pos == -1)
                        pos = i;
                }
                else if (char.IsSeparator(@this[i]) || char.IsPunctuation(@this[i]) || char.IsSymbol(@this[i]))
                {
                    if (pos != -1)
                    {
                        ret.Add(@this.Substring(pos, i - pos));
                        pos = -1;
                    }
                    ret.Add(@this[i].ToString());
                }
            }

            return ret.ToArray();
        }

        /// <summary>
        /// 如果不为空则放回Trim后的结果, 否则直接返回null
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static string TrimIfNotNull(this string @this)
        {
            return @this == null ? null : @this.Trim();
        }

        /// <summary>
        /// 获得字符串中的参数名称:
        /// abcd{var1}def{var2} -> var1, var2
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static List<string> GetContainedParameterNames(this string @this, char leftMark, char rightMark)
        {
            List<string> results = new List<string>();
            int pos = 0;
        rp0:
            pos = @this.IndexOf(leftMark, pos);
            if (pos >= 0)
            {
                int end = @this.IndexOf(rightMark, pos);
                if (end >= 0)
                {
                    results.AddUnique(@this.Substring(pos + 1, end - pos - 1));
                    pos = end + 1;
                    goto rp0;
                }
            }

            return results;
        }

        public static List<string> GetContainedParameterNames(this string @this)
        {
            return GetContainedParameterNames(@this, '{', '}');
        }

        /// <summary>
        /// 对字符串替换参数
        /// </summary>
        /// <param name="this">要被处理的字符串</param>
        /// <param name="parameters">参数字典</param>
        /// <param name="leftMark">参数左侧标记</param>
        /// <param name="rightMark">参数右侧标记</param>
        /// <returns>处理后的字符串</returns>
        /// <remarks>这种处理方法对于@this很大的情况下效率不一定高, 也比较占内存</remarks>
        public static string ReplaceParameters(this string @this, IDictionary<string, string> parameters, string leftMark, string rightMark)
        {
            if (parameters != null)
                foreach (string key in parameters.Keys)
                    @this = @this.Replace(leftMark + key + rightMark, parameters.ContainsKey(key) ? parameters[key] : null);

            return @this;
        }
        /// <summary>
        /// 对于{parameter}参数格式的字符串替换参数方法
        /// (value为String.Empty表示删除，null表示DoNothing不替换)
        /// </summary>
        /// <param name="this"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static string ReplaceParameters(this string @this, IDictionary<string, string> parameters)
        {
            if (parameters != null)
            {
                int pos = 0;
            rp0:
                pos = @this.IndexOf('{', pos);
                if (pos >= 0)
                {
                    int end = @this.IndexOf('}', pos);
                    if (end >= 0)
                    {
                        string key = @this.Substring(pos + 1, end - pos - 1);
                        if (parameters.ContainsKey(key) && parameters[key] != null)
                        {
                            @this.Remove(pos, end - pos + 1).Insert(pos, parameters[key]);
                            pos += parameters[key].Length;
                        }
                        else
                        {
                            pos++;
                        }
                        goto rp0;
                    }
                }
            }

            return @this;
        }

        /// <summary>
        /// 分隔字符串并对结果中的每个字符串Trim
        /// </summary>
        /// <param name="this"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static string[] SplitAndTrimEach(this string @this, params char[] separator)
        {
            string[] strs = @this.Split(separator);
            for (int i = 0; i < strs.Length; i++)
                strs[i] = strs[i].Trim();

            return strs;
        }
        /// <summary>
        /// 分隔字符串并对结果中的每个字符串Trim
        /// </summary>
        /// <param name="this"></param>
        /// <param name="separator"></param>
        /// <param name="count">要返回的字符串数组中字符串的最大数量</param>
        /// <returns></returns>
        public static string[] SplitAndTrimEach(this string @this, char[] separator, int count)
        {
            string[] strs = @this.Split(separator, count);
            for (int i = 0; i < strs.Length; i++)
                strs[i] = strs[i].Trim();

            return strs;
        }
        public static string[] SplitAndTrimEach(this string @this, char[] separator, StringSplitOptions options)
        {
            string[] strs = @this.Split(separator, options);
            for (int i = 0; i < strs.Length; i++)
                strs[i] = strs[i].Trim();

            return strs;
        }
        public static string[] SplitAndTrimEach(this string @this, string[] separator, StringSplitOptions options)
        {
            string[] strs = @this.Split(separator, options);
            for (int i = 0; i < strs.Length; i++)
                strs[i] = strs[i].Trim();

            return strs;
        }

        /// <summary>
        /// 切分字符串, 引号内的将不被切分
        /// </summary>
        /// <param name="this"></param>
        /// <param name="separator"></param>
        /// <param name="quote"></param>
        /// <returns></returns>
        public static string[] SplitQuotes(this string @this, char[] separator, char[] quote)
        {
            List<string> strs = new List<string>();

            //  遍历字符的索引
            int pos = 0;
            //  最后一个分隔符的位置(-1表示无)
            int lastSepIndex = -1;
            //  最后一个引号的位置(-1表示无)
            int quoteLeft = -1;

            while (pos < @this.Length)
            {
                if (quoteLeft == -1)
                {
                    //  找到quote左符号(这里先找quote再找separator, 也就是separator和qoute同时包含的字符串将在separator中不生效)
                    if (quote.Contains(@this[pos]))
                        quoteLeft = pos;
                    //  遇到分隔符
                    else if (separator.Contains(@this[pos]))
                    {
                        strs.Add(@this.Substring(lastSepIndex + 1, pos - lastSepIndex - 1));
                        lastSepIndex = pos;
                    }
                }
                //  找到quote右符号
                else if (@this[quoteLeft] == @this[pos])
                {
                    quoteLeft = -1;
                }
                pos++;
            }
            if (lastSepIndex + 1 < @this.Length)
                strs.Add(@this.Substring(lastSepIndex + 1, pos - lastSepIndex - 1));

            return strs.ToArray();
        }

        public static string[] SplitQuotes(this string @this, params char[] separator)
        {
            return SplitQuotes(@this, separator, new char[] { '"' });
        }
        public static string[] SplitQuotes(this string @this)
        {
            return SplitQuotes(@this, new char[] { ' ' }, new char[] { '"' });
        }

        /// <summary>
        /// 切分字符串, 引号内的将不被切分
        /// </summary>
        /// <param name="this"></param>
        /// <param name="separator"></param>
        /// <param name="quote"></param>
        /// <returns></returns>
        public static string[] SplitQuotes(this string @this, string[] separator, string[] quote)
        {
            List<string> strs = new List<string>();

            //  遍历字符的索引
            int pos = 0;
            //  最后一个分隔符的位置(-1表示无)
            int lastSepIndex = -1;
            //  最后一个引号的位置(-1表示无)
            int quoteLeft = -1;
            string quoteString = null;
            string sepString = null;

            while (pos < @this.Length)
            {
                if (quoteLeft == -1)
                {
                    //  找到quote左符号(这里先找quote再找separator, 也就是separator和qoute同时包含的字符串将在separator中不生效)
                    foreach (string q in quote)
                    {
                        if (@this.Length - pos >= q.Length && @this.Substring(pos, q.Length) == q)
                        {
                            quoteLeft = pos;
                            quoteString = q;
                            goto sq0;
                        }
                    }

                    foreach (string s in separator)
                    {
                        //  遇到分隔符
                        if (@this.Length - pos >= s.Length && @this.Substring(pos, s.Length) == s)
                        {
                            sepString = s;
                            strs.Add(@this.Substring(lastSepIndex + s.Length, pos - lastSepIndex - s.Length));
                            lastSepIndex = pos;
                        }
                    }
                }
                //  找到quote右符号
                else if (@this.Length - pos >= quoteString.Length && @this.Substring(pos, quoteString.Length) == quoteString)
                {
                    quoteLeft = -1;
                }
            sq0:
                pos++;
            }

            if (lastSepIndex + sepString.Length < @this.Length)
                strs.Add(@this.Substring(lastSepIndex + sepString.Length));

            return strs.ToArray();
        }

        public static string[] SplitQuotes(this string @this, params string[] separator)
        {
            return SplitQuotes(@this, separator, new string[] { "\"" });
        }
        /// <summary>
        /// 切分字符串为二维数组(groupSeparator目前不起作用)
        /// </summary>
        /// <param name="this"></param>
        /// <param name="seperator"></param>
        /// <param name="leftGroupTag"></param>
        /// <param name="rightGroupTag"></param>
        /// <returns></returns>
        /// <example>
        /// (Id, 唯一标识),(姓名),(性别)
        /// </example>
        public static string[][] DoubleSplit(this string @this, char groupSeparator, char separator, char leftGroupTag, char rightGroupTag)
        {
            List<string[]> temp0 = new List<string[]>();

            //  遍历字符的索引
            int pos = 0;
            int lastLeftGroupTagIndex = -1;

            while (pos < @this.Length)
            {
                if (@this[pos] == leftGroupTag)
                {
                    lastLeftGroupTagIndex = pos;
                }
                else if (@this[pos] == rightGroupTag)
                {
                    if (lastLeftGroupTagIndex != -1)
                    {
                        temp0.Add(@this.Substring(lastLeftGroupTagIndex + 1, pos - lastLeftGroupTagIndex - 1).SplitAndTrimEach(separator));
                        lastLeftGroupTagIndex = -1;
                    }
                }
                pos++;
            }

            return temp0.ToArray();
        }
        public static string[][] DoubleSplit(this string @this, char separator, char leftGroupTag, char rightGroupTag)
        {
            return DoubleSplit(@this, separator, separator, leftGroupTag, rightGroupTag);
        }
        public static string[][] DoubleSplit(this string @this, char leftGroupTag, char rightGroupTag)
        {
            return DoubleSplit(@this, ',', leftGroupTag, rightGroupTag);
        }
        public static string[][] DoubleSplit(this string @this, char separator)
        {
            return DoubleSplit(@this, separator, '(', ')');
        }
        public static string[][] DoubleSplit(this string @this)
        {
            return DoubleSplit(@this, ',');
        }

        /// <summary>
        /// 以指定字符串分隔一次
        /// </summary>
        /// <param name="this">要处理的字符串</param>
        /// <param name="c">分隔字符</param>
        /// <param name="backwards">是否从后面开始取</param>
        /// <param name="trimming">是否对end进行Trim</param>
        /// <param name="end">取到的端字符串</param>
        /// <returns>剩余的字符串</returns>
        public static string SplitOne(this string @this, char c, bool backwards, bool trimming, out string end)
        {
            end = null;

            if (!backwards)
            {
                int index = @this.IndexOf(c);
                if (index == -1)
                    return @this;
                end = @this.Substring(0, index);
                end = trimming ? end.Trim() : end;
                return @this.Substring(index + 1);
            }
            else
            {
                int index = @this.LastIndexOf(c);
                if (index == -1)
                    return @this;
                end = @this.Substring(index + 1);
                end = trimming ? end.Trim() : end;
                return @this.Substring(0, index);
            }
        }
        public static string SplitOne(this string @this, char c, bool backwards, out string end)
        {
            return SplitOne(@this, c, backwards, false, out end);
        }
        public static string SplitOne(this string @this, char c, out string end)
        {
            return SplitOne(@this, c, false, out end);
        }

        /// <summary>
        /// "name:John, age:28, sex:Male" -> (name, John), (age, 28), (sex, male)
        /// </summary>
        /// <param name="this"></param>
        /// <param name="sep1"></param>
        /// <param name="sep2"></param>
        /// <returns></returns>
        public static Dictionary<string, string> ToKeyValues(this string @this, char sep, char subSep, char quot)
        {
            if (string.IsNullOrEmpty(@this))
                return null;

            Dictionary<string, string> ret = new Dictionary<string, string>();
            string[] ss = @this.SplitQuotes(new char[] { sep }, new char[] { quot });
            ss.TrimEach();
            foreach (string s in ss)
            {
                string key = s;
                int index = s.IndexOf(subSep);
                if (index >= 0)
                {
                    key = s.Substring(0, index).Trim();
                    ret[key] = s.Substring(index + 1).Trim();

                    //  如果是quot包围的值, 则去掉quot
                    if (ret[key].Length > 1 && ret[key][0] == quot && ret[key][ret[key].Length - 1] == quot)
                        ret[key] = ret[key].Substring(1, ret[key].Length - 2);
                }
                else
                    ret[key] = null;
            }

            return ret;
        }
        public static Dictionary<string, string> ToKeyValues(this string @this, char sep, char subSep)
        {
            return ToKeyValues(@this, sep, subSep, '\"');
        }

        /// <summary>
        /// 是否是空白字符串
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsWhiteSpace(this string value)
        {
            for (int i = 0; i < value.Length; i++)
            {
                if (!char.IsWhiteSpace(value[i]))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Split, 然后取某个索引的值
        /// </summary>
        /// <param name="this"></param>
        /// <param name="index"></param>
        /// <param name="sep"></param>
        /// <returns></returns>
        public static string SubSplit(this string @this, int index, string defaultSub, params char[] sep)
        {
            string[] temp = @this.SplitAndTrimEach(sep);
            return temp.Length > index ? temp[index] : defaultSub;
        }
        public static string SubSplit(this string @this, int index, params char[] sep)
        {
            return SubSplit(@this, index, null, sep);
        }

        /// <summary>
        /// 查看字符串中某个字符的所有位置
        /// </summary>
        /// <param name="this"></param>
        /// <param name="value"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static int[] IndexesOf(this string @this, char value, IEqualityComparer<char> comparer)
        {
            List<int> indexes = new List<int>();
            int index = 0;
        io0:
            index = @this.IndexOf(value, index, comparer);
            if (index >= 0)
            {
                indexes.Add(index);
                index++;
                goto io0;
            }
            return indexes.ToArray();
        }
        public static int[] IndexesOf(this string @this, char value)
        {
            List<int> indexes = new List<int>();
            int index = 0;
        io0:
            index = @this.IndexOf(value, index);
            if (index >= 0)
            {
                indexes.Add(index);
                index++;
                goto io0;
            }
            return indexes.ToArray();
        }

        public static int[] IndexesOf(this string @this, string value, StringComparison comparisonType)
        {
            List<int> indexes = new List<int>();
            int index = 0;
        io0:
            index = @this.IndexOf(value, index, comparisonType);
            if (index >= 0)
            {
                indexes.Add(index);
                index += value.Length;
                goto io0;
            }
            return indexes.ToArray();
        }

        public static int[] IndexesOf(this string @this, string value)
        {
            List<int> indexes = new List<int>();
            int index = 0;
        io0:
            index = @this.IndexOf(value, index);
            if (index >= 0)
            {
                indexes.Add(index);
                index += value.Length;
                goto io0;
            }
            return indexes.ToArray();
        }

        /// <summary>
        /// 去除字符串的Quote(比如 "abc" -> abc)
        /// </summary>
        /// <param name="this"></param>
        /// <param name="quotes"></param>
        /// <returns></returns>
        public static string DeQuote(this string @this, params char[] quotes)
        {
            foreach (char quote in quotes)
                if (@this[0] == quote && @this[@this.Length - 1] == quote)
                    return @this.Substring(1, @this.Length - 2);

            return @this;
        }

        public static string DeQuote(this string @this)
        {
            return DeQuote(@this, '\"');
        }

        /// <summary>
        /// 把字符串解析成Key和Value(Name:John->Name, John)
        /// </summary>
        /// <param name="this"></param>
        /// <param name="sep"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToKeyValue(this string @this, char sep, out string value)
        {
            int index = @this.IndexOf(sep);
            value = @this.Substring(index + 1).Trim();
            return @this.Substring(0, index).Trim();
        }

        public static string ToKeyValue(this string @this, out string value)
        {
            return ToKeyValue(@this, ':', out value);
        }
        #endregion

        #region WJ  //  Regex based extension methods

        /*
     * While participating in a forum discussion, the need to clean up HTML from "dangerous" constructs came up.
     * In the present case, it was needed to remove SCRIPT, OBJECT, APPLET, EMBBED, FRAMESET, IFRAME, FORM, INPUT, BUTTON and TEXTAREA elements (as far as I can think of) from the HTML source. Every event attribute (ONEVENT) should also be removed keep all other attributes, though.
     * HTML is very loose and extremely hard to parse. Elements can be defined as a start tag (<element-name>) and an end tag (</element-name>) although some elements don't require the end tag. If XHTML is being parsed, elements without an end tag require the tag to be terminated with /> instead of just >.
     * Attributes are not easier to parse. By definition, attribute values are required to be delimited by quotes (') or double quotes ("), but some browsers accept attribute values without any delimiter.
     * We could build a parser, but then it will become costly to add or remove elements or attributes. Using a regular expression to remove unwanted elements and attributes seems like the best option.
     * First, let's capture all unwanted elements with start and end tags. To capture these elements, we must:
     * Capture the begin tag character followed by the element name (for which we will store its name - t): <(?<t>element-name)
     * Capture optional white spaces followed by any character: (\s+.*?)?
     * Capture the end tag character: >
     * Capture optional any characters: .*?
     * Capture the begin tag character followed by closing tag character, the element name (referenced by the name - t) and the end tag character: </\k<t>>
     * <(?<t>tag-name(\s+.*?)?>.*?</\k<t>>
     * To capture all unwanted element types, we end up with the following regular expression:
     * 
     * <(?<t>script|object|applet|embbed|frameset|iframe|form|textarea)(\s+.*?)?>.*?</\k<t>>
     * Next, let's capture all unwanted elements without an end tag. To capture these elements, we must:
     * Capture the begin tag character followed by the element name: <element-name
     * Capture optional white spaces followed by any character: (\s+.*?)?
     * Capture an optional closing tag character: /?
     * Capture the end tag character: >
     * 
     * <tag-name(\s+.*?)?/?>
     * To capture all unwanted element types, we end up with the following regular expression:
     * 
     * <(script|object|applet|embbed|frameset|
     * iframe|form|textarea|input|button)(\s+.*?)?/?>
     * To remove those unwanted elements from the source HTML, we can combine these two previous regular expressions into one and replace any match with an empty string:
     * 
     * Regex.Replace(
     * sourceHtml,
     * "|(<(?<t>script|object|applet|embbed|frameset|
     * iframe|form|textarea)(\\s+.*?)?>.*?</\\k<t>>)"
     *  + "|(<(script|object|applet|embbed|frameset|iframe|
     *  form|input|button|textarea)(<a>\\s+.*?)?/?>)"</a>    ,
     *  string.Empty);
     *  And finally, the unwanted attributes. This one is trickier because we want to capture unwanted attributes inside an element's start tag. To achieve that, we need to match an element's opening tag and capture all attribute definitions. To capture these attributes, we must:
     *  Match but ignore the begin tag character followed by any element name: (?<=<\w+)
     *  Match all:
     *  Don’t capture mandatory with spaces: (?:\s+)
     *  Capture attribute definition:
     *  Capture mandatory attribute name: \w+
     *  Capture mandatory equals sign: =
     *  Capture value specification in one of the forms:
     *  Capture double quoted value: "[^"]*"
     *  Capture single quoted value: '[^']*'
     *  Capture unquoted value: .*?
     *  Match but ignore end tag: (?=/?>)
     *  
     * (?<=<\w+)((?:\s+)(\w+=(("
     * [^"]*")|('[^']*')|(.*?)))*(?=/?>)
     * The problem with the previous regular expression is that it matches the start tag and captures the whole list of attributes and not each unwanted attribute by itself. This prevents us from from replacing each match with a fixed value (empty string).
     * To solve this, we have to name what we want to capture and use the Replace overload that uses a MatchEvaluator.
     * We could capture unwanted attributes as we did for the unwanted elements, but then we would need to remove them from the list of all the element’s attributes. Instead, we’ll capture the wanted attributes and build the list of attributes. To identify the wanted attributes, we’ll need to name them (a). The resulting code will be something like this:
     * To avoid parsing the source HTML more than once, we can combine all the regular expressions into a single one.
     * Because we are still outputting only the wanted attributes, there’s no change to the match evaluator.
     * A few options (RegexOptions) will also be added to increase functionality and performance:
     * IgnoreCase: For case-insensitive matching
     * CultureInvariant: For ignoring cultural differences in language
     * Multiline: For multiline mode
     * ExplicitCapture: For capturing only named captures
     * Compiled: For compiling the regular expression into an assembly. Only if the regular expression is to be used many times.
     * The resulting code will be this:
     */
        public static string ClearHtml(this string @this)
        {
            return Regex.Replace(@this,
                "(<(?<t>script|object|applet|embbed|frameset|iframe|form|textarea)(\\s+.*?)?>.*?</\\k<t>>)"
                + "|(<(script|object|applet|embbed|frameset|iframe|form|input|button|textarea)(\\s+.*?)?/?>)"
                + "|((?<=<\\w+)((?:\\s+)((?:on\\w+=((\"[^\"]*\")|('[^']*')|(.*?)))|(?<a>(?!on)\\w+=((\"[^\"]*\")|('[^']*')|(.*?)))))*(?=/?>))",
                match =>
                {
                    if (!match.Groups["a"].Success)
                    {
                        return string.Empty;
                    }

                    var attributesBuilder = new StringBuilder();
                    foreach (Capture capture in match.Groups["a"].Captures)
                    {
                        attributesBuilder.Append(' ');
                        attributesBuilder.Append(capture.Value);
                    }
                    return attributesBuilder.ToString();
                },
                RegexOptions.IgnoreCase
                | RegexOptions.Multiline
                | RegexOptions.ExplicitCapture
                | RegexOptions.CultureInvariant
                | RegexOptions.Compiled
                );
        }
        /*
         * This was not extensively tested and there might be some wanted HTML removed and some unwanted HTML kept, but it’s probably very close to a good solution.
         */
        #endregion //   WJ  //  Regex based extension methods

        #region WJ //   Seldom
        /// <summary>
        /// "高淳县人民医院" -> "高 淳 县 人 民 医 院"
        /// </summary>
        /// <param name="this"></param>
        /// <param name="sep"></param>
        /// <returns></returns>
        public static string Intersperse(this string @this, char sep)
        {
            if (string.IsNullOrEmpty(@this))
                return @this;

            char[] cs = new char[2 * @this.Length - 1];
            for (int i = 0; i < @this.Length - 1; i++)
            {
                cs[i * 2] = @this[i];
                cs[i * 2 + 1] = sep;
            }
            cs[cs.Length - 1] = @this[@this.Length - 1];
            return new string(cs);
        }
        public static string Intersperse(this string @this, string sep)
        {
            if (string.IsNullOrEmpty(@this))
                return @this;

            char[] cs = new char[(1 + sep.Length) * @this.Length - sep.Length];
            for (int i = 0; i < @this.Length - 1; i++)
            {
                cs[i * (1 + sep.Length)] = @this[i];
                sep.CopyTo(0, cs, i * (1 + sep.Length) + 1, sep.Length);
            }
            cs[cs.Length - 1] = @this[@this.Length - 1];
            return new string(cs);
        }
        public static string Intersperse(this string @this)
        {
            return Intersperse(@this, ' ');
        }

        /// <summary>
        /// 判断两个字符串是否相等, null和""看做相等, 比如在验证密码时该方法可能有用
        /// </summary>
        /// <param name="this">本字符串</param>
        /// <param name="str">其他字符串</param>
        /// <returns>是否一致</returns>
        public static bool EqualsNullAsEmpty(this string @this, string str)
        {
            if (@this == str)
                return true;
            return string.IsNullOrEmpty(@this) && string.IsNullOrEmpty(str);
        }
        #endregion WJ   //  Seldom

        #region WJ  //  General
        //  20120223
        public static bool In(this string @this, params string[] strs)
        {
            return strs.Contains(@this);
        }

        /// <summary>
        /// 把字符串中的多个连续空格，\n和\r替换为单个
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static string TrimToNormal(this string @this)
        {
            @this = @this.Trim();
            string[] trimmedStrs = { Environment.NewLine, "\r", "\n", "\r\n", "\n\r", " " };
            StringBuilder sb = new StringBuilder();
            int pos = 0;
            string currentTrimmedStr = null;
        ttn0:
            while (pos < @this.Length)
            {
                foreach (string ts in trimmedStrs)
                {
                    if (pos + ts.Length < @this.Length)
                    {
                        if (@this.Substring(pos, ts.Length) == ts)
                        {
                            if (currentTrimmedStr != ts)
                            {
                                sb.Append(ts);
                                currentTrimmedStr = ts;
                            }
                            pos += ts.Length;
                            goto ttn0;
                        }
                    }
                }
                currentTrimmedStr = null;
                sb.Append(@this[pos]);
                pos++;
            }

            return sb.ToString();
        }

        /// <summary>
        /// 获得下一个词
        /// </summary>
        /// <param name="this"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static string GetNextWord(this string @this, ref int index)
        {
            int pos = index;
            while (pos < @this.Length && char.IsWhiteSpace(@this[pos]))   //  TODO, 可能还要跳过其他情况
                pos++;

            index = pos;
            if (index == @this.Length)
                return null;

            if (char.IsLetterOrDigit(@this[index]) || @this[index] == '_')
                while (index < @this.Length && (char.IsLetterOrDigit(@this[index]) || @this[index] == '_'))
                    index++;
            else if (char.IsSymbol(@this[index]))
                while (index < @this.Length && char.IsSymbol(@this[index]))
                    index++;
            else if (char.IsSeparator(@this[index]) || char.IsPunctuation(@this[index]))
                index++;

            return @this.Substring(pos, index - pos);
        }

        /// <summary>
        /// 获得前一个词
        /// </summary>
        /// <param name="this"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static string GetPreviousWord(this string @this, ref int index)
        {
            int pos = index;
            while (pos - 1 >= 0 && char.IsWhiteSpace(@this[pos - 1]))   //  TODO, 可能还要跳过其他情况
                pos--;

            index = pos;
            if (index == 0)
                return null;

            if (char.IsLetterOrDigit(@this[index - 1]) || @this[index - 1] == '_')
                while (index - 1 >= 0 && (char.IsLetterOrDigit(@this[index - 1]) || @this[index - 1] == '_'))
                    index--;
            else if (char.IsSymbol(@this[index - 1]))
                while (index - 1 >= 0 && char.IsSymbol(@this[index - 1]))
                    index--;
            else if (char.IsSeparator(@this[index - 1]) || char.IsPunctuation(@this[index - 1]))
                index--;

            return @this.Substring(index, pos - index);
        }
        #endregion

        #region Codeproject
        #region Rive
        /*
     * Introduction
     * This is an Extension Method which will split a string into substrings much like String.Split only better -- when requested, it will not split on delimiters within quotes or ones that have been "escaped".
     * Background
     * Often when I have to split a string -- from a CSV file or a command line perhaps -- I can't use Split because the values may contain the delimiter character. Many years ago, I wrote a string splitter function in C and later ported it to C#, but I haven't been very happy with it. This week I decided to begin afresh and write a new version. This version doesn't have all the features of the old one, but it is easier to read and has more flexibility than Split does.
     * Option enumeration
     * As with Split, an enumeration controls which features to use during the split operation; however, Rive supports more options -- specifically the ability to ignore delimiters within quotes. I also threw in the ability to escape characters so they won't be treated as delimiters or quotes.
     */
        /// <summary>
        /// Options for use with Rive.
        /// </summary>
        [FlagsAttribute()]
        public enum RiveOption
        {
            /**
            <summary>
                No options.
            </summary>
            */
            None = 0
        ,
            /**
            <summary>
                Do not include empty substrings.
            </summary>
            */
            RemoveEmptyEntries = 1
        ,
            /**
            <summary>
                Treat a special character following a backslash (\) as a regular character.
            </summary>
            */
            HonorEscapes = 2
        ,
            /**
            <summary>
                Do not split on delimiters within quotes (").
            </summary>
            */
            HonorQuotes = 4
        ,
            /**
            <summary>
                Do not split on delimiters within apostrophes (').
            </summary>
            */
            HonorApostrophes = 8
        }
        /*
        DoRive

        DoRive behaves much like Split except that it returns an IList<string> rather than a string[], and has additional features.

        The default delimiters are as documented for String.Split.
        If Count is zero (0), then an empty collection is returned.
        If Count is one (1), then the original string is returned unchanged.
        Otherwise, iterate the string, checking for delimiters and other characters as requested.
        If Count-1 substrings have been produced, then the rest of the string becomes the final substring.
        The additional features are straight-forward:

        If HonorEscapes is specified and a backslash (\) is encountered, then the following character is copied intact.
        If HonorQuotes is specified and a quote (") is encountered, then the characters up to the next quote are copied intact.
        If HonorApostrophes is specified and an apostrophe (') is encountered, then the characters up to the next apostrophe are copied intact.
        Backslashes, Quotes, and Apostrophes may be escaped.
         */
        public static System.Collections.Generic.IList<string> Rive(this string @this, int count, RiveOption options, char[] delimiters)
        {
            List<string> result = new List<string>();

            if (count > 1)
            {
                StringBuilder temp = new StringBuilder();

                HashSet<char> delims = new HashSet<char>();

                if (delimiters != null)
                    delims.UnionWith(delimiters);

                char[] defaultdelimiters = new char[] { ' ', ',' };
                if (delims.Count == 0)
                    delims.UnionWith(defaultdelimiters);

                bool remove = (options & RiveOption.RemoveEmptyEntries) == RiveOption.RemoveEmptyEntries;
                bool escape = (options & RiveOption.HonorEscapes) == RiveOption.HonorEscapes;
                bool quote = (options & RiveOption.HonorQuotes) == RiveOption.HonorQuotes;
                bool apos = (options & RiveOption.HonorApostrophes) == RiveOption.HonorApostrophes;

                char ch;
                int pos = 0;
                int len = @this.Length;

                while (pos < len)
                {
                    ch = @this[pos++];

                    if (delims.Contains(ch))
                    {
                        if ((temp.Length > 0) || !remove)
                        {
                            result.Add(temp.ToString());
                            temp.Length = 0;
                            if ((result.Count == count - 1) && (pos < len))
                            {
                                temp.Append(@this.Substring(pos));
                                pos = len;
                            }
                        }
                    }
                    else
                    {
                        if (escape && (ch == '\\') && (pos < len))
                        {
                            temp.Append(ch);
                            ch = @this[pos++];
                        }
                        else if (quote && (ch == '\"') && (pos < len))
                        {
                            do
                            {
                                if (escape && (ch == '\\'))
                                {
                                    temp.Append(ch);
                                    ch = @this[pos++];
                                }
                                temp.Append(ch);
                                ch = @this[pos++];
                            }
                            while ((pos < len) && (ch != '\"'));
                        }
                        else if (apos && (ch == '\'') && (pos < len))
                        {
                            do
                            {
                                if (escape && (ch == '\\'))
                                {
                                    temp.Append(ch);
                                    ch = @this[pos++];
                                }
                                temp.Append(ch);
                                ch = @this[pos++];
                            }
                            while ((pos < len) && (ch != '\''));
                        }
                        temp.Append(ch);
                    }
                }

                if ((temp.Length > 0) || !remove)
                {
                    result.Add(temp.ToString());
                }
            }
            else if (count == 1)
            {
                result.Add(@this);
            }

            return (result.AsReadOnly());
        }
        #endregion //   Rive
        #endregion //   Codeproject

        #region //  Edge.Extensions
        //  http://edgeextensions.codeplex.com/SourceControl/list/changesets

        /// <summary>
        /// Base64 decodes a <see cref="string">string</see>.
        /// </summary>
        /// <param name="s">A base64 encoded <see cref="string">string</see>.</param>
        /// <returns>A decoded <see cref="string">string</see>.</returns>
        /// <example>
        ///   <para>The following example encodes a <see cref="string">string</see> and decodes it back to its original value.</para>
        ///   <code language="c#">
        ///     string encoded = "Hello, World!".Base64Encode();   
        ///     string decoded = encoded.Base64Decode();
        ///   </code>
        /// </example>
        public static string Base64Decode(this string s)
        {
            byte[] decbuff = Convert.FromBase64String(s);

            return System.Text.Encoding.UTF8.GetString(decbuff);
        }

        /// <summary>
        /// Base64 encodes a <see cref="string">string</see>.
        /// </summary>
        /// <param name="s">An input <see cref="string">string</see>.</param>
        /// <returns>A base64 encoded <see cref="string">string</see>.</returns>
        /// <example>
        ///   <para>The following example encodes a <see cref="string">string</see> and decodes it back to its original value.</para>
        ///   <code language="c#">
        ///     string encoded = "Hello, World!".Base64Encode();   
        ///     string decoded = encoded.Base64Decode();
        ///   </code>
        /// </example>
        public static string Base64Encode(this string s)
        {
            byte[] encbuff = System.Text.Encoding.UTF8.GetBytes(s);

            return Convert.ToBase64String(encbuff);
        }

        /// <summary>
        /// Gets the number of instances of specified character in the <see cref="string">string</see>.
        /// </summary>
        /// <param name="value">The <see cref="String">String</see> to evaulate.</param>
        /// <param name="character">The value to look for in the <paramref name="value">value</paramref>.</param>
        /// <returns>The number of instances of the specified character in the specified <see cref="string">string</see>.</returns>
        /// <example>
        ///   <code language="c#">
        ///     string value = "Hello, World!";
        ///     string character = "l";
        ///     int charCount = value.CharacterInstanceCount(character);
        ///   </code>
        /// </example>
        public static int CharacterInstanceCount(this string value, string character)
        {
            int intReturnValue = 0;

            for (int intCharacter = 0; intCharacter <= (value.Length - 1); intCharacter++)
            {
                if (value.Substring(intCharacter, 1) == character)
                {
                    intReturnValue += 1;
                }
            }

            return intReturnValue;
        }

        /// <summary>
        /// An overload of the built-in .NET String.Contains() method, this method determines whether a substring exists 
        /// within a <see cref="string">string</see> in an optionally case-insensitive way.
        /// </summary>
        /// <param name="s"><see cref="string">String</see> to search.</param>
        /// <param name="subString">Substring to match when searching.</param>
        /// <param name="caseSensitive">Determines whether or not to ignore case.</param>
        /// <returns>Indicator of substring presence within the <see cref="string">string</see>.</returns>
        /// <example>
        ///   <code language="c#">
        ///     string s = "Hello, World!";
        ///     bool valid = s.Contains("hello", true); -> returns false
        ///   </code>
        /// </example>
        public static bool Contains(this string s, string subString, bool caseSensitive)
        {
            if (caseSensitive)
            {
                return s.Contains(subString);
            }
            else
            {
                return s.ToLower().IndexOf(subString.ToLower(), 0) >= 0;
            }
        }

        /// <summary>
        /// Determines whether the ending of this instance matches any of the specified strings.  Case sensitive.
        /// </summary>
        /// <param name="s"><see cref="string">String</see> to inspect.</param>
        /// <param name="values">The <see cref="string">string</see> instances to compare.</param>
        /// <returns>Indicator of presence of any of the supplied values at the end of the <see cref="string">string</see>.</returns>
        /// <example>
        ///     <code language="c#">
        ///         string s = "Hello, World!";
        ///         List&lt;string&gt; values = new List&lt;string&gt;();
        ///         values.AddRange("hello", "!", "World");
        ///         bool result = s.EndsWithAny(values);
        ///     </code>
        /// </example>
        public static bool EndsWithAny(this string s, List<string> values)
        {
            return s.EndsWithAny(values, true);
        }

        /// <summary>
        /// Determines whether the ending of this <see cref="string">string</see> instance matches any of the specified strings.  
        /// Optionally allows case sensitivity to be specified.
        /// </summary>
        /// <param name="s">String to inspect.</param>
        /// <param name="values">The System.Strings to compare.</param>
        /// <param name="ignoreCase">True to ignore case when comparing this string and value.  Otherwise false.</param>
        /// <returns>Indicator of presence of any of the supplied values at the end of the string.</returns>
        /// <example>
        ///     <code language="c#">
        ///         string s = "Hello, World!";
        ///         List&lt;string&gt; values = new List&lt;string&gt;();
        ///         values.AddRange("hello", "!", "World");
        ///         bool result = s.EndsWithAny(values, false);
        ///     </code>
        /// </example>
        public static bool EndsWithAny(this string s, List<string> values, bool ignoreCase)
        {
            return s.EndsWithAny(values, ignoreCase, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Determines whether the ending of this <see cref="string">string</see> instance matches any of the specified strings. 
        /// Optionally allows case sensitivity to be specified. 
        /// Optionally allows the culture to be specified.
        /// </summary>
        /// <param name="s"><see cref="string">String</see> to inspect.</param>
        /// <param name="values">The System.Strings to compare.</param>
        /// <param name="ignoreCase">True to ignore case when comparing this string and value.  Otherwise false.</param>
        /// <param name="culture">Culteral information that determines how this string and value are compared.</param>
        /// <returns>Indicator of presence of any of the supplied values at the end of the string.</returns>
        /// <example>
        ///     <code language="c#">
        ///         string s = "Hello, World!";
        ///         List&lt;string&gt; values = new List&lt;string&gt;();
        ///         values.AddRange("hello", "!", "World");
        ///         bool result = s.EndsWithAny(values, true, CultureInfo.CurrentCulture);
        ///     </code>
        /// </example>
        public static bool EndsWithAny(this string s, List<string> values, bool ignoreCase, CultureInfo culture)
        {
            foreach (string value in values)
            {
                if (s.EndsWith(value, ignoreCase, culture))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Removes all the words passed in the filter words parameters. The replace is NOT case sensitive.
        /// </summary>
        /// <param name="s">The <see cref="string">string</see> to search.</param>
        /// <param name="filterWords">The words to repace in the input <see cref="string">string</see>.</param>
        /// <returns>The resulting <see cref="string">string</see>.</returns>
        /// <example>
        ///     <code language="c#">
        ///         string s = "This is a test sentence.";
        ///         string[] filterWords = new string[] { " is a", " sentence" };
        ///         string results = s.FilterWords(filterWords); -> results in "This test."
        ///     </code>
        /// </example>
        public static string FilterWords(this string s, params string[] filterWords)
        {
            return s.FilterWords(char.MinValue, filterWords);
        }

        /// <summary>
        /// Removes all the words passed in the filter words parameters using the specified mask. 
        /// The replace is NOT case sensitive.
        /// </summary>
        /// <param name="s">The <see cref="string">string</see> to search.</param>
        /// <param name="mask">A character that is inserted for each letter of the replaced word.</param>
        /// <param name="filterWords">The words to repace in the input <see cref="string">string</see>.</param>
        /// <returns>The resulting <see cref="string">string</see>.</returns>
        /// <example>
        ///     <code language="c#">
        ///         string s = "This is a test sentence.";
        ///         char mask = '*';
        ///         string[] filterWords = new string[] { "test" };
        ///         actual = s.FilterWords(mask, filterWords); -> results in "This is a **** sentence."
        ///     </code>
        /// </example>
        public static string FilterWords(this string s, char mask, params string[] filterWords)
        {
            string stringMask = mask == char.MinValue ? string.Empty : mask.ToString();
            string totalMask = stringMask;

            foreach (string word in filterWords)
            {
                Regex regEx = new Regex(word, RegexOptions.IgnoreCase | RegexOptions.Multiline);

                if (stringMask.Length > 0)
                {
                    for (int i = 1; i < word.Length; i++)
                    {
                        totalMask += stringMask;
                    }
                }

                s = regEx.Replace(s, totalMask);

                totalMask = stringMask;
            }

            return s;
        }

        /// <summary>
        /// Finds strings that exist between the indicated start and end <see cref="string">string</see> patterns.
        /// </summary>
        /// <param name="s"><see cref="string">String</see> to evaluate.</param>
        /// <param name="startString"><see cref="string">String</see> that should precede the match.</param>
        /// <param name="endString"><see cref="string">String</see> that should follow the match.</param>
        /// <returns>Match collection of found strings.</returns>
        /// <example>
        ///     <code language="c#">
        ///         string s = "Hello, <span>World</span>!";
        ///         string startString = "<span>";
        ///         string endString = "</span>";
        ///         MatchCollection matches = s.FindBetween(startString, endString);
        ///         string firstMatch;
        ///         if (matches.Count > 0)
        ///         {
        ///             firstMatch = matches[0].ToString();
        ///         }
        ///     </code>
        /// </example>
        public static MatchCollection FindBetween(this string s, string startString, string endString)
        {
            return s.FindBetween(startString, endString, true);
        }

        /// <summary>
        /// Finds strings that exist between the indicated start and end <see cref="string">string</see> patterns.  Optionally recursive - if true,
        /// finds the last instance of the start <see cref="string">string</see> before the end <see cref="string">string</see> prior to retrieving the results.
        /// </summary>
        /// <param name="s"><see cref="string">String</see> to evaluate.</param>
        /// <param name="startString"><see cref="string">String</see> that should precede the match.</param>
        /// <param name="endString"><see cref="string">String</see> that should follow the match.</param>
        /// <param name="recursive">If true, finds the last instance of the start <see cref="string">string</see>.  Otherwise it uses the first instance.</param>
        /// <returns>Match collection of found strings.</returns>
        /// <example>
        ///     <code language="c#">
        ///         string s = "Hello, <span>World</span>!";
        ///         string startString = "<span>";
        ///         string endString = "</span>";
        ///         MatchCollection matches = s.FindBetween(startString, endString, true);
        ///         string firstMatch;
        ///         if (matches.Count > 0)
        ///         {
        ///             firstMatch = matches[0].ToString();
        ///         }
        ///     </code>
        /// </example>
        public static MatchCollection FindBetween(this string s, string startString, string endString, bool recursive)
        {
            MatchCollection matches;

            startString = Regex.Escape(startString);
            endString = Regex.Escape(endString);

            Regex regex = new Regex("(?<=" + startString + ").*(?=" + endString + ")");

            matches = regex.Matches(s);

            if (!recursive)
            {
                return matches;
            }

            if (matches.Count > 0)
            {
                if (matches[0].ToString().IndexOf(Regex.Unescape(startString)) > -1)
                {
                    s = matches[0].ToString() + Regex.Unescape(endString);

                    return s.FindBetween(Regex.Unescape(startString), Regex.Unescape(endString));
                }
                else
                {
                    return matches;
                }
            }
            else
            {
                return matches;
            }
        }

        /// <summary>
        /// Prepends a prefix to a <see cref="string">string</see> if it doesn't already exist.
        /// </summary>
        /// <param name="s">The <see cref="string">string</see> to alter.</param>
        /// <param name="prefix">The prefix to prepend.</param>
        /// <returns>The resulting <see cref="string">string</see>.</returns>
        /// <example>
        ///     <code language="c#">
        ///         string s = " World!";
        ///         string prefix = "Hello,";
        ///         string results = s.ForcePrefix(prefix); -> results in "Hello, World!"
        ///     </code>
        /// </example>
        public static string ForcePrefix(this string s, string prefix)
        {
            string result = s;

            if (!result.StartsWith(prefix))
            {
                result = prefix + result;
            }

            return result;
        }

        /// <summary>
        /// Appends a suffix to a <see cref="string">string</see> if it doesn't already exist.
        /// </summary>
        /// <param name="s"><see cref="string">String</see> to alter.</param>
        /// <param name="suffix">The suffix to append.</param>
        /// <returns>The resulting <see cref="string">string</see>.</returns>
        /// <example>
        ///     <code language="c#">
        ///         string s = "Hello, ";
        ///         string suffix = "World!";
        ///         string results = s.ForceSuffix(suffix); -> results in "Hello, World!"
        ///     </code>
        /// </example>
        public static string ForceSuffix(this string s, string suffix)
        {
            string result = s;

            if (!result.EndsWith(suffix))
            {
                result += suffix;
            }

            return result;
        }

        /// <summary>
        /// Attempts to format a phone number to a (xxx) xxx-xxxx format.
        /// </summary>
        /// <param name="value">The phone number to format.</param>
        /// <returns>The formatted phone number.</returns>
        /// <example>
        ///     <code language="c#">
        ///         string formatted = "4065557485".FormatPhoneNumber();
        ///     </code>
        /// </example>
        public static string FormatPhoneNumber(this string value)
        {
            string strReturnValue = string.Empty;

            if (value.Contains("(") == false && value.Contains(")") == false && value.Contains("-") == false)
            {
                if (value.Length == 7)
                {
                    strReturnValue = value.Substring(0, 3) + "-" + value.Substring(3, 4);
                }
                else if (value.Length == 10)
                {
                    strReturnValue = "(" + value.Substring(0, 3) + ") " + value.Substring(3, 3) + "-" + value.Substring(6, 4);
                }
                else if (value.Length > 10)
                {
                    string strExtensionFormat = string.Empty.PadLeft(value.Length - 10, Convert.ToChar("#"));

                    strReturnValue = "(" + value.Substring(0, 3) + ") " + value.Substring(3, 3) + "-" + value.Substring(6, 4) + " x" + value.Substring(10);
                }
            }
            else
            {
                strReturnValue = value;
            }

            return strReturnValue;
        }

        /// <summary>
        /// Detects if a <see cref="string">string</see> can be parsed to a valid date.
        /// </summary>
        /// <param name="value">Value to inspect.</param>
        /// <returns>Whether or not the <see cref="string">string</see> is formatted as a date.</returns>
        /// <example>
        ///     <code language="c#">
        ///         bool isDate = "12/31/1971".IsDate();
        ///     </code>
        /// </example>
        public static bool IsDate(this string value)
        {
            DateTime dt;
            return DateTime.TryParse(value, out dt);
        }

        /// <summary>
        /// Determines whether the specified <see cref="string">string</see> is null or empty.
        /// </summary>
        /// <param name="s">The <see cref="string">string</see> value to check.</param>
        /// <returns>Boolean indicating whether the <see cref="string">string</see> is null or empty.</returns>
        /// <example>
        ///     <code language="c#">
        ///         if (!myString.IsEmpty())
        ///         {
        ///             doStuff(myString);
        ///         }
        ///     </code>
        /// </example>
        public static bool IsEmpty_2(this string s)
        {
            return (s == null) || (s.Length == 0);
        }

        /// <summary>
        /// Indicates whether the specified System.String object is
        /// null or an System.String.Empty string.
        /// </summary>
        /// <param name="s">String to evaluate.</param>
        /// <returns>Indicator of whether the string is null or empty.</returns>
        /// <example>
        ///     <code language="c#">
        ///         if (!myString.IsNullOrEmpty())
        ///         {
        ///             doStuff(myString);
        ///         }
        ///     </code>
        /// </example>
        public static bool IsNullOrEmpty(this string s)
        {
            return string.IsNullOrEmpty(s);
        }

        /// <summary>
        /// Determines if a <see cref="string">string</see> can be converted to an integer.
        /// </summary>
        /// <param name="s">The <see cref="string">string</see> to examine.</param>
        /// <returns>True if the <see cref="string">string</see> is numeric.</returns>
        /// <example>
        ///     <code language="c#">
        ///         if (myString.IsNumeric())
        ///         {
        ///             int myInt = myString.ToInt32();
        ///         }
        ///     </code>
        /// </example>
        public static bool IsNumeric_2(this string s)
        {
            Regex regularExpression = new Regex("^-[0-9]+$|^[0-9]+$");

            return regularExpression.Match(s).Success;
        }

        /// <summary>
        /// Detects whether this instance is a valid email credit card number format.
        /// </summary>
        /// <param name="s">Credit card number to test.</param>
        /// <returns>True if instance is valid credit card format.</returns>
        /// <example>
        ///     <code language="c#">
        ///         if (TextCardNumber.Text.IsValidCreditCardNumber())
        ///         {
        ///             ProcessCard();
        ///         }
        ///     </code>
        /// </example>
        public static bool IsValidCreditCardNumber(this string s)
        {
            return new Regex(@"^(\d{4}-){3}\d{4}$|^(\d{4} ){3}\d{4}$|^\d{16}$").IsMatch(s);
        }

        /// <summary>
        /// Detects whether this instance is a valid email address.
        /// </summary>
        /// <param name="s">Email address to test.</param>
        /// <returns>True if instance is valid email address.</returns>
        /// <example>
        ///     <code language="c#">
        ///         if (TextEmail.Text.IsValidEmailAddress())
        ///         {
        ///             ProcessRegistration();
        ///         }
        ///     </code>
        /// </example>
        public static bool IsValidEmailAddress(this string s)
        {
            return new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,6}$").IsMatch(s);
        }

        /// <summary>
        /// Detects whether the supplied <see cref="string">string</see> is a valid IP address.
        /// </summary>
        /// <param name="s">The <see cref="string">string</see> to inspect.</param>
        /// <returns>Results of the check.</returns>
        /// <example>
        ///     <code language="c#">
        ///         bool isValid = ipAddress.IsValidIPAddress();
        ///     </code>
        /// </example>
        public static bool IsValidIPAddress(this string s)
        {
            return Regex.IsMatch(s, @"\b(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\b");
        }

        /// <summary>
        /// Checks if url is valid.
        /// </summary>
        /// <param name="url">Url to use in the check.</param>
        /// <returns>True if the url is valid.</returns>
        /// <example>
        ///     <code language="c#">
        ///         bool isValid = TextBoxUrl.Text.IsValidUrl();
        ///     </code>
        /// </example>
        public static bool IsValidUrl(this string url)
        {
            string strRegex = @"(file|gopher|news|nntp|telnet|http|ftp|https|ftps|sftp):\/\/"
                + "?(([0-9a-z_!~*'().&=+$%-]+: )?[0-9a-z_!~*'().&=+$%-]+@)?" // user@
                + @"(([0-9]{1,3}\.){3}[0-9]{1,3}" // IP- 199.194.52.184
                + "|" // allows either IP or domain
                + @"([0-9a-z_!~*'()-]+\.)*" // tertiary domain(s)- www.
                + @"([0-9a-z][0-9a-z-]{0,61})?[0-9a-z]" // second level domain
                + @"(\.[a-z]{2,6})?)" // first level domain- .com or .museum is optional
                + "(:[0-9]{1,5})?" // port number- :80
                + "((/?)|" // a slash isn't required if there is no file name
                + "(/[0-9a-z_!~*'().;?:@&=+$,%#-]+)+/?)$";

            return new Regex(strRegex).IsMatch(url);
        }

        /// <summary>
        /// Retrieves the left x characters of a <see cref="string">string</see>.
        /// </summary>
        /// <param name="s">The <see cref="string">string</see> to examine.</param>
        /// <param name="count">The number of characters to retrieve.</param>
        /// <returns>The resulting substring.</returns>
        /// <example>
        ///     <code language="c#">
        ///         string s = "Hello, World!";
        ///         int charCount = 5;
        ///         string result = s.Left(charCount); // results in "Hello"
        ///     </code>
        /// </example>
        public static string Left(this string s, int count)
        {
            return s.Substring(0, count);
        }

        /// <summary>
        /// Masks a credit card string by replacing all characters except the last 4.
        /// </summary>
        /// <param name="s">Credit card number to format.</param>
        /// <param name="maskCharacter">Character to use as the mask.</param>
        /// <returns>Masked currency string.</returns>
        /// <example>
        ///     <code language="c#">
        ///         string expected = "XXXXXXXXXXXX3456";
        ///         string actual = ccNumber.MaskCreditCard('X');
        ///     </code>
        /// </example>
        public static string MaskCreditCard(this string s, char maskCharacter)
        {
            string result = "";

            for (int i = 0; i < 12; i++)
            {
                result += maskCharacter;
            }

            result += s.Substring(s.Length - 4, 4);

            return result;
        }

        /// <summary>
        /// Retrieves a section of a string.
        /// </summary>
        /// <param name="s">The string to examine.</param>
        /// <param name="index">The start index of the substring.</param>
        /// <param name="count">The number of characters to retrieve.</param>
        /// <returns>The resulting substring.</returns>
        /// <example>
        ///     <code language="c#">
        ///         string s = "Hello, World!";
        ///         string results = s.Mid(2, 4);
        ///     </code>
        /// </example>
        public static string Mid(this string s, int index, int count)
        {
            return s.Substring(index, count);
        }

        /// <summary>
        /// Left pads a <see cref="string">string</see> using the supplied pad <see cref="string">string</see> for the total number of spaces.  
        /// It will not cut-off the pad even if it causes the <see cref="string">string</see> to exceed the total width.
        /// </summary>
        /// <param name="s">The <see cref="string">string</see> to pad.</param>
        /// <param name="pad">The <see cref="string">string</see> to uses as padding.</param>
        /// <returns>A padded <see cref="string">string</see>.</returns>
        /// <example>
        ///     <code language="c#">
        ///         string s = "Hello, World!";
        ///         string pad = " How are you?";
        ///         string results = s.PadLeft(pad);
        ///     </code>
        /// </example>
        public static string PadLeft(this string s, string pad)
        {
            return s.PadLeft(pad, s.Length + pad.Length, false);
        }

        /// <summary>
        /// Left pads a <see cref="string">string</see> using the passed pad <see cref="string">string</see> for the total number of spaces.  
        /// Optionally, it will cut-off the pad so that the <see cref="string">string</see> does not exceed the total width.
        /// </summary>
        /// <param name="s">The <see cref="string">string</see> to pad.</param>
        /// <param name="pad">The <see cref="string">string</see> to uses as padding.</param>
        /// <param name="totalWidth">The total number to pad the <see cref="string">string</see>.</param>
        /// <param name="cutOff">Limits overall size of <see cref="string">string</see>.</param>
        /// <returns>A padded <see cref="string">string</see>.</returns>
        /// <example>
        ///     <code language="c#">
        ///         string s = "Hello, World!";
        ///         string pad = " How are you?";
        ///         string actual = s.PadLeft(pad, 17, true);
        ///     </code>
        /// </example>
        public static string PadLeft(this string s, string pad, int totalWidth, bool cutOff)
        {
            if (s.Length >= totalWidth)
            {
                return s;
            }

            int padCount = pad.Length;

            string paddedString = s;

            while (paddedString.Length < totalWidth)
            {
                paddedString += pad;
            }

            // trim the excess.
            if (cutOff)
            {
                paddedString = paddedString.Substring(0, totalWidth);
            }

            return paddedString;
        }

        /// <summary>
        /// Right pads a <see cref="string">string</see> using the supplied pad <see cref="string">string</see> for the total number of spaces.  
        /// It will not cut-off the pad even if it causes the <see cref="string">string</see> to exceed the total width.
        /// </summary>
        /// <param name="s">The <see cref="string">string</see> to pad.</param>
        /// <param name="pad">The <see cref="string">string</see> to uses as padding.</param>
        /// <returns>A padded <see cref="string">string</see>.</returns>
        /// <example>
        ///     <code language="c#">
        ///         string s = "How are you?";
        ///         string pad = "Hello, World! ";
        ///         string results = s.PadRight(pad);
        ///     </code>
        /// </example>
        public static string PadRight(this string s, string pad)
        {
            return PadRight(s, pad, s.Length + pad.Length, false);
        }

        /// <summary>
        /// Right pads a <see cref="string">string</see> using the supplied pad <see cref="string">string</see> for the total number of spaces.  
        /// It will cut-off the pad so that the <see cref="string">string</see> does not exceed the total width.
        /// </summary>
        /// <param name="s">The <see cref="string">string</see> to pad.</param>
        /// <param name="pad">The <see cref="string">string</see> to uses as padding.</param>
        /// <param name="length">The total length to pad the <see cref="string">string</see>.</param>
        /// <param name="cutOff">Limits the overall length of the <see cref="string">string</see>.</param>
        /// <returns>A padded <see cref="string">string</see>.</returns>
        /// <example>
        ///     <code language="c#">
        ///         string s = "How are you?";
        ///         string pad = "Hello, World! ";
        ///         string results = s.PadRight(pad, 17, true);
        ///     </code>
        /// </example>
        public static string PadRight(this string s, string pad, int length, bool cutOff)
        {
            if (s.Length >= length)
            {
                return s;
            }

            string paddedString = string.Empty;

            while (paddedString.Length < length - s.Length)
            {
                paddedString += pad;
            }

            // trim the excess.
            if (cutOff)
            {
                paddedString = paddedString.Substring(0, length - s.Length);
            }

            paddedString += s;

            return paddedString;
        }

        /// <summary>
        /// Removes duplicate words from a string.
        /// </summary>
        /// <param name="s">String to evaluate.</param>
        /// <returns>A string with duplicates removed.</returns>
        /// <example>
        ///     <code language="c#">
        ///         string expected = "To be or not - that is the question.";
        ///         string results = s.RemoveDuplicateWords();
        ///     </code>
        /// </example>
        public static string RemoveDuplicateWords(this string s)
        {
            var d = new Dictionary<string, bool>();

            StringBuilder sb = new StringBuilder();

            string[] a = s.Split(new char[] { ' ', ',', ';', '.', ':' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string current in a)
            {
                string lower = current.ToLower();

                if (!d.ContainsKey(lower))
                {
                    sb.Append(current).Append(' ');
                    d.Add(lower, true);
                }
            }

            string results = sb.ToString().Trim();

            if (s.EndsWith("."))
            {
                results += ".";
            }

            if (s.EndsWith(";"))
            {
                results += ";";
            }

            if (s.EndsWith(":"))
            {
                results += ":";
            }

            if (s.EndsWith(","))
            {
                results += ",";
            }

            return results;
        }

        /// <summary>
        /// Removes the new line (\n) and carriage return (\r) symbols.
        /// </summary>
        /// <param name="s">The string to search.</param>
        /// <returns>The resulting string.</returns>
        /// <remarks>By default, does not replace the new line with a space.</remarks>
        /// <example>
        ///     <code language="c#">
        ///         string s = "Hello, \r\nWorld!";
        ///         string results = s.RemoveNewLines();
        ///     </code>
        /// </example>
        public static string RemoveNewLines(this string s)
        {
            return s.RemoveNewLines(false);
        }

        /// <summary>
        /// Removes the new line (\n) and carriage return (\r) symbols.  
        /// Optionally adds a space for each newline and carriage return.
        /// </summary>
        /// <param name="s">The <see cref="string">string</see> to search.</param>
        /// <param name="addSpace">If true, adds a space (" ") for each newline and carriage return found.</param>
        /// <returns>The resulting string.</returns>
        /// <example>
        ///     <code language="c#">
        ///         string s = "Hello, \r\nWorld!";
        ///         string results = s.RemoveNewLines(true);
        ///     </code>
        /// </example>
        public static string RemoveNewLines(this string s, bool addSpace)
        {
            string replace = string.Empty;

            if (addSpace)
            {
                replace = " ";
            }

            return s.Replace(Environment.NewLine, replace);
        }

        /// <summary>
        /// Removes a prefix from a <see cref="string">string</see> if it exists.
        /// </summary>
        /// <param name="s">Input <see cref="string">string</see> to remove the prefix from.</param>
        /// <param name="prefix"><see cref="string">String</see> that defines the prefix to remove.</param>
        /// <returns>The resulting <see cref="string">string</see>.</returns>
        /// <example>
        ///     <code language="c#">
        ///         string s = "Hello, World!";
        ///         string results = s.RemovePrefix("Hello, ");
        ///     </code>
        /// </example>
        public static string RemovePrefix(this string s, string prefix)
        {
            return Regex.Replace(s, "^" + prefix, System.String.Empty, RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// Remove whitespace from within a <see cref="string">string</see>.
        /// </summary>
        /// <param name="s"><see cref="string">String</see> to remove the spaces from.</param>
        /// <returns><see cref="string">String</see> without spaces.</returns>
        /// <example>
        ///     <code language="c#">
        ///         string phoneNumber = "555 555 5555";
        ///         string trimmed = phoneNumber.RemoveSpaces();
        ///     </code>
        /// </example>
        public static string RemoveSpaces(this string s)
        {
            return s.Replace(" ", string.Empty);
        }

        /// <summary>
        /// Removes a suffix from a <see cref="string">string</see> if it exists.
        /// </summary>
        /// <param name="s">Input <see cref="string">string</see> to remove the suffix from.</param>
        /// <param name="suffix"><see cref="string">String</see> that defines the suffix to remove.</param>
        /// <returns>The resulting <see cref="string">string</see>.</returns>
        /// <example>
        ///     <code language="c#">
        ///         string s = "Hello, World!";
        ///         string results = s.RemoveSuffix(", World!");
        ///     </code>
        /// </example>
        public static string RemoveSuffix(this string s, string suffix)
        {
            return Regex.Replace(s, suffix + "$", System.String.Empty, RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// A case insenstive replace function.
        /// </summary>
        /// <param name="s">The <see cref="string">string</see> to examine.</param>
        /// <param name="oldString">The new value to be inserted.</param>
        /// <param name="newString">The value to replace.</param>
        /// <param name="caseSensitive">Determines whether or not to ignore case.</param>
        /// <returns>The resulting <see cref="string">string</see>.</returns>
        /// <example>
        ///     <code language="c#">
        ///         string s = "Hello, World!";
        ///         string results = s.Replace("world", "Dude", false);
        ///     </code>
        /// </example>
        public static string Replace(this string s, string oldString, string newString, bool caseSensitive)
        {
            if (caseSensitive)
            {
                return s.Replace(oldString, newString);
            }
            else
            {
                Regex regEx = new Regex(oldString, RegexOptions.IgnoreCase | RegexOptions.Multiline);

                return regEx.Replace(s, newString);
            }
        }

        /// <summary>
        /// Reverses a <see cref="string">string</see>.
        /// </summary>
        /// <param name="s">The <see cref="string">string</see> to reverse.</param>
        /// <returns>The resulting <see cref="string">string</see>.</returns>
        /// <example>
        ///     <code language="c#">
        ///         string s = "Hello, World!";
        ///         string results = s.Reverse();
        ///     </code>
        /// </example>
        public static string Reverse_2(this string s)
        {
            if (s.Length <= 1)
            {
                return s;
            }

            char[] c = s.ToCharArray();

            StringBuilder sb = new StringBuilder(c.Length);

            for (int i = c.Length - 1; i > -1; i--)
            {
                sb.Append(c[i]);
            }

            return sb.ToString();
        }

        /// <summary>
        /// Retrieves the right x characters of a <see cref="string">string</see>.
        /// </summary>
        /// <param name="s">The <see cref="string">string</see> to examine.</param>
        /// <param name="count">The number of characters to retrieve.</param>
        /// <returns>The resulting substring.</returns>
        /// <example>
        ///     <code language="c#">
        ///         string s = "Hello, World!";
        ///         string results = s.Right(6);
        ///     </code>
        /// </example>
        public static string Right(this string s, int count)
        {
            return s.Substring(s.Length - count, count);
        }

        /// <summary>
        /// Converts a <see cref="string">string</see> to sentence case.
        /// </summary>
        /// <param name="s">The <see cref="string">string</see> to convert.</param>
        /// <returns>The resulting <see cref="string">string</see>.</returns>
        /// <example>
        ///     <code language="c#">
        ///         string s = "This Is Just a Test.";
        ///         string results = s.SentenceCase();
        ///     </code>
        /// </example>
        public static string SentenceCase(this string s)
        {
            if (s.Length < 1)
            {
                return s;
            }

            string sentence = s.ToLower();

            return sentence[0].ToString().ToUpper() + sentence.Substring(1);
        }

        /// <summary>
        /// Retrieves a section of a string by using start and end indexes.
        /// </summary>
        /// <param name="s">The string to examine.</param>
        /// <param name="startIndex">The start index of the substring.</param>
        /// <param name="endIndex">The end index of the substring.</param>
        /// <returns>The resulting substring.</returns>
        /// <example>
        ///     <code language="c#">
        ///         string s = "Hello, World!";
        ///         string results = s.Slice(0, 5);
        ///     </code>
        /// </example>
        public static string Slice(this string s, int startIndex, int endIndex)
        {
            return s.Substring(startIndex, endIndex - startIndex);
        }

        /// <summary>
        /// Replaces underscores with a space.
        /// </summary>
        /// <param name="s"><see cref="string">String</see> to examine.</param>
        /// <returns>The resulting <see cref="string">string</see>.</returns>
        /// <example>
        ///     <code language="c#">
        ///         string s = "Hello_World!_How_are_you_today?";
        ///         string results = s.SlugDecode();
        ///     </code>
        /// </example>
        public static string SlugDecode(this string s)
        {
            return s.Replace("_", " ");
        }

        /// <summary>
        /// Replaces spaces with a underscores.
        /// </summary>
        /// <param name="s"><see cref="string">String</see> to examine.</param>
        /// <returns>The resulting <see cref="string">string</see>.</returns>
        /// <example>
        ///     <code language="c#">
        ///         string s = "Hello World! How are you today?";
        ///         string results = s.SlugEncode();
        ///     </code>
        /// </example>
        public static string SlugEncode(this string s)
        {
            return s.Replace(" ", "_");
        }

        /// <summary>
        /// Splits a <see cref="string">string</see> into an array by delimiter.
        /// </summary>
        /// <param name="s"><see cref="string">String</see> to split.</param>
        /// <param name="delimiter">Delimiter <see cref="string">string</see>.</param>
        /// <returns>Array of strings.</returns>
        /// <example>
        ///     <code language="c#">
        ///         string s = "one, two, three";
        ///         string[] results = s.Split(",");
        ///     </code>
        /// </example>
        public static string[] Split_2(this string s, string delimiter)
        {
            return s.Split(delimiter.ToCharArray());
        }

        /// <summary>
        /// Splits a <see cref="string">string</see> into an array by delimiter.
        /// Optionally allows for the trimming of each token during the split.
        /// </summary>
        /// <param name="s"><see cref="string">String</see> to split.</param>
        /// <param name="delimiter">Delimiter <see cref="string">string</see>.</param>
        /// <param name="trimTokens">Specifies whether to trim each item in the array during the split.</param>
        /// <returns>Array of strings.</returns>
        /// <example>
        ///     <code language="c#">
        ///         string s = "one, two, three";
        ///         string[] results = s.Split(",", true);
        ///     </code>
        /// </example>
        public static string[] Split(this string s, string delimiter, bool trimTokens)
        {
            if (trimTokens)
            {
                string[] results = s.Split(delimiter.ToCharArray());

                for (int i = 0; i < results.Length; i++)
                {
                    results[i] = results[i].Trim();
                }

                return results;
            }
            else
            {
                return s.Split(delimiter.ToCharArray());
            }
        }

        /// <summary>
        /// Determines whether the beginning of this instance matches any of the specified strings.
        /// </summary>
        /// <param name="s"><see cref="string">String</see> to inspect.</param>
        /// <param name="values">The strings to compare.</param>
        /// <returns>Indicator of presence of any of the supplied values at the beginning of the <see cref="string">string</see>.</returns>
        /// <example>
        ///     <code language="c#">
        ///         string s = "Hello, World!";
        ///         List&lt;string&gt; values = new List&lt;string&gt;();
        ///         values.Add("Goodbye");
        ///         values.Add("Hello");
        ///         bool results = s.StartsWithAny(values);
        ///     </code>
        /// </example>
        public static bool StartsWithAny(this string s, List<string> values)
        {
            return s.StartsWithAny(values, true);
        }

        /// <summary>
        /// Determines whether the beginning of this instance matches any of the specified strings.  
        /// Optionally allows for case ignorance.
        /// </summary>
        /// <param name="s"><see cref="string">string</see> to inspect.</param>
        /// <param name="values">The strings to compare.</param>
        /// <param name="ignoreCase">True to ignore case when comparing this <see cref="string">string</see> and value.  Otherwise false.</param>
        /// <returns>Indicator of presence of any of the supplied values at the beginning of the <see cref="string">string</see>.</returns>
        /// <example>
        ///     <code language="c#">
        ///         string s = "Hello, World!";
        ///         List&lt;string&gt; values = new List&lt;string&gt;();
        ///         values.Add("Goodbye");
        ///         values.Add("Hello");
        ///         bool results = s.StartsWithAny(values, true);
        ///     </code>
        /// </example>
        public static bool StartsWithAny(this string s, List<string> values, bool ignoreCase)
        {
            return s.StartsWithAny(values, ignoreCase, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Determines whether the beginning of this instance matches any of the specified strings.
        /// Optionally allows the culture to be specified.
        /// </summary>
        /// <param name="s"><see cref="string">String</see> to inspect.</param>
        /// <param name="values">The strings to compare.</param>
        /// <param name="ignoreCase">True to ignore case when comparing this <see cref="string">string</see> and value.  Otherwise false.</param>
        /// <param name="culture">Culteral information that determines how this <see cref="string">string</see> and value are compared.</param>
        /// <returns>Indicator of presence of any of the supplied values at the beginning of the <see cref="string">string</see>.</returns>
        /// <example>
        ///     <code language="c#">
        ///         string s = "Hello, World!";
        ///         List&lt;string&gt; values = new List&lt;string&gt;();
        ///         values.Add("Goodbye");
        ///         values.Add("Hello");
        ///         bool results = s.StartsWithAny(values, true, CultureInfo.CurrentCulture);
        ///     </code>
        /// </example>
        public static bool StartsWithAny(this string s, List<string> values, bool ignoreCase, CultureInfo culture)
        {
            foreach (string value in values)
            {
                if (s.StartsWith(value, ignoreCase, culture))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Removes all HTML tags from the passed <see cref="string">string</see>.
        /// </summary>
        /// <param name="s">The <see cref="string">string</see> whose values should be replaced.</param>
        /// <returns>The resulting <see cref="string">string</see>.</returns>
        /// <example>
        ///     <code language="c#">
        ///         string s = "&lt;body&gt;Hello, World!&lt;/body&gt;";
        ///         string results = s.StripTags();
        ///     </code>
        /// </example>
        public static string StripTags(this string s)
        {
            Regex stripTags = new Regex("<(.|\n)+?>");

            return stripTags.Replace(s, string.Empty);
        }

        /// <summary>
        /// Converts a <see cref="string">string</see> to title case.
        /// </summary>
        /// <param name="s">The <see cref="string">string</see> to convert.</param>
        /// <returns>The resulting <see cref="string">string</see>.</returns>
        /// <example>
        ///     <code language="c#">
        ///         string s = "On the waterfront";
        ///         string title = s.TitleCase();
        ///     </code>
        /// </example>
        public static string TitleCase(this string s)
        {
            return TitleCase(s, true);
        }

        /// <summary>
        /// Converts a <see cref="string">string</see> to title case.
        /// Optionally allows short words to be ignored.
        /// </summary>
        /// <param name="s">The <see cref="string">string</see> to convert.</param>
        /// <param name="ignoreShortWords">If true, does not capitalize words like
        /// "a", "is", "the", etc.</param>
        /// <returns>The resulting <see cref="string">string</see>.</returns>
        /// <example>
        ///     <code language="c#">
        ///         string s = "On the waterfront";
        ///         string title = s.TitleCase(false);
        ///     </code>
        /// </example>
        public static string TitleCase(this string s, bool ignoreShortWords)
        {
            List<string> ignoreWords = null;

            if (ignoreShortWords)
            {
                ignoreWords = new List<string>();
                ignoreWords.Add("a");
                ignoreWords.Add("is");
                ignoreWords.Add("was");
                ignoreWords.Add("the");
            }

            string[] tokens = s.Split(' ');

            StringBuilder sb = new StringBuilder(s.Length);

            foreach (string token in tokens)
            {
                if (ignoreShortWords == true
                    && token != tokens[0]
                    && ignoreWords.Contains(token.ToLower()))
                {
                    sb.Append(token + " ");
                }
                else
                {
                    sb.Append(token[0].ToString().ToUpper());
                    sb.Append(token.Substring(1).ToLower());
                    sb.Append(" ");
                }
            }

            return sb.ToString().Trim();
        }

        /// <summary>
        /// Converts the supplied <paramref name="Value">value</paramref> to an <see cref="Boolean">Boolean</see>.
        /// </summary>
        /// <param name="value">The <see cref="string">string</see> value to convert.</param>
        /// <returns>The resulting <see cref="Boolean">Boolean</see> value.</returns>
        /// <remarks>If an error occurs while converting the value (ie the <see cref="string">string</see> value does not convert to a boolean value), <c>false</c> will be returned.</remarks>
        /// <example>
        ///     <code language="c#">
        ///         string s = "False";
        ///         bool results = s.ToBoolean();     
        ///     </code>
        /// </example>
        public static bool ToBoolean(this string value)
        {
            try
            {
                return Convert.ToBoolean(value);
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Returns a byte array of a specified <see cref="string">string</see>.
        /// </summary>
        /// <param name="text">The text to go into the byte array.</param>
        /// <returns>A byte array of text.</returns>
        /// <example>
        ///     <code language="c#">
        ///         string s = "Hello, World!";
        ///         byte[] converted = s.ToByteArray();
        ///     </code>
        /// </example>
        public static byte[] ToByteArray(this string text)
        {
            return Encoding.ASCII.GetBytes(text);
        }

        /// <summary>
        /// Converts the supplied <paramref name="value">value</paramref> to a <see cref="Decimal">Decimal</see>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The resulting <see cref="Decimal">Decimal</see> value.</returns>
        /// <example>
        ///     <code language="c#">
        ///         Decimal result = "5".ToDecimal();
        ///     </code>
        /// </example>
        public static decimal ToDecimal(this string value)
        {
            if (string.IsNullOrEmpty(value) == true || (value == null) == true || value.IsNumeric() == false)
            {
                return 0;
            }
            else
            {
                return System.Convert.ToDecimal(value);
            }
        }

        /// <summary>
        /// Converts the supplied <paramref name="Value">value</paramref> to an <see cref="Int16">Int16</see>.
        /// </summary>
        /// <param name="value">The <see cref="string">string</see> value to convert.</param>
        /// <returns>The resulting <see cref="Int16">Int16</see> value.</returns>
        /// <example>
        ///     <code language="c#">
        ///         int number = "5".ToInt16();
        ///     </code>
        /// </example>
        public static short ToInt16(this string value)
        {
            if (string.IsNullOrEmpty(value) == true || (value == null) == true || value.IsNumeric() == false)
            {
                return System.Convert.ToInt16(0);
            }
            else
            {
                return System.Convert.ToInt16(value);
            }
        }

        /// <summary>
        /// Converts the supplied <paramref name="value">value</paramref> to an <see cref="Int32">Int32.</see>
        /// </summary>
        /// <param name="value">The <see cref="string">string</see> value to convert.</param>
        /// <returns>The resulting Integer.</returns>
        /// <example>
        ///     <code language="c#">
        ///         int number = "5".ToInt32();
        ///     </code>
        /// </example>
        public static int ToInt32(this string value)
        {
            if (string.IsNullOrEmpty(value) == true || (value == null) == true || value.IsNumeric() == false)
            {
                return System.Convert.ToInt32(0);
            }
            else
            {
                return System.Convert.ToInt32(value);
            }
        }

        /// <summary>
        /// Converts the supplied <paramref name="Value">value</paramref> to an <see cref="Int64">Int64</see>.
        /// </summary>
        /// <param name="value">The <see cref="string">string</see> value to convert.</param>
        /// <returns>The resulting <see cref="Int64">Int64</see> value.</returns>
        /// <example>
        ///     <code language="c#">
        ///         int number = "294967295".ToInt64();
        ///     </code>
        /// </example>
        public static long ToInt64(this string value)
        {
            if (string.IsNullOrEmpty(value) == true || (value == null) == true || value.IsNumeric() == false)
            {
                return System.Convert.ToInt64(0);
            }
            else
            {
                return System.Convert.ToInt64(value);
            }
        }

        /// <summary>
        /// Removes multiple spaces between words.
        /// </summary>
        /// <param name="s">The <see cref="string">string</see> to trim.</param>
        /// <returns>The resulting <see cref="string">string</see>.</returns>
        /// <example>
        ///     <code language="c#">
        ///         string s = "Hello,   World!";
        ///         string trimmed = s.TrimIntraWords();
        ///     </code>
        /// </example>
        public static string TrimIntraWords(this string s)
        {
            Regex regEx = new Regex(@"[\s]+");

            return regEx.Replace(s, " ");
        }

        /// <summary>
        /// Truncates the <see cref="string">string</see> to a specified length and replace the truncated to a ...
        /// </summary>
        /// <param name="s"><see cref="string">String</see> that will be truncated.</param>
        /// <param name="maxLength">Total length of characters to maintain before the truncate happens.</param>
        /// <returns>Truncated <see cref="string">string</see>.</returns>
        /// <remarks>maxLength is inclusive of the three characters in the ellipsis.</remarks>
        /// <example>
        ///     <code language="c#">
        ///         string s = "Hello, World!";
        ///         string results = s.Truncate(5);
        ///     </code>
        /// </example>
        public static string Truncate(this string s, int maxLength)
        {
            // replaces the truncated string to a ...
            string suffix = "...";

            string truncatedString = s;

            if (maxLength <= 0)
            {
                return truncatedString;
            }

            int strLength = maxLength - suffix.Length;

            if (strLength <= 0)
            {
                return truncatedString;
            }

            if (s == null || s.Length <= maxLength)
            {
                return truncatedString;
            }

            truncatedString = s.Substring(0, strLength);
            truncatedString = truncatedString.TrimEnd();
            truncatedString += suffix;

            return truncatedString;
        }

        /// <summary>
        /// Counts all words in a given <see cref="string">string</see>.  Excludes whitespaces, tabs and line breaks.
        /// </summary>
        /// <param name="s"><see cref="string">String</see> to inspect.</param>
        /// <returns>The number of words in the <see cref="string">string</see>.</returns>
        /// <example>
        ///     <code language="c#">
        ///         string s = "Hello, World!";
        ///         int count = s.WordCount();    
        ///     </code>
        /// </example>
        public static int WordCount(this string s)
        {
            var count = 0;

            var re = new Regex(@"[^\s]+");
            var matches = re.Matches(s);
            count = matches.Count;

            return count;
        }

        /// <summary>
        /// Calculates the number of times a word exists withing a <see cref="string">string</see>.
        /// </summary>
        /// <param name="s"><see cref="string">String</see> to evaluate.</param>
        /// <param name="word">Word to search for.</param>
        /// <returns>Number of times the word exists within the <see cref="string">string</see>.</returns>
        /// <example>
        ///     <code language="c#">
        ///         string s = "Hello, World! How are you doing today?";
        ///         string word = "How";
        ///         int count = s.WordInstanceCount(word);
        ///     </code>
        /// </example>
        public static int WordInstanceCount(this string s, string word)
        {
            Regex r = new Regex(@"\b" + word + @"\b", RegexOptions.IgnoreCase);
            MatchCollection mc = r.Matches(s);
            return mc.Count;
        }

        /// <summary>
        /// Wraps the passed <see cref="string">string</see> up the total number of characters until the next whitespace on or after 
        /// the total character count has been reached for that line.  
        /// Uses the environment new line symbol for the break text.
        /// </summary>
        /// <param name="s">The <see cref="string">string</see> to wrap.</param>
        /// <param name="charCount">The number of characters per line.</param>
        /// <returns>The resulting <see cref="string">string</see>.</returns>
        /// <example>
        ///     <code language="c#">
        ///         string s = "Hello, World! How are you doing today?";
        ///         string results = s.WordWrap(9);
        ///     </code>
        /// </example>
        public static string WordWrap(this string s, int charCount)
        {
            return WordWrap(s, charCount, false, Environment.NewLine);
        }

        /// <summary>
        /// Wraps the passed <see cref="string">string</see> up the total number of characters (if cutOff is true)
        /// or until the next whitespace (if cutOff is false).  Uses the environment new line
        /// symbol for the break text.
        /// </summary>
        /// <param name="s">The <see cref="string">string</see> to wrap.</param>
        /// <param name="charCount">The number of characters per line.</param>
        /// <param name="cutOff">If true, will break in the middle of a word.</param>
        /// <returns>The resulting <see cref="string">string</see>.</returns>
        /// <example>
        ///     <code language="c#">
        ///         string s = "Hello, World! How are you doing today?";
        ///         string results = s.WordWrap(9, false);
        ///     </code>
        /// </example>
        public static string WordWrap(this string s, int charCount, bool cutOff)
        {
            return WordWrap(s, charCount, cutOff, Environment.NewLine);
        }

        /// <summary>
        /// Wraps the passed <see cref="string">string</see> up the total number of characters (if cutOff is true)
        /// or until the next whitespace (if cutOff is false).  Uses the supplied breakText
        /// for line breaks.
        /// </summary>
        /// <param name="s">The <see cref="string">string</see> to wrap.</param>
        /// <param name="charCount">The number of characters per line.</param>
        /// <param name="cutOff">If true, will break in the middle of a word.</param>
        /// <param name="breakText">The line break text to use.</param>
        /// <returns>The resulting <see cref="string">string</see>.</returns>
        /// <example>
        ///     <code language="c#">
        ///         string s = "Hello, World! How are you doing today?";
        ///         string results = s.WordWrap(9, false, Environment.NewLine);
        ///     </code>
        /// </example>
        public static string WordWrap(this string s, int charCount, bool cutOff, string breakText)
        {
            StringBuilder sb = new StringBuilder(s.Length + 100);
            int counter = 0;

            if (cutOff)
            {
                while (counter < s.Length)
                {
                    if (s.Length > counter + charCount)
                    {
                        sb.Append(s.Substring(counter, charCount));
                        sb.Append(breakText);
                    }
                    else
                    {
                        sb.Append(s.Substring(counter));
                    }

                    counter += charCount;
                }
            }
            else
            {
                string[] strings = s.Split(' ');

                for (int i = 0; i < strings.Length; i++)
                {
                    counter += strings[i].Length + 1;

                    if (i != 0 && counter > charCount)
                    {
                        sb.Append(breakText);
                        counter = 0;
                    }

                    sb.Append(strings[i] + ' ');
                }
            }

            return sb.ToString().TrimEnd();
        }
        #endregion
    }

}