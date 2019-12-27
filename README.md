# Jetpack for .NET

[![Version](https://img.shields.io/nuget/vpre/Jetsons.JetPack.svg)](https://www.nuget.org/packages/Jetsons.JetPack)
[![Downloads](https://img.shields.io/nuget/dt/Jetsons.JetPack.svg)](https://www.nuget.org/packages/Jetsons.JetPack)
[![GitHub contributors](https://img.shields.io/github/contributors/jetsons/JetPack.Net.svg)](https://github.com/jetsons/JetPack.Net/graphs/contributors)
[![License](https://img.shields.io/github/license/jetsons/JetPack.Net.svg)](https://github.com/jetsons/JetPack.Net/blob/master/LICENSE)

To use this simply grab our Nuget package `Jetsons.JetPack` and add this to the top of your class:

    using Jetsons.JetPack;
	
This statement unlocks all the extension methods below. Enjoy!

### Extensions

Extension methods for Strings:

- string.**Exists**
- string.**IsNumber**
- string.**IsSymbol**
- string.**IsLetter**
- string.**Contains**
- string.**BeginsWith**
- string.**EndsWith**
- string.**Between**
- string.**Before**
- string.**BeforeFirst**
- string.**BeforeLast**
- string.**After**
- string.**AfterFirst**
- string.**AfterLast**
- string.**BeforeIndex**
- string.**AfterIndex**
- string.**SmartSplit**
- string.**Chars**
- string.**Lines**
- string.**Words**
- string.**CodeWords**
- string.**SplitCamelCase**
- string.**LineCount**
- string.**RemovePrefix**
- string.**RemovePostfix**
- string.**Prefix**
- string.**Postfix**
- string.**EqualsCI**
- string.**BeginsWithCI**
- string.**EndsWithCI**
- string.**Remove**
- string.**EqualsAny**
- string.**ContainsAny**
- string.**IndexOfAny**
- string.**BeginsWithAny**
- string.**EndsWithAny**
- string.**ContainsLowercase**
- string.**ContainsUppercase**
- string.**ContainsLetter**
- string.**ContainsNumber**
- string.**IsSingleNumber**
- string.**IsMultiline**
- string.**IsSingleline**
- string.**FirstLetterUppercase**
- string.**FirstLetterLowercase**
- string.**SingleLine**
- string.**RemoveMultipleSpaces**
- string.**RemoveMultipleNewlines**
- string.**Truncate**
- string.**CountPrefix**
- string.**CountPostfix**
- string.**RemoveSymbols**
- string.**WebSafeFilename**

Extension methods for String encodings:

- string.**EncodeBase64**
- string.**DecodeBase64**
- bytes.**EncodeBase64**
- string.**EscapeHTML**
- string.**EncodeUTF8**
- string.**EncodeANSI**
- bytes.**DecodeUTF8**
- bytes.**DecodeANSI**

Extension methods for manipulating file path Strings:

- string.**IsPathValid**
- string.**IsFolderPath**
- string.**FilenameAndExt**
- string.**Filename**
- string.**Extension**
- string.**SetFilenameAndExt**
- string.**SetFilename**
- string.**SetExtension**
- string.**ParentFolder**
- string.**AddPath**

Extension methods for file I/O performed using file path Strings:

- string.**FileExists**
- string.**FolderExists**
- string.**EnsureFolderExists**
- string.**DeleteFolder**
- string.**EmptyFolder**
- string.**LoadBytes**
- string.**LoadTextFile**
- string.**SaveToFile** (bytes)
- string.**SaveToTempFile** (bytes)
- string.**SaveToFile** (string)
- string.**SaveToTempFile** (string)
- string.**FileSize**
- string.**FileCreatedDate**
- string.**FileModifiedDate**
- string.**GetFilesInDirectory**
- string.**GetDirectoriesInDirectory**
- string.**OpenFileInDefaultApp**
- string.**OpenFolderInExplorer**
- string.**OpenFileInExplorer**

Extension methods for URL Strings:

- string.**DownloadURLToFile**

Extension methods for Streams:

- stream.**ToBytes**
- stream.**SaveToFile**

Extension methods for Dictionaries & ExpandoObjects:

- dictionary.**SetProp**
- dictionary.**GetProp**
- dictionary.**GetPath**
- dictionary.**SetPath**

Extension methods for Objects using Reflection:

- any.**GetPropValue**
- any.**SetPropValue**

Extension methods for Lists:

- list.**Get**
- list.**Set**
- list.**HasSlot**
- list.**HasSlotAndValue**
- list.**First**
- list.**Last**
- list.**Repeat**
- list.**Exists**
- list.**ToList**
- list.**ToArray**
- list.**Transpose**
- list.**AddIfExists**
- list.**AddOnce**
- list.**EnsureValidIndex**
- list.**Part**
- list.**RemoveFirst**
- list.**RemoveLast**
- list.**ShallowClone**
- list.**AddAndReturn**
- list.**RemoveAndReturn**

Extension methods for Object Lists:

- list.**GetProps**

Extension methods for Numbers:

- number.**Round**
- number.**RoundToDigits**
- number.**Ceiling**
- number.**Floor**
- number.**Snap**
- number.**Limit**
- number.**AtLeast**
- number.**AtMost**
- number.**Min**
- number.**Max**
- number.**BytesToString**

Extension methods for Numeric Lists:

- list.**Min**
- list.**Max**
- list.**IndexOfMin**
- list.**IndexOfMax**

Extension methods for Colors:

- int.**ToColor**
- color.**ToInt**

Extension methods for RichTextBoxes:

- rtf.**HighlightAll**

### Static Classes

.NET applications:

- **AppResources.GetText**
- **AppResources.GetIcon**
- **AppResources.GetBitmap**
- **AppResources.GetBytes**

File handlers:

- **FileTypeHandlers.Register**

File manipulation:

- **FileIcons.FileIcon**
- **FileIcons.IconFromExtension**
- **FileIcons.IconFromExtensionShell**
- **FileIcons.IconFromResource**

Common dialog utilities:

- **Browse.Folder**
- **Browse.Files**
- **Browse.File**

Message box utilities:

- **Messages.Info**
- **Messages.Error**
- **Messages.Question**

Background worker dialogs:

- **BackgroundTasks.ShowStatusDialog**
- **BackgroundTasks.ShowRunnerDialog**

### Dynamic Classes

String manipulation:

- **ReverseCaseLearner**
- **ReverseCaseGenerator**

Common character codes are in : **Chars**
