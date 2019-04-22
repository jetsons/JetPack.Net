# JetPack

### Extensions

Extension methods for Strings:

- **Exists**
- **IsNumber**
- **IsSymbol**
- **IsLetter**
- **Contains**
- **BeginsWith**
- **EndsWith**
- **Between**
- **Before**
- **BeforeFirst**
- **BeforeLast**
- **After**
- **AfterFirst**
- **AfterLast**
- **BeforeIndex**
- **AfterIndex**
- **SmartSplit**
- **Chars**
- **Lines**

Extension methods for Strings, relating to file paths:

- **IsPathValid**
- **IsFolderPath**
- **FilenameAndExt**
- **Filename**
- **Extension**
- **ParentFolder**

Extension methods for Strings, relating to file I/O:

- **FileExists**
- **FolderExists**
- **EnsureFolderExists**
- **DeleteFolder**
- **EmptyFolder**
- **LoadBytes**
- **LoadZFO**
- **LoadTextFile**
- **SaveToFile** (bytes)
- **SaveToTempFile** (bytes)
- **SaveToFile** (string)
- **SaveToTempFile** (string)

Extension methods for Streams:

- **ToBytes**

Extension methods for Objects:

- **GetPropValue**
- **GetPropValue<T>**

Extension methods for Lists:

- **First**
- **Last**
- **Repeat**
- **Exists**
- **ToList**
- **ToArray**
- **Transpose**
- **AddIfExists**
- **AddOnce**

Extension methods for Object Lists:

- **GetProps**

Extension methods for Numeric Lists:

- **Min**
- **Max**
- **IndexOfMin**
- **IndexOfMax**

Message box utilities:

- **Messages.Info**
- **Messages.Error**
- **Messages.Question**

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
