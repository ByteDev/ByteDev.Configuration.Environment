using System;

namespace ByteDev.Configuration.Environment
{
    public interface IEnvironmentVariableProvider
    {
        /// <summary>
        /// Delete an environment variable. If the variable does not exist then no
        /// exception is thrown.
        /// </summary>
        /// <param name="name">Name of environment variable.</param>
        /// <exception cref="T:System.ArgumentException"><paramref name="name" /> was null or empty.</exception>
        void Delete(string name);

        /// <summary>
        /// Delete an environment variable. If the variable does not exist then an exception
        /// of type <see cref="T:ByteDev.Configuration.Environment.EnvironmentVariableNotExistException" /> is thrown.
        /// </summary>
        /// <param name="name">Name of environment variable.</param>
        /// <exception cref="T:System.ArgumentException"><paramref name="name" /> was null or empty.</exception>
        /// <exception cref="T:ByteDev.Configuration.Environment.EnvironmentVariableNotExistException">Environment variable does not exist.</exception>
        void DeleteOrThrow(string name);

        /// <summary>
        /// Determines if an environment variable exists.
        /// </summary>
        /// <param name="name">Name of environment variable.</param>
        /// <returns>True if the environment variable exists; otherwise false.</returns>
        /// <exception cref="T:System.ArgumentException"><paramref name="name" /> was null or empty.</exception>
        bool Exists(string name);

        /// <summary>
        /// Set an environment variable. If it does not exist it will be created otherwise
        /// it's value will be updated. If the value is null then the variable will be deleted.
        /// </summary>
        /// <param name="name">Name of environment variable.</param>
        /// <param name="value">Environment variable's new value.</param>
        /// <exception cref="T:System.ArgumentException"><paramref name="name" /> was null or empty.</exception>
        void Set(string name, object value);

        /// <summary>
        /// Retrieve an environment variable as a String. If it does not exist then an
        /// exception will be thrown.
        /// </summary>
        /// <param name="name">Name of environment variable.</param>
        /// <returns>Environment variable's value.</returns>
        /// <exception cref="T:System.ArgumentException"><paramref name="name" /> was null or empty.</exception>
        /// <exception cref="T:ByteDev.Configuration.Environment.EnvironmentVariableNotExistException">Environment variable does not exist.</exception>
        string GetString(string name);

        /// <summary>
        /// Retrieve an environment variable as a String. If it does not exist then the
        /// <paramref name="defaultValue" /> will be returned.
        /// </summary>
        /// <param name="name">Name of environment variable.</param>
        /// <param name="defaultValue">Value to return if the environment variable does not exist.</param>
        /// <returns>Environment variable's value.</returns>
        /// <exception cref="T:System.ArgumentException"><paramref name="name" /> was null or empty.</exception>
        string GetStringOrDefault(string name, string defaultValue = null);

        /// <summary>
        /// Retrieve an environment variable as a Char. If it does not exist or it's value cannot be cast
        /// then an exception will be thrown.
        /// </summary>
        /// <param name="name">Name of environment variable.</param>
        /// <returns>Environment variable's value.</returns>
        /// <exception cref="T:System.ArgumentException"><paramref name="name" /> was null or empty.</exception>
        /// <exception cref="T:ByteDev.Configuration.Environment.EnvironmentVariableNotExistException">Environment variable does not exist.</exception>
        /// <exception cref="T:ByteDev.Configuration.Environment.UnexpectedEnvironmentVariableTypeException">Environment variable value is not a char.</exception>
        char GetChar(string name);

        /// <summary>
        /// Retrieve an environment variable as a Char. If it does not exist or it's value cannot be cast 
        /// then the <paramref name="defaultValue" /> will be returned.
        /// </summary>
        /// <param name="name">Name of environment variable.</param>
        /// <param name="defaultValue">Value to return if the environment variable does not exist or cannot be cast.</param>
        /// <returns>Environment variable's value.</returns>
        /// <exception cref="T:System.ArgumentException"><paramref name="name" /> was null or empty.</exception>
        char GetCharOrDefault(string name, char defaultValue = '\0');

        /// <summary>
        /// Retrieve an environment variable as a Boolean. If it does not exist or it's value cannot be cast
        /// then an exception will be thrown.
        /// </summary>
        /// <param name="name">Name of environment variable.</param>
        /// <returns>Environment variable's value.</returns>
        /// <exception cref="T:System.ArgumentException"><paramref name="name" /> was null or empty.</exception>
        /// <exception cref="T:ByteDev.Configuration.Environment.EnvironmentVariableNotExistException">Environment variable does not exist.</exception>
        /// <exception cref="T:ByteDev.Configuration.Environment.UnexpectedEnvironmentVariableTypeException">Environment variable value is not a bool.</exception>
        bool GetBoolean(string name);

        /// <summary>
        /// Retrieve an environment variable as a Boolean. If it does not exist or it's value cannot be cast 
        /// then the <paramref name="defaultValue" /> will be returned.
        /// </summary>
        /// <param name="name">Name of environment variable.</param>
        /// <param name="defaultValue">Value to return if the environment variable does not exist or cannot be cast.</param>
        /// <returns>Environment variable's value.</returns>
        /// <exception cref="T:System.ArgumentException"><paramref name="name" /> was null or empty.</exception>
        bool GetBooleanOrDefault(string name, bool defaultValue = false);

        /// <summary>
        /// Retrieve an environment variable as a Byte. If it does not exist or it's value cannot be cast
        /// then an exception will be thrown.
        /// </summary>
        /// <param name="name">Name of environment variable.</param>
        /// <returns>Environment variable's value.</returns>
        /// <exception cref="T:System.ArgumentException"><paramref name="name" /> was null or empty.</exception>
        /// <exception cref="T:ByteDev.Configuration.Environment.EnvironmentVariableNotExistException">Environment variable does not exist.</exception>
        /// <exception cref="T:ByteDev.Configuration.Environment.UnexpectedEnvironmentVariableTypeException">Environment variable value is not a byte.</exception>
        byte GetByte(string name);

        /// <summary>
        /// Retrieve an environment variable as a Byte. If it does not exist or it's value cannot be cast 
        /// then the <paramref name="defaultValue" /> will be returned.
        /// </summary>
        /// <param name="name">Name of environment variable.</param>
        /// <param name="defaultValue">Value to return if the environment variable does not exist or cannot be cast.</param>
        /// <returns>Environment variable's value.</returns>
        /// <exception cref="T:System.ArgumentException"><paramref name="name" /> was null or empty.</exception>
        byte GetByteOrDefault(string name, byte defaultValue = 0);

        /// <summary>
        /// Retrieve an environment variable as a Int16. If it does not exist or it's value cannot be cast
        /// then an exception will be thrown.
        /// </summary>
        /// <param name="name">Name of environment variable.</param>
        /// <returns>Environment variable's value.</returns>
        /// <exception cref="T:System.ArgumentException"><paramref name="name" /> was null or empty.</exception>
        /// <exception cref="T:ByteDev.Configuration.Environment.EnvironmentVariableNotExistException">Environment variable does not exist.</exception>
        /// <exception cref="T:ByteDev.Configuration.Environment.UnexpectedEnvironmentVariableTypeException">Environment variable value is not a short.</exception>
        short GetInt16(string name);

        /// <summary>
        /// Retrieve an environment variable as a Int16. If it does not exist or it's value cannot be cast 
        /// then the <paramref name="defaultValue" /> will be returned.
        /// </summary>
        /// <param name="name">Name of environment variable.</param>
        /// <param name="defaultValue">Value to return if the environment variable does not exist or cannot be cast.</param>
        /// <returns>Environment variable's value.</returns>
        /// <exception cref="T:System.ArgumentException"><paramref name="name" /> was null or empty.</exception>
        short GetInt16OrDefault(string name, short defaultValue = 0);

        /// <summary>
        /// Retrieve an environment variable as a Int32. If it does not exist or it's value cannot be cast
        /// then an exception will be thrown.
        /// </summary>
        /// <param name="name">Name of environment variable.</param>
        /// <returns>Environment variable's value.</returns>
        /// <exception cref="T:System.ArgumentException"><paramref name="name" /> was null or empty.</exception>
        /// <exception cref="T:ByteDev.Configuration.Environment.EnvironmentVariableNotExistException">Environment variable does not exist.</exception>
        /// <exception cref="T:ByteDev.Configuration.Environment.UnexpectedEnvironmentVariableTypeException">Environment variable value is not a int.</exception>
        int GetInt32(string name);

        /// <summary>
        /// Retrieve an environment variable as a Int32. If it does not exist or it's value cannot be cast 
        /// then the <paramref name="defaultValue" /> will be returned.
        /// </summary>
        /// <param name="name">Name of environment variable.</param>
        /// <param name="defaultValue">Value to return if the environment variable does not exist or cannot be cast.</param>
        /// <returns>Environment variable's value.</returns>
        /// <exception cref="T:System.ArgumentException"><paramref name="name" /> was null or empty.</exception>
        int GetInt32OrDefault(string name, int defaultValue = 0);

        /// <summary>
        /// Retrieve an environment variable as a Int64. If it does not exist or it's value cannot be cast
        /// then an exception will be thrown.
        /// </summary>
        /// <param name="name">Name of environment variable.</param>
        /// <returns>Environment variable's value.</returns>
        /// <exception cref="T:System.ArgumentException"><paramref name="name" /> was null or empty.</exception>
        /// <exception cref="T:ByteDev.Configuration.Environment.EnvironmentVariableNotExistException">Environment variable does not exist.</exception>
        /// <exception cref="T:ByteDev.Configuration.Environment.UnexpectedEnvironmentVariableTypeException">Environment variable value is not a long.</exception>
        long GetInt64(string name);

        /// <summary>
        /// Retrieve an environment variable as a Int64. If it does not exist or it's value cannot be cast 
        /// then the <paramref name="defaultValue" /> will be returned.
        /// </summary>
        /// <param name="name">Name of environment variable.</param>
        /// <param name="defaultValue">Value to return if the environment variable does not exist or cannot be cast.</param>
        /// <returns>Environment variable's value.</returns>
        /// <exception cref="T:System.ArgumentException"><paramref name="name" /> was null or empty.</exception>
        long GetInt64OrDefault(string name, long defaultValue = 0);

        /// <summary>
        /// Retrieve an environment variable as a Single. If it does not exist or it's value cannot be cast
        /// then an exception will be thrown.
        /// </summary>
        /// <param name="name">Name of environment variable.</param>
        /// <returns>Environment variable's value.</returns>
        /// <exception cref="T:System.ArgumentException"><paramref name="name" /> was null or empty.</exception>
        /// <exception cref="T:ByteDev.Configuration.Environment.EnvironmentVariableNotExistException">Environment variable does not exist.</exception>
        /// <exception cref="T:ByteDev.Configuration.Environment.UnexpectedEnvironmentVariableTypeException">Environment variable value is not a float.</exception>
        float GetSingle(string name);

        /// <summary>
        /// Retrieve an environment variable as a Single. If it does not exist or it's value cannot be cast 
        /// then the <paramref name="defaultValue" /> will be returned.
        /// </summary>
        /// <param name="name">Name of environment variable.</param>
        /// <param name="defaultValue">Value to return if the environment variable does not exist or cannot be cast.</param>
        /// <returns>Environment variable's value.</returns>
        /// <exception cref="T:System.ArgumentException"><paramref name="name" /> was null or empty.</exception>
        float GetSingleOrDefault(string name, float defaultValue = 0);

        /// <summary>
        /// Retrieve an environment variable as a Double. If it does not exist or it's value cannot be cast
        /// then an exception will be thrown.
        /// </summary>
        /// <param name="name">Name of environment variable.</param>
        /// <returns>Environment variable's value.</returns>
        /// <exception cref="T:System.ArgumentException"><paramref name="name" /> was null or empty.</exception>
        /// <exception cref="T:ByteDev.Configuration.Environment.EnvironmentVariableNotExistException">Environment variable does not exist.</exception>
        /// <exception cref="T:ByteDev.Configuration.Environment.UnexpectedEnvironmentVariableTypeException">Environment variable value is not a double.</exception>
        double GetDouble(string name);

        /// <summary>
        /// Retrieve an environment variable as a Double. If it does not exist or it's value cannot be cast 
        /// then the <paramref name="defaultValue" /> will be returned.
        /// </summary>
        /// <param name="name">Name of environment variable.</param>
        /// <param name="defaultValue">Value to return if the environment variable does not exist or cannot be cast.</param>
        /// <returns>Environment variable's value.</returns>
        /// <exception cref="T:System.ArgumentException"><paramref name="name" /> was null or empty.</exception>
        double GetDoubleOrDefault(string name, double defaultValue = 0);

        /// <summary>
        /// Retrieve an environment variable as a Decimal. If it does not exist or it's value cannot be cast
        /// then an exception will be thrown.
        /// </summary>
        /// <param name="name">Name of environment variable.</param>
        /// <returns>Environment variable's value.</returns>
        /// <exception cref="T:System.ArgumentException"><paramref name="name" /> was null or empty.</exception>
        /// <exception cref="T:ByteDev.Configuration.Environment.EnvironmentVariableNotExistException">Environment variable does not exist.</exception>
        /// <exception cref="T:ByteDev.Configuration.Environment.UnexpectedEnvironmentVariableTypeException">Environment variable value is not a decimal.</exception>
        decimal GetDecimal(string name);

        /// <summary>
        /// Retrieve an environment variable as a Decimal. If it does not exist or it's value cannot be cast 
        /// then the <paramref name="defaultValue" /> will be returned.
        /// </summary>
        /// <param name="name">Name of environment variable.</param>
        /// <param name="defaultValue">Value to return if the environment variable does not exist or cannot be cast.</param>
        /// <returns>Environment variable's value.</returns>
        /// <exception cref="T:System.ArgumentException"><paramref name="name" /> was null or empty.</exception>
        decimal GetDecimalOrDefault(string name, decimal defaultValue = 0);

        /// <summary>
        /// Retrieve an environment variable as a Uri. If it does not exist or it's value cannot be cast
        /// then an exception will be thrown.
        /// </summary>
        /// <param name="name">Name of environment variable.</param>
        /// <returns>Environment variable's value.</returns>
        /// <exception cref="T:System.ArgumentException"><paramref name="name" /> was null or empty.</exception>
        /// <exception cref="T:ByteDev.Configuration.Environment.EnvironmentVariableNotExistException">Environment variable does not exist.</exception>
        /// <exception cref="T:ByteDev.Configuration.Environment.UnexpectedEnvironmentVariableTypeException">Environment variable value is not a Uri.</exception>
        Uri GetUri(string name);

        /// <summary>
        /// Retrieve an environment variable as a Guid. If it does not exist or it's value cannot be cast
        /// then an exception will be thrown.
        /// </summary>
        /// <param name="name">Name of environment variable.</param>
        /// <returns>Environment variable's value.</returns>
        /// <exception cref="T:System.ArgumentException"><paramref name="name" /> was null or empty.</exception>
        /// <exception cref="T:ByteDev.Configuration.Environment.EnvironmentVariableNotExistException">Environment variable does not exist.</exception>
        /// <exception cref="T:ByteDev.Configuration.Environment.UnexpectedEnvironmentVariableTypeException">Environment variable value is not a Guid.</exception>
        Guid GetGuid(string name);

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
        TEnum GetEnum<TEnum>(string name) where TEnum : struct, Enum;

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
        TEnum GetEnumOrDefault<TEnum>(string name, TEnum defaultValue) where TEnum : struct, Enum;

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
        DateTime GetDateTime(string name, string format);
    }
}