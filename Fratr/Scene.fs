namespace Fratr

open Color
open Light
open Ray
open SceneObject
open Vector
open ViewPort

module Scene =
    type Scene = {
        ViewPort: ViewPort
        ResX: int
        ResY: int
        Objs: SceneObject seq
        Lights: Light seq
        }

    let private shade (hit: HitResult) (light: Light) : Color =
        // lighting factors
        let ambient = 0.4
        let diffuse = 0.6
        // get the direction of the light for this point
        let lightDir = GetDirection light hit.Pos
        // get the fraction of diffuse Light (cap at 0)
        let diffF = max 0.0 (Dot (hit.Normal, lightDir))
        // calculate the total color by ambient + diffuse light
        let s = diffuse * diffF + ambient
        // return the shaded color
        s * hit.Color

    let private findHitObj (ray: Ray) (objs: SceneObject seq) =
        let getDistance (h: HitResult option) =
            if h.IsNone
            then infinity
            else (Option.get h).Distance

        objs
        // intersect ray with each possible object
        |> Seq.map (fun o -> o.HitTest ray)
        // get the nearest (or none of no hit)
        |> Seq.minBy getDistance

    let private traceRay (objs: SceneObject seq) (lights: Light seq) (ray: Ray) =
        findHitObj ray objs
        |> Option.map (fun hit -> lights |> Seq.averageBy (shade hit))

        (* alternate implentation ...
        let lgs = lights |> Array.ofSeq
        // modify the strength of the shading to account for multiple
        // lights (so for two lights each will contribute 1/2 to the color)
        let strMod (color : Color) = (1.0 / (float lgs.Length)) * color
        // search for a hitpoint
        let hit = findHitObj ray objs
        // get the shaded color if a point was hit
        let shadeCol = hit |> Option.map (fun hit -> lgs |> Array.sumBy (strMod << shade hit))
        // return the shaded color
        shadeCol
        *)

    let RayTrace (scene: Scene) =
        ViewPort.CreateRayRaster scene.ViewPort scene.ResX scene.ResY
        |> Array2D.map (traceRay scene.Objs scene.Lights)

