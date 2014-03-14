namespace MGDash
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    
    using System;
    using System.Collections.Generic;

    public sealed class CategorySlide : BaseClassDrawable
    {
        public bool bool_4;
        public bool bool_5;
        public int CatListCount;
        public int int_1;
        public int int_2;
        public int int_3;
        public int int_4;
        public List<int> list_0;
        private Rectangle rectangle_1;
        private Rectangle rectangle_2;
        private Rectangle rectangle_3;
        private Rectangle rectangle_4;
        private Vector2 vector2_0;

        public CategorySlide(Game game_0) : base(game_0)
        {
            this.Init();
        }

        public override void CalculateBounds(bool bool_6)
        {
            this.rectangle_1.X = (base.DashBoard.width - this.int_1) + 20;
            this.rectangle_2.X = ((base.DashBoard.width / 4) - (this.int_1 / 4)) + 5;
            this.rectangle_3.X = (base.DashBoard.width - this.int_1) + 20;
            this.rectangle_4.X = this.rectangle_3.X - 20;
            this.rectangle_4.Y = this.rectangle_3.Y;
            if (bool_6)
            {
                this.rectangle_1.Height = base.DashBoard.heigth;
                this.rectangle_2.Height = base.DashBoard.heigth / 4;
                this.rectangle_3.Y = (base.DashBoard.heigth / 2) - 40;
            }
        }

        public override void Draw(GameTime gameTime)
        {
            if ((base.spriteBatch != null) && (this.int_1 >= 0))
            {
                base.spriteBatch.Draw(base.DashBoard.BlurBackground.BluredBackground, this.rectangle_1, new Rectangle?(this.rectangle_2), Color.AliceBlue, 0f, Vector2.Zero, SpriteEffects.None, Depth.float_40);
                base.spriteBatch.Draw(base.getTexture("white"), this.rectangle_3, null, Color.FromNonPremultiplied(0x1f, 160, 200, 230), 0f, Vector2.Zero, SpriteEffects.None, Depth.float_39);
                base.spriteBatch.Draw(base.getTexture("breadcum"), this.rectangle_4, null, Color.FromNonPremultiplied(0x1f, 160, 200, 230), 0f, Vector2.Zero, SpriteEffects.FlipHorizontally, Depth.float_39);
                for (int i = Math.Max(0, this.CatListCount - 6); i < Math.Min(this.list_0.Count, this.CatListCount + 7); i++)
                {
                    string text = (this.list_0[i] == -1) ? "any" : base.DashBoard.settings.Categories[this.list_0[i]].display_name;
                    Color color = Color.FromNonPremultiplied(0xff, 0xff, 0xff, Math.Max(0, 240 - (Math.Abs((int) (i - this.CatListCount)) * 40)));
                    int num2 = (((int) (base.DashBoard.heigth * 0.5f)) + this.int_2) + ((i - this.CatListCount) * 90);
                    this.vector2_0.X = (((base.DashBoard.width + 160) - this.int_1) + 20) - ((base.getFont("TextsLight").MeasureString(text).X * 0.8f) / 2f);
                    this.vector2_0.Y = num2 - ((base.getFont("TextsLight").MeasureString(text).Y * 0.8f) / 2f);
                    base.spriteBatch.DrawString(base.getFont("TextsLight"), text, this.vector2_0, color, 0f, Vector2.Zero, (float) 0.8f, SpriteEffects.None, Depth.float_38);
                }
            }
        }

        public void Init()
        {
            this.CatListCount = 0;
            this.int_2 = 0;
            this.int_1 = 0;
            this.int_3 = 0;
            this.int_4 = 0;
            base.bool_3 = true;
            this.vector2_0 = new Vector2();
            this.rectangle_1 = new Rectangle(0, 0, 320, 0);
            this.rectangle_2 = new Rectangle(0, 0, 80, 0);
            this.rectangle_3 = new Rectangle(0, 0, 320, 80);
            this.rectangle_4 = new Rectangle(0, 0, 20, 80);
        }

        public bool method_5()
        {
            if (!this.bool_4)
            {
                //Console.WriteLine(this.bool_5);
                return this.bool_5;
            }
            return true;
        }

        public override void Update(GameTime gameTime)
        {
            if (this.int_3 > 0)
            {
                this.int_3--;
                if (this.int_4 < 90)
                {
                    this.int_4 += 5;
                    this.int_1 = (int) (340.0 * Math.Sin((double) MathHelper.ToRadians((float) this.int_4)));
                }
            }
            else if ((this.int_3 == 0) && (this.int_4 > 0))
            {
                this.int_4 -= 5;
                this.int_1 = (int) (340.0 * Math.Sin((double) MathHelper.ToRadians((float) this.int_4)));
            }
            if (this.int_1 >= 0)
            {
                base.setTitle = true;
            }
            if (this.int_4 == 90)
            {
                if (this.bool_4)
                {
                    this.int_2 += 9;
                    if (this.int_2 == 90)
                    {
                        this.bool_4 = false;
                        this.int_2 = 0;
                        this.CatListCount--;
                        base.setTitle = true;
                    }
                }
                if (this.bool_5)
                {
                    this.int_2 -= 9;
                    if (this.int_2 == -90)
                    {
                        this.bool_5 = false;
                        this.int_2 = 0;
                        this.CatListCount++;
                        base.setTitle = true;
                    }
                }
            }
            base.Update(gameTime);
        }
    }
}

