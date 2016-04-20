namespace Fratr

open Color

module Bitmap =
    let SaveImage scene =
        let pixels = Scene.RayTrace scene

        let toSysColor (c: Color) =
            let f x = 255.0 * min 1.0 (max 0.0 x) |> int
            System.Drawing.Color.FromArgb(255, f c.R, f c.G, f c.B)
 
        let resx = scene.ResX
        let resy = scene.ResY

        use bitmap = new System.Drawing.Bitmap(resx, resy)
        let setPixel x y c =
            match c with
            | None -> ()
            | Some color -> bitmap.SetPixel(x, resy - 1 - y, color |> toSysColor)
 
        use dc = System.Drawing.Graphics.FromImage(bitmap)
        dc.Clear(System.Drawing.Color.DarkSlateGray)
 
        pixels |> Array2D.iteri setPixel
 
        bitmap.Save(@"d:\temp\Fratr.bmp")

