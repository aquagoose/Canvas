using System.Drawing;

namespace Canvas;

public unsafe partial class Image
{
    public void Rectangle(int x, int y, int w, int h, Color color)
    {
        SetColorBuffer(color);
        
        float alpha = _colorBuffer[3] / 255f;
        
        fixed (byte* buf = _buffer)
        {
            for (int rX = 0; rX < w; rX++)
            {
                for (int rY = 0; rY < h; rY++)
                {
                    int bufPos = (rY + y) * 4 * Size.Width + (rX + x) * 4;
                    buf[bufPos] = PerformAlphaBlend(_colorBuffer[0], buf[bufPos], alpha);
                    buf[bufPos + 1] = PerformAlphaBlend(_colorBuffer[1], buf[bufPos + 1], alpha);
                    buf[bufPos + 2] = PerformAlphaBlend(_colorBuffer[2], buf[bufPos + 2], alpha);
                }
            }
        }
    }

    public void Border(int x, int y, int w, int h, int borderWidth, Color color)
    {
        // Top side
        Rectangle(x, y, w, borderWidth, color);
        // Bottom side
        Rectangle(x, y + (h - borderWidth), w, borderWidth, color);
        // Left side
        Rectangle(x, y + borderWidth, borderWidth, h - borderWidth * 2, color);
        // Right side
        Rectangle(x + (w - borderWidth), y + borderWidth, borderWidth, h - borderWidth * 2, color);
    }

    public void BorderRectangle(int x, int y, int w, int h, int borderWidth, Color mainColor, Color borderColor)
    {
        Border(x, y, w, h, borderWidth, borderColor);
        Rectangle(x + borderWidth, y + borderWidth, w - borderWidth * 2, h - borderWidth * 2, mainColor);
    }

    public void Pixel(int x, int y, Color color)
    {
        if (x < 0 || x >= Size.Width || y < 0 || y >= Size.Height)
            return;
        SetColorBuffer(color);
        fixed (byte* buf = _buffer)
        {
            int bufPos = y * 4 * Size.Width + x * 4;
            buf[bufPos] = _colorBuffer[0];
            buf[bufPos + 1] = _colorBuffer[1];
            buf[bufPos + 2] = _colorBuffer[2];
        }
    }
    
    //public void Line(int x1, int y1, int x2, int y2)

    public void Circle(int x, int y, int r, Color color)
    {
        int cX = 0;
        int cY = r;
        int d = 3 - 2 * r;
        CirclePx(x, y, cX, cY, color);
        while (cY >= cX)
        {
            cX++;
            if (d > 0)
            {
                cY--;
                d = d + 4 * (cX - cY) + 10;
            }
            else
                d = d + 4 * cX + 6;
            CirclePx(x, y, cX, cY, color);
        }
    }

    private void CirclePx(int xc, int yc, int x, int y, Color color)
    {
        Pixel(xc + x, yc + y, color);
        Pixel(xc - x, yc + y, color);
        Pixel(xc + x, yc - y, color);
        Pixel(xc - x, yc - y, color);
        Pixel(xc + y, yc + x, color);
        Pixel(xc - y, yc + x, color);
        Pixel(xc + y, yc - x, color);
        Pixel(xc - y, yc - x, color);
    }
}