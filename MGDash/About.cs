namespace MGDash
{
    using Microsoft.Xna.Framework;
    
    using System;

    public sealed class About : DialogScreen
    {
        private bool bool_6;

        public About(Game game_0, bool bool_7) : base(game_0)
        {
            base.append("Accept", ButtonControl.Options.Back, null);
            this.vector2_0 = new Vector2();
            this.vector2_1 = new Vector2();
            this.bool_6 = bool_7;
        }

        public override void CalculateBounds(bool bool_7)
        {
            if (bool_7)
            {
                if (!this.bool_6)
                {
                    this.vector2_0.X = (int) ((((float) base.DashBoard.width) / 2f) - (base.getFont("TextsLight").MeasureString(base.Title).X / 2f));
                    this.vector2_0.Y = (int) (((base.DashBoard.heigth * 0.6f) - 120f) - base.getFont("TextsLight").MeasureString(base.Title).Y);
                    this.vector2_1.X = (int) ((((float) base.DashBoard.width) / 2f) - ((base.getFont("TextsLight").MeasureString(base.Text).X * 0.9f) / 2f));
                    this.vector2_1.Y = (int) (((base.DashBoard.heigth * 0.6f) - 40f) - (base.getFont("TextsLight").MeasureString(base.Text).Y * 0.9f));
                    base.y_position = ((int) ((base.DashBoard.heigth * 0.6f) - (((float) ButtonControl.backgroundheigth) / 2f))) + (base.Text.Equals("") ? 0 : 40);
                }
                else
                {
                    base.Text = base.getWrappedText(base.getFont("TextsLight"), 0.9f, base.Text, base.DashBoard.width * 0.8f);
                    float num = base.getTextMeasure(base.getFont("TextsLight"), 0.9f, base.Text);
                    this.vector2_1.X = (int) (base.DashBoard.width * 0.1f);
                    this.vector2_1.Y = (base.DashBoard.heigth / 2) - ((int) (num / 2f));
                    this.vector2_0.X = this.vector2_1.X;
                    this.vector2_0.Y = (int) ((this.vector2_1.Y - base.getFont("TextsLight").MeasureString(base.Title).Y) - 20f);
                    base.y_position = (int) ((this.vector2_1.Y + num) + 40f);
                }
            }
            base.CalculateBounds(bool_7);
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

