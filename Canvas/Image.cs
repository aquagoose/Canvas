using System;
using System.Drawing;
using System.IO;
using StbImageWriteSharp;

namespace Canvas;

public unsafe partial class Image
{
    public readonly Size Size;

    private byte[] _buffer;

    public byte[] Data => _buffer;

    private byte[] _colorBuffer;
    
    public Image(Size size)
    {
        Size = size;
        _buffer = new byte[size.Width * size.Height * 4];
        _colorBuffer = new byte[4];
    }

    public void Clear(Color color)
    {
        SetColorBuffer(color);
        fixed (byte* buf = _buffer)
        {
            for (int i = 0; i < _buffer.Length; i += 4)
            {
                buf[i] = _colorBuffer[0];
                buf[i + 1] = _colorBuffer[1];
                buf[i + 2] = _colorBuffer[2];
                buf[i + 3] = _colorBuffer[3];
            }
        }
    }

    private void SetColorBuffer(Color color)
    {
        _colorBuffer[0] = color.R;
        _colorBuffer[1] = color.G;
        _colorBuffer[2] = color.B;
        _colorBuffer[3] = color.A;
    }

    private byte PerformAlphaBlend(byte newValue, byte oldValue, float alpha) =>
        (byte) (alpha * newValue + (1 - alpha) * oldValue);

    public void Save(string path)
    {
        using Stream stream = File.OpenWrite(path);
        ImageWriter writer = new ImageWriter();
        writer.WritePng(_buffer, Size.Width, Size.Height, ColorComponents.RedGreenBlueAlpha, stream);
    }

    public MemoryStream SavePngToMemory()
    {
        MemoryStream stream = new MemoryStream();
        ImageWriter writer = new ImageWriter();
        writer.WritePng(_buffer, Size.Width, Size.Height, ColorComponents.RedGreenBlueAlpha, stream);
        stream.Position = 0;
        return stream;
    }
}