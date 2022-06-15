using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using NUnit.Framework;

namespace ByteDev.Configuration.Environment.UnitTests
{
    [TestFixture]
    public class UnexpectedEnvironmentVariableTypeExceptionTests
    {
        private const string ExMessage = "some message";

        [Test]
        public void WhenNoArgs_ThenSetMessageToDefault()
        {
            var sut = new UnexpectedEnvironmentVariableTypeException();

            Assert.That(sut.Message, Is.EqualTo("Unexpected environment variable type."));
        }

        [Test]
        public void WhenMessageSpecified_ThenSetMessage()
        {
            var sut = new UnexpectedEnvironmentVariableTypeException(ExMessage);

            Assert.That(sut.Message, Is.EqualTo(ExMessage));
        }

        [Test]
        public void WhenMessageAndInnerExSpecified_ThenSetMessageAndInnerEx()
        {
            var innerException = new Exception();

            var sut = new UnexpectedEnvironmentVariableTypeException(ExMessage, innerException);

            Assert.That(sut.Message, Is.EqualTo(ExMessage));
            Assert.That(sut.InnerException, Is.SameAs(innerException));
        }

        [Test]
        public void WhenExpectedTypeSpecified_ThenSetMessage()
        {
            var sut = new UnexpectedEnvironmentVariableTypeException("MyName", "MyValue", typeof(int));

            Assert.That(sut.Message, Is.EqualTo("Environment variable: 'MyName' value: 'MyValue' is not of expected type: Int32."));
        }

        [Test]
        public void WhenSerialized_ThenDeserializeCorrectly()
        {
            var sut = new UnexpectedEnvironmentVariableTypeException(ExMessage);

            var formatter = new BinaryFormatter();
            
            using (var stream = new MemoryStream())
            {
                formatter.Serialize(stream, sut);

                stream.Seek(0, 0);

                var result = (UnexpectedEnvironmentVariableTypeException)formatter.Deserialize(stream);

                Assert.That(result.ToString(), Is.EqualTo(sut.ToString()));
            }
        }
    }
}