namespace Fratr

open Ray
open Vector

module ViewPort = 
    type ViewPort = {
        Eye: Point
        Center: Point
        DirUp: Direction
        DirRight: Direction
        Height: float
        Width: float
        } 
        
    let private createRay (vp: ViewPort) (viewPortPoint: Point) =
        Ray.FromPoints vp.Eye viewPortPoint