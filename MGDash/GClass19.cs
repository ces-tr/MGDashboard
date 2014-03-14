namespace MGDash
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    
    using System;
    using System.Collections.Generic;

    public sealed class Configuration : BaseClassDrawable, IControllerEvents
    {
        public bool bool_4;
        private bool bool_5;
        private bool bool_6;
        private Color color_0;
        private Color color_1;
        private double double_0;
        private OptionsSection gclass12_0;
        public int int_0;
        private int int_1;
        public List<Control> list_0;
        private Rectangle rectangle_1;
        private Rectangle rectangle_2;
        private Rectangle rectangle_3;
        private string string_0;
        private Vector2 vector2_0;

        public Configuration(Game game_0, OptionsSection gclass12_1, string _title, Color color_2) : base(game_0)
        {
            this.list_0 = new List<Control>();
            this.gclass12_0 = gclass12_1;
            this.string_0 = _title;
            this.color_0 = color_2;
            this.bool_4 = false;
            this.background.Width = OptionsSection.int_0;
            base.bool_1 = true;
            this.vector2_0 = new Vector2(0f, 140f);
            this.rectangle_2 = new Rectangle(0, 0, 0, 0x18);
            this.rectangle_1 = new Rectangle(0, 0, 0, 0x18);
            this.rectangle_3 = new Rectangle();
        }

        public override void CalculateBounds(bool bool_7)
        {
            if (bool_7)
            {
                this.background.Height = base.DashBoard.heigth;
            }
            this.vector2_0.X = this.background.X + 60;
            this.rectangle_2.X = this.background.X;
            this.rectangle_2.Width = this.background.Width;
            this.rectangle_2.Y = ((((int) this.vector2_0.Y) + 120) - 0x18) - 5;
            this.rectangle_1.X = this.background.X;
            this.rectangle_1.Width = this.background.Width;
            this.rectangle_1.Y = this.background.Bottom - 0x18;
            this.rectangle_3.X = this.background.X;
            this.rectangle_3.Y = this.background.Y;
            this.rectangle_3.Width = this.background.Width;
            this.rectangle_3.Height = ((((int) this.vector2_0.Y) + 120) - 0x18) - 5;
            for (int i = 0; i < this.list_0.Count; i++)
            {
                this.list_0[i].background.X = this.background.X + 50;
                if (i > 0)
                {
                    this.list_0[i].background.Y = this.list_0[i - 1].background.Bottom + 10;
                }
                else
                {
                    this.list_0[i].background.Y = (int) ((this.vector2_0.Y + 120f) + this.double_0);
                }
                this.list_0[i].background.Width = this.background.Width - 100;
                this.list_0[i].CalculateBounds(bool_7);
            }
        }

        public override void CalculateColors()
        {
            this.color_1 = Color.FromNonPremultiplied(0, 0, 0, (int) (204.0 * Math.Sin((double) MathHelper.ToRadians((float) this.int_1))));
            for (int i = 0; i < this.list_0.Count; i++)
            {
                this.list_0[i].CalculateColors();
            }
        }

        public override void Draw(GameTime gameTime)
        {
            if (base.spriteBatch != null)
            {
                if (base.DashBoard.rectangle.Intersects(base.background))
                {
                    if (!this.bool_4)
                    {
                        base.spriteBatch.Draw(base.getTexture("black"), base.background, null, this.color_1, 0f, Vector2.Zero, SpriteEffects.None, Depth.float_51);
                    }
                    base.spriteBatch.Draw(base.getTexture("top_gradient_white"), this.rectangle_2, null, this.color_0, 0f, Vector2.Zero, SpriteEffects.None, Depth.float_53);
                    base.spriteBatch.Draw(base.getTexture("down_gradient_white"), this.rectangle_1, null, this.color_0, 0f, Vector2.Zero, SpriteEffects.None, Depth.float_53);
                    base.spriteBatch.Draw(base.getTexture("white"), this.rectangle_3, null, this.color_0, 0f, Vector2.Zero, SpriteEffects.None, Depth.float_53);
                    base.spriteBatch.Draw(base.getTexture("white"), base.background, null, this.color_0, 0f, Vector2.Zero, SpriteEffects.None, Depth.float_55);
                    base.spriteBatch.DrawString(base.getFont("Titles"), this.string_0, this.vector2_0, Color.White, 0f, Vector2.Zero, (float)1f, SpriteEffects.None, Depth.float_52);
                }
                for (int i = 0; i < this.list_0.Count; i++)
                {
                    this.list_0[i].Draw(gameTime);
                }
            }
            base.Draw(gameTime);
        }

        public int GetPriority()
        {
            return Priority.int_9;
        }

        public bool method_4()
        {
            if (!this.bool_5)
            {
                return this.bool_6;
            }
            return true;
        }

        public bool OnControllerAccept()
        {
            if (this.list_0.Count > 0)
            {
                this.list_0[this.int_0].OnControllerAccept();
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
            if (((this.list_0.Count > 0) && (this.int_0 < (this.list_0.Count - 1))) && !this.method_4())
            {
                this.list_0[this.int_0].bool_4 = false;
                this.int_0++;
                this.list_0[this.int_0].bool_4 = true;
                this.bool_6 = true;
            }
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
            if (((this.list_0.Count > 0) && (this.int_0 > 0)) && !this.method_4())
            {
                this.list_0[this.int_0].bool_4 = false;
                this.int_0--;
                this.list_0[this.int_0].bool_4 = true;
                this.bool_5 = true;
            }
            return true;
        }

        public override void Show()
        {
            base.Show();
        }

        public override void Update(GameTime gameTime)
        {
            if (!this.bool_4)
            {
                if (this.int_1 < 90)
                {
                    this.int_1 += 5;
                    base.bool_1 = true;
                }
                if (this.list_0.Count > 0)
                {
                    this.list_0[this.int_0].bool_4 = false;
                }
            }
            else
            {
                if (this.int_1 > 0)
                {
                    this.int_1 -= 5;
                    base.bool_1 = true;
                }
                if (this.list_0.Count > 0)
                {
                    this.list_0[this.int_0].bool_4 = true;
                }
            }
            this.bool_6 = this.bool_6 && (this.list_0[this.int_0].background.Bottom > (base.DashBoard.heigth - 0x18));
            this.bool_5 = this.bool_5 && (this.list_0[this.int_0].background.Y < (this.vector2_0.Y + 120f));
            if (this.bool_6)
            {
                this.double_0 -= Math.Min(20, (this.list_0[this.int_0].background.Bottom - base.DashBoard.heigth) + 0x18);
                base.setTitle = true;
            }
            if (this.bool_5)
            {
                this.double_0 += Math.Min((float) 20f, (float) ((this.vector2_0.Y + 120f) - this.list_0[this.int_0].background.Y));
                base.setTitle = true;
            }
            for (int i = 0; i < this.list_0.Count; i++)
            {
                this.list_0[i].Update(gameTime);
            }
            base.Update(gameTime);
        }
    }
}

