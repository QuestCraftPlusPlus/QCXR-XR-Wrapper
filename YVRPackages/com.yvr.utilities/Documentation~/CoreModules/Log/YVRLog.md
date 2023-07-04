# YVRLog

YVRLog 中会管理一个由 [Logger](./Logger.md) 组成的 List，可以通过 `RegisterLogger`，`UnregisterLogger` 和 `SetLogger` 和 `ClearLoggers` 函数对 Logger List 进行控制。

> [!Note]
> YVRLog 默认以 [DefaultLogger](./Logger.md#defaultlogger) 作为 List 中的唯一元素。

YVRLog 通过拓展函数的方式为 `System.Object` 提供了 `Debug`，`Info`， `Warn` 和 `Error` 四种输出 Log 的函数（下统称为 `Log 函数`）。当一个 Log 函数会触发时， Logger List 中的所有 Logger 都会进行相应的输出。

> [!Note]
> YVRLog 并不直接处理 Log 输出，所有的 Log 输出由各 Logger 决定。