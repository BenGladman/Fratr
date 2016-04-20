namespace Fratr

open FloatHelpers

module Vector = 
    type Vector = 
        struct
            val X: float
            val Y: float
            val Z: float
            
            new (x, y, z) = {
                X = x
                Y = y
                Z = z
                }
            
            static member (+) (a: Vector, b: Vector) =
                Vector(a.X + b.X, a.Y + b.Y, a.Z + b.Z)

            static member (-) (a: Vector, b: Vector) =
                Vector(a.X - b.X, a.Y - b.Y, a.Z - b.Z)

            static member (*) (s: float, v: Vector) =
                Vector(s * v.X, s * v.Y, s * v.Z)

            override v.ToString() =
                sprintf "[%f, %f, %f]" v.X v.Y v.Z
        end
    
    /// (0,0,0)
    let Zero = Vector (0.0, 0.0, 0.0)

    /// (1,0,0)
    let E1 = Vector (1.0, 0.0, 0.0)

    /// (0,1,0)
    let E2 = Vector (0.0, 1.0, 0.0)

    /// (0,0,1)
    let E3 = Vector (0.0, 0.0, 1.0)

    let Dot (a: Vector, b: Vector) =
        (a.X * b.X) + (a.Y * b.Y) + (a.Z * b.Z)

    let MagSquared (v: Vector) =
        (v.X * v.X) + (v.Y * v.Y) + (v.Z * v.Z)
    
    let Mag (v: Vector) = 
        v |> MagSquared |> sqrt
        
    let Norm (v: Vector) = 
        match Mag v with
        | NotNearZero mag -> ((1.0 / mag) * v)
        | _ -> Zero
    
    let Cross (v1: Vector, v2: Vector) = 
        Vector (v1.Y * v2.Z - v1.Z * v2.Y,
            v1.Z * v2.X - v1.X * v2.Z,
            v1.X * v2.Y - v1.Y * v2.X)

    /// Synonym of Vector
    type Point = Vector

    /// Synonym of Vector
    type Direction = Vector
    
