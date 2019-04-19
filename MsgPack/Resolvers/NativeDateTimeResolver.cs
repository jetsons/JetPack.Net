using Jetsons.MsgPack.Formatters;
using Jetsons.MsgPack.Internal;
using System;

namespace Jetsons.MsgPack.Resolvers
{
    public sealed class NativeDateTimeResolver : IFormatterResolver
    {
        public static readonly IFormatterResolver Instance = new NativeDateTimeResolver();

        NativeDateTimeResolver()
        {

        }

        public IMessagePackFormatter<T> GetFormatter<T>()
        {
            return FormatterCache<T>.formatter;
        }

        static class FormatterCache<T>
        {
            public static readonly IMessagePackFormatter<T> formatter;

            static FormatterCache()
            {
                formatter = (IMessagePackFormatter<T>)NativeDateTimeResolverGetFormatterHelper.GetFormatter(typeof(T));
            }
        }
    }
}

namespace Jetsons.MsgPack.Internal
{
    internal static class NativeDateTimeResolverGetFormatterHelper
    {
        internal static object GetFormatter(Type t)
        {
            if (t == typeof(DateTime))
            {
                return NativeDateTimeFormatter.Instance;
            }
            else if (t == typeof(DateTime?))
            {
                return new StaticNullableFormatter<DateTime>(NativeDateTimeFormatter.Instance);
            }
            else if (t == typeof(DateTime[]))
            {
                return NativeDateTimeArrayFormatter.Instance;
            }

            return null;
        }
    }
}