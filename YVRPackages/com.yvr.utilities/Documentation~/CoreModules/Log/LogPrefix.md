# LogPrefix

LogPrefix 用来为 Log 增加额外的前缀信息。所有的 LogPrefix 都派生自 `LogPrefixBase` 类，且可以包含其他的 `LogPrefixBase`。将被包含的 `LogPrefixBase` 称为 `wrappedLogPrefix`。

所有的 LogPrefix 都需要重写 [GetPrefix](<xref:YFramework.Log.LogPrefixBase.GetPrefix(System.Object@,System.String@,YFramework.Log.LogPriority@)>) 函数。

当 [LoggerBase](xref:YFramework.Log.LoggerBase) 中触发 [GetFormattedLog](<xref:YFramework.Log.LoggerBase.GetFormattedLog(System.Object@,System.String@,YFramework.Log.LogPriority@)>) 时，会调用 [GetCombinedPrefix](<xref:YFramework.Log.LogPrefixBase.GetCombinedPrefix(System.Object@,System.String@,YFramework.Log.LogPriority@)>)。

该函数会触发 `wrappedLogPrefix` 以及自身的 [GetPrefix](<xref:YFramework.Log.LogPrefixBase.GetPrefix(System.Object@,System.String@,YFramework.Log.LogPriority@)>) 函数，并将结果结合在一起。

## DummyLogPrefix

`GetPrefix` 函数返回空字符串

## ContextInfoLogPrefix

返回调用 Log 函数的对象的类信息，对于 `GameObject` 会输出 `GameObject.name`，对于 `MonoBehaviour` 会返回 `GameObject.name - MonoBehaviour.name`，对于其他类型会返回该类型的类型名。

类信息会以 `Context: <contextInfo>` 的格式被输出。

## PriorityLogPrefix

返回调用 Log 函数的优先级，会以 `Priority: <PriorityInfo>` 的格式被输出。
