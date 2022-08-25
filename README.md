[![Build status](https://ci.appveyor.com/api/projects/status/github/bytedev/ByteDev.Configuration.Environment?branch=master&svg=true)](https://ci.appveyor.com/project/bytedev/ByteDev-Configuration-Environment/branch/master)
[![NuGet Package](https://img.shields.io/nuget/v/ByteDev.Configuration.Environment.svg)](https://www.nuget.org/packages/ByteDev.Configuration.Environment)
[![License: MIT](https://img.shields.io/badge/License-MIT-green.svg)](https://github.com/ByteDev/ByteDev.Configuration.Environment/blob/master/LICENSE)

# ByteDev.Configuration.Environment

Library to help when accessing environment variables.

## Installation

ByteDev.Configuration.Environment is hosted as a package on nuget.org.  To install from the Package Manager Console in Visual Studio run:

`Install-Package ByteDev.Configuration.Environment`

Further details can be found on the [nuget page](https://www.nuget.org/packages/ByteDev.Configuration.Environment/).

## Release Notes

Releases follow semantic versioning.

Full details of the release notes can be viewed on [GitHub](https://github.com/ByteDev/ByteDev.Configuration.Environment/blob/master/docs/RELEASE-NOTES.md).

## Usage

The primary class for accessing environment variable is `EnvironmentVariableProvider` (`IEnvironmentVariableProvider`).

`EnvironmentVariableProvider` has the following methods:
- Delete
- DeleteOrThrow
- Exists
- GetBoolean
- GetBooleanOrDefault
- GetByte
- GetByteOrDefault
- GetChar
- GetCharOrDefault
- GetDecimal
- GetDecimalOrDefault
- GetDouble
- GetDoubleOrDefault
- GetEnum
- GetEnumOrDefault
- GetGuid
- GetInt16
- GetInt16OrDefault
- GetInt32
- GetInt32OrDefault
- GetInt64
- GetInt64OrDefault
- GetUri
- GetSingle
- GetSingleOrDefault
- GetString
- GetStringOrDefault
- Set

```csharp
// Initialize provider with an optional target level (Process by default)

IEnvironmentVariableProvider provider = new EnvironmentVariableProvider(EnvironmentVariableTarget.User);
```

```csharp
// Example string value

provider.Set("MyVar1", "Value1");

bool exists = provider.Exists("MyVar1");    // exists == true

string s1 = provider.GetString("MyVar1");   // s1 == "Value1"

string s2 = provider.GetString("MyVar2");   // throws EnvironmentVariableNotExistException

string s3 = provider.GetStringOrDefault("MyVar1");            // s3 == "Value1"

string s4 = provider.GetStringOrDefault("MyVar2");            // s4 == null

string s5 = provider.GetStringOrDefault("MyVar2", "myValue"); // s5 == "myValue"

provider.Delete("MyVar1");

provider.Delete("MyVar2");   // Does not throw exception when not exist
```

```csharp
// Example URI value

provider.Set("MyUri1", "http://www.google.com/");
provider.Set("MyUri2", "ThisIsNotUri");

Uri uri0 = provider.GetUri("MyUri0");   // throws EnvironmentVariableNotExistException

Uri uri1 = provider.GetUri("MyUri1");   // uri1 == new Uri("http://www.google.com/")

Uri uri2 = provider.GetUri("MyUri2");   // throws UnexpectedEnvironmentVariableTypeException
```

