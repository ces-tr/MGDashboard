namespace MGDash
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    
    using System;
    using System.Collections.Generic;

    public sealed class GameDetails : BaseClassDrawable, IControllerEvents
    {
        private bool bool_4;
        private bool bool_5;
        private bool bool_6;
        private bool bool_7;
        private bool bool_8;
        private bool bool_9;
        private Color color_0;
        private Color color_1;
        private Color color_2;
        private Dictionary<string, string> Data;
        private float float_0;
        private Box Box;
        private int int_0;
        private int int_1;
        private int int_2;
        private int int_3;
        private int int_4;
        private int int_5;
        private int int_6;
        private int int_7;
        private int int_8;
        private Rectangle rectangle_1;
        private Rectangle rectangle_2;
        private Rectangle rectangle_3;
        private Rectangle rectangle_4;
        private Rectangle rectangle_5;
        private Rectangle rectangle_6;
        private Rectangle rectangle_7;
        private Rectangle rectangle_8;
        private Rectangle[] rectangle_9;
        public string name;
        public string cat;
        public string description;
        private Vector2 vector2_0;
        private Vector2 vector2_1;
        private Vector2 vector2_2;
        private Vector2[] vector2_3;
        private Vector2[] vector2_4;

        public GameDetails(Game game_0) : base(game_0)
        {
            this.int_0 = 0;
            this.Box = null;
            base.bool_2 = false;
            base.bool_3 = true;
            this.vector2_0 = new Vector2(40f, 10f);
            this.vector2_1 = new Vector2(40f, 70f);
            this.Data = new Dictionary<string, string>();
            this.float_0 = 0f;
            this.rectangle_1 = new Rectangle(0, 0, 0, 170);
            this.rectangle_2 = new Rectangle(0, 0, 0, 0x2a);
            this.rectangle_3 = new Rectangle(0, 170, 0, 0);
            this.rectangle_4 = new Rectangle(0, 0x2a, 0, 0);
            this.rectangle_5 = new Rectangle(0, 0, 0, 140);
            this.rectangle_8 = new Rectangle();
            this.vector2_2 = new Vector2();
            this.rectangle_6 = new Rectangle(0, 170, 320, 0);
            this.rectangle_7 = new Rectangle(0, 0, 50, 0x4d);
            this.rectangle_9 = new Rectangle[5];
            for (int i = 0; i < 5; i++)
            {
                this.rectangle_9[i] = new Rectangle(0, 0, 0x30, 0x30);
            }
        }

        public override void CalculateBounds(bool bool_10)
        {
            if (bool_10)
            {
                this.rectangle_8.Width = base.DashBoard.width - 80;
                this.rectangle_8.Height = base.DashBoard.heigth - 170;
                this.description = base.getWrappedText(base.getFont("TextsLight"), 0.735f, this.description, (float) this.rectangle_8.Width);
                this.float_0 = base.getTextMeasure(base.getFont("TextsLight"), 0.7f, this.description + "\n");
                this.int_5 = Math.Max(100 * ((int) (((double) (this.float_0 - this.rectangle_8.Height)) / 300.0)), 100);
                this.rectangle_3.Width = base.DashBoard.width;
                this.rectangle_4.Width = base.DashBoard.width / 4;
                this.rectangle_3.Height = base.DashBoard.heigth - 170;
                this.rectangle_4.Height = (base.DashBoard.heigth - 170) / 4;
                this.rectangle_1.Width = base.DashBoard.width;
                this.rectangle_2.Width = base.DashBoard.width / 4;
                this.rectangle_5.Y = base.DashBoard.heigth - 140;
                this.rectangle_5.Width = base.DashBoard.width;
            }
            this.int_1 = (int) Math.Round((double) (Math.Sin((double) MathHelper.ToRadians((float) this.int_3)) * (this.int_8 - 40)));
            this.int_2 = (int) ((((this.float_0 - this.rectangle_8.Height) * ((this.int_4 / 100) + (((double) (this.int_4 % 100)) / 100.0))) * 100.0) / ((double) this.int_5));
            for (int i = 0; i < this.Data.Count; i++)
            {
                this.vector2_3[i].X = ((40 + this.rectangle_6.Width) + 80) - this.int_1;
                this.vector2_3[i].Y = 170 + (i * 60);
                this.vector2_4[i].X = (this.vector2_3[i].X + (this.int_7 * 0.7f)) + 20f;
                this.vector2_4[i].Y = this.vector2_3[i].Y;
            }
            this.rectangle_8.X = this.int_8 - this.int_1;
            this.rectangle_8.Y = 170 - this.int_2;
            this.vector2_2.X = this.rectangle_8.X;
            this.vector2_2.Y = this.rectangle_8.Y;
            if (this.Box.Cover != null)
            {
                this.rectangle_6.X = 40 - this.int_1;
                this.rectangle_6.Height = (int) ((((float) this.rectangle_6.Width) / ((float) this.Box.Cover.Width)) * this.Box.Cover.Height);
            }
            for (int j = 0; j < 5; j++)
            {
                this.rectangle_9[j].X = (0x26 + (j * 0x34)) - this.int_1;
                this.rectangle_9[j].Y = this.rectangle_6.Bottom + 20;
            }
            this.rectangle_7.X = this.rectangle_6.X + 10;
            this.rectangle_7.Y = this.rectangle_6.Y - 7;
        }

        public override void CalculateColors()
        {
            this.color_0 = Color.FromNonPremultiplied(0xff, 0xff, 0xff, this.int_0);
            this.color_1 = Color.FromNonPremultiplied(160, 160, 160, this.int_0);
            this.color_2 = Color.FromNonPremultiplied(30, 30, 30, this.int_0);
        }

        public override void Draw(GameTime gameTime)
        {
            if (base.spriteBatch != null)
            {
                base.spriteBatch.Draw(base.DashBoard.BlurBackground.BluredBackground, this.rectangle_3, new Rectangle?(this.rectangle_4), this.color_2, 0f, Vector2.Zero, SpriteEffects.None, Depth.float_17);
                base.spriteBatch.Draw(base.DashBoard.BlurBackground.BluredBackground, this.rectangle_1, new Rectangle?(this.rectangle_2), this.color_2, 0f, Vector2.Zero, SpriteEffects.None, Depth.float_14);
                base.spriteBatch.Draw(base.getTexture("top_gradient"), this.rectangle_1, null, this.color_0, 0f, Vector2.Zero, SpriteEffects.None, Depth.float_13);
                base.spriteBatch.Draw(base.getTexture("down_gradient"), this.rectangle_5, null, this.color_0, 0f, Vector2.Zero, SpriteEffects.None, Depth.float_14);
                base.spriteBatch.DrawString(base.getFont("Texts"), this.name, this.vector2_0, this.color_0, 0f, Vector2.Zero, (float) 0.85f, SpriteEffects.None, Depth.float_12);
                base.spriteBatch.DrawString(base.getFont("TextsLight"), this.cat, this.vector2_1, this.color_1, 0f, Vector2.Zero, (float) 0.75f, SpriteEffects.None, Depth.float_12);
                if (this.Box.Cover != null)
                {
                    base.spriteBatch.Draw(this.Box.Cover, this.rectangle_6, null, this.color_0, 0f, Vector2.Zero, SpriteEffects.None, Depth.float_16);
                    if (this.Box.VideoGame.favorite)
                    {
                        base.spriteBatch.Draw(base.getTexture("favorite"), this.rectangle_7, null, this.color_0, 0f, Vector2.Zero, SpriteEffects.None, Depth.float_15);
                    }
                }
                for (int i = 0; i < 5; i++)
                {
                    Rectangle? sourceRectangle = null;
                    base.spriteBatch.Draw((i < this.Box.VideoGame.rating) ? base.getTexture("star_white") : base.getTexture("star_gray"), this.rectangle_9[i], sourceRectangle, this.color_0, 0f, Vector2.Zero, SpriteEffects.None, Depth.float_15);
                }
                int index = 0;
                foreach (KeyValuePair<string, string> pair in this.Data)
                {
                    base.spriteBatch.DrawString(base.getFont("TextsLight"), pair.Key, this.vector2_3[index], this.color_0, 0f, Vector2.Zero, (float) 0.7f, SpriteEffects.None, Depth.float_15);
                    base.spriteBatch.DrawString(base.getFont("TextsLight"), pair.Value, this.vector2_4[index], this.color_1, 0f, Vector2.Zero, (float) 0.7f, SpriteEffects.None, Depth.float_15);
                    index++;
                }
                base.spriteBatch.DrawString(base.getFont("TextsLight"), this.description, this.vector2_2, this.color_0, 0f, Vector2.Zero, (float) 0.7f, SpriteEffects.None, Depth.float_15);
            }
            base.Draw(gameTime);
        }

        public int GetPriority()
        {
            return Priority.int_3;
        }

        public override void Hide()
        {
            this.bool_4 = false;
            base.Hide();
        }

        public void Set(Box box)
        {
            this.Box = box;
            if ((box.VideoGame.description == null) || (box.VideoGame.description.Trim().Length == 0))
            {
                box.VideoGame.description = "No description available";
            }
            this.int_1 = 0;
            this.int_2 = 0;
            this.int_3 = 0;
            this.int_4 = 0;
            this.bool_5 = false;
            this.bool_6 = false;
            this.bool_7 = false;
            this.bool_8 = false;
            this.bool_9 = false;
            this.description = box.VideoGame.description;
            this.name = box.VideoGame.name;
            this.cat = box.VideoGame.category("·");
            this.Data.Clear();
            this.int_7 = 0;
            this.int_6 = 0;
            if (box.VideoGame.developer != null)
            {
                this.Data.Add("Developer", box.VideoGame.developer);
                this.int_7 = (int) Math.Max((float) this.int_7, base.getFont("Texts").MeasureString("Developer").X);
                this.int_6 = (int) Math.Max((float) this.int_6, base.getFont("TextsLight").MeasureString(box.VideoGame.developer).X);
            }
            if (box.VideoGame.publisher != null)
            {
                this.Data.Add("Publisher", box.VideoGame.publisher);
                this.int_7 = (int)Math.Max((float)this.int_7, base.getFont("Texts").MeasureString("Publisher").X);
                this.int_6 = (int) Math.Max((float) this.int_6, base.getFont("TextsLight").MeasureString(box.VideoGame.publisher).X);
            }
            if (box.VideoGame.release_date != null)
            {
                this.Data.Add("Release date", box.VideoGame.release_date);
                this.int_7 = (int) Math.Max((float) this.int_7, base.getFont("Texts").MeasureString("Release date").X);
                this.int_6 = (int) Math.Max((float) this.int_6, base.getFont("TextsLight").MeasureString(box.VideoGame.release_date).X);
            }
            if (box.VideoGame.esrb != null)
            {
                this.Data.Add("ESRB", box.VideoGame.esrb);
                this.int_7 = (int) Math.Max((float) this.int_7, base.getFont("Texts").MeasureString("ESRB").X);
                this.int_6 = (int) Math.Max((float) this.int_6, base.getFont("TextsLight").MeasureString(box.VideoGame.esrb).X);
            }
            if (box.VideoGame.coop != null)
            {
                this.Data.Add("Coop", box.VideoGame.coop);
                this.int_7 = (int) Math.Max((float) this.int_7, base.getFont("Texts").MeasureString("Coop").X);
                this.int_6 = (int) Math.Max((float) this.int_6, base.getFont("TextsLight").MeasureString(box.VideoGame.coop).X);
            }
            if (box.VideoGame.players != null)
            {
                this.Data.Add("Players", box.VideoGame.players);
                this.int_7 = (int) Math.Max((float) this.int_7, base.getFont("Texts").MeasureString("Players").X);
                this.int_6 = (int) Math.Max((float) this.int_6, base.getFont("TextsLight").MeasureString(box.VideoGame.players).X);
            }
            this.int_8 = (int) (((((40 + this.rectangle_6.Width) + 80) + 20) + ((this.int_7 + this.int_6) * 0.7f)) + 160f);
            this.vector2_4 = new Vector2[this.Data.Count];
            this.vector2_3 = new Vector2[this.Data.Count];
        }

        public bool method_5()
        {
            if ((!this.bool_5 && !this.bool_6) && !this.bool_7)
            {
                return this.bool_8;
            }
            return true;
        }

        public bool method_6()
        {
            if (!this.bool_7)
            {
                return this.bool_8;
            }
            return true;
        }

        public bool method_7()
        {
            return (this.float_0 > this.rectangle_8.Height);
        }

        public bool OnControllerAccept()
        {
            return true;
        }

        public bool OnControllerBack()
        {
            base.DashBoard.controllerEvents.method_6(this);
            base.DashBoard.controllerEvents.method_8();
            this.bool_4 = true;
            return true;
        }

        public bool OnControllerDetails()
        {
            return true;
        }

        public bool OnControllerDown()
        {
            if ((!this.method_6() && this.bool_9) && this.method_7())
            {
                this.bool_8 = true;
            }
            return true;
        }

        public bool OnControllerLeft()
        {
            if (!this.method_5() && this.bool_9)
            {
                this.bool_5 = true;
            }
            return true;
        }

        public bool OnControllerRight()
        {
            if (!this.method_5() && !this.bool_9)
            {
                this.bool_6 = true;
            }
            return true;
        }

        public bool OnControllerSort()
        {
            return true;
        }

        public bool OnControllerUp()
        {
            if ((!this.method_6() && this.bool_9) && this.method_7())
            {
                this.bool_7 = true;
            }
            return true;
        }

        public override void Show()
        {
            if (!this.bool_4)
            {
                this.int_0 = 0;
                this.bool_4 = false;
                base.DashBoard.controllerEvents.addControllerEvent(this);
                base.DashBoard.controllerEvents.method_7(this);
                base.Show();
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (!this.bool_4)
            {
                if (this.int_0 < 0xff)
                {
                    this.int_0 = Math.Min(0xff, this.int_0 + 10);
                    base.bool_1 = true;
                }
            }
            else if (this.int_0 > 0)
            {
                this.int_0 = Math.Max(0, this.int_0 - 10);
                base.bool_1 = true;
                if (this.int_0 == 0)
                {
                    this.Hide();
                }
            }
            if (this.bool_6)
            {
                this.int_3 += 3;
                this.bool_6 = this.int_3 < 90;
                if (!this.bool_6)
                {
                    this.bool_9 = true;
                }
                base.setTitle = true;
            }
            if (this.bool_5)
            {
                this.int_3 -= 3;
                this.bool_5 = this.int_3 > 0;
                if (!this.bool_5)
                {
                    this.bool_9 = false;
                }
                base.setTitle = true;
            }
            if (this.method_7())
            {
                if (this.bool_7)
                {
                    if (this.int_4 > 0)
                    {
                        this.int_4 -= 5;
                    }
                    this.bool_7 = (this.int_4 % 100) != 0;
                    base.setTitle = true;
                }
                if (this.bool_8)
                {
                    if (this.int_4 < this.int_5)
                    {
                        this.int_4 += 5;
                    }
                    this.bool_8 = (this.int_4 % 100) != 0;
                    base.setTitle = true;
                }
                if (!this.bool_9 && (this.int_4 > 0))
                {
                    this.int_4 = Math.Max(0, this.int_4 - 10);
                    base.setTitle = true;
                }
            }
            else
            {
                this.int_2 = 0;
            }
            base.Update(gameTime);
        }
    }
}

