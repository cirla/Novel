namespace NovelMac

open System
open System.Drawing

open MonoMac.ObjCRuntime
open MonoMac.Foundation
open MonoMac.AppKit

open NovelMac.Controls

type AboutWindow () as self =
    inherit NSWindow ()

    do
        self.StyleMask <- NSWindowStyle.Closable ||| NSWindowStyle.Miniaturizable ||| NSWindowStyle.Titled
        self.SetFrame (new RectangleF (0.0f, 0.0f, 400.0f, 400.0f), false)
        self.Center ()

        // override close button to hide window
        let closeButton = self.StandardWindowButton NSWindowButton.CloseButton
        closeButton.Action <- new Selector "orderOut:"

        let view = self.ContentView
        let label = new Label "Novel"
        view.AddSubview label

        NSLayoutConstraint.Create
            (label, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal,
             view, NSLayoutAttribute.CenterX, 1.0f, 0.0f)
        |> view.AddConstraint

        NSLayoutConstraint.Create
            (label, NSLayoutAttribute.CenterY, NSLayoutRelation.Equal,
             view, NSLayoutAttribute.CenterY, 1.0f, 0.0f)
        |> view.AddConstraint

type NovelAppDelegate () =
    inherit NSApplicationDelegate ()

    let aboutWindow = new AboutWindow ()
    let documentController = new NovelDocumentController ()

    let newDocument sender =
        let controller = NovelDocumentController.SharedDocumentController
        controller.NewDocument sender

    let newDocumentHandler (sender:obj) (e:EventArgs) =
        newDocument (sender :?> NSObject)

    let quitHandler (sender:obj) (e:EventArgs) =
        NSApplication.SharedApplication.Terminate (sender :?> NSObject)

    let createMainMenu appName =
        let mainMenu = new NSMenu ()

        let appMenuItem = new NSMenuItem ()
        let appMenu = new NSMenu ()

        appMenu.AddItem (new NSMenuItem (sprintf "About %s" appName, (fun sender e ->
            aboutWindow.MakeKeyAndOrderFront mainMenu)))
        appMenu.AddItem (new NSMenuItem ("Check for Updates...", (fun sender e -> () )))
        appMenu.AddItem NSMenuItem.SeparatorItem
        appMenu.AddItem (new NSMenuItem ("Preferences...", ",", (fun sender e -> () )))
        appMenu.AddItem NSMenuItem.SeparatorItem
        appMenu.AddItem (new NSMenuItem (sprintf "Quit %s" appName, "q", quitHandler))

        appMenuItem.Submenu <- appMenu
        mainMenu.AddItem appMenuItem

        let fileMenuItem = new NSMenuItem ()
        let fileMenu = new NSMenu "File"

        fileMenu.AddItem (new NSMenuItem ("New...", "n", newDocumentHandler))
        fileMenu.AddItem (new NSMenuItem ("Open...", "o", (fun sender e -> () )))
        fileMenu.AddItem NSMenuItem.SeparatorItem
        fileMenu.AddItem (new NSMenuItem ("Save", "s", (fun sender e -> () )))
        fileMenu.AddItem (new NSMenuItem ("Save As...", (fun sender e -> () )))
        fileMenu.AddItem (new NSMenuItem ("Save All", "S", (fun sender e -> () )))

        fileMenuItem.Submenu <- fileMenu
        mainMenu.AddItem fileMenuItem

        let editMenuItem = new NSMenuItem ()
        let editMenu = new NSMenu "Edit"
        editMenuItem.Submenu <- editMenu
        mainMenu.AddItem editMenuItem

        mainMenu

    override this.FinishedLaunching notification =
        NSApplication.SharedApplication.MainMenu <- createMainMenu NSProcessInfo.ProcessInfo.ProcessName

        newDocument this

module main =
    [<EntryPoint>]
    let main args =
        NSApplication.Init ()

        let app = NSApplication.SharedApplication
        app.Delegate <- new NovelAppDelegate ()
        app.Run ()
        0
