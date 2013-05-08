
struct HookConfig
{
	volatile LONG VersionTag;
	volatile HHOOK HookId;
	volatile HWND Target;
};
typedef struct HookConfig HookConfig;

/*Number of hook types implemented.*/
#define CKHookCount			3

#define CKMouseHookIdx	0
#define CKKeyboardHookIdx 1
#define CKShellHookIdx		2

/*
Updates a process config from the shared one.
Returns FALSE if synchronization went wrong: the configuration should not be used and the 
hook shoould be considered deactivated.
*/
extern BOOL WINAPI GetHookConfig( int ckHookIdx, HookConfig& config );

/*
Updates the shared configuration for a hook.
If the targetWnd is null, the hook is disabled.
If the targetWnd is already associated to another hook, the previous hook is disabled.
*/
extern BOOL WINAPI ActivateSharedHook( int ckHookIdx, HWND targetWnd, HOOKPROC hookProc );
