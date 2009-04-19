﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using ICSharpCode.SharpZipLib.Checksums;

using VGMToolbox.util;

namespace VGMToolbox.format
{
    class Genh : IFormat
    {
        public static readonly byte[] ASCII_SIGNATURE = new byte[] { 0x47, 0x45, 0x4E, 0x48 };
        private const string FORMAT_ABBREVIATION = "GENH";
        public static readonly byte[] CURRENT_VERSION = new byte[] { 0x34, 0x30, 0x43, 0x23 };
        public const UInt32 GENH_HEADER_SIZE = 0x1000;
        public const string FILE_EXTENSION = ".genh";

        private const int SIG_OFFSET = 0x00;
        private const int SIG_LENGTH = 0x04;

        private const int CHANNELS_OFFSET = 0x04;
        private const int CHANNELS_LENGTH = 0x04;

        private const int INTERLEAVE_OFFSET = 0x08;
        private const int INTERLEAVE_LENGTH = 0x04;

        private const int FREQUENCY_OFFSET = 0x0C;
        private const int FREQUENCY_LENGTH = 0x04;

        private const int LOOP_START_OFFSET = 0x10;
        private const int LOOP_START_LENGTH = 0x04;

        private const int LOOP_END_OFFSET = 0x14;
        private const int LOOP_END_LENGTH = 0x04;

        private const int IDENTIFIER_OFFSET = 0x18;
        private const int IDENTIFIER_LENGTH = 0x04;

        private const int AUDIO_START_OFFSET = 0x1C;
        private const int AUDIO_START_LENGTH = 0x04;

        private const int HEADER_LENGTH_OFFSET = 0x20;
        private const int HEADER_LENGTH_LENGTH = 0x04;

        private const int ORIG_FILENAME_OFFSET = 0x200;
        private const int ORIG_FILENAME_LENGTH = 0x100;

        private const int ORIG_FILESIZE_OFFSET = 0x300;
        private const int ORIG_FILESIZE_LENGTH = 0x04;

        private const int GENH_VERSION_OFFSET = 0x304;
        private const int GENH_VERSION_LENGTH = 0x04;

        private byte[] asciiSignature;
        private byte[] channels;
        private byte[] interleave;
        private byte[] frequency;
        private byte[] loopStart;
        private byte[] loopEnd;
        private byte[] identifier;
        private byte[] audioStart;
        private byte[] headerLength;
        private byte[] originalFileName;
        private byte[] originalFileSize;                
        private byte[] versionNumber;

        private string filePath;
        public string FilePath
        {
            get { return filePath; }
            set { filePath = value; }
        }

        public byte[] AsciiSignature { get { return asciiSignature; } }
        public byte[] Channels { get { return channels; } }
        public byte[] Interleave { get { return interleave; } }
        public byte[] Frequency { get { return frequency; } }
        public byte[] LoopStart { get { return loopStart; } }
        public byte[] LoopEnd { get { return loopEnd; } }
        public byte[] Identifier { get { return identifier; } }
        public byte[] AudioStart { get { return audioStart; } }
        public byte[] HeaderLength { get { return headerLength; } }
        public byte[] OriginalFileName { get { return originalFileName; } }
        public byte[] OriginalFileSize { get { return originalFileSize; } }
        public byte[] VersionNumber { get { return versionNumber; } }

        Dictionary<string, string> tagHash = new Dictionary<string, string>();

        #region IFormat

        public void Initialize(Stream pStream, string pFilePath)
        {
            this.filePath = pFilePath;            
            this.asciiSignature = ParseFile.parseSimpleOffset(pStream, SIG_OFFSET, SIG_LENGTH);
            this.channels = ParseFile.parseSimpleOffset(pStream, CHANNELS_OFFSET, CHANNELS_LENGTH);
            this.interleave = ParseFile.parseSimpleOffset(pStream, INTERLEAVE_OFFSET, INTERLEAVE_LENGTH);
            this.frequency = ParseFile.parseSimpleOffset(pStream, FREQUENCY_OFFSET, FREQUENCY_LENGTH);
            this.loopStart = ParseFile.parseSimpleOffset(pStream, LOOP_START_OFFSET, LOOP_START_LENGTH);
            this.loopEnd = ParseFile.parseSimpleOffset(pStream, LOOP_END_OFFSET, LOOP_END_LENGTH);
            this.identifier = ParseFile.parseSimpleOffset(pStream, IDENTIFIER_OFFSET, IDENTIFIER_LENGTH);
            this.audioStart = ParseFile.parseSimpleOffset(pStream, AUDIO_START_OFFSET, AUDIO_START_LENGTH);
            this.headerLength = ParseFile.parseSimpleOffset(pStream, HEADER_LENGTH_OFFSET, HEADER_LENGTH_LENGTH);
            this.originalFileName = ParseFile.parseSimpleOffset(pStream, ORIG_FILENAME_OFFSET, ORIG_FILENAME_LENGTH);
            this.originalFileSize = ParseFile.parseSimpleOffset(pStream, ORIG_FILESIZE_OFFSET, ORIG_FILESIZE_LENGTH);
            this.versionNumber = ParseFile.parseSimpleOffset(pStream, GENH_VERSION_OFFSET, GENH_VERSION_LENGTH);

            this.initializeTagHash();
        }

        private void initializeTagHash()
        {
            System.Text.Encoding enc = System.Text.Encoding.ASCII;

            tagHash.Add("GENH Version", enc.GetString(FileUtil.ReplaceNullByteWithSpace(this.versionNumber)));
            tagHash.Add("Channels", String.Format("0x{0}", BitConverter.ToUInt32(this.channels, 0).ToString("X4")));
            tagHash.Add("Interleave", String.Format("0x{0}", BitConverter.ToUInt32(this.interleave, 0).ToString("X4")));
            tagHash.Add("Frequency", String.Format("0x{0}", BitConverter.ToUInt32(this.frequency, 0).ToString("X4")));
            tagHash.Add("Loop Start", String.Format("0x{0}", BitConverter.ToUInt32(this.loopStart, 0).ToString("X4")));
            tagHash.Add("Loop End", String.Format("0x{0}", BitConverter.ToUInt32(this.loopEnd, 0).ToString("X4")));
            tagHash.Add("Identifier", String.Format("0x{0}", BitConverter.ToUInt32(this.identifier, 0).ToString("X4")));
            tagHash.Add("Audio Start", String.Format("0x{0}", BitConverter.ToUInt32(this.audioStart, 0).ToString("X4")));
            tagHash.Add("GENH Header Length", String.Format("0x{0}", BitConverter.ToUInt32(this.headerLength, 0).ToString("X4")));            
            tagHash.Add("Original File Name", enc.GetString(FileUtil.ReplaceNullByteWithSpace(this.originalFileName)));
            tagHash.Add("Original File Size", String.Format("0x{0}", BitConverter.ToUInt32(this.originalFileSize, 0).ToString("X4")));            
        }

        public byte[] GetAsciiSignature()
        {
            return ASCII_SIGNATURE;
        }
        public string GetFileExtensions()
        {
            return null;
        }
        public string GetFormatAbbreviation()
        {
            return FORMAT_ABBREVIATION;
        }
        public bool IsFileLibrary() { return false; }
        public bool HasMultipleFileExtensions()
        {
            return false;
        }
        public bool UsesLibraries() { return false; }
        public bool IsLibraryPresent() { return true; }
        public Dictionary<string, string> GetTagHash()
        {
            return this.tagHash;
        }

        public void GetDatFileCrc32(ref Crc32 pChecksum)
        {
            pChecksum.Reset();

            using (FileStream fs = File.Open(this.filePath, FileMode.Open, FileAccess.Read))
            {
                using (BinaryReader br = new BinaryReader(fs))
                {
                    br.BaseStream.Position = (long)BitConverter.ToUInt32(this.headerLength, 0);

                    byte[] data = new byte[Constants.FILE_READ_CHUNK_SIZE];
                    int bytesRead;

                    while ((bytesRead = br.Read(data, 0, data.Length)) > 0)
                    {
                        pChecksum.Update(data, 0, bytesRead);
                    }
                }               
            }
        }
        
        #endregion
    }
}