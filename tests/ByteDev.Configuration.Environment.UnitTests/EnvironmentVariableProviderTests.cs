﻿using System;
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

            [Test]
            public void WhenVarExists_AndValueNull_ThenDelete()
            {
                var name = GetName();

                _sut.Set(name, "TestValue");
                _sut.Set(name, null);

                Assert.That(_sut.Exists(name), Is.False);
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
            public void WhenVarExists_AndIsNotBool_ThenThrowException()
            {
                var name = GetName();

                _sut.Set(name, "NotBool");

                Assert.Throws<UnexpectedEnvironmentVariableTypeException>(() => _sut.GetBool(name));
            }

            [Test]
            public void WhenVarExists_AndIsBool_ThenReturnValue()
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

            [Test]
            public void WhenVarExists_AndIsNotBool_ThenReturnDefault()
            {
                var name = GetName();

                _sut.Set(name, "NotBool");

                var result = _sut.GetBoolOrDefault(name, true);

                Assert.That(result, Is.True);
            }

            [TestCase("true")]
            [TestCase("True")]
            [TestCase("TRUE")]
            public void WhenVarExists_AndIsBool_ThenReturnValue(string value)
            {
                var name = GetName();

                _sut.Set(name, value);

                var result = _sut.GetBoolOrDefault(name);
                
                Assert.That(result, Is.True);
            }
        }

        [TestFixture]
        public class GetInt : EnvironmentVariableProviderTests
        {
            [TestCase(null)]
            [TestCase("")]
            public void WhenNameIsNullOrEmpty_ThenThrowException(string name)
            {
                Assert.Throws<ArgumentException>(() => _sut.GetInt(name));    
            }

            [Test]
            public void WhenVarDoesNotExist_ThenThrowException()
            {
                var name = GetName();

                Assert.Throws<EnvironmentVariableNotExistException>(() => _sut.GetInt(name));
            }

            [Test]
            public void WhenVarExists_AndIsNotInt_ThenThrowException()
            {
                var name = GetName();

                _sut.Set(name, "NotInt");

                Assert.Throws<UnexpectedEnvironmentVariableTypeException>(() => _sut.GetInt(name));
            }

            [Test]
            public void WhenVarExists_AndIsInt_ThenReturnValue()
            {
                var name = GetName();

                _sut.Set(name, 10);

                var result = _sut.GetInt(name);
                
                Assert.That(result, Is.EqualTo(10));
            }
        }

        [TestFixture]
        public class GetIntOrDefault : EnvironmentVariableProviderTests
        {
            [TestCase(null)]
            [TestCase("")]
            public void WhenNameIsNullOrEmpty_ThenThrowException(string name)
            {
                Assert.Throws<ArgumentException>(() => _sut.GetIntOrDefault(name));    
            }

            [Test]
            public void WhenVarDoesNotExist_ThenReturnDefault()
            {
                var name = GetName();

                var result = _sut.GetIntOrDefault(name, 5);

                Assert.That(result, Is.EqualTo(5));
            }

            [Test]
            public void WhenVarExists_AndIsNotInt_ThenReturnDefault()
            {
                var name = GetName();

                _sut.Set(name, "NotInt");

                var result = _sut.GetIntOrDefault(name, 5);

                Assert.That(result, Is.EqualTo(5));
            }

            [Test]
            public void WhenVarExists_AndIsOutOfBounds_ThenReturnDefault()
            {
                var name = GetName();

                _sut.Set(name, "2147483648");  // int.Max == 2147483647

                var result = _sut.GetIntOrDefault(name, 5);

                Assert.That(result, Is.EqualTo(5));
            }

            [TestCase("-2147483648", -2147483648)]
            [TestCase("-1", -1)]
            [TestCase("0", 0)]
            [TestCase("1", 1)]
            [TestCase("2147483647", 2147483647)]
            public void WhenVarExists_AndIsInt_ThenReturnValue(string value, int expected)
            {
                var name = GetName();

                _sut.Set(name, value);

                var result = _sut.GetIntOrDefault(name);
                
                Assert.That(result, Is.EqualTo(expected));
            }
        }

        [TestFixture]
        public class GetLong : EnvironmentVariableProviderTests
        {
            [TestCase(null)]
            [TestCase("")]
            public void WhenNameIsNullOrEmpty_ThenThrowException(string name)
            {
                Assert.Throws<ArgumentException>(() => _sut.GetLong(name));    
            }

            [Test]
            public void WhenVarDoesNotExist_ThenThrowException()
            {
                var name = GetName();

                Assert.Throws<EnvironmentVariableNotExistException>(() => _sut.GetLong(name));
            }

            [Test]
            public void WhenVarExists_AndIsNotLong_ThenThrowException()
            {
                var name = GetName();

                _sut.Set(name, "NotLong");

                Assert.Throws<UnexpectedEnvironmentVariableTypeException>(() => _sut.GetLong(name));
            }

            [Test]
            public void WhenVarExists_AndIsLong_ThenReturnValue()
            {
                var name = GetName();

                _sut.Set(name, 10);

                var result = _sut.GetLong(name);
                
                Assert.That(result, Is.EqualTo(10));
            }
        }

        [TestFixture]
        public class GetLongOrDefault : EnvironmentVariableProviderTests
        {
            [TestCase(null)]
            [TestCase("")]
            public void WhenNameIsNullOrEmpty_ThenThrowException(string name)
            {
                Assert.Throws<ArgumentException>(() => _sut.GetLongOrDefault(name));    
            }

            [Test]
            public void WhenVarDoesNotExist_ThenReturnDefault()
            {
                var name = GetName();

                var result = _sut.GetLongOrDefault(name, 5);

                Assert.That(result, Is.EqualTo(5));
            }

            [Test]
            public void WhenVarExists_AndIsNotLong_ThenReturnDefault()
            {
                var name = GetName();

                _sut.Set(name, "NotLong");

                var result = _sut.GetLongOrDefault(name, 5);

                Assert.That(result, Is.EqualTo(5));
            }

            [Test]
            public void WhenVarExists_AndIsOutOfBounds_ThenReturnDefault()
            {
                var name = GetName();

                _sut.Set(name, "9223372036854775808");  // long.Max == 9223372036854775807

                var result = _sut.GetLongOrDefault(name, 5);

                Assert.That(result, Is.EqualTo(5));
            }

            [TestCase("-9223372036854775808", -9223372036854775808)]
            [TestCase("-1", -1)]
            [TestCase("0", 0)]
            [TestCase("1", 1)]
            [TestCase("9223372036854775807", 9223372036854775807)]
            public void WhenVarExists_AndIsLong_ThenReturnValue(string value, long expected)
            {
                var name = GetName();

                _sut.Set(name, value);

                var result = _sut.GetLongOrDefault(name);
                
                Assert.That(result, Is.EqualTo(expected));
            }
        }

        [TestFixture]
        public class GetDouble : EnvironmentVariableProviderTests
        {
            [TestCase(null)]
            [TestCase("")]
            public void WhenNameIsNullOrEmpty_ThenThrowException(string name)
            {
                Assert.Throws<ArgumentException>(() => _sut.GetDouble(name));    
            }

            [Test]
            public void WhenVarDoesNotExist_ThenThrowException()
            {
                var name = GetName();

                Assert.Throws<EnvironmentVariableNotExistException>(() => _sut.GetDouble(name));
            }

            [Test]
            public void WhenVarExists_AndIsNotDouble_ThenThrowException()
            {
                var name = GetName();

                _sut.Set(name, "NotDouble");

                Assert.Throws<UnexpectedEnvironmentVariableTypeException>(() => _sut.GetDouble(name));
            }

            [Test]
            public void WhenVarExists_AndIsDouble_ThenReturnValue()
            {
                var name = GetName();

                _sut.Set(name, 10.1);

                var result = _sut.GetDouble(name);
                
                Assert.That(result, Is.EqualTo(10.1));
            }
        }

        [TestFixture]
        public class GetDoubleOrDefault : EnvironmentVariableProviderTests
        {
            [TestCase(null)]
            [TestCase("")]
            public void WhenNameIsNullOrEmpty_ThenThrowException(string name)
            {
                Assert.Throws<ArgumentException>(() => _sut.GetDoubleOrDefault(name));    
            }

            [Test]
            public void WhenVarDoesNotExist_ThenReturnDefault()
            {
                var name = GetName();

                var result = _sut.GetDoubleOrDefault(name, 5);

                Assert.That(result, Is.EqualTo(5));
            }

            [Test]
            public void WhenVarExists_AndIsNotDouble_ThenReturnDefault()
            {
                var name = GetName();

                _sut.Set(name, "NotDouble");

                var result = _sut.GetDoubleOrDefault(name, 5.1);

                Assert.That(result, Is.EqualTo(5.1));
            }

            [TestCase("-1.20", -1.2)]
            [TestCase("0", 0)]
            [TestCase("1.2", 1.2)]
            public void WhenVarExists_AndIsDouble_ThenReturnValue(string value, double expected)
            {
                var name = GetName();

                _sut.Set(name, value);

                var result = _sut.GetDoubleOrDefault(name);
                
                Assert.That(result, Is.EqualTo(expected));
            }
        }

        [TestFixture]
        public class GetDecimal : EnvironmentVariableProviderTests
        {
            [TestCase(null)]
            [TestCase("")]
            public void WhenNameIsNullOrEmpty_ThenThrowException(string name)
            {
                Assert.Throws<ArgumentException>(() => _sut.GetDecimal(name));    
            }

            [Test]
            public void WhenVarDoesNotExist_ThenThrowException()
            {
                var name = GetName();

                Assert.Throws<EnvironmentVariableNotExistException>(() => _sut.GetDecimal(name));
            }

            [Test]
            public void WhenVarExists_AndIsNotDecimal_ThenThrowException()
            {
                var name = GetName();

                _sut.Set(name, "NotDecimal");

                Assert.Throws<UnexpectedEnvironmentVariableTypeException>(() => _sut.GetDecimal(name));
            }

            [Test]
            public void WhenVarExists_AndIsDecimal_ThenReturnValue()
            {
                var name = GetName();

                _sut.Set(name, decimal.MaxValue);

                var result = _sut.GetDecimal(name);
                
                Assert.That(result, Is.EqualTo(decimal.MaxValue));
            }
        }

        [TestFixture]
        public class GetDecimalOrDefault : EnvironmentVariableProviderTests
        {
            [TestCase(null)]
            [TestCase("")]
            public void WhenNameIsNullOrEmpty_ThenThrowException(string name)
            {
                Assert.Throws<ArgumentException>(() => _sut.GetDecimalOrDefault(name));    
            }

            [Test]
            public void WhenVarDoesNotExist_ThenReturnDefault()
            {
                var name = GetName();

                var result = _sut.GetDecimalOrDefault(name, decimal.MinValue);

                Assert.That(result, Is.EqualTo(decimal.MinValue));
            }

            [Test]
            public void WhenVarExists_AndIsNotDecimal_ThenReturnDefault()
            {
                var name = GetName();

                _sut.Set(name, "NotDecimal");

                var result = _sut.GetDecimalOrDefault(name, decimal.MinValue);

                Assert.That(result, Is.EqualTo(decimal.MinValue));
            }

            [TestCase("-1.20", -1.2)]
            [TestCase("0", 0)]
            [TestCase("1.23456", 1.23456)]
            public void WhenVarExists_AndIsDecimal_ThenReturnValue(string value, decimal expected)
            {
                var name = GetName();

                _sut.Set(name, value);

                var result = _sut.GetDecimalOrDefault(name);
                
                Assert.That(result, Is.EqualTo(expected));
            }
        }

        [TestFixture]
        public class GetUri : EnvironmentVariableProviderTests
        {
            [TestCase(null)]
            [TestCase("")]
            public void WhenNameIsNullOrEmpty_ThenThrowException(string name)
            {
                Assert.Throws<ArgumentException>(() => _sut.GetUri(name));    
            }

            [Test]
            public void WhenVarDoesNotExist_ThenThrowException()
            {
                var name = GetName();

                Assert.Throws<EnvironmentVariableNotExistException>(() => _sut.GetUri(name));
            }

            [Test]
            public void WhenVarExists_AndIsNotUri_ThenThrowException()
            {
                var name = GetName();

                _sut.Set(name, "NotUri");

                Assert.Throws<UnexpectedEnvironmentVariableTypeException>(() => _sut.GetUri(name));
            }

            [Test]
            public void WhenVarExists_AndIsUri_ThenReturnValue()
            {
                var name = GetName();

                _sut.Set(name, "http://www.google.com/");

                var result = _sut.GetUri(name);
                
                Assert.That(result.AbsoluteUri, Is.EqualTo("http://www.google.com/"));
            }
        }

        [TestFixture]
        public class GetGuid : EnvironmentVariableProviderTests
        {
            [TestCase(null)]
            [TestCase("")]
            public void WhenNameIsNullOrEmpty_ThenThrowException(string name)
            {
                Assert.Throws<ArgumentException>(() => _sut.GetGuid(name));    
            }

            [Test]
            public void WhenVarDoesNotExist_ThenThrowException()
            {
                var name = GetName();

                Assert.Throws<EnvironmentVariableNotExistException>(() => _sut.GetGuid(name));
            }

            [Test]
            public void WhenVarExists_AndIsNotUri_ThenThrowException()
            {
                var name = GetName();

                _sut.Set(name, "NotGuid");

                Assert.Throws<UnexpectedEnvironmentVariableTypeException>(() => _sut.GetGuid(name));
            }

            [Test]
            public void WhenVarExists_AndIsUri_ThenReturnValue()
            {
                var name = GetName();

                _sut.Set(name, "38fafb60-a70e-4140-a186-acf4a5f1dea8");

                var result = _sut.GetGuid(name);
                
                Assert.That(result, Is.EqualTo(new Guid("38fafb60-a70e-4140-a186-acf4a5f1dea8")));
            }
        }

        private static string GetName()
        {
            return "IntTest" + Guid.NewGuid().ToString().Replace("-", string.Empty);
        }
    }
}