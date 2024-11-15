using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShapeScape;

public class Main : Game
{

    public static Main game; 
    public GraphicsDeviceManager graphics;
    private SpriteBatch _spriteBatch;
    public Main()
    {
        game = this;
        graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        WindowManager.SetupWindow(Window);
        Renderer.Init(_spriteBatch, graphics.GraphicsDevice);
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
        InputManager.Update();
        UIManager.UpdateAll();
        LevelManager.Update();
        ObjectManager.UpdateAll();
        PhysicsManager.UpdateAll();
        

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        Camera.FollowTargets();

        AnimationManager.Update();
        Renderer.DrawAll();

        base.Draw(gameTime);
    }
}
