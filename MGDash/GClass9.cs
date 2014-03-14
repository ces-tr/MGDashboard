namespace MGDash
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Newtonsoft.Json.Linq;
   
    using System;
    using System.Text;

    public sealed class BGImageButtonControl : Control
    {
        private bool bool_5;
        public FileChooser fileChooser;
        private int int_1;
        private Rectangle rectangle;
        public string string_1;
        public string defaultImage;
        private Vector2 vector2_0;

        public BGImageButtonControl(Game game_0, float float_1, string string_3, StringBuilder stringBuilder_1, string _defaultImage)
            : base(game_0, float_1, stringBuilder_1, _defaultImage)
        {
            this.int_1 = 160;
            this.string_1 = string_3;
            this.defaultImage = _defaultImage;
            this.vector2_0 = new Vector2();
            this.rectangle = new Rectangle();
            this.background.Height = 0;
            
            this.fileChooser = new FileChooser(game_0, Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), new string[] { ".png", ".jpg", ".jpeg" }, true);
            this.fileChooser.Title = "Choose your new dashboard background:";
            this.fileChooser.bindfunc1 = new Func<bool>(this.setImage);
            this.fileChooser.bindfunc_setDefaultImage = new Func<bool>(this.setdefaultImage);
            this.bool_5 = false;
        }

        public override void CalculateBounds(bool bool_6)
        {
            Texture2D textured = base.DashBoard.ContentManagement.Images[this.defaultImage];
            if (textured != null)
            {
                this.background.Height = (int) ((((-1f + (base.getFont("TextsLight").MeasureString(this.string_1).Y * 0.7f)) + 10f) + this.int_1) + 10f);
                this.rectangle.X = this.background.X + 10;
                this.rectangle.Y = (int) (((this.background.Y - 1) + (base.getFont("TextsLight").MeasureString(this.string_1).Y * 0.7f)) + 10f);
                this.rectangle.Height = this.int_1;
                this.rectangle.Width = (int) Math.Min((double) (this.background.Width - 20), ((double) (this.int_1 * textured.Width)) / ((double) textured.Height));
            }
            this.vector2_0.X = this.background.X + 10;
            this.vector2_0.Y = this.background.Y - 1;
            base.CalculateBounds(bool_6);
        }

        public override void Draw(GameTime gameTime)
        {
            if (base.spriteBatch != null)
            {
                if (base.DashBoard.ContentManagement.Images[this.defaultImage] != null)
                {
                    base.spriteBatch.Draw(base.DashBoard.ContentManagement.Images[this.defaultImage], this.rectangle, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, base.float_0 + 0.001f);
                }
                base.spriteBatch.DrawString(base.getFont("TextsLight"), this.string_1, this.vector2_0, Color.White, 0f, Vector2.Zero, (float) 0.7f, SpriteEffects.None, base.float_0);
            }
            base.Draw(gameTime);
        }

        public bool setImage()
        {
            if (this.fileChooser.getfileInfo() != null)
            {
                base.DashBoard.ContentManagement.setImage(this.fileChooser.getfileInfo().FullName, this.defaultImage);
                JObject obj2 = JObject.Parse(base.stringBuilder_0.ToString());
                obj2[base.string_0] = this.fileChooser.getfileInfo().FullName;
                base.stringBuilder_0.Clear();
                base.stringBuilder_0.Append(obj2.ToString());
                base.setTitle = true;
            }
            return true;
        }

        public bool setdefaultImage()
        {
            base.DashBoard.ContentManagement.setImage(null, this.defaultImage);
            JObject obj2 = JObject.Parse(base.stringBuilder_0.ToString());
            obj2[base.string_0] = null;
            base.stringBuilder_0.Clear();
            base.stringBuilder_0.Append(obj2.ToString());
            base.setTitle = true;
            return true;
        }

        public override bool OnControllerAccept()
        {
            this.fileChooser.Show();
            return true;
        }

        public override void Update(GameTime gameTime)
        {
            if (!this.bool_5 && (base.DashBoard.ContentManagement.Images[this.defaultImage] != null))
            {
                base.setTitle = true;
                this.bool_5 = true;
            }
            base.Update(gameTime);
        }
    }
}

