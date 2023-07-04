# ManifestTagInfo

存储有关清单标记的信息，包括其节点路径、标记名称、属性名称、属性值和任何其他属性。

## nodePath

需要操作的目标节点路径，例如： `/manifest/application/activity/intent-filter` ，`/manifest/application`

## tag

清单标记的名称， 例如： `meta-data` ， `uses-feature` ， `uses-permission`

## attrName

清单标记的属性的名称, 例如： `name` ， `resource` ，`value`。一般都是固定的 `name`

## attrValue

清单标记的属性值, 例如： `com.yvr.application.mode`

## attrs

清单标记的附加属性数组，这里必须是成对的 (key ,value)数组， 例如： `{"value","vr_only"}`

## required

指示是否需要此清单标记。

## modifyIfFound

指示如果找到清单标记，是否修改该标记。
