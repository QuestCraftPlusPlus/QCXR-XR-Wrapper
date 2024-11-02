using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.XR.OpenXR.Features.PICOSupport;
using UnityEngine;
using UnityEngine.XR;

namespace Unity.XR.PXR
{
    public delegate void XrEventDataBufferCallBack(ref XrEventDataBuffer dataBuffer);
    [StructLayout(LayoutKind.Sequential)]
    public struct XrEventDataBuffer
    {
        public XrStructureType type;
        public IntPtr next;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4000)]
        public byte[] data;
    };
    public enum PxrSenseDataProviderState
    {
        Initialized,
        Running,
        Stopped
    }
    public enum PxrFutureState
    {
        Pending = 1,
        Ready = 2
    }
    public enum PxrSemanticLabel
    {
        Unknown = 0,
        Floor,
        Ceiling,
        Wall,
        Door,
        Window,
        Opening,
        Table,
        Sofa,
        Chair,
        Human = 10,
        VirtualWall = 18,
    }
    public enum PxrSceneComponentType
    {
        Location = 0,
        Semantic,
        Box2D,
        Polygon,
        Box3D,
        TriangleMesh = 5,
    }

    public enum PxrSenseDataProviderType
    {
        SpatialAnchor,
        SceneCapture,
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct PxrSceneBox2D
    {
        public Vector2 offset;
        public XrExtent2Df extent;
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct PxrScenePolygon
    {
        public Vector2[] vertices;
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct PxrSceneBox3D
    {
        public Vector3 position;
        public Quaternion rotation;
        public Vector3 extent;
    }
    public struct PxrSceneComponentData
    {
        public Guid uuid;
        public Vector3 position;
        public Quaternion rotation;
        public PxrSemanticLabel label;
        public PxrSceneComponentType[] types;
        public PxrSceneBox3D box3D;
        public PxrSceneBox2D box2D;
        public PxrScenePolygon polygon;
    }
    public enum PxrMeshLod
    {
        Low,
        Medium,
        High
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct PxrSpatialMeshInfo
    {
        public Guid uuid;
        public MeshChangeState state;
        public Vector3 position;
        public Quaternion rotation;
        public ushort[] indices;
        public Vector3[] vertices;
        public PxrSemanticLabel[] labels;
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct PxrEventSenseDataProviderStateChanged
    {
        public ulong providerHandle;
        public PxrSenseDataProviderState newState;
    }
    public enum PxrResult
    {
        SUCCESS = 0,
        TIMEOUT_EXPIRED = 1,
        SESSION_LOSS_PENDING = 3,
        EVENT_UNAVAILABLE = 4,
        SPACE_BOUNDS_UNAVAILABLE = 7,
        SESSION_NOT_FOCUSED = 8,
        FRAME_DISCARDED = 9,
        ERROR_VALIDATION_FAILURE = -1,
        ERROR_RUNTIME_FAILURE = -2,
        ERROR_OUT_OF_MEMORY = -3,
        ERROR_API_VERSION_UNSUPPORTED = -4,
        ERROR_INITIALIZATION_FAILED = -6,
        ERROR_FUNCTION_UNSUPPORTED = -7,
        ERROR_FEATURE_UNSUPPORTED = -8,
        ERROR_EXTENSION_NOT_PRESENT = -9,
        ERROR_LIMIT_REACHED = -10,
        ERROR_SIZE_INSUFFICIENT = -11,
        ERROR_HANDLE_INVALID = -12,
        ERROR_INSTANCE_LOST = -13,
        ERROR_SESSION_RUNNING = -14,
        ERROR_SESSION_NOT_RUNNING = -16,
        ERROR_SESSION_LOST = -17,
        ERROR_SYSTEM_INVALID = -18,
        ERROR_PATH_INVALID = -19,
        ERROR_PATH_COUNT_EXCEEDED = -20,
        ERROR_PATH_FORMAT_INVALID = -21,
        ERROR_PATH_UNSUPPORTED = -22,
        ERROR_LAYER_INVALID = -23,
        ERROR_LAYER_LIMIT_EXCEEDED = -24,
        ERROR_SWAPCHAIN_RECT_INVALID = -25,
        ERROR_SWAPCHAIN_FORMAT_UNSUPPORTED = -26,
        ERROR_ACTION_TYPE_MISMATCH = -27,
        ERROR_SESSION_NOT_READY = -28,
        ERROR_SESSION_NOT_STOPPING = -29,
        ERROR_TIME_INVALID = -30,
        ERROR_REFERENCE_SPACE_UNSUPPORTED = -31,
        ERROR_FILE_ACCESS_ERROR = -32,
        ERROR_FILE_CONTENTS_INVALID = -33,
        ERROR_FORM_FACTOR_UNSUPPORTED = -34,
        ERROR_FORM_FACTOR_UNAVAILABLE = -35,
        ERROR_API_LAYER_NOT_PRESENT = -36,
        ERROR_CALL_ORDER_INVALID = -37,
        ERROR_GRAPHICS_DEVICE_INVALID = -38,
        ERROR_POSE_INVALID = -39,
        ERROR_INDEX_OUT_OF_RANGE = -40,
        ERROR_VIEW_CONFIGURATION_TYPE_UNSUPPORTED = -41,
        ERROR_ENVIRONMENT_BLEND_MODE_UNSUPPORTED = -42,
        ERROR_NAME_DUPLICATED = -44,
        ERROR_NAME_INVALID = -45,
        ERROR_ACTIONSET_NOT_ATTACHED = -46,
        ERROR_ACTIONSETS_ALREADY_ATTACHED = -47,
        ERROR_LOCALIZED_NAME_DUPLICATED = -48,
        ERROR_LOCALIZED_NAME_INVALID = -49,
        ERROR_GRAPHICS_REQUIREMENTS_CALL_MISSING = -50,
        ERROR_RUNTIME_UNAVAILABLE = -51,
        ERROR_EXTENSION_NOT_ENABLED = -800,

        ERROR_SPATIAL_LOCALIZATION_RUNNING = -1000,
        ERROR_SPATIAL_LOCALIZATION_NOT_RUNNING = -1001,
        ERROR_SPATIAL_MAP_CREATED = -1002,
        ERROR_SPATIAL_MAP_NOT_CREATED = -1003,
        ERROR_COMPONENT_NOT_SUPPORTED = -501,
        ERROR_COMPONENT_CONFLICT = -502,
        ERROR_COMPONENT_NOT_ADDED = -503,
        ERROR_COMPONENT_ADDED = -504,
        ERROR_ANCHOR_ENTITY_NOT_FOUND = -505,
        ERROR_TRACKING_STATE_INVALID = -506,

        ERROR_ANCHOR_SHARING_NETWORK_TIMEOUT = -601,
        ERROR_ANCHOR_SHARING_AUTHENTICATION_FAILURE = -602,
        ERROR_ANCHOR_SHARING_NETWORK_FAILURE = -603,
        ERROR_ANCHOR_SHARING_LOCALIZATION_FAIL = -604,
        ERROR_ANCHOR_SHARING_MAP_INSUFFICIENT = -605,


        ERROR_EXTENSION_DEPENDENCY_NOT_ENABLED = -1000710001,
        ERROR_PERMISSION_INSUFFICIENT = -1000710000,
        ERROR_ANDROID_THREAD_SETTINGS_ID_INVALID_KHR = -1000003000,
        ERROR_ANDROID_THREAD_SETTINGS_FAILURE_KHR = -1000003001,
        ERROR_CREATE_SPATIAL_ANCHOR_FAILED_MSFT = -1000039001,
        ERROR_SECONDARY_VIEW_CONFIGURATION_TYPE_NOT_ENABLED_MSFT = -1000053000,
        ERROR_CONTROLLER_MODEL_KEY_INVALID_MSFT = -1000055000,
        ERROR_REPROJECTION_MODE_UNSUPPORTED_MSFT = -1000066000,
        ERROR_COMPUTE_NEW_SCENE_NOT_COMPLETED_MSFT = -1000097000,
        ERROR_SCENE_COMPONENT_ID_INVALID_MSFT = -1000097001,
        ERROR_SCENE_COMPONENT_TYPE_MISMATCH_MSFT = -1000097002,
        ERROR_SCENE_MESH_BUFFER_ID_INVALID_MSFT = -1000097003,
        ERROR_SCENE_COMPUTE_FEATURE_INCOMPATIBLE_MSFT = -1000097004,
        ERROR_SCENE_COMPUTE_CONSISTENCY_MISMATCH_MSFT = -1000097005,
        ERROR_DISPLAY_REFRESH_RATE_UNSUPPORTED_FB = -1000101000,
        ERROR_COLOR_SPACE_UNSUPPORTED_FB = -1000108000,
        ERROR_SPACE_COMPONENT_NOT_SUPPORTED_FB = -1000113000,
        ERROR_SPACE_COMPONENT_NOT_ENABLED_FB = -1000113001,
        ERROR_SPACE_COMPONENT_STATUS_PENDING_FB = -1000113002,
        ERROR_SPACE_COMPONENT_STATUS_ALREADY_SET_FB = -1000113003,
        ERROR_UNEXPECTED_STATE_PASSTHROUGH_FB = -1000118000,
        ERROR_FEATURE_ALREADY_CREATED_PASSTHROUGH_FB = -1000118001,
        ERROR_FEATURE_REQUIRED_PASSTHROUGH_FB = -1000118002,
        ERROR_NOT_PERMITTED_PASSTHROUGH_FB = -1000118003,
        ERROR_INSUFFICIENT_RESOURCES_PASSTHROUGH_FB = -1000118004,
        ERROR_UNKNOWN_PASSTHROUGH_FB = -1000118050,
        ERROR_RENDER_MODEL_KEY_INVALID_FB = -1000119000,
        RENDER_MODEL_UNAVAILABLE_FB = 1000119020,
        ERROR_MARKER_NOT_TRACKED_VARJO = -1000124000,
        ERROR_MARKER_ID_INVALID_VARJO = -1000124001,
        ERROR_MARKER_DETECTOR_PERMISSION_DENIED_ML = -1000138000,
        ERROR_MARKER_DETECTOR_LOCATE_FAILED_ML = -1000138001,
        ERROR_MARKER_DETECTOR_INVALID_DATA_QUERY_ML = -1000138002,
        ERROR_MARKER_DETECTOR_INVALID_CREATE_INFO_ML = -1000138003,
        ERROR_MARKER_INVALID_ML = -1000138004,
        ERROR_LOCALIZATION_MAP_INCOMPATIBLE_ML = -1000139000,
        ERROR_LOCALIZATION_MAP_UNAVAILABLE_ML = -1000139001,
        ERROR_LOCALIZATION_MAP_FAIL_ML = -1000139002,
        ERROR_LOCALIZATION_MAP_IMPORT_EXPORT_PERMISSION_DENIED_ML = -1000139003,
        ERROR_LOCALIZATION_MAP_PERMISSION_DENIED_ML = -1000139004,
        ERROR_LOCALIZATION_MAP_ALREADY_EXISTS_ML = -1000139005,
        ERROR_LOCALIZATION_MAP_CANNOT_EXPORT_CLOUD_MAP_ML = -1000139006,
        ERROR_SPATIAL_ANCHOR_NAME_NOT_FOUND_MSFT = -1000142001,
        ERROR_SPATIAL_ANCHOR_NAME_INVALID_MSFT = -1000142002,
        SCENE_MARKER_DATA_NOT_STRING_MSFT = 1000147000,
        ERROR_SPACE_MAPPING_INSUFFICIENT_FB = -1000169000,
        ERROR_SPACE_LOCALIZATION_FAILED_FB = -1000169001,
        ERROR_SPACE_NETWORK_TIMEOUT_FB = -1000169002,
        ERROR_SPACE_NETWORK_REQUEST_FAILED_FB = -1000169003,
        ERROR_SPACE_CLOUD_STORAGE_DISABLED_FB = -1000169004,
        ERROR_PASSTHROUGH_COLOR_LUT_BUFFER_SIZE_MISMATCH_META = -1000266000,
        ENVIRONMENT_DEPTH_NOT_AVAILABLE_META = 1000291000,
        ERROR_HINT_ALREADY_SET_QCOM = -1000306000,
        ERROR_NOT_AN_ANCHOR_HTC = -1000319000,
        ERROR_SPACE_NOT_LOCATABLE_EXT = -1000429000,
        ERROR_PLANE_DETECTION_PERMISSION_DENIED_EXT = -1000429001,
        ERROR_FUTURE_PENDING_EXT = -1000469001,
        ERROR_FUTURE_INVALID_EXT = -1000469002,
        ERROR_EXTENSION_DEPENDENCY_NOT_ENABLED_KHR = ERROR_EXTENSION_DEPENDENCY_NOT_ENABLED,
        ERROR_PERMISSION_INSUFFICIENT_KHR = ERROR_PERMISSION_INSUFFICIENT,
    }

}