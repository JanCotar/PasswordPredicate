# Password Predicate

[![NuGet](https://img.shields.io/badge/nuget-v1.0.1-orange)](https://www.nuget.org/packages/PasswordPredicate/) [![License](https://img.shields.io/badge/license-MIT-orange.svg)](LICENSE.md)

## Description

**Password Predicate** is a C# .NET Standard 2.0 library used for checking if a string complies with the given requirements. It is meant for checking password strings.

**Password Predicate** does not encrypt or decrypt the password and does not return a password strength.

### BUT IT DOES OFFER:
 - setting the minimum and maximum *password length*,
 - setting required *upper case* letter count,
 - setting required *lower case* letter count,
 - setting required *digit* count and
 - setting required *special character* count.

### ON TOP OF THAT IT ALSO OFFERS:
 - setting a list of words (strings) that must not be used in password,
 - finding these prohibited strings as *substrings* of the password,
 - finding these prohibited strings as *subsequences* of the password,
 - both substring and subsequence search can be turned *on or off* and
 - *match prohibited string case* can also be turned on or off.

 Password string must not be null and must not contain backslash (`\`).

**Password Predicate** is also available as a [NuGet package](https://www.nuget.org/packages/PasswordPredicate/).

**This project is licensed under the terms of the [MIT License](LICENSE.md).**

## Instructions

Once Password Predicate is accessible within your project ...

 1. Initialize a `PasswordPredicate` object.
 2. Set requirements or leave them default.
 3. Pass password string into the `bool PasswordPredicate.CheckPassword(string password)` method.
 4. Method returns a boolean value indicating whether a password meets requirements.
  - Fulfilment of each individual requirement can be later returned via getters or
  - a `bool[]` of all individual requirements fulfilments can also be returned later.
  - Found substring and/or found subsequence are also saved and can be returned via getters.

EXAMPLE:

```cs
// Initialize PasswordPredicate object.
PasswordPredicate passPred = new PasswordPredicate();

// Set minimum length to 10 characters.
passPred.MinLength = 10;

// Set maximum length to 20 characters.
passPred.MaxLength = 20;

// At least 1 upper case letter is required.
passPred.ReqUpperCount = 1;

// At least 1 lower case letter is required.
passPred.ReqLowerCount = 1;

// Digit is optional.
passPred.ReqDigitCount = -1;

// Must not contain special character.
passPred.ReqSpecialCharCount = 0;


// Initialize prohibited strings.
string[] prohibitedStrings = new string[]
{
    "1234",
    "password"
};


// Set prohibited strings.
passPred.ProhibitedStr = prohibitedStrings;

// Password must not contain a prohibited string as a substring.
passPred.PermitProhibitedSubstr = false;

// Password must not contain a prohibited string as a subsequence.
passPred.PermitProhibitedSubseq = false;

// Ignore casing in prohibited strings (means also that "PASSWORD" or "PaSsWoRd" for example are prohibited).
passPred.ProhibitedStrIgnoreCasing = true;


string password = "MySpecialPassword!";

            
// Pass password string to CheckPassword(string password) method.
bool meetsReq = passPred.CheckPassword(password);

// CheckPassword in this case returns false because
// password violates ReqSpecialCharCount requirement
// and substring "Password" is also present in the password string.
// Subsequence in this example is the same as substring.

// Get all requirements fullfilment by calling AllMeets.
bool[] allMeets = passPred.AllMeets;

bool notNullOrNoBackslash = allMeets[0]; // true
bool meetsMinLength = allMeets[1]; // true
bool meetsMaxLength = allMeets[2]; // true
bool meetsUpper = allMeets[3]; // true
bool meetsLower = allMeets[4]; // true
bool meetsDigit = allMeets[5]; // true
bool meetsSpecialChar = allMeets[6]; // false
bool meetsSubstr = allMeets[7]; // false
bool meetsSubseq = allMeets[8]; // false
bool meetsCriteria = allMeets[9]; // false
```