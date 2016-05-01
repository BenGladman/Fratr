namespace Fratr

open Color
open FloatHelpers
open Light
open Ray
open Scene
open SceneObject
open Vector
open ViewPort

module RayTrace =
    let private findHitObj (getSigObjs: Ray -> SceneObject seq) (ray: Ray) =
        let getDistance (h: HitResult option) =
            if h.IsNone
            then infinity
            else (Option.get h).Distance

        ray
        |> getSigObjs
        // intersect ray with each possible object
        |> Seq.map (fun o -> o.HitTest ray)
        // get the nearest (or none of no hit)
        |> Seq.minBy getDistance

    let private shade (getSigObjs: Ray -> SceneObject seq) (hit: HitResult) (light: Light) : Color =
        // get the direction of the light for this point
        let lightDir = Light.GetDirection light hit.Pos
        // is the light visible?
        let rayToLight = Ray (hit.Pos, lightDir)
        let lightVisible = findHitObj getSigObjs rayToLight |> Option.isNone

        let diffuseColor =
            if lightVisible then
                let diffFrac = (Dot (hit.Normal, lightDir))
                if (IsPositive diffFrac) then
                    let diffuseFactor = 0.6
                    diffuseFactor * diffFrac * hit.Material.DiffuseColor * light.Color
                else
                    Color.Black
            else
                Color.Black

        let specularColor =
            if lightVisible then
                let reflDir = hit.Ray.Direction - 2.0 * (Dot (hit.Normal, hit.Ray.Direction)) * hit.Normal;
                let specFrac = (Dot (reflDir, lightDir))
                if (specFrac > 0.1) then
                    System.Math.Pow(specFrac, 10.0) * hit.Material.Shininess * hit.Material.SpecularColor * light.Color
                else
                    Color.Black
            else
                Color.Black

        let ambientColor =
            let ambientFactor = 0.4
            ambientFactor * hit.Material.DiffuseColor * light.Color

        diffuseColor + specularColor + ambientColor

    let private traceRay (getSigObjs: Ray -> SceneObject seq) (scene: Scene) (ray: Ray) =
        let rec traceRecursive iter ray =
            if iter > 9 then
                // Max reflections
                Color.Black
            else
                let reflectHit (hit: HitResult) =
                    if hit.Material.Reflectivity |> IsPositive then
                        let reflDir = hit.Ray.Direction - 2.0 * (Dot (hit.Normal, hit.Ray.Direction)) * hit.Normal;
                        let reflRay = Ray (hit.Pos, reflDir)
                        let reflColor = traceRecursive (iter + 1) reflRay
                        hit.Material.Reflectivity * reflColor
                    else
                        Color.Black        

                match findHitObj getSigObjs ray with
                | Some hit -> (scene.Lights |> Seq.averageBy (shade getSigObjs hit)) + (reflectHit hit)
                | None when iter = 0 -> scene.Background
                | None -> Color.Black

        traceRecursive 0 ray

    /// Applies the given function to each pixel in the image
    let RayTrace (func: int -> int -> Color -> unit) (scene: Scene) =
        let vp = scene.ViewPort
        let resX = scene.ResX
        let resY = scene.ResY

        let dX = vp.Width / (float resX)
        let dY = vp.Height / (float resY)
        let start = vp.Center - ((vp.Width - dX) / 2.0) * vp.DirRight - ((vp.Height - dY) / 2.0) * vp.DirUp

        let getSigObjs (ray: Ray) =
            scene.Objs

        for x in 0 .. resX - 1 do
            for y in 0 .. resY - 1 do
                let point = start + (float x * dX) * vp.DirRight + (float y * dY) * vp.DirUp
                let ray = Ray.FromPoints vp.Eye point
                let c = traceRay getSigObjs scene ray
                func x y c

