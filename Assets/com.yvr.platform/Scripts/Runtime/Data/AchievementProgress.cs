using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YVR.Platform
{
    /// <summary>
    /// Encapsulate properties of achievement progress data
    /// </summary>
    public class AchievementProgress
    {
        /// <summary>
        /// ID of achievement
        /// </summary>
        public readonly int id;

        private readonly AndroidJavaObject definition;
        /// <summary>
        /// Api name of achievement
        /// </summary>
        public readonly string apiName;
        /// <summary>
        /// target of achievement
        /// </summary>
        public readonly int target;
        /// <summary>
        /// Count progress of achievement
        /// </summary>
        public readonly int countProgress;
        /// <summary>
        /// Bitfield progress of achievement
        /// </summary>
        public readonly string bitfieldProgress;
        /// <summary>
        /// Is this achievement unlocked
        /// </summary>
        public readonly bool isUnlocked;
        /// <summary>
        /// Time when this achievement unlocked
        /// </summary>
        public readonly long unlockTime;

        public AchievementProgress(AndroidJavaObject obj)
        {
            this.id = YVRPlatform.YVR_AchievementProgress_GetIdFromElementOfAchievementProgress(obj);
            this.definition = YVRPlatform.YVR_AchievementProgress_GetDefinitionFromElementOfAchievementProgress(obj);
            this.apiName = YVRPlatform.YVR_AchievementProgress_GetNameFromDefinitionOfAchievementProgress(definition);
            this.target = YVRPlatform.YVR_AchievementProgress_GetTargetFromDefinitionOfAchievementProgress(definition);
            this.countProgress = YVRPlatform.YVR_AchievementProgress_GetCountProgressFromElementOfAchievementProgress(obj);
            this.bitfieldProgress = YVRPlatform.YVR_AchievementProgress_GetBitfieldProgressFromElementOfAchievementProgress(obj);
            this.isUnlocked = YVRPlatform.YVR_AchievementProgress_IsUnlockedFromElementOfAchievementProgress(obj);
            this.unlockTime = YVRPlatform.YVR_AchievementProgress_GetUnlockTimeFromElementOfAchievementProgress(obj);
        }

        public override string ToString()
        {
            System.Text.StringBuilder str = new System.Text.StringBuilder();
            str.Append(string.Format("id:【{0}】,\n\r", id));
            str.Append(string.Format("api name:【{0}】,\n\r", apiName ?? "null"));
            str.Append(string.Format("target:【{0}】,\n\r", target));
            str.Append(string.Format("countProgress:【{0}】,\n\r", countProgress));
            str.Append(string.Format("bitfieldProgress:【{0}】,\n\r", bitfieldProgress ?? "null"));
            str.Append(string.Format("isUnlocked:【{0}】,\n\r", isUnlocked));
            str.Append(string.Format("unlockTime:【{0}】,\n\r", unlockTime));
            return str.ToString();
        }
    }

    /// <summary>
    /// The storage of achievement progress data
    /// </summary>
    public class AchievementProgressList : DeserializableList<AchievementProgress>
    {
        public AchievementProgressList(AndroidJavaObject obj)
        {
            int count = YVRPlatform.YVR_AchievementProgress_GetSizeOfAchievementProgress(obj);

            data = new List<AchievementProgress>(count);

            for (int i = 0; i < count; i++)
            {
                data.Add(new AchievementProgress(YVRPlatform.YVR_AchievementProgress_GetElementOfAchievementProgress(obj, i)));
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