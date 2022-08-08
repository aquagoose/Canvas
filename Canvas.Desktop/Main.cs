using System.Numerics;
using Cubic;
using Cubic.Render;
using Cubic.Scenes;

namespace Canvas.Desktop;

public class Main : Scene
{
    private Texture2D _tex;
    
    protected override void Initialize()
    {
        base.Initialize();

        _tex = new Texture2D(Graphics.Viewport.Size.Width, Graphics.Viewport.Size.Height,
            CanvasApplication.Image.Data);
    }

    protected override void Update()
    {
        base.Update();

        if (Input.KeyPressed(Keys.R))
            UpdateCanvas();
    }

    protected override void Draw()
    {
        base.Draw();
        
        Graphics.SpriteRenderer.Begin();
        Graphics.SpriteRenderer.Draw(_tex, Vector2.Zero);
        Graphics.SpriteRenderer.End();
    }

    private void UpdateCanvas()
    {
        CanvasApplication.CreateCanvas.Invoke(CanvasApplication.Image);
        _tex.SetData(CanvasApplication.Image.Data, 0, 0, CanvasApplication.Image.Size.Width,
            CanvasApplication.Image.Size.Height);
    }
}