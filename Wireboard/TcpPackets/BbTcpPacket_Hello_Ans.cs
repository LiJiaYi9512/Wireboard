﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wireboard.TcpPackets
{
    class BbTcpPacket_Hello_Ans : BbTcpPacket
    {
        public byte MaxSupportedVersion { get; private set; }
        public byte MinSupportedVersion { get; private set; }
        public bool RequiresPassword { get; private set; }
        public String Desc { get; private set; } = "";
        public int ServerGUID { get; private set; }
        public bool SupportsScreenCapture { get; private set; }
        public bool IsProVersion { get; private set; }

        internal BbTcpPacket_Hello_Ans(BbTcpPacket src) : base(src)
        {
        }

        internal void ProcessData(BinaryReader data)
        {
            // [MaxVer 1][MinVer 1][ReqPassword 1][Desc String][ServerGUID 4]
            MaxSupportedVersion = data.ReadByte();
            MinSupportedVersion = data.ReadByte();
            RequiresPassword = data.ReadByte() > 0;
            Desc = BBProtocol.ReadString(data);
            ServerGUID = data.ReadInt32();

            if (MaxSupportedVersion >= 2)
            {
                // [SupportsScreenCapture 1][ProVersion 1]
                SupportsScreenCapture = data.ReadByte() > 0;
                IsProVersion = data.ReadByte() > 0;
            }
            IsValid = true;
        }
    }
}
