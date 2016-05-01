open Fratr
open System
open System.Windows
open System.Windows.Controls
open System.Windows.Markup

/// Raytracer in F#.
/// Closely based on http://gettingsharper.de/2011/11/30/next-raytracing-in-f/
/// Also https://github.com/MartinDoms/Fray
/// And http://www.tryfsharp.org/create/cpoulain/shared/raytracer.fsx
  
[<STAThread>]
[<EntryPoint>]
let main argv = 
    let application =
        //let uri = System.Uri("/Fratr;component/App.xaml", UriKind.Relative)
        let uri = System.Uri("App.xaml", UriKind.Relative)
        Application.LoadComponent(uri) :?> Application
    
    // Hook into UI elements here
    application.Activated
    |> Event.add (fun _ -> Bitmap.Initialize application.MainWindow SampleScene.Scene3)

    // Use this to save file rather than display in window
    // Bitmap.SaveImage SampleScene.Scene1

    application.Run()

