using System;
using System.Drawing;
using System.Numerics;
using Canvas;
using Canvas.Desktop;
using Cubic;
using Bitmap = Canvas.Bitmap;

Random random = new Random();

Vector2 pos = Vector2.Zero;
Bitmap bp = new Bitmap("C:/Users/ollie/Pictures/blending_transparent_window.png");

CanvasApplication appl = new CanvasApplication(new Size(800, 600), CreateCanvas);

void CreateCanvas(Image image)
{
    image.Clear(Color.CornflowerBlue);
    Bitmap bp1 = new Bitmap(@"C:\Users\ollie\Documents\C#\SpaceBox\SpaceBox\Content\Textures\UI\default.png");
    Bitmap bp2 = new Bitmap(@"C:\Users\ollie\Documents\C#\SpaceBox\SpaceBox\Content\Textures\UI\logo.png");
    Bitmap bp3 = new Bitmap(@"C:\Users\ollie\Documents\C#\SpaceBox\SpaceBox\Content\Icon.bmp");
    
    image.Draw(bp2, 0, 0, 0.42f, 0.15f);
    image.Draw(bp1, 100, 075, 1.25f, 3.5f, 1);
    image.Draw(bp3, 400, 305, 0.25f, 0.25f, 0.5f);
    
    image.Draw(bp, (int) pos.X, (int) pos.Y);
    
    image.Save("C:/Users/ollie/Pictures/CANVAS_TEST.png");
}