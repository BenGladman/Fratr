namespace Fratr

open Scene
open Vector
open ViewPort

/// Raytracer in F#.
/// Closely based on http://gettingsharper.de/2011/11/30/next-raytracing-in-f/
/// Also https://github.com/MartinDoms/Fray
/// And http://www.tryfsharp.org/create/cpoulain/shared/raytracer.fsx
module Program = 
    //[<EntryPoint>]
    let main argv =
        printfn "Start..."

        Bitmap.SaveImage SampleScene1.Scene

        printfn "Done!"
        System.Threading.Thread.Sleep(1000)
        0 // return an integer exit code
