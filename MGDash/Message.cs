namespace MGDash
{
    using Microsoft.Xna.Framework;
    using System;

    public sealed class Message : DialogScreen
    {
        public Message(Game game_0, bool addCancel, bool addBack) : base(game_0)
        {
            if (addCancel)
            {
                base.append("", ButtonControl.Options.Cancel, null);
            }
            if (addBack)
            {
                base.append("", ButtonControl.Options.Back, null);
            }
            this.vector2_0 = new Vector2();
            this.vector2_1 = new Vector2();
        }

        public override void CalculateBounds(bool bool_6)
        {
            if (bool_6)
            {
                this.vector2_0.X = (int)((((float)base.DashBoard.width) / 2f) - (base.getFont("TextsLight").MeasureString(base.Title).X / 2f));
                this.vector2_0.Y = (int) (((base.DashBoard.heigth * 0.6f) - 120f) - base.getFont("TextsLight").MeasureString(base.Title).Y);
                this.vector2_1.X = (int) ((((float) base.DashBoard.width) / 2f) - ((base.getFont("TextsLight").MeasureString(base.Text).X * 0.9f) / 2f));
                this.vector2_1.Y = (int) (((base.DashBoard.heigth * 0.6f) - 40f) - (base.getFont("TextsLight").MeasureString(base.Text).Y * 0.9f));
                base.y_position = ((int) ((base.DashBoard.heigth * 0.6f) - (((float) ButtonControl.backgroundheigth) / 2f))) + (base.Text.Equals("") ? 0 : 40);
            }
            base.CalculateBounds(bool_6);
        }

        public override int GetPriority()
        {
            return Priority.int_1;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}

