namespace Fratr

open Color
open Vector

module Light =
    type LightType =
        | DirectionalLight of Direction
        | PointLight of Point
    
    type Light =
        struct
            val LightType: LightType
            val Color: Color
            new (lightType, color) = {
                LightType = lightType
                Color = color
                }
        end
    
    /// Directional light has the same direction everywhere
    let CreateDirectionalLight (color: Color) (direction: Direction) =
        Light (DirectionalLight(-1.0 * direction |> Norm), color)
    
    /// Point light has a point source
    let CreatePointLight (color: Color) (position: Point) =
        Light (PointLight(position), color)
    
    /// Direction of light at point
    let GetDirection (light: Light) point =
        match light.LightType with
        | DirectionalLight v -> v
        | PointLight v -> v - point |> Norm
