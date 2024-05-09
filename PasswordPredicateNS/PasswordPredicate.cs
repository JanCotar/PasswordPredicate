using System;

namespace PasswordPredicateNS
{
    /// <summary>
    /// Provides a method for checking if a string complies with multiple predefined requirements.<br/>
    /// It also provides getters and setters for these requirements.
    /// </summary>
    public class PasswordPredicate
    {
        private bool _notNullOrNoBackslash = true;

        private int _minLength = 8;
        private int _maxLength = 24;
        private bool _meetsMinLength = true;
        private bool _meetsMaxLength = true;

        private int _reqUpperCount = 1;
        private bool _meetsUpper = true;

        private int _reqLowerCount = 1;
        private bool _meetsLower = true;

        private int _reqDigitCount = 1;
        private bool _meetsDigit = true;

        private int _reqSpecialCharCount = 1;
        private bool _meetsSpecialChar = true;

        private string[] _prohibitedStr = new string[0];
        private bool _permitProhibitedSubstr = false;
        private bool _permitProhibitedSubseq = false;
        private bool _prohibitedStrIgnoreCasing = true;
        private bool _meetsSubstr = true;
        private bool _meetsSubseq = true;
        private string _foundProhibitedSubstr = null;
        private string _foundProhibitedSubseq = null;

        private bool _meetsCriteria = true;

        /// <summary>
        /// Gets a value indicating whether a string is not null or does not contain a backslash.
        /// </summary>
        public bool NotNullOrNoBackslash { get { return _notNullOrNoBackslash; } }

        /// <summary>
        /// Gets or sets the minimal length of a string.<br/>
        /// If less than 0, then minimal length is automatically set to 1, otherwise the specified value.
        /// </summary>
        public int MinLength
        {
            get { return _minLength; }
            set
            {
                if (value < 1)
                {
                    _minLength = 1;
                }
                else
                {
                    _minLength = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the maximal length of a string.<br/>
        /// If less than MinLenght, then maximal length is automatically set to MinLength, otherwise the specified value.
        /// </summary>
        public int MaxLength
        {
            get { return _maxLength; }
            set
            {
                if (value < _minLength)
                {
                    _maxLength = _minLength;
                }
                else
                {
                    _maxLength = value;
                }
            }
        }

        /// <summary>
        /// Gets a value indicating whether a string meets the MinLength requirement.
        /// </summary>
        public bool MeetsMinLength { get { return _meetsMinLength; } }

        /// <summary>
        /// Gets a value indicating whether a string meets the MaxLength requirement.
        /// </summary>
        public bool MeetsMaxLength { get { return _meetsMaxLength; } }

        /// <summary>
        /// Gets or sets the required upper case count.<br/>
        /// If less than 0, the upper case is optional. If 0, the upper case is prohibited, otherwise equal or greater than the specified value.
        /// </summary>
        public int ReqUpperCount
        {
            get { return _reqUpperCount; }
            set { _reqUpperCount = value; }
        }

        /// <summary>
        /// Gets a value indicating whether a string meets the ReqUpperCount requirement.
        /// </summary>
        public bool MeetsUpper { get { return _meetsUpper; } }

        /// <summary>
        /// Gets or sets the required lower case count.<br/>
        /// If less than 0, the lower case is optional. If 0, the lower case is prohibited, otherwise equal or greater than the specified value.
        /// </summary>
        public int ReqLowerCount
        {
            get { return _reqLowerCount; }
            set { _reqLowerCount = value; }
        }

        /// <summary>
        /// Gets a value indicating whether a string meets the ReqLowerCount requirement.
        /// </summary>
        public bool MeetsLower { get { return _meetsLower; } }

        /// <summary>
        /// Gets or sets the required digit count.<br/>
        /// If less than 0, the digit is optional. If 0, the digit is prohibited, otherwise equal or greater than the specified value.
        /// </summary>
        public int ReqDigitCount
        {
            get { return _reqDigitCount; }
            set { _reqDigitCount = value; }
        }

        /// <summary>
        /// Gets a value indicating whether a string meets the ReqDigitCount requirement.
        /// </summary>
        public bool MeetsDigit { get { return _meetsDigit; } }

        /// <summary>
        /// Gets or sets the required special character count.<br/>
        /// If less than 0, the special character is optional. If 0, the special character is prohibited, otherwise equal or greater than the specified value.
        /// </summary>
        public int ReqSpecialCharCount
        {
            get { return _reqSpecialCharCount; }
            set { _reqSpecialCharCount = value; }
        }

        /// <summary>
        /// Gets a value indicating whether a string meets the ReqSpecialCharCount requirement.
        /// </summary>
        public bool MeetsSpecialChar { get { return _meetsSpecialChar; } }

        /// <summary>
        /// Gets or sets an array of strings that are prohibited to use.
        /// </summary>
        public string[] ProhibitedStr
        {
            get { return _prohibitedStr; }
            set
            {
                if (value != null)
                {
                    _prohibitedStr = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets a boolean value indicating if a prohibited string can be used as a substring.
        /// </summary>
        public bool PermitProhibitedSubstr
        {
            get { return _permitProhibitedSubstr; }
            set { _permitProhibitedSubstr = value; }
        }

        /// <summary>
        /// Gets or sets a boolean value indicating if a prohibited string can be used as a subsequence.
        /// </summary>
        public bool PermitProhibitedSubseq
        {
            get { return _permitProhibitedSubseq; }
            set { _permitProhibitedSubseq = value; }
        }

        /// <summary>
        /// Gets or sets a boolean value indicating if searching for a prohibited string should ignore casing.
        /// </summary>
        public bool ProhibitedStrIgnoreCasing
        {
            get { return _prohibitedStrIgnoreCasing; }
            set { _prohibitedStrIgnoreCasing = value; }
        }

        /// <summary>
        /// Gets a value indicating whether no prohibited string was found as a substring of the password string.
        /// </summary>
        public bool MeetsSubstr { get { return _meetsSubstr; } }

        /// <summary>
        /// Gets a value indicating whether no prohibited string was found as a subsequence of the password string.
        /// </summary>
        public bool MeetsSubseq { get { return _meetsSubseq; } }

        /// <summary>
        /// Gets a string that was found as a substring of the password string.
        /// </summary>
        public string FoundProhibitedSubstr { get { return _foundProhibitedSubstr; } }

        /// <summary>
        /// Gets a string that was found as a subsequence of the password string.
        /// </summary>
        public string FoundProhibitedSubseq { get { return _foundProhibitedSubseq; } }

        /// <summary>
        /// Gets a value indicating whether a string meets all of the requirements.
        /// </summary>
        public bool MeetsCriteria
        {
            get { return _meetsCriteria; }
        }

        /// <summary>
        /// Gets an array of boolean values representing meets of each individual requirement.<br/>
        /// Values by index:<br/>
        /// [0] NotNullOrNoBackslash<br/>
        /// [1] MeetsMinLength<br/>
        /// [2] MeetsMaxLength<br/>
        /// [3] MeetsUpper<br/>
        /// [4] MeetsLower<br/>
        /// [5] MeetsDigit<br/>
        /// [6] MeetsSpecialChar<br/>
        /// [7] MeetsSubstr<br/>
        /// [8] MeetsSubseq<br/>
        /// [9] MeetsCriteria<br/>
        /// </summary>
        public bool[] AllMeets
        {
            get
            {
                return new bool[]
                {
                    _notNullOrNoBackslash,
                    _meetsMinLength,
                    _meetsMaxLength,
                    _meetsUpper,
                    _meetsLower,
                    _meetsDigit,
                    _meetsSpecialChar,
                    _meetsSubstr,
                    _meetsSubseq,
                    _meetsCriteria,
                };
            }
        }

        /// <summary>
        /// Checks string compliance with requirements.
        /// </summary>
        public bool CheckPassword(string password)
        {
            if (password == null || password.Contains("\\"))
            {
                _notNullOrNoBackslash = false;
                _meetsCriteria = false;
                return false;
            }

            char[] passChars = password.ToCharArray();

            if (passChars.Length < _minLength)
            {
                _meetsMinLength = false;
                _meetsCriteria = false;
            }

            if (passChars.Length > _maxLength)
            {
                _meetsMaxLength = false;
                _meetsCriteria = false;
            }

            CountOccurrences(passChars, Char.IsUpper, _reqUpperCount, ref _meetsUpper);
            CountOccurrences(passChars, Char.IsLower, _reqLowerCount, ref _meetsLower);
            CountOccurrences(passChars, Char.IsDigit, _reqDigitCount, ref _meetsDigit);
            CountOccurrences(passChars, fun => !Char.IsLetterOrDigit(fun), _reqSpecialCharCount, ref _meetsSpecialChar);

            if (_prohibitedStrIgnoreCasing)
            {
                string passwordUpper = password.ToUpper();
                char[] passCharsUpper = passwordUpper.ToCharArray();

                string[] prohibitedStrUpper = new string[_prohibitedStr.Length];

                for (int i = 0; i < prohibitedStrUpper.Length; i++)
                {
                    prohibitedStrUpper[i] = _prohibitedStr[i].ToUpper();
                }

                CheckForSubstrings(passwordUpper, prohibitedStrUpper);
                CheckForSubsequences(passCharsUpper, prohibitedStrUpper);
            }
            else
            {
                CheckForSubstrings(password, _prohibitedStr);
                CheckForSubsequences(passChars, _prohibitedStr);
            }

            return _meetsCriteria;
        }

        private void CountOccurrences(char[] chars, Predicate<char> validation, int reqCount, ref bool meetsReq)
        {
            if (reqCount < 0) return;

            int counter = 0;

            foreach (char c in chars)
            {
                if (validation(c))
                {
                    counter++;
                }
            }

            if (counter < reqCount || (reqCount == 0 && counter > 0))
            {
                meetsReq = false;
                _meetsCriteria = false;
                return;
            }

            return;
        }

        private void CheckForSubstrings(string password, string[] prohibitedStr)
        {
            if (_permitProhibitedSubstr) return;

            for (int i = 0; i < prohibitedStr.Length; i++)
            {
                if (password.Contains(prohibitedStr[i]))
                {
                    _meetsSubstr = false;
                    _foundProhibitedSubstr = _prohibitedStr[i];
                    _meetsCriteria = false;
                    break;
                }
            }
        }

        private void CheckForSubsequences(char[] passChars, string[] prohibitedStr)
        {
            if (_permitProhibitedSubseq) return;

            char[] prhbChars;

            for (int i = 0; i < prohibitedStr.Length; i++)
            {
                prhbChars = prohibitedStr[i].ToCharArray();

                int pcIndex = 0;

                foreach (char passChar in passChars)
                {
                    if (passChar == prhbChars[pcIndex])
                    {
                        pcIndex++;
                    }

                    if (pcIndex == prhbChars.Length)
                    {
                        _meetsSubseq = false;
                        _foundProhibitedSubseq = _prohibitedStr[i];
                        _meetsCriteria = false;
                        break;
                    }
                }

                if (!_meetsSubseq)
                {
                    break;
                }
            }
        }
    }
}
