using System;

namespace ByteDev.Configuration.Environment
{
    public class EnvironmentVariableProvider
    {
        private readonly EnvironmentVariableTarget _target;

        public EnvironmentVariableProvider() : this(EnvironmentVariableTarget.Process)
        {
        }

        public EnvironmentVariableProvider(EnvironmentVariableTarget target)
        {
            _target = target;
        }

        public void Delete(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Name was null or empty.", nameof(name));

            Set(name, null);
        }

        public bool Exists(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Name was null or empty.", nameof(name));

            return GetStringOrDefault(name) != null;
        }

        public void Set(string name, object value)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Name was null or empty.", nameof(name));

            System.Environment.SetEnvironmentVariable(name, value?.ToString(), _target);
        }

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

        public bool GetBool(string name)
        {
            var value = GetString(name);

            if (bool.TryParse(value, out bool result))
                return result;

            throw new UnexpectedEnvironmentVariableTypeException(name, value, typeof(bool));
        }

        public bool GetBoolOrDefault(string name, bool defaultValue = false)
        {
            var value = GetStringOrDefault(name);

            if (bool.TryParse(value, out bool result))
                return result;

            return defaultValue;
        }

        // GetByte
        // GetShort
        // GetInt
        // GetLong
        // GetFloat
        // GetDouble
        // GetDecimal
        // GetUri
        // GetGuid
    }
}