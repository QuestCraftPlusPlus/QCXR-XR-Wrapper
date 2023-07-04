# Logger

Logger 负责具体的将 Log 输出的工作，所有实现都必须派生自 [LoggerBase](xref:YFramework.Log.LoggerBase) 类 。

对于一个 LoggerBase 类，主要有以下特性：

-   Log Handle 函数处理

    各派生类中必须实现 `DebugHandle`，`InfoHandle`, `WarnHandle`, `ErrorHandle` 四个函数作为 Log 输出的具体操作，如在 [DefaultLogger](#defaultlogger) 中 `DebugHandle` 的实现如下：

    ```csharp
    protected override void DebugHandle(in object context, in string log) => UnityEngine.Debug.Log(log);
    ```

    对于每个 Log 函数，都会调用其对应的 Handle 函数。

-   Logger 嵌套

    一个 Logger 还可以包含其他的 Logger，将被包含的 Logger 称为 `wrappedLogger`，对于每个 Logger 的 Log 函数会先触发其 `wrappedLogger` 的 Log 函数，再触发自身的 Log 函数。

-   LogPrefix 嵌套

    在 [LoggerBase](xref:YFramework.Log.LoggerBase) 中默认的 Log 函数实现中，还会先调用 [GetFormattedLog](<xref:YFramework.Log.LoggerBase.GetFormattedLog(System.Object@,System.String@,YFramework.Log.LogPriority@)>) 函数将需要输出的 Log 信息与由 [LogPrefix](./LogPrefix.md) 定义的 Log 前缀信息结合，再交由具体的 Logger 进行输出。

如下为使用嵌套 Logger 并结合 LogPrefix 的实例：

使用如下方法在 `LogTest` 类中定义了一个名为 `DoubleLogger` 的 Logger，并将其赋值给 `YVRLog`：

```csharp
ContextInfoLogPrefix contextInfo = new ContextInfoLogPrefix();
DefaultLogger defaultLogger = new DefaultLogger();
DefaultLogger doubleLogger = new DefaultLogger(defaultLogger, contextInfo);
YVRLog.SetLogger(doubleLogger);
```

此时调用在 `LogTest` 中调用 `this.Debug("This is a debug")` 的结果如下：

```text
This is a debug
[Context: Main Camera-Main Camera]  This is a debug
```

其中第一条 Log 由作为 `wrappedLogger` 的 `defaultLogger` 打出，第二条 Log 由带有 `contextInfo` 的 `doubleLogger` 本身打出，在输出第二条时还结合了定义的 [ContextInfoLogPrefix](./LogPrefix.md#contextinfologprefix)。

目前实现了下述三种 Logger：

1. [DefaultLogger](#defaultlogger)：封装 Unity 默认的 `Debug.Log` 逻辑。
2. [DummyLogger](#dummylogger)：虚假的 Logger，实际上不输出任何 Log。
3. [YLogLogger](#yloglogger)：封装 SDK 中提供的 YLog 功能。

## DefaultLogger

封装 Unity 默认的 `Debug` 逻辑，即`DebugHandle`，`InfoHandle`, `WarnHandle`, `ErrorHandle` 分别调用 `UnityEngine.Debug.Log`，`UnityEngine.Debug.Log`，`UnityEngine.Debug.LogWarning` 和 `UnityEngine.Debug.Error`。

## DummyLogger

`DebugHandle`，`InfoHandle`, `WarnHandle`, `ErrorHandle` 中都是空实现。

## YLogLogger

该功能依赖应用的系统权限，请参考 [Requirement](../../Requirement.md#yloglogger)。

封装 SDK 中提供的 `YLog` 功能，`YLog` 会分别根据 Log 的优先级分别或同时向安卓的 Logcat 和内存中输出。

可以通过属性 `logcatPriority` 和 `ramLogPriority` 分别控制输出 Log 的优先级，如 `logcatPriority` 的优先级设为 `LogPriority::WARN`，则只有大于等于 warn 等级的 `Warn` 和 `Error` 才会输出到 Logcat 中。 如果将 `logcatPriority` 和 `ramLogPriority` 优先级都设置为 `LogPriority::HIGHEST` 则所有的 Log 操作都会被无视。

> [!Important]
> Logcat 默认的优先级为 `LogPriority::LOWEST`，RamLog 默认的等级为 `LogPriority.HIGHEST`，即默认所有的 Log 都会使用 Logcat 进行输出。

> [!Note]
> 直接向内存中写入 Log 信息，相较于使用 Android 的 Logcat 有更高的性能。因此可以将高频的信息写入内存中，并在需要的时候将数据从内存中保存至磁盘中查看。

可通过函数 `ConfigureYLog` 对 `YLog` 进行配置，包括前缀名与为 RamLog 分配的内存大小（默认为 3 MB）。当输出到内存中的 Log 超过了预定大小，则该内存中最老的 Log 会被覆盖。

可通过函数 `SaveLog` 将内存中的 Log 保存至 `sdcard` 中。
