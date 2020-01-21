# JetPack for .NET

[![Version](https://img.shields.io/nuget/vpre/Jetsons.JetPack.svg)](https://www.nuget.org/packages/Jetsons.JetPack)
[![Downloads](https://img.shields.io/nuget/dt/Jetsons.JetPack.svg)](https://www.nuget.org/packages/Jetsons.JetPack)
[![GitHub contributors](https://img.shields.io/github/contributors/jetsons/JetPack.Net.svg)](https://github.com/jetsons/JetPack.Net/graphs/contributors)
[![License](https://img.shields.io/github/license/jetsons/JetPack.Net.svg)](https://github.com/jetsons/JetPack.Net/blob/master/LICENSE)

To use this simply grab our Nuget package `Jetsons.JetPack` and add this to the top of your class:

    using Jetsons.JetPack;
	
This statement unlocks all the extension methods below. Enjoy!

Supported platforms:

- .NET Framework 4.5
- .NET Standard 2.0

### Addons

If you enjoyed JetPack, check out our add-on libraries that add more flavor and spice!

- [JetPack.Data](https://github.com/jetsons/JetPack.Data.Net) - read and write CSV, JSON, MessagePack and Archive files.
- [JetPack.Office](https://github.com/jetsons/JetPack.Office.Net) - read and write Word, Excel, PowerPoint and PDF files.
- [JetPack.Image](https://github.com/jetsons/JetPack.Image.Net) - read and write PNG, JPG, BMP and ICO images.
- [JetPack.Text](JetPack.Text.Net) - extract high quality text from Word, Excel, PowerPoint and PDF.

And if you want a similar set of extension methods for your JavaScript and Node.js projects:

- [Jetpack.js](https://github.com/jetsons/Jetpack.js) 

### Extensions

Extension methods for Strings:

- string.**Or**
- string.**Exists**
- string.**Blank**
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
- string.**ToCamelCase**
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
- string.**RemoveSpaces**
- string.**RemoveMultipleSpaces**
- string.**RemoveMultipleNewlines**
- string.**RemoveSymbols**
- string.**Truncate**
- string.**Repeat**
- string.**Part**
- string.**CountPrefix**
- string.**CountPostfix**
- string.**WebSafeFilename**
- string.**EnsureValidIndex**
- string.**RestrictToURLComponent**
- string.**RestrictToLettersNumbers**
- string.**RestrictToAsciiLettersNumbers**
- string.**RestrictToAsciiNumbers**
- string.**RestrictToFilename**

Extension methods for Characters:

- char.**IsNumber**
- char.**IsAsciiNumber**
- char.**IsSymbol**
- char.**IsLetter**
- char.**IsAsciiLetter**
- char.**IsLetterOrDigit**
- char.**IsHexDigit**
- char.**IsLower**
- char.**IsUpper**
- char.**IsNewline**
- char.**IsWhitespace**
- char.**ToLower**
- char.**ToUpper**

Extension methods for String encodings:

- string.**EncodeBase64**
- string.**DecodeBase64**
- bytes.**EncodeBase64**
- string.**EncodeHTML**
- string.**DecodeHTML**
- string.**EncodeUTF8**
- string.**EncodeANSI**
- bytes.**DecodeUTF8**
- bytes.**DecodeANSI**
- string.**EncodeStringLiteral**
- string.**DecodeStringLiteral**

Extension methods for manipulating file path Strings:

- string.**IsPathValid**
- string.**IsFolderPath**
- string.**FilenameAndExt**
- string.**Filename**
- string.**FolderName**
- string.**Extension**
- string.**SetFilenameAndExt**
- string.**SetFilename**
- string.**SetExtension**
- string.**ParentFolder**
- string.**AddPath**
- string.**SetSlash**

Extension methods for file manipulation performed using file path Strings:

- string.**FileExists**
- string.**FolderExists**
- string.**NoFile**
- string.**NoFolder**
- string.**EnsureFolderExists**
- string.**DeleteFolder**
- string.**EmptyFolder**
- string.**FileSize**
- string.**FileCreatedDate**
- string.**SetFileCreatedDate**
- string.**FileModifiedDate**
- string.**SetFileModifiedDate**
- string.**GetFilesInDirectory**
- string.**GetDirectoriesInDirectory**
- string.**OpenFileInDefaultApp**
- string.**OpenFolderInExplorer**
- string.**OpenFileInExplorer**
- string.**SearchForFile**
- string.**SearchForFiles**
- string.**CopyFile**
- string.**MoveFile**

Extension methods for file I/O performed using file path Strings:

- string.**LoadBytes**
- string.**LoadTextFile**
- string.**LoadAssembly**
- string.**LoadAssemblyTypes**

Extension methods for file I/O performed using content:

- string.**SaveToFile**
- string.**SaveToTempFile**
- xmlDoc.**SaveToFile**
- xmlDoc.**SaveToTempFile**

Extension methods for Byte arrays & Streams:

- bytes.**SaveToFile**
- bytes.**SaveToTempFile**
- bytes.**ToStream**
- bytes.**ToJetArray**
- stream.**SaveToFile**
- stream.**ToBytes**
- stream.**ToJetArray**

Extension methods for Binary formatting:

- any.**EncodeBinaryFormatted**
- bytes.**DecodeBinaryFormatted**

Extension methods for URL Strings:

- string.**DownloadURLToFile**

Extension methods for manipulating HTML snippet Strings:

- string.**Bold**
- string.**Italic**
- string.**Link**
- string.**Header**

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
- list.**Exists**
- list.**Blank**
- list.**HasSlot**
- list.**HasSlotAndValue**
- list.**First**
- list.**Last**
- list.**Repeat**
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
- number.**Map**
- number.**AtLeast**
- number.**AtMost**
- number.**Min**
- number.**Max**
- number.**GetFlag**
- number.**SetFlag**
- number.**ClearFlag**
- number.**BytesToString**
- number.**DecimalToHex**
- string.**HexToDecimal**

Extension methods for Numeric Lists:

- list.**Min**
- list.**Max**
- list.**IndexOfMin**
- list.**IndexOfMax**

Extension methods for Colors:

- int.**ToColor**
- color.**ToInt**

Extension methods for XML objects:

- xmlNode.**Node**
- xmlNode.**NodeValue**
- xmlNode.**Attribute**

Extension methods for WinForms Controls:

- control.**CrossThread**
- control.**VisibleCrossThread**
- control.**EnabledCrossThread**

Extension methods for WinForms RichTextBoxes:

- rtf.**HighlightAll**

### Static Classes

.NET applications:

- **AppResources.GetText**
- **AppResources.GetIcon**
- **AppResources.GetBitmap**
- **AppResources.GetBytes**

.NET assemblies:

- **AppAssemblies.RegisterFolder**
- **AppAssemblies.RegisterExtension**

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

Binary stream manipulation:

- **JetArray**
- **jetarray[]**
- **jetarray.Append**
- **jetarray.Xor**
- **jetarray.ToBytes**
- **jetarray.ToStream**
- **jetarray.ReadBytes**
- **jetarray.WriteBytes**
- **jetarray.ReadFlags**
- **jetarray.WriteFlags**
- **jetarray.ReadBitArray**
- **jetarray.WriteBitArray**
- **jetarray.ReadBoolean**
- **jetarray.WriteBoolean**
- **jetarray.ReadString**
- **jetarray.WriteString**
- **jetarray.ReadByte**
- **jetarray.WriteByte**
- **jetarray.ReadSByte**
- **jetarray.WriteSByte**
- **jetarray.ReadShort**
- **jetarray.WriteShort**
- **jetarray.ReadUShort**
- **jetarray.WriteUShort**
- **jetarray.ReadInt**
- **jetarray.WriteInt**
- **jetarray.ReadUInt**
- **jetarray.WriteUInt**
- **jetarray.ReadLong**
- **jetarray.WriteLong**
- **jetarray.ReadULong**
- **jetarray.WriteULong**
- **jetarray.ReadFloat**
- **jetarray.WriteFloat**
- **jetarray.ReadDouble**
- **jetarray.WriteDouble**
- **jetarray.ReadVarUInt32**
- **jetarray.WriteVarUInt32**
- **jetarray.ReadVarInt32**
- **jetarray.WriteVarInt32**
- **jetarray.ReadVarDouble**
- **jetarray.WriteVarDouble**

Common character codes are in : **Chars**