# Jetpack.Net

To use this simply grab our Nuget package Jetpack.Net and add this to the top of your class:

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
- string.**LoadJSON**
- string.**LoadCSV**
- string.**LoadZFO**
- string.**LoadMsgPack**
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

Extension methods for Objects relating to file I/O:

- any.**SaveToFileJSON**
- any.**SaveToFileZFO**
- any.**SaveToFileMsgPack**

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

### ZFO

Fastest C# Serializer and Deserializer for .NET

Types supported : All primitives, All enums, TimeSpan, DateTime, DateTimeOffset, Guid, Tuple<,...>, KeyValuePair<,>, KeyTuple<,...>, Array, List<>, HashSet<>, Dictionary<,>, ReadOnlyCollection<>, ReadOnlyDictionary<,>, IEnumerable<>, ICollection<>, IList<>, ISet<,>, IReadOnlyCollection<>, IReadOnlyList<>, IReadOnlyDictionary<,>, ILookup<,> and inherited ICollection<> with paramterless constructor.

### JSON

Fastest and Zero Allocation JSON Serializer for C#

Types supported : Primitives(int, string, etc...), Enum, Nullable<>, TimeSpan, DateTime, DateTimeOffset, Guid, Uri, Version, StringBuilder, BitArray, Type, ArraySegment<>, BigInteger, Complext, ExpandoObject , Task, Array[], Array[,], Array[,,], Array[,,,], KeyValuePair<,>, Tuple<,...>, ValueTuple<,...>, List<>, LinkedList<>, Queue<>, Stack<>, HashSet<>, ReadOnlyCollection<>, IList<>, ICollection<>, IEnumerable<>, Dictionary<,>, IDictionary<,>, SortedDictionary<,>, SortedList<,>, ILookup<,>, IGrouping<,>, ObservableCollection<>, ReadOnlyOnservableCollection<>, IReadOnlyList<>, IReadOnlyCollection<>, ISet<>, ConcurrentBag<>, ConcurrentQueue<>, ConcurrentStack<>, ReadOnlyDictionary<,>, IReadOnlyDictionary<,>, ConcurrentDictionary<,>, Lazy<>, Task<>, custom inherited ICollection<> or IDictionary<,> with paramterless constructor, IEnumerable, ICollection, IList, IDictionary and custom inherited ICollection or IDictionary with paramterless constructor(includes ArrayList and Hashtable), Exception, your own class or struct(includes anonymous type).

### MsgPack

Extremely Fast MessagePack Serializer for C#

Types supported : Primitives(int, string, etc...), Enum, Nullable<>, TimeSpan, DateTime, DateTimeOffset, Nil, Guid, Uri, Version, StringBuilder, BitArray, ArraySegment<>, BigInteger, Complext, Task, Array[], Array[,], Array[,,], Array[,,,], KeyValuePair<,>, Tuple<,...>, ValueTuple<,...>, List<>, LinkedList<>, Queue<>, Stack<>, HashSet<>, ReadOnlyCollection<>, IList<>, ICollection<>, IEnumerable<>, Dictionary<,>, IDictionary<,>, SortedDictionary<,>, SortedList<,>, ILookup<,>, IGrouping<,>, ObservableCollection<>, ReadOnlyOnservableCollection<>, IReadOnlyList<>, IReadOnlyCollection<>, ISet<>, ConcurrentBag<>, ConcurrentQueue<>, ConcurrentStack<>, ReadOnlyDictionary<,>, IReadOnlyDictionary<,>, ConcurrentDictionary<,>, Lazy<>, Task<>, custom inherited ICollection<> or IDictionary<,> with paramterless constructor, IList, IDictionary and custom inherited ICollection or IDictionary with paramterless constructor(includes ArrayList and Hashtable).
