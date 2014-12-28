namespace NovelMac

open System

open MonoMac.ObjCRuntime
open MonoMac.Foundation
open MonoMac.AppKit

type NovelAppDelegate () =
    inherit NSApplicationDelegate ()

    let documentController = new NovelDocumentController ()

    let createMenu appName =
        let menu = new NSMenu ()

        let appMenuItem = new NSMenuItem ()
        menu.AddItem (appMenuItem)
        let appMenu = new NSMenu ();
        let quitMenuItem = new NSMenuItem (sprintf "Quit %s" appName, "q", (fun s e ->
            NSApplication.SharedApplication.Terminate menu))
        appMenu.AddItem quitMenuItem;
        appMenuItem.Submenu <- appMenu;

        NSApplication.SharedApplication.MainMenu <- menu;

    override this.FinishedLaunching notification =
        do createMenu NSProcessInfo.ProcessInfo.ProcessName

        let controller = NovelDocumentController.SharedDocumentController
        controller.NewDocument this

module main =
    [<EntryPoint>]
    let main args =
        NSApplication.Init ()

        let app = NSApplication.SharedApplication
        app.Delegate <- new NovelAppDelegate ()
        app.Run ()
        0

