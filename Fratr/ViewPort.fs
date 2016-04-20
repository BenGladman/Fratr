namespace Fratr

open Ray
open Vector

module ViewPort = 
    type RayRaster = Ray [,]
    
    type ViewPort = {
        Eye: Point
        Center: Point
        DirUp: Direction
        DirRight: Direction
        Height: float
        Width: float
        } 
        
    let private createRaster vp (resX: int) (resY: int): Point [,] = 
        let dX = vp.Width / (float resX)
        let dY = vp.Height / (float resY)
        let start = vp.Center - ((vp.Width - dX) / 2.0) * vp.DirRight - ((vp.Height - dY) / 2.0) * vp.DirUp
        Array2D.init resX resY (fun x y -> start + (float x * dX) * vp.DirRight + (float y * dY) * vp.DirUp)
        
    let private createRay (vp: ViewPort) (viewPortPoint: Point) =
        Ray.FromPoints vp.Eye viewPortPoint

    let CreateRayRaster vp resX resY: RayRaster = 
        createRaster vp resX resY |> Array2D.map (createRay vp)
