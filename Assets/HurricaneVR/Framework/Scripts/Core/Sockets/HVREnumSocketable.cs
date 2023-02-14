using System;

namespace HurricaneVR.Framework.Core.Sockets
{
    public class HVREnumSocketable<TEnum> : HVRSocketable where TEnum : Enum
    {
        public TEnum SocketType;
    }
}