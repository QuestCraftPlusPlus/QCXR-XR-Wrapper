#include "pch-cpp.hpp"

#ifndef _MSC_VER
# include <alloca.h>
#else
# include <malloc.h>
#endif


#include <limits>


template <typename R, typename T1, typename T2>
struct InvokerFuncInvoker2;
template <typename R, typename T1, typename T2>
struct InvokerFuncInvoker2<R, T1*, T2*>
{
	static inline R Invoke (Il2CppMethodPointer methodPtr, const RuntimeMethod* method, void* obj, T1* p1, T2* p2)
	{
		R ret;
		void* params[2] = { p1, p2 };
		method->invoker_method(methodPtr, method, obj, params, &ret);
		return ret;
	}
};
template <typename R, typename T1, typename T2, typename T3>
struct InvokerFuncInvoker3;
template <typename R, typename T1, typename T2, typename T3>
struct InvokerFuncInvoker3<R, T1*, T2*, T3*>
{
	static inline R Invoke (Il2CppMethodPointer methodPtr, const RuntimeMethod* method, void* obj, T1* p1, T2* p2, T3* p3)
	{
		R ret;
		void* params[3] = { p1, p2, p3 };
		method->invoker_method(methodPtr, method, obj, params, &ret);
		return ret;
	}
};

// System.Delegate[]
struct DelegateU5BU5D_tC5AB7E8F745616680F337909D3A8E6C722CDF771;
// System.Single[]
struct SingleU5BU5D_t89DEFE97BCEDB5857010E79ECE0F52CF6E93B87C;
// System.AsyncCallback
struct AsyncCallback_t7FEF460CBDCFB9C5FA2EF776984778B9A4145F4C;
// UnityEngine.Component
struct Component_t39FBE53E5EFCF4409111FB22C15FF73717632EC3;
// Oculus.Interaction.HandGrab.SnapSurfaces.CylinderSurface
struct CylinderSurface_t071F3C35CED4F1296F988B93595DF4736770D4F7;
// Oculus.Interaction.HandGrab.SnapSurfaces.CylinderSurfaceData
struct CylinderSurfaceData_t8F949CD513A3C48F106518CF244120A2C884D700;
// System.Delegate
struct Delegate_t;
// System.DelegateData
struct DelegateData_t9B286B493293CD2D23A5B2B5EF0E5B1324C2B77E;
// UnityEngine.GameObject
struct GameObject_t76FEDD663AB33C991A9C9A23129337651094216F;
// Oculus.Interaction.HandGrab.HandGrabPose
struct HandGrabPose_t347682EF05B32B395F27F50DB38500405C17C633;
// Oculus.Interaction.HandGrab.HandPose
struct HandPose_t0B0F57FC79F74C9D20E54C6999A45D59FDDAC733;
// System.IAsyncResult
struct IAsyncResult_t7B9B5A0ECB35DCEC31B8A8122C37D687369253B5;
// Oculus.Interaction.HandGrab.SnapSurfaces.ISnapSurface
struct ISnapSurface_t749D26D4701D02220A4838119E68A6698225FF2E;
// System.Reflection.MethodInfo
struct MethodInfo_t;
// UnityEngine.MonoBehaviour
struct MonoBehaviour_t532A11E69716D348D8AA7F854AFCBFCB8AD17F71;
// UnityEngine.Object
struct Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C;
// Oculus.Interaction.HandGrab.SnapSurfaces.SphereSurface
struct SphereSurface_t7C6F8A614D76E8B3AE10EE722BBDB6774B96A30B;
// Oculus.Interaction.HandGrab.SnapSurfaces.SphereSurfaceData
struct SphereSurfaceData_tE8331CA08F2DB69533F08417636C96FFA01E7CC1;
// System.String
struct String_t;
// UnityEngine.Transform
struct Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1;
// System.Void
struct Void_t4861ACF8F4594C3437BB48B6E56783494B843915;
// Oculus.Interaction.HandGrab.SnapSurfaces.SnapSurfaceHelper/PoseCalculator
struct PoseCalculator_t74377137B14A0C57A79A02A9C33C5814E64F1839;

IL2CPP_EXTERN_C RuntimeClass* CylinderSurfaceData_t8F949CD513A3C48F106518CF244120A2C884D700_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Math_tEB65DE7CA8B083C412C969C92981C030865486CE_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Mathf_tE284D016E3B297B72311AAD9EB8F0E643F6A4682_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* PoseCalculator_t74377137B14A0C57A79A02A9C33C5814E64F1839_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* SingleU5BU5D_t89DEFE97BCEDB5857010E79ECE0F52CF6E93B87C_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* SphereSurfaceData_tE8331CA08F2DB69533F08417636C96FFA01E7CC1_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C const RuntimeMethod* Component_TryGetComponent_TisHandGrabPose_t347682EF05B32B395F27F50DB38500405C17C633_m48D0952AB02E5B8946E540747BC71058019E55C6_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* CylinderSurface_MinimalRotationPoseAtSurface_m948E72EC899B829FEBA889791D16BD6ED86875B9_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* CylinderSurface_MinimalTranslationPoseAtSurface_m842AD7D70ABC3A537FC4042FAAF5D2AEFAF4A95F_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* GameObject_AddComponent_TisCylinderSurface_t071F3C35CED4F1296F988B93595DF4736770D4F7_m3772C5F6A909279BE2427B29C4E3E9F0462E1031_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* GameObject_AddComponent_TisSphereSurface_t7C6F8A614D76E8B3AE10EE722BBDB6774B96A30B_m61F5ABE86B0C4374D3A35820F96D50228F1767C9_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* SphereSurface_MinimalRotationPoseAtSurface_mAA27B9D91F134707E946B7718F6B29EF1E5CEF1C_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* SphereSurface_MinimalTranslationPoseAtSurface_m888D23B10E2443C9E359760B9F4927D8D0B2A3D6_RuntimeMethod_var;
struct Delegate_t_marshaled_com;
struct Delegate_t_marshaled_pinvoke;

struct DelegateU5BU5D_tC5AB7E8F745616680F337909D3A8E6C722CDF771;
struct SingleU5BU5D_t89DEFE97BCEDB5857010E79ECE0F52CF6E93B87C;

IL2CPP_EXTERN_C_BEGIN
IL2CPP_EXTERN_C_END

#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif

// Oculus.Interaction.HandGrab.SnapSurfaces.SnapSurfaceHelper
struct SnapSurfaceHelper_tEB9342E6741A2F2AEE8399EB845A5D5803534928  : public RuntimeObject
{
};

// System.ValueType
struct ValueType_t6D9B272BD21782F0A9A14F2E41F85A50E97A986F  : public RuntimeObject
{
};
// Native definition for P/Invoke marshalling of System.ValueType
struct ValueType_t6D9B272BD21782F0A9A14F2E41F85A50E97A986F_marshaled_pinvoke
{
};
// Native definition for COM marshalling of System.ValueType
struct ValueType_t6D9B272BD21782F0A9A14F2E41F85A50E97A986F_marshaled_com
{
};

// System.Boolean
struct Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22 
{
	// System.Boolean System.Boolean::m_value
	bool ___m_value_0;
};

// System.Double
struct Double_tE150EF3D1D43DEE85D533810AB4C742307EEDE5F 
{
	// System.Double System.Double::m_value
	double ___m_value_0;
};

// System.Int32
struct Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C 
{
	// System.Int32 System.Int32::m_value
	int32_t ___m_value_0;
};

// System.IntPtr
struct IntPtr_t 
{
	// System.Void* System.IntPtr::m_value
	void* ___m_value_0;
};

// UnityEngine.Mathf
struct Mathf_tE284D016E3B297B72311AAD9EB8F0E643F6A4682 
{
	union
	{
		struct
		{
		};
		uint8_t Mathf_tE284D016E3B297B72311AAD9EB8F0E643F6A4682__padding[1];
	};
};

// Oculus.Interaction.HandGrab.PoseMeasureParameters
struct PoseMeasureParameters_tB95EB8BC83F81E86544854D1B13C04E88F2C0203 
{
	// System.Single Oculus.Interaction.HandGrab.PoseMeasureParameters::_maxDistance
	float ____maxDistance_0;
	// System.Single Oculus.Interaction.HandGrab.PoseMeasureParameters::_positionRotationWeight
	float ____positionRotationWeight_1;
};

// UnityEngine.Quaternion
struct Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 
{
	// System.Single UnityEngine.Quaternion::x
	float ___x_0;
	// System.Single UnityEngine.Quaternion::y
	float ___y_1;
	// System.Single UnityEngine.Quaternion::z
	float ___z_2;
	// System.Single UnityEngine.Quaternion::w
	float ___w_3;
};

// System.Single
struct Single_t4530F2FF86FCB0DC29F35385CA1BD21BE294761C 
{
	// System.Single System.Single::m_value
	float ___m_value_0;
};

// UnityEngine.Vector3
struct Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 
{
	// System.Single UnityEngine.Vector3::x
	float ___x_2;
	// System.Single UnityEngine.Vector3::y
	float ___y_3;
	// System.Single UnityEngine.Vector3::z
	float ___z_4;
};

// System.Void
struct Void_t4861ACF8F4594C3437BB48B6E56783494B843915 
{
	union
	{
		struct
		{
		};
		uint8_t Void_t4861ACF8F4594C3437BB48B6E56783494B843915__padding[1];
	};
};

// <PrivateImplementationDetails>/__StaticArrayInitTypeSize=12
struct __StaticArrayInitTypeSizeU3D12_t762F06A1B9DDEBDC6D8AF7B257311874AC29741D 
{
	union
	{
		struct
		{
			union
			{
			};
		};
		uint8_t __StaticArrayInitTypeSizeU3D12_t762F06A1B9DDEBDC6D8AF7B257311874AC29741D__padding[12];
	};
};

// <PrivateImplementationDetails>/__StaticArrayInitTypeSize=16
struct __StaticArrayInitTypeSizeU3D16_t27EFFA97FE987A68DFD348E1EA30D2A26985C121 
{
	union
	{
		struct
		{
			union
			{
			};
		};
		uint8_t __StaticArrayInitTypeSizeU3D16_t27EFFA97FE987A68DFD348E1EA30D2A26985C121__padding[16];
	};
};

// <PrivateImplementationDetails>/__StaticArrayInitTypeSize=17
struct __StaticArrayInitTypeSizeU3D17_t4959168BD7CF3BF86512C10948FAE301A1A2E0AB 
{
	union
	{
		struct
		{
			union
			{
			};
		};
		uint8_t __StaticArrayInitTypeSizeU3D17_t4959168BD7CF3BF86512C10948FAE301A1A2E0AB__padding[17];
	};
};

// <PrivateImplementationDetails>/__StaticArrayInitTypeSize=20
struct __StaticArrayInitTypeSizeU3D20_t0A8103C19F9B0776A39079FD4105AFF72E164001 
{
	union
	{
		struct
		{
			union
			{
			};
		};
		uint8_t __StaticArrayInitTypeSizeU3D20_t0A8103C19F9B0776A39079FD4105AFF72E164001__padding[20];
	};
};

// <PrivateImplementationDetails>/__StaticArrayInitTypeSize=68
struct __StaticArrayInitTypeSizeU3D68_t8553F026D1D75E1D4A7C21EEB23D8BCC30DCA6C5 
{
	union
	{
		struct
		{
			union
			{
			};
		};
		uint8_t __StaticArrayInitTypeSizeU3D68_t8553F026D1D75E1D4A7C21EEB23D8BCC30DCA6C5__padding[68];
	};
};

// <PrivateImplementationDetails>/__StaticArrayInitTypeSize=96
struct __StaticArrayInitTypeSizeU3D96_t2A9C6E4E95E1881E37A1DFD2ECB0E96121BAD3F9 
{
	union
	{
		struct
		{
			union
			{
			};
		};
		uint8_t __StaticArrayInitTypeSizeU3D96_t2A9C6E4E95E1881E37A1DFD2ECB0E96121BAD3F9__padding[96];
	};
};

// <PrivateImplementationDetails>
struct U3CPrivateImplementationDetailsU3E_t6884355282165D4E80B093E8026110DAC79B4BC8  : public RuntimeObject
{
};

// Oculus.Interaction.HandGrab.SnapSurfaces.CylinderSurfaceData
struct CylinderSurfaceData_t8F949CD513A3C48F106518CF244120A2C884D700  : public RuntimeObject
{
	// UnityEngine.Vector3 Oculus.Interaction.HandGrab.SnapSurfaces.CylinderSurfaceData::startPoint
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___startPoint_0;
	// UnityEngine.Vector3 Oculus.Interaction.HandGrab.SnapSurfaces.CylinderSurfaceData::endPoint
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___endPoint_1;
	// System.Single Oculus.Interaction.HandGrab.SnapSurfaces.CylinderSurfaceData::arcOffset
	float ___arcOffset_2;
	// System.Single Oculus.Interaction.HandGrab.SnapSurfaces.CylinderSurfaceData::arcLength
	float ___arcLength_3;
};

// System.Delegate
struct Delegate_t  : public RuntimeObject
{
	// System.IntPtr System.Delegate::method_ptr
	Il2CppMethodPointer ___method_ptr_0;
	// System.IntPtr System.Delegate::invoke_impl
	intptr_t ___invoke_impl_1;
	// System.Object System.Delegate::m_target
	RuntimeObject* ___m_target_2;
	// System.IntPtr System.Delegate::method
	intptr_t ___method_3;
	// System.IntPtr System.Delegate::delegate_trampoline
	intptr_t ___delegate_trampoline_4;
	// System.IntPtr System.Delegate::extra_arg
	intptr_t ___extra_arg_5;
	// System.IntPtr System.Delegate::method_code
	intptr_t ___method_code_6;
	// System.IntPtr System.Delegate::interp_method
	intptr_t ___interp_method_7;
	// System.IntPtr System.Delegate::interp_invoke_impl
	intptr_t ___interp_invoke_impl_8;
	// System.Reflection.MethodInfo System.Delegate::method_info
	MethodInfo_t* ___method_info_9;
	// System.Reflection.MethodInfo System.Delegate::original_method_info
	MethodInfo_t* ___original_method_info_10;
	// System.DelegateData System.Delegate::data
	DelegateData_t9B286B493293CD2D23A5B2B5EF0E5B1324C2B77E* ___data_11;
	// System.Boolean System.Delegate::method_is_virtual
	bool ___method_is_virtual_12;
};
// Native definition for P/Invoke marshalling of System.Delegate
struct Delegate_t_marshaled_pinvoke
{
	intptr_t ___method_ptr_0;
	intptr_t ___invoke_impl_1;
	Il2CppIUnknown* ___m_target_2;
	intptr_t ___method_3;
	intptr_t ___delegate_trampoline_4;
	intptr_t ___extra_arg_5;
	intptr_t ___method_code_6;
	intptr_t ___interp_method_7;
	intptr_t ___interp_invoke_impl_8;
	MethodInfo_t* ___method_info_9;
	MethodInfo_t* ___original_method_info_10;
	DelegateData_t9B286B493293CD2D23A5B2B5EF0E5B1324C2B77E* ___data_11;
	int32_t ___method_is_virtual_12;
};
// Native definition for COM marshalling of System.Delegate
struct Delegate_t_marshaled_com
{
	intptr_t ___method_ptr_0;
	intptr_t ___invoke_impl_1;
	Il2CppIUnknown* ___m_target_2;
	intptr_t ___method_3;
	intptr_t ___delegate_trampoline_4;
	intptr_t ___extra_arg_5;
	intptr_t ___method_code_6;
	intptr_t ___interp_method_7;
	intptr_t ___interp_invoke_impl_8;
	MethodInfo_t* ___method_info_9;
	MethodInfo_t* ___original_method_info_10;
	DelegateData_t9B286B493293CD2D23A5B2B5EF0E5B1324C2B77E* ___data_11;
	int32_t ___method_is_virtual_12;
};

// UnityEngine.Object
struct Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C  : public RuntimeObject
{
	// System.IntPtr UnityEngine.Object::m_CachedPtr
	intptr_t ___m_CachedPtr_0;
};
// Native definition for P/Invoke marshalling of UnityEngine.Object
struct Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_marshaled_pinvoke
{
	intptr_t ___m_CachedPtr_0;
};
// Native definition for COM marshalling of UnityEngine.Object
struct Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_marshaled_com
{
	intptr_t ___m_CachedPtr_0;
};

// UnityEngine.Pose
struct Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971 
{
	// UnityEngine.Vector3 UnityEngine.Pose::position
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___position_0;
	// UnityEngine.Quaternion UnityEngine.Pose::rotation
	Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 ___rotation_1;
};

// UnityEngine.Ray
struct Ray_t2B1742D7958DC05BDC3EFC7461D3593E1430DC00 
{
	// UnityEngine.Vector3 UnityEngine.Ray::m_Origin
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___m_Origin_0;
	// UnityEngine.Vector3 UnityEngine.Ray::m_Direction
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___m_Direction_1;
};

// Oculus.Interaction.HandGrab.SnapSurfaces.SphereSurfaceData
struct SphereSurfaceData_tE8331CA08F2DB69533F08417636C96FFA01E7CC1  : public RuntimeObject
{
	// UnityEngine.Vector3 Oculus.Interaction.HandGrab.SnapSurfaces.SphereSurfaceData::centre
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___centre_0;
};

// UnityEngine.Component
struct Component_t39FBE53E5EFCF4409111FB22C15FF73717632EC3  : public Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C
{
};

// UnityEngine.GameObject
struct GameObject_t76FEDD663AB33C991A9C9A23129337651094216F  : public Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C
{
};

// System.MulticastDelegate
struct MulticastDelegate_t  : public Delegate_t
{
	// System.Delegate[] System.MulticastDelegate::delegates
	DelegateU5BU5D_tC5AB7E8F745616680F337909D3A8E6C722CDF771* ___delegates_13;
};
// Native definition for P/Invoke marshalling of System.MulticastDelegate
struct MulticastDelegate_t_marshaled_pinvoke : public Delegate_t_marshaled_pinvoke
{
	Delegate_t_marshaled_pinvoke** ___delegates_13;
};
// Native definition for COM marshalling of System.MulticastDelegate
struct MulticastDelegate_t_marshaled_com : public Delegate_t_marshaled_com
{
	Delegate_t_marshaled_com** ___delegates_13;
};

// System.AsyncCallback
struct AsyncCallback_t7FEF460CBDCFB9C5FA2EF776984778B9A4145F4C  : public MulticastDelegate_t
{
};

// UnityEngine.Behaviour
struct Behaviour_t01970CFBBA658497AE30F311C447DB0440BAB7FA  : public Component_t39FBE53E5EFCF4409111FB22C15FF73717632EC3
{
};

// UnityEngine.Transform
struct Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1  : public Component_t39FBE53E5EFCF4409111FB22C15FF73717632EC3
{
};

// Oculus.Interaction.HandGrab.SnapSurfaces.SnapSurfaceHelper/PoseCalculator
struct PoseCalculator_t74377137B14A0C57A79A02A9C33C5814E64F1839  : public MulticastDelegate_t
{
};

// UnityEngine.MonoBehaviour
struct MonoBehaviour_t532A11E69716D348D8AA7F854AFCBFCB8AD17F71  : public Behaviour_t01970CFBBA658497AE30F311C447DB0440BAB7FA
{
};

// Oculus.Interaction.HandGrab.SnapSurfaces.CylinderSurface
struct CylinderSurface_t071F3C35CED4F1296F988B93595DF4736770D4F7  : public MonoBehaviour_t532A11E69716D348D8AA7F854AFCBFCB8AD17F71
{
	// Oculus.Interaction.HandGrab.SnapSurfaces.CylinderSurfaceData Oculus.Interaction.HandGrab.SnapSurfaces.CylinderSurface::_data
	CylinderSurfaceData_t8F949CD513A3C48F106518CF244120A2C884D700* ____data_4;
	// UnityEngine.Transform Oculus.Interaction.HandGrab.SnapSurfaces.CylinderSurface::_relativeTo
	Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* ____relativeTo_5;
	// UnityEngine.Transform Oculus.Interaction.HandGrab.SnapSurfaces.CylinderSurface::_gripPoint
	Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* ____gripPoint_6;
};

// Oculus.Interaction.HandGrab.HandGrabPose
struct HandGrabPose_t347682EF05B32B395F27F50DB38500405C17C633  : public MonoBehaviour_t532A11E69716D348D8AA7F854AFCBFCB8AD17F71
{
	// UnityEngine.Transform Oculus.Interaction.HandGrab.HandGrabPose::_relativeTo
	Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* ____relativeTo_4;
	// UnityEngine.MonoBehaviour Oculus.Interaction.HandGrab.HandGrabPose::_surface
	MonoBehaviour_t532A11E69716D348D8AA7F854AFCBFCB8AD17F71* ____surface_5;
	// Oculus.Interaction.HandGrab.SnapSurfaces.ISnapSurface Oculus.Interaction.HandGrab.HandGrabPose::_snapSurface
	RuntimeObject* ____snapSurface_6;
	// System.Boolean Oculus.Interaction.HandGrab.HandGrabPose::_usesHandPose
	bool ____usesHandPose_7;
	// Oculus.Interaction.HandGrab.HandPose Oculus.Interaction.HandGrab.HandGrabPose::_handPose
	HandPose_t0B0F57FC79F74C9D20E54C6999A45D59FDDAC733* ____handPose_8;
};

// Oculus.Interaction.HandGrab.SnapSurfaces.SphereSurface
struct SphereSurface_t7C6F8A614D76E8B3AE10EE722BBDB6774B96A30B  : public MonoBehaviour_t532A11E69716D348D8AA7F854AFCBFCB8AD17F71
{
	// Oculus.Interaction.HandGrab.SnapSurfaces.SphereSurfaceData Oculus.Interaction.HandGrab.SnapSurfaces.SphereSurface::_data
	SphereSurfaceData_tE8331CA08F2DB69533F08417636C96FFA01E7CC1* ____data_4;
	// UnityEngine.Transform Oculus.Interaction.HandGrab.SnapSurfaces.SphereSurface::_relativeTo
	Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* ____relativeTo_5;
	// UnityEngine.Transform Oculus.Interaction.HandGrab.SnapSurfaces.SphereSurface::_gripPoint
	Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* ____gripPoint_6;
};

// Oculus.Interaction.HandGrab.SnapSurfaces.SnapSurfaceHelper

// Oculus.Interaction.HandGrab.SnapSurfaces.SnapSurfaceHelper

// System.ValueType

// System.ValueType

// System.Boolean
struct Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_StaticFields
{
	// System.String System.Boolean::TrueString
	String_t* ___TrueString_5;
	// System.String System.Boolean::FalseString
	String_t* ___FalseString_6;
};

// System.Boolean

// System.Double

// System.Double

// System.Int32

// System.Int32

// System.IntPtr
struct IntPtr_t_StaticFields
{
	// System.IntPtr System.IntPtr::Zero
	intptr_t ___Zero_1;
};

// System.IntPtr

// UnityEngine.Mathf
struct Mathf_tE284D016E3B297B72311AAD9EB8F0E643F6A4682_StaticFields
{
	// System.Single UnityEngine.Mathf::Epsilon
	float ___Epsilon_0;
};

// UnityEngine.Mathf

// Oculus.Interaction.HandGrab.PoseMeasureParameters
struct PoseMeasureParameters_tB95EB8BC83F81E86544854D1B13C04E88F2C0203_StaticFields
{
	// Oculus.Interaction.HandGrab.PoseMeasureParameters Oculus.Interaction.HandGrab.PoseMeasureParameters::DEFAULT
	PoseMeasureParameters_tB95EB8BC83F81E86544854D1B13C04E88F2C0203 ___DEFAULT_2;
};

// Oculus.Interaction.HandGrab.PoseMeasureParameters

// UnityEngine.Quaternion
struct Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974_StaticFields
{
	// UnityEngine.Quaternion UnityEngine.Quaternion::identityQuaternion
	Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 ___identityQuaternion_4;
};

// UnityEngine.Quaternion

// System.Single

// System.Single

// UnityEngine.Vector3
struct Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2_StaticFields
{
	// UnityEngine.Vector3 UnityEngine.Vector3::zeroVector
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___zeroVector_5;
	// UnityEngine.Vector3 UnityEngine.Vector3::oneVector
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___oneVector_6;
	// UnityEngine.Vector3 UnityEngine.Vector3::upVector
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___upVector_7;
	// UnityEngine.Vector3 UnityEngine.Vector3::downVector
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___downVector_8;
	// UnityEngine.Vector3 UnityEngine.Vector3::leftVector
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___leftVector_9;
	// UnityEngine.Vector3 UnityEngine.Vector3::rightVector
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___rightVector_10;
	// UnityEngine.Vector3 UnityEngine.Vector3::forwardVector
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___forwardVector_11;
	// UnityEngine.Vector3 UnityEngine.Vector3::backVector
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___backVector_12;
	// UnityEngine.Vector3 UnityEngine.Vector3::positiveInfinityVector
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___positiveInfinityVector_13;
	// UnityEngine.Vector3 UnityEngine.Vector3::negativeInfinityVector
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___negativeInfinityVector_14;
};

// UnityEngine.Vector3

// System.Void

// System.Void

// <PrivateImplementationDetails>/__StaticArrayInitTypeSize=12

// <PrivateImplementationDetails>/__StaticArrayInitTypeSize=12

// <PrivateImplementationDetails>/__StaticArrayInitTypeSize=16

// <PrivateImplementationDetails>/__StaticArrayInitTypeSize=16

// <PrivateImplementationDetails>/__StaticArrayInitTypeSize=17

// <PrivateImplementationDetails>/__StaticArrayInitTypeSize=17

// <PrivateImplementationDetails>/__StaticArrayInitTypeSize=20

// <PrivateImplementationDetails>/__StaticArrayInitTypeSize=20

// <PrivateImplementationDetails>/__StaticArrayInitTypeSize=68

// <PrivateImplementationDetails>/__StaticArrayInitTypeSize=68

// <PrivateImplementationDetails>/__StaticArrayInitTypeSize=96

// <PrivateImplementationDetails>/__StaticArrayInitTypeSize=96

// <PrivateImplementationDetails>
struct U3CPrivateImplementationDetailsU3E_t6884355282165D4E80B093E8026110DAC79B4BC8_StaticFields
{
	// <PrivateImplementationDetails>/__StaticArrayInitTypeSize=68 <PrivateImplementationDetails>::0299EA23B40AFFB6F2D4C56761D939F88BEAEDFFC98799B1D0011E2FC867A388
	__StaticArrayInitTypeSizeU3D68_t8553F026D1D75E1D4A7C21EEB23D8BCC30DCA6C5 ___0299EA23B40AFFB6F2D4C56761D939F88BEAEDFFC98799B1D0011E2FC867A388_0;
	// <PrivateImplementationDetails>/__StaticArrayInitTypeSize=20 <PrivateImplementationDetails>::1085AB18045526E0E6BC49579C2783F82561DA676F694D26D184D6EB7F99118F
	__StaticArrayInitTypeSizeU3D20_t0A8103C19F9B0776A39079FD4105AFF72E164001 ___1085AB18045526E0E6BC49579C2783F82561DA676F694D26D184D6EB7F99118F_1;
	// <PrivateImplementationDetails>/__StaticArrayInitTypeSize=16 <PrivateImplementationDetails>::1C3D8119FF82FC2957242BBC5C8A184F08DADCE3CF113F282639E90D4E35BC0B
	__StaticArrayInitTypeSizeU3D16_t27EFFA97FE987A68DFD348E1EA30D2A26985C121 ___1C3D8119FF82FC2957242BBC5C8A184F08DADCE3CF113F282639E90D4E35BC0B_2;
	// <PrivateImplementationDetails>/__StaticArrayInitTypeSize=16 <PrivateImplementationDetails>::2E72A286F6E80D4ED2E83596D4A0AA21DCECB4DD925F30310EC73BCDF7BCFF08
	__StaticArrayInitTypeSizeU3D16_t27EFFA97FE987A68DFD348E1EA30D2A26985C121 ___2E72A286F6E80D4ED2E83596D4A0AA21DCECB4DD925F30310EC73BCDF7BCFF08_3;
	// <PrivateImplementationDetails>/__StaticArrayInitTypeSize=96 <PrivateImplementationDetails>::32D25725828EB444D141C82FB6F3FE5F46BC72A69AC4DEB861BA8130F035E2E7
	__StaticArrayInitTypeSizeU3D96_t2A9C6E4E95E1881E37A1DFD2ECB0E96121BAD3F9 ___32D25725828EB444D141C82FB6F3FE5F46BC72A69AC4DEB861BA8130F035E2E7_4;
	// <PrivateImplementationDetails>/__StaticArrayInitTypeSize=12 <PrivateImplementationDetails>::3CF966F20334243238BDC191F80FA740E98ACF8F5FDD0CA2DCCE683C1542EEDF
	__StaticArrayInitTypeSizeU3D12_t762F06A1B9DDEBDC6D8AF7B257311874AC29741D ___3CF966F20334243238BDC191F80FA740E98ACF8F5FDD0CA2DCCE683C1542EEDF_5;
	// <PrivateImplementationDetails>/__StaticArrayInitTypeSize=16 <PrivateImplementationDetails>::407885E61A69335134A1F85FD82A94E871508B8B6E33095F8E39FAEAC298C63E
	__StaticArrayInitTypeSizeU3D16_t27EFFA97FE987A68DFD348E1EA30D2A26985C121 ___407885E61A69335134A1F85FD82A94E871508B8B6E33095F8E39FAEAC298C63E_6;
	// <PrivateImplementationDetails>/__StaticArrayInitTypeSize=16 <PrivateImplementationDetails>::418D8378E48059C857D5F7CA8BE28422B288CAAD519525F1A1FF93F68F825B97
	__StaticArrayInitTypeSizeU3D16_t27EFFA97FE987A68DFD348E1EA30D2A26985C121 ___418D8378E48059C857D5F7CA8BE28422B288CAAD519525F1A1FF93F68F825B97_7;
	// <PrivateImplementationDetails>/__StaticArrayInitTypeSize=12 <PrivateImplementationDetails>::4F26A554B65395F540C074E9557877CF00BC194281240AB820E5297B8C499C70
	__StaticArrayInitTypeSizeU3D12_t762F06A1B9DDEBDC6D8AF7B257311874AC29741D ___4F26A554B65395F540C074E9557877CF00BC194281240AB820E5297B8C499C70_8;
	// <PrivateImplementationDetails>/__StaticArrayInitTypeSize=16 <PrivateImplementationDetails>::5B3F9EC0646608DAE43294162F92F82B97E7011A2BFA51A25FE477D18BDC6B21
	__StaticArrayInitTypeSizeU3D16_t27EFFA97FE987A68DFD348E1EA30D2A26985C121 ___5B3F9EC0646608DAE43294162F92F82B97E7011A2BFA51A25FE477D18BDC6B21_9;
	// <PrivateImplementationDetails>/__StaticArrayInitTypeSize=16 <PrivateImplementationDetails>::63859276EDC9733EDCD11B6E9B87C024B4519C893567720D95DA60C9850C22DE
	__StaticArrayInitTypeSizeU3D16_t27EFFA97FE987A68DFD348E1EA30D2A26985C121 ___63859276EDC9733EDCD11B6E9B87C024B4519C893567720D95DA60C9850C22DE_10;
	// <PrivateImplementationDetails>/__StaticArrayInitTypeSize=16 <PrivateImplementationDetails>::6FD4287A48C4D2CF873A476F8EA1781656383AE265F2F2FAE9C6B9F159863EFE
	__StaticArrayInitTypeSizeU3D16_t27EFFA97FE987A68DFD348E1EA30D2A26985C121 ___6FD4287A48C4D2CF873A476F8EA1781656383AE265F2F2FAE9C6B9F159863EFE_11;
	// <PrivateImplementationDetails>/__StaticArrayInitTypeSize=12 <PrivateImplementationDetails>::8CA6EE1043DEFCFD05AA29DEE581CBC519E783E414A687D7C26AC6070D3F6DEE
	__StaticArrayInitTypeSizeU3D12_t762F06A1B9DDEBDC6D8AF7B257311874AC29741D ___8CA6EE1043DEFCFD05AA29DEE581CBC519E783E414A687D7C26AC6070D3F6DEE_12;
	// <PrivateImplementationDetails>/__StaticArrayInitTypeSize=16 <PrivateImplementationDetails>::8E1614B69F720DC37A9ED6825E1DD7A6656F63DD9ABE7D0A48C911FD2DC418CE
	__StaticArrayInitTypeSizeU3D16_t27EFFA97FE987A68DFD348E1EA30D2A26985C121 ___8E1614B69F720DC37A9ED6825E1DD7A6656F63DD9ABE7D0A48C911FD2DC418CE_13;
	// <PrivateImplementationDetails>/__StaticArrayInitTypeSize=12 <PrivateImplementationDetails>::8EE3A1C9C508357E9D0EBCB0A6C6F8E01416BD7CDA0320AC080CEA649014F412
	__StaticArrayInitTypeSizeU3D12_t762F06A1B9DDEBDC6D8AF7B257311874AC29741D ___8EE3A1C9C508357E9D0EBCB0A6C6F8E01416BD7CDA0320AC080CEA649014F412_14;
	// <PrivateImplementationDetails>/__StaticArrayInitTypeSize=12 <PrivateImplementationDetails>::918234F629EBF0C84BFE41B60824833200105B6094AB357EE6A872A28F2BAD6A
	__StaticArrayInitTypeSizeU3D12_t762F06A1B9DDEBDC6D8AF7B257311874AC29741D ___918234F629EBF0C84BFE41B60824833200105B6094AB357EE6A872A28F2BAD6A_15;
	// <PrivateImplementationDetails>/__StaticArrayInitTypeSize=12 <PrivateImplementationDetails>::9290AB548294BA6BFDDD1E2EE079ABB3E02A463A085D6CFA86701AE11DAF4E85
	__StaticArrayInitTypeSizeU3D12_t762F06A1B9DDEBDC6D8AF7B257311874AC29741D ___9290AB548294BA6BFDDD1E2EE079ABB3E02A463A085D6CFA86701AE11DAF4E85_16;
	// <PrivateImplementationDetails>/__StaticArrayInitTypeSize=12 <PrivateImplementationDetails>::94AD2BDBB6455BBF8B60747E6C5D85F859F15DFEDAAE84DA39E8AF4D5F07BFC9
	__StaticArrayInitTypeSizeU3D12_t762F06A1B9DDEBDC6D8AF7B257311874AC29741D ___94AD2BDBB6455BBF8B60747E6C5D85F859F15DFEDAAE84DA39E8AF4D5F07BFC9_17;
	// <PrivateImplementationDetails>/__StaticArrayInitTypeSize=16 <PrivateImplementationDetails>::9C3B1F31D79675E772863CCCEEBB691C9A6C10718B45796B5DA322FB6C7A4881
	__StaticArrayInitTypeSizeU3D16_t27EFFA97FE987A68DFD348E1EA30D2A26985C121 ___9C3B1F31D79675E772863CCCEEBB691C9A6C10718B45796B5DA322FB6C7A4881_18;
	// <PrivateImplementationDetails>/__StaticArrayInitTypeSize=16 <PrivateImplementationDetails>::9D525C94DA0D9E0D4A9CE96909F6AE5E6C4DB27466EF98E0288AC9A99A07F07B
	__StaticArrayInitTypeSizeU3D16_t27EFFA97FE987A68DFD348E1EA30D2A26985C121 ___9D525C94DA0D9E0D4A9CE96909F6AE5E6C4DB27466EF98E0288AC9A99A07F07B_19;
	// <PrivateImplementationDetails>/__StaticArrayInitTypeSize=12 <PrivateImplementationDetails>::A56D6BBBE254A23749343FB727E7F348B719BC6314763D6A792843E2F7C466EE
	__StaticArrayInitTypeSizeU3D12_t762F06A1B9DDEBDC6D8AF7B257311874AC29741D ___A56D6BBBE254A23749343FB727E7F348B719BC6314763D6A792843E2F7C466EE_20;
	// <PrivateImplementationDetails>/__StaticArrayInitTypeSize=96 <PrivateImplementationDetails>::A634C1261CFDC51CA97439046CF09516F654F96F8A99317B3DE5965811495B3D
	__StaticArrayInitTypeSizeU3D96_t2A9C6E4E95E1881E37A1DFD2ECB0E96121BAD3F9 ___A634C1261CFDC51CA97439046CF09516F654F96F8A99317B3DE5965811495B3D_21;
	// <PrivateImplementationDetails>/__StaticArrayInitTypeSize=16 <PrivateImplementationDetails>::B5D565C4D932EDF37E8039156FB4F9391D01A5EA20FCD322DB107B5FB01AF5F3
	__StaticArrayInitTypeSizeU3D16_t27EFFA97FE987A68DFD348E1EA30D2A26985C121 ___B5D565C4D932EDF37E8039156FB4F9391D01A5EA20FCD322DB107B5FB01AF5F3_22;
	// <PrivateImplementationDetails>/__StaticArrayInitTypeSize=16 <PrivateImplementationDetails>::BAED642339816AFFB3FE8719792D0E4CE82F12DB72B7373D244EAA65445800FE
	__StaticArrayInitTypeSizeU3D16_t27EFFA97FE987A68DFD348E1EA30D2A26985C121 ___BAED642339816AFFB3FE8719792D0E4CE82F12DB72B7373D244EAA65445800FE_23;
	// <PrivateImplementationDetails>/__StaticArrayInitTypeSize=12 <PrivateImplementationDetails>::BCBC01A5036673E493422616677A83718EDFE475D3E938B1A879903FFB2A05A0
	__StaticArrayInitTypeSizeU3D12_t762F06A1B9DDEBDC6D8AF7B257311874AC29741D ___BCBC01A5036673E493422616677A83718EDFE475D3E938B1A879903FFB2A05A0_24;
	// <PrivateImplementationDetails>/__StaticArrayInitTypeSize=20 <PrivateImplementationDetails>::BEFD47F245DFF15422B7998AF33B5BFB32F8CDB3B32232065909F28F4DCD91A1
	__StaticArrayInitTypeSizeU3D20_t0A8103C19F9B0776A39079FD4105AFF72E164001 ___BEFD47F245DFF15422B7998AF33B5BFB32F8CDB3B32232065909F28F4DCD91A1_25;
	// <PrivateImplementationDetails>/__StaticArrayInitTypeSize=20 <PrivateImplementationDetails>::C1C0669C18711EC44EABC6FF99E1BCB996C6B892AF94287F5CC4B8774138D7AC
	__StaticArrayInitTypeSizeU3D20_t0A8103C19F9B0776A39079FD4105AFF72E164001 ___C1C0669C18711EC44EABC6FF99E1BCB996C6B892AF94287F5CC4B8774138D7AC_26;
	// <PrivateImplementationDetails>/__StaticArrayInitTypeSize=12 <PrivateImplementationDetails>::CE99AE045C8B2A2A8A58FD1A2120956E74E90322EEF45F7DFE1CA73EEFE655D4
	__StaticArrayInitTypeSizeU3D12_t762F06A1B9DDEBDC6D8AF7B257311874AC29741D ___CE99AE045C8B2A2A8A58FD1A2120956E74E90322EEF45F7DFE1CA73EEFE655D4_27;
	// <PrivateImplementationDetails>/__StaticArrayInitTypeSize=16 <PrivateImplementationDetails>::CFDD61EEBBC699DC56E78F04CF5CCE1896957BC5D29AF2B21468B62CD1E759C5
	__StaticArrayInitTypeSizeU3D16_t27EFFA97FE987A68DFD348E1EA30D2A26985C121 ___CFDD61EEBBC699DC56E78F04CF5CCE1896957BC5D29AF2B21468B62CD1E759C5_28;
	// <PrivateImplementationDetails>/__StaticArrayInitTypeSize=17 <PrivateImplementationDetails>::D421CA4F288D780319BC80684387DE61CF750142A8AC39A87240A6CB9261F552
	__StaticArrayInitTypeSizeU3D17_t4959168BD7CF3BF86512C10948FAE301A1A2E0AB ___D421CA4F288D780319BC80684387DE61CF750142A8AC39A87240A6CB9261F552_29;
	// <PrivateImplementationDetails>/__StaticArrayInitTypeSize=12 <PrivateImplementationDetails>::D8C9807AE9FA3FFE054D614675E4FD8DEE163272C453DEA596107ABE48E4412B
	__StaticArrayInitTypeSizeU3D12_t762F06A1B9DDEBDC6D8AF7B257311874AC29741D ___D8C9807AE9FA3FFE054D614675E4FD8DEE163272C453DEA596107ABE48E4412B_30;
	// <PrivateImplementationDetails>/__StaticArrayInitTypeSize=12 <PrivateImplementationDetails>::D9F6C3F18D26E171FCA762EBF56C34EA1CAAF992D3AF8F901BC55595F2BE125F
	__StaticArrayInitTypeSizeU3D12_t762F06A1B9DDEBDC6D8AF7B257311874AC29741D ___D9F6C3F18D26E171FCA762EBF56C34EA1CAAF992D3AF8F901BC55595F2BE125F_31;
	// <PrivateImplementationDetails>/__StaticArrayInitTypeSize=96 <PrivateImplementationDetails>::E0757700FBD2A0A111F78FE8D30FA1BEC4ABECC6B52669266B91208535F9647A
	__StaticArrayInitTypeSizeU3D96_t2A9C6E4E95E1881E37A1DFD2ECB0E96121BAD3F9 ___E0757700FBD2A0A111F78FE8D30FA1BEC4ABECC6B52669266B91208535F9647A_32;
	// <PrivateImplementationDetails>/__StaticArrayInitTypeSize=12 <PrivateImplementationDetails>::E54A423CAFA0DAA9A176E7D9CB32B41C39E2A6BF367AA389214A3FC6A1044F78
	__StaticArrayInitTypeSizeU3D12_t762F06A1B9DDEBDC6D8AF7B257311874AC29741D ___E54A423CAFA0DAA9A176E7D9CB32B41C39E2A6BF367AA389214A3FC6A1044F78_33;
};

// <PrivateImplementationDetails>

// Oculus.Interaction.HandGrab.SnapSurfaces.CylinderSurfaceData

// Oculus.Interaction.HandGrab.SnapSurfaces.CylinderSurfaceData

// System.Delegate

// System.Delegate

// UnityEngine.Object
struct Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_StaticFields
{
	// System.Int32 UnityEngine.Object::OffsetOfInstanceIDInCPlusPlusObject
	int32_t ___OffsetOfInstanceIDInCPlusPlusObject_1;
};

// UnityEngine.Object

// UnityEngine.Pose
struct Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971_StaticFields
{
	// UnityEngine.Pose UnityEngine.Pose::k_Identity
	Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971 ___k_Identity_2;
};

// UnityEngine.Pose

// UnityEngine.Ray

// UnityEngine.Ray

// Oculus.Interaction.HandGrab.SnapSurfaces.SphereSurfaceData

// Oculus.Interaction.HandGrab.SnapSurfaces.SphereSurfaceData

// UnityEngine.Component

// UnityEngine.Component

// UnityEngine.GameObject

// UnityEngine.GameObject

// System.MulticastDelegate

// System.MulticastDelegate

// System.AsyncCallback

// System.AsyncCallback

// UnityEngine.Behaviour

// UnityEngine.Behaviour

// UnityEngine.Transform

// UnityEngine.Transform

// Oculus.Interaction.HandGrab.SnapSurfaces.SnapSurfaceHelper/PoseCalculator

// Oculus.Interaction.HandGrab.SnapSurfaces.SnapSurfaceHelper/PoseCalculator

// UnityEngine.MonoBehaviour

// UnityEngine.MonoBehaviour

// Oculus.Interaction.HandGrab.SnapSurfaces.CylinderSurface

// Oculus.Interaction.HandGrab.SnapSurfaces.CylinderSurface

// Oculus.Interaction.HandGrab.HandGrabPose

// Oculus.Interaction.HandGrab.HandGrabPose

// Oculus.Interaction.HandGrab.SnapSurfaces.SphereSurface

// Oculus.Interaction.HandGrab.SnapSurfaces.SphereSurface
#ifdef __clang__
#pragma clang diagnostic pop
#endif
// System.Single[]
struct SingleU5BU5D_t89DEFE97BCEDB5857010E79ECE0F52CF6E93B87C  : public RuntimeArray
{
	ALIGN_FIELD (8) float m_Items[1];

	inline float GetAt(il2cpp_array_size_t index) const
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items[index];
	}
	inline float* GetAddressAt(il2cpp_array_size_t index)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items + index;
	}
	inline void SetAt(il2cpp_array_size_t index, float value)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		m_Items[index] = value;
	}
	inline float GetAtUnchecked(il2cpp_array_size_t index) const
	{
		return m_Items[index];
	}
	inline float* GetAddressAtUnchecked(il2cpp_array_size_t index)
	{
		return m_Items + index;
	}
	inline void SetAtUnchecked(il2cpp_array_size_t index, float value)
	{
		m_Items[index] = value;
	}
};
// System.Delegate[]
struct DelegateU5BU5D_tC5AB7E8F745616680F337909D3A8E6C722CDF771  : public RuntimeArray
{
	ALIGN_FIELD (8) Delegate_t* m_Items[1];

	inline Delegate_t* GetAt(il2cpp_array_size_t index) const
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items[index];
	}
	inline Delegate_t** GetAddressAt(il2cpp_array_size_t index)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items + index;
	}
	inline void SetAt(il2cpp_array_size_t index, Delegate_t* value)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		m_Items[index] = value;
		Il2CppCodeGenWriteBarrier((void**)m_Items + index, (void*)value);
	}
	inline Delegate_t* GetAtUnchecked(il2cpp_array_size_t index) const
	{
		return m_Items[index];
	}
	inline Delegate_t** GetAddressAtUnchecked(il2cpp_array_size_t index)
	{
		return m_Items + index;
	}
	inline void SetAtUnchecked(il2cpp_array_size_t index, Delegate_t* value)
	{
		m_Items[index] = value;
		Il2CppCodeGenWriteBarrier((void**)m_Items + index, (void*)value);
	}
};


// System.Boolean UnityEngine.Component::TryGetComponent<System.Object>(T&)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Component_TryGetComponent_TisRuntimeObject_m69D4ED21F14D49D805A2337F4332934CDA07DEDB_gshared (Component_t39FBE53E5EFCF4409111FB22C15FF73717632EC3* __this, RuntimeObject** ___0_component, const RuntimeMethod* method) ;
// T UnityEngine.GameObject::AddComponent<System.Object>()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* GameObject_AddComponent_TisRuntimeObject_m69B93700FACCF372F5753371C6E8FB780800B824_gshared (GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* __this, const RuntimeMethod* method) ;

// System.Void Oculus.Interaction.HandGrab.SnapSurfaces.CylinderSurfaceData::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void CylinderSurfaceData__ctor_mEEC8BFE66D1A3497A974E1CAB57012ADB7C8DB24 (CylinderSurfaceData_t8F949CD513A3C48F106518CF244120A2C884D700* __this, const RuntimeMethod* method) ;
// System.Object Oculus.Interaction.HandGrab.SnapSurfaces.CylinderSurfaceData::Clone()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* CylinderSurfaceData_Clone_m50C26B0C43F1C3F73F18D07B7E9CB1C4E5AA831A (CylinderSurfaceData_t8F949CD513A3C48F106518CF244120A2C884D700* __this, const RuntimeMethod* method) ;
// System.Void UnityEngine.Vector3::.ctor(System.Single,System.Single,System.Single)
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void Vector3__ctor_m376936E6B999EF1ECBE57D990A386303E2283DE0_inline (Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2* __this, float ___0_x, float ___1_y, float ___2_z, const RuntimeMethod* method) ;
// System.Void System.Object::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2 (RuntimeObject* __this, const RuntimeMethod* method) ;
// UnityEngine.Transform Oculus.Interaction.HandGrab.SnapSurfaces.CylinderSurface::get_GripPoint()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* CylinderSurface_get_GripPoint_m5EB373A3D69443440ACCAD9212CD470ECC0526D2_inline (CylinderSurface_t071F3C35CED4F1296F988B93595DF4736770D4F7* __this, const RuntimeMethod* method) ;
// System.Boolean UnityEngine.Object::op_Equality(UnityEngine.Object,UnityEngine.Object)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Object_op_Equality_mB6120F782D83091EF56A198FCEBCF066DB4A9605 (Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* ___0_x, Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* ___1_y, const RuntimeMethod* method) ;
// UnityEngine.Vector3 UnityEngine.Vector3::get_forward()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 Vector3_get_forward_mAA55A7034304DF8B2152EAD49AE779FC4CA2EB4A_inline (const RuntimeMethod* method) ;
// UnityEngine.Transform UnityEngine.Component::get_transform()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371 (Component_t39FBE53E5EFCF4409111FB22C15FF73717632EC3* __this, const RuntimeMethod* method) ;
// UnityEngine.Vector3 UnityEngine.Transform::get_position()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1 (Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* __this, const RuntimeMethod* method) ;
// UnityEngine.Vector3 Oculus.Interaction.HandGrab.SnapSurfaces.CylinderSurface::get_StartPoint()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 CylinderSurface_get_StartPoint_mF7CC10FE14B1ADE9745E95695C456FC2AF3D9A7B (CylinderSurface_t071F3C35CED4F1296F988B93595DF4736770D4F7* __this, const RuntimeMethod* method) ;
// UnityEngine.Vector3 UnityEngine.Vector3::op_Subtraction(UnityEngine.Vector3,UnityEngine.Vector3)
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 Vector3_op_Subtraction_mE42023FF80067CB44A1D4A27EB7CF2B24CABB828_inline (Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___0_a, Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___1_b, const RuntimeMethod* method) ;
// UnityEngine.Vector3 Oculus.Interaction.HandGrab.SnapSurfaces.CylinderSurface::get_Direction()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 CylinderSurface_get_Direction_mB62C7EB59706C2FD57685D22D0C98389F2ABEE5B (CylinderSurface_t071F3C35CED4F1296F988B93595DF4736770D4F7* __this, const RuntimeMethod* method) ;
// UnityEngine.Vector3 UnityEngine.Vector3::ProjectOnPlane(UnityEngine.Vector3,UnityEngine.Vector3)
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 Vector3_ProjectOnPlane_m68FB895F6E9FCC45676BB8B95857D091C0D78794_inline (Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___0_vector, Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___1_planeNormal, const RuntimeMethod* method) ;
// UnityEngine.Vector3 UnityEngine.Vector3::get_normalized()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 Vector3_get_normalized_m736BBF65D5CDA7A18414370D15B4DFCC1E466F07_inline (Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2* __this, const RuntimeMethod* method) ;
// System.Single Oculus.Interaction.HandGrab.SnapSurfaces.CylinderSurface::get_ArcOffset()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float CylinderSurface_get_ArcOffset_mC3B510DBF0216286DBBE3BDA64735BDE992D8804 (CylinderSurface_t071F3C35CED4F1296F988B93595DF4736770D4F7* __this, const RuntimeMethod* method) ;
// UnityEngine.Quaternion UnityEngine.Quaternion::AngleAxis(System.Single,UnityEngine.Vector3)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 Quaternion_AngleAxis_mF37022977B297E63AA70D69EA1C4C922FF22CC80 (float ___0_angle, Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___1_axis, const RuntimeMethod* method) ;
// UnityEngine.Vector3 Oculus.Interaction.HandGrab.SnapSurfaces.CylinderSurface::get_OriginalDir()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 CylinderSurface_get_OriginalDir_mD1944E5349E62DB0869436092A1FA89359177DF7 (CylinderSurface_t071F3C35CED4F1296F988B93595DF4736770D4F7* __this, const RuntimeMethod* method) ;
// UnityEngine.Vector3 UnityEngine.Quaternion::op_Multiply(UnityEngine.Quaternion,UnityEngine.Vector3)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 Quaternion_op_Multiply_mE1EBA73F9173432B50F8F17CE8190C5A7986FB8C (Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 ___0_rotation, Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___1_point, const RuntimeMethod* method) ;
// System.Single Oculus.Interaction.HandGrab.SnapSurfaces.CylinderSurface::get_ArcLength()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float CylinderSurface_get_ArcLength_m96967AE172A13B61C840D0286A318690D110DAF4 (CylinderSurface_t071F3C35CED4F1296F988B93595DF4736770D4F7* __this, const RuntimeMethod* method) ;
// UnityEngine.Vector3 Oculus.Interaction.HandGrab.SnapSurfaces.CylinderSurface::get_StartArcDir()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 CylinderSurface_get_StartArcDir_m25095FA9EF2F0D23F3282ED86433F298554F1FFB (CylinderSurface_t071F3C35CED4F1296F988B93595DF4736770D4F7* __this, const RuntimeMethod* method) ;
// UnityEngine.Transform Oculus.Interaction.HandGrab.SnapSurfaces.CylinderSurface::get_RelativeTo()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* CylinderSurface_get_RelativeTo_m3B6807164E9EE3C2F8A8688910AE9DAE16A3A612_inline (CylinderSurface_t071F3C35CED4F1296F988B93595DF4736770D4F7* __this, const RuntimeMethod* method) ;
// System.Boolean UnityEngine.Object::op_Inequality(UnityEngine.Object,UnityEngine.Object)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Object_op_Inequality_mD0BE578448EAA61948F25C32F8DD55AB1F778602 (Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* ___0_x, Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* ___1_y, const RuntimeMethod* method) ;
// UnityEngine.Vector3 UnityEngine.Transform::TransformPoint(UnityEngine.Vector3)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 Transform_TransformPoint_m05BFF013DB830D7BFE44A007703694AE1062EE44 (Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* __this, Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___0_position, const RuntimeMethod* method) ;
// UnityEngine.Vector3 UnityEngine.Transform::InverseTransformPoint(UnityEngine.Vector3)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 Transform_InverseTransformPoint_m18CD395144D9C78F30E15A5B82B6670E792DBA5D (Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* __this, Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___0_position, const RuntimeMethod* method) ;
// System.Single UnityEngine.Mathf::Repeat(System.Single,System.Single)
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR float Mathf_Repeat_m6F1560A163481BB311D685294E1B463C3E4EB3BA_inline (float ___0_t, float ___1_length, const RuntimeMethod* method) ;
// UnityEngine.Vector3 UnityEngine.Vector3::Project(UnityEngine.Vector3,UnityEngine.Vector3)
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 Vector3_Project_m85DF3CB297EC5E1A17BD6266FF65E86AB7372C9B_inline (Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___0_vector, Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___1_onNormal, const RuntimeMethod* method) ;
// UnityEngine.Vector3 UnityEngine.Vector3::op_Addition(UnityEngine.Vector3,UnityEngine.Vector3)
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 Vector3_op_Addition_m78C0EC70CB66E8DCAC225743D82B268DAEE92067_inline (Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___0_a, Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___1_b, const RuntimeMethod* method) ;
// System.Single UnityEngine.Vector3::Distance(UnityEngine.Vector3,UnityEngine.Vector3)
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR float Vector3_Distance_m2314DB9B8BD01157E013DF87BEA557375C7F9FF9_inline (Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___0_a, Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___1_b, const RuntimeMethod* method) ;
// UnityEngine.Vector3 Oculus.Interaction.HandGrab.SnapSurfaces.CylinderSurface::get_EndPoint()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 CylinderSurface_get_EndPoint_mE98BE2ABCCE9D6E1ABDD30C99B3DDF1212B638E4 (CylinderSurface_t071F3C35CED4F1296F988B93595DF4736770D4F7* __this, const RuntimeMethod* method) ;
// System.Single UnityEngine.Vector3::get_sqrMagnitude()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR float Vector3_get_sqrMagnitude_m43C27DEC47C4811FB30AB474FF2131A963B66FC8_inline (Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2* __this, const RuntimeMethod* method) ;
// System.Boolean UnityEngine.Object::op_Implicit(UnityEngine.Object)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Object_op_Implicit_m93896EF7D68FA113C42D3FE2BC6F661FC7EF514A (Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* ___0_exists, const RuntimeMethod* method) ;
// UnityEngine.Vector3 UnityEngine.Vector3::get_up()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 Vector3_get_up_m128AF3FDC820BF59D5DE86D973E7DE3F20C3AEBA_inline (const RuntimeMethod* method) ;
// UnityEngine.Vector3 UnityEngine.Transform::get_up()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 Transform_get_up_mE47A9D9D96422224DD0539AA5524DA5440145BB2 (Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* __this, const RuntimeMethod* method) ;
// System.Single UnityEngine.Vector3::get_magnitude()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR float Vector3_get_magnitude_mF0D6017E90B345F1F52D1CC564C640F1A847AF2D_inline (Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2* __this, const RuntimeMethod* method) ;
// System.Boolean UnityEngine.Vector3::op_Equality(UnityEngine.Vector3,UnityEngine.Vector3)
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR bool Vector3_op_Equality_mCDCBB8D2EDC3D3BF20F31A25ACB34705D352B479_inline (Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___0_lhs, Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___1_rhs, const RuntimeMethod* method) ;
// UnityEngine.Quaternion UnityEngine.Quaternion::LookRotation(UnityEngine.Vector3)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 Quaternion_LookRotation_mDB2CCA75B8E1AB98104F2A6E1A1EA57D0D1298D7 (Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___0_forward, const RuntimeMethod* method) ;
// UnityEngine.Quaternion UnityEngine.Quaternion::LookRotation(UnityEngine.Vector3,UnityEngine.Vector3)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 Quaternion_LookRotation_mFB02EDC8F733774DFAC3BEA4B4BB265A228F8307 (Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___0_forward, Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___1_upwards, const RuntimeMethod* method) ;
// System.Boolean UnityEngine.Component::TryGetComponent<Oculus.Interaction.HandGrab.HandGrabPose>(T&)
inline bool Component_TryGetComponent_TisHandGrabPose_t347682EF05B32B395F27F50DB38500405C17C633_m48D0952AB02E5B8946E540747BC71058019E55C6 (Component_t39FBE53E5EFCF4409111FB22C15FF73717632EC3* __this, HandGrabPose_t347682EF05B32B395F27F50DB38500405C17C633** ___0_component, const RuntimeMethod* method)
{
	return ((  bool (*) (Component_t39FBE53E5EFCF4409111FB22C15FF73717632EC3*, HandGrabPose_t347682EF05B32B395F27F50DB38500405C17C633**, const RuntimeMethod*))Component_TryGetComponent_TisRuntimeObject_m69D4ED21F14D49D805A2337F4332934CDA07DEDB_gshared)(__this, ___0_component, method);
}
// UnityEngine.Transform Oculus.Interaction.HandGrab.HandGrabPose::get_RelativeTo()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* HandGrabPose_get_RelativeTo_m5DFEA3BC65DF4B58C8BB75628AFB81A18B9399C4_inline (HandGrabPose_t347682EF05B32B395F27F50DB38500405C17C633* __this, const RuntimeMethod* method) ;
// UnityEngine.Quaternion UnityEngine.Transform::get_rotation()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 Transform_get_rotation_m32AF40CA0D50C797DA639A696F8EAEC7524C179C (Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* __this, const RuntimeMethod* method) ;
// UnityEngine.Quaternion UnityEngine.Quaternion::Inverse(UnityEngine.Quaternion)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 Quaternion_Inverse_mD9C060AC626A7B406F4984AC98F8358DC89EF512 (Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 ___0_rotation, const RuntimeMethod* method) ;
// UnityEngine.Pose Oculus.Interaction.PoseUtils::MirrorPoseRotation(UnityEngine.Pose&,UnityEngine.Vector3,UnityEngine.Vector3)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971 PoseUtils_MirrorPoseRotation_m0F4E6DD5661B752CE896089B11F9483DF19E5C5F (Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* ___0_pose, Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___1_normal, Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___2_tangent, const RuntimeMethod* method) ;
// System.Void Oculus.Interaction.HandGrab.SnapSurfaces.SnapSurfaceHelper/PoseCalculator::.ctor(System.Object,System.IntPtr)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void PoseCalculator__ctor_mCB0A713E75C96AE4D9C311D91EBBE4A016F8763B (PoseCalculator_t74377137B14A0C57A79A02A9C33C5814E64F1839* __this, RuntimeObject* ___0_object, intptr_t ___1_method, const RuntimeMethod* method) ;
// System.Single Oculus.Interaction.HandGrab.SnapSurfaces.SnapSurfaceHelper::CalculateBestPoseAtSurface(UnityEngine.Pose&,UnityEngine.Pose&,UnityEngine.Pose&,Oculus.Interaction.HandGrab.PoseMeasureParameters&,Oculus.Interaction.HandGrab.SnapSurfaces.SnapSurfaceHelper/PoseCalculator,Oculus.Interaction.HandGrab.SnapSurfaces.SnapSurfaceHelper/PoseCalculator)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float SnapSurfaceHelper_CalculateBestPoseAtSurface_m59B6D35DAD5E611E28D9D44A33DF752471144B3E (Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* ___0_desiredPose, Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* ___1_snapPose, Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* ___2_bestPose, PoseMeasureParameters_tB95EB8BC83F81E86544854D1B13C04E88F2C0203* ___3_scoringModifier, PoseCalculator_t74377137B14A0C57A79A02A9C33C5814E64F1839* ___4_minimalTranslationPoseCalculator, PoseCalculator_t74377137B14A0C57A79A02A9C33C5814E64F1839* ___5_minimalRotationPoseCalculator, const RuntimeMethod* method) ;
// T UnityEngine.GameObject::AddComponent<Oculus.Interaction.HandGrab.SnapSurfaces.CylinderSurface>()
inline CylinderSurface_t071F3C35CED4F1296F988B93595DF4736770D4F7* GameObject_AddComponent_TisCylinderSurface_t071F3C35CED4F1296F988B93595DF4736770D4F7_m3772C5F6A909279BE2427B29C4E3E9F0462E1031 (GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* __this, const RuntimeMethod* method)
{
	return ((  CylinderSurface_t071F3C35CED4F1296F988B93595DF4736770D4F7* (*) (GameObject_t76FEDD663AB33C991A9C9A23129337651094216F*, const RuntimeMethod*))GameObject_AddComponent_TisRuntimeObject_m69B93700FACCF372F5753371C6E8FB780800B824_gshared)(__this, method);
}
// Oculus.Interaction.HandGrab.SnapSurfaces.CylinderSurfaceData Oculus.Interaction.HandGrab.SnapSurfaces.CylinderSurfaceData::Mirror()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR CylinderSurfaceData_t8F949CD513A3C48F106518CF244120A2C884D700* CylinderSurfaceData_Mirror_m5E5FF75620F1DC159CDD6B025D302D5CE6ADE0E0 (CylinderSurfaceData_t8F949CD513A3C48F106518CF244120A2C884D700* __this, const RuntimeMethod* method) ;
// System.Void Oculus.Interaction.HandGrab.SnapSurfaces.CylinderSurface::set_Data(Oculus.Interaction.HandGrab.SnapSurfaces.CylinderSurfaceData)
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void CylinderSurface_set_Data_m87230DD8507A149A857F965B1BE004A62ADBC500_inline (CylinderSurface_t071F3C35CED4F1296F988B93595DF4736770D4F7* __this, CylinderSurfaceData_t8F949CD513A3C48F106518CF244120A2C884D700* ___0_value, const RuntimeMethod* method) ;
// System.Single Oculus.Interaction.HandGrab.SnapSurfaces.CylinderSurface::get_Height()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float CylinderSurface_get_Height_mAC52DA1EAA6A0AFB914D0C107FD2F4B038639721 (CylinderSurface_t071F3C35CED4F1296F988B93595DF4736770D4F7* __this, const RuntimeMethod* method) ;
// UnityEngine.Vector3 UnityEngine.Vector3::op_Multiply(UnityEngine.Vector3,System.Single)
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 Vector3_op_Multiply_m87BA7C578F96C8E49BB07088DAAC4649F83B0353_inline (Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___0_a, float ___1_d, const RuntimeMethod* method) ;
// System.Single UnityEngine.Vector3::Dot(UnityEngine.Vector3,UnityEngine.Vector3)
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR float Vector3_Dot_mBB86BB940AA0A32FA7D3C02AC42E5BC7095A5D52_inline (Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___0_lhs, Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___1_rhs, const RuntimeMethod* method) ;
// UnityEngine.Vector3 UnityEngine.Vector3::get_zero()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 Vector3_get_zero_m0C1249C3F25B1C70EAD3CC8B31259975A457AE39_inline (const RuntimeMethod* method) ;
// System.Single UnityEngine.Vector3::SignedAngle(UnityEngine.Vector3,UnityEngine.Vector3,UnityEngine.Vector3)
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR float Vector3_SignedAngle_m76C77F9D7BAF5969FA5B7500ED2D5FF9F9FA4153_inline (Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___0_from, Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___1_to, Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___2_axis, const RuntimeMethod* method) ;
// UnityEngine.Vector3 Oculus.Interaction.HandGrab.SnapSurfaces.CylinderSurface::get_EndArcDir()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 CylinderSurface_get_EndArcDir_m1782B93B9682F9037311B58FAD20295EA7A63CC9 (CylinderSurface_t071F3C35CED4F1296F988B93595DF4736770D4F7* __this, const RuntimeMethod* method) ;
// System.Single Oculus.Interaction.HandGrab.SnapSurfaces.CylinderSurface::get_Radius()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float CylinderSurface_get_Radius_mC66E163DBCC270B93198392EC52EC9DC40BE1559 (CylinderSurface_t071F3C35CED4F1296F988B93595DF4736770D4F7* __this, const RuntimeMethod* method) ;
// UnityEngine.Vector3 UnityEngine.Ray::get_origin()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 Ray_get_origin_m97604A8F180316A410DCD77B7D74D04522FA1BA6 (Ray_t2B1742D7958DC05BDC3EFC7461D3593E1430DC00* __this, const RuntimeMethod* method) ;
// UnityEngine.Vector3 UnityEngine.Ray::get_direction()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 Ray_get_direction_m21C2D22D3BD4A683BD4DC191AB22DD05F5EC2086 (Ray_t2B1742D7958DC05BDC3EFC7461D3593E1430DC00* __this, const RuntimeMethod* method) ;
// System.Single UnityEngine.Mathf::Max(System.Single[])
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR float Mathf_Max_m870B77F6A5225EC21D9778043F987B9B17D08F6B_inline (SingleU5BU5D_t89DEFE97BCEDB5857010E79ECE0F52CF6E93B87C* ___0_values, const RuntimeMethod* method) ;
// UnityEngine.Vector3 Oculus.Interaction.HandGrab.SnapSurfaces.CylinderSurface::NearestPointInSurface(UnityEngine.Vector3)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 CylinderSurface_NearestPointInSurface_mC5B4F8F1CA761511DC0E2164F306232C7996B7EE (CylinderSurface_t071F3C35CED4F1296F988B93595DF4736770D4F7* __this, Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___0_targetPosition, const RuntimeMethod* method) ;
// System.Void UnityEngine.Pose::.ctor(UnityEngine.Vector3,UnityEngine.Quaternion)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Pose__ctor_m15CA45808A2BBF1956E836D22C387FAB80BED051 (Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* __this, Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___0_position, Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 ___1_rotation, const RuntimeMethod* method) ;
// UnityEngine.Pose Oculus.Interaction.HandGrab.SnapSurfaces.CylinderSurface::MinimalTranslationPoseAtSurface(UnityEngine.Pose&,UnityEngine.Pose&)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971 CylinderSurface_MinimalTranslationPoseAtSurface_m842AD7D70ABC3A537FC4042FAAF5D2AEFAF4A95F (CylinderSurface_t071F3C35CED4F1296F988B93595DF4736770D4F7* __this, Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* ___0_userPose, Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* ___1_snapPose, const RuntimeMethod* method) ;
// UnityEngine.Quaternion UnityEngine.Quaternion::op_Multiply(UnityEngine.Quaternion,UnityEngine.Quaternion)
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 Quaternion_op_Multiply_mCB375FCCC12A2EC8F9EB824A1BFB4453B58C2012_inline (Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 ___0_lhs, Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 ___1_rhs, const RuntimeMethod* method) ;
// UnityEngine.Quaternion Oculus.Interaction.HandGrab.SnapSurfaces.CylinderSurface::get_Rotation()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 CylinderSurface_get_Rotation_mC2C8899BA8E598D1A45709C6700BBE4698EF2F41 (CylinderSurface_t071F3C35CED4F1296F988B93595DF4736770D4F7* __this, const RuntimeMethod* method) ;
// UnityEngine.Vector3 Oculus.Interaction.HandGrab.SnapSurfaces.CylinderSurface::PointAltitude(UnityEngine.Vector3)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 CylinderSurface_PointAltitude_m93AC9011BF0191EBC3A70CE06E0039F179E161F5 (CylinderSurface_t071F3C35CED4F1296F988B93595DF4736770D4F7* __this, Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___0_point, const RuntimeMethod* method) ;
// UnityEngine.Quaternion Oculus.Interaction.HandGrab.SnapSurfaces.CylinderSurface::CalculateRotationOffset(UnityEngine.Vector3)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 CylinderSurface_CalculateRotationOffset_m1A7CF0FD4877A053DCD3CCACF2AE5883E8E95574 (CylinderSurface_t071F3C35CED4F1296F988B93595DF4736770D4F7* __this, Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___0_surfacePoint, const RuntimeMethod* method) ;
// UnityEngine.Quaternion UnityEngine.Quaternion::FromToRotation(UnityEngine.Vector3,UnityEngine.Vector3)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 Quaternion_FromToRotation_mCB3100F93637E72455388B901C36EF8A25DFDB9A (Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___0_fromDirection, Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___1_toDirection, const RuntimeMethod* method) ;
// System.Void Oculus.Interaction.HandGrab.SnapSurfaces.CylinderSurface::InjectData(Oculus.Interaction.HandGrab.SnapSurfaces.CylinderSurfaceData)
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void CylinderSurface_InjectData_m4466DEF60883B115512651A9AABA2084187E6702_inline (CylinderSurface_t071F3C35CED4F1296F988B93595DF4736770D4F7* __this, CylinderSurfaceData_t8F949CD513A3C48F106518CF244120A2C884D700* ___0_data, const RuntimeMethod* method) ;
// System.Void Oculus.Interaction.HandGrab.SnapSurfaces.CylinderSurface::InjectRelativeTo(UnityEngine.Transform)
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void CylinderSurface_InjectRelativeTo_m656BD54F4AF5E9937B458F16AB710AC356B2EE41_inline (CylinderSurface_t071F3C35CED4F1296F988B93595DF4736770D4F7* __this, Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* ___0_relativeTo, const RuntimeMethod* method) ;
// System.Void Oculus.Interaction.HandGrab.SnapSurfaces.CylinderSurface::InjectGripPoint(UnityEngine.Transform)
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void CylinderSurface_InjectGripPoint_mD9FE7A069861670C8803680CC3385866A22908EB_inline (CylinderSurface_t071F3C35CED4F1296F988B93595DF4736770D4F7* __this, Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* ___0_gripPoint, const RuntimeMethod* method) ;
// System.Void UnityEngine.MonoBehaviour::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void MonoBehaviour__ctor_m592DB0105CA0BC97AA1C5F4AD27B12D68A3B7C1E (MonoBehaviour_t532A11E69716D348D8AA7F854AFCBFCB8AD17F71* __this, const RuntimeMethod* method) ;
// System.Single Oculus.Interaction.HandGrab.SnapSurfaces.CylinderSurface::CalculateBestPoseAtSurface(UnityEngine.Pose&,UnityEngine.Pose&,UnityEngine.Pose&,Oculus.Interaction.HandGrab.PoseMeasureParameters&)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float CylinderSurface_CalculateBestPoseAtSurface_m00B0699D91CA4B6C7E1F61A571910FCA3558853B (CylinderSurface_t071F3C35CED4F1296F988B93595DF4736770D4F7* __this, Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* ___0_targetPose, Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* ___1_reference, Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* ___2_bestPose, PoseMeasureParameters_tB95EB8BC83F81E86544854D1B13C04E88F2C0203* ___3_scoringModifier, const RuntimeMethod* method) ;
// System.Boolean Oculus.Interaction.HandGrab.SnapSurfaces.CylinderSurface::CalculateBestPoseAtSurface(UnityEngine.Ray,UnityEngine.Pose&,UnityEngine.Pose&)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool CylinderSurface_CalculateBestPoseAtSurface_m1EDF5AF042F7CDEDAF7EB8EFD9385E632CBCEF82 (CylinderSurface_t071F3C35CED4F1296F988B93595DF4736770D4F7* __this, Ray_t2B1742D7958DC05BDC3EFC7461D3593E1430DC00 ___0_targetRay, Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* ___1_recordedPose, Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* ___2_bestPose, const RuntimeMethod* method) ;
// UnityEngine.Pose Oculus.Interaction.HandGrab.SnapSurfaces.CylinderSurface::MirrorPose(UnityEngine.Pose&)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971 CylinderSurface_MirrorPose_mA421C34FD2EAA278F4FCD31596B60ACC88934CA6 (CylinderSurface_t071F3C35CED4F1296F988B93595DF4736770D4F7* __this, Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* ___0_pose, const RuntimeMethod* method) ;
// UnityEngine.Pose Oculus.Interaction.HandGrab.SnapSurfaces.SnapSurfaceHelper/PoseCalculator::Invoke(UnityEngine.Pose& modreq(System.Runtime.InteropServices.InAttribute),UnityEngine.Pose& modreq(System.Runtime.InteropServices.InAttribute))
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971 PoseCalculator_Invoke_m3E4562A6120025438594E5257CFC28EBE2489014_inline (PoseCalculator_t74377137B14A0C57A79A02A9C33C5814E64F1839* __this, Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* ___0_desiredPose, Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* ___1_snapPose, const RuntimeMethod* method) ;
// System.Single Oculus.Interaction.HandGrab.PoseMeasureParameters::get_MaxDistance()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR float PoseMeasureParameters_get_MaxDistance_m7F443662F2C07ECBDFA621D40147E782266495EE_inline (PoseMeasureParameters_tB95EB8BC83F81E86544854D1B13C04E88F2C0203* __this, const RuntimeMethod* method) ;
// UnityEngine.Pose Oculus.Interaction.HandGrab.SnapSurfaces.SnapSurfaceHelper::SelectBestPose(UnityEngine.Pose&,UnityEngine.Pose&,UnityEngine.Pose&,Oculus.Interaction.HandGrab.PoseMeasureParameters,System.Single&)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971 SnapSurfaceHelper_SelectBestPose_m3C63764B6C1716502F14229D9533FF41136BAEC0 (Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* ___0_a, Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* ___1_b, Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* ___2_reference, PoseMeasureParameters_tB95EB8BC83F81E86544854D1B13C04E88F2C0203 ___3_scoringModifier, float* ___4_bestScore, const RuntimeMethod* method) ;
// System.Single Oculus.Interaction.PoseUtils::RotationalSimilarity(UnityEngine.Quaternion&,UnityEngine.Quaternion&)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float PoseUtils_RotationalSimilarity_m7863C9DC052D1B76B17754929FB2F31E06CFE0BD (Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974* ___0_from, Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974* ___1_to, const RuntimeMethod* method) ;
// System.Single Oculus.Interaction.PoseUtils::Similarity(UnityEngine.Pose&,UnityEngine.Pose&,Oculus.Interaction.HandGrab.PoseMeasureParameters)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float PoseUtils_Similarity_m1E048E90FD665EADAB792568C5B3A34543B184B1 (Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* ___0_from, Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* ___1_to, PoseMeasureParameters_tB95EB8BC83F81E86544854D1B13C04E88F2C0203 ___2_scoringModifier, const RuntimeMethod* method) ;
// System.Void Oculus.Interaction.HandGrab.SnapSurfaces.SphereSurfaceData::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void SphereSurfaceData__ctor_mC4A3CEB2FC052A2580F50116E0B4C351ABBA389C (SphereSurfaceData_tE8331CA08F2DB69533F08417636C96FFA01E7CC1* __this, const RuntimeMethod* method) ;
// System.Object Oculus.Interaction.HandGrab.SnapSurfaces.SphereSurfaceData::Clone()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* SphereSurfaceData_Clone_m901F6F5CF4062FC6C1AEA3089478B857002CB7DB (SphereSurfaceData_tE8331CA08F2DB69533F08417636C96FFA01E7CC1* __this, const RuntimeMethod* method) ;
// UnityEngine.Transform Oculus.Interaction.HandGrab.SnapSurfaces.SphereSurface::get_RelativeTo()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* SphereSurface_get_RelativeTo_m012CCC88748350E90BC484526CB65F7AD94132CC_inline (SphereSurface_t7C6F8A614D76E8B3AE10EE722BBDB6774B96A30B* __this, const RuntimeMethod* method) ;
// UnityEngine.Transform Oculus.Interaction.HandGrab.SnapSurfaces.SphereSurface::get_GripPoint()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* SphereSurface_get_GripPoint_m5E0F7198EDAC70BECB20A3B861CD7C854324ABA6_inline (SphereSurface_t7C6F8A614D76E8B3AE10EE722BBDB6774B96A30B* __this, const RuntimeMethod* method) ;
// UnityEngine.Vector3 Oculus.Interaction.HandGrab.SnapSurfaces.SphereSurface::get_Centre()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 SphereSurface_get_Centre_mDB3654A871802C00C70C60409AA15AD8F7EAF00B (SphereSurface_t7C6F8A614D76E8B3AE10EE722BBDB6774B96A30B* __this, const RuntimeMethod* method) ;
// UnityEngine.Vector3 Oculus.Interaction.HandGrab.SnapSurfaces.SphereSurface::get_Direction()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 SphereSurface_get_Direction_mEF366B8EE0B94083D54C4E3716AAEAF02E9AD9F5 (SphereSurface_t7C6F8A614D76E8B3AE10EE722BBDB6774B96A30B* __this, const RuntimeMethod* method) ;
// UnityEngine.Vector3 UnityEngine.Transform::get_forward()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 Transform_get_forward_mFCFACF7165FDAB21E80E384C494DF278386CEE2F (Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* __this, const RuntimeMethod* method) ;
// UnityEngine.Vector3 UnityEngine.Vector3::Cross(UnityEngine.Vector3,UnityEngine.Vector3)
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 Vector3_Cross_mF93A280558BCE756D13B6CC5DCD7DE8A43148987_inline (Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___0_lhs, Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___1_rhs, const RuntimeMethod* method) ;
// System.Single Oculus.Interaction.HandGrab.SnapSurfaces.SphereSurface::get_Radius()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float SphereSurface_get_Radius_m3A32B797BC6684468CC1BA7C6E35DD04100288DF (SphereSurface_t7C6F8A614D76E8B3AE10EE722BBDB6774B96A30B* __this, const RuntimeMethod* method) ;
// UnityEngine.Vector3 Oculus.Interaction.HandGrab.SnapSurfaces.SphereSurface::NearestPointInSurface(UnityEngine.Vector3)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 SphereSurface_NearestPointInSurface_m8571F38D5010A262130B3D0918187AE76290470D (SphereSurface_t7C6F8A614D76E8B3AE10EE722BBDB6774B96A30B* __this, Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___0_targetPosition, const RuntimeMethod* method) ;
// UnityEngine.Pose Oculus.Interaction.HandGrab.SnapSurfaces.SphereSurface::MinimalTranslationPoseAtSurface(UnityEngine.Pose&,UnityEngine.Pose&)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971 SphereSurface_MinimalTranslationPoseAtSurface_m888D23B10E2443C9E359760B9F4927D8D0B2A3D6 (SphereSurface_t7C6F8A614D76E8B3AE10EE722BBDB6774B96A30B* __this, Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* ___0_userPose, Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* ___1_snapPose, const RuntimeMethod* method) ;
// T UnityEngine.GameObject::AddComponent<Oculus.Interaction.HandGrab.SnapSurfaces.SphereSurface>()
inline SphereSurface_t7C6F8A614D76E8B3AE10EE722BBDB6774B96A30B* GameObject_AddComponent_TisSphereSurface_t7C6F8A614D76E8B3AE10EE722BBDB6774B96A30B_m61F5ABE86B0C4374D3A35820F96D50228F1767C9 (GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* __this, const RuntimeMethod* method)
{
	return ((  SphereSurface_t7C6F8A614D76E8B3AE10EE722BBDB6774B96A30B* (*) (GameObject_t76FEDD663AB33C991A9C9A23129337651094216F*, const RuntimeMethod*))GameObject_AddComponent_TisRuntimeObject_m69B93700FACCF372F5753371C6E8FB780800B824_gshared)(__this, method);
}
// Oculus.Interaction.HandGrab.SnapSurfaces.SphereSurfaceData Oculus.Interaction.HandGrab.SnapSurfaces.SphereSurfaceData::Mirror()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR SphereSurfaceData_tE8331CA08F2DB69533F08417636C96FFA01E7CC1* SphereSurfaceData_Mirror_m783B2836B5EA587D72120C22298A11C93433CD56 (SphereSurfaceData_tE8331CA08F2DB69533F08417636C96FFA01E7CC1* __this, const RuntimeMethod* method) ;
// System.Void Oculus.Interaction.HandGrab.SnapSurfaces.SphereSurface::set_Data(Oculus.Interaction.HandGrab.SnapSurfaces.SphereSurfaceData)
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void SphereSurface_set_Data_m463CDA705345ED889DAD5A98A3A704F56A65BD26_inline (SphereSurface_t7C6F8A614D76E8B3AE10EE722BBDB6774B96A30B* __this, SphereSurfaceData_tE8331CA08F2DB69533F08417636C96FFA01E7CC1* ___0_value, const RuntimeMethod* method) ;
// UnityEngine.Quaternion Oculus.Interaction.HandGrab.SnapSurfaces.SphereSurface::RotationAtPoint(UnityEngine.Vector3,UnityEngine.Quaternion,UnityEngine.Quaternion)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 SphereSurface_RotationAtPoint_m9907775BA1F5ED5424D50A607A44ADB19FD971E2 (SphereSurface_t7C6F8A614D76E8B3AE10EE722BBDB6774B96A30B* __this, Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___0_surfacePoint, Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 ___1_baseRot, Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 ___2_desiredRotation, const RuntimeMethod* method) ;
// System.Void Oculus.Interaction.HandGrab.SnapSurfaces.SphereSurface::InjectData(Oculus.Interaction.HandGrab.SnapSurfaces.SphereSurfaceData)
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void SphereSurface_InjectData_m5B528A93C577760B667FE1A98CA1A71B4DD6861F_inline (SphereSurface_t7C6F8A614D76E8B3AE10EE722BBDB6774B96A30B* __this, SphereSurfaceData_tE8331CA08F2DB69533F08417636C96FFA01E7CC1* ___0_data, const RuntimeMethod* method) ;
// System.Void Oculus.Interaction.HandGrab.SnapSurfaces.SphereSurface::InjectRelativeTo(UnityEngine.Transform)
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void SphereSurface_InjectRelativeTo_m6B1E6609020D4F861398621C3B3AAA89E822B781_inline (SphereSurface_t7C6F8A614D76E8B3AE10EE722BBDB6774B96A30B* __this, Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* ___0_relativeTo, const RuntimeMethod* method) ;
// System.Void Oculus.Interaction.HandGrab.SnapSurfaces.SphereSurface::InjectGripPoint(UnityEngine.Transform)
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void SphereSurface_InjectGripPoint_m2329EBE5F4F5CBAD77341BCF37E9F3BF8B7F9DC4_inline (SphereSurface_t7C6F8A614D76E8B3AE10EE722BBDB6774B96A30B* __this, Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* ___0_gripPoint, const RuntimeMethod* method) ;
// System.Single Oculus.Interaction.HandGrab.SnapSurfaces.SphereSurface::CalculateBestPoseAtSurface(UnityEngine.Pose&,UnityEngine.Pose&,UnityEngine.Pose&,Oculus.Interaction.HandGrab.PoseMeasureParameters&)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float SphereSurface_CalculateBestPoseAtSurface_m5C024F13CD2E88EF909798301ED2322AFE6629B2 (SphereSurface_t7C6F8A614D76E8B3AE10EE722BBDB6774B96A30B* __this, Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* ___0_targetPose, Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* ___1_reference, Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* ___2_bestPose, PoseMeasureParameters_tB95EB8BC83F81E86544854D1B13C04E88F2C0203* ___3_scoringModifier, const RuntimeMethod* method) ;
// System.Boolean Oculus.Interaction.HandGrab.SnapSurfaces.SphereSurface::CalculateBestPoseAtSurface(UnityEngine.Ray,UnityEngine.Pose&,UnityEngine.Pose&)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool SphereSurface_CalculateBestPoseAtSurface_m75444B60CED07377ED2BB91B7BB15B7E220C613E (SphereSurface_t7C6F8A614D76E8B3AE10EE722BBDB6774B96A30B* __this, Ray_t2B1742D7958DC05BDC3EFC7461D3593E1430DC00 ___0_targetRay, Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* ___1_recordedPose, Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* ___2_bestPose, const RuntimeMethod* method) ;
// UnityEngine.Pose Oculus.Interaction.HandGrab.SnapSurfaces.SphereSurface::MirrorPose(UnityEngine.Pose&)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971 SphereSurface_MirrorPose_m25AE869A0553646F0CCBF97B12E16C491E1CDC62 (SphereSurface_t7C6F8A614D76E8B3AE10EE722BBDB6774B96A30B* __this, Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* ___0_pose, const RuntimeMethod* method) ;
// UnityEngine.Vector3 UnityEngine.Vector3::Normalize(UnityEngine.Vector3)
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 Vector3_Normalize_mEF8349CC39674236CFC694189AFD36E31F89AC8F_inline (Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___0_value, const RuntimeMethod* method) ;
// System.Single UnityEngine.Mathf::Clamp(System.Single,System.Single,System.Single)
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR float Mathf_Clamp_mEB9AEA827D27D20FCC787F7375156AF46BB12BBF_inline (float ___0_value, float ___1_min, float ___2_max, const RuntimeMethod* method) ;
// System.Single UnityEngine.Vector3::Angle(UnityEngine.Vector3,UnityEngine.Vector3)
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR float Vector3_Angle_mB16906B482814C140FE5BA9D041D2DC11E42A68D_inline (Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___0_from, Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___1_to, const RuntimeMethod* method) ;
// System.Single UnityEngine.Mathf::Sign(System.Single)
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR float Mathf_Sign_m42EE1F0BC041AF14F89DED7F762BE996E2C50D8A_inline (float ___0_f, const RuntimeMethod* method) ;
// System.Void UnityEngine.Quaternion::.ctor(System.Single,System.Single,System.Single,System.Single)
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void Quaternion__ctor_m868FD60AA65DD5A8AC0C5DEB0608381A8D85FCD8_inline (Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974* __this, float ___0_x, float ___1_y, float ___2_z, float ___3_w, const RuntimeMethod* method) ;
// System.Single UnityEngine.Vector3::Magnitude(UnityEngine.Vector3)
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR float Vector3_Magnitude_m21652D951393A3D7CE92CE40049A0E7F76544D1B_inline (Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___0_vector, const RuntimeMethod* method) ;
// UnityEngine.Vector3 UnityEngine.Vector3::op_Division(UnityEngine.Vector3,System.Single)
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 Vector3_op_Division_mCC6BB24E372AB96B8380D1678446EF6A8BAE13BB_inline (Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___0_a, float ___1_d, const RuntimeMethod* method) ;
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// System.Object Oculus.Interaction.HandGrab.SnapSurfaces.CylinderSurfaceData::Clone()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* CylinderSurfaceData_Clone_m50C26B0C43F1C3F73F18D07B7E9CB1C4E5AA831A (CylinderSurfaceData_t8F949CD513A3C48F106518CF244120A2C884D700* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&CylinderSurfaceData_t8F949CD513A3C48F106518CF244120A2C884D700_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// CylinderSurfaceData clone = new CylinderSurfaceData();
		CylinderSurfaceData_t8F949CD513A3C48F106518CF244120A2C884D700* L_0 = (CylinderSurfaceData_t8F949CD513A3C48F106518CF244120A2C884D700*)il2cpp_codegen_object_new(CylinderSurfaceData_t8F949CD513A3C48F106518CF244120A2C884D700_il2cpp_TypeInfo_var);
		NullCheck(L_0);
		CylinderSurfaceData__ctor_mEEC8BFE66D1A3497A974E1CAB57012ADB7C8DB24(L_0, NULL);
		// clone.startPoint = this.startPoint;
		CylinderSurfaceData_t8F949CD513A3C48F106518CF244120A2C884D700* L_1 = L_0;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_2 = __this->___startPoint_0;
		NullCheck(L_1);
		L_1->___startPoint_0 = L_2;
		// clone.endPoint = this.endPoint;
		CylinderSurfaceData_t8F949CD513A3C48F106518CF244120A2C884D700* L_3 = L_1;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_4 = __this->___endPoint_1;
		NullCheck(L_3);
		L_3->___endPoint_1 = L_4;
		// clone.arcOffset = this.arcOffset;
		CylinderSurfaceData_t8F949CD513A3C48F106518CF244120A2C884D700* L_5 = L_3;
		float L_6 = __this->___arcOffset_2;
		NullCheck(L_5);
		L_5->___arcOffset_2 = L_6;
		// clone.arcLength = this.arcLength;
		CylinderSurfaceData_t8F949CD513A3C48F106518CF244120A2C884D700* L_7 = L_5;
		float L_8 = __this->___arcLength_3;
		NullCheck(L_7);
		L_7->___arcLength_3 = L_8;
		// return clone;
		return L_7;
	}
}
// Oculus.Interaction.HandGrab.SnapSurfaces.CylinderSurfaceData Oculus.Interaction.HandGrab.SnapSurfaces.CylinderSurfaceData::Mirror()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR CylinderSurfaceData_t8F949CD513A3C48F106518CF244120A2C884D700* CylinderSurfaceData_Mirror_m5E5FF75620F1DC159CDD6B025D302D5CE6ADE0E0 (CylinderSurfaceData_t8F949CD513A3C48F106518CF244120A2C884D700* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&CylinderSurfaceData_t8F949CD513A3C48F106518CF244120A2C884D700_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// CylinderSurfaceData mirror = Clone() as CylinderSurfaceData;
		RuntimeObject* L_0;
		L_0 = CylinderSurfaceData_Clone_m50C26B0C43F1C3F73F18D07B7E9CB1C4E5AA831A(__this, NULL);
		// return mirror;
		return ((CylinderSurfaceData_t8F949CD513A3C48F106518CF244120A2C884D700*)IsInstClass((RuntimeObject*)L_0, CylinderSurfaceData_t8F949CD513A3C48F106518CF244120A2C884D700_il2cpp_TypeInfo_var));
	}
}
// System.Void Oculus.Interaction.HandGrab.SnapSurfaces.CylinderSurfaceData::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void CylinderSurfaceData__ctor_mEEC8BFE66D1A3497A974E1CAB57012ADB7C8DB24 (CylinderSurfaceData_t8F949CD513A3C48F106518CF244120A2C884D700* __this, const RuntimeMethod* method) 
{
	{
		// public Vector3 startPoint = new Vector3(0f, 0.1f, 0f);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_0;
		memset((&L_0), 0, sizeof(L_0));
		Vector3__ctor_m376936E6B999EF1ECBE57D990A386303E2283DE0_inline((&L_0), (0.0f), (0.100000001f), (0.0f), /*hidden argument*/NULL);
		__this->___startPoint_0 = L_0;
		// public Vector3 endPoint = new Vector3(0f, -0.1f, 0f);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_1;
		memset((&L_1), 0, sizeof(L_1));
		Vector3__ctor_m376936E6B999EF1ECBE57D990A386303E2283DE0_inline((&L_1), (0.0f), (-0.100000001f), (0.0f), /*hidden argument*/NULL);
		__this->___endPoint_1 = L_1;
		// public float arcLength = 360f;
		__this->___arcLength_3 = (360.0f);
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2(__this, NULL);
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// Oculus.Interaction.HandGrab.SnapSurfaces.CylinderSurfaceData Oculus.Interaction.HandGrab.SnapSurfaces.CylinderSurface::get_Data()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR CylinderSurfaceData_t8F949CD513A3C48F106518CF244120A2C884D700* CylinderSurface_get_Data_mB514FEE5AE43017E9FCDC833AF9EA5B0C1231BC3 (CylinderSurface_t071F3C35CED4F1296F988B93595DF4736770D4F7* __this, const RuntimeMethod* method) 
{
	{
		// return _data;
		CylinderSurfaceData_t8F949CD513A3C48F106518CF244120A2C884D700* L_0 = __this->____data_4;
		return L_0;
	}
}
// System.Void Oculus.Interaction.HandGrab.SnapSurfaces.CylinderSurface::set_Data(Oculus.Interaction.HandGrab.SnapSurfaces.CylinderSurfaceData)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void CylinderSurface_set_Data_m87230DD8507A149A857F965B1BE004A62ADBC500 (CylinderSurface_t071F3C35CED4F1296F988B93595DF4736770D4F7* __this, CylinderSurfaceData_t8F949CD513A3C48F106518CF244120A2C884D700* ___0_value, const RuntimeMethod* method) 
{
	{
		// _data = value;
		CylinderSurfaceData_t8F949CD513A3C48F106518CF244120A2C884D700* L_0 = ___0_value;
		__this->____data_4 = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->____data_4), (void*)L_0);
		// }
		return;
	}
}
// UnityEngine.Transform Oculus.Interaction.HandGrab.SnapSurfaces.CylinderSurface::get_RelativeTo()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* CylinderSurface_get_RelativeTo_m3B6807164E9EE3C2F8A8688910AE9DAE16A3A612 (CylinderSurface_t071F3C35CED4F1296F988B93595DF4736770D4F7* __this, const RuntimeMethod* method) 
{
	{
		// get => _relativeTo;
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_0 = __this->____relativeTo_5;
		return L_0;
	}
}
// System.Void Oculus.Interaction.HandGrab.SnapSurfaces.CylinderSurface::set_RelativeTo(UnityEngine.Transform)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void CylinderSurface_set_RelativeTo_m6A6392423425754DAE54369D345886EB53356B96 (CylinderSurface_t071F3C35CED4F1296F988B93595DF4736770D4F7* __this, Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* ___0_value, const RuntimeMethod* method) 
{
	{
		// set => _relativeTo = value;
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_0 = ___0_value;
		__this->____relativeTo_5 = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->____relativeTo_5), (void*)L_0);
		return;
	}
}
// UnityEngine.Transform Oculus.Interaction.HandGrab.SnapSurfaces.CylinderSurface::get_GripPoint()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* CylinderSurface_get_GripPoint_m5EB373A3D69443440ACCAD9212CD470ECC0526D2 (CylinderSurface_t071F3C35CED4F1296F988B93595DF4736770D4F7* __this, const RuntimeMethod* method) 
{
	{
		// get => _gripPoint;
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_0 = __this->____gripPoint_6;
		return L_0;
	}
}
// System.Void Oculus.Interaction.HandGrab.SnapSurfaces.CylinderSurface::set_GripPoint(UnityEngine.Transform)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void CylinderSurface_set_GripPoint_m0FE208BF7E6557B540AA9D6419BB41E3A6A8F664 (CylinderSurface_t071F3C35CED4F1296F988B93595DF4736770D4F7* __this, Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* ___0_value, const RuntimeMethod* method) 
{
	{
		// set => _gripPoint = value;
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_0 = ___0_value;
		__this->____gripPoint_6 = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->____gripPoint_6), (void*)L_0);
		return;
	}
}
// UnityEngine.Vector3 Oculus.Interaction.HandGrab.SnapSurfaces.CylinderSurface::get_OriginalDir()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 CylinderSurface_get_OriginalDir_mD1944E5349E62DB0869436092A1FA89359177DF7 (CylinderSurface_t071F3C35CED4F1296F988B93595DF4736770D4F7* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		// if (this.GripPoint == null)
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_0;
		L_0 = CylinderSurface_get_GripPoint_m5EB373A3D69443440ACCAD9212CD470ECC0526D2_inline(__this, NULL);
		il2cpp_codegen_runtime_class_init_inline(Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		bool L_1;
		L_1 = Object_op_Equality_mB6120F782D83091EF56A198FCEBCF066DB4A9605(L_0, (Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C*)NULL, NULL);
		if (!L_1)
		{
			goto IL_0014;
		}
	}
	{
		// return Vector3.forward;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_2;
		L_2 = Vector3_get_forward_mAA55A7034304DF8B2152EAD49AE779FC4CA2EB4A_inline(NULL);
		return L_2;
	}

IL_0014:
	{
		// return Vector3.ProjectOnPlane(GripPoint.transform.position - StartPoint, Direction).normalized;
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_3;
		L_3 = CylinderSurface_get_GripPoint_m5EB373A3D69443440ACCAD9212CD470ECC0526D2_inline(__this, NULL);
		NullCheck(L_3);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_4;
		L_4 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_3, NULL);
		NullCheck(L_4);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_5;
		L_5 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_4, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_6;
		L_6 = CylinderSurface_get_StartPoint_mF7CC10FE14B1ADE9745E95695C456FC2AF3D9A7B(__this, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_7;
		L_7 = Vector3_op_Subtraction_mE42023FF80067CB44A1D4A27EB7CF2B24CABB828_inline(L_5, L_6, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_8;
		L_8 = CylinderSurface_get_Direction_mB62C7EB59706C2FD57685D22D0C98389F2ABEE5B(__this, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_9;
		L_9 = Vector3_ProjectOnPlane_m68FB895F6E9FCC45676BB8B95857D091C0D78794_inline(L_7, L_8, NULL);
		V_0 = L_9;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_10;
		L_10 = Vector3_get_normalized_m736BBF65D5CDA7A18414370D15B4DFCC1E466F07_inline((&V_0), NULL);
		return L_10;
	}
}
// UnityEngine.Vector3 Oculus.Interaction.HandGrab.SnapSurfaces.CylinderSurface::get_StartArcDir()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 CylinderSurface_get_StartArcDir_m25095FA9EF2F0D23F3282ED86433F298554F1FFB (CylinderSurface_t071F3C35CED4F1296F988B93595DF4736770D4F7* __this, const RuntimeMethod* method) 
{
	{
		// return Quaternion.AngleAxis(ArcOffset, Direction) * OriginalDir;
		float L_0;
		L_0 = CylinderSurface_get_ArcOffset_mC3B510DBF0216286DBBE3BDA64735BDE992D8804(__this, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_1;
		L_1 = CylinderSurface_get_Direction_mB62C7EB59706C2FD57685D22D0C98389F2ABEE5B(__this, NULL);
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_2;
		L_2 = Quaternion_AngleAxis_mF37022977B297E63AA70D69EA1C4C922FF22CC80(L_0, L_1, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_3;
		L_3 = CylinderSurface_get_OriginalDir_mD1944E5349E62DB0869436092A1FA89359177DF7(__this, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_4;
		L_4 = Quaternion_op_Multiply_mE1EBA73F9173432B50F8F17CE8190C5A7986FB8C(L_2, L_3, NULL);
		return L_4;
	}
}
// UnityEngine.Vector3 Oculus.Interaction.HandGrab.SnapSurfaces.CylinderSurface::get_EndArcDir()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 CylinderSurface_get_EndArcDir_m1782B93B9682F9037311B58FAD20295EA7A63CC9 (CylinderSurface_t071F3C35CED4F1296F988B93595DF4736770D4F7* __this, const RuntimeMethod* method) 
{
	{
		// return Quaternion.AngleAxis(ArcLength, Direction) * StartArcDir;
		float L_0;
		L_0 = CylinderSurface_get_ArcLength_m96967AE172A13B61C840D0286A318690D110DAF4(__this, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_1;
		L_1 = CylinderSurface_get_Direction_mB62C7EB59706C2FD57685D22D0C98389F2ABEE5B(__this, NULL);
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_2;
		L_2 = Quaternion_AngleAxis_mF37022977B297E63AA70D69EA1C4C922FF22CC80(L_0, L_1, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_3;
		L_3 = CylinderSurface_get_StartArcDir_m25095FA9EF2F0D23F3282ED86433F298554F1FFB(__this, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_4;
		L_4 = Quaternion_op_Multiply_mE1EBA73F9173432B50F8F17CE8190C5A7986FB8C(L_2, L_3, NULL);
		return L_4;
	}
}
// UnityEngine.Vector3 Oculus.Interaction.HandGrab.SnapSurfaces.CylinderSurface::get_StartPoint()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 CylinderSurface_get_StartPoint_mF7CC10FE14B1ADE9745E95695C456FC2AF3D9A7B (CylinderSurface_t071F3C35CED4F1296F988B93595DF4736770D4F7* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// if (this.RelativeTo != null)
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_0;
		L_0 = CylinderSurface_get_RelativeTo_m3B6807164E9EE3C2F8A8688910AE9DAE16A3A612_inline(__this, NULL);
		il2cpp_codegen_runtime_class_init_inline(Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		bool L_1;
		L_1 = Object_op_Inequality_mD0BE578448EAA61948F25C32F8DD55AB1F778602(L_0, (Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C*)NULL, NULL);
		if (!L_1)
		{
			goto IL_0025;
		}
	}
	{
		// return this.RelativeTo.TransformPoint(_data.startPoint);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_2;
		L_2 = CylinderSurface_get_RelativeTo_m3B6807164E9EE3C2F8A8688910AE9DAE16A3A612_inline(__this, NULL);
		CylinderSurfaceData_t8F949CD513A3C48F106518CF244120A2C884D700* L_3 = __this->____data_4;
		NullCheck(L_3);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_4 = L_3->___startPoint_0;
		NullCheck(L_2);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_5;
		L_5 = Transform_TransformPoint_m05BFF013DB830D7BFE44A007703694AE1062EE44(L_2, L_4, NULL);
		return L_5;
	}

IL_0025:
	{
		// return _data.startPoint;
		CylinderSurfaceData_t8F949CD513A3C48F106518CF244120A2C884D700* L_6 = __this->____data_4;
		NullCheck(L_6);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_7 = L_6->___startPoint_0;
		return L_7;
	}
}
// System.Void Oculus.Interaction.HandGrab.SnapSurfaces.CylinderSurface::set_StartPoint(UnityEngine.Vector3)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void CylinderSurface_set_StartPoint_mD62D85B69FC68A92685CA32B19D02C967ED27BDC (CylinderSurface_t071F3C35CED4F1296F988B93595DF4736770D4F7* __this, Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___0_value, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// if (this.RelativeTo != null)
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_0;
		L_0 = CylinderSurface_get_RelativeTo_m3B6807164E9EE3C2F8A8688910AE9DAE16A3A612_inline(__this, NULL);
		il2cpp_codegen_runtime_class_init_inline(Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		bool L_1;
		L_1 = Object_op_Inequality_mD0BE578448EAA61948F25C32F8DD55AB1F778602(L_0, (Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C*)NULL, NULL);
		if (!L_1)
		{
			goto IL_0026;
		}
	}
	{
		// _data.startPoint = this.RelativeTo.InverseTransformPoint(value);
		CylinderSurfaceData_t8F949CD513A3C48F106518CF244120A2C884D700* L_2 = __this->____data_4;
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_3;
		L_3 = CylinderSurface_get_RelativeTo_m3B6807164E9EE3C2F8A8688910AE9DAE16A3A612_inline(__this, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_4 = ___0_value;
		NullCheck(L_3);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_5;
		L_5 = Transform_InverseTransformPoint_m18CD395144D9C78F30E15A5B82B6670E792DBA5D(L_3, L_4, NULL);
		NullCheck(L_2);
		L_2->___startPoint_0 = L_5;
		return;
	}

IL_0026:
	{
		// _data.startPoint = value;
		CylinderSurfaceData_t8F949CD513A3C48F106518CF244120A2C884D700* L_6 = __this->____data_4;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_7 = ___0_value;
		NullCheck(L_6);
		L_6->___startPoint_0 = L_7;
		// }
		return;
	}
}
// UnityEngine.Vector3 Oculus.Interaction.HandGrab.SnapSurfaces.CylinderSurface::get_EndPoint()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 CylinderSurface_get_EndPoint_mE98BE2ABCCE9D6E1ABDD30C99B3DDF1212B638E4 (CylinderSurface_t071F3C35CED4F1296F988B93595DF4736770D4F7* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// if (this.RelativeTo != null)
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_0;
		L_0 = CylinderSurface_get_RelativeTo_m3B6807164E9EE3C2F8A8688910AE9DAE16A3A612_inline(__this, NULL);
		il2cpp_codegen_runtime_class_init_inline(Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		bool L_1;
		L_1 = Object_op_Inequality_mD0BE578448EAA61948F25C32F8DD55AB1F778602(L_0, (Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C*)NULL, NULL);
		if (!L_1)
		{
			goto IL_0025;
		}
	}
	{
		// return this.RelativeTo.TransformPoint(_data.endPoint);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_2;
		L_2 = CylinderSurface_get_RelativeTo_m3B6807164E9EE3C2F8A8688910AE9DAE16A3A612_inline(__this, NULL);
		CylinderSurfaceData_t8F949CD513A3C48F106518CF244120A2C884D700* L_3 = __this->____data_4;
		NullCheck(L_3);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_4 = L_3->___endPoint_1;
		NullCheck(L_2);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_5;
		L_5 = Transform_TransformPoint_m05BFF013DB830D7BFE44A007703694AE1062EE44(L_2, L_4, NULL);
		return L_5;
	}

IL_0025:
	{
		// return _data.endPoint;
		CylinderSurfaceData_t8F949CD513A3C48F106518CF244120A2C884D700* L_6 = __this->____data_4;
		NullCheck(L_6);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_7 = L_6->___endPoint_1;
		return L_7;
	}
}
// System.Void Oculus.Interaction.HandGrab.SnapSurfaces.CylinderSurface::set_EndPoint(UnityEngine.Vector3)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void CylinderSurface_set_EndPoint_m722FC6ED65AB01BF52822ACD6C7B0A582CD0DB70 (CylinderSurface_t071F3C35CED4F1296F988B93595DF4736770D4F7* __this, Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___0_value, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// if (this.RelativeTo != null)
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_0;
		L_0 = CylinderSurface_get_RelativeTo_m3B6807164E9EE3C2F8A8688910AE9DAE16A3A612_inline(__this, NULL);
		il2cpp_codegen_runtime_class_init_inline(Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		bool L_1;
		L_1 = Object_op_Inequality_mD0BE578448EAA61948F25C32F8DD55AB1F778602(L_0, (Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C*)NULL, NULL);
		if (!L_1)
		{
			goto IL_0026;
		}
	}
	{
		// _data.endPoint = this.RelativeTo.InverseTransformPoint(value);
		CylinderSurfaceData_t8F949CD513A3C48F106518CF244120A2C884D700* L_2 = __this->____data_4;
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_3;
		L_3 = CylinderSurface_get_RelativeTo_m3B6807164E9EE3C2F8A8688910AE9DAE16A3A612_inline(__this, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_4 = ___0_value;
		NullCheck(L_3);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_5;
		L_5 = Transform_InverseTransformPoint_m18CD395144D9C78F30E15A5B82B6670E792DBA5D(L_3, L_4, NULL);
		NullCheck(L_2);
		L_2->___endPoint_1 = L_5;
		return;
	}

IL_0026:
	{
		// _data.endPoint = value;
		CylinderSurfaceData_t8F949CD513A3C48F106518CF244120A2C884D700* L_6 = __this->____data_4;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_7 = ___0_value;
		NullCheck(L_6);
		L_6->___endPoint_1 = L_7;
		// }
		return;
	}
}
// System.Single Oculus.Interaction.HandGrab.SnapSurfaces.CylinderSurface::get_ArcOffset()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float CylinderSurface_get_ArcOffset_mC3B510DBF0216286DBBE3BDA64735BDE992D8804 (CylinderSurface_t071F3C35CED4F1296F988B93595DF4736770D4F7* __this, const RuntimeMethod* method) 
{
	{
		// return _data.arcOffset;
		CylinderSurfaceData_t8F949CD513A3C48F106518CF244120A2C884D700* L_0 = __this->____data_4;
		NullCheck(L_0);
		float L_1 = L_0->___arcOffset_2;
		return L_1;
	}
}
// System.Void Oculus.Interaction.HandGrab.SnapSurfaces.CylinderSurface::set_ArcOffset(System.Single)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void CylinderSurface_set_ArcOffset_m1D874BEF328846F886FCDB42316F33DD38AF59C7 (CylinderSurface_t071F3C35CED4F1296F988B93595DF4736770D4F7* __this, float ___0_value, const RuntimeMethod* method) 
{
	{
		// if (value != 0 && value % 360f == 0)
		float L_0 = ___0_value;
		if ((((float)L_0) == ((float)(0.0f))))
		{
			goto IL_0027;
		}
	}
	{
		float L_1 = ___0_value;
		if ((!(((float)(fmodf(L_1, (360.0f)))) == ((float)(0.0f)))))
		{
			goto IL_0027;
		}
	}
	{
		// _data.arcOffset = 360f;
		CylinderSurfaceData_t8F949CD513A3C48F106518CF244120A2C884D700* L_2 = __this->____data_4;
		NullCheck(L_2);
		L_2->___arcOffset_2 = (360.0f);
		return;
	}

IL_0027:
	{
		// _data.arcOffset = Mathf.Repeat(value, 360f);
		CylinderSurfaceData_t8F949CD513A3C48F106518CF244120A2C884D700* L_3 = __this->____data_4;
		float L_4 = ___0_value;
		float L_5;
		L_5 = Mathf_Repeat_m6F1560A163481BB311D685294E1B463C3E4EB3BA_inline(L_4, (360.0f), NULL);
		NullCheck(L_3);
		L_3->___arcOffset_2 = L_5;
		// }
		return;
	}
}
// System.Single Oculus.Interaction.HandGrab.SnapSurfaces.CylinderSurface::get_ArcLength()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float CylinderSurface_get_ArcLength_m96967AE172A13B61C840D0286A318690D110DAF4 (CylinderSurface_t071F3C35CED4F1296F988B93595DF4736770D4F7* __this, const RuntimeMethod* method) 
{
	{
		// return _data.arcLength;
		CylinderSurfaceData_t8F949CD513A3C48F106518CF244120A2C884D700* L_0 = __this->____data_4;
		NullCheck(L_0);
		float L_1 = L_0->___arcLength_3;
		return L_1;
	}
}
// System.Void Oculus.Interaction.HandGrab.SnapSurfaces.CylinderSurface::set_ArcLength(System.Single)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void CylinderSurface_set_ArcLength_m43D8DB7D563D273EC9A7A421297A6615302CAD4B (CylinderSurface_t071F3C35CED4F1296F988B93595DF4736770D4F7* __this, float ___0_value, const RuntimeMethod* method) 
{
	{
		// if (value != 0 && value % 360f == 0)
		float L_0 = ___0_value;
		if ((((float)L_0) == ((float)(0.0f))))
		{
			goto IL_0027;
		}
	}
	{
		float L_1 = ___0_value;
		if ((!(((float)(fmodf(L_1, (360.0f)))) == ((float)(0.0f)))))
		{
			goto IL_0027;
		}
	}
	{
		// _data.arcLength = 360f;
		CylinderSurfaceData_t8F949CD513A3C48F106518CF244120A2C884D700* L_2 = __this->____data_4;
		NullCheck(L_2);
		L_2->___arcLength_3 = (360.0f);
		return;
	}

IL_0027:
	{
		// _data.arcLength = Mathf.Repeat(value, 360f);
		CylinderSurfaceData_t8F949CD513A3C48F106518CF244120A2C884D700* L_3 = __this->____data_4;
		float L_4 = ___0_value;
		float L_5;
		L_5 = Mathf_Repeat_m6F1560A163481BB311D685294E1B463C3E4EB3BA_inline(L_4, (360.0f), NULL);
		NullCheck(L_3);
		L_3->___arcLength_3 = L_5;
		// }
		return;
	}
}
// System.Single Oculus.Interaction.HandGrab.SnapSurfaces.CylinderSurface::get_Radius()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float CylinderSurface_get_Radius_mC66E163DBCC270B93198392EC52EC9DC40BE1559 (CylinderSurface_t071F3C35CED4F1296F988B93595DF4736770D4F7* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		// if (this.GripPoint == null)
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_0;
		L_0 = CylinderSurface_get_GripPoint_m5EB373A3D69443440ACCAD9212CD470ECC0526D2_inline(__this, NULL);
		il2cpp_codegen_runtime_class_init_inline(Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		bool L_1;
		L_1 = Object_op_Equality_mB6120F782D83091EF56A198FCEBCF066DB4A9605(L_0, (Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C*)NULL, NULL);
		if (!L_1)
		{
			goto IL_0014;
		}
	}
	{
		// return 0f;
		return (0.0f);
	}

IL_0014:
	{
		// Vector3 start = StartPoint;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_2;
		L_2 = CylinderSurface_get_StartPoint_mF7CC10FE14B1ADE9745E95695C456FC2AF3D9A7B(__this, NULL);
		V_0 = L_2;
		// Vector3 projectedPoint = start + Vector3.Project(this.GripPoint.position - start, Direction);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_3 = V_0;
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_4;
		L_4 = CylinderSurface_get_GripPoint_m5EB373A3D69443440ACCAD9212CD470ECC0526D2_inline(__this, NULL);
		NullCheck(L_4);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_5;
		L_5 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_4, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_6 = V_0;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_7;
		L_7 = Vector3_op_Subtraction_mE42023FF80067CB44A1D4A27EB7CF2B24CABB828_inline(L_5, L_6, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_8;
		L_8 = CylinderSurface_get_Direction_mB62C7EB59706C2FD57685D22D0C98389F2ABEE5B(__this, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_9;
		L_9 = Vector3_Project_m85DF3CB297EC5E1A17BD6266FF65E86AB7372C9B_inline(L_7, L_8, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_10;
		L_10 = Vector3_op_Addition_m78C0EC70CB66E8DCAC225743D82B268DAEE92067_inline(L_3, L_9, NULL);
		// return Vector3.Distance(projectedPoint, this.GripPoint.position);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_11;
		L_11 = CylinderSurface_get_GripPoint_m5EB373A3D69443440ACCAD9212CD470ECC0526D2_inline(__this, NULL);
		NullCheck(L_11);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_12;
		L_12 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_11, NULL);
		float L_13;
		L_13 = Vector3_Distance_m2314DB9B8BD01157E013DF87BEA557375C7F9FF9_inline(L_10, L_12, NULL);
		return L_13;
	}
}
// UnityEngine.Vector3 Oculus.Interaction.HandGrab.SnapSurfaces.CylinderSurface::get_Direction()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 CylinderSurface_get_Direction_mB62C7EB59706C2FD57685D22D0C98389F2ABEE5B (CylinderSurface_t071F3C35CED4F1296F988B93595DF4736770D4F7* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		// Vector3 dir = (EndPoint - StartPoint);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_0;
		L_0 = CylinderSurface_get_EndPoint_mE98BE2ABCCE9D6E1ABDD30C99B3DDF1212B638E4(__this, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_1;
		L_1 = CylinderSurface_get_StartPoint_mF7CC10FE14B1ADE9745E95695C456FC2AF3D9A7B(__this, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_2;
		L_2 = Vector3_op_Subtraction_mE42023FF80067CB44A1D4A27EB7CF2B24CABB828_inline(L_0, L_1, NULL);
		V_0 = L_2;
		// if (dir.sqrMagnitude == 0f)
		float L_3;
		L_3 = Vector3_get_sqrMagnitude_m43C27DEC47C4811FB30AB474FF2131A963B66FC8_inline((&V_0), NULL);
		if ((!(((float)L_3) == ((float)(0.0f)))))
		{
			goto IL_003f;
		}
	}
	{
		// return this.RelativeTo ? this.RelativeTo.up : Vector3.up;
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_4;
		L_4 = CylinderSurface_get_RelativeTo_m3B6807164E9EE3C2F8A8688910AE9DAE16A3A612_inline(__this, NULL);
		il2cpp_codegen_runtime_class_init_inline(Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		bool L_5;
		L_5 = Object_op_Implicit_m93896EF7D68FA113C42D3FE2BC6F661FC7EF514A(L_4, NULL);
		if (L_5)
		{
			goto IL_0033;
		}
	}
	{
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_6;
		L_6 = Vector3_get_up_m128AF3FDC820BF59D5DE86D973E7DE3F20C3AEBA_inline(NULL);
		return L_6;
	}

IL_0033:
	{
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_7;
		L_7 = CylinderSurface_get_RelativeTo_m3B6807164E9EE3C2F8A8688910AE9DAE16A3A612_inline(__this, NULL);
		NullCheck(L_7);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_8;
		L_8 = Transform_get_up_mE47A9D9D96422224DD0539AA5524DA5440145BB2(L_7, NULL);
		return L_8;
	}

IL_003f:
	{
		// return dir.normalized;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_9;
		L_9 = Vector3_get_normalized_m736BBF65D5CDA7A18414370D15B4DFCC1E466F07_inline((&V_0), NULL);
		return L_9;
	}
}
// System.Single Oculus.Interaction.HandGrab.SnapSurfaces.CylinderSurface::get_Height()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float CylinderSurface_get_Height_mAC52DA1EAA6A0AFB914D0C107FD2F4B038639721 (CylinderSurface_t071F3C35CED4F1296F988B93595DF4736770D4F7* __this, const RuntimeMethod* method) 
{
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		// return (EndPoint - StartPoint).magnitude;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_0;
		L_0 = CylinderSurface_get_EndPoint_mE98BE2ABCCE9D6E1ABDD30C99B3DDF1212B638E4(__this, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_1;
		L_1 = CylinderSurface_get_StartPoint_mF7CC10FE14B1ADE9745E95695C456FC2AF3D9A7B(__this, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_2;
		L_2 = Vector3_op_Subtraction_mE42023FF80067CB44A1D4A27EB7CF2B24CABB828_inline(L_0, L_1, NULL);
		V_0 = L_2;
		float L_3;
		L_3 = Vector3_get_magnitude_mF0D6017E90B345F1F52D1CC564C640F1A847AF2D_inline((&V_0), NULL);
		return L_3;
	}
}
// UnityEngine.Quaternion Oculus.Interaction.HandGrab.SnapSurfaces.CylinderSurface::get_Rotation()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 CylinderSurface_get_Rotation_mC2C8899BA8E598D1A45709C6700BBE4698EF2F41 (CylinderSurface_t071F3C35CED4F1296F988B93595DF4736770D4F7* __this, const RuntimeMethod* method) 
{
	{
		// if (_data.startPoint == _data.endPoint)
		CylinderSurfaceData_t8F949CD513A3C48F106518CF244120A2C884D700* L_0 = __this->____data_4;
		NullCheck(L_0);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_1 = L_0->___startPoint_0;
		CylinderSurfaceData_t8F949CD513A3C48F106518CF244120A2C884D700* L_2 = __this->____data_4;
		NullCheck(L_2);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_3 = L_2->___endPoint_1;
		bool L_4;
		L_4 = Vector3_op_Equality_mCDCBB8D2EDC3D3BF20F31A25ACB34705D352B479_inline(L_1, L_3, NULL);
		if (!L_4)
		{
			goto IL_0028;
		}
	}
	{
		// return Quaternion.LookRotation(Vector3.forward);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_5;
		L_5 = Vector3_get_forward_mAA55A7034304DF8B2152EAD49AE779FC4CA2EB4A_inline(NULL);
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_6;
		L_6 = Quaternion_LookRotation_mDB2CCA75B8E1AB98104F2A6E1A1EA57D0D1298D7(L_5, NULL);
		return L_6;
	}

IL_0028:
	{
		// return Quaternion.LookRotation(OriginalDir, Direction);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_7;
		L_7 = CylinderSurface_get_OriginalDir_mD1944E5349E62DB0869436092A1FA89359177DF7(__this, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_8;
		L_8 = CylinderSurface_get_Direction_mB62C7EB59706C2FD57685D22D0C98389F2ABEE5B(__this, NULL);
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_9;
		L_9 = Quaternion_LookRotation_mFB02EDC8F733774DFAC3BEA4B4BB265A228F8307(L_7, L_8, NULL);
		return L_9;
	}
}
// System.Void Oculus.Interaction.HandGrab.SnapSurfaces.CylinderSurface::Reset()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void CylinderSurface_Reset_m7F99DD03BB4F3F81216BEFB28147E99BCA92254A (CylinderSurface_t071F3C35CED4F1296F988B93595DF4736770D4F7* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Component_TryGetComponent_TisHandGrabPose_t347682EF05B32B395F27F50DB38500405C17C633_m48D0952AB02E5B8946E540747BC71058019E55C6_RuntimeMethod_var);
		s_Il2CppMethodInitialized = true;
	}
	HandGrabPose_t347682EF05B32B395F27F50DB38500405C17C633* V_0 = NULL;
	{
		// _gripPoint = this.transform;
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_0;
		L_0 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(__this, NULL);
		__this->____gripPoint_6 = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->____gripPoint_6), (void*)L_0);
		// if (this.TryGetComponent(out HandGrabPose grabPose))
		bool L_1;
		L_1 = Component_TryGetComponent_TisHandGrabPose_t347682EF05B32B395F27F50DB38500405C17C633_m48D0952AB02E5B8946E540747BC71058019E55C6(__this, (&V_0), Component_TryGetComponent_TisHandGrabPose_t347682EF05B32B395F27F50DB38500405C17C633_m48D0952AB02E5B8946E540747BC71058019E55C6_RuntimeMethod_var);
		if (!L_1)
		{
			goto IL_0022;
		}
	}
	{
		// _relativeTo = grabPose.RelativeTo;
		HandGrabPose_t347682EF05B32B395F27F50DB38500405C17C633* L_2 = V_0;
		NullCheck(L_2);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_3;
		L_3 = HandGrabPose_get_RelativeTo_m5DFEA3BC65DF4B58C8BB75628AFB81A18B9399C4_inline(L_2, NULL);
		__this->____relativeTo_5 = L_3;
		Il2CppCodeGenWriteBarrier((void**)(&__this->____relativeTo_5), (void*)L_3);
	}

IL_0022:
	{
		// }
		return;
	}
}
// System.Void Oculus.Interaction.HandGrab.SnapSurfaces.CylinderSurface::Start()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void CylinderSurface_Start_m63FD465CFE46AB4D3EC19C0D3BD131753C128384 (CylinderSurface_t071F3C35CED4F1296F988B93595DF4736770D4F7* __this, const RuntimeMethod* method) 
{
	{
		// }
		return;
	}
}
// UnityEngine.Pose Oculus.Interaction.HandGrab.SnapSurfaces.CylinderSurface::MirrorPose(UnityEngine.Pose&)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971 CylinderSurface_MirrorPose_mA421C34FD2EAA278F4FCD31596B60ACC88934CA6 (CylinderSurface_t071F3C35CED4F1296F988B93595DF4736770D4F7* __this, Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* ___0_pose, const RuntimeMethod* method) 
{
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 V_0;
	memset((&V_0), 0, sizeof(V_0));
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 V_1;
	memset((&V_1), 0, sizeof(V_1));
	{
		// Vector3 normal = Quaternion.Inverse(this.RelativeTo.rotation) * OriginalDir;
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_0;
		L_0 = CylinderSurface_get_RelativeTo_m3B6807164E9EE3C2F8A8688910AE9DAE16A3A612_inline(__this, NULL);
		NullCheck(L_0);
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_1;
		L_1 = Transform_get_rotation_m32AF40CA0D50C797DA639A696F8EAEC7524C179C(L_0, NULL);
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_2;
		L_2 = Quaternion_Inverse_mD9C060AC626A7B406F4984AC98F8358DC89EF512(L_1, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_3;
		L_3 = CylinderSurface_get_OriginalDir_mD1944E5349E62DB0869436092A1FA89359177DF7(__this, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_4;
		L_4 = Quaternion_op_Multiply_mE1EBA73F9173432B50F8F17CE8190C5A7986FB8C(L_2, L_3, NULL);
		V_0 = L_4;
		// Vector3 tangent = Quaternion.Inverse(this.RelativeTo.rotation) * Direction;
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_5;
		L_5 = CylinderSurface_get_RelativeTo_m3B6807164E9EE3C2F8A8688910AE9DAE16A3A612_inline(__this, NULL);
		NullCheck(L_5);
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_6;
		L_6 = Transform_get_rotation_m32AF40CA0D50C797DA639A696F8EAEC7524C179C(L_5, NULL);
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_7;
		L_7 = Quaternion_Inverse_mD9C060AC626A7B406F4984AC98F8358DC89EF512(L_6, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_8;
		L_8 = CylinderSurface_get_Direction_mB62C7EB59706C2FD57685D22D0C98389F2ABEE5B(__this, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_9;
		L_9 = Quaternion_op_Multiply_mE1EBA73F9173432B50F8F17CE8190C5A7986FB8C(L_7, L_8, NULL);
		V_1 = L_9;
		// return pose.MirrorPoseRotation(normal, tangent);
		Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* L_10 = ___0_pose;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_11 = V_0;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_12 = V_1;
		Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971 L_13;
		L_13 = PoseUtils_MirrorPoseRotation_m0F4E6DD5661B752CE896089B11F9483DF19E5C5F(L_10, L_11, L_12, NULL);
		return L_13;
	}
}
// UnityEngine.Vector3 Oculus.Interaction.HandGrab.SnapSurfaces.CylinderSurface::PointAltitude(UnityEngine.Vector3)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 CylinderSurface_PointAltitude_m93AC9011BF0191EBC3A70CE06E0039F179E161F5 (CylinderSurface_t071F3C35CED4F1296F988B93595DF4736770D4F7* __this, Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___0_point, const RuntimeMethod* method) 
{
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		// Vector3 start = StartPoint;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_0;
		L_0 = CylinderSurface_get_StartPoint_mF7CC10FE14B1ADE9745E95695C456FC2AF3D9A7B(__this, NULL);
		V_0 = L_0;
		// Vector3 projectedPoint = start + Vector3.Project(point - start, Direction);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_1 = V_0;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_2 = ___0_point;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_3 = V_0;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_4;
		L_4 = Vector3_op_Subtraction_mE42023FF80067CB44A1D4A27EB7CF2B24CABB828_inline(L_2, L_3, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_5;
		L_5 = CylinderSurface_get_Direction_mB62C7EB59706C2FD57685D22D0C98389F2ABEE5B(__this, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_6;
		L_6 = Vector3_Project_m85DF3CB297EC5E1A17BD6266FF65E86AB7372C9B_inline(L_4, L_5, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_7;
		L_7 = Vector3_op_Addition_m78C0EC70CB66E8DCAC225743D82B268DAEE92067_inline(L_1, L_6, NULL);
		// return projectedPoint;
		return L_7;
	}
}
// System.Single Oculus.Interaction.HandGrab.SnapSurfaces.CylinderSurface::CalculateBestPoseAtSurface(UnityEngine.Pose&,UnityEngine.Pose&,UnityEngine.Pose&,Oculus.Interaction.HandGrab.PoseMeasureParameters&)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float CylinderSurface_CalculateBestPoseAtSurface_m00B0699D91CA4B6C7E1F61A571910FCA3558853B (CylinderSurface_t071F3C35CED4F1296F988B93595DF4736770D4F7* __this, Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* ___0_targetPose, Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* ___1_reference, Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* ___2_bestPose, PoseMeasureParameters_tB95EB8BC83F81E86544854D1B13C04E88F2C0203* ___3_scoringModifier, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&CylinderSurface_MinimalRotationPoseAtSurface_m948E72EC899B829FEBA889791D16BD6ED86875B9_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&CylinderSurface_MinimalTranslationPoseAtSurface_m842AD7D70ABC3A537FC4042FAAF5D2AEFAF4A95F_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&PoseCalculator_t74377137B14A0C57A79A02A9C33C5814E64F1839_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// return SnapSurfaceHelper.CalculateBestPoseAtSurface(targetPose, reference, out bestPose,
		//     scoringModifier, MinimalTranslationPoseAtSurface, MinimalRotationPoseAtSurface);
		Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* L_0 = ___0_targetPose;
		Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* L_1 = ___1_reference;
		Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* L_2 = ___2_bestPose;
		PoseMeasureParameters_tB95EB8BC83F81E86544854D1B13C04E88F2C0203* L_3 = ___3_scoringModifier;
		PoseCalculator_t74377137B14A0C57A79A02A9C33C5814E64F1839* L_4 = (PoseCalculator_t74377137B14A0C57A79A02A9C33C5814E64F1839*)il2cpp_codegen_object_new(PoseCalculator_t74377137B14A0C57A79A02A9C33C5814E64F1839_il2cpp_TypeInfo_var);
		NullCheck(L_4);
		PoseCalculator__ctor_mCB0A713E75C96AE4D9C311D91EBBE4A016F8763B(L_4, __this, (intptr_t)((void*)CylinderSurface_MinimalTranslationPoseAtSurface_m842AD7D70ABC3A537FC4042FAAF5D2AEFAF4A95F_RuntimeMethod_var), NULL);
		PoseCalculator_t74377137B14A0C57A79A02A9C33C5814E64F1839* L_5 = (PoseCalculator_t74377137B14A0C57A79A02A9C33C5814E64F1839*)il2cpp_codegen_object_new(PoseCalculator_t74377137B14A0C57A79A02A9C33C5814E64F1839_il2cpp_TypeInfo_var);
		NullCheck(L_5);
		PoseCalculator__ctor_mCB0A713E75C96AE4D9C311D91EBBE4A016F8763B(L_5, __this, (intptr_t)((void*)CylinderSurface_MinimalRotationPoseAtSurface_m948E72EC899B829FEBA889791D16BD6ED86875B9_RuntimeMethod_var), NULL);
		float L_6;
		L_6 = SnapSurfaceHelper_CalculateBestPoseAtSurface_m59B6D35DAD5E611E28D9D44A33DF752471144B3E(L_0, L_1, L_2, L_3, L_4, L_5, NULL);
		return L_6;
	}
}
// Oculus.Interaction.HandGrab.SnapSurfaces.ISnapSurface Oculus.Interaction.HandGrab.SnapSurfaces.CylinderSurface::CreateMirroredSurface(UnityEngine.GameObject)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* CylinderSurface_CreateMirroredSurface_mCF0ED8A4D37FB0E25B403F03F31E5C56690FF86B (CylinderSurface_t071F3C35CED4F1296F988B93595DF4736770D4F7* __this, GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* ___0_gameObject, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&GameObject_AddComponent_TisCylinderSurface_t071F3C35CED4F1296F988B93595DF4736770D4F7_m3772C5F6A909279BE2427B29C4E3E9F0462E1031_RuntimeMethod_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// CylinderSurface surface = gameObject.AddComponent<CylinderSurface>();
		GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* L_0 = ___0_gameObject;
		NullCheck(L_0);
		CylinderSurface_t071F3C35CED4F1296F988B93595DF4736770D4F7* L_1;
		L_1 = GameObject_AddComponent_TisCylinderSurface_t071F3C35CED4F1296F988B93595DF4736770D4F7_m3772C5F6A909279BE2427B29C4E3E9F0462E1031(L_0, GameObject_AddComponent_TisCylinderSurface_t071F3C35CED4F1296F988B93595DF4736770D4F7_m3772C5F6A909279BE2427B29C4E3E9F0462E1031_RuntimeMethod_var);
		// surface.Data = _data.Mirror();
		CylinderSurface_t071F3C35CED4F1296F988B93595DF4736770D4F7* L_2 = L_1;
		CylinderSurfaceData_t8F949CD513A3C48F106518CF244120A2C884D700* L_3 = __this->____data_4;
		NullCheck(L_3);
		CylinderSurfaceData_t8F949CD513A3C48F106518CF244120A2C884D700* L_4;
		L_4 = CylinderSurfaceData_Mirror_m5E5FF75620F1DC159CDD6B025D302D5CE6ADE0E0(L_3, NULL);
		NullCheck(L_2);
		CylinderSurface_set_Data_m87230DD8507A149A857F965B1BE004A62ADBC500_inline(L_2, L_4, NULL);
		// return surface;
		return L_2;
	}
}
// Oculus.Interaction.HandGrab.SnapSurfaces.ISnapSurface Oculus.Interaction.HandGrab.SnapSurfaces.CylinderSurface::CreateDuplicatedSurface(UnityEngine.GameObject)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* CylinderSurface_CreateDuplicatedSurface_m6B23AE1DEA8FE96167305031586C2E6C990F4B6F (CylinderSurface_t071F3C35CED4F1296F988B93595DF4736770D4F7* __this, GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* ___0_gameObject, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&GameObject_AddComponent_TisCylinderSurface_t071F3C35CED4F1296F988B93595DF4736770D4F7_m3772C5F6A909279BE2427B29C4E3E9F0462E1031_RuntimeMethod_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// CylinderSurface surface = gameObject.AddComponent<CylinderSurface>();
		GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* L_0 = ___0_gameObject;
		NullCheck(L_0);
		CylinderSurface_t071F3C35CED4F1296F988B93595DF4736770D4F7* L_1;
		L_1 = GameObject_AddComponent_TisCylinderSurface_t071F3C35CED4F1296F988B93595DF4736770D4F7_m3772C5F6A909279BE2427B29C4E3E9F0462E1031(L_0, GameObject_AddComponent_TisCylinderSurface_t071F3C35CED4F1296F988B93595DF4736770D4F7_m3772C5F6A909279BE2427B29C4E3E9F0462E1031_RuntimeMethod_var);
		// surface.Data = _data;
		CylinderSurface_t071F3C35CED4F1296F988B93595DF4736770D4F7* L_2 = L_1;
		CylinderSurfaceData_t8F949CD513A3C48F106518CF244120A2C884D700* L_3 = __this->____data_4;
		NullCheck(L_2);
		CylinderSurface_set_Data_m87230DD8507A149A857F965B1BE004A62ADBC500_inline(L_2, L_3, NULL);
		// return surface;
		return L_2;
	}
}
// UnityEngine.Vector3 Oculus.Interaction.HandGrab.SnapSurfaces.CylinderSurface::NearestPointInSurface(UnityEngine.Vector3)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 CylinderSurface_NearestPointInSurface_mC5B4F8F1CA761511DC0E2164F306232C7996B7EE (CylinderSurface_t071F3C35CED4F1296F988B93595DF4736770D4F7* __this, Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___0_targetPosition, const RuntimeMethod* method) 
{
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 V_0;
	memset((&V_0), 0, sizeof(V_0));
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 V_1;
	memset((&V_1), 0, sizeof(V_1));
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 V_2;
	memset((&V_2), 0, sizeof(V_2));
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 V_3;
	memset((&V_3), 0, sizeof(V_3));
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 V_4;
	memset((&V_4), 0, sizeof(V_4));
	float V_5 = 0.0f;
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 V_6;
	memset((&V_6), 0, sizeof(V_6));
	{
		// Vector3 start = StartPoint;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_0;
		L_0 = CylinderSurface_get_StartPoint_mF7CC10FE14B1ADE9745E95695C456FC2AF3D9A7B(__this, NULL);
		V_0 = L_0;
		// Vector3 dir = Direction;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_1;
		L_1 = CylinderSurface_get_Direction_mB62C7EB59706C2FD57685D22D0C98389F2ABEE5B(__this, NULL);
		V_1 = L_1;
		// Vector3 projectedVector = Vector3.Project(targetPosition - start, dir);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_2 = ___0_targetPosition;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_3 = V_0;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_4;
		L_4 = Vector3_op_Subtraction_mE42023FF80067CB44A1D4A27EB7CF2B24CABB828_inline(L_2, L_3, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_5 = V_1;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_6;
		L_6 = Vector3_Project_m85DF3CB297EC5E1A17BD6266FF65E86AB7372C9B_inline(L_4, L_5, NULL);
		V_2 = L_6;
		// if (projectedVector.magnitude > Height)
		float L_7;
		L_7 = Vector3_get_magnitude_mF0D6017E90B345F1F52D1CC564C640F1A847AF2D_inline((&V_2), NULL);
		float L_8;
		L_8 = CylinderSurface_get_Height_mAC52DA1EAA6A0AFB914D0C107FD2F4B038639721(__this, NULL);
		if ((!(((float)L_7) > ((float)L_8))))
		{
			goto IL_003e;
		}
	}
	{
		// projectedVector = projectedVector.normalized * Height;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_9;
		L_9 = Vector3_get_normalized_m736BBF65D5CDA7A18414370D15B4DFCC1E466F07_inline((&V_2), NULL);
		float L_10;
		L_10 = CylinderSurface_get_Height_mAC52DA1EAA6A0AFB914D0C107FD2F4B038639721(__this, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_11;
		L_11 = Vector3_op_Multiply_m87BA7C578F96C8E49BB07088DAAC4649F83B0353_inline(L_9, L_10, NULL);
		V_2 = L_11;
	}

IL_003e:
	{
		// if (Vector3.Dot(projectedVector, dir) < 0f)
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_12 = V_2;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_13 = V_1;
		float L_14;
		L_14 = Vector3_Dot_mBB86BB940AA0A32FA7D3C02AC42E5BC7095A5D52_inline(L_12, L_13, NULL);
		if ((!(((float)L_14) < ((float)(0.0f)))))
		{
			goto IL_0052;
		}
	}
	{
		// projectedVector = Vector3.zero;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_15;
		L_15 = Vector3_get_zero_m0C1249C3F25B1C70EAD3CC8B31259975A457AE39_inline(NULL);
		V_2 = L_15;
	}

IL_0052:
	{
		// Vector3 projectedPoint = StartPoint + projectedVector;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_16;
		L_16 = CylinderSurface_get_StartPoint_mF7CC10FE14B1ADE9745E95695C456FC2AF3D9A7B(__this, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_17 = V_2;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_18;
		L_18 = Vector3_op_Addition_m78C0EC70CB66E8DCAC225743D82B268DAEE92067_inline(L_16, L_17, NULL);
		V_3 = L_18;
		// Vector3 targetDirection = Vector3.ProjectOnPlane((targetPosition - projectedPoint), dir).normalized;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_19 = ___0_targetPosition;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_20 = V_3;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_21;
		L_21 = Vector3_op_Subtraction_mE42023FF80067CB44A1D4A27EB7CF2B24CABB828_inline(L_19, L_20, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_22 = V_1;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_23;
		L_23 = Vector3_ProjectOnPlane_m68FB895F6E9FCC45676BB8B95857D091C0D78794_inline(L_21, L_22, NULL);
		V_6 = L_23;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_24;
		L_24 = Vector3_get_normalized_m736BBF65D5CDA7A18414370D15B4DFCC1E466F07_inline((&V_6), NULL);
		V_4 = L_24;
		// float desiredAngle = Mathf.Repeat(Vector3.SignedAngle(StartArcDir, targetDirection, dir), 360f);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_25;
		L_25 = CylinderSurface_get_StartArcDir_m25095FA9EF2F0D23F3282ED86433F298554F1FFB(__this, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_26 = V_4;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_27 = V_1;
		float L_28;
		L_28 = Vector3_SignedAngle_m76C77F9D7BAF5969FA5B7500ED2D5FF9F9FA4153_inline(L_25, L_26, L_27, NULL);
		float L_29;
		L_29 = Mathf_Repeat_m6F1560A163481BB311D685294E1B463C3E4EB3BA_inline(L_28, (360.0f), NULL);
		V_5 = L_29;
		// if (desiredAngle > ArcLength)
		float L_30 = V_5;
		float L_31;
		L_31 = CylinderSurface_get_ArcLength_m96967AE172A13B61C840D0286A318690D110DAF4(__this, NULL);
		if ((!(((float)L_30) > ((float)L_31))))
		{
			goto IL_00ca;
		}
	}
	{
		// if (Mathf.Abs(desiredAngle - ArcLength) >= Mathf.Abs(360f - desiredAngle))
		float L_32 = V_5;
		float L_33;
		L_33 = CylinderSurface_get_ArcLength_m96967AE172A13B61C840D0286A318690D110DAF4(__this, NULL);
		float L_34;
		L_34 = fabsf(((float)il2cpp_codegen_subtract(L_32, L_33)));
		float L_35 = V_5;
		float L_36;
		L_36 = fabsf(((float)il2cpp_codegen_subtract((360.0f), L_35)));
		if ((!(((float)L_34) >= ((float)L_36))))
		{
			goto IL_00c2;
		}
	}
	{
		// targetDirection = StartArcDir;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_37;
		L_37 = CylinderSurface_get_StartArcDir_m25095FA9EF2F0D23F3282ED86433F298554F1FFB(__this, NULL);
		V_4 = L_37;
		goto IL_00ca;
	}

IL_00c2:
	{
		// targetDirection = EndArcDir;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_38;
		L_38 = CylinderSurface_get_EndArcDir_m1782B93B9682F9037311B58FAD20295EA7A63CC9(__this, NULL);
		V_4 = L_38;
	}

IL_00ca:
	{
		// Vector3 surfacePoint = projectedPoint + targetDirection * Radius;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_39 = V_3;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_40 = V_4;
		float L_41;
		L_41 = CylinderSurface_get_Radius_mC66E163DBCC270B93198392EC52EC9DC40BE1559(__this, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_42;
		L_42 = Vector3_op_Multiply_m87BA7C578F96C8E49BB07088DAAC4649F83B0353_inline(L_40, L_41, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_43;
		L_43 = Vector3_op_Addition_m78C0EC70CB66E8DCAC225743D82B268DAEE92067_inline(L_39, L_42, NULL);
		// return surfacePoint;
		return L_43;
	}
}
// System.Boolean Oculus.Interaction.HandGrab.SnapSurfaces.CylinderSurface::CalculateBestPoseAtSurface(UnityEngine.Ray,UnityEngine.Pose&,UnityEngine.Pose&)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool CylinderSurface_CalculateBestPoseAtSurface_m1EDF5AF042F7CDEDAF7EB8EFD9385E632CBCEF82 (CylinderSurface_t071F3C35CED4F1296F988B93595DF4736770D4F7* __this, Ray_t2B1742D7958DC05BDC3EFC7461D3593E1430DC00 ___0_targetRay, Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* ___1_recordedPose, Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* ___2_bestPose, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&SingleU5BU5D_t89DEFE97BCEDB5857010E79ECE0F52CF6E93B87C_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	float V_0 = 0.0f;
	float V_1 = 0.0f;
	float V_2 = 0.0f;
	float V_3 = 0.0f;
	float V_4 = 0.0f;
	float V_5 = 0.0f;
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 V_6;
	memset((&V_6), 0, sizeof(V_6));
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 V_7;
	memset((&V_7), 0, sizeof(V_7));
	float V_8 = 0.0f;
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 V_9;
	memset((&V_9), 0, sizeof(V_9));
	Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971 V_10;
	memset((&V_10), 0, sizeof(V_10));
	float V_11 = 0.0f;
	{
		// Vector3 lineToCylinder = StartPoint - targetRay.origin;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_0;
		L_0 = CylinderSurface_get_StartPoint_mF7CC10FE14B1ADE9745E95695C456FC2AF3D9A7B(__this, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_1;
		L_1 = Ray_get_origin_m97604A8F180316A410DCD77B7D74D04522FA1BA6((&___0_targetRay), NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_2;
		L_2 = Vector3_op_Subtraction_mE42023FF80067CB44A1D4A27EB7CF2B24CABB828_inline(L_0, L_1, NULL);
		// float perpendiculiarity = Vector3.Dot(targetRay.direction, Direction);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_3;
		L_3 = Ray_get_direction_m21C2D22D3BD4A683BD4DC191AB22DD05F5EC2086((&___0_targetRay), NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_4;
		L_4 = CylinderSurface_get_Direction_mB62C7EB59706C2FD57685D22D0C98389F2ABEE5B(__this, NULL);
		float L_5;
		L_5 = Vector3_Dot_mBB86BB940AA0A32FA7D3C02AC42E5BC7095A5D52_inline(L_3, L_4, NULL);
		V_0 = L_5;
		// float rayToLineDiff = Vector3.Dot(lineToCylinder, targetRay.direction);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_6 = L_2;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_7;
		L_7 = Ray_get_direction_m21C2D22D3BD4A683BD4DC191AB22DD05F5EC2086((&___0_targetRay), NULL);
		float L_8;
		L_8 = Vector3_Dot_mBB86BB940AA0A32FA7D3C02AC42E5BC7095A5D52_inline(L_6, L_7, NULL);
		V_1 = L_8;
		// float cylinderToLineDiff = Vector3.Dot(lineToCylinder, Direction);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_9;
		L_9 = CylinderSurface_get_Direction_mB62C7EB59706C2FD57685D22D0C98389F2ABEE5B(__this, NULL);
		float L_10;
		L_10 = Vector3_Dot_mBB86BB940AA0A32FA7D3C02AC42E5BC7095A5D52_inline(L_6, L_9, NULL);
		V_2 = L_10;
		// float determinant = 1f / (perpendiculiarity * perpendiculiarity - 1f);
		float L_11 = V_0;
		float L_12 = V_0;
		V_3 = ((float)((1.0f)/((float)il2cpp_codegen_subtract(((float)il2cpp_codegen_multiply(L_11, L_12)), (1.0f)))));
		// float lineOffset = (perpendiculiarity * cylinderToLineDiff - rayToLineDiff) * determinant;
		float L_13 = V_0;
		float L_14 = V_2;
		float L_15 = V_1;
		float L_16 = V_3;
		V_4 = ((float)il2cpp_codegen_multiply(((float)il2cpp_codegen_subtract(((float)il2cpp_codegen_multiply(L_13, L_14)), L_15)), L_16));
		// float cylinderOffset = (cylinderToLineDiff - perpendiculiarity * rayToLineDiff) * determinant;
		float L_17 = V_2;
		float L_18 = V_0;
		float L_19 = V_1;
		float L_20 = V_3;
		V_5 = ((float)il2cpp_codegen_multiply(((float)il2cpp_codegen_subtract(L_17, ((float)il2cpp_codegen_multiply(L_18, L_19)))), L_20));
		// Vector3 pointInLine = targetRay.origin + targetRay.direction * lineOffset;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_21;
		L_21 = Ray_get_origin_m97604A8F180316A410DCD77B7D74D04522FA1BA6((&___0_targetRay), NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_22;
		L_22 = Ray_get_direction_m21C2D22D3BD4A683BD4DC191AB22DD05F5EC2086((&___0_targetRay), NULL);
		float L_23 = V_4;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_24;
		L_24 = Vector3_op_Multiply_m87BA7C578F96C8E49BB07088DAAC4649F83B0353_inline(L_22, L_23, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_25;
		L_25 = Vector3_op_Addition_m78C0EC70CB66E8DCAC225743D82B268DAEE92067_inline(L_21, L_24, NULL);
		V_6 = L_25;
		// Vector3 pointInCylinder = StartPoint + Direction * cylinderOffset;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_26;
		L_26 = CylinderSurface_get_StartPoint_mF7CC10FE14B1ADE9745E95695C456FC2AF3D9A7B(__this, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_27;
		L_27 = CylinderSurface_get_Direction_mB62C7EB59706C2FD57685D22D0C98389F2ABEE5B(__this, NULL);
		float L_28 = V_5;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_29;
		L_29 = Vector3_op_Multiply_m87BA7C578F96C8E49BB07088DAAC4649F83B0353_inline(L_27, L_28, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_30;
		L_30 = Vector3_op_Addition_m78C0EC70CB66E8DCAC225743D82B268DAEE92067_inline(L_26, L_29, NULL);
		V_7 = L_30;
		// float distanceToSurface = Mathf.Max(Vector3.Distance(pointInCylinder, pointInLine) - Radius);
		SingleU5BU5D_t89DEFE97BCEDB5857010E79ECE0F52CF6E93B87C* L_31 = (SingleU5BU5D_t89DEFE97BCEDB5857010E79ECE0F52CF6E93B87C*)(SingleU5BU5D_t89DEFE97BCEDB5857010E79ECE0F52CF6E93B87C*)SZArrayNew(SingleU5BU5D_t89DEFE97BCEDB5857010E79ECE0F52CF6E93B87C_il2cpp_TypeInfo_var, (uint32_t)1);
		SingleU5BU5D_t89DEFE97BCEDB5857010E79ECE0F52CF6E93B87C* L_32 = L_31;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_33 = V_7;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_34 = V_6;
		float L_35;
		L_35 = Vector3_Distance_m2314DB9B8BD01157E013DF87BEA557375C7F9FF9_inline(L_33, L_34, NULL);
		float L_36;
		L_36 = CylinderSurface_get_Radius_mC66E163DBCC270B93198392EC52EC9DC40BE1559(__this, NULL);
		NullCheck(L_32);
		(L_32)->SetAt(static_cast<il2cpp_array_size_t>(0), (float)((float)il2cpp_codegen_subtract(L_35, L_36)));
		float L_37;
		L_37 = Mathf_Max_m870B77F6A5225EC21D9778043F987B9B17D08F6B_inline(L_32, NULL);
		V_8 = L_37;
		// if (distanceToSurface < Radius)
		float L_38 = V_8;
		float L_39;
		L_39 = CylinderSurface_get_Radius_mC66E163DBCC270B93198392EC52EC9DC40BE1559(__this, NULL);
		if ((!(((float)L_38) < ((float)L_39))))
		{
			goto IL_00f2;
		}
	}
	{
		// float adjustedDistance = Mathf.Sqrt(Radius * Radius - distanceToSurface * distanceToSurface);
		float L_40;
		L_40 = CylinderSurface_get_Radius_mC66E163DBCC270B93198392EC52EC9DC40BE1559(__this, NULL);
		float L_41;
		L_41 = CylinderSurface_get_Radius_mC66E163DBCC270B93198392EC52EC9DC40BE1559(__this, NULL);
		float L_42 = V_8;
		float L_43 = V_8;
		float L_44;
		L_44 = sqrtf(((float)il2cpp_codegen_subtract(((float)il2cpp_codegen_multiply(L_40, L_41)), ((float)il2cpp_codegen_multiply(L_42, L_43)))));
		V_11 = L_44;
		// pointInLine -= targetRay.direction * adjustedDistance;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_45 = V_6;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_46;
		L_46 = Ray_get_direction_m21C2D22D3BD4A683BD4DC191AB22DD05F5EC2086((&___0_targetRay), NULL);
		float L_47 = V_11;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_48;
		L_48 = Vector3_op_Multiply_m87BA7C578F96C8E49BB07088DAAC4649F83B0353_inline(L_46, L_47, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_49;
		L_49 = Vector3_op_Subtraction_mE42023FF80067CB44A1D4A27EB7CF2B24CABB828_inline(L_45, L_48, NULL);
		V_6 = L_49;
	}

IL_00f2:
	{
		// Vector3 surfacePoint = NearestPointInSurface(pointInLine);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_50 = V_6;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_51;
		L_51 = CylinderSurface_NearestPointInSurface_mC5B4F8F1CA761511DC0E2164F306232C7996B7EE(__this, L_50, NULL);
		V_9 = L_51;
		// Pose desiredPose = new Pose(surfacePoint, recordedPose.rotation);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_52 = V_9;
		Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* L_53 = ___1_recordedPose;
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_54 = L_53->___rotation_1;
		Pose__ctor_m15CA45808A2BBF1956E836D22C387FAB80BED051((&V_10), L_52, L_54, NULL);
		// bestPose = MinimalTranslationPoseAtSurface(desiredPose, recordedPose);
		Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* L_55 = ___2_bestPose;
		Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* L_56 = ___1_recordedPose;
		Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971 L_57;
		L_57 = CylinderSurface_MinimalTranslationPoseAtSurface_m842AD7D70ABC3A537FC4042FAAF5D2AEFAF4A95F(__this, (&V_10), L_56, NULL);
		*(Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971*)L_55 = L_57;
		// return true;
		return (bool)1;
	}
}
// UnityEngine.Pose Oculus.Interaction.HandGrab.SnapSurfaces.CylinderSurface::MinimalRotationPoseAtSurface(UnityEngine.Pose&,UnityEngine.Pose&)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971 CylinderSurface_MinimalRotationPoseAtSurface_m948E72EC899B829FEBA889791D16BD6ED86875B9 (CylinderSurface_t071F3C35CED4F1296F988B93595DF4736770D4F7* __this, Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* ___0_userPose, Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* ___1_snapPose, const RuntimeMethod* method) 
{
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 V_0;
	memset((&V_0), 0, sizeof(V_0));
	Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 V_1;
	memset((&V_1), 0, sizeof(V_1));
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 V_2;
	memset((&V_2), 0, sizeof(V_2));
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 V_3;
	memset((&V_3), 0, sizeof(V_3));
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 V_4;
	memset((&V_4), 0, sizeof(V_4));
	Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 V_5;
	memset((&V_5), 0, sizeof(V_5));
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 V_6;
	memset((&V_6), 0, sizeof(V_6));
	{
		// Vector3 desiredPos = userPose.position;
		Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* L_0 = ___0_userPose;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_1 = L_0->___position_0;
		V_0 = L_1;
		// Quaternion desiredRot = userPose.rotation;
		Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* L_2 = ___0_userPose;
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_3 = L_2->___rotation_1;
		// Quaternion baseRot = snapPose.rotation;
		Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* L_4 = ___1_snapPose;
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_5 = L_4->___rotation_1;
		V_1 = L_5;
		// Quaternion rotDif = (desiredRot) * Quaternion.Inverse(baseRot);
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_6 = V_1;
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_7;
		L_7 = Quaternion_Inverse_mD9C060AC626A7B406F4984AC98F8358DC89EF512(L_6, NULL);
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_8;
		L_8 = Quaternion_op_Multiply_mCB375FCCC12A2EC8F9EB824A1BFB4453B58C2012_inline(L_3, L_7, NULL);
		// Vector3 desiredDirection = (rotDif * Rotation) * Vector3.forward;
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_9;
		L_9 = CylinderSurface_get_Rotation_mC2C8899BA8E598D1A45709C6700BBE4698EF2F41(__this, NULL);
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_10;
		L_10 = Quaternion_op_Multiply_mCB375FCCC12A2EC8F9EB824A1BFB4453B58C2012_inline(L_8, L_9, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_11;
		L_11 = Vector3_get_forward_mAA55A7034304DF8B2152EAD49AE779FC4CA2EB4A_inline(NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_12;
		L_12 = Quaternion_op_Multiply_mE1EBA73F9173432B50F8F17CE8190C5A7986FB8C(L_10, L_11, NULL);
		// Vector3 projectedDirection = Vector3.ProjectOnPlane(desiredDirection, Direction).normalized;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_13;
		L_13 = CylinderSurface_get_Direction_mB62C7EB59706C2FD57685D22D0C98389F2ABEE5B(__this, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_14;
		L_14 = Vector3_ProjectOnPlane_m68FB895F6E9FCC45676BB8B95857D091C0D78794_inline(L_12, L_13, NULL);
		V_6 = L_14;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_15;
		L_15 = Vector3_get_normalized_m736BBF65D5CDA7A18414370D15B4DFCC1E466F07_inline((&V_6), NULL);
		V_2 = L_15;
		// Vector3 altitudePoint = PointAltitude(desiredPos);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_16 = V_0;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_17;
		L_17 = CylinderSurface_PointAltitude_m93AC9011BF0191EBC3A70CE06E0039F179E161F5(__this, L_16, NULL);
		V_3 = L_17;
		// Vector3 surfacePoint = NearestPointInSurface(altitudePoint + projectedDirection * Radius);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_18 = V_3;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_19 = V_2;
		float L_20;
		L_20 = CylinderSurface_get_Radius_mC66E163DBCC270B93198392EC52EC9DC40BE1559(__this, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_21;
		L_21 = Vector3_op_Multiply_m87BA7C578F96C8E49BB07088DAAC4649F83B0353_inline(L_19, L_20, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_22;
		L_22 = Vector3_op_Addition_m78C0EC70CB66E8DCAC225743D82B268DAEE92067_inline(L_18, L_21, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_23;
		L_23 = CylinderSurface_NearestPointInSurface_mC5B4F8F1CA761511DC0E2164F306232C7996B7EE(__this, L_22, NULL);
		V_4 = L_23;
		// Quaternion surfaceRotation = CalculateRotationOffset(surfacePoint) * baseRot;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_24 = V_4;
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_25;
		L_25 = CylinderSurface_CalculateRotationOffset_m1A7CF0FD4877A053DCD3CCACF2AE5883E8E95574(__this, L_24, NULL);
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_26 = V_1;
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_27;
		L_27 = Quaternion_op_Multiply_mCB375FCCC12A2EC8F9EB824A1BFB4453B58C2012_inline(L_25, L_26, NULL);
		V_5 = L_27;
		// return new Pose(surfacePoint, surfaceRotation);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_28 = V_4;
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_29 = V_5;
		Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971 L_30;
		memset((&L_30), 0, sizeof(L_30));
		Pose__ctor_m15CA45808A2BBF1956E836D22C387FAB80BED051((&L_30), L_28, L_29, /*hidden argument*/NULL);
		return L_30;
	}
}
// UnityEngine.Pose Oculus.Interaction.HandGrab.SnapSurfaces.CylinderSurface::MinimalTranslationPoseAtSurface(UnityEngine.Pose&,UnityEngine.Pose&)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971 CylinderSurface_MinimalTranslationPoseAtSurface_m842AD7D70ABC3A537FC4042FAAF5D2AEFAF4A95F (CylinderSurface_t071F3C35CED4F1296F988B93595DF4736770D4F7* __this, Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* ___0_userPose, Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* ___1_snapPose, const RuntimeMethod* method) 
{
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 V_0;
	memset((&V_0), 0, sizeof(V_0));
	Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 V_1;
	memset((&V_1), 0, sizeof(V_1));
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 V_2;
	memset((&V_2), 0, sizeof(V_2));
	Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 V_3;
	memset((&V_3), 0, sizeof(V_3));
	{
		// Vector3 desiredPos = userPose.position;
		Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* L_0 = ___0_userPose;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_1 = L_0->___position_0;
		V_0 = L_1;
		// Quaternion baseRot = snapPose.rotation;
		Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* L_2 = ___1_snapPose;
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_3 = L_2->___rotation_1;
		V_1 = L_3;
		// Vector3 surfacePoint = NearestPointInSurface(desiredPos);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_4 = V_0;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_5;
		L_5 = CylinderSurface_NearestPointInSurface_mC5B4F8F1CA761511DC0E2164F306232C7996B7EE(__this, L_4, NULL);
		V_2 = L_5;
		// Quaternion surfaceRotation = CalculateRotationOffset(surfacePoint) * baseRot;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_6 = V_2;
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_7;
		L_7 = CylinderSurface_CalculateRotationOffset_m1A7CF0FD4877A053DCD3CCACF2AE5883E8E95574(__this, L_6, NULL);
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_8 = V_1;
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_9;
		L_9 = Quaternion_op_Multiply_mCB375FCCC12A2EC8F9EB824A1BFB4453B58C2012_inline(L_7, L_8, NULL);
		V_3 = L_9;
		// return new Pose(surfacePoint, surfaceRotation);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_10 = V_2;
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_11 = V_3;
		Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971 L_12;
		memset((&L_12), 0, sizeof(L_12));
		Pose__ctor_m15CA45808A2BBF1956E836D22C387FAB80BED051((&L_12), L_10, L_11, /*hidden argument*/NULL);
		return L_12;
	}
}
// UnityEngine.Quaternion Oculus.Interaction.HandGrab.SnapSurfaces.CylinderSurface::CalculateRotationOffset(UnityEngine.Vector3)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 CylinderSurface_CalculateRotationOffset_m1A7CF0FD4877A053DCD3CCACF2AE5883E8E95574 (CylinderSurface_t071F3C35CED4F1296F988B93595DF4736770D4F7* __this, Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___0_surfacePoint, const RuntimeMethod* method) 
{
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		// Vector3 recordedDirection = Vector3.ProjectOnPlane(this.GripPoint.position - StartPoint, Direction);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_0;
		L_0 = CylinderSurface_get_GripPoint_m5EB373A3D69443440ACCAD9212CD470ECC0526D2_inline(__this, NULL);
		NullCheck(L_0);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_1;
		L_1 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_0, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_2;
		L_2 = CylinderSurface_get_StartPoint_mF7CC10FE14B1ADE9745E95695C456FC2AF3D9A7B(__this, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_3;
		L_3 = Vector3_op_Subtraction_mE42023FF80067CB44A1D4A27EB7CF2B24CABB828_inline(L_1, L_2, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_4;
		L_4 = CylinderSurface_get_Direction_mB62C7EB59706C2FD57685D22D0C98389F2ABEE5B(__this, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_5;
		L_5 = Vector3_ProjectOnPlane_m68FB895F6E9FCC45676BB8B95857D091C0D78794_inline(L_3, L_4, NULL);
		// Vector3 desiredDirection = Vector3.ProjectOnPlane(surfacePoint - StartPoint, Direction);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_6 = ___0_surfacePoint;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_7;
		L_7 = CylinderSurface_get_StartPoint_mF7CC10FE14B1ADE9745E95695C456FC2AF3D9A7B(__this, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_8;
		L_8 = Vector3_op_Subtraction_mE42023FF80067CB44A1D4A27EB7CF2B24CABB828_inline(L_6, L_7, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_9;
		L_9 = CylinderSurface_get_Direction_mB62C7EB59706C2FD57685D22D0C98389F2ABEE5B(__this, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_10;
		L_10 = Vector3_ProjectOnPlane_m68FB895F6E9FCC45676BB8B95857D091C0D78794_inline(L_8, L_9, NULL);
		V_0 = L_10;
		// return Quaternion.FromToRotation(recordedDirection, desiredDirection);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_11 = V_0;
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_12;
		L_12 = Quaternion_FromToRotation_mCB3100F93637E72455388B901C36EF8A25DFDB9A(L_5, L_11, NULL);
		return L_12;
	}
}
// System.Void Oculus.Interaction.HandGrab.SnapSurfaces.CylinderSurface::InjectAllCylinderSurface(Oculus.Interaction.HandGrab.SnapSurfaces.CylinderSurfaceData,UnityEngine.Transform,UnityEngine.Transform)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void CylinderSurface_InjectAllCylinderSurface_m95F1C38F9C7356F8ABBEF70DF4AFBC61AADDB777 (CylinderSurface_t071F3C35CED4F1296F988B93595DF4736770D4F7* __this, CylinderSurfaceData_t8F949CD513A3C48F106518CF244120A2C884D700* ___0_data, Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* ___1_relativeTo, Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* ___2_gripPoint, const RuntimeMethod* method) 
{
	{
		// InjectData(data);
		CylinderSurfaceData_t8F949CD513A3C48F106518CF244120A2C884D700* L_0 = ___0_data;
		CylinderSurface_InjectData_m4466DEF60883B115512651A9AABA2084187E6702_inline(__this, L_0, NULL);
		// InjectRelativeTo(relativeTo);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_1 = ___1_relativeTo;
		CylinderSurface_InjectRelativeTo_m656BD54F4AF5E9937B458F16AB710AC356B2EE41_inline(__this, L_1, NULL);
		// InjectGripPoint(gripPoint);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_2 = ___2_gripPoint;
		CylinderSurface_InjectGripPoint_mD9FE7A069861670C8803680CC3385866A22908EB_inline(__this, L_2, NULL);
		// }
		return;
	}
}
// System.Void Oculus.Interaction.HandGrab.SnapSurfaces.CylinderSurface::InjectData(Oculus.Interaction.HandGrab.SnapSurfaces.CylinderSurfaceData)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void CylinderSurface_InjectData_m4466DEF60883B115512651A9AABA2084187E6702 (CylinderSurface_t071F3C35CED4F1296F988B93595DF4736770D4F7* __this, CylinderSurfaceData_t8F949CD513A3C48F106518CF244120A2C884D700* ___0_data, const RuntimeMethod* method) 
{
	{
		// _data = data;
		CylinderSurfaceData_t8F949CD513A3C48F106518CF244120A2C884D700* L_0 = ___0_data;
		__this->____data_4 = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->____data_4), (void*)L_0);
		// }
		return;
	}
}
// System.Void Oculus.Interaction.HandGrab.SnapSurfaces.CylinderSurface::InjectRelativeTo(UnityEngine.Transform)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void CylinderSurface_InjectRelativeTo_m656BD54F4AF5E9937B458F16AB710AC356B2EE41 (CylinderSurface_t071F3C35CED4F1296F988B93595DF4736770D4F7* __this, Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* ___0_relativeTo, const RuntimeMethod* method) 
{
	{
		// _relativeTo = relativeTo;
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_0 = ___0_relativeTo;
		__this->____relativeTo_5 = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->____relativeTo_5), (void*)L_0);
		// }
		return;
	}
}
// System.Void Oculus.Interaction.HandGrab.SnapSurfaces.CylinderSurface::InjectGripPoint(UnityEngine.Transform)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void CylinderSurface_InjectGripPoint_mD9FE7A069861670C8803680CC3385866A22908EB (CylinderSurface_t071F3C35CED4F1296F988B93595DF4736770D4F7* __this, Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* ___0_gripPoint, const RuntimeMethod* method) 
{
	{
		// _gripPoint = gripPoint;
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_0 = ___0_gripPoint;
		__this->____gripPoint_6 = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->____gripPoint_6), (void*)L_0);
		// }
		return;
	}
}
// System.Void Oculus.Interaction.HandGrab.SnapSurfaces.CylinderSurface::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void CylinderSurface__ctor_m2ECC5DE5DE05496ED2BEF1958237B6CFC3AAE955 (CylinderSurface_t071F3C35CED4F1296F988B93595DF4736770D4F7* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&CylinderSurfaceData_t8F949CD513A3C48F106518CF244120A2C884D700_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// protected CylinderSurfaceData _data = new CylinderSurfaceData();
		CylinderSurfaceData_t8F949CD513A3C48F106518CF244120A2C884D700* L_0 = (CylinderSurfaceData_t8F949CD513A3C48F106518CF244120A2C884D700*)il2cpp_codegen_object_new(CylinderSurfaceData_t8F949CD513A3C48F106518CF244120A2C884D700_il2cpp_TypeInfo_var);
		NullCheck(L_0);
		CylinderSurfaceData__ctor_mEEC8BFE66D1A3497A974E1CAB57012ADB7C8DB24(L_0, NULL);
		__this->____data_4 = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->____data_4), (void*)L_0);
		MonoBehaviour__ctor_m592DB0105CA0BC97AA1C5F4AD27B12D68A3B7C1E(__this, NULL);
		return;
	}
}
// System.Single Oculus.Interaction.HandGrab.SnapSurfaces.CylinderSurface::Oculus.Interaction.HandGrab.SnapSurfaces.ISnapSurface.CalculateBestPoseAtSurface(UnityEngine.Pose& modreq(System.Runtime.InteropServices.InAttribute),UnityEngine.Pose& modreq(System.Runtime.InteropServices.InAttribute),UnityEngine.Pose&,Oculus.Interaction.HandGrab.PoseMeasureParameters& modreq(System.Runtime.InteropServices.InAttribute))
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float CylinderSurface_Oculus_Interaction_HandGrab_SnapSurfaces_ISnapSurface_CalculateBestPoseAtSurface_m961EB74DE806908879082B510C0BF85B039BB6D8 (CylinderSurface_t071F3C35CED4F1296F988B93595DF4736770D4F7* __this, Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* ___0_targetPose, Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* ___1_reference, Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* ___2_bestPose, PoseMeasureParameters_tB95EB8BC83F81E86544854D1B13C04E88F2C0203* ___3_scoringModifier, const RuntimeMethod* method) 
{
	{
		Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* L_0 = ___0_targetPose;
		Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* L_1 = ___1_reference;
		Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* L_2 = ___2_bestPose;
		PoseMeasureParameters_tB95EB8BC83F81E86544854D1B13C04E88F2C0203* L_3 = ___3_scoringModifier;
		float L_4;
		L_4 = CylinderSurface_CalculateBestPoseAtSurface_m00B0699D91CA4B6C7E1F61A571910FCA3558853B(__this, L_0, L_1, L_2, L_3, NULL);
		return L_4;
	}
}
// System.Boolean Oculus.Interaction.HandGrab.SnapSurfaces.CylinderSurface::Oculus.Interaction.HandGrab.SnapSurfaces.ISnapSurface.CalculateBestPoseAtSurface(UnityEngine.Ray,UnityEngine.Pose& modreq(System.Runtime.InteropServices.InAttribute),UnityEngine.Pose&)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool CylinderSurface_Oculus_Interaction_HandGrab_SnapSurfaces_ISnapSurface_CalculateBestPoseAtSurface_mA7C725C490000B926FDDC8F1F3B62E44B54D849F (CylinderSurface_t071F3C35CED4F1296F988B93595DF4736770D4F7* __this, Ray_t2B1742D7958DC05BDC3EFC7461D3593E1430DC00 ___0_targetRay, Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* ___1_reference, Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* ___2_bestPose, const RuntimeMethod* method) 
{
	{
		Ray_t2B1742D7958DC05BDC3EFC7461D3593E1430DC00 L_0 = ___0_targetRay;
		Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* L_1 = ___1_reference;
		Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* L_2 = ___2_bestPose;
		bool L_3;
		L_3 = CylinderSurface_CalculateBestPoseAtSurface_m1EDF5AF042F7CDEDAF7EB8EFD9385E632CBCEF82(__this, L_0, L_1, L_2, NULL);
		return L_3;
	}
}
// UnityEngine.Pose Oculus.Interaction.HandGrab.SnapSurfaces.CylinderSurface::Oculus.Interaction.HandGrab.SnapSurfaces.ISnapSurface.MirrorPose(UnityEngine.Pose& modreq(System.Runtime.InteropServices.InAttribute))
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971 CylinderSurface_Oculus_Interaction_HandGrab_SnapSurfaces_ISnapSurface_MirrorPose_m03F212D5B79A95397999CD6C908920C1A905DA4D (CylinderSurface_t071F3C35CED4F1296F988B93595DF4736770D4F7* __this, Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* ___0_gripPose, const RuntimeMethod* method) 
{
	{
		Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* L_0 = ___0_gripPose;
		Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971 L_1;
		L_1 = CylinderSurface_MirrorPose_mA421C34FD2EAA278F4FCD31596B60ACC88934CA6(__this, L_0, NULL);
		return L_1;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// System.Single Oculus.Interaction.HandGrab.SnapSurfaces.SnapSurfaceHelper::CalculateBestPoseAtSurface(UnityEngine.Pose&,UnityEngine.Pose&,UnityEngine.Pose&,Oculus.Interaction.HandGrab.PoseMeasureParameters&,Oculus.Interaction.HandGrab.SnapSurfaces.SnapSurfaceHelper/PoseCalculator,Oculus.Interaction.HandGrab.SnapSurfaces.SnapSurfaceHelper/PoseCalculator)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float SnapSurfaceHelper_CalculateBestPoseAtSurface_m59B6D35DAD5E611E28D9D44A33DF752471144B3E (Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* ___0_desiredPose, Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* ___1_snapPose, Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* ___2_bestPose, PoseMeasureParameters_tB95EB8BC83F81E86544854D1B13C04E88F2C0203* ___3_scoringModifier, PoseCalculator_t74377137B14A0C57A79A02A9C33C5814E64F1839* ___4_minimalTranslationPoseCalculator, PoseCalculator_t74377137B14A0C57A79A02A9C33C5814E64F1839* ___5_minimalRotationPoseCalculator, const RuntimeMethod* method) 
{
	float V_0 = 0.0f;
	Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971 V_1;
	memset((&V_1), 0, sizeof(V_1));
	PoseMeasureParameters_tB95EB8BC83F81E86544854D1B13C04E88F2C0203 V_2;
	memset((&V_2), 0, sizeof(V_2));
	Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971 V_3;
	memset((&V_3), 0, sizeof(V_3));
	{
		// Pose minimalRotationPose = minimalRotationPoseCalculator(desiredPose, snapPose);
		PoseCalculator_t74377137B14A0C57A79A02A9C33C5814E64F1839* L_0 = ___5_minimalRotationPoseCalculator;
		Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* L_1 = ___0_desiredPose;
		Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* L_2 = ___1_snapPose;
		NullCheck(L_0);
		Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971 L_3;
		L_3 = PoseCalculator_Invoke_m3E4562A6120025438594E5257CFC28EBE2489014_inline(L_0, L_1, L_2, NULL);
		V_1 = L_3;
		// if (scoringModifier.MaxDistance > 0)
		PoseMeasureParameters_tB95EB8BC83F81E86544854D1B13C04E88F2C0203* L_4 = ___3_scoringModifier;
		PoseMeasureParameters_tB95EB8BC83F81E86544854D1B13C04E88F2C0203 L_5 = (*(PoseMeasureParameters_tB95EB8BC83F81E86544854D1B13C04E88F2C0203*)L_4);
		V_2 = L_5;
		float L_6;
		L_6 = PoseMeasureParameters_get_MaxDistance_m7F443662F2C07ECBDFA621D40147E782266495EE_inline((&V_2), NULL);
		if ((!(((float)L_6) > ((float)(0.0f)))))
		{
			goto IL_0043;
		}
	}
	{
		// Pose minimalTranslationPose = minimalTranslationPoseCalculator(desiredPose, snapPose);
		PoseCalculator_t74377137B14A0C57A79A02A9C33C5814E64F1839* L_7 = ___4_minimalTranslationPoseCalculator;
		Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* L_8 = ___0_desiredPose;
		Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* L_9 = ___1_snapPose;
		NullCheck(L_7);
		Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971 L_10;
		L_10 = PoseCalculator_Invoke_m3E4562A6120025438594E5257CFC28EBE2489014_inline(L_7, L_8, L_9, NULL);
		V_3 = L_10;
		// bestPose = SelectBestPose(minimalRotationPose, minimalTranslationPose, desiredPose, scoringModifier, out bestScore);
		Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* L_11 = ___2_bestPose;
		Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* L_12 = ___0_desiredPose;
		PoseMeasureParameters_tB95EB8BC83F81E86544854D1B13C04E88F2C0203* L_13 = ___3_scoringModifier;
		PoseMeasureParameters_tB95EB8BC83F81E86544854D1B13C04E88F2C0203 L_14 = (*(PoseMeasureParameters_tB95EB8BC83F81E86544854D1B13C04E88F2C0203*)L_13);
		Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971 L_15;
		L_15 = SnapSurfaceHelper_SelectBestPose_m3C63764B6C1716502F14229D9533FF41136BAEC0((&V_1), (&V_3), L_12, L_14, (&V_0), NULL);
		*(Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971*)L_11 = L_15;
		goto IL_005c;
	}

IL_0043:
	{
		// bestPose = minimalRotationPose;
		Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* L_16 = ___2_bestPose;
		Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971 L_17 = V_1;
		*(Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971*)L_16 = L_17;
		// bestScore = PoseUtils.RotationalSimilarity(desiredPose.rotation, bestPose.rotation);
		Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* L_18 = ___0_desiredPose;
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974* L_19 = (&L_18->___rotation_1);
		Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* L_20 = ___2_bestPose;
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974* L_21 = (&L_20->___rotation_1);
		float L_22;
		L_22 = PoseUtils_RotationalSimilarity_m7863C9DC052D1B76B17754929FB2F31E06CFE0BD(L_19, L_21, NULL);
		V_0 = L_22;
	}

IL_005c:
	{
		// return bestScore;
		float L_23 = V_0;
		return L_23;
	}
}
// UnityEngine.Pose Oculus.Interaction.HandGrab.SnapSurfaces.SnapSurfaceHelper::SelectBestPose(UnityEngine.Pose&,UnityEngine.Pose&,UnityEngine.Pose&,Oculus.Interaction.HandGrab.PoseMeasureParameters,System.Single&)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971 SnapSurfaceHelper_SelectBestPose_m3C63764B6C1716502F14229D9533FF41136BAEC0 (Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* ___0_a, Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* ___1_b, Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* ___2_reference, PoseMeasureParameters_tB95EB8BC83F81E86544854D1B13C04E88F2C0203 ___3_scoringModifier, float* ___4_bestScore, const RuntimeMethod* method) 
{
	float V_0 = 0.0f;
	float V_1 = 0.0f;
	{
		// float aScore = PoseUtils.Similarity(reference, a, scoringModifier);
		Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* L_0 = ___2_reference;
		Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* L_1 = ___0_a;
		PoseMeasureParameters_tB95EB8BC83F81E86544854D1B13C04E88F2C0203 L_2 = ___3_scoringModifier;
		float L_3;
		L_3 = PoseUtils_Similarity_m1E048E90FD665EADAB792568C5B3A34543B184B1(L_0, L_1, L_2, NULL);
		V_0 = L_3;
		// float bScore = PoseUtils.Similarity(reference, b, scoringModifier);
		Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* L_4 = ___2_reference;
		Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* L_5 = ___1_b;
		PoseMeasureParameters_tB95EB8BC83F81E86544854D1B13C04E88F2C0203 L_6 = ___3_scoringModifier;
		float L_7;
		L_7 = PoseUtils_Similarity_m1E048E90FD665EADAB792568C5B3A34543B184B1(L_4, L_5, L_6, NULL);
		V_1 = L_7;
		// if (aScore >= bScore)
		float L_8 = V_0;
		float L_9 = V_1;
		if ((!(((float)L_8) >= ((float)L_9))))
		{
			goto IL_0021;
		}
	}
	{
		// bestScore = aScore;
		float* L_10 = ___4_bestScore;
		float L_11 = V_0;
		*((float*)L_10) = (float)L_11;
		// return a;
		Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* L_12 = ___0_a;
		Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971 L_13 = (*(Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971*)L_12);
		return L_13;
	}

IL_0021:
	{
		// bestScore = bScore;
		float* L_14 = ___4_bestScore;
		float L_15 = V_1;
		*((float*)L_14) = (float)L_15;
		// return b;
		Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* L_16 = ___1_b;
		Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971 L_17 = (*(Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971*)L_16);
		return L_17;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971 PoseCalculator_Invoke_m3E4562A6120025438594E5257CFC28EBE2489014_Multicast(PoseCalculator_t74377137B14A0C57A79A02A9C33C5814E64F1839* __this, Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* ___0_desiredPose, Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* ___1_snapPose, const RuntimeMethod* method)
{
	il2cpp_array_size_t length = __this->___delegates_13->max_length;
	Delegate_t** delegatesToInvoke = reinterpret_cast<Delegate_t**>(__this->___delegates_13->GetAddressAtUnchecked(0));
	Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971 retVal;
	memset((&retVal), 0, sizeof(retVal));
	for (il2cpp_array_size_t i = 0; i < length; i++)
	{
		PoseCalculator_t74377137B14A0C57A79A02A9C33C5814E64F1839* currentDelegate = reinterpret_cast<PoseCalculator_t74377137B14A0C57A79A02A9C33C5814E64F1839*>(delegatesToInvoke[i]);
		typedef Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971 (*FunctionPointerType) (RuntimeObject*, Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971*, Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971*, const RuntimeMethod*);
		retVal = ((FunctionPointerType)currentDelegate->___invoke_impl_1)((Il2CppObject*)currentDelegate->___method_code_6, ___0_desiredPose, ___1_snapPose, reinterpret_cast<RuntimeMethod*>(currentDelegate->___method_3));
	}
	return retVal;
}
Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971 PoseCalculator_Invoke_m3E4562A6120025438594E5257CFC28EBE2489014_OpenInst(PoseCalculator_t74377137B14A0C57A79A02A9C33C5814E64F1839* __this, Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* ___0_desiredPose, Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* ___1_snapPose, const RuntimeMethod* method)
{
	NullCheck(___0_desiredPose);
	typedef Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971 (*FunctionPointerType) (Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971*, Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971*, const RuntimeMethod*);
	return ((FunctionPointerType)__this->___method_ptr_0)(___0_desiredPose, ___1_snapPose, method);
}
Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971 PoseCalculator_Invoke_m3E4562A6120025438594E5257CFC28EBE2489014_OpenStatic(PoseCalculator_t74377137B14A0C57A79A02A9C33C5814E64F1839* __this, Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* ___0_desiredPose, Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* ___1_snapPose, const RuntimeMethod* method)
{
	typedef Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971 (*FunctionPointerType) (Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971*, Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971*, const RuntimeMethod*);
	return ((FunctionPointerType)__this->___method_ptr_0)(___0_desiredPose, ___1_snapPose, method);
}
Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971 PoseCalculator_Invoke_m3E4562A6120025438594E5257CFC28EBE2489014_OpenStaticInvoker(PoseCalculator_t74377137B14A0C57A79A02A9C33C5814E64F1839* __this, Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* ___0_desiredPose, Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* ___1_snapPose, const RuntimeMethod* method)
{
	return InvokerFuncInvoker2< Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971, Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971*, Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* >::Invoke(__this->___method_ptr_0, method, NULL, ___0_desiredPose, ___1_snapPose);
}
Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971 PoseCalculator_Invoke_m3E4562A6120025438594E5257CFC28EBE2489014_ClosedStaticInvoker(PoseCalculator_t74377137B14A0C57A79A02A9C33C5814E64F1839* __this, Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* ___0_desiredPose, Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* ___1_snapPose, const RuntimeMethod* method)
{
	return InvokerFuncInvoker3< Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971, RuntimeObject*, Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971*, Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* >::Invoke(__this->___method_ptr_0, method, NULL, __this->___m_target_2, ___0_desiredPose, ___1_snapPose);
}
IL2CPP_EXTERN_C  Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971 DelegatePInvokeWrapper_PoseCalculator_t74377137B14A0C57A79A02A9C33C5814E64F1839 (PoseCalculator_t74377137B14A0C57A79A02A9C33C5814E64F1839* __this, Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* ___0_desiredPose, Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* ___1_snapPose, const RuntimeMethod* method)
{
	typedef Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971 (DEFAULT_CALL *PInvokeFunc)(Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971*, Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971*);
	PInvokeFunc il2cppPInvokeFunc = reinterpret_cast<PInvokeFunc>(il2cpp_codegen_get_reverse_pinvoke_function_ptr(__this));
	// Native function invocation
	Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971 returnValue = il2cppPInvokeFunc(___0_desiredPose, ___1_snapPose);

	return returnValue;
}
// System.Void Oculus.Interaction.HandGrab.SnapSurfaces.SnapSurfaceHelper/PoseCalculator::.ctor(System.Object,System.IntPtr)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void PoseCalculator__ctor_mCB0A713E75C96AE4D9C311D91EBBE4A016F8763B (PoseCalculator_t74377137B14A0C57A79A02A9C33C5814E64F1839* __this, RuntimeObject* ___0_object, intptr_t ___1_method, const RuntimeMethod* method) 
{
	__this->___method_ptr_0 = il2cpp_codegen_get_virtual_call_method_pointer((RuntimeMethod*)___1_method);
	__this->___method_3 = ___1_method;
	__this->___m_target_2 = ___0_object;
	Il2CppCodeGenWriteBarrier((void**)(&__this->___m_target_2), (void*)___0_object);
	int parameterCount = il2cpp_codegen_method_parameter_count((RuntimeMethod*)___1_method);
	__this->___method_code_6 = (intptr_t)__this;
	if (MethodIsStatic((RuntimeMethod*)___1_method))
	{
		bool isOpen = parameterCount == 2;
		if (il2cpp_codegen_call_method_via_invoker((RuntimeMethod*)___1_method))
			if (isOpen)
				__this->___invoke_impl_1 = (intptr_t)&PoseCalculator_Invoke_m3E4562A6120025438594E5257CFC28EBE2489014_OpenStaticInvoker;
			else
				__this->___invoke_impl_1 = (intptr_t)&PoseCalculator_Invoke_m3E4562A6120025438594E5257CFC28EBE2489014_ClosedStaticInvoker;
		else
			if (isOpen)
				__this->___invoke_impl_1 = (intptr_t)&PoseCalculator_Invoke_m3E4562A6120025438594E5257CFC28EBE2489014_OpenStatic;
			else
				{
					__this->___invoke_impl_1 = (intptr_t)__this->___method_ptr_0;
					__this->___method_code_6 = (intptr_t)__this->___m_target_2;
				}
	}
	else
	{
		bool isOpen = parameterCount == 1;
		if (isOpen)
		{
			__this->___invoke_impl_1 = (intptr_t)&PoseCalculator_Invoke_m3E4562A6120025438594E5257CFC28EBE2489014_OpenInst;
		}
		else
		{
			if (___0_object == NULL)
				il2cpp_codegen_raise_exception(il2cpp_codegen_get_argument_exception(NULL, "Delegate to an instance method cannot have null 'this'."), NULL);
			__this->___invoke_impl_1 = (intptr_t)__this->___method_ptr_0;
			__this->___method_code_6 = (intptr_t)__this->___m_target_2;
		}
	}
	__this->___extra_arg_5 = (intptr_t)&PoseCalculator_Invoke_m3E4562A6120025438594E5257CFC28EBE2489014_Multicast;
}
// UnityEngine.Pose Oculus.Interaction.HandGrab.SnapSurfaces.SnapSurfaceHelper/PoseCalculator::Invoke(UnityEngine.Pose& modreq(System.Runtime.InteropServices.InAttribute),UnityEngine.Pose& modreq(System.Runtime.InteropServices.InAttribute))
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971 PoseCalculator_Invoke_m3E4562A6120025438594E5257CFC28EBE2489014 (PoseCalculator_t74377137B14A0C57A79A02A9C33C5814E64F1839* __this, Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* ___0_desiredPose, Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* ___1_snapPose, const RuntimeMethod* method) 
{
	typedef Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971 (*FunctionPointerType) (RuntimeObject*, Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971*, Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971*, const RuntimeMethod*);
	return ((FunctionPointerType)__this->___invoke_impl_1)((Il2CppObject*)__this->___method_code_6, ___0_desiredPose, ___1_snapPose, reinterpret_cast<RuntimeMethod*>(__this->___method_3));
}
// System.IAsyncResult Oculus.Interaction.HandGrab.SnapSurfaces.SnapSurfaceHelper/PoseCalculator::BeginInvoke(UnityEngine.Pose& modreq(System.Runtime.InteropServices.InAttribute),UnityEngine.Pose& modreq(System.Runtime.InteropServices.InAttribute),System.AsyncCallback,System.Object)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* PoseCalculator_BeginInvoke_m9A6EB4DAAD26979C53AA498C2E319689BCD01F29 (PoseCalculator_t74377137B14A0C57A79A02A9C33C5814E64F1839* __this, Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* ___0_desiredPose, Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* ___1_snapPose, AsyncCallback_t7FEF460CBDCFB9C5FA2EF776984778B9A4145F4C* ___2_callback, RuntimeObject* ___3_object, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	void *__d_args[3] = {0};
	__d_args[0] = Box(Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971_il2cpp_TypeInfo_var, &*___0_desiredPose);
	__d_args[1] = Box(Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971_il2cpp_TypeInfo_var, &*___1_snapPose);
	return (RuntimeObject*)il2cpp_codegen_delegate_begin_invoke((RuntimeDelegate*)__this, __d_args, (RuntimeDelegate*)___2_callback, (RuntimeObject*)___3_object);
}
// UnityEngine.Pose Oculus.Interaction.HandGrab.SnapSurfaces.SnapSurfaceHelper/PoseCalculator::EndInvoke(UnityEngine.Pose& modreq(System.Runtime.InteropServices.InAttribute),UnityEngine.Pose& modreq(System.Runtime.InteropServices.InAttribute),System.IAsyncResult)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971 PoseCalculator_EndInvoke_m348313336F22439CEC647FD636D026F2DEC15E9F (PoseCalculator_t74377137B14A0C57A79A02A9C33C5814E64F1839* __this, Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* ___0_desiredPose, Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* ___1_snapPose, RuntimeObject* ___2_result, const RuntimeMethod* method) 
{
	RuntimeObject *__result = il2cpp_codegen_delegate_end_invoke((Il2CppAsyncResult*) ___2_result, 0);
	return *(Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971*)UnBox ((RuntimeObject*)__result);
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// System.Object Oculus.Interaction.HandGrab.SnapSurfaces.SphereSurfaceData::Clone()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* SphereSurfaceData_Clone_m901F6F5CF4062FC6C1AEA3089478B857002CB7DB (SphereSurfaceData_tE8331CA08F2DB69533F08417636C96FFA01E7CC1* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&SphereSurfaceData_tE8331CA08F2DB69533F08417636C96FFA01E7CC1_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// SphereSurfaceData clone = new SphereSurfaceData();
		SphereSurfaceData_tE8331CA08F2DB69533F08417636C96FFA01E7CC1* L_0 = (SphereSurfaceData_tE8331CA08F2DB69533F08417636C96FFA01E7CC1*)il2cpp_codegen_object_new(SphereSurfaceData_tE8331CA08F2DB69533F08417636C96FFA01E7CC1_il2cpp_TypeInfo_var);
		NullCheck(L_0);
		SphereSurfaceData__ctor_mC4A3CEB2FC052A2580F50116E0B4C351ABBA389C(L_0, NULL);
		// clone.centre = this.centre;
		SphereSurfaceData_tE8331CA08F2DB69533F08417636C96FFA01E7CC1* L_1 = L_0;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_2 = __this->___centre_0;
		NullCheck(L_1);
		L_1->___centre_0 = L_2;
		// return clone;
		return L_1;
	}
}
// Oculus.Interaction.HandGrab.SnapSurfaces.SphereSurfaceData Oculus.Interaction.HandGrab.SnapSurfaces.SphereSurfaceData::Mirror()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR SphereSurfaceData_tE8331CA08F2DB69533F08417636C96FFA01E7CC1* SphereSurfaceData_Mirror_m783B2836B5EA587D72120C22298A11C93433CD56 (SphereSurfaceData_tE8331CA08F2DB69533F08417636C96FFA01E7CC1* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&SphereSurfaceData_tE8331CA08F2DB69533F08417636C96FFA01E7CC1_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// SphereSurfaceData mirror = Clone() as SphereSurfaceData;
		RuntimeObject* L_0;
		L_0 = SphereSurfaceData_Clone_m901F6F5CF4062FC6C1AEA3089478B857002CB7DB(__this, NULL);
		// return mirror;
		return ((SphereSurfaceData_tE8331CA08F2DB69533F08417636C96FFA01E7CC1*)IsInstClass((RuntimeObject*)L_0, SphereSurfaceData_tE8331CA08F2DB69533F08417636C96FFA01E7CC1_il2cpp_TypeInfo_var));
	}
}
// System.Void Oculus.Interaction.HandGrab.SnapSurfaces.SphereSurfaceData::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void SphereSurfaceData__ctor_mC4A3CEB2FC052A2580F50116E0B4C351ABBA389C (SphereSurfaceData_tE8331CA08F2DB69533F08417636C96FFA01E7CC1* __this, const RuntimeMethod* method) 
{
	{
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2(__this, NULL);
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// Oculus.Interaction.HandGrab.SnapSurfaces.SphereSurfaceData Oculus.Interaction.HandGrab.SnapSurfaces.SphereSurface::get_Data()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR SphereSurfaceData_tE8331CA08F2DB69533F08417636C96FFA01E7CC1* SphereSurface_get_Data_m3AEFCA31B76F4C8E1826618CEBF99E7DA996C059 (SphereSurface_t7C6F8A614D76E8B3AE10EE722BBDB6774B96A30B* __this, const RuntimeMethod* method) 
{
	{
		// return _data;
		SphereSurfaceData_tE8331CA08F2DB69533F08417636C96FFA01E7CC1* L_0 = __this->____data_4;
		return L_0;
	}
}
// System.Void Oculus.Interaction.HandGrab.SnapSurfaces.SphereSurface::set_Data(Oculus.Interaction.HandGrab.SnapSurfaces.SphereSurfaceData)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void SphereSurface_set_Data_m463CDA705345ED889DAD5A98A3A704F56A65BD26 (SphereSurface_t7C6F8A614D76E8B3AE10EE722BBDB6774B96A30B* __this, SphereSurfaceData_tE8331CA08F2DB69533F08417636C96FFA01E7CC1* ___0_value, const RuntimeMethod* method) 
{
	{
		// _data = value;
		SphereSurfaceData_tE8331CA08F2DB69533F08417636C96FFA01E7CC1* L_0 = ___0_value;
		__this->____data_4 = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->____data_4), (void*)L_0);
		// }
		return;
	}
}
// UnityEngine.Transform Oculus.Interaction.HandGrab.SnapSurfaces.SphereSurface::get_RelativeTo()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* SphereSurface_get_RelativeTo_m012CCC88748350E90BC484526CB65F7AD94132CC (SphereSurface_t7C6F8A614D76E8B3AE10EE722BBDB6774B96A30B* __this, const RuntimeMethod* method) 
{
	{
		// get => _relativeTo;
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_0 = __this->____relativeTo_5;
		return L_0;
	}
}
// System.Void Oculus.Interaction.HandGrab.SnapSurfaces.SphereSurface::set_RelativeTo(UnityEngine.Transform)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void SphereSurface_set_RelativeTo_mB70C4EDC8C960D5F0FFB8EDFF99E3FDF51BC4233 (SphereSurface_t7C6F8A614D76E8B3AE10EE722BBDB6774B96A30B* __this, Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* ___0_value, const RuntimeMethod* method) 
{
	{
		// set => _relativeTo = value;
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_0 = ___0_value;
		__this->____relativeTo_5 = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->____relativeTo_5), (void*)L_0);
		return;
	}
}
// UnityEngine.Transform Oculus.Interaction.HandGrab.SnapSurfaces.SphereSurface::get_GripPoint()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* SphereSurface_get_GripPoint_m5E0F7198EDAC70BECB20A3B861CD7C854324ABA6 (SphereSurface_t7C6F8A614D76E8B3AE10EE722BBDB6774B96A30B* __this, const RuntimeMethod* method) 
{
	{
		// get => _gripPoint;
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_0 = __this->____gripPoint_6;
		return L_0;
	}
}
// System.Void Oculus.Interaction.HandGrab.SnapSurfaces.SphereSurface::set_GripPoint(UnityEngine.Transform)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void SphereSurface_set_GripPoint_m3F4FD3B9AC2E6C9FBA43A984C33032A0AF384F6F (SphereSurface_t7C6F8A614D76E8B3AE10EE722BBDB6774B96A30B* __this, Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* ___0_value, const RuntimeMethod* method) 
{
	{
		// set => _gripPoint = value;
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_0 = ___0_value;
		__this->____gripPoint_6 = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->____gripPoint_6), (void*)L_0);
		return;
	}
}
// UnityEngine.Vector3 Oculus.Interaction.HandGrab.SnapSurfaces.SphereSurface::get_Centre()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 SphereSurface_get_Centre_mDB3654A871802C00C70C60409AA15AD8F7EAF00B (SphereSurface_t7C6F8A614D76E8B3AE10EE722BBDB6774B96A30B* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// if (RelativeTo != null)
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_0;
		L_0 = SphereSurface_get_RelativeTo_m012CCC88748350E90BC484526CB65F7AD94132CC_inline(__this, NULL);
		il2cpp_codegen_runtime_class_init_inline(Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		bool L_1;
		L_1 = Object_op_Inequality_mD0BE578448EAA61948F25C32F8DD55AB1F778602(L_0, (Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C*)NULL, NULL);
		if (!L_1)
		{
			goto IL_0025;
		}
	}
	{
		// return RelativeTo.TransformPoint(_data.centre);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_2;
		L_2 = SphereSurface_get_RelativeTo_m012CCC88748350E90BC484526CB65F7AD94132CC_inline(__this, NULL);
		SphereSurfaceData_tE8331CA08F2DB69533F08417636C96FFA01E7CC1* L_3 = __this->____data_4;
		NullCheck(L_3);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_4 = L_3->___centre_0;
		NullCheck(L_2);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_5;
		L_5 = Transform_TransformPoint_m05BFF013DB830D7BFE44A007703694AE1062EE44(L_2, L_4, NULL);
		return L_5;
	}

IL_0025:
	{
		// return _data.centre;
		SphereSurfaceData_tE8331CA08F2DB69533F08417636C96FFA01E7CC1* L_6 = __this->____data_4;
		NullCheck(L_6);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_7 = L_6->___centre_0;
		return L_7;
	}
}
// System.Void Oculus.Interaction.HandGrab.SnapSurfaces.SphereSurface::set_Centre(UnityEngine.Vector3)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void SphereSurface_set_Centre_mC45D2EB1ADA132142996114E764FA50945725C23 (SphereSurface_t7C6F8A614D76E8B3AE10EE722BBDB6774B96A30B* __this, Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___0_value, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// if (RelativeTo != null)
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_0;
		L_0 = SphereSurface_get_RelativeTo_m012CCC88748350E90BC484526CB65F7AD94132CC_inline(__this, NULL);
		il2cpp_codegen_runtime_class_init_inline(Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		bool L_1;
		L_1 = Object_op_Inequality_mD0BE578448EAA61948F25C32F8DD55AB1F778602(L_0, (Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C*)NULL, NULL);
		if (!L_1)
		{
			goto IL_0026;
		}
	}
	{
		// _data.centre = RelativeTo.InverseTransformPoint(value);
		SphereSurfaceData_tE8331CA08F2DB69533F08417636C96FFA01E7CC1* L_2 = __this->____data_4;
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_3;
		L_3 = SphereSurface_get_RelativeTo_m012CCC88748350E90BC484526CB65F7AD94132CC_inline(__this, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_4 = ___0_value;
		NullCheck(L_3);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_5;
		L_5 = Transform_InverseTransformPoint_m18CD395144D9C78F30E15A5B82B6670E792DBA5D(L_3, L_4, NULL);
		NullCheck(L_2);
		L_2->___centre_0 = L_5;
		return;
	}

IL_0026:
	{
		// _data.centre = value;
		SphereSurfaceData_tE8331CA08F2DB69533F08417636C96FFA01E7CC1* L_6 = __this->____data_4;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_7 = ___0_value;
		NullCheck(L_6);
		L_6->___centre_0 = L_7;
		// }
		return;
	}
}
// System.Single Oculus.Interaction.HandGrab.SnapSurfaces.SphereSurface::get_Radius()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float SphereSurface_get_Radius_m3A32B797BC6684468CC1BA7C6E35DD04100288DF (SphereSurface_t7C6F8A614D76E8B3AE10EE722BBDB6774B96A30B* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// if (this.GripPoint == null)
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_0;
		L_0 = SphereSurface_get_GripPoint_m5E0F7198EDAC70BECB20A3B861CD7C854324ABA6_inline(__this, NULL);
		il2cpp_codegen_runtime_class_init_inline(Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		bool L_1;
		L_1 = Object_op_Equality_mB6120F782D83091EF56A198FCEBCF066DB4A9605(L_0, (Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C*)NULL, NULL);
		if (!L_1)
		{
			goto IL_0014;
		}
	}
	{
		// return 0f;
		return (0.0f);
	}

IL_0014:
	{
		// return Vector3.Distance(Centre, this.GripPoint.position);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_2;
		L_2 = SphereSurface_get_Centre_mDB3654A871802C00C70C60409AA15AD8F7EAF00B(__this, NULL);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_3;
		L_3 = SphereSurface_get_GripPoint_m5E0F7198EDAC70BECB20A3B861CD7C854324ABA6_inline(__this, NULL);
		NullCheck(L_3);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_4;
		L_4 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_3, NULL);
		float L_5;
		L_5 = Vector3_Distance_m2314DB9B8BD01157E013DF87BEA557375C7F9FF9_inline(L_2, L_4, NULL);
		return L_5;
	}
}
// UnityEngine.Vector3 Oculus.Interaction.HandGrab.SnapSurfaces.SphereSurface::get_Direction()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 SphereSurface_get_Direction_mEF366B8EE0B94083D54C4E3716AAEAF02E9AD9F5 (SphereSurface_t7C6F8A614D76E8B3AE10EE722BBDB6774B96A30B* __this, const RuntimeMethod* method) 
{
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		// return (this.GripPoint.position - Centre).normalized;
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_0;
		L_0 = SphereSurface_get_GripPoint_m5E0F7198EDAC70BECB20A3B861CD7C854324ABA6_inline(__this, NULL);
		NullCheck(L_0);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_1;
		L_1 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_0, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_2;
		L_2 = SphereSurface_get_Centre_mDB3654A871802C00C70C60409AA15AD8F7EAF00B(__this, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_3;
		L_3 = Vector3_op_Subtraction_mE42023FF80067CB44A1D4A27EB7CF2B24CABB828_inline(L_1, L_2, NULL);
		V_0 = L_3;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_4;
		L_4 = Vector3_get_normalized_m736BBF65D5CDA7A18414370D15B4DFCC1E466F07_inline((&V_0), NULL);
		return L_4;
	}
}
// UnityEngine.Quaternion Oculus.Interaction.HandGrab.SnapSurfaces.SphereSurface::get_Rotation()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 SphereSurface_get_Rotation_m4D67517D6E3B6DD7FEEBD7A488BA1A978B37C473 (SphereSurface_t7C6F8A614D76E8B3AE10EE722BBDB6774B96A30B* __this, const RuntimeMethod* method) 
{
	{
		// return Quaternion.LookRotation(Direction, this.GripPoint.forward);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_0;
		L_0 = SphereSurface_get_Direction_mEF366B8EE0B94083D54C4E3716AAEAF02E9AD9F5(__this, NULL);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_1;
		L_1 = SphereSurface_get_GripPoint_m5E0F7198EDAC70BECB20A3B861CD7C854324ABA6_inline(__this, NULL);
		NullCheck(L_1);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_2;
		L_2 = Transform_get_forward_mFCFACF7165FDAB21E80E384C494DF278386CEE2F(L_1, NULL);
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_3;
		L_3 = Quaternion_LookRotation_mFB02EDC8F733774DFAC3BEA4B4BB265A228F8307(L_0, L_2, NULL);
		return L_3;
	}
}
// System.Void Oculus.Interaction.HandGrab.SnapSurfaces.SphereSurface::Reset()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void SphereSurface_Reset_mE26AC62850264D13CC9A50C9E493ED18A9B865E7 (SphereSurface_t7C6F8A614D76E8B3AE10EE722BBDB6774B96A30B* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Component_TryGetComponent_TisHandGrabPose_t347682EF05B32B395F27F50DB38500405C17C633_m48D0952AB02E5B8946E540747BC71058019E55C6_RuntimeMethod_var);
		s_Il2CppMethodInitialized = true;
	}
	HandGrabPose_t347682EF05B32B395F27F50DB38500405C17C633* V_0 = NULL;
	{
		// _gripPoint = this.transform;
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_0;
		L_0 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(__this, NULL);
		__this->____gripPoint_6 = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->____gripPoint_6), (void*)L_0);
		// if (this.TryGetComponent(out HandGrabPose grabPose))
		bool L_1;
		L_1 = Component_TryGetComponent_TisHandGrabPose_t347682EF05B32B395F27F50DB38500405C17C633_m48D0952AB02E5B8946E540747BC71058019E55C6(__this, (&V_0), Component_TryGetComponent_TisHandGrabPose_t347682EF05B32B395F27F50DB38500405C17C633_m48D0952AB02E5B8946E540747BC71058019E55C6_RuntimeMethod_var);
		if (!L_1)
		{
			goto IL_0022;
		}
	}
	{
		// _relativeTo = grabPose.RelativeTo;
		HandGrabPose_t347682EF05B32B395F27F50DB38500405C17C633* L_2 = V_0;
		NullCheck(L_2);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_3;
		L_3 = HandGrabPose_get_RelativeTo_m5DFEA3BC65DF4B58C8BB75628AFB81A18B9399C4_inline(L_2, NULL);
		__this->____relativeTo_5 = L_3;
		Il2CppCodeGenWriteBarrier((void**)(&__this->____relativeTo_5), (void*)L_3);
	}

IL_0022:
	{
		// }
		return;
	}
}
// System.Void Oculus.Interaction.HandGrab.SnapSurfaces.SphereSurface::Start()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void SphereSurface_Start_m5591282849F5A9A85E7F91E327C947B1CC7DDEED (SphereSurface_t7C6F8A614D76E8B3AE10EE722BBDB6774B96A30B* __this, const RuntimeMethod* method) 
{
	{
		// }
		return;
	}
}
// UnityEngine.Pose Oculus.Interaction.HandGrab.SnapSurfaces.SphereSurface::MirrorPose(UnityEngine.Pose&)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971 SphereSurface_MirrorPose_m25AE869A0553646F0CCBF97B12E16C491E1CDC62 (SphereSurface_t7C6F8A614D76E8B3AE10EE722BBDB6774B96A30B* __this, Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* ___0_pose, const RuntimeMethod* method) 
{
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 V_0;
	memset((&V_0), 0, sizeof(V_0));
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 V_1;
	memset((&V_1), 0, sizeof(V_1));
	{
		// Vector3 normal = Quaternion.Inverse(RelativeTo.rotation) * Direction;
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_0;
		L_0 = SphereSurface_get_RelativeTo_m012CCC88748350E90BC484526CB65F7AD94132CC_inline(__this, NULL);
		NullCheck(L_0);
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_1;
		L_1 = Transform_get_rotation_m32AF40CA0D50C797DA639A696F8EAEC7524C179C(L_0, NULL);
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_2;
		L_2 = Quaternion_Inverse_mD9C060AC626A7B406F4984AC98F8358DC89EF512(L_1, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_3;
		L_3 = SphereSurface_get_Direction_mEF366B8EE0B94083D54C4E3716AAEAF02E9AD9F5(__this, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_4;
		L_4 = Quaternion_op_Multiply_mE1EBA73F9173432B50F8F17CE8190C5A7986FB8C(L_2, L_3, NULL);
		V_0 = L_4;
		// Vector3 tangent = Vector3.Cross(normal, Vector3.up);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_5 = V_0;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_6;
		L_6 = Vector3_get_up_m128AF3FDC820BF59D5DE86D973E7DE3F20C3AEBA_inline(NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_7;
		L_7 = Vector3_Cross_mF93A280558BCE756D13B6CC5DCD7DE8A43148987_inline(L_5, L_6, NULL);
		V_1 = L_7;
		// return pose.MirrorPoseRotation(normal, tangent);
		Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* L_8 = ___0_pose;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_9 = V_0;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_10 = V_1;
		Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971 L_11;
		L_11 = PoseUtils_MirrorPoseRotation_m0F4E6DD5661B752CE896089B11F9483DF19E5C5F(L_8, L_9, L_10, NULL);
		return L_11;
	}
}
// System.Boolean Oculus.Interaction.HandGrab.SnapSurfaces.SphereSurface::CalculateBestPoseAtSurface(UnityEngine.Ray,UnityEngine.Pose&,UnityEngine.Pose&)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool SphereSurface_CalculateBestPoseAtSurface_m75444B60CED07377ED2BB91B7BB15B7E220C613E (SphereSurface_t7C6F8A614D76E8B3AE10EE722BBDB6774B96A30B* __this, Ray_t2B1742D7958DC05BDC3EFC7461D3593E1430DC00 ___0_targetRay, Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* ___1_recordedPose, Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* ___2_bestPose, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&SingleU5BU5D_t89DEFE97BCEDB5857010E79ECE0F52CF6E93B87C_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 V_0;
	memset((&V_0), 0, sizeof(V_0));
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 V_1;
	memset((&V_1), 0, sizeof(V_1));
	float V_2 = 0.0f;
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 V_3;
	memset((&V_3), 0, sizeof(V_3));
	Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971 V_4;
	memset((&V_4), 0, sizeof(V_4));
	float V_5 = 0.0f;
	{
		// Vector3 projection = Vector3.Project(Centre - targetRay.origin, targetRay.direction);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_0;
		L_0 = SphereSurface_get_Centre_mDB3654A871802C00C70C60409AA15AD8F7EAF00B(__this, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_1;
		L_1 = Ray_get_origin_m97604A8F180316A410DCD77B7D74D04522FA1BA6((&___0_targetRay), NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_2;
		L_2 = Vector3_op_Subtraction_mE42023FF80067CB44A1D4A27EB7CF2B24CABB828_inline(L_0, L_1, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_3;
		L_3 = Ray_get_direction_m21C2D22D3BD4A683BD4DC191AB22DD05F5EC2086((&___0_targetRay), NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_4;
		L_4 = Vector3_Project_m85DF3CB297EC5E1A17BD6266FF65E86AB7372C9B_inline(L_2, L_3, NULL);
		V_0 = L_4;
		// Vector3 nearestCentre = targetRay.origin + projection;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_5;
		L_5 = Ray_get_origin_m97604A8F180316A410DCD77B7D74D04522FA1BA6((&___0_targetRay), NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_6 = V_0;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_7;
		L_7 = Vector3_op_Addition_m78C0EC70CB66E8DCAC225743D82B268DAEE92067_inline(L_5, L_6, NULL);
		V_1 = L_7;
		// float distanceToSurface = Mathf.Max(Vector3.Distance(Centre, nearestCentre) - Radius);
		SingleU5BU5D_t89DEFE97BCEDB5857010E79ECE0F52CF6E93B87C* L_8 = (SingleU5BU5D_t89DEFE97BCEDB5857010E79ECE0F52CF6E93B87C*)(SingleU5BU5D_t89DEFE97BCEDB5857010E79ECE0F52CF6E93B87C*)SZArrayNew(SingleU5BU5D_t89DEFE97BCEDB5857010E79ECE0F52CF6E93B87C_il2cpp_TypeInfo_var, (uint32_t)1);
		SingleU5BU5D_t89DEFE97BCEDB5857010E79ECE0F52CF6E93B87C* L_9 = L_8;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_10;
		L_10 = SphereSurface_get_Centre_mDB3654A871802C00C70C60409AA15AD8F7EAF00B(__this, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_11 = V_1;
		float L_12;
		L_12 = Vector3_Distance_m2314DB9B8BD01157E013DF87BEA557375C7F9FF9_inline(L_10, L_11, NULL);
		float L_13;
		L_13 = SphereSurface_get_Radius_m3A32B797BC6684468CC1BA7C6E35DD04100288DF(__this, NULL);
		NullCheck(L_9);
		(L_9)->SetAt(static_cast<il2cpp_array_size_t>(0), (float)((float)il2cpp_codegen_subtract(L_12, L_13)));
		float L_14;
		L_14 = Mathf_Max_m870B77F6A5225EC21D9778043F987B9B17D08F6B_inline(L_9, NULL);
		V_2 = L_14;
		// if (distanceToSurface < Radius)
		float L_15 = V_2;
		float L_16;
		L_16 = SphereSurface_get_Radius_m3A32B797BC6684468CC1BA7C6E35DD04100288DF(__this, NULL);
		if ((!(((float)L_15) < ((float)L_16))))
		{
			goto IL_0085;
		}
	}
	{
		// float adjustedDistance = Mathf.Sqrt(Radius * Radius - distanceToSurface * distanceToSurface);
		float L_17;
		L_17 = SphereSurface_get_Radius_m3A32B797BC6684468CC1BA7C6E35DD04100288DF(__this, NULL);
		float L_18;
		L_18 = SphereSurface_get_Radius_m3A32B797BC6684468CC1BA7C6E35DD04100288DF(__this, NULL);
		float L_19 = V_2;
		float L_20 = V_2;
		float L_21;
		L_21 = sqrtf(((float)il2cpp_codegen_subtract(((float)il2cpp_codegen_multiply(L_17, L_18)), ((float)il2cpp_codegen_multiply(L_19, L_20)))));
		V_5 = L_21;
		// nearestCentre -= targetRay.direction * adjustedDistance;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_22 = V_1;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_23;
		L_23 = Ray_get_direction_m21C2D22D3BD4A683BD4DC191AB22DD05F5EC2086((&___0_targetRay), NULL);
		float L_24 = V_5;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_25;
		L_25 = Vector3_op_Multiply_m87BA7C578F96C8E49BB07088DAAC4649F83B0353_inline(L_23, L_24, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_26;
		L_26 = Vector3_op_Subtraction_mE42023FF80067CB44A1D4A27EB7CF2B24CABB828_inline(L_22, L_25, NULL);
		V_1 = L_26;
	}

IL_0085:
	{
		// Vector3 surfacePoint = NearestPointInSurface(nearestCentre);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_27 = V_1;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_28;
		L_28 = SphereSurface_NearestPointInSurface_m8571F38D5010A262130B3D0918187AE76290470D(__this, L_27, NULL);
		V_3 = L_28;
		// Pose desiredPose = new Pose(surfacePoint, recordedPose.rotation);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_29 = V_3;
		Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* L_30 = ___1_recordedPose;
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_31 = L_30->___rotation_1;
		Pose__ctor_m15CA45808A2BBF1956E836D22C387FAB80BED051((&V_4), L_29, L_31, NULL);
		// bestPose = MinimalTranslationPoseAtSurface(desiredPose, recordedPose);
		Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* L_32 = ___2_bestPose;
		Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* L_33 = ___1_recordedPose;
		Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971 L_34;
		L_34 = SphereSurface_MinimalTranslationPoseAtSurface_m888D23B10E2443C9E359760B9F4927D8D0B2A3D6(__this, (&V_4), L_33, NULL);
		*(Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971*)L_32 = L_34;
		// return true;
		return (bool)1;
	}
}
// System.Single Oculus.Interaction.HandGrab.SnapSurfaces.SphereSurface::CalculateBestPoseAtSurface(UnityEngine.Pose&,UnityEngine.Pose&,UnityEngine.Pose&,Oculus.Interaction.HandGrab.PoseMeasureParameters&)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float SphereSurface_CalculateBestPoseAtSurface_m5C024F13CD2E88EF909798301ED2322AFE6629B2 (SphereSurface_t7C6F8A614D76E8B3AE10EE722BBDB6774B96A30B* __this, Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* ___0_targetPose, Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* ___1_reference, Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* ___2_bestPose, PoseMeasureParameters_tB95EB8BC83F81E86544854D1B13C04E88F2C0203* ___3_scoringModifier, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&PoseCalculator_t74377137B14A0C57A79A02A9C33C5814E64F1839_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&SphereSurface_MinimalRotationPoseAtSurface_mAA27B9D91F134707E946B7718F6B29EF1E5CEF1C_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&SphereSurface_MinimalTranslationPoseAtSurface_m888D23B10E2443C9E359760B9F4927D8D0B2A3D6_RuntimeMethod_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// return SnapSurfaceHelper.CalculateBestPoseAtSurface(targetPose, reference, out bestPose,
		//     scoringModifier, MinimalTranslationPoseAtSurface, MinimalRotationPoseAtSurface);
		Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* L_0 = ___0_targetPose;
		Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* L_1 = ___1_reference;
		Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* L_2 = ___2_bestPose;
		PoseMeasureParameters_tB95EB8BC83F81E86544854D1B13C04E88F2C0203* L_3 = ___3_scoringModifier;
		PoseCalculator_t74377137B14A0C57A79A02A9C33C5814E64F1839* L_4 = (PoseCalculator_t74377137B14A0C57A79A02A9C33C5814E64F1839*)il2cpp_codegen_object_new(PoseCalculator_t74377137B14A0C57A79A02A9C33C5814E64F1839_il2cpp_TypeInfo_var);
		NullCheck(L_4);
		PoseCalculator__ctor_mCB0A713E75C96AE4D9C311D91EBBE4A016F8763B(L_4, __this, (intptr_t)((void*)SphereSurface_MinimalTranslationPoseAtSurface_m888D23B10E2443C9E359760B9F4927D8D0B2A3D6_RuntimeMethod_var), NULL);
		PoseCalculator_t74377137B14A0C57A79A02A9C33C5814E64F1839* L_5 = (PoseCalculator_t74377137B14A0C57A79A02A9C33C5814E64F1839*)il2cpp_codegen_object_new(PoseCalculator_t74377137B14A0C57A79A02A9C33C5814E64F1839_il2cpp_TypeInfo_var);
		NullCheck(L_5);
		PoseCalculator__ctor_mCB0A713E75C96AE4D9C311D91EBBE4A016F8763B(L_5, __this, (intptr_t)((void*)SphereSurface_MinimalRotationPoseAtSurface_mAA27B9D91F134707E946B7718F6B29EF1E5CEF1C_RuntimeMethod_var), NULL);
		float L_6;
		L_6 = SnapSurfaceHelper_CalculateBestPoseAtSurface_m59B6D35DAD5E611E28D9D44A33DF752471144B3E(L_0, L_1, L_2, L_3, L_4, L_5, NULL);
		return L_6;
	}
}
// Oculus.Interaction.HandGrab.SnapSurfaces.ISnapSurface Oculus.Interaction.HandGrab.SnapSurfaces.SphereSurface::CreateMirroredSurface(UnityEngine.GameObject)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* SphereSurface_CreateMirroredSurface_mA5F968033C6DD1A52F3C6E79C8A2EBB9A22FFE57 (SphereSurface_t7C6F8A614D76E8B3AE10EE722BBDB6774B96A30B* __this, GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* ___0_gameObject, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&GameObject_AddComponent_TisSphereSurface_t7C6F8A614D76E8B3AE10EE722BBDB6774B96A30B_m61F5ABE86B0C4374D3A35820F96D50228F1767C9_RuntimeMethod_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// SphereSurface surface = gameObject.AddComponent<SphereSurface>();
		GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* L_0 = ___0_gameObject;
		NullCheck(L_0);
		SphereSurface_t7C6F8A614D76E8B3AE10EE722BBDB6774B96A30B* L_1;
		L_1 = GameObject_AddComponent_TisSphereSurface_t7C6F8A614D76E8B3AE10EE722BBDB6774B96A30B_m61F5ABE86B0C4374D3A35820F96D50228F1767C9(L_0, GameObject_AddComponent_TisSphereSurface_t7C6F8A614D76E8B3AE10EE722BBDB6774B96A30B_m61F5ABE86B0C4374D3A35820F96D50228F1767C9_RuntimeMethod_var);
		// surface.Data = _data.Mirror();
		SphereSurface_t7C6F8A614D76E8B3AE10EE722BBDB6774B96A30B* L_2 = L_1;
		SphereSurfaceData_tE8331CA08F2DB69533F08417636C96FFA01E7CC1* L_3 = __this->____data_4;
		NullCheck(L_3);
		SphereSurfaceData_tE8331CA08F2DB69533F08417636C96FFA01E7CC1* L_4;
		L_4 = SphereSurfaceData_Mirror_m783B2836B5EA587D72120C22298A11C93433CD56(L_3, NULL);
		NullCheck(L_2);
		SphereSurface_set_Data_m463CDA705345ED889DAD5A98A3A704F56A65BD26_inline(L_2, L_4, NULL);
		// return surface;
		return L_2;
	}
}
// Oculus.Interaction.HandGrab.SnapSurfaces.ISnapSurface Oculus.Interaction.HandGrab.SnapSurfaces.SphereSurface::CreateDuplicatedSurface(UnityEngine.GameObject)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* SphereSurface_CreateDuplicatedSurface_m1789792D4AFDC0F114CCE784997C54A1DFB6B9AF (SphereSurface_t7C6F8A614D76E8B3AE10EE722BBDB6774B96A30B* __this, GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* ___0_gameObject, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&GameObject_AddComponent_TisSphereSurface_t7C6F8A614D76E8B3AE10EE722BBDB6774B96A30B_m61F5ABE86B0C4374D3A35820F96D50228F1767C9_RuntimeMethod_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// SphereSurface surface = gameObject.AddComponent<SphereSurface>();
		GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* L_0 = ___0_gameObject;
		NullCheck(L_0);
		SphereSurface_t7C6F8A614D76E8B3AE10EE722BBDB6774B96A30B* L_1;
		L_1 = GameObject_AddComponent_TisSphereSurface_t7C6F8A614D76E8B3AE10EE722BBDB6774B96A30B_m61F5ABE86B0C4374D3A35820F96D50228F1767C9(L_0, GameObject_AddComponent_TisSphereSurface_t7C6F8A614D76E8B3AE10EE722BBDB6774B96A30B_m61F5ABE86B0C4374D3A35820F96D50228F1767C9_RuntimeMethod_var);
		// surface.Data = _data;
		SphereSurface_t7C6F8A614D76E8B3AE10EE722BBDB6774B96A30B* L_2 = L_1;
		SphereSurfaceData_tE8331CA08F2DB69533F08417636C96FFA01E7CC1* L_3 = __this->____data_4;
		NullCheck(L_2);
		SphereSurface_set_Data_m463CDA705345ED889DAD5A98A3A704F56A65BD26_inline(L_2, L_3, NULL);
		// return surface;
		return L_2;
	}
}
// UnityEngine.Vector3 Oculus.Interaction.HandGrab.SnapSurfaces.SphereSurface::NearestPointInSurface(UnityEngine.Vector3)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 SphereSurface_NearestPointInSurface_m8571F38D5010A262130B3D0918187AE76290470D (SphereSurface_t7C6F8A614D76E8B3AE10EE722BBDB6774B96A30B* __this, Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___0_targetPosition, const RuntimeMethod* method) 
{
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 V_0;
	memset((&V_0), 0, sizeof(V_0));
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 V_1;
	memset((&V_1), 0, sizeof(V_1));
	{
		// Vector3 direction = (targetPosition - Centre).normalized;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_0 = ___0_targetPosition;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_1;
		L_1 = SphereSurface_get_Centre_mDB3654A871802C00C70C60409AA15AD8F7EAF00B(__this, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_2;
		L_2 = Vector3_op_Subtraction_mE42023FF80067CB44A1D4A27EB7CF2B24CABB828_inline(L_0, L_1, NULL);
		V_1 = L_2;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_3;
		L_3 = Vector3_get_normalized_m736BBF65D5CDA7A18414370D15B4DFCC1E466F07_inline((&V_1), NULL);
		V_0 = L_3;
		// return Centre + direction * Radius;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_4;
		L_4 = SphereSurface_get_Centre_mDB3654A871802C00C70C60409AA15AD8F7EAF00B(__this, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_5 = V_0;
		float L_6;
		L_6 = SphereSurface_get_Radius_m3A32B797BC6684468CC1BA7C6E35DD04100288DF(__this, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_7;
		L_7 = Vector3_op_Multiply_m87BA7C578F96C8E49BB07088DAAC4649F83B0353_inline(L_5, L_6, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_8;
		L_8 = Vector3_op_Addition_m78C0EC70CB66E8DCAC225743D82B268DAEE92067_inline(L_4, L_7, NULL);
		return L_8;
	}
}
// UnityEngine.Pose Oculus.Interaction.HandGrab.SnapSurfaces.SphereSurface::MinimalRotationPoseAtSurface(UnityEngine.Pose&,UnityEngine.Pose&)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971 SphereSurface_MinimalRotationPoseAtSurface_mAA27B9D91F134707E946B7718F6B29EF1E5CEF1C (SphereSurface_t7C6F8A614D76E8B3AE10EE722BBDB6774B96A30B* __this, Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* ___0_userPose, Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* ___1_snapPose, const RuntimeMethod* method) 
{
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 V_0;
	memset((&V_0), 0, sizeof(V_0));
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 V_1;
	memset((&V_1), 0, sizeof(V_1));
	Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 V_2;
	memset((&V_2), 0, sizeof(V_2));
	{
		// Quaternion rotCorrection = userPose.rotation * Quaternion.Inverse(snapPose.rotation);
		Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* L_0 = ___0_userPose;
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_1 = L_0->___rotation_1;
		Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* L_2 = ___1_snapPose;
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_3 = L_2->___rotation_1;
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_4;
		L_4 = Quaternion_Inverse_mD9C060AC626A7B406F4984AC98F8358DC89EF512(L_3, NULL);
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_5;
		L_5 = Quaternion_op_Multiply_mCB375FCCC12A2EC8F9EB824A1BFB4453B58C2012_inline(L_1, L_4, NULL);
		// Vector3 correctedDir = rotCorrection * Direction;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_6;
		L_6 = SphereSurface_get_Direction_mEF366B8EE0B94083D54C4E3716AAEAF02E9AD9F5(__this, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_7;
		L_7 = Quaternion_op_Multiply_mE1EBA73F9173432B50F8F17CE8190C5A7986FB8C(L_5, L_6, NULL);
		V_0 = L_7;
		// Vector3 surfacePoint = NearestPointInSurface(Centre + correctedDir * Radius);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_8;
		L_8 = SphereSurface_get_Centre_mDB3654A871802C00C70C60409AA15AD8F7EAF00B(__this, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_9 = V_0;
		float L_10;
		L_10 = SphereSurface_get_Radius_m3A32B797BC6684468CC1BA7C6E35DD04100288DF(__this, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_11;
		L_11 = Vector3_op_Multiply_m87BA7C578F96C8E49BB07088DAAC4649F83B0353_inline(L_9, L_10, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_12;
		L_12 = Vector3_op_Addition_m78C0EC70CB66E8DCAC225743D82B268DAEE92067_inline(L_8, L_11, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_13;
		L_13 = SphereSurface_NearestPointInSurface_m8571F38D5010A262130B3D0918187AE76290470D(__this, L_12, NULL);
		V_1 = L_13;
		// Quaternion surfaceRotation = RotationAtPoint(surfacePoint, snapPose.rotation, userPose.rotation);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_14 = V_1;
		Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* L_15 = ___1_snapPose;
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_16 = L_15->___rotation_1;
		Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* L_17 = ___0_userPose;
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_18 = L_17->___rotation_1;
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_19;
		L_19 = SphereSurface_RotationAtPoint_m9907775BA1F5ED5424D50A607A44ADB19FD971E2(__this, L_14, L_16, L_18, NULL);
		V_2 = L_19;
		// return new Pose(surfacePoint, surfaceRotation);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_20 = V_1;
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_21 = V_2;
		Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971 L_22;
		memset((&L_22), 0, sizeof(L_22));
		Pose__ctor_m15CA45808A2BBF1956E836D22C387FAB80BED051((&L_22), L_20, L_21, /*hidden argument*/NULL);
		return L_22;
	}
}
// UnityEngine.Pose Oculus.Interaction.HandGrab.SnapSurfaces.SphereSurface::MinimalTranslationPoseAtSurface(UnityEngine.Pose&,UnityEngine.Pose&)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971 SphereSurface_MinimalTranslationPoseAtSurface_m888D23B10E2443C9E359760B9F4927D8D0B2A3D6 (SphereSurface_t7C6F8A614D76E8B3AE10EE722BBDB6774B96A30B* __this, Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* ___0_userPose, Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* ___1_snapPose, const RuntimeMethod* method) 
{
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 V_0;
	memset((&V_0), 0, sizeof(V_0));
	Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 V_1;
	memset((&V_1), 0, sizeof(V_1));
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 V_2;
	memset((&V_2), 0, sizeof(V_2));
	Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 V_3;
	memset((&V_3), 0, sizeof(V_3));
	{
		// Vector3 desiredPos = userPose.position;
		Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* L_0 = ___0_userPose;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_1 = L_0->___position_0;
		V_0 = L_1;
		// Quaternion baseRot = snapPose.rotation;
		Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* L_2 = ___1_snapPose;
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_3 = L_2->___rotation_1;
		V_1 = L_3;
		// Vector3 surfacePoint = NearestPointInSurface(desiredPos);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_4 = V_0;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_5;
		L_5 = SphereSurface_NearestPointInSurface_m8571F38D5010A262130B3D0918187AE76290470D(__this, L_4, NULL);
		V_2 = L_5;
		// Quaternion surfaceRotation = RotationAtPoint(surfacePoint, baseRot, userPose.rotation);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_6 = V_2;
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_7 = V_1;
		Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* L_8 = ___0_userPose;
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_9 = L_8->___rotation_1;
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_10;
		L_10 = SphereSurface_RotationAtPoint_m9907775BA1F5ED5424D50A607A44ADB19FD971E2(__this, L_6, L_7, L_9, NULL);
		V_3 = L_10;
		// return new Pose(surfacePoint, surfaceRotation);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_11 = V_2;
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_12 = V_3;
		Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971 L_13;
		memset((&L_13), 0, sizeof(L_13));
		Pose__ctor_m15CA45808A2BBF1956E836D22C387FAB80BED051((&L_13), L_11, L_12, /*hidden argument*/NULL);
		return L_13;
	}
}
// UnityEngine.Quaternion Oculus.Interaction.HandGrab.SnapSurfaces.SphereSurface::RotationAtPoint(UnityEngine.Vector3,UnityEngine.Quaternion,UnityEngine.Quaternion)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 SphereSurface_RotationAtPoint_m9907775BA1F5ED5424D50A607A44ADB19FD971E2 (SphereSurface_t7C6F8A614D76E8B3AE10EE722BBDB6774B96A30B* __this, Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___0_surfacePoint, Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 ___1_baseRot, Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 ___2_desiredRotation, const RuntimeMethod* method) 
{
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 V_0;
	memset((&V_0), 0, sizeof(V_0));
	Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 V_1;
	memset((&V_1), 0, sizeof(V_1));
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 V_2;
	memset((&V_2), 0, sizeof(V_2));
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 V_3;
	memset((&V_3), 0, sizeof(V_3));
	{
		// Vector3 desiredDirection = (surfacePoint - Centre).normalized;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_0 = ___0_surfacePoint;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_1;
		L_1 = SphereSurface_get_Centre_mDB3654A871802C00C70C60409AA15AD8F7EAF00B(__this, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_2;
		L_2 = Vector3_op_Subtraction_mE42023FF80067CB44A1D4A27EB7CF2B24CABB828_inline(L_0, L_1, NULL);
		V_3 = L_2;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_3;
		L_3 = Vector3_get_normalized_m736BBF65D5CDA7A18414370D15B4DFCC1E466F07_inline((&V_3), NULL);
		V_0 = L_3;
		// Quaternion targetRotation = Quaternion.FromToRotation(Direction, desiredDirection) * baseRot;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_4;
		L_4 = SphereSurface_get_Direction_mEF366B8EE0B94083D54C4E3716AAEAF02E9AD9F5(__this, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_5 = V_0;
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_6;
		L_6 = Quaternion_FromToRotation_mCB3100F93637E72455388B901C36EF8A25DFDB9A(L_4, L_5, NULL);
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_7 = ___1_baseRot;
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_8;
		L_8 = Quaternion_op_Multiply_mCB375FCCC12A2EC8F9EB824A1BFB4453B58C2012_inline(L_6, L_7, NULL);
		V_1 = L_8;
		// Vector3 targetProjected = Vector3.ProjectOnPlane(targetRotation * Vector3.forward, desiredDirection).normalized;
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_9 = V_1;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_10;
		L_10 = Vector3_get_forward_mAA55A7034304DF8B2152EAD49AE779FC4CA2EB4A_inline(NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_11;
		L_11 = Quaternion_op_Multiply_mE1EBA73F9173432B50F8F17CE8190C5A7986FB8C(L_9, L_10, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_12 = V_0;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_13;
		L_13 = Vector3_ProjectOnPlane_m68FB895F6E9FCC45676BB8B95857D091C0D78794_inline(L_11, L_12, NULL);
		V_3 = L_13;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_14;
		L_14 = Vector3_get_normalized_m736BBF65D5CDA7A18414370D15B4DFCC1E466F07_inline((&V_3), NULL);
		// Vector3 desiredProjected = Vector3.ProjectOnPlane(desiredRotation * Vector3.forward, desiredDirection).normalized;
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_15 = ___2_desiredRotation;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_16;
		L_16 = Vector3_get_forward_mAA55A7034304DF8B2152EAD49AE779FC4CA2EB4A_inline(NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_17;
		L_17 = Quaternion_op_Multiply_mE1EBA73F9173432B50F8F17CE8190C5A7986FB8C(L_15, L_16, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_18 = V_0;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_19;
		L_19 = Vector3_ProjectOnPlane_m68FB895F6E9FCC45676BB8B95857D091C0D78794_inline(L_17, L_18, NULL);
		V_3 = L_19;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_20;
		L_20 = Vector3_get_normalized_m736BBF65D5CDA7A18414370D15B4DFCC1E466F07_inline((&V_3), NULL);
		V_2 = L_20;
		// Quaternion rotCorrection = Quaternion.FromToRotation(targetProjected, desiredProjected);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_21 = V_2;
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_22;
		L_22 = Quaternion_FromToRotation_mCB3100F93637E72455388B901C36EF8A25DFDB9A(L_14, L_21, NULL);
		// return rotCorrection * targetRotation;
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_23 = V_1;
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_24;
		L_24 = Quaternion_op_Multiply_mCB375FCCC12A2EC8F9EB824A1BFB4453B58C2012_inline(L_22, L_23, NULL);
		return L_24;
	}
}
// System.Void Oculus.Interaction.HandGrab.SnapSurfaces.SphereSurface::InjectAllSphereSurface(Oculus.Interaction.HandGrab.SnapSurfaces.SphereSurfaceData,UnityEngine.Transform,UnityEngine.Transform)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void SphereSurface_InjectAllSphereSurface_m2A6E761AB49B4352B8B4C10DAE8F1FF61328B729 (SphereSurface_t7C6F8A614D76E8B3AE10EE722BBDB6774B96A30B* __this, SphereSurfaceData_tE8331CA08F2DB69533F08417636C96FFA01E7CC1* ___0_data, Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* ___1_relativeTo, Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* ___2_gripPoint, const RuntimeMethod* method) 
{
	{
		// InjectData(data);
		SphereSurfaceData_tE8331CA08F2DB69533F08417636C96FFA01E7CC1* L_0 = ___0_data;
		SphereSurface_InjectData_m5B528A93C577760B667FE1A98CA1A71B4DD6861F_inline(__this, L_0, NULL);
		// InjectRelativeTo(relativeTo);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_1 = ___1_relativeTo;
		SphereSurface_InjectRelativeTo_m6B1E6609020D4F861398621C3B3AAA89E822B781_inline(__this, L_1, NULL);
		// InjectGripPoint(gripPoint);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_2 = ___2_gripPoint;
		SphereSurface_InjectGripPoint_m2329EBE5F4F5CBAD77341BCF37E9F3BF8B7F9DC4_inline(__this, L_2, NULL);
		// }
		return;
	}
}
// System.Void Oculus.Interaction.HandGrab.SnapSurfaces.SphereSurface::InjectData(Oculus.Interaction.HandGrab.SnapSurfaces.SphereSurfaceData)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void SphereSurface_InjectData_m5B528A93C577760B667FE1A98CA1A71B4DD6861F (SphereSurface_t7C6F8A614D76E8B3AE10EE722BBDB6774B96A30B* __this, SphereSurfaceData_tE8331CA08F2DB69533F08417636C96FFA01E7CC1* ___0_data, const RuntimeMethod* method) 
{
	{
		// _data = data;
		SphereSurfaceData_tE8331CA08F2DB69533F08417636C96FFA01E7CC1* L_0 = ___0_data;
		__this->____data_4 = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->____data_4), (void*)L_0);
		// }
		return;
	}
}
// System.Void Oculus.Interaction.HandGrab.SnapSurfaces.SphereSurface::InjectRelativeTo(UnityEngine.Transform)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void SphereSurface_InjectRelativeTo_m6B1E6609020D4F861398621C3B3AAA89E822B781 (SphereSurface_t7C6F8A614D76E8B3AE10EE722BBDB6774B96A30B* __this, Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* ___0_relativeTo, const RuntimeMethod* method) 
{
	{
		// _relativeTo = relativeTo;
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_0 = ___0_relativeTo;
		__this->____relativeTo_5 = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->____relativeTo_5), (void*)L_0);
		// }
		return;
	}
}
// System.Void Oculus.Interaction.HandGrab.SnapSurfaces.SphereSurface::InjectGripPoint(UnityEngine.Transform)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void SphereSurface_InjectGripPoint_m2329EBE5F4F5CBAD77341BCF37E9F3BF8B7F9DC4 (SphereSurface_t7C6F8A614D76E8B3AE10EE722BBDB6774B96A30B* __this, Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* ___0_gripPoint, const RuntimeMethod* method) 
{
	{
		// _gripPoint = gripPoint;
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_0 = ___0_gripPoint;
		__this->____gripPoint_6 = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->____gripPoint_6), (void*)L_0);
		// }
		return;
	}
}
// System.Void Oculus.Interaction.HandGrab.SnapSurfaces.SphereSurface::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void SphereSurface__ctor_m86D6FC5DC831B1F1FAF90DA8128A945CD6184142 (SphereSurface_t7C6F8A614D76E8B3AE10EE722BBDB6774B96A30B* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&SphereSurfaceData_tE8331CA08F2DB69533F08417636C96FFA01E7CC1_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// protected SphereSurfaceData _data = new SphereSurfaceData();
		SphereSurfaceData_tE8331CA08F2DB69533F08417636C96FFA01E7CC1* L_0 = (SphereSurfaceData_tE8331CA08F2DB69533F08417636C96FFA01E7CC1*)il2cpp_codegen_object_new(SphereSurfaceData_tE8331CA08F2DB69533F08417636C96FFA01E7CC1_il2cpp_TypeInfo_var);
		NullCheck(L_0);
		SphereSurfaceData__ctor_mC4A3CEB2FC052A2580F50116E0B4C351ABBA389C(L_0, NULL);
		__this->____data_4 = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->____data_4), (void*)L_0);
		MonoBehaviour__ctor_m592DB0105CA0BC97AA1C5F4AD27B12D68A3B7C1E(__this, NULL);
		return;
	}
}
// System.Single Oculus.Interaction.HandGrab.SnapSurfaces.SphereSurface::Oculus.Interaction.HandGrab.SnapSurfaces.ISnapSurface.CalculateBestPoseAtSurface(UnityEngine.Pose& modreq(System.Runtime.InteropServices.InAttribute),UnityEngine.Pose& modreq(System.Runtime.InteropServices.InAttribute),UnityEngine.Pose&,Oculus.Interaction.HandGrab.PoseMeasureParameters& modreq(System.Runtime.InteropServices.InAttribute))
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float SphereSurface_Oculus_Interaction_HandGrab_SnapSurfaces_ISnapSurface_CalculateBestPoseAtSurface_m0E375D43F1EE8AEDCCBB4297D5C356401140367F (SphereSurface_t7C6F8A614D76E8B3AE10EE722BBDB6774B96A30B* __this, Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* ___0_targetPose, Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* ___1_reference, Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* ___2_bestPose, PoseMeasureParameters_tB95EB8BC83F81E86544854D1B13C04E88F2C0203* ___3_scoringModifier, const RuntimeMethod* method) 
{
	{
		Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* L_0 = ___0_targetPose;
		Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* L_1 = ___1_reference;
		Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* L_2 = ___2_bestPose;
		PoseMeasureParameters_tB95EB8BC83F81E86544854D1B13C04E88F2C0203* L_3 = ___3_scoringModifier;
		float L_4;
		L_4 = SphereSurface_CalculateBestPoseAtSurface_m5C024F13CD2E88EF909798301ED2322AFE6629B2(__this, L_0, L_1, L_2, L_3, NULL);
		return L_4;
	}
}
// System.Boolean Oculus.Interaction.HandGrab.SnapSurfaces.SphereSurface::Oculus.Interaction.HandGrab.SnapSurfaces.ISnapSurface.CalculateBestPoseAtSurface(UnityEngine.Ray,UnityEngine.Pose& modreq(System.Runtime.InteropServices.InAttribute),UnityEngine.Pose&)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool SphereSurface_Oculus_Interaction_HandGrab_SnapSurfaces_ISnapSurface_CalculateBestPoseAtSurface_m3C035DFD6D2A61F1C0C6638D0E7CD410BB6E5000 (SphereSurface_t7C6F8A614D76E8B3AE10EE722BBDB6774B96A30B* __this, Ray_t2B1742D7958DC05BDC3EFC7461D3593E1430DC00 ___0_targetRay, Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* ___1_reference, Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* ___2_bestPose, const RuntimeMethod* method) 
{
	{
		Ray_t2B1742D7958DC05BDC3EFC7461D3593E1430DC00 L_0 = ___0_targetRay;
		Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* L_1 = ___1_reference;
		Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* L_2 = ___2_bestPose;
		bool L_3;
		L_3 = SphereSurface_CalculateBestPoseAtSurface_m75444B60CED07377ED2BB91B7BB15B7E220C613E(__this, L_0, L_1, L_2, NULL);
		return L_3;
	}
}
// UnityEngine.Pose Oculus.Interaction.HandGrab.SnapSurfaces.SphereSurface::Oculus.Interaction.HandGrab.SnapSurfaces.ISnapSurface.MirrorPose(UnityEngine.Pose& modreq(System.Runtime.InteropServices.InAttribute))
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971 SphereSurface_Oculus_Interaction_HandGrab_SnapSurfaces_ISnapSurface_MirrorPose_mCBDDA6AE040E9D4A28BD576491991A12491CCFCF (SphereSurface_t7C6F8A614D76E8B3AE10EE722BBDB6774B96A30B* __this, Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* ___0_gripPose, const RuntimeMethod* method) 
{
	{
		Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* L_0 = ___0_gripPose;
		Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971 L_1;
		L_1 = SphereSurface_MirrorPose_m25AE869A0553646F0CCBF97B12E16C491E1CDC62(__this, L_0, NULL);
		return L_1;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void Vector3__ctor_m376936E6B999EF1ECBE57D990A386303E2283DE0_inline (Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2* __this, float ___0_x, float ___1_y, float ___2_z, const RuntimeMethod* method) 
{
	{
		float L_0 = ___0_x;
		__this->___x_2 = L_0;
		float L_1 = ___1_y;
		__this->___y_3 = L_1;
		float L_2 = ___2_z;
		__this->___z_4 = L_2;
		return;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* CylinderSurface_get_GripPoint_m5EB373A3D69443440ACCAD9212CD470ECC0526D2_inline (CylinderSurface_t071F3C35CED4F1296F988B93595DF4736770D4F7* __this, const RuntimeMethod* method) 
{
	{
		// get => _gripPoint;
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_0 = __this->____gripPoint_6;
		return L_0;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 Vector3_get_forward_mAA55A7034304DF8B2152EAD49AE779FC4CA2EB4A_inline (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_0 = ((Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2_StaticFields*)il2cpp_codegen_static_fields_for(Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2_il2cpp_TypeInfo_var))->___forwardVector_11;
		V_0 = L_0;
		goto IL_0009;
	}

IL_0009:
	{
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_1 = V_0;
		return L_1;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 Vector3_op_Subtraction_mE42023FF80067CB44A1D4A27EB7CF2B24CABB828_inline (Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___0_a, Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___1_b, const RuntimeMethod* method) 
{
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_0 = ___0_a;
		float L_1 = L_0.___x_2;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_2 = ___1_b;
		float L_3 = L_2.___x_2;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_4 = ___0_a;
		float L_5 = L_4.___y_3;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_6 = ___1_b;
		float L_7 = L_6.___y_3;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_8 = ___0_a;
		float L_9 = L_8.___z_4;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_10 = ___1_b;
		float L_11 = L_10.___z_4;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_12;
		memset((&L_12), 0, sizeof(L_12));
		Vector3__ctor_m376936E6B999EF1ECBE57D990A386303E2283DE0_inline((&L_12), ((float)il2cpp_codegen_subtract(L_1, L_3)), ((float)il2cpp_codegen_subtract(L_5, L_7)), ((float)il2cpp_codegen_subtract(L_9, L_11)), /*hidden argument*/NULL);
		V_0 = L_12;
		goto IL_0030;
	}

IL_0030:
	{
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_13 = V_0;
		return L_13;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 Vector3_ProjectOnPlane_m68FB895F6E9FCC45676BB8B95857D091C0D78794_inline (Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___0_vector, Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___1_planeNormal, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Mathf_tE284D016E3B297B72311AAD9EB8F0E643F6A4682_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	float V_0 = 0.0f;
	bool V_1 = false;
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 V_2;
	memset((&V_2), 0, sizeof(V_2));
	float V_3 = 0.0f;
	{
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_0 = ___1_planeNormal;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_1 = ___1_planeNormal;
		float L_2;
		L_2 = Vector3_Dot_mBB86BB940AA0A32FA7D3C02AC42E5BC7095A5D52_inline(L_0, L_1, NULL);
		V_0 = L_2;
		float L_3 = V_0;
		float L_4 = ((Mathf_tE284D016E3B297B72311AAD9EB8F0E643F6A4682_StaticFields*)il2cpp_codegen_static_fields_for(Mathf_tE284D016E3B297B72311AAD9EB8F0E643F6A4682_il2cpp_TypeInfo_var))->___Epsilon_0;
		V_1 = (bool)((((float)L_3) < ((float)L_4))? 1 : 0);
		bool L_5 = V_1;
		if (!L_5)
		{
			goto IL_0019;
		}
	}
	{
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_6 = ___0_vector;
		V_2 = L_6;
		goto IL_005d;
	}

IL_0019:
	{
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_7 = ___0_vector;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_8 = ___1_planeNormal;
		float L_9;
		L_9 = Vector3_Dot_mBB86BB940AA0A32FA7D3C02AC42E5BC7095A5D52_inline(L_7, L_8, NULL);
		V_3 = L_9;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_10 = ___0_vector;
		float L_11 = L_10.___x_2;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_12 = ___1_planeNormal;
		float L_13 = L_12.___x_2;
		float L_14 = V_3;
		float L_15 = V_0;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_16 = ___0_vector;
		float L_17 = L_16.___y_3;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_18 = ___1_planeNormal;
		float L_19 = L_18.___y_3;
		float L_20 = V_3;
		float L_21 = V_0;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_22 = ___0_vector;
		float L_23 = L_22.___z_4;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_24 = ___1_planeNormal;
		float L_25 = L_24.___z_4;
		float L_26 = V_3;
		float L_27 = V_0;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_28;
		memset((&L_28), 0, sizeof(L_28));
		Vector3__ctor_m376936E6B999EF1ECBE57D990A386303E2283DE0_inline((&L_28), ((float)il2cpp_codegen_subtract(L_11, ((float)(((float)il2cpp_codegen_multiply(L_13, L_14))/L_15)))), ((float)il2cpp_codegen_subtract(L_17, ((float)(((float)il2cpp_codegen_multiply(L_19, L_20))/L_21)))), ((float)il2cpp_codegen_subtract(L_23, ((float)(((float)il2cpp_codegen_multiply(L_25, L_26))/L_27)))), /*hidden argument*/NULL);
		V_2 = L_28;
		goto IL_005d;
	}

IL_005d:
	{
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_29 = V_2;
		return L_29;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 Vector3_get_normalized_m736BBF65D5CDA7A18414370D15B4DFCC1E466F07_inline (Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2* __this, const RuntimeMethod* method) 
{
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_0 = (*(Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2*)__this);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_1;
		L_1 = Vector3_Normalize_mEF8349CC39674236CFC694189AFD36E31F89AC8F_inline(L_0, NULL);
		V_0 = L_1;
		goto IL_000f;
	}

IL_000f:
	{
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_2 = V_0;
		return L_2;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* CylinderSurface_get_RelativeTo_m3B6807164E9EE3C2F8A8688910AE9DAE16A3A612_inline (CylinderSurface_t071F3C35CED4F1296F988B93595DF4736770D4F7* __this, const RuntimeMethod* method) 
{
	{
		// get => _relativeTo;
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_0 = __this->____relativeTo_5;
		return L_0;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR float Mathf_Repeat_m6F1560A163481BB311D685294E1B463C3E4EB3BA_inline (float ___0_t, float ___1_length, const RuntimeMethod* method) 
{
	float V_0 = 0.0f;
	{
		float L_0 = ___0_t;
		float L_1 = ___0_t;
		float L_2 = ___1_length;
		float L_3;
		L_3 = floorf(((float)(L_1/L_2)));
		float L_4 = ___1_length;
		float L_5 = ___1_length;
		float L_6;
		L_6 = Mathf_Clamp_mEB9AEA827D27D20FCC787F7375156AF46BB12BBF_inline(((float)il2cpp_codegen_subtract(L_0, ((float)il2cpp_codegen_multiply(L_3, L_4)))), (0.0f), L_5, NULL);
		V_0 = L_6;
		goto IL_001b;
	}

IL_001b:
	{
		float L_7 = V_0;
		return L_7;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 Vector3_Project_m85DF3CB297EC5E1A17BD6266FF65E86AB7372C9B_inline (Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___0_vector, Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___1_onNormal, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Mathf_tE284D016E3B297B72311AAD9EB8F0E643F6A4682_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	float V_0 = 0.0f;
	bool V_1 = false;
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 V_2;
	memset((&V_2), 0, sizeof(V_2));
	float V_3 = 0.0f;
	{
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_0 = ___1_onNormal;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_1 = ___1_onNormal;
		float L_2;
		L_2 = Vector3_Dot_mBB86BB940AA0A32FA7D3C02AC42E5BC7095A5D52_inline(L_0, L_1, NULL);
		V_0 = L_2;
		float L_3 = V_0;
		float L_4 = ((Mathf_tE284D016E3B297B72311AAD9EB8F0E643F6A4682_StaticFields*)il2cpp_codegen_static_fields_for(Mathf_tE284D016E3B297B72311AAD9EB8F0E643F6A4682_il2cpp_TypeInfo_var))->___Epsilon_0;
		V_1 = (bool)((((float)L_3) < ((float)L_4))? 1 : 0);
		bool L_5 = V_1;
		if (!L_5)
		{
			goto IL_001d;
		}
	}
	{
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_6;
		L_6 = Vector3_get_zero_m0C1249C3F25B1C70EAD3CC8B31259975A457AE39_inline(NULL);
		V_2 = L_6;
		goto IL_004c;
	}

IL_001d:
	{
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_7 = ___0_vector;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_8 = ___1_onNormal;
		float L_9;
		L_9 = Vector3_Dot_mBB86BB940AA0A32FA7D3C02AC42E5BC7095A5D52_inline(L_7, L_8, NULL);
		V_3 = L_9;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_10 = ___1_onNormal;
		float L_11 = L_10.___x_2;
		float L_12 = V_3;
		float L_13 = V_0;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_14 = ___1_onNormal;
		float L_15 = L_14.___y_3;
		float L_16 = V_3;
		float L_17 = V_0;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_18 = ___1_onNormal;
		float L_19 = L_18.___z_4;
		float L_20 = V_3;
		float L_21 = V_0;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_22;
		memset((&L_22), 0, sizeof(L_22));
		Vector3__ctor_m376936E6B999EF1ECBE57D990A386303E2283DE0_inline((&L_22), ((float)(((float)il2cpp_codegen_multiply(L_11, L_12))/L_13)), ((float)(((float)il2cpp_codegen_multiply(L_15, L_16))/L_17)), ((float)(((float)il2cpp_codegen_multiply(L_19, L_20))/L_21)), /*hidden argument*/NULL);
		V_2 = L_22;
		goto IL_004c;
	}

IL_004c:
	{
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_23 = V_2;
		return L_23;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 Vector3_op_Addition_m78C0EC70CB66E8DCAC225743D82B268DAEE92067_inline (Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___0_a, Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___1_b, const RuntimeMethod* method) 
{
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_0 = ___0_a;
		float L_1 = L_0.___x_2;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_2 = ___1_b;
		float L_3 = L_2.___x_2;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_4 = ___0_a;
		float L_5 = L_4.___y_3;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_6 = ___1_b;
		float L_7 = L_6.___y_3;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_8 = ___0_a;
		float L_9 = L_8.___z_4;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_10 = ___1_b;
		float L_11 = L_10.___z_4;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_12;
		memset((&L_12), 0, sizeof(L_12));
		Vector3__ctor_m376936E6B999EF1ECBE57D990A386303E2283DE0_inline((&L_12), ((float)il2cpp_codegen_add(L_1, L_3)), ((float)il2cpp_codegen_add(L_5, L_7)), ((float)il2cpp_codegen_add(L_9, L_11)), /*hidden argument*/NULL);
		V_0 = L_12;
		goto IL_0030;
	}

IL_0030:
	{
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_13 = V_0;
		return L_13;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR float Vector3_Distance_m2314DB9B8BD01157E013DF87BEA557375C7F9FF9_inline (Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___0_a, Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___1_b, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Math_tEB65DE7CA8B083C412C969C92981C030865486CE_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	float V_0 = 0.0f;
	float V_1 = 0.0f;
	float V_2 = 0.0f;
	float V_3 = 0.0f;
	{
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_0 = ___0_a;
		float L_1 = L_0.___x_2;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_2 = ___1_b;
		float L_3 = L_2.___x_2;
		V_0 = ((float)il2cpp_codegen_subtract(L_1, L_3));
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_4 = ___0_a;
		float L_5 = L_4.___y_3;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_6 = ___1_b;
		float L_7 = L_6.___y_3;
		V_1 = ((float)il2cpp_codegen_subtract(L_5, L_7));
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_8 = ___0_a;
		float L_9 = L_8.___z_4;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_10 = ___1_b;
		float L_11 = L_10.___z_4;
		V_2 = ((float)il2cpp_codegen_subtract(L_9, L_11));
		float L_12 = V_0;
		float L_13 = V_0;
		float L_14 = V_1;
		float L_15 = V_1;
		float L_16 = V_2;
		float L_17 = V_2;
		il2cpp_codegen_runtime_class_init_inline(Math_tEB65DE7CA8B083C412C969C92981C030865486CE_il2cpp_TypeInfo_var);
		double L_18;
		L_18 = sqrt(((double)((float)il2cpp_codegen_add(((float)il2cpp_codegen_add(((float)il2cpp_codegen_multiply(L_12, L_13)), ((float)il2cpp_codegen_multiply(L_14, L_15)))), ((float)il2cpp_codegen_multiply(L_16, L_17))))));
		V_3 = ((float)L_18);
		goto IL_0040;
	}

IL_0040:
	{
		float L_19 = V_3;
		return L_19;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR float Vector3_get_sqrMagnitude_m43C27DEC47C4811FB30AB474FF2131A963B66FC8_inline (Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2* __this, const RuntimeMethod* method) 
{
	float V_0 = 0.0f;
	{
		float L_0 = __this->___x_2;
		float L_1 = __this->___x_2;
		float L_2 = __this->___y_3;
		float L_3 = __this->___y_3;
		float L_4 = __this->___z_4;
		float L_5 = __this->___z_4;
		V_0 = ((float)il2cpp_codegen_add(((float)il2cpp_codegen_add(((float)il2cpp_codegen_multiply(L_0, L_1)), ((float)il2cpp_codegen_multiply(L_2, L_3)))), ((float)il2cpp_codegen_multiply(L_4, L_5))));
		goto IL_002d;
	}

IL_002d:
	{
		float L_6 = V_0;
		return L_6;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 Vector3_get_up_m128AF3FDC820BF59D5DE86D973E7DE3F20C3AEBA_inline (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_0 = ((Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2_StaticFields*)il2cpp_codegen_static_fields_for(Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2_il2cpp_TypeInfo_var))->___upVector_7;
		V_0 = L_0;
		goto IL_0009;
	}

IL_0009:
	{
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_1 = V_0;
		return L_1;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR float Vector3_get_magnitude_mF0D6017E90B345F1F52D1CC564C640F1A847AF2D_inline (Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Math_tEB65DE7CA8B083C412C969C92981C030865486CE_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	float V_0 = 0.0f;
	{
		float L_0 = __this->___x_2;
		float L_1 = __this->___x_2;
		float L_2 = __this->___y_3;
		float L_3 = __this->___y_3;
		float L_4 = __this->___z_4;
		float L_5 = __this->___z_4;
		il2cpp_codegen_runtime_class_init_inline(Math_tEB65DE7CA8B083C412C969C92981C030865486CE_il2cpp_TypeInfo_var);
		double L_6;
		L_6 = sqrt(((double)((float)il2cpp_codegen_add(((float)il2cpp_codegen_add(((float)il2cpp_codegen_multiply(L_0, L_1)), ((float)il2cpp_codegen_multiply(L_2, L_3)))), ((float)il2cpp_codegen_multiply(L_4, L_5))))));
		V_0 = ((float)L_6);
		goto IL_0034;
	}

IL_0034:
	{
		float L_7 = V_0;
		return L_7;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR bool Vector3_op_Equality_mCDCBB8D2EDC3D3BF20F31A25ACB34705D352B479_inline (Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___0_lhs, Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___1_rhs, const RuntimeMethod* method) 
{
	float V_0 = 0.0f;
	float V_1 = 0.0f;
	float V_2 = 0.0f;
	float V_3 = 0.0f;
	bool V_4 = false;
	{
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_0 = ___0_lhs;
		float L_1 = L_0.___x_2;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_2 = ___1_rhs;
		float L_3 = L_2.___x_2;
		V_0 = ((float)il2cpp_codegen_subtract(L_1, L_3));
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_4 = ___0_lhs;
		float L_5 = L_4.___y_3;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_6 = ___1_rhs;
		float L_7 = L_6.___y_3;
		V_1 = ((float)il2cpp_codegen_subtract(L_5, L_7));
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_8 = ___0_lhs;
		float L_9 = L_8.___z_4;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_10 = ___1_rhs;
		float L_11 = L_10.___z_4;
		V_2 = ((float)il2cpp_codegen_subtract(L_9, L_11));
		float L_12 = V_0;
		float L_13 = V_0;
		float L_14 = V_1;
		float L_15 = V_1;
		float L_16 = V_2;
		float L_17 = V_2;
		V_3 = ((float)il2cpp_codegen_add(((float)il2cpp_codegen_add(((float)il2cpp_codegen_multiply(L_12, L_13)), ((float)il2cpp_codegen_multiply(L_14, L_15)))), ((float)il2cpp_codegen_multiply(L_16, L_17))));
		float L_18 = V_3;
		V_4 = (bool)((((float)L_18) < ((float)(9.99999944E-11f)))? 1 : 0);
		goto IL_0043;
	}

IL_0043:
	{
		bool L_19 = V_4;
		return L_19;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* HandGrabPose_get_RelativeTo_m5DFEA3BC65DF4B58C8BB75628AFB81A18B9399C4_inline (HandGrabPose_t347682EF05B32B395F27F50DB38500405C17C633* __this, const RuntimeMethod* method) 
{
	{
		// public Transform RelativeTo { get => _relativeTo; set => _relativeTo = value; }
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_0 = __this->____relativeTo_4;
		return L_0;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void CylinderSurface_set_Data_m87230DD8507A149A857F965B1BE004A62ADBC500_inline (CylinderSurface_t071F3C35CED4F1296F988B93595DF4736770D4F7* __this, CylinderSurfaceData_t8F949CD513A3C48F106518CF244120A2C884D700* ___0_value, const RuntimeMethod* method) 
{
	{
		// _data = value;
		CylinderSurfaceData_t8F949CD513A3C48F106518CF244120A2C884D700* L_0 = ___0_value;
		__this->____data_4 = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->____data_4), (void*)L_0);
		// }
		return;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 Vector3_op_Multiply_m87BA7C578F96C8E49BB07088DAAC4649F83B0353_inline (Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___0_a, float ___1_d, const RuntimeMethod* method) 
{
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_0 = ___0_a;
		float L_1 = L_0.___x_2;
		float L_2 = ___1_d;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_3 = ___0_a;
		float L_4 = L_3.___y_3;
		float L_5 = ___1_d;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_6 = ___0_a;
		float L_7 = L_6.___z_4;
		float L_8 = ___1_d;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_9;
		memset((&L_9), 0, sizeof(L_9));
		Vector3__ctor_m376936E6B999EF1ECBE57D990A386303E2283DE0_inline((&L_9), ((float)il2cpp_codegen_multiply(L_1, L_2)), ((float)il2cpp_codegen_multiply(L_4, L_5)), ((float)il2cpp_codegen_multiply(L_7, L_8)), /*hidden argument*/NULL);
		V_0 = L_9;
		goto IL_0021;
	}

IL_0021:
	{
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_10 = V_0;
		return L_10;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR float Vector3_Dot_mBB86BB940AA0A32FA7D3C02AC42E5BC7095A5D52_inline (Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___0_lhs, Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___1_rhs, const RuntimeMethod* method) 
{
	float V_0 = 0.0f;
	{
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_0 = ___0_lhs;
		float L_1 = L_0.___x_2;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_2 = ___1_rhs;
		float L_3 = L_2.___x_2;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_4 = ___0_lhs;
		float L_5 = L_4.___y_3;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_6 = ___1_rhs;
		float L_7 = L_6.___y_3;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_8 = ___0_lhs;
		float L_9 = L_8.___z_4;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_10 = ___1_rhs;
		float L_11 = L_10.___z_4;
		V_0 = ((float)il2cpp_codegen_add(((float)il2cpp_codegen_add(((float)il2cpp_codegen_multiply(L_1, L_3)), ((float)il2cpp_codegen_multiply(L_5, L_7)))), ((float)il2cpp_codegen_multiply(L_9, L_11))));
		goto IL_002d;
	}

IL_002d:
	{
		float L_12 = V_0;
		return L_12;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 Vector3_get_zero_m0C1249C3F25B1C70EAD3CC8B31259975A457AE39_inline (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_0 = ((Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2_StaticFields*)il2cpp_codegen_static_fields_for(Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2_il2cpp_TypeInfo_var))->___zeroVector_5;
		V_0 = L_0;
		goto IL_0009;
	}

IL_0009:
	{
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_1 = V_0;
		return L_1;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR float Vector3_SignedAngle_m76C77F9D7BAF5969FA5B7500ED2D5FF9F9FA4153_inline (Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___0_from, Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___1_to, Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___2_axis, const RuntimeMethod* method) 
{
	float V_0 = 0.0f;
	float V_1 = 0.0f;
	float V_2 = 0.0f;
	float V_3 = 0.0f;
	float V_4 = 0.0f;
	float V_5 = 0.0f;
	{
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_0 = ___0_from;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_1 = ___1_to;
		float L_2;
		L_2 = Vector3_Angle_mB16906B482814C140FE5BA9D041D2DC11E42A68D_inline(L_0, L_1, NULL);
		V_0 = L_2;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_3 = ___0_from;
		float L_4 = L_3.___y_3;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_5 = ___1_to;
		float L_6 = L_5.___z_4;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_7 = ___0_from;
		float L_8 = L_7.___z_4;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_9 = ___1_to;
		float L_10 = L_9.___y_3;
		V_1 = ((float)il2cpp_codegen_subtract(((float)il2cpp_codegen_multiply(L_4, L_6)), ((float)il2cpp_codegen_multiply(L_8, L_10))));
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_11 = ___0_from;
		float L_12 = L_11.___z_4;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_13 = ___1_to;
		float L_14 = L_13.___x_2;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_15 = ___0_from;
		float L_16 = L_15.___x_2;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_17 = ___1_to;
		float L_18 = L_17.___z_4;
		V_2 = ((float)il2cpp_codegen_subtract(((float)il2cpp_codegen_multiply(L_12, L_14)), ((float)il2cpp_codegen_multiply(L_16, L_18))));
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_19 = ___0_from;
		float L_20 = L_19.___x_2;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_21 = ___1_to;
		float L_22 = L_21.___y_3;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_23 = ___0_from;
		float L_24 = L_23.___y_3;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_25 = ___1_to;
		float L_26 = L_25.___x_2;
		V_3 = ((float)il2cpp_codegen_subtract(((float)il2cpp_codegen_multiply(L_20, L_22)), ((float)il2cpp_codegen_multiply(L_24, L_26))));
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_27 = ___2_axis;
		float L_28 = L_27.___x_2;
		float L_29 = V_1;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_30 = ___2_axis;
		float L_31 = L_30.___y_3;
		float L_32 = V_2;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_33 = ___2_axis;
		float L_34 = L_33.___z_4;
		float L_35 = V_3;
		float L_36;
		L_36 = Mathf_Sign_m42EE1F0BC041AF14F89DED7F762BE996E2C50D8A_inline(((float)il2cpp_codegen_add(((float)il2cpp_codegen_add(((float)il2cpp_codegen_multiply(L_28, L_29)), ((float)il2cpp_codegen_multiply(L_31, L_32)))), ((float)il2cpp_codegen_multiply(L_34, L_35)))), NULL);
		V_4 = L_36;
		float L_37 = V_0;
		float L_38 = V_4;
		V_5 = ((float)il2cpp_codegen_multiply(L_37, L_38));
		goto IL_0086;
	}

IL_0086:
	{
		float L_39 = V_5;
		return L_39;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR float Mathf_Max_m870B77F6A5225EC21D9778043F987B9B17D08F6B_inline (SingleU5BU5D_t89DEFE97BCEDB5857010E79ECE0F52CF6E93B87C* ___0_values, const RuntimeMethod* method) 
{
	int32_t V_0 = 0;
	float V_1 = 0.0f;
	bool V_2 = false;
	float V_3 = 0.0f;
	int32_t V_4 = 0;
	bool V_5 = false;
	bool V_6 = false;
	{
		SingleU5BU5D_t89DEFE97BCEDB5857010E79ECE0F52CF6E93B87C* L_0 = ___0_values;
		NullCheck(L_0);
		V_0 = ((int32_t)(((RuntimeArray*)L_0)->max_length));
		int32_t L_1 = V_0;
		V_2 = (bool)((((int32_t)L_1) == ((int32_t)0))? 1 : 0);
		bool L_2 = V_2;
		if (!L_2)
		{
			goto IL_0015;
		}
	}
	{
		V_3 = (0.0f);
		goto IL_0047;
	}

IL_0015:
	{
		SingleU5BU5D_t89DEFE97BCEDB5857010E79ECE0F52CF6E93B87C* L_3 = ___0_values;
		NullCheck(L_3);
		int32_t L_4 = 0;
		float L_5 = (L_3)->GetAt(static_cast<il2cpp_array_size_t>(L_4));
		V_1 = L_5;
		V_4 = 1;
		goto IL_0038;
	}

IL_001e:
	{
		SingleU5BU5D_t89DEFE97BCEDB5857010E79ECE0F52CF6E93B87C* L_6 = ___0_values;
		int32_t L_7 = V_4;
		NullCheck(L_6);
		int32_t L_8 = L_7;
		float L_9 = (L_6)->GetAt(static_cast<il2cpp_array_size_t>(L_8));
		float L_10 = V_1;
		V_5 = (bool)((((float)L_9) > ((float)L_10))? 1 : 0);
		bool L_11 = V_5;
		if (!L_11)
		{
			goto IL_0031;
		}
	}
	{
		SingleU5BU5D_t89DEFE97BCEDB5857010E79ECE0F52CF6E93B87C* L_12 = ___0_values;
		int32_t L_13 = V_4;
		NullCheck(L_12);
		int32_t L_14 = L_13;
		float L_15 = (L_12)->GetAt(static_cast<il2cpp_array_size_t>(L_14));
		V_1 = L_15;
	}

IL_0031:
	{
		int32_t L_16 = V_4;
		V_4 = ((int32_t)il2cpp_codegen_add(L_16, 1));
	}

IL_0038:
	{
		int32_t L_17 = V_4;
		int32_t L_18 = V_0;
		V_6 = (bool)((((int32_t)L_17) < ((int32_t)L_18))? 1 : 0);
		bool L_19 = V_6;
		if (L_19)
		{
			goto IL_001e;
		}
	}
	{
		float L_20 = V_1;
		V_3 = L_20;
		goto IL_0047;
	}

IL_0047:
	{
		float L_21 = V_3;
		return L_21;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 Quaternion_op_Multiply_mCB375FCCC12A2EC8F9EB824A1BFB4453B58C2012_inline (Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 ___0_lhs, Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 ___1_rhs, const RuntimeMethod* method) 
{
	Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_0 = ___0_lhs;
		float L_1 = L_0.___w_3;
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_2 = ___1_rhs;
		float L_3 = L_2.___x_0;
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_4 = ___0_lhs;
		float L_5 = L_4.___x_0;
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_6 = ___1_rhs;
		float L_7 = L_6.___w_3;
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_8 = ___0_lhs;
		float L_9 = L_8.___y_1;
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_10 = ___1_rhs;
		float L_11 = L_10.___z_2;
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_12 = ___0_lhs;
		float L_13 = L_12.___z_2;
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_14 = ___1_rhs;
		float L_15 = L_14.___y_1;
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_16 = ___0_lhs;
		float L_17 = L_16.___w_3;
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_18 = ___1_rhs;
		float L_19 = L_18.___y_1;
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_20 = ___0_lhs;
		float L_21 = L_20.___y_1;
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_22 = ___1_rhs;
		float L_23 = L_22.___w_3;
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_24 = ___0_lhs;
		float L_25 = L_24.___z_2;
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_26 = ___1_rhs;
		float L_27 = L_26.___x_0;
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_28 = ___0_lhs;
		float L_29 = L_28.___x_0;
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_30 = ___1_rhs;
		float L_31 = L_30.___z_2;
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_32 = ___0_lhs;
		float L_33 = L_32.___w_3;
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_34 = ___1_rhs;
		float L_35 = L_34.___z_2;
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_36 = ___0_lhs;
		float L_37 = L_36.___z_2;
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_38 = ___1_rhs;
		float L_39 = L_38.___w_3;
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_40 = ___0_lhs;
		float L_41 = L_40.___x_0;
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_42 = ___1_rhs;
		float L_43 = L_42.___y_1;
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_44 = ___0_lhs;
		float L_45 = L_44.___y_1;
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_46 = ___1_rhs;
		float L_47 = L_46.___x_0;
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_48 = ___0_lhs;
		float L_49 = L_48.___w_3;
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_50 = ___1_rhs;
		float L_51 = L_50.___w_3;
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_52 = ___0_lhs;
		float L_53 = L_52.___x_0;
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_54 = ___1_rhs;
		float L_55 = L_54.___x_0;
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_56 = ___0_lhs;
		float L_57 = L_56.___y_1;
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_58 = ___1_rhs;
		float L_59 = L_58.___y_1;
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_60 = ___0_lhs;
		float L_61 = L_60.___z_2;
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_62 = ___1_rhs;
		float L_63 = L_62.___z_2;
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_64;
		memset((&L_64), 0, sizeof(L_64));
		Quaternion__ctor_m868FD60AA65DD5A8AC0C5DEB0608381A8D85FCD8_inline((&L_64), ((float)il2cpp_codegen_subtract(((float)il2cpp_codegen_add(((float)il2cpp_codegen_add(((float)il2cpp_codegen_multiply(L_1, L_3)), ((float)il2cpp_codegen_multiply(L_5, L_7)))), ((float)il2cpp_codegen_multiply(L_9, L_11)))), ((float)il2cpp_codegen_multiply(L_13, L_15)))), ((float)il2cpp_codegen_subtract(((float)il2cpp_codegen_add(((float)il2cpp_codegen_add(((float)il2cpp_codegen_multiply(L_17, L_19)), ((float)il2cpp_codegen_multiply(L_21, L_23)))), ((float)il2cpp_codegen_multiply(L_25, L_27)))), ((float)il2cpp_codegen_multiply(L_29, L_31)))), ((float)il2cpp_codegen_subtract(((float)il2cpp_codegen_add(((float)il2cpp_codegen_add(((float)il2cpp_codegen_multiply(L_33, L_35)), ((float)il2cpp_codegen_multiply(L_37, L_39)))), ((float)il2cpp_codegen_multiply(L_41, L_43)))), ((float)il2cpp_codegen_multiply(L_45, L_47)))), ((float)il2cpp_codegen_subtract(((float)il2cpp_codegen_subtract(((float)il2cpp_codegen_subtract(((float)il2cpp_codegen_multiply(L_49, L_51)), ((float)il2cpp_codegen_multiply(L_53, L_55)))), ((float)il2cpp_codegen_multiply(L_57, L_59)))), ((float)il2cpp_codegen_multiply(L_61, L_63)))), /*hidden argument*/NULL);
		V_0 = L_64;
		goto IL_00e5;
	}

IL_00e5:
	{
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_65 = V_0;
		return L_65;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void CylinderSurface_InjectData_m4466DEF60883B115512651A9AABA2084187E6702_inline (CylinderSurface_t071F3C35CED4F1296F988B93595DF4736770D4F7* __this, CylinderSurfaceData_t8F949CD513A3C48F106518CF244120A2C884D700* ___0_data, const RuntimeMethod* method) 
{
	{
		// _data = data;
		CylinderSurfaceData_t8F949CD513A3C48F106518CF244120A2C884D700* L_0 = ___0_data;
		__this->____data_4 = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->____data_4), (void*)L_0);
		// }
		return;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void CylinderSurface_InjectRelativeTo_m656BD54F4AF5E9937B458F16AB710AC356B2EE41_inline (CylinderSurface_t071F3C35CED4F1296F988B93595DF4736770D4F7* __this, Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* ___0_relativeTo, const RuntimeMethod* method) 
{
	{
		// _relativeTo = relativeTo;
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_0 = ___0_relativeTo;
		__this->____relativeTo_5 = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->____relativeTo_5), (void*)L_0);
		// }
		return;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void CylinderSurface_InjectGripPoint_mD9FE7A069861670C8803680CC3385866A22908EB_inline (CylinderSurface_t071F3C35CED4F1296F988B93595DF4736770D4F7* __this, Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* ___0_gripPoint, const RuntimeMethod* method) 
{
	{
		// _gripPoint = gripPoint;
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_0 = ___0_gripPoint;
		__this->____gripPoint_6 = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->____gripPoint_6), (void*)L_0);
		// }
		return;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971 PoseCalculator_Invoke_m3E4562A6120025438594E5257CFC28EBE2489014_inline (PoseCalculator_t74377137B14A0C57A79A02A9C33C5814E64F1839* __this, Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* ___0_desiredPose, Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971* ___1_snapPose, const RuntimeMethod* method) 
{
	typedef Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971 (*FunctionPointerType) (RuntimeObject*, Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971*, Pose_t06BA69EAA6E9FAF60056D519A87D25F54AFE7971*, const RuntimeMethod*);
	return ((FunctionPointerType)__this->___invoke_impl_1)((Il2CppObject*)__this->___method_code_6, ___0_desiredPose, ___1_snapPose, reinterpret_cast<RuntimeMethod*>(__this->___method_3));
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR float PoseMeasureParameters_get_MaxDistance_m7F443662F2C07ECBDFA621D40147E782266495EE_inline (PoseMeasureParameters_tB95EB8BC83F81E86544854D1B13C04E88F2C0203* __this, const RuntimeMethod* method) 
{
	{
		// public float MaxDistance => _maxDistance;
		float L_0 = __this->____maxDistance_0;
		return L_0;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* SphereSurface_get_RelativeTo_m012CCC88748350E90BC484526CB65F7AD94132CC_inline (SphereSurface_t7C6F8A614D76E8B3AE10EE722BBDB6774B96A30B* __this, const RuntimeMethod* method) 
{
	{
		// get => _relativeTo;
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_0 = __this->____relativeTo_5;
		return L_0;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* SphereSurface_get_GripPoint_m5E0F7198EDAC70BECB20A3B861CD7C854324ABA6_inline (SphereSurface_t7C6F8A614D76E8B3AE10EE722BBDB6774B96A30B* __this, const RuntimeMethod* method) 
{
	{
		// get => _gripPoint;
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_0 = __this->____gripPoint_6;
		return L_0;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 Vector3_Cross_mF93A280558BCE756D13B6CC5DCD7DE8A43148987_inline (Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___0_lhs, Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___1_rhs, const RuntimeMethod* method) 
{
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_0 = ___0_lhs;
		float L_1 = L_0.___y_3;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_2 = ___1_rhs;
		float L_3 = L_2.___z_4;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_4 = ___0_lhs;
		float L_5 = L_4.___z_4;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_6 = ___1_rhs;
		float L_7 = L_6.___y_3;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_8 = ___0_lhs;
		float L_9 = L_8.___z_4;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_10 = ___1_rhs;
		float L_11 = L_10.___x_2;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_12 = ___0_lhs;
		float L_13 = L_12.___x_2;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_14 = ___1_rhs;
		float L_15 = L_14.___z_4;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_16 = ___0_lhs;
		float L_17 = L_16.___x_2;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_18 = ___1_rhs;
		float L_19 = L_18.___y_3;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_20 = ___0_lhs;
		float L_21 = L_20.___y_3;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_22 = ___1_rhs;
		float L_23 = L_22.___x_2;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_24;
		memset((&L_24), 0, sizeof(L_24));
		Vector3__ctor_m376936E6B999EF1ECBE57D990A386303E2283DE0_inline((&L_24), ((float)il2cpp_codegen_subtract(((float)il2cpp_codegen_multiply(L_1, L_3)), ((float)il2cpp_codegen_multiply(L_5, L_7)))), ((float)il2cpp_codegen_subtract(((float)il2cpp_codegen_multiply(L_9, L_11)), ((float)il2cpp_codegen_multiply(L_13, L_15)))), ((float)il2cpp_codegen_subtract(((float)il2cpp_codegen_multiply(L_17, L_19)), ((float)il2cpp_codegen_multiply(L_21, L_23)))), /*hidden argument*/NULL);
		V_0 = L_24;
		goto IL_005a;
	}

IL_005a:
	{
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_25 = V_0;
		return L_25;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void SphereSurface_set_Data_m463CDA705345ED889DAD5A98A3A704F56A65BD26_inline (SphereSurface_t7C6F8A614D76E8B3AE10EE722BBDB6774B96A30B* __this, SphereSurfaceData_tE8331CA08F2DB69533F08417636C96FFA01E7CC1* ___0_value, const RuntimeMethod* method) 
{
	{
		// _data = value;
		SphereSurfaceData_tE8331CA08F2DB69533F08417636C96FFA01E7CC1* L_0 = ___0_value;
		__this->____data_4 = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->____data_4), (void*)L_0);
		// }
		return;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void SphereSurface_InjectData_m5B528A93C577760B667FE1A98CA1A71B4DD6861F_inline (SphereSurface_t7C6F8A614D76E8B3AE10EE722BBDB6774B96A30B* __this, SphereSurfaceData_tE8331CA08F2DB69533F08417636C96FFA01E7CC1* ___0_data, const RuntimeMethod* method) 
{
	{
		// _data = data;
		SphereSurfaceData_tE8331CA08F2DB69533F08417636C96FFA01E7CC1* L_0 = ___0_data;
		__this->____data_4 = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->____data_4), (void*)L_0);
		// }
		return;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void SphereSurface_InjectRelativeTo_m6B1E6609020D4F861398621C3B3AAA89E822B781_inline (SphereSurface_t7C6F8A614D76E8B3AE10EE722BBDB6774B96A30B* __this, Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* ___0_relativeTo, const RuntimeMethod* method) 
{
	{
		// _relativeTo = relativeTo;
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_0 = ___0_relativeTo;
		__this->____relativeTo_5 = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->____relativeTo_5), (void*)L_0);
		// }
		return;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void SphereSurface_InjectGripPoint_m2329EBE5F4F5CBAD77341BCF37E9F3BF8B7F9DC4_inline (SphereSurface_t7C6F8A614D76E8B3AE10EE722BBDB6774B96A30B* __this, Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* ___0_gripPoint, const RuntimeMethod* method) 
{
	{
		// _gripPoint = gripPoint;
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_0 = ___0_gripPoint;
		__this->____gripPoint_6 = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->____gripPoint_6), (void*)L_0);
		// }
		return;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 Vector3_Normalize_mEF8349CC39674236CFC694189AFD36E31F89AC8F_inline (Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___0_value, const RuntimeMethod* method) 
{
	float V_0 = 0.0f;
	bool V_1 = false;
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 V_2;
	memset((&V_2), 0, sizeof(V_2));
	{
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_0 = ___0_value;
		float L_1;
		L_1 = Vector3_Magnitude_m21652D951393A3D7CE92CE40049A0E7F76544D1B_inline(L_0, NULL);
		V_0 = L_1;
		float L_2 = V_0;
		V_1 = (bool)((((float)L_2) > ((float)(9.99999975E-06f)))? 1 : 0);
		bool L_3 = V_1;
		if (!L_3)
		{
			goto IL_001e;
		}
	}
	{
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_4 = ___0_value;
		float L_5 = V_0;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_6;
		L_6 = Vector3_op_Division_mCC6BB24E372AB96B8380D1678446EF6A8BAE13BB_inline(L_4, L_5, NULL);
		V_2 = L_6;
		goto IL_0026;
	}

IL_001e:
	{
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_7;
		L_7 = Vector3_get_zero_m0C1249C3F25B1C70EAD3CC8B31259975A457AE39_inline(NULL);
		V_2 = L_7;
		goto IL_0026;
	}

IL_0026:
	{
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_8 = V_2;
		return L_8;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR float Mathf_Clamp_mEB9AEA827D27D20FCC787F7375156AF46BB12BBF_inline (float ___0_value, float ___1_min, float ___2_max, const RuntimeMethod* method) 
{
	bool V_0 = false;
	bool V_1 = false;
	float V_2 = 0.0f;
	{
		float L_0 = ___0_value;
		float L_1 = ___1_min;
		V_0 = (bool)((((float)L_0) < ((float)L_1))? 1 : 0);
		bool L_2 = V_0;
		if (!L_2)
		{
			goto IL_000e;
		}
	}
	{
		float L_3 = ___1_min;
		___0_value = L_3;
		goto IL_0019;
	}

IL_000e:
	{
		float L_4 = ___0_value;
		float L_5 = ___2_max;
		V_1 = (bool)((((float)L_4) > ((float)L_5))? 1 : 0);
		bool L_6 = V_1;
		if (!L_6)
		{
			goto IL_0019;
		}
	}
	{
		float L_7 = ___2_max;
		___0_value = L_7;
	}

IL_0019:
	{
		float L_8 = ___0_value;
		V_2 = L_8;
		goto IL_001d;
	}

IL_001d:
	{
		float L_9 = V_2;
		return L_9;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR float Vector3_Angle_mB16906B482814C140FE5BA9D041D2DC11E42A68D_inline (Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___0_from, Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___1_to, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Math_tEB65DE7CA8B083C412C969C92981C030865486CE_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	float V_0 = 0.0f;
	float V_1 = 0.0f;
	bool V_2 = false;
	float V_3 = 0.0f;
	{
		float L_0;
		L_0 = Vector3_get_sqrMagnitude_m43C27DEC47C4811FB30AB474FF2131A963B66FC8_inline((&___0_from), NULL);
		float L_1;
		L_1 = Vector3_get_sqrMagnitude_m43C27DEC47C4811FB30AB474FF2131A963B66FC8_inline((&___1_to), NULL);
		il2cpp_codegen_runtime_class_init_inline(Math_tEB65DE7CA8B083C412C969C92981C030865486CE_il2cpp_TypeInfo_var);
		double L_2;
		L_2 = sqrt(((double)((float)il2cpp_codegen_multiply(L_0, L_1))));
		V_0 = ((float)L_2);
		float L_3 = V_0;
		V_2 = (bool)((((float)L_3) < ((float)(1.0E-15f)))? 1 : 0);
		bool L_4 = V_2;
		if (!L_4)
		{
			goto IL_002c;
		}
	}
	{
		V_3 = (0.0f);
		goto IL_0056;
	}

IL_002c:
	{
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_5 = ___0_from;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_6 = ___1_to;
		float L_7;
		L_7 = Vector3_Dot_mBB86BB940AA0A32FA7D3C02AC42E5BC7095A5D52_inline(L_5, L_6, NULL);
		float L_8 = V_0;
		float L_9;
		L_9 = Mathf_Clamp_mEB9AEA827D27D20FCC787F7375156AF46BB12BBF_inline(((float)(L_7/L_8)), (-1.0f), (1.0f), NULL);
		V_1 = L_9;
		float L_10 = V_1;
		il2cpp_codegen_runtime_class_init_inline(Math_tEB65DE7CA8B083C412C969C92981C030865486CE_il2cpp_TypeInfo_var);
		double L_11;
		L_11 = acos(((double)L_10));
		V_3 = ((float)il2cpp_codegen_multiply(((float)L_11), (57.2957802f)));
		goto IL_0056;
	}

IL_0056:
	{
		float L_12 = V_3;
		return L_12;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR float Mathf_Sign_m42EE1F0BC041AF14F89DED7F762BE996E2C50D8A_inline (float ___0_f, const RuntimeMethod* method) 
{
	float V_0 = 0.0f;
	float G_B3_0 = 0.0f;
	{
		float L_0 = ___0_f;
		if ((((float)L_0) >= ((float)(0.0f))))
		{
			goto IL_0010;
		}
	}
	{
		G_B3_0 = (-1.0f);
		goto IL_0015;
	}

IL_0010:
	{
		G_B3_0 = (1.0f);
	}

IL_0015:
	{
		V_0 = G_B3_0;
		goto IL_0018;
	}

IL_0018:
	{
		float L_1 = V_0;
		return L_1;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void Quaternion__ctor_m868FD60AA65DD5A8AC0C5DEB0608381A8D85FCD8_inline (Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974* __this, float ___0_x, float ___1_y, float ___2_z, float ___3_w, const RuntimeMethod* method) 
{
	{
		float L_0 = ___0_x;
		__this->___x_0 = L_0;
		float L_1 = ___1_y;
		__this->___y_1 = L_1;
		float L_2 = ___2_z;
		__this->___z_2 = L_2;
		float L_3 = ___3_w;
		__this->___w_3 = L_3;
		return;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR float Vector3_Magnitude_m21652D951393A3D7CE92CE40049A0E7F76544D1B_inline (Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___0_vector, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Math_tEB65DE7CA8B083C412C969C92981C030865486CE_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	float V_0 = 0.0f;
	{
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_0 = ___0_vector;
		float L_1 = L_0.___x_2;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_2 = ___0_vector;
		float L_3 = L_2.___x_2;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_4 = ___0_vector;
		float L_5 = L_4.___y_3;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_6 = ___0_vector;
		float L_7 = L_6.___y_3;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_8 = ___0_vector;
		float L_9 = L_8.___z_4;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_10 = ___0_vector;
		float L_11 = L_10.___z_4;
		il2cpp_codegen_runtime_class_init_inline(Math_tEB65DE7CA8B083C412C969C92981C030865486CE_il2cpp_TypeInfo_var);
		double L_12;
		L_12 = sqrt(((double)((float)il2cpp_codegen_add(((float)il2cpp_codegen_add(((float)il2cpp_codegen_multiply(L_1, L_3)), ((float)il2cpp_codegen_multiply(L_5, L_7)))), ((float)il2cpp_codegen_multiply(L_9, L_11))))));
		V_0 = ((float)L_12);
		goto IL_0034;
	}

IL_0034:
	{
		float L_13 = V_0;
		return L_13;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 Vector3_op_Division_mCC6BB24E372AB96B8380D1678446EF6A8BAE13BB_inline (Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___0_a, float ___1_d, const RuntimeMethod* method) 
{
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_0 = ___0_a;
		float L_1 = L_0.___x_2;
		float L_2 = ___1_d;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_3 = ___0_a;
		float L_4 = L_3.___y_3;
		float L_5 = ___1_d;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_6 = ___0_a;
		float L_7 = L_6.___z_4;
		float L_8 = ___1_d;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_9;
		memset((&L_9), 0, sizeof(L_9));
		Vector3__ctor_m376936E6B999EF1ECBE57D990A386303E2283DE0_inline((&L_9), ((float)(L_1/L_2)), ((float)(L_4/L_5)), ((float)(L_7/L_8)), /*hidden argument*/NULL);
		V_0 = L_9;
		goto IL_0021;
	}

IL_0021:
	{
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_10 = V_0;
		return L_10;
	}
}
