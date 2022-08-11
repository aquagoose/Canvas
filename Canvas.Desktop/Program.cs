using System;
using System.Drawing;
using System.Numerics;
using Canvas;
using Canvas.Desktop;
using Bitmap = Canvas.Bitmap;

Random random = new Random();

CanvasApplication appl = new CanvasApplication(new Size(800, 600), CreateCanvas);

void CreateCanvas(Image image)
{
    image.Clear(Color.CornflowerBlue);

    Bitmap bp1 =
        new Bitmap(
            "/home/ollie/Documents/C#/SpaceBox/SpaceBox/bin/Debug/net6.0/Data/Screenshots/2022-07-30-10-29-09.png");
    image.Draw(bp1, 0, 0, image.Size.Width / (float) bp1.Size.Width, image.Size.Height / (float) bp1.Size.Height);

    image.Circle(50, 50, 50, Color.White);
    
    
}