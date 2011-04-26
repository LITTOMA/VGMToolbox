﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using VGMToolbox.util;

namespace VGMToolbox.format.iso
{    
    public enum CdSectorType
    { 
        Audio,
        Mode0,
        Mode1,
        Mode2,
        Mode2Form1,
        Mode2Form2,
        Unknown
    };
    
    public class CdRom
    {
        public static readonly byte[] SYNC_BYTES = new byte[] { 0x00, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 
                                                                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0x00 };

        public const long RAW_SECTOR_SIZE = 2352;
        public const long NON_RAW_SECTOR_SIZE = 2048;
        public const int MAX_HEADER_SIZE = 0x18;

        public static readonly Dictionary<CdSectorType, int> ModeHeaderSize = new Dictionary<CdSectorType, int>()
        {
            { CdSectorType.Audio, 0 },
            { CdSectorType.Mode0, 0x10 },
            { CdSectorType.Mode1, 0x10 },
            { CdSectorType.Mode2, 0x10 },
            { CdSectorType.Mode2Form1, 0x18 },
            { CdSectorType.Mode2Form2, 0x18 }

        };
                
        public static readonly Dictionary<CdSectorType, int> ModeDataSize = new Dictionary<CdSectorType, int>()
        {
            { CdSectorType.Audio, 2352 },
            { CdSectorType.Mode0, 2336 },
            { CdSectorType.Mode1, 2048 },
            { CdSectorType.Mode2, 2336 },
            { CdSectorType.Mode2Form1, 2048 },
            { CdSectorType.Mode2Form2, 2324 }

        };

        public static CdSectorType GetSectorType(byte[] headerBytes)
        {
            CdSectorType mode;

            if (headerBytes.Length != 0x18)
            {
                mode = CdSectorType.Unknown;
            }
            else
            {
                switch (headerBytes[0x0F])
                {
                    case 0x00:
                        mode = CdSectorType.Mode0;
                        break;
                    case 0x01:
                        mode = CdSectorType.Mode1;
                        break;
                    case 0x02:
                        if (headerBytes[0x10] == headerBytes[0x14] &&
                            headerBytes[0x11] == headerBytes[0x15] &&
                            headerBytes[0x12] == headerBytes[0x16] &&
                            headerBytes[0x13] == headerBytes[0x17])
                        {
                            switch (headerBytes[0x12])
                            {
                                case 0x64:
                                    mode = CdSectorType.Mode2Form2;
                                    break;
                                default:
                                    mode = CdSectorType.Mode2Form1;
                                    break;
                            }
                        }
                        else
                        {
                            mode = CdSectorType.Mode2;
                        }
                        break;
                    default:
                        mode = CdSectorType.Unknown;
                        break;
                }
            }

            return mode;
        }

        public static byte[] GetDataChunkFromSector(byte[] sectorBytes, bool isRaw)
        {
            CdSectorType mode;
            byte[] sectorHeader;
            byte[] dataChunk;

            if (isRaw)
            {
                sectorHeader = ParseFile.ParseSimpleOffset(sectorBytes, 0, MAX_HEADER_SIZE);
                mode = GetSectorType(sectorHeader);

                dataChunk = ParseFile.ParseSimpleOffset(sectorBytes, CdRom.ModeHeaderSize[mode], CdRom.ModeDataSize[mode]);
            }
            else
            {
                dataChunk = sectorBytes;
            }
            
            return dataChunk;
        }

        public static byte[] GetSectorByLba(Stream cdStream,
            long volumeBaseOffset, long lba, bool isRaw, 
            int nonRawSectorSize)
        {
            long sectorOffset;
            byte[] sectorBytes;            

            if (isRaw)
            {
                sectorOffset = volumeBaseOffset + (lba * CdRom.RAW_SECTOR_SIZE);
                sectorBytes = ParseFile.ParseSimpleOffset(cdStream, sectorOffset, (int)CdRom.RAW_SECTOR_SIZE);
            }
            else
            {
                sectorOffset = volumeBaseOffset + (lba * nonRawSectorSize);
                sectorBytes = ParseFile.ParseSimpleOffset(cdStream, sectorOffset, nonRawSectorSize);
            }
            
            return sectorBytes;
        }

        public static void ExtractCdData(Stream cdStream, 
            string destinationPath, long volumeBaseOffset, 
            long lba, long length, bool isRaw, long nonRawSectorSize)
        {
            long offset;
            int maxWriteSize;
            long bytesWritten = 0;
            
            CdSectorType mode;
            long lbaCounter = 0;

            byte[] sectorHeader;
            byte[] sector;

            // create directory
            string destinationFolder = Path.GetDirectoryName(destinationPath);

            if (!Directory.Exists(destinationFolder))
            {
                Directory.CreateDirectory(destinationFolder);
            }

            if (isRaw)
            {
                using (FileStream outStream = File.OpenWrite(destinationPath))
                {
                    while (bytesWritten < length)
                    {
                        offset = volumeBaseOffset + ((lba + lbaCounter) * CdRom.RAW_SECTOR_SIZE);
                        sector = ParseFile.ParseSimpleOffset(cdStream, offset, (int)CdRom.RAW_SECTOR_SIZE);
                        sectorHeader = ParseFile.ParseSimpleOffset(sector, 0, MAX_HEADER_SIZE);
                        mode = GetSectorType(sectorHeader);

                        maxWriteSize = CdRom.ModeDataSize[mode] < (length - bytesWritten) ? CdRom.ModeDataSize[mode] : (int)(length - bytesWritten);
                        outStream.Write(sector, CdRom.ModeHeaderSize[mode], maxWriteSize);

                        bytesWritten += maxWriteSize;
                        lbaCounter++;
                    }
                }
            }
            else
            { 
                offset = volumeBaseOffset + (lba * nonRawSectorSize);
                ParseFile.ExtractChunkToFile(cdStream, offset, length, destinationPath);
            }
        }

    }
}