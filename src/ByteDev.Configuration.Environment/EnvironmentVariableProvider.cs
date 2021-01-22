using System;

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
        /// Delete an environment variable. If it does not exist then no
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
        /// it's value will be updated.
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
        /// Retrieve an environment variable as a string. If it does not exist then an
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
        /// Retrieve an environment variable as a string. If it does not exist then the
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
        /// Retrieve an environment variable as a bool. If it does not exist or it's value cannot be cast
        /// then an exception will be thrown.
        /// </summary>
        /// <param name="name">Name of environment variable.</param>
        /// <returns>Environment variable's value.</returns>
        /// <exception cref="T:System.ArgumentException"><paramref name="name" /> was null or empty.</exception>
        /// <exception cref="T:ByteDev.Configuration.Environment.EnvironmentVariableNotExistException">Environment variable does not exist.</exception>
        /// <exception cref="T:ByteDev.Configuration.Environment.UnexpectedEnvironmentVariableTypeException">Environment variable value is not a bool.</exception>
        public bool GetBool(string name)
        {
            var value = GetString(name);

            if (bool.TryParse(value, out var result))
                return result;

            throw new UnexpectedEnvironmentVariableTypeException(name, value, typeof(bool));
        }

        /// <summary>
        /// Retrieve an environment variable as a bool. If it does not exist or it's value cannot be cast 
        /// then the <paramref name="defaultValue" /> will be returned.
        /// </summary>
        /// <param name="name">Name of environment variable.</param>
        /// <param name="defaultValue">Value to return if the environment variable does not exist.</param>
        /// <returns>Environment variable's value.</returns>
        /// <exception cref="T:System.ArgumentException"><paramref name="name" /> was null or empty.</exception>
        public bool GetBoolOrDefault(string name, bool defaultValue = false)
        {
            var value = GetStringOrDefault(name);

            if (bool.TryParse(value, out var result))
                return result;

            return defaultValue;
        }

        /// <summary>
        /// Retrieve an environment variable as a byte. If it does not exist or it's value cannot be cast
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
        /// Retrieve an environment variable as a byte. If it does not exist or it's value cannot be cast 
        /// then the <paramref name="defaultValue" /> will be returned.
        /// </summary>
        /// <param name="name">Name of environment variable.</param>
        /// <param name="defaultValue">Value to return if the environment variable does not exist.</param>
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
        /// Retrieve an environment variable as a short. If it does not exist or it's value cannot be cast
        /// then an exception will be thrown.
        /// </summary>
        /// <param name="name">Name of environment variable.</param>
        /// <returns>Environment variable's value.</returns>
        /// <exception cref="T:System.ArgumentException"><paramref name="name" /> was null or empty.</exception>
        /// <exception cref="T:ByteDev.Configuration.Environment.EnvironmentVariableNotExistException">Environment variable does not exist.</exception>
        /// <exception cref="T:ByteDev.Configuration.Environment.UnexpectedEnvironmentVariableTypeException">Environment variable value is not a short.</exception>
        public short GetShort(string name)
        {
            var value = GetString(name);

            if (short.TryParse(value, out var result))
                return result;

            throw new UnexpectedEnvironmentVariableTypeException(name, value, typeof(short));
        }

        /// <summary>
        /// Retrieve an environment variable as a short. If it does not exist or it's value cannot be cast 
        /// then the <paramref name="defaultValue" /> will be returned.
        /// </summary>
        /// <param name="name">Name of environment variable.</param>
        /// <param name="defaultValue">Value to return if the environment variable does not exist.</param>
        /// <returns>Environment variable's value.</returns>
        /// <exception cref="T:System.ArgumentException"><paramref name="name" /> was null or empty.</exception>
        public short GetShortOrDefault(string name, short defaultValue = 0)
        {
            var value = GetStringOrDefault(name);

            if (short.TryParse(value, out var result))
                return result;

            return defaultValue;
        }

        /// <summary>
        /// Retrieve an environment variable as a int. If it does not exist or it's value cannot be cast
        /// then an exception will be thrown.
        /// </summary>
        /// <param name="name">Name of environment variable.</param>
        /// <returns>Environment variable's value.</returns>
        /// <exception cref="T:System.ArgumentException"><paramref name="name" /> was null or empty.</exception>
        /// <exception cref="T:ByteDev.Configuration.Environment.EnvironmentVariableNotExistException">Environment variable does not exist.</exception>
        /// <exception cref="T:ByteDev.Configuration.Environment.UnexpectedEnvironmentVariableTypeException">Environment variable value is not a int.</exception>
        public int GetInt(string name)
        {
            var value = GetString(name);

            if (int.TryParse(value, out var result))
                return result;

            throw new UnexpectedEnvironmentVariableTypeException(name, value, typeof(int));
        }

        /// <summary>
        /// Retrieve an environment variable as a int. If it does not exist or it's value cannot be cast 
        /// then the <paramref name="defaultValue" /> will be returned.
        /// </summary>
        /// <param name="name">Name of environment variable.</param>
        /// <param name="defaultValue">Value to return if the environment variable does not exist.</param>
        /// <returns>Environment variable's value.</returns>
        /// <exception cref="T:System.ArgumentException"><paramref name="name" /> was null or empty.</exception>
        public int GetIntOrDefault(string name, int defaultValue = 0)
        {
            var value = GetStringOrDefault(name);

            if (int.TryParse(value, out var result))
                return result;

            return defaultValue;
        }

        /// <summary>
        /// Retrieve an environment variable as a long. If it does not exist or it's value cannot be cast
        /// then an exception will be thrown.
        /// </summary>
        /// <param name="name">Name of environment variable.</param>
        /// <returns>Environment variable's value.</returns>
        /// <exception cref="T:System.ArgumentException"><paramref name="name" /> was null or empty.</exception>
        /// <exception cref="T:ByteDev.Configuration.Environment.EnvironmentVariableNotExistException">Environment variable does not exist.</exception>
        /// <exception cref="T:ByteDev.Configuration.Environment.UnexpectedEnvironmentVariableTypeException">Environment variable value is not a long.</exception>
        public long GetLong(string name)
        {
            var value = GetString(name);

            if (long.TryParse(value, out var result))
                return result;

            throw new UnexpectedEnvironmentVariableTypeException(name, value, typeof(long));
        }

        /// <summary>
        /// Retrieve an environment variable as a long. If it does not exist or it's value cannot be cast 
        /// then the <paramref name="defaultValue" /> will be returned.
        /// </summary>
        /// <param name="name">Name of environment variable.</param>
        /// <param name="defaultValue">Value to return if the environment variable does not exist.</param>
        /// <returns>Environment variable's value.</returns>
        /// <exception cref="T:System.ArgumentException"><paramref name="name" /> was null or empty.</exception>
        public long GetLongOrDefault(string name, long defaultValue = 0)
        {
            var value = GetStringOrDefault(name);

            if (long.TryParse(value, out var result))
                return result;

            return defaultValue;
        }

        /// <summary>
        /// Retrieve an environment variable as a float. If it does not exist or it's value cannot be cast
        /// then an exception will be thrown.
        /// </summary>
        /// <param name="name">Name of environment variable.</param>
        /// <returns>Environment variable's value.</returns>
        /// <exception cref="T:System.ArgumentException"><paramref name="name" /> was null or empty.</exception>
        /// <exception cref="T:ByteDev.Configuration.Environment.EnvironmentVariableNotExistException">Environment variable does not exist.</exception>
        /// <exception cref="T:ByteDev.Configuration.Environment.UnexpectedEnvironmentVariableTypeException">Environment variable value is not a float.</exception>
        public float GetFloat(string name)
        {
            var value = GetString(name);

            if (float.TryParse(value, out var result))
                return result;

            throw new UnexpectedEnvironmentVariableTypeException(name, value, typeof(float));
        }

        /// <summary>
        /// Retrieve an environment variable as a float. If it does not exist or it's value cannot be cast 
        /// then the <paramref name="defaultValue" /> will be returned.
        /// </summary>
        /// <param name="name">Name of environment variable.</param>
        /// <param name="defaultValue">Value to return if the environment variable does not exist.</param>
        /// <returns>Environment variable's value.</returns>
        /// <exception cref="T:System.ArgumentException"><paramref name="name" /> was null or empty.</exception>
        public float GetFloatOrDefault(string name, float defaultValue = 0)
        {
            var value = GetStringOrDefault(name);

            if (float.TryParse(value, out var result))
                return result;

            return defaultValue;
        }

        /// <summary>
        /// Retrieve an environment variable as a double. If it does not exist or it's value cannot be cast
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
        /// Retrieve an environment variable as a double. If it does not exist or it's value cannot be cast 
        /// then the <paramref name="defaultValue" /> will be returned.
        /// </summary>
        /// <param name="name">Name of environment variable.</param>
        /// <param name="defaultValue">Value to return if the environment variable does not exist.</param>
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
        /// Retrieve an environment variable as a decimal. If it does not exist or it's value cannot be cast
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
        /// Retrieve an environment variable as a decimal. If it does not exist or it's value cannot be cast 
        /// then the <paramref name="defaultValue" /> will be returned.
        /// </summary>
        /// <param name="name">Name of environment variable.</param>
        /// <param name="defaultValue">Value to return if the environment variable does not exist.</param>
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
        /// Retrieve an environment variable as a enum. The environment variable value can be stored as the
        /// string (name) enum representation or the defined number value.
        /// </summary>
        /// <typeparam name="TEnum">Tyoe of enum.</typeparam>
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
    }
}