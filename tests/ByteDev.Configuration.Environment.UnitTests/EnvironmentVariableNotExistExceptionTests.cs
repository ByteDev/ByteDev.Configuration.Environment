using System;
using NUnit.Framework;

namespace ByteDev.Configuration.Environment.UnitTests
{
    [TestFixture]
    public class EnvironmentVariableNotExistExceptionTests
    {
        [Test]
        public void WhenNoArgs_ThenSetMessageToDefault()
        {
            var sut = new EnvironmentVariableNotExistException();

            Assert.That(sut.Message, Is.EqualTo("Environment variable does not exist."));
        }

        [Test]
        public void WhenMessageSpecified_ThenSetMessage()
        {
            var sut = new EnvironmentVariableNotExistException("Some message.");

            Assert.That(sut.Message, Is.EqualTo("Some message."));
        }

        [Test]
        public void WhenMessageAndInnerExSpecified_ThenSetMessageAndInnerEx()
        {
            var innerException = new Exception();

            var sut = new EnvironmentVariableNotExistException("Some message.", innerException);

            Assert.That(sut.Message, Is.EqualTo("Some message."));
            Assert.That(sut.InnerException, Is.SameAs(innerException));
        }
    }
}