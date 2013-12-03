using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Microsoft.Samples.Kinect.AudioBasics
{
    public class WAVEFORMATEX
    {
        private const int WF_OFFSET_FORMATTAG = 20;
        private const int WF_OFFSET_CHANNELS = 22;
        private const int WF_OFFSET_SAMPLESPERSEC = 24;
        private const int WF_OFFSET_AVGBYTESPERSEC = 28;
        private const int WF_OFFSET_BLOCKALIGN = 32;
        private const int WF_OFFSET_BITSPERSAMPLE = 34;
        public const int WF_OFFSET_DATA = 44;
        public short cbSize;

        public ushort wFormatTag = 0;
        public ushort nChannels = 0;
        public uint nSamplesPerSec = 0;
        public uint nAvgBytesPerSec = 0;
        public ushort nBlockAlign = 0;
        public ushort wBitsPerSample = 0;

        public void SeekTo(Stream fs)
        {
            fs.Seek(WF_OFFSET_FORMATTAG, SeekOrigin.Begin);
        }

        public void Skip(Stream fs)
        {
            fs.Seek(WF_OFFSET_DATA, SeekOrigin.Begin);
        }

        public uint Read(BinaryReader rdr)
        {
            wFormatTag = rdr.ReadUInt16();
            nChannels = rdr.ReadUInt16();
            nSamplesPerSec = rdr.ReadUInt32();
            nAvgBytesPerSec = rdr.ReadUInt32();
            nBlockAlign = rdr.ReadUInt16();
            wBitsPerSample = rdr.ReadUInt16();

            // Unused subchunk Id and size
            uint dataId = rdr.ReadUInt32();
            uint dataLength = rdr.ReadUInt32();

            return dataLength;
        }

        public void Write(BinaryWriter wrtr)
        {
            wrtr.Write(wFormatTag);
            wrtr.Write(nChannels);
            wrtr.Write(nSamplesPerSec);
            wrtr.Write(nAvgBytesPerSec);
            wrtr.Write(nBlockAlign);
            wrtr.Write(wBitsPerSample);
        }
    }
}
