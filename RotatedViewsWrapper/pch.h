#pragma once

#ifndef VC_EXTRALEAN
#define VC_EXTRALEAN            // Exclude rarely-used stuff from Windows headers
#endif

#include "targetver.h"
#include "3DVarsDefines_CH.h" // for C_H_VERSION

#if C_H_VERSION >= 2100 // Mastercam 2019 or later
#ifndef _UNICODE
#define _UNICODE
#endif
//#pragma message("Building for Mastercam with _UNICODE Character Set")
#else // Mastercam prior to 2019
#ifndef _MBCS
#define _MBCS
#endif
//#pragma message("Building for Mastercam _MBCS Character Set")
#endif

#define _ATL_CSTRING_EXPLICIT_CONSTRUCTORS  // some CString constructors will be explicit

#include <afxwin.h>             // MFC core and standard components
#include <afxext.h>             // MFC extensions

#ifndef _AFX_NO_OLE_SUPPORT
#include <afxole.h>             // MFC OLE classes
#include <afxodlgs.h>           // MFC OLE dialog classes
#include <afxdisp.h>            // MFC Automation classes
#endif // _AFX_NO_OLE_SUPPORT

#ifndef _AFX_NO_DB_SUPPORT
#include <afxdb.h>              // MFC ODBC database classes
#endif // _AFX_NO_DB_SUPPORT

#ifndef _AFX_NO_DAO_SUPPORT
#include <afxdao.h>             // MFC DAO database classes
#endif // _AFX_NO_DAO_SUPPORT

#ifndef _AFX_NO_OLE_SUPPORT
#include <afxdtctl.h>           // MFC support for Internet Explorer 4 Common Controls
#endif
#ifndef _AFX_NO_AFXCMN_SUPPORT
#include <afxcmn.h>             // MFC support for Windows Common Controls
#endif // _AFX_NO_AFXCMN_SUPPORT

// Mastercam specific items:
// These standard includes are required here starting with Mastercam 2018
#include <array>
#include <deque>
#include <functional>
#include <map>
#include <memory>
#include <set>
#include <unordered_map>
#include <unordered_set>
#include <vector>
// This will cause the proper BCGCBPRO#######.lib to be linked in.
#include "BCGCBProInc.h"
// Include these "main" Mastercam header files.
#include "m_core.h"
#include "m_mastercam.h"
#if C_H_VERSION >= 2200 // Mastercam 2020 or later
#include "m_corebase.h"
#include "m_guibase.h"
#endif

// Optional 'master' headers that include groups of related headers ->
//#include "m_msurf.h"
//#include "m_post.h"
//#include "m_solids.h"
//#include "m_tool.h"
//#include "m_wire.h"
//#include "m_bnci.h"
//#include "m_cad.h"
//#include "m_cadutil.h"
//#include "m_control.h"
//#include "m_curvesurfgeom.h"
//#include "m_guibase.h"
//#include "m_lathe.h"
//#include "m_machinedef.h"
//#include "m_math.h"
//#include "m_mill.h"



