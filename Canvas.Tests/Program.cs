using System.Diagnostics;
using System.Drawing;
using Canvas;

Image image = new Image(new Size(800, 600));
Stopwatch sw = Stopwatch.StartNew();
image.Clear(Color.CornflowerBlue);
Bitmap bp = new Bitmap("C:/Users/ollie/Documents/awesomeface.png");
image.Draw(bp, 500, 300);
sw.Stop();
Console.WriteLine(sw.ElapsedMilliseconds);
image.Save("C:/Users/ollie/Pictures/CANVAS_TEST.png");