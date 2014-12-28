namespace NovelMac

open System.Drawing

open MonoMac.ObjCRuntime
open MonoMac.Foundation
open MonoMac.AppKit

type NovelWindowController() as this = 
    inherit NSWindowController()

    do
        let rect = new RectangleF (200.f, 20.0f, 640.0f, 480.0f)
        this.Window <- new NovelWindow (
            rect,
            NSWindowStyle.Titled ||| NSWindowStyle.Resizable ||| NSWindowStyle.Closable ||| NSWindowStyle.Miniaturizable,
            NSBackingStore.Buffered,
            false)
