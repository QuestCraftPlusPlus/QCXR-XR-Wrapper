#include "pch-cpp.hpp"

#ifndef _MSC_VER
# include <alloca.h>
#else
# include <malloc.h>
#endif


#include <limits>



// System.Action`1<UnityEngine.AsyncOperation>
struct Action_1_tE8693FF0E67CDBA52BAFB211BFF1844D076ABAFB;
// System.Delegate[]
struct DelegateU5BU5D_tC5AB7E8F745616680F337909D3A8E6C722CDF771;
// System.Type[]
struct TypeU5BU5D_t97234E1129B564EB38B8D85CAC2AD8B5B9522FFB;
// UnityEngine.AsyncOperation
struct AsyncOperation_tD2789250E4B098DEDA92B366A577E500A92D2D3C;
// System.Reflection.Binder
struct Binder_t91BFCE95A7057FADF4D8A1A342AFE52872246235;
// System.DelegateData
struct DelegateData_t9B286B493293CD2D23A5B2B5EF0E5B1324C2B77E;
// System.Reflection.MemberFilter
struct MemberFilter_tF644F1AE82F611B677CE1964D5A3277DDA21D553;
// System.Reflection.MethodInfo
struct MethodInfo_t;
// System.String
struct String_t;
// System.Type
struct Type_t;
// System.Void
struct Void_t4861ACF8F4594C3437BB48B6E56783494B843915;

IL2CPP_EXTERN_C RuntimeClass* Action_1_tE8693FF0E67CDBA52BAFB211BFF1844D076ABAFB_il2cpp_TypeInfo_var;
struct Delegate_t_marshaled_com;
struct Delegate_t_marshaled_pinvoke;


IL2CPP_EXTERN_C_BEGIN
IL2CPP_EXTERN_C_END

#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif

// <Module>
struct U3CModuleU3E_tAE2FD82E48B7893673000630524705C468010525 
{
};

// System.Reflection.MemberInfo
struct MemberInfo_t  : public RuntimeObject
{
};

// System.String
struct String_t  : public RuntimeObject
{
	// System.Int32 System.String::_stringLength
	int32_t ____stringLength_4;
	// System.Char System.String::_firstChar
	Il2CppChar ____firstChar_5;
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

// UnityEngine.YieldInstruction
struct YieldInstruction_tFCE35FD0907950EFEE9BC2890AC664E41C53728D  : public RuntimeObject
{
};
// Native definition for P/Invoke marshalling of UnityEngine.YieldInstruction
struct YieldInstruction_tFCE35FD0907950EFEE9BC2890AC664E41C53728D_marshaled_pinvoke
{
};
// Native definition for COM marshalling of UnityEngine.YieldInstruction
struct YieldInstruction_tFCE35FD0907950EFEE9BC2890AC664E41C53728D_marshaled_com
{
};

// System.IntPtr
struct IntPtr_t 
{
	// System.Void* System.IntPtr::m_value
	void* ___m_value_0;
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

// UnityEngine.AsyncOperation
struct AsyncOperation_tD2789250E4B098DEDA92B366A577E500A92D2D3C  : public YieldInstruction_tFCE35FD0907950EFEE9BC2890AC664E41C53728D
{
	// System.IntPtr UnityEngine.AsyncOperation::m_Ptr
	intptr_t ___m_Ptr_0;
	// System.Action`1<UnityEngine.AsyncOperation> UnityEngine.AsyncOperation::m_completeCallback
	Action_1_tE8693FF0E67CDBA52BAFB211BFF1844D076ABAFB* ___m_completeCallback_1;
};
// Native definition for P/Invoke marshalling of UnityEngine.AsyncOperation
struct AsyncOperation_tD2789250E4B098DEDA92B366A577E500A92D2D3C_marshaled_pinvoke : public YieldInstruction_tFCE35FD0907950EFEE9BC2890AC664E41C53728D_marshaled_pinvoke
{
	intptr_t ___m_Ptr_0;
	Il2CppMethodPointer ___m_completeCallback_1;
};
// Native definition for COM marshalling of UnityEngine.AsyncOperation
struct AsyncOperation_tD2789250E4B098DEDA92B366A577E500A92D2D3C_marshaled_com : public YieldInstruction_tFCE35FD0907950EFEE9BC2890AC664E41C53728D_marshaled_com
{
	intptr_t ___m_Ptr_0;
	Il2CppMethodPointer ___m_completeCallback_1;
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

// System.RuntimeTypeHandle
struct RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B 
{
	// System.IntPtr System.RuntimeTypeHandle::value
	intptr_t ___value_0;
};

// UnityEngine.AssetBundleCreateRequest
struct AssetBundleCreateRequest_t73B8714B9459A01540E091C3770A408E67188CF6  : public AsyncOperation_tD2789250E4B098DEDA92B366A577E500A92D2D3C
{
};
// Native definition for P/Invoke marshalling of UnityEngine.AssetBundleCreateRequest
struct AssetBundleCreateRequest_t73B8714B9459A01540E091C3770A408E67188CF6_marshaled_pinvoke : public AsyncOperation_tD2789250E4B098DEDA92B366A577E500A92D2D3C_marshaled_pinvoke
{
};
// Native definition for COM marshalling of UnityEngine.AssetBundleCreateRequest
struct AssetBundleCreateRequest_t73B8714B9459A01540E091C3770A408E67188CF6_marshaled_com : public AsyncOperation_tD2789250E4B098DEDA92B366A577E500A92D2D3C_marshaled_com
{
};

// UnityEngine.AssetBundleRecompressOperation
struct AssetBundleRecompressOperation_tFDA1FB5AE1E072FC6CAC1CF0064C13D77F87CDDE  : public AsyncOperation_tD2789250E4B098DEDA92B366A577E500A92D2D3C
{
};
// Native definition for P/Invoke marshalling of UnityEngine.AssetBundleRecompressOperation
struct AssetBundleRecompressOperation_tFDA1FB5AE1E072FC6CAC1CF0064C13D77F87CDDE_marshaled_pinvoke : public AsyncOperation_tD2789250E4B098DEDA92B366A577E500A92D2D3C_marshaled_pinvoke
{
};
// Native definition for COM marshalling of UnityEngine.AssetBundleRecompressOperation
struct AssetBundleRecompressOperation_tFDA1FB5AE1E072FC6CAC1CF0064C13D77F87CDDE_marshaled_com : public AsyncOperation_tD2789250E4B098DEDA92B366A577E500A92D2D3C_marshaled_com
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

// UnityEngine.ResourceRequest
struct ResourceRequest_tE6953FBA45EAAEFE866C635B9E7852044E62D868  : public AsyncOperation_tD2789250E4B098DEDA92B366A577E500A92D2D3C
{
	// System.String UnityEngine.ResourceRequest::m_Path
	String_t* ___m_Path_2;
	// System.Type UnityEngine.ResourceRequest::m_Type
	Type_t* ___m_Type_3;
};
// Native definition for P/Invoke marshalling of UnityEngine.ResourceRequest
struct ResourceRequest_tE6953FBA45EAAEFE866C635B9E7852044E62D868_marshaled_pinvoke : public AsyncOperation_tD2789250E4B098DEDA92B366A577E500A92D2D3C_marshaled_pinvoke
{
	char* ___m_Path_2;
	Type_t* ___m_Type_3;
};
// Native definition for COM marshalling of UnityEngine.ResourceRequest
struct ResourceRequest_tE6953FBA45EAAEFE866C635B9E7852044E62D868_marshaled_com : public AsyncOperation_tD2789250E4B098DEDA92B366A577E500A92D2D3C_marshaled_com
{
	Il2CppChar* ___m_Path_2;
	Type_t* ___m_Type_3;
};

// System.Type
struct Type_t  : public MemberInfo_t
{
	// System.RuntimeTypeHandle System.Type::_impl
	RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B ____impl_8;
};

// System.Action`1<UnityEngine.AsyncOperation>
struct Action_1_tE8693FF0E67CDBA52BAFB211BFF1844D076ABAFB  : public MulticastDelegate_t
{
};

// UnityEngine.AssetBundleRequest
struct AssetBundleRequest_tED9F5504E75ED1BCFF8DA9B51F5C7356617E6621  : public ResourceRequest_tE6953FBA45EAAEFE866C635B9E7852044E62D868
{
};
// Native definition for P/Invoke marshalling of UnityEngine.AssetBundleRequest
struct AssetBundleRequest_tED9F5504E75ED1BCFF8DA9B51F5C7356617E6621_marshaled_pinvoke : public ResourceRequest_tE6953FBA45EAAEFE866C635B9E7852044E62D868_marshaled_pinvoke
{
};
// Native definition for COM marshalling of UnityEngine.AssetBundleRequest
struct AssetBundleRequest_tED9F5504E75ED1BCFF8DA9B51F5C7356617E6621_marshaled_com : public ResourceRequest_tE6953FBA45EAAEFE866C635B9E7852044E62D868_marshaled_com
{
};

// <Module>

// <Module>

// System.String
struct String_t_StaticFields
{
	// System.String System.String::Empty
	String_t* ___Empty_6;
};

// System.String

// UnityEngine.AssetBundleCreateRequest

// UnityEngine.AssetBundleCreateRequest

// UnityEngine.AssetBundleRecompressOperation

// UnityEngine.AssetBundleRecompressOperation

// System.Type
struct Type_t_StaticFields
{
	// System.Reflection.Binder modreq(System.Runtime.CompilerServices.IsVolatile) System.Type::s_defaultBinder
	Binder_t91BFCE95A7057FADF4D8A1A342AFE52872246235* ___s_defaultBinder_0;
	// System.Char System.Type::Delimiter
	Il2CppChar ___Delimiter_1;
	// System.Type[] System.Type::EmptyTypes
	TypeU5BU5D_t97234E1129B564EB38B8D85CAC2AD8B5B9522FFB* ___EmptyTypes_2;
	// System.Object System.Type::Missing
	RuntimeObject* ___Missing_3;
	// System.Reflection.MemberFilter System.Type::FilterAttribute
	MemberFilter_tF644F1AE82F611B677CE1964D5A3277DDA21D553* ___FilterAttribute_4;
	// System.Reflection.MemberFilter System.Type::FilterName
	MemberFilter_tF644F1AE82F611B677CE1964D5A3277DDA21D553* ___FilterName_5;
	// System.Reflection.MemberFilter System.Type::FilterNameIgnoreCase
	MemberFilter_tF644F1AE82F611B677CE1964D5A3277DDA21D553* ___FilterNameIgnoreCase_6;
};

// System.Type

// System.Action`1<UnityEngine.AsyncOperation>

// System.Action`1<UnityEngine.AsyncOperation>

// UnityEngine.AssetBundleRequest

// UnityEngine.AssetBundleRequest
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
// Conversion methods for marshalling of: UnityEngine.AssetBundleRecompressOperation
IL2CPP_EXTERN_C void AssetBundleRecompressOperation_tFDA1FB5AE1E072FC6CAC1CF0064C13D77F87CDDE_marshal_pinvoke(const AssetBundleRecompressOperation_tFDA1FB5AE1E072FC6CAC1CF0064C13D77F87CDDE& unmarshaled, AssetBundleRecompressOperation_tFDA1FB5AE1E072FC6CAC1CF0064C13D77F87CDDE_marshaled_pinvoke& marshaled)
{
	marshaled.___m_Ptr_0 = unmarshaled.___m_Ptr_0;
	marshaled.___m_completeCallback_1 = il2cpp_codegen_marshal_delegate(reinterpret_cast<MulticastDelegate_t*>(unmarshaled.___m_completeCallback_1));
}
IL2CPP_EXTERN_C void AssetBundleRecompressOperation_tFDA1FB5AE1E072FC6CAC1CF0064C13D77F87CDDE_marshal_pinvoke_back(const AssetBundleRecompressOperation_tFDA1FB5AE1E072FC6CAC1CF0064C13D77F87CDDE_marshaled_pinvoke& marshaled, AssetBundleRecompressOperation_tFDA1FB5AE1E072FC6CAC1CF0064C13D77F87CDDE& unmarshaled)
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Action_1_tE8693FF0E67CDBA52BAFB211BFF1844D076ABAFB_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	intptr_t unmarshaledm_Ptr_temp_0;
	memset((&unmarshaledm_Ptr_temp_0), 0, sizeof(unmarshaledm_Ptr_temp_0));
	unmarshaledm_Ptr_temp_0 = marshaled.___m_Ptr_0;
	unmarshaled.___m_Ptr_0 = unmarshaledm_Ptr_temp_0;
	unmarshaled.___m_completeCallback_1 = il2cpp_codegen_marshal_function_ptr_to_delegate<Action_1_tE8693FF0E67CDBA52BAFB211BFF1844D076ABAFB>(marshaled.___m_completeCallback_1, Action_1_tE8693FF0E67CDBA52BAFB211BFF1844D076ABAFB_il2cpp_TypeInfo_var);
	Il2CppCodeGenWriteBarrier((void**)(&unmarshaled.___m_completeCallback_1), (void*)il2cpp_codegen_marshal_function_ptr_to_delegate<Action_1_tE8693FF0E67CDBA52BAFB211BFF1844D076ABAFB>(marshaled.___m_completeCallback_1, Action_1_tE8693FF0E67CDBA52BAFB211BFF1844D076ABAFB_il2cpp_TypeInfo_var));
}
// Conversion method for clean up from marshalling of: UnityEngine.AssetBundleRecompressOperation
IL2CPP_EXTERN_C void AssetBundleRecompressOperation_tFDA1FB5AE1E072FC6CAC1CF0064C13D77F87CDDE_marshal_pinvoke_cleanup(AssetBundleRecompressOperation_tFDA1FB5AE1E072FC6CAC1CF0064C13D77F87CDDE_marshaled_pinvoke& marshaled)
{
}
// Conversion methods for marshalling of: UnityEngine.AssetBundleRecompressOperation
IL2CPP_EXTERN_C void AssetBundleRecompressOperation_tFDA1FB5AE1E072FC6CAC1CF0064C13D77F87CDDE_marshal_com(const AssetBundleRecompressOperation_tFDA1FB5AE1E072FC6CAC1CF0064C13D77F87CDDE& unmarshaled, AssetBundleRecompressOperation_tFDA1FB5AE1E072FC6CAC1CF0064C13D77F87CDDE_marshaled_com& marshaled)
{
	marshaled.___m_Ptr_0 = unmarshaled.___m_Ptr_0;
	marshaled.___m_completeCallback_1 = il2cpp_codegen_marshal_delegate(reinterpret_cast<MulticastDelegate_t*>(unmarshaled.___m_completeCallback_1));
}
IL2CPP_EXTERN_C void AssetBundleRecompressOperation_tFDA1FB5AE1E072FC6CAC1CF0064C13D77F87CDDE_marshal_com_back(const AssetBundleRecompressOperation_tFDA1FB5AE1E072FC6CAC1CF0064C13D77F87CDDE_marshaled_com& marshaled, AssetBundleRecompressOperation_tFDA1FB5AE1E072FC6CAC1CF0064C13D77F87CDDE& unmarshaled)
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Action_1_tE8693FF0E67CDBA52BAFB211BFF1844D076ABAFB_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	intptr_t unmarshaledm_Ptr_temp_0;
	memset((&unmarshaledm_Ptr_temp_0), 0, sizeof(unmarshaledm_Ptr_temp_0));
	unmarshaledm_Ptr_temp_0 = marshaled.___m_Ptr_0;
	unmarshaled.___m_Ptr_0 = unmarshaledm_Ptr_temp_0;
	unmarshaled.___m_completeCallback_1 = il2cpp_codegen_marshal_function_ptr_to_delegate<Action_1_tE8693FF0E67CDBA52BAFB211BFF1844D076ABAFB>(marshaled.___m_completeCallback_1, Action_1_tE8693FF0E67CDBA52BAFB211BFF1844D076ABAFB_il2cpp_TypeInfo_var);
	Il2CppCodeGenWriteBarrier((void**)(&unmarshaled.___m_completeCallback_1), (void*)il2cpp_codegen_marshal_function_ptr_to_delegate<Action_1_tE8693FF0E67CDBA52BAFB211BFF1844D076ABAFB>(marshaled.___m_completeCallback_1, Action_1_tE8693FF0E67CDBA52BAFB211BFF1844D076ABAFB_il2cpp_TypeInfo_var));
}
// Conversion method for clean up from marshalling of: UnityEngine.AssetBundleRecompressOperation
IL2CPP_EXTERN_C void AssetBundleRecompressOperation_tFDA1FB5AE1E072FC6CAC1CF0064C13D77F87CDDE_marshal_com_cleanup(AssetBundleRecompressOperation_tFDA1FB5AE1E072FC6CAC1CF0064C13D77F87CDDE_marshaled_com& marshaled)
{
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// Conversion methods for marshalling of: UnityEngine.AssetBundleRequest
IL2CPP_EXTERN_C void AssetBundleRequest_tED9F5504E75ED1BCFF8DA9B51F5C7356617E6621_marshal_pinvoke(const AssetBundleRequest_tED9F5504E75ED1BCFF8DA9B51F5C7356617E6621& unmarshaled, AssetBundleRequest_tED9F5504E75ED1BCFF8DA9B51F5C7356617E6621_marshaled_pinvoke& marshaled)
{
	Exception_t* ___m_Type_3Exception = il2cpp_codegen_get_marshal_directive_exception("Cannot marshal field 'm_Type' of type 'AssetBundleRequest': Reference type field marshaling is not supported.");
	IL2CPP_RAISE_MANAGED_EXCEPTION(___m_Type_3Exception, NULL);
}
IL2CPP_EXTERN_C void AssetBundleRequest_tED9F5504E75ED1BCFF8DA9B51F5C7356617E6621_marshal_pinvoke_back(const AssetBundleRequest_tED9F5504E75ED1BCFF8DA9B51F5C7356617E6621_marshaled_pinvoke& marshaled, AssetBundleRequest_tED9F5504E75ED1BCFF8DA9B51F5C7356617E6621& unmarshaled)
{
	Exception_t* ___m_Type_3Exception = il2cpp_codegen_get_marshal_directive_exception("Cannot marshal field 'm_Type' of type 'AssetBundleRequest': Reference type field marshaling is not supported.");
	IL2CPP_RAISE_MANAGED_EXCEPTION(___m_Type_3Exception, NULL);
}
// Conversion method for clean up from marshalling of: UnityEngine.AssetBundleRequest
IL2CPP_EXTERN_C void AssetBundleRequest_tED9F5504E75ED1BCFF8DA9B51F5C7356617E6621_marshal_pinvoke_cleanup(AssetBundleRequest_tED9F5504E75ED1BCFF8DA9B51F5C7356617E6621_marshaled_pinvoke& marshaled)
{
}
// Conversion methods for marshalling of: UnityEngine.AssetBundleRequest
IL2CPP_EXTERN_C void AssetBundleRequest_tED9F5504E75ED1BCFF8DA9B51F5C7356617E6621_marshal_com(const AssetBundleRequest_tED9F5504E75ED1BCFF8DA9B51F5C7356617E6621& unmarshaled, AssetBundleRequest_tED9F5504E75ED1BCFF8DA9B51F5C7356617E6621_marshaled_com& marshaled)
{
	Exception_t* ___m_Type_3Exception = il2cpp_codegen_get_marshal_directive_exception("Cannot marshal field 'm_Type' of type 'AssetBundleRequest': Reference type field marshaling is not supported.");
	IL2CPP_RAISE_MANAGED_EXCEPTION(___m_Type_3Exception, NULL);
}
IL2CPP_EXTERN_C void AssetBundleRequest_tED9F5504E75ED1BCFF8DA9B51F5C7356617E6621_marshal_com_back(const AssetBundleRequest_tED9F5504E75ED1BCFF8DA9B51F5C7356617E6621_marshaled_com& marshaled, AssetBundleRequest_tED9F5504E75ED1BCFF8DA9B51F5C7356617E6621& unmarshaled)
{
	Exception_t* ___m_Type_3Exception = il2cpp_codegen_get_marshal_directive_exception("Cannot marshal field 'm_Type' of type 'AssetBundleRequest': Reference type field marshaling is not supported.");
	IL2CPP_RAISE_MANAGED_EXCEPTION(___m_Type_3Exception, NULL);
}
// Conversion method for clean up from marshalling of: UnityEngine.AssetBundleRequest
IL2CPP_EXTERN_C void AssetBundleRequest_tED9F5504E75ED1BCFF8DA9B51F5C7356617E6621_marshal_com_cleanup(AssetBundleRequest_tED9F5504E75ED1BCFF8DA9B51F5C7356617E6621_marshaled_com& marshaled)
{
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// Conversion methods for marshalling of: UnityEngine.AssetBundleCreateRequest
IL2CPP_EXTERN_C void AssetBundleCreateRequest_t73B8714B9459A01540E091C3770A408E67188CF6_marshal_pinvoke(const AssetBundleCreateRequest_t73B8714B9459A01540E091C3770A408E67188CF6& unmarshaled, AssetBundleCreateRequest_t73B8714B9459A01540E091C3770A408E67188CF6_marshaled_pinvoke& marshaled)
{
	marshaled.___m_Ptr_0 = unmarshaled.___m_Ptr_0;
	marshaled.___m_completeCallback_1 = il2cpp_codegen_marshal_delegate(reinterpret_cast<MulticastDelegate_t*>(unmarshaled.___m_completeCallback_1));
}
IL2CPP_EXTERN_C void AssetBundleCreateRequest_t73B8714B9459A01540E091C3770A408E67188CF6_marshal_pinvoke_back(const AssetBundleCreateRequest_t73B8714B9459A01540E091C3770A408E67188CF6_marshaled_pinvoke& marshaled, AssetBundleCreateRequest_t73B8714B9459A01540E091C3770A408E67188CF6& unmarshaled)
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Action_1_tE8693FF0E67CDBA52BAFB211BFF1844D076ABAFB_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	intptr_t unmarshaledm_Ptr_temp_0;
	memset((&unmarshaledm_Ptr_temp_0), 0, sizeof(unmarshaledm_Ptr_temp_0));
	unmarshaledm_Ptr_temp_0 = marshaled.___m_Ptr_0;
	unmarshaled.___m_Ptr_0 = unmarshaledm_Ptr_temp_0;
	unmarshaled.___m_completeCallback_1 = il2cpp_codegen_marshal_function_ptr_to_delegate<Action_1_tE8693FF0E67CDBA52BAFB211BFF1844D076ABAFB>(marshaled.___m_completeCallback_1, Action_1_tE8693FF0E67CDBA52BAFB211BFF1844D076ABAFB_il2cpp_TypeInfo_var);
	Il2CppCodeGenWriteBarrier((void**)(&unmarshaled.___m_completeCallback_1), (void*)il2cpp_codegen_marshal_function_ptr_to_delegate<Action_1_tE8693FF0E67CDBA52BAFB211BFF1844D076ABAFB>(marshaled.___m_completeCallback_1, Action_1_tE8693FF0E67CDBA52BAFB211BFF1844D076ABAFB_il2cpp_TypeInfo_var));
}
// Conversion method for clean up from marshalling of: UnityEngine.AssetBundleCreateRequest
IL2CPP_EXTERN_C void AssetBundleCreateRequest_t73B8714B9459A01540E091C3770A408E67188CF6_marshal_pinvoke_cleanup(AssetBundleCreateRequest_t73B8714B9459A01540E091C3770A408E67188CF6_marshaled_pinvoke& marshaled)
{
}
// Conversion methods for marshalling of: UnityEngine.AssetBundleCreateRequest
IL2CPP_EXTERN_C void AssetBundleCreateRequest_t73B8714B9459A01540E091C3770A408E67188CF6_marshal_com(const AssetBundleCreateRequest_t73B8714B9459A01540E091C3770A408E67188CF6& unmarshaled, AssetBundleCreateRequest_t73B8714B9459A01540E091C3770A408E67188CF6_marshaled_com& marshaled)
{
	marshaled.___m_Ptr_0 = unmarshaled.___m_Ptr_0;
	marshaled.___m_completeCallback_1 = il2cpp_codegen_marshal_delegate(reinterpret_cast<MulticastDelegate_t*>(unmarshaled.___m_completeCallback_1));
}
IL2CPP_EXTERN_C void AssetBundleCreateRequest_t73B8714B9459A01540E091C3770A408E67188CF6_marshal_com_back(const AssetBundleCreateRequest_t73B8714B9459A01540E091C3770A408E67188CF6_marshaled_com& marshaled, AssetBundleCreateRequest_t73B8714B9459A01540E091C3770A408E67188CF6& unmarshaled)
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Action_1_tE8693FF0E67CDBA52BAFB211BFF1844D076ABAFB_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	intptr_t unmarshaledm_Ptr_temp_0;
	memset((&unmarshaledm_Ptr_temp_0), 0, sizeof(unmarshaledm_Ptr_temp_0));
	unmarshaledm_Ptr_temp_0 = marshaled.___m_Ptr_0;
	unmarshaled.___m_Ptr_0 = unmarshaledm_Ptr_temp_0;
	unmarshaled.___m_completeCallback_1 = il2cpp_codegen_marshal_function_ptr_to_delegate<Action_1_tE8693FF0E67CDBA52BAFB211BFF1844D076ABAFB>(marshaled.___m_completeCallback_1, Action_1_tE8693FF0E67CDBA52BAFB211BFF1844D076ABAFB_il2cpp_TypeInfo_var);
	Il2CppCodeGenWriteBarrier((void**)(&unmarshaled.___m_completeCallback_1), (void*)il2cpp_codegen_marshal_function_ptr_to_delegate<Action_1_tE8693FF0E67CDBA52BAFB211BFF1844D076ABAFB>(marshaled.___m_completeCallback_1, Action_1_tE8693FF0E67CDBA52BAFB211BFF1844D076ABAFB_il2cpp_TypeInfo_var));
}
// Conversion method for clean up from marshalling of: UnityEngine.AssetBundleCreateRequest
IL2CPP_EXTERN_C void AssetBundleCreateRequest_t73B8714B9459A01540E091C3770A408E67188CF6_marshal_com_cleanup(AssetBundleCreateRequest_t73B8714B9459A01540E091C3770A408E67188CF6_marshaled_com& marshaled)
{
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
