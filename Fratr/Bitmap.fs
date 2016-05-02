namespace Fratr

open Color
open RayTrace
open Scene
open System.Windows
open System.Windows.Media.Imaging

module Bitmap =
    let Initialize (mainWindow: System.Windows.Window) (scene: Scene) =
        let image = mainWindow.FindName("image") :?> System.Windows.Controls.Image
        let infotext = mainWindow.FindName("infotext") :?> System.Windows.Controls.TextBlock

        let resx = scene.ResX
        let resy = scene.ResY

        let wb = WriteableBitmap (resx, resy, 96.0, 96.0, System.Windows.Media.PixelFormats.Bgra32, null)
        image.Source <- wb
        infotext.Text <- "Rendering..."

        // convert float between 0 and 1 to byte value
        let f2b (f: float) =
            255.0 * min 1.0 (max 0.0 f) |> byte

        // 1x1px rectangle
        let sourceRect = Int32Rect (0, 0, 1, 1)
        // bytes per (1px-wide) row
        let sourceStride = 4
        let alpha = 255uy
        let pixel = [| 0uy; 0uy; 0uy; alpha; |]

        let setPixel x y (color: Color) =
            pixel.SetValue (f2b color.B, 0)
            pixel.SetValue (f2b color.G, 1)
            pixel.SetValue (f2b color.R, 2)
            wb.WritePixels (sourceRect, pixel, sourceStride, x, resy - 1 - y)

        let stopWatch = System.Diagnostics.Stopwatch.StartNew()
        RayTrace setPixel scene
        stopWatch.Stop()

        infotext.Text <- sprintf "Rendered in %i ms" stopWatch.ElapsedMilliseconds

    let SaveImage scene =
        let resx = scene.ResX
        let resy = scene.ResY

        use bitmap = new System.Drawing.Bitmap(resx, resy)
 
        use dc = System.Drawing.Graphics.FromImage(bitmap)
        dc.Clear(System.Drawing.Color.DarkSlateGray)

        // convert float between 0 and 1 to integer 0-255
        let f2i x = 255.0 * min 1.0 (max 0.0 x) |> int

        let setPixel x y (c: Color) =
            let color = System.Drawing.Color.FromArgb(255, f2i c.R, f2i c.G, f2i c.B)
            bitmap.SetPixel(x, resy - 1 - y, color)

        RayTrace setPixel scene
 
        bitmap.Save(@"d:\temp\Fratr.bmp")