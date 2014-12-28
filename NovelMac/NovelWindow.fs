namespace NovelMac

open MonoMac.ObjCRuntime
open MonoMac.Foundation
open MonoMac.AppKit

type NovelWindow = 
    inherit NSWindow

    new () = { inherit NSWindow() }

    new (contentRect, style, bufferingType, deferCreation) = 
        { inherit NSWindow (contentRect, style, bufferingType, deferCreation) }
