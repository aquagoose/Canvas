using System;
using System.Drawing;
using Cubic.Scenes;
using Cubic.Windowing;

namespace Canvas.Desktop;

public sealed class CanvasApplication : IDisposable
{
    internal static Image Image;
    private CubicGame _game;
    internal static OnCreateCanvas CreateCanvas;

    public CanvasApplication(Size canvasSize, OnCreateCanvas createCanvas)
    {
        Image = new Image(canvasSize);
        CreateCanvas = createCanvas;
        CreateCanvas.Invoke(Image);

        _game = new CubicGame(new GameSettings()
        {
            Title = "Image",
            Size = canvasSize,
            TargetFps = 0,
            VSync = true
        });
        SceneManager.RegisterScene<Main>("main");
        _game.Run();
    }
    
    public void Dispose() { }

    public delegate void OnCreateCanvas(Image image);
}