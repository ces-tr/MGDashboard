namespace MGDash
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    
    using System;

    public sealed class Rate : DialogScreen
    {
        private Color color_3;
        private Color color_4;
        private Color color_5;
        public Func<bool> func_0;
        public int rating;
        private Rectangle[] rectangle_1;

        public Rate(Game game_0) : base(game_0)
        {
            this.rectangle_1 = new Rectangle[5];
            for (int i = 0; i < 5; i++)
            {
                this.rectangle_1[i] = new Rectangle(0, 0, 80, 80);
            }
            base.bool_5 = true;
            this.vector2_1 = new Vector2();
            this.vector2_0 = new Vector2();
        }

        public override void CalculateBounds(bool bool_6)
        {
            if (bool_6)
            {
                this.vector2_0.X = (int) ((((float) base.DashBoard.width) / 2f) - (base.getFont("TextsLight").MeasureString(base.Title).X / 2f));
                this.vector2_0.Y = (int) (((base.DashBoard.heigth * 0.5f) - 120f) - base.getFont("TextsLight").MeasureString(base.Title).Y);
                this.vector2_1.X = (int) ((((float) base.DashBoard.width) / 2f) - ((base.getFont("TextsLight").MeasureString(base.Text).X * 0.9f) / 2f));
                this.vector2_1.Y = (int) (((base.DashBoard.heigth * 0.5f) - 40f) - (base.getFont("TextsLight").MeasureString(base.Text).Y * 0.9f));
                for (int i = 0; i < 5; i++)
                {
                    this.rectangle_1[i].X = (int) ((base.DashBoard.width * 0.5f) + ((10 + this.rectangle_1[i].Width) * (i - 2.5)));
                    this.rectangle_1[i].Y = ((int) (base.DashBoard.heigth * 0.6f)) - (this.rectangle_1[i].Height / 2);
                }
            }
            base.CalculateBounds(bool_6);
        }

        public override void CalculateColors()
        {
            this.color_3 = Color.FromNonPremultiplied(0xff, 0xff, 0xff, base.int_2);
            this.color_4 = Color.FromNonPremultiplied(140, 140, 140, base.int_2);
            this.color_5 = Color.FromNonPremultiplied(0x2d, 0x2d, 0x2d, base.int_2);
            base.CalculateColors();
        }

        public override void Draw(GameTime gameTime)
        {
            for (int i = 0; i < 5; i++)
            {
                Rectangle? sourceRectangle = null;
                base.spriteBatch.Draw((i < this.rating) ? base.getTexture("star_white_big") : base.getTexture("star_gray_big"), this.rectangle_1[i], sourceRectangle, this.color_3, 0f, Vector2.Zero, SpriteEffects.None, Depth.float_7);
            }
            base.Draw(gameTime);
        }

        public override int GetPriority()
        {
            return Priority.int_1;
        }

        public override bool OnControllerAccept()
        {
            if (this.func_0 != null)
            {
                this.func_0();
                base.cancel = true;
            }
            return true;
        }

        public override bool OnControllerBack()
        {
            base.cancel = true;
            return true;
        }

        public override bool OnControllerDetails()
        {
            return true;
        }

        public override bool OnControllerDown()
        {
            return true;
        }

        public override bool OnControllerLeft()
        {
            if (this.rating > 0)
            {
                this.rating--;
            }
            return true;
        }

        public override bool OnControllerRight()
        {
            if (this.rating < 5)
            {
                this.rating++;
            }
            return true;
        }

        public override bool OnControllerSort()
        {
            return true;
        }

        public override bool OnControllerUp()
        {
            return true;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}

