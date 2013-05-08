#pragma once

void WINAPI CriticalLogError( LPCWSTR errorMessage );

void WINAPI CriticalLogError( LPCWSTR prefix, LPCWSTR errorMessage );

void WINAPI CriticalLogErrorWithLastError( LPCWSTR errorMessage );

void WINAPI CriticalLogErrorWithLastError( LPCWSTR prefix, LPCWSTR errorMessage );
