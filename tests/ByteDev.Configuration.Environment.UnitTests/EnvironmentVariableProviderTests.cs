using System;
using NUnit.Framework;

namespace ByteDev.Configuration.Environment.UnitTests
{
    [TestFixture]
    public class EnvironmentVariableProviderTests
    {
        private EnvironmentVariableProvider _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new EnvironmentVariableProvider(EnvironmentVariableTarget.Process);
        }

        [TestFixture]
        public class Delete : EnvironmentVariableProviderTests
        {
            [TestCase(null)]
            [TestCase("")]
            public void WhenNameIsNullOrEmpty_ThenThrowException(string name)
            {
                Assert.Throws<ArgumentException>(() => _sut.Delete(name));    
            }

            [Test]
            public void WhenVarExists_ThenDelete()
            {
                var name = GetName();

                _sut.Set(name, "TestValue");

                _sut.Delete(name);

                Assert.That(_sut.Exists(name), Is.False);
            }

            [Test]
            public void WhenVarDoesNotExist_ThenDoNothing()
            {
                var name = GetName();

                _sut.Delete(name);

                Assert.That(_sut.Exists(name), Is.False);
            }
        }

        [TestFixture]
        public class Exists : EnvironmentVariableProviderTests
        {
            [TestCase(null)]
            [TestCase("")]
            public void WhenNameIsNullOrEmpty_ThenThrowException(string name)
            {
                Assert.Throws<ArgumentException>(() => _sut.Exists(name));    
            }

            [Test]
            public void WhenVarExists_ThenReturnTrue()
            {
                var name = GetName();

                _sut.Set(name, "TestValue");

                var result = _sut.Exists(name);

                Assert.That(result, Is.True);
            }

            [Test]
            public void WhenVarDoesNotExist_ThenReturnFalse()
            {
                var name = GetName();

                var result = _sut.Exists(name);

                Assert.That(result, Is.False);
            }
        }

        [TestFixture]
        public class Set : EnvironmentVariableProviderTests
        {
            [TestCase(null)]
            [TestCase("")]
            public void WhenNameIsNullOrEmpty_ThenThrowException(string name)
            {
                Assert.Throws<ArgumentException>(() => _sut.Set(name, "SomeValue"));    
            }

            [Test]
            public void WhenVarDoesNotExist_ThenCreate()
            {
                var name = GetName();

                _sut.Set(name, "TestValue");

                var value = _sut.GetString(name);

                Assert.That(value, Is.EqualTo("TestValue"));
            }

            [Test]
            public void WhenVarExists_ThenUpdate()
            {
                var name = GetName();

                _sut.Set(name, "TestValue");
                _sut.Set(name, "TestValue2");

                var value = _sut.GetString(name);

                Assert.That(value, Is.EqualTo("TestValue2"));
            }
        }

        [TestFixture]
        public class GetString : EnvironmentVariableProviderTests
        {
            [TestCase(null)]
            [TestCase("")]
            public void WhenNameIsNullOrEmpty_ThenThrowException(string name)
            {
                Assert.Throws<ArgumentException>(() => _sut.GetString(name));    
            }

            [Test]
            public void WhenVarDoesNotExist_ThenThrowException()
            {
                var name = GetName();

                Assert.Throws<EnvironmentVariableNotExistException>(() => _sut.GetString(name));
            }

            [Test]
            public void WhenVarExists_ThenReturnValue()
            {
                var name = GetName();

                _sut.Set(name, "TestValue");

                var result = _sut.GetString(name);
                
                Assert.That(result, Is.EqualTo("TestValue"));
            }
        }

        [TestFixture]
        public class GetStringOrDefault : EnvironmentVariableProviderTests
        {
            [TestCase(null)]
            [TestCase("")]
            public void WhenNameIsNullOrEmpty_ThenThrowException(string name)
            {
                Assert.Throws<ArgumentException>(() => _sut.GetStringOrDefault(name));    
            }

            [Test]
            public void WhenVarDoesNotExist_ThenReturnDefault()
            {
                var name = GetName();

                var result = _sut.GetStringOrDefault(name, "myDefault");

                Assert.That(result, Is.EqualTo("myDefault"));
            }

            [Test]
            public void WhenVarExists_ThenReturnValue()
            {
                var name = GetName();

                _sut.Set(name, "TestValue");

                var result = _sut.GetStringOrDefault(name);
                
                Assert.That(result, Is.EqualTo("TestValue"));

                _sut.Delete(name);  // cleanup
            }
        }

        [TestFixture]
        public class GetBool : EnvironmentVariableProviderTests
        {
            [TestCase(null)]
            [TestCase("")]
            public void WhenNameIsNullOrEmpty_ThenThrowException(string name)
            {
                Assert.Throws<ArgumentException>(() => _sut.GetBool(name));    
            }

            [Test]
            public void WhenVarDoesNotExist_ThenThrowException()
            {
                var name = GetName();

                Assert.Throws<EnvironmentVariableNotExistException>(() => _sut.GetBool(name));
            }

            [Test]
            public void WhenVarExists_AndNotBool_ThenThrowException()
            {
                var name = GetName();

                _sut.Set(name, "NotBool");

                Assert.Throws<UnexpectedEnvironmentVariableTypeException>(() => _sut.GetBool(name));
            }

            [Test]
            public void WhenVarExists_AndBool_ThenReturnValue()
            {
                var name = GetName();

                _sut.Set(name, true);

                var result = _sut.GetBool(name);
                
                Assert.That(result, Is.True);
            }
        }

        [TestFixture]
        public class GetBoolOrDefault : EnvironmentVariableProviderTests
        {
            [TestCase(null)]
            [TestCase("")]
            public void WhenNameIsNullOrEmpty_ThenThrowException(string name)
            {
                Assert.Throws<ArgumentException>(() => _sut.GetBoolOrDefault(name));    
            }

            [Test]
            public void WhenVarDoesNotExist_ThenReturnDefault()
            {
                var name = GetName();

                var result = _sut.GetBoolOrDefault(name, true);

                Assert.That(result, Is.True);
            }

            [TestCase("true")]
            [TestCase("True")]
            [TestCase("TRUE")]
            public void WhenVarExists_ThenReturnValue(string value)
            {
                var name = GetName();

                _sut.Set(name, value);

                var result = _sut.GetBoolOrDefault(name);
                
                Assert.That(result, Is.True);
            }
        }

        private static string GetName()
        {
            return "IntTest" + Guid.NewGuid().ToString().Replace("-", string.Empty);
        }
    }
}