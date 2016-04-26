namespace Fratr

open Color
open RayTrace
open Scene
open System.Windows
open System.Windows.Media.Imaging

module Bitmap =
    let Initialize (mainWindow: System.Windows.Window) (scene: Scene) =
        let image = mainWindow.FindName("image") :?> System.Windows.Controls.Image

        let resx = scene.ResX
        let resy = scene.ResY

        let wb = WriteableBitmap (resx, resy, 96.0, 96.0, System.Windows.Media.PixelFormats.Bgra32, null)

        // convert float between 0 and 1 to byte value
        let f2b (f: float) =
            255.0 * min 1.0 (max 0.0 f) |> byte

        // 1x1px rectangle
        let sourceRect = Int32Rect (0, 0, 1, 1)
        // bytes per (1px-wide) row
        let sourceStride = 4
        let alpha = 255uy
        let bgpixel = [| 50uy; 50uy; 50uy; alpha; |]
        let pixel = [| 0uy; 0uy; 0uy; alpha; |]

        let setPixel x y (c: Color option) =
            match c with
            | None -> wb.WritePixels (sourceRect, bgpixel, sourceStride, x, resy - 1 - y)
            | Some color -> 
                pixel.SetValue (f2b color.B, 0)
                pixel.SetValue (f2b color.G, 1)
                pixel.SetValue (f2b color.R, 2)
                wb.WritePixels (sourceRect, pixel, sourceStride, x, resy - 1 - y)

        RayTrace setPixel scene

        image.Source <- wb

    let SaveImage scene =
        let resx = scene.ResX
        let resy = scene.ResY

        use bitmap = new System.Drawing.Bitmap(resx, resy)
 
        use dc = System.Drawing.Graphics.FromImage(bitmap)
        dc.Clear(System.Drawing.Color.DarkSlateGray)

        let toSysColor (c: Color) =
            let f x = 255.0 * min 1.0 (max 0.0 x) |> int
            System.Drawing.Color.FromArgb(255, f c.R, f c.G, f c.B)

        let setPixel x y c =
            match c with
            | None -> ()
            | Some color -> bitmap.SetPixel(x, resy - 1 - y, color |> toSysColor)

        RayTrace setPixel scene
 
        bitmap.Save(@"d:\temp\Fratr.bmp")