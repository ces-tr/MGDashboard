using MGDash.Sources.Model;
using Microsoft.Win32;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;

namespace MGDash
{
	public sealed class DashBoard : Game
	{
		public bool bool_0;

		public BaseClassDrawable[] DrawComponent0;

		public BaseClassDrawable[] DrawComponent1;

		public MGDash.ContentManagement ContentManagement;

		public ControllerEvents controllerEvents;

		public MGDash.BlurBackground BlurBackground;

		public Settings settings;

		public InitializeComponents initializeComponents;

		public GraphicsDeviceManager graphicsDeviceManager;

		public int int_0;

		public int width;

		public int heigth;

		public Rectangle rectangle;

		public RenderTarget2D renderTarget2D;

		public RenderTarget2D renderModel;

		public SpriteBatch spriteBatch;

		public List<Components> DashboardComponents;

		public List<Components> ModelComponents;

		public DashBoard(HttpConnect httpconnection)
		{
            base.Content.RootDirectory = "Content";
			this.int_0 = 0;
			this.settings = new Settings(this, ref httpconnection);
			this.ContentManagement = new MGDash.ContentManagement(this);
            base.Components.Add(this.ContentManagement);
			this.setGraphicsDevice();
			this.init();
			this.method_3();
			this.DrawComponent0 = new BaseClassDrawable[0];
			this.DrawComponent1 = new BaseClassDrawable[0];
			this.bool_0 = true;
			CloseMethods.dashBoard = this;
		}

		public DashBoard()
		{
            base.Content.RootDirectory = "Content";
			this.int_0 = 0;
			this.settings = new Settings(this);
			this.ContentManagement = new MGDash.ContentManagement(this);
            base.Components.Add(this.ContentManagement);
			this.setGraphicsDevice();
			this.init();
			this.method_3();
			this.DrawComponent0 = new BaseClassDrawable[0];
			this.DrawComponent1 = new BaseClassDrawable[0];
			this.bool_0 = true;
			CloseMethods.dashBoard = this;
		}

		protected override void Draw(GameTime gameTime)
		{
            base.GraphicsDevice.SetRenderTarget(this.renderTarget2D);
			this.graphicsDeviceManager.GraphicsDevice.Clear(Color.Black);
            this.spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
			int length = (int)this.DrawComponent1.Length;
			for (int i = 0; i < length; i++)
			{
				if (this.DrawComponent1[i].Visible)
				{
					this.DrawComponent1[i].Draw(gameTime);
				}
			}
			this.spriteBatch.End();
			if ((!this.settings.user.blur_effect ? true : !this.controllerEvents.method_1()))
			{
				this.BlurBackground.BluredBackground = this.ContentManagement.Textures["black"];
			}
			else
			{
				this.BlurBackground.setBluredBackground();
				base.GraphicsDevice.SetRenderTarget(this.renderTarget2D);
			}
            this.spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
			int num3 = (int)this.DrawComponent0.Length;
			for (int j = 0; j < num3; j++)
			{
				if (this.DrawComponent0[j].Visible)
				{
					this.DrawComponent0[j].Draw(gameTime);
				}
			}
			this.spriteBatch.End();
			base.GraphicsDevice.SetRenderTarget(null);
            this.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Opaque);
			this.spriteBatch.Draw(this.renderTarget2D, this.rectangle, Color.White);
			this.spriteBatch.End();
		}

		public void init()
		{
			this.initializeComponents = new InitializeComponents(this);
			this.initializeComponents.start();
		}

		protected override void LoadContent()
		{
			this.spriteBatch = new SpriteBatch(base.GraphicsDevice);
			this.BlurBackground = new MGDash.BlurBackground(this, this.graphicsDeviceManager);
            this.renderTarget2D = new RenderTarget2D(base.GraphicsDevice, this.width, this.heigth, false, base.GraphicsDevice.PresentationParameters.BackBufferFormat, DepthFormat.Depth24Stencil8, 4, RenderTargetUsage.PreserveContents);
            this.renderModel = new RenderTarget2D(base.GraphicsDevice, this.width, this.heigth, false, base.GraphicsDevice.PresentationParameters.BackBufferFormat, DepthFormat.Depth24Stencil8, 4, RenderTargetUsage.PreserveContents);
		}

		private void method_0()
		{
			List<BaseClassDrawable> list = new List<BaseClassDrawable>();
			List<BaseClassDrawable> list2 = new List<BaseClassDrawable>();
			foreach (object obj2 in base.Components)
			{
				if (obj2 is BaseClassDrawable)
				{
					BaseClassDrawable item = (BaseClassDrawable)obj2;
					if (!item.bool_3)
					{
						list.Add(item);
					}
					else
					{
						list2.Add(item);
					}
				}
			}
			this.DrawComponent1 = list.ToArray();
			this.DrawComponent0 = list2.ToArray();
		}

		private void method_3()
		{
			this.controllerEvents = new ControllerEvents(this);
			this.initializeComponents.intro_Components.addControllerEvents();
			this.controllerEvents.ginterface1_0 = (IControllerEventsMenu)this.initializeComponents.menu_Components.DrawableComponents["menu"];
		}

		public void method_5(bool bool_1)
		{
			if (!bool_1)
			{
				Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true).DeleteValue("Gamecher", false);
			}
			else
			{
				RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
				string str = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "Turizoft\\Gamecher\\Gamecher.exe");
				key.SetValue("Gamecher", str);
			}
		}

		public void setGraphicsDevice()
		{
			this.graphicsDeviceManager = new GraphicsDeviceManager(this);
			this.graphicsDeviceManager.PreferMultiSampling=true;
			if (!this.settings.user.fullscreen)
			{
				this.width = 1366;
				this.heigth = 768;
			}
			else
			{
				this.width = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
				this.heigth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
			}
			this.rectangle = new Rectangle(0, 0, this.width, this.heigth);
			this.graphicsDeviceManager.PreferredBackBufferWidth=this.width;
			this.graphicsDeviceManager.PreferredBackBufferHeight=this.heigth;
			this.graphicsDeviceManager.IsFullScreen=this.settings.user.fullscreen;
			this.graphicsDeviceManager.ApplyChanges();
			this.setPosition(new Point(0, 0));
		}

		public void setPosition(Point position)
		{
			OpenTK.GameWindow OTKWindow = null;
			FieldInfo field = typeof(OpenTKGameWindow).GetField("window", BindingFlags.Instance | BindingFlags.NonPublic);
			if (field != null)
			{
				OTKWindow = field.GetValue(base.Window) as OpenTK.GameWindow;
			}
			if (OTKWindow != null)
			{
				OTKWindow.X = position.X;
				OTKWindow.Y = position.Y;
			}
		}

		public void toggleFullScreen()
		{
			try
			{
				this.graphicsDeviceManager.ToggleFullScreen();
			}
			catch
			{
			}
		}

		protected override void UnloadContent()
		{
			this.renderTarget2D.Dispose();
			this.renderTarget2D = null;
			this.BlurBackground.clearRenderTargets();
		}

		protected override void Update(GameTime gameTime)
		{
			if (this.bool_0)
			{
				this.method_0();
			}
			this.controllerEvents.method_3(gameTime);
			base.Update(gameTime);
		}
	}
}