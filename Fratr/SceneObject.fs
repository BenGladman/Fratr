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

    let private solveQuadEquation (a : float,  b : float, c : float) =
        let rad = b*b - 4.0*a*c
        let k0 = b / (-2.0*a)
        match rad with
        | Negative -> []
        | NearZero -> [ k0 ]
        | Positive -> let sqrtRad = (sqrt rad)/(2.0 * a)
                      [ k0 + sqrtRad; k0 - sqrtRad ]

    let Sphere (center: Point) (radius: float) (material: Material) : SceneObject =
        let r2 = radius * radius
        let hitTest (ray : Ray) =
            
            let getResult t =
                let pos = ray.Start + t * ray.Direction
                let normal = Norm (pos - center)
                { Ray = ray; Distance = t; Pos = pos; Normal = normal; Material = material }

            let ts =
                let v0 = center - ray.Start
                let b = -2.0*(Dot (v0,ray.Direction))
                let c = (Dot (v0,v0)) - r2
                solveQuadEquation (1.0, b, c)

            match ts |> List.filter IsPositive with
            | [t]    -> t |> getResult |> Some
            | [a; b] -> (min a b) |> getResult |> Some
            | _      -> None

        { HitTest = hitTest }

