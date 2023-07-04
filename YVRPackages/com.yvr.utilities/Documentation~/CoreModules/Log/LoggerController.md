# Logger Controller

`Logger Controller` 用来对 [Logger](./Logger.md) 进行相应控制。当一个 [Logger](./Logger.md) 被设置了 Controller 后，该 Logger 的每个输出函数中都会调用 [IsLogValid](xref:YFramework.Log.LoggerControllerBase.IsLogValid(System.Object@,System.String@,YFramework.Log.LogPriority@)) 函数判断该 Log 函数是否该被输出。


## LoggerDummyController

不对 Logger 进行任何控制

## YLogLoggerController

提供通过安卓广播对 [YLogLogger](./Logger.md#yloglogger) 进行控制的功能，包括保存 RamLog 以及设置 Log 优先级过滤阈值。

默认保存 RamLog 的广播 Action 为 `YLog.SaveRamLog`，可通过如下 adb 命令发送广播：

```shell
adb shell am broadcast -a YLog.SaveRamLog
```

当收到该广播后，会自动将 RamLog 写在内存中的数据保存到 `/sdcard/misc/yvr/<Tag>` 目录下，其中 `Tag` 值可通过 [YLogLogger.ConfigureYLog](xref:YFramework.Log.YLogLogger.ConfigureYLog(System.String,System.Int32)) 函数设置。

默认设置 Log 优先级过滤阈值的 Action 为 `YLog.ConfigurePriorityTHold`，且分别通过 Key 值 `logcatTHold` 和 `ramLogTHold` 设置 logcat 及 ramLog 的优先级阈值。

可通过如下 adb 命令发送广播：
```shell
adb shell am broadcast -a YLog.ConfigurePriorityTHold --ei logcatTHold 2 --ei ramLogTHold 0
```

上例中，大于等于 `INFO` 等级的 Log 会被输出到 Logcat，所有 Log 都会被输出到 RamLog。