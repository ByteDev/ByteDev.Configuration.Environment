using System;
using NUnit.Framework;

namespace ByteDev.Configuration.Environment.UnitTests
{
    [TestFixture]
    public class UnexpectedEnvironmentVariableTypeExceptionTests
    {
        [Test]
        public void WhenNoArgs_ThenSetMessageToDefault()
        {
            var sut = new UnexpectedEnvironmentVariableTypeException();

            Assert.That(sut.Message, Is.EqualTo("Unexpected environment variable type."));
        }

        [Test]
        public void WhenMessageSpecified_ThenSetMessage()
        {
            var sut = new UnexpectedEnvironmentVariableTypeException("Some message.");

            Assert.That(sut.Message, Is.EqualTo("Some message."));
        }

        [Test]
        public void WhenMessageAndInnerExSpecified_ThenSetMessageAndInnerEx()
        {
            var innerException = new Exception();

            var sut = new UnexpectedEnvironmentVariableTypeException("Some message.", innerException);

            Assert.That(sut.Message, Is.EqualTo("Some message."));
            Assert.That(sut.InnerException, Is.SameAs(innerException));
        }

        [Test]
        public void WhenExpectedTypeSpecified_ThenSetMessage()
        {
            var sut = new UnexpectedEnvironmentVariableTypeException("MyName", "MyValue", typeof(int));

            Assert.That(sut.Message, Is.EqualTo("Environment variable: 'MyName' value: 'MyValue' is not of expected type: Int32."));
        }
    }
}