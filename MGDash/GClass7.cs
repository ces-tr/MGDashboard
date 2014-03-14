namespace MGDash
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
   
    using System;
    using System.Text;

    public class Control : BaseClassDrawable, IControllerEvents
    {
        public bool bool_4;
        private Color color_0;
        public float float_0;
        protected int int_0;
        protected string string_0;
        protected StringBuilder stringBuilder_0;

        public Control(Game game_0, float float_1, StringBuilder stringBuilder_1, string string_1) : base(game_0)
        {
            this.float_0 = float_1;
            this.stringBuilder_0 = stringBuilder_1;
            this.string_0 = string_1;
            this.bool_4 = false;
            this.int_0 = 0;
        }

        public override void CalculateColors()
        {
            this.color_0 = Color.FromNonPremultiplied(0xff, 0xff, 0xff, (int) ((255.0 * Math.Sin((double) MathHelper.ToRadians((float) this.int_0))) * 0.5));
        }

        public override void Draw(GameTime gameTime)
        {
            if ((base.spriteBatch != null) && (this.int_0 > 0))
            {
                base.spriteBatch.Draw(base.getTexture("white"), base.background, null, this.color_0, 0f, Vector2.Zero, SpriteEffects.None, this.float_0 + 0.009f);
            }
        }

        public virtual int GetPriority()
        {
            return Priority.int_4;
        }

        public virtual bool OnControllerAccept()
        {
            return true;
        }

        public virtual bool OnControllerBack()
        {
            return true;
        }

        public virtual bool OnControllerDetails()
        {
            return true;
        }

        public virtual bool OnControllerDown()
        {
            return true;
        }

        public virtual bool OnControllerLeft()
        {
            return true;
        }

        public virtual bool OnControllerRight()
        {
            return true;
        }

        public virtual bool OnControllerSort()
        {
            return true;
        }

        public virtual bool OnControllerUp()
        {
            return true;
        }

        public override void Update(GameTime gameTime)
        {
            if (this.bool_4)
            {
                if (this.int_0 < 90)
                {
                    this.int_0 += 10;
                    base.bool_1 = true;
                }
            }
            else if (this.int_0 > 0)
            {
                this.int_0 -= 10;
                base.bool_1 = true;
            }
            base.Update(gameTime);
        }
    }
}

