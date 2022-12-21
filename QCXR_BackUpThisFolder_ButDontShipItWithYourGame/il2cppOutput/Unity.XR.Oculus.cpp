#include "pch-cpp.hpp"

#ifndef _MSC_VER
# include <alloca.h>
#else
# include <malloc.h>
#endif


#include <limits>


template <typename R>
struct VirtualFuncInvoker0
{
	typedef R (*Func)(void*, const RuntimeMethod*);

	static inline R Invoke (Il2CppMethodSlot slot, RuntimeObject* obj)
	{
		const VirtualInvokeData& invokeData = il2cpp_codegen_get_virtual_invoke_data(slot, obj);
		return ((Func)invokeData.methodPtr)(obj, invokeData.method);
	}
};
template <typename R, typename T1, typename T2, typename T3>
struct VirtualFuncInvoker3
{
	typedef R (*Func)(void*, T1, T2, T3, const RuntimeMethod*);

	static inline R Invoke (Il2CppMethodSlot slot, RuntimeObject* obj, T1 p1, T2 p2, T3 p3)
	{
		const VirtualInvokeData& invokeData = il2cpp_codegen_get_virtual_invoke_data(slot, obj);
		return ((Func)invokeData.methodPtr)(obj, p1, p2, p3, invokeData.method);
	}
};
template <typename R>
struct GenericVirtualFuncInvoker0
{
	typedef R (*Func)(void*, const RuntimeMethod*);

	static inline R Invoke (const RuntimeMethod* method, RuntimeObject* obj)
	{
		VirtualInvokeData invokeData;
		il2cpp_codegen_get_generic_virtual_invoke_data(method, obj, &invokeData);
		return ((Func)invokeData.methodPtr)(obj, invokeData.method);
	}
};

// System.Action`1<System.Boolean>
struct Action_1_t10DCB0C07D0D3C565CEACADC80D1152B35A45F6C;
// System.Action`1<System.Object>
struct Action_1_t6F9EB113EB3F16226AEF811A2744F4111C116C87;
// System.Action`1<System.String>
struct Action_1_t3CB5D1A819C3ED3F99E9E39F890F18633253949A;
// System.Action`1<UnityEngine.XR.XRInputSubsystem>
struct Action_1_tC867D66471C553CFFF8707FF2C59FB7AAB03086A;
// System.Collections.Generic.Dictionary`2<System.Int32,System.Text.Encoding>
struct Dictionary_2_t87EDE08B2E48F793A22DE50D6B3CC2E7EBB2DB54;
// System.Collections.Generic.Dictionary`2<System.Type,UnityEngine.ISubsystem>
struct Dictionary_2_tCDC65F572855EBDD1C12CEE33EBEBE0131F60C9C;
// UnityEngine.IntegratedSubsystem`1<System.Object>
struct IntegratedSubsystem_1_t6CAFC4ADB928A1CB6A1BAA66C12250FB6C841842;
// UnityEngine.IntegratedSubsystem`1<UnityEngine.XR.XRDisplaySubsystemDescriptor>
struct IntegratedSubsystem_1_t8312865F01EEA1EDE4B24A973E47ADD526616848;
// System.Collections.Generic.List`1<System.Object>
struct List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D;
// System.Collections.Generic.List`1<System.UInt64>
struct List_1_tB88E7361EE76DFB3EBB7FCD60CC59ACC3E48C284;
// System.Collections.Generic.List`1<UnityEngine.XR.XRDisplaySubsystem>
struct List_1_tA7666C6690CE2AEE97571615AD3AFCE2BB020597;
// System.Collections.Generic.List`1<UnityEngine.XR.XRDisplaySubsystemDescriptor>
struct List_1_tC3F021D09EFA4F3516555517B5E0D39308C9C1B4;
// System.Collections.Generic.List`1<UnityEngine.XR.XRInputSubsystemDescriptor>
struct List_1_tE3AE94237CE649B47E1D52E1A3120E772255FF87;
// System.Int32[][]
struct Int32U5BU5DU5BU5D_t179D865D5B30EFCBC50F82C9774329C15943466E;
// System.Byte[]
struct ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031;
// System.Delegate[]
struct DelegateU5BU5D_tC5AB7E8F745616680F337909D3A8E6C722CDF771;
// System.Text.RegularExpressions.Group[]
struct GroupU5BU5D_t9924453EAB39E5BC350475A536C5C7093F9A04A9;
// System.Int32[]
struct Int32U5BU5D_t19C97395396A72ECAF310612F0760F165060314C;
// System.IntPtr[]
struct IntPtrU5BU5D_tFD177F8C806A6921AD7150264CCC62FA00CAD832;
// System.Single[]
struct SingleU5BU5D_t89DEFE97BCEDB5857010E79ECE0F52CF6E93B87C;
// System.Diagnostics.StackTrace[]
struct StackTraceU5BU5D_t32FBCB20930EAF5BAE3F450FF75228E5450DA0DF;
// UnityEngine.XR.XRDisplaySubsystem[]
struct XRDisplaySubsystemU5BU5D_t741124D80DCCCF62F2AF47431354B7387672F264;
// UnityEngine.XR.XRDisplaySubsystemDescriptor[]
struct XRDisplaySubsystemDescriptorU5BU5D_t80B6AEA854B63F06CAE27E51662BBC06D752BFF0;
// UnityEngine.XR.XRInputSubsystemDescriptor[]
struct XRInputSubsystemDescriptorU5BU5D_tC53A4274C4DC364C07C751B6A9CF029515898A75;
// System.Action
struct Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07;
// UnityEngine.AndroidJavaClass
struct AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03;
// System.Text.RegularExpressions.Capture
struct Capture_tE11B735186DAFEE5F7A3BF5A739E9CCCE99DC24A;
// System.Text.RegularExpressions.CaptureCollection
struct CaptureCollection_t38405272BD6A6DA77CD51487FD39624C6E95CC93;
// System.Globalization.CodePageDataItem
struct CodePageDataItem_t52460FA30AE37F4F26ACB81055E58002262F19F2;
// System.Text.DecoderFallback
struct DecoderFallback_t7324102215E4ED41EC065C02EB501CB0BC23CD90;
// System.Delegate
struct Delegate_t;
// System.DelegateData
struct DelegateData_t9B286B493293CD2D23A5B2B5EF0E5B1324C2B77E;
// System.Text.EncoderFallback
struct EncoderFallback_tD2C40CE114AA9D8E1F7196608B2D088548015293;
// System.Text.Encoding
struct Encoding_t65CDEF28CF20A7B8C92E85A4E808920C2465F095;
// UnityEngine.GlobalJavaObjectRef
struct GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8;
// System.Text.RegularExpressions.Group
struct Group_t26371E9136D6F43782C487B63C67C5FC4F472881;
// System.Text.RegularExpressions.GroupCollection
struct GroupCollection_tFFA1789730DD9EA122FBE77DC03BFEDCC3F2945E;
// System.Collections.Hashtable
struct Hashtable_tEFC3B6496E6747787D8BB761B51F2AE3A8CFFE2D;
// System.Collections.IDictionary
struct IDictionary_t6D03155AF1FA9083817AA5B6AD7DEEACC26AB220;
// UnityEngine.ISubsystemDescriptor
struct ISubsystemDescriptor_tEF29944D579CC7D70F52CB883150735991D54E6E;
// Unity.XR.Oculus.InputFocus
struct InputFocus_t76018DDD56BB288561D354B825D5929965DC38FE;
// UnityEngine.IntegratedSubsystem
struct IntegratedSubsystem_t990160A89854D87C0836DC589B720231C02D4CE3;
// UnityEngine.IntegratedSubsystemDescriptor
struct IntegratedSubsystemDescriptor_t9232963B842E01748A8E032928DC8E35DF00C10D;
// System.Text.RegularExpressions.Match
struct Match_tFBEBCF225BD8EA17BCE6CE3FE5C1BD8E3074105F;
// System.Reflection.MethodInfo
struct MethodInfo_t;
// UnityEngine.Object
struct Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C;
// Unity.XR.Oculus.OculusLoader
struct OculusLoader_tA386B9AA0786D042EA272EDF385F96C0AD1A56BB;
// Unity.XR.Oculus.OculusSettings
struct OculusSettings_t0584FB71432B697479FD0BFC5B68C195F17CD321;
// UnityEngine.Android.PermissionCallbacks
struct PermissionCallbacks_t6C5002298E6649261FCB52C244695459E5C9890F;
// System.Text.RegularExpressions.Regex
struct Regex_tE773142C2BE45C5D362B0F815AFF831707A51772;
// System.Runtime.Serialization.SafeSerializationManager
struct SafeSerializationManager_tCBB85B95DFD1634237140CD892E82D06ECB3F5E6;
// UnityEngine.ScriptableObject
struct ScriptableObject_tB3BFDB921A1B1795B38A5417D3B97A89A140436A;
// Unity.XR.Oculus.Stats
struct Stats_t532BDF72F0CFA5121B447784EC03851E18A51BFB;
// System.String
struct String_t;
// UnityEngine.Texture2D
struct Texture2D_tE6505BC111DD8A424A9DBE8E05D7D09E11FFFCF4;
// UnityEngine.Events.UnityAction
struct UnityAction_t11A1F3B953B365C072A5DCC32677EE1796A962A7;
// System.Void
struct Void_t4861ACF8F4594C3437BB48B6E56783494B843915;
// UnityEngine.XR.XRDisplaySubsystem
struct XRDisplaySubsystem_t4B00B0BF1894A039ACFA8DDC2C2EB9301118C1F1;
// UnityEngine.XR.XRDisplaySubsystemDescriptor
struct XRDisplaySubsystemDescriptor_t72DD88EE9094488AE723A495F48884BA4EA8311A;
// UnityEngine.XR.XRInputSubsystem
struct XRInputSubsystem_tFECE6683FCAEBF05BAD05E5D612690095D8BAD34;
// UnityEngine.XR.Management.XRLoaderHelper
struct XRLoaderHelper_tE96E7AE003148D5319D20BAD7E02654367E41DCC;

IL2CPP_EXTERN_C RuntimeClass* Action_1_t3CB5D1A819C3ED3F99E9E39F890F18633253949A_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Development_t106B6EDC97423218186995B6FDF3EF5E6EE40C40_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* DllNotFoundException_t8CAE636A394C482C9FCF38FB7B7929506319D534_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* InputFocus_t76018DDD56BB288561D354B825D5929965DC38FE_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* IntPtr_t_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* List_1_tA7666C6690CE2AEE97571615AD3AFCE2BB020597_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* List_1_tC3F021D09EFA4F3516555517B5E0D39308C9C1B4_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* List_1_tE3AE94237CE649B47E1D52E1A3120E772255FF87_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Marshal_tD976A56A90263C3CE2B780D4B1CADADE2E70B4A7_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* OculusLoader_tA386B9AA0786D042EA272EDF385F96C0AD1A56BB_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* OculusSettings_t0584FB71432B697479FD0BFC5B68C195F17CD321_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* OculusUsages_t26394A1703082235CAC869B16DF3A491A7965196_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Performance_tB1393E2318BEDFD1E01AF8B40C104A3C938D5777_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* PermissionCallbacks_t6C5002298E6649261FCB52C244695459E5C9890F_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Regex_tE773142C2BE45C5D362B0F815AFF831707A51772_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* RuntimeObject_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* SingleU5BU5D_t89DEFE97BCEDB5857010E79ECE0F52CF6E93B87C_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Stats_t532BDF72F0CFA5121B447784EC03851E18A51BFB_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* String_t_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* SubsystemManager_t9A7261E4D0B53B996F04B8707D8E1C33AB65E824_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* UnityAction_t11A1F3B953B365C072A5DCC32677EE1796A962A7_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* XRDisplaySubsystem_t4B00B0BF1894A039ACFA8DDC2C2EB9301118C1F1_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C String_t* _stringLiteral039FC8798456705B4F372FB22E7B8A75FE2E6D6D;
IL2CPP_EXTERN_C String_t* _stringLiteral066D7D93F8175DDAAA3D6E4337D52AB827615B03;
IL2CPP_EXTERN_C String_t* _stringLiteral0A04B971B03DA607CE6C455184037B660CA89F78;
IL2CPP_EXTERN_C String_t* _stringLiteral0E33CA6894EABEA68F4151858D5322F8246508A3;
IL2CPP_EXTERN_C String_t* _stringLiteral18731F484474DDB7AD0F0E7C15988C0A794DEC4D;
IL2CPP_EXTERN_C String_t* _stringLiteral197E4C0FFF522D6C8FD3D9B10A88C7BA37107AAD;
IL2CPP_EXTERN_C String_t* _stringLiteral19EEE9FEA675F3AD8283953350F19D8A2E2934A0;
IL2CPP_EXTERN_C String_t* _stringLiteral2089C15C4332D83D0388E9B6CF7057950BB5CD54;
IL2CPP_EXTERN_C String_t* _stringLiteral271AF6878EC3872B415EA8A73A1433E4B604ACDF;
IL2CPP_EXTERN_C String_t* _stringLiteral28DC90CC5E864B9BEFE7447A1CCD759D1F2D3991;
IL2CPP_EXTERN_C String_t* _stringLiteral3665CEE66FFACBAAC4FEA9EBCFB744AC1F3A9A57;
IL2CPP_EXTERN_C String_t* _stringLiteral4DADF60B90978099A286AA09DF75E789888C9904;
IL2CPP_EXTERN_C String_t* _stringLiteral50B8349DC34E14AB475F3453803BCDBD9F3B0F85;
IL2CPP_EXTERN_C String_t* _stringLiteral549D4E1BD7FFA7F485E084D961369B26386BA2A5;
IL2CPP_EXTERN_C String_t* _stringLiteral60EABBC07A25977B87CF58F7CB0D8D536D013DBA;
IL2CPP_EXTERN_C String_t* _stringLiteral650C77761A0B8B1C5C9AB2BB0D61E4767DDDB6E8;
IL2CPP_EXTERN_C String_t* _stringLiteral666C1D75F394950EFFDBE5C128752A9E0CBD1DEA;
IL2CPP_EXTERN_C String_t* _stringLiteral6B78D11BAB6670A660082318236CEB73140FC391;
IL2CPP_EXTERN_C String_t* _stringLiteral6C095088ADD88C25A47E7BBE6A81D13C798F9E75;
IL2CPP_EXTERN_C String_t* _stringLiteral6E837F416B0AD538A7C4B0B672467CAD351051C1;
IL2CPP_EXTERN_C String_t* _stringLiteral71D87D03368ADC0E5018E85E30CA4984F5FF2AA8;
IL2CPP_EXTERN_C String_t* _stringLiteral753B6D37AEAF368AA772306EFBD496750FDE357A;
IL2CPP_EXTERN_C String_t* _stringLiteral7B4329AE6518370E7FA79EABB817A9A8F33E72A1;
IL2CPP_EXTERN_C String_t* _stringLiteral7CF7D253C5E081CD8124B453E189315E3AB51312;
IL2CPP_EXTERN_C String_t* _stringLiteral82C791C1966A9B7EFCEB102734ECB5B1DB8AF742;
IL2CPP_EXTERN_C String_t* _stringLiteral8695EE74D804B608F4CB465B35B41E02389AD412;
IL2CPP_EXTERN_C String_t* _stringLiteral86CB83E014FB5A27545E6442E0E4C0E783301DED;
IL2CPP_EXTERN_C String_t* _stringLiteral8A017E46CE09C02B042A499A98229FB4CB75E992;
IL2CPP_EXTERN_C String_t* _stringLiteral8B2341C27300FE7CC95F015A82D27378FA3E44C2;
IL2CPP_EXTERN_C String_t* _stringLiteral8D4C1624EBCE886FC4F782A22F67C15E64EF0BE1;
IL2CPP_EXTERN_C String_t* _stringLiteral9348643C476E6565E37FC0900AC244BD6F18AC32;
IL2CPP_EXTERN_C String_t* _stringLiteral9CA2736EB42A97B73E816FF8369ACA2005FA5AA2;
IL2CPP_EXTERN_C String_t* _stringLiteral9DD0635215B9CAFE0FAF9AA23554729A2A8BD63A;
IL2CPP_EXTERN_C String_t* _stringLiteralA1B23EF8DFFD7E2D6521CBDDA3630AC111AE5F69;
IL2CPP_EXTERN_C String_t* _stringLiteralB0A88A6DB46B5BCFC8ED5871C81A6C91204F1E45;
IL2CPP_EXTERN_C String_t* _stringLiteralB81FB9482B9D82D5EF7151BE5724BB998E6C5F83;
IL2CPP_EXTERN_C String_t* _stringLiteralCD42F30283C4CE60465C4010C800AD9704733102;
IL2CPP_EXTERN_C String_t* _stringLiteralCDF3E124A1507F39224D73C8EFA9828D8BE3846B;
IL2CPP_EXTERN_C String_t* _stringLiteralD3F9023582F96AC5F3DEB69BCAC72DB7F59028A8;
IL2CPP_EXTERN_C String_t* _stringLiteralDA39A3EE5E6B4B0D3255BFEF95601890AFD80709;
IL2CPP_EXTERN_C String_t* _stringLiteralDC74641CA8B52702019111B91E29821576E700BB;
IL2CPP_EXTERN_C String_t* _stringLiteralDC7C9CB0A5D4E7E14BC551B5F290A52563997520;
IL2CPP_EXTERN_C String_t* _stringLiteralDCD1BF12664AC38299958513D10BAA016D22904B;
IL2CPP_EXTERN_C String_t* _stringLiteralE0C17AE8C1199F6CD8F16D39E1B77CC319F01B26;
IL2CPP_EXTERN_C String_t* _stringLiteralEB59BE99DEEFC290177DB43CF6B387636E1E0904;
IL2CPP_EXTERN_C String_t* _stringLiteralF1CA3E581C0778F5F9FE0D32597E39A13E302CAB;
IL2CPP_EXTERN_C String_t* _stringLiteralF3C6C902DBF80139640F6554F0C3392016A8ADF7;
IL2CPP_EXTERN_C String_t* _stringLiteralF752B27A3F46D6A1A7B84CA9CC6E730C9B472E9A;
IL2CPP_EXTERN_C String_t* _stringLiteralFFBC2C924E5DC9232A3732A59008339C044352A3;
IL2CPP_EXTERN_C const RuntimeMethod* Array_IndexOf_TisByte_t94D9231AC217BE4D2E004C4CD32DF6D099EA41A3_m3E47AE0BD5292CA28182DF90A3A3B19D2DBE1B6F_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* Enumerator_Dispose_m25A1E45E653D7F73DF65C041A66C0A6E01F2964D_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* Enumerator_MoveNext_m9A1452618AC875C7A20A5917509A7B90E76E6A6A_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* Enumerator_get_Current_mF9383BAD37E56B1D5BCDBFF0C3ADA58BB6E67A04_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* InputFeatureUsage_1__ctor_mEB36F8937385A1065CD9F48AE2DAD9EAE49EFCE7_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* IntegratedSubsystem_1_get_SubsystemDescriptor_mF302CEB5B3EFD7EFCB9FC2B54CF739543AB2CFF3_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* List_1_GetEnumerator_mA450E85CB8D7D5CB81FAAF9D11A1D4945B942423_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* List_1__ctor_m2211D9F948E2002179E205222318FE448863F2CD_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* List_1__ctor_m3E15C72C5BBB246B014CD4F0B141BD78A648B773_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* List_1__ctor_mBE7647ECE0B8ABB952EDC379472F9E541D41D6DF_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* OculusLoader_PermissionGrantedCallback_mE09E412A87E4A838FA3C8E3DAC98E0AB4DE2922C_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* RegisterUpdateCallback_Update_m782D01D846D30720A9212BC883A3602D5B5B5958_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* SubsystemManager_GetInstances_TisXRDisplaySubsystem_t4B00B0BF1894A039ACFA8DDC2C2EB9301118C1F1_mCDFAF63EF2A2778CA3677E75360BC7961FCB3370_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* XRLoaderHelper_CreateSubsystem_TisXRDisplaySubsystemDescriptor_t72DD88EE9094488AE723A495F48884BA4EA8311A_TisXRDisplaySubsystem_t4B00B0BF1894A039ACFA8DDC2C2EB9301118C1F1_m47BB00ACEADFC3AF821DC1EE31F79CF6EEB4681A_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* XRLoaderHelper_CreateSubsystem_TisXRInputSubsystemDescriptor_t42088DD6542C0BDD27C2951B911E4F69DD1F917D_TisXRInputSubsystem_tFECE6683FCAEBF05BAD05E5D612690095D8BAD34_mA9FE0AE2F98657CD075CD191BAB94910F963C08C_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* XRLoaderHelper_DestroySubsystem_TisXRDisplaySubsystem_t4B00B0BF1894A039ACFA8DDC2C2EB9301118C1F1_m8E2572BB5610CCE3B1DBA87453CFE09BB4B2B606_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* XRLoaderHelper_DestroySubsystem_TisXRInputSubsystem_tFECE6683FCAEBF05BAD05E5D612690095D8BAD34_m22B2454EB0F4E32155EEE6022768B9781DB0085F_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* XRLoaderHelper_GetLoadedSubsystem_TisXRDisplaySubsystem_t4B00B0BF1894A039ACFA8DDC2C2EB9301118C1F1_m9FC253637CE85B86CE3DFA51346D7E4D49913E7B_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* XRLoaderHelper_GetLoadedSubsystem_TisXRInputSubsystem_tFECE6683FCAEBF05BAD05E5D612690095D8BAD34_mFFFE07D8A1F3526DB3003FFAE9681221AA4A876A_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* XRLoaderHelper_StartSubsystem_TisXRDisplaySubsystem_t4B00B0BF1894A039ACFA8DDC2C2EB9301118C1F1_mC1643794A5D3CC32BF1EE9C976CE5F23A6BB8962_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* XRLoaderHelper_StartSubsystem_TisXRInputSubsystem_tFECE6683FCAEBF05BAD05E5D612690095D8BAD34_m08AC127201FE89396BD81BEA52D40790BC0CA3FA_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* XRLoaderHelper_StopSubsystem_TisXRDisplaySubsystem_t4B00B0BF1894A039ACFA8DDC2C2EB9301118C1F1_m00A3A82597D484DE2EBB03EA9A2430AFDE44DE24_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* XRLoaderHelper_StopSubsystem_TisXRInputSubsystem_tFECE6683FCAEBF05BAD05E5D612690095D8BAD34_mE6F3E85E0C64666EE9A517CD7CF3669FB7BAC750_RuntimeMethod_var;
struct Delegate_t_marshaled_com;
struct Delegate_t_marshaled_pinvoke;
struct Exception_t_marshaled_com;
struct Exception_t_marshaled_pinvoke;

struct ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031;
struct SingleU5BU5D_t89DEFE97BCEDB5857010E79ECE0F52CF6E93B87C;

IL2CPP_EXTERN_C_BEGIN
IL2CPP_EXTERN_C_END

#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif

// <Module>
struct U3CModuleU3E_t58034802D4CFEFF188A9FAB3F39C67CD90CD8FD4 
{
};

// System.Collections.Generic.List`1<UnityEngine.XR.XRDisplaySubsystem>
struct List_1_tA7666C6690CE2AEE97571615AD3AFCE2BB020597  : public RuntimeObject
{
	// T[] System.Collections.Generic.List`1::_items
	XRDisplaySubsystemU5BU5D_t741124D80DCCCF62F2AF47431354B7387672F264* ____items_1;
	// System.Int32 System.Collections.Generic.List`1::_size
	int32_t ____size_2;
	// System.Int32 System.Collections.Generic.List`1::_version
	int32_t ____version_3;
	// System.Object System.Collections.Generic.List`1::_syncRoot
	RuntimeObject* ____syncRoot_4;
};

// System.Collections.Generic.List`1<UnityEngine.XR.XRDisplaySubsystemDescriptor>
struct List_1_tC3F021D09EFA4F3516555517B5E0D39308C9C1B4  : public RuntimeObject
{
	// T[] System.Collections.Generic.List`1::_items
	XRDisplaySubsystemDescriptorU5BU5D_t80B6AEA854B63F06CAE27E51662BBC06D752BFF0* ____items_1;
	// System.Int32 System.Collections.Generic.List`1::_size
	int32_t ____size_2;
	// System.Int32 System.Collections.Generic.List`1::_version
	int32_t ____version_3;
	// System.Object System.Collections.Generic.List`1::_syncRoot
	RuntimeObject* ____syncRoot_4;
};

// System.Collections.Generic.List`1<UnityEngine.XR.XRInputSubsystemDescriptor>
struct List_1_tE3AE94237CE649B47E1D52E1A3120E772255FF87  : public RuntimeObject
{
	// T[] System.Collections.Generic.List`1::_items
	XRInputSubsystemDescriptorU5BU5D_tC53A4274C4DC364C07C751B6A9CF029515898A75* ____items_1;
	// System.Int32 System.Collections.Generic.List`1::_size
	int32_t ____size_2;
	// System.Int32 System.Collections.Generic.List`1::_version
	int32_t ____version_3;
	// System.Object System.Collections.Generic.List`1::_syncRoot
	RuntimeObject* ____syncRoot_4;
};

// Unity.XR.Oculus.Boundary
struct Boundary_tC6E78E14EE1EB255CF7CA00C7138EDB83ECF1DAA  : public RuntimeObject
{
};

// System.Text.RegularExpressions.Capture
struct Capture_tE11B735186DAFEE5F7A3BF5A739E9CCCE99DC24A  : public RuntimeObject
{
	// System.Int32 System.Text.RegularExpressions.Capture::<Index>k__BackingField
	int32_t ___U3CIndexU3Ek__BackingField_0;
	// System.Int32 System.Text.RegularExpressions.Capture::<Length>k__BackingField
	int32_t ___U3CLengthU3Ek__BackingField_1;
	// System.String System.Text.RegularExpressions.Capture::<Text>k__BackingField
	String_t* ___U3CTextU3Ek__BackingField_2;
};

// Unity.XR.Oculus.Development
struct Development_t106B6EDC97423218186995B6FDF3EF5E6EE40C40  : public RuntimeObject
{
};

// System.Text.Encoding
struct Encoding_t65CDEF28CF20A7B8C92E85A4E808920C2465F095  : public RuntimeObject
{
	// System.Int32 System.Text.Encoding::m_codePage
	int32_t ___m_codePage_9;
	// System.Globalization.CodePageDataItem System.Text.Encoding::dataItem
	CodePageDataItem_t52460FA30AE37F4F26ACB81055E58002262F19F2* ___dataItem_10;
	// System.Boolean System.Text.Encoding::m_deserializedFromEverett
	bool ___m_deserializedFromEverett_11;
	// System.Boolean System.Text.Encoding::m_isReadOnly
	bool ___m_isReadOnly_12;
	// System.Text.EncoderFallback System.Text.Encoding::encoderFallback
	EncoderFallback_tD2C40CE114AA9D8E1F7196608B2D088548015293* ___encoderFallback_13;
	// System.Text.DecoderFallback System.Text.Encoding::decoderFallback
	DecoderFallback_t7324102215E4ED41EC065C02EB501CB0BC23CD90* ___decoderFallback_14;
};

// System.Text.RegularExpressions.GroupCollection
struct GroupCollection_tFFA1789730DD9EA122FBE77DC03BFEDCC3F2945E  : public RuntimeObject
{
	// System.Text.RegularExpressions.Match System.Text.RegularExpressions.GroupCollection::_match
	Match_tFBEBCF225BD8EA17BCE6CE3FE5C1BD8E3074105F* ____match_0;
	// System.Collections.Hashtable System.Text.RegularExpressions.GroupCollection::_captureMap
	Hashtable_tEFC3B6496E6747787D8BB761B51F2AE3A8CFFE2D* ____captureMap_1;
	// System.Text.RegularExpressions.Group[] System.Text.RegularExpressions.GroupCollection::_groups
	GroupU5BU5D_t9924453EAB39E5BC350475A536C5C7093F9A04A9* ____groups_2;
};

// Unity.XR.Oculus.InputFocus
struct InputFocus_t76018DDD56BB288561D354B825D5929965DC38FE  : public RuntimeObject
{
};

// Unity.XR.Oculus.NativeMethods
struct NativeMethods_tC6375926AE4EE34672287439E197A37B086427EE  : public RuntimeObject
{
};

// Unity.XR.Oculus.Performance
struct Performance_tB1393E2318BEDFD1E01AF8B40C104A3C938D5777  : public RuntimeObject
{
};

// Unity.XR.Oculus.RegisterUpdateCallback
struct RegisterUpdateCallback_t701583C89E70B1DCDEA6366E2DF87769BCE1C2CB  : public RuntimeObject
{
};

// Unity.XR.Oculus.Stats
struct Stats_t532BDF72F0CFA5121B447784EC03851E18A51BFB  : public RuntimeObject
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

// Unity.XR.Oculus.Utils
struct Utils_tD3A438448520D35DE3F6B3D5DDF2B0305141FCB2  : public RuntimeObject
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

// Unity.XR.Oculus.NativeMethods/Internal
struct Internal_tB52482155DCE77A052BD67C85767CBB3571BDE53  : public RuntimeObject
{
};

// Unity.XR.Oculus.Stats/AdaptivePerformance
struct AdaptivePerformance_t332F727125725046EAF94AE45817EBF7969EA93E  : public RuntimeObject
{
};

// Unity.XR.Oculus.Stats/AppMetrics
struct AppMetrics_t911B1EB58629B57319DEB999AFC9CF3DB889D03A  : public RuntimeObject
{
};

// Unity.XR.Oculus.Stats/PerfMetrics
struct PerfMetrics_tDDB5FFCB3FA9394077551671DC3994207428CE58  : public RuntimeObject
{
};

// System.Collections.Generic.List`1/Enumerator<System.Object>
struct Enumerator_t9473BAB568A27E2339D48C1F91319E0F6D244D7A 
{
	// System.Collections.Generic.List`1<T> System.Collections.Generic.List`1/Enumerator::_list
	List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D* ____list_0;
	// System.Int32 System.Collections.Generic.List`1/Enumerator::_index
	int32_t ____index_1;
	// System.Int32 System.Collections.Generic.List`1/Enumerator::_version
	int32_t ____version_2;
	// T System.Collections.Generic.List`1/Enumerator::_current
	RuntimeObject* ____current_3;
};

// System.Collections.Generic.List`1/Enumerator<UnityEngine.XR.XRDisplaySubsystem>
struct Enumerator_t7B44DEF95515943B67640F1A20853509F98BA143 
{
	// System.Collections.Generic.List`1<T> System.Collections.Generic.List`1/Enumerator::_list
	List_1_tA7666C6690CE2AEE97571615AD3AFCE2BB020597* ____list_0;
	// System.Int32 System.Collections.Generic.List`1/Enumerator::_index
	int32_t ____index_1;
	// System.Int32 System.Collections.Generic.List`1/Enumerator::_version
	int32_t ____version_2;
	// T System.Collections.Generic.List`1/Enumerator::_current
	XRDisplaySubsystem_t4B00B0BF1894A039ACFA8DDC2C2EB9301118C1F1* ____current_3;
};

// UnityEngine.XR.InputFeatureUsage`1<System.Boolean>
struct InputFeatureUsage_1_tE336B2F0B9AC721519BFA17A08D6353FD5221637 
{
	// System.String UnityEngine.XR.InputFeatureUsage`1::<name>k__BackingField
	String_t* ___U3CnameU3Ek__BackingField_0;
};
// Native definition for P/Invoke marshalling of UnityEngine.XR.InputFeatureUsage`1
#ifndef InputFeatureUsage_1_t66EDAF8AFFA2E9DDC0248C48B76ADAB8E2728858_marshaled_pinvoke_define
#define InputFeatureUsage_1_t66EDAF8AFFA2E9DDC0248C48B76ADAB8E2728858_marshaled_pinvoke_define
struct InputFeatureUsage_1_t66EDAF8AFFA2E9DDC0248C48B76ADAB8E2728858_marshaled_pinvoke
{
	char* ___U3CnameU3Ek__BackingField_0;
};
#endif
// Native definition for COM marshalling of UnityEngine.XR.InputFeatureUsage`1
#ifndef InputFeatureUsage_1_t66EDAF8AFFA2E9DDC0248C48B76ADAB8E2728858_marshaled_com_define
#define InputFeatureUsage_1_t66EDAF8AFFA2E9DDC0248C48B76ADAB8E2728858_marshaled_com_define
struct InputFeatureUsage_1_t66EDAF8AFFA2E9DDC0248C48B76ADAB8E2728858_marshaled_com
{
	Il2CppChar* ___U3CnameU3Ek__BackingField_0;
};
#endif

// System.Boolean
struct Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22 
{
	// System.Boolean System.Boolean::m_value
	bool ___m_value_0;
};

// System.Byte
struct Byte_t94D9231AC217BE4D2E004C4CD32DF6D099EA41A3 
{
	// System.Byte System.Byte::m_value
	uint8_t ___m_value_0;
};

// System.Text.RegularExpressions.Group
struct Group_t26371E9136D6F43782C487B63C67C5FC4F472881  : public Capture_tE11B735186DAFEE5F7A3BF5A739E9CCCE99DC24A
{
	// System.Int32[] System.Text.RegularExpressions.Group::_caps
	Int32U5BU5D_t19C97395396A72ECAF310612F0760F165060314C* ____caps_4;
	// System.Int32 System.Text.RegularExpressions.Group::_capcount
	int32_t ____capcount_5;
	// System.Text.RegularExpressions.CaptureCollection System.Text.RegularExpressions.Group::_capcoll
	CaptureCollection_t38405272BD6A6DA77CD51487FD39624C6E95CC93* ____capcoll_6;
	// System.String System.Text.RegularExpressions.Group::<Name>k__BackingField
	String_t* ___U3CNameU3Ek__BackingField_7;
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

// System.Single
struct Single_t4530F2FF86FCB0DC29F35385CA1BD21BE294761C 
{
	// System.Single System.Single::m_value
	float ___m_value_0;
};

// System.UInt16
struct UInt16_tF4C148C876015C212FD72652D0B6ED8CC247A455 
{
	// System.UInt16 System.UInt16::m_value
	uint16_t ___m_value_0;
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

// UnityEngine.Vector4
struct Vector4_t58B63D32F48C0DBF50DE2C60794C4676C80EDBE3 
{
	// System.Single UnityEngine.Vector4::x
	float ___x_1;
	// System.Single UnityEngine.Vector4::y
	float ___y_2;
	// System.Single UnityEngine.Vector4::z
	float ___z_3;
	// System.Single UnityEngine.Vector4::w
	float ___w_4;
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

// Unity.XR.Oculus.NativeMethods/UserDefinedSettings
struct UserDefinedSettings_tE823040D57E8C8C9D8A028F206230E1C7BAC8EE3 
{
	// System.UInt16 Unity.XR.Oculus.NativeMethods/UserDefinedSettings::sharedDepthBuffer
	uint16_t ___sharedDepthBuffer_0;
	// System.UInt16 Unity.XR.Oculus.NativeMethods/UserDefinedSettings::dashSupport
	uint16_t ___dashSupport_1;
	// System.UInt16 Unity.XR.Oculus.NativeMethods/UserDefinedSettings::stereoRenderingMode
	uint16_t ___stereoRenderingMode_2;
	// System.UInt16 Unity.XR.Oculus.NativeMethods/UserDefinedSettings::colorSpace
	uint16_t ___colorSpace_3;
	// System.UInt16 Unity.XR.Oculus.NativeMethods/UserDefinedSettings::lowOverheadMode
	uint16_t ___lowOverheadMode_4;
	// System.UInt16 Unity.XR.Oculus.NativeMethods/UserDefinedSettings::optimizeBufferDiscards
	uint16_t ___optimizeBufferDiscards_5;
	// System.UInt16 Unity.XR.Oculus.NativeMethods/UserDefinedSettings::phaseSync
	uint16_t ___phaseSync_6;
	// System.UInt16 Unity.XR.Oculus.NativeMethods/UserDefinedSettings::symmetricProjection
	uint16_t ___symmetricProjection_7;
	// System.UInt16 Unity.XR.Oculus.NativeMethods/UserDefinedSettings::subsampledLayout
	uint16_t ___subsampledLayout_8;
	// System.UInt16 Unity.XR.Oculus.NativeMethods/UserDefinedSettings::lateLatching
	uint16_t ___lateLatching_9;
	// System.UInt16 Unity.XR.Oculus.NativeMethods/UserDefinedSettings::lateLatchingDebug
	uint16_t ___lateLatchingDebug_10;
	// System.UInt16 Unity.XR.Oculus.NativeMethods/UserDefinedSettings::enableTrackingOriginStageMode
	uint16_t ___enableTrackingOriginStageMode_11;
	// System.UInt16 Unity.XR.Oculus.NativeMethods/UserDefinedSettings::spaceWarp
	uint16_t ___spaceWarp_12;
	// System.UInt16 Unity.XR.Oculus.NativeMethods/UserDefinedSettings::depthSubmission
	uint16_t ___depthSubmission_13;
	// System.UInt16 Unity.XR.Oculus.NativeMethods/UserDefinedSettings::foveatedRenderingMethod
	uint16_t ___foveatedRenderingMethod_14;
};

// UnityEngine.AndroidJavaProxy
struct AndroidJavaProxy_tE5521F9761F7B95444B9C39FB15FDFC23F80A78D  : public RuntimeObject
{
	// UnityEngine.AndroidJavaClass UnityEngine.AndroidJavaProxy::javaInterface
	AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03* ___javaInterface_0;
	// System.IntPtr UnityEngine.AndroidJavaProxy::proxyObject
	intptr_t ___proxyObject_1;
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

// System.Exception
struct Exception_t  : public RuntimeObject
{
	// System.String System.Exception::_className
	String_t* ____className_1;
	// System.String System.Exception::_message
	String_t* ____message_2;
	// System.Collections.IDictionary System.Exception::_data
	RuntimeObject* ____data_3;
	// System.Exception System.Exception::_innerException
	Exception_t* ____innerException_4;
	// System.String System.Exception::_helpURL
	String_t* ____helpURL_5;
	// System.Object System.Exception::_stackTrace
	RuntimeObject* ____stackTrace_6;
	// System.String System.Exception::_stackTraceString
	String_t* ____stackTraceString_7;
	// System.String System.Exception::_remoteStackTraceString
	String_t* ____remoteStackTraceString_8;
	// System.Int32 System.Exception::_remoteStackIndex
	int32_t ____remoteStackIndex_9;
	// System.Object System.Exception::_dynamicMethods
	RuntimeObject* ____dynamicMethods_10;
	// System.Int32 System.Exception::_HResult
	int32_t ____HResult_11;
	// System.String System.Exception::_source
	String_t* ____source_12;
	// System.Runtime.Serialization.SafeSerializationManager System.Exception::_safeSerializationManager
	SafeSerializationManager_tCBB85B95DFD1634237140CD892E82D06ECB3F5E6* ____safeSerializationManager_13;
	// System.Diagnostics.StackTrace[] System.Exception::captured_traces
	StackTraceU5BU5D_t32FBCB20930EAF5BAE3F450FF75228E5450DA0DF* ___captured_traces_14;
	// System.IntPtr[] System.Exception::native_trace_ips
	IntPtrU5BU5D_tFD177F8C806A6921AD7150264CCC62FA00CAD832* ___native_trace_ips_15;
	// System.Int32 System.Exception::caught_in_unmanaged
	int32_t ___caught_in_unmanaged_16;
};
// Native definition for P/Invoke marshalling of System.Exception
struct Exception_t_marshaled_pinvoke
{
	char* ____className_1;
	char* ____message_2;
	RuntimeObject* ____data_3;
	Exception_t_marshaled_pinvoke* ____innerException_4;
	char* ____helpURL_5;
	Il2CppIUnknown* ____stackTrace_6;
	char* ____stackTraceString_7;
	char* ____remoteStackTraceString_8;
	int32_t ____remoteStackIndex_9;
	Il2CppIUnknown* ____dynamicMethods_10;
	int32_t ____HResult_11;
	char* ____source_12;
	SafeSerializationManager_tCBB85B95DFD1634237140CD892E82D06ECB3F5E6* ____safeSerializationManager_13;
	StackTraceU5BU5D_t32FBCB20930EAF5BAE3F450FF75228E5450DA0DF* ___captured_traces_14;
	Il2CppSafeArray/*NONE*/* ___native_trace_ips_15;
	int32_t ___caught_in_unmanaged_16;
};
// Native definition for COM marshalling of System.Exception
struct Exception_t_marshaled_com
{
	Il2CppChar* ____className_1;
	Il2CppChar* ____message_2;
	RuntimeObject* ____data_3;
	Exception_t_marshaled_com* ____innerException_4;
	Il2CppChar* ____helpURL_5;
	Il2CppIUnknown* ____stackTrace_6;
	Il2CppChar* ____stackTraceString_7;
	Il2CppChar* ____remoteStackTraceString_8;
	int32_t ____remoteStackIndex_9;
	Il2CppIUnknown* ____dynamicMethods_10;
	int32_t ____HResult_11;
	Il2CppChar* ____source_12;
	SafeSerializationManager_tCBB85B95DFD1634237140CD892E82D06ECB3F5E6* ____safeSerializationManager_13;
	StackTraceU5BU5D_t32FBCB20930EAF5BAE3F450FF75228E5450DA0DF* ___captured_traces_14;
	Il2CppSafeArray/*NONE*/* ___native_trace_ips_15;
	int32_t ___caught_in_unmanaged_16;
};

// UnityEngine.IntegratedSubsystem
struct IntegratedSubsystem_t990160A89854D87C0836DC589B720231C02D4CE3  : public RuntimeObject
{
	// System.IntPtr UnityEngine.IntegratedSubsystem::m_Ptr
	intptr_t ___m_Ptr_0;
	// UnityEngine.ISubsystemDescriptor UnityEngine.IntegratedSubsystem::m_SubsystemDescriptor
	RuntimeObject* ___m_SubsystemDescriptor_1;
};
// Native definition for P/Invoke marshalling of UnityEngine.IntegratedSubsystem
struct IntegratedSubsystem_t990160A89854D87C0836DC589B720231C02D4CE3_marshaled_pinvoke
{
	intptr_t ___m_Ptr_0;
	RuntimeObject* ___m_SubsystemDescriptor_1;
};
// Native definition for COM marshalling of UnityEngine.IntegratedSubsystem
struct IntegratedSubsystem_t990160A89854D87C0836DC589B720231C02D4CE3_marshaled_com
{
	intptr_t ___m_Ptr_0;
	RuntimeObject* ___m_SubsystemDescriptor_1;
};

// UnityEngine.IntegratedSubsystemDescriptor
struct IntegratedSubsystemDescriptor_t9232963B842E01748A8E032928DC8E35DF00C10D  : public RuntimeObject
{
	// System.IntPtr UnityEngine.IntegratedSubsystemDescriptor::m_Ptr
	intptr_t ___m_Ptr_0;
};
// Native definition for P/Invoke marshalling of UnityEngine.IntegratedSubsystemDescriptor
struct IntegratedSubsystemDescriptor_t9232963B842E01748A8E032928DC8E35DF00C10D_marshaled_pinvoke
{
	intptr_t ___m_Ptr_0;
};
// Native definition for COM marshalling of UnityEngine.IntegratedSubsystemDescriptor
struct IntegratedSubsystemDescriptor_t9232963B842E01748A8E032928DC8E35DF00C10D_marshaled_com
{
	intptr_t ___m_Ptr_0;
};

// System.Text.RegularExpressions.Match
struct Match_tFBEBCF225BD8EA17BCE6CE3FE5C1BD8E3074105F  : public Group_t26371E9136D6F43782C487B63C67C5FC4F472881
{
	// System.Text.RegularExpressions.GroupCollection System.Text.RegularExpressions.Match::_groupcoll
	GroupCollection_tFFA1789730DD9EA122FBE77DC03BFEDCC3F2945E* ____groupcoll_8;
	// System.Text.RegularExpressions.Regex System.Text.RegularExpressions.Match::_regex
	Regex_tE773142C2BE45C5D362B0F815AFF831707A51772* ____regex_9;
	// System.Int32 System.Text.RegularExpressions.Match::_textbeg
	int32_t ____textbeg_10;
	// System.Int32 System.Text.RegularExpressions.Match::_textpos
	int32_t ____textpos_11;
	// System.Int32 System.Text.RegularExpressions.Match::_textend
	int32_t ____textend_12;
	// System.Int32 System.Text.RegularExpressions.Match::_textstart
	int32_t ____textstart_13;
	// System.Int32[][] System.Text.RegularExpressions.Match::_matches
	Int32U5BU5DU5BU5D_t179D865D5B30EFCBC50F82C9774329C15943466E* ____matches_14;
	// System.Int32[] System.Text.RegularExpressions.Match::_matchcount
	Int32U5BU5D_t19C97395396A72ECAF310612F0760F165060314C* ____matchcount_15;
	// System.Boolean System.Text.RegularExpressions.Match::_balancing
	bool ____balancing_16;
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

// Unity.XR.Oculus.OculusUsages
struct OculusUsages_t26394A1703082235CAC869B16DF3A491A7965196  : public RuntimeObject
{
};

// UnityEngine.IntegratedSubsystemDescriptor`1<UnityEngine.XR.XRDisplaySubsystem>
struct IntegratedSubsystemDescriptor_1_t7261AA0914165CB589AD41C4F9B463D44E333D7C  : public IntegratedSubsystemDescriptor_t9232963B842E01748A8E032928DC8E35DF00C10D
{
};
// Native definition for P/Invoke marshalling of UnityEngine.IntegratedSubsystemDescriptor`1
#ifndef IntegratedSubsystemDescriptor_1_tC541D17A8306FA1C3A608A1328A6DBFDA3264671_marshaled_pinvoke_define
#define IntegratedSubsystemDescriptor_1_tC541D17A8306FA1C3A608A1328A6DBFDA3264671_marshaled_pinvoke_define
struct IntegratedSubsystemDescriptor_1_tC541D17A8306FA1C3A608A1328A6DBFDA3264671_marshaled_pinvoke : public IntegratedSubsystemDescriptor_t9232963B842E01748A8E032928DC8E35DF00C10D_marshaled_pinvoke
{
};
#endif
// Native definition for COM marshalling of UnityEngine.IntegratedSubsystemDescriptor`1
#ifndef IntegratedSubsystemDescriptor_1_tC541D17A8306FA1C3A608A1328A6DBFDA3264671_marshaled_com_define
#define IntegratedSubsystemDescriptor_1_tC541D17A8306FA1C3A608A1328A6DBFDA3264671_marshaled_com_define
struct IntegratedSubsystemDescriptor_1_tC541D17A8306FA1C3A608A1328A6DBFDA3264671_marshaled_com : public IntegratedSubsystemDescriptor_t9232963B842E01748A8E032928DC8E35DF00C10D_marshaled_com
{
};
#endif

// UnityEngine.IntegratedSubsystem`1<UnityEngine.XR.XRDisplaySubsystemDescriptor>
struct IntegratedSubsystem_1_t8312865F01EEA1EDE4B24A973E47ADD526616848  : public IntegratedSubsystem_t990160A89854D87C0836DC589B720231C02D4CE3
{
};

// UnityEngine.IntegratedSubsystem`1<UnityEngine.XR.XRInputSubsystemDescriptor>
struct IntegratedSubsystem_1_tF93BC76362E85BDD215312162457BE510FC76D3B  : public IntegratedSubsystem_t990160A89854D87C0836DC589B720231C02D4CE3
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

// UnityEngine.Android.PermissionCallbacks
struct PermissionCallbacks_t6C5002298E6649261FCB52C244695459E5C9890F  : public AndroidJavaProxy_tE5521F9761F7B95444B9C39FB15FDFC23F80A78D
{
	// System.Action`1<System.String> UnityEngine.Android.PermissionCallbacks::PermissionGranted
	Action_1_t3CB5D1A819C3ED3F99E9E39F890F18633253949A* ___PermissionGranted_4;
	// System.Action`1<System.String> UnityEngine.Android.PermissionCallbacks::PermissionDenied
	Action_1_t3CB5D1A819C3ED3F99E9E39F890F18633253949A* ___PermissionDenied_5;
	// System.Action`1<System.String> UnityEngine.Android.PermissionCallbacks::PermissionDeniedAndDontAskAgain
	Action_1_t3CB5D1A819C3ED3F99E9E39F890F18633253949A* ___PermissionDeniedAndDontAskAgain_6;
};

// UnityEngine.ScriptableObject
struct ScriptableObject_tB3BFDB921A1B1795B38A5417D3B97A89A140436A  : public Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C
{
};
// Native definition for P/Invoke marshalling of UnityEngine.ScriptableObject
struct ScriptableObject_tB3BFDB921A1B1795B38A5417D3B97A89A140436A_marshaled_pinvoke : public Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_marshaled_pinvoke
{
};
// Native definition for COM marshalling of UnityEngine.ScriptableObject
struct ScriptableObject_tB3BFDB921A1B1795B38A5417D3B97A89A140436A_marshaled_com : public Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_marshaled_com
{
};

// System.SystemException
struct SystemException_tCC48D868298F4C0705279823E34B00F4FBDB7295  : public Exception_t
{
};

// System.Action`1<System.String>
struct Action_1_t3CB5D1A819C3ED3F99E9E39F890F18633253949A  : public MulticastDelegate_t
{
};

// System.Action
struct Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07  : public MulticastDelegate_t
{
};

// Unity.XR.Oculus.OculusSettings
struct OculusSettings_t0584FB71432B697479FD0BFC5B68C195F17CD321  : public ScriptableObject_tB3BFDB921A1B1795B38A5417D3B97A89A140436A
{
	// Unity.XR.Oculus.OculusSettings/StereoRenderingModeDesktop Unity.XR.Oculus.OculusSettings::m_StereoRenderingModeDesktop
	int32_t ___m_StereoRenderingModeDesktop_4;
	// Unity.XR.Oculus.OculusSettings/StereoRenderingModeAndroid Unity.XR.Oculus.OculusSettings::m_StereoRenderingModeAndroid
	int32_t ___m_StereoRenderingModeAndroid_5;
	// System.Boolean Unity.XR.Oculus.OculusSettings::SharedDepthBuffer
	bool ___SharedDepthBuffer_6;
	// System.Boolean Unity.XR.Oculus.OculusSettings::DepthSubmission
	bool ___DepthSubmission_7;
	// System.Boolean Unity.XR.Oculus.OculusSettings::DashSupport
	bool ___DashSupport_8;
	// System.Boolean Unity.XR.Oculus.OculusSettings::LowOverheadMode
	bool ___LowOverheadMode_9;
	// System.Boolean Unity.XR.Oculus.OculusSettings::OptimizeBufferDiscards
	bool ___OptimizeBufferDiscards_10;
	// System.Boolean Unity.XR.Oculus.OculusSettings::PhaseSync
	bool ___PhaseSync_11;
	// System.Boolean Unity.XR.Oculus.OculusSettings::SymmetricProjection
	bool ___SymmetricProjection_12;
	// System.Boolean Unity.XR.Oculus.OculusSettings::SubsampledLayout
	bool ___SubsampledLayout_13;
	// Unity.XR.Oculus.OculusSettings/FoveationMethod Unity.XR.Oculus.OculusSettings::FoveatedRenderingMethod
	int32_t ___FoveatedRenderingMethod_14;
	// System.Boolean Unity.XR.Oculus.OculusSettings::LateLatching
	bool ___LateLatching_15;
	// System.Boolean Unity.XR.Oculus.OculusSettings::LateLatchingDebug
	bool ___LateLatchingDebug_16;
	// System.Boolean Unity.XR.Oculus.OculusSettings::EnableTrackingOriginStageMode
	bool ___EnableTrackingOriginStageMode_17;
	// System.Boolean Unity.XR.Oculus.OculusSettings::SpaceWarp
	bool ___SpaceWarp_18;
	// System.Boolean Unity.XR.Oculus.OculusSettings::TargetQuest
	bool ___TargetQuest_19;
	// System.Boolean Unity.XR.Oculus.OculusSettings::TargetQuest2
	bool ___TargetQuest2_20;
	// System.Boolean Unity.XR.Oculus.OculusSettings::TargetQuestPro
	bool ___TargetQuestPro_21;
	// UnityEngine.Texture2D Unity.XR.Oculus.OculusSettings::SystemSplashScreen
	Texture2D_tE6505BC111DD8A424A9DBE8E05D7D09E11FFFCF4* ___SystemSplashScreen_22;
};

// System.TypeLoadException
struct TypeLoadException_t6333E3083F7BFF1A582969E6F67ACBA8B0035C32  : public SystemException_tCC48D868298F4C0705279823E34B00F4FBDB7295
{
	// System.String System.TypeLoadException::ClassName
	String_t* ___ClassName_18;
	// System.String System.TypeLoadException::AssemblyName
	String_t* ___AssemblyName_19;
	// System.String System.TypeLoadException::MessageArg
	String_t* ___MessageArg_20;
	// System.Int32 System.TypeLoadException::ResourceId
	int32_t ___ResourceId_21;
};

// UnityEngine.Events.UnityAction
struct UnityAction_t11A1F3B953B365C072A5DCC32677EE1796A962A7  : public MulticastDelegate_t
{
};

// UnityEngine.XR.XRDisplaySubsystem
struct XRDisplaySubsystem_t4B00B0BF1894A039ACFA8DDC2C2EB9301118C1F1  : public IntegratedSubsystem_1_t8312865F01EEA1EDE4B24A973E47ADD526616848
{
	// System.Action`1<System.Boolean> UnityEngine.XR.XRDisplaySubsystem::displayFocusChanged
	Action_1_t10DCB0C07D0D3C565CEACADC80D1152B35A45F6C* ___displayFocusChanged_2;
};

// UnityEngine.XR.XRDisplaySubsystemDescriptor
struct XRDisplaySubsystemDescriptor_t72DD88EE9094488AE723A495F48884BA4EA8311A  : public IntegratedSubsystemDescriptor_1_t7261AA0914165CB589AD41C4F9B463D44E333D7C
{
};

// UnityEngine.XR.XRInputSubsystem
struct XRInputSubsystem_tFECE6683FCAEBF05BAD05E5D612690095D8BAD34  : public IntegratedSubsystem_1_tF93BC76362E85BDD215312162457BE510FC76D3B
{
	// System.Action`1<UnityEngine.XR.XRInputSubsystem> UnityEngine.XR.XRInputSubsystem::trackingOriginUpdated
	Action_1_tC867D66471C553CFFF8707FF2C59FB7AAB03086A* ___trackingOriginUpdated_2;
	// System.Action`1<UnityEngine.XR.XRInputSubsystem> UnityEngine.XR.XRInputSubsystem::boundaryChanged
	Action_1_tC867D66471C553CFFF8707FF2C59FB7AAB03086A* ___boundaryChanged_3;
	// System.Collections.Generic.List`1<System.UInt64> UnityEngine.XR.XRInputSubsystem::m_DeviceIdsCache
	List_1_tB88E7361EE76DFB3EBB7FCD60CC59ACC3E48C284* ___m_DeviceIdsCache_4;
};

// UnityEngine.XR.Management.XRLoader
struct XRLoader_t80B1B1934C40561C5352ABC95D567DC2A7C9C976  : public ScriptableObject_tB3BFDB921A1B1795B38A5417D3B97A89A140436A
{
};

// System.DllNotFoundException
struct DllNotFoundException_t8CAE636A394C482C9FCF38FB7B7929506319D534  : public TypeLoadException_t6333E3083F7BFF1A582969E6F67ACBA8B0035C32
{
};

// UnityEngine.XR.Management.XRLoaderHelper
struct XRLoaderHelper_tE96E7AE003148D5319D20BAD7E02654367E41DCC  : public XRLoader_t80B1B1934C40561C5352ABC95D567DC2A7C9C976
{
	// System.Collections.Generic.Dictionary`2<System.Type,UnityEngine.ISubsystem> UnityEngine.XR.Management.XRLoaderHelper::m_SubsystemInstanceMap
	Dictionary_2_tCDC65F572855EBDD1C12CEE33EBEBE0131F60C9C* ___m_SubsystemInstanceMap_4;
};

// Unity.XR.Oculus.OculusLoader
struct OculusLoader_tA386B9AA0786D042EA272EDF385F96C0AD1A56BB  : public XRLoaderHelper_tE96E7AE003148D5319D20BAD7E02654367E41DCC
{
};

// <Module>

// <Module>

// System.Collections.Generic.List`1<UnityEngine.XR.XRDisplaySubsystem>
struct List_1_tA7666C6690CE2AEE97571615AD3AFCE2BB020597_StaticFields
{
	// T[] System.Collections.Generic.List`1::s_emptyArray
	XRDisplaySubsystemU5BU5D_t741124D80DCCCF62F2AF47431354B7387672F264* ___s_emptyArray_5;
};

// System.Collections.Generic.List`1<UnityEngine.XR.XRDisplaySubsystem>

// System.Collections.Generic.List`1<UnityEngine.XR.XRDisplaySubsystemDescriptor>
struct List_1_tC3F021D09EFA4F3516555517B5E0D39308C9C1B4_StaticFields
{
	// T[] System.Collections.Generic.List`1::s_emptyArray
	XRDisplaySubsystemDescriptorU5BU5D_t80B6AEA854B63F06CAE27E51662BBC06D752BFF0* ___s_emptyArray_5;
};

// System.Collections.Generic.List`1<UnityEngine.XR.XRDisplaySubsystemDescriptor>

// System.Collections.Generic.List`1<UnityEngine.XR.XRInputSubsystemDescriptor>
struct List_1_tE3AE94237CE649B47E1D52E1A3120E772255FF87_StaticFields
{
	// T[] System.Collections.Generic.List`1::s_emptyArray
	XRInputSubsystemDescriptorU5BU5D_tC53A4274C4DC364C07C751B6A9CF029515898A75* ___s_emptyArray_5;
};

// System.Collections.Generic.List`1<UnityEngine.XR.XRInputSubsystemDescriptor>

// Unity.XR.Oculus.Boundary

// Unity.XR.Oculus.Boundary

// System.Text.RegularExpressions.Capture

// System.Text.RegularExpressions.Capture

// Unity.XR.Oculus.Development
struct Development_t106B6EDC97423218186995B6FDF3EF5E6EE40C40_StaticFields
{
	// Unity.XR.Oculus.Development/UserDeveloperModeSettingCache Unity.XR.Oculus.Development::s_CachedMode
	int32_t ___s_CachedMode_0;
};

// Unity.XR.Oculus.Development

// System.Text.Encoding
struct Encoding_t65CDEF28CF20A7B8C92E85A4E808920C2465F095_StaticFields
{
	// System.Text.Encoding modreq(System.Runtime.CompilerServices.IsVolatile) System.Text.Encoding::defaultEncoding
	Encoding_t65CDEF28CF20A7B8C92E85A4E808920C2465F095* ___defaultEncoding_0;
	// System.Text.Encoding modreq(System.Runtime.CompilerServices.IsVolatile) System.Text.Encoding::unicodeEncoding
	Encoding_t65CDEF28CF20A7B8C92E85A4E808920C2465F095* ___unicodeEncoding_1;
	// System.Text.Encoding modreq(System.Runtime.CompilerServices.IsVolatile) System.Text.Encoding::bigEndianUnicode
	Encoding_t65CDEF28CF20A7B8C92E85A4E808920C2465F095* ___bigEndianUnicode_2;
	// System.Text.Encoding modreq(System.Runtime.CompilerServices.IsVolatile) System.Text.Encoding::utf7Encoding
	Encoding_t65CDEF28CF20A7B8C92E85A4E808920C2465F095* ___utf7Encoding_3;
	// System.Text.Encoding modreq(System.Runtime.CompilerServices.IsVolatile) System.Text.Encoding::utf8Encoding
	Encoding_t65CDEF28CF20A7B8C92E85A4E808920C2465F095* ___utf8Encoding_4;
	// System.Text.Encoding modreq(System.Runtime.CompilerServices.IsVolatile) System.Text.Encoding::utf32Encoding
	Encoding_t65CDEF28CF20A7B8C92E85A4E808920C2465F095* ___utf32Encoding_5;
	// System.Text.Encoding modreq(System.Runtime.CompilerServices.IsVolatile) System.Text.Encoding::asciiEncoding
	Encoding_t65CDEF28CF20A7B8C92E85A4E808920C2465F095* ___asciiEncoding_6;
	// System.Text.Encoding modreq(System.Runtime.CompilerServices.IsVolatile) System.Text.Encoding::latin1Encoding
	Encoding_t65CDEF28CF20A7B8C92E85A4E808920C2465F095* ___latin1Encoding_7;
	// System.Collections.Generic.Dictionary`2<System.Int32,System.Text.Encoding> modreq(System.Runtime.CompilerServices.IsVolatile) System.Text.Encoding::encodings
	Dictionary_2_t87EDE08B2E48F793A22DE50D6B3CC2E7EBB2DB54* ___encodings_8;
	// System.Object System.Text.Encoding::s_InternalSyncObject
	RuntimeObject* ___s_InternalSyncObject_15;
};

// System.Text.Encoding

// System.Text.RegularExpressions.GroupCollection

// System.Text.RegularExpressions.GroupCollection

// Unity.XR.Oculus.InputFocus
struct InputFocus_t76018DDD56BB288561D354B825D5929965DC38FE_StaticFields
{
	// System.Action Unity.XR.Oculus.InputFocus::InputFocusAcquired
	Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* ___InputFocusAcquired_0;
	// System.Action Unity.XR.Oculus.InputFocus::InputFocusLost
	Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* ___InputFocusLost_1;
	// System.Boolean Unity.XR.Oculus.InputFocus::hadInputFocus
	bool ___hadInputFocus_2;
};

// Unity.XR.Oculus.InputFocus

// Unity.XR.Oculus.NativeMethods

// Unity.XR.Oculus.NativeMethods

// Unity.XR.Oculus.Performance
struct Performance_tB1393E2318BEDFD1E01AF8B40C104A3C938D5777_StaticFields
{
	// System.Single[] Unity.XR.Oculus.Performance::cachedDisplayAvailableFrequencies
	SingleU5BU5D_t89DEFE97BCEDB5857010E79ECE0F52CF6E93B87C* ___cachedDisplayAvailableFrequencies_0;
};

// Unity.XR.Oculus.Performance

// Unity.XR.Oculus.RegisterUpdateCallback

// Unity.XR.Oculus.RegisterUpdateCallback

// Unity.XR.Oculus.Stats
struct Stats_t532BDF72F0CFA5121B447784EC03851E18A51BFB_StaticFields
{
	// UnityEngine.IntegratedSubsystem Unity.XR.Oculus.Stats::m_Display
	IntegratedSubsystem_t990160A89854D87C0836DC589B720231C02D4CE3* ___m_Display_0;
	// System.String Unity.XR.Oculus.Stats::m_PluginVersion
	String_t* ___m_PluginVersion_1;
};

// Unity.XR.Oculus.Stats

// System.String
struct String_t_StaticFields
{
	// System.String System.String::Empty
	String_t* ___Empty_6;
};

// System.String

// Unity.XR.Oculus.Utils

// Unity.XR.Oculus.Utils

// System.ValueType

// System.ValueType

// Unity.XR.Oculus.NativeMethods/Internal

// Unity.XR.Oculus.NativeMethods/Internal

// Unity.XR.Oculus.Stats/AdaptivePerformance

// Unity.XR.Oculus.Stats/AdaptivePerformance

// Unity.XR.Oculus.Stats/AppMetrics

// Unity.XR.Oculus.Stats/AppMetrics

// Unity.XR.Oculus.Stats/PerfMetrics

// Unity.XR.Oculus.Stats/PerfMetrics

// System.Collections.Generic.List`1/Enumerator<System.Object>

// System.Collections.Generic.List`1/Enumerator<System.Object>

// System.Collections.Generic.List`1/Enumerator<UnityEngine.XR.XRDisplaySubsystem>

// System.Collections.Generic.List`1/Enumerator<UnityEngine.XR.XRDisplaySubsystem>

// UnityEngine.XR.InputFeatureUsage`1<System.Boolean>

// UnityEngine.XR.InputFeatureUsage`1<System.Boolean>

// System.Boolean
struct Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_StaticFields
{
	// System.String System.Boolean::TrueString
	String_t* ___TrueString_5;
	// System.String System.Boolean::FalseString
	String_t* ___FalseString_6;
};

// System.Boolean

// System.Byte

// System.Byte

// System.Text.RegularExpressions.Group
struct Group_t26371E9136D6F43782C487B63C67C5FC4F472881_StaticFields
{
	// System.Text.RegularExpressions.Group System.Text.RegularExpressions.Group::s_emptyGroup
	Group_t26371E9136D6F43782C487B63C67C5FC4F472881* ___s_emptyGroup_3;
};

// System.Text.RegularExpressions.Group

// System.Int32

// System.Int32

// System.IntPtr
struct IntPtr_t_StaticFields
{
	// System.IntPtr System.IntPtr::Zero
	intptr_t ___Zero_1;
};

// System.IntPtr

// System.Single

// System.Single

// System.UInt16

// System.UInt16

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

// UnityEngine.Vector4
struct Vector4_t58B63D32F48C0DBF50DE2C60794C4676C80EDBE3_StaticFields
{
	// UnityEngine.Vector4 UnityEngine.Vector4::zeroVector
	Vector4_t58B63D32F48C0DBF50DE2C60794C4676C80EDBE3 ___zeroVector_5;
	// UnityEngine.Vector4 UnityEngine.Vector4::oneVector
	Vector4_t58B63D32F48C0DBF50DE2C60794C4676C80EDBE3 ___oneVector_6;
	// UnityEngine.Vector4 UnityEngine.Vector4::positiveInfinityVector
	Vector4_t58B63D32F48C0DBF50DE2C60794C4676C80EDBE3 ___positiveInfinityVector_7;
	// UnityEngine.Vector4 UnityEngine.Vector4::negativeInfinityVector
	Vector4_t58B63D32F48C0DBF50DE2C60794C4676C80EDBE3 ___negativeInfinityVector_8;
};

// UnityEngine.Vector4

// System.Void

// System.Void

// Unity.XR.Oculus.NativeMethods/UserDefinedSettings

// Unity.XR.Oculus.NativeMethods/UserDefinedSettings

// UnityEngine.AndroidJavaProxy
struct AndroidJavaProxy_tE5521F9761F7B95444B9C39FB15FDFC23F80A78D_StaticFields
{
	// UnityEngine.GlobalJavaObjectRef UnityEngine.AndroidJavaProxy::s_JavaLangSystemClass
	GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* ___s_JavaLangSystemClass_2;
	// System.IntPtr UnityEngine.AndroidJavaProxy::s_HashCodeMethodID
	intptr_t ___s_HashCodeMethodID_3;
};

// UnityEngine.AndroidJavaProxy

// System.Delegate

// System.Delegate

// System.Exception
struct Exception_t_StaticFields
{
	// System.Object System.Exception::s_EDILock
	RuntimeObject* ___s_EDILock_0;
};

// System.Exception

// UnityEngine.IntegratedSubsystem

// UnityEngine.IntegratedSubsystem

// UnityEngine.IntegratedSubsystemDescriptor

// UnityEngine.IntegratedSubsystemDescriptor

// System.Text.RegularExpressions.Match
struct Match_tFBEBCF225BD8EA17BCE6CE3FE5C1BD8E3074105F_StaticFields
{
	// System.Text.RegularExpressions.Match System.Text.RegularExpressions.Match::<Empty>k__BackingField
	Match_tFBEBCF225BD8EA17BCE6CE3FE5C1BD8E3074105F* ___U3CEmptyU3Ek__BackingField_17;
};

// System.Text.RegularExpressions.Match

// UnityEngine.Object
struct Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_StaticFields
{
	// System.Int32 UnityEngine.Object::OffsetOfInstanceIDInCPlusPlusObject
	int32_t ___OffsetOfInstanceIDInCPlusPlusObject_1;
};

// UnityEngine.Object

// Unity.XR.Oculus.OculusUsages
struct OculusUsages_t26394A1703082235CAC869B16DF3A491A7965196_StaticFields
{
	// UnityEngine.XR.InputFeatureUsage`1<System.Boolean> Unity.XR.Oculus.OculusUsages::thumbrest
	InputFeatureUsage_1_tE336B2F0B9AC721519BFA17A08D6353FD5221637 ___thumbrest_0;
	// UnityEngine.XR.InputFeatureUsage`1<System.Boolean> Unity.XR.Oculus.OculusUsages::indexTouch
	InputFeatureUsage_1_tE336B2F0B9AC721519BFA17A08D6353FD5221637 ___indexTouch_1;
	// UnityEngine.XR.InputFeatureUsage`1<System.Boolean> Unity.XR.Oculus.OculusUsages::thumbTouch
	InputFeatureUsage_1_tE336B2F0B9AC721519BFA17A08D6353FD5221637 ___thumbTouch_2;
};

// Unity.XR.Oculus.OculusUsages

// UnityEngine.IntegratedSubsystemDescriptor`1<UnityEngine.XR.XRDisplaySubsystem>

// UnityEngine.IntegratedSubsystemDescriptor`1<UnityEngine.XR.XRDisplaySubsystem>

// UnityEngine.IntegratedSubsystem`1<UnityEngine.XR.XRDisplaySubsystemDescriptor>

// UnityEngine.IntegratedSubsystem`1<UnityEngine.XR.XRDisplaySubsystemDescriptor>

// UnityEngine.IntegratedSubsystem`1<UnityEngine.XR.XRInputSubsystemDescriptor>

// UnityEngine.IntegratedSubsystem`1<UnityEngine.XR.XRInputSubsystemDescriptor>

// System.MulticastDelegate

// System.MulticastDelegate

// UnityEngine.Android.PermissionCallbacks

// UnityEngine.Android.PermissionCallbacks

// UnityEngine.ScriptableObject

// UnityEngine.ScriptableObject

// System.SystemException

// System.SystemException

// System.Action`1<System.String>

// System.Action`1<System.String>

// System.Action

// System.Action

// Unity.XR.Oculus.OculusSettings
struct OculusSettings_t0584FB71432B697479FD0BFC5B68C195F17CD321_StaticFields
{
	// Unity.XR.Oculus.OculusSettings Unity.XR.Oculus.OculusSettings::s_Settings
	OculusSettings_t0584FB71432B697479FD0BFC5B68C195F17CD321* ___s_Settings_23;
};

// Unity.XR.Oculus.OculusSettings

// System.TypeLoadException

// System.TypeLoadException

// UnityEngine.Events.UnityAction

// UnityEngine.Events.UnityAction

// UnityEngine.XR.XRDisplaySubsystem

// UnityEngine.XR.XRDisplaySubsystem

// UnityEngine.XR.XRDisplaySubsystemDescriptor

// UnityEngine.XR.XRDisplaySubsystemDescriptor

// UnityEngine.XR.XRInputSubsystem

// UnityEngine.XR.XRInputSubsystem

// UnityEngine.XR.Management.XRLoader

// UnityEngine.XR.Management.XRLoader

// System.DllNotFoundException

// System.DllNotFoundException

// UnityEngine.XR.Management.XRLoaderHelper

// UnityEngine.XR.Management.XRLoaderHelper

// Unity.XR.Oculus.OculusLoader
struct OculusLoader_tA386B9AA0786D042EA272EDF385F96C0AD1A56BB_StaticFields
{
	// System.Collections.Generic.List`1<UnityEngine.XR.XRDisplaySubsystemDescriptor> Unity.XR.Oculus.OculusLoader::s_DisplaySubsystemDescriptors
	List_1_tC3F021D09EFA4F3516555517B5E0D39308C9C1B4* ___s_DisplaySubsystemDescriptors_5;
	// System.Collections.Generic.List`1<UnityEngine.XR.XRInputSubsystemDescriptor> Unity.XR.Oculus.OculusLoader::s_InputSubsystemDescriptors
	List_1_tE3AE94237CE649B47E1D52E1A3120E772255FF87* ___s_InputSubsystemDescriptors_6;
};

// Unity.XR.Oculus.OculusLoader
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
// System.Byte[]
struct ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031  : public RuntimeArray
{
	ALIGN_FIELD (8) uint8_t m_Items[1];

	inline uint8_t GetAt(il2cpp_array_size_t index) const
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items[index];
	}
	inline uint8_t* GetAddressAt(il2cpp_array_size_t index)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items + index;
	}
	inline void SetAt(il2cpp_array_size_t index, uint8_t value)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		m_Items[index] = value;
	}
	inline uint8_t GetAtUnchecked(il2cpp_array_size_t index) const
	{
		return m_Items[index];
	}
	inline uint8_t* GetAddressAtUnchecked(il2cpp_array_size_t index)
	{
		return m_Items + index;
	}
	inline void SetAtUnchecked(il2cpp_array_size_t index, uint8_t value)
	{
		m_Items[index] = value;
	}
};


// System.Void UnityEngine.XR.Management.XRLoaderHelper::CreateSubsystem<System.Object,System.Object>(System.Collections.Generic.List`1<TDescriptor>,System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void XRLoaderHelper_CreateSubsystem_TisRuntimeObject_TisRuntimeObject_m4FA794B59AA23B850EE0DF5DA0776E9DD231D768_gshared (XRLoaderHelper_tE96E7AE003148D5319D20BAD7E02654367E41DCC* __this, List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D* ___0_descriptors, String_t* ___1_id, const RuntimeMethod* method) ;
// System.Void System.Action`1<System.Object>::.ctor(System.Object,System.IntPtr)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Action_1__ctor_m2E1DFA67718FC1A0B6E5DFEB78831FFE9C059EB4_gshared (Action_1_t6F9EB113EB3F16226AEF811A2744F4111C116C87* __this, RuntimeObject* ___0_object, intptr_t ___1_method, const RuntimeMethod* method) ;
// System.Void UnityEngine.XR.Management.XRLoaderHelper::StartSubsystem<System.Object>()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void XRLoaderHelper_StartSubsystem_TisRuntimeObject_mC3EF63B68F73D6809F68E225847BB59D472A2EA5_gshared (XRLoaderHelper_tE96E7AE003148D5319D20BAD7E02654367E41DCC* __this, const RuntimeMethod* method) ;
// System.Void UnityEngine.XR.Management.XRLoaderHelper::StopSubsystem<System.Object>()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void XRLoaderHelper_StopSubsystem_TisRuntimeObject_m26C61BBD9562F521BC7DE34ABC8E6AA01E656572_gshared (XRLoaderHelper_tE96E7AE003148D5319D20BAD7E02654367E41DCC* __this, const RuntimeMethod* method) ;
// System.Void UnityEngine.XR.Management.XRLoaderHelper::DestroySubsystem<System.Object>()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void XRLoaderHelper_DestroySubsystem_TisRuntimeObject_mF0CB81C6BD9DA12D6E8C21703A18E939389A1185_gshared (XRLoaderHelper_tE96E7AE003148D5319D20BAD7E02654367E41DCC* __this, const RuntimeMethod* method) ;
// System.Void System.Collections.Generic.List`1<System.Object>::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1__ctor_m7F078BB342729BDF11327FD89D7872265328F690_gshared (List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D* __this, const RuntimeMethod* method) ;
// System.Int32 System.Array::IndexOf<System.Byte>(T[],T)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t Array_IndexOf_TisByte_t94D9231AC217BE4D2E004C4CD32DF6D099EA41A3_m3E47AE0BD5292CA28182DF90A3A3B19D2DBE1B6F_gshared (ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* ___0_array, uint8_t ___1_value, const RuntimeMethod* method) ;
// System.Void UnityEngine.SubsystemManager::GetInstances<System.Object>(System.Collections.Generic.List`1<T>)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void SubsystemManager_GetInstances_TisRuntimeObject_mCBA01F889BE624767358AB5494CD2D7F1B2FB064_gshared (List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D* ___0_subsystems, const RuntimeMethod* method) ;
// System.Collections.Generic.List`1/Enumerator<T> System.Collections.Generic.List`1<System.Object>::GetEnumerator()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Enumerator_t9473BAB568A27E2339D48C1F91319E0F6D244D7A List_1_GetEnumerator_mD8294A7FA2BEB1929487127D476F8EC1CDC23BFC_gshared (List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D* __this, const RuntimeMethod* method) ;
// System.Void System.Collections.Generic.List`1/Enumerator<System.Object>::Dispose()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Enumerator_Dispose_mD9DC3E3C3697830A4823047AB29A77DBBB5ED419_gshared (Enumerator_t9473BAB568A27E2339D48C1F91319E0F6D244D7A* __this, const RuntimeMethod* method) ;
// T System.Collections.Generic.List`1/Enumerator<System.Object>::get_Current()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR RuntimeObject* Enumerator_get_Current_m6330F15D18EE4F547C05DF9BF83C5EB710376027_gshared_inline (Enumerator_t9473BAB568A27E2339D48C1F91319E0F6D244D7A* __this, const RuntimeMethod* method) ;
// TSubsystemDescriptor UnityEngine.IntegratedSubsystem`1<System.Object>::get_SubsystemDescriptor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* IntegratedSubsystem_1_get_SubsystemDescriptor_mC6FFC8B129C4D3146C76A9D0B2CE69811F0F5AB8_gshared (IntegratedSubsystem_1_t6CAFC4ADB928A1CB6A1BAA66C12250FB6C841842* __this, const RuntimeMethod* method) ;
// System.Boolean System.Collections.Generic.List`1/Enumerator<System.Object>::MoveNext()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Enumerator_MoveNext_mE921CC8F29FBBDE7CC3209A0ED0D921D58D00BCB_gshared (Enumerator_t9473BAB568A27E2339D48C1F91319E0F6D244D7A* __this, const RuntimeMethod* method) ;
// System.Void UnityEngine.XR.InputFeatureUsage`1<System.Boolean>::.ctor(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void InputFeatureUsage_1__ctor_mEB36F8937385A1065CD9F48AE2DAD9EAE49EFCE7_gshared (InputFeatureUsage_1_tE336B2F0B9AC721519BFA17A08D6353FD5221637* __this, String_t* ___0_usageName, const RuntimeMethod* method) ;

// System.Void Unity.XR.Oculus.NativeMethods::SetColorScale(System.Single,System.Single,System.Single,System.Single)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void NativeMethods_SetColorScale_m71466C371AE4D8AE0393FCA19BCB93DFF87FCF9B (float ___0_x, float ___1_y, float ___2_z, float ___3_w, const RuntimeMethod* method) ;
// System.Void Unity.XR.Oculus.NativeMethods::SetColorOffset(System.Single,System.Single,System.Single,System.Single)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void NativeMethods_SetColorOffset_m708A9D66EF8DBDDBD1DB4F5FFCC572F007742BF8 (float ___0_x, float ___1_y, float ___2_z, float ___3_w, const RuntimeMethod* method) ;
// Unity.XR.Oculus.SystemHeadset Unity.XR.Oculus.NativeMethods::GetSystemHeadsetType()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t NativeMethods_GetSystemHeadsetType_mD18D6819F35C667E7E150E538A884850C6E89A4E (const RuntimeMethod* method) ;
// System.Boolean Unity.XR.Oculus.NativeMethods::GetTiledMultiResSupported()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool NativeMethods_GetTiledMultiResSupported_mBEFDE3CDA036B5D2B2FADCCCC791C6035E711970 (const RuntimeMethod* method) ;
// System.Void UnityEngine.Debug::LogWarning(System.Object)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Debug_LogWarning_m33EF1B897E0C7C6FF538989610BFAFFEF4628CA9 (RuntimeObject* ___0_message, const RuntimeMethod* method) ;
// System.Void Unity.XR.Oculus.NativeMethods::SetTiledMultiResLevel(System.Int32)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void NativeMethods_SetTiledMultiResLevel_mAA62ED9D93CA810457DE6B4E90DD27B877739D7B (int32_t ___0_level, const RuntimeMethod* method) ;
// System.Void Unity.XR.Oculus.NativeMethods::SetTiledMultiResDynamic(System.Boolean)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void NativeMethods_SetTiledMultiResDynamic_m8FC41964A4A054ADC21137FDD47C03248139FA05 (bool ___0_isDynamic, const RuntimeMethod* method) ;
// System.Int32 Unity.XR.Oculus.NativeMethods::GetTiledMultiResLevel()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t NativeMethods_GetTiledMultiResLevel_mA712F64DAA92C2D399980A48ABFE282E92887012 (const RuntimeMethod* method) ;
// System.Delegate System.Delegate::Combine(System.Delegate,System.Delegate)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Delegate_t* Delegate_Combine_m1F725AEF318BE6F0426863490691A6F4606E7D00 (Delegate_t* ___0_a, Delegate_t* ___1_b, const RuntimeMethod* method) ;
// System.Delegate System.Delegate::Remove(System.Delegate,System.Delegate)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Delegate_t* Delegate_Remove_m8B7DD5661308FA972E23CA1CC3FC9CEB355504E3 (Delegate_t* ___0_source, Delegate_t* ___1_value, const RuntimeMethod* method) ;
// System.Boolean Unity.XR.Oculus.NativeMethods::GetHasInputFocus()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool NativeMethods_GetHasInputFocus_m1257051A904A1CD9F07C962F059E01223F14DF1B (const RuntimeMethod* method) ;
// System.Boolean Unity.XR.Oculus.InputFocus::get_hasInputFocus()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool InputFocus_get_hasInputFocus_mB9DD992DB215FF22B842E83C96118736B463A64C (const RuntimeMethod* method) ;
// System.Void System.Action::Invoke()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void Action_Invoke_m7126A54DACA72B845424072887B5F3A51FC3808E_inline (Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* __this, const RuntimeMethod* method) ;
// System.Void System.Object::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2 (RuntimeObject* __this, const RuntimeMethod* method) ;
// System.Boolean Unity.XR.Oculus.NativeMethods::GetBoundaryConfigured()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool NativeMethods_GetBoundaryConfigured_m4010CCBA97D29852557408736FCE53C8A3CB6C9F (const RuntimeMethod* method) ;
// System.Boolean Unity.XR.Oculus.NativeMethods::GetBoundaryDimensions(Unity.XR.Oculus.Boundary/BoundaryType,UnityEngine.Vector3&)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool NativeMethods_GetBoundaryDimensions_m2C4E14BF8B564D95BA1FD6D7F6BB3CDC68A368CD (int32_t ___0_boundaryType, Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2* ___1_dimensions, const RuntimeMethod* method) ;
// System.Boolean Unity.XR.Oculus.NativeMethods::GetBoundaryVisible()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool NativeMethods_GetBoundaryVisible_m68BB43585F6289250D59230FF61F4411537D169C (const RuntimeMethod* method) ;
// System.Void Unity.XR.Oculus.NativeMethods::SetBoundaryVisible(System.Boolean)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void NativeMethods_SetBoundaryVisible_m8281A6859FEE7659C63C644B9C37245D187FAC8F (bool ___0_boundaryVisible, const RuntimeMethod* method) ;
// System.Boolean UnityEngine.Debug::get_isDebugBuild()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Debug_get_isDebugBuild_m9277C4A9591F7E1D8B76340B4CAE5EA33D63AF01 (const RuntimeMethod* method) ;
// System.Boolean Unity.XR.Oculus.NativeMethods::SetDeveloperModeStrict(System.Boolean)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool NativeMethods_SetDeveloperModeStrict_mDD7A7FF224AC714D8D8C265C2E1452DB0AE653C8 (bool ___0_active, const RuntimeMethod* method) ;
// System.Void UnityEngine.Debug::LogError(System.Object)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Debug_LogError_mB00B2B4468EF3CAF041B038D840820FB84C924B2 (RuntimeObject* ___0_message, const RuntimeMethod* method) ;
// System.Boolean Unity.XR.Oculus.NativeMethods::GetIsSupportedDevice()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool NativeMethods_GetIsSupportedDevice_m336DCB5F80772C6F89CBF1CA7122EDC4C35D78D4 (const RuntimeMethod* method) ;
// System.Boolean Unity.XR.Oculus.OculusLoader::CheckUnityVersionCompatibility()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool OculusLoader_CheckUnityVersionCompatibility_mC211D3EFA35B0E509154B8F2993B08079A5C4413 (OculusLoader_tA386B9AA0786D042EA272EDF385F96C0AD1A56BB* __this, const RuntimeMethod* method) ;
// Unity.XR.Oculus.OculusLoader/DeviceSupportedResult Unity.XR.Oculus.OculusLoader::IsDeviceSupported()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t OculusLoader_IsDeviceSupported_m478A97E527F8DA4B6C08459445739C2E9C72C36D (const RuntimeMethod* method) ;
// Unity.XR.Oculus.OculusSettings Unity.XR.Oculus.OculusLoader::GetSettings()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR OculusSettings_t0584FB71432B697479FD0BFC5B68C195F17CD321* OculusLoader_GetSettings_m2FA61767F36A8CFB03124EB295A79C7A7F0A863B_inline (OculusLoader_tA386B9AA0786D042EA272EDF385F96C0AD1A56BB* __this, const RuntimeMethod* method) ;
// System.Boolean UnityEngine.Object::op_Inequality(UnityEngine.Object,UnityEngine.Object)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Object_op_Inequality_mD0BE578448EAA61948F25C32F8DD55AB1F778602 (Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* ___0_x, Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* ___1_y, const RuntimeMethod* method) ;
// System.UInt16 Unity.XR.Oculus.OculusSettings::GetStereoRenderingMode()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR uint16_t OculusSettings_GetStereoRenderingMode_mE9C6ABC56B9EDEB3BE5599B091BB6699429BB6BD (OculusSettings_t0584FB71432B697479FD0BFC5B68C195F17CD321* __this, const RuntimeMethod* method) ;
// UnityEngine.ColorSpace UnityEngine.QualitySettings::get_activeColorSpace()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t QualitySettings_get_activeColorSpace_m4F47784E7B0FE0A5497C8BAB9CA86BD576FB92F9 (const RuntimeMethod* method) ;
// System.Void Unity.XR.Oculus.NativeMethods::SetUserDefinedSettings(Unity.XR.Oculus.NativeMethods/UserDefinedSettings)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void NativeMethods_SetUserDefinedSettings_m42FD3CA27980B25D9C638E3AC876749FBB23CBA6 (UserDefinedSettings_tE823040D57E8C8C9D8A028F206230E1C7BAC8EE3 ___0_settings, const RuntimeMethod* method) ;
// System.Void UnityEngine.XR.Management.XRLoaderHelper::CreateSubsystem<UnityEngine.XR.XRDisplaySubsystemDescriptor,UnityEngine.XR.XRDisplaySubsystem>(System.Collections.Generic.List`1<TDescriptor>,System.String)
inline void XRLoaderHelper_CreateSubsystem_TisXRDisplaySubsystemDescriptor_t72DD88EE9094488AE723A495F48884BA4EA8311A_TisXRDisplaySubsystem_t4B00B0BF1894A039ACFA8DDC2C2EB9301118C1F1_m47BB00ACEADFC3AF821DC1EE31F79CF6EEB4681A (XRLoaderHelper_tE96E7AE003148D5319D20BAD7E02654367E41DCC* __this, List_1_tC3F021D09EFA4F3516555517B5E0D39308C9C1B4* ___0_descriptors, String_t* ___1_id, const RuntimeMethod* method)
{
	((  void (*) (XRLoaderHelper_tE96E7AE003148D5319D20BAD7E02654367E41DCC*, List_1_tC3F021D09EFA4F3516555517B5E0D39308C9C1B4*, String_t*, const RuntimeMethod*))XRLoaderHelper_CreateSubsystem_TisRuntimeObject_TisRuntimeObject_m4FA794B59AA23B850EE0DF5DA0776E9DD231D768_gshared)(__this, ___0_descriptors, ___1_id, method);
}
// System.Void UnityEngine.XR.Management.XRLoaderHelper::CreateSubsystem<UnityEngine.XR.XRInputSubsystemDescriptor,UnityEngine.XR.XRInputSubsystem>(System.Collections.Generic.List`1<TDescriptor>,System.String)
inline void XRLoaderHelper_CreateSubsystem_TisXRInputSubsystemDescriptor_t42088DD6542C0BDD27C2951B911E4F69DD1F917D_TisXRInputSubsystem_tFECE6683FCAEBF05BAD05E5D612690095D8BAD34_mA9FE0AE2F98657CD075CD191BAB94910F963C08C (XRLoaderHelper_tE96E7AE003148D5319D20BAD7E02654367E41DCC* __this, List_1_tE3AE94237CE649B47E1D52E1A3120E772255FF87* ___0_descriptors, String_t* ___1_id, const RuntimeMethod* method)
{
	((  void (*) (XRLoaderHelper_tE96E7AE003148D5319D20BAD7E02654367E41DCC*, List_1_tE3AE94237CE649B47E1D52E1A3120E772255FF87*, String_t*, const RuntimeMethod*))XRLoaderHelper_CreateSubsystem_TisRuntimeObject_TisRuntimeObject_m4FA794B59AA23B850EE0DF5DA0776E9DD231D768_gshared)(__this, ___0_descriptors, ___1_id, method);
}
// UnityEngine.XR.XRDisplaySubsystem Unity.XR.Oculus.OculusLoader::get_displaySubsystem()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR XRDisplaySubsystem_t4B00B0BF1894A039ACFA8DDC2C2EB9301118C1F1* OculusLoader_get_displaySubsystem_mBF36D42BABD9D5DA3ECE4F2D25862BF35C29D4E3 (OculusLoader_tA386B9AA0786D042EA272EDF385F96C0AD1A56BB* __this, const RuntimeMethod* method) ;
// UnityEngine.XR.XRInputSubsystem Unity.XR.Oculus.OculusLoader::get_inputSubsystem()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR XRInputSubsystem_tFECE6683FCAEBF05BAD05E5D612690095D8BAD34* OculusLoader_get_inputSubsystem_m3690AE48575D2196C79757CE5F21C96F1A0963B2 (OculusLoader_tA386B9AA0786D042EA272EDF385F96C0AD1A56BB* __this, const RuntimeMethod* method) ;
// System.Void Unity.XR.Oculus.RegisterUpdateCallback::Initialize()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void RegisterUpdateCallback_Initialize_m0256D76B69C444418E9E394DD280C4D54F122BC5 (const RuntimeMethod* method) ;
// System.Boolean Unity.XR.Oculus.NativeMethods::GetEyeTrackedFoveatedRenderingSupported()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool NativeMethods_GetEyeTrackedFoveatedRenderingSupported_m7017B3CE2D6DE03700864069DECC810199A2C9E0 (const RuntimeMethod* method) ;
// System.Void UnityEngine.Android.PermissionCallbacks::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void PermissionCallbacks__ctor_m91B14BBBC8913C131E400BA0D13576822AAE7A75 (PermissionCallbacks_t6C5002298E6649261FCB52C244695459E5C9890F* __this, const RuntimeMethod* method) ;
// System.Void System.Action`1<System.String>::.ctor(System.Object,System.IntPtr)
inline void Action_1__ctor_m9DC2953C55C4D7D4B7BEFE03D84DA1F9362D652C (Action_1_t3CB5D1A819C3ED3F99E9E39F890F18633253949A* __this, RuntimeObject* ___0_object, intptr_t ___1_method, const RuntimeMethod* method)
{
	((  void (*) (Action_1_t3CB5D1A819C3ED3F99E9E39F890F18633253949A*, RuntimeObject*, intptr_t, const RuntimeMethod*))Action_1__ctor_m2E1DFA67718FC1A0B6E5DFEB78831FFE9C059EB4_gshared)(__this, ___0_object, ___1_method, method);
}
// System.Void UnityEngine.Android.PermissionCallbacks::add_PermissionGranted(System.Action`1<System.String>)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void PermissionCallbacks_add_PermissionGranted_m74335D4200D9B1A7C80AB9C133F95C61FCDCDF89 (PermissionCallbacks_t6C5002298E6649261FCB52C244695459E5C9890F* __this, Action_1_t3CB5D1A819C3ED3F99E9E39F890F18633253949A* ___0_value, const RuntimeMethod* method) ;
// System.Void UnityEngine.Android.Permission::RequestUserPermission(System.String,UnityEngine.Android.PermissionCallbacks)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Permission_RequestUserPermission_m7B8E817C03FDB5C99F22002C7181F27BF031F117 (String_t* ___0_permission, PermissionCallbacks_t6C5002298E6649261FCB52C244695459E5C9890F* ___1_callbacks, const RuntimeMethod* method) ;
// System.Boolean System.String::op_Equality(System.String,System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool String_op_Equality_m030E1B219352228970A076136E455C4E568C02C1 (String_t* ___0_a, String_t* ___1_b, const RuntimeMethod* method) ;
// System.Void Unity.XR.Oculus.NativeMethods::SetHasUserAuthorizedEyeTrackingPermission(System.Boolean)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void NativeMethods_SetHasUserAuthorizedEyeTrackingPermission_mC35B88244AA80A2CA74022E853A6D94CF10C4ACA (bool ___0_authorized, const RuntimeMethod* method) ;
// System.Void UnityEngine.XR.Management.XRLoaderHelper::StartSubsystem<UnityEngine.XR.XRDisplaySubsystem>()
inline void XRLoaderHelper_StartSubsystem_TisXRDisplaySubsystem_t4B00B0BF1894A039ACFA8DDC2C2EB9301118C1F1_mC1643794A5D3CC32BF1EE9C976CE5F23A6BB8962 (XRLoaderHelper_tE96E7AE003148D5319D20BAD7E02654367E41DCC* __this, const RuntimeMethod* method)
{
	((  void (*) (XRLoaderHelper_tE96E7AE003148D5319D20BAD7E02654367E41DCC*, const RuntimeMethod*))XRLoaderHelper_StartSubsystem_TisRuntimeObject_mC3EF63B68F73D6809F68E225847BB59D472A2EA5_gshared)(__this, method);
}
// System.Void UnityEngine.XR.Management.XRLoaderHelper::StartSubsystem<UnityEngine.XR.XRInputSubsystem>()
inline void XRLoaderHelper_StartSubsystem_TisXRInputSubsystem_tFECE6683FCAEBF05BAD05E5D612690095D8BAD34_m08AC127201FE89396BD81BEA52D40790BC0CA3FA (XRLoaderHelper_tE96E7AE003148D5319D20BAD7E02654367E41DCC* __this, const RuntimeMethod* method)
{
	((  void (*) (XRLoaderHelper_tE96E7AE003148D5319D20BAD7E02654367E41DCC*, const RuntimeMethod*))XRLoaderHelper_StartSubsystem_TisRuntimeObject_mC3EF63B68F73D6809F68E225847BB59D472A2EA5_gshared)(__this, method);
}
// System.Void Unity.XR.Oculus.Development::OverrideDeveloperModeStart()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Development_OverrideDeveloperModeStart_m7017971A663FEE72864A6D5039091E21DB045C1B (const RuntimeMethod* method) ;
// System.Void UnityEngine.XR.Management.XRLoaderHelper::StopSubsystem<UnityEngine.XR.XRDisplaySubsystem>()
inline void XRLoaderHelper_StopSubsystem_TisXRDisplaySubsystem_t4B00B0BF1894A039ACFA8DDC2C2EB9301118C1F1_m00A3A82597D484DE2EBB03EA9A2430AFDE44DE24 (XRLoaderHelper_tE96E7AE003148D5319D20BAD7E02654367E41DCC* __this, const RuntimeMethod* method)
{
	((  void (*) (XRLoaderHelper_tE96E7AE003148D5319D20BAD7E02654367E41DCC*, const RuntimeMethod*))XRLoaderHelper_StopSubsystem_TisRuntimeObject_m26C61BBD9562F521BC7DE34ABC8E6AA01E656572_gshared)(__this, method);
}
// System.Void UnityEngine.XR.Management.XRLoaderHelper::StopSubsystem<UnityEngine.XR.XRInputSubsystem>()
inline void XRLoaderHelper_StopSubsystem_TisXRInputSubsystem_tFECE6683FCAEBF05BAD05E5D612690095D8BAD34_mE6F3E85E0C64666EE9A517CD7CF3669FB7BAC750 (XRLoaderHelper_tE96E7AE003148D5319D20BAD7E02654367E41DCC* __this, const RuntimeMethod* method)
{
	((  void (*) (XRLoaderHelper_tE96E7AE003148D5319D20BAD7E02654367E41DCC*, const RuntimeMethod*))XRLoaderHelper_StopSubsystem_TisRuntimeObject_m26C61BBD9562F521BC7DE34ABC8E6AA01E656572_gshared)(__this, method);
}
// System.Void Unity.XR.Oculus.Development::OverrideDeveloperModeStop()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Development_OverrideDeveloperModeStop_m89921E0CCD7DAC487814309D82E5D42288C8BBC4 (const RuntimeMethod* method) ;
// System.Void Unity.XR.Oculus.RegisterUpdateCallback::Deinitialize()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void RegisterUpdateCallback_Deinitialize_m3826762052F507B9B18E62483D8341495B5EEA13 (const RuntimeMethod* method) ;
// System.Void UnityEngine.XR.Management.XRLoaderHelper::DestroySubsystem<UnityEngine.XR.XRDisplaySubsystem>()
inline void XRLoaderHelper_DestroySubsystem_TisXRDisplaySubsystem_t4B00B0BF1894A039ACFA8DDC2C2EB9301118C1F1_m8E2572BB5610CCE3B1DBA87453CFE09BB4B2B606 (XRLoaderHelper_tE96E7AE003148D5319D20BAD7E02654367E41DCC* __this, const RuntimeMethod* method)
{
	((  void (*) (XRLoaderHelper_tE96E7AE003148D5319D20BAD7E02654367E41DCC*, const RuntimeMethod*))XRLoaderHelper_DestroySubsystem_TisRuntimeObject_mF0CB81C6BD9DA12D6E8C21703A18E939389A1185_gshared)(__this, method);
}
// System.Void UnityEngine.XR.Management.XRLoaderHelper::DestroySubsystem<UnityEngine.XR.XRInputSubsystem>()
inline void XRLoaderHelper_DestroySubsystem_TisXRInputSubsystem_tFECE6683FCAEBF05BAD05E5D612690095D8BAD34_m22B2454EB0F4E32155EEE6022768B9781DB0085F (XRLoaderHelper_tE96E7AE003148D5319D20BAD7E02654367E41DCC* __this, const RuntimeMethod* method)
{
	((  void (*) (XRLoaderHelper_tE96E7AE003148D5319D20BAD7E02654367E41DCC*, const RuntimeMethod*))XRLoaderHelper_DestroySubsystem_TisRuntimeObject_mF0CB81C6BD9DA12D6E8C21703A18E939389A1185_gshared)(__this, method);
}
// System.Void UnityEngine.Application::Quit()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Application_Quit_mE304382DB9A6455C2A474C8F364C7387F37E9281 (const RuntimeMethod* method) ;
// System.Boolean Unity.XR.Oculus.NativeMethods::LoadOVRPlugin(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool NativeMethods_LoadOVRPlugin_mE7DA65BF795C1E1E7DB69B59793C413556875F7F (String_t* ___0_ovrpPath, const RuntimeMethod* method) ;
// System.String UnityEngine.Application::get_unityVersion()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* Application_get_unityVersion_m27BB3207901305BD239E1C3A74035E15CF3E5D21 (const RuntimeMethod* method) ;
// System.Text.RegularExpressions.Match System.Text.RegularExpressions.Regex::Match(System.String,System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Match_tFBEBCF225BD8EA17BCE6CE3FE5C1BD8E3074105F* Regex_Match_mE3EC82B72BF82AA4B8749251C12C383047531972 (String_t* ___0_input, String_t* ___1_pattern, const RuntimeMethod* method) ;
// System.Boolean System.Text.RegularExpressions.Group::get_Success()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Group_get_Success_m4E0238EE4B1E7F927E2AF13E2E5901BCA92BE62F (Group_t26371E9136D6F43782C487B63C67C5FC4F472881* __this, const RuntimeMethod* method) ;
// System.Text.RegularExpressions.Group System.Text.RegularExpressions.GroupCollection::get_Item(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Group_t26371E9136D6F43782C487B63C67C5FC4F472881* GroupCollection_get_Item_mE9B3A83B3563620EF70CFCD5F13E404864351B7A (GroupCollection_tFFA1789730DD9EA122FBE77DC03BFEDCC3F2945E* __this, String_t* ___0_groupname, const RuntimeMethod* method) ;
// System.String System.Text.RegularExpressions.Capture::get_Value()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* Capture_get_Value_m1AB4193C2FC4B0D08AA34FECF10D03876D848BDC (Capture_tE11B735186DAFEE5F7A3BF5A739E9CCCE99DC24A* __this, const RuntimeMethod* method) ;
// System.Int32 System.Int32::Parse(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t Int32_Parse_m273CA1A9C7717C99641291A95C543711C0202AF0 (String_t* ___0_s, const RuntimeMethod* method) ;
// System.Void UnityEngine.XR.Management.XRLoaderHelper::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void XRLoaderHelper__ctor_mEAD9691DBE10C223AB11AB7056ED0B3BA59D7699 (XRLoaderHelper_tE96E7AE003148D5319D20BAD7E02654367E41DCC* __this, const RuntimeMethod* method) ;
// System.Void System.Collections.Generic.List`1<UnityEngine.XR.XRDisplaySubsystemDescriptor>::.ctor()
inline void List_1__ctor_m3E15C72C5BBB246B014CD4F0B141BD78A648B773 (List_1_tC3F021D09EFA4F3516555517B5E0D39308C9C1B4* __this, const RuntimeMethod* method)
{
	((  void (*) (List_1_tC3F021D09EFA4F3516555517B5E0D39308C9C1B4*, const RuntimeMethod*))List_1__ctor_m7F078BB342729BDF11327FD89D7872265328F690_gshared)(__this, method);
}
// System.Void System.Collections.Generic.List`1<UnityEngine.XR.XRInputSubsystemDescriptor>::.ctor()
inline void List_1__ctor_m2211D9F948E2002179E205222318FE448863F2CD (List_1_tE3AE94237CE649B47E1D52E1A3120E772255FF87* __this, const RuntimeMethod* method)
{
	((  void (*) (List_1_tE3AE94237CE649B47E1D52E1A3120E772255FF87*, const RuntimeMethod*))List_1__ctor_m7F078BB342729BDF11327FD89D7872265328F690_gshared)(__this, method);
}
// System.Int32 Unity.XR.Oculus.NativeMethods::SetCPULevel(System.Int32)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t NativeMethods_SetCPULevel_mC41C8161E7E18D20F81200D55D9E753E9C945DEF (int32_t ___0_cpuLevel, const RuntimeMethod* method) ;
// System.Int32 Unity.XR.Oculus.NativeMethods::SetGPULevel(System.Int32)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t NativeMethods_SetGPULevel_m2AC7CFBE5A721004B00E1584236A864BBF65EB3F (int32_t ___0_gpuLevel, const RuntimeMethod* method) ;
// System.Boolean Unity.XR.Oculus.NativeMethods::GetDisplayAvailableFrequencies(System.IntPtr,System.Int32&)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool NativeMethods_GetDisplayAvailableFrequencies_m7A3461EECCFE5AAFE6EA83A486D7DA8313098EEF (intptr_t ___0_ptr, int32_t* ___1_numFrequencies, const RuntimeMethod* method) ;
// System.IntPtr System.Runtime.InteropServices.Marshal::AllocHGlobal(System.Int32)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR intptr_t Marshal_AllocHGlobal_mE1D700DF967E28BE8AB3E0D67C81A96B4FCC8F4F (int32_t ___0_cb, const RuntimeMethod* method) ;
// System.Void System.Runtime.InteropServices.Marshal::Copy(System.IntPtr,System.Single[],System.Int32,System.Int32)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Marshal_Copy_m4744F803E7E605726758725D11D157455BD43775 (intptr_t ___0_source, SingleU5BU5D_t89DEFE97BCEDB5857010E79ECE0F52CF6E93B87C* ___1_destination, int32_t ___2_startIndex, int32_t ___3_length, const RuntimeMethod* method) ;
// System.Void System.Runtime.InteropServices.Marshal::FreeHGlobal(System.IntPtr)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Marshal_FreeHGlobal_m298EF0650E82E326EDA8048488DC384BB9171EB9 (intptr_t ___0_hglobal, const RuntimeMethod* method) ;
// System.Boolean Unity.XR.Oculus.NativeMethods::SetDisplayFrequency(System.Single)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool NativeMethods_SetDisplayFrequency_m1EF40C70A0CFA32EB0D5426681C13177C496F03F (float ___0_refreshRate, const RuntimeMethod* method) ;
// System.Boolean Unity.XR.Oculus.NativeMethods::GetDisplayFrequency(System.Single&)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool NativeMethods_GetDisplayFrequency_mB096687577976716563167FA72DD77F669A18C65 (float* ___0_refreshRate, const RuntimeMethod* method) ;
// System.Boolean System.String::Equals(System.String,System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool String_Equals_m3354EFE6393BED8DD6E18F69BEA131AAADCC622D (String_t* ___0_a, String_t* ___1_b, const RuntimeMethod* method) ;
// System.Void Unity.XR.Oculus.NativeMethods::GetOVRPVersion(System.Byte[])
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void NativeMethods_GetOVRPVersion_m38823D86CD59C69E62D5835B798C1E1BAAF696BA (ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* ___0_version, const RuntimeMethod* method) ;
// System.Int32 System.Array::IndexOf<System.Byte>(T[],T)
inline int32_t Array_IndexOf_TisByte_t94D9231AC217BE4D2E004C4CD32DF6D099EA41A3_m3E47AE0BD5292CA28182DF90A3A3B19D2DBE1B6F (ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* ___0_array, uint8_t ___1_value, const RuntimeMethod* method)
{
	return ((  int32_t (*) (ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031*, uint8_t, const RuntimeMethod*))Array_IndexOf_TisByte_t94D9231AC217BE4D2E004C4CD32DF6D099EA41A3_m3E47AE0BD5292CA28182DF90A3A3B19D2DBE1B6F_gshared)(___0_array, ___1_value, method);
}
// System.Text.Encoding System.Text.Encoding::get_ASCII()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Encoding_t65CDEF28CF20A7B8C92E85A4E808920C2465F095* Encoding_get_ASCII_mCC61B512D320FD4E2E71CC0DFDF8DDF3CD215C65 (const RuntimeMethod* method) ;
// System.Void System.Collections.Generic.List`1<UnityEngine.XR.XRDisplaySubsystem>::.ctor()
inline void List_1__ctor_mBE7647ECE0B8ABB952EDC379472F9E541D41D6DF (List_1_tA7666C6690CE2AEE97571615AD3AFCE2BB020597* __this, const RuntimeMethod* method)
{
	((  void (*) (List_1_tA7666C6690CE2AEE97571615AD3AFCE2BB020597*, const RuntimeMethod*))List_1__ctor_m7F078BB342729BDF11327FD89D7872265328F690_gshared)(__this, method);
}
// System.Void UnityEngine.SubsystemManager::GetInstances<UnityEngine.XR.XRDisplaySubsystem>(System.Collections.Generic.List`1<T>)
inline void SubsystemManager_GetInstances_TisXRDisplaySubsystem_t4B00B0BF1894A039ACFA8DDC2C2EB9301118C1F1_mCDFAF63EF2A2778CA3677E75360BC7961FCB3370 (List_1_tA7666C6690CE2AEE97571615AD3AFCE2BB020597* ___0_subsystems, const RuntimeMethod* method)
{
	((  void (*) (List_1_tA7666C6690CE2AEE97571615AD3AFCE2BB020597*, const RuntimeMethod*))SubsystemManager_GetInstances_TisRuntimeObject_mCBA01F889BE624767358AB5494CD2D7F1B2FB064_gshared)(___0_subsystems, method);
}
// System.Collections.Generic.List`1/Enumerator<T> System.Collections.Generic.List`1<UnityEngine.XR.XRDisplaySubsystem>::GetEnumerator()
inline Enumerator_t7B44DEF95515943B67640F1A20853509F98BA143 List_1_GetEnumerator_mA450E85CB8D7D5CB81FAAF9D11A1D4945B942423 (List_1_tA7666C6690CE2AEE97571615AD3AFCE2BB020597* __this, const RuntimeMethod* method)
{
	return ((  Enumerator_t7B44DEF95515943B67640F1A20853509F98BA143 (*) (List_1_tA7666C6690CE2AEE97571615AD3AFCE2BB020597*, const RuntimeMethod*))List_1_GetEnumerator_mD8294A7FA2BEB1929487127D476F8EC1CDC23BFC_gshared)(__this, method);
}
// System.Void System.Collections.Generic.List`1/Enumerator<UnityEngine.XR.XRDisplaySubsystem>::Dispose()
inline void Enumerator_Dispose_m25A1E45E653D7F73DF65C041A66C0A6E01F2964D (Enumerator_t7B44DEF95515943B67640F1A20853509F98BA143* __this, const RuntimeMethod* method)
{
	((  void (*) (Enumerator_t7B44DEF95515943B67640F1A20853509F98BA143*, const RuntimeMethod*))Enumerator_Dispose_mD9DC3E3C3697830A4823047AB29A77DBBB5ED419_gshared)(__this, method);
}
// T System.Collections.Generic.List`1/Enumerator<UnityEngine.XR.XRDisplaySubsystem>::get_Current()
inline XRDisplaySubsystem_t4B00B0BF1894A039ACFA8DDC2C2EB9301118C1F1* Enumerator_get_Current_mF9383BAD37E56B1D5BCDBFF0C3ADA58BB6E67A04_inline (Enumerator_t7B44DEF95515943B67640F1A20853509F98BA143* __this, const RuntimeMethod* method)
{
	return ((  XRDisplaySubsystem_t4B00B0BF1894A039ACFA8DDC2C2EB9301118C1F1* (*) (Enumerator_t7B44DEF95515943B67640F1A20853509F98BA143*, const RuntimeMethod*))Enumerator_get_Current_m6330F15D18EE4F547C05DF9BF83C5EB710376027_gshared_inline)(__this, method);
}
// TSubsystemDescriptor UnityEngine.IntegratedSubsystem`1<UnityEngine.XR.XRDisplaySubsystemDescriptor>::get_SubsystemDescriptor()
inline XRDisplaySubsystemDescriptor_t72DD88EE9094488AE723A495F48884BA4EA8311A* IntegratedSubsystem_1_get_SubsystemDescriptor_mF302CEB5B3EFD7EFCB9FC2B54CF739543AB2CFF3 (IntegratedSubsystem_1_t8312865F01EEA1EDE4B24A973E47ADD526616848* __this, const RuntimeMethod* method)
{
	return ((  XRDisplaySubsystemDescriptor_t72DD88EE9094488AE723A495F48884BA4EA8311A* (*) (IntegratedSubsystem_1_t8312865F01EEA1EDE4B24A973E47ADD526616848*, const RuntimeMethod*))IntegratedSubsystem_1_get_SubsystemDescriptor_mC6FFC8B129C4D3146C76A9D0B2CE69811F0F5AB8_gshared)(__this, method);
}
// System.String UnityEngine.IntegratedSubsystemDescriptor::get_id()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* IntegratedSubsystemDescriptor_get_id_m89DBA940C79ED7EFE1137E3EC4A5A53BF7052F15 (IntegratedSubsystemDescriptor_t9232963B842E01748A8E032928DC8E35DF00C10D* __this, const RuntimeMethod* method) ;
// System.Boolean UnityEngine.IntegratedSubsystem::get_running()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool IntegratedSubsystem_get_running_m18AA0D7AD1CB593DC9EE5F3DC79643717509D6E8 (IntegratedSubsystem_t990160A89854D87C0836DC589B720231C02D4CE3* __this, const RuntimeMethod* method) ;
// System.Boolean System.Collections.Generic.List`1/Enumerator<UnityEngine.XR.XRDisplaySubsystem>::MoveNext()
inline bool Enumerator_MoveNext_m9A1452618AC875C7A20A5917509A7B90E76E6A6A (Enumerator_t7B44DEF95515943B67640F1A20853509F98BA143* __this, const RuntimeMethod* method)
{
	return ((  bool (*) (Enumerator_t7B44DEF95515943B67640F1A20853509F98BA143*, const RuntimeMethod*))Enumerator_MoveNext_mE921CC8F29FBBDE7CC3209A0ED0D921D58D00BCB_gshared)(__this, method);
}
// UnityEngine.IntegratedSubsystem Unity.XR.Oculus.Stats::GetOculusDisplaySubsystem()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR IntegratedSubsystem_t990160A89854D87C0836DC589B720231C02D4CE3* Stats_GetOculusDisplaySubsystem_m9A09B8096996A92963F6226D54D2C929819D5829 (const RuntimeMethod* method) ;
// System.Boolean UnityEngine.XR.XRDisplaySubsystem::TryGetAppGPUTimeLastFrame(System.Single&)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool XRDisplaySubsystem_TryGetAppGPUTimeLastFrame_m17BABDCD1716475A2473143A47ECCFE64F17A766 (XRDisplaySubsystem_t4B00B0BF1894A039ACFA8DDC2C2EB9301118C1F1* __this, float* ___0_gpuTimeLastFrame, const RuntimeMethod* method) ;
// System.Boolean UnityEngine.XR.XRDisplaySubsystem::TryGetCompositorGPUTimeLastFrame(System.Single&)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool XRDisplaySubsystem_TryGetCompositorGPUTimeLastFrame_m9D8587317EBC9785C2AEAD86ACC3B939A497E82D (XRDisplaySubsystem_t4B00B0BF1894A039ACFA8DDC2C2EB9301118C1F1* __this, float* ___0_gpuTimeLastFrameCompositor, const RuntimeMethod* method) ;
// System.Boolean UnityEngine.XR.XRDisplaySubsystem::TryGetMotionToPhoton(System.Single&)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool XRDisplaySubsystem_TryGetMotionToPhoton_mEF734C48F96C17C3A63305B75988F1851298D3E6 (XRDisplaySubsystem_t4B00B0BF1894A039ACFA8DDC2C2EB9301118C1F1* __this, float* ___0_motionToPhoton, const RuntimeMethod* method) ;
// System.Boolean UnityEngine.XR.XRDisplaySubsystem::TryGetDisplayRefreshRate(System.Single&)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool XRDisplaySubsystem_TryGetDisplayRefreshRate_mC92C72C1B54E33C4281B714483222D5CD11866BB (XRDisplaySubsystem_t4B00B0BF1894A039ACFA8DDC2C2EB9301118C1F1* __this, float* ___0_displayRefreshRate, const RuntimeMethod* method) ;
// System.Boolean UnityEngine.XR.Provider.XRStats::TryGetStat(UnityEngine.IntegratedSubsystem,System.String,System.Single&)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool XRStats_TryGetStat_mE91924B240A89BA396554CE3742A06210514FA8B (IntegratedSubsystem_t990160A89854D87C0836DC589B720231C02D4CE3* ___0_xrSubsystem, String_t* ___1_tag, float* ___2_value, const RuntimeMethod* method) ;
// System.Void Unity.XR.Oculus.NativeMethods::EnablePerfMetrics(System.Boolean)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void NativeMethods_EnablePerfMetrics_m0701876E6E45D0BFEB5D2695338AD29090AAAC5C (bool ___0_enable, const RuntimeMethod* method) ;
// System.Void Unity.XR.Oculus.NativeMethods::EnableAppMetrics(System.Boolean)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void NativeMethods_EnableAppMetrics_m2B18E67E20FD049593C2A332FB34E9AB9558B719 (bool ___0_enable, const RuntimeMethod* method) ;
// System.Void Unity.XR.Oculus.NativeMethods/Internal::SetColorScale(System.Single,System.Single,System.Single,System.Single)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Internal_SetColorScale_m0CF78314E44A665ECFD2F23F091AF8698EF671A5 (float ___0_x, float ___1_y, float ___2_z, float ___3_w, const RuntimeMethod* method) ;
// System.Void Unity.XR.Oculus.NativeMethods/Internal::SetColorOffset(System.Single,System.Single,System.Single,System.Single)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Internal_SetColorOffset_mB2961D46D55669467F0C3DEEAD4502F28248E984 (float ___0_x, float ___1_y, float ___2_z, float ___3_w, const RuntimeMethod* method) ;
// System.Boolean Unity.XR.Oculus.NativeMethods/Internal::GetIsSupportedDevice()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Internal_GetIsSupportedDevice_m737F888EF5B63C9A5205B97FC0555CBED941568E (const RuntimeMethod* method) ;
// System.Boolean Unity.XR.Oculus.NativeMethods/Internal::LoadOVRPlugin(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Internal_LoadOVRPlugin_m9C05A911848136E203C79089AB828E9F8B094BA8 (String_t* ___0_ovrpPath, const RuntimeMethod* method) ;
// System.Void Unity.XR.Oculus.NativeMethods/Internal::UnloadOVRPlugin()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Internal_UnloadOVRPlugin_mADAC9DD8335CFB22730ECF073A49BC8C89562613 (const RuntimeMethod* method) ;
// System.Void Unity.XR.Oculus.NativeMethods/Internal::SetUserDefinedSettings(Unity.XR.Oculus.NativeMethods/UserDefinedSettings)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Internal_SetUserDefinedSettings_mFB3E779348369441A2822EAEB2D45F728C5C1BEC (UserDefinedSettings_tE823040D57E8C8C9D8A028F206230E1C7BAC8EE3 ___0_settings, const RuntimeMethod* method) ;
// System.Void Unity.XR.Oculus.NativeMethods/Internal::SetHasUserAuthorizedEyeTrackingPermission(System.Boolean)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Internal_SetHasUserAuthorizedEyeTrackingPermission_m4C206014E71D2A988E9E7D8848EA7F4B10D144F8 (bool ___0_authorized, const RuntimeMethod* method) ;
// System.Int32 Unity.XR.Oculus.NativeMethods/Internal::SetCPULevel(System.Int32)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t Internal_SetCPULevel_m50846DBC8F018A48D7E85322474C7ABC1294CA27 (int32_t ___0_cpuLevel, const RuntimeMethod* method) ;
// System.Int32 Unity.XR.Oculus.NativeMethods/Internal::SetGPULevel(System.Int32)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t Internal_SetGPULevel_m37B7AB9351972BD300DC4082CBD7BD9B3ED0E7E8 (int32_t ___0_gpuLevel, const RuntimeMethod* method) ;
// System.Void Unity.XR.Oculus.NativeMethods/Internal::GetOVRPVersion(System.Byte[])
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Internal_GetOVRPVersion_m0520E7DF32DAF9849741C5021281EE5E08648E5B (ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* ___0_version, const RuntimeMethod* method) ;
// System.Void Unity.XR.Oculus.NativeMethods/Internal::EnablePerfMetrics(System.Boolean)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Internal_EnablePerfMetrics_mA09098817AD178FB7866F322161AA2E96A0D9443 (bool ___0_enable, const RuntimeMethod* method) ;
// System.Void Unity.XR.Oculus.NativeMethods/Internal::EnableAppMetrics(System.Boolean)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Internal_EnableAppMetrics_m1BA9E761EE232812C41F2768308D2E6BA559965C (bool ___0_enable, const RuntimeMethod* method) ;
// System.Boolean Unity.XR.Oculus.NativeMethods/Internal::SetDeveloperModeStrict(System.Boolean)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Internal_SetDeveloperModeStrict_m0A501B72BA2BFBB119B94006225C227206A631C1 (bool ___0_active, const RuntimeMethod* method) ;
// System.Boolean Unity.XR.Oculus.NativeMethods/Internal::GetAppHasInputFocus()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Internal_GetAppHasInputFocus_m0569830C6EDC013793FE3C523FBA23517DB07A0B (const RuntimeMethod* method) ;
// System.Boolean Unity.XR.Oculus.NativeMethods/Internal::GetBoundaryConfigured()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Internal_GetBoundaryConfigured_m449078868F8DECD125BEBA07D1D1D7EB9766E9AF (const RuntimeMethod* method) ;
// System.Boolean Unity.XR.Oculus.NativeMethods/Internal::GetBoundaryDimensions(Unity.XR.Oculus.Boundary/BoundaryType,UnityEngine.Vector3&)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Internal_GetBoundaryDimensions_mAE3195FBFDE6A3213BA8748376FB0F8B7554754B (int32_t ___0_boundaryType, Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2* ___1_dimensions, const RuntimeMethod* method) ;
// System.Boolean Unity.XR.Oculus.NativeMethods/Internal::GetBoundaryVisible()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Internal_GetBoundaryVisible_mCD829BA8761743E005786D1B6BE9345F01A6CF3F (const RuntimeMethod* method) ;
// System.Void Unity.XR.Oculus.NativeMethods/Internal::SetBoundaryVisible(System.Boolean)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Internal_SetBoundaryVisible_mD04D1930919267DE52554242A2AA7B3D9955E193 (bool ___0_boundaryVisible, const RuntimeMethod* method) ;
// System.Boolean Unity.XR.Oculus.NativeMethods/Internal::GetAppShouldQuit()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Internal_GetAppShouldQuit_m969E969C8CB61307597152EAFAA9251E145B48E2 (const RuntimeMethod* method) ;
// System.Boolean Unity.XR.Oculus.NativeMethods/Internal::GetDisplayAvailableFrequencies(System.IntPtr,System.Int32&)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Internal_GetDisplayAvailableFrequencies_m265CF48B6D6244BD5999ED28F4417922A3964BCC (intptr_t ___0_ptr, int32_t* ___1_numFrequencies, const RuntimeMethod* method) ;
// System.Boolean Unity.XR.Oculus.NativeMethods/Internal::SetDisplayFrequency(System.Single)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Internal_SetDisplayFrequency_m6E8BE138EDF3AD30421E8A299597827A81F86804 (float ___0_refreshRate, const RuntimeMethod* method) ;
// System.Boolean Unity.XR.Oculus.NativeMethods/Internal::GetDisplayFrequency(System.Single&)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Internal_GetDisplayFrequency_mF645008727871526DAE4877348FD48E0AEFE6316 (float* ___0_refreshRate, const RuntimeMethod* method) ;
// Unity.XR.Oculus.SystemHeadset Unity.XR.Oculus.NativeMethods/Internal::GetSystemHeadsetType()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t Internal_GetSystemHeadsetType_m0901C44C3A772A62308D273AA37CF55BA6CEE646 (const RuntimeMethod* method) ;
// System.Boolean Unity.XR.Oculus.NativeMethods/Internal::GetTiledMultiResSupported()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Internal_GetTiledMultiResSupported_m7A36253D53F4DA8D56E1EFA242457D3771220FBF (const RuntimeMethod* method) ;
// System.Void Unity.XR.Oculus.NativeMethods/Internal::SetTiledMultiResLevel(System.Int32)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Internal_SetTiledMultiResLevel_m59849D5EF74DE2C7F25813486E3ED47C947F15C3 (int32_t ___0_level, const RuntimeMethod* method) ;
// System.Int32 Unity.XR.Oculus.NativeMethods/Internal::GetTiledMultiResLevel()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t Internal_GetTiledMultiResLevel_mC95B03E83E572C960C79746554C070E91A43F8BC (const RuntimeMethod* method) ;
// System.Void Unity.XR.Oculus.NativeMethods/Internal::SetTiledMultiResDynamic(System.Boolean)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Internal_SetTiledMultiResDynamic_m92E2C4C0C862B7A30582EE5B96006FFE3367D658 (bool ___0_isDynamic, const RuntimeMethod* method) ;
// System.Boolean Unity.XR.Oculus.NativeMethods/Internal::GetEyeTrackedFoveatedRenderingSupported()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Internal_GetEyeTrackedFoveatedRenderingSupported_mA2F9106F5BD6571BB7D19D2FFDF61EF36D33F0E5 (const RuntimeMethod* method) ;
// System.Void UnityEngine.ScriptableObject::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ScriptableObject__ctor_mD037FDB0B487295EA47F79A4DB1BF1846C9087FF (ScriptableObject_tB3BFDB921A1B1795B38A5417D3B97A89A140436A* __this, const RuntimeMethod* method) ;
// System.Void UnityEngine.XR.InputFeatureUsage`1<System.Boolean>::.ctor(System.String)
inline void InputFeatureUsage_1__ctor_mEB36F8937385A1065CD9F48AE2DAD9EAE49EFCE7 (InputFeatureUsage_1_tE336B2F0B9AC721519BFA17A08D6353FD5221637* __this, String_t* ___0_usageName, const RuntimeMethod* method)
{
	((  void (*) (InputFeatureUsage_1_tE336B2F0B9AC721519BFA17A08D6353FD5221637*, String_t*, const RuntimeMethod*))InputFeatureUsage_1__ctor_mEB36F8937385A1065CD9F48AE2DAD9EAE49EFCE7_gshared)(__this, ___0_usageName, method);
}
// System.Void UnityEngine.Events.UnityAction::.ctor(System.Object,System.IntPtr)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void UnityAction__ctor_mC53E20D6B66E0D5688CD81B88DBB34F5A58B7131 (UnityAction_t11A1F3B953B365C072A5DCC32677EE1796A962A7* __this, RuntimeObject* ___0_object, intptr_t ___1_method, const RuntimeMethod* method) ;
// System.Void UnityEngine.Application::add_onBeforeRender(UnityEngine.Events.UnityAction)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Application_add_onBeforeRender_mEE8925294C807AD08FA0FF35D4C663E098510394 (UnityAction_t11A1F3B953B365C072A5DCC32677EE1796A962A7* ___0_value, const RuntimeMethod* method) ;
// System.Void UnityEngine.Application::remove_onBeforeRender(UnityEngine.Events.UnityAction)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Application_remove_onBeforeRender_m9F54448ED4059A26C9972E5C9ED2F6DCD58B4E24 (UnityAction_t11A1F3B953B365C072A5DCC32677EE1796A962A7* ___0_value, const RuntimeMethod* method) ;
// System.Void Unity.XR.Oculus.InputFocus::Update()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void InputFocus_Update_mAA7C0E54D60D5D05EF693009DA4AFE7C63BE044A (const RuntimeMethod* method) ;
#if FORCE_PINVOKE_INTERNAL || FORCE_PINVOKE_OculusXRPlugin_INTERNAL
IL2CPP_EXTERN_C void DEFAULT_CALL SetColorScale(float, float, float, float);
#endif
#if FORCE_PINVOKE_INTERNAL || FORCE_PINVOKE_OculusXRPlugin_INTERNAL
IL2CPP_EXTERN_C void DEFAULT_CALL SetColorOffset(float, float, float, float);
#endif
#if FORCE_PINVOKE_INTERNAL || FORCE_PINVOKE_OculusXRPlugin_INTERNAL
IL2CPP_EXTERN_C int32_t DEFAULT_CALL GetIsSupportedDevice();
#endif
#if FORCE_PINVOKE_INTERNAL || FORCE_PINVOKE_OculusXRPlugin_INTERNAL
IL2CPP_EXTERN_C int32_t DEFAULT_CALL LoadOVRPlugin(Il2CppChar*);
#endif
#if FORCE_PINVOKE_INTERNAL || FORCE_PINVOKE_OculusXRPlugin_INTERNAL
IL2CPP_EXTERN_C void DEFAULT_CALL UnloadOVRPlugin();
#endif
#if FORCE_PINVOKE_INTERNAL || FORCE_PINVOKE_OculusXRPlugin_INTERNAL
IL2CPP_EXTERN_C void DEFAULT_CALL SetUserDefinedSettings(UserDefinedSettings_tE823040D57E8C8C9D8A028F206230E1C7BAC8EE3);
#endif
#if FORCE_PINVOKE_INTERNAL || FORCE_PINVOKE_OculusXRPlugin_INTERNAL
IL2CPP_EXTERN_C void DEFAULT_CALL SetHasUserAuthorizedEyeTrackingPermission(int32_t);
#endif
#if FORCE_PINVOKE_INTERNAL || FORCE_PINVOKE_OculusXRPlugin_INTERNAL
IL2CPP_EXTERN_C int32_t DEFAULT_CALL SetCPULevel(int32_t);
#endif
#if FORCE_PINVOKE_INTERNAL || FORCE_PINVOKE_OculusXRPlugin_INTERNAL
IL2CPP_EXTERN_C int32_t DEFAULT_CALL SetGPULevel(int32_t);
#endif
#if FORCE_PINVOKE_INTERNAL || FORCE_PINVOKE_OculusXRPlugin_INTERNAL
IL2CPP_EXTERN_C void DEFAULT_CALL GetOVRPVersion(uint8_t*);
#endif
#if FORCE_PINVOKE_INTERNAL || FORCE_PINVOKE_OculusXRPlugin_INTERNAL
IL2CPP_EXTERN_C void DEFAULT_CALL EnablePerfMetrics(int32_t);
#endif
#if FORCE_PINVOKE_INTERNAL || FORCE_PINVOKE_OculusXRPlugin_INTERNAL
IL2CPP_EXTERN_C void DEFAULT_CALL EnableAppMetrics(int32_t);
#endif
#if FORCE_PINVOKE_INTERNAL || FORCE_PINVOKE_OculusXRPlugin_INTERNAL
IL2CPP_EXTERN_C int32_t DEFAULT_CALL SetDeveloperModeStrict(int32_t);
#endif
#if FORCE_PINVOKE_INTERNAL || FORCE_PINVOKE_OculusXRPlugin_INTERNAL
IL2CPP_EXTERN_C int32_t DEFAULT_CALL GetAppHasInputFocus();
#endif
#if FORCE_PINVOKE_INTERNAL || FORCE_PINVOKE_OculusXRPlugin_INTERNAL
IL2CPP_EXTERN_C int32_t DEFAULT_CALL GetBoundaryConfigured();
#endif
#if FORCE_PINVOKE_INTERNAL || FORCE_PINVOKE_OculusXRPlugin_INTERNAL
IL2CPP_EXTERN_C int32_t DEFAULT_CALL GetBoundaryDimensions(int32_t, Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2*);
#endif
#if FORCE_PINVOKE_INTERNAL || FORCE_PINVOKE_OculusXRPlugin_INTERNAL
IL2CPP_EXTERN_C int32_t DEFAULT_CALL GetBoundaryVisible();
#endif
#if FORCE_PINVOKE_INTERNAL || FORCE_PINVOKE_OculusXRPlugin_INTERNAL
IL2CPP_EXTERN_C void DEFAULT_CALL SetBoundaryVisible(int32_t);
#endif
#if FORCE_PINVOKE_INTERNAL || FORCE_PINVOKE_OculusXRPlugin_INTERNAL
IL2CPP_EXTERN_C int32_t DEFAULT_CALL GetAppShouldQuit();
#endif
#if FORCE_PINVOKE_INTERNAL || FORCE_PINVOKE_OculusXRPlugin_INTERNAL
IL2CPP_EXTERN_C int32_t DEFAULT_CALL GetDisplayAvailableFrequencies(intptr_t, int32_t*);
#endif
#if FORCE_PINVOKE_INTERNAL || FORCE_PINVOKE_OculusXRPlugin_INTERNAL
IL2CPP_EXTERN_C int32_t DEFAULT_CALL SetDisplayFrequency(float);
#endif
#if FORCE_PINVOKE_INTERNAL || FORCE_PINVOKE_OculusXRPlugin_INTERNAL
IL2CPP_EXTERN_C int32_t DEFAULT_CALL GetDisplayFrequency(float*);
#endif
#if FORCE_PINVOKE_INTERNAL || FORCE_PINVOKE_OculusXRPlugin_INTERNAL
IL2CPP_EXTERN_C int32_t DEFAULT_CALL GetSystemHeadsetType();
#endif
#if FORCE_PINVOKE_INTERNAL || FORCE_PINVOKE_OculusXRPlugin_INTERNAL
IL2CPP_EXTERN_C int32_t DEFAULT_CALL GetTiledMultiResSupported();
#endif
#if FORCE_PINVOKE_INTERNAL || FORCE_PINVOKE_OculusXRPlugin_INTERNAL
IL2CPP_EXTERN_C void DEFAULT_CALL SetTiledMultiResLevel(int32_t);
#endif
#if FORCE_PINVOKE_INTERNAL || FORCE_PINVOKE_OculusXRPlugin_INTERNAL
IL2CPP_EXTERN_C int32_t DEFAULT_CALL GetTiledMultiResLevel();
#endif
#if FORCE_PINVOKE_INTERNAL || FORCE_PINVOKE_OculusXRPlugin_INTERNAL
IL2CPP_EXTERN_C void DEFAULT_CALL SetTiledMultiResDynamic(int32_t);
#endif
#if FORCE_PINVOKE_INTERNAL || FORCE_PINVOKE_OculusXRPlugin_INTERNAL
IL2CPP_EXTERN_C int32_t DEFAULT_CALL GetEyeTrackedFoveatedRenderingSupported();
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
// System.Void Unity.XR.Oculus.Utils::SetColorScaleAndOffset(UnityEngine.Vector4,UnityEngine.Vector4)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Utils_SetColorScaleAndOffset_m75F70D601D8634D1B8016898EAA699820F738EF2 (Vector4_t58B63D32F48C0DBF50DE2C60794C4676C80EDBE3 ___0_colorScale, Vector4_t58B63D32F48C0DBF50DE2C60794C4676C80EDBE3 ___1_colorOffset, const RuntimeMethod* method) 
{
	{
		// NativeMethods.SetColorScale(colorScale.x, colorScale.y, colorScale.z, colorScale.w);
		Vector4_t58B63D32F48C0DBF50DE2C60794C4676C80EDBE3 L_0 = ___0_colorScale;
		float L_1 = L_0.___x_1;
		Vector4_t58B63D32F48C0DBF50DE2C60794C4676C80EDBE3 L_2 = ___0_colorScale;
		float L_3 = L_2.___y_2;
		Vector4_t58B63D32F48C0DBF50DE2C60794C4676C80EDBE3 L_4 = ___0_colorScale;
		float L_5 = L_4.___z_3;
		Vector4_t58B63D32F48C0DBF50DE2C60794C4676C80EDBE3 L_6 = ___0_colorScale;
		float L_7 = L_6.___w_4;
		NativeMethods_SetColorScale_m71466C371AE4D8AE0393FCA19BCB93DFF87FCF9B(L_1, L_3, L_5, L_7, NULL);
		// NativeMethods.SetColorOffset(colorOffset.x, colorOffset.y, colorOffset.z, colorOffset.w);
		Vector4_t58B63D32F48C0DBF50DE2C60794C4676C80EDBE3 L_8 = ___1_colorOffset;
		float L_9 = L_8.___x_1;
		Vector4_t58B63D32F48C0DBF50DE2C60794C4676C80EDBE3 L_10 = ___1_colorOffset;
		float L_11 = L_10.___y_2;
		Vector4_t58B63D32F48C0DBF50DE2C60794C4676C80EDBE3 L_12 = ___1_colorOffset;
		float L_13 = L_12.___z_3;
		Vector4_t58B63D32F48C0DBF50DE2C60794C4676C80EDBE3 L_14 = ___1_colorOffset;
		float L_15 = L_14.___w_4;
		NativeMethods_SetColorOffset_m708A9D66EF8DBDDBD1DB4F5FFCC572F007742BF8(L_9, L_11, L_13, L_15, NULL);
		// }
		return;
	}
}
// Unity.XR.Oculus.SystemHeadset Unity.XR.Oculus.Utils::GetSystemHeadsetType()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t Utils_GetSystemHeadsetType_m9DA59EDFF28E5445F351BFE0C6574F12E07FDD91 (const RuntimeMethod* method) 
{
	{
		// return NativeMethods.GetSystemHeadsetType();
		int32_t L_0;
		L_0 = NativeMethods_GetSystemHeadsetType_mD18D6819F35C667E7E150E538A884850C6E89A4E(NULL);
		return L_0;
	}
}
// System.Boolean Unity.XR.Oculus.Utils::SetFoveationLevel(System.Int32)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Utils_SetFoveationLevel_m133D8F1FA3820637CF3AA54083D54636B85E3644 (int32_t ___0_level, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral8D4C1624EBCE886FC4F782A22F67C15E64EF0BE1);
		s_Il2CppMethodInitialized = true;
	}
	{
		// if (!NativeMethods.GetTiledMultiResSupported())
		bool L_0;
		L_0 = NativeMethods_GetTiledMultiResSupported_mBEFDE3CDA036B5D2B2FADCCCC791C6035E711970(NULL);
		if (L_0)
		{
			goto IL_0013;
		}
	}
	{
		// Debug.LogWarning("Can't set foveation level on current platform");
		il2cpp_codegen_runtime_class_init_inline(Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var);
		Debug_LogWarning_m33EF1B897E0C7C6FF538989610BFAFFEF4628CA9(_stringLiteral8D4C1624EBCE886FC4F782A22F67C15E64EF0BE1, NULL);
		// return false;
		return (bool)0;
	}

IL_0013:
	{
		// NativeMethods.SetTiledMultiResLevel(level);
		int32_t L_1 = ___0_level;
		NativeMethods_SetTiledMultiResLevel_mAA62ED9D93CA810457DE6B4E90DD27B877739D7B(L_1, NULL);
		// return true;
		return (bool)1;
	}
}
// System.Boolean Unity.XR.Oculus.Utils::EnableDynamicFFR(System.Boolean)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Utils_EnableDynamicFFR_m1E1AFFB8CBC78A75C3AB9C8EE1D53002E0176B88 (bool ___0_enable, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral039FC8798456705B4F372FB22E7B8A75FE2E6D6D);
		s_Il2CppMethodInitialized = true;
	}
	{
		// if (!NativeMethods.GetTiledMultiResSupported())
		bool L_0;
		L_0 = NativeMethods_GetTiledMultiResSupported_mBEFDE3CDA036B5D2B2FADCCCC791C6035E711970(NULL);
		if (L_0)
		{
			goto IL_0013;
		}
	}
	{
		// Debug.LogWarning("Can't enable dynamic FFR on current platform");
		il2cpp_codegen_runtime_class_init_inline(Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var);
		Debug_LogWarning_m33EF1B897E0C7C6FF538989610BFAFFEF4628CA9(_stringLiteral039FC8798456705B4F372FB22E7B8A75FE2E6D6D, NULL);
		// return false;
		return (bool)0;
	}

IL_0013:
	{
		// NativeMethods.SetTiledMultiResDynamic(enable);
		bool L_1 = ___0_enable;
		NativeMethods_SetTiledMultiResDynamic_m8FC41964A4A054ADC21137FDD47C03248139FA05(L_1, NULL);
		// return true;
		return (bool)1;
	}
}
// System.Int32 Unity.XR.Oculus.Utils::GetFoveationLevel()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t Utils_GetFoveationLevel_m33A6944E95E4EC3DDC80216F88064A68F8C12E7B (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral0E33CA6894EABEA68F4151858D5322F8246508A3);
		s_Il2CppMethodInitialized = true;
	}
	{
		// if (!NativeMethods.GetTiledMultiResSupported())
		bool L_0;
		L_0 = NativeMethods_GetTiledMultiResSupported_mBEFDE3CDA036B5D2B2FADCCCC791C6035E711970(NULL);
		if (L_0)
		{
			goto IL_0013;
		}
	}
	{
		// Debug.LogWarning("Can't get foveation level on current platform");
		il2cpp_codegen_runtime_class_init_inline(Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var);
		Debug_LogWarning_m33EF1B897E0C7C6FF538989610BFAFFEF4628CA9(_stringLiteral0E33CA6894EABEA68F4151858D5322F8246508A3, NULL);
		// return -1;
		return (-1);
	}

IL_0013:
	{
		// return NativeMethods.GetTiledMultiResLevel();
		int32_t L_1;
		L_1 = NativeMethods_GetTiledMultiResLevel_mA712F64DAA92C2D399980A48ABFE282E92887012(NULL);
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
// System.Void Unity.XR.Oculus.InputFocus::add_InputFocusAcquired(System.Action)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void InputFocus_add_InputFocusAcquired_mA8C7E7D598DD5231B794344BAD8E5A045903ECBB (Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* ___0_value, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&InputFocus_t76018DDD56BB288561D354B825D5929965DC38FE_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* V_0 = NULL;
	Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* V_1 = NULL;
	Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* V_2 = NULL;
	{
		Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* L_0 = ((InputFocus_t76018DDD56BB288561D354B825D5929965DC38FE_StaticFields*)il2cpp_codegen_static_fields_for(InputFocus_t76018DDD56BB288561D354B825D5929965DC38FE_il2cpp_TypeInfo_var))->___InputFocusAcquired_0;
		V_0 = L_0;
	}

IL_0006:
	{
		Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* L_1 = V_0;
		V_1 = L_1;
		Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* L_2 = V_1;
		Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* L_3 = ___0_value;
		Delegate_t* L_4;
		L_4 = Delegate_Combine_m1F725AEF318BE6F0426863490691A6F4606E7D00(L_2, L_3, NULL);
		V_2 = ((Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07*)CastclassSealed((RuntimeObject*)L_4, Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07_il2cpp_TypeInfo_var));
		Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* L_5 = V_2;
		Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* L_6 = V_1;
		Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* L_7;
		L_7 = InterlockedCompareExchangeImpl<Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07*>((&((InputFocus_t76018DDD56BB288561D354B825D5929965DC38FE_StaticFields*)il2cpp_codegen_static_fields_for(InputFocus_t76018DDD56BB288561D354B825D5929965DC38FE_il2cpp_TypeInfo_var))->___InputFocusAcquired_0), L_5, L_6);
		V_0 = L_7;
		Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* L_8 = V_0;
		Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* L_9 = V_1;
		if ((!(((RuntimeObject*)(Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07*)L_8) == ((RuntimeObject*)(Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07*)L_9))))
		{
			goto IL_0006;
		}
	}
	{
		return;
	}
}
// System.Void Unity.XR.Oculus.InputFocus::remove_InputFocusAcquired(System.Action)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void InputFocus_remove_InputFocusAcquired_mCDA79AF7C64903CAC8203CD80695BFFFF41FB1CD (Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* ___0_value, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&InputFocus_t76018DDD56BB288561D354B825D5929965DC38FE_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* V_0 = NULL;
	Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* V_1 = NULL;
	Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* V_2 = NULL;
	{
		Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* L_0 = ((InputFocus_t76018DDD56BB288561D354B825D5929965DC38FE_StaticFields*)il2cpp_codegen_static_fields_for(InputFocus_t76018DDD56BB288561D354B825D5929965DC38FE_il2cpp_TypeInfo_var))->___InputFocusAcquired_0;
		V_0 = L_0;
	}

IL_0006:
	{
		Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* L_1 = V_0;
		V_1 = L_1;
		Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* L_2 = V_1;
		Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* L_3 = ___0_value;
		Delegate_t* L_4;
		L_4 = Delegate_Remove_m8B7DD5661308FA972E23CA1CC3FC9CEB355504E3(L_2, L_3, NULL);
		V_2 = ((Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07*)CastclassSealed((RuntimeObject*)L_4, Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07_il2cpp_TypeInfo_var));
		Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* L_5 = V_2;
		Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* L_6 = V_1;
		Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* L_7;
		L_7 = InterlockedCompareExchangeImpl<Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07*>((&((InputFocus_t76018DDD56BB288561D354B825D5929965DC38FE_StaticFields*)il2cpp_codegen_static_fields_for(InputFocus_t76018DDD56BB288561D354B825D5929965DC38FE_il2cpp_TypeInfo_var))->___InputFocusAcquired_0), L_5, L_6);
		V_0 = L_7;
		Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* L_8 = V_0;
		Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* L_9 = V_1;
		if ((!(((RuntimeObject*)(Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07*)L_8) == ((RuntimeObject*)(Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07*)L_9))))
		{
			goto IL_0006;
		}
	}
	{
		return;
	}
}
// System.Void Unity.XR.Oculus.InputFocus::add_InputFocusLost(System.Action)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void InputFocus_add_InputFocusLost_mA3BAC183DE3A481D20D32439CDE3399CFE0CF6CD (Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* ___0_value, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&InputFocus_t76018DDD56BB288561D354B825D5929965DC38FE_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* V_0 = NULL;
	Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* V_1 = NULL;
	Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* V_2 = NULL;
	{
		Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* L_0 = ((InputFocus_t76018DDD56BB288561D354B825D5929965DC38FE_StaticFields*)il2cpp_codegen_static_fields_for(InputFocus_t76018DDD56BB288561D354B825D5929965DC38FE_il2cpp_TypeInfo_var))->___InputFocusLost_1;
		V_0 = L_0;
	}

IL_0006:
	{
		Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* L_1 = V_0;
		V_1 = L_1;
		Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* L_2 = V_1;
		Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* L_3 = ___0_value;
		Delegate_t* L_4;
		L_4 = Delegate_Combine_m1F725AEF318BE6F0426863490691A6F4606E7D00(L_2, L_3, NULL);
		V_2 = ((Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07*)CastclassSealed((RuntimeObject*)L_4, Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07_il2cpp_TypeInfo_var));
		Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* L_5 = V_2;
		Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* L_6 = V_1;
		Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* L_7;
		L_7 = InterlockedCompareExchangeImpl<Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07*>((&((InputFocus_t76018DDD56BB288561D354B825D5929965DC38FE_StaticFields*)il2cpp_codegen_static_fields_for(InputFocus_t76018DDD56BB288561D354B825D5929965DC38FE_il2cpp_TypeInfo_var))->___InputFocusLost_1), L_5, L_6);
		V_0 = L_7;
		Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* L_8 = V_0;
		Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* L_9 = V_1;
		if ((!(((RuntimeObject*)(Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07*)L_8) == ((RuntimeObject*)(Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07*)L_9))))
		{
			goto IL_0006;
		}
	}
	{
		return;
	}
}
// System.Void Unity.XR.Oculus.InputFocus::remove_InputFocusLost(System.Action)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void InputFocus_remove_InputFocusLost_m0AA42A75AD2BC5B4AAE5DDE93BC9D5FB915B8BAA (Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* ___0_value, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&InputFocus_t76018DDD56BB288561D354B825D5929965DC38FE_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* V_0 = NULL;
	Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* V_1 = NULL;
	Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* V_2 = NULL;
	{
		Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* L_0 = ((InputFocus_t76018DDD56BB288561D354B825D5929965DC38FE_StaticFields*)il2cpp_codegen_static_fields_for(InputFocus_t76018DDD56BB288561D354B825D5929965DC38FE_il2cpp_TypeInfo_var))->___InputFocusLost_1;
		V_0 = L_0;
	}

IL_0006:
	{
		Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* L_1 = V_0;
		V_1 = L_1;
		Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* L_2 = V_1;
		Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* L_3 = ___0_value;
		Delegate_t* L_4;
		L_4 = Delegate_Remove_m8B7DD5661308FA972E23CA1CC3FC9CEB355504E3(L_2, L_3, NULL);
		V_2 = ((Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07*)CastclassSealed((RuntimeObject*)L_4, Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07_il2cpp_TypeInfo_var));
		Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* L_5 = V_2;
		Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* L_6 = V_1;
		Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* L_7;
		L_7 = InterlockedCompareExchangeImpl<Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07*>((&((InputFocus_t76018DDD56BB288561D354B825D5929965DC38FE_StaticFields*)il2cpp_codegen_static_fields_for(InputFocus_t76018DDD56BB288561D354B825D5929965DC38FE_il2cpp_TypeInfo_var))->___InputFocusLost_1), L_5, L_6);
		V_0 = L_7;
		Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* L_8 = V_0;
		Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* L_9 = V_1;
		if ((!(((RuntimeObject*)(Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07*)L_8) == ((RuntimeObject*)(Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07*)L_9))))
		{
			goto IL_0006;
		}
	}
	{
		return;
	}
}
// System.Boolean Unity.XR.Oculus.InputFocus::get_hasInputFocus()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool InputFocus_get_hasInputFocus_mB9DD992DB215FF22B842E83C96118736B463A64C (const RuntimeMethod* method) 
{
	{
		// return NativeMethods.GetHasInputFocus();
		bool L_0;
		L_0 = NativeMethods_GetHasInputFocus_m1257051A904A1CD9F07C962F059E01223F14DF1B(NULL);
		return L_0;
	}
}
// System.Void Unity.XR.Oculus.InputFocus::Update()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void InputFocus_Update_mAA7C0E54D60D5D05EF693009DA4AFE7C63BE044A (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&InputFocus_t76018DDD56BB288561D354B825D5929965DC38FE_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	bool G_B4_0 = false;
	bool G_B1_0 = false;
	bool G_B2_0 = false;
	bool G_B3_0 = false;
	bool G_B8_0 = false;
	bool G_B5_0 = false;
	bool G_B6_0 = false;
	bool G_B7_0 = false;
	{
		// bool appHasInputFocus = hasInputFocus;
		bool L_0;
		L_0 = InputFocus_get_hasInputFocus_mB9DD992DB215FF22B842E83C96118736B463A64C(NULL);
		// if (!appHasInputFocus && hadInputFocus)
		bool L_1 = L_0;
		G_B1_0 = L_1;
		if (L_1)
		{
			G_B4_0 = L_1;
			goto IL_0020;
		}
	}
	{
		bool L_2 = ((InputFocus_t76018DDD56BB288561D354B825D5929965DC38FE_StaticFields*)il2cpp_codegen_static_fields_for(InputFocus_t76018DDD56BB288561D354B825D5929965DC38FE_il2cpp_TypeInfo_var))->___hadInputFocus_2;
		G_B2_0 = G_B1_0;
		if (!L_2)
		{
			G_B4_0 = G_B1_0;
			goto IL_0020;
		}
	}
	{
		// if (InputFocusLost != null)
		Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* L_3 = ((InputFocus_t76018DDD56BB288561D354B825D5929965DC38FE_StaticFields*)il2cpp_codegen_static_fields_for(InputFocus_t76018DDD56BB288561D354B825D5929965DC38FE_il2cpp_TypeInfo_var))->___InputFocusLost_1;
		G_B3_0 = G_B2_0;
		if (!L_3)
		{
			G_B4_0 = G_B2_0;
			goto IL_0020;
		}
	}
	{
		// InputFocusLost();
		Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* L_4 = ((InputFocus_t76018DDD56BB288561D354B825D5929965DC38FE_StaticFields*)il2cpp_codegen_static_fields_for(InputFocus_t76018DDD56BB288561D354B825D5929965DC38FE_il2cpp_TypeInfo_var))->___InputFocusLost_1;
		NullCheck(L_4);
		Action_Invoke_m7126A54DACA72B845424072887B5F3A51FC3808E_inline(L_4, NULL);
		G_B4_0 = G_B3_0;
	}

IL_0020:
	{
		// if (appHasInputFocus && !hadInputFocus)
		bool L_5 = G_B4_0;
		G_B5_0 = L_5;
		if (!L_5)
		{
			G_B8_0 = L_5;
			goto IL_003b;
		}
	}
	{
		bool L_6 = ((InputFocus_t76018DDD56BB288561D354B825D5929965DC38FE_StaticFields*)il2cpp_codegen_static_fields_for(InputFocus_t76018DDD56BB288561D354B825D5929965DC38FE_il2cpp_TypeInfo_var))->___hadInputFocus_2;
		G_B6_0 = G_B5_0;
		if (L_6)
		{
			G_B8_0 = G_B5_0;
			goto IL_003b;
		}
	}
	{
		// if (InputFocusAcquired != null)
		Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* L_7 = ((InputFocus_t76018DDD56BB288561D354B825D5929965DC38FE_StaticFields*)il2cpp_codegen_static_fields_for(InputFocus_t76018DDD56BB288561D354B825D5929965DC38FE_il2cpp_TypeInfo_var))->___InputFocusAcquired_0;
		G_B7_0 = G_B6_0;
		if (!L_7)
		{
			G_B8_0 = G_B6_0;
			goto IL_003b;
		}
	}
	{
		// InputFocusAcquired();
		Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* L_8 = ((InputFocus_t76018DDD56BB288561D354B825D5929965DC38FE_StaticFields*)il2cpp_codegen_static_fields_for(InputFocus_t76018DDD56BB288561D354B825D5929965DC38FE_il2cpp_TypeInfo_var))->___InputFocusAcquired_0;
		NullCheck(L_8);
		Action_Invoke_m7126A54DACA72B845424072887B5F3A51FC3808E_inline(L_8, NULL);
		G_B8_0 = G_B7_0;
	}

IL_003b:
	{
		// hadInputFocus = appHasInputFocus;
		((InputFocus_t76018DDD56BB288561D354B825D5929965DC38FE_StaticFields*)il2cpp_codegen_static_fields_for(InputFocus_t76018DDD56BB288561D354B825D5929965DC38FE_il2cpp_TypeInfo_var))->___hadInputFocus_2 = G_B8_0;
		// }
		return;
	}
}
// System.Void Unity.XR.Oculus.InputFocus::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void InputFocus__ctor_m3FDC5A5ECFC5247C91B39C8EE86F768A28CD1F0E (InputFocus_t76018DDD56BB288561D354B825D5929965DC38FE* __this, const RuntimeMethod* method) 
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
// System.Boolean Unity.XR.Oculus.Boundary::GetBoundaryConfigured()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Boundary_GetBoundaryConfigured_m7183DA782C7EB580E70B499D8B55BBA226A6B9D6 (const RuntimeMethod* method) 
{
	{
		// return NativeMethods.GetBoundaryConfigured();
		bool L_0;
		L_0 = NativeMethods_GetBoundaryConfigured_m4010CCBA97D29852557408736FCE53C8A3CB6C9F(NULL);
		return L_0;
	}
}
// System.Boolean Unity.XR.Oculus.Boundary::GetBoundaryDimensions(Unity.XR.Oculus.Boundary/BoundaryType,UnityEngine.Vector3&)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Boundary_GetBoundaryDimensions_m271019296BCF211170845B68A8A6F93B73C54435 (int32_t ___0_boundaryType, Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2* ___1_dimensions, const RuntimeMethod* method) 
{
	{
		// return NativeMethods.GetBoundaryDimensions(boundaryType, out dimensions);
		int32_t L_0 = ___0_boundaryType;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2* L_1 = ___1_dimensions;
		bool L_2;
		L_2 = NativeMethods_GetBoundaryDimensions_m2C4E14BF8B564D95BA1FD6D7F6BB3CDC68A368CD(L_0, L_1, NULL);
		return L_2;
	}
}
// System.Boolean Unity.XR.Oculus.Boundary::GetBoundaryVisible()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Boundary_GetBoundaryVisible_m2EFDBAF717B810653962E1B5E0D11ADF5A35C5E4 (const RuntimeMethod* method) 
{
	{
		// return NativeMethods.GetBoundaryVisible();
		bool L_0;
		L_0 = NativeMethods_GetBoundaryVisible_m68BB43585F6289250D59230FF61F4411537D169C(NULL);
		return L_0;
	}
}
// System.Void Unity.XR.Oculus.Boundary::SetBoundaryVisible(System.Boolean)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Boundary_SetBoundaryVisible_m49E92EF5DF493A2D8D704A4479383F6A4AB774B7 (bool ___0_boundaryVisible, const RuntimeMethod* method) 
{
	{
		// NativeMethods.SetBoundaryVisible(boundaryVisible);
		bool L_0 = ___0_boundaryVisible;
		NativeMethods_SetBoundaryVisible_m8281A6859FEE7659C63C644B9C37245D187FAC8F(L_0, NULL);
		// }
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
// System.Void Unity.XR.Oculus.Development::TrySetDeveloperMode(System.Boolean)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Development_TrySetDeveloperMode_m900F2C89D15334020DB39313D6863E90ECD62ABE (bool ___0_active, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Development_t106B6EDC97423218186995B6FDF3EF5E6EE40C40_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	int32_t G_B3_0 = 0;
	{
		// s_CachedMode = active ? UserDeveloperModeSettingCache.UserSettingTrue : UserDeveloperModeSettingCache.UserSettingFalse;
		bool L_0 = ___0_active;
		if (L_0)
		{
			goto IL_0006;
		}
	}
	{
		G_B3_0 = 1;
		goto IL_0007;
	}

IL_0006:
	{
		G_B3_0 = 2;
	}

IL_0007:
	{
		((Development_t106B6EDC97423218186995B6FDF3EF5E6EE40C40_StaticFields*)il2cpp_codegen_static_fields_for(Development_t106B6EDC97423218186995B6FDF3EF5E6EE40C40_il2cpp_TypeInfo_var))->___s_CachedMode_0 = G_B3_0;
		// }
		return;
	}
}
// System.Void Unity.XR.Oculus.Development::OverrideDeveloperModeStart()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Development_OverrideDeveloperModeStart_m7017971A663FEE72864A6D5039091E21DB045C1B (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Development_t106B6EDC97423218186995B6FDF3EF5E6EE40C40_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral9CA2736EB42A97B73E816FF8369ACA2005FA5AA2);
		s_Il2CppMethodInitialized = true;
	}
	bool V_0 = false;
	bool V_1 = false;
	{
		// bool enable = true;
		V_0 = (bool)1;
		// bool shouldOverride = false;
		V_1 = (bool)0;
		// if (s_CachedMode != UserDeveloperModeSettingCache.NoUserSettingCached)
		int32_t L_0 = ((Development_t106B6EDC97423218186995B6FDF3EF5E6EE40C40_StaticFields*)il2cpp_codegen_static_fields_for(Development_t106B6EDC97423218186995B6FDF3EF5E6EE40C40_il2cpp_TypeInfo_var))->___s_CachedMode_0;
		if (!L_0)
		{
			goto IL_0018;
		}
	}
	{
		// shouldOverride = true;
		V_1 = (bool)1;
		// enable = (s_CachedMode == UserDeveloperModeSettingCache.UserSettingTrue);
		int32_t L_1 = ((Development_t106B6EDC97423218186995B6FDF3EF5E6EE40C40_StaticFields*)il2cpp_codegen_static_fields_for(Development_t106B6EDC97423218186995B6FDF3EF5E6EE40C40_il2cpp_TypeInfo_var))->___s_CachedMode_0;
		V_0 = (bool)((((int32_t)L_1) == ((int32_t)2))? 1 : 0);
		goto IL_0021;
	}

IL_0018:
	{
		// else if (Debug.isDebugBuild)
		il2cpp_codegen_runtime_class_init_inline(Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var);
		bool L_2;
		L_2 = Debug_get_isDebugBuild_m9277C4A9591F7E1D8B76340B4CAE5EA33D63AF01(NULL);
		if (!L_2)
		{
			goto IL_0021;
		}
	}
	{
		// shouldOverride = true;
		V_1 = (bool)1;
	}

IL_0021:
	{
		// if (shouldOverride && !NativeMethods.SetDeveloperModeStrict(enable))
		bool L_3 = V_1;
		if (!L_3)
		{
			goto IL_0036;
		}
	}
	{
		bool L_4 = V_0;
		bool L_5;
		L_5 = NativeMethods_SetDeveloperModeStrict_mDD7A7FF224AC714D8D8C265C2E1452DB0AE653C8(L_4, NULL);
		if (L_5)
		{
			goto IL_0036;
		}
	}
	{
		// Debug.LogError("Failed to set DeveloperMode on Start.");
		il2cpp_codegen_runtime_class_init_inline(Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var);
		Debug_LogError_mB00B2B4468EF3CAF041B038D840820FB84C924B2(_stringLiteral9CA2736EB42A97B73E816FF8369ACA2005FA5AA2, NULL);
	}

IL_0036:
	{
		// }
		return;
	}
}
// System.Void Unity.XR.Oculus.Development::OverrideDeveloperModeStop()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Development_OverrideDeveloperModeStop_m89921E0CCD7DAC487814309D82E5D42288C8BBC4 (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral6C095088ADD88C25A47E7BBE6A81D13C798F9E75);
		s_Il2CppMethodInitialized = true;
	}
	{
		// if (!NativeMethods.SetDeveloperModeStrict(false))
		bool L_0;
		L_0 = NativeMethods_SetDeveloperModeStrict_mDD7A7FF224AC714D8D8C265C2E1452DB0AE653C8((bool)0, NULL);
		if (L_0)
		{
			goto IL_0012;
		}
	}
	{
		// Debug.LogError("Failed to set DeveloperMode to false on Stop.");
		il2cpp_codegen_runtime_class_init_inline(Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var);
		Debug_LogError_mB00B2B4468EF3CAF041B038D840820FB84C924B2(_stringLiteral6C095088ADD88C25A47E7BBE6A81D13C798F9E75, NULL);
	}

IL_0012:
	{
		// }
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
// Unity.XR.Oculus.OculusLoader/DeviceSupportedResult Unity.XR.Oculus.OculusLoader::IsDeviceSupported()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t OculusLoader_IsDeviceSupported_m478A97E527F8DA4B6C08459445739C2E9C72C36D (const RuntimeMethod* method) 
{
	int32_t V_0 = 0;
	il2cpp::utils::ExceptionSupportStack<RuntimeObject*, 1> __active_exceptions;
	try
	{// begin try (depth: 1)
		{
			// if (NativeMethods.GetIsSupportedDevice())
			bool L_0;
			L_0 = NativeMethods_GetIsSupportedDevice_m336DCB5F80772C6F89CBF1CA7122EDC4C35D78D4(NULL);
			if (!L_0)
			{
				goto IL_000b_1;
			}
		}
		{
			// return DeviceSupportedResult.Supported;
			V_0 = 0;
			goto IL_0014;
		}

IL_000b_1:
		{
			// return DeviceSupportedResult.ExitApplication;
			V_0 = 2;
			goto IL_0014;
		}
	}// end try (depth: 1)
	catch(Il2CppExceptionWrapper& e)
	{
		if(il2cpp_codegen_class_is_assignable_from (((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&DllNotFoundException_t8CAE636A394C482C9FCF38FB7B7929506319D534_il2cpp_TypeInfo_var)), il2cpp_codegen_object_class(e.ex)))
		{
			IL2CPP_PUSH_ACTIVE_EXCEPTION(e.ex);
			goto CATCH_000f;
		}
		throw e;
	}

CATCH_000f:
	{// begin catch(System.DllNotFoundException)
		// catch(DllNotFoundException)
		// return DeviceSupportedResult.NotSupported;
		V_0 = 1;
		IL2CPP_POP_ACTIVE_EXCEPTION();
		goto IL_0014;
	}// end catch (depth: 1)

IL_0014:
	{
		// }
		int32_t L_1 = V_0;
		return L_1;
	}
}
// UnityEngine.XR.XRDisplaySubsystem Unity.XR.Oculus.OculusLoader::get_displaySubsystem()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR XRDisplaySubsystem_t4B00B0BF1894A039ACFA8DDC2C2EB9301118C1F1* OculusLoader_get_displaySubsystem_mBF36D42BABD9D5DA3ECE4F2D25862BF35C29D4E3 (OculusLoader_tA386B9AA0786D042EA272EDF385F96C0AD1A56BB* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&XRLoaderHelper_GetLoadedSubsystem_TisXRDisplaySubsystem_t4B00B0BF1894A039ACFA8DDC2C2EB9301118C1F1_m9FC253637CE85B86CE3DFA51346D7E4D49913E7B_RuntimeMethod_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// return GetLoadedSubsystem<XRDisplaySubsystem>();
		XRDisplaySubsystem_t4B00B0BF1894A039ACFA8DDC2C2EB9301118C1F1* L_0;
		L_0 = GenericVirtualFuncInvoker0< XRDisplaySubsystem_t4B00B0BF1894A039ACFA8DDC2C2EB9301118C1F1* >::Invoke(XRLoaderHelper_GetLoadedSubsystem_TisXRDisplaySubsystem_t4B00B0BF1894A039ACFA8DDC2C2EB9301118C1F1_m9FC253637CE85B86CE3DFA51346D7E4D49913E7B_RuntimeMethod_var, __this);
		return L_0;
	}
}
// UnityEngine.XR.XRInputSubsystem Unity.XR.Oculus.OculusLoader::get_inputSubsystem()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR XRInputSubsystem_tFECE6683FCAEBF05BAD05E5D612690095D8BAD34* OculusLoader_get_inputSubsystem_m3690AE48575D2196C79757CE5F21C96F1A0963B2 (OculusLoader_tA386B9AA0786D042EA272EDF385F96C0AD1A56BB* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&XRLoaderHelper_GetLoadedSubsystem_TisXRInputSubsystem_tFECE6683FCAEBF05BAD05E5D612690095D8BAD34_mFFFE07D8A1F3526DB3003FFAE9681221AA4A876A_RuntimeMethod_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// return GetLoadedSubsystem<XRInputSubsystem>();
		XRInputSubsystem_tFECE6683FCAEBF05BAD05E5D612690095D8BAD34* L_0;
		L_0 = GenericVirtualFuncInvoker0< XRInputSubsystem_tFECE6683FCAEBF05BAD05E5D612690095D8BAD34* >::Invoke(XRLoaderHelper_GetLoadedSubsystem_TisXRInputSubsystem_tFECE6683FCAEBF05BAD05E5D612690095D8BAD34_mFFFE07D8A1F3526DB3003FFAE9681221AA4A876A_RuntimeMethod_var, __this);
		return L_0;
	}
}
// System.Boolean Unity.XR.Oculus.OculusLoader::Initialize()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool OculusLoader_Initialize_mDEFC017849247485E672462C30068D016613B08A (OculusLoader_tA386B9AA0786D042EA272EDF385F96C0AD1A56BB* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Action_1_t3CB5D1A819C3ED3F99E9E39F890F18633253949A_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&OculusLoader_PermissionGrantedCallback_mE09E412A87E4A838FA3C8E3DAC98E0AB4DE2922C_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&OculusLoader_tA386B9AA0786D042EA272EDF385F96C0AD1A56BB_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&PermissionCallbacks_t6C5002298E6649261FCB52C244695459E5C9890F_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&XRLoaderHelper_CreateSubsystem_TisXRDisplaySubsystemDescriptor_t72DD88EE9094488AE723A495F48884BA4EA8311A_TisXRDisplaySubsystem_t4B00B0BF1894A039ACFA8DDC2C2EB9301118C1F1_m47BB00ACEADFC3AF821DC1EE31F79CF6EEB4681A_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&XRLoaderHelper_CreateSubsystem_TisXRInputSubsystemDescriptor_t42088DD6542C0BDD27C2951B911E4F69DD1F917D_TisXRInputSubsystem_tFECE6683FCAEBF05BAD05E5D612690095D8BAD34_mA9FE0AE2F98657CD075CD191BAB94910F963C08C_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral271AF6878EC3872B415EA8A73A1433E4B604ACDF);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral50B8349DC34E14AB475F3453803BCDBD9F3B0F85);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral666C1D75F394950EFFDBE5C128752A9E0CBD1DEA);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral71D87D03368ADC0E5018E85E30CA4984F5FF2AA8);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral753B6D37AEAF368AA772306EFBD496750FDE357A);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralFFBC2C924E5DC9232A3732A59008339C044352A3);
		s_Il2CppMethodInitialized = true;
	}
	OculusSettings_t0584FB71432B697479FD0BFC5B68C195F17CD321* V_0 = NULL;
	UserDefinedSettings_tE823040D57E8C8C9D8A028F206230E1C7BAC8EE3 V_1;
	memset((&V_1), 0, sizeof(V_1));
	PermissionCallbacks_t6C5002298E6649261FCB52C244695459E5C9890F* V_2 = NULL;
	UserDefinedSettings_tE823040D57E8C8C9D8A028F206230E1C7BAC8EE3* G_B7_0 = NULL;
	UserDefinedSettings_tE823040D57E8C8C9D8A028F206230E1C7BAC8EE3* G_B6_0 = NULL;
	int32_t G_B8_0 = 0;
	UserDefinedSettings_tE823040D57E8C8C9D8A028F206230E1C7BAC8EE3* G_B8_1 = NULL;
	UserDefinedSettings_tE823040D57E8C8C9D8A028F206230E1C7BAC8EE3* G_B10_0 = NULL;
	UserDefinedSettings_tE823040D57E8C8C9D8A028F206230E1C7BAC8EE3* G_B9_0 = NULL;
	int32_t G_B11_0 = 0;
	UserDefinedSettings_tE823040D57E8C8C9D8A028F206230E1C7BAC8EE3* G_B11_1 = NULL;
	UserDefinedSettings_tE823040D57E8C8C9D8A028F206230E1C7BAC8EE3* G_B13_0 = NULL;
	UserDefinedSettings_tE823040D57E8C8C9D8A028F206230E1C7BAC8EE3* G_B12_0 = NULL;
	int32_t G_B14_0 = 0;
	UserDefinedSettings_tE823040D57E8C8C9D8A028F206230E1C7BAC8EE3* G_B14_1 = NULL;
	UserDefinedSettings_tE823040D57E8C8C9D8A028F206230E1C7BAC8EE3* G_B16_0 = NULL;
	UserDefinedSettings_tE823040D57E8C8C9D8A028F206230E1C7BAC8EE3* G_B15_0 = NULL;
	int32_t G_B17_0 = 0;
	UserDefinedSettings_tE823040D57E8C8C9D8A028F206230E1C7BAC8EE3* G_B17_1 = NULL;
	UserDefinedSettings_tE823040D57E8C8C9D8A028F206230E1C7BAC8EE3* G_B19_0 = NULL;
	UserDefinedSettings_tE823040D57E8C8C9D8A028F206230E1C7BAC8EE3* G_B18_0 = NULL;
	int32_t G_B20_0 = 0;
	UserDefinedSettings_tE823040D57E8C8C9D8A028F206230E1C7BAC8EE3* G_B20_1 = NULL;
	UserDefinedSettings_tE823040D57E8C8C9D8A028F206230E1C7BAC8EE3* G_B22_0 = NULL;
	UserDefinedSettings_tE823040D57E8C8C9D8A028F206230E1C7BAC8EE3* G_B21_0 = NULL;
	int32_t G_B23_0 = 0;
	UserDefinedSettings_tE823040D57E8C8C9D8A028F206230E1C7BAC8EE3* G_B23_1 = NULL;
	UserDefinedSettings_tE823040D57E8C8C9D8A028F206230E1C7BAC8EE3* G_B25_0 = NULL;
	UserDefinedSettings_tE823040D57E8C8C9D8A028F206230E1C7BAC8EE3* G_B24_0 = NULL;
	int32_t G_B26_0 = 0;
	UserDefinedSettings_tE823040D57E8C8C9D8A028F206230E1C7BAC8EE3* G_B26_1 = NULL;
	UserDefinedSettings_tE823040D57E8C8C9D8A028F206230E1C7BAC8EE3* G_B28_0 = NULL;
	UserDefinedSettings_tE823040D57E8C8C9D8A028F206230E1C7BAC8EE3* G_B27_0 = NULL;
	int32_t G_B29_0 = 0;
	UserDefinedSettings_tE823040D57E8C8C9D8A028F206230E1C7BAC8EE3* G_B29_1 = NULL;
	UserDefinedSettings_tE823040D57E8C8C9D8A028F206230E1C7BAC8EE3* G_B31_0 = NULL;
	UserDefinedSettings_tE823040D57E8C8C9D8A028F206230E1C7BAC8EE3* G_B30_0 = NULL;
	int32_t G_B32_0 = 0;
	UserDefinedSettings_tE823040D57E8C8C9D8A028F206230E1C7BAC8EE3* G_B32_1 = NULL;
	UserDefinedSettings_tE823040D57E8C8C9D8A028F206230E1C7BAC8EE3* G_B34_0 = NULL;
	UserDefinedSettings_tE823040D57E8C8C9D8A028F206230E1C7BAC8EE3* G_B33_0 = NULL;
	int32_t G_B35_0 = 0;
	UserDefinedSettings_tE823040D57E8C8C9D8A028F206230E1C7BAC8EE3* G_B35_1 = NULL;
	UserDefinedSettings_tE823040D57E8C8C9D8A028F206230E1C7BAC8EE3* G_B37_0 = NULL;
	UserDefinedSettings_tE823040D57E8C8C9D8A028F206230E1C7BAC8EE3* G_B36_0 = NULL;
	int32_t G_B38_0 = 0;
	UserDefinedSettings_tE823040D57E8C8C9D8A028F206230E1C7BAC8EE3* G_B38_1 = NULL;
	UserDefinedSettings_tE823040D57E8C8C9D8A028F206230E1C7BAC8EE3* G_B40_0 = NULL;
	UserDefinedSettings_tE823040D57E8C8C9D8A028F206230E1C7BAC8EE3* G_B39_0 = NULL;
	int32_t G_B41_0 = 0;
	UserDefinedSettings_tE823040D57E8C8C9D8A028F206230E1C7BAC8EE3* G_B41_1 = NULL;
	UserDefinedSettings_tE823040D57E8C8C9D8A028F206230E1C7BAC8EE3* G_B43_0 = NULL;
	UserDefinedSettings_tE823040D57E8C8C9D8A028F206230E1C7BAC8EE3* G_B42_0 = NULL;
	int32_t G_B44_0 = 0;
	UserDefinedSettings_tE823040D57E8C8C9D8A028F206230E1C7BAC8EE3* G_B44_1 = NULL;
	{
		// if (!CheckUnityVersionCompatibility())
		bool L_0;
		L_0 = OculusLoader_CheckUnityVersionCompatibility_mC211D3EFA35B0E509154B8F2993B08079A5C4413(__this, NULL);
		if (L_0)
		{
			goto IL_000a;
		}
	}
	{
		// return false;
		return (bool)0;
	}

IL_000a:
	{
		//             if (IsDeviceSupported() != DeviceSupportedResult.Supported
		// #if UNITY_EDITOR_WIN
		//                 || SystemInfo.graphicsDeviceType != GraphicsDeviceType.Direct3D11
		// #endif
		//             )
		il2cpp_codegen_runtime_class_init_inline(OculusLoader_tA386B9AA0786D042EA272EDF385F96C0AD1A56BB_il2cpp_TypeInfo_var);
		int32_t L_1;
		L_1 = OculusLoader_IsDeviceSupported_m478A97E527F8DA4B6C08459445739C2E9C72C36D(NULL);
		if (!L_1)
		{
			goto IL_0013;
		}
	}
	{
		// return false;
		return (bool)0;
	}

IL_0013:
	{
		// OculusSettings settings = GetSettings();
		OculusSettings_t0584FB71432B697479FD0BFC5B68C195F17CD321* L_2;
		L_2 = OculusLoader_GetSettings_m2FA61767F36A8CFB03124EB295A79C7A7F0A863B_inline(__this, NULL);
		V_0 = L_2;
		// if (settings != null)
		OculusSettings_t0584FB71432B697479FD0BFC5B68C195F17CD321* L_3 = V_0;
		il2cpp_codegen_runtime_class_init_inline(Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		bool L_4;
		L_4 = Object_op_Inequality_mD0BE578448EAA61948F25C32F8DD55AB1F778602(L_3, (Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C*)NULL, NULL);
		if (!L_4)
		{
			goto IL_014b;
		}
	}
	{
		// userDefinedSettings.sharedDepthBuffer = (ushort)(settings.SharedDepthBuffer ? 1 : 0);
		OculusSettings_t0584FB71432B697479FD0BFC5B68C195F17CD321* L_5 = V_0;
		NullCheck(L_5);
		bool L_6 = L_5->___SharedDepthBuffer_6;
		G_B6_0 = (&V_1);
		if (L_6)
		{
			G_B7_0 = (&V_1);
			goto IL_0033;
		}
	}
	{
		G_B8_0 = 0;
		G_B8_1 = G_B6_0;
		goto IL_0034;
	}

IL_0033:
	{
		G_B8_0 = 1;
		G_B8_1 = G_B7_0;
	}

IL_0034:
	{
		G_B8_1->___sharedDepthBuffer_0 = (uint16_t)((int32_t)(uint16_t)G_B8_0);
		// userDefinedSettings.dashSupport = (ushort)(settings.DashSupport ? 1 : 0);
		OculusSettings_t0584FB71432B697479FD0BFC5B68C195F17CD321* L_7 = V_0;
		NullCheck(L_7);
		bool L_8 = L_7->___DashSupport_8;
		G_B9_0 = (&V_1);
		if (L_8)
		{
			G_B10_0 = (&V_1);
			goto IL_0047;
		}
	}
	{
		G_B11_0 = 0;
		G_B11_1 = G_B9_0;
		goto IL_0048;
	}

IL_0047:
	{
		G_B11_0 = 1;
		G_B11_1 = G_B10_0;
	}

IL_0048:
	{
		G_B11_1->___dashSupport_1 = (uint16_t)((int32_t)(uint16_t)G_B11_0);
		// userDefinedSettings.stereoRenderingMode = (ushort)settings.GetStereoRenderingMode();
		OculusSettings_t0584FB71432B697479FD0BFC5B68C195F17CD321* L_9 = V_0;
		NullCheck(L_9);
		uint16_t L_10;
		L_10 = OculusSettings_GetStereoRenderingMode_mE9C6ABC56B9EDEB3BE5599B091BB6699429BB6BD(L_9, NULL);
		(&V_1)->___stereoRenderingMode_2 = L_10;
		// userDefinedSettings.colorSpace = (ushort)((QualitySettings.activeColorSpace == ColorSpace.Linear) ? 1 : 0);
		int32_t L_11;
		L_11 = QualitySettings_get_activeColorSpace_m4F47784E7B0FE0A5497C8BAB9CA86BD576FB92F9(NULL);
		G_B12_0 = (&V_1);
		if ((((int32_t)L_11) == ((int32_t)1)))
		{
			G_B13_0 = (&V_1);
			goto IL_0068;
		}
	}
	{
		G_B14_0 = 0;
		G_B14_1 = G_B12_0;
		goto IL_0069;
	}

IL_0068:
	{
		G_B14_0 = 1;
		G_B14_1 = G_B13_0;
	}

IL_0069:
	{
		G_B14_1->___colorSpace_3 = (uint16_t)((int32_t)(uint16_t)G_B14_0);
		// userDefinedSettings.lowOverheadMode = (ushort)(settings.LowOverheadMode ? 1 : 0);
		OculusSettings_t0584FB71432B697479FD0BFC5B68C195F17CD321* L_12 = V_0;
		NullCheck(L_12);
		bool L_13 = L_12->___LowOverheadMode_9;
		G_B15_0 = (&V_1);
		if (L_13)
		{
			G_B16_0 = (&V_1);
			goto IL_007c;
		}
	}
	{
		G_B17_0 = 0;
		G_B17_1 = G_B15_0;
		goto IL_007d;
	}

IL_007c:
	{
		G_B17_0 = 1;
		G_B17_1 = G_B16_0;
	}

IL_007d:
	{
		G_B17_1->___lowOverheadMode_4 = (uint16_t)((int32_t)(uint16_t)G_B17_0);
		// userDefinedSettings.optimizeBufferDiscards = (ushort)(settings.OptimizeBufferDiscards ? 1 : 0);
		OculusSettings_t0584FB71432B697479FD0BFC5B68C195F17CD321* L_14 = V_0;
		NullCheck(L_14);
		bool L_15 = L_14->___OptimizeBufferDiscards_10;
		G_B18_0 = (&V_1);
		if (L_15)
		{
			G_B19_0 = (&V_1);
			goto IL_0090;
		}
	}
	{
		G_B20_0 = 0;
		G_B20_1 = G_B18_0;
		goto IL_0091;
	}

IL_0090:
	{
		G_B20_0 = 1;
		G_B20_1 = G_B19_0;
	}

IL_0091:
	{
		G_B20_1->___optimizeBufferDiscards_5 = (uint16_t)((int32_t)(uint16_t)G_B20_0);
		// userDefinedSettings.phaseSync = (ushort)(settings.PhaseSync ? 1 : 0);
		OculusSettings_t0584FB71432B697479FD0BFC5B68C195F17CD321* L_16 = V_0;
		NullCheck(L_16);
		bool L_17 = L_16->___PhaseSync_11;
		G_B21_0 = (&V_1);
		if (L_17)
		{
			G_B22_0 = (&V_1);
			goto IL_00a4;
		}
	}
	{
		G_B23_0 = 0;
		G_B23_1 = G_B21_0;
		goto IL_00a5;
	}

IL_00a4:
	{
		G_B23_0 = 1;
		G_B23_1 = G_B22_0;
	}

IL_00a5:
	{
		G_B23_1->___phaseSync_6 = (uint16_t)((int32_t)(uint16_t)G_B23_0);
		// userDefinedSettings.symmetricProjection = (ushort)(settings.SymmetricProjection ? 1 : 0);
		OculusSettings_t0584FB71432B697479FD0BFC5B68C195F17CD321* L_18 = V_0;
		NullCheck(L_18);
		bool L_19 = L_18->___SymmetricProjection_12;
		G_B24_0 = (&V_1);
		if (L_19)
		{
			G_B25_0 = (&V_1);
			goto IL_00b8;
		}
	}
	{
		G_B26_0 = 0;
		G_B26_1 = G_B24_0;
		goto IL_00b9;
	}

IL_00b8:
	{
		G_B26_0 = 1;
		G_B26_1 = G_B25_0;
	}

IL_00b9:
	{
		G_B26_1->___symmetricProjection_7 = (uint16_t)((int32_t)(uint16_t)G_B26_0);
		// userDefinedSettings.subsampledLayout = (ushort)(settings.SubsampledLayout ? 1 : 0);
		OculusSettings_t0584FB71432B697479FD0BFC5B68C195F17CD321* L_20 = V_0;
		NullCheck(L_20);
		bool L_21 = L_20->___SubsampledLayout_13;
		G_B27_0 = (&V_1);
		if (L_21)
		{
			G_B28_0 = (&V_1);
			goto IL_00cc;
		}
	}
	{
		G_B29_0 = 0;
		G_B29_1 = G_B27_0;
		goto IL_00cd;
	}

IL_00cc:
	{
		G_B29_0 = 1;
		G_B29_1 = G_B28_0;
	}

IL_00cd:
	{
		G_B29_1->___subsampledLayout_8 = (uint16_t)((int32_t)(uint16_t)G_B29_0);
		// userDefinedSettings.foveatedRenderingMethod = (ushort)settings.FoveatedRenderingMethod;
		OculusSettings_t0584FB71432B697479FD0BFC5B68C195F17CD321* L_22 = V_0;
		NullCheck(L_22);
		int32_t L_23 = L_22->___FoveatedRenderingMethod_14;
		(&V_1)->___foveatedRenderingMethod_14 = (uint16_t)((int32_t)(uint16_t)L_23);
		// userDefinedSettings.lateLatching = (ushort)(settings.LateLatching ? 1 : 0);
		OculusSettings_t0584FB71432B697479FD0BFC5B68C195F17CD321* L_24 = V_0;
		NullCheck(L_24);
		bool L_25 = L_24->___LateLatching_15;
		G_B30_0 = (&V_1);
		if (L_25)
		{
			G_B31_0 = (&V_1);
			goto IL_00ee;
		}
	}
	{
		G_B32_0 = 0;
		G_B32_1 = G_B30_0;
		goto IL_00ef;
	}

IL_00ee:
	{
		G_B32_0 = 1;
		G_B32_1 = G_B31_0;
	}

IL_00ef:
	{
		G_B32_1->___lateLatching_9 = (uint16_t)((int32_t)(uint16_t)G_B32_0);
		// userDefinedSettings.lateLatchingDebug = (ushort)(settings.LateLatchingDebug ? 1 : 0);
		OculusSettings_t0584FB71432B697479FD0BFC5B68C195F17CD321* L_26 = V_0;
		NullCheck(L_26);
		bool L_27 = L_26->___LateLatchingDebug_16;
		G_B33_0 = (&V_1);
		if (L_27)
		{
			G_B34_0 = (&V_1);
			goto IL_0102;
		}
	}
	{
		G_B35_0 = 0;
		G_B35_1 = G_B33_0;
		goto IL_0103;
	}

IL_0102:
	{
		G_B35_0 = 1;
		G_B35_1 = G_B34_0;
	}

IL_0103:
	{
		G_B35_1->___lateLatchingDebug_10 = (uint16_t)((int32_t)(uint16_t)G_B35_0);
		// userDefinedSettings.enableTrackingOriginStageMode = (ushort)(settings.EnableTrackingOriginStageMode ? 1 : 0);
		OculusSettings_t0584FB71432B697479FD0BFC5B68C195F17CD321* L_28 = V_0;
		NullCheck(L_28);
		bool L_29 = L_28->___EnableTrackingOriginStageMode_17;
		G_B36_0 = (&V_1);
		if (L_29)
		{
			G_B37_0 = (&V_1);
			goto IL_0116;
		}
	}
	{
		G_B38_0 = 0;
		G_B38_1 = G_B36_0;
		goto IL_0117;
	}

IL_0116:
	{
		G_B38_0 = 1;
		G_B38_1 = G_B37_0;
	}

IL_0117:
	{
		G_B38_1->___enableTrackingOriginStageMode_11 = (uint16_t)((int32_t)(uint16_t)G_B38_0);
		// userDefinedSettings.spaceWarp = (ushort)(settings.SpaceWarp ? 1 : 0);
		OculusSettings_t0584FB71432B697479FD0BFC5B68C195F17CD321* L_30 = V_0;
		NullCheck(L_30);
		bool L_31 = L_30->___SpaceWarp_18;
		G_B39_0 = (&V_1);
		if (L_31)
		{
			G_B40_0 = (&V_1);
			goto IL_012a;
		}
	}
	{
		G_B41_0 = 0;
		G_B41_1 = G_B39_0;
		goto IL_012b;
	}

IL_012a:
	{
		G_B41_0 = 1;
		G_B41_1 = G_B40_0;
	}

IL_012b:
	{
		G_B41_1->___spaceWarp_12 = (uint16_t)((int32_t)(uint16_t)G_B41_0);
		// userDefinedSettings.depthSubmission = (ushort)(settings.DepthSubmission ? 1 : 0);
		OculusSettings_t0584FB71432B697479FD0BFC5B68C195F17CD321* L_32 = V_0;
		NullCheck(L_32);
		bool L_33 = L_32->___DepthSubmission_7;
		G_B42_0 = (&V_1);
		if (L_33)
		{
			G_B43_0 = (&V_1);
			goto IL_013e;
		}
	}
	{
		G_B44_0 = 0;
		G_B44_1 = G_B42_0;
		goto IL_013f;
	}

IL_013e:
	{
		G_B44_0 = 1;
		G_B44_1 = G_B43_0;
	}

IL_013f:
	{
		G_B44_1->___depthSubmission_13 = (uint16_t)((int32_t)(uint16_t)G_B44_0);
		// NativeMethods.SetUserDefinedSettings(userDefinedSettings);
		UserDefinedSettings_tE823040D57E8C8C9D8A028F206230E1C7BAC8EE3 L_34 = V_1;
		NativeMethods_SetUserDefinedSettings_m42FD3CA27980B25D9C638E3AC876749FBB23CBA6(L_34, NULL);
	}

IL_014b:
	{
		// CreateSubsystem<XRDisplaySubsystemDescriptor, XRDisplaySubsystem>(s_DisplaySubsystemDescriptors, "oculus display");
		il2cpp_codegen_runtime_class_init_inline(OculusLoader_tA386B9AA0786D042EA272EDF385F96C0AD1A56BB_il2cpp_TypeInfo_var);
		List_1_tC3F021D09EFA4F3516555517B5E0D39308C9C1B4* L_35 = ((OculusLoader_tA386B9AA0786D042EA272EDF385F96C0AD1A56BB_StaticFields*)il2cpp_codegen_static_fields_for(OculusLoader_tA386B9AA0786D042EA272EDF385F96C0AD1A56BB_il2cpp_TypeInfo_var))->___s_DisplaySubsystemDescriptors_5;
		XRLoaderHelper_CreateSubsystem_TisXRDisplaySubsystemDescriptor_t72DD88EE9094488AE723A495F48884BA4EA8311A_TisXRDisplaySubsystem_t4B00B0BF1894A039ACFA8DDC2C2EB9301118C1F1_m47BB00ACEADFC3AF821DC1EE31F79CF6EEB4681A(__this, L_35, _stringLiteral753B6D37AEAF368AA772306EFBD496750FDE357A, XRLoaderHelper_CreateSubsystem_TisXRDisplaySubsystemDescriptor_t72DD88EE9094488AE723A495F48884BA4EA8311A_TisXRDisplaySubsystem_t4B00B0BF1894A039ACFA8DDC2C2EB9301118C1F1_m47BB00ACEADFC3AF821DC1EE31F79CF6EEB4681A_RuntimeMethod_var);
		// CreateSubsystem<XRInputSubsystemDescriptor, XRInputSubsystem>(s_InputSubsystemDescriptors, "oculus input");
		List_1_tE3AE94237CE649B47E1D52E1A3120E772255FF87* L_36 = ((OculusLoader_tA386B9AA0786D042EA272EDF385F96C0AD1A56BB_StaticFields*)il2cpp_codegen_static_fields_for(OculusLoader_tA386B9AA0786D042EA272EDF385F96C0AD1A56BB_il2cpp_TypeInfo_var))->___s_InputSubsystemDescriptors_6;
		XRLoaderHelper_CreateSubsystem_TisXRInputSubsystemDescriptor_t42088DD6542C0BDD27C2951B911E4F69DD1F917D_TisXRInputSubsystem_tFECE6683FCAEBF05BAD05E5D612690095D8BAD34_mA9FE0AE2F98657CD075CD191BAB94910F963C08C(__this, L_36, _stringLiteral71D87D03368ADC0E5018E85E30CA4984F5FF2AA8, XRLoaderHelper_CreateSubsystem_TisXRInputSubsystemDescriptor_t42088DD6542C0BDD27C2951B911E4F69DD1F917D_TisXRInputSubsystem_tFECE6683FCAEBF05BAD05E5D612690095D8BAD34_mA9FE0AE2F98657CD075CD191BAB94910F963C08C_RuntimeMethod_var);
		// if (displaySubsystem == null && inputSubsystem == null)
		XRDisplaySubsystem_t4B00B0BF1894A039ACFA8DDC2C2EB9301118C1F1* L_37;
		L_37 = OculusLoader_get_displaySubsystem_mBF36D42BABD9D5DA3ECE4F2D25862BF35C29D4E3(__this, NULL);
		if (L_37)
		{
			goto IL_0187;
		}
	}
	{
		XRInputSubsystem_tFECE6683FCAEBF05BAD05E5D612690095D8BAD34* L_38;
		L_38 = OculusLoader_get_inputSubsystem_m3690AE48575D2196C79757CE5F21C96F1A0963B2(__this, NULL);
		if (L_38)
		{
			goto IL_0187;
		}
	}
	{
		// Debug.LogWarning("Unable to start Oculus XR Plugin.");
		il2cpp_codegen_runtime_class_init_inline(Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var);
		Debug_LogWarning_m33EF1B897E0C7C6FF538989610BFAFFEF4628CA9(_stringLiteral666C1D75F394950EFFDBE5C128752A9E0CBD1DEA, NULL);
		goto IL_01b4;
	}

IL_0187:
	{
		// else if (displaySubsystem == null)
		XRDisplaySubsystem_t4B00B0BF1894A039ACFA8DDC2C2EB9301118C1F1* L_39;
		L_39 = OculusLoader_get_displaySubsystem_mBF36D42BABD9D5DA3ECE4F2D25862BF35C29D4E3(__this, NULL);
		if (L_39)
		{
			goto IL_019b;
		}
	}
	{
		// Debug.LogError("Unable to start Oculus XR Plugin. Failed to load display subsystem.");
		il2cpp_codegen_runtime_class_init_inline(Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var);
		Debug_LogError_mB00B2B4468EF3CAF041B038D840820FB84C924B2(_stringLiteral50B8349DC34E14AB475F3453803BCDBD9F3B0F85, NULL);
		goto IL_01b4;
	}

IL_019b:
	{
		// else if (inputSubsystem == null)
		XRInputSubsystem_tFECE6683FCAEBF05BAD05E5D612690095D8BAD34* L_40;
		L_40 = OculusLoader_get_inputSubsystem_m3690AE48575D2196C79757CE5F21C96F1A0963B2(__this, NULL);
		if (L_40)
		{
			goto IL_01af;
		}
	}
	{
		// Debug.LogError("Unable to start Oculus XR Plugin. Failed to load input subsystem.");
		il2cpp_codegen_runtime_class_init_inline(Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var);
		Debug_LogError_mB00B2B4468EF3CAF041B038D840820FB84C924B2(_stringLiteral271AF6878EC3872B415EA8A73A1433E4B604ACDF, NULL);
		goto IL_01b4;
	}

IL_01af:
	{
		// RegisterUpdateCallback.Initialize();
		RegisterUpdateCallback_Initialize_m0256D76B69C444418E9E394DD280C4D54F122BC5(NULL);
	}

IL_01b4:
	{
		// if (settings != null && (settings.FoveatedRenderingMethod == OculusSettings.FoveationMethod.EyeTrackedFoveatedRendering) && NativeMethods.GetEyeTrackedFoveatedRenderingSupported())
		OculusSettings_t0584FB71432B697479FD0BFC5B68C195F17CD321* L_41 = V_0;
		il2cpp_codegen_runtime_class_init_inline(Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		bool L_42;
		L_42 = Object_op_Inequality_mD0BE578448EAA61948F25C32F8DD55AB1F778602(L_41, (Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C*)NULL, NULL);
		if (!L_42)
		{
			goto IL_01f0;
		}
	}
	{
		OculusSettings_t0584FB71432B697479FD0BFC5B68C195F17CD321* L_43 = V_0;
		NullCheck(L_43);
		int32_t L_44 = L_43->___FoveatedRenderingMethod_14;
		if ((!(((uint32_t)L_44) == ((uint32_t)1))))
		{
			goto IL_01f0;
		}
	}
	{
		bool L_45;
		L_45 = NativeMethods_GetEyeTrackedFoveatedRenderingSupported_m7017B3CE2D6DE03700864069DECC810199A2C9E0(NULL);
		if (!L_45)
		{
			goto IL_01f0;
		}
	}
	{
		// var permissionCallbacks = new PermissionCallbacks();
		PermissionCallbacks_t6C5002298E6649261FCB52C244695459E5C9890F* L_46 = (PermissionCallbacks_t6C5002298E6649261FCB52C244695459E5C9890F*)il2cpp_codegen_object_new(PermissionCallbacks_t6C5002298E6649261FCB52C244695459E5C9890F_il2cpp_TypeInfo_var);
		NullCheck(L_46);
		PermissionCallbacks__ctor_m91B14BBBC8913C131E400BA0D13576822AAE7A75(L_46, NULL);
		V_2 = L_46;
		// permissionCallbacks.PermissionGranted += PermissionGrantedCallback;
		PermissionCallbacks_t6C5002298E6649261FCB52C244695459E5C9890F* L_47 = V_2;
		Action_1_t3CB5D1A819C3ED3F99E9E39F890F18633253949A* L_48 = (Action_1_t3CB5D1A819C3ED3F99E9E39F890F18633253949A*)il2cpp_codegen_object_new(Action_1_t3CB5D1A819C3ED3F99E9E39F890F18633253949A_il2cpp_TypeInfo_var);
		NullCheck(L_48);
		Action_1__ctor_m9DC2953C55C4D7D4B7BEFE03D84DA1F9362D652C(L_48, __this, (intptr_t)((void*)OculusLoader_PermissionGrantedCallback_mE09E412A87E4A838FA3C8E3DAC98E0AB4DE2922C_RuntimeMethod_var), NULL);
		NullCheck(L_47);
		PermissionCallbacks_add_PermissionGranted_m74335D4200D9B1A7C80AB9C133F95C61FCDCDF89(L_47, L_48, NULL);
		// Permission.RequestUserPermission("com.oculus.permission.EYE_TRACKING", permissionCallbacks);
		PermissionCallbacks_t6C5002298E6649261FCB52C244695459E5C9890F* L_49 = V_2;
		Permission_RequestUserPermission_m7B8E817C03FDB5C99F22002C7181F27BF031F117(_stringLiteralFFBC2C924E5DC9232A3732A59008339C044352A3, L_49, NULL);
	}

IL_01f0:
	{
		// return displaySubsystem != null && inputSubsystem != null;
		XRDisplaySubsystem_t4B00B0BF1894A039ACFA8DDC2C2EB9301118C1F1* L_50;
		L_50 = OculusLoader_get_displaySubsystem_mBF36D42BABD9D5DA3ECE4F2D25862BF35C29D4E3(__this, NULL);
		if (!L_50)
		{
			goto IL_0202;
		}
	}
	{
		XRInputSubsystem_tFECE6683FCAEBF05BAD05E5D612690095D8BAD34* L_51;
		L_51 = OculusLoader_get_inputSubsystem_m3690AE48575D2196C79757CE5F21C96F1A0963B2(__this, NULL);
		return (bool)((!(((RuntimeObject*)(XRInputSubsystem_tFECE6683FCAEBF05BAD05E5D612690095D8BAD34*)L_51) <= ((RuntimeObject*)(RuntimeObject*)NULL)))? 1 : 0);
	}

IL_0202:
	{
		return (bool)0;
	}
}
// System.Void Unity.XR.Oculus.OculusLoader::PermissionGrantedCallback(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void OculusLoader_PermissionGrantedCallback_mE09E412A87E4A838FA3C8E3DAC98E0AB4DE2922C (OculusLoader_tA386B9AA0786D042EA272EDF385F96C0AD1A56BB* __this, String_t* ___0_permissionName, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralFFBC2C924E5DC9232A3732A59008339C044352A3);
		s_Il2CppMethodInitialized = true;
	}
	{
		// if (permissionName == "com.oculus.permission.EYE_TRACKING")
		String_t* L_0 = ___0_permissionName;
		bool L_1;
		L_1 = String_op_Equality_m030E1B219352228970A076136E455C4E568C02C1(L_0, _stringLiteralFFBC2C924E5DC9232A3732A59008339C044352A3, NULL);
		if (!L_1)
		{
			goto IL_0013;
		}
	}
	{
		// NativeMethods.SetHasUserAuthorizedEyeTrackingPermission(true);
		NativeMethods_SetHasUserAuthorizedEyeTrackingPermission_mC35B88244AA80A2CA74022E853A6D94CF10C4ACA((bool)1, NULL);
	}

IL_0013:
	{
		// }
		return;
	}
}
// System.Boolean Unity.XR.Oculus.OculusLoader::Start()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool OculusLoader_Start_m0FBB6E2DE04857565E8B5E09165D380E24DBCCBE (OculusLoader_tA386B9AA0786D042EA272EDF385F96C0AD1A56BB* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&XRLoaderHelper_StartSubsystem_TisXRDisplaySubsystem_t4B00B0BF1894A039ACFA8DDC2C2EB9301118C1F1_mC1643794A5D3CC32BF1EE9C976CE5F23A6BB8962_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&XRLoaderHelper_StartSubsystem_TisXRInputSubsystem_tFECE6683FCAEBF05BAD05E5D612690095D8BAD34_m08AC127201FE89396BD81BEA52D40790BC0CA3FA_RuntimeMethod_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// StartSubsystem<XRDisplaySubsystem>();
		XRLoaderHelper_StartSubsystem_TisXRDisplaySubsystem_t4B00B0BF1894A039ACFA8DDC2C2EB9301118C1F1_mC1643794A5D3CC32BF1EE9C976CE5F23A6BB8962(__this, XRLoaderHelper_StartSubsystem_TisXRDisplaySubsystem_t4B00B0BF1894A039ACFA8DDC2C2EB9301118C1F1_mC1643794A5D3CC32BF1EE9C976CE5F23A6BB8962_RuntimeMethod_var);
		// StartSubsystem<XRInputSubsystem>();
		XRLoaderHelper_StartSubsystem_TisXRInputSubsystem_tFECE6683FCAEBF05BAD05E5D612690095D8BAD34_m08AC127201FE89396BD81BEA52D40790BC0CA3FA(__this, XRLoaderHelper_StartSubsystem_TisXRInputSubsystem_tFECE6683FCAEBF05BAD05E5D612690095D8BAD34_m08AC127201FE89396BD81BEA52D40790BC0CA3FA_RuntimeMethod_var);
		// Development.OverrideDeveloperModeStart();
		Development_OverrideDeveloperModeStart_m7017971A663FEE72864A6D5039091E21DB045C1B(NULL);
		// return true;
		return (bool)1;
	}
}
// System.Boolean Unity.XR.Oculus.OculusLoader::Stop()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool OculusLoader_Stop_mD8C0412877AD12612ED916895FEB52EA4CADB30F (OculusLoader_tA386B9AA0786D042EA272EDF385F96C0AD1A56BB* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&XRLoaderHelper_StopSubsystem_TisXRDisplaySubsystem_t4B00B0BF1894A039ACFA8DDC2C2EB9301118C1F1_m00A3A82597D484DE2EBB03EA9A2430AFDE44DE24_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&XRLoaderHelper_StopSubsystem_TisXRInputSubsystem_tFECE6683FCAEBF05BAD05E5D612690095D8BAD34_mE6F3E85E0C64666EE9A517CD7CF3669FB7BAC750_RuntimeMethod_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// StopSubsystem<XRDisplaySubsystem>();
		XRLoaderHelper_StopSubsystem_TisXRDisplaySubsystem_t4B00B0BF1894A039ACFA8DDC2C2EB9301118C1F1_m00A3A82597D484DE2EBB03EA9A2430AFDE44DE24(__this, XRLoaderHelper_StopSubsystem_TisXRDisplaySubsystem_t4B00B0BF1894A039ACFA8DDC2C2EB9301118C1F1_m00A3A82597D484DE2EBB03EA9A2430AFDE44DE24_RuntimeMethod_var);
		// StopSubsystem<XRInputSubsystem>();
		XRLoaderHelper_StopSubsystem_TisXRInputSubsystem_tFECE6683FCAEBF05BAD05E5D612690095D8BAD34_mE6F3E85E0C64666EE9A517CD7CF3669FB7BAC750(__this, XRLoaderHelper_StopSubsystem_TisXRInputSubsystem_tFECE6683FCAEBF05BAD05E5D612690095D8BAD34_mE6F3E85E0C64666EE9A517CD7CF3669FB7BAC750_RuntimeMethod_var);
		// Development.OverrideDeveloperModeStop();
		Development_OverrideDeveloperModeStop_m89921E0CCD7DAC487814309D82E5D42288C8BBC4(NULL);
		// return true;
		return (bool)1;
	}
}
// System.Boolean Unity.XR.Oculus.OculusLoader::Deinitialize()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool OculusLoader_Deinitialize_mC164D18317DB38A780AD46B671CAD17830ED9930 (OculusLoader_tA386B9AA0786D042EA272EDF385F96C0AD1A56BB* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&XRLoaderHelper_DestroySubsystem_TisXRDisplaySubsystem_t4B00B0BF1894A039ACFA8DDC2C2EB9301118C1F1_m8E2572BB5610CCE3B1DBA87453CFE09BB4B2B606_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&XRLoaderHelper_DestroySubsystem_TisXRInputSubsystem_tFECE6683FCAEBF05BAD05E5D612690095D8BAD34_m22B2454EB0F4E32155EEE6022768B9781DB0085F_RuntimeMethod_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// RegisterUpdateCallback.Deinitialize();
		RegisterUpdateCallback_Deinitialize_m3826762052F507B9B18E62483D8341495B5EEA13(NULL);
		// DestroySubsystem<XRDisplaySubsystem>();
		XRLoaderHelper_DestroySubsystem_TisXRDisplaySubsystem_t4B00B0BF1894A039ACFA8DDC2C2EB9301118C1F1_m8E2572BB5610CCE3B1DBA87453CFE09BB4B2B606(__this, XRLoaderHelper_DestroySubsystem_TisXRDisplaySubsystem_t4B00B0BF1894A039ACFA8DDC2C2EB9301118C1F1_m8E2572BB5610CCE3B1DBA87453CFE09BB4B2B606_RuntimeMethod_var);
		// DestroySubsystem<XRInputSubsystem>();
		XRLoaderHelper_DestroySubsystem_TisXRInputSubsystem_tFECE6683FCAEBF05BAD05E5D612690095D8BAD34_m22B2454EB0F4E32155EEE6022768B9781DB0085F(__this, XRLoaderHelper_DestroySubsystem_TisXRInputSubsystem_tFECE6683FCAEBF05BAD05E5D612690095D8BAD34_m22B2454EB0F4E32155EEE6022768B9781DB0085F_RuntimeMethod_var);
		// return true;
		return (bool)1;
	}
}
// System.Void Unity.XR.Oculus.OculusLoader::RuntimeLoadOVRPlugin()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void OculusLoader_RuntimeLoadOVRPlugin_m32633DD701CFD191697981E7111D34B6DF482ABC (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&OculusLoader_tA386B9AA0786D042EA272EDF385F96C0AD1A56BB_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral066D7D93F8175DDAAA3D6E4337D52AB827615B03);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral8A017E46CE09C02B042A499A98229FB4CB75E992);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralDA39A3EE5E6B4B0D3255BFEF95601890AFD80709);
		s_Il2CppMethodInitialized = true;
	}
	il2cpp::utils::ExceptionSupportStack<RuntimeObject*, 1> __active_exceptions;
	int32_t G_B2_0 = 0;
	int32_t G_B1_0 = 0;
	{
		// var supported = IsDeviceSupported();
		il2cpp_codegen_runtime_class_init_inline(OculusLoader_tA386B9AA0786D042EA272EDF385F96C0AD1A56BB_il2cpp_TypeInfo_var);
		int32_t L_0;
		L_0 = OculusLoader_IsDeviceSupported_m478A97E527F8DA4B6C08459445739C2E9C72C36D(NULL);
		// if (supported == DeviceSupportedResult.ExitApplication)
		int32_t L_1 = L_0;
		G_B1_0 = L_1;
		if ((!(((uint32_t)L_1) == ((uint32_t)2))))
		{
			G_B2_0 = L_1;
			goto IL_0018;
		}
	}
	{
		// Debug.LogError("\n\nExiting application:\n\nThis .apk was built with the Oculus XR Plugin loader enabled, but is attempting to run on a non-Oculus device.\nTo build for general Android devices, please disable the Oculus XR Plugin before building the Android player.\n\n\n");
		il2cpp_codegen_runtime_class_init_inline(Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var);
		Debug_LogError_mB00B2B4468EF3CAF041B038D840820FB84C924B2(_stringLiteral8A017E46CE09C02B042A499A98229FB4CB75E992, NULL);
		// Application.Quit();
		Application_Quit_mE304382DB9A6455C2A474C8F364C7387F37E9281(NULL);
		G_B2_0 = G_B1_0;
	}

IL_0018:
	{
		// if (supported != DeviceSupportedResult.Supported)
		if (!G_B2_0)
		{
			goto IL_001b;
		}
	}
	{
		// return;
		return;
	}

IL_001b:
	{
	}
	try
	{// begin try (depth: 1)
		{
			// if (!NativeMethods.LoadOVRPlugin(""))
			bool L_2;
			L_2 = NativeMethods_LoadOVRPlugin_mE7DA65BF795C1E1E7DB69B59793C413556875F7F(_stringLiteralDA39A3EE5E6B4B0D3255BFEF95601890AFD80709, NULL);
			if (L_2)
			{
				goto IL_0032_1;
			}
		}
		{
			// Debug.LogError("Failed to load libOVRPlugin.so");
			il2cpp_codegen_runtime_class_init_inline(Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var);
			Debug_LogError_mB00B2B4468EF3CAF041B038D840820FB84C924B2(_stringLiteral066D7D93F8175DDAAA3D6E4337D52AB827615B03, NULL);
		}

IL_0032_1:
		{
			// }
			goto IL_0037;
		}
	}// end try (depth: 1)
	catch(Il2CppExceptionWrapper& e)
	{
		if(il2cpp_codegen_class_is_assignable_from (((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&RuntimeObject_il2cpp_TypeInfo_var)), il2cpp_codegen_object_class(e.ex)))
		{
			IL2CPP_PUSH_ACTIVE_EXCEPTION(e.ex);
			goto CATCH_0034;
		}
		throw e;
	}

CATCH_0034:
	{// begin catch(System.Object)
		// catch
		// }
		IL2CPP_POP_ACTIVE_EXCEPTION();
		goto IL_0037;
	}// end catch (depth: 1)

IL_0037:
	{
		// }
		return;
	}
}
// Unity.XR.Oculus.OculusSettings Unity.XR.Oculus.OculusLoader::GetSettings()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR OculusSettings_t0584FB71432B697479FD0BFC5B68C195F17CD321* OculusLoader_GetSettings_m2FA61767F36A8CFB03124EB295A79C7A7F0A863B (OculusLoader_tA386B9AA0786D042EA272EDF385F96C0AD1A56BB* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&OculusSettings_t0584FB71432B697479FD0BFC5B68C195F17CD321_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// settings = OculusSettings.s_Settings;
		OculusSettings_t0584FB71432B697479FD0BFC5B68C195F17CD321* L_0 = ((OculusSettings_t0584FB71432B697479FD0BFC5B68C195F17CD321_StaticFields*)il2cpp_codegen_static_fields_for(OculusSettings_t0584FB71432B697479FD0BFC5B68C195F17CD321_il2cpp_TypeInfo_var))->___s_Settings_23;
		// return settings;
		return L_0;
	}
}
// System.Boolean Unity.XR.Oculus.OculusLoader::CheckUnityVersionCompatibility()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool OculusLoader_CheckUnityVersionCompatibility_mC211D3EFA35B0E509154B8F2993B08079A5C4413 (OculusLoader_tA386B9AA0786D042EA272EDF385F96C0AD1A56BB* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Regex_tE773142C2BE45C5D362B0F815AFF831707A51772_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral0A04B971B03DA607CE6C455184037B660CA89F78);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral197E4C0FFF522D6C8FD3D9B10A88C7BA37107AAD);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral6B78D11BAB6670A660082318236CEB73140FC391);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral82C791C1966A9B7EFCEB102734ECB5B1DB8AF742);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral9DD0635215B9CAFE0FAF9AA23554729A2A8BD63A);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralD3F9023582F96AC5F3DEB69BCAC72DB7F59028A8);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralDC7C9CB0A5D4E7E14BC551B5F290A52563997520);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralF1CA3E581C0778F5F9FE0D32597E39A13E302CAB);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralF3C6C902DBF80139640F6554F0C3392016A8ADF7);
		s_Il2CppMethodInitialized = true;
	}
	String_t* V_0 = NULL;
	Match_tFBEBCF225BD8EA17BCE6CE3FE5C1BD8E3074105F* V_1 = NULL;
	int32_t V_2 = 0;
	int32_t V_3 = 0;
	int32_t V_4 = 0;
	String_t* V_5 = NULL;
	{
		// var unityVersion = Application.unityVersion;
		String_t* L_0;
		L_0 = Application_get_unityVersion_m27BB3207901305BD239E1C3A74035E15CF3E5D21(NULL);
		// var versionRegex = @"^(?<major>\d+)\.(?<minor>\d+)\.(?<patch>\d+)(?<type>.).*";
		V_0 = _stringLiteral6B78D11BAB6670A660082318236CEB73140FC391;
		// var match = Regex.Match(unityVersion, versionRegex);
		String_t* L_1 = V_0;
		il2cpp_codegen_runtime_class_init_inline(Regex_tE773142C2BE45C5D362B0F815AFF831707A51772_il2cpp_TypeInfo_var);
		Match_tFBEBCF225BD8EA17BCE6CE3FE5C1BD8E3074105F* L_2;
		L_2 = Regex_Match_mE3EC82B72BF82AA4B8749251C12C383047531972(L_0, L_1, NULL);
		V_1 = L_2;
		// if (!match.Success)
		Match_tFBEBCF225BD8EA17BCE6CE3FE5C1BD8E3074105F* L_3 = V_1;
		NullCheck(L_3);
		bool L_4;
		L_4 = Group_get_Success_m4E0238EE4B1E7F927E2AF13E2E5901BCA92BE62F(L_3, NULL);
		if (L_4)
		{
			goto IL_001c;
		}
	}
	{
		// return true;
		return (bool)1;
	}

IL_001c:
	{
		// int major = Int32.Parse((match.Groups["major"].Value));
		Match_tFBEBCF225BD8EA17BCE6CE3FE5C1BD8E3074105F* L_5 = V_1;
		NullCheck(L_5);
		GroupCollection_tFFA1789730DD9EA122FBE77DC03BFEDCC3F2945E* L_6;
		L_6 = VirtualFuncInvoker0< GroupCollection_tFFA1789730DD9EA122FBE77DC03BFEDCC3F2945E* >::Invoke(5 /* System.Text.RegularExpressions.GroupCollection System.Text.RegularExpressions.Match::get_Groups() */, L_5);
		NullCheck(L_6);
		Group_t26371E9136D6F43782C487B63C67C5FC4F472881* L_7;
		L_7 = GroupCollection_get_Item_mE9B3A83B3563620EF70CFCD5F13E404864351B7A(L_6, _stringLiteral82C791C1966A9B7EFCEB102734ECB5B1DB8AF742, NULL);
		NullCheck(L_7);
		String_t* L_8;
		L_8 = Capture_get_Value_m1AB4193C2FC4B0D08AA34FECF10D03876D848BDC(L_7, NULL);
		int32_t L_9;
		L_9 = Int32_Parse_m273CA1A9C7717C99641291A95C543711C0202AF0(L_8, NULL);
		V_2 = L_9;
		// int minor = Int32.Parse((match.Groups["minor"].Value));
		Match_tFBEBCF225BD8EA17BCE6CE3FE5C1BD8E3074105F* L_10 = V_1;
		NullCheck(L_10);
		GroupCollection_tFFA1789730DD9EA122FBE77DC03BFEDCC3F2945E* L_11;
		L_11 = VirtualFuncInvoker0< GroupCollection_tFFA1789730DD9EA122FBE77DC03BFEDCC3F2945E* >::Invoke(5 /* System.Text.RegularExpressions.GroupCollection System.Text.RegularExpressions.Match::get_Groups() */, L_10);
		NullCheck(L_11);
		Group_t26371E9136D6F43782C487B63C67C5FC4F472881* L_12;
		L_12 = GroupCollection_get_Item_mE9B3A83B3563620EF70CFCD5F13E404864351B7A(L_11, _stringLiteralD3F9023582F96AC5F3DEB69BCAC72DB7F59028A8, NULL);
		NullCheck(L_12);
		String_t* L_13;
		L_13 = Capture_get_Value_m1AB4193C2FC4B0D08AA34FECF10D03876D848BDC(L_12, NULL);
		int32_t L_14;
		L_14 = Int32_Parse_m273CA1A9C7717C99641291A95C543711C0202AF0(L_13, NULL);
		V_3 = L_14;
		// int patch = Int32.Parse((match.Groups["patch"].Value));
		Match_tFBEBCF225BD8EA17BCE6CE3FE5C1BD8E3074105F* L_15 = V_1;
		NullCheck(L_15);
		GroupCollection_tFFA1789730DD9EA122FBE77DC03BFEDCC3F2945E* L_16;
		L_16 = VirtualFuncInvoker0< GroupCollection_tFFA1789730DD9EA122FBE77DC03BFEDCC3F2945E* >::Invoke(5 /* System.Text.RegularExpressions.GroupCollection System.Text.RegularExpressions.Match::get_Groups() */, L_15);
		NullCheck(L_16);
		Group_t26371E9136D6F43782C487B63C67C5FC4F472881* L_17;
		L_17 = GroupCollection_get_Item_mE9B3A83B3563620EF70CFCD5F13E404864351B7A(L_16, _stringLiteralF1CA3E581C0778F5F9FE0D32597E39A13E302CAB, NULL);
		NullCheck(L_17);
		String_t* L_18;
		L_18 = Capture_get_Value_m1AB4193C2FC4B0D08AA34FECF10D03876D848BDC(L_17, NULL);
		int32_t L_19;
		L_19 = Int32_Parse_m273CA1A9C7717C99641291A95C543711C0202AF0(L_18, NULL);
		V_4 = L_19;
		// var type = match.Groups["type"].Value;
		Match_tFBEBCF225BD8EA17BCE6CE3FE5C1BD8E3074105F* L_20 = V_1;
		NullCheck(L_20);
		GroupCollection_tFFA1789730DD9EA122FBE77DC03BFEDCC3F2945E* L_21;
		L_21 = VirtualFuncInvoker0< GroupCollection_tFFA1789730DD9EA122FBE77DC03BFEDCC3F2945E* >::Invoke(5 /* System.Text.RegularExpressions.GroupCollection System.Text.RegularExpressions.Match::get_Groups() */, L_20);
		NullCheck(L_21);
		Group_t26371E9136D6F43782C487B63C67C5FC4F472881* L_22;
		L_22 = GroupCollection_get_Item_mE9B3A83B3563620EF70CFCD5F13E404864351B7A(L_21, _stringLiteralF3C6C902DBF80139640F6554F0C3392016A8ADF7, NULL);
		NullCheck(L_22);
		String_t* L_23;
		L_23 = Capture_get_Value_m1AB4193C2FC4B0D08AA34FECF10D03876D848BDC(L_22, NULL);
		V_5 = L_23;
		// if (major == 2021)
		int32_t L_24 = V_2;
		if ((!(((uint32_t)L_24) == ((uint32_t)((int32_t)2021)))))
		{
			goto IL_00a6;
		}
	}
	{
		// if ((minor < 3) || ((minor == 3) && (patch < 4)))
		int32_t L_25 = V_3;
		if ((((int32_t)L_25) < ((int32_t)3)))
		{
			goto IL_009a;
		}
	}
	{
		int32_t L_26 = V_3;
		if ((!(((uint32_t)L_26) == ((uint32_t)3))))
		{
			goto IL_00a6;
		}
	}
	{
		int32_t L_27 = V_4;
		if ((((int32_t)L_27) >= ((int32_t)4)))
		{
			goto IL_00a6;
		}
	}

IL_009a:
	{
		// Debug.LogWarning("This version of the Oculus XR Plugin requires at least Unity 2021.3.4f1.\n" +
		//                  "Please update to that version or higher of Unity, or use the verified Oculus XR Plugin package for this version of Unity.");
		il2cpp_codegen_runtime_class_init_inline(Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var);
		Debug_LogWarning_m33EF1B897E0C7C6FF538989610BFAFFEF4628CA9(_stringLiteralDC7C9CB0A5D4E7E14BC551B5F290A52563997520, NULL);
		// return false;
		return (bool)0;
	}

IL_00a6:
	{
		// if ((major == 2022) && (minor == 1) && (patch < 12))
		int32_t L_28 = V_2;
		if ((!(((uint32_t)L_28) == ((uint32_t)((int32_t)2022)))))
		{
			goto IL_00c4;
		}
	}
	{
		int32_t L_29 = V_3;
		if ((!(((uint32_t)L_29) == ((uint32_t)1))))
		{
			goto IL_00c4;
		}
	}
	{
		int32_t L_30 = V_4;
		if ((((int32_t)L_30) >= ((int32_t)((int32_t)12))))
		{
			goto IL_00c4;
		}
	}
	{
		// Debug.LogWarning("This version of the Oculus XR Plugin requires at least Unity 2022.1.12f1.\n" +
		//                  "Please update to that version or higher of Unity, or use the verified Oculus XR Plugin package for this version of Unity.");
		il2cpp_codegen_runtime_class_init_inline(Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var);
		Debug_LogWarning_m33EF1B897E0C7C6FF538989610BFAFFEF4628CA9(_stringLiteral9DD0635215B9CAFE0FAF9AA23554729A2A8BD63A, NULL);
		// return false;
		return (bool)0;
	}

IL_00c4:
	{
		// if ((major == 2022) && (minor == 2) && (patch == 0) && (type == "a"))
		int32_t L_31 = V_2;
		if ((!(((uint32_t)L_31) == ((uint32_t)((int32_t)2022)))))
		{
			goto IL_00ee;
		}
	}
	{
		int32_t L_32 = V_3;
		if ((!(((uint32_t)L_32) == ((uint32_t)2))))
		{
			goto IL_00ee;
		}
	}
	{
		int32_t L_33 = V_4;
		if (L_33)
		{
			goto IL_00ee;
		}
	}
	{
		String_t* L_34 = V_5;
		bool L_35;
		L_35 = String_op_Equality_m030E1B219352228970A076136E455C4E568C02C1(L_34, _stringLiteral0A04B971B03DA607CE6C455184037B660CA89F78, NULL);
		if (!L_35)
		{
			goto IL_00ee;
		}
	}
	{
		// Debug.LogWarning("This version of the Oculus XR Plugin requires at least Unity 2022.2.0b1.\n" +
		//                  "Please update to that version or higher of Unity, or use the verified Oculus XR Plugin package for this version of Unity.");
		il2cpp_codegen_runtime_class_init_inline(Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var);
		Debug_LogWarning_m33EF1B897E0C7C6FF538989610BFAFFEF4628CA9(_stringLiteral197E4C0FFF522D6C8FD3D9B10A88C7BA37107AAD, NULL);
		// return false;
		return (bool)0;
	}

IL_00ee:
	{
		// return true;
		return (bool)1;
	}
}
// System.Void Unity.XR.Oculus.OculusLoader::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void OculusLoader__ctor_m80DAEED59119B8AE0D9F39C2F410CED5AF61007F (OculusLoader_tA386B9AA0786D042EA272EDF385F96C0AD1A56BB* __this, const RuntimeMethod* method) 
{
	{
		XRLoaderHelper__ctor_mEAD9691DBE10C223AB11AB7056ED0B3BA59D7699(__this, NULL);
		return;
	}
}
// System.Void Unity.XR.Oculus.OculusLoader::.cctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void OculusLoader__cctor_mCCDC2B69B603B15F29071A77AFB992F4A2535CCB (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1__ctor_m2211D9F948E2002179E205222318FE448863F2CD_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1__ctor_m3E15C72C5BBB246B014CD4F0B141BD78A648B773_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_tC3F021D09EFA4F3516555517B5E0D39308C9C1B4_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_tE3AE94237CE649B47E1D52E1A3120E772255FF87_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&OculusLoader_tA386B9AA0786D042EA272EDF385F96C0AD1A56BB_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// private static List<XRDisplaySubsystemDescriptor> s_DisplaySubsystemDescriptors = new List<XRDisplaySubsystemDescriptor>();
		List_1_tC3F021D09EFA4F3516555517B5E0D39308C9C1B4* L_0 = (List_1_tC3F021D09EFA4F3516555517B5E0D39308C9C1B4*)il2cpp_codegen_object_new(List_1_tC3F021D09EFA4F3516555517B5E0D39308C9C1B4_il2cpp_TypeInfo_var);
		NullCheck(L_0);
		List_1__ctor_m3E15C72C5BBB246B014CD4F0B141BD78A648B773(L_0, List_1__ctor_m3E15C72C5BBB246B014CD4F0B141BD78A648B773_RuntimeMethod_var);
		((OculusLoader_tA386B9AA0786D042EA272EDF385F96C0AD1A56BB_StaticFields*)il2cpp_codegen_static_fields_for(OculusLoader_tA386B9AA0786D042EA272EDF385F96C0AD1A56BB_il2cpp_TypeInfo_var))->___s_DisplaySubsystemDescriptors_5 = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&((OculusLoader_tA386B9AA0786D042EA272EDF385F96C0AD1A56BB_StaticFields*)il2cpp_codegen_static_fields_for(OculusLoader_tA386B9AA0786D042EA272EDF385F96C0AD1A56BB_il2cpp_TypeInfo_var))->___s_DisplaySubsystemDescriptors_5), (void*)L_0);
		// private static List<XRInputSubsystemDescriptor> s_InputSubsystemDescriptors = new List<XRInputSubsystemDescriptor>();
		List_1_tE3AE94237CE649B47E1D52E1A3120E772255FF87* L_1 = (List_1_tE3AE94237CE649B47E1D52E1A3120E772255FF87*)il2cpp_codegen_object_new(List_1_tE3AE94237CE649B47E1D52E1A3120E772255FF87_il2cpp_TypeInfo_var);
		NullCheck(L_1);
		List_1__ctor_m2211D9F948E2002179E205222318FE448863F2CD(L_1, List_1__ctor_m2211D9F948E2002179E205222318FE448863F2CD_RuntimeMethod_var);
		((OculusLoader_tA386B9AA0786D042EA272EDF385F96C0AD1A56BB_StaticFields*)il2cpp_codegen_static_fields_for(OculusLoader_tA386B9AA0786D042EA272EDF385F96C0AD1A56BB_il2cpp_TypeInfo_var))->___s_InputSubsystemDescriptors_6 = L_1;
		Il2CppCodeGenWriteBarrier((void**)(&((OculusLoader_tA386B9AA0786D042EA272EDF385F96C0AD1A56BB_StaticFields*)il2cpp_codegen_static_fields_for(OculusLoader_tA386B9AA0786D042EA272EDF385F96C0AD1A56BB_il2cpp_TypeInfo_var))->___s_InputSubsystemDescriptors_6), (void*)L_1);
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
// System.Boolean Unity.XR.Oculus.Performance::TrySetCPULevel(System.Int32)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Performance_TrySetCPULevel_mC2223CBF9E58F9A73EB88A0562EA2EDBA4ACA21C (int32_t ___0_level, const RuntimeMethod* method) 
{
	{
		// return (NativeMethods.SetCPULevel(level) == 0);
		int32_t L_0 = ___0_level;
		int32_t L_1;
		L_1 = NativeMethods_SetCPULevel_mC41C8161E7E18D20F81200D55D9E753E9C945DEF(L_0, NULL);
		return (bool)((((int32_t)L_1) == ((int32_t)0))? 1 : 0);
	}
}
// System.Boolean Unity.XR.Oculus.Performance::TrySetGPULevel(System.Int32)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Performance_TrySetGPULevel_m3700D943DDB328BB060529317E1DBE1261DD8135 (int32_t ___0_level, const RuntimeMethod* method) 
{
	{
		// return (NativeMethods.SetGPULevel(level) == 0);
		int32_t L_0 = ___0_level;
		int32_t L_1;
		L_1 = NativeMethods_SetGPULevel_m2AC7CFBE5A721004B00E1584236A864BBF65EB3F(L_0, NULL);
		return (bool)((((int32_t)L_1) == ((int32_t)0))? 1 : 0);
	}
}
// System.Boolean Unity.XR.Oculus.Performance::TryGetAvailableDisplayRefreshRates(System.Single[]&)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Performance_TryGetAvailableDisplayRefreshRates_m10DCEC50ACA2C917984F2FA0AA03D374D8282866 (SingleU5BU5D_t89DEFE97BCEDB5857010E79ECE0F52CF6E93B87C** ___0_refreshRates, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IntPtr_t_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Marshal_tD976A56A90263C3CE2B780D4B1CADADE2E70B4A7_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Performance_tB1393E2318BEDFD1E01AF8B40C104A3C938D5777_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&SingleU5BU5D_t89DEFE97BCEDB5857010E79ECE0F52CF6E93B87C_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	int32_t V_0 = 0;
	intptr_t V_1;
	memset((&V_1), 0, sizeof(V_1));
	{
		// if (cachedDisplayAvailableFrequencies == null || cachedDisplayAvailableFrequencies.Length == 0)
		SingleU5BU5D_t89DEFE97BCEDB5857010E79ECE0F52CF6E93B87C* L_0 = ((Performance_tB1393E2318BEDFD1E01AF8B40C104A3C938D5777_StaticFields*)il2cpp_codegen_static_fields_for(Performance_tB1393E2318BEDFD1E01AF8B40C104A3C938D5777_il2cpp_TypeInfo_var))->___cachedDisplayAvailableFrequencies_0;
		if (!L_0)
		{
			goto IL_000f;
		}
	}
	{
		SingleU5BU5D_t89DEFE97BCEDB5857010E79ECE0F52CF6E93B87C* L_1 = ((Performance_tB1393E2318BEDFD1E01AF8B40C104A3C938D5777_StaticFields*)il2cpp_codegen_static_fields_for(Performance_tB1393E2318BEDFD1E01AF8B40C104A3C938D5777_il2cpp_TypeInfo_var))->___cachedDisplayAvailableFrequencies_0;
		NullCheck(L_1);
		if ((((RuntimeArray*)L_1)->max_length))
		{
			goto IL_0063;
		}
	}

IL_000f:
	{
		// cachedDisplayAvailableFrequencies = new float[0];
		SingleU5BU5D_t89DEFE97BCEDB5857010E79ECE0F52CF6E93B87C* L_2 = (SingleU5BU5D_t89DEFE97BCEDB5857010E79ECE0F52CF6E93B87C*)(SingleU5BU5D_t89DEFE97BCEDB5857010E79ECE0F52CF6E93B87C*)SZArrayNew(SingleU5BU5D_t89DEFE97BCEDB5857010E79ECE0F52CF6E93B87C_il2cpp_TypeInfo_var, (uint32_t)0);
		((Performance_tB1393E2318BEDFD1E01AF8B40C104A3C938D5777_StaticFields*)il2cpp_codegen_static_fields_for(Performance_tB1393E2318BEDFD1E01AF8B40C104A3C938D5777_il2cpp_TypeInfo_var))->___cachedDisplayAvailableFrequencies_0 = L_2;
		Il2CppCodeGenWriteBarrier((void**)(&((Performance_tB1393E2318BEDFD1E01AF8B40C104A3C938D5777_StaticFields*)il2cpp_codegen_static_fields_for(Performance_tB1393E2318BEDFD1E01AF8B40C104A3C938D5777_il2cpp_TypeInfo_var))->___cachedDisplayAvailableFrequencies_0), (void*)L_2);
		// int numFrequencies = 0;
		V_0 = 0;
		// if (NativeMethods.GetDisplayAvailableFrequencies(IntPtr.Zero, ref numFrequencies) && numFrequencies > 0)
		intptr_t L_3 = ((IntPtr_t_StaticFields*)il2cpp_codegen_static_fields_for(IntPtr_t_il2cpp_TypeInfo_var))->___Zero_1;
		bool L_4;
		L_4 = NativeMethods_GetDisplayAvailableFrequencies_m7A3461EECCFE5AAFE6EA83A486D7DA8313098EEF(L_3, (&V_0), NULL);
		if (!L_4)
		{
			goto IL_0063;
		}
	}
	{
		int32_t L_5 = V_0;
		if ((((int32_t)L_5) <= ((int32_t)0)))
		{
			goto IL_0063;
		}
	}
	{
		// int size = sizeof(float) * numFrequencies;
		int32_t L_6 = V_0;
		// IntPtr ptr = Marshal.AllocHGlobal(size);
		il2cpp_codegen_runtime_class_init_inline(Marshal_tD976A56A90263C3CE2B780D4B1CADADE2E70B4A7_il2cpp_TypeInfo_var);
		intptr_t L_7;
		L_7 = Marshal_AllocHGlobal_mE1D700DF967E28BE8AB3E0D67C81A96B4FCC8F4F(((int32_t)il2cpp_codegen_multiply(4, L_6)), NULL);
		V_1 = L_7;
		// if (NativeMethods.GetDisplayAvailableFrequencies(ptr, ref numFrequencies) && numFrequencies > 0)
		intptr_t L_8 = V_1;
		bool L_9;
		L_9 = NativeMethods_GetDisplayAvailableFrequencies_m7A3461EECCFE5AAFE6EA83A486D7DA8313098EEF(L_8, (&V_0), NULL);
		if (!L_9)
		{
			goto IL_005d;
		}
	}
	{
		int32_t L_10 = V_0;
		if ((((int32_t)L_10) <= ((int32_t)0)))
		{
			goto IL_005d;
		}
	}
	{
		// cachedDisplayAvailableFrequencies = new float[numFrequencies];
		int32_t L_11 = V_0;
		SingleU5BU5D_t89DEFE97BCEDB5857010E79ECE0F52CF6E93B87C* L_12 = (SingleU5BU5D_t89DEFE97BCEDB5857010E79ECE0F52CF6E93B87C*)(SingleU5BU5D_t89DEFE97BCEDB5857010E79ECE0F52CF6E93B87C*)SZArrayNew(SingleU5BU5D_t89DEFE97BCEDB5857010E79ECE0F52CF6E93B87C_il2cpp_TypeInfo_var, (uint32_t)L_11);
		((Performance_tB1393E2318BEDFD1E01AF8B40C104A3C938D5777_StaticFields*)il2cpp_codegen_static_fields_for(Performance_tB1393E2318BEDFD1E01AF8B40C104A3C938D5777_il2cpp_TypeInfo_var))->___cachedDisplayAvailableFrequencies_0 = L_12;
		Il2CppCodeGenWriteBarrier((void**)(&((Performance_tB1393E2318BEDFD1E01AF8B40C104A3C938D5777_StaticFields*)il2cpp_codegen_static_fields_for(Performance_tB1393E2318BEDFD1E01AF8B40C104A3C938D5777_il2cpp_TypeInfo_var))->___cachedDisplayAvailableFrequencies_0), (void*)L_12);
		// Marshal.Copy(ptr, cachedDisplayAvailableFrequencies, 0, numFrequencies);
		intptr_t L_13 = V_1;
		SingleU5BU5D_t89DEFE97BCEDB5857010E79ECE0F52CF6E93B87C* L_14 = ((Performance_tB1393E2318BEDFD1E01AF8B40C104A3C938D5777_StaticFields*)il2cpp_codegen_static_fields_for(Performance_tB1393E2318BEDFD1E01AF8B40C104A3C938D5777_il2cpp_TypeInfo_var))->___cachedDisplayAvailableFrequencies_0;
		int32_t L_15 = V_0;
		il2cpp_codegen_runtime_class_init_inline(Marshal_tD976A56A90263C3CE2B780D4B1CADADE2E70B4A7_il2cpp_TypeInfo_var);
		Marshal_Copy_m4744F803E7E605726758725D11D157455BD43775(L_13, L_14, 0, L_15, NULL);
	}

IL_005d:
	{
		// Marshal.FreeHGlobal(ptr);
		intptr_t L_16 = V_1;
		il2cpp_codegen_runtime_class_init_inline(Marshal_tD976A56A90263C3CE2B780D4B1CADADE2E70B4A7_il2cpp_TypeInfo_var);
		Marshal_FreeHGlobal_m298EF0650E82E326EDA8048488DC384BB9171EB9(L_16, NULL);
	}

IL_0063:
	{
		// refreshRates = cachedDisplayAvailableFrequencies;
		SingleU5BU5D_t89DEFE97BCEDB5857010E79ECE0F52CF6E93B87C** L_17 = ___0_refreshRates;
		SingleU5BU5D_t89DEFE97BCEDB5857010E79ECE0F52CF6E93B87C* L_18 = ((Performance_tB1393E2318BEDFD1E01AF8B40C104A3C938D5777_StaticFields*)il2cpp_codegen_static_fields_for(Performance_tB1393E2318BEDFD1E01AF8B40C104A3C938D5777_il2cpp_TypeInfo_var))->___cachedDisplayAvailableFrequencies_0;
		*((RuntimeObject**)L_17) = (RuntimeObject*)L_18;
		Il2CppCodeGenWriteBarrier((void**)(RuntimeObject**)L_17, (void*)(RuntimeObject*)L_18);
		// return (cachedDisplayAvailableFrequencies.Length > 0);
		SingleU5BU5D_t89DEFE97BCEDB5857010E79ECE0F52CF6E93B87C* L_19 = ((Performance_tB1393E2318BEDFD1E01AF8B40C104A3C938D5777_StaticFields*)il2cpp_codegen_static_fields_for(Performance_tB1393E2318BEDFD1E01AF8B40C104A3C938D5777_il2cpp_TypeInfo_var))->___cachedDisplayAvailableFrequencies_0;
		NullCheck(L_19);
		return (bool)((!(((uint32_t)(((RuntimeArray*)L_19)->max_length)) <= ((uint32_t)0)))? 1 : 0);
	}
}
// System.Boolean Unity.XR.Oculus.Performance::TrySetDisplayRefreshRate(System.Single)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Performance_TrySetDisplayRefreshRate_m7820F3FE24F801F9A071688EF82E16FCEF404F95 (float ___0_refreshRate, const RuntimeMethod* method) 
{
	{
		// return NativeMethods.SetDisplayFrequency(refreshRate);
		float L_0 = ___0_refreshRate;
		bool L_1;
		L_1 = NativeMethods_SetDisplayFrequency_m1EF40C70A0CFA32EB0D5426681C13177C496F03F(L_0, NULL);
		return L_1;
	}
}
// System.Boolean Unity.XR.Oculus.Performance::TryGetDisplayRefreshRate(System.Single&)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Performance_TryGetDisplayRefreshRate_m0CCDA416DC24AB8B3171B4DA28DCCD63631323FA (float* ___0_refreshRate, const RuntimeMethod* method) 
{
	{
		// return NativeMethods.GetDisplayFrequency(out refreshRate);
		float* L_0 = ___0_refreshRate;
		bool L_1;
		L_1 = NativeMethods_GetDisplayFrequency_mB096687577976716563167FA72DD77F669A18C65(L_0, NULL);
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
// System.String Unity.XR.Oculus.Stats::get_PluginVersion()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* Stats_get_PluginVersion_m1C241BBDEE69FFFF57ACEEE42B441C75C34C4EA4 (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Array_IndexOf_TisByte_t94D9231AC217BE4D2E004C4CD32DF6D099EA41A3_m3E47AE0BD5292CA28182DF90A3A3B19D2DBE1B6F_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Stats_t532BDF72F0CFA5121B447784EC03851E18A51BFB_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&String_t_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* V_0 = NULL;
	int32_t V_1 = 0;
	{
		// if (string.Equals(string.Empty, m_PluginVersion))
		String_t* L_0 = ((String_t_StaticFields*)il2cpp_codegen_static_fields_for(String_t_il2cpp_TypeInfo_var))->___Empty_6;
		il2cpp_codegen_runtime_class_init_inline(Stats_t532BDF72F0CFA5121B447784EC03851E18A51BFB_il2cpp_TypeInfo_var);
		String_t* L_1 = ((Stats_t532BDF72F0CFA5121B447784EC03851E18A51BFB_StaticFields*)il2cpp_codegen_static_fields_for(Stats_t532BDF72F0CFA5121B447784EC03851E18A51BFB_il2cpp_TypeInfo_var))->___m_PluginVersion_1;
		bool L_2;
		L_2 = String_Equals_m3354EFE6393BED8DD6E18F69BEA131AAADCC622D(L_0, L_1, NULL);
		if (!L_2)
		{
			goto IL_003c;
		}
	}
	{
		// byte[] buf = new byte[256];
		ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* L_3 = (ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031*)(ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031*)SZArrayNew(ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031_il2cpp_TypeInfo_var, (uint32_t)((int32_t)256));
		V_0 = L_3;
		// NativeMethods.GetOVRPVersion(buf);
		ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* L_4 = V_0;
		NativeMethods_GetOVRPVersion_m38823D86CD59C69E62D5835B798C1E1BAAF696BA(L_4, NULL);
		// var end = Array.IndexOf<byte>(buf, 0);
		ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* L_5 = V_0;
		int32_t L_6;
		L_6 = Array_IndexOf_TisByte_t94D9231AC217BE4D2E004C4CD32DF6D099EA41A3_m3E47AE0BD5292CA28182DF90A3A3B19D2DBE1B6F(L_5, (uint8_t)0, Array_IndexOf_TisByte_t94D9231AC217BE4D2E004C4CD32DF6D099EA41A3_m3E47AE0BD5292CA28182DF90A3A3B19D2DBE1B6F_RuntimeMethod_var);
		V_1 = L_6;
		// m_PluginVersion = System.Text.Encoding.ASCII.GetString(buf, 0, end);
		Encoding_t65CDEF28CF20A7B8C92E85A4E808920C2465F095* L_7;
		L_7 = Encoding_get_ASCII_mCC61B512D320FD4E2E71CC0DFDF8DDF3CD215C65(NULL);
		ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* L_8 = V_0;
		int32_t L_9 = V_1;
		NullCheck(L_7);
		String_t* L_10;
		L_10 = VirtualFuncInvoker3< String_t*, ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031*, int32_t, int32_t >::Invoke(34 /* System.String System.Text.Encoding::GetString(System.Byte[],System.Int32,System.Int32) */, L_7, L_8, 0, L_9);
		il2cpp_codegen_runtime_class_init_inline(Stats_t532BDF72F0CFA5121B447784EC03851E18A51BFB_il2cpp_TypeInfo_var);
		((Stats_t532BDF72F0CFA5121B447784EC03851E18A51BFB_StaticFields*)il2cpp_codegen_static_fields_for(Stats_t532BDF72F0CFA5121B447784EC03851E18A51BFB_il2cpp_TypeInfo_var))->___m_PluginVersion_1 = L_10;
		Il2CppCodeGenWriteBarrier((void**)(&((Stats_t532BDF72F0CFA5121B447784EC03851E18A51BFB_StaticFields*)il2cpp_codegen_static_fields_for(Stats_t532BDF72F0CFA5121B447784EC03851E18A51BFB_il2cpp_TypeInfo_var))->___m_PluginVersion_1), (void*)L_10);
	}

IL_003c:
	{
		// return m_PluginVersion;
		il2cpp_codegen_runtime_class_init_inline(Stats_t532BDF72F0CFA5121B447784EC03851E18A51BFB_il2cpp_TypeInfo_var);
		String_t* L_11 = ((Stats_t532BDF72F0CFA5121B447784EC03851E18A51BFB_StaticFields*)il2cpp_codegen_static_fields_for(Stats_t532BDF72F0CFA5121B447784EC03851E18A51BFB_il2cpp_TypeInfo_var))->___m_PluginVersion_1;
		return L_11;
	}
}
// UnityEngine.IntegratedSubsystem Unity.XR.Oculus.Stats::GetOculusDisplaySubsystem()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR IntegratedSubsystem_t990160A89854D87C0836DC589B720231C02D4CE3* Stats_GetOculusDisplaySubsystem_m9A09B8096996A92963F6226D54D2C929819D5829 (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Enumerator_Dispose_m25A1E45E653D7F73DF65C041A66C0A6E01F2964D_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Enumerator_MoveNext_m9A1452618AC875C7A20A5917509A7B90E76E6A6A_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Enumerator_get_Current_mF9383BAD37E56B1D5BCDBFF0C3ADA58BB6E67A04_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IntegratedSubsystem_1_get_SubsystemDescriptor_mF302CEB5B3EFD7EFCB9FC2B54CF739543AB2CFF3_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_GetEnumerator_mA450E85CB8D7D5CB81FAAF9D11A1D4945B942423_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1__ctor_mBE7647ECE0B8ABB952EDC379472F9E541D41D6DF_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_tA7666C6690CE2AEE97571615AD3AFCE2BB020597_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Stats_t532BDF72F0CFA5121B447784EC03851E18A51BFB_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&SubsystemManager_GetInstances_TisXRDisplaySubsystem_t4B00B0BF1894A039ACFA8DDC2C2EB9301118C1F1_mCDFAF63EF2A2778CA3677E75360BC7961FCB3370_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&SubsystemManager_t9A7261E4D0B53B996F04B8707D8E1C33AB65E824_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral753B6D37AEAF368AA772306EFBD496750FDE357A);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral86CB83E014FB5A27545E6442E0E4C0E783301DED);
		s_Il2CppMethodInitialized = true;
	}
	Enumerator_t7B44DEF95515943B67640F1A20853509F98BA143 V_0;
	memset((&V_0), 0, sizeof(V_0));
	XRDisplaySubsystem_t4B00B0BF1894A039ACFA8DDC2C2EB9301118C1F1* V_1 = NULL;
	IntegratedSubsystem_t990160A89854D87C0836DC589B720231C02D4CE3* V_2 = NULL;
	{
		// if (m_Display != null)
		il2cpp_codegen_runtime_class_init_inline(Stats_t532BDF72F0CFA5121B447784EC03851E18A51BFB_il2cpp_TypeInfo_var);
		IntegratedSubsystem_t990160A89854D87C0836DC589B720231C02D4CE3* L_0 = ((Stats_t532BDF72F0CFA5121B447784EC03851E18A51BFB_StaticFields*)il2cpp_codegen_static_fields_for(Stats_t532BDF72F0CFA5121B447784EC03851E18A51BFB_il2cpp_TypeInfo_var))->___m_Display_0;
		if (!L_0)
		{
			goto IL_000d;
		}
	}
	{
		// return m_Display;
		il2cpp_codegen_runtime_class_init_inline(Stats_t532BDF72F0CFA5121B447784EC03851E18A51BFB_il2cpp_TypeInfo_var);
		IntegratedSubsystem_t990160A89854D87C0836DC589B720231C02D4CE3* L_1 = ((Stats_t532BDF72F0CFA5121B447784EC03851E18A51BFB_StaticFields*)il2cpp_codegen_static_fields_for(Stats_t532BDF72F0CFA5121B447784EC03851E18A51BFB_il2cpp_TypeInfo_var))->___m_Display_0;
		return L_1;
	}

IL_000d:
	{
		// List<XRDisplaySubsystem> displays = new List<XRDisplaySubsystem>();
		List_1_tA7666C6690CE2AEE97571615AD3AFCE2BB020597* L_2 = (List_1_tA7666C6690CE2AEE97571615AD3AFCE2BB020597*)il2cpp_codegen_object_new(List_1_tA7666C6690CE2AEE97571615AD3AFCE2BB020597_il2cpp_TypeInfo_var);
		NullCheck(L_2);
		List_1__ctor_mBE7647ECE0B8ABB952EDC379472F9E541D41D6DF(L_2, List_1__ctor_mBE7647ECE0B8ABB952EDC379472F9E541D41D6DF_RuntimeMethod_var);
		// SubsystemManager.GetInstances(displays);
		List_1_tA7666C6690CE2AEE97571615AD3AFCE2BB020597* L_3 = L_2;
		il2cpp_codegen_runtime_class_init_inline(SubsystemManager_t9A7261E4D0B53B996F04B8707D8E1C33AB65E824_il2cpp_TypeInfo_var);
		SubsystemManager_GetInstances_TisXRDisplaySubsystem_t4B00B0BF1894A039ACFA8DDC2C2EB9301118C1F1_mCDFAF63EF2A2778CA3677E75360BC7961FCB3370(L_3, SubsystemManager_GetInstances_TisXRDisplaySubsystem_t4B00B0BF1894A039ACFA8DDC2C2EB9301118C1F1_mCDFAF63EF2A2778CA3677E75360BC7961FCB3370_RuntimeMethod_var);
		// foreach (XRDisplaySubsystem xrDisplaySubsystem in displays)
		NullCheck(L_3);
		Enumerator_t7B44DEF95515943B67640F1A20853509F98BA143 L_4;
		L_4 = List_1_GetEnumerator_mA450E85CB8D7D5CB81FAAF9D11A1D4945B942423(L_3, List_1_GetEnumerator_mA450E85CB8D7D5CB81FAAF9D11A1D4945B942423_RuntimeMethod_var);
		V_0 = L_4;
	}
	{
		auto __finallyBlock = il2cpp::utils::Finally([&]
		{

FINALLY_0060:
			{// begin finally (depth: 1)
				Enumerator_Dispose_m25A1E45E653D7F73DF65C041A66C0A6E01F2964D((&V_0), Enumerator_Dispose_m25A1E45E653D7F73DF65C041A66C0A6E01F2964D_RuntimeMethod_var);
				return;
			}// end finally (depth: 1)
		});
		try
		{// begin try (depth: 1)
			{
				goto IL_0055_1;
			}

IL_0020_1:
			{
				// foreach (XRDisplaySubsystem xrDisplaySubsystem in displays)
				XRDisplaySubsystem_t4B00B0BF1894A039ACFA8DDC2C2EB9301118C1F1* L_5;
				L_5 = Enumerator_get_Current_mF9383BAD37E56B1D5BCDBFF0C3ADA58BB6E67A04_inline((&V_0), Enumerator_get_Current_mF9383BAD37E56B1D5BCDBFF0C3ADA58BB6E67A04_RuntimeMethod_var);
				V_1 = L_5;
				// if (xrDisplaySubsystem.SubsystemDescriptor.id == "oculus display" && xrDisplaySubsystem.running)
				XRDisplaySubsystem_t4B00B0BF1894A039ACFA8DDC2C2EB9301118C1F1* L_6 = V_1;
				NullCheck(L_6);
				XRDisplaySubsystemDescriptor_t72DD88EE9094488AE723A495F48884BA4EA8311A* L_7;
				L_7 = IntegratedSubsystem_1_get_SubsystemDescriptor_mF302CEB5B3EFD7EFCB9FC2B54CF739543AB2CFF3(L_6, IntegratedSubsystem_1_get_SubsystemDescriptor_mF302CEB5B3EFD7EFCB9FC2B54CF739543AB2CFF3_RuntimeMethod_var);
				NullCheck(L_7);
				String_t* L_8;
				L_8 = IntegratedSubsystemDescriptor_get_id_m89DBA940C79ED7EFE1137E3EC4A5A53BF7052F15(L_7, NULL);
				bool L_9;
				L_9 = String_op_Equality_m030E1B219352228970A076136E455C4E568C02C1(L_8, _stringLiteral753B6D37AEAF368AA772306EFBD496750FDE357A, NULL);
				if (!L_9)
				{
					goto IL_0055_1;
				}
			}
			{
				XRDisplaySubsystem_t4B00B0BF1894A039ACFA8DDC2C2EB9301118C1F1* L_10 = V_1;
				NullCheck(L_10);
				bool L_11;
				L_11 = IntegratedSubsystem_get_running_m18AA0D7AD1CB593DC9EE5F3DC79643717509D6E8(L_10, NULL);
				if (!L_11)
				{
					goto IL_0055_1;
				}
			}
			{
				// m_Display = xrDisplaySubsystem;
				XRDisplaySubsystem_t4B00B0BF1894A039ACFA8DDC2C2EB9301118C1F1* L_12 = V_1;
				il2cpp_codegen_runtime_class_init_inline(Stats_t532BDF72F0CFA5121B447784EC03851E18A51BFB_il2cpp_TypeInfo_var);
				((Stats_t532BDF72F0CFA5121B447784EC03851E18A51BFB_StaticFields*)il2cpp_codegen_static_fields_for(Stats_t532BDF72F0CFA5121B447784EC03851E18A51BFB_il2cpp_TypeInfo_var))->___m_Display_0 = L_12;
				Il2CppCodeGenWriteBarrier((void**)(&((Stats_t532BDF72F0CFA5121B447784EC03851E18A51BFB_StaticFields*)il2cpp_codegen_static_fields_for(Stats_t532BDF72F0CFA5121B447784EC03851E18A51BFB_il2cpp_TypeInfo_var))->___m_Display_0), (void*)L_12);
				// return m_Display;
				IntegratedSubsystem_t990160A89854D87C0836DC589B720231C02D4CE3* L_13 = ((Stats_t532BDF72F0CFA5121B447784EC03851E18A51BFB_StaticFields*)il2cpp_codegen_static_fields_for(Stats_t532BDF72F0CFA5121B447784EC03851E18A51BFB_il2cpp_TypeInfo_var))->___m_Display_0;
				V_2 = L_13;
				goto IL_007e;
			}

IL_0055_1:
			{
				// foreach (XRDisplaySubsystem xrDisplaySubsystem in displays)
				bool L_14;
				L_14 = Enumerator_MoveNext_m9A1452618AC875C7A20A5917509A7B90E76E6A6A((&V_0), Enumerator_MoveNext_m9A1452618AC875C7A20A5917509A7B90E76E6A6A_RuntimeMethod_var);
				if (L_14)
				{
					goto IL_0020_1;
				}
			}
			{
				goto IL_006e;
			}
		}// end try (depth: 1)
		catch(Il2CppExceptionWrapper& e)
		{
			__finallyBlock.StoreException(e.ex);
		}
	}

IL_006e:
	{
		// Debug.LogError("No active Oculus display subsystem was found.");
		il2cpp_codegen_runtime_class_init_inline(Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var);
		Debug_LogError_mB00B2B4468EF3CAF041B038D840820FB84C924B2(_stringLiteral86CB83E014FB5A27545E6442E0E4C0E783301DED, NULL);
		// return m_Display;
		il2cpp_codegen_runtime_class_init_inline(Stats_t532BDF72F0CFA5121B447784EC03851E18A51BFB_il2cpp_TypeInfo_var);
		IntegratedSubsystem_t990160A89854D87C0836DC589B720231C02D4CE3* L_15 = ((Stats_t532BDF72F0CFA5121B447784EC03851E18A51BFB_StaticFields*)il2cpp_codegen_static_fields_for(Stats_t532BDF72F0CFA5121B447784EC03851E18A51BFB_il2cpp_TypeInfo_var))->___m_Display_0;
		return L_15;
	}

IL_007e:
	{
		// }
		IntegratedSubsystem_t990160A89854D87C0836DC589B720231C02D4CE3* L_16 = V_2;
		return L_16;
	}
}
// System.Void Unity.XR.Oculus.Stats::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Stats__ctor_m8EE6D4544ED048D96A6679385FFF8EECEA33E08B (Stats_t532BDF72F0CFA5121B447784EC03851E18A51BFB* __this, const RuntimeMethod* method) 
{
	{
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2(__this, NULL);
		return;
	}
}
// System.Void Unity.XR.Oculus.Stats::.cctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Stats__cctor_m579D3FD2DC24D63A3CB0EA4581EE345D412E4C85 (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Stats_t532BDF72F0CFA5121B447784EC03851E18A51BFB_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&String_t_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// private static string m_PluginVersion = string.Empty;
		String_t* L_0 = ((String_t_StaticFields*)il2cpp_codegen_static_fields_for(String_t_il2cpp_TypeInfo_var))->___Empty_6;
		((Stats_t532BDF72F0CFA5121B447784EC03851E18A51BFB_StaticFields*)il2cpp_codegen_static_fields_for(Stats_t532BDF72F0CFA5121B447784EC03851E18A51BFB_il2cpp_TypeInfo_var))->___m_PluginVersion_1 = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&((Stats_t532BDF72F0CFA5121B447784EC03851E18A51BFB_StaticFields*)il2cpp_codegen_static_fields_for(Stats_t532BDF72F0CFA5121B447784EC03851E18A51BFB_il2cpp_TypeInfo_var))->___m_PluginVersion_1), (void*)L_0);
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
// System.Single Unity.XR.Oculus.Stats/AdaptivePerformance::get_GPUAppTime()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float AdaptivePerformance_get_GPUAppTime_m5D37A5B750FFA0BBD001F399BF6E5528ABFF1BA2 (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Stats_t532BDF72F0CFA5121B447784EC03851E18A51BFB_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&XRDisplaySubsystem_t4B00B0BF1894A039ACFA8DDC2C2EB9301118C1F1_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	float V_0 = 0.0f;
	{
		// ((XRDisplaySubsystem) GetOculusDisplaySubsystem()).TryGetAppGPUTimeLastFrame(out val);
		il2cpp_codegen_runtime_class_init_inline(Stats_t532BDF72F0CFA5121B447784EC03851E18A51BFB_il2cpp_TypeInfo_var);
		IntegratedSubsystem_t990160A89854D87C0836DC589B720231C02D4CE3* L_0;
		L_0 = Stats_GetOculusDisplaySubsystem_m9A09B8096996A92963F6226D54D2C929819D5829(NULL);
		NullCheck(((XRDisplaySubsystem_t4B00B0BF1894A039ACFA8DDC2C2EB9301118C1F1*)CastclassClass((RuntimeObject*)L_0, XRDisplaySubsystem_t4B00B0BF1894A039ACFA8DDC2C2EB9301118C1F1_il2cpp_TypeInfo_var)));
		bool L_1;
		L_1 = XRDisplaySubsystem_TryGetAppGPUTimeLastFrame_m17BABDCD1716475A2473143A47ECCFE64F17A766(((XRDisplaySubsystem_t4B00B0BF1894A039ACFA8DDC2C2EB9301118C1F1*)CastclassClass((RuntimeObject*)L_0, XRDisplaySubsystem_t4B00B0BF1894A039ACFA8DDC2C2EB9301118C1F1_il2cpp_TypeInfo_var)), (&V_0), NULL);
		// return val;
		float L_2 = V_0;
		return L_2;
	}
}
// System.Single Unity.XR.Oculus.Stats/AdaptivePerformance::get_GPUCompositorTime()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float AdaptivePerformance_get_GPUCompositorTime_mA8070CCE34B43B3A0B4BEFC4CE4772D1C06521E2 (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Stats_t532BDF72F0CFA5121B447784EC03851E18A51BFB_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&XRDisplaySubsystem_t4B00B0BF1894A039ACFA8DDC2C2EB9301118C1F1_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	float V_0 = 0.0f;
	{
		// ((XRDisplaySubsystem) GetOculusDisplaySubsystem()).TryGetCompositorGPUTimeLastFrame(out val);
		il2cpp_codegen_runtime_class_init_inline(Stats_t532BDF72F0CFA5121B447784EC03851E18A51BFB_il2cpp_TypeInfo_var);
		IntegratedSubsystem_t990160A89854D87C0836DC589B720231C02D4CE3* L_0;
		L_0 = Stats_GetOculusDisplaySubsystem_m9A09B8096996A92963F6226D54D2C929819D5829(NULL);
		NullCheck(((XRDisplaySubsystem_t4B00B0BF1894A039ACFA8DDC2C2EB9301118C1F1*)CastclassClass((RuntimeObject*)L_0, XRDisplaySubsystem_t4B00B0BF1894A039ACFA8DDC2C2EB9301118C1F1_il2cpp_TypeInfo_var)));
		bool L_1;
		L_1 = XRDisplaySubsystem_TryGetCompositorGPUTimeLastFrame_m9D8587317EBC9785C2AEAD86ACC3B939A497E82D(((XRDisplaySubsystem_t4B00B0BF1894A039ACFA8DDC2C2EB9301118C1F1*)CastclassClass((RuntimeObject*)L_0, XRDisplaySubsystem_t4B00B0BF1894A039ACFA8DDC2C2EB9301118C1F1_il2cpp_TypeInfo_var)), (&V_0), NULL);
		// return val;
		float L_2 = V_0;
		return L_2;
	}
}
// System.Single Unity.XR.Oculus.Stats/AdaptivePerformance::get_MotionToPhoton()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float AdaptivePerformance_get_MotionToPhoton_m8CDBEDC36B414C003043DD62A7524037C4C111A2 (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Stats_t532BDF72F0CFA5121B447784EC03851E18A51BFB_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&XRDisplaySubsystem_t4B00B0BF1894A039ACFA8DDC2C2EB9301118C1F1_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	float V_0 = 0.0f;
	{
		// ((XRDisplaySubsystem) GetOculusDisplaySubsystem()).TryGetMotionToPhoton(out val);
		il2cpp_codegen_runtime_class_init_inline(Stats_t532BDF72F0CFA5121B447784EC03851E18A51BFB_il2cpp_TypeInfo_var);
		IntegratedSubsystem_t990160A89854D87C0836DC589B720231C02D4CE3* L_0;
		L_0 = Stats_GetOculusDisplaySubsystem_m9A09B8096996A92963F6226D54D2C929819D5829(NULL);
		NullCheck(((XRDisplaySubsystem_t4B00B0BF1894A039ACFA8DDC2C2EB9301118C1F1*)CastclassClass((RuntimeObject*)L_0, XRDisplaySubsystem_t4B00B0BF1894A039ACFA8DDC2C2EB9301118C1F1_il2cpp_TypeInfo_var)));
		bool L_1;
		L_1 = XRDisplaySubsystem_TryGetMotionToPhoton_mEF734C48F96C17C3A63305B75988F1851298D3E6(((XRDisplaySubsystem_t4B00B0BF1894A039ACFA8DDC2C2EB9301118C1F1*)CastclassClass((RuntimeObject*)L_0, XRDisplaySubsystem_t4B00B0BF1894A039ACFA8DDC2C2EB9301118C1F1_il2cpp_TypeInfo_var)), (&V_0), NULL);
		// return val;
		float L_2 = V_0;
		return L_2;
	}
}
// System.Single Unity.XR.Oculus.Stats/AdaptivePerformance::get_RefreshRate()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float AdaptivePerformance_get_RefreshRate_m6BEC7E148E6996EE4258A0B023DB1887DB04A392 (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Stats_t532BDF72F0CFA5121B447784EC03851E18A51BFB_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&XRDisplaySubsystem_t4B00B0BF1894A039ACFA8DDC2C2EB9301118C1F1_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	float V_0 = 0.0f;
	{
		// ((XRDisplaySubsystem) GetOculusDisplaySubsystem()).TryGetDisplayRefreshRate(out val);
		il2cpp_codegen_runtime_class_init_inline(Stats_t532BDF72F0CFA5121B447784EC03851E18A51BFB_il2cpp_TypeInfo_var);
		IntegratedSubsystem_t990160A89854D87C0836DC589B720231C02D4CE3* L_0;
		L_0 = Stats_GetOculusDisplaySubsystem_m9A09B8096996A92963F6226D54D2C929819D5829(NULL);
		NullCheck(((XRDisplaySubsystem_t4B00B0BF1894A039ACFA8DDC2C2EB9301118C1F1*)CastclassClass((RuntimeObject*)L_0, XRDisplaySubsystem_t4B00B0BF1894A039ACFA8DDC2C2EB9301118C1F1_il2cpp_TypeInfo_var)));
		bool L_1;
		L_1 = XRDisplaySubsystem_TryGetDisplayRefreshRate_mC92C72C1B54E33C4281B714483222D5CD11866BB(((XRDisplaySubsystem_t4B00B0BF1894A039ACFA8DDC2C2EB9301118C1F1*)CastclassClass((RuntimeObject*)L_0, XRDisplaySubsystem_t4B00B0BF1894A039ACFA8DDC2C2EB9301118C1F1_il2cpp_TypeInfo_var)), (&V_0), NULL);
		// return val;
		float L_2 = V_0;
		return L_2;
	}
}
// System.Single Unity.XR.Oculus.Stats/AdaptivePerformance::get_BatteryTemp()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float AdaptivePerformance_get_BatteryTemp_mBFD5ED14346CEE66944507B0CA27E87B952F194F (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Stats_t532BDF72F0CFA5121B447784EC03851E18A51BFB_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral650C77761A0B8B1C5C9AB2BB0D61E4767DDDB6E8);
		s_Il2CppMethodInitialized = true;
	}
	float V_0 = 0.0f;
	{
		// XRStats.TryGetStat(GetOculusDisplaySubsystem(), "batteryTemperature", out batteryTemp);
		il2cpp_codegen_runtime_class_init_inline(Stats_t532BDF72F0CFA5121B447784EC03851E18A51BFB_il2cpp_TypeInfo_var);
		IntegratedSubsystem_t990160A89854D87C0836DC589B720231C02D4CE3* L_0;
		L_0 = Stats_GetOculusDisplaySubsystem_m9A09B8096996A92963F6226D54D2C929819D5829(NULL);
		bool L_1;
		L_1 = XRStats_TryGetStat_mE91924B240A89BA396554CE3742A06210514FA8B(L_0, _stringLiteral650C77761A0B8B1C5C9AB2BB0D61E4767DDDB6E8, (&V_0), NULL);
		// return batteryTemp;
		float L_2 = V_0;
		return L_2;
	}
}
// System.Single Unity.XR.Oculus.Stats/AdaptivePerformance::get_BatteryLevel()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float AdaptivePerformance_get_BatteryLevel_m8902BBC7F8A870A702A1182B88C33F4574FA645B (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Stats_t532BDF72F0CFA5121B447784EC03851E18A51BFB_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral2089C15C4332D83D0388E9B6CF7057950BB5CD54);
		s_Il2CppMethodInitialized = true;
	}
	float V_0 = 0.0f;
	{
		// XRStats.TryGetStat(GetOculusDisplaySubsystem(), "batteryLevel", out batteryLevel);
		il2cpp_codegen_runtime_class_init_inline(Stats_t532BDF72F0CFA5121B447784EC03851E18A51BFB_il2cpp_TypeInfo_var);
		IntegratedSubsystem_t990160A89854D87C0836DC589B720231C02D4CE3* L_0;
		L_0 = Stats_GetOculusDisplaySubsystem_m9A09B8096996A92963F6226D54D2C929819D5829(NULL);
		bool L_1;
		L_1 = XRStats_TryGetStat_mE91924B240A89BA396554CE3742A06210514FA8B(L_0, _stringLiteral2089C15C4332D83D0388E9B6CF7057950BB5CD54, (&V_0), NULL);
		// return batteryLevel;
		float L_2 = V_0;
		return L_2;
	}
}
// System.Boolean Unity.XR.Oculus.Stats/AdaptivePerformance::get_PowerSavingMode()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool AdaptivePerformance_get_PowerSavingMode_m3934F288CF8E796DE7F8106F7E075D21CB1D55B0 (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Stats_t532BDF72F0CFA5121B447784EC03851E18A51BFB_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralA1B23EF8DFFD7E2D6521CBDDA3630AC111AE5F69);
		s_Il2CppMethodInitialized = true;
	}
	float V_0 = 0.0f;
	{
		// XRStats.TryGetStat(GetOculusDisplaySubsystem(), "powerSavingMode", out powerSavingMode);
		il2cpp_codegen_runtime_class_init_inline(Stats_t532BDF72F0CFA5121B447784EC03851E18A51BFB_il2cpp_TypeInfo_var);
		IntegratedSubsystem_t990160A89854D87C0836DC589B720231C02D4CE3* L_0;
		L_0 = Stats_GetOculusDisplaySubsystem_m9A09B8096996A92963F6226D54D2C929819D5829(NULL);
		bool L_1;
		L_1 = XRStats_TryGetStat_mE91924B240A89BA396554CE3742A06210514FA8B(L_0, _stringLiteralA1B23EF8DFFD7E2D6521CBDDA3630AC111AE5F69, (&V_0), NULL);
		// return !(powerSavingMode == 0.0f);
		float L_2 = V_0;
		return (bool)((((int32_t)((((float)L_2) == ((float)(0.0f)))? 1 : 0)) == ((int32_t)0))? 1 : 0);
	}
}
// System.Single Unity.XR.Oculus.Stats/AdaptivePerformance::get_AdaptivePerformanceScale()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float AdaptivePerformance_get_AdaptivePerformanceScale_m1B4C297932F8200332FE62A551AC69A6C46B5AAF (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Stats_t532BDF72F0CFA5121B447784EC03851E18A51BFB_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral4DADF60B90978099A286AA09DF75E789888C9904);
		s_Il2CppMethodInitialized = true;
	}
	float V_0 = 0.0f;
	{
		// XRStats.TryGetStat(GetOculusDisplaySubsystem(), "adaptivePerformanceScale", out performanceScale);
		il2cpp_codegen_runtime_class_init_inline(Stats_t532BDF72F0CFA5121B447784EC03851E18A51BFB_il2cpp_TypeInfo_var);
		IntegratedSubsystem_t990160A89854D87C0836DC589B720231C02D4CE3* L_0;
		L_0 = Stats_GetOculusDisplaySubsystem_m9A09B8096996A92963F6226D54D2C929819D5829(NULL);
		bool L_1;
		L_1 = XRStats_TryGetStat_mE91924B240A89BA396554CE3742A06210514FA8B(L_0, _stringLiteral4DADF60B90978099A286AA09DF75E789888C9904, (&V_0), NULL);
		// return performanceScale;
		float L_2 = V_0;
		return L_2;
	}
}
// System.Int32 Unity.XR.Oculus.Stats/AdaptivePerformance::get_CPULevel()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t AdaptivePerformance_get_CPULevel_mDDA743A4F819E7E8E954571F0940B8C705EE5F65 (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Stats_t532BDF72F0CFA5121B447784EC03851E18A51BFB_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral18731F484474DDB7AD0F0E7C15988C0A794DEC4D);
		s_Il2CppMethodInitialized = true;
	}
	float V_0 = 0.0f;
	{
		// XRStats.TryGetStat(GetOculusDisplaySubsystem(), "cpuLevel", out cpuLevel);
		il2cpp_codegen_runtime_class_init_inline(Stats_t532BDF72F0CFA5121B447784EC03851E18A51BFB_il2cpp_TypeInfo_var);
		IntegratedSubsystem_t990160A89854D87C0836DC589B720231C02D4CE3* L_0;
		L_0 = Stats_GetOculusDisplaySubsystem_m9A09B8096996A92963F6226D54D2C929819D5829(NULL);
		bool L_1;
		L_1 = XRStats_TryGetStat_mE91924B240A89BA396554CE3742A06210514FA8B(L_0, _stringLiteral18731F484474DDB7AD0F0E7C15988C0A794DEC4D, (&V_0), NULL);
		// return (int) cpuLevel;
		float L_2 = V_0;
		return il2cpp_codegen_cast_double_to_int<int32_t>(L_2);
	}
}
// System.Int32 Unity.XR.Oculus.Stats/AdaptivePerformance::get_GPULevel()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t AdaptivePerformance_get_GPULevel_m344115378339731737EAB7847BD0A8358C139962 (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Stats_t532BDF72F0CFA5121B447784EC03851E18A51BFB_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralB0A88A6DB46B5BCFC8ED5871C81A6C91204F1E45);
		s_Il2CppMethodInitialized = true;
	}
	float V_0 = 0.0f;
	{
		// XRStats.TryGetStat(GetOculusDisplaySubsystem(), "gpuLevel", out gpuLevel);
		il2cpp_codegen_runtime_class_init_inline(Stats_t532BDF72F0CFA5121B447784EC03851E18A51BFB_il2cpp_TypeInfo_var);
		IntegratedSubsystem_t990160A89854D87C0836DC589B720231C02D4CE3* L_0;
		L_0 = Stats_GetOculusDisplaySubsystem_m9A09B8096996A92963F6226D54D2C929819D5829(NULL);
		bool L_1;
		L_1 = XRStats_TryGetStat_mE91924B240A89BA396554CE3742A06210514FA8B(L_0, _stringLiteralB0A88A6DB46B5BCFC8ED5871C81A6C91204F1E45, (&V_0), NULL);
		// return (int) gpuLevel;
		float L_2 = V_0;
		return il2cpp_codegen_cast_double_to_int<int32_t>(L_2);
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
// System.Single Unity.XR.Oculus.Stats/PerfMetrics::get_AppCPUTime()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float PerfMetrics_get_AppCPUTime_m606D329458729333566AB3CEC85717F71A6633B4 (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Stats_t532BDF72F0CFA5121B447784EC03851E18A51BFB_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral3665CEE66FFACBAAC4FEA9EBCFB744AC1F3A9A57);
		s_Il2CppMethodInitialized = true;
	}
	float V_0 = 0.0f;
	{
		// XRStats.TryGetStat(GetOculusDisplaySubsystem(), "perfmetrics.appcputime", out val);
		il2cpp_codegen_runtime_class_init_inline(Stats_t532BDF72F0CFA5121B447784EC03851E18A51BFB_il2cpp_TypeInfo_var);
		IntegratedSubsystem_t990160A89854D87C0836DC589B720231C02D4CE3* L_0;
		L_0 = Stats_GetOculusDisplaySubsystem_m9A09B8096996A92963F6226D54D2C929819D5829(NULL);
		bool L_1;
		L_1 = XRStats_TryGetStat_mE91924B240A89BA396554CE3742A06210514FA8B(L_0, _stringLiteral3665CEE66FFACBAAC4FEA9EBCFB744AC1F3A9A57, (&V_0), NULL);
		// return val;
		float L_2 = V_0;
		return L_2;
	}
}
// System.Single Unity.XR.Oculus.Stats/PerfMetrics::get_AppGPUTime()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float PerfMetrics_get_AppGPUTime_mF5A4AE1F88CE0D6D5FDD5CC3A6364475F71DD98A (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Stats_t532BDF72F0CFA5121B447784EC03851E18A51BFB_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral6E837F416B0AD538A7C4B0B672467CAD351051C1);
		s_Il2CppMethodInitialized = true;
	}
	float V_0 = 0.0f;
	{
		// XRStats.TryGetStat(GetOculusDisplaySubsystem(), "perfmetrics.appgputime", out val);
		il2cpp_codegen_runtime_class_init_inline(Stats_t532BDF72F0CFA5121B447784EC03851E18A51BFB_il2cpp_TypeInfo_var);
		IntegratedSubsystem_t990160A89854D87C0836DC589B720231C02D4CE3* L_0;
		L_0 = Stats_GetOculusDisplaySubsystem_m9A09B8096996A92963F6226D54D2C929819D5829(NULL);
		bool L_1;
		L_1 = XRStats_TryGetStat_mE91924B240A89BA396554CE3742A06210514FA8B(L_0, _stringLiteral6E837F416B0AD538A7C4B0B672467CAD351051C1, (&V_0), NULL);
		// return val;
		float L_2 = V_0;
		return L_2;
	}
}
// System.Single Unity.XR.Oculus.Stats/PerfMetrics::get_CompositorCPUTime()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float PerfMetrics_get_CompositorCPUTime_mDDF1668CD1BC0657E879DFD4F4898EBEDF9B23F2 (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Stats_t532BDF72F0CFA5121B447784EC03851E18A51BFB_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral19EEE9FEA675F3AD8283953350F19D8A2E2934A0);
		s_Il2CppMethodInitialized = true;
	}
	float V_0 = 0.0f;
	{
		// XRStats.TryGetStat(GetOculusDisplaySubsystem(), "perfmetrics.compositorcputime", out val);
		il2cpp_codegen_runtime_class_init_inline(Stats_t532BDF72F0CFA5121B447784EC03851E18A51BFB_il2cpp_TypeInfo_var);
		IntegratedSubsystem_t990160A89854D87C0836DC589B720231C02D4CE3* L_0;
		L_0 = Stats_GetOculusDisplaySubsystem_m9A09B8096996A92963F6226D54D2C929819D5829(NULL);
		bool L_1;
		L_1 = XRStats_TryGetStat_mE91924B240A89BA396554CE3742A06210514FA8B(L_0, _stringLiteral19EEE9FEA675F3AD8283953350F19D8A2E2934A0, (&V_0), NULL);
		// return val;
		float L_2 = V_0;
		return L_2;
	}
}
// System.Single Unity.XR.Oculus.Stats/PerfMetrics::get_CompositorGPUTime()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float PerfMetrics_get_CompositorGPUTime_mBF7B62F774F80FB3E7E6D62690AEEDEEE1410E1B (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Stats_t532BDF72F0CFA5121B447784EC03851E18A51BFB_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral7B4329AE6518370E7FA79EABB817A9A8F33E72A1);
		s_Il2CppMethodInitialized = true;
	}
	float V_0 = 0.0f;
	{
		// XRStats.TryGetStat(GetOculusDisplaySubsystem(), "perfmetrics.compositorgputime", out val);
		il2cpp_codegen_runtime_class_init_inline(Stats_t532BDF72F0CFA5121B447784EC03851E18A51BFB_il2cpp_TypeInfo_var);
		IntegratedSubsystem_t990160A89854D87C0836DC589B720231C02D4CE3* L_0;
		L_0 = Stats_GetOculusDisplaySubsystem_m9A09B8096996A92963F6226D54D2C929819D5829(NULL);
		bool L_1;
		L_1 = XRStats_TryGetStat_mE91924B240A89BA396554CE3742A06210514FA8B(L_0, _stringLiteral7B4329AE6518370E7FA79EABB817A9A8F33E72A1, (&V_0), NULL);
		// return val;
		float L_2 = V_0;
		return L_2;
	}
}
// System.Single Unity.XR.Oculus.Stats/PerfMetrics::get_GPUUtilization()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float PerfMetrics_get_GPUUtilization_m5621AD04B860C5586E1479BE85FDA0FD91908C2C (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Stats_t532BDF72F0CFA5121B447784EC03851E18A51BFB_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralE0C17AE8C1199F6CD8F16D39E1B77CC319F01B26);
		s_Il2CppMethodInitialized = true;
	}
	float V_0 = 0.0f;
	{
		// XRStats.TryGetStat(GetOculusDisplaySubsystem(), "perfmetrics.gpuutil", out val);
		il2cpp_codegen_runtime_class_init_inline(Stats_t532BDF72F0CFA5121B447784EC03851E18A51BFB_il2cpp_TypeInfo_var);
		IntegratedSubsystem_t990160A89854D87C0836DC589B720231C02D4CE3* L_0;
		L_0 = Stats_GetOculusDisplaySubsystem_m9A09B8096996A92963F6226D54D2C929819D5829(NULL);
		bool L_1;
		L_1 = XRStats_TryGetStat_mE91924B240A89BA396554CE3742A06210514FA8B(L_0, _stringLiteralE0C17AE8C1199F6CD8F16D39E1B77CC319F01B26, (&V_0), NULL);
		// return val;
		float L_2 = V_0;
		return L_2;
	}
}
// System.Single Unity.XR.Oculus.Stats/PerfMetrics::get_CPUUtilizationAverage()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float PerfMetrics_get_CPUUtilizationAverage_m0CEDE98E0DC04A04723C7DF9D216B67415ED4F12 (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Stats_t532BDF72F0CFA5121B447784EC03851E18A51BFB_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral9348643C476E6565E37FC0900AC244BD6F18AC32);
		s_Il2CppMethodInitialized = true;
	}
	float V_0 = 0.0f;
	{
		// XRStats.TryGetStat(GetOculusDisplaySubsystem(), "perfmetrics.cpuutilavg", out val);
		il2cpp_codegen_runtime_class_init_inline(Stats_t532BDF72F0CFA5121B447784EC03851E18A51BFB_il2cpp_TypeInfo_var);
		IntegratedSubsystem_t990160A89854D87C0836DC589B720231C02D4CE3* L_0;
		L_0 = Stats_GetOculusDisplaySubsystem_m9A09B8096996A92963F6226D54D2C929819D5829(NULL);
		bool L_1;
		L_1 = XRStats_TryGetStat_mE91924B240A89BA396554CE3742A06210514FA8B(L_0, _stringLiteral9348643C476E6565E37FC0900AC244BD6F18AC32, (&V_0), NULL);
		// return val;
		float L_2 = V_0;
		return L_2;
	}
}
// System.Single Unity.XR.Oculus.Stats/PerfMetrics::get_CPUUtilizationWorst()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float PerfMetrics_get_CPUUtilizationWorst_mE53AC89C35AD9DA3DC539DB6B6AFB0906DF26B53 (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Stats_t532BDF72F0CFA5121B447784EC03851E18A51BFB_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralF752B27A3F46D6A1A7B84CA9CC6E730C9B472E9A);
		s_Il2CppMethodInitialized = true;
	}
	float V_0 = 0.0f;
	{
		// XRStats.TryGetStat(GetOculusDisplaySubsystem(), "perfmetrics.cpuutilworst", out val);
		il2cpp_codegen_runtime_class_init_inline(Stats_t532BDF72F0CFA5121B447784EC03851E18A51BFB_il2cpp_TypeInfo_var);
		IntegratedSubsystem_t990160A89854D87C0836DC589B720231C02D4CE3* L_0;
		L_0 = Stats_GetOculusDisplaySubsystem_m9A09B8096996A92963F6226D54D2C929819D5829(NULL);
		bool L_1;
		L_1 = XRStats_TryGetStat_mE91924B240A89BA396554CE3742A06210514FA8B(L_0, _stringLiteralF752B27A3F46D6A1A7B84CA9CC6E730C9B472E9A, (&V_0), NULL);
		// return val;
		float L_2 = V_0;
		return L_2;
	}
}
// System.Single Unity.XR.Oculus.Stats/PerfMetrics::get_CPUClockFrequency()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float PerfMetrics_get_CPUClockFrequency_mA5A4AAF33A303B453FFC7537A1516B55D8B25731 (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Stats_t532BDF72F0CFA5121B447784EC03851E18A51BFB_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral7CF7D253C5E081CD8124B453E189315E3AB51312);
		s_Il2CppMethodInitialized = true;
	}
	float V_0 = 0.0f;
	{
		// XRStats.TryGetStat(GetOculusDisplaySubsystem(), "perfmetrics.cpuclockfreq", out val);
		il2cpp_codegen_runtime_class_init_inline(Stats_t532BDF72F0CFA5121B447784EC03851E18A51BFB_il2cpp_TypeInfo_var);
		IntegratedSubsystem_t990160A89854D87C0836DC589B720231C02D4CE3* L_0;
		L_0 = Stats_GetOculusDisplaySubsystem_m9A09B8096996A92963F6226D54D2C929819D5829(NULL);
		bool L_1;
		L_1 = XRStats_TryGetStat_mE91924B240A89BA396554CE3742A06210514FA8B(L_0, _stringLiteral7CF7D253C5E081CD8124B453E189315E3AB51312, (&V_0), NULL);
		// return val;
		float L_2 = V_0;
		return L_2;
	}
}
// System.Single Unity.XR.Oculus.Stats/PerfMetrics::get_GPUClockFrequency()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float PerfMetrics_get_GPUClockFrequency_mB33488C129386D0EF0D15F027FFB3C2A76A40906 (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Stats_t532BDF72F0CFA5121B447784EC03851E18A51BFB_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralB81FB9482B9D82D5EF7151BE5724BB998E6C5F83);
		s_Il2CppMethodInitialized = true;
	}
	float V_0 = 0.0f;
	{
		// XRStats.TryGetStat(GetOculusDisplaySubsystem(), "perfmetrics.gpuclockfreq", out val);
		il2cpp_codegen_runtime_class_init_inline(Stats_t532BDF72F0CFA5121B447784EC03851E18A51BFB_il2cpp_TypeInfo_var);
		IntegratedSubsystem_t990160A89854D87C0836DC589B720231C02D4CE3* L_0;
		L_0 = Stats_GetOculusDisplaySubsystem_m9A09B8096996A92963F6226D54D2C929819D5829(NULL);
		bool L_1;
		L_1 = XRStats_TryGetStat_mE91924B240A89BA396554CE3742A06210514FA8B(L_0, _stringLiteralB81FB9482B9D82D5EF7151BE5724BB998E6C5F83, (&V_0), NULL);
		// return val;
		float L_2 = V_0;
		return L_2;
	}
}
// System.Void Unity.XR.Oculus.Stats/PerfMetrics::EnablePerfMetrics(System.Boolean)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void PerfMetrics_EnablePerfMetrics_m8BCD93329F3F316FB16CDF61077D28732D4BDA67 (bool ___0_enable, const RuntimeMethod* method) 
{
	{
		// NativeMethods.EnablePerfMetrics(enable);
		bool L_0 = ___0_enable;
		NativeMethods_EnablePerfMetrics_m0701876E6E45D0BFEB5D2695338AD29090AAAC5C(L_0, NULL);
		// }
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
// System.Single Unity.XR.Oculus.Stats/AppMetrics::get_AppQueueAheadTime()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float AppMetrics_get_AppQueueAheadTime_m7D7D2E18AF4541D3D96231FEBA0E58EDE0E09F68 (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Stats_t532BDF72F0CFA5121B447784EC03851E18A51BFB_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralEB59BE99DEEFC290177DB43CF6B387636E1E0904);
		s_Il2CppMethodInitialized = true;
	}
	float V_0 = 0.0f;
	{
		// XRStats.TryGetStat(GetOculusDisplaySubsystem(), "appstats.appqueueahead", out val);
		il2cpp_codegen_runtime_class_init_inline(Stats_t532BDF72F0CFA5121B447784EC03851E18A51BFB_il2cpp_TypeInfo_var);
		IntegratedSubsystem_t990160A89854D87C0836DC589B720231C02D4CE3* L_0;
		L_0 = Stats_GetOculusDisplaySubsystem_m9A09B8096996A92963F6226D54D2C929819D5829(NULL);
		bool L_1;
		L_1 = XRStats_TryGetStat_mE91924B240A89BA396554CE3742A06210514FA8B(L_0, _stringLiteralEB59BE99DEEFC290177DB43CF6B387636E1E0904, (&V_0), NULL);
		// return val;
		float L_2 = V_0;
		return L_2;
	}
}
// System.Single Unity.XR.Oculus.Stats/AppMetrics::get_AppCPUElapsedTime()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float AppMetrics_get_AppCPUElapsedTime_m2C8258441111F39E4412939EF174893FAD8DBBFB (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Stats_t532BDF72F0CFA5121B447784EC03851E18A51BFB_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralDC74641CA8B52702019111B91E29821576E700BB);
		s_Il2CppMethodInitialized = true;
	}
	float V_0 = 0.0f;
	{
		// XRStats.TryGetStat(GetOculusDisplaySubsystem(), "appstats.cpuelapsedtime", out val);
		il2cpp_codegen_runtime_class_init_inline(Stats_t532BDF72F0CFA5121B447784EC03851E18A51BFB_il2cpp_TypeInfo_var);
		IntegratedSubsystem_t990160A89854D87C0836DC589B720231C02D4CE3* L_0;
		L_0 = Stats_GetOculusDisplaySubsystem_m9A09B8096996A92963F6226D54D2C929819D5829(NULL);
		bool L_1;
		L_1 = XRStats_TryGetStat_mE91924B240A89BA396554CE3742A06210514FA8B(L_0, _stringLiteralDC74641CA8B52702019111B91E29821576E700BB, (&V_0), NULL);
		// return val;
		float L_2 = V_0;
		return L_2;
	}
}
// System.Single Unity.XR.Oculus.Stats/AppMetrics::get_CompositorDroppedFrames()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float AppMetrics_get_CompositorDroppedFrames_m0282BBDDB0E5FD143212DA2F7E640C19D7175AC5 (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Stats_t532BDF72F0CFA5121B447784EC03851E18A51BFB_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral60EABBC07A25977B87CF58F7CB0D8D536D013DBA);
		s_Il2CppMethodInitialized = true;
	}
	float V_0 = 0.0f;
	{
		// XRStats.TryGetStat(GetOculusDisplaySubsystem(), "appstats.compositordroppedframes", out val);
		il2cpp_codegen_runtime_class_init_inline(Stats_t532BDF72F0CFA5121B447784EC03851E18A51BFB_il2cpp_TypeInfo_var);
		IntegratedSubsystem_t990160A89854D87C0836DC589B720231C02D4CE3* L_0;
		L_0 = Stats_GetOculusDisplaySubsystem_m9A09B8096996A92963F6226D54D2C929819D5829(NULL);
		bool L_1;
		L_1 = XRStats_TryGetStat_mE91924B240A89BA396554CE3742A06210514FA8B(L_0, _stringLiteral60EABBC07A25977B87CF58F7CB0D8D536D013DBA, (&V_0), NULL);
		// return val;
		float L_2 = V_0;
		return L_2;
	}
}
// System.Single Unity.XR.Oculus.Stats/AppMetrics::get_CompositorLatency()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float AppMetrics_get_CompositorLatency_mFAF066A98B7BA03541C44C4C3C892A5428AEF2F8 (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Stats_t532BDF72F0CFA5121B447784EC03851E18A51BFB_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral8B2341C27300FE7CC95F015A82D27378FA3E44C2);
		s_Il2CppMethodInitialized = true;
	}
	float V_0 = 0.0f;
	{
		// XRStats.TryGetStat(GetOculusDisplaySubsystem(), "appstats.compositorlatency", out val);
		il2cpp_codegen_runtime_class_init_inline(Stats_t532BDF72F0CFA5121B447784EC03851E18A51BFB_il2cpp_TypeInfo_var);
		IntegratedSubsystem_t990160A89854D87C0836DC589B720231C02D4CE3* L_0;
		L_0 = Stats_GetOculusDisplaySubsystem_m9A09B8096996A92963F6226D54D2C929819D5829(NULL);
		bool L_1;
		L_1 = XRStats_TryGetStat_mE91924B240A89BA396554CE3742A06210514FA8B(L_0, _stringLiteral8B2341C27300FE7CC95F015A82D27378FA3E44C2, (&V_0), NULL);
		// return val;
		float L_2 = V_0;
		return L_2;
	}
}
// System.Single Unity.XR.Oculus.Stats/AppMetrics::get_CompositorCPUTime()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float AppMetrics_get_CompositorCPUTime_mC0F68748285040FEADE7EAFF7C67AA761B5E5955 (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Stats_t532BDF72F0CFA5121B447784EC03851E18A51BFB_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral28DC90CC5E864B9BEFE7447A1CCD759D1F2D3991);
		s_Il2CppMethodInitialized = true;
	}
	float V_0 = 0.0f;
	{
		// XRStats.TryGetStat(GetOculusDisplaySubsystem(), "appstats.compositorcputime", out val);
		il2cpp_codegen_runtime_class_init_inline(Stats_t532BDF72F0CFA5121B447784EC03851E18A51BFB_il2cpp_TypeInfo_var);
		IntegratedSubsystem_t990160A89854D87C0836DC589B720231C02D4CE3* L_0;
		L_0 = Stats_GetOculusDisplaySubsystem_m9A09B8096996A92963F6226D54D2C929819D5829(NULL);
		bool L_1;
		L_1 = XRStats_TryGetStat_mE91924B240A89BA396554CE3742A06210514FA8B(L_0, _stringLiteral28DC90CC5E864B9BEFE7447A1CCD759D1F2D3991, (&V_0), NULL);
		// return val;
		float L_2 = V_0;
		return L_2;
	}
}
// System.Single Unity.XR.Oculus.Stats/AppMetrics::get_CPUStartToGPUEnd()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float AppMetrics_get_CPUStartToGPUEnd_mCD1DE2D96D31248BB067EEFE21B04B0B6E1719EA (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Stats_t532BDF72F0CFA5121B447784EC03851E18A51BFB_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralCDF3E124A1507F39224D73C8EFA9828D8BE3846B);
		s_Il2CppMethodInitialized = true;
	}
	float V_0 = 0.0f;
	{
		// XRStats.TryGetStat(GetOculusDisplaySubsystem(), "appstats.compositorcpustartgpuendelapsedtime", out val);
		il2cpp_codegen_runtime_class_init_inline(Stats_t532BDF72F0CFA5121B447784EC03851E18A51BFB_il2cpp_TypeInfo_var);
		IntegratedSubsystem_t990160A89854D87C0836DC589B720231C02D4CE3* L_0;
		L_0 = Stats_GetOculusDisplaySubsystem_m9A09B8096996A92963F6226D54D2C929819D5829(NULL);
		bool L_1;
		L_1 = XRStats_TryGetStat_mE91924B240A89BA396554CE3742A06210514FA8B(L_0, _stringLiteralCDF3E124A1507F39224D73C8EFA9828D8BE3846B, (&V_0), NULL);
		// return val;
		float L_2 = V_0;
		return L_2;
	}
}
// System.Single Unity.XR.Oculus.Stats/AppMetrics::get_GPUEndToVsync()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float AppMetrics_get_GPUEndToVsync_m592455C4344DEFD7EA676D4481EE9A504E5E0907 (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Stats_t532BDF72F0CFA5121B447784EC03851E18A51BFB_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral8695EE74D804B608F4CB465B35B41E02389AD412);
		s_Il2CppMethodInitialized = true;
	}
	float V_0 = 0.0f;
	{
		// XRStats.TryGetStat(GetOculusDisplaySubsystem(), "appstats.compositorgpuendtovsyncelapsedtime", out val);
		il2cpp_codegen_runtime_class_init_inline(Stats_t532BDF72F0CFA5121B447784EC03851E18A51BFB_il2cpp_TypeInfo_var);
		IntegratedSubsystem_t990160A89854D87C0836DC589B720231C02D4CE3* L_0;
		L_0 = Stats_GetOculusDisplaySubsystem_m9A09B8096996A92963F6226D54D2C929819D5829(NULL);
		bool L_1;
		L_1 = XRStats_TryGetStat_mE91924B240A89BA396554CE3742A06210514FA8B(L_0, _stringLiteral8695EE74D804B608F4CB465B35B41E02389AD412, (&V_0), NULL);
		// return val;
		float L_2 = V_0;
		return L_2;
	}
}
// System.Void Unity.XR.Oculus.Stats/AppMetrics::EnableAppMetrics(System.Boolean)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AppMetrics_EnableAppMetrics_mA48C5AD3B0A5D4A1CAA45A3DF5C4623536ACF9B4 (bool ___0_enable, const RuntimeMethod* method) 
{
	{
		// NativeMethods.EnableAppMetrics(enable);
		bool L_0 = ___0_enable;
		NativeMethods_EnableAppMetrics_m2B18E67E20FD049593C2A332FB34E9AB9558B719(L_0, NULL);
		// }
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
// System.Void Unity.XR.Oculus.NativeMethods::SetColorScale(System.Single,System.Single,System.Single,System.Single)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void NativeMethods_SetColorScale_m71466C371AE4D8AE0393FCA19BCB93DFF87FCF9B (float ___0_x, float ___1_y, float ___2_z, float ___3_w, const RuntimeMethod* method) 
{
	{
		// Internal.SetColorScale(x, y, z, w);
		float L_0 = ___0_x;
		float L_1 = ___1_y;
		float L_2 = ___2_z;
		float L_3 = ___3_w;
		Internal_SetColorScale_m0CF78314E44A665ECFD2F23F091AF8698EF671A5(L_0, L_1, L_2, L_3, NULL);
		// }
		return;
	}
}
// System.Void Unity.XR.Oculus.NativeMethods::SetColorOffset(System.Single,System.Single,System.Single,System.Single)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void NativeMethods_SetColorOffset_m708A9D66EF8DBDDBD1DB4F5FFCC572F007742BF8 (float ___0_x, float ___1_y, float ___2_z, float ___3_w, const RuntimeMethod* method) 
{
	{
		// Internal.SetColorOffset(x, y, z, w);
		float L_0 = ___0_x;
		float L_1 = ___1_y;
		float L_2 = ___2_z;
		float L_3 = ___3_w;
		Internal_SetColorOffset_mB2961D46D55669467F0C3DEEAD4502F28248E984(L_0, L_1, L_2, L_3, NULL);
		// }
		return;
	}
}
// System.Boolean Unity.XR.Oculus.NativeMethods::GetIsSupportedDevice()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool NativeMethods_GetIsSupportedDevice_m336DCB5F80772C6F89CBF1CA7122EDC4C35D78D4 (const RuntimeMethod* method) 
{
	{
		// return Internal.GetIsSupportedDevice();
		bool L_0;
		L_0 = Internal_GetIsSupportedDevice_m737F888EF5B63C9A5205B97FC0555CBED941568E(NULL);
		return L_0;
	}
}
// System.Boolean Unity.XR.Oculus.NativeMethods::LoadOVRPlugin(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool NativeMethods_LoadOVRPlugin_mE7DA65BF795C1E1E7DB69B59793C413556875F7F (String_t* ___0_ovrpPath, const RuntimeMethod* method) 
{
	{
		// return Internal.LoadOVRPlugin(ovrpPath);
		String_t* L_0 = ___0_ovrpPath;
		bool L_1;
		L_1 = Internal_LoadOVRPlugin_m9C05A911848136E203C79089AB828E9F8B094BA8(L_0, NULL);
		return L_1;
	}
}
// System.Void Unity.XR.Oculus.NativeMethods::UnloadOVRPlugin()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void NativeMethods_UnloadOVRPlugin_m38401A33540C13466A3F9073B42AB7ADCDD2B34E (const RuntimeMethod* method) 
{
	{
		// Internal.UnloadOVRPlugin();
		Internal_UnloadOVRPlugin_mADAC9DD8335CFB22730ECF073A49BC8C89562613(NULL);
		// }
		return;
	}
}
// System.Void Unity.XR.Oculus.NativeMethods::SetUserDefinedSettings(Unity.XR.Oculus.NativeMethods/UserDefinedSettings)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void NativeMethods_SetUserDefinedSettings_m42FD3CA27980B25D9C638E3AC876749FBB23CBA6 (UserDefinedSettings_tE823040D57E8C8C9D8A028F206230E1C7BAC8EE3 ___0_settings, const RuntimeMethod* method) 
{
	{
		// Internal.SetUserDefinedSettings(settings);
		UserDefinedSettings_tE823040D57E8C8C9D8A028F206230E1C7BAC8EE3 L_0 = ___0_settings;
		Internal_SetUserDefinedSettings_mFB3E779348369441A2822EAEB2D45F728C5C1BEC(L_0, NULL);
		// }
		return;
	}
}
// System.Void Unity.XR.Oculus.NativeMethods::SetHasUserAuthorizedEyeTrackingPermission(System.Boolean)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void NativeMethods_SetHasUserAuthorizedEyeTrackingPermission_mC35B88244AA80A2CA74022E853A6D94CF10C4ACA (bool ___0_authorized, const RuntimeMethod* method) 
{
	{
		// Internal.SetHasUserAuthorizedEyeTrackingPermission(authorized);
		bool L_0 = ___0_authorized;
		Internal_SetHasUserAuthorizedEyeTrackingPermission_m4C206014E71D2A988E9E7D8848EA7F4B10D144F8(L_0, NULL);
		// }
		return;
	}
}
// System.Int32 Unity.XR.Oculus.NativeMethods::SetCPULevel(System.Int32)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t NativeMethods_SetCPULevel_mC41C8161E7E18D20F81200D55D9E753E9C945DEF (int32_t ___0_cpuLevel, const RuntimeMethod* method) 
{
	{
		// return Internal.SetCPULevel(cpuLevel);
		int32_t L_0 = ___0_cpuLevel;
		int32_t L_1;
		L_1 = Internal_SetCPULevel_m50846DBC8F018A48D7E85322474C7ABC1294CA27(L_0, NULL);
		return L_1;
	}
}
// System.Int32 Unity.XR.Oculus.NativeMethods::SetGPULevel(System.Int32)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t NativeMethods_SetGPULevel_m2AC7CFBE5A721004B00E1584236A864BBF65EB3F (int32_t ___0_gpuLevel, const RuntimeMethod* method) 
{
	{
		// return Internal.SetGPULevel(gpuLevel);
		int32_t L_0 = ___0_gpuLevel;
		int32_t L_1;
		L_1 = Internal_SetGPULevel_m37B7AB9351972BD300DC4082CBD7BD9B3ED0E7E8(L_0, NULL);
		return L_1;
	}
}
// System.Void Unity.XR.Oculus.NativeMethods::GetOVRPVersion(System.Byte[])
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void NativeMethods_GetOVRPVersion_m38823D86CD59C69E62D5835B798C1E1BAAF696BA (ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* ___0_version, const RuntimeMethod* method) 
{
	{
		// Internal.GetOVRPVersion(version);
		ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* L_0 = ___0_version;
		Internal_GetOVRPVersion_m0520E7DF32DAF9849741C5021281EE5E08648E5B(L_0, NULL);
		// }
		return;
	}
}
// System.Void Unity.XR.Oculus.NativeMethods::EnablePerfMetrics(System.Boolean)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void NativeMethods_EnablePerfMetrics_m0701876E6E45D0BFEB5D2695338AD29090AAAC5C (bool ___0_enable, const RuntimeMethod* method) 
{
	{
		// Internal.EnablePerfMetrics(enable);
		bool L_0 = ___0_enable;
		Internal_EnablePerfMetrics_mA09098817AD178FB7866F322161AA2E96A0D9443(L_0, NULL);
		// }
		return;
	}
}
// System.Void Unity.XR.Oculus.NativeMethods::EnableAppMetrics(System.Boolean)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void NativeMethods_EnableAppMetrics_m2B18E67E20FD049593C2A332FB34E9AB9558B719 (bool ___0_enable, const RuntimeMethod* method) 
{
	{
		// Internal.EnableAppMetrics(enable);
		bool L_0 = ___0_enable;
		Internal_EnableAppMetrics_m1BA9E761EE232812C41F2768308D2E6BA559965C(L_0, NULL);
		// }
		return;
	}
}
// System.Boolean Unity.XR.Oculus.NativeMethods::SetDeveloperModeStrict(System.Boolean)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool NativeMethods_SetDeveloperModeStrict_mDD7A7FF224AC714D8D8C265C2E1452DB0AE653C8 (bool ___0_active, const RuntimeMethod* method) 
{
	{
		// return Internal.SetDeveloperModeStrict(active);
		bool L_0 = ___0_active;
		bool L_1;
		L_1 = Internal_SetDeveloperModeStrict_m0A501B72BA2BFBB119B94006225C227206A631C1(L_0, NULL);
		return L_1;
	}
}
// System.Boolean Unity.XR.Oculus.NativeMethods::GetHasInputFocus()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool NativeMethods_GetHasInputFocus_m1257051A904A1CD9F07C962F059E01223F14DF1B (const RuntimeMethod* method) 
{
	{
		// return Internal.GetAppHasInputFocus();
		bool L_0;
		L_0 = Internal_GetAppHasInputFocus_m0569830C6EDC013793FE3C523FBA23517DB07A0B(NULL);
		return L_0;
	}
}
// System.Boolean Unity.XR.Oculus.NativeMethods::GetBoundaryConfigured()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool NativeMethods_GetBoundaryConfigured_m4010CCBA97D29852557408736FCE53C8A3CB6C9F (const RuntimeMethod* method) 
{
	{
		// return Internal.GetBoundaryConfigured();
		bool L_0;
		L_0 = Internal_GetBoundaryConfigured_m449078868F8DECD125BEBA07D1D1D7EB9766E9AF(NULL);
		return L_0;
	}
}
// System.Boolean Unity.XR.Oculus.NativeMethods::GetBoundaryDimensions(Unity.XR.Oculus.Boundary/BoundaryType,UnityEngine.Vector3&)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool NativeMethods_GetBoundaryDimensions_m2C4E14BF8B564D95BA1FD6D7F6BB3CDC68A368CD (int32_t ___0_boundaryType, Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2* ___1_dimensions, const RuntimeMethod* method) 
{
	{
		// return Internal.GetBoundaryDimensions(boundaryType, out dimensions);
		int32_t L_0 = ___0_boundaryType;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2* L_1 = ___1_dimensions;
		bool L_2;
		L_2 = Internal_GetBoundaryDimensions_mAE3195FBFDE6A3213BA8748376FB0F8B7554754B(L_0, L_1, NULL);
		return L_2;
	}
}
// System.Boolean Unity.XR.Oculus.NativeMethods::GetBoundaryVisible()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool NativeMethods_GetBoundaryVisible_m68BB43585F6289250D59230FF61F4411537D169C (const RuntimeMethod* method) 
{
	{
		// return Internal.GetBoundaryVisible();
		bool L_0;
		L_0 = Internal_GetBoundaryVisible_mCD829BA8761743E005786D1B6BE9345F01A6CF3F(NULL);
		return L_0;
	}
}
// System.Void Unity.XR.Oculus.NativeMethods::SetBoundaryVisible(System.Boolean)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void NativeMethods_SetBoundaryVisible_m8281A6859FEE7659C63C644B9C37245D187FAC8F (bool ___0_boundaryVisible, const RuntimeMethod* method) 
{
	{
		// Internal.SetBoundaryVisible(boundaryVisible);
		bool L_0 = ___0_boundaryVisible;
		Internal_SetBoundaryVisible_mD04D1930919267DE52554242A2AA7B3D9955E193(L_0, NULL);
		// }
		return;
	}
}
// System.Boolean Unity.XR.Oculus.NativeMethods::GetAppShouldQuit()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool NativeMethods_GetAppShouldQuit_mF7D33F7306C09A47EEE51C3B5569497A2BA0031B (const RuntimeMethod* method) 
{
	{
		// return Internal.GetAppShouldQuit();
		bool L_0;
		L_0 = Internal_GetAppShouldQuit_m969E969C8CB61307597152EAFAA9251E145B48E2(NULL);
		return L_0;
	}
}
// System.Boolean Unity.XR.Oculus.NativeMethods::GetDisplayAvailableFrequencies(System.IntPtr,System.Int32&)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool NativeMethods_GetDisplayAvailableFrequencies_m7A3461EECCFE5AAFE6EA83A486D7DA8313098EEF (intptr_t ___0_ptr, int32_t* ___1_numFrequencies, const RuntimeMethod* method) 
{
	{
		// return Internal.GetDisplayAvailableFrequencies(ptr, ref numFrequencies);
		intptr_t L_0 = ___0_ptr;
		int32_t* L_1 = ___1_numFrequencies;
		bool L_2;
		L_2 = Internal_GetDisplayAvailableFrequencies_m265CF48B6D6244BD5999ED28F4417922A3964BCC(L_0, L_1, NULL);
		return L_2;
	}
}
// System.Boolean Unity.XR.Oculus.NativeMethods::SetDisplayFrequency(System.Single)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool NativeMethods_SetDisplayFrequency_m1EF40C70A0CFA32EB0D5426681C13177C496F03F (float ___0_refreshRate, const RuntimeMethod* method) 
{
	{
		// return Internal.SetDisplayFrequency(refreshRate);
		float L_0 = ___0_refreshRate;
		bool L_1;
		L_1 = Internal_SetDisplayFrequency_m6E8BE138EDF3AD30421E8A299597827A81F86804(L_0, NULL);
		return L_1;
	}
}
// System.Boolean Unity.XR.Oculus.NativeMethods::GetDisplayFrequency(System.Single&)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool NativeMethods_GetDisplayFrequency_mB096687577976716563167FA72DD77F669A18C65 (float* ___0_refreshRate, const RuntimeMethod* method) 
{
	{
		// return Internal.GetDisplayFrequency(out refreshRate);
		float* L_0 = ___0_refreshRate;
		bool L_1;
		L_1 = Internal_GetDisplayFrequency_mF645008727871526DAE4877348FD48E0AEFE6316(L_0, NULL);
		return L_1;
	}
}
// Unity.XR.Oculus.SystemHeadset Unity.XR.Oculus.NativeMethods::GetSystemHeadsetType()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t NativeMethods_GetSystemHeadsetType_mD18D6819F35C667E7E150E538A884850C6E89A4E (const RuntimeMethod* method) 
{
	{
		// return Internal.GetSystemHeadsetType();
		int32_t L_0;
		L_0 = Internal_GetSystemHeadsetType_m0901C44C3A772A62308D273AA37CF55BA6CEE646(NULL);
		return L_0;
	}
}
// System.Boolean Unity.XR.Oculus.NativeMethods::GetTiledMultiResSupported()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool NativeMethods_GetTiledMultiResSupported_mBEFDE3CDA036B5D2B2FADCCCC791C6035E711970 (const RuntimeMethod* method) 
{
	{
		// return Internal.GetTiledMultiResSupported();
		bool L_0;
		L_0 = Internal_GetTiledMultiResSupported_m7A36253D53F4DA8D56E1EFA242457D3771220FBF(NULL);
		return L_0;
	}
}
// System.Void Unity.XR.Oculus.NativeMethods::SetTiledMultiResLevel(System.Int32)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void NativeMethods_SetTiledMultiResLevel_mAA62ED9D93CA810457DE6B4E90DD27B877739D7B (int32_t ___0_level, const RuntimeMethod* method) 
{
	{
		// Internal.SetTiledMultiResLevel(level);
		int32_t L_0 = ___0_level;
		Internal_SetTiledMultiResLevel_m59849D5EF74DE2C7F25813486E3ED47C947F15C3(L_0, NULL);
		// }
		return;
	}
}
// System.Int32 Unity.XR.Oculus.NativeMethods::GetTiledMultiResLevel()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t NativeMethods_GetTiledMultiResLevel_mA712F64DAA92C2D399980A48ABFE282E92887012 (const RuntimeMethod* method) 
{
	{
		// return Internal.GetTiledMultiResLevel();
		int32_t L_0;
		L_0 = Internal_GetTiledMultiResLevel_mC95B03E83E572C960C79746554C070E91A43F8BC(NULL);
		return L_0;
	}
}
// System.Void Unity.XR.Oculus.NativeMethods::SetTiledMultiResDynamic(System.Boolean)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void NativeMethods_SetTiledMultiResDynamic_m8FC41964A4A054ADC21137FDD47C03248139FA05 (bool ___0_isDynamic, const RuntimeMethod* method) 
{
	{
		// Internal.SetTiledMultiResDynamic(isDynamic);
		bool L_0 = ___0_isDynamic;
		Internal_SetTiledMultiResDynamic_m92E2C4C0C862B7A30582EE5B96006FFE3367D658(L_0, NULL);
		// }
		return;
	}
}
// System.Boolean Unity.XR.Oculus.NativeMethods::GetEyeTrackedFoveatedRenderingSupported()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool NativeMethods_GetEyeTrackedFoveatedRenderingSupported_m7017B3CE2D6DE03700864069DECC810199A2C9E0 (const RuntimeMethod* method) 
{
	{
		// return Internal.GetEyeTrackedFoveatedRenderingSupported();
		bool L_0;
		L_0 = Internal_GetEyeTrackedFoveatedRenderingSupported_mA2F9106F5BD6571BB7D19D2FFDF61EF36D33F0E5(NULL);
		return L_0;
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
// System.Void Unity.XR.Oculus.NativeMethods/Internal::SetColorScale(System.Single,System.Single,System.Single,System.Single)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Internal_SetColorScale_m0CF78314E44A665ECFD2F23F091AF8698EF671A5 (float ___0_x, float ___1_y, float ___2_z, float ___3_w, const RuntimeMethod* method) 
{
	typedef void (DEFAULT_CALL *PInvokeFunc) (float, float, float, float);
	#if !FORCE_PINVOKE_INTERNAL && !FORCE_PINVOKE_OculusXRPlugin_INTERNAL
	static PInvokeFunc il2cppPInvokeFunc;
	if (il2cppPInvokeFunc == NULL)
	{
		int parameterSize = sizeof(float) + sizeof(float) + sizeof(float) + sizeof(float);
		il2cppPInvokeFunc = il2cpp_codegen_resolve_pinvoke<PInvokeFunc>(IL2CPP_NATIVE_STRING("OculusXRPlugin"), "SetColorScale", IL2CPP_CALL_DEFAULT, CHARSET_NOT_SPECIFIED, parameterSize, false);
		IL2CPP_ASSERT(il2cppPInvokeFunc != NULL);
	}
	#endif

	// Native function invocation
	#if FORCE_PINVOKE_INTERNAL || FORCE_PINVOKE_OculusXRPlugin_INTERNAL
	reinterpret_cast<PInvokeFunc>(SetColorScale)(___0_x, ___1_y, ___2_z, ___3_w);
	#else
	il2cppPInvokeFunc(___0_x, ___1_y, ___2_z, ___3_w);
	#endif

}
// System.Void Unity.XR.Oculus.NativeMethods/Internal::SetColorOffset(System.Single,System.Single,System.Single,System.Single)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Internal_SetColorOffset_mB2961D46D55669467F0C3DEEAD4502F28248E984 (float ___0_x, float ___1_y, float ___2_z, float ___3_w, const RuntimeMethod* method) 
{
	typedef void (DEFAULT_CALL *PInvokeFunc) (float, float, float, float);
	#if !FORCE_PINVOKE_INTERNAL && !FORCE_PINVOKE_OculusXRPlugin_INTERNAL
	static PInvokeFunc il2cppPInvokeFunc;
	if (il2cppPInvokeFunc == NULL)
	{
		int parameterSize = sizeof(float) + sizeof(float) + sizeof(float) + sizeof(float);
		il2cppPInvokeFunc = il2cpp_codegen_resolve_pinvoke<PInvokeFunc>(IL2CPP_NATIVE_STRING("OculusXRPlugin"), "SetColorOffset", IL2CPP_CALL_DEFAULT, CHARSET_NOT_SPECIFIED, parameterSize, false);
		IL2CPP_ASSERT(il2cppPInvokeFunc != NULL);
	}
	#endif

	// Native function invocation
	#if FORCE_PINVOKE_INTERNAL || FORCE_PINVOKE_OculusXRPlugin_INTERNAL
	reinterpret_cast<PInvokeFunc>(SetColorOffset)(___0_x, ___1_y, ___2_z, ___3_w);
	#else
	il2cppPInvokeFunc(___0_x, ___1_y, ___2_z, ___3_w);
	#endif

}
// System.Boolean Unity.XR.Oculus.NativeMethods/Internal::GetIsSupportedDevice()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Internal_GetIsSupportedDevice_m737F888EF5B63C9A5205B97FC0555CBED941568E (const RuntimeMethod* method) 
{
	typedef int32_t (DEFAULT_CALL *PInvokeFunc) ();
	#if !FORCE_PINVOKE_INTERNAL && !FORCE_PINVOKE_OculusXRPlugin_INTERNAL
	static PInvokeFunc il2cppPInvokeFunc;
	if (il2cppPInvokeFunc == NULL)
	{
		int parameterSize = 0;
		il2cppPInvokeFunc = il2cpp_codegen_resolve_pinvoke<PInvokeFunc>(IL2CPP_NATIVE_STRING("OculusXRPlugin"), "GetIsSupportedDevice", IL2CPP_CALL_DEFAULT, CHARSET_NOT_SPECIFIED, parameterSize, false);
		IL2CPP_ASSERT(il2cppPInvokeFunc != NULL);
	}
	#endif

	// Native function invocation
	#if FORCE_PINVOKE_INTERNAL || FORCE_PINVOKE_OculusXRPlugin_INTERNAL
	int32_t returnValue = reinterpret_cast<PInvokeFunc>(GetIsSupportedDevice)();
	#else
	int32_t returnValue = il2cppPInvokeFunc();
	#endif

	return static_cast<bool>(returnValue);
}
// System.Boolean Unity.XR.Oculus.NativeMethods/Internal::LoadOVRPlugin(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Internal_LoadOVRPlugin_m9C05A911848136E203C79089AB828E9F8B094BA8 (String_t* ___0_ovrpPath, const RuntimeMethod* method) 
{
	typedef int32_t (DEFAULT_CALL *PInvokeFunc) (Il2CppChar*);
	#if !FORCE_PINVOKE_INTERNAL && !FORCE_PINVOKE_OculusXRPlugin_INTERNAL
	static PInvokeFunc il2cppPInvokeFunc;
	if (il2cppPInvokeFunc == NULL)
	{
		int parameterSize = sizeof(Il2CppChar*);
		il2cppPInvokeFunc = il2cpp_codegen_resolve_pinvoke<PInvokeFunc>(IL2CPP_NATIVE_STRING("OculusXRPlugin"), "LoadOVRPlugin", IL2CPP_CALL_DEFAULT, CHARSET_UNICODE, parameterSize, false);
		IL2CPP_ASSERT(il2cppPInvokeFunc != NULL);
	}
	#endif

	// Marshaling of parameter '___0_ovrpPath' to native representation
	Il2CppChar* ____0_ovrpPath_marshaled = NULL;
	if (___0_ovrpPath != NULL)
	{
		____0_ovrpPath_marshaled = &___0_ovrpPath->____firstChar_5;
	}

	// Native function invocation
	#if FORCE_PINVOKE_INTERNAL || FORCE_PINVOKE_OculusXRPlugin_INTERNAL
	int32_t returnValue = reinterpret_cast<PInvokeFunc>(LoadOVRPlugin)(____0_ovrpPath_marshaled);
	#else
	int32_t returnValue = il2cppPInvokeFunc(____0_ovrpPath_marshaled);
	#endif

	return static_cast<bool>(returnValue);
}
// System.Void Unity.XR.Oculus.NativeMethods/Internal::UnloadOVRPlugin()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Internal_UnloadOVRPlugin_mADAC9DD8335CFB22730ECF073A49BC8C89562613 (const RuntimeMethod* method) 
{
	typedef void (DEFAULT_CALL *PInvokeFunc) ();
	#if !FORCE_PINVOKE_INTERNAL && !FORCE_PINVOKE_OculusXRPlugin_INTERNAL
	static PInvokeFunc il2cppPInvokeFunc;
	if (il2cppPInvokeFunc == NULL)
	{
		int parameterSize = 0;
		il2cppPInvokeFunc = il2cpp_codegen_resolve_pinvoke<PInvokeFunc>(IL2CPP_NATIVE_STRING("OculusXRPlugin"), "UnloadOVRPlugin", IL2CPP_CALL_DEFAULT, CHARSET_NOT_SPECIFIED, parameterSize, false);
		IL2CPP_ASSERT(il2cppPInvokeFunc != NULL);
	}
	#endif

	// Native function invocation
	#if FORCE_PINVOKE_INTERNAL || FORCE_PINVOKE_OculusXRPlugin_INTERNAL
	reinterpret_cast<PInvokeFunc>(UnloadOVRPlugin)();
	#else
	il2cppPInvokeFunc();
	#endif

}
// System.Void Unity.XR.Oculus.NativeMethods/Internal::SetUserDefinedSettings(Unity.XR.Oculus.NativeMethods/UserDefinedSettings)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Internal_SetUserDefinedSettings_mFB3E779348369441A2822EAEB2D45F728C5C1BEC (UserDefinedSettings_tE823040D57E8C8C9D8A028F206230E1C7BAC8EE3 ___0_settings, const RuntimeMethod* method) 
{
	typedef void (DEFAULT_CALL *PInvokeFunc) (UserDefinedSettings_tE823040D57E8C8C9D8A028F206230E1C7BAC8EE3);
	#if !FORCE_PINVOKE_INTERNAL && !FORCE_PINVOKE_OculusXRPlugin_INTERNAL
	static PInvokeFunc il2cppPInvokeFunc;
	if (il2cppPInvokeFunc == NULL)
	{
		int parameterSize = sizeof(UserDefinedSettings_tE823040D57E8C8C9D8A028F206230E1C7BAC8EE3) + 2;
		il2cppPInvokeFunc = il2cpp_codegen_resolve_pinvoke<PInvokeFunc>(IL2CPP_NATIVE_STRING("OculusXRPlugin"), "SetUserDefinedSettings", IL2CPP_CALL_DEFAULT, CHARSET_NOT_SPECIFIED, parameterSize, false);
		IL2CPP_ASSERT(il2cppPInvokeFunc != NULL);
	}
	#endif

	// Native function invocation
	#if FORCE_PINVOKE_INTERNAL || FORCE_PINVOKE_OculusXRPlugin_INTERNAL
	reinterpret_cast<PInvokeFunc>(SetUserDefinedSettings)(___0_settings);
	#else
	il2cppPInvokeFunc(___0_settings);
	#endif

}
// System.Void Unity.XR.Oculus.NativeMethods/Internal::SetHasUserAuthorizedEyeTrackingPermission(System.Boolean)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Internal_SetHasUserAuthorizedEyeTrackingPermission_m4C206014E71D2A988E9E7D8848EA7F4B10D144F8 (bool ___0_authorized, const RuntimeMethod* method) 
{
	typedef void (DEFAULT_CALL *PInvokeFunc) (int32_t);
	#if !FORCE_PINVOKE_INTERNAL && !FORCE_PINVOKE_OculusXRPlugin_INTERNAL
	static PInvokeFunc il2cppPInvokeFunc;
	if (il2cppPInvokeFunc == NULL)
	{
		int parameterSize = 4;
		il2cppPInvokeFunc = il2cpp_codegen_resolve_pinvoke<PInvokeFunc>(IL2CPP_NATIVE_STRING("OculusXRPlugin"), "SetHasUserAuthorizedEyeTrackingPermission", IL2CPP_CALL_DEFAULT, CHARSET_NOT_SPECIFIED, parameterSize, false);
		IL2CPP_ASSERT(il2cppPInvokeFunc != NULL);
	}
	#endif

	// Native function invocation
	#if FORCE_PINVOKE_INTERNAL || FORCE_PINVOKE_OculusXRPlugin_INTERNAL
	reinterpret_cast<PInvokeFunc>(SetHasUserAuthorizedEyeTrackingPermission)(static_cast<int32_t>(___0_authorized));
	#else
	il2cppPInvokeFunc(static_cast<int32_t>(___0_authorized));
	#endif

}
// System.Int32 Unity.XR.Oculus.NativeMethods/Internal::SetCPULevel(System.Int32)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t Internal_SetCPULevel_m50846DBC8F018A48D7E85322474C7ABC1294CA27 (int32_t ___0_cpuLevel, const RuntimeMethod* method) 
{
	typedef int32_t (DEFAULT_CALL *PInvokeFunc) (int32_t);
	#if !FORCE_PINVOKE_INTERNAL && !FORCE_PINVOKE_OculusXRPlugin_INTERNAL
	static PInvokeFunc il2cppPInvokeFunc;
	if (il2cppPInvokeFunc == NULL)
	{
		int parameterSize = sizeof(int32_t);
		il2cppPInvokeFunc = il2cpp_codegen_resolve_pinvoke<PInvokeFunc>(IL2CPP_NATIVE_STRING("OculusXRPlugin"), "SetCPULevel", IL2CPP_CALL_DEFAULT, CHARSET_NOT_SPECIFIED, parameterSize, false);
		IL2CPP_ASSERT(il2cppPInvokeFunc != NULL);
	}
	#endif

	// Native function invocation
	#if FORCE_PINVOKE_INTERNAL || FORCE_PINVOKE_OculusXRPlugin_INTERNAL
	int32_t returnValue = reinterpret_cast<PInvokeFunc>(SetCPULevel)(___0_cpuLevel);
	#else
	int32_t returnValue = il2cppPInvokeFunc(___0_cpuLevel);
	#endif

	return returnValue;
}
// System.Int32 Unity.XR.Oculus.NativeMethods/Internal::SetGPULevel(System.Int32)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t Internal_SetGPULevel_m37B7AB9351972BD300DC4082CBD7BD9B3ED0E7E8 (int32_t ___0_gpuLevel, const RuntimeMethod* method) 
{
	typedef int32_t (DEFAULT_CALL *PInvokeFunc) (int32_t);
	#if !FORCE_PINVOKE_INTERNAL && !FORCE_PINVOKE_OculusXRPlugin_INTERNAL
	static PInvokeFunc il2cppPInvokeFunc;
	if (il2cppPInvokeFunc == NULL)
	{
		int parameterSize = sizeof(int32_t);
		il2cppPInvokeFunc = il2cpp_codegen_resolve_pinvoke<PInvokeFunc>(IL2CPP_NATIVE_STRING("OculusXRPlugin"), "SetGPULevel", IL2CPP_CALL_DEFAULT, CHARSET_NOT_SPECIFIED, parameterSize, false);
		IL2CPP_ASSERT(il2cppPInvokeFunc != NULL);
	}
	#endif

	// Native function invocation
	#if FORCE_PINVOKE_INTERNAL || FORCE_PINVOKE_OculusXRPlugin_INTERNAL
	int32_t returnValue = reinterpret_cast<PInvokeFunc>(SetGPULevel)(___0_gpuLevel);
	#else
	int32_t returnValue = il2cppPInvokeFunc(___0_gpuLevel);
	#endif

	return returnValue;
}
// System.Void Unity.XR.Oculus.NativeMethods/Internal::GetOVRPVersion(System.Byte[])
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Internal_GetOVRPVersion_m0520E7DF32DAF9849741C5021281EE5E08648E5B (ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* ___0_version, const RuntimeMethod* method) 
{
	typedef void (DEFAULT_CALL *PInvokeFunc) (uint8_t*);
	#if !FORCE_PINVOKE_INTERNAL && !FORCE_PINVOKE_OculusXRPlugin_INTERNAL
	static PInvokeFunc il2cppPInvokeFunc;
	if (il2cppPInvokeFunc == NULL)
	{
		int parameterSize = sizeof(void*);
		il2cppPInvokeFunc = il2cpp_codegen_resolve_pinvoke<PInvokeFunc>(IL2CPP_NATIVE_STRING("OculusXRPlugin"), "GetOVRPVersion", IL2CPP_CALL_DEFAULT, CHARSET_UNICODE, parameterSize, false);
		IL2CPP_ASSERT(il2cppPInvokeFunc != NULL);
	}
	#endif

	// Marshaling of parameter '___0_version' to native representation
	uint8_t* ____0_version_marshaled = NULL;
	if (___0_version != NULL)
	{
		____0_version_marshaled = reinterpret_cast<uint8_t*>((___0_version)->GetAddressAtUnchecked(0));
	}

	// Native function invocation
	#if FORCE_PINVOKE_INTERNAL || FORCE_PINVOKE_OculusXRPlugin_INTERNAL
	reinterpret_cast<PInvokeFunc>(GetOVRPVersion)(____0_version_marshaled);
	#else
	il2cppPInvokeFunc(____0_version_marshaled);
	#endif

}
// System.Void Unity.XR.Oculus.NativeMethods/Internal::EnablePerfMetrics(System.Boolean)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Internal_EnablePerfMetrics_mA09098817AD178FB7866F322161AA2E96A0D9443 (bool ___0_enable, const RuntimeMethod* method) 
{
	typedef void (DEFAULT_CALL *PInvokeFunc) (int32_t);
	#if !FORCE_PINVOKE_INTERNAL && !FORCE_PINVOKE_OculusXRPlugin_INTERNAL
	static PInvokeFunc il2cppPInvokeFunc;
	if (il2cppPInvokeFunc == NULL)
	{
		int parameterSize = 4;
		il2cppPInvokeFunc = il2cpp_codegen_resolve_pinvoke<PInvokeFunc>(IL2CPP_NATIVE_STRING("OculusXRPlugin"), "EnablePerfMetrics", IL2CPP_CALL_DEFAULT, CHARSET_NOT_SPECIFIED, parameterSize, false);
		IL2CPP_ASSERT(il2cppPInvokeFunc != NULL);
	}
	#endif

	// Native function invocation
	#if FORCE_PINVOKE_INTERNAL || FORCE_PINVOKE_OculusXRPlugin_INTERNAL
	reinterpret_cast<PInvokeFunc>(EnablePerfMetrics)(static_cast<int32_t>(___0_enable));
	#else
	il2cppPInvokeFunc(static_cast<int32_t>(___0_enable));
	#endif

}
// System.Void Unity.XR.Oculus.NativeMethods/Internal::EnableAppMetrics(System.Boolean)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Internal_EnableAppMetrics_m1BA9E761EE232812C41F2768308D2E6BA559965C (bool ___0_enable, const RuntimeMethod* method) 
{
	typedef void (DEFAULT_CALL *PInvokeFunc) (int32_t);
	#if !FORCE_PINVOKE_INTERNAL && !FORCE_PINVOKE_OculusXRPlugin_INTERNAL
	static PInvokeFunc il2cppPInvokeFunc;
	if (il2cppPInvokeFunc == NULL)
	{
		int parameterSize = 4;
		il2cppPInvokeFunc = il2cpp_codegen_resolve_pinvoke<PInvokeFunc>(IL2CPP_NATIVE_STRING("OculusXRPlugin"), "EnableAppMetrics", IL2CPP_CALL_DEFAULT, CHARSET_NOT_SPECIFIED, parameterSize, false);
		IL2CPP_ASSERT(il2cppPInvokeFunc != NULL);
	}
	#endif

	// Native function invocation
	#if FORCE_PINVOKE_INTERNAL || FORCE_PINVOKE_OculusXRPlugin_INTERNAL
	reinterpret_cast<PInvokeFunc>(EnableAppMetrics)(static_cast<int32_t>(___0_enable));
	#else
	il2cppPInvokeFunc(static_cast<int32_t>(___0_enable));
	#endif

}
// System.Boolean Unity.XR.Oculus.NativeMethods/Internal::SetDeveloperModeStrict(System.Boolean)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Internal_SetDeveloperModeStrict_m0A501B72BA2BFBB119B94006225C227206A631C1 (bool ___0_active, const RuntimeMethod* method) 
{
	typedef int32_t (DEFAULT_CALL *PInvokeFunc) (int32_t);
	#if !FORCE_PINVOKE_INTERNAL && !FORCE_PINVOKE_OculusXRPlugin_INTERNAL
	static PInvokeFunc il2cppPInvokeFunc;
	if (il2cppPInvokeFunc == NULL)
	{
		int parameterSize = 4;
		il2cppPInvokeFunc = il2cpp_codegen_resolve_pinvoke<PInvokeFunc>(IL2CPP_NATIVE_STRING("OculusXRPlugin"), "SetDeveloperModeStrict", IL2CPP_CALL_DEFAULT, CHARSET_NOT_SPECIFIED, parameterSize, false);
		IL2CPP_ASSERT(il2cppPInvokeFunc != NULL);
	}
	#endif

	// Native function invocation
	#if FORCE_PINVOKE_INTERNAL || FORCE_PINVOKE_OculusXRPlugin_INTERNAL
	int32_t returnValue = reinterpret_cast<PInvokeFunc>(SetDeveloperModeStrict)(static_cast<int32_t>(___0_active));
	#else
	int32_t returnValue = il2cppPInvokeFunc(static_cast<int32_t>(___0_active));
	#endif

	return static_cast<bool>(returnValue);
}
// System.Boolean Unity.XR.Oculus.NativeMethods/Internal::GetAppHasInputFocus()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Internal_GetAppHasInputFocus_m0569830C6EDC013793FE3C523FBA23517DB07A0B (const RuntimeMethod* method) 
{
	typedef int32_t (DEFAULT_CALL *PInvokeFunc) ();
	#if !FORCE_PINVOKE_INTERNAL && !FORCE_PINVOKE_OculusXRPlugin_INTERNAL
	static PInvokeFunc il2cppPInvokeFunc;
	if (il2cppPInvokeFunc == NULL)
	{
		int parameterSize = 0;
		il2cppPInvokeFunc = il2cpp_codegen_resolve_pinvoke<PInvokeFunc>(IL2CPP_NATIVE_STRING("OculusXRPlugin"), "GetAppHasInputFocus", IL2CPP_CALL_DEFAULT, CHARSET_NOT_SPECIFIED, parameterSize, false);
		IL2CPP_ASSERT(il2cppPInvokeFunc != NULL);
	}
	#endif

	// Native function invocation
	#if FORCE_PINVOKE_INTERNAL || FORCE_PINVOKE_OculusXRPlugin_INTERNAL
	int32_t returnValue = reinterpret_cast<PInvokeFunc>(GetAppHasInputFocus)();
	#else
	int32_t returnValue = il2cppPInvokeFunc();
	#endif

	return static_cast<bool>(returnValue);
}
// System.Boolean Unity.XR.Oculus.NativeMethods/Internal::GetBoundaryConfigured()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Internal_GetBoundaryConfigured_m449078868F8DECD125BEBA07D1D1D7EB9766E9AF (const RuntimeMethod* method) 
{
	typedef int32_t (DEFAULT_CALL *PInvokeFunc) ();
	#if !FORCE_PINVOKE_INTERNAL && !FORCE_PINVOKE_OculusXRPlugin_INTERNAL
	static PInvokeFunc il2cppPInvokeFunc;
	if (il2cppPInvokeFunc == NULL)
	{
		int parameterSize = 0;
		il2cppPInvokeFunc = il2cpp_codegen_resolve_pinvoke<PInvokeFunc>(IL2CPP_NATIVE_STRING("OculusXRPlugin"), "GetBoundaryConfigured", IL2CPP_CALL_DEFAULT, CHARSET_NOT_SPECIFIED, parameterSize, false);
		IL2CPP_ASSERT(il2cppPInvokeFunc != NULL);
	}
	#endif

	// Native function invocation
	#if FORCE_PINVOKE_INTERNAL || FORCE_PINVOKE_OculusXRPlugin_INTERNAL
	int32_t returnValue = reinterpret_cast<PInvokeFunc>(GetBoundaryConfigured)();
	#else
	int32_t returnValue = il2cppPInvokeFunc();
	#endif

	return static_cast<bool>(returnValue);
}
// System.Boolean Unity.XR.Oculus.NativeMethods/Internal::GetBoundaryDimensions(Unity.XR.Oculus.Boundary/BoundaryType,UnityEngine.Vector3&)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Internal_GetBoundaryDimensions_mAE3195FBFDE6A3213BA8748376FB0F8B7554754B (int32_t ___0_boundaryType, Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2* ___1_dimensions, const RuntimeMethod* method) 
{
	typedef int32_t (DEFAULT_CALL *PInvokeFunc) (int32_t, Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2*);
	#if !FORCE_PINVOKE_INTERNAL && !FORCE_PINVOKE_OculusXRPlugin_INTERNAL
	static PInvokeFunc il2cppPInvokeFunc;
	if (il2cppPInvokeFunc == NULL)
	{
		int parameterSize = sizeof(int32_t) + sizeof(Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2*);
		il2cppPInvokeFunc = il2cpp_codegen_resolve_pinvoke<PInvokeFunc>(IL2CPP_NATIVE_STRING("OculusXRPlugin"), "GetBoundaryDimensions", IL2CPP_CALL_DEFAULT, CHARSET_NOT_SPECIFIED, parameterSize, false);
		IL2CPP_ASSERT(il2cppPInvokeFunc != NULL);
	}
	#endif

	// Native function invocation
	#if FORCE_PINVOKE_INTERNAL || FORCE_PINVOKE_OculusXRPlugin_INTERNAL
	int32_t returnValue = reinterpret_cast<PInvokeFunc>(GetBoundaryDimensions)(___0_boundaryType, ___1_dimensions);
	#else
	int32_t returnValue = il2cppPInvokeFunc(___0_boundaryType, ___1_dimensions);
	#endif

	return static_cast<bool>(returnValue);
}
// System.Boolean Unity.XR.Oculus.NativeMethods/Internal::GetBoundaryVisible()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Internal_GetBoundaryVisible_mCD829BA8761743E005786D1B6BE9345F01A6CF3F (const RuntimeMethod* method) 
{
	typedef int32_t (DEFAULT_CALL *PInvokeFunc) ();
	#if !FORCE_PINVOKE_INTERNAL && !FORCE_PINVOKE_OculusXRPlugin_INTERNAL
	static PInvokeFunc il2cppPInvokeFunc;
	if (il2cppPInvokeFunc == NULL)
	{
		int parameterSize = 0;
		il2cppPInvokeFunc = il2cpp_codegen_resolve_pinvoke<PInvokeFunc>(IL2CPP_NATIVE_STRING("OculusXRPlugin"), "GetBoundaryVisible", IL2CPP_CALL_DEFAULT, CHARSET_NOT_SPECIFIED, parameterSize, false);
		IL2CPP_ASSERT(il2cppPInvokeFunc != NULL);
	}
	#endif

	// Native function invocation
	#if FORCE_PINVOKE_INTERNAL || FORCE_PINVOKE_OculusXRPlugin_INTERNAL
	int32_t returnValue = reinterpret_cast<PInvokeFunc>(GetBoundaryVisible)();
	#else
	int32_t returnValue = il2cppPInvokeFunc();
	#endif

	return static_cast<bool>(returnValue);
}
// System.Void Unity.XR.Oculus.NativeMethods/Internal::SetBoundaryVisible(System.Boolean)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Internal_SetBoundaryVisible_mD04D1930919267DE52554242A2AA7B3D9955E193 (bool ___0_boundaryVisible, const RuntimeMethod* method) 
{
	typedef void (DEFAULT_CALL *PInvokeFunc) (int32_t);
	#if !FORCE_PINVOKE_INTERNAL && !FORCE_PINVOKE_OculusXRPlugin_INTERNAL
	static PInvokeFunc il2cppPInvokeFunc;
	if (il2cppPInvokeFunc == NULL)
	{
		int parameterSize = 4;
		il2cppPInvokeFunc = il2cpp_codegen_resolve_pinvoke<PInvokeFunc>(IL2CPP_NATIVE_STRING("OculusXRPlugin"), "SetBoundaryVisible", IL2CPP_CALL_DEFAULT, CHARSET_NOT_SPECIFIED, parameterSize, false);
		IL2CPP_ASSERT(il2cppPInvokeFunc != NULL);
	}
	#endif

	// Native function invocation
	#if FORCE_PINVOKE_INTERNAL || FORCE_PINVOKE_OculusXRPlugin_INTERNAL
	reinterpret_cast<PInvokeFunc>(SetBoundaryVisible)(static_cast<int32_t>(___0_boundaryVisible));
	#else
	il2cppPInvokeFunc(static_cast<int32_t>(___0_boundaryVisible));
	#endif

}
// System.Boolean Unity.XR.Oculus.NativeMethods/Internal::GetAppShouldQuit()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Internal_GetAppShouldQuit_m969E969C8CB61307597152EAFAA9251E145B48E2 (const RuntimeMethod* method) 
{
	typedef int32_t (DEFAULT_CALL *PInvokeFunc) ();
	#if !FORCE_PINVOKE_INTERNAL && !FORCE_PINVOKE_OculusXRPlugin_INTERNAL
	static PInvokeFunc il2cppPInvokeFunc;
	if (il2cppPInvokeFunc == NULL)
	{
		int parameterSize = 0;
		il2cppPInvokeFunc = il2cpp_codegen_resolve_pinvoke<PInvokeFunc>(IL2CPP_NATIVE_STRING("OculusXRPlugin"), "GetAppShouldQuit", IL2CPP_CALL_DEFAULT, CHARSET_NOT_SPECIFIED, parameterSize, false);
		IL2CPP_ASSERT(il2cppPInvokeFunc != NULL);
	}
	#endif

	// Native function invocation
	#if FORCE_PINVOKE_INTERNAL || FORCE_PINVOKE_OculusXRPlugin_INTERNAL
	int32_t returnValue = reinterpret_cast<PInvokeFunc>(GetAppShouldQuit)();
	#else
	int32_t returnValue = il2cppPInvokeFunc();
	#endif

	return static_cast<bool>(returnValue);
}
// System.Boolean Unity.XR.Oculus.NativeMethods/Internal::GetDisplayAvailableFrequencies(System.IntPtr,System.Int32&)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Internal_GetDisplayAvailableFrequencies_m265CF48B6D6244BD5999ED28F4417922A3964BCC (intptr_t ___0_ptr, int32_t* ___1_numFrequencies, const RuntimeMethod* method) 
{
	typedef int32_t (DEFAULT_CALL *PInvokeFunc) (intptr_t, int32_t*);
	#if !FORCE_PINVOKE_INTERNAL && !FORCE_PINVOKE_OculusXRPlugin_INTERNAL
	static PInvokeFunc il2cppPInvokeFunc;
	if (il2cppPInvokeFunc == NULL)
	{
		int parameterSize = sizeof(intptr_t) + sizeof(int32_t*);
		il2cppPInvokeFunc = il2cpp_codegen_resolve_pinvoke<PInvokeFunc>(IL2CPP_NATIVE_STRING("OculusXRPlugin"), "GetDisplayAvailableFrequencies", IL2CPP_CALL_DEFAULT, CHARSET_NOT_SPECIFIED, parameterSize, false);
		IL2CPP_ASSERT(il2cppPInvokeFunc != NULL);
	}
	#endif

	// Native function invocation
	#if FORCE_PINVOKE_INTERNAL || FORCE_PINVOKE_OculusXRPlugin_INTERNAL
	int32_t returnValue = reinterpret_cast<PInvokeFunc>(GetDisplayAvailableFrequencies)(___0_ptr, ___1_numFrequencies);
	#else
	int32_t returnValue = il2cppPInvokeFunc(___0_ptr, ___1_numFrequencies);
	#endif

	return static_cast<bool>(returnValue);
}
// System.Boolean Unity.XR.Oculus.NativeMethods/Internal::SetDisplayFrequency(System.Single)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Internal_SetDisplayFrequency_m6E8BE138EDF3AD30421E8A299597827A81F86804 (float ___0_refreshRate, const RuntimeMethod* method) 
{
	typedef int32_t (DEFAULT_CALL *PInvokeFunc) (float);
	#if !FORCE_PINVOKE_INTERNAL && !FORCE_PINVOKE_OculusXRPlugin_INTERNAL
	static PInvokeFunc il2cppPInvokeFunc;
	if (il2cppPInvokeFunc == NULL)
	{
		int parameterSize = sizeof(float);
		il2cppPInvokeFunc = il2cpp_codegen_resolve_pinvoke<PInvokeFunc>(IL2CPP_NATIVE_STRING("OculusXRPlugin"), "SetDisplayFrequency", IL2CPP_CALL_DEFAULT, CHARSET_NOT_SPECIFIED, parameterSize, false);
		IL2CPP_ASSERT(il2cppPInvokeFunc != NULL);
	}
	#endif

	// Native function invocation
	#if FORCE_PINVOKE_INTERNAL || FORCE_PINVOKE_OculusXRPlugin_INTERNAL
	int32_t returnValue = reinterpret_cast<PInvokeFunc>(SetDisplayFrequency)(___0_refreshRate);
	#else
	int32_t returnValue = il2cppPInvokeFunc(___0_refreshRate);
	#endif

	return static_cast<bool>(returnValue);
}
// System.Boolean Unity.XR.Oculus.NativeMethods/Internal::GetDisplayFrequency(System.Single&)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Internal_GetDisplayFrequency_mF645008727871526DAE4877348FD48E0AEFE6316 (float* ___0_refreshRate, const RuntimeMethod* method) 
{
	typedef int32_t (DEFAULT_CALL *PInvokeFunc) (float*);
	#if !FORCE_PINVOKE_INTERNAL && !FORCE_PINVOKE_OculusXRPlugin_INTERNAL
	static PInvokeFunc il2cppPInvokeFunc;
	if (il2cppPInvokeFunc == NULL)
	{
		int parameterSize = sizeof(float*);
		il2cppPInvokeFunc = il2cpp_codegen_resolve_pinvoke<PInvokeFunc>(IL2CPP_NATIVE_STRING("OculusXRPlugin"), "GetDisplayFrequency", IL2CPP_CALL_DEFAULT, CHARSET_NOT_SPECIFIED, parameterSize, false);
		IL2CPP_ASSERT(il2cppPInvokeFunc != NULL);
	}
	#endif

	// Native function invocation
	#if FORCE_PINVOKE_INTERNAL || FORCE_PINVOKE_OculusXRPlugin_INTERNAL
	int32_t returnValue = reinterpret_cast<PInvokeFunc>(GetDisplayFrequency)(___0_refreshRate);
	#else
	int32_t returnValue = il2cppPInvokeFunc(___0_refreshRate);
	#endif

	return static_cast<bool>(returnValue);
}
// Unity.XR.Oculus.SystemHeadset Unity.XR.Oculus.NativeMethods/Internal::GetSystemHeadsetType()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t Internal_GetSystemHeadsetType_m0901C44C3A772A62308D273AA37CF55BA6CEE646 (const RuntimeMethod* method) 
{
	typedef int32_t (DEFAULT_CALL *PInvokeFunc) ();
	#if !FORCE_PINVOKE_INTERNAL && !FORCE_PINVOKE_OculusXRPlugin_INTERNAL
	static PInvokeFunc il2cppPInvokeFunc;
	if (il2cppPInvokeFunc == NULL)
	{
		int parameterSize = 0;
		il2cppPInvokeFunc = il2cpp_codegen_resolve_pinvoke<PInvokeFunc>(IL2CPP_NATIVE_STRING("OculusXRPlugin"), "GetSystemHeadsetType", IL2CPP_CALL_DEFAULT, CHARSET_NOT_SPECIFIED, parameterSize, false);
		IL2CPP_ASSERT(il2cppPInvokeFunc != NULL);
	}
	#endif

	// Native function invocation
	#if FORCE_PINVOKE_INTERNAL || FORCE_PINVOKE_OculusXRPlugin_INTERNAL
	int32_t returnValue = reinterpret_cast<PInvokeFunc>(GetSystemHeadsetType)();
	#else
	int32_t returnValue = il2cppPInvokeFunc();
	#endif

	return returnValue;
}
// System.Boolean Unity.XR.Oculus.NativeMethods/Internal::GetTiledMultiResSupported()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Internal_GetTiledMultiResSupported_m7A36253D53F4DA8D56E1EFA242457D3771220FBF (const RuntimeMethod* method) 
{
	typedef int32_t (DEFAULT_CALL *PInvokeFunc) ();
	#if !FORCE_PINVOKE_INTERNAL && !FORCE_PINVOKE_OculusXRPlugin_INTERNAL
	static PInvokeFunc il2cppPInvokeFunc;
	if (il2cppPInvokeFunc == NULL)
	{
		int parameterSize = 0;
		il2cppPInvokeFunc = il2cpp_codegen_resolve_pinvoke<PInvokeFunc>(IL2CPP_NATIVE_STRING("OculusXRPlugin"), "GetTiledMultiResSupported", IL2CPP_CALL_DEFAULT, CHARSET_NOT_SPECIFIED, parameterSize, false);
		IL2CPP_ASSERT(il2cppPInvokeFunc != NULL);
	}
	#endif

	// Native function invocation
	#if FORCE_PINVOKE_INTERNAL || FORCE_PINVOKE_OculusXRPlugin_INTERNAL
	int32_t returnValue = reinterpret_cast<PInvokeFunc>(GetTiledMultiResSupported)();
	#else
	int32_t returnValue = il2cppPInvokeFunc();
	#endif

	return static_cast<bool>(returnValue);
}
// System.Void Unity.XR.Oculus.NativeMethods/Internal::SetTiledMultiResLevel(System.Int32)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Internal_SetTiledMultiResLevel_m59849D5EF74DE2C7F25813486E3ED47C947F15C3 (int32_t ___0_level, const RuntimeMethod* method) 
{
	typedef void (DEFAULT_CALL *PInvokeFunc) (int32_t);
	#if !FORCE_PINVOKE_INTERNAL && !FORCE_PINVOKE_OculusXRPlugin_INTERNAL
	static PInvokeFunc il2cppPInvokeFunc;
	if (il2cppPInvokeFunc == NULL)
	{
		int parameterSize = sizeof(int32_t);
		il2cppPInvokeFunc = il2cpp_codegen_resolve_pinvoke<PInvokeFunc>(IL2CPP_NATIVE_STRING("OculusXRPlugin"), "SetTiledMultiResLevel", IL2CPP_CALL_DEFAULT, CHARSET_NOT_SPECIFIED, parameterSize, false);
		IL2CPP_ASSERT(il2cppPInvokeFunc != NULL);
	}
	#endif

	// Native function invocation
	#if FORCE_PINVOKE_INTERNAL || FORCE_PINVOKE_OculusXRPlugin_INTERNAL
	reinterpret_cast<PInvokeFunc>(SetTiledMultiResLevel)(___0_level);
	#else
	il2cppPInvokeFunc(___0_level);
	#endif

}
// System.Int32 Unity.XR.Oculus.NativeMethods/Internal::GetTiledMultiResLevel()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t Internal_GetTiledMultiResLevel_mC95B03E83E572C960C79746554C070E91A43F8BC (const RuntimeMethod* method) 
{
	typedef int32_t (DEFAULT_CALL *PInvokeFunc) ();
	#if !FORCE_PINVOKE_INTERNAL && !FORCE_PINVOKE_OculusXRPlugin_INTERNAL
	static PInvokeFunc il2cppPInvokeFunc;
	if (il2cppPInvokeFunc == NULL)
	{
		int parameterSize = 0;
		il2cppPInvokeFunc = il2cpp_codegen_resolve_pinvoke<PInvokeFunc>(IL2CPP_NATIVE_STRING("OculusXRPlugin"), "GetTiledMultiResLevel", IL2CPP_CALL_DEFAULT, CHARSET_NOT_SPECIFIED, parameterSize, false);
		IL2CPP_ASSERT(il2cppPInvokeFunc != NULL);
	}
	#endif

	// Native function invocation
	#if FORCE_PINVOKE_INTERNAL || FORCE_PINVOKE_OculusXRPlugin_INTERNAL
	int32_t returnValue = reinterpret_cast<PInvokeFunc>(GetTiledMultiResLevel)();
	#else
	int32_t returnValue = il2cppPInvokeFunc();
	#endif

	return returnValue;
}
// System.Void Unity.XR.Oculus.NativeMethods/Internal::SetTiledMultiResDynamic(System.Boolean)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Internal_SetTiledMultiResDynamic_m92E2C4C0C862B7A30582EE5B96006FFE3367D658 (bool ___0_isDynamic, const RuntimeMethod* method) 
{
	typedef void (DEFAULT_CALL *PInvokeFunc) (int32_t);
	#if !FORCE_PINVOKE_INTERNAL && !FORCE_PINVOKE_OculusXRPlugin_INTERNAL
	static PInvokeFunc il2cppPInvokeFunc;
	if (il2cppPInvokeFunc == NULL)
	{
		int parameterSize = 4;
		il2cppPInvokeFunc = il2cpp_codegen_resolve_pinvoke<PInvokeFunc>(IL2CPP_NATIVE_STRING("OculusXRPlugin"), "SetTiledMultiResDynamic", IL2CPP_CALL_DEFAULT, CHARSET_NOT_SPECIFIED, parameterSize, false);
		IL2CPP_ASSERT(il2cppPInvokeFunc != NULL);
	}
	#endif

	// Native function invocation
	#if FORCE_PINVOKE_INTERNAL || FORCE_PINVOKE_OculusXRPlugin_INTERNAL
	reinterpret_cast<PInvokeFunc>(SetTiledMultiResDynamic)(static_cast<int32_t>(___0_isDynamic));
	#else
	il2cppPInvokeFunc(static_cast<int32_t>(___0_isDynamic));
	#endif

}
// System.Boolean Unity.XR.Oculus.NativeMethods/Internal::GetEyeTrackedFoveatedRenderingSupported()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Internal_GetEyeTrackedFoveatedRenderingSupported_mA2F9106F5BD6571BB7D19D2FFDF61EF36D33F0E5 (const RuntimeMethod* method) 
{
	typedef int32_t (DEFAULT_CALL *PInvokeFunc) ();
	#if !FORCE_PINVOKE_INTERNAL && !FORCE_PINVOKE_OculusXRPlugin_INTERNAL
	static PInvokeFunc il2cppPInvokeFunc;
	if (il2cppPInvokeFunc == NULL)
	{
		int parameterSize = 0;
		il2cppPInvokeFunc = il2cpp_codegen_resolve_pinvoke<PInvokeFunc>(IL2CPP_NATIVE_STRING("OculusXRPlugin"), "GetEyeTrackedFoveatedRenderingSupported", IL2CPP_CALL_DEFAULT, CHARSET_NOT_SPECIFIED, parameterSize, false);
		IL2CPP_ASSERT(il2cppPInvokeFunc != NULL);
	}
	#endif

	// Native function invocation
	#if FORCE_PINVOKE_INTERNAL || FORCE_PINVOKE_OculusXRPlugin_INTERNAL
	int32_t returnValue = reinterpret_cast<PInvokeFunc>(GetEyeTrackedFoveatedRenderingSupported)();
	#else
	int32_t returnValue = il2cppPInvokeFunc();
	#endif

	return static_cast<bool>(returnValue);
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// System.UInt16 Unity.XR.Oculus.OculusSettings::GetStereoRenderingMode()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR uint16_t OculusSettings_GetStereoRenderingMode_mE9C6ABC56B9EDEB3BE5599B091BB6699429BB6BD (OculusSettings_t0584FB71432B697479FD0BFC5B68C195F17CD321* __this, const RuntimeMethod* method) 
{
	{
		// return (ushort)m_StereoRenderingModeAndroid;
		int32_t L_0 = __this->___m_StereoRenderingModeAndroid_5;
		return (uint16_t)((int32_t)(uint16_t)L_0);
	}
}
// System.Void Unity.XR.Oculus.OculusSettings::Awake()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void OculusSettings_Awake_m6206BE150DEB602C65EEF7C255DE3BA17FB1452E (OculusSettings_t0584FB71432B697479FD0BFC5B68C195F17CD321* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&OculusSettings_t0584FB71432B697479FD0BFC5B68C195F17CD321_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// s_Settings = this;
		((OculusSettings_t0584FB71432B697479FD0BFC5B68C195F17CD321_StaticFields*)il2cpp_codegen_static_fields_for(OculusSettings_t0584FB71432B697479FD0BFC5B68C195F17CD321_il2cpp_TypeInfo_var))->___s_Settings_23 = __this;
		Il2CppCodeGenWriteBarrier((void**)(&((OculusSettings_t0584FB71432B697479FD0BFC5B68C195F17CD321_StaticFields*)il2cpp_codegen_static_fields_for(OculusSettings_t0584FB71432B697479FD0BFC5B68C195F17CD321_il2cpp_TypeInfo_var))->___s_Settings_23), (void*)__this);
		// }
		return;
	}
}
// System.Void Unity.XR.Oculus.OculusSettings::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void OculusSettings__ctor_m2A441A38D66B2B4538A2CE744BCFD3283910F324 (OculusSettings_t0584FB71432B697479FD0BFC5B68C195F17CD321* __this, const RuntimeMethod* method) 
{
	{
		// public StereoRenderingModeDesktop m_StereoRenderingModeDesktop = StereoRenderingModeDesktop.SinglePassInstanced;
		__this->___m_StereoRenderingModeDesktop_4 = 1;
		// public StereoRenderingModeAndroid m_StereoRenderingModeAndroid = StereoRenderingModeAndroid.Multiview;
		__this->___m_StereoRenderingModeAndroid_5 = 2;
		// public bool SharedDepthBuffer = true;
		__this->___SharedDepthBuffer_6 = (bool)1;
		// public bool DashSupport = true;
		__this->___DashSupport_8 = (bool)1;
		// public bool OptimizeBufferDiscards = true;
		__this->___OptimizeBufferDiscards_10 = (bool)1;
		// public bool SymmetricProjection = true;
		__this->___SymmetricProjection_12 = (bool)1;
		// public bool TargetQuest = true;
		__this->___TargetQuest_19 = (bool)1;
		// public bool TargetQuest2 = true;
		__this->___TargetQuest2_20 = (bool)1;
		ScriptableObject__ctor_mD037FDB0B487295EA47F79A4DB1BF1846C9087FF(__this, NULL);
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
// System.Void Unity.XR.Oculus.OculusUsages::.cctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void OculusUsages__cctor_m7024A4EF41067C41E2C7F24147CBBB7684B11E6C (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&InputFeatureUsage_1__ctor_mEB36F8937385A1065CD9F48AE2DAD9EAE49EFCE7_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&OculusUsages_t26394A1703082235CAC869B16DF3A491A7965196_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral549D4E1BD7FFA7F485E084D961369B26386BA2A5);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralCD42F30283C4CE60465C4010C800AD9704733102);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralDCD1BF12664AC38299958513D10BAA016D22904B);
		s_Il2CppMethodInitialized = true;
	}
	{
		// public static InputFeatureUsage<bool> thumbrest = new InputFeatureUsage<bool>("Thumbrest");
		InputFeatureUsage_1_tE336B2F0B9AC721519BFA17A08D6353FD5221637 L_0;
		memset((&L_0), 0, sizeof(L_0));
		InputFeatureUsage_1__ctor_mEB36F8937385A1065CD9F48AE2DAD9EAE49EFCE7((&L_0), _stringLiteralCD42F30283C4CE60465C4010C800AD9704733102, /*hidden argument*/InputFeatureUsage_1__ctor_mEB36F8937385A1065CD9F48AE2DAD9EAE49EFCE7_RuntimeMethod_var);
		((OculusUsages_t26394A1703082235CAC869B16DF3A491A7965196_StaticFields*)il2cpp_codegen_static_fields_for(OculusUsages_t26394A1703082235CAC869B16DF3A491A7965196_il2cpp_TypeInfo_var))->___thumbrest_0 = L_0;
		Il2CppCodeGenWriteBarrier((void**)&(((&((OculusUsages_t26394A1703082235CAC869B16DF3A491A7965196_StaticFields*)il2cpp_codegen_static_fields_for(OculusUsages_t26394A1703082235CAC869B16DF3A491A7965196_il2cpp_TypeInfo_var))->___thumbrest_0))->___U3CnameU3Ek__BackingField_0), (void*)NULL);
		// public static InputFeatureUsage<bool> indexTouch = new InputFeatureUsage<bool>("IndexTouch");
		InputFeatureUsage_1_tE336B2F0B9AC721519BFA17A08D6353FD5221637 L_1;
		memset((&L_1), 0, sizeof(L_1));
		InputFeatureUsage_1__ctor_mEB36F8937385A1065CD9F48AE2DAD9EAE49EFCE7((&L_1), _stringLiteral549D4E1BD7FFA7F485E084D961369B26386BA2A5, /*hidden argument*/InputFeatureUsage_1__ctor_mEB36F8937385A1065CD9F48AE2DAD9EAE49EFCE7_RuntimeMethod_var);
		((OculusUsages_t26394A1703082235CAC869B16DF3A491A7965196_StaticFields*)il2cpp_codegen_static_fields_for(OculusUsages_t26394A1703082235CAC869B16DF3A491A7965196_il2cpp_TypeInfo_var))->___indexTouch_1 = L_1;
		Il2CppCodeGenWriteBarrier((void**)&(((&((OculusUsages_t26394A1703082235CAC869B16DF3A491A7965196_StaticFields*)il2cpp_codegen_static_fields_for(OculusUsages_t26394A1703082235CAC869B16DF3A491A7965196_il2cpp_TypeInfo_var))->___indexTouch_1))->___U3CnameU3Ek__BackingField_0), (void*)NULL);
		// public static InputFeatureUsage<bool> thumbTouch = new InputFeatureUsage<bool>("ThumbTouch");
		InputFeatureUsage_1_tE336B2F0B9AC721519BFA17A08D6353FD5221637 L_2;
		memset((&L_2), 0, sizeof(L_2));
		InputFeatureUsage_1__ctor_mEB36F8937385A1065CD9F48AE2DAD9EAE49EFCE7((&L_2), _stringLiteralDCD1BF12664AC38299958513D10BAA016D22904B, /*hidden argument*/InputFeatureUsage_1__ctor_mEB36F8937385A1065CD9F48AE2DAD9EAE49EFCE7_RuntimeMethod_var);
		((OculusUsages_t26394A1703082235CAC869B16DF3A491A7965196_StaticFields*)il2cpp_codegen_static_fields_for(OculusUsages_t26394A1703082235CAC869B16DF3A491A7965196_il2cpp_TypeInfo_var))->___thumbTouch_2 = L_2;
		Il2CppCodeGenWriteBarrier((void**)&(((&((OculusUsages_t26394A1703082235CAC869B16DF3A491A7965196_StaticFields*)il2cpp_codegen_static_fields_for(OculusUsages_t26394A1703082235CAC869B16DF3A491A7965196_il2cpp_TypeInfo_var))->___thumbTouch_2))->___U3CnameU3Ek__BackingField_0), (void*)NULL);
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
// System.Void Unity.XR.Oculus.RegisterUpdateCallback::Initialize()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void RegisterUpdateCallback_Initialize_m0256D76B69C444418E9E394DD280C4D54F122BC5 (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&RegisterUpdateCallback_Update_m782D01D846D30720A9212BC883A3602D5B5B5958_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&UnityAction_t11A1F3B953B365C072A5DCC32677EE1796A962A7_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// Application.onBeforeRender += Update;
		UnityAction_t11A1F3B953B365C072A5DCC32677EE1796A962A7* L_0 = (UnityAction_t11A1F3B953B365C072A5DCC32677EE1796A962A7*)il2cpp_codegen_object_new(UnityAction_t11A1F3B953B365C072A5DCC32677EE1796A962A7_il2cpp_TypeInfo_var);
		NullCheck(L_0);
		UnityAction__ctor_mC53E20D6B66E0D5688CD81B88DBB34F5A58B7131(L_0, NULL, (intptr_t)((void*)RegisterUpdateCallback_Update_m782D01D846D30720A9212BC883A3602D5B5B5958_RuntimeMethod_var), NULL);
		Application_add_onBeforeRender_mEE8925294C807AD08FA0FF35D4C663E098510394(L_0, NULL);
		// }
		return;
	}
}
// System.Void Unity.XR.Oculus.RegisterUpdateCallback::Deinitialize()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void RegisterUpdateCallback_Deinitialize_m3826762052F507B9B18E62483D8341495B5EEA13 (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&RegisterUpdateCallback_Update_m782D01D846D30720A9212BC883A3602D5B5B5958_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&UnityAction_t11A1F3B953B365C072A5DCC32677EE1796A962A7_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// Application.onBeforeRender -= Update;
		UnityAction_t11A1F3B953B365C072A5DCC32677EE1796A962A7* L_0 = (UnityAction_t11A1F3B953B365C072A5DCC32677EE1796A962A7*)il2cpp_codegen_object_new(UnityAction_t11A1F3B953B365C072A5DCC32677EE1796A962A7_il2cpp_TypeInfo_var);
		NullCheck(L_0);
		UnityAction__ctor_mC53E20D6B66E0D5688CD81B88DBB34F5A58B7131(L_0, NULL, (intptr_t)((void*)RegisterUpdateCallback_Update_m782D01D846D30720A9212BC883A3602D5B5B5958_RuntimeMethod_var), NULL);
		Application_remove_onBeforeRender_m9F54448ED4059A26C9972E5C9ED2F6DCD58B4E24(L_0, NULL);
		// }
		return;
	}
}
// System.Void Unity.XR.Oculus.RegisterUpdateCallback::Update()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void RegisterUpdateCallback_Update_m782D01D846D30720A9212BC883A3602D5B5B5958 (const RuntimeMethod* method) 
{
	{
		// InputFocus.Update();
		InputFocus_Update_mAA7C0E54D60D5D05EF693009DA4AFE7C63BE044A(NULL);
		// }
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void Action_Invoke_m7126A54DACA72B845424072887B5F3A51FC3808E_inline (Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* __this, const RuntimeMethod* method) 
{
	typedef void (*FunctionPointerType) (RuntimeObject*, const RuntimeMethod*);
	((FunctionPointerType)__this->___invoke_impl_1)((Il2CppObject*)__this->___method_code_6, reinterpret_cast<RuntimeMethod*>(__this->___method_3));
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR OculusSettings_t0584FB71432B697479FD0BFC5B68C195F17CD321* OculusLoader_GetSettings_m2FA61767F36A8CFB03124EB295A79C7A7F0A863B_inline (OculusLoader_tA386B9AA0786D042EA272EDF385F96C0AD1A56BB* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&OculusSettings_t0584FB71432B697479FD0BFC5B68C195F17CD321_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// settings = OculusSettings.s_Settings;
		OculusSettings_t0584FB71432B697479FD0BFC5B68C195F17CD321* L_0 = ((OculusSettings_t0584FB71432B697479FD0BFC5B68C195F17CD321_StaticFields*)il2cpp_codegen_static_fields_for(OculusSettings_t0584FB71432B697479FD0BFC5B68C195F17CD321_il2cpp_TypeInfo_var))->___s_Settings_23;
		// return settings;
		return L_0;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR RuntimeObject* Enumerator_get_Current_m6330F15D18EE4F547C05DF9BF83C5EB710376027_gshared_inline (Enumerator_t9473BAB568A27E2339D48C1F91319E0F6D244D7A* __this, const RuntimeMethod* method) 
{
	{
		RuntimeObject* L_0 = (RuntimeObject*)__this->____current_3;
		return L_0;
	}
}
