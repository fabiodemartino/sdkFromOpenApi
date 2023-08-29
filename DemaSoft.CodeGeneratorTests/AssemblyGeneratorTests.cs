using DemaSoft.CodeGenerator.Languages.CSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DemaSoft.CodeGeneratorTests
{
    [TestClass]
    public class AssemblyGeneratorTests
    {
        [TestMethod]
        public void GenerateAndCompileAssemblyToMemoryStream_Success()
        {
            // Arrange
            var assemblyGenerator = new AssemblyGenerator();

            const string className = "TestClass";
            const string classCodeFilePath = "Assets\\GoodSampleCSharpCodeToUseToTest.txt"; // Path to the text file
            var classCode = File.ReadAllText(classCodeFilePath);
            
            // Act
            MemoryStream? resultStream = assemblyGenerator.GenerateAndCompileAssemblyToMemoryStream(className, classCode);

            // Assert
            Assert.IsNotNull(resultStream);
        }
        [TestMethod]
        public void GenerateAndCompileAssemblyToMemoryStream_Failure()
        {
            // Arrange
            var assemblyGenerator = new AssemblyGenerator();

            const string className = "TestClass";
            const string classCodeFilePath = "Assets\\BadSampleCSharpCodeToUseToTest.txt"; // Path to the text file
            var classCode = File.ReadAllText(classCodeFilePath);

            // Act
            MemoryStream? resultStream = assemblyGenerator.GenerateAndCompileAssemblyToMemoryStream(className, classCode);

            // Assert
            Assert.IsNull(resultStream);
        }
    }
}