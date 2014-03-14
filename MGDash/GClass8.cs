namespace MGDash
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Text;

    public sealed class SwitchButtonControl : Control
    {
        public bool bool_5;
        private Color color_1;
        private int int_1;
        private Rectangle rectangle_1;
        private Rectangle rectangle_2;
        public string string_1;
        private Vector2 vector2_0;

        public SwitchButtonControl(Game game_0, float float_1, string string_2, StringBuilder stringBuilder_1, string string_3) : base(game_0, float_1, stringBuilder_1, string_3)
        {
            this.string_1 = string_2;
            this.bool_5 = (bool) JObject.Parse(stringBuilder_1.ToString())[string_3];
            this.int_1 = this.bool_5 ? 80 : 2;
            this.rectangle_1 = new Rectangle(0, 0, 160, 0x30);
            this.rectangle_2 = new Rectangle(0, 0, 0x4e, 0x2c);
            this.vector2_0 = new Vector2();
            this.color_1 = Color.FromNonPremultiplied(0x2d, 0x5e, 0xc0, 0xff);
            this.background.Height = 0x40;
        }

        public override void CalculateBounds(bool bool_6)
        {
            this.rectangle_1.X = (this.background.Right - this.rectangle_1.Width) - 10;
            this.rectangle_1.Y = this.background.Center.Y - (this.rectangle_1.Height / 2);
            this.rectangle_2.X = this.rectangle_1.X + this.int_1;
            this.rectangle_2.Y = this.rectangle_1.Y + 2;
            this.vector2_0.X = this.background.X + 10;
            this.vector2_0.Y = this.background.Center.Y - ((base.getFont("TextsLight").MeasureString(this.string_1).Y * 0.7f) / 2f);
            base.CalculateBounds(bool_6);
        }

        public override void Draw(GameTime gameTime)
        {
            if (base.spriteBatch != null)
            {
                base.spriteBatch.Draw(base.getTexture("white"), this.rectangle_1, null, Color.GhostWhite, 0f, Vector2.Zero, SpriteEffects.None, base.float_0 + 0.002f);
                base.spriteBatch.Draw(base.getTexture("white"), this.rectangle_2, null, this.bool_5 ? this.color_1 : Color.Gray, 0f, Vector2.Zero, SpriteEffects.None, base.float_0 + 0.001f);
                base.spriteBatch.DrawString(base.getFont("TextsLight"), this.string_1, this.vector2_0, Color.White, 0f, Vector2.Zero, (float) 0.7f, SpriteEffects.None, base.float_0);
            }
            base.Draw(gameTime);
        }

        public override bool OnControllerAccept()
        {
            this.bool_5 = !this.bool_5;
            JObject obj2 = JObject.Parse(base.stringBuilder_0.ToString());
            obj2[base.string_0] = this.bool_5;
            base.stringBuilder_0.Clear();
            base.stringBuilder_0.Append(obj2.ToString());
            return true;
        }

        public override void Update(GameTime gameTime)
        {
            if (this.bool_5)
            {
                if (this.int_1 < 80)
                {
                    this.int_1 += 6;
                    base.setTitle = true;
                }
            }
            else if (this.int_1 > 2)
            {
                this.int_1 -= 6;
                base.setTitle = true;
            }
            base.Update(gameTime);
        }
    }
}

