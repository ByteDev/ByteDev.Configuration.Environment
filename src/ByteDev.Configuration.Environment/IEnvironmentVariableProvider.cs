using System;

namespace ByteDev.Configuration.Environment
{
    public interface IEnvironmentVariableProvider
    {
        /// <summary>
        /// Delete an environment variable. If it does not exist then no
        /// exception is thrown.
        /// </summary>
        /// <param name="name">Name of environment variable.</param>
        /// <exception cref="T:System.ArgumentException"><paramref name="name" /> was null or empty.</exception>
        void Delete(string name);

        /// <summary>
        /// Determines if an environment variable exists.
        /// </summary>
        /// <param name="name">Name of environment variable.</param>
        /// <returns>True if the environment variable exists; otherwise false.</returns>
        /// <exception cref="T:System.ArgumentException"><paramref name="name" /> was null or empty.</exception>
        bool Exists(string name);

        /// <summary>
        /// Set an environment variable. If it does not exist it will be created otherwise
        /// it's value will be updated.
        /// </summary>
        /// <param name="name">Name of environment variable.</param>
        /// <param name="value">Environment variable's new value.</param>
        /// <exception cref="T:System.ArgumentException"><paramref name="name" /> was null or empty.</exception>
        void Set(string name, object value);

        /// <summary>
        /// Retrieve an environment variable as a string. If it does not exist then an
        /// exception will be thrown.
        /// </summary>
        /// <param name="name">Name of environment variable.</param>
        /// <returns>Environment variable's value.</returns>
        /// <exception cref="T:System.ArgumentException"><paramref name="name" /> was null or empty.</exception>
        /// <exception cref="T:ByteDev.Configuration.Environment.EnvironmentVariableNotExistException">Environment variable does not exist.</exception>
        string GetString(string name);

        /// <summary>
        /// Retrieve an environment variable as a string. If it does not exist then the
        /// <paramref name="defaultValue" /> will be returned.
        /// </summary>
        /// <param name="name">Name of environment variable.</param>
        /// <param name="defaultValue">Value to return if the environment variable does not exist.</param>
        /// <returns>Environment variable's value.</returns>
        /// <exception cref="T:System.ArgumentException"><paramref name="name" /> was null or empty.</exception>
        string GetStringOrDefault(string name, string defaultValue = null);

        /// <summary>
        /// Retrieve an environment variable as a bool. If it does not exist or it's value cannot be cast
        /// then an exception will be thrown.
        /// </summary>
        /// <param name="name">Name of environment variable.</param>
        /// <returns>Environment variable's value.</returns>
        /// <exception cref="T:System.ArgumentException"><paramref name="name" /> was null or empty.</exception>
        /// <exception cref="T:ByteDev.Configuration.Environment.EnvironmentVariableNotExistException">Environment variable does not exist.</exception>
        /// <exception cref="T:ByteDev.Configuration.Environment.UnexpectedEnvironmentVariableTypeException">Environment variable value is not a bool.</exception>
        bool GetBool(string name);

        /// <summary>
        /// Retrieve an environment variable as a bool. If it does not exist or it's value cannot be cast 
        /// then the <paramref name="defaultValue" /> will be returned.
        /// </summary>
        /// <param name="name">Name of environment variable.</param>
        /// <param name="defaultValue">Value to return if the environment variable does not exist.</param>
        /// <returns>Environment variable's value.</returns>
        /// <exception cref="T:System.ArgumentException"><paramref name="name" /> was null or empty.</exception>
        bool GetBoolOrDefault(string name, bool defaultValue = false);

        /// <summary>
        /// Retrieve an environment variable as a byte. If it does not exist or it's value cannot be cast
        /// then an exception will be thrown.
        /// </summary>
        /// <param name="name">Name of environment variable.</param>
        /// <returns>Environment variable's value.</returns>
        /// <exception cref="T:System.ArgumentException"><paramref name="name" /> was null or empty.</exception>
        /// <exception cref="T:ByteDev.Configuration.Environment.EnvironmentVariableNotExistException">Environment variable does not exist.</exception>
        /// <exception cref="T:ByteDev.Configuration.Environment.UnexpectedEnvironmentVariableTypeException">Environment variable value is not a byte.</exception>
        byte GetByte(string name);

        /// <summary>
        /// Retrieve an environment variable as a byte. If it does not exist or it's value cannot be cast 
        /// then the <paramref name="defaultValue" /> will be returned.
        /// </summary>
        /// <param name="name">Name of environment variable.</param>
        /// <param name="defaultValue">Value to return if the environment variable does not exist.</param>
        /// <returns>Environment variable's value.</returns>
        /// <exception cref="T:System.ArgumentException"><paramref name="name" /> was null or empty.</exception>
        byte GetByteOrDefault(string name, byte defaultValue = 0);

        /// <summary>
        /// Retrieve an environment variable as a short. If it does not exist or it's value cannot be cast
        /// then an exception will be thrown.
        /// </summary>
        /// <param name="name">Name of environment variable.</param>
        /// <returns>Environment variable's value.</returns>
        /// <exception cref="T:System.ArgumentException"><paramref name="name" /> was null or empty.</exception>
        /// <exception cref="T:ByteDev.Configuration.Environment.EnvironmentVariableNotExistException">Environment variable does not exist.</exception>
        /// <exception cref="T:ByteDev.Configuration.Environment.UnexpectedEnvironmentVariableTypeException">Environment variable value is not a short.</exception>
        short GetShort(string name);

        /// <summary>
        /// Retrieve an environment variable as a short. If it does not exist or it's value cannot be cast 
        /// then the <paramref name="defaultValue" /> will be returned.
        /// </summary>
        /// <param name="name">Name of environment variable.</param>
        /// <param name="defaultValue">Value to return if the environment variable does not exist.</param>
        /// <returns>Environment variable's value.</returns>
        /// <exception cref="T:System.ArgumentException"><paramref name="name" /> was null or empty.</exception>
        short GetShortOrDefault(string name, short defaultValue = 0);

        /// <summary>
        /// Retrieve an environment variable as a int. If it does not exist or it's value cannot be cast
        /// then an exception will be thrown.
        /// </summary>
        /// <param name="name">Name of environment variable.</param>
        /// <returns>Environment variable's value.</returns>
        /// <exception cref="T:System.ArgumentException"><paramref name="name" /> was null or empty.</exception>
        /// <exception cref="T:ByteDev.Configuration.Environment.EnvironmentVariableNotExistException">Environment variable does not exist.</exception>
        /// <exception cref="T:ByteDev.Configuration.Environment.UnexpectedEnvironmentVariableTypeException">Environment variable value is not a int.</exception>
        int GetInt(string name);

        /// <summary>
        /// Retrieve an environment variable as a int. If it does not exist or it's value cannot be cast 
        /// then the <paramref name="defaultValue" /> will be returned.
        /// </summary>
        /// <param name="name">Name of environment variable.</param>
        /// <param name="defaultValue">Value to return if the environment variable does not exist.</param>
        /// <returns>Environment variable's value.</returns>
        /// <exception cref="T:System.ArgumentException"><paramref name="name" /> was null or empty.</exception>
        int GetIntOrDefault(string name, int defaultValue = 0);

        /// <summary>
        /// Retrieve an environment variable as a long. If it does not exist or it's value cannot be cast
        /// then an exception will be thrown.
        /// </summary>
        /// <param name="name">Name of environment variable.</param>
        /// <returns>Environment variable's value.</returns>
        /// <exception cref="T:System.ArgumentException"><paramref name="name" /> was null or empty.</exception>
        /// <exception cref="T:ByteDev.Configuration.Environment.EnvironmentVariableNotExistException">Environment variable does not exist.</exception>
        /// <exception cref="T:ByteDev.Configuration.Environment.UnexpectedEnvironmentVariableTypeException">Environment variable value is not a long.</exception>
        long GetLong(string name);

        /// <summary>
        /// Retrieve an environment variable as a long. If it does not exist or it's value cannot be cast 
        /// then the <paramref name="defaultValue" /> will be returned.
        /// </summary>
        /// <param name="name">Name of environment variable.</param>
        /// <param name="defaultValue">Value to return if the environment variable does not exist.</param>
        /// <returns>Environment variable's value.</returns>
        /// <exception cref="T:System.ArgumentException"><paramref name="name" /> was null or empty.</exception>
        long GetLongOrDefault(string name, long defaultValue = 0);

        /// <summary>
        /// Retrieve an environment variable as a float. If it does not exist or it's value cannot be cast
        /// then an exception will be thrown.
        /// </summary>
        /// <param name="name">Name of environment variable.</param>
        /// <returns>Environment variable's value.</returns>
        /// <exception cref="T:System.ArgumentException"><paramref name="name" /> was null or empty.</exception>
        /// <exception cref="T:ByteDev.Configuration.Environment.EnvironmentVariableNotExistException">Environment variable does not exist.</exception>
        /// <exception cref="T:ByteDev.Configuration.Environment.UnexpectedEnvironmentVariableTypeException">Environment variable value is not a float.</exception>
        float GetFloat(string name);

        /// <summary>
        /// Retrieve an environment variable as a float. If it does not exist or it's value cannot be cast 
        /// then the <paramref name="defaultValue" /> will be returned.
        /// </summary>
        /// <param name="name">Name of environment variable.</param>
        /// <param name="defaultValue">Value to return if the environment variable does not exist.</param>
        /// <returns>Environment variable's value.</returns>
        /// <exception cref="T:System.ArgumentException"><paramref name="name" /> was null or empty.</exception>
        float GetFloatOrDefault(string name, float defaultValue = 0);

        /// <summary>
        /// Retrieve an environment variable as a double. If it does not exist or it's value cannot be cast
        /// then an exception will be thrown.
        /// </summary>
        /// <param name="name">Name of environment variable.</param>
        /// <returns>Environment variable's value.</returns>
        /// <exception cref="T:System.ArgumentException"><paramref name="name" /> was null or empty.</exception>
        /// <exception cref="T:ByteDev.Configuration.Environment.EnvironmentVariableNotExistException">Environment variable does not exist.</exception>
        /// <exception cref="T:ByteDev.Configuration.Environment.UnexpectedEnvironmentVariableTypeException">Environment variable value is not a double.</exception>
        double GetDouble(string name);

        /// <summary>
        /// Retrieve an environment variable as a double. If it does not exist or it's value cannot be cast 
        /// then the <paramref name="defaultValue" /> will be returned.
        /// </summary>
        /// <param name="name">Name of environment variable.</param>
        /// <param name="defaultValue">Value to return if the environment variable does not exist.</param>
        /// <returns>Environment variable's value.</returns>
        /// <exception cref="T:System.ArgumentException"><paramref name="name" /> was null or empty.</exception>
        double GetDoubleOrDefault(string name, double defaultValue = 0);

        /// <summary>
        /// Retrieve an environment variable as a decimal. If it does not exist or it's value cannot be cast
        /// then an exception will be thrown.
        /// </summary>
        /// <param name="name">Name of environment variable.</param>
        /// <returns>Environment variable's value.</returns>
        /// <exception cref="T:System.ArgumentException"><paramref name="name" /> was null or empty.</exception>
        /// <exception cref="T:ByteDev.Configuration.Environment.EnvironmentVariableNotExistException">Environment variable does not exist.</exception>
        /// <exception cref="T:ByteDev.Configuration.Environment.UnexpectedEnvironmentVariableTypeException">Environment variable value is not a decimal.</exception>
        decimal GetDecimal(string name);

        /// <summary>
        /// Retrieve an environment variable as a decimal. If it does not exist or it's value cannot be cast 
        /// then the <paramref name="defaultValue" /> will be returned.
        /// </summary>
        /// <param name="name">Name of environment variable.</param>
        /// <param name="defaultValue">Value to return if the environment variable does not exist.</param>
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
    }
}