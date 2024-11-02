/*******************************************************************************
Copyright ? 2015-2022 Pico Technology Co., Ltd.All rights reserved.  

NOTICE All information contained herein is, and remains the property of 
Pico Technology Co., Ltd. The intellectual and technical concepts 
contained hererin are proprietary to Pico Technology Co., Ltd. and may be 
covered by patents, patents in process, and are protected by trade secret or 
copyright law. Dissemination of this information or reproduction of this 
material is strictly forbidden unless prior written permission is obtained from
Pico Technology Co., Ltd. 
*******************************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Unity.XR.OpenXR.Features.PICOSupport;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.OpenXR;
using UnityEngine.XR.OpenXR.Features;

namespace Unity.XR.PXR
{
    public class PXR_MixedReality
    {
         private const string TAG = "[PXR_MixedReality]";
        /// <summary>
        /// Starts the Spatial Anchor or Scene Capture feature by starting the corresponding sense data provider.
        /// </summary>
        /// <param name="type">Specifies the type of sense data provider to start: `SpatialAnchor` or `SceneCapture`.</param>
        /// <param name="token">Propagates notification that operations should be canceled.</param>
        /// <returns>Refer to the `PxrResult` enumeration for details.</returns>
        public static async Task<PxrResult> StartSenseDataProvider(PxrSenseDataProviderType type, CancellationToken token = default)
        {
            var isFeature = false;

            if (type==PxrSenseDataProviderType.SpatialAnchor)
            {
                isFeature = PICOSpatialAnchor.isEnable;
            }
            else if (type==PxrSenseDataProviderType.SceneCapture)
            {
                isFeature = PICOSceneCapture.isEnable;
            }

            if (!isFeature)
            {
                return await Task.Run(() => { return PxrResult.ERROR_EXTENSION_NOT_ENABLED; }, token);
            }
            
            int isPermissionMR = -1;
            PXR_PermissionRequest.RequestUserPermissionMR(d =>
            {
                isPermissionMR = -2;
                Debug.LogError($"request permission result is {d}");
            }, async g => { isPermissionMR = 0; }, dda =>
            {
                isPermissionMR = -2;
                Debug.LogError($"request permission result is {dda}");
            });

            while (isPermissionMR==-1)
            {
                await Task.Delay(200);
            }
            if (isPermissionMR != 0)
            {
                return await Task.Run(() => { return PxrResult.ERROR_PERMISSION_INSUFFICIENT; }, token);
            }
            return await Task.Run(() =>
            {
                var providerHandle = PXR_Plugin.MixedReality.UPxr_GetSenseDataProviderHandle(type);
                var startResult =
                    PXR_Plugin.MixedReality.UPxr_StartSenseDataProviderAsync(providerHandle, out var future);
                if (startResult == PxrResult.SUCCESS)
                {
                    while (true)
                    {
                        var pollResult = PXR_Plugin.MixedReality.UPxr_PollFuture(future, out var futureState);
                        if (pollResult == PxrResult.SUCCESS)
                        {
                            if (futureState == PxrFutureState.Ready)
                            {
                                var completeResult =
                                    PXR_Plugin.MixedReality.UPxr_StartSenseDataProviderComplete(future,
                                        out var completion);
                                if (completeResult == PxrResult.SUCCESS)
                                {
                                    return completion.futureResult;
                                }
                                else
                                {
                                    return completeResult;
                                }
                            }
                        }
                        else
                        {
                            return pollResult;
                        }
                    }
                }
                else
                {
                    return startResult;
                }
            }, token);
        }
        /// <summary>
        /// Gets the state of the sense data provider.
        /// </summary>
        /// <param name="type">Specifies the type of sense data provider to get state for: `SpatialAnchor` or `SceneCapture`.</param>
        /// <param name="state">Returns the state of the specified sense data provider.</param>
        /// <returns>Refer to the `PxrResult` enumeration for details.</returns>
        public static PxrResult GetSenseDataProviderState(PxrSenseDataProviderType type,out PxrSenseDataProviderState state)
        {
            var providerHandle = PXR_Plugin.MixedReality.UPxr_GetSenseDataProviderHandle(type);
            return PXR_Plugin.MixedReality.UPxr_GetSenseDataProviderState(providerHandle, out state);
        }
        /// <summary>
        /// Stops the Spatial Anchor or Scene Capture feature by stopping the corresponding sense data provider.
        /// </summary>
        /// <param name="type">Specifies the sense data provider to stop: `SpatialAnchor` or `SceneCapture`.</param>
        /// <returns>Refer to the `PxrResult` enumeration for details.</returns>
        public static PxrResult StopSenseDataProvider(PxrSenseDataProviderType type)
        {
            var providerHandle = PXR_Plugin.MixedReality.UPxr_GetSenseDataProviderHandle(type);
            var stopResult = PXR_Plugin.MixedReality.UPxr_StopSenseDataProvide(providerHandle);
            return stopResult;
        }
        /// <summary>
        /// Creates a spatial anchor in the app's memory.
        /// </summary>
        /// <param name="position">Specifies the position of the spatial anchor.</param>
        /// <param name="rotation">Specifies the rotation of the spatial anchor.</param>
        /// <param name="token">Propagates notification that operations should be canceled.</param>
        /// <returns>Refer to the `PxrResult` enumeration for details. In addition, the handle and UUID of the spatial anchor created are returned.</returns>

        public static async Task<(PxrResult result, ulong anchorHandle, Guid uuid)> CreateSpatialAnchorAsync(
            Vector3 position, Quaternion rotation, CancellationToken token = default)
        {
              return await Task.Run(() =>
                {
                    var createResult = PXR_Plugin.MixedReality.UPxr_CreateSpatialAnchorAsync(
                        PXR_Plugin.MixedReality.UPxr_GetSenseDataProviderHandle(PxrSenseDataProviderType.SpatialAnchor), position, rotation, out var future);
                    if (createResult == PxrResult.SUCCESS)
                    {
                        while (true)
                        {
                            var pollResult = PXR_Plugin.MixedReality.UPxr_PollFuture(future, out var futureState);
                            if (pollResult == PxrResult.SUCCESS)
                            {
                                if (futureState == PxrFutureState.Ready)
                                {
                                    var completeResult = PXR_Plugin.MixedReality.UPxr_CreateSpatialAnchorComplete(
                                        PXR_Plugin.MixedReality.UPxr_GetSenseDataProviderHandle(PxrSenseDataProviderType.SpatialAnchor), future,
                                        out var completion);
                                    if (completeResult == PxrResult.SUCCESS)
                                    {
                                        byte[] byteArray = new byte[16];
                                       // Debug.Log($"request anchor result is {completeResult}");
                                        BitConverter.GetBytes(completion.uuid.value0).CopyTo(byteArray, 0);
                                        BitConverter.GetBytes(completion.uuid.value1).CopyTo(byteArray, 8);
                                        //Debug.Log($"xrCreateSpatialAnchorComplete byteArray result is {string.Join(", ", byteArray)}");
                                        var uuid = new Guid(byteArray);
                                        return (completion.futureResult, completion.anchorHandle, uuid);
                                    }
                                    else
                                    {
                                        return (completeResult, ulong.MinValue, Guid.Empty);
                                    }
                                }
                            }
                            else
                            {
                                return (pollResult, ulong.MinValue, Guid.Empty);
                            }
                        }
                    }
                    else
                    {
                        return (createResult, ulong.MinValue, Guid.Empty);
                    }
                }, token);
        }
        /// <summary>
        /// Persists a spatial anchor to the PICO device's local disk.
        /// </summary>
        /// <param name="anchorHandle">Specifies the handle of the spatial anchor to persist.</param>
        /// <param name="token">Propagates notification that operations should be canceled.</param>
        /// <returns>Refer to the `PxrResult` enumeration for details.</returns>
        public static async Task<PxrResult> PersistSpatialAnchorAsync(ulong anchorHandle,CancellationToken token = default)
        {
            return await Task.Run(() =>
            {
                var persistResult = PXR_Plugin.MixedReality.UPxr_PersistSpatialAnchorAsync(
                    PXR_Plugin.MixedReality.UPxr_GetSenseDataProviderHandle(PxrSenseDataProviderType.SpatialAnchor), anchorHandle, out var future);
                if (persistResult == PxrResult.SUCCESS)
                {
                    while (true)
                    {
                        var pollResult = PXR_Plugin.MixedReality.UPxr_PollFuture(future, out var futureState);
                        if (pollResult == PxrResult.SUCCESS)
                        {
                            if (futureState == PxrFutureState.Ready)
                            {
                                var completeResult = PXR_Plugin.MixedReality.UPxr_PersistSpatialAnchorComplete(
                                    PXR_Plugin.MixedReality.UPxr_GetSenseDataProviderHandle(PxrSenseDataProviderType.SpatialAnchor), future,
                                    out var completion);
                                if (completeResult == PxrResult.SUCCESS)
                                {
                                    return completion.futureResult;
                                }
                                else
                                {
                                    return completeResult;
                                }
                            }
                        }
                        else
                        {
                            return pollResult;
                        }
                    }
                }
                else
                {
                    return persistResult;
                }
            }, token);
        }
        /// <summary>
        /// Destroys an anchor in the app's memory.
        /// </summary>
        /// <param name="anchorHandle">Specifies the handle of the anchor to destroy. If you specify the handle of a scene anchor, the "Invalid handle" prompt will appear.</param>
        /// <returns>Refer to the `PxrResult` enumeration for details.</returns>
        public static PxrResult DestroyAnchor(ulong anchorHandle)
        {
            if (PXR_Plugin.MixedReality.SceneAnchorData.ContainsKey(anchorHandle))
            {
                return PxrResult.ERROR_HANDLE_INVALID;
            }
            else
            {
                return PXR_Plugin.MixedReality.UPxr_DestroyAnchor(anchorHandle);
            }
        }
        /// <summary>
        /// Loads spatial anchor(s) from the device's local storage and the app's memory.
        /// </summary>
        /// <param name="uuids">Specifies the UUID(s) of the spatial anchor(s) you want to load. If you do not pass any UUID, all spatial anchors will be loaded.</param>
        /// <param name="token">Propagates notification that operations should be canceled.</param>
        /// <returns>Refer to the `PxrResult` enumeration for details. In addition, a list of the handles of the loaded anchors is returned.</returns>
        public static async Task<(PxrResult result, List<ulong> anchorHandleList)> QuerySpatialAnchorAsync(Guid[] uuids = null, CancellationToken token = default)
        {
               return await Task.Run(() =>
                {
                    if (uuids == null)
                    {
                        uuids = Array.Empty<Guid>();
                    }
                    var queryResult = PXR_Plugin.MixedReality.UPxr_QuerySenseDataByUuidAsync(uuids, out var future);
                    if (queryResult == PxrResult.SUCCESS)
                    {
                        while (true)
                        {
                            var pollResult = PXR_Plugin.MixedReality.UPxr_PollFuture(future, out var futureState);
                            if (pollResult == PxrResult.SUCCESS)
                            {
                                if (futureState == PxrFutureState.Ready)
                                {
                                    var completeResult = PXR_Plugin.MixedReality.UPxr_QuerySenseDataComplete(
                                        PXR_Plugin.MixedReality.UPxr_GetSenseDataProviderHandle(PxrSenseDataProviderType.SpatialAnchor), future,
                                        out var completion);
                                    if (completeResult == PxrResult.SUCCESS)
                                    {
                                        if (completion.futureResult == PxrResult.SUCCESS)
                                        {
                                            var getResult = PXR_Plugin.MixedReality.UPxr_GetQueriedSenseData(
                                                PXR_Plugin.MixedReality.UPxr_GetSenseDataProviderHandle(PxrSenseDataProviderType.SpatialAnchor),
                                                completion.snapshotHandle, out var entityInfos);
                                            if (getResult == PxrResult.SUCCESS)
                                            {
                                                var anchorHandleList = new List<ulong>();
                                                foreach (var e in entityInfos)
                                                {
                                                    var retrieveResult = PXR_Plugin.MixedReality.UPxr_RetrieveSpatialEntityAnchor(completion.snapshotHandle,
                                                        e.spatialEntity, out var anchorHandle);
                                                    if (retrieveResult == PxrResult.SUCCESS)
                                                    {
                                                        anchorHandleList.Add(anchorHandle);
                                                    }
                                                }
                                                PXR_Plugin.MixedReality.UPxr_DestroySenseDataQueryResult(completion.snapshotHandle);
                                                return (getResult, anchorHandleList);
                                            }
                                            else
                                            {
                                                PXR_Plugin.MixedReality.UPxr_DestroySenseDataQueryResult(completion.snapshotHandle);
                                                return (getResult, new List<ulong>());
                                            }
                                        }
                                        else
                                        {
                                            return (completion.futureResult, new List<ulong>());
                                        }
                                    }
                                    else
                                    {
                                        return (completeResult, new List<ulong>());
                                    }
                                }
                            }
                            else
                            {
                                return (pollResult, new List<ulong>());
                            }
                        }
                    }
                    else
                    {
                        return (queryResult, new List<ulong>());
                    }
                }, token);
        }
        /// <summary>
        /// Locates an anchor by getting its real-time position and rotation.
        /// </summary>
        /// <param name="anchorHandle">Specifies the handle of the anchor to locate.</param>
        /// <param name="position">Returns the position of the anchor.</param>
        /// <param name="rotation">Returns the rotation of the anchor.</param>
        /// <returns>Refer to the `PxrResult` enumeration for details.</returns>
        public static PxrResult LocateAnchor(ulong anchorHandle, out Vector3 position, out Quaternion rotation)
        {
            if (PXR_Plugin.MixedReality.SceneAnchorData.ContainsKey(anchorHandle))
            {
                if (PXR_Plugin.MixedReality.SceneAnchorData.TryGetValue(anchorHandle, out var data))
                {
                    position = data.position;
                    rotation = data.rotation;
                    return PxrResult.SUCCESS;
                }
                else
                {
                    position = Vector3.zero;
                    rotation = Quaternion.identity;
                    return PxrResult.ERROR_HANDLE_INVALID;
                }
            }
            else
            {
                return PXR_Plugin.MixedReality.UPxr_LocateAnchor(anchorHandle, out position, out rotation);
            }
        }
        /// <summary>
        /// Gets the UUID of an anchor.
        /// </summary>
        /// <param name="anchorHandle">Specifies the handle of the anchor to get UUID for.</param>
        /// <param name="uuid">Returns the UUID of the specified anchor.</param>
        /// <returns>Refer to the `PxrResult` enumeration for details.</returns>
        public static PxrResult GetAnchorUuid(ulong anchorHandle, out Guid uuid)
        {
            if (PXR_Plugin.MixedReality.SceneAnchorData.ContainsKey(anchorHandle))
            {
                if (PXR_Plugin.MixedReality.SceneAnchorData.TryGetValue(anchorHandle, out var data))
                {
                    uuid = data.uuid;
                    return PxrResult.SUCCESS;
                }
                else
                {
                    uuid = Guid.Empty;
                    return PxrResult.ERROR_HANDLE_INVALID;
                }
            }
            else
            {
                return PXR_Plugin.MixedReality.UPxr_GetAnchorUuid(anchorHandle, out uuid);
            }
        }
        /// <summary>
        /// Unpersists a spatial anchor from the PICO device's local disk.
        /// </summary>
        /// <param name="anchorHandle">Specifies the handle of the spatial anchor to unpersist.</param>
        /// <param name="token">Propagates notification that operations should be canceled.</param>
        /// <returns>Refer to the `PxrResult` enumeration for details.</returns>
        public static async Task<PxrResult> UnPersistSpatialAnchorAsync(ulong anchorHandle, CancellationToken token = default)
        {
            return await Task.Run(() =>
            {
                var unPersistResult = PXR_Plugin.MixedReality.UPxr_UnPersistSpatialAnchorAsync(
                    PXR_Plugin.MixedReality.UPxr_GetSenseDataProviderHandle(PxrSenseDataProviderType.SpatialAnchor), anchorHandle, out var future);
                if (unPersistResult == PxrResult.SUCCESS)
                {
                    while (true)
                    {
                        var pollResult = PXR_Plugin.MixedReality.UPxr_PollFuture(future, out var futureState);
                        if (pollResult == PxrResult.SUCCESS)
                        {
                            if (futureState == PxrFutureState.Ready)
                            {
                                var completeResult = PXR_Plugin.MixedReality.UPxr_UnPersistSpatialAnchorComplete(
                                    PXR_Plugin.MixedReality.UPxr_GetSenseDataProviderHandle(PxrSenseDataProviderType.SpatialAnchor), future,
                                    out var completion);
                                if (completeResult == PxrResult.SUCCESS)
                                {
                                    return completion.futureResult;
                                }
                                else
                                {
                                    return completeResult;
                                }
                            }
                        }
                        else
                        {
                            return pollResult;
                        }
                    }
                }
                else
                {
                    return unPersistResult;
                }
            }, token);
        }
        /// <summary>
        /// Launches the Room Capture app to capture the current real-world scene.
        /// </summary>
        /// <param name="token">Propagates notification that operations should be canceled.</param>
        /// <returns>Refer to the `PxrResult` enumeration for details.</returns>
        public static async Task<PxrResult> StartSceneCaptureAsync(CancellationToken token = default)
        {
            return await Task.Run(() =>
            {
                var startResult = PXR_Plugin.MixedReality.UPxr_StartSceneCaptureAsync(out var future);
                if (startResult == PxrResult.SUCCESS)
                {
                    while (true)
                    {
                        var pollResult = PXR_Plugin.MixedReality.UPxr_PollFuture(future, out var futureState);
                        if (pollResult == PxrResult.SUCCESS)
                        {
                            if (futureState == PxrFutureState.Ready)
                            {
                                var completeResult = PXR_Plugin.MixedReality.UPxr_StartSceneCaptureComplete(future, out var completion);
                                if (completeResult == PxrResult.SUCCESS)
                                {
                                    return completion.futureResult;
                                }
                                else
                                {
                                    return completeResult;
                                }
                            }
                        }
                        else
                        {
                            return pollResult;
                        }
                    }
                }
                else
                {
                    return startResult;
                }
            }, token);
        }
        /// <summary>
        /// Loads scene anchors with specified semantic label(s).
        /// </summary>
        /// <param name="labels">Specifies the semantic label(s). If not specified, all scene anchors will be returned.</param>
        /// <param name="token">Propagates notification that operations should be canceled.</param>
        /// <returns>Refer to the `PxrResult` enumeration for details. In addition, a list of the handles of loaded anchors is returned.</returns>
        public static async Task<(PxrResult result, List<ulong> anchorHandleList)> QuerySceneAnchorAsync(PxrSemanticLabel[] labels = null,
            CancellationToken token = default)
        {
                 return await Task.Run(() =>
                {
                    if (labels == null)
                    {
                        labels = Array.Empty<PxrSemanticLabel>();
                    }

                    var queryResult = PXR_Plugin.MixedReality.UPxr_QuerySenseDataBySemanticAsync(labels, out var future);
                    if (queryResult == PxrResult.SUCCESS)
                    {
                        while (true)
                        {
                            var pollResult = PXR_Plugin.MixedReality.UPxr_PollFuture(future, out var futureState);
                            if (pollResult == PxrResult.SUCCESS)
                            {
                                if (futureState == PxrFutureState.Ready)
                                {
                                    var completeResult = PXR_Plugin.MixedReality.UPxr_QuerySenseDataComplete(
                                        PXR_Plugin.MixedReality.UPxr_GetSenseDataProviderHandle(PxrSenseDataProviderType.SceneCapture), future,
                                        out var completion);
                                    if (completeResult == PxrResult.SUCCESS)
                                    {
                                        if (completion.futureResult == PxrResult.SUCCESS)
                                        {
                                            var getResult = PXR_Plugin.MixedReality.UPxr_GetQueriedSenseData(
                                                PXR_Plugin.MixedReality.UPxr_GetSenseDataProviderHandle(PxrSenseDataProviderType.SceneCapture),
                                                completion.snapshotHandle, out var entityInfos);
                                            if (getResult == PxrResult.SUCCESS)
                                            {
                                                var anchorHandleList = new List<ulong>();
                                                PXR_Plugin.MixedReality.SceneAnchorData.Clear();
                                                PXR_Plugin.MixedReality.SceneAnchorData = new Dictionary<ulong, PxrSceneComponentData>();
                                                foreach (var e in entityInfos)
                                                {
                                                    byte[] byteArray = new byte[16];
                                                    BitConverter.GetBytes(e.uuid.value0).CopyTo(byteArray, 0);
                                                    BitConverter.GetBytes(e.uuid.value1).CopyTo(byteArray, 8);
                                                 
                                                    Guid guid = new Guid(byteArray);
                                                    anchorHandleList.Add(e.spatialEntity);
                                                    var sceneAnchor = new PxrSceneComponentData
                                                    {
                                                        uuid = guid
                                                    };
                                                    var result = PXR_Plugin.MixedReality.UPxr_GetSpatialEntitySemanticInfo(completion.snapshotHandle, e.spatialEntity, out var label);
                                                    if (result == PxrResult.SUCCESS)
                                                    {
                                                        sceneAnchor.label = label;
                                                    }

                                                    result = PXR_Plugin.MixedReality.UPxr_GetSpatialEntityLocationInfo(completion.snapshotHandle,
                                                        e.spatialEntity, out var position, out var rotation);
                                                    if (result == PxrResult.SUCCESS)
                                                    {
                                                        sceneAnchor.position = position;
                                                        sceneAnchor.rotation = rotation;
                                                    }

                                                    result = PXR_Plugin.MixedReality.UPxr_EnumerateSpatialEntityComponentTypes(completion.snapshotHandle,
                                                        e.spatialEntity, out var types);
                                                    if (result == PxrResult.SUCCESS)
                                                    {
                                                        sceneAnchor.types = types;
                                                    }

                                                    foreach (var t in types)
                                                    {
                                                        switch (t)
                                                        {
                                                            case PxrSceneComponentType.Box3D:
                                                                {
                                                                    result = PXR_Plugin.MixedReality.UPxr_GetSpatialEntityBox3DInfo(completion.snapshotHandle,
                                                                        e.spatialEntity, out var cPosition, out var cRotation, out var extent);
                                                                    if (result == PxrResult.SUCCESS)
                                                                    {
                                                                        sceneAnchor.box3D = new PxrSceneBox3D()
                                                                        {
                                                                            position = cPosition,
                                                                            rotation = cRotation,
                                                                            extent = extent
                                                                        };
                                                                    }
                                                                    break;
                                                                }
                                                            case PxrSceneComponentType.Box2D:
                                                                {
                                                                    result = PXR_Plugin.MixedReality.UPxr_GetSpatialEntityBox2DInfo(completion.snapshotHandle,
                                                                        e.spatialEntity, out var offset, out var extent);
                                                                    if (result == PxrResult.SUCCESS)
                                                                    {
                                                                        sceneAnchor.box2D = new PxrSceneBox2D()
                                                                        {
                                                                            offset = offset,
                                                                            extent =new XrExtent2Df(extent) 
                                                                        };
                                                                    }
                                                                    break;
                                                                }
                                                            case PxrSceneComponentType.Polygon:
                                                                {
                                                                    result = PXR_Plugin.MixedReality.UPxr_GetSpatialEntityPolygonInfo(completion.snapshotHandle,
                                                                        e.spatialEntity, out var vertices);
                                                                    if (result == PxrResult.SUCCESS)
                                                                    {
                                                                        sceneAnchor.polygon = new PxrScenePolygon()
                                                                        {
                                                                            vertices = vertices
                                                                        };
                                                                    }
                                                                    break;
                                                                }
                                                        }
                                                    }
#if UNITY_2021_1_OR_NEWER
                                                    PXR_Plugin.MixedReality.SceneAnchorData.TryAdd(e.spatialEntity, sceneAnchor);
#else
                                                    PXR_Plugin.MixedReality.SceneAnchorData.Add(e.spatialEntity, sceneAnchor);
#endif
                                                }
                                                PXR_Plugin.MixedReality.UPxr_DestroySenseDataQueryResult(completion.snapshotHandle);
                                                return (getResult, anchorHandleList);
                                            }
                                            else
                                            {
                                                PXR_Plugin.MixedReality.UPxr_DestroySenseDataQueryResult(completion.snapshotHandle);
                                                return (getResult, new List<ulong>());
                                            }
                                        }
                                        else
                                        {
                                            return (completion.futureResult, new List<ulong>());
                                        }
                                    }
                                    else
                                    {
                                        return (completeResult, new List<ulong>());
                                    }
                                }
                            }
                            else
                            {
                                return (pollResult, new List<ulong>());
                            }
                        }
                    }
                    else
                    {
                        return (queryResult, new List<ulong>());
                    }
                }, token);
        }
        /// <summary>
        /// Loads all scene anchors.
        /// </summary>
        /// <param name="token">Propagates notification that operations should be canceled.</param>
        /// <returns>Refer to the `PxrResult` enumeration for details. In addition, a list of the handles and UUIDs of all scene anchors is returned.</returns>
         public static async Task<(PxrResult result, Dictionary<ulong,Guid> anchorDictionary)> QuerySceneAnchorAsync(CancellationToken token = default)
        {
               return await Task.Run(() =>
                {
                    var queryResult = PXR_Plugin.MixedReality.UPxr_QuerySenseDataBySemanticAsync(Array.Empty<PxrSemanticLabel>(), out var future);
                    if (queryResult == PxrResult.SUCCESS)
                    {
                        while (true)
                        {
                            var pollResult = PXR_Plugin.MixedReality.UPxr_PollFuture(future, out var futureState);
                            if (pollResult == PxrResult.SUCCESS)
                            {
                                if (futureState == PxrFutureState.Ready)
                                {
                                    var completeResult = PXR_Plugin.MixedReality.UPxr_QuerySenseDataComplete(PXR_Plugin.MixedReality.UPxr_GetSenseDataProviderHandle(PxrSenseDataProviderType.SceneCapture), future, out var completion);
                                    if (completeResult == PxrResult.SUCCESS)
                                    {
                                        if (completion.futureResult == PxrResult.SUCCESS)
                                        {
                                            var getResult = PXR_Plugin.MixedReality.UPxr_GetQueriedSenseData(PXR_Plugin.MixedReality.UPxr_GetSenseDataProviderHandle(PxrSenseDataProviderType.SceneCapture), completion.snapshotHandle, out var entityInfos);
                                            if (getResult == PxrResult.SUCCESS)
                                            {
                                                var anchorDictionary = new Dictionary<ulong, Guid>();
                                                PXR_Plugin.MixedReality.SceneAnchorData.Clear();
                                                PXR_Plugin.MixedReality.SceneAnchorData = new Dictionary<ulong, PxrSceneComponentData>();
                                                foreach (var e in entityInfos)
                                                {
                                                    byte[] byteArray = new byte[16];
                                                    BitConverter.GetBytes(e.uuid.value0).CopyTo(byteArray, 0);
                                                    BitConverter.GetBytes(e.uuid.value1).CopyTo(byteArray, 8);
                                                    Guid guid = new Guid(byteArray);
                                                    anchorDictionary.Add(e.spatialEntity, guid);
                                                    var sceneAnchor = new PxrSceneComponentData();
                                                    var result = PXR_Plugin.MixedReality.UPxr_GetSpatialEntitySemanticInfo(completion.snapshotHandle, e.spatialEntity, out var label);
                                                    if (result == PxrResult.SUCCESS)
                                                    {
                                                        sceneAnchor.label = label;
                                                    }
                                                    result = PXR_Plugin.MixedReality.UPxr_GetSpatialEntityLocationInfo(completion.snapshotHandle, e.spatialEntity, out var position, out var rotation);
                                                    if (result == PxrResult.SUCCESS)
                                                    {
                                                        sceneAnchor.position = position;
                                                        sceneAnchor.rotation = rotation;
                                                    }
                                                    result = PXR_Plugin.MixedReality.UPxr_EnumerateSpatialEntityComponentTypes(completion.snapshotHandle, e.spatialEntity, out var types);
                                                    if (result == PxrResult.SUCCESS)
                                                    {
                                                        sceneAnchor.types = types;
                                                    }

                                                    foreach (var t in types)
                                                    {
                                                        switch (t)
                                                        {
                                                            case PxrSceneComponentType.Box3D:
                                                                {
                                                                    result = PXR_Plugin.MixedReality.UPxr_GetSpatialEntityBox3DInfo(completion.snapshotHandle, e.spatialEntity, out var cPosition, out var cRotation, out var extent);
                                                                    if (result == PxrResult.SUCCESS)
                                                                    {
                                                                        sceneAnchor.box3D = new PxrSceneBox3D()
                                                                        {
                                                                            position = cPosition,
                                                                            rotation = cRotation,
                                                                            extent = extent
                                                                        };
                                                                    }
                                                                    break;
                                                                }
                                                            case PxrSceneComponentType.Box2D:
                                                                {
                                                                    result = PXR_Plugin.MixedReality.UPxr_GetSpatialEntityBox2DInfo(completion.snapshotHandle, e.spatialEntity, out var offset, out var extent);
                                                                    if (result == PxrResult.SUCCESS)
                                                                    {
                                                                        sceneAnchor.box2D = new PxrSceneBox2D()
                                                                        {
                                                                            offset = offset,
                                                                            extent = new XrExtent2Df(extent) 
                                                                        };
                                                                    }
                                                                    break;
                                                                }
                                                            case PxrSceneComponentType.Polygon:
                                                                {
                                                                    result = PXR_Plugin.MixedReality.UPxr_GetSpatialEntityPolygonInfo(completion.snapshotHandle, e.spatialEntity, out var vertices);
                                                                    if (result == PxrResult.SUCCESS)
                                                                    {
                                                                        sceneAnchor.polygon = new PxrScenePolygon()
                                                                        {
                                                                            vertices = vertices
                                                                        };
                                                                    }
                                                                    break;
                                                                }
                                                        }
                                                    }
#if UNITY_2021_1_OR_NEWER
                                                    PXR_Plugin.MixedReality.SceneAnchorData.TryAdd(e.spatialEntity, sceneAnchor);
#else
                                                    PXR_Plugin.MixedReality.SceneAnchorData.Add(e.spatialEntity, sceneAnchor);
#endif
                                                }
                                                PXR_Plugin.MixedReality.UPxr_DestroySenseDataQueryResult(completion.snapshotHandle);
                                                return (getResult, anchorDictionary);
                                            }
                                            else
                                            {
                                                return (getResult, new Dictionary<ulong, Guid>());
                                            }
                                        }
                                        else
                                        {
                                            return (completion.futureResult, new Dictionary<ulong, Guid>());
                                        }
                                    }
                                    else
                                    {
                                        return (completeResult, new Dictionary<ulong, Guid>());
                                    }
                                }
                            }
                            else
                            {
                                return (pollResult, new Dictionary<ulong, Guid>());
                            }
                        }
                    }
                    else
                    {
                        return (queryResult, new Dictionary<ulong, Guid>());
                    }
                }, token);
        }
        /// <summary>
        /// Gets the component type of a scene anchor.
        /// </summary>
        /// <param name="anchorHandle">Specifies the handle of the anchor to get component type for.</param>
        /// <param name="types">Returns the component type of the specified anchor.</param>
        /// <returns>Refer to the `PxrResult` enumeration for details.</returns>
        public static PxrResult GetSceneAnchorComponentTypes(ulong anchorHandle, out PxrSceneComponentType[] types)
        {
            if (PXR_Plugin.MixedReality.SceneAnchorData.TryGetValue(anchorHandle, out var data))
            {
                types = data.types;
                return PxrResult.SUCCESS;
            }
            else
            {
                types = Array.Empty<PxrSceneComponentType>();
                return PxrResult.ERROR_HANDLE_INVALID;
            }
        }
        /// <summary>
        /// Gets the information of a 3D box object.
        /// </summary>
        /// <param name="anchorHandle">Specifies the handle of the scene anchor that the 3D box object is associated with.</param>
        /// <param name="position">Returns the position of the 3D box object relative to the center of the scene anchor.</param>
        /// <param name="rotation">Returns the rotation of the 3D box object relative to the center of the scene anchor.</param>
        /// <param name="extent">Returns the length, width, and height of the 3D box object.</param>
        /// <returns>Refer to the `PxrResult` enumeration for details.</returns>
        public static PxrResult GetSceneBox3DData(ulong anchorHandle, out Vector3 position, out Quaternion rotation, out Vector3 extent)
        {
            if (PXR_Plugin.MixedReality.SceneAnchorData.TryGetValue(anchorHandle, out var data))
            {
                if (data.types.Contains(PxrSceneComponentType.Box3D))
                {
                    position = data.box3D.position;
                    rotation = data.box3D.rotation;
                    extent = data.box3D.extent;
                    return PxrResult.SUCCESS;
                }
                else
                {
                    position = Vector3.zero;
                    rotation = Quaternion.identity;
                    extent = Vector3.zero;
                    return PxrResult.ERROR_HANDLE_INVALID;
                }

            }
            else
            {
                position = Vector3.zero;
                rotation = Quaternion.identity;
                extent = Vector3.zero;
                return PxrResult.ERROR_HANDLE_INVALID;
            }
        }
        /// <summary>
        /// Gets the information of a 2D box object.
        /// </summary>
        /// <param name="anchorHandle">Specifies the handle of the scene anchor that the 2D box object is associated with.</param>
        /// <param name="offset">Returns the offset of the 2D box object relative to the center of the scene anchor.</param>
        /// <param name="extent">Returns the length and width of the 2D box object.</param>
        /// <returns>Refer to the `PxrResult` enumeration for details.</returns>
        public static PxrResult GetSceneBox2DData(ulong anchorHandle, out Vector2 offset, out Vector2 extent)
        {
            if (PXR_Plugin.MixedReality.SceneAnchorData.TryGetValue(anchorHandle, out var data))
            {
                if (data.types.Contains(PxrSceneComponentType.Box2D))
                {
                    offset = data.box2D.offset;
                    extent = data.box2D.extent.ToVector2();
                    return PxrResult.SUCCESS;
                }
                else
                {
                    offset = Vector2.zero;
                    extent = Vector2.zero;
                    return PxrResult.ERROR_HANDLE_INVALID;
                }
            }
            else
            {
                offset = Vector2.zero;
                extent = Vector2.zero;
                return PxrResult.ERROR_HANDLE_INVALID;
            }
        }

        /// <summary>
        /// Gets the semantic label of a scene anchor.
        /// </summary>
        /// <param name="anchorHandle">Specifies the handle of the anchor to get semantic label for.</param>
        /// <param name="label">Returns the semantic label of the specified anchor.</param>
        /// <returns>Refer to the `PxrResult` enumeration for details.</returns>
        public static PxrResult GetSceneSemanticLabel(ulong anchorHandle, out PxrSemanticLabel label)
        {
            if (PXR_Plugin.MixedReality.SceneAnchorData.TryGetValue(anchorHandle, out var data))
            {
                label = data.label;
                return PxrResult.SUCCESS;
            }
            else
            {
                label = PxrSemanticLabel.Unknown;
                return PxrResult.ERROR_HANDLE_INVALID;
            }
        }
        /// <summary>
        /// Gets the information of a polygon object.
        /// </summary>
        /// <param name="anchorHandle">Specifies the handle of the scene anchor that the polygon object is associated with.</param>
        /// <param name="vertices">Returns the array of vertices of the polygon object.</param>
        /// <returns>Refer to the `PxrResult` enumeration for details.</returns>
        public static PxrResult GetScenePolygonData(ulong anchorHandle, out Vector2[] vertices)
        {
            if (PXR_Plugin.MixedReality.SceneAnchorData.TryGetValue(anchorHandle, out var data))
            {
                if (data.types.Contains(PxrSceneComponentType.Polygon))
                {
                    vertices = data.polygon.vertices;
                    return PxrResult.SUCCESS;
                }
                else
                {
                    vertices = Array.Empty<Vector2>();
                    return PxrResult.ERROR_HANDLE_INVALID;
                }
            }
            else
            {
                vertices = Array.Empty<Vector2>();
                return PxrResult.ERROR_HANDLE_INVALID;
            }
        }

        public static async Task<(PxrResult result, List<PxrSpatialMeshInfo> meshInfos)> QueryMeshAnchorAsync(CancellationToken token = default)
        {
            return await Task.Run(() =>
            {
                XrSenseDataQueryInfo info = new XrSenseDataQueryInfo()
                {
                    type = XrStructureType.XR_TYPE_SENSE_DATA_QUERY_INFO,
                    filter = IntPtr.Zero
                };
                var pxrResult = PXR_Plugin.MixedReality.UPxr_QuerySenseDataAsync(PXR_Plugin.MixedReality.UPxr_GetSpatialMeshProviderHandle(), ref info,
                    out var future);
                if (pxrResult == PxrResult.SUCCESS)
                {
                    while (true)
                    {
                        var pollResult = PXR_Plugin.MixedReality.UPxr_PollFuture(future, out var futureState);
                        if (pollResult == PxrResult.SUCCESS)
                        {
                            if (futureState == PxrFutureState.Ready)
                            {
                                var completeResult =
                                    PXR_Plugin.MixedReality.UPxr_QuerySenseDataComplete(PXR_Plugin.MixedReality.UPxr_GetSpatialMeshProviderHandle(), future,
                                        out var completion);
                                if (completeResult == PxrResult.SUCCESS)
                                {
                                    if (completion.futureResult == PxrResult.SUCCESS)
                                    {
                                        var getResult = PXR_Plugin.MixedReality.UPxr_GetQueriedSenseData(
                                            PXR_Plugin.MixedReality.UPxr_GetSpatialMeshProviderHandle(), completion.snapshotHandle, out var entityHandles);
                                        if (getResult == PxrResult.SUCCESS)
                                        {
                                            var keysToRemove = PXR_Plugin.MixedReality.SpatialMeshData
                                                .Where(pair => pair.Value.state == MeshChangeState.Removed)
                                                .Select(pair => pair.Key)
                                                .ToList();
                                            foreach (var key in keysToRemove)
                                            {
                                                PXR_Plugin.MixedReality.SpatialMeshData.Remove(key);
                                            }

                                            var toBeRemove = new List<Guid>(PXR_Plugin.MixedReality.meshAnchorLastData.Keys);
                                            foreach (var e in entityHandles)
                                            {
                                                byte[] byteArray = new byte[16];
                                                BitConverter.GetBytes(e.uuid.value0).CopyTo(byteArray, 0);
                                                BitConverter.GetBytes(e.uuid.value1).CopyTo(byteArray, 8);
                                                Guid guid = new Guid(byteArray);
                                                var item = new PxrSpatialMeshInfo()
                                                {
                                                    uuid = guid,
                                                };
                                                toBeRemove.Remove(guid);

                                                if (PXR_Plugin.MixedReality.meshAnchorLastData.TryGetValue(guid, out var lastTime))
                                                {
                                                    if (lastTime < e.time)
                                                    {
                                                        var result = PXR_Plugin.MixedReality.UPxr_GetSpatialMesh(completion.snapshotHandle, e.spatialEntity,
                                                            ref item);
                                                        if (result == PxrResult.SUCCESS)
                                                        {
                                                            item.state = MeshChangeState.Updated;
                                                            PXR_Plugin.MixedReality.SpatialMeshData[guid] = item;
                                                        }

                                                        PXR_Plugin.MixedReality.meshAnchorLastData[guid] = e.time;
                                                    }
                                                    else
                                                    {
                                                        var tempMesh = PXR_Plugin.MixedReality.SpatialMeshData[guid];
                                                        tempMesh.state = MeshChangeState.Unchanged;
                                                        PXR_Plugin.MixedReality.SpatialMeshData[guid] = tempMesh;
                                                    }
                                                }
                                                else
                                                {
                                                    var result = PXR_Plugin.MixedReality.UPxr_GetSpatialMesh(completion.snapshotHandle, e.spatialEntity,
                                                        ref item);
                                                    if (result == PxrResult.SUCCESS)
                                                    {
                                                        item.state = MeshChangeState.Added;
#if UNITY_2021_1_OR_NEWER
                                                        PXR_Plugin.MixedReality.SpatialMeshData.TryAdd(guid, item);
#else
                                                        PXR_Plugin.MixedReality.SpatialMeshData.Add(guid, item);
#endif
                                                    }
#if UNITY_2021_1_OR_NEWER
                                                    PXR_Plugin.MixedReality.meshAnchorLastData.TryAdd(guid, e.time);
#else
                                                    PXR_Plugin.MixedReality.meshAnchorLastData.Add(guid, e.time);
#endif
                                                }
                                            }

                                            foreach (var m in toBeRemove)
                                            {
                                                PXR_Plugin.MixedReality.meshAnchorLastData.Remove(m);
                                                PXR_Plugin.MixedReality.SpatialMeshData.Remove(m);
                                                var removedMesh = new PxrSpatialMeshInfo()
                                                {
                                                    uuid = m,
                                                    state = MeshChangeState.Removed
                                                };
#if UNITY_2021_1_OR_NEWER
                                                PXR_Plugin.MixedReality.SpatialMeshData.TryAdd(m, removedMesh);
#else
                                                PXR_Plugin.MixedReality.SpatialMeshData.Add(m, removedMesh);
#endif
                                            }

                                            PXR_Plugin.MixedReality.UPxr_DestroySenseDataQueryResult(completion.snapshotHandle);
                                            return (getResult, PXR_Plugin.MixedReality.SpatialMeshData.Values.ToList());
                                        }
                                        else
                                        {
                                            PXR_Plugin.MixedReality.UPxr_DestroySenseDataQueryResult(completion.snapshotHandle);
                                            return (getResult, new List<PxrSpatialMeshInfo>());
                                        }
                                    }
                                    else
                                    {
                                        return (completion.futureResult, new List<PxrSpatialMeshInfo>());
                                    }
                                }
                                else
                                {
                                    return (completeResult, new List<PxrSpatialMeshInfo>());
                                }
                            }
                        }
                        else
                        {
                            return (pollResult, new List<PxrSpatialMeshInfo>());
                        }
                    }
                }
                else
                {
                    return (pxrResult, new List<PxrSpatialMeshInfo>());
                }
            }, token);
        }

    }
}