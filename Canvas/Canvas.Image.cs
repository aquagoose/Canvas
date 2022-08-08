using System;
using StbImageSharp;

namespace Canvas;

public unsafe partial class Image
{
    public void Draw(Bitmap image, int x, int y, float alpha = 1)
    {
        fixed (byte* buf = _buffer)
        {
            int startX = x < 0 ? -x : 0;
            int startY = y < 0 ? -y : 0;

            int width = image.Size.Width + x > Size.Width ? Size.Width - x : image.Size.Width;
            int height = image.Size.Height + y > Size.Height ? Size.Height - y : image.Size.Height;

            int multiplier = image.Components == ColorComponents.RedGreenBlueAlpha ? 4 : 3;
            
            for (int pX = startX; pX < width; pX++)
            {
                for (int pY = startY; pY < height; pY++)
                {
                    int bufPos = (pY * 4 + y * 4) * Size.Width + pX * 4 + x * 4;
                    int imgPos = pY * multiplier * image.Size.Width + pX * multiplier;

                    float alph = alpha * (image.Components == ColorComponents.RedGreenBlueAlpha ? image.Data[imgPos + 3] / 255f : 1);
                    
                    buf[bufPos] = (byte) (alph * image.Data[imgPos] + (1 - alph) * buf[bufPos]);
                    buf[bufPos + 1] = (byte) (alph * image.Data[imgPos + 1] + (1 - alph) * buf[bufPos + 1]);
                    buf[bufPos + 2] = (byte) (alph * image.Data[imgPos + 2] + (1 - alph) * buf[bufPos + 2]);
                    buf[bufPos + 3] = 255;
                }
            }
        }
    }
    
    public void Draw(Bitmap image, int x, int y, float scaleX, float scaleY, float alpha = 1)
    {
        fixed (byte* buf = _buffer)
        {
            int startX = x < 0 ? -x : 0;
            int startY = y < 0 ? -y : 0;

            int iW = (int) (image.Size.Width * scaleX);
            int iH = (int) (image.Size.Height * scaleY);
            
            int width = iW + x > Size.Width ? Size.Width - x : iW;
            int height = iH + y > Size.Height ? Size.Height - y : iH;

            int multiplier = image.Components == ColorComponents.RedGreenBlueAlpha ? 4 : 3;

            float iScaleX = 1 / scaleX;
            float iScaleY = 1 / scaleY;

            float cX = 0, cY = 0;
            
            for (int pX = startX; pX < width; pX++)
            {
                for (int pY = startY; pY < height; pY++)
                {
                    int iX = (int) (cX + startX * iScaleX);
                    int iY = (int) (cY + startY * iScaleY);

                    int bufPos = (pY * 4 + y * 4) * Size.Width + (pX * 4 + x * 4);
                    int imgPos = iY * multiplier * image.Size.Width + iX * multiplier;

                    float alph = alpha * (image.Components == ColorComponents.RedGreenBlueAlpha ? image.Data[imgPos + 3] / 255f : 1);
                    
                    buf[bufPos] = (byte) (alph * image.Data[imgPos] + (1 - alph) * buf[bufPos]);
                    buf[bufPos + 1] = (byte) (alph * image.Data[imgPos + 1] + (1 - alph) * buf[bufPos + 1]);
                    buf[bufPos + 2] = (byte) (alph * image.Data[imgPos + 2] + (1 - alph) * buf[bufPos + 2]);
                    buf[bufPos + 3] = 255;
                    
                    cY += iScaleY;
                }
                
                cX += iScaleX;
                cY = 0;
            }
        }
    }
}