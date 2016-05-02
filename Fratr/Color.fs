namespace Fratr

open FloatHelpers

module Color = 
    type Color = 
        struct
            val R: float
            val G: float
            val B: float
            
            new (r, g, b) = {
                R = r
                G = g
                B = b
                }
            
            static member (*) (c1: Color, c2: Color) =
                Color (c1.R * c2.R, c1.G * c2.G, c1.B * c2.B)

            static member (*) (s: float, c: Color) =
                Color (s * c.R, s * c.G, s * c.B)

            static member (+) (c1: Color, c2: Color) =
                Color (c1.R + c2.R, c1.G + c2.G, c1.B + c2.B)

            /// Used by Seq.Average
            static member DivideByInt (c: Color, s: int) =
                Color (c.R / (float)s, c.G / (float)s, c.B / (float)s)

            static member Zero = Color (0.0, 0.0, 0.0)
            static member One = Color (1.0, 1.0, 1.0)
        end
    
    let Black = Color (0.0, 0.0, 0.0)
    let White = Color (1.0, 1.0, 1.0)
    let Red = Color (1.0, 0.0, 0.0)
    let Green = Color (0.0, 1.0, 0.0)
    let Blue = Color (0.0, 0.0, 1.0)
    let LightGrey = Color (0.8, 0.8, 0.8)
    let DarkGrey = Color (0.3, 0.3, 0.3)
