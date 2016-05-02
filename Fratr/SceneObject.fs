namespace Fratr

open Color
open FloatHelpers
open Material
open Ray
open Vector

module SceneObject =
    type HitResult = {
        Ray: Ray
        Distance: float
        Pos: Point
        Normal: Direction
        Material: Material
        }

    type SceneObject = {
        HitTest: Ray -> HitResult option
        }

    let Sphere (center: Point) (radius: float) (material: Material) : SceneObject =
        let r2 = radius * radius

        let hitTest2 (ray: Ray) =
            let v0 = center - ray.Start
            let k0 = (Dot (v0, ray.Direction))
            let b = -2.0 * k0
            let c = (Dot (v0, v0)) - r2
            let rad = b * b - 4.0 * c

            let dist =
                match rad with
                | Negative -> 0.0
                | NearZero -> k0
                | Positive ->
                    let sqrtRad = (sqrt rad) / 2.0
                    let root1 = k0 - sqrtRad
                    if root1 |> IsPositive then
                        root1
                    else
                        k0 + sqrtRad

            if dist |> IsPositive then
                let pos = ray.Start + dist * ray.Direction
                let normal = Norm (pos - center)
                { Ray = ray; Distance = dist; Pos = pos; Normal = normal; Material = material }
                |> Some
            else
                None
                                
        { HitTest = hitTest2 }

    let Plane (pointOnPlane: Point) (normal: Direction) (material: Material) : SceneObject =
        let hitTest (ray: Ray) =
            let k = Dot (ray.Direction, normal)
            if k |> IsNearZero then
                // ray is perpendicular to plane normal
                None
            else
                let l = Dot ((pointOnPlane - ray.Start), normal)
                let dist = (l / k)
                if dist |> IsPositive then
                    let pos = ray.Start + dist * ray.Direction
                    { Ray = ray; Distance = dist; Pos = pos; Normal = normal; Material = material }
                    |> Some
                else
                    None
                
        { HitTest = hitTest }
