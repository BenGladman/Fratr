namespace Fratr

open Vector

module Ray =
    type Ray =
        struct
            val Start: Point
            val Direction: Direction

            new (s, d) = {
                Start = s
                Direction = d
                }
        end
    
    let FromPoints start (pointOnRay: Point) =
        let direction = pointOnRay - start |> Norm
        Ray (start, direction)