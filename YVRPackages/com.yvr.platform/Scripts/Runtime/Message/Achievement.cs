using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YVR.Platform
{
    /// <summary>
    /// Encapsulate functions of achievement request
    /// </summary>
    public static class Achievement
    {
        /// <summary>
        /// Add achievement count progress
        /// </summary>
        /// <param name="achievementName">Achievement name</param>
        /// <param name="count">Add count</param>
        /// <returns></returns>
        public static YVRRequest AchievementAddCount(string achievementName, long count)
        {
            if (!YVRPlatform.IsInitialized)
                return null;

            return new YVRRequest(YVRPlatform.YVR_AchievementUpdate_AddCount(achievementName, count));
        }

        /// <summary>
        /// Add achievement fields progress
        /// </summary>
        /// <param name="achievementName">Achievement name</param>
        /// <param name="fields">Fields string</param>
        /// <returns></returns>
        public static YVRRequest AchievementAddFields(string achievementName, string fields)
        {
            if (!YVRPlatform.IsInitialized)
                return null;

            return new YVRRequest(YVRPlatform.YVR_AchievementUpdate_AddFields(achievementName, fields));
        }

        /// <summary>
        /// Unlock this achievement
        /// </summary>
        /// <param name="achievementName">Achievement name</param>
        /// <returns></returns>
        public static YVRRequest UnlockAchievement(string achievementName)
        {
            if (!YVRPlatform.IsInitialized)
                return null;

            return new YVRRequest(YVRPlatform.YVR_AchievementUpdate_UnlockAchievement(achievementName));
        }

        /// <summary>
        /// Get definitions of all achievements
        /// </summary>
        /// <returns></returns>
        public static YVRRequest<AchievementDefinitionList> GetAllDefinitions()
        {
            if (!YVRPlatform.IsInitialized)
                return null;

            return new YVRRequest<AchievementDefinitionList>(YVRPlatform.YVR_AchievementDefinition_GetAllDefinitions());
        }

        /// <summary>
        /// Get definitions of designated achievements
        /// </summary>
        /// <param name="names">Designated achievement names</param>
        /// <returns></returns>
        public static YVRRequest<AchievementDefinitionList> GetDefinitionByName(string[] names)
        {
            if (!YVRPlatform.IsInitialized)
                return null;

            return new YVRRequest<AchievementDefinitionList>(YVRPlatform.YVR_AchievementDefinition_GetDefinitionByName(names));
        }

        /// <summary>
        ///  Get progress of all achievements
        /// </summary>
        /// <returns></returns>
        public static YVRRequest<AchievementProgressList> GetAllProgress()
        {
            if (!YVRPlatform.IsInitialized)
                return null;

            return new YVRRequest<AchievementProgressList>(YVRPlatform.YVR_AchievementProgress_GetAllProgress());
        }

        /// <summary>
        /// Get progress of designated achievements
        /// </summary>
        /// <param name="names">Designated achievement names</param>
        /// <returns></returns>
        public static YVRRequest<AchievementProgressList> GetProgressByName(string[] names)
        {
            if (!YVRPlatform.IsInitialized)
                return null;

            return new YVRRequest<AchievementProgressList>(YVRPlatform.YVR_AchievementProgress_GetProgressByName(names));
        }
    }
}
