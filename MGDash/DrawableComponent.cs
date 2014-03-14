namespace MGDash
{   
    using System;
    using System.Text;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Media;

    public abstract class BaseClassDrawable : DrawableGameComponent
    {
        public bool setTitle;
        public bool bool_1;
        public bool bool_2;
        public bool bool_3;
        protected DashBoard DashBoard;
        public Rectangle background;
        public SpriteBatch spriteBatch;

        public BaseClassDrawable(Game _dashboard) : base(_dashboard) {
            this.DashBoard = (DashBoard) _dashboard;
            this.bool_2 = true;
            this.bool_3 = false;
            this.setTitle = false;
        }

        public virtual void CalculateBounds(bool bool_4)
        {
        }

        public virtual void CalculateColors()
        {
        }

        public virtual bool CallTextureReflections() {
            return true;
        }

        public virtual void Hide() {
            this.DashBoard.Components.Remove(this);
        }

        public Texture2D getTexture(string texture) {
            return this.DashBoard.ContentManagement.Textures[texture];
        }

        public SpriteFont getFont(string font) {
            return this.DashBoard.ContentManagement.Fonts[font];
        }

        public Model getModel(string model) {
            return this.DashBoard.ContentManagement.Models[model];
        }

        public Video getVideo(string video) {
            return this.DashBoard.ContentManagement.Videos[video];
        }

        public string getWrappedText(SpriteFont font, float float_0, string string_0, float float_1) {
            if ((string_0 == null) || (string_0.Trim().Length == 0)) {
                return "";
            }
            string[] strArray = string_0.Split(new char[] { ' ' });
            StringBuilder builder = new StringBuilder();
            float num = 0f;
            float num2 = font.MeasureString(" ").X * float_0;
            
            foreach (string str in strArray) {
                Vector2 vector = font.MeasureString(str);
                if ((num + (vector.X * float_0)) < float_1)
                {
                    builder.Append(str + " ");
                    if (str.Contains("\n"))
                    {
                        num = 0f;
                    }
                    num += (vector.X * float_0) + num2;
                }
                else
                {
                    builder.Append("\n" + str + " ");
                    num = (vector.X * float_0) + num2;
                }
            }
            return builder.ToString();
        }

        public float getTextMeasure(SpriteFont font, float float_0, string string_0)
        {
            if ((string_0 != null) && (string_0.Trim().Length != 0)) {
                return (font.MeasureString(string_0).Y * float_0);
            }
            return 0f;
        }

        public virtual void Show() {
            this.CalculateBounds(true);
            this.DashBoard.Components.Add(this);
            this.DashBoard.bool_0 = true;
        }

        public override void Update(GameTime gameTime) {
            
            if (this.setTitle) {
                this.CalculateBounds(false);
            }
            if (this.bool_1) {
                this.CalculateColors();
            }
            this.setTitle = false;
            this.bool_1 = false;
            if (this.spriteBatch == null) {
                this.spriteBatch = this.DashBoard.spriteBatch;
            }
        }
    }
}

