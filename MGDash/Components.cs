namespace MGDash
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using System;
    using System.Collections.Generic;

    public sealed class Components : BaseClassDrawable
    {
        public bool bool_4;
        private bool bool_5;
        private bool bool_6;
        public bool bool_7;
        public bool bool_8;
        public Dictionary<string, BaseClassDrawable> DrawableComponents;
        public double double_0;
        public Components components;
        private int int_0;
        public List<IControllerEvents> ControllerEventlist;

        public Components(DashBoard _dashBoard)
            : base(_dashBoard)
        {
            this.DrawableComponents = new Dictionary<string, BaseClassDrawable>();
            this.ControllerEventlist = new List<IControllerEvents>();
            base.DashBoard = _dashBoard;
            this.int_0 = 0;
            this.components = null;
            this.bool_4 = false;
            this.bool_5 = false;
            this.double_0 = 100.0;
            this.bool_7 = true;
            this.bool_8 = false;
        }

        public override void Draw(GameTime gameTime)
        {
            if (((base.spriteBatch != null) && this.bool_4) && this.bool_5)
            {
                if (this.int_0 <= this.double_0)
                {
                    this.int_0++;
                    int num = (int) (255.0 * Math.Sin(((((double) this.int_0) / this.double_0) * 3.1415926535897931) / 2.0));
                    Color color = Color.FromNonPremultiplied(0, 0, 0, this.bool_6 ? (0xff - num) : num);
                    base.spriteBatch.Draw(base.getTexture("black"), base.DashBoard.rectangle, null, color, 0f, Vector2.Zero, SpriteEffects.None, Depth.float_0);
                    if (!this.bool_6 && (this.int_0 == (this.double_0 - 10.0)))
                    {
                        this.method_4();
                    }
                }
                else
                {
                    this.bool_5 = false;
                    this.int_0 = 0;
                }
            }
            base.Draw(gameTime);
        }

        public override void Hide()
        {
            this.bool_6 = false;
            this.bool_5 = true;
            if (!this.bool_4)
            {
                this.method_4();
            }
        }

        private void method_4()
        {
            base.Hide();
            foreach (BaseClassDrawable drawableComponent in this.DrawableComponents.Values)
            {
                drawableComponent.Hide();
            }
            if (this.components != null)
            {
                this.components.bool_4 = this.bool_4;
                this.components.Show();
                this.components.addControllerEvents();
            }
            this.bool_7 = true;
        }

        public void addControllerEvents()
        {
            foreach (IControllerEvents controllerEvent in this.ControllerEventlist)
            {
                base.DashBoard.controllerEvents.addControllerEvent(controllerEvent);
            }
            this.bool_8 = true;
        }

        public void method_6()
        {
            foreach (IControllerEvents interface2 in this.ControllerEventlist)
            {
                base.DashBoard.controllerEvents.method_6(interface2);
            }
            this.bool_8 = false;
        }

        public override void Show()
        {
            this.bool_5 = true;
            this.bool_6 = true;
            foreach (BaseClassDrawable drawableComponent in this.DrawableComponents.Values)
            {
                if (drawableComponent.bool_2)
                {
                    drawableComponent.Show();
                }
            }
            this.bool_7 = false;
            base.Show();
        }
    }
}

