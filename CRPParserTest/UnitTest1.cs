using System;
using System.IO;
using CRPTools;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CRPParserTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {   
            var cwd = Directory.GetCurrentDirectory();
            var path = Path.Combine(cwd, "..", "..", "testfiles", "Colossal Mills.crp");
            var info = CrpDeserializer.parseFile( path );
            Assert.AreEqual( info.metadata["name"], "Colossal Mills" );
        }
    }
}
