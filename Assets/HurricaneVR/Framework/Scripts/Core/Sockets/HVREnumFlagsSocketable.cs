using System;
using HurricaneVR.Framework.Core.Utils;

namespace HurricaneVR.Framework.Core.Sockets
{
    public class HVREnumFlagsSocketable<TEnum> : HVRSocketable where TEnum : Enum
    {
        [EnumFlag]
        public TEnum SocketType;
    }
}