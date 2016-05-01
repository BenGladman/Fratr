namespace Fratr

open Scene
open Vector
open ViewPort

module SampleScene =
    let Scene1 =
        let vp: ViewPort = {
            Eye = Vector (0.0, 0.0, -10.0)
            Center = Vector.Zero
            DirUp = Vector.E2
            DirRight = Vector.E1
            Height = 20.0
            Width = 20.0
            }

        let objs = [
            SceneObject.Sphere (Vector (0.0, 0.0, 10.0)) 10.0 Material.RedShiny
            SceneObject.Sphere (Vector (5.0, 3.0, 5.0)) 5.0 Material.BlueMatt
            SceneObject.Sphere (Vector (-5.0, -3.0, 14.0)) 15.0 Material.WhiteMatt
            ]

        let lights = [
            Light.CreatePointLight Color.White (Vector (1.0, 10.0, -10.0))
            Light.CreateDirectionalLight Color.White (Vector (0.0, -1.0, 0.1))
            ]

        {
            ViewPort = vp
            ResX = 400
            ResY = 400
            Objs = objs
            Lights = lights
            Background = Color.DarkGrey
        }

    let Scene2 =
        let vp: ViewPort = {
            Eye = Vector (0.0, 0.0, -30.0)
            Center = Vector.Zero
            DirUp = Vector.E2
            DirRight = Vector.E1
            Height = 20.0
            Width = 20.0
            }

        let objs = [
            SceneObject.Sphere (Vector (0.0, 0.0, 10.0)) 5.0 Material.RedShiny
            SceneObject.Sphere (Vector (-3.5, -9.0, 9.0)) 4.0 Material.GreenShiny
            SceneObject.Sphere (Vector (0.5, 7.5, 4.0)) 3.0 Material.BlueShiny
            ]

        let lights = [
            Light.CreatePointLight Color.White (Vector (1.0, 20.0, -10.0))
            Light.CreateDirectionalLight Color.White (Vector (0.0, -1.0, 0.1))
            ]

        {
            ViewPort = vp
            ResX = 400
            ResY = 400
            Objs = objs
            Lights = lights
            Background = Color.DarkGrey
        }

    let Scene3 =
        let vp: ViewPort = {
            Eye = Vector (0.0, 0.0, -20.0)
            Center = Vector.Zero
            DirUp = Vector.E2
            DirRight = Vector.E1
            Height = 20.0
            Width = 20.0
            }

        let objs = [
            SceneObject.Sphere (Vector (-5.0, -2.0, 15.0)) 4.0 Material.BlueReflective
            SceneObject.Sphere (Vector (5.0, -2.0, 15.0)) 7.0 Material.WhiteReflective
            SceneObject.Sphere (Vector (0.0, -8.0, 11.0)) 3.0 Material.BlackReflective
            SceneObject.Sphere (Vector (0.0, 9.0, 16.0)) 7.0 Material.BlackReflective
            ]

        let lights = [
            Light.CreatePointLight Color.White (Vector (1.0, 20.0, -10.0))
            Light.CreateDirectionalLight Color.White (Vector (0.0, -1.0, 0.1))
            ]

        {
            ViewPort = vp
            ResX = 400
            ResY = 400
            Objs = objs
            Lights = lights
            Background = Color.Blue
        }