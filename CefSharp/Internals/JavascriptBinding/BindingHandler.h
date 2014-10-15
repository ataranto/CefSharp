#include "Stdafx.h"
#pragma once

using namespace System::Collections::Generic;
using namespace System::Reflection;

namespace CefSharp
{
    namespace Internals
    {
        namespace JavascriptBinding
        {
            private class BindingHandler : public CefV8Handler, public AppDomainSafeCefBase
            {
            public:
                static void Bind(String^ name, Object^ obj, CefRefPtr<CefV8Value> window);          
                IMPLEMENT_SAFE_REFCOUNTING(BindingHandler);

            private:
				private:
				static bool _Execute(const CefString* const _name, CefRefPtr<CefV8Value> object, const CefV8ValueList* const _arguments, CefRefPtr<CefV8Value>* const _retval, CefString* const _exception);
                virtual bool Execute(const CefString& name, CefRefPtr<CefV8Value> object, const CefV8ValueList& arguments, CefRefPtr<CefV8Value>& retval, CefString& exception);

                static HashSet<String^>^ GetMethodNames(Type^ type);

                static bool IsNullableType(Type^ type);
                static int GetChangeTypeCost(Object^ value, Type^ conversionType);
                static Object^ ChangeType(Object^ value, Type^ conversionType);

                static void CreateJavascriptMethods(CefV8Handler* handler, CefRefPtr<CefV8Value> javascriptObject, IEnumerable<String^>^ methodNames);
                static void CreateJavascriptProperties(CefV8Handler* handler, CefRefPtr<CefV8Value> javascriptObject, Dictionary<String^, PropertyInfo^>^ properties);
                static Dictionary<String^, PropertyInfo^>^ GetProperties(Type^ type);
                static void BindingHandler::FindBestMethod(array<MemberInfo^>^ methods, array<Object^>^ suppliedArguments, MethodInfo^% bestMethod, array<Object^>^% bestMethodArguments);

                CefRefPtr<CefV8Value> ConvertToCef(Object^ obj, Type^ type);
                Object^ ConvertFromCef(CefRefPtr<CefV8Value> obj);
                static String^ BindingHandler::LowercaseFirst(String^ str);          
            };
        }
    }
}