# Overview

AndroidManifest 模块主要是为了向 `AndroidManifest.mxl` 文件中添加和移除标签属性功能。

该模块主要分为如下部分：

1. [ManifestTagInfo](./AndroidManifest/ManifestTagInfo.md): 存储有关清单标记的信息，包括其节点路径、标记名称、属性名称、属性值和任何其他属性。
2. [ManifestPreprocessor](./AndroidManifest/ManifestPreprocessor.md): 一个静态类，包含以[ManifestTagInfo](./AndroidManifest/ManifestTagInfo.md)的信息修补 `AndroidManifest.xml` 文件的方法。
