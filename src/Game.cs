using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SquarePlatformer;

public class Main : Game
{

    public static Main game; 
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    public Main()
    {
        game = this;
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        WindowManager.SetupWindow(Window);
        Renderer.Init(_spriteBatch);
        AssetManager.Init(_spriteBatch);
        ObjectManager.Init();
        PhysicsManager.Init();

        base.Initialize();
    }

    protected override void LoadContent()
    {
        AssetManager.FetchAllContent();
    }

    protected override void Update(GameTime gameTime)
    {

        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();
        ObjectManager.UpdateAll();
        PhysicsManager.UpdateAll();
        LevelManager.Update();

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        Camera.FollowTargets();

        Renderer.DrawAll();

        base.Draw(gameTime);
    }
}
