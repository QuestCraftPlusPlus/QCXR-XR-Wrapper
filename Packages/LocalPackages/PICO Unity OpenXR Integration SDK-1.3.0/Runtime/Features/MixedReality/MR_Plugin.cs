using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.XR.OpenXR.Features.PICOSupport;
using UnityEngine;

namespace Unity.XR.PXR
{
    public partial class PXR_Plugin
    {
        public static class MixedReality
        {
            private const string TAG = "[MR_Plugin/MixedReality]";
            const string PXR_PLATFORM_DLL = "openxr_pico";

            [DllImport(PXR_PLATFORM_DLL, CallingConvention = CallingConvention.Cdecl)]
            public static extern int Pxr_SetMeshLOD(ushort spatialMeshLod);

            [DllImport(PXR_PLATFORM_DLL, CallingConvention = CallingConvention.Cdecl)]
            private static extern int Pxr_StartSenseDataProviderAsync(ulong providerHandle, out ulong future);

            [DllImport(PXR_PLATFORM_DLL, CallingConvention = CallingConvention.Cdecl)]
            private static extern int Pxr_PollFutureEXT(ref XrFuturePollInfoEXT pollInfo, ref XrFuturePollResultEXT pollResult);

            [DllImport(PXR_PLATFORM_DLL, CallingConvention = CallingConvention.Cdecl)]
            private static extern int Pxr_CreateSenseDataProvider(ref XrSenseDataProviderCreateInfoBaseHeader createInfo, out ulong providerHandle);

            [DllImport(PXR_PLATFORM_DLL, CallingConvention = CallingConvention.Cdecl)]
            private static extern int Pxr_StartSenseDataProviderComplete(ulong future, ref XrSenseDataProviderStartCompletion completion);

            [DllImport(PXR_PLATFORM_DLL, CallingConvention = CallingConvention.Cdecl)]
            private static extern int Pxr_GetSenseDataProviderState(ulong providerHandle, ref PxrSenseDataProviderState state);

            [DllImport(PXR_PLATFORM_DLL, CallingConvention = CallingConvention.Cdecl)]
            private static extern int Pxr_StopSenseDataProvider(ulong providerHandle);

            [DllImport(PXR_PLATFORM_DLL, CallingConvention = CallingConvention.Cdecl)]
            private static extern int Pxr_DestroySenseDataProvider(ulong providerHandle);

            [DllImport(PXR_PLATFORM_DLL, CallingConvention = CallingConvention.Cdecl)]
            private static extern int Pxr_CreateSpatialAnchorAsync(ulong providerHandle, ref PxrPosef info, out ulong future);

            [DllImport(PXR_PLATFORM_DLL, CallingConvention = CallingConvention.Cdecl)]
            private static extern int Pxr_CreateSpatialAnchorComplete(ulong providerHandle, ulong future, ref XrSpatialAnchorCompletion completion);

            [DllImport(PXR_PLATFORM_DLL, CallingConvention = CallingConvention.Cdecl)]
            private static extern int Pxr_PersistSpatialAnchorAsync(ulong providerHandle, ref XrSpatialAnchorPersistInfo info, out ulong future);

            [DllImport(PXR_PLATFORM_DLL, CallingConvention = CallingConvention.Cdecl)]
            private static extern int Pxr_PersistSpatialAnchorComplete(ulong providerHandle, ulong future, ref XrSpatialAnchorCompletion completion);

            [DllImport(PXR_PLATFORM_DLL, CallingConvention = CallingConvention.Cdecl)]
            private static extern int Pxr_DestroyAnchor(ulong anchorHandle);

            [DllImport(PXR_PLATFORM_DLL, CallingConvention = CallingConvention.Cdecl)]
            private static extern int Pxr_QuerySenseDataAsync(ulong providerHandle, ref XrSenseDataQueryInfo info, out ulong future);

            [DllImport(PXR_PLATFORM_DLL, CallingConvention = CallingConvention.Cdecl)]
            private static extern int Pxr_QuerySenseDataComplete(ulong providerHandle, ulong future, ref XrSenseDataQueryCompletion completion);

            [DllImport(PXR_PLATFORM_DLL, CallingConvention = CallingConvention.Cdecl)]
            private static extern int Pxr_GetQueriedSenseData(ulong providerHandle, ref XrQueriedSenseDataGetInfo info, ref XrQueriedSenseData senseData);

            [DllImport(PXR_PLATFORM_DLL, CallingConvention = CallingConvention.Cdecl)]
            private static extern int Pxr_RetrieveSpatialEntityAnchor(ulong snapshotHandle, ref XrSpatialEntityAnchorRetrieveInfo info, out ulong anchorHandle);

            [DllImport(PXR_PLATFORM_DLL, CallingConvention = CallingConvention.Cdecl)]
            private static extern int Pxr_DestroySenseDataQueryResult(ulong snapshotHandle);

            [DllImport(PXR_PLATFORM_DLL, CallingConvention = CallingConvention.Cdecl)]
            private static extern int Pxr_LocateAnchor(ulong anchorHandle, ref XrSpaceLocation location);

            [DllImport(PXR_PLATFORM_DLL, CallingConvention = CallingConvention.Cdecl)]
            private static extern int Pxr_GetAnchorUuid(ulong anchorHandle, out PxrUuid uuid);

            [DllImport(PXR_PLATFORM_DLL, CallingConvention = CallingConvention.Cdecl)]
            private static extern int Pxr_UnpersistSpatialAnchorAsync(ulong providerHandle, ref XrSpatialAnchorUnpersistInfo info, out ulong future);

            [DllImport(PXR_PLATFORM_DLL, CallingConvention = CallingConvention.Cdecl)]
            private static extern int Pxr_UnpersistSpatialAnchorComplete(ulong providerHandle, ulong future, ref XrSpatialAnchorCompletion completion);

            [DllImport(PXR_PLATFORM_DLL, CallingConvention = CallingConvention.Cdecl)]
            private static extern int Pxr_StartSceneCaptureAsync(ulong providerHandle, out ulong future);

            [DllImport(PXR_PLATFORM_DLL, CallingConvention = CallingConvention.Cdecl)]
            private static extern int Pxr_StartSceneCaptureComplete(ulong providerHandle, ulong future, ref XrSceneCaptureStartCompletion completion);

            [DllImport(PXR_PLATFORM_DLL, CallingConvention = CallingConvention.Cdecl)]
            private static extern int Pxr_EnumerateSpatialEntityComponentTypes(ulong snapshotHandle, ulong spatialEntity, uint inputCount, out uint outputCount,
                IntPtr types);

            [DllImport(PXR_PLATFORM_DLL, CallingConvention = CallingConvention.Cdecl)]
            private static extern ulong Pxr_GetSpatialMeshProviderHandle();

            [DllImport(PXR_PLATFORM_DLL, CallingConvention = CallingConvention.Cdecl)]
            private static extern int Pxr_GetSpatialEntityComponentInfo(ulong snapshotHandle, ref XrSpatialEntityComponentGetInfoBaseHeader componentGetInfo,
                ref XrSpatialEntityComponentDataBaseHeader componentInfo);

            [DllImport(PXR_PLATFORM_DLL, CallingConvention = CallingConvention.Cdecl)]
            private static extern int Pxr_GetSpatialEntitySemanticInfo(ulong snapshotHandle, ref XrSpatialEntityGetInfo componentGetInfo,
                ref XrSpatialEntitySemanticData componentInfo);

            [DllImport(PXR_PLATFORM_DLL, CallingConvention = CallingConvention.Cdecl)]
            private static extern int Pxr_GetSpatialEntityLocationInfo(ulong snapshotHandle, ref XrSpatialEntityLocationGetInfo componentGetInfo,
                ref XrSpatialEntityLocationData componentInfo);

            [DllImport(PXR_PLATFORM_DLL, CallingConvention = CallingConvention.Cdecl)]
            private static extern int Pxr_GetSpatialEntityBox3DInfo(ulong snapshotHandle, ref XrSpatialEntityGetInfo componentGetInfo,
                ref XrSpatialEntityBoundingBox3DData componentInfo);

            [DllImport(PXR_PLATFORM_DLL, CallingConvention = CallingConvention.Cdecl)]
            private static extern int Pxr_GetSpatialEntityBox2DInfo(ulong snapshotHandle, ref XrSpatialEntityGetInfo componentGetInfo,
                ref XrSpatialEntityBoundingBox2DData componentInfo);

            [DllImport(PXR_PLATFORM_DLL, CallingConvention = CallingConvention.Cdecl)]
            private static extern int Pxr_GetSpatialEntityPolygonInfo(ulong snapshotHandle, ref XrSpatialEntityGetInfo componentGetInfo,
                ref XrSpatialEntityPolygonData componentInfo);

            [DllImport(PXR_PLATFORM_DLL, CallingConvention = CallingConvention.Cdecl)]
            private static extern int Pxr_GetSpatialMeshVerticesAndIndices(ulong snapshotHandle, ref XrSpatialEntityGetInfo componentGetInfo,
                ref PxrTriangleMeshInfo componentInfo);

            [DllImport(PXR_PLATFORM_DLL, CallingConvention = CallingConvention.Cdecl)]
            private static extern unsafe void Pxr_AddOrUpdateMesh(ulong id1, ulong id2, int numVertices, void* vertices, int numTriangles, void* indices,
                Vector3 position, Quaternion rotation);

            [DllImport(PXR_PLATFORM_DLL, CallingConvention = CallingConvention.Cdecl)]
            private static extern void Pxr_RemoveMesh(ulong id1, ulong id2);

            [DllImport(PXR_PLATFORM_DLL, CallingConvention = CallingConvention.Cdecl)]
            private static extern void Pxr_ClearMeshes();
            public static ulong SpatialAnchorProviderHandle { get; set; }
            public static ulong SceneCaptureProviderHandle { get; set; }
     

            public static Dictionary<Guid, ulong> meshAnchorLastData = new Dictionary<Guid, ulong>();
            public static Dictionary<ulong, PxrSceneComponentData> SceneAnchorData = new Dictionary<ulong, PxrSceneComponentData>();
            public static Dictionary<Guid, PxrSpatialMeshInfo> SpatialMeshData = new Dictionary<Guid, PxrSpatialMeshInfo>();
            private static readonly Dictionary<Guid, List<IDisposable>> nativeMeshArrays = new Dictionary<Guid, List<IDisposable>>();

            public static ulong UPxr_GetSenseDataProviderHandle(PxrSenseDataProviderType type)
            {
                switch (type)
                {
                    case PxrSenseDataProviderType.SpatialAnchor:
                        return SpatialAnchorProviderHandle;
                    case PxrSenseDataProviderType.SceneCapture:
                        return SceneCaptureProviderHandle;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(type), type, null);
                }
            }

            public static PxrResult UPxr_StartSenseDataProviderAsync(ulong providerHandle, out ulong future)
            {
                future = UInt64.MinValue;
                var pxrResult = (PxrResult)Pxr_StartSenseDataProviderAsync(providerHandle, out future);
                return pxrResult;
            }

            public static PxrResult UPxr_PollFuture(ulong future, out PxrFutureState futureState)
            {
                XrFuturePollInfoEXT pollInfo = new XrFuturePollInfoEXT()
                {
                    type = XrStructureType.XR_TYPE_FUTURE_POLL_INFO_EXT,
                    future = future,
                };
                XrFuturePollResultEXT pollResult = new XrFuturePollResultEXT()
                {
                    type = XrStructureType.XR_TYPE_FUTURE_POLL_RESULT_EXT,
                };
                var pxrResult = (PxrResult)Pxr_PollFutureEXT(ref pollInfo, ref pollResult);
                futureState = pollResult.state;

                return pxrResult;
            }

            public static PxrResult UPxr_CreateSenseDataProvider(ref XrSenseDataProviderCreateInfoBaseHeader info, out ulong providerHandle)
            {
                var pxrResult = (PxrResult)Pxr_CreateSenseDataProvider(ref info, out providerHandle);
                return pxrResult;
            }

            public static PxrResult UPxr_CreateSpatialAnchorSenseDataProvider()
            {
                XrSenseDataProviderCreateInfoBaseHeader header = new XrSenseDataProviderCreateInfoBaseHeader()
                {
                    type = XrStructureType.XR_TYPE_SENSE_DATA_PROVIDER_CREATE_INFO_SPATIAL_ANCHOR
                };

                var pxrResult = UPxr_CreateSenseDataProvider(ref header, out var providerHandle);
                SpatialAnchorProviderHandle = providerHandle;
                return pxrResult;
            }

            public static PxrResult UPxr_CreateSceneCaptureSenseDataProvider()
            {
                XrSenseDataProviderCreateInfoBaseHeader header = new XrSenseDataProviderCreateInfoBaseHeader()
                {
                    type = XrStructureType.XR_TYPE_SENSE_DATA_PROVIDER_CREATE_INFO_SCENE_CAPTURE,
                };

                var pxrResult = UPxr_CreateSenseDataProvider(ref header, out var providerHandle);
                SceneCaptureProviderHandle = providerHandle;
                return pxrResult;
            }

            public static PxrResult UPxr_StartSenseDataProviderComplete(ulong future, out XrSenseDataProviderStartCompletion completion)
            {
                completion = new XrSenseDataProviderStartCompletion()
                {
                    type = XrStructureType.XR_TYPE_SENSE_DATA_PROVIDER_START_COMPLETION,
                };
                var pxrResult = (PxrResult)Pxr_StartSenseDataProviderComplete(future, ref completion);
                return pxrResult;
            }

            public static PxrResult UPxr_GetSenseDataProviderState(ulong providerHandle, out PxrSenseDataProviderState state)
            {
                state = PxrSenseDataProviderState.Stopped;
                var pxrResult = (PxrResult)Pxr_GetSenseDataProviderState(providerHandle, ref state);
                return pxrResult;
            }

            public static PxrResult UPxr_StopSenseDataProvide(ulong providerHandle)
            {
                var pxrResult = (PxrResult)Pxr_StopSenseDataProvider(providerHandle);
                return pxrResult;
            }

            public static PxrResult UPxr_DestroySenseDataProvider(ulong providerHandle)
            {
                var pxrResult = (PxrResult)Pxr_DestroySenseDataProvider(providerHandle);
                return pxrResult;
            }

            public static PxrResult UPxr_CreateSpatialAnchorAsync(ulong providerHandle, Vector3 position, Quaternion rotation, out ulong future)
            {
                PxrPosef pose = new PxrPosef()
                {
                    orientation = new PxrVector4f()
                    {
                        x = rotation.x,
                        y = rotation.y,
                        z = -rotation.z,
                        w = -rotation.w
                    },
                    position = new PxrVector3f()
                    {
                        x = position.x,
                        y = position.y,
                        z = -position.z
                    }
                };
                var pxrResult = (PxrResult)Pxr_CreateSpatialAnchorAsync(providerHandle, ref pose, out future);
                return pxrResult;
            }

            public static PxrResult UPxr_CreateSpatialAnchorComplete(ulong providerHandle, ulong future, out XrSpatialAnchorCompletion completion)
            {
                completion = new XrSpatialAnchorCompletion()
                {
                    type = XrStructureType.XR_TYPE_SPATIAL_ANCHOR_CREATE_COMPLETION
                };
                var pxrResult = (PxrResult)Pxr_CreateSpatialAnchorComplete(providerHandle, future, ref completion);
                return pxrResult;
            }

            public static PxrResult UPxr_PersistSpatialAnchorAsync(ulong providerHandle, ulong anchorHandle, out ulong future)
            {
                XrSpatialAnchorPersistInfo persistInfo = new XrSpatialAnchorPersistInfo()
                {
                    type = XrStructureType.XR_TYPE_SPATIAL_ANCHOR_PERSIST_INFO,
                    location = PxrPersistenceLocation.Local,
                    anchorHandle = anchorHandle
                };
                var pxrResult = (PxrResult)Pxr_PersistSpatialAnchorAsync(providerHandle, ref persistInfo, out future);
                return pxrResult;
            }
            
            public static PxrResult UPxr_PersistSpatialAnchorComplete(ulong providerHandle,ulong future,out XrSpatialAnchorCompletion completion)
            {
                completion = new XrSpatialAnchorCompletion()
                {
                    type = XrStructureType.XR_TYPE_SPATIAL_ANCHOR_PERSIST_COMPLETION,
                };
                var pxrResult = (PxrResult)Pxr_PersistSpatialAnchorComplete(providerHandle, future, ref completion);
                return pxrResult;
            }

            public static PxrResult UPxr_DestroyAnchor(ulong anchorHandle)
            {
                var pxrResult = (PxrResult)Pxr_DestroyAnchor(anchorHandle);
                return pxrResult;
            }
   
            public static PxrResult UPxr_QuerySenseDataAsync(ulong providerHandle,ref XrSenseDataQueryInfo info, out ulong future)
            {
                var pxrResult = (PxrResult)Pxr_QuerySenseDataAsync(providerHandle, ref info, out future);
                return pxrResult;
            }
            public static PxrResult UPxr_QuerySenseDataByUuidAsync(Guid[] uuids, out ulong future)
            {
                XrSenseDataQueryInfo info = new XrSenseDataQueryInfo()
                {
                    type = XrStructureType.XR_TYPE_SENSE_DATA_QUERY_INFO,
                };
                XrSenseDataFilterUuid uuidFilter = new XrSenseDataFilterUuid()
                {
                    type = XrStructureType.XR_TYPE_SENSE_DATA_FILTER_UUID
                };

                if (uuids.Length > 0)
                {
                    uuidFilter.uuidCount = (uint)uuids.Length;
                    uuidFilter.uuidList = Marshal.AllocHGlobal(uuids.Length * Marshal.SizeOf(typeof(Guid)));
                    byte[] bytes = uuids.SelectMany(g => g.ToByteArray()).ToArray();
                    Marshal.Copy(bytes, 0, uuidFilter.uuidList, uuids.Length * Marshal.SizeOf(typeof(Guid)));
                    int size = Marshal.SizeOf<XrSenseDataFilterUuid>();
                    info.filter = Marshal.AllocHGlobal(size);
                    Marshal.StructureToPtr(uuidFilter, info.filter, false);
                }
                else
                {
                    info.filter = IntPtr.Zero;
                }

                var pxrResult = UPxr_QuerySenseDataAsync(UPxr_GetSenseDataProviderHandle(PxrSenseDataProviderType.SpatialAnchor), ref info, out future);
                Marshal.FreeHGlobal(uuidFilter.uuidList);
                Marshal.FreeHGlobal(info.filter);
                return pxrResult;
            }

            public static PxrResult UPxr_QuerySenseDataComplete(ulong providerHandle,ulong future,out XrSenseDataQueryCompletion completion)
            {
                completion = new XrSenseDataQueryCompletion()
                {
                    type = XrStructureType.XR_TYPE_SENSE_DATA_QUERY_COMPLETION,
                };
                var pxrResult = (PxrResult)Pxr_QuerySenseDataComplete(providerHandle, future,ref completion);
                return pxrResult;
            }

        

            public static PxrResult UPxr_GetQueriedSenseData(ulong providerHandle, ulong snapshotHandle, out List<PxrQueriedSpatialEntityInfo> entityinfos)
            {
                XrQueriedSenseDataGetInfo info = new XrQueriedSenseDataGetInfo()
                {
                    type = XrStructureType.XR_TYPE_QUERIED_SENSE_DATA_GET_INFO,
                    snapshotHandle = snapshotHandle
                };

                XrQueriedSenseData senseDataFirst = new XrQueriedSenseData()
                {
                    type = XrStructureType.XR_TYPE_QUERIED_SENSE_DATA_GET_INFO,
                    queriedSpatialEntityCapacityInput = 0,
                    queriedSpatialEntityCountOutput = 0,
                };

                var getResultFirst = (PxrResult)Pxr_GetQueriedSenseData(providerHandle, ref info, ref senseDataFirst);
                if (getResultFirst == PxrResult.SUCCESS)
                {
                    XrQueriedSenseData senseDataSecond = new XrQueriedSenseData()
                    {
                        type = XrStructureType.XR_TYPE_QUERIED_SENSE_DATA_GET_INFO,
                        queriedSpatialEntityCapacityInput = senseDataFirst.queriedSpatialEntityCountOutput,
                        queriedSpatialEntityCountOutput = senseDataFirst.queriedSpatialEntityCountOutput,
                    };
                    int resultSize = Marshal.SizeOf<PxrQueriedSpatialEntityInfo>();
                    // Debug.Log("[PoxrUnity] PxrQueriedSpatialEntityInfo size:"+resultSize);
                    int bytesSize = (int)senseDataFirst.queriedSpatialEntityCountOutput * resultSize;
                    senseDataSecond.queriedSpatialEntities = Marshal.AllocHGlobal(bytesSize);
                    var getResultSecond = (PxrResult)Pxr_GetQueriedSenseData(providerHandle, ref info, ref senseDataSecond);
                    entityinfos = new List<PxrQueriedSpatialEntityInfo>();
                    if (getResultSecond==PxrResult.SUCCESS)
                    {
                        for (int i = 0; i < senseDataFirst.queriedSpatialEntityCountOutput; i++)
                        {
                            PxrQueriedSpatialEntityInfo t =
                                (PxrQueriedSpatialEntityInfo)Marshal.PtrToStructure(senseDataSecond.queriedSpatialEntities + i * resultSize,
                                    typeof(PxrQueriedSpatialEntityInfo));
                           // Debug.Log($"[PoxrUnity] {i} spatialEntity="+t.spatialEntity);
                            entityinfos.Add(t);
                        }
                    }
                    Marshal.FreeHGlobal(senseDataSecond.queriedSpatialEntities);
                    return getResultSecond;
                }
                else
                {
                    entityinfos = new List<PxrQueriedSpatialEntityInfo>();
                    return getResultFirst;
                }
            }
           
            public static PxrResult UPxr_RetrieveSpatialEntityAnchor(ulong snapshotHandle, ulong spatialEntityHandle,out ulong anchorHandle)
            {
                XrSpatialEntityAnchorRetrieveInfo info = new XrSpatialEntityAnchorRetrieveInfo()
                {
                    type = XrStructureType.XR_TYPE_SPATIAL_ENTITY_ANCHOR_RETRIEVE_INFO,
                    spatialEntity = spatialEntityHandle,
                };
                var pxrResult = (PxrResult)Pxr_RetrieveSpatialEntityAnchor(snapshotHandle, ref info,out anchorHandle);
                return pxrResult;
            }
            public static PxrResult UPxr_DestroySenseDataQueryResult(ulong queryResultHandle)
            {
                var pxrResult = (PxrResult)Pxr_DestroySenseDataQueryResult(queryResultHandle);
                return pxrResult;
            }

            public static PxrResult UPxr_LocateAnchor(ulong anchorHandle, out Vector3 position, out Quaternion rotation)
            {
          
           
                XrSpaceLocation location = new XrSpaceLocation()
                {
                    type = XrStructureType.XR_TYPE_SPACE_LOCATION,
                };
                var pxrResult = (PxrResult)Pxr_LocateAnchor(anchorHandle, ref location);
                foreach (PxrSpaceLocationFlags value in Enum.GetValues(typeof(PxrSpaceLocationFlags)))
                {
                    if ((location.locationFlags & (ulong)value) != (ulong)value)
                    {
                        position = Vector3.zero;
                        rotation = Quaternion.identity;
                        return PxrResult.ERROR_POSE_INVALID;
                    }
                }
                rotation = new Quaternion(location.pose.orientation.x, location.pose.orientation.y, -location.pose.orientation.z, -location.pose.orientation.w);
                position = new Vector3(location.pose.position.x, location.pose.position.y, -location.pose.position.z);
                return pxrResult;
            }
          
            public static PxrResult UPxr_GetAnchorUuid(ulong anchorHandle, out Guid uuid)
            {
                var pxrResult = (PxrResult)Pxr_GetAnchorUuid(anchorHandle,out var pUuid);
                byte[] byteArray = new byte[16];
                BitConverter.GetBytes(pUuid.value0).CopyTo(byteArray, 0);
                BitConverter.GetBytes(pUuid.value1).CopyTo(byteArray, 8);
               // Debug.Log($"GetAnchorUuid, uuid byteArray result is {string.Join(", ", byteArray)}");
                uuid = new Guid(byteArray);
                return pxrResult;
            }
            
           
            public static PxrResult UPxr_UnPersistSpatialAnchorAsync(ulong providerHandle, ulong anchorHandle, out ulong future)
            {
                XrSpatialAnchorUnpersistInfo unPersistInfo = new XrSpatialAnchorUnpersistInfo()
                {
                    type = XrStructureType.XR_TYPE_SPATIAL_ANCHOR_UNPERSIST_INFO,
                    location = PxrPersistenceLocation.Local,
                    anchorHandle = anchorHandle
                };
                var pxrResult = (PxrResult)Pxr_UnpersistSpatialAnchorAsync(providerHandle, ref unPersistInfo, out future);
                return pxrResult;
            }
            public static PxrResult UPxr_UnPersistSpatialAnchorComplete(ulong providerHandle,ulong future, out XrSpatialAnchorCompletion completion)
            {
                completion = new XrSpatialAnchorCompletion()
                {
                    type = XrStructureType.XR_TYPE_SPATIAL_ANCHOR_UNPERSIST_COMPLETION,
                };
                var pxrResult = (PxrResult)Pxr_UnpersistSpatialAnchorComplete(providerHandle, future, ref completion);
                return pxrResult;
            }
       
            public static PxrResult UPxr_StartSceneCaptureAsync(out ulong future)
            {
                var pxrResult = (PxrResult)Pxr_StartSceneCaptureAsync(UPxr_GetSenseDataProviderHandle(PxrSenseDataProviderType.SceneCapture), out future);
                return pxrResult;
            }
           
            public static PxrResult UPxr_StartSceneCaptureComplete(ulong future,out XrSceneCaptureStartCompletion completion)
            {
                completion = new XrSceneCaptureStartCompletion()
                {
                    type = XrStructureType.XR_TYPE_SCENE_CAPTURE_START_COMPLETION
                };
                var pxrResult = (PxrResult)Pxr_StartSceneCaptureComplete(UPxr_GetSenseDataProviderHandle(PxrSenseDataProviderType.SceneCapture), future,ref completion);
                return pxrResult;
            }
            
            public static PxrResult UPxr_QuerySenseDataBySemanticAsync(PxrSemanticLabel[] labels, out ulong future)
            {
                XrSenseDataQueryInfo info = new XrSenseDataQueryInfo()
                {
                    type = XrStructureType.XR_TYPE_SENSE_DATA_QUERY_INFO,
                };
                
                XrSenseDataFilterSemantic semanticFilter = new XrSenseDataFilterSemantic()
                {
                    type = XrStructureType.XR_TYPE_SENSE_DATA_FILTER_SEMANTIC
                };

                if (labels.Length > 0)
                {
                    semanticFilter.semanticCount = (uint)labels.Length;
                    int[] labelsAsInts = labels.Select(x => (int)x).ToArray();
                    semanticFilter.semantics = Marshal.AllocHGlobal(labels.Length * Marshal.SizeOf(typeof(int)));
                    Marshal.Copy(labelsAsInts, 0, semanticFilter.semantics, labelsAsInts.Length);
                    int size = Marshal.SizeOf<XrSenseDataFilterSemantic>();
                    info.filter = Marshal.AllocHGlobal(size);
                    Marshal.StructureToPtr(semanticFilter, info.filter, false);
                }
                else
                {
                    info.filter = IntPtr.Zero;
                }

                var pxrResult = UPxr_QuerySenseDataAsync(UPxr_GetSenseDataProviderHandle(PxrSenseDataProviderType.SceneCapture), ref info, out future);
                Marshal.FreeHGlobal(semanticFilter.semantics);
                Marshal.FreeHGlobal(info.filter);
                return pxrResult;
            }

         

            public static PxrResult UPxr_GetSpatialEntitySemanticInfo(ulong snapshotHandle, ulong spatialEntityHandle, out PxrSemanticLabel label)
            {
                label = PxrSemanticLabel.Unknown;
                PxrResult result = UPxr_GetSpatialSemantics(snapshotHandle, spatialEntityHandle, out var labels);
                if (result==PxrResult.SUCCESS&&labels.Length>0)
                {
                    label = labels[0];
                }
                return result;
            }
            
   
            

            public static PxrResult UPxr_GetSpatialEntityLocationInfo(ulong snapshotHandle, ulong spatialEntityHandle, out Vector3 position,
                out Quaternion rotation)
            {
                position = Vector3.zero;
                rotation = Quaternion.identity;
                var getInfo = new XrSpatialEntityLocationGetInfo
                {
                    type = XrStructureType.XR_TYPE_SPATIAL_ENTITY_LOCATION_GET_INFO,
                    entity = spatialEntityHandle,
                    componentType = PxrSceneComponentType.Location,
                    baseSpace = 0,
                    time = 0,
                };
                XrSpatialEntityLocationData locationInfo = new XrSpatialEntityLocationData()
                {
                    type = XrStructureType.XR_TYPE_SPATIAL_ENTITY_LOCATION_DATA
                };
                var result = (PxrResult)Pxr_GetSpatialEntityLocationInfo(snapshotHandle, ref getInfo, ref locationInfo);
                if (result == PxrResult.SUCCESS)
                {
                    foreach (PxrSpaceLocationFlags value in Enum.GetValues(typeof(PxrSpaceLocationFlags)))
                    {
                        if ((locationInfo.location.locationFlags & (ulong)value) != (ulong)value)
                        {
                            position = Vector3.zero;
                            rotation = Quaternion.identity;
                            return PxrResult.ERROR_POSE_INVALID;
                        }
                    }
                    rotation = new Quaternion(locationInfo.location.pose.orientation.x, locationInfo.location.pose.orientation.y, -locationInfo.location.pose.orientation.z,
                        -locationInfo.location.pose.orientation.w);
                    position = new Vector3(locationInfo.location.pose.position.x, locationInfo.location.pose.position.y, -locationInfo.location.pose.position.z);
                    // Debug.Log("[PoxrUnity] UPxr_GetSpatialEntityLocationInfo rotation:"+rotation+"   position:"+position);
                }
                return result;
            }
           
            public static PxrResult UPxr_EnumerateSpatialEntityComponentTypes(ulong snapshotHandle, ulong spatialEntityHandle,
                out PxrSceneComponentType[] types)
            {
                var componentTypes = IntPtr.Zero;
                types = Array.Empty<PxrSceneComponentType>();
                var firstResult =
                    (PxrResult)Pxr_EnumerateSpatialEntityComponentTypes(snapshotHandle, spatialEntityHandle, 0, out var firstOutputCount, componentTypes);
                if (firstResult == PxrResult.SUCCESS)
                {
                    int size = (int)firstOutputCount * Marshal.SizeOf(typeof(int));
                    componentTypes = Marshal.AllocHGlobal(size);
                    var secondResult = (PxrResult)Pxr_EnumerateSpatialEntityComponentTypes(snapshotHandle, spatialEntityHandle, firstOutputCount,
                        out var outputCount, componentTypes);
                    if (secondResult == PxrResult.SUCCESS)
                    {
                        types = new PxrSceneComponentType[outputCount];
                        int[] typesInts = new int[outputCount];
                        Marshal.Copy(componentTypes, typesInts, 0, (int)firstOutputCount);
                        for (int i = 0; i < outputCount; i++)
                        {
                            types[i] = (PxrSceneComponentType)typesInts[i];
                            // Debug.Log("[PoxrUnity] UPxr_EnumerateSpatialEntityComponentTypes componentTypes="+ typesInts[i]);
                        }

                        Marshal.FreeHGlobal(componentTypes);
                        return PxrResult.SUCCESS;
                    }
                    else
                    {
                        types = Array.Empty<PxrSceneComponentType>();
                        Marshal.FreeHGlobal(componentTypes);
                        return secondResult;
                    }
                }
                else
                {
                    types = Array.Empty<PxrSceneComponentType>();
                    return firstResult;
                }
            }
         

            public static PxrResult UPxr_GetSpatialEntityBox3DInfo(ulong snapshotHandle, ulong spatialEntityHandle, out Vector3 position,
                out Quaternion rotation, out Vector3 extent)
            {
                position = Vector3.zero;
                rotation = Quaternion.identity;
                extent = Vector3.zero;
                var getInfo = new XrSpatialEntityGetInfo
                {
                    type = XrStructureType.XR_TYPE_SPATIAL_ENTITY_BOUNDING_BOX_3D_GET_INFO,
                    next = IntPtr.Zero,
                    entity = spatialEntityHandle,
                    componentType = PxrSceneComponentType.Box3D
                };
          
                
                XrSpatialEntityBoundingBox3DData box3DInfo = new XrSpatialEntityBoundingBox3DData()
                {
                    type = XrStructureType.XR_TYPE_SPATIAL_ENTITY_BOUNDING_BOX_3D_DATA,
                    next = IntPtr.Zero
                };

                var result =  (PxrResult)Pxr_GetSpatialEntityBox3DInfo(snapshotHandle, ref getInfo, ref box3DInfo);
                if (result == PxrResult.SUCCESS)
                {
                    position = new Vector3(box3DInfo.box3D.center.position.x, box3DInfo.box3D.center.position.y, box3DInfo.box3D.center.position.z);
                    rotation = new Quaternion(box3DInfo.box3D.center.orientation.x, box3DInfo.box3D.center.orientation.y, box3DInfo.box3D.center.orientation.z,
                        box3DInfo.box3D.center.orientation.w);
                    extent = new Vector3(box3DInfo.box3D.extents.width, box3DInfo.box3D.extents.height, box3DInfo.box3D.extents.depth);
                    // Debug.Log($"[PoxrUnity] UPxr_GetSpatialEntityBox3DInfo snapshotHandle={snapshotHandle} position={position} rotation={rotation}  extent={ extent}");
            
                }
                return result;
            }
    
            public static PxrResult UPxr_GetSpatialEntityBox2DInfo(ulong snapshotHandle, ulong spatialEntityHandle, out Vector2 offset, out Vector2 extent)
            {
                offset = Vector2.zero;
                extent = Vector2.zero;
                var getInfo = new XrSpatialEntityGetInfo
                {
                    type = XrStructureType.XR_TYPE_SPATIAL_ENTITY_BOUNDING_BOX_2D_GET_INFO,
                    entity = spatialEntityHandle,
                    componentType = PxrSceneComponentType.Box2D
                };
                
                XrSpatialEntityBoundingBox2DData box2DInfo = new XrSpatialEntityBoundingBox2DData()
                {
                    type = XrStructureType.XR_TYPE_SPATIAL_ENTITY_BOUNDING_BOX_2D_DATA,
                };
                
                var result =  (PxrResult)Pxr_GetSpatialEntityBox2DInfo(snapshotHandle, ref getInfo, ref box2DInfo);
                if (result == PxrResult.SUCCESS)
                {
                    offset = box2DInfo.box2D.offset;
                    extent =box2DInfo.box2D.extent.ToVector2();
                    Debug.Log($"[PoxrUnity] UPxr_GetSpatialEntityBox2DInfo snapshotHandle={snapshotHandle} offset={offset} extent={extent} ");
                }
                return result;
            }
 

            public static PxrResult UPxr_GetSpatialEntityPolygonInfo(ulong snapshotHandle, ulong spatialEntityHandle, out Vector2[] vertices)
            {
                vertices = Array.Empty<Vector2>();
                var getInfo = new XrSpatialEntityGetInfo
                {
                    type = XrStructureType.XR_TYPE_SPATIAL_ENTITY_POLYGON_GET_INFO,
                    entity = spatialEntityHandle,
                    componentType = PxrSceneComponentType.Polygon
                };

                XrSpatialEntityPolygonData polygonInfo = new XrSpatialEntityPolygonData()
                {
                    type = XrStructureType.XR_TYPE_SPATIAL_ENTITY_POLYGON_DATA,
                    polygonCapacityInput = 0,
                    polygonCountOutput = 0,
                    vertices = IntPtr.Zero
                };

                var result = (PxrResult)Pxr_GetSpatialEntityPolygonInfo(snapshotHandle, ref getInfo, ref polygonInfo);

                if (result == PxrResult.SUCCESS)
                {
                    if (polygonInfo.polygonCountOutput > 0)
                    {
                        polygonInfo.polygonCapacityInput = polygonInfo.polygonCountOutput;
                        polygonInfo.vertices = Marshal.AllocHGlobal((int)polygonInfo.polygonCountOutput * Marshal.SizeOf(typeof(PxrVector2f)));

                        result = (PxrResult)Pxr_GetSpatialEntityPolygonInfo(snapshotHandle, ref getInfo, ref polygonInfo);
                        if (result == PxrResult.SUCCESS)
                        {
                            vertices = new Vector2[polygonInfo.polygonCountOutput];
                            var vector2fs = new PxrVector2f[polygonInfo.polygonCountOutput];
                            for (int i = 0; i < polygonInfo.polygonCountOutput; i++)
                            {
                                vector2fs[i] = Marshal.PtrToStructure<PxrVector2f>(polygonInfo.vertices + i * Marshal.SizeOf(typeof(PxrVector2f)));
                                vertices[i].x = vector2fs[i].x;
                                vertices[i].y = vector2fs[i].y;
                                // Debug.Log($"[PoxrUnity] UPxr_GetSpatialEntityPolygonInfo snapshotHandle={snapshotHandle}  vertices[{i}]={vertices[i]}");
                            }
                        }

                        Marshal.FreeHGlobal(polygonInfo.vertices);
                    }
                }

                return result;
            }
            public static ulong UPxr_GetSpatialMeshProviderHandle()
            {
                return Pxr_GetSpatialMeshProviderHandle();
            }
            
            public static PxrResult UPxr_GetSpatialMesh(ulong snapshotHandle, ulong entityHandle, ref PxrSpatialMeshInfo meshInfo)
            {
                var result = UPxr_GetSpatialMeshVerticesAndIndices(snapshotHandle, entityHandle, out var indices, out var vertices);
                if (result == PxrResult.SUCCESS)
                {
                    meshInfo.indices = indices;
                    meshInfo.vertices = vertices;
                    result = UPxr_GetSpatialSemantics(snapshotHandle, entityHandle, out var labels);
                    if (result == PxrResult.SUCCESS)
                    {
                        meshInfo.labels = labels;
                        result = UPxr_GetSpatialEntityLocationInfo(snapshotHandle, entityHandle, out var position, out var rotation);
                        if (result == PxrResult.SUCCESS)
                        {
                            meshInfo.position = position;
                            meshInfo.rotation = rotation;

                            return PxrResult.SUCCESS;
                        }
                        else
                        {
                            return result;
                        }
                    }
                    else
                    {
                        return result;
                    }
                }
                else
                {
                    return result;
                }
            }
      

            public static PxrResult UPxr_GetSpatialMeshVerticesAndIndices(ulong snapshotHandle, ulong entityHandle, out ushort[] indices, out Vector3[] vertices)
            {
                indices = Array.Empty<ushort>();
                vertices = Array.Empty<Vector3>();
                var getInfo = new XrSpatialEntityGetInfo
                {
                    type = XrStructureType.XR_TYPE_SPATIAL_ENTITY_TRIANGLE_MESH_GET_INFO,
                    entity = entityHandle,
                    componentType = PxrSceneComponentType.TriangleMesh
                };

                PxrTriangleMeshInfo meshInfo = new PxrTriangleMeshInfo()
                {
                    type = XrStructureType.XR_TYPE_SPATIAL_ENTITY_TRIANGLE_MESH_DATA,
                    vertexCapacityInput = 0,
                    vertexCountOutput = 0,
                    vertices = IntPtr.Zero,
                    indexCapacityInput = 0,
                    indexCountOutput = 0,
                    indices = IntPtr.Zero
                };
    
                var result = (PxrResult)Pxr_GetSpatialMeshVerticesAndIndices(snapshotHandle, ref getInfo, ref meshInfo);
                if (result == PxrResult.SUCCESS)
                {
                 
                    meshInfo.indexCapacityInput = meshInfo.indexCountOutput;
                    meshInfo.indices = Marshal.AllocHGlobal((int)meshInfo.indexCountOutput * Marshal.SizeOf(typeof(ushort)));
                    meshInfo.vertexCapacityInput = meshInfo.vertexCountOutput;
                    meshInfo.vertices = Marshal.AllocHGlobal((int)meshInfo.vertexCountOutput * Marshal.SizeOf(typeof(PxrVector3f)));
                   
                    result = (PxrResult)Pxr_GetSpatialMeshVerticesAndIndices(snapshotHandle, ref getInfo, ref meshInfo);
                    if (result == PxrResult.SUCCESS)
                    {
                       
                        indices = new ushort[meshInfo.indexCountOutput];
                        if (meshInfo.indexCountOutput > 0)
                        {
                            var indicesTmp = new short[meshInfo.indexCountOutput];
                            Marshal.Copy(meshInfo.indices, indicesTmp, 0, (int)meshInfo.indexCountOutput);
                            indices = indicesTmp.Select(l => (ushort)l).ToArray();

                            for (int i = 0; i < indices.Length; i += 3)
                            {
                                (indices[i + 1], indices[i + 2]) = (indices[i + 2], indices[i + 1]);
                            }
                        }
                        vertices = new Vector3[meshInfo.vertexCountOutput];
                        if (meshInfo.vertexCountOutput > 0)
                        {
                            IntPtr tempPtr = meshInfo.vertices;
                            for (int i = 0; i < meshInfo.vertexCountOutput; i++)
                            {
                                vertices[i] = Marshal.PtrToStructure<Vector3>(tempPtr);
                                tempPtr += Marshal.SizeOf(typeof(Vector3));
                            }

                            vertices = vertices.Select(v => new Vector3(v.x, v.y, -v.z)).ToArray();
                        }
                    }
                    
                    Marshal.FreeHGlobal(meshInfo.indices);
                    Marshal.FreeHGlobal(meshInfo.vertices);
                    return result;
                }
               
                return result;

            }

            public static PxrResult UPxr_GetSpatialSemantics(ulong snapshotHandle, ulong spatialEntityHandle, out PxrSemanticLabel[] labels)
            {
                labels = Array.Empty<PxrSemanticLabel>();
                XrSpatialEntityGetInfo getInfo = new XrSpatialEntityGetInfo
                {
                    type = XrStructureType.XR_TYPE_SPATIAL_ENTITY_SEMANTIC_GET_INFO,
                    entity = spatialEntityHandle,
                    componentType = PxrSceneComponentType.Semantic
                };
                XrSpatialEntitySemanticData semanticInfo = new XrSpatialEntitySemanticData()
                {
                    type = XrStructureType.XR_TYPE_SPATIAL_ENTITY_SEMANTIC_DATA,
                    semanticCapacityInput = 0,
                    semanticCountOutput = 0,
                    semanticLabels = IntPtr.Zero
                };

                var result = (PxrResult)Pxr_GetSpatialEntitySemanticInfo(snapshotHandle, ref getInfo, ref semanticInfo);
                if (result == PxrResult.SUCCESS)
                {
                    if (semanticInfo.semanticCountOutput > 0)
                    {
                        semanticInfo.semanticCapacityInput = semanticInfo.semanticCountOutput;
                    
                        semanticInfo.semanticLabels = Marshal.AllocHGlobal((int)semanticInfo.semanticCapacityInput*Marshal.SizeOf(typeof(int)));
                   
                        result = (PxrResult)Pxr_GetSpatialEntitySemanticInfo(snapshotHandle, ref getInfo, ref semanticInfo);
                        if (result == PxrResult.SUCCESS)
                        {
                            labels = new PxrSemanticLabel[semanticInfo.semanticCountOutput];
                            var sTmp = new int[semanticInfo.semanticCountOutput];
                            Marshal.Copy(semanticInfo.semanticLabels, sTmp, 0, (int)semanticInfo.semanticCountOutput);
                            labels = sTmp.Select(l => (PxrSemanticLabel)l).ToArray();
                            // label = (PxrSemanticLabel)Marshal.ReadInt32(semanticInfo.semanticLabels);
                            // Debug.Log("[PoxrUnity] UPxr_GetSpatialEntitySemanticInfo semanticLabels:"+label);
                        }

                        Marshal.FreeHGlobal(semanticInfo.semanticLabels);
                        return result;
                    }

                    return result;
                }

                return result;
            }

            public static void UPxr_AddOrUpdateMesh(PxrSpatialMeshInfo meshInfo)
            {
                byte[] temp = meshInfo.uuid.ToByteArray();
                var id1 = BitConverter.ToUInt64(temp, 0);
                var id2 = BitConverter.ToUInt64(temp, 8);
                var vertices = new NativeArray<Vector3>(meshInfo.vertices, Allocator.Persistent);
                var indices = new NativeArray<ushort>(meshInfo.indices, Allocator.Persistent);
                
                unsafe
                {
                    Pxr_AddOrUpdateMesh(id1, id2, meshInfo.vertices.Length, NativeArrayUnsafeUtility.GetUnsafeReadOnlyPtr(vertices), meshInfo.indices.Length, NativeArrayUnsafeUtility.GetUnsafeReadOnlyPtr(indices), meshInfo.position, meshInfo.rotation);
                }

                if (nativeMeshArrays.TryGetValue(meshInfo.uuid, out var nativeArrays))
                    nativeArrays.ForEach(x => x.Dispose());
                nativeMeshArrays[meshInfo.uuid] = new List<IDisposable> { vertices, indices};
            }
            public static void UPxr_RemoveMesh(Guid uuid)
            {
                byte[] temp = uuid.ToByteArray();
                var id1 = BitConverter.ToUInt64(temp, 0);
                var id2 = BitConverter.ToUInt64(temp, 8);
                Pxr_RemoveMesh(id1, id2);
                if (nativeMeshArrays.TryGetValue(uuid, out var nativeArrays))
                {
                    nativeArrays.ForEach(x => x.Dispose());
                    nativeMeshArrays.Remove(uuid);
                }
            }
            public static void UPxr_DisposeMesh()
            {
                foreach (var nativeArrays in nativeMeshArrays.Values)
                {
                    nativeArrays.ForEach(x => x.Dispose());
                }

                nativeMeshArrays.Clear();
                UPxr_ClearMeshes();
            }
            public static void UPxr_ClearMeshes()
            {
                Pxr_ClearMeshes();
            }
        }
    }
}