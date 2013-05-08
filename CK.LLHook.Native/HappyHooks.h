#include <Windows.h>

/*
	Base structure of all hook options.
*/
typedef struct CommonHookOption
{
	BOOL RawIntercept;

} CommonHookOption;

extern void WINAPI GetBasicLogFileName( WCHAR* nameMaxPath );

extern void WINAPI DeactivateAllHooks();

extern HWND WINAPI GetKeyboardHookTarget();

extern BOOL WINAPI ActivateKeyboardHook( HWND targetWnd );

extern HWND WINAPI GetMouseHookTarget();

extern BOOL WINAPI ActivateMouseHook( HWND targetWnd );

extern HWND WINAPI GetShellHookTarget();

extern BOOL WINAPI ActivateShellHook( HWND targetWnd );

typedef struct ShellHookOption : public CommonHookOption
{
} ShellHookOption;

extern BOOL WINAPI SetShellHookOptions( ShellHookOption options );

