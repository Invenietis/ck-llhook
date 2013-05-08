CK-LLHook
=========

CK-LLHook implements Global windows Hooks on .Net thanks to two native 32 &amp; 64 bits dlls (8 &amp; 10 KB, 
totally autonomous - there is no C Runtime in them) and a 32/64 exe bridge (5 &amp; 9KB).
Goals was: light, fast (minimize impacts on application). Small is always beautiful. 

This project is part of http://www.civikey.fr/.

CK.MiniCRT
=========

This ridiculous native size (and great benefits of zero-dependencyies requirements) is achieved thanks to the 
CK.MiniCRT sub project (included in this repository but with its own solution file).
This Mini CRT is based upon Libctiny written by Matt Pietrick in 1996!

I'm not the only one that is not a big fan of the standard C runtime and all the code bloat it creates. 
(Honnestly, in 2013, should we continue to use strtok and its required per-thread initialization?)
After some hours lost fighting against linking errors related to standard
C function (atol), I decided to rewrite just what I need in C with C++ namespaces. And with my names.
I will not do it again. But it's done and covers just what is needed.
An interesting story of Tiny, Mini, etc. CRT can be found here: http://www.benshoof.org/blog/minicrt/.

Last words: 
- I did not practice C/C++ for years. Last compiler I used was named Visual C++. There is, for sure, horrible
things in the C projects. I'd be glad to learn!
- do not use it, unless you perfectly know what you are doing. Some really dirty things may happen to you.

Other dependencies
==================

CK.LLHook C# assembly uses CK.Core and CK.Interop and CK.Reflection.
These 3 packages (LGPL licence) can be obtained from the public nuget feed https://get-package.com/org/CiviKey/
(nuget feed: https://get-package.net/feed/CiviKey/feed-CiviKey).


Hooks, Processesn Dlls
=====================

Currently 3 hooks are implemented (WH_SHELL, WH_KEYBOARD, WH_MOUSE) but it will be quite easy to support all of them.
Implementations will be restricted by an important issue: the native hooks only communicate to the C# client 
via PostMessage, without any provision for marshalling extra data (the information must fit in WParam &amp; LParam).
The fact that the hooks use PostMessage is good (there is no useless intermediate threads betwwen the hook 
and the WPF/WindowsForms Gui thread). What is bad:  a mapped file that stores extra-data information is
missing. It should be not too complicated to implement (and very efficient since I think a nearly lock-free 
mechanism can be achieved - it is a kind of FIFO).

Another important point: a shared section is used to store the hook identifiers, the target window and optional 
per-hook options (options are not currently implemented).

C# side
=======

Not a lot to say. A NativeHookManager can be instanciated that exposes the hooks. Each of them can be started 
or stopped and expose specialized .Net events.

Tests, Tools & Demo
===================
It's not that easy to unit test windows global hooks :-(.
Three identical .Net applications exist (ViewHookApp32, ViewHookApp64 and ViewHookAppAnyCPU) that can start/stop the 
hooks and displays their events.


