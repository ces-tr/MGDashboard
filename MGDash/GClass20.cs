namespace MGDash
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    
    using System;
    using System.Runtime.InteropServices;

    public sealed class Exit : BaseClassDrawable, IControllerEvents
    {
        private bool bool_4;
        public bool bool_5;
        private Color color_0;
        private Color color_1;
        private Color color_2;
        private Color color_3;
        private Color color_4;
        public Message exit;
        public Message poweroff;
        private int int_0;
        private int int_1;
        private int int_2;
        private Rectangle rectangle_1;
        private Rectangle rectangle_2;
        private Rectangle rectangle_3;
        private Vector2 vector2_0;
        private Vector2 vector2_1;

        public Exit(Game game_0) : base(game_0)
        {
            base.bool_2 = false;
            this.int_2 = 0;
            this.int_0 = 0;
            this.int_1 = 0;
            base.bool_3 = true;
            this.rectangle_1 = new Rectangle(0, 0, 400, 200);
            this.rectangle_2 = new Rectangle(0, 0, 380, 0x55);
            this.rectangle_3 = new Rectangle(0, 0, 380, 0x55);
            
            this.exit = new Message(base.DashBoard, true, false);
            this.exit.Title = "Are you sure you want to exit?";
            this.exit.append("Log out", ButtonControl.Options.Adhoc, new Func<bool>(CloseMethods.logout));
            this.exit.append("Exit", ButtonControl.Options.Adhoc, new Func<bool>(CloseMethods.exit));
            
            
            this.poweroff = new Message(base.DashBoard, true, false);
            this.poweroff.append("Restart", ButtonControl.Options.Adhoc, new Func<bool>(CloseMethods.restart));
            if (IsPwrHibernateAllowed())
            {
                this.poweroff.append("Hibernate", ButtonControl.Options.Adhoc, new Func<bool>(CloseMethods.hibernate));
            }
            this.poweroff.append("Power off", ButtonControl.Options.Adhoc, new Func<bool>(CloseMethods.shutdown));
            this.poweroff.Title = "Select an option to shutdown your machine";
            this.poweroff.Text = "You won't be asked to confirm";
        }

        public override void CalculateBounds(bool bool_6)
        {
            if (bool_6)
            {
                this.rectangle_1.X = (base.DashBoard.width / 2) - (this.rectangle_1.Width / 2);
                this.rectangle_1.Y = (base.DashBoard.heigth / 2) - (this.rectangle_1.Height / 2);
                this.rectangle_2.X = this.rectangle_1.X + 10;
                this.rectangle_2.Y = this.rectangle_1.Y + 10;
                this.vector2_0.X = this.rectangle_2.Center.X - ((int) ((((double) base.getFont("TextsLight").MeasureString("Power Off").X) / 2.0) * 0.8));
                this.vector2_0.Y = this.rectangle_2.Center.Y - ((int) ((((double) base.getFont("TextsLight").MeasureString("Power Off").Y) / 2.0) * 0.8));
                this.rectangle_3.X = this.rectangle_2.X;
                this.rectangle_3.Y = this.rectangle_2.Bottom + 10;
                this.vector2_1.X = this.rectangle_3.Center.X - ((int) ((((double) base.getFont("TextsLight").MeasureString("Exit").X) / 2.0) * 0.8));
                this.vector2_1.Y = this.rectangle_3.Center.Y - ((int) ((((double) base.getFont("TextsLight").MeasureString("Exit").Y) / 2.0) * 0.8));
            }
        }

        public override void CalculateColors()
        {
            this.color_0 = Color.FromNonPremultiplied(0xff, 0xff, 0xff, this.int_2);
            this.color_1 = Color.FromNonPremultiplied(200, 200, 200, this.int_2);
            this.color_2 = Color.FromNonPremultiplied(100, 100, 100, this.int_2);
            this.color_3 = Color.FromNonPremultiplied(70, 70, 70, this.int_2);
            this.color_4 = Color.FromNonPremultiplied(0x1f, 100, 190, this.int_2);
        }

        public override void Draw(GameTime gameTime)
        {
            if (base.spriteBatch != null)
            {
                base.spriteBatch.Draw(base.DashBoard.BlurBackground.BluredBackground, base.DashBoard.rectangle, null, this.color_3, 0f, Vector2.Zero, SpriteEffects.None, Depth.float_4);
                base.spriteBatch.Draw(base.getTexture("white"), this.rectangle_1, null, this.color_0, 0f, Vector2.Zero, SpriteEffects.None, Depth.float_3);
                base.spriteBatch.Draw(base.getTexture("white"), this.rectangle_2, null, (this.int_0 == 0) ? this.color_4 : this.color_1, 0f, Vector2.Zero, SpriteEffects.None, Depth.float_2);
                base.spriteBatch.DrawString(base.getFont("TextsLight"), "Power Off", this.vector2_0, (this.int_0 == 0) ? Color.White : this.color_2, 0f, Vector2.Zero, (float)0.8f, SpriteEffects.None, Depth.float_1);
                base.spriteBatch.Draw(base.getTexture("white"), this.rectangle_3, null, (this.int_0 == 1) ? this.color_4 : this.color_1, 0f, Vector2.Zero, SpriteEffects.None, Depth.float_2);
                base.spriteBatch.DrawString(base.getFont("TextsLight"), "Exit", this.vector2_1, (this.int_0 == 1) ? Color.White : this.color_2, 0f, Vector2.Zero, (float) 0.8f, SpriteEffects.None, Depth.float_1);
            }
            base.Draw(gameTime);
        }

        public int GetPriority()
        {
            return Priority.int_0;
        }

        public override void Hide()
        {
            base.DashBoard.controllerEvents.method_6(this);
            base.DashBoard.controllerEvents.method_8();
            base.Hide();
            this.bool_4 = false;
            this.bool_5 = false;
        }

        [DllImport("powrprof.dll")]
        private static extern bool IsPwrHibernateAllowed();
        public bool OnControllerAccept()
        {
            this.Hide();
            switch (this.int_0)
            {
                case 0:
                    this.poweroff.Show();
                    break;

                case 1:
                    this.exit.Show();
                    break;
            }
            return true;
        }

        public bool OnControllerBack()
        {
            this.bool_4 = true;
            return true;
        }

        public bool OnControllerDetails()
        {
            return true;
        }

        public bool OnControllerDown()
        {
            this.int_0 = 1;
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
            this.int_0 = 0;
            return true;
        }

        public override void Show()
        {
            if (!this.bool_4)
            {
                this.bool_5 = true;
                this.int_2 = 0;
                this.int_0 = this.int_1;
                base.DashBoard.controllerEvents.addControllerEvent(this);
                base.DashBoard.controllerEvents.method_7(this);
                base.Show();
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (this.bool_4)
            {
                if (this.int_2 > 0)
                {
                    this.int_2 -= 15;
                    base.bool_1 = true;
                    if (this.int_2 == 0)
                    {
                        this.Hide();
                    }
                }
            }
            else if (this.int_2 < 0xff)
            {
                this.int_2 += 15;
                base.bool_1 = true;
            }
            base.Update(gameTime);
        }
    }
}

