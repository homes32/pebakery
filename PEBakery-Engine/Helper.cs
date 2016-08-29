﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace PEBakery_Engine
{
    /// <summary>
    /// Contains static helper methods.
    /// </summary>
    public static class Helper
    {
        /// <summary>
        /// Count occurrences of strings.
        /// http://www.dotnetperls.com/string-occurrence
        /// </summary>
        public static int CountStringOccurrences(string text, string pattern)
        {
            // Loop through all instances of the string 'text'.
            int count = 0;
            int i = 0;
            while ((i = text.IndexOf(pattern, i)) != -1)
            {
                i += pattern.Length;
                count++;
            }
            return count;
        }

        /// <summary>
        /// Detect text file's encoding with BOM
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static Encoding DetectTextEncoding(string fileName)
        {
            byte[] bom = new byte[4];
            FileStream fs = null;

            try
            {
                fs = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
                fs.Read(bom, 0, bom.Length);
                fs.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("[ERR]\n{0}", e.ToString());
            }

            if (bom[0] == 0xEF && bom[1] == 0xBB && bom[2] == 0xBF)
                return Encoding.UTF8;
            else if (bom[0] == 0xFF && bom[1] == 0xF)
                return Encoding.Unicode;
            else if (bom[0] == 0xFE && bom[1] == 0xFF)
                return Encoding.BigEndianUnicode;
            return Encoding.Default;
        }

        /// <summary>
        /// Exception used in BakerEngine::ParseCommand
        /// </summary>
        public class UnsupportedEncodingException : Exception
        {
            public UnsupportedEncodingException() { }
            public UnsupportedEncodingException(string message) : base(message) { }
            public UnsupportedEncodingException(string message, Exception inner) : base(message, inner) { }
        }

        /// <summary>
        /// Write Unicode BOM into text file stream
        /// </summary>
        /// <param name="fs"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static FileStream WriteTextBOM(FileStream fs, Encoding encoding)
        {
            try
            {
                if (encoding == Encoding.UTF8)
                {
                    byte[] bom = new byte[] { 0xEF, 0xBB, 0xBF };
                    fs.Write(bom, 0, bom.Length);
                }
                else if (encoding == Encoding.Unicode)
                {
                    byte[] bom = new byte[] { 0xFF, 0xFE };
                    fs.Write(bom, 0, bom.Length);
                }
                else if (encoding == Encoding.BigEndianUnicode)
                {
                    byte[] bom = new byte[] { 0xFE, 0xFF };
                    fs.Write(bom, 0, bom.Length);
                }
                else if (encoding != Encoding.Default)
                { // Unsupported Encoding
                    throw new UnsupportedEncodingException(encoding.ToString() + " is not supported");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("[ERR]\n{0}", e.ToString());
            }

            return fs;
        }

        /// <summary>
        /// Read full text from file.
        /// Automatically detect encoding.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string ReadTextFile(string fileName)
        {
            FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
            char[] buffer = new char[fs.Length];
            StreamReader sr = new StreamReader(fs, Helper.DetectTextEncoding(fileName));
            sr.Read(buffer, 0, buffer.Length);
            sr.Close();
            fs.Close();
            return new string(buffer);
        }
    }
}
