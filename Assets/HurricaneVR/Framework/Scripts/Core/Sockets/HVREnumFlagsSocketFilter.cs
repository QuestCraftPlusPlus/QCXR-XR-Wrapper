using System;
using HurricaneVR.Framework.Core.Utils;

namespace HurricaneVR.Framework.Core.Sockets
{
    public class HVREnumFlagsSocketFilter<TEnum> : HVRSocketFilter where TEnum : Enum
    {
        [EnumFlag]
        public TEnum SocketType;

        public override bool IsValid(HVRSocketable socketable)
        {
            if (!socketable) 
                return false;
            var socketableEnumFlags = socketable as HVREnumFlagsSocketable<TEnum>;
            if (socketableEnumFlags == null) 
                return false;
            if ((int)(object)socketableEnumFlags.SocketType == 0)
                return false;
            return socketableEnumFlags.SocketType.HasFlag(SocketType);
            //return SocketType.HasFlag(enumFilter.SocketType);
        }
    }
}