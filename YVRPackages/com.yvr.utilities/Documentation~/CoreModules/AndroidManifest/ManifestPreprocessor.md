# ManifestPreprocessor

一个静态类，包含以[ManifestTagInfo](./AndroidManifest/ManifestTagInfo.md)的信息修补 `AndroidManifest.xml` 文件的方法。

## PatchAndroidManifest

将给定的 `manifestTagInfos` 应用于指定的 `AndroidManifest.xml` 文件。

- `manifestTagInfos` : 要应用于 `AndroidManifest.xml` 文件的 ManifestTagInfos 列表。
- `sourceFile` : 修改依据的源 `AndroidManifest.xml` 文件的路径。
- `destinationFile` : 修改后保存目标 `AndroidManifest.xml` 文件的路径。如果为空，将覆盖 `sourceFile`。
