﻿using System;
using System.Windows.Controls;
using System.Globalization;
using System.Text.RegularExpressions;

namespace UniGuy.Controls.ValidationRules
{
    /// <summary>
    /// A <see cref="System.Windows.Controls.ValidationRule"/>-derived class which 
    /// supports the use of regular expressions for validation.
    /// </summary>
    public class RegexValidationRule : ValidationRule
    {
        #region Fields

        private string errorMessage;
        private RegexOptions regexOptions = RegexOptions.None;
        private string regexText;

        #endregion

        #region Constructors

        /// <summary>
        /// Parameterless constructor.
        /// </summary>
        public RegexValidationRule()
        {
        }

        /// <summary>
        /// Creates a RegexValidationRule with the specified regular expression.
        /// </summary>
        /// <param name="regexText">The regular expression used by the new instance.</param>
        public RegexValidationRule(string regexText)
        {
            this.RegexText = regexText;
        }

        /// <summary>
        /// Creates a RegexValidationRule with the specified regular expression
        /// and error message.
        /// </summary>
        /// <param name="regexText">The regular expression used by the new instance.</param>
        /// <param name="errorMessage">The error message used when validation fails.</param>
        public RegexValidationRule(string regexText, string errorMessage)
            : this(regexText)
        {
            this.RegexOptions = regexOptions;
        }

        /// <summary>
        /// Creates a RegexValidationRule with the specified regular expression,
        /// error message, and RegexOptions.
        /// </summary>
        /// <param name="regexText">The regular expression used by the new instance.</param>
        /// <param name="errorMessage">The error message used when validation fails.</param>
        /// <param name="regexOptions">The RegexOptions used by the new instance.</param>
        public RegexValidationRule(string regexText, string errorMessage, RegexOptions regexOptions)
            : this(regexText)
        {
            this.RegexOptions = regexOptions;
        }

        #endregion // Constructors

        #region Properties

        /// <summary>
        /// Gets/sets the error message to be used when validation fails.
        /// </summary>
        public string ErrorMessage
        {
            get { return this.errorMessage; }
            set { this.errorMessage = value; }
        }

        /// <summary>
        /// Gets/sets the RegexOptions to be used during validation.
        /// This property's default value is 'None'.
        /// </summary>
        public RegexOptions RegexOptions
        {
            get { return regexOptions; }
            set { regexOptions = value; }
        }

        /// <summary>
        /// Gets/sets the regular expression used during validation.
        /// </summary>
        public string RegexText
        {
            get { return regexText; }
            set { regexText = value; }
        }

        #endregion // Properties

        #region Validate

        /// <summary>
        /// Validates the 'value' argument using the regular 
        /// expression and RegexOptions associated with this object.
        /// </summary>
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            ValidationResult result = ValidationResult.ValidResult;

            // If there is no regular expression to evaluate,
            // then the data is considered to be valid.
            if (!String.IsNullOrEmpty(this.RegexText))
            {
                // Cast the input value to a string (null becomes empty string).
                string text = value as string ?? String.Empty;

                // If the string does not match the regex, return a value
                // which indicates failure and provide an error mesasge.
                if (!Regex.IsMatch(text, this.RegexText, this.RegexOptions))
                    result = new ValidationResult(false, this.ErrorMessage);
            }

            return result;
        }

        #endregion // Validate
    }
}
