# Gramma.Configuration
This .NET library facilitates configuration files written in XAML.

The reason behind using XAML for configuration purposes is that XAML files can represent class instantiations in a lot more concise and less noisy way than traditional dependency injection frameworks like Spring or Unity. The latter systems typically clutter XML files with 'bean', 'constructor', 'param', 'property' elements and the like. On the other hand, XAML maps classes to elements and attributes, defining an XML schema from the object model directly, which IDE's like Visual Studio understand providing context-sensitive intellisense. The result is easy to read and maintain configuration files, especially when an application needs a lot of complex settings and not just some property values.

Of course, this is not a replacement for a general purpose dependency injection system. For example, only the singleton instantiation mode is supported (that's why the project is aimed at application configuration).

In order to use the system to provide an instance of settings type `T`, add a configuration element in the application's config file of type `XamlSettingsSection` using the standard .NET way and set its `settingsXamlPath` to a XAML filename describing the instance of type `T`. Then create a singleton of type `XamlConfiguration<T>`, passing the name of the `XamlSettingsSection` element in the application's standard config file. The instance of type `T` will be available via property `XamlConfiguration<T>.Settings` or a `ConfigurationException` will be thrown.

In case where any post-deserialization actions are required on the instance of type `T`, the type can implement the `IXamlLoadListener` interface to have its `OnPostLoad` method called after deserialization.

This library has no dependencies.
