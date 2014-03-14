namespace MGDash
{
    using System;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
   
    public sealed class BlurBackground {
        
        private const float float_0 = 2f;
        private BlurEffect gaussianBlur;
        private DashBoard dashBoard;
        private const int int_0 = 6;
        private int[] int_1;
        private RenderTarget2D renderTarget2D_0;
        private RenderTarget2D renderTarget2D_1;
        private SpriteBatch spriteBatch_0;
        public Texture2D BluredBackground;

        public BlurBackground(Game game_0, GraphicsDeviceManager graphicsDeviceManager_0)
        {
            this.dashBoard = (DashBoard) game_0;
            this.gaussianBlur = new BlurEffect(this.dashBoard);
            this.gaussianBlur.method_0(6, 2f);
            this.BluredBackground = new Texture2D(this.dashBoard.GraphicsDevice, this.dashBoard.width, this.dashBoard.heigth, false, this.dashBoard.GraphicsDevice.DisplayMode.Format);
            this.int_1 = new int[this.dashBoard.width * this.dashBoard.heigth];
            int width = this.dashBoard.width / 2;
            int height = this.dashBoard.heigth / 2;
            
            this.renderTarget2D_0 = new RenderTarget2D(this.dashBoard.GraphicsDevice, width, height, false, this.dashBoard.GraphicsDevice.PresentationParameters.BackBufferFormat, DepthFormat.None, 4, RenderTargetUsage.PreserveContents);
            this.renderTarget2D_1 = new RenderTarget2D(this.dashBoard.GraphicsDevice, width, height, false, this.dashBoard.GraphicsDevice.PresentationParameters.BackBufferFormat, DepthFormat.None, 4, RenderTargetUsage.PreserveContents);
            this.gaussianBlur.method_1((float) width, (float) height);
            
            this.spriteBatch_0 = new SpriteBatch(this.dashBoard.GraphicsDevice);
        }

        public void setBluredBackground()
        {
            this.BluredBackground = this.gaussianBlur.createBlur(this.dashBoard.renderTarget2D, this.renderTarget2D_0, this.renderTarget2D_1, this.spriteBatch_0);
        }

        public void clearRenderTargets()
        {
            this.renderTarget2D_0.Dispose();
            this.renderTarget2D_0 = null;
            this.renderTarget2D_1.Dispose();
            this.renderTarget2D_1 = null;
            this.BluredBackground.Dispose();
            this.BluredBackground = null;
        }
    }
}

