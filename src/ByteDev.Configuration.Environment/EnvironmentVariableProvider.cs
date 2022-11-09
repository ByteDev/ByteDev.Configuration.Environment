using System;
using System.Globalization;

namespace ByteDev.Configuration.Environment
{
    /// <summary>
    /// Represents a provider to access environment variables.
    /// </summary>
    public class EnvironmentVariableProvider : IEnvironmentVariableProvider
    {
        private readonly EnvironmentVariableTarget _target;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ByteDev.Configuration.Environment.EnvironmentVariableProvider" /> class.
        /// Targets environment variables at the process level.
        /// </summary>
        public EnvironmentVariableProvider() : this(EnvironmentVariableTarget.Process)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ByteDev.Configuration.Environment.EnvironmentVariableProvider" /> class.
        /// </summary>
        /// <param name="target"></param>
        public EnvironmentVariableProvider(EnvironmentVariableTarget target)
        {
            _target = target;
        }

        /// <summary>
        /// Delete an environment variable. If the variable does not exist then no
        /// exception is thrown.
        /// </summary>
        /// <param name="name">Name of environment variable.</param>
        /// <exception cref="T:System.ArgumentException"><paramref name="name" /> was null or empty.</exception>
        public void Delete(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Name was null or empty.", nameof(name));

            Set(name, null);
        }

        /// <summary>
        /// Delete an environment variable. If the variable does not exist then an exception
        /// of type <see cref="T:ByteDev.Configuration.Environment.EnvironmentVariableNotExistException" /> is thrown.
        /// </summary>
        /// <param name="name">Name of environment variable.</param>
        /// <exception cref="T:System.ArgumentException"><paramref name="name" /> was null or empty.</exception>
        /// <exception cref="T:ByteDev.Configuration.Environment.EnvironmentVariableNotExistException">Environment variable does not exist.</exception>
        public void DeleteOrThrow(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Name was null or empty.", nameof(name));

            if (!Exists(name))
                throw new EnvironmentVariableNotExistException($"Environment variable: '{name}' does not exist.");

            Set(name, null);
        }

        /// <summary>
        /// Determines if an environment variable exists.
        /// </summary>
        /// <param name="name">Name of environment variable.</param>
        /// <returns>True if the environment variable exists; otherwise false.</returns>
        /// <exception cref="T:System.ArgumentException"><paramref name="name" /> was null or empty.</exception>
        public bool Exists(string name)
        {
            return GetStringOrDefault(name) != null;
        }

        /// <summary>
        /// Set an environment variable. If it does not exist it will be created otherwise
        /// it's value will be updated. If the value is null then the variable will be deleted.
        /// </summary>
        /// <param name="name">Name of environment variable.</param>
        /// <param name="value">Environment variable's new value.</param>
        /// <exception cref="T:System.ArgumentException"><paramref name="name" /> was null or empty.</exception>
        public void Set(string name, object value)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Name was null or empty.", nameof(name));

            System.Environment.SetEnvironmentVariable(name, value?.ToString(), _target);
        }

        /// <summary>
        /// Retrieve an environment variable as a String. If it does not exist then an
        /// exception will be thrown.
        /// </summary>
        /// <param name="name">Name of environment variable.</param>
        /// <returns>Environment variable's value.</returns>
        /// <exception cref="T:System.ArgumentException"><paramref name="name" /> was null or empty.</exception>
        /// <exception cref="T:ByteDev.Configuration.Environment.EnvironmentVariableNotExistException">Environment variable does not exist.</exception>
        public string GetString(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Name was null or empty.", nameof(name));

            var value = System.Environment.GetEnvironmentVariable(name, _target);

            if (value == null)
            {
                throw new EnvironmentVariableNotExistException($"Environment variable: '{name}' does not exist.");
            }

            return value;
        }

        /// <summary>
        /// Retrieve an environment variable as a String. If it does not exist then the
        /// <paramref name="defaultValue" /> will be returned.
        /// </summary>
        /// <param name="name">Name of environment variable.</param>
        /// <param name="defaultValue">Value to return if the environment variable does not exist.</param>
        /// <returns>Environment variable's value.</returns>
        /// <exception cref="T:System.ArgumentException"><paramref name="name" /> was null or empty.</exception>
        public string GetStringOrDefault(string name, string defaultValue = null)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Name was null or empty.", nameof(name));

            var value = System.Environment.GetEnvironmentVariable(name, _target);

            if (value == default)
            {
                return defaultValue;
            }

            return value;
        }

        /// <summary>
        /// Retrieve an environment variable as a Char. If it does not exist or it's value cannot be cast
        /// then an exception will be thrown.
        /// </summary>
        /// <param name="name">Name of environment variable.</param>
        /// <returns>Environment variable's value.</returns>
        /// <exception cref="T:System.ArgumentException"><paramref name="name" /> was null or empty.</exception>
        /// <exception cref="T:ByteDev.Configuration.Environment.EnvironmentVariableNotExistException">Environment variable does not exist.</exception>
        /// <exception cref="T:ByteDev.Configuration.Environment.UnexpectedEnvironmentVariableTypeException">Environment variable value is not a char.</exception>
        public char GetChar(string name)
        {
            var value = GetString(name);

            if (char.TryParse(value, out var result))
                return result;

            throw new UnexpectedEnvironmentVariableTypeException(name, value, typeof(char));
        }

        /// <summary>
        /// Retrieve an environment variable as a Char. If it does not exist or it's value cannot be cast 
        /// then the <paramref name="defaultValue" /> will be returned.
        /// </summary>
        /// <param name="name">Name of environment variable.</param>
        /// <param name="defaultValue">Value to return if the environment variable does not exist or cannot be cast.</param>
        /// <returns>Environment variable's value.</returns>
        /// <exception cref="T:System.ArgumentException"><paramref name="name" /> was null or empty.</exception>
        public char GetCharOrDefault(string name, char defaultValue = '\0')
        {
            var value = GetStringOrDefault(name);

            if (char.TryParse(value, out var result))
                return result;

            return defaultValue;
        }

        /// <summary>
        /// Retrieve an environment variable as a Boolean. If it does not exist or it's value cannot be cast
        /// then an exception will be thrown.
        /// </summary>
        /// <param name="name">Name of environment variable.</param>
        /// <returns>Environment variable's value.</returns>
        /// <exception cref="T:System.ArgumentException"><paramref name="name" /> was null or empty.</exception>
        /// <exception cref="T:ByteDev.Configuration.Environment.EnvironmentVariableNotExistException">Environment variable does not exist.</exception>
        /// <exception cref="T:ByteDev.Configuration.Environment.UnexpectedEnvironmentVariableTypeException">Environment variable value is not a bool.</exception>
        public bool GetBoolean(string name)
        {
            var value = GetString(name);

            if (bool.TryParse(value, out var result))
                return result;

            throw new UnexpectedEnvironmentVariableTypeException(name, value, typeof(bool));
        }

        /// <summary>
        /// Retrieve an environment variable as a Boolean. If it does not exist or it's value cannot be cast 
        /// then the <paramref name="defaultValue" /> will be returned.
        /// </summary>
        /// <param name="name">Name of environment variable.</param>
        /// <param name="defaultValue">Value to return if the environment variable does not exist or cannot be cast.</param>
        /// <returns>Environment variable's value.</returns>
        /// <exception cref="T:System.ArgumentException"><paramref name="name" /> was null or empty.</exception>
        public bool GetBooleanOrDefault(string name, bool defaultValue = false)
        {
            var value = GetStringOrDefault(name);

            if (bool.TryParse(value, out var result))
                return result;

            return defaultValue;
        }

        /// <summary>
        /// Retrieve an environment variable as a Byte. If it does not exist or it's value cannot be cast
        /// then an exception will be thrown.
        /// </summary>
        /// <param name="name">Name of environment variable.</param>
        /// <returns>Environment variable's value.</returns>
        /// <exception cref="T:System.ArgumentException"><paramref name="name" /> was null or empty.</exception>
        /// <exception cref="T:ByteDev.Configuration.Environment.EnvironmentVariableNotExistException">Environment variable does not exist.</exception>
        /// <exception cref="T:ByteDev.Configuration.Environment.UnexpectedEnvironmentVariableTypeException">Environment variable value is not a byte.</exception>
        public byte GetByte(string name)
        {
            var value = GetString(name);

            if (byte.TryParse(value, out var result))
                return result;

            throw new UnexpectedEnvironmentVariableTypeException(name, value, typeof(byte));
        }

        /// <summary>
        /// Retrieve an environment variable as a Byte. If it does not exist or it's value cannot be cast 
        /// then the <paramref name="defaultValue" /> will be returned.
        /// </summary>
        /// <param name="name">Name of environment variable.</param>
        /// <param name="defaultValue">Value to return if the environment variable does not exist or cannot be cast.</param>
        /// <returns>Environment variable's value.</returns>
        /// <exception cref="T:System.ArgumentException"><paramref name="name" /> was null or empty.</exception>
        public byte GetByteOrDefault(string name, byte defaultValue = 0)
        {
            var value = GetStringOrDefault(name);

            if (byte.TryParse(value, out var result))
                return result;

            return defaultValue;
        }

        /// <summary>
        /// Retrieve an environment variable as a Int16. If it does not exist or it's value cannot be cast
        /// then an exception will be thrown.
        /// </summary>
        /// <param name="name">Name of environment variable.</param>
        /// <returns>Environment variable's value.</returns>
        /// <exception cref="T:System.ArgumentException"><paramref name="name" /> was null or empty.</exception>
        /// <exception cref="T:ByteDev.Configuration.Environment.EnvironmentVariableNotExistException">Environment variable does not exist.</exception>
        /// <exception cref="T:ByteDev.Configuration.Environment.UnexpectedEnvironmentVariableTypeException">Environment variable value is not a short.</exception>
        public short GetInt16(string name)
        {
            var value = GetString(name);

            if (short.TryParse(value, out var result))
                return result;

            throw new UnexpectedEnvironmentVariableTypeException(name, value, typeof(short));
        }

        /// <summary>
        /// Retrieve an environment variable as a Int16. If it does not exist or it's value cannot be cast 
        /// then the <paramref name="defaultValue" /> will be returned.
        /// </summary>
        /// <param name="name">Name of environment variable.</param>
        /// <param name="defaultValue">Value to return if the environment variable does not exist or cannot be cast.</param>
        /// <returns>Environment variable's value.</returns>
        /// <exception cref="T:System.ArgumentException"><paramref name="name" /> was null or empty.</exception>
        public short GetInt16OrDefault(string name, short defaultValue = 0)
        {
            var value = GetStringOrDefault(name);

            if (short.TryParse(value, out var result))
                return result;

            return defaultValue;
        }

        /// <summary>
        /// Retrieve an environment variable as a Int32. If it does not exist or it's value cannot be cast
        /// then an exception will be thrown.
        /// </summary>
        /// <param name="name">Name of environment variable.</param>
        /// <returns>Environment variable's value.</returns>
        /// <exception cref="T:System.ArgumentException"><paramref name="name" /> was null or empty.</exception>
        /// <exception cref="T:ByteDev.Configuration.Environment.EnvironmentVariableNotExistException">Environment variable does not exist.</exception>
        /// <exception cref="T:ByteDev.Configuration.Environment.UnexpectedEnvironmentVariableTypeException">Environment variable value is not a int.</exception>
        public int GetInt32(string name)
        {
            var value = GetString(name);

            if (int.TryParse(value, out var result))
                return result;

            throw new UnexpectedEnvironmentVariableTypeException(name, value, typeof(int));
        }

        /// <summary>
        /// Retrieve an environment variable as a Int32. If it does not exist or it's value cannot be cast 
        /// then the <paramref name="defaultValue" /> will be returned.
        /// </summary>
        /// <param name="name">Name of environment variable.</param>
        /// <param name="defaultValue">Value to return if the environment variable does not exist or cannot be cast.</param>
        /// <returns>Environment variable's value.</returns>
        /// <exception cref="T:System.ArgumentException"><paramref name="name" /> was null or empty.</exception>
        public int GetInt32OrDefault(string name, int defaultValue = 0)
        {
            var value = GetStringOrDefault(name);

            if (int.TryParse(value, out var result))
                return result;

            return defaultValue;
        }

        /// <summary>
        /// Retrieve an environment variable as a Int64. If it does not exist or it's value cannot be cast
        /// then an exception will be thrown.
        /// </summary>
        /// <param name="name">Name of environment variable.</param>
        /// <returns>Environment variable's value.</returns>
        /// <exception cref="T:System.ArgumentException"><paramref name="name" /> was null or empty.</exception>
        /// <exception cref="T:ByteDev.Configuration.Environment.EnvironmentVariableNotExistException">Environment variable does not exist.</exception>
        /// <exception cref="T:ByteDev.Configuration.Environment.UnexpectedEnvironmentVariableTypeException">Environment variable value is not a long.</exception>
        public long GetInt64(string name)
        {
            var value = GetString(name);

            if (long.TryParse(value, out var result))
                return result;

            throw new UnexpectedEnvironmentVariableTypeException(name, value, typeof(long));
        }

        /// <summary>
        /// Retrieve an environment variable as a Int64. If it does not exist or it's value cannot be cast 
        /// then the <paramref name="defaultValue" /> will be returned.
        /// </summary>
        /// <param name="name">Name of environment variable.</param>
        /// <param name="defaultValue">Value to return if the environment variable does not exist or cannot be cast.</param>
        /// <returns>Environment variable's value.</returns>
        /// <exception cref="T:System.ArgumentException"><paramref name="name" /> was null or empty.</exception>
        public long GetInt64OrDefault(string name, long defaultValue = 0)
        {
            var value = GetStringOrDefault(name);

            if (long.TryParse(value, out var result))
                return result;

            return defaultValue;
        }

        /// <summary>
        /// Retrieve an environment variable as a Single. If it does not exist or it's value cannot be cast
        /// then an exception will be thrown.
        /// </summary>
        /// <param name="name">Name of environment variable.</param>
        /// <returns>Environment variable's value.</returns>
        /// <exception cref="T:System.ArgumentException"><paramref name="name" /> was null or empty.</exception>
        /// <exception cref="T:ByteDev.Configuration.Environment.EnvironmentVariableNotExistException">Environment variable does not exist.</exception>
        /// <exception cref="T:ByteDev.Configuration.Environment.UnexpectedEnvironmentVariableTypeException">Environment variable value is not a float.</exception>
        public float GetSingle(string name)
        {
            var value = GetString(name);

            if (float.TryParse(value, out var result))
                return result;

            throw new UnexpectedEnvironmentVariableTypeException(name, value, typeof(float));
        }

        /// <summary>
        /// Retrieve an environment variable as a Single. If it does not exist or it's value cannot be cast 
        /// then the <paramref name="defaultValue" /> will be returned.
        /// </summary>
        /// <param name="name">Name of environment variable.</param>
        /// <param name="defaultValue">Value to return if the environment variable does not exist or cannot be cast.</param>
        /// <returns>Environment variable's value.</returns>
        /// <exception cref="T:System.ArgumentException"><paramref name="name" /> was null or empty.</exception>
        public float GetSingleOrDefault(string name, float defaultValue = 0)
        {
            var value = GetStringOrDefault(name);

            if (float.TryParse(value, out var result))
                return result;

            return defaultValue;
        }

        /// <summary>
        /// Retrieve an environment variable as a Double. If it does not exist or it's value cannot be cast
        /// then an exception will be thrown.
        /// </summary>
        /// <param name="name">Name of environment variable.</param>
        /// <returns>Environment variable's value.</returns>
        /// <exception cref="T:System.ArgumentException"><paramref name="name" /> was null or empty.</exception>
        /// <exception cref="T:ByteDev.Configuration.Environment.EnvironmentVariableNotExistException">Environment variable does not exist.</exception>
        /// <exception cref="T:ByteDev.Configuration.Environment.UnexpectedEnvironmentVariableTypeException">Environment variable value is not a double.</exception>
        public double GetDouble(string name)
        {
            var value = GetString(name);
            
            if (double.TryParse(value, out var result))
                return result;

            throw new UnexpectedEnvironmentVariableTypeException(name, value, typeof(double));
        }

        /// <summary>
        /// Retrieve an environment variable as a Double. If it does not exist or it's value cannot be cast 
        /// then the <paramref name="defaultValue" /> will be returned.
        /// </summary>
        /// <param name="name">Name of environment variable.</param>
        /// <param name="defaultValue">Value to return if the environment variable does not exist or cannot be cast.</param>
        /// <returns>Environment variable's value.</returns>
        /// <exception cref="T:System.ArgumentException"><paramref name="name" /> was null or empty.</exception>
        public double GetDoubleOrDefault(string name, double defaultValue = 0)
        {
            var value = GetStringOrDefault(name);

            if (double.TryParse(value, out var result))
                return result;

            return defaultValue;
        }

        /// <summary>
        /// Retrieve an environment variable as a Decimal. If it does not exist or it's value cannot be cast
        /// then an exception will be thrown.
        /// </summary>
        /// <param name="name">Name of environment variable.</param>
        /// <returns>Environment variable's value.</returns>
        /// <exception cref="T:System.ArgumentException"><paramref name="name" /> was null or empty.</exception>
        /// <exception cref="T:ByteDev.Configuration.Environment.EnvironmentVariableNotExistException">Environment variable does not exist.</exception>
        /// <exception cref="T:ByteDev.Configuration.Environment.UnexpectedEnvironmentVariableTypeException">Environment variable value is not a decimal.</exception>
        public decimal GetDecimal(string name)
        {
            var value = GetString(name);
            
            if (decimal.TryParse(value, out var result))
                return result;

            throw new UnexpectedEnvironmentVariableTypeException(name, value, typeof(decimal));
        }

        /// <summary>
        /// Retrieve an environment variable as a Decimal. If it does not exist or it's value cannot be cast 
        /// then the <paramref name="defaultValue" /> will be returned.
        /// </summary>
        /// <param name="name">Name of environment variable.</param>
        /// <param name="defaultValue">Value to return if the environment variable does not exist or cannot be cast.</param>
        /// <returns>Environment variable's value.</returns>
        /// <exception cref="T:System.ArgumentException"><paramref name="name" /> was null or empty.</exception>
        public decimal GetDecimalOrDefault(string name, decimal defaultValue = 0)
        {
            var value = GetStringOrDefault(name);

            if (decimal.TryParse(value, out var result))
                return result;

            return defaultValue;
        }

        /// <summary>
        /// Retrieve an environment variable as a Uri. If it does not exist or it's value cannot be cast
        /// then an exception will be thrown.
        /// </summary>
        /// <param name="name">Name of environment variable.</param>
        /// <returns>Environment variable's value.</returns>
        /// <exception cref="T:System.ArgumentException"><paramref name="name" /> was null or empty.</exception>
        /// <exception cref="T:ByteDev.Configuration.Environment.EnvironmentVariableNotExistException">Environment variable does not exist.</exception>
        /// <exception cref="T:ByteDev.Configuration.Environment.UnexpectedEnvironmentVariableTypeException">Environment variable value is not a Uri.</exception>
        public Uri GetUri(string name)
        {
            var value = GetString(name);

            try
            {
                return new Uri(value);
            }
            catch (UriFormatException ex)
            {
                throw new UnexpectedEnvironmentVariableTypeException(name, value, typeof(Uri), ex);
            }
        }

        /// <summary>
        /// Retrieve an environment variable as a Guid. If it does not exist or it's value cannot be cast
        /// then an exception will be thrown.
        /// </summary>
        /// <param name="name">Name of environment variable.</param>
        /// <returns>Environment variable's value.</returns>
        /// <exception cref="T:System.ArgumentException"><paramref name="name" /> was null or empty.</exception>
        /// <exception cref="T:ByteDev.Configuration.Environment.EnvironmentVariableNotExistException">Environment variable does not exist.</exception>
        /// <exception cref="T:ByteDev.Configuration.Environment.UnexpectedEnvironmentVariableTypeException">Environment variable value is not a Guid.</exception>
        public Guid GetGuid(string name)
        {
            var value = GetString(name);

            try
            {
                return new Guid(value);
            }
            catch (FormatException ex)
            {
                throw new UnexpectedEnvironmentVariableTypeException(name, value, typeof(Guid), ex);
            }
        }

        /// <summary>
        /// Retrieve an environment variable as a Enum. The environment variable value can be stored as the
        /// string (name) enum representation or the defined number value.
        /// </summary>
        /// <typeparam name="TEnum">Type of enum.</typeparam>
        /// <param name="name">Name of environment variable.</param>
        /// <returns>Environment variable's value.</returns>
        /// <exception cref="T:System.ArgumentException"><paramref name="name" /> was null or empty.</exception>
        /// <exception cref="T:ByteDev.Configuration.Environment.EnvironmentVariableNotExistException">Environment variable does not exist.</exception>
        /// <exception cref="T:ByteDev.Configuration.Environment.UnexpectedEnvironmentVariableTypeException">Environment variable value is not a defined name or number value of the enum.</exception>
        public TEnum GetEnum<TEnum>(string name) where TEnum : struct, Enum
        {
            var value = GetString(name);

            if (Enum.TryParse<TEnum>(value, out var result))
            {
                if (Enum.IsDefined(typeof(TEnum), result))
                {
                    return result;
                }
            }

            throw new UnexpectedEnvironmentVariableTypeException(name, value, typeof(TEnum));
        }

        /// <summary>
        /// Retrieve an environment variable as a Enum. The environment variable value can be stored as the
        /// string (name) enum representation or the defined number value. If it does not exist or it's value cannot be cast 
        /// then the <paramref name="defaultValue" /> will be returned.
        /// </summary>
        /// <typeparam name="TEnum">Type of enum.</typeparam>
        /// <param name="name">Name of environment variable.</param>
        /// <param name="defaultValue">Value to return if the environment variable does not exist or cannot be cast.</param>
        /// <returns>Environment variable's value.</returns>
        /// <exception cref="T:System.ArgumentException"><paramref name="name" /> was null or empty.</exception>
        public TEnum GetEnumOrDefault<TEnum>(string name, TEnum defaultValue) where TEnum : struct, Enum
        {
            if (!Exists(name))
                return defaultValue;
            
            var value = GetString(name);

            if (Enum.TryParse<TEnum>(value, out var result))
            {
                if (Enum.IsDefined(typeof(TEnum), result))
                {
                    return result;
                }
            }

            return defaultValue;
        }

        /// <summary>
        /// Retrieve an environment variable as a DateTime.
        /// </summary>
        /// <param name="name">Name of environment variable.</param>
        /// <param name="format">Format of the string.</param>
        /// <returns>Environment variable's value.</returns>
        /// <exception cref="T:System.ArgumentException"><paramref name="name" /> was null or empty.</exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="format" /> is null.</exception>
        /// <exception cref="T:ByteDev.Configuration.Environment.EnvironmentVariableNotExistException">Environment variable does not exist.</exception>
        /// <exception cref="T:ByteDev.Configuration.Environment.UnexpectedEnvironmentVariableTypeException">Environment variable value is not a defined name or number value of the enum.</exception>
        public DateTime GetDateTime(string name, string format)
        {
            if (format == null)
                throw new ArgumentNullException(nameof(format));

            var value = GetString(name);

            try
            {
                return DateTime.ParseExact(value, format, null, DateTimeStyles.None);
            }
            catch (FormatException ex)
            {
                throw new UnexpectedEnvironmentVariableTypeException(name, value, typeof(DateTime), ex);
            }
        }
    }
}