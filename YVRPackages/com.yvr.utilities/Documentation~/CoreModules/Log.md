# Overview

Log 模块旨在为 Unity 默认的 `UnityEngin.Debug` 模块添加如下功能：

1. 使用相同的接口封装不同实现的 Logger，如输出到 Logcat 的 Logger，输出到内存中的 Logger，输出到本地文件的 Logger。
2. 为 Log 增控制能力，如 Runtime 时设置输出 Log 的过滤级
3. 全局为输出的 Log 增加特定信息，如在每条 Log 输出前添加打出该 Log 的游戏物体的名称

该模块主要分为如下部分：

1. [YVRLog](./Log/YVRLog.md)：模块的核心，实现了对各 Logger 的管理控制，并通过拓展函数的方式为 `system.object` 类提供输出 Log 的方式。
2. [Logger](./Log/Logger.md)：各 Logger 的具体实现
3. [LogPrefix](./Log/LogPrefix.md)：为各 Log 提供增加前缀信息的功能。
4. [LoggerController](./Log/LoggerController.md)：为 Logger 提供控制函数

其中 [Logger](./Log/Logger.md)，[LogPrefix](./Log/LogPrefix.md)，[LoggerController](./Log/LoggerController.md) 都以装饰器模式设计，保证了高拓展性。

整体关系如下所示：

```plantuml
Class YVRLog

Class LoggerBase
Class DefaultLogger
Class DummyLogger
Class YLogLogger

class LogPrefixBase
class DummyLogPrefix
class ContextInfoLogPrefix

class LoggerControllerBase
class LoggerDummyController
class YLogLoggerController


YVRLog *-- LoggerBase

' Part for LoggerBase
LoggerBase *-- LogPrefixBase
LoggerBase *-- LoggerControllerBase
LoggerBase o-- LoggerBase

DefaultLogger --|> LoggerBase
DummyLogger --|> LoggerBase
YLogLogger --|> LoggerBase

' Part for LogPrefix
LogPrefixBase o-- LogPrefixBase

DummyLogPrefix --|> LogPrefixBase
ContextInfoLogPrefix --|> LogPrefixBase

' Part for LoggerController
LoggerControllerBase <--> LoggerBase
LoggerControllerBase o-- LoggerControllerBase

LoggerDummyController --|> LoggerControllerBase
YLogLoggerController --|> LoggerControllerBase
YLogLoggerController ..> YLogLogger
```
