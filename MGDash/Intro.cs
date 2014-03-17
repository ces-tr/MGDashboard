namespace MGDash
{   
    using System;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    
    public sealed class Intro : BaseClassDrawable, IControllerEvents 
    {
        private bool skippedIntro;
        private Color MGlogo_color;
        private Color gamecherlogo_color;
        public readonly int int_0;
        private int MGlogo_duration;
        private int gamecherlogo_duration;
        private int[] int_3;
        private Rectangle rectangle_1;
        private Rectangle[] rectangle_2;
        private Rectangle rectangle_3;
        private Vector2 vector2_0;

        public Intro(Game game_0) : base(game_0)
        {
            this.int_0 = 0xbb8;
            base.DashBoard = (DashBoard) game_0;
            this.MGlogo_duration = 0;
            this.gamecherlogo_duration = 0;
            this.rectangle_2 = new Rectangle[this.int_0];
            this.int_3 = new int[this.int_0];
            this.rectangle_1 = new Rectangle(0, 0, 0x200, 0x200);
            this.skippedIntro = false;
        }

        public override void CalculateBounds(bool bool_5) {
            int minValue = base.DashBoard.width;
            int maxValue = base.DashBoard.heigth;
            if (bool_5)
            {
                this.rectangle_1.X = (minValue / 2) - 0x100;
                this.rectangle_1.Y = (maxValue / 2) - 0x100;
                this.rectangle_3 = new Rectangle(0x10, (base.DashBoard.heigth - 0x20) - 6, 0x20, 0x20);
                this.vector2_0 = new Vector2(56f, (float) ((base.DashBoard.heigth - 0x20) - 12));
                
                for (int i = 0; i < this.int_0; i++)
                {
                    Random random = new Random(Guid.NewGuid().GetHashCode());
                    int num4 = this.method_4(random);
                    int num5 = random.Next((int) (-minValue * 0.25), (int) (minValue * 1.25));
                    int num6 = random.Next(0, maxValue);
                    this.rectangle_2[i].X = num5;
                    this.rectangle_2[i].Y = num6;
                    this.rectangle_2[i].Width = num4 * 2;
                    this.rectangle_2[i].Height = num4 * 2;
                    this.int_3[i] = 0x80 + ((int) (127.0 * (((double) this.rectangle_2[i].Width) / 32.0)));
                }
            }
            else
            {
                for (int j = 0; j < this.int_0; j++)
                {
                    Rectangle rectangle = this.rectangle_2[j];
                    rectangle.Offset(-rectangle.Width / 2, 0);
                    if ((rectangle.X < 0) || (rectangle.Y > maxValue))
                    {
                        Random random2 = new Random(Guid.NewGuid().GetHashCode());
                        int num8 = this.method_4(random2);
                        int num9 = random2.Next(minValue, (int) (minValue * 1.5));
                        int num10 = random2.Next(0, maxValue);
                        rectangle.X = num9;
                        rectangle.Y = num10;
                        rectangle.Width = num8 * 2;
                        rectangle.Height = num8 * 2;
                        this.int_3[j] = 0x80 + ((int) (127.0 * (((double) rectangle.Width) / 32.0)));
                    }
                    this.rectangle_2[j] = rectangle;
                }
            }
        }

        public override void CalculateColors() {

            var logo = (int)(Math.Sin((3.1415926535897931 * this.MGlogo_duration) / 300.0) * 255.0);

            this.MGlogo_color = Color.FromNonPremultiplied(0xff, 0xff, 0xff, (int) logo < 0 ? 0 : logo);
            this.gamecherlogo_color = Color.FromNonPremultiplied(0xff, 0xff, 0xff, (int) (Math.Sin((1.5707963267948966 * this.gamecherlogo_duration) / 300.0) * 255.0));
            
        }

        public override void Update(GameTime gameTime) {

            if (this.MGlogo_duration <= 300) {
                this.MGlogo_duration++;
            }
            else if (this.gamecherlogo_duration <= 300) {
                this.gamecherlogo_duration++;
            }
            else if (!this.skippedIntro) {
                this.skipIntro();
            }
            base.bool_1 = true;
            base.setTitle = true;
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            if (base.spriteBatch != null)
            {
                base.spriteBatch.Draw(base.getTexture("space_bg"), base.DashBoard.rectangle, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, Depth.float_37);
                for (int i = 0; i < this.int_0; i++)
                {
                    Rectangle? sourceRectangle = null;
                    base.spriteBatch.Draw(base.getTexture("space_star"), this.rectangle_2[i], sourceRectangle, Color.FromNonPremultiplied(0xff, 0xff, 0xff, this.int_3[i]), 0f, Vector2.Zero, SpriteEffects.None, Depth.float_36);
                }
                base.spriteBatch.Draw(base.getTexture("MonogameLogo"), this.rectangle_1, null, this.MGlogo_color, 0f, Vector2.Zero, SpriteEffects.None, Depth.float_32);
                base.spriteBatch.Draw(base.getTexture("gamecher"), this.rectangle_1, null, this.gamecherlogo_color, 0f, Vector2.Zero, SpriteEffects.None, Depth.float_32);
                if (this.MGlogo_duration >= 100)
                {
                    Texture2D texture = base.getTexture(base.DashBoard.controllerEvents.GamePadAtached() ? "btn_a" : "key_enter");
                    base.spriteBatch.Draw(texture, this.rectangle_3, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, Depth.float_21);
                    base.spriteBatch.DrawString(base.getFont("TextsShadow"), "skip", this.vector2_0, Color.White, 0f, Vector2.Zero, (float)0.5f, SpriteEffects.None, Depth.float_21);
                }
            }
            base.Draw(gameTime);
        }

        public int GetPriority()
        {
            return Priority.int_11;
        }

        public int method_4(Random random_0)
        {
            return (0x11 - ((int) Math.Log((double) random_0.Next((int) Math.Pow(1.5, 16.0)), 1.5)));
        }

        private void skipIntro()
        {
            this.skippedIntro = true;
            base.DashBoard.initializeComponents.titleBar.AppendControllerIcon("DPADR", "open menu");
            base.DashBoard.initializeComponents.intro_Components.method_6();
            base.DashBoard.initializeComponents.intro_Components.Hide();
        }

        public bool OnControllerAccept()
        {
            if ((this.MGlogo_duration > 100) && !this.skippedIntro)
            {
                this.skipIntro();
            }
            return true;
        }

        public bool OnControllerBack()
        {
            return true;
        }

        public bool OnControllerDetails()
        {
            return true;
        }

        public bool OnControllerDown()
        {
            return true;
        }

        public bool OnControllerLeft()
        {
            return true;
        }

        public bool OnControllerRight()
        {
            return true;
        }

        public bool OnControllerSort()
        {
            return true;
        }

        public bool OnControllerUp()
        {
            return true;
        }


    }
}

