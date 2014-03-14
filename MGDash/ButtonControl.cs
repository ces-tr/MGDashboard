namespace MGDash
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    
    using System;

    public sealed class ButtonControl : BaseClassDrawable
    {
        public bool bool_4;
        private Color color_0;
        private Color color_1;
        public float float_0;
        public Func<bool> bindFunction;
        private DialogScreen DialogScreen;
        public Options Option;
        public int int_0;
        public static readonly int backgroundheigth = 80;
        public static readonly int margin = 30;
        public static readonly int padding = 50;
        public string Text;
        private Vector2 vector2_0;

        public ButtonControl(Game game_0, DialogScreen _dialog, Options _option) : base(game_0)
        {
            this.DialogScreen = _dialog;
            this.Option = _option;
            this.Text = "";
            this.bool_4 = false;
            this.float_0 = 0.5f;
            switch (_option)
            {
                case Options.Cancel:
                    if (_dialog != null)
                    {
                        this.bindFunction = new Func<bool>(_dialog.Back);
                        this.Text = "Cancel";
                    }
                    break;

                case Options.Accept:
                        this.Text = "Accept";
                    break;

                case Options.Back:
                    if (_dialog != null)
                    {
                        this.bindFunction = new Func<bool>(_dialog.Back);
                        this.Text = "Back";
                    }
                    break;
            }
            this.background = new Rectangle();
            this.vector2_0 = new Vector2();
            base.setTitle = true;
        }

        public ButtonControl(Game game_0, DialogScreen gclass2_1, Options options, string _name, Func<bool> bindfunc) : this(game_0, gclass2_1, options)
        {
            this.Text = _name;
            this.bindFunction = bindfunc;
        }

        public override void CalculateBounds(bool bool_5)
        {
            this.vector2_0.X = this.background.X + padding;
            this.vector2_0.Y = this.background.Center.Y - ((int)((base.getFont("TextsLight").MeasureString(this.Text).Y * 0.75f) / 2f));
        }

        public override void CalculateColors()
        {
            this.color_1 = Color.FromNonPremultiplied(0xff, 0xff, 0xff, Math.Min(0xff, (int) ((this.int_0 * this.float_0) * 2f)));
            if (this.bool_4)
            {
                int a = (int) Math.Min((float) 255f, (float) (((this.int_0 * 0.95f) * this.float_0) * 2f));
                switch (this.Option)
                {
                    case Options.Adhoc:
                        this.color_0 = Color.FromNonPremultiplied(0x1f, 160, 200, a);
                        return;

                    case Options.Cancel:
                        this.color_0 = Color.FromNonPremultiplied(0xbb, 0x21, 0x21, a);
                        return;

                    case Options.Accept:
                    case Options.Back:
                        this.color_0 = Color.FromNonPremultiplied(0x21, 160, 0x21, a);
                        return;
                }
            }
            else
            {
                int num2 = (int) (this.int_0 * this.float_0);
                this.color_0 = Color.FromNonPremultiplied(50, 50, 50, num2);
            }
        }

        public override void Draw(GameTime gameTime)
        {
            if (base.spriteBatch != null)
            {
                base.spriteBatch.Draw(base.getTexture("white"), base.background, null, this.color_0, 0f, Vector2.Zero, SpriteEffects.None, Depth.float_9);
                base.spriteBatch.DrawString(base.getFont("TextsLight"), this.Text, this.vector2_0, this.color_1, 0f, Vector2.Zero, (float)0.75f, SpriteEffects.None, Depth.float_7);
            }
            base.Update(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public enum Options
        {
            Adhoc,
            Cancel,
            Accept,
            Back
        }
    }
}

