# 成就

## 整合成就

在你创建完成就后，你可以把它们整合到你的游戏中。当你调用这部分的函数时，请确保使用你在开发者仪表板上指定的成就名称。

1. `YVR.Platform.GetDefinitionsByName`：检索特定成就信息，包括成就名称、类型、目标或比特域长度。
2. `YVR.Platform. GetProgressByName`：检索用户在特定成就上的进展信息；包括名称、解锁状态、成就解锁时间、当前位域和当前计数。
3. `YVR.Platform.GetAllDefinitions`：检索所有成就信息；包括成就名称、类型、目标或位域长度。
4. `YVR.Platform.GetAllProgress`：检索所有成就信息，包括成就名称、类型、目标或位域长度。检索用户在所有成就上的进展信息；包括名称、解锁状态、成就解锁时间、当前比特域和当前计数。

以下 SDK 方法可以为任何具有客户端授权写入策略的成就调用。

1. `YVR.Platform.UnlockAchievement`: 解锁一个指定成就。这将完全解锁一个成就，包括计数和比特字段成就，即使没有达到目标。
2. `YVR.Platform.AchievementAddCount`: 增加计数成就的计数。
3. `YVR.Platform.AchievementAddFields`: 解锁位域类型成就中的一个位。

> [!NOTE]
> 更多信息，请参考 [Achievement API Documentation](xref:YVR.Platform.Achievement)。
