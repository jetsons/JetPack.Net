using System;
using System.Collections.Generic;
using System.Text;

namespace Jetsons.MsgPack
{
    public interface IMessagePackSerializationCallbackReceiver
    {
        void OnBeforeSerialize();
        void OnAfterDeserialize();
    }
}
