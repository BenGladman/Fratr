namespace Fratr

open Scene
open Vector
open ViewPort

/// Raytracer in F#.
/// Closely based on http://gettingsharper.de/2011/11/30/next-raytracing-in-f/
/// Also https://github.com/MartinDoms/Fray
/// And http://www.tryfsharp.org/create/cpoulain/shared/raytracer.fsx
module Program = 
    [<EntryPoint>]
    let main argv =
        printfn "Start..."

        let vp: ViewPort = {
            Eye = Vector (0.0, 0.0, -10.0)
            Center = Vector.Zero
            DirUp = Vector.E2
            DirRight = Vector.E1
            Height = 20.0
            Width = 20.0
            }

        let objs = [
            SceneObject.Sphere (Vector (0.0, 0.0, 10.0)) 10.0 Color.Red
            SceneObject.Sphere (Vector (5.0, 3.0, 5.0)) 5.0 Color.Blue
            SceneObject.Sphere (Vector (-5.0, -3.0, 14.0)) 15.0 Color.White
            ]

        let lights = [
            Light.CreatePointLight Color.White (Vector (1.0, 10.0, -10.0))
            Light.CreateDirectionalLight Color.White (Vector (0.0, -1.0, 0.1))
            ]

        let scene: Scene = {
            ViewPort = vp
            ResX = 600
            ResY = 600
            Objs = objs
            Lights = lights
            }

        Bitmap.SaveImage scene

        printfn "Done!"
        System.Threading.Thread.Sleep(1000)
        0 // return an integer exit code
