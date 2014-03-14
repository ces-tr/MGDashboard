namespace MGDash
{
    using System;
    using System.IO;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;


    public sealed class BlurEffect {
        private Effect blurEffect;
        private float float_0;
        private float float_1;
        private float[] float_2;
        private Game game_0;
        private int int_0;
        private Vector2[] vector2_0;
        private Vector2[] vector2_1;

        public BlurEffect(Game game_1) {
            this.game_0 = game_1;
            try {
               //this.blurEffect = game_1.Content.Load<Effect>(@"Content\\Effects\\GaussianBlur.fx");
               BinaryReader Reader = new BinaryReader(File.Open(@"Content\\Effects\\GaussianBlur.mgfxo", FileMode.Open));
               this.blurEffect = new Effect(this.game_0.GraphicsDevice, Reader.ReadBytes((int)Reader.BaseStream.Length)); 
            }
            catch (ContentLoadException) {
                //this.blurEffect = game_1.Content.Load<Effect>("GaussianBlur");
            }
        }

        public void method_0(int int_1, float float_3) {
            this.int_0 = int_1;
            this.float_0 = float_3;
            this.float_2 = null;
            this.float_2 = new float[(this.int_0 * 2) + 1];
            this.float_1 = ((float) this.int_0) / this.float_0;
            float num = (2f * this.float_1) * this.float_1;
            float num2 = (float) Math.Sqrt(num * 3.1415926535897931);
            float num3 = 0f;
            float num4 = 0f;
            int index = 0;
            
            for (int i = -this.int_0; i <= this.int_0; i++) {
                num4 = i * i;
                index = i + this.int_0;
                this.float_2[index] = ((float) Math.Exp((double) (-num4 / num))) / num2;
                num3 += this.float_2[index];
            }
            for (int j = 0; j < this.float_2.Length; j++) {
                this.float_2[j] /= num3;
            }
        }

        public void method_1(float float_3, float float_4) {
            this.vector2_0 = null;
            this.vector2_0 = new Vector2[(this.int_0 * 2) + 1];
            this.vector2_1 = null;
            this.vector2_1 = new Vector2[(this.int_0 * 2) + 1];
            int index = 0;
            float num2 = 1f / float_3;
            float num3 = 1f / float_4;
            for (int i = -this.int_0; i <= this.int_0; i++)
            {
                index = i + this.int_0;
                this.vector2_0[index] = new Vector2(i * num2, 0f);
                this.vector2_1[index] = new Vector2(0f, i * num3);
            }
        }

        public Texture2D createBlur(Texture2D texture2D_0, RenderTarget2D renderTarget2D_0, RenderTarget2D renderTarget2D_1, SpriteBatch spriteBatch_0) {
            
            Texture2D textured = renderTarget2D_0;
            
            if (this.blurEffect == null) {
                throw new InvalidOperationException("GaussianBlur.fx effect not loaded.");
            }
            
            Rectangle destinationRectangle = new Rectangle(0, 0, renderTarget2D_0.Width, renderTarget2D_0.Height);
            Rectangle rectangle2 = new Rectangle(0, 0, renderTarget2D_1.Width, renderTarget2D_1.Height);
            
            this.game_0.GraphicsDevice.SetRenderTarget(renderTarget2D_0);
            this.blurEffect.CurrentTechnique = this.blurEffect.Techniques["GaussianBlur"];
            this.blurEffect.Parameters["weights"].SetValue(this.float_2);
            
            this.blurEffect.Parameters["colorMapTexture"].SetValue(texture2D_0);
            this.blurEffect.Parameters["offsets"].SetValue(this.vector2_0);
            spriteBatch_0.Begin(SpriteSortMode.Deferred, BlendState.Opaque, null, null, null, this.blurEffect);
            spriteBatch_0.Draw(texture2D_0, destinationRectangle, Color.White);
            spriteBatch_0.End();
            this.game_0.GraphicsDevice.SetRenderTarget(renderTarget2D_1);
            
            this.blurEffect.Parameters["colorMapTexture"].SetValue(textured);
            this.blurEffect.Parameters["offsets"].SetValue(this.vector2_1);
            spriteBatch_0.Begin(SpriteSortMode.Deferred, BlendState.Opaque, null, null, null, this.blurEffect);
            spriteBatch_0.Draw(textured, rectangle2, Color.White);
            spriteBatch_0.End();
            this.game_0.GraphicsDevice.SetRenderTarget(null);
            return renderTarget2D_1;
        }

        public float Amount {
            get {
                return this.float_0;
            }
        }

        public float[] Kernel {
            get {
                return this.float_2;
            }
        }

        public int Radius {
            get {
                return this.int_0;
            }
        }

        public float Sigma {
            get {
                return this.float_1;
            }
        }

        public Vector2[] TextureOffsetsX {
            get {
                return this.vector2_0;
            }
        }

        public Vector2[] TextureOffsetsY {
            get {
                return this.vector2_1;
            }
        }
    }
}

