# Grammophone.Configuration
This dual-target .NET Framework and .NET Core 3.1 library facilitates configuration files written in XAML.

The reason behind using XAML for configuration purposes is that XAML files can represent class instantiations in a lot more concise and less noisy way than traditional dependency injection frameworks like Spring or Unity. The latter systems typically clutter XML files with 'bean', 'constructor', 'param', 'property' elements and the like. On the other hand, XAML maps classes to elements and attributes, defining an XML schema from the object model directly, which IDE's like Visual Studio understand providing context-sensitive intellisense. The result is easy to read and maintain configuration files, especially when an application needs a lot of complex settings and not just some property values.

Of course, this is not a replacement for a general purpose dependency injection system. For example, only the singleton instantiation mode is supported (that's why the project is aimed at application configuration).

There are two ways to use the library:

1. Direct construction of an instance of type `T`: Call `XamlConfiguration<T>.LoadSettings(xamlFilename)` to create an instance of `T` defined in a XAML file describing the instance of `T`, specified with an absolute path or relative to the application. Failure throws a `ConfigurationException`.
2. Factory of an instance of type `T`: Add a configuration element in the application's .config file of type `XamlSettingsSection` using the standard .NET way. Set its `settingsXamlPath` to an absolute or relative to the application path of a XAML file describing the instance of type `T`. Then create a singleton of type `XamlConfiguration<T>`, passing the `name` of the `XamlSettingsSection` element in the application's standard config file. The instance of type `T` will be available via property `XamlConfiguration<T>.Settings`. Failure throws a `ConfigurationException`.

In case where any post-deserialization actions are required on the instance of type `T`, the type can implement the `IXamlLoadListener` interface to have its `OnPostLoad` method called after deserialization.

When such a XAML file is included in a Visual Studio project, make sure that you set its "Build Action" to "None" in its properties. You would probably also set the "Copy to Output Directory" property to "Copy if newer". If you use [XAML 2009 features](https://msdn.microsoft.com/en-us/library/ee792007(v=vs.110).aspx), in order to get Intellisense support for them, add the System.Xaml assembly to the project's references. This assembly is also referenced by this library.

This library has no dependencies.
