namespace NovelMac

open MonoMac.ObjCRuntime
open MonoMac.Foundation
open MonoMac.AppKit

open Novel

type NovelDocument () as this = 
    inherit NSDocument ()

    let mutable novel = {
        Novel.Title = "The Title";
        Authors = [ "The Author" ];
    }

    let windowController = new NovelWindowController ()
    let window = windowController.Window

    do
        this.AddWindowController (windowController)
        window.Title <- novel.Title
        window.MakeKeyAndOrderFront (windowController)

    override this.GetAsData (documentType, outError) =
        outError <- NSError.FromDomain (NSError.OsStatusErrorDomain, -4)
        null

    override this.ReadFromData (data, typeName, outError) =
        outError <- NSError.FromDomain (NSError.OsStatusErrorDomain, -4)
        false
