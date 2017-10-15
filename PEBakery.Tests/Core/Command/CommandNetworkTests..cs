﻿/*
    Copyright (C) 2017 Hajin Jang
    Licensed under GPL 3.0
 
    PEBakery is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PEBakery.Core;
using System.IO;
using PEBakery.Helper;
using System.Text;
using System.Linq;
using System.Net.NetworkInformation;

namespace PEBakery.Tests.Core.Command
{
    [TestClass]
    public class CommandNetworkTests
    {
        #region WebGet
        [TestMethod]
        [TestCategory("Command")]
        [TestCategory("CommandNetwork")]
        public void Network_WebGet()
        { // WebGet,<URL>,<DestPath>,[HashType],[HashDigest]
            if (NetworkInterface.GetIsNetworkAvailable())
            { // This test will be skipped if the computer is disconnected
                WebGet_1();
            }

            // files: test
            WebGet_MD5();
            WebGet_SHA1();
            WebGet_SHA256();
            WebGet_SHA384();
            WebGet_SHA512();
            WebGet_HashError();
        }

        public void WebGet_1()
        {
            string tempFile = Path.GetTempFileName();
            File.Delete(tempFile);

            try
            {
                string rawCode = $"WebGet,\"https://github.com\",\"{tempFile}\"";
                EngineState s = EngineTests.Eval(rawCode, CodeType.WebGet, ErrorCheck.Success);

                Assert.IsTrue(File.Exists(tempFile));
            }
            finally
            {
                if (File.Exists(tempFile))
                    File.Delete(tempFile);
            }
        }

        public void WebGet_MD5()
        {
            string tempSrc = CommandHashTests.SampleText();
            string tempDest = Path.GetTempFileName();
            File.Delete(tempDest);

            try
            {
                Uri fileUri = new Uri(tempSrc);
                string rawCode = $"WebGet,\"{fileUri.AbsoluteUri}\",\"{tempDest}\",MD5,1179cf94187d2d2f94010a8d39099543";
                EngineState s = EngineTests.Eval(rawCode, CodeType.WebGet, ErrorCheck.Success);

                Assert.IsTrue(File.Exists(tempSrc));
            }
            finally
            {
                if (File.Exists(tempSrc))
                    File.Delete(tempSrc);
                if (File.Exists(tempDest))
                    File.Delete(tempDest);
            }
        }

        public void WebGet_SHA1()
        {
            string tempSrc = CommandHashTests.SampleText();
            string tempDest = Path.GetTempFileName();
            File.Delete(tempDest);

            try
            {
                Uri fileUri = new Uri(tempSrc);
                string rawCode = $"WebGet,\"{fileUri.AbsoluteUri}\",\"{tempDest}\",SHA1,0aaac8883f1c8dd48dbf974299a9422f1ab437ee";
                EngineState s = EngineTests.Eval(rawCode, CodeType.WebGet, ErrorCheck.Success);

                Assert.IsTrue(File.Exists(tempDest));
            }
            finally
            {
                if (File.Exists(tempSrc))
                    File.Delete(tempSrc);
                if (File.Exists(tempDest))
                    File.Delete(tempDest);
            }
        }

        public void WebGet_SHA256()
        {
            string tempSrc = CommandHashTests.SampleText();
            string tempDest = Path.GetTempFileName();
            File.Delete(tempDest);

            try
            {
                Uri fileUri = new Uri(tempSrc);
                string rawCode = $"WebGet,\"{fileUri.AbsoluteUri}\",\"{tempDest}\",SHA256,3596bc5a263736c9d5b9a06e85a66ed2a866b457a44e5ed8548e504ca5599772";
                EngineState s = EngineTests.Eval(rawCode, CodeType.WebGet, ErrorCheck.Success);

                Assert.IsTrue(File.Exists(tempDest));
            }
            finally
            {
                if (File.Exists(tempSrc))
                    File.Delete(tempSrc);
                if (File.Exists(tempDest))
                    File.Delete(tempDest);
            }
        }

        public void WebGet_SHA384()
        {
            string tempSrc = CommandHashTests.SampleText();
            string tempDest = Path.GetTempFileName();
            File.Delete(tempDest);

            try
            {
                Uri fileUri = new Uri(tempSrc);
                string rawCode = $"WebGet,\"{fileUri.AbsoluteUri}\",\"{tempDest}\",SHA384,e068a3ac0b4ab4b37306dc354af6b8a4c89ef3fbbf1db969ec6d6a4281f1ab1f472fcd7bc2f16c0cf41c1991056846a6";
                EngineState s = EngineTests.Eval(rawCode, CodeType.WebGet, ErrorCheck.Success);

                Assert.IsTrue(File.Exists(tempDest));
            }
            finally
            {
                if (File.Exists(tempSrc))
                    File.Delete(tempSrc);
                if (File.Exists(tempDest))
                    File.Delete(tempDest);
            }
        }

        public void WebGet_SHA512()
        {
            string tempSrc = CommandHashTests.SampleText();
            string tempDest = Path.GetTempFileName();
            File.Delete(tempDest);

            try
            {
                Uri fileUri = new Uri(tempSrc);
                string rawCode = $"WebGet,\"{fileUri.AbsoluteUri}\",\"{tempDest}\",SHA512,f5829cb5e052ab5ef6820630fd992acabb798512d21b5c5295fb81b88b74f3812863c0804e730f26e166b51d77eb5f1de200fd75913278522da78fbb269600cc";
                EngineState s = EngineTests.Eval(rawCode, CodeType.WebGet, ErrorCheck.Success);

                Assert.IsTrue(File.Exists(tempDest));
            }
            finally
            {
                if (File.Exists(tempSrc))
                    File.Delete(tempSrc);
                if (File.Exists(tempDest))
                    File.Delete(tempDest);
            }
        }

        public void WebGet_HashError()
        {
            string tempSrc = CommandHashTests.SampleText();
            string tempDest = Path.GetTempFileName();
            File.Delete(tempDest);

            try
            {
                Uri fileUri = new Uri(tempSrc);
                string rawCode = $"WebGet,\"{fileUri.AbsoluteUri}\",\"{tempDest}\",MD5,00000000000000000000000000000000";
                EngineState s = EngineTests.Eval(rawCode, CodeType.WebGet, ErrorCheck.Error);
            }
            finally
            {
                if (File.Exists(tempSrc))
                    File.Delete(tempSrc);
                if (File.Exists(tempDest))
                    File.Delete(tempDest);
            }
        }
        #endregion

        #region WebGetIfNotExist
        [TestMethod]
        [TestCategory("Command")]
        [TestCategory("CommandNetwork")]
        public void Network_WebGetIfNotExist()
        { // WebGetIfNotExist,<URL>,<DestPath>,[HashType],[HashDigest]
            WebGetIfNotExist_Local();
        }

        public void WebGetIfNotExist_Local()
        {
            string tempSrc = CommandHashTests.SampleText();
            string tempDest = Path.GetTempFileName();

            try
            {
                Uri fileUri = new Uri(tempSrc);
                string rawCode = $"WebGetIfNotExist,\"{fileUri.AbsoluteUri}\",\"{tempDest}\"";
                EngineState s = EngineTests.Eval(rawCode, CodeType.WebGetIfNotExist, ErrorCheck.Warning); // Wari

                // WebGet should have been ignored
                FileInfo info = new FileInfo(tempDest);
                Assert.IsTrue(info.Length == 0);
            }
            finally
            {
                if (File.Exists(tempSrc))
                    File.Delete(tempSrc);
                if (File.Exists(tempDest))
                    File.Delete(tempDest);
            }
        }
        #endregion
    }
}
