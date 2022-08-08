using System.Drawing;
using System.IO;
using StbImageSharp;

namespace Canvas;

public class Bitmap
{
    public readonly byte[] Data;
    public readonly Size Size;
    public readonly ColorComponents Components;

    public Bitmap(string path)
    {
        ImageResult result = ImageResult.FromMemory(File.ReadAllBytes(path));
        Components = result.Comp;
        Size = new Size(result.Width, result.Height);
        Data = result.Data;
    }
}