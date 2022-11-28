using System;
using NUnit.Framework;

namespace ByteDev.Configuration.Environment.UnitTests
{
    [TestFixture]
    public class EnvironmentVariableProviderTests
    {
        private IEnvironmentVariableProvider _sut;

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
        public class DeleteOrThrow : EnvironmentVariableProviderTests
        {
            [TestCase(null)]
            [TestCase("")]
            public void WhenNameIsNullOrEmpty_ThenThrowException(string name)
            {
                Assert.Throws<ArgumentException>(() => _sut.DeleteOrThrow(name));    
            }

            [Test]
            public void WhenVarExists_ThenDelete()
            {
                var name = GetName();

                _sut.Set(name, "TestValue");

                _sut.DeleteOrThrow(name);
                
                Assert.That(_sut.Exists(name), Is.False);
            }

            [Test]
            public void WhenVarDoesNotExist_ThenThrowException()
            {
                var name = GetName();

                var ex = Assert.Throws<EnvironmentVariableNotExistException>(() => _sut.DeleteOrThrow(name));
                Assert.That(ex.Message, Is.EqualTo($"Environment variable: '{name}' does not exist."));
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
        public class GetChar : EnvironmentVariableProviderTests
        {
            [TestCase(null)]
            [TestCase("")]
            public void WhenNameIsNullOrEmpty_ThenThrowException(string name)
            {
                Assert.Throws<ArgumentException>(() => _sut.GetChar(name));    
            }

            [Test]
            public void WhenVarDoesNotExist_ThenThrowException()
            {
                var name = GetName();

                Assert.Throws<EnvironmentVariableNotExistException>(() => _sut.GetChar(name));
            }

            [Test]
            public void WhenVarExists_AndIsNotChar_ThenThrowException()
            {
                var name = GetName();

                _sut.Set(name, "NotChar");

                Assert.Throws<UnexpectedEnvironmentVariableTypeException>(() => _sut.GetChar(name));
            }

            [Test]
            public void WhenVarExists_AndIsChar_ThenReturnValue()
            {
                var name = GetName();

                _sut.Set(name, 'Z');

                var result = _sut.GetChar(name);
                
                Assert.That(result, Is.EqualTo('Z'));
            }
        }

        [TestFixture]
        public class GetCharOrDefault : EnvironmentVariableProviderTests
        {
            [TestCase(null)]
            [TestCase("")]
            public void WhenNameIsNullOrEmpty_ThenThrowException(string name)
            {
                Assert.Throws<ArgumentException>(() => _sut.GetCharOrDefault(name));    
            }

            [Test]
            public void WhenVarDoesNotExist_ThenReturnDefault()
            {
                var name = GetName();

                var result = _sut.GetCharOrDefault(name, 'A');

                Assert.That(result, Is.EqualTo('A'));
            }

            [Test]
            public void WhenVarExists_AndIsNotChar_ThenReturnDefault()
            {
                var name = GetName();

                _sut.Set(name, true);

                var result = _sut.GetCharOrDefault(name, 'A');

                Assert.That(result, Is.EqualTo('A'));
            }

            [Test]
            public void WhenVarExists_AndIsChar_ThenReturnValue()
            {
                var name = GetName();

                _sut.Set(name, 'Z');

                var result = _sut.GetCharOrDefault(name);
                
                Assert.That(result, Is.EqualTo('Z'));
            }
        }

        [TestFixture]
        public class GetBoolean : EnvironmentVariableProviderTests
        {
            [TestCase(null)]
            [TestCase("")]
            public void WhenNameIsNullOrEmpty_ThenThrowException(string name)
            {
                Assert.Throws<ArgumentException>(() => _sut.GetBoolean(name));    
            }

            [Test]
            public void WhenVarDoesNotExist_ThenThrowException()
            {
                var name = GetName();

                Assert.Throws<EnvironmentVariableNotExistException>(() => _sut.GetBoolean(name));
            }

            [Test]
            public void WhenVarExists_AndIsNotBool_ThenThrowException()
            {
                var name = GetName();

                _sut.Set(name, "NotBool");

                Assert.Throws<UnexpectedEnvironmentVariableTypeException>(() => _sut.GetBoolean(name));
            }

            [Test]
            public void WhenVarExists_AndIsBool_ThenReturnValue()
            {
                var name = GetName();

                _sut.Set(name, true);

                var result = _sut.GetBoolean(name);
                
                Assert.That(result, Is.True);
            }
        }

        [TestFixture]
        public class GetBooleanOrDefault : EnvironmentVariableProviderTests
        {
            [TestCase(null)]
            [TestCase("")]
            public void WhenNameIsNullOrEmpty_ThenThrowException(string name)
            {
                Assert.Throws<ArgumentException>(() => _sut.GetBooleanOrDefault(name));    
            }

            [Test]
            public void WhenVarDoesNotExist_ThenReturnDefault()
            {
                var name = GetName();

                var result = _sut.GetBooleanOrDefault(name, true);

                Assert.That(result, Is.True);
            }

            [Test]
            public void WhenVarExists_AndIsNotBool_ThenReturnDefault()
            {
                var name = GetName();

                _sut.Set(name, "NotBool");

                var result = _sut.GetBooleanOrDefault(name, true);

                Assert.That(result, Is.True);
            }

            [TestCase("true")]
            [TestCase("True")]
            [TestCase("TRUE")]
            public void WhenVarExists_AndIsBool_ThenReturnValue(string value)
            {
                var name = GetName();

                _sut.Set(name, value);

                var result = _sut.GetBooleanOrDefault(name);
                
                Assert.That(result, Is.True);
            }
        }

        [TestFixture]
        public class GetByte : EnvironmentVariableProviderTests
        {
            [TestCase(null)]
            [TestCase("")]
            public void WhenNameIsNullOrEmpty_ThenThrowException(string name)
            {
                Assert.Throws<ArgumentException>(() => _sut.GetByte(name));    
            }

            [Test]
            public void WhenVarDoesNotExist_ThenThrowException()
            {
                var name = GetName();

                Assert.Throws<EnvironmentVariableNotExistException>(() => _sut.GetByte(name));
            }

            [Test]
            public void WhenVarExists_AndIsNotByte_ThenThrowException()
            {
                var name = GetName();

                _sut.Set(name, "NotByte");

                Assert.Throws<UnexpectedEnvironmentVariableTypeException>(() => _sut.GetByte(name));
            }

            [Test]
            public void WhenVarExists_AndIsByte_ThenReturnValue()
            {
                var name = GetName();

                _sut.Set(name, 10);

                var result = _sut.GetByte(name);
                
                Assert.That(result, Is.EqualTo(10));
            }
        }

        [TestFixture]
        public class GetByteOrDefault : EnvironmentVariableProviderTests
        {
            [TestCase(null)]
            [TestCase("")]
            public void WhenNameIsNullOrEmpty_ThenThrowException(string name)
            {
                Assert.Throws<ArgumentException>(() => _sut.GetByteOrDefault(name));    
            }

            [Test]
            public void WhenVarDoesNotExist_ThenReturnDefault()
            {
                var name = GetName();

                var result = _sut.GetByteOrDefault(name, 5);

                Assert.That(result, Is.EqualTo(5));
            }

            [Test]
            public void WhenVarExists_AndIsNotByte_ThenReturnDefault()
            {
                var name = GetName();

                _sut.Set(name, "NotByte");

                var result = _sut.GetByteOrDefault(name, 5);

                Assert.That(result, Is.EqualTo(5));
            }

            [Test]
            public void WhenVarExists_AndIsOutOfBounds_ThenReturnDefault()
            {
                var name = GetName();

                _sut.Set(name, "256");

                var result = _sut.GetByteOrDefault(name, 5);

                Assert.That(result, Is.EqualTo(5));
            }

            [TestCase("0", 0)]
            [TestCase("255", 255)]
            public void WhenVarExists_AndIsByte_ThenReturnValue(string value, byte expected)
            {
                var name = GetName();

                _sut.Set(name, value);

                var result = _sut.GetByteOrDefault(name);
                
                Assert.That(result, Is.EqualTo(expected));
            }
        }

        [TestFixture]
        public class GetInt16 : EnvironmentVariableProviderTests
        {
            [TestCase(null)]
            [TestCase("")]
            public void WhenNameIsNullOrEmpty_ThenThrowException(string name)
            {
                Assert.Throws<ArgumentException>(() => _sut.GetInt16(name));    
            }

            [Test]
            public void WhenVarDoesNotExist_ThenThrowException()
            {
                var name = GetName();

                Assert.Throws<EnvironmentVariableNotExistException>(() => _sut.GetInt16(name));
            }

            [Test]
            public void WhenVarExists_AndIsNotShort_ThenThrowException()
            {
                var name = GetName();

                _sut.Set(name, "NotShort");

                Assert.Throws<UnexpectedEnvironmentVariableTypeException>(() => _sut.GetInt16(name));
            }

            [Test]
            public void WhenVarExists_AndIsShort_ThenReturnValue()
            {
                var name = GetName();

                _sut.Set(name, 10);

                var result = _sut.GetInt16(name);
                
                Assert.That(result, Is.EqualTo(10));
            }
        }

        [TestFixture]
        public class GetInt16OrDefault : EnvironmentVariableProviderTests
        {
            [TestCase(null)]
            [TestCase("")]
            public void WhenNameIsNullOrEmpty_ThenThrowException(string name)
            {
                Assert.Throws<ArgumentException>(() => _sut.GetInt16OrDefault(name));    
            }

            [Test]
            public void WhenVarDoesNotExist_ThenReturnDefault()
            {
                var name = GetName();

                var result = _sut.GetInt16OrDefault(name, 5);

                Assert.That(result, Is.EqualTo(5));
            }

            [Test]
            public void WhenVarExists_AndIsNotShort_ThenReturnDefault()
            {
                var name = GetName();

                _sut.Set(name, "NotShort");

                var result = _sut.GetInt16OrDefault(name, 5);

                Assert.That(result, Is.EqualTo(5));
            }

            [Test]
            public void WhenVarExists_AndIsOutOfBounds_ThenReturnDefault()
            {
                var name = GetName();

                _sut.Set(name, "32768");

                var result = _sut.GetInt16OrDefault(name, 5);

                Assert.That(result, Is.EqualTo(5));
            }

            [TestCase("-32768", -32768)]
            [TestCase("-1", -1)]
            [TestCase("0", 0)]
            [TestCase("1", 1)]
            [TestCase("32767", 32767)]
            public void WhenVarExists_AndIsShort_ThenReturnValue(string value, short expected)
            {
                var name = GetName();

                _sut.Set(name, value);

                var result = _sut.GetInt16OrDefault(name);
                
                Assert.That(result, Is.EqualTo(expected));
            }
        }

        [TestFixture]
        public class GetInt32 : EnvironmentVariableProviderTests
        {
            [TestCase(null)]
            [TestCase("")]
            public void WhenNameIsNullOrEmpty_ThenThrowException(string name)
            {
                Assert.Throws<ArgumentException>(() => _sut.GetInt32(name));    
            }

            [Test]
            public void WhenVarDoesNotExist_ThenThrowException()
            {
                var name = GetName();

                Assert.Throws<EnvironmentVariableNotExistException>(() => _sut.GetInt32(name));
            }

            [Test]
            public void WhenVarExists_AndIsNotInt_ThenThrowException()
            {
                var name = GetName();

                _sut.Set(name, "NotInt");

                Assert.Throws<UnexpectedEnvironmentVariableTypeException>(() => _sut.GetInt32(name));
            }

            [Test]
            public void WhenVarExists_AndIsInt_ThenReturnValue()
            {
                var name = GetName();

                _sut.Set(name, 10);

                var result = _sut.GetInt32(name);
                
                Assert.That(result, Is.EqualTo(10));
            }
        }

        [TestFixture]
        public class GetInt32OrDefault : EnvironmentVariableProviderTests
        {
            [TestCase(null)]
            [TestCase("")]
            public void WhenNameIsNullOrEmpty_ThenThrowException(string name)
            {
                Assert.Throws<ArgumentException>(() => _sut.GetInt32OrDefault(name));    
            }

            [Test]
            public void WhenVarDoesNotExist_ThenReturnDefault()
            {
                var name = GetName();

                var result = _sut.GetInt32OrDefault(name, 5);

                Assert.That(result, Is.EqualTo(5));
            }

            [Test]
            public void WhenVarExists_AndIsNotInt_ThenReturnDefault()
            {
                var name = GetName();

                _sut.Set(name, "NotInt");

                var result = _sut.GetInt32OrDefault(name, 5);

                Assert.That(result, Is.EqualTo(5));
            }

            [Test]
            public void WhenVarExists_AndIsOutOfBounds_ThenReturnDefault()
            {
                var name = GetName();

                _sut.Set(name, "2147483648");  // int.Max == 2147483647

                var result = _sut.GetInt32OrDefault(name, 5);

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

                var result = _sut.GetInt32OrDefault(name);
                
                Assert.That(result, Is.EqualTo(expected));
            }
        }

        [TestFixture]
        public class GetInt64 : EnvironmentVariableProviderTests
        {
            [TestCase(null)]
            [TestCase("")]
            public void WhenNameIsNullOrEmpty_ThenThrowException(string name)
            {
                Assert.Throws<ArgumentException>(() => _sut.GetInt64(name));    
            }

            [Test]
            public void WhenVarDoesNotExist_ThenThrowException()
            {
                var name = GetName();

                Assert.Throws<EnvironmentVariableNotExistException>(() => _sut.GetInt64(name));
            }

            [Test]
            public void WhenVarExists_AndIsNotLong_ThenThrowException()
            {
                var name = GetName();

                _sut.Set(name, "NotLong");

                Assert.Throws<UnexpectedEnvironmentVariableTypeException>(() => _sut.GetInt64(name));
            }

            [Test]
            public void WhenVarExists_AndIsLong_ThenReturnValue()
            {
                var name = GetName();

                _sut.Set(name, 10);

                var result = _sut.GetInt64(name);
                
                Assert.That(result, Is.EqualTo(10));
            }
        }

        [TestFixture]
        public class GetInt64OrDefault : EnvironmentVariableProviderTests
        {
            [TestCase(null)]
            [TestCase("")]
            public void WhenNameIsNullOrEmpty_ThenThrowException(string name)
            {
                Assert.Throws<ArgumentException>(() => _sut.GetInt64OrDefault(name));    
            }

            [Test]
            public void WhenVarDoesNotExist_ThenReturnDefault()
            {
                var name = GetName();

                var result = _sut.GetInt64OrDefault(name, 5);

                Assert.That(result, Is.EqualTo(5));
            }

            [Test]
            public void WhenVarExists_AndIsNotLong_ThenReturnDefault()
            {
                var name = GetName();

                _sut.Set(name, "NotLong");

                var result = _sut.GetInt64OrDefault(name, 5);

                Assert.That(result, Is.EqualTo(5));
            }

            [Test]
            public void WhenVarExists_AndIsOutOfBounds_ThenReturnDefault()
            {
                var name = GetName();

                _sut.Set(name, "9223372036854775808");  // long.Max == 9223372036854775807

                var result = _sut.GetInt64OrDefault(name, 5);

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

                var result = _sut.GetInt64OrDefault(name);
                
                Assert.That(result, Is.EqualTo(expected));
            }
        }

        [TestFixture]
        public class GetSingle : EnvironmentVariableProviderTests
        {
            [TestCase(null)]
            [TestCase("")]
            public void WhenNameIsNullOrEmpty_ThenThrowException(string name)
            {
                Assert.Throws<ArgumentException>(() => _sut.GetSingle(name));    
            }

            [Test]
            public void WhenVarDoesNotExist_ThenThrowException()
            {
                var name = GetName();

                Assert.Throws<EnvironmentVariableNotExistException>(() => _sut.GetSingle(name));
            }

            [Test]
            public void WhenVarExists_AndIsNotFloat_ThenThrowException()
            {
                var name = GetName();

                _sut.Set(name, "NoteFloat");

                Assert.Throws<UnexpectedEnvironmentVariableTypeException>(() => _sut.GetSingle(name));
            }

            [Test]
            public void WhenVarExists_AndIsFloat_ThenReturnValue()
            {
                var name = GetName();

                _sut.Set(name, 10.1f);

                var result = _sut.GetSingle(name);
                
                Assert.That(result, Is.EqualTo(10.1f));
            }
        }

        [TestFixture]
        public class GetSingleOrDefault : EnvironmentVariableProviderTests
        {
            [TestCase(null)]
            [TestCase("")]
            public void WhenNameIsNullOrEmpty_ThenThrowException(string name)
            {
                Assert.Throws<ArgumentException>(() => _sut.GetSingleOrDefault(name));    
            }

            [Test]
            public void WhenVarDoesNotExist_ThenReturnDefault()
            {
                var name = GetName();

                var result = _sut.GetSingleOrDefault(name, 5);

                Assert.That(result, Is.EqualTo(5));
            }

            [Test]
            public void WhenVarExists_AndIsNotFloat_ThenReturnDefault()
            {
                var name = GetName();

                _sut.Set(name, "NotFloat");

                var result = _sut.GetSingleOrDefault(name, 5.1f);

                Assert.That(result, Is.EqualTo(5.1f));
            }

            [TestCase("-1.20", -1.2f)]
            [TestCase("0", 0)]
            [TestCase("1.2", 1.2f)]
            public void WhenVarExists_AndIsDouble_ThenReturnValue(string value, float expected)
            {
                var name = GetName();

                _sut.Set(name, value);

                var result = _sut.GetSingleOrDefault(name);
                
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

            [TestCase("{38fafb60-a70e-4140-a186-acf4a5f1dea8}")]
            [TestCase("{38FAFB60-A70E-4140-A186-ACF4A5F1DEA8}")]
            [TestCase("38fafb60-a70e-4140-a186-acf4a5f1dea8")]
            [TestCase("38fafb60a70e4140a186acf4a5f1dea8")]
            [TestCase("38FAFB60-A70E-4140-A186-ACF4A5F1DEA8")]
            [TestCase("38FAFB60A70E4140A186ACF4A5F1DEA8")]
            public void WhenVarExists_AndIsUri_ThenReturnValue(string value)
            {
                var name = GetName();

                _sut.Set(name, value);

                var result = _sut.GetGuid(name);
                
                Assert.That(result, Is.EqualTo(new Guid("38fafb60-a70e-4140-a186-acf4a5f1dea8")));
            }
        }

        [TestFixture]
        public class GetEnum : EnvironmentVariableProviderTests
        {
            [TestCase(null)]
            [TestCase("")]
            public void WhenNameIsNullOrEmpty_ThenThrowException(string name)
            {
                Assert.Throws<ArgumentException>(() => _sut.GetEnum<DummyColor>(name));
            }

            [Test]
            public void WhenVarDoesNotExist_ThenThrowException()
            {
                var name = GetName();

                Assert.Throws<EnvironmentVariableNotExistException>(() => _sut.GetEnum<DummyColor>(name));
            }

            [TestCase("Reddish")]
            [TestCase("red")]
            [TestCase("RED")]
            [TestCase(-1)]
            [TestCase(0)]
            [TestCase(3)]
            [TestCase("3")]
            public void WhenVarExists_AndIsNotEnum_ThenThrowException(object value)
            {
                var name = GetName();

                _sut.Set(name, value);

                Assert.Throws<UnexpectedEnvironmentVariableTypeException>(() => _sut.GetEnum<DummyColor>(name));
            }

            [Test]
            public void WhenVarExists_AndIsEnumString_ThenReturnValue()
            {
                var name = GetName();

                _sut.Set(name, "Blue");

                var result = _sut.GetEnum<DummyColor>(name);

                Assert.That(result, Is.EqualTo(DummyColor.Blue));
            }

            [TestCase(1, DummyColor.Red)]
            [TestCase(2, DummyColor.Blue)]
            [TestCase("1", DummyColor.Red)]
            [TestCase("2", DummyColor.Blue)]
            public void WhenVarExists_AndIsEnumNumber_ThenReturnValue(object value, DummyColor expected)
            {
                var name = GetName();

                _sut.Set(name, value);

                var result = _sut.GetEnum<DummyColor>(name);

                Assert.That(result, Is.EqualTo(expected));
            }
        }

        [TestFixture]
        public class GetEnumOrDefault : EnvironmentVariableProviderTests
        {
            [TestCase(null)]
            [TestCase("")]
            public void WhenNameIsNullOrEmpty_ThenThrowException(string name)
            {
                Assert.Throws<ArgumentException>(() => _sut.GetEnumOrDefault(name, DummyColor.Red));
            }

            [Test]
            public void WhenVarDoesNotExist_ThenReturnDefault()
            {
                var name = GetName();

                var result = _sut.GetEnumOrDefault(name, DummyColor.Red);

                Assert.That(result, Is.EqualTo(DummyColor.Red));
            }

            [TestCase("Reddish")]
            [TestCase("red")]
            [TestCase("RED")]
            [TestCase(-1)]
            [TestCase(0)]
            [TestCase(3)]
            [TestCase("3")]
            public void WhenVarExists_AndIsNotEnum_ThenReturnDefault(object value)
            {
                var name = GetName();

                _sut.Set(name, value);

                var result = _sut.GetEnumOrDefault(name, DummyColor.Blue);

                Assert.That(result, Is.EqualTo(DummyColor.Blue));
            }

            [Test]
            public void WhenVarExists_AndIsEnumString_ThenReturnValue()
            {
                var name = GetName();

                _sut.Set(name, "Blue");

                var result = _sut.GetEnumOrDefault(name, DummyColor.Red);

                Assert.That(result, Is.EqualTo(DummyColor.Blue));
            }

            [TestCase(1, DummyColor.Red)]
            [TestCase(2, DummyColor.Blue)]
            [TestCase("1", DummyColor.Red)]
            [TestCase("2", DummyColor.Blue)]
            public void WhenVarExists_AndIsEnumNumber_ThenReturnValue(object value, DummyColor expected)
            {
                var name = GetName();

                _sut.Set(name, value);

                var result = _sut.GetEnumOrDefault(name, DummyColor.Red);

                Assert.That(result, Is.EqualTo(expected));
            }
        }

        [TestFixture]
        public class GetDateTime : EnvironmentVariableProviderTests
        {
            private const string FormatDate = "yyyyMMdd";

            [TestCase(null)]
            [TestCase("")]
            public void WhenNameIsNullOrEmpty_ThenThrowException(string name)
            {
                Assert.Throws<ArgumentException>(() => _sut.GetDateTime(name, FormatDate));    
            }

            [Test]
            public void WhenFormatIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentNullException>(() => _sut.GetDateTime("name", null));
            }
            
            [Test]
            public void WhenVarDoesNotExist_ThenThrowException()
            {
                var name = GetName();

                Assert.Throws<EnvironmentVariableNotExistException>(() => _sut.GetDateTime(name, FormatDate));
            }

            [TestCase("NotDateTime")]
            [TestCase("2022011A")]
            [TestCase("20220132")]
            [TestCase("2022013")]
            public void WhenVarExists_AndIsNotInCorrectFormat_ThenThrowException(string format)
            {
                var name = GetName();

                _sut.Set(name, format);

                Assert.Throws<UnexpectedEnvironmentVariableTypeException>(() => _sut.GetDateTime(name, FormatDate));
            }

            [Test]
            public void WhenVarExists_AndIsDateTime_ThenReturnValue()
            {
                var name = GetName();

                _sut.Set(name, "20220110");

                var result = _sut.GetDateTime(name, FormatDate);
                
                Assert.That(result, Is.EqualTo(new DateTime(2022, 1, 10)));
            }
        }

        [TestFixture]
        public class GetDateTimeOrDefault : EnvironmentVariableProviderTests
        {
            private const string FormatDate = "yyyyMMdd";

            private readonly DateTime DefaultDateTime = DateTime.MinValue;

            [TestCase(null)]
            [TestCase("")]
            public void WhenNameIsNullOrEmpty_ThenThrowException(string name)
            {
                Assert.Throws<ArgumentException>(() => _sut.GetDateTimeOrDefault(name, FormatDate, DefaultDateTime));    
            }

            [Test]
            public void WhenFormatIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentNullException>(() => _sut.GetDateTimeOrDefault("name", null, DefaultDateTime));
            }

            [Test]
            public void WhenVarDoesNotExist_ThenReturnDefault()
            {
                var name = GetName();

                var result = _sut.GetDateTimeOrDefault(name, FormatDate, DefaultDateTime);

                Assert.That(result, Is.EqualTo(DefaultDateTime));
            }

            [TestCase("NotDateTime")]
            [TestCase("2022011A")]
            [TestCase("20220132")]
            [TestCase("2022013")]
            public void WhenVarExists_AndIsNotInCorrectFormat_ThenReturnDefault(string format)
            {
                var name = GetName();

                _sut.Set(name, format);

                var result = _sut.GetDateTimeOrDefault(name, FormatDate, DefaultDateTime);

                Assert.That(result, Is.EqualTo(DefaultDateTime));
            }

            [Test]
            public void WhenVarExists_AndIsDateTime_ThenReturnValue()
            {
                var name = GetName();

                _sut.Set(name, "20220110");

                var result = _sut.GetDateTimeOrDefault(name, FormatDate, DefaultDateTime);
                
                Assert.That(result, Is.EqualTo(new DateTime(2022, 1, 10)));
            }
        }

        [TestFixture]
        public class GetTimeSpan : EnvironmentVariableProviderTests
        {
            private const string FormatTimeSpan = @"hh\:mm\:ss";
            private const string FormatTimeSpanMs = @"hh\:mm\:ss\.fff";

            [TestCase(null)]
            [TestCase("")]
            public void WhenNameIsNullOrEmpty_ThenThrowException(string name)
            {
                Assert.Throws<ArgumentException>(() => _sut.GetTimeSpan(name, FormatTimeSpan));    
            }

            [Test]
            public void WhenFormatIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentNullException>(() => _sut.GetTimeSpan("name", null));
            }

            [Test]
            public void WhenVarDoesNotExist_ThenThrowException()
            {
                var name = GetName();

                Assert.Throws<EnvironmentVariableNotExistException>(() => _sut.GetTimeSpan(name, FormatTimeSpan));
            }

            [TestCase("NotTimeSpan")]
            [TestCase("00:00:01.1")]
            [TestCase("00:00:01A")]
            public void WhenVarExists_AndIsNotInCorrectFormat_ThenThrowException(string format)
            {
                var name = GetName();

                _sut.Set(name, format);

                Assert.Throws<UnexpectedEnvironmentVariableTypeException>(() => _sut.GetTimeSpan(name, FormatTimeSpan));
            }

            [Test]
            public void WhenVarExists_AndIsCorrectFormat_ThenReturnValue()
            {
                var name = GetName();

                _sut.Set(name, "09:14:48");

                var result = _sut.GetTimeSpan(name, FormatTimeSpan);
                
                Assert.That(result, Is.EqualTo(new TimeSpan(9, 14, 48)));
            }

            [Test]
            public void WhenVarExists_AndIsCorrectFormatWithMs_ThenReturnValue()
            {
                var name = GetName();

                _sut.Set(name, "09:14:48.500");

                var result = _sut.GetTimeSpan(name, FormatTimeSpanMs);
                
                Assert.That(result, Is.EqualTo(new TimeSpan(0, 9, 14, 48, 500)));
            }
        }

        [TestFixture]
        public class GetTimeSpanOrDefault : EnvironmentVariableProviderTests
        {
            private const string FormatTimeSpan = @"hh\:mm\:ss";

            private readonly TimeSpan DefaultTimeSpan = TimeSpan.Zero;

            [TestCase(null)]
            [TestCase("")]
            public void WhenNameIsNullOrEmpty_ThenThrowException(string name)
            {
                Assert.Throws<ArgumentException>(() => _sut.GetTimeSpanOrDefault(name, FormatTimeSpan, DefaultTimeSpan));    
            }

            [Test]
            public void WhenFormatIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentNullException>(() => _sut.GetTimeSpanOrDefault("name", null, DefaultTimeSpan));
            }

            [Test]
            public void WhenVarDoesNotExist_ThenReturnDefault()
            {
                var name = GetName();

                var result = _sut.GetTimeSpanOrDefault(name, FormatTimeSpan, DefaultTimeSpan);

                Assert.That(result, Is.EqualTo(DefaultTimeSpan));
            }

            [TestCase("NotTimeSpan")]
            [TestCase("00:00:01.1")]
            [TestCase("00:00:01A")]
            public void WhenVarExists_AndIsNotInCorrectFormat_ThenReturnDefault(string format)
            {
                var name = GetName();

                _sut.Set(name, format);

                var result = _sut.GetTimeSpanOrDefault(name, FormatTimeSpan, DefaultTimeSpan);

                Assert.That(result, Is.EqualTo(DefaultTimeSpan));
            }

            [Test]
            public void WhenVarExists_AndIsTimeSpan_ThenReturnValue()
            {
                var name = GetName();

                _sut.Set(name, "09:14:48");

                var result = _sut.GetTimeSpanOrDefault(name, FormatTimeSpan, DefaultTimeSpan);
                
                Assert.That(result, Is.EqualTo(new TimeSpan(9, 14, 48)));
            }
        }

        private static string GetName()
        {
            return "IntTest" + Guid.NewGuid().ToString().Replace("-", string.Empty);
        }
    }
}