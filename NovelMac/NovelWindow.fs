namespace NovelMac

open System.Drawing

open MonoMac.ObjCRuntime
open MonoMac.Foundation
open MonoMac.AppKit

type NovelWindow = 
    inherit NSWindow

    new () = { inherit NSWindow() }

    new (contentRect, style, bufferingType, deferCreation) = 
        { inherit NSWindow (contentRect, style, bufferingType, deferCreation) }

type NovelWindowController() as this = 
    inherit NSWindowController()

    do
        let rect = new RectangleF (0.0f, 0.0f, 640.0f, 480.0f)
        this.Window <- new NovelWindow (
            rect,
            NSWindowStyle.Titled ||| NSWindowStyle.Resizable ||| NSWindowStyle.Closable ||| NSWindowStyle.Miniaturizable,
            NSBackingStore.Buffered,
            false)
        this.Window.Center ()
