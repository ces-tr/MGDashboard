namespace MGDash
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
   
    using System;

    public sealed class BoxOptionMenu : BaseClassDrawable, IControllerEvents
    {
        private Color color_0;
        private Color color_1;
        private Color color_2;
        private Color color_3;
        private Color color_4;
        private Color color_5;
        public float float_0;
        private Func<bool>[] bindfunc_CoverOptions;
        private Box Box;
        public int int_0;
        private int int_1;
        private Rectangle rectangle_1;
        private Rectangle[] rectangle_2;
        private string[] string_0;
        private Vector2[] vector2_0;

        public BoxOptionMenu(Game game_0, Box _box) : base(game_0)
        {
            this.Box = _box;
            this.background = new Rectangle();
            this.rectangle_1 = new Rectangle(0, 0, 12, 14);
            this.float_0 = 0f;
            this.setOptionMenu();
        }

        public override void CalculateBounds(bool bool_4)
        {
            this.background.X = this.Box.background.X ;
            this.background.Y = (this.Box.background.Bottom - (this.string_0.Length * 0x30)) - 14;
            this.background.Width = this.Box.background.Width;
            this.background.Height = this.string_0.Length * 0x30;
            this.rectangle_1.X = this.background.Center.X - (base.getTexture("tooltip_arrow").Width / 2);
            this.rectangle_1.Y = this.background.Bottom;
            for (int i = 0; i < this.string_0.Length; i++)
            {
                Vector2 vector = base.getFont("TextsLight").MeasureString(this.string_0[i]);
                this.vector2_0[i].X = this.background.Center.X - ((vector.X * 0.6f) / 2f);
                this.vector2_0[i].Y = ((this.background.Bottom - ((i + 1) * 0x30)) + 0x18) - ((vector.Y * 0.6f) / 2f);
            }
            for (int j = 0; j < this.string_0.Length; j++)
            {
                this.rectangle_2[j].X = this.background.X;
                this.rectangle_2[j].Y = this.background.Bottom - ((j + 1) * 0x30);
                this.rectangle_2[j].Width = this.background.Width;
                this.rectangle_2[j].Height = 0x30;
            }
        }

        public override void CalculateColors()
        {
            this.float_0 = (float) Math.Sin(((((double) this.int_0) / 100.0) * 3.1415926535897931) / 2.0);
            this.color_0 = Color.FromNonPremultiplied(0x1f, 160, 200, (int) (225f * this.float_0));
            this.color_1 = Color.FromNonPremultiplied(0, 0, 0, (int) (180f * this.float_0));
            this.color_2 = (this.int_1 == 0) ? Color.FromNonPremultiplied(20, 0x35, 0x40, (int) (248f * this.float_0)) : this.color_0;
            this.color_3 = Color.FromNonPremultiplied(0, 0, 0, (int) (18f * this.float_0));
            this.color_4 = Color.FromNonPremultiplied(0xff, 0xff, 0xff, (int) (255f * this.float_0));
            this.color_5 = Color.FromNonPremultiplied(160, 160, 160, (int) (255f * this.float_0));
        }

        public override void Draw(GameTime gameTime)
        {
            if (base.spriteBatch != null)
            {
                base.spriteBatch.Draw(base.getTexture("white"), base.background, null, this.color_0, 0f, Vector2.Zero, SpriteEffects.None, Depth.float_45);
                base.spriteBatch.Draw(base.getTexture("tooltip_arrow"), this.rectangle_1, null, this.color_2, 0f, Vector2.Zero, SpriteEffects.None, Depth.float_19);
                for (int i = 0; i < this.string_0.Length; i++)
                {
                    Color color = ((i != 0) || !this.Box.coverExists) ? this.color_4 : this.color_5;
                    base.spriteBatch.DrawString(base.getFont("TextsLight"), this.string_0[i], this.vector2_0[i], color, 0f, Vector2.Zero, (float) 0.6f, SpriteEffects.None, Depth.float_43);
                    if (i != this.int_1)
                    {
                        Rectangle? sourceRectangle = null;
                        base.spriteBatch.Draw(base.getTexture("top_gradient"), this.rectangle_2[i], sourceRectangle, this.color_3, 0f, Vector2.Zero, SpriteEffects.None, Depth.float_44);
                    }
                    else
                    {
                        Rectangle? nullable4 = null;
                        base.spriteBatch.Draw(base.getTexture("white"), this.rectangle_2[i], nullable4, this.color_1, 0f, Vector2.Zero, SpriteEffects.None, Depth.float_44);
                    }
                }
            }
            else
            {
                base.spriteBatch = this.Box.spriteBatch;
            }
            base.Draw(gameTime);
        }

        public int GetPriority()
        {
            return Priority.int_7;
        }

        public override void Hide()
        {
            this.Box.bool_6 = false;
            base.DashBoard.controllerEvents.method_6(this);
            base.DashBoard.controllerEvents.method_8();
        }

        public bool unlinkGame()
        {
            this.Box.VideoGame.unlink();
            this.Box.bool_1 = true;
            return true;
        }

        public bool favorite()
        {
            this.Box.VideoGame.setfavorite(true);
            return true;
        }

        public bool unfavorite()
        {
            this.Box.VideoGame.setfavorite(false);
            return true;
        }

        public bool method_13()
        {
            this.Box.GameDetails.Set(this.Box);
            this.Box.GameDetails.Show();
            return true;
        }

        public bool method_4() {
            return (this.int_0 > 0);
        }

        public void setOptionMenu()
        {
            this.string_0 = new string[] { "edit box art", "set rating", this.Box.VideoGame.favorite ? "unfavorite" : "favorite", this.Box.VideoGame.linked ? "unlink game" : "link game", "view details" };
            this.bindfunc_CoverOptions = new Func<bool>[5];
            this.bindfunc_CoverOptions[0] = new Func<bool>(this.editCover);
            this.bindfunc_CoverOptions[1] = new Func<bool>(this.setRating);

            this.bindfunc_CoverOptions[2] = (this.Box.VideoGame.favorite) ? new Func<bool>(this.unfavorite) : this.bindfunc_CoverOptions[2] = new Func<bool>(this.favorite);
            
            
            if (this.Box.VideoGame.linked)
            {
                this.bindfunc_CoverOptions[3] = new Func<bool>(this.unlinkGame);
            }
            else
            {
                this.bindfunc_CoverOptions[3] = new Func<bool>(this.Box.GamesSection.linkGame);
            }
            this.bindfunc_CoverOptions[4] = new Func<bool>(this.method_13);
            this.vector2_0 = new Vector2[this.string_0.Length];
            this.rectangle_2 = new Rectangle[this.string_0.Length];
            base.setTitle = true;
        }

        public bool editCover()
        {
            if (!this.Box.coverExists)
            {
                this.Box.GamesSection.MsgChangeCover.Title = this.Box.VideoGame.name;
                this.Box.GamesSection.MsgChangeCover.Show();
            }
            return true;
        }

        public bool setRating()
        {
            this.Box.GamesSection.Coverflow.Rate.rating = this.Box.VideoGame.rating;
            this.Box.GamesSection.Coverflow.Rate.Title = this.Box.VideoGame.name;
            this.Box.GamesSection.Coverflow.Rate.Text = "Use left and right arrows to set rating";
            this.Box.GamesSection.Coverflow.Rate.Show();
            this.Box.GamesSection.Coverflow.Rate.func_0 = new Func<bool>(this.rate);
            return true;
        }

        public bool rate()
        {
            this.Box.VideoGame.rate(this.Box.GamesSection.Coverflow.Rate.rating);
            return true;
        }

        public bool link()
        {
            this.Box.VideoGame.link(this.Box.GamesSection.FileChooser1.getfileInfo());
            this.Box.bool_1 = true;
            this.setOptionMenu();
            return true;
        }

        public bool OnControllerAccept()
        {
            this.bindfunc_CoverOptions[this.int_1]();
            this.setOptionMenu();
            return true;
        }

        public bool OnControllerBack()
        {
            base.DashBoard.initializeComponents.titleBar.clearDictionaries();
            base.DashBoard.initializeComponents.titleBar.AppendControllerIcon("A", "launch");
            base.DashBoard.initializeComponents.titleBar.AppendControllerIcon("B", "back");
            base.DashBoard.initializeComponents.titleBar.AppendControllerIcon("Y", "details");
            base.DashBoard.initializeComponents.titleBar.AppendControllerIcon("DPADV", "change category");
            this.Hide();
            return true;
        }

        public bool OnControllerDetails()
        {
            this.Hide();
            return true;
        }

        public bool OnControllerDown()
        {
            if (this.int_1 > 0)
            {
                this.int_1--;
                base.setTitle = true;
                base.bool_1 = true;
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
            if (this.int_1 < (this.string_0.Length - 1))
            {
                this.int_1++;
                base.setTitle = true;
                base.bool_1 = true;
            }
            return true;
        }

        public override void Show()
        {
            this.int_0 = 0;
            this.Box.VideoGame.checklinked();
            this.Box.bool_6 = true;
            this.setOptionMenu();
            this.int_1 = this.string_0.Length - 1;
            base.DashBoard.controllerEvents.addControllerEvent(this);
            base.DashBoard.controllerEvents.method_7(this);
            this.CalculateBounds(true);
        }

        public override void Update(GameTime gameTime)
        {
            if (this.Box.bool_6)
            {
                if (this.int_0 < 100)
                {
                    this.int_0 += 5;
                    base.bool_1 = true;
                }
            }
            else if (this.int_0 > 0)
            {
                this.int_0 -= 5;
                base.bool_1 = true;
            }
            base.Update(gameTime);
        }
    }
}

