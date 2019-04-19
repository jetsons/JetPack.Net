#if NETSTANDARD || NETFRAMEWORK

using Jetsons.MsgPack.Formatters;
using Jetsons.MsgPack.Internal;
using System;

namespace Jetsons.MsgPack.Resolvers
{
    public sealed class UnsafeBinaryResolver : IFormatterResolver
    {
        public static readonly IFormatterResolver Instance = new UnsafeBinaryResolver();

        UnsafeBinaryResolver()
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
                formatter = (IMessagePackFormatter<T>)UnsafeBinaryResolverGetFormatterHelper.GetFormatter(typeof(T));
            }
        }
    }
}

namespace Jetsons.MsgPack.Internal
{
    internal static class UnsafeBinaryResolverGetFormatterHelper
    {
        internal static object GetFormatter(Type t)
        {
            if (t == typeof(Guid))
            {
                return BinaryGuidFormatter.Instance;
            }
            else if (t == typeof(Guid?))
            {
                return new StaticNullableFormatter<Guid>(BinaryGuidFormatter.Instance);
            }
            else if (t == typeof(Decimal))
            {
                return BinaryDecimalFormatter.Instance;
            }
            else if (t == typeof(Decimal?))
            {
                return new StaticNullableFormatter<Decimal>(BinaryDecimalFormatter.Instance);
            }

            return null;
        }
    }
}

#endif