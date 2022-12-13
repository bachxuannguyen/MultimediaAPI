using System;
using System.Diagnostics;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultimediaAPI.Libraries;

namespace MultimediaAPITest
{
    [TestClass]
    public class Test_LibMethods
    {
        [TestMethod]
        public void Test_GetMediaTypeNameById()
        {
            List<int> idSet = new() { 0, 1, 2, 3, 4, -1, 5 };
            List<string> nameSet = new()
            { "Image", "Infographic", "Video", "Sound", "Animation", String.Empty, String.Empty };
            for (int i = 0; i < idSet.Count; i++)
                Assert.AreEqual(nameSet[i], LibMethod.GetMediaTypeNameById(idSet[i]));
        }
        [TestMethod]
        public void Test_GetMediaTypeDescById()
        {
            List<int> idSet = new() { 0, 1, 2, 3, 4, -1, 5 };
            List<string> descSet = new()
            { "Ảnh", "Infographic", "Video", "Âm thanh", "Hoạt họa", String.Empty, String.Empty };
            for (int i = 0; i < idSet.Count; i++)
                Assert.AreEqual(descSet[i], LibMethod.GetMediaTypeDescById(idSet[i]));
        }
        [TestMethod]
        public void Test_GetMediaTypeIdByExtension()
        {
            List<ValueTuple<int, List<string>>> testSet = new()
            {
                ValueTuple.Create(0, new List<string>() { ".bmp", ".gif", ".png", ".jpg", ".jpeg" }),
                ValueTuple.Create(2, new List<string>() { ".mp4", ".mov", ".wmv", ".mkv" }),
                ValueTuple.Create(3, new List<string>() { ".mp3", ".wma" }),
                ValueTuple.Create(4, new List<string>() { ".fla", ".swf" }),
                ValueTuple.Create(-1, new List<string>() { null, "", ".fakeextension", "png" })
            };
            for (int i = 0; i < testSet.Count; i++)
            {
                for (int j = 0; j < testSet[i].Item2.Count; j++)
                    Assert.AreEqual(testSet[i].Item1, LibMethod.GetMediaTypeIdByExtension(testSet[i].Item2[j]));
            }
        }
        [TestMethod]
        public void Test_GetMediaTypeNameByExtension()
        {
            List<List<string>> extentionSet = new()
            {
                new List<string>() { ".bmp", ".gif", ".png", ".jpg", ".jpeg" },
                new List<string>() { ".mp4", ".mov", ".wmv", ".mkv" },
                new List<string>() { ".mp3", ".wma" },
                new List<string>() { ".fla", ".swf" },
                new List<string>() { null, "", ".fakeextension", "png" }
            };
            List<string> nameSet = new()
            {
                "Image", "Video", "Sound", "Animation", String.Empty
            };
            for (int i = 0; i < extentionSet.Count; i++)
                for (int j = 0; j < extentionSet[i].Count; j++)
                    Assert.AreEqual(nameSet[i], LibMethod.GetMediaTypeNameByExtension(extentionSet[i][j]));
        }
        [TestMethod]
        public void Test_GetMediaTypeDescByExtension()
        {
            List<List<string>> extensionSet = new()
            {
                new List<string>() { ".bmp", ".gif", ".png", ".jpg", ".jpeg" },
                new List<string>() { ".mp4", ".mov", ".wmv", ".mkv" },
                new List<string>() { ".mp3", ".wma" },
                new List<string>() { ".fla", ".swf" },
                new List<string>() { null, "", ".fakeextension", "png" }
            };
            List<string> nameSet = new()
            {
                "Ảnh", "Video", "Âm thanh", "Hoạt họa", String.Empty
            };
            for (int i = 0; i < extensionSet.Count; i++)
                for (int j = 0; j < extensionSet[i].Count; j++)
                    Assert.AreEqual(nameSet[i], LibMethod.GetMediaTypeDescByExtension(extensionSet[i][j]));
        }
    }
}
