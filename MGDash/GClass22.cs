namespace MGDash
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
   
    using System;

    public sealed class MenuItem : BaseClassDrawable
    {
        public bool bool_4;
        public bool bool_5;
        private Color color_0;
        private Color color_1;
        private Color color_2;
        private Color color_3;
        private Color color_4;
        private float float_0;
        private float float_1;
        public Menu menu;
        public int int_0;
        public int int_1;
        private int int_2;
        private int int_3;
        private int int_4;
        public int int_5;
        private int int_6;
        public Rectangle rectangle_1;
        private Rectangle rectangle_2;
        private Rectangle[] rectangle_3;
        public string videos;
        public string[] string_1;
        private Vector2 vector2_0;
        private Vector2[] vector2_1;

        public MenuItem(Game game_0, Menu _menu, string _videos, Color color_5, string[] string_3) : base(game_0)
        {
            this.menu = _menu;
            this.videos = _videos;
            this.color_0 = color_5;
            this.string_1 = string_3;
            this.bool_4 = false;
            this.bool_5 = false;
            this.int_1 = 0xff;
            this.int_0 = 0;
            this.int_2 = 0;
            this.int_5 = -1;
            this.background = new Rectangle();
            this.rectangle_2 = new Rectangle(0, 0, 0x1a, 0);
            this.rectangle_1 = new Rectangle();
            this.vector2_0 = new Vector2();
            this.rectangle_3 = new Rectangle[string_3.Length];
            this.vector2_1 = new Vector2[string_3.Length];
            for (int i = 0; i < string_3.Length; i++)
            {
                this.rectangle_3[i] = new Rectangle(0, 0, 0, 100);
                this.vector2_1[i] = new Vector2(84f, 0f);
            }
            this.int_6 = -1;
        }

        public override void CalculateBounds(bool bool_6)
        {
            if (bool_6)
            {
                this.float_0 = ((float) base.DashBoard.width) / 1920f;
            }
            double num = Math.Sin((double) MathHelper.ToRadians(((float) this.int_2) / 2f));
            if (bool_6) {
                this.background.Height = base.DashBoard.heigth;
            }
            this.background.Width = (int) Math.Ceiling((double) (base.DashBoard.width * (0.15 + (0.85 * num))));
            this.background.X = (int) (this.int_5 * (1.0 - num));
            double num2 = ((double) base.DashBoard.width) / ((double) base.DashBoard.heigth);
            Texture2D textured = base.DashBoard.ContentManagement.Images[this.videos + "_menu_bg"];
            this.rectangle_1.Height = (int) Math.Min(((textured.Height * num2) * 9.0) / 16.0, (double) textured.Height);
            this.rectangle_1.Width = (int) Math.Min(((double) (textured.Width * this.background.Width)) / ((double) base.DashBoard.width), (double) textured.Width);
            this.rectangle_1.X = (int) Math.Round((double) (((double) (textured.Width * this.background.X)) / ((double) base.DashBoard.width)));
            this.rectangle_2.X = this.background.X;
            if (bool_6) {
                this.rectangle_2.Height = this.background.Height;
            }
            this.vector2_0.X = this.background.X + 0x10;
            if (this.bool_4)
            {
                this.vector2_0.X = Math.Max(this.vector2_0.X, 78f);
            }
            this.vector2_0.Y = (int) ((((base.DashBoard.heigth * 0.8f) - 8f) + 60f) - ((base.getFont("TitlesShadow").MeasureString(this.videos).Y * this.float_0) / 2f));
            if (this.bool_4)
            {
                this.menu.method_4(bool_6);
            }
            base.Visible = base.DashBoard.rectangle.Intersects(base.background);
            if (this.int_6 == -1)
            {
                for (int j = 0; j < this.string_1.Length; j++)
                {
                    this.int_6 = (int) Math.Max((base.getFont("TextsLight").MeasureString(this.string_1[j]).X * 0.85f) * this.float_0, (float) this.int_6);
                }
            }
            for (int i = 0; i < this.string_1.Length; i++)
            {
                this.rectangle_3[i].Y = (int) (((base.DashBoard.heigth * 0.8f) - 12f) - (((i + 1) * 110) * this.float_0));
                this.rectangle_3[i].Width = this.int_6 + 160;
                this.rectangle_3[i].Height = (int) (100f * this.float_0);
                this.vector2_1[i].Y = (this.rectangle_3[i].Center.Y - (((base.getFont("TextsLight").MeasureString(this.string_1[i]).Y * 0.85f) * this.float_0) / 2f)) + 4f;
            }
            this.float_1 = this.bool_4 ? Depth.float_30 : Depth.float_27;
            if (this.bool_4)
            {
                this.menu.CalculateBounds(bool_6);
            }
        }

        public override void CalculateColors()
        {
            double num = Math.Sin((double) MathHelper.ToRadians((float) this.int_3));
            double num2 = Math.Sin((double) MathHelper.ToRadians((float) this.int_4));
            int r = 100 + ((int) (155.0 * num));
            this.color_0 = Color.FromNonPremultiplied(r, r, r, this.int_1);
            this.color_1 = Color.FromNonPremultiplied(0, 0, 0, this.int_1 / 2);
            this.color_2 = Color.FromNonPremultiplied(0, 0, 0, (int) ((this.int_1 * 0.6f) * num2));
            this.color_3 = Color.FromNonPremultiplied(0x1f, 100, 190, (int) ((this.int_1 * 0.9f) * num2));
            this.color_4 = Color.FromNonPremultiplied(0xff, 0xff, 0xff, (int) (this.int_1 * num2));
        }

        public override void Draw(GameTime gameTime)
        {
            if (((base.spriteBatch != null) && !this.menu.rectangle_2.Contains(base.background)) && base.DashBoard.rectangle.Intersects(base.background))
            {
                base.spriteBatch.Draw(base.DashBoard.ContentManagement.Images[this.videos + "_menu_bg"], base.background, new Rectangle?(this.rectangle_1), this.color_0, 0f, Vector2.Zero, SpriteEffects.None, this.float_1);
                base.spriteBatch.Draw(base.getTexture("right_shadow"), this.rectangle_2, null, this.color_1, 0f, Vector2.Zero, SpriteEffects.None, Depth.float_26);
                base.spriteBatch.DrawString(base.getFont("TitlesShadow"), this.videos, this.vector2_0, this.color_0, 0f, Vector2.Zero, this.float_0, SpriteEffects.None, Depth.float_24);
                for (int i = 0; i < this.string_1.Length; i++)
                {
                    Rectangle? sourceRectangle = null;
                    base.spriteBatch.Draw(base.getTexture("white"), this.rectangle_3[i], sourceRectangle, (i == this.int_0) ? this.color_3 : this.color_2, 0f, Vector2.Zero, SpriteEffects.None, Depth.float_29);
                    base.spriteBatch.DrawString(base.getFont("TextsLight"), this.string_1[i], this.vector2_1[i], this.color_4, 0f, Vector2.Zero, (float) (0.85f * this.float_0), SpriteEffects.None, Depth.float_28);
                }
            }
            base.Draw(gameTime);
        }

        public bool method_4()
        {
            return ((this.int_2 % 180) != 0);
        }

        public bool method_5()
        {
            return (this.int_2 == 180);
        }

        public void method_6(int int_7)
        {
            this.int_0 += int_7;
        }

        public override void Update(GameTime gameTime)
        {
            if (this.bool_5)
            {
                if (this.int_2 < 180)
                {
                    this.int_2 += 4;
                    base.setTitle = true;
                }
            }
            else if (this.int_2 > 0)
            {
                this.int_2 -= 4;
                base.setTitle = true;
            }
            if (this.bool_4)
            {
                if (this.int_3 < 90)
                {
                    this.int_3 += 3;
                    base.bool_1 = true;
                }
            }
            else if (this.int_3 > 0)
            {
                this.int_3 -= 3;
                base.bool_1 = true;
            }
            if (this.bool_5)
            {
                if (this.int_4 < 90)
                {
                    this.int_4 += 3;
                    base.bool_1 = true;
                }
            }
            else if (this.int_4 > 0)
            {
                this.int_4 -= 3;
                base.bool_1 = true;
            }
            base.Update(gameTime);
        }
    }
}

