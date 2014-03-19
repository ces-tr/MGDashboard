namespace MGDash
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    
    using System;
    using System.Collections.Generic;

    public sealed class Menu : BaseClassDrawable, IControllerEvents, IControllerEventsMenu
    {
        private bool bool_4;
        private bool bool_5;
        public bool bool_6;
        private bool bool_7;
        private bool[][] bool_8;
        private Color[] color_0;
        private Color color_1;
        private Color color_2;
        private float float_0;
        private Components components;
        public Exit gclass20_0;
        public About gclass5_0;
        public About gclass5_1;
        private const int int_0 = 0;
        private const int int_1 = 1;
        private const int int_2 = 2;
        private const int int_3 = 3;
        private const int int_4 = 4;
        private int int_5;
        public int int_6;
        private int int_7;
        public int int_8;
        public List<MenuItem> list_0;
        private Rectangle rectangle_1;
        public Rectangle rectangle_2;
        private string[] menu0Titles;
        private string[] menu1Titles;
        private string[][] GamesMenuTitles;
        private Vector2 vector2_0;
        private Vector2 vector2_1;
        public Version version_0;

        public Menu(Game game) : base(game)
        {
            this.menu0Titles = new string[] { "games", "music", "videos", "social", "options" };
            this.menu1Titles = new string[] { "games", "music", "videos", "social", "options" };
            this.GamesMenuTitles = new string[][] { new string[] { "All", "Favorites", "Installed" }, 
                                            new string[] { "Coming soon" }, 
                                            new string[] { "Coming soon" }, 
                                            new string[] { "Coming soon" }, 
                                            new string[] { "Exit", "Power Off", "Configuration", "About" } };
            bool[][] flagArray = new bool[5][];
            flagArray[0] = new bool[] { true, true, true };
            flagArray[1] = new bool[1];
            flagArray[2] = new bool[1];
            flagArray[3] = new bool[1];
            bool[] flagArray5 = new bool[4];
            flagArray5[2] = true;
            flagArray[4] = flagArray5;
            this.bool_8 = flagArray;
            this.color_0 = new Color[] { Color.FromNonPremultiplied(15, 140, 30, 0xff), Color.FromNonPremultiplied(0x1f, 100, 190, 0xff), Color.FromNonPremultiplied(180, 0x21, 0x21, 0xff), Color.FromNonPremultiplied(220, 0x84, 0x11, 0xff), Color.FromNonPremultiplied(0x80, 0x80, 0x80, 0xff) };
            this.list_0 = new List<MenuItem>();
            this.gclass20_0 = new Exit(game);
            for (int i = 0; i < this.menu0Titles.Length; i++)
            {
                MenuItem item = new MenuItem(base.DashBoard, this, this.menu0Titles[i], this.color_0[i], this.GamesMenuTitles[i]);
                this.list_0.Add(item);
            }
            this.method_5(0);
            this.bool_7 = false;
            this.components = null;
            this.bool_5 = false;
            this.bool_6 = false;
            this.int_6 = 90;
            this.gclass5_0 = new About(base.DashBoard, false);
            this.gclass5_0.Title = "Gamecher: Play with style";
            this.gclass5_0.Text = "Visit www.gamecher.com for updates.";
            this.gclass5_1 = new About(base.DashBoard, true);
            this.rectangle_1 = new Rectangle(0, 0, 0, 120);
            this.rectangle_2 = new Rectangle();
            this.vector2_0 = new Vector2();
            this.vector2_1 = new Vector2();
        }

        public override void CalculateBounds(bool bool_9)
        {
            this.int_8 = (int) Math.Round((double) (Math.Sin((double) MathHelper.ToRadians((float) this.int_6)) * base.DashBoard.width));
            if (this.list_0[this.int_7].int_5 == -1)
            {
                this.list_0[this.int_7].int_5 = (int) Math.Round((double) (base.DashBoard.width * (0.25 + (this.int_7 * 0.15))));
            }
            if (bool_9)
            {
                this.rectangle_1.Width = base.DashBoard.width;
                this.rectangle_1.Y = (int) ((base.DashBoard.heigth * 0.8f) - 12f);
                this.rectangle_2.Width = base.DashBoard.width;
                this.rectangle_2.Height = base.DashBoard.heigth;
                this.float_0 = ((float) base.DashBoard.rectangle.Width) / 1920f;
            }
            this.rectangle_2.X = Math.Min((this.list_0[0].background.X - this.rectangle_2.Width) + this.int_8, 0);
            this.vector2_0.X = this.rectangle_2.X + 80;
            this.vector2_0.Y = (int) (base.DashBoard.heigth * 0.3);
            this.vector2_1.X = this.rectangle_2.X + 80;
            this.vector2_1.Y = (int)((this.vector2_0.Y + (base.getFont("TitlesBig").MeasureString("99").Y * this.float_0)) - 16f);
        }

        public override void CalculateColors()
        {
            int r = (int) (255.0 * ((Math.Sin((double) MathHelper.ToRadians((float) this.int_6)) * 0.7) + 0.3));
            this.color_1 = Color.FromNonPremultiplied(r, r, r, (int) (255.0 * Math.Sin((double) MathHelper.ToRadians((float) this.int_5))));
            this.color_2 = Color.FromNonPremultiplied(0xff, 0xff, 0xff, (int) (200.0 * Math.Sin((double) MathHelper.ToRadians((float) this.int_5))));
            foreach (MenuItem class2 in this.list_0)
            {
                class2.int_1 = (int) (255.0 * Math.Sin((double) MathHelper.ToRadians((float) this.int_5)));
                class2.bool_1 = true;
            }
        }

        public override void Draw(GameTime gameTime)
        {
            if ((base.spriteBatch != null) && (this.int_5 > 0))
            {
                if (!this.list_0[this.int_7].method_5())
                {
                    base.spriteBatch.Draw(base.DashBoard.ContentManagement.Images["user_bg"], this.rectangle_2, null, this.color_1, 0f, Vector2.Zero, SpriteEffects.None, Depth.float_23);
                    base.spriteBatch.DrawString(base.getFont("TitlesBig"), base.DashBoard.initializeComponents.titleBar.dayText, this.vector2_0, this.color_1, 0f, Vector2.Zero, this.float_0, SpriteEffects.None, Depth.float_21);
                    base.spriteBatch.DrawString(base.getFont("TitlesShadow"), base.DashBoard.initializeComponents.titleBar.monthText, this.vector2_1, this.color_1, 0f, Vector2.Zero, this.float_0, SpriteEffects.None, Depth.float_21);
                }
                base.spriteBatch.Draw(base.getTexture("black"), this.rectangle_1, null, this.color_2, 0f, Vector2.Zero, SpriteEffects.None, Depth.float_25);
            }
            base.Draw(gameTime);
        }

        public int GetPriority()
        {
            return Priority.int_10;
        }

        public void method_4(bool bool_9)
        {
            for (int i = this.int_7 + 1; i < this.list_0.Count; i++)
            {
                this.list_0[i].int_5 = this.list_0[i - 1].background.Right;
                this.list_0[i].CalculateBounds(bool_9);
            }
            for (int j = this.int_7 - 1; j >= 0; j--)
            {
                this.list_0[j].int_5 = (int) Math.Round((double) (this.list_0[j + 1].background.X - (base.DashBoard.width * 0.15)));
                this.list_0[j].CalculateBounds(bool_9);
            }
        }

        private void method_5(int int_9)
        {
            if (this.bool_7)
            {
                this.list_0[this.int_7].bool_5 = false;
            }
            this.list_0[this.int_7].bool_4 = false;
            this.int_7 = int_9;
            if (this.bool_7)
            {
                this.list_0[this.int_7].bool_5 = true;
            }
            this.list_0[this.int_7].bool_4 = true;
        }

        private void method_6(Components gclass13_1)
        {
            gclass13_1.Show();
            gclass13_1.addControllerEvents();
            this.components = gclass13_1;
        }

        private bool method_7()
        {
            return this.bool_8[this.int_7][this.list_0[this.int_7].int_0];
        }

        public void InitializeGamesSection()
        {
            int num = this.int_7;
            if (num == 0)
            {
                GamesSection games = (GamesSection)base.DashBoard.initializeComponents.games_Components.DrawableComponents["collection"];
                switch (this.list_0[this.int_7].int_0)
                {
                    case 0:
                        games.checkGames(GamesSection.GamesMenu.All);
                        break;

                    case 1:
                        games.checkGames(GamesSection.GamesMenu.Favorites);
                        break;

                    case 2:
                        games.checkGames(GamesSection.GamesMenu.Installed);
                        break;
                }
                games.Coverflow.method_4();
                this.method_6(base.DashBoard.initializeComponents.games_Components);
            }
            else if (num == 4)
            {
                switch (this.list_0[this.int_7].int_0)
                {
                    case 0:
                        this.gclass20_0.exit.Show();
                        return;

                    case 1:
                        this.gclass20_0.poweroff.Show();
                        return;

                    case 2:
                        this.method_6(base.DashBoard.initializeComponents.options_Items);
                        return;

                    case 3:
                        this.gclass5_0.Show();
                        return;
                }
            }
        }

        public bool OnControllerAccept()
        {
            if ((!this.bool_5 && this.bool_6) && (!this.list_0[this.int_7].method_4() && (this.components == null)))
            {
                if (this.bool_7)
                {
                    if (this.method_7())
                    {
                        this.bool_4 = true;
                        this.bool_7 = false;
                    }
                    this.InitializeGamesSection();
                }
                else if (this.components == null)
                {
                    this.list_0[this.int_7].bool_5 = true;
                    this.bool_7 = true;
                }
            }
            return true;
        }

        public bool OnControllerBack()
        {
            if ((!this.bool_5 && this.bool_6) && !this.list_0[this.int_7].method_4())
            {
                if (!this.bool_7)
                {
                    if (this.list_0[this.int_7].method_5())
                    {
                        if (this.components != null)
                        {
                            this.components.method_6();
                            this.bool_4 = false;
                        }
                        this.bool_7 = true;
                    }
                    else
                    {
                        this.bool_6 = false;
                        base.DashBoard.initializeComponents.titleBar.AppendControllerIcon("DPADR", "open menu");
                        base.DashBoard.initializeComponents.titleBar.method_6("A");
                        base.DashBoard.initializeComponents.titleBar.method_6("B");
                    }
                }
                else
                {
                    this.list_0[this.int_7].bool_5 = false;
                    this.bool_7 = false;
                }
            }
            return true;
        }

        public bool OnControllerDetails()
        {
            return true;
        }

        public bool OnControllerDown()
        {
            if (((!this.bool_5 && this.bool_6) && (this.bool_7 && (this.components == null))) && (this.list_0[this.int_7].int_0 > 0))
            {
                this.list_0[this.int_7].method_6(-1);
            }
            return true;
        }

        public bool OnControllerLeft()
        {
            if (((!this.bool_5 && this.bool_6) && (!this.bool_7 && (this.components == null))) && (((this.int_7 > 0) && !this.bool_5) && !this.list_0[this.int_7].method_4()))
            {
                this.method_5(this.int_7 - 1);
            }
            return true;
        }

        public bool OnControllerMenu()
        {
            if (!this.gclass20_0.bool_5)
            {
                this.gclass20_0.Show();
            }
            return true;
        }

        public bool OnControllerRight()
        {
            if (!this.bool_5)
            {
                if (this.bool_6)
                {
                    if (((!this.bool_7 && (this.components == null)) && ((this.int_7 < (this.list_0.Count - 1)) && !this.bool_5)) && !this.list_0[this.int_7].method_4())
                    {
                        this.method_5(this.int_7 + 1);
                    }
                }
                else
                {
                    this.bool_6 = true;
                    base.DashBoard.initializeComponents.titleBar.method_6("DPADR");
                    base.DashBoard.initializeComponents.titleBar.AppendControllerIcon("A", "open");
                    base.DashBoard.initializeComponents.titleBar.AppendControllerIcon("B", "back");
                }
            }
            return true;
        }

        public bool OnControllerSort()
        {
            return true;
        }

        public bool OnControllerUp()
        {
            if (((!this.bool_5 && this.bool_6) && (this.bool_7 && (this.components == null))) && (this.list_0[this.int_7].int_0 < (this.GamesMenuTitles[this.int_7].Length - 1)))
            {
                this.list_0[this.int_7].method_6(1);
            }
            return true;
        }

        public override void Show()
        {
            if (base.DashBoard.Settings.user.announcement)
            {
                this.gclass5_1.Title = base.DashBoard.Settings.user.announcement_title;
                this.gclass5_1.Text = base.DashBoard.Settings.user.announcement_content;
                this.gclass5_1.Show();
                base.DashBoard.Settings.method_4("announcement", false);
            }
            base.Show();
        }

        public override void Update(GameTime gameTime)
        {
            if (this.bool_4)
            {
                if (this.int_5 > 0)
                {
                    this.int_5 -= 3;
                    base.bool_1 = true;
                }
            }
            else if (this.int_5 < 90)
            {
                this.int_5 += 2;
                base.bool_1 = true;
            }
            if (this.bool_6)
            {
                if (this.int_6 > 0)
                {
                    this.int_6 -= 2;
                    base.setTitle = true;
                    base.bool_1 = true;
                }
            }
            else if (this.int_6 < 90)
            {
                this.int_6 += 2;
                base.setTitle = true;
                base.bool_1 = true;
            }
            this.bool_5 = ((this.int_5 % 90) != 0) || ((this.int_6 % 90) != 0);
            if (((this.components != null) && (this.int_5 == 90)) && !this.components.bool_7)
            {
                this.components.Hide();
                this.components = null;
            }
            base.Update(gameTime);
        }
    }
}

