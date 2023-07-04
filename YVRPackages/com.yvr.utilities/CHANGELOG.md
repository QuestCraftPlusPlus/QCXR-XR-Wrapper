# Changelog

## [0.9.3] - 2023-03-31

### Changed

- 减少检测 Ray 是否和 RectTransform 是否相交算法的 GC Alloc

## [0.9.2] - 2023-03-30

### Added

- 增加关于 RT 重建和释放的安全检查

## [0.9.1] - 2023-03-24

### Fixed

- 修复 RenderEventMgr 反复注销回调的问题

## [0.9.0] - 2023-03-24

### Added

- 增加 RenderEventMgr 控制渲染相关事件的顺序
- 为 RenderTextureWrapper 增加宽/高参数

### Fixed

- 修复 Gfx Helper 相关函数在 Editor 中执行顺序问题


## [0.8.0] - 2023-03-17

### Added

- 为 double 增加拓展类型，可以将纳秒转换为微秒
- 为 string 增加拓展类型，可以将 string 写入到特定文件中

## [0.7.2] - 2023-02-22

### Added

- 新增操作 AndroidManifest.xml 文件

### Changed

## [0.7.1] - 2023-02-21

### Changed

- 删除函数调用间无用的 in parameter
- 命名规范化以符合标准

### Added

- 增加部分拓展函数单元测试

## [0.7.0] - 2023-01-09

### Added

- 增加 BinaryReader/Writer 拓展

### Changed

- 将 RenderTexture 转换为 Texture2D 的函数 public 化

## [0.6.4] - 2022-11-03

### Added

- 添加 CopyTextureRect 方法，为 MRC 在一张纹理上画前景、前景透明通道、背景做支持

## [0.5.4] - 2022-11-03

### Fixed

- 修复 Array.ForEach 对尺寸为 0 的数组处理异常

## [0.5.3] - 2022-09-05

### Fixed

- 修复应用暂停和恢复时对 RT 的重绘操作导致透明部分有杂色的问题

## [0.5.2] - 2022-08-19

### Added

- 为 RenderTextureWrapper 增加使用 RenderTexture 的构造函数重载。当外部调用者需要使用较为复杂的 RT 时可通过该重载调用。
- 在 Singleton 相关的类中增加 createdInstance，该 instance 变量在访问时并不会创建 instance

### Changed

- 修改 MonoBehaviorSingleton 中部分变量的命名

## [0.5.1] - 2022-06-21

### Fixed

- 修复 RamLog 偶现 Crash 问题

## [0.5.0-preview.0] - 2022-06-10

### Added

- 为 Generic Enumerable 类型的 Object 增加拓展
- 为 String 增加关于 MD5 的拓展函数

### Fixed

- 修复当任意目标是 Null 时，ReflectionEqual 失败的问题

## [0.4.1] - 2022-06-17

### Fixed

- 修复 unity 2019 对 c# 新语法不支持编译失败的问题

## [0.4.0] - 2022-05-12

### Added

- 增加 RenderTextureWrapper 用以封装 RT 被重新创建时的操作

## [0.3.0] - 2022-05-12

### Changed

- 正式发布 0.3.0

## [0.3.0-preview.0] - 2022-05-12

### Added

- 为 Array 增加拓展，如 ForEach 和 IndexForEach
- 增加 YLog

## [0.2.1-preview.0] - 2022-05-10

### Fixed

- 修复 Singleton 单例在 2019 中编译失败的问题

## [0.2.0] - 2022-04-16

### Changed

- 0.2.0 正式发布

## [0.2.0-preview.1] - 2022-04-16

### Added

- Copy/Write Texture 支持 Texture2DArray 格式

## [0.2.0-preview.0] - 2022-04-13

### Changed

- 使用 Native RenderEvent 替代 RenderEventAndData

## [0.1.20-preview.0] - 2022-4-08

### Added

- 增加 GfxHelper 封装渲染线程操作
- 增加 Color / Color 32 相关拓展函数
- 增加关于 GfxHelper 和部分拓展函数 的单元测试

### Removed

- 删除 ExcludeFromDocs Attribute，可直接使用 UnityEngine.Internal 中的 Attribute 替代

## [0.1.19] - 2022-3-30

### Added

- 添加扩展函数

## [0.1.18] - 2022-2-21

### Fixed

- PringPackagesVersion 的拼写错误。

## [0.1.17] - 2022-2-21

### Added

- 将 PackageVersion 拆分为多个脚本，增加获取和打印指定 package 版本信息的函数。

### Fixed

- 编辑器下运行崩溃

## [0.1.16] - 2022-2-19

### Added

- 版本号存储打印支持列表
- 为 RenderTexture 增加 GetPixel 拓展函数

## [0.1.15] - 2022-2-19

### Added

- 添加运行时打印版本号功能

## [0.1.14] - 2022-2-14

### Added

- 添加 Byte[]、Enum、Collection 扩展方法

## [0.1.13] - 2022-1-05

### Removed

- 删除 localization 相关文件。

# Changelog

## [0.1.12] - 2022-1-05

### Added

- 修改单例类的创建实例方式为通过 new() ,因为通过反射创建打包在安卓不适用。

# Changelog

## [0.1.11] - 2022-1-04

### Added

- 修改单例类的创建实例方式为通过反射创建

# Changelog

## [0.1.10] - 2021-12-16

### fixed

- 打包应用的时候，ConditionalDisplayModifierAttribute.conditionMet 和 ColorModifierAttribute.backupGUIColor 报警告，变量定义了但未使用

## [0.1.9] - 2021-12-01

### Added

- 为 RectTransform 增加 RayIntersect 拓展函数

## [0.1.8] - 2021-11-29

### Added

- 增加 L10nTextMgr.GetFont 可根据 localization.json id:Font_Home 加载字体

## [0.1.7] - 2021-11-04

### Added

- 增加 Localization Demo 场景
- 为 Localization 模块增加注释
- 为本地化模块增加 Manual 文档

### Changed

- 将原 Document 文件夹重命名为 Documentation
- 根据 Android Locale Config 修改 LangPack 中成员变量的命名

## [0.1.6] - 2021-11-03

### Added

- 增加 Text Localization 模块

## [0.1.5] - 2021-06-15

### Added

- 增加 Overdraw Monitor，可以在 URP 中查看 Overdraw 的情况

### Fixed

- 修复因空文件夹产生的 .meta 文件导致的 Warning 消息

## [0.1.4] - 2021-04-22

### Added

- 增加 Singleton 单例基类
- 增加 ExcludeFromDocs Attribute，用来让特定对象不会产生文档
- 初始化文档文件夹

## [0.1.3] - 2021-03-30

### Fixed

- 修复因为缺少 ChangeLog 对应的 meta 文件导致无法编译的问题

## [0.1.2] - 2021-03-29

### Added

- 增加 Change log
- 增加 `NotifiableList` 容器，该容器可以在元素数量发生变化时，以及元素内容发生改变时，分别通过 `onCollectionChanged` 和 `onElementsPropertyChanged` 接口通知观察者
