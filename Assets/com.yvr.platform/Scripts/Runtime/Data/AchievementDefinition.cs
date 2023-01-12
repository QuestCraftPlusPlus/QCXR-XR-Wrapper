using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YVR.Platform
{
    /// <summary>
    /// Encapsulate properties of achievement definition data
    /// </summary>
    public class AchievementDefinition
    {
        /// <summary>
        /// ID of achievement
        /// </summary>
        public readonly int id;
        /// <summary>
        /// Api name of achievement
        /// </summary>
        public readonly string apiName;
        /// <summary>
        /// Type of achievement
        /// </summary>
        public readonly int type;
        /// <summary>
        /// Policy of achievement
        /// </summary>
        public readonly int entryWritePolicy;
        /// <summary>
        /// Target of achievement
        /// </summary>
        public readonly int target;
        /// <summary>
        /// Bit field length of achievement
        /// </summary>
        public readonly int bitfieldLength;
        /// <summary>
        /// Is this achievement archived
        /// </summary>
        public readonly bool isArchived;
        /// <summary>
        /// Title of achievement
        /// </summary>
        public readonly string title;
        /// <summary>
        /// Description of achievement
        /// </summary>
        public readonly string description;
        /// <summary>
        /// Unlock description of achievement
        /// </summary>
        public readonly string unlockedDescription;
        /// <summary>
        /// Is this achievement secret
        /// </summary>
        public readonly bool isSecret;
        /// <summary>
        /// Locked image url of achievement
        /// </summary>
        public readonly string lockedImageFile;
        /// <summary>
        /// Unlocked image url of achievement
        /// </summary>
        public readonly string unlockedImageFile;
        /// <summary>
        /// Time when this achievement created
        /// </summary>
        public readonly long createTime;
        /// <summary>
        /// Time when this achievement updated
        /// </summary>
        public readonly long updateTime;

        public AchievementDefinition(AndroidJavaObject obj)
        {
            this.id = YVRPlatform.YVR_AchievementDefinition_GetIdFromElementOfDefinitions(obj);
            this.apiName = YVRPlatform.YVR_AchievementDefinition_GetApiNameFromElementOfDefinitions(obj);
            this.type = YVRPlatform.YVR_AchievementDefinition_GetTypeFromElementOfDefinitions(obj);
            this.entryWritePolicy = YVRPlatform.YVR_AchievementDefinition_GetPolicyFromElementOfDefinitions(obj);
            this.target = YVRPlatform.YVR_AchievementDefinition_GetTargetFromElementOfDefinitions(obj);
            this.bitfieldLength = YVRPlatform.YVR_AchievementDefinition_GetBitfieldLengthFromElementOfDefinitions(obj);
            this.isArchived = YVRPlatform.YVR_AchievementDefinition_GetIsAchievedFromElementOfDefinitions(obj);
            this.title = YVRPlatform.YVR_AchievementDefinition_GetTitleFromElementOfDefinitions(obj);
            this.description = YVRPlatform.YVR_AchievementDefinition_GetDescriptionFromElementOfDefinitions(obj);
            this.unlockedDescription = YVRPlatform.YVR_AchievementDefinition_GetUnlockedDescriptionFromElementOfDefinitions(obj);
            this.isSecret = YVRPlatform.YVR_AchievementDefinition_GetIsSecretFromElementOfDefinitions(obj);
            this.lockedImageFile = YVRPlatform.YVR_AchievementDefinition_GetLockedImageFromElementOfDefinitions(obj);
            this.unlockedImageFile = YVRPlatform.YVR_AchievementDefinition_GetUnlockedImageFromElementOfDefinitions(obj);
            this.createTime = YVRPlatform.YVR_AchievementDefinition_GetCreatedTimeFromElementOfDefinitions(obj);
            this.updateTime = YVRPlatform.YVR_AchievementDefinition_GetUpdateTimeFromElementOfDefinitions(obj);
        }

        public override string ToString()
        {
            System.Text.StringBuilder str = new System.Text.StringBuilder();
            str.Append(string.Format("id:【{0}】,\n\r", id));
            str.Append(string.Format("api name:【{0}】,\n\r", apiName ?? "null"));
            str.Append(string.Format("type:【{0}】,\n\r", type));
            str.Append(string.Format("entryWritePolicy:【{0}】,\n\r", entryWritePolicy));
            str.Append(string.Format("target:【{0}】,\n\r", target));
            str.Append(string.Format("bitfieldLength:【{0}】,\n\r", bitfieldLength));
            str.Append(string.Format("isArchived:【{0}】,\n\r", isArchived));
            str.Append(string.Format("title:【{0}】,\n\r", title ?? "null"));
            str.Append(string.Format("description:【{0}】,\n\r", description ?? "null"));
            str.Append(string.Format("unlockedDescription:【{0}】,\n\r", unlockedDescription ?? "null"));
            str.Append(string.Format("isSecret:【{0}】,\n\r", isSecret));
            str.Append(string.Format("lockedImageFile:【{0}】,\n\r", lockedImageFile ?? "null"));
            str.Append(string.Format("unlockedImageFile:【{0}】,\n\r", unlockedImageFile ?? "null"));
            str.Append(string.Format("createTime:【{0}】,\n\r", createTime));
            str.Append(string.Format("updateTime:【{0}】,\n\r", updateTime));
            return str.ToString();
        }
    }

    /// <summary>
    /// The storage of achievement definition data
    /// </summary>
    public class AchievementDefinitionList : DeserializableList<AchievementDefinition>
    {
        public AchievementDefinitionList(AndroidJavaObject obj)
        {
            int count = YVRPlatform.YVR_AchievementDefinition_GetSizeOfAllDefinitions(obj);

            data = new List<AchievementDefinition>(count);

            for (int i = 0; i < count; i++)
            {
                data.Add(new AchievementDefinition(YVRPlatform.YVR_AchievementDefinition_GetElementOfDefinitions(obj, i)));
            }
        }

        public override string ToString()
        {
            System.Text.StringBuilder str = new System.Text.StringBuilder();
            foreach (var item in data)
                str.Append(item.ToString() + "\n\r");
            return str.ToString();
        }
    }
}