namespace NovelMac

open MonoMac.ObjCRuntime
open MonoMac.Foundation
open MonoMac.AppKit

type NovelDocumentController () =
    inherit NSDocumentController ()

    static member SharedDocumentController
        with get () = NSDocumentController.SharedDocumentController :?> NovelDocumentController

    override this.NewDocument (sender : NSObject) =
        let document = new NovelDocument ()
        document.ShowWindows ()
