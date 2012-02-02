#include "stdafx.h"
#pragma once

#include "ICefWebBrowser.h"
#include "Request.h"

using namespace System;
using namespace System::IO;

namespace CefSharp 
{
    public enum class NavigationType
    {
        LinkClicked = 0,
        FormSubmitted,
        BackForward,
        Reload,
        FormResubmitted,
        Other
    };

    public interface class IBeforeBrowse
    {
    public:
        
        /// called before page navigation.
        /// allows the Request to be cancelled
        bool HandleBeforeBrowse(ICefWebBrowser^ browserControl, /*ICefFrame^ frame,*/ IRequest^ request, NavigationType navType, bool isRedirect);
    };
}