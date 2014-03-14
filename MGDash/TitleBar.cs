namespace MGDash
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    public sealed class TitleBar : BaseClassDrawable
    {
        private Calendar calendar;
        public Color color_0;
        public Color color_1;
        private Dictionary<string, string> dictionary_0;
        private Dictionary<string, string> JoystickIcons;
        private Dictionary<string, string> KeyBoardIcons;
        private Dictionary<string, Rectangle> dictionary_3;
        private Dictionary<string, Vector2> dictionary_4;
        private int elapsedtime;
        private Rectangle rectangle_1;
        private Rectangle rectangle_2;
        private Rectangle rectangle_3;
        private Rectangle rectangle_4;
        private Rectangle rectangle_5;
        private Rectangle rectangle_6;
        public string timeText;
        public string minuteText;
        public string hourText;
        public string dayText;
        public string monthText;
        private Vector2 vectorTime;
        private Vector2 vector2_1;
        private Vector2 vector2_2;

        public TitleBar(Game _dashboard) : base(_dashboard) {
            this.calendar = new GregorianCalendar();
            this.timeText = "";
            this.minuteText = "";
            this.hourText = "";
            this.dayText = "";
            this.monthText = "";
            this.elapsedtime = -1;
            this.color_0 = Color.FromNonPremultiplied(0, 0, 0, 100);
            this.color_1 = Color.FromNonPremultiplied(0, 0, 0, 0x80);
            this.vector2_1 = new Vector2(80f, 0f);
            this.vector2_2 = new Vector2(64f, -16f);
            this.vectorTime = new Vector2(0f, -3f);
            this.rectangle_1 = new Rectangle(0x10, 0x10, 0x30, 0x30);
            this.rectangle_2 = new Rectangle(0, 0, 80, 80);
            this.rectangle_4 = new Rectangle(0, 0, 0x40, 0x40);
            this.rectangle_3 = new Rectangle(0, 0, 0x40, 0x40);
            this.rectangle_5 = new Rectangle(0, 0, 0, 120);
            this.rectangle_6 = new Rectangle(0, 0, 0, 60);
            this.dictionary_0 = new Dictionary<string, string>();
            this.dictionary_3 = new Dictionary<string, Rectangle>();
            this.dictionary_4 = new Dictionary<string, Vector2>();
            this.JoystickIcons = new Dictionary<string, string>();
            this.KeyBoardIcons = new Dictionary<string, string>();

            this.JoystickIcons.Add("A", "btn_a");
            this.JoystickIcons.Add("B", "btn_b");
            this.JoystickIcons.Add("X", "btn_x");
            this.JoystickIcons.Add("Y", "btn_y");
            this.JoystickIcons.Add("RB", "btn_rb");
            this.JoystickIcons.Add("RT", "btn_rt");
            this.JoystickIcons.Add("RS", "btn_rs");
            this.JoystickIcons.Add("LB", "btn_lb");
            this.JoystickIcons.Add("LT", "btn_lt");
            this.JoystickIcons.Add("LS", "btn_ls");
            this.JoystickIcons.Add("DPAD", "btn_d_pad");
            this.JoystickIcons.Add("DPADR", "btn_d_pad_r");
            this.JoystickIcons.Add("DPADL", "btn_d_pad_l");
            this.JoystickIcons.Add("DPADU", "btn_d_pad_u");
            this.JoystickIcons.Add("DPADD", "btn_d_pad_d");
            this.JoystickIcons.Add("DPADV", "btn_d_pad_v");
            this.JoystickIcons.Add("BACK", "btn_back");
            this.JoystickIcons.Add("START", "btn_start");
           
            this.KeyBoardIcons.Add("A", "key_enter");
            this.KeyBoardIcons.Add("B", "key_back");
            this.KeyBoardIcons.Add("Y", "key_f1");
            this.KeyBoardIcons.Add("X", "key_f2");
            this.KeyBoardIcons.Add("DPAD", "key_arrows");
            this.KeyBoardIcons.Add("DPADR", "key_arrows_r");
            this.KeyBoardIcons.Add("DPADV", "key_arrows_v");
            this.KeyBoardIcons.Add("START", "key_esc");
            this.elapsedtime = 0x3e8;
        }

        public override void CalculateBounds(bool bool_4)
        {
            this.vectorTime.X = (base.DashBoard.width - (base.getFont("TitlesShadow").MeasureString(this.timeText).X * 0.8f)) - 16f;
            if (bool_4)
            {
                this.rectangle_6.Width = base.DashBoard.width;
                this.rectangle_6.Y = base.DashBoard.heigth - 60;
                this.rectangle_5.Width = base.DashBoard.width;
                this.rectangle_3.X = (base.DashBoard.width - 0x40) - 20;
                this.rectangle_3.Y = (base.DashBoard.heigth - 0x40) + 9;
                this.rectangle_4.X = (base.DashBoard.width - 0x40) - 20;
                this.rectangle_4.Y = (base.DashBoard.heigth - 0x40) + 4;
            }
        }

        public override void Update(GameTime gameTime) {
            this.elapsedtime += gameTime.ElapsedGameTime.Milliseconds;
            if (this.elapsedtime > 0x3e8) {
                this.dayText = this.calendar.GetDayOfMonth(DateTime.Now).ToString();
                this.monthText = CultureInfo.CurrentUICulture.DateTimeFormat.GetMonthName(this.calendar.GetMonth(DateTime.Now));
                this.hourText = this.calendar.GetHour(DateTime.Now).ToString();
                this.minuteText = ((this.calendar.GetMinute(DateTime.Now) < 10) ? "0" : "") + this.calendar.GetMinute(DateTime.Now);
                this.timeText = this.hourText + ":" + this.minuteText;
                this.elapsedtime = 0;
                base.setTitle = true;
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            bool flag;
            base.spriteBatch.Draw(base.getTexture("top_gradient"), this.rectangle_5, null, this.color_0, 0f, Vector2.Zero, SpriteEffects.None, Depth.float_25);
            base.spriteBatch.DrawString(base.getFont("TitlesShadow"), this.timeText, this.vectorTime, Color.White, 0f, Vector2.Zero, (float) 0.8f, SpriteEffects.None, Depth.float_21);
            base.spriteBatch.Draw(base.getTexture("user_pic_shadow"), this.rectangle_2, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, Depth.float_21 + 0.0001f);
            base.spriteBatch.Draw(base.getTexture("user_pic"), this.rectangle_1, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, Depth.float_21);
            base.spriteBatch.DrawString(base.getFont("TextsFullShadow"), base.DashBoard.settings.user.username, this.vector2_2, this.color_1, 0f, Vector2.Zero, (float) 1f, SpriteEffects.None, Depth.float_21 + 0.0001f);
            base.spriteBatch.DrawString(base.getFont("TextsShadow"), base.DashBoard.settings.user.username, this.vector2_1, Color.White, 0f, Vector2.Zero, (float) 1f, SpriteEffects.None, Depth.float_21);
            if (flag = base.DashBoard.controllerEvents.GamePadAtached())
            {
                base.spriteBatch.Draw(base.getTexture("pad"), this.rectangle_3, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, Depth.float_21);
            }
            else
            {
                base.spriteBatch.Draw(base.getTexture("keyboard"), this.rectangle_4, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, Depth.float_21);
            }
            if (base.DashBoard.settings.user.show_hints)
            {
                if (this.dictionary_0.Count > 0)
                {
                    base.spriteBatch.Draw(base.getTexture("down_gradient"), this.rectangle_6, null, this.color_1, 0f, Vector2.Zero, SpriteEffects.None, Depth.float_22);
                }
                foreach (string str in this.dictionary_0.Keys)
                {
                    Texture2D texture = base.getTexture(flag ? this.JoystickIcons[str] : this.KeyBoardIcons[str]);
                    Rectangle? sourceRectangle = null;
                    base.spriteBatch.Draw(texture, this.dictionary_3[str], sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, Depth.float_21);
                    base.spriteBatch.DrawString(base.getFont("TextsShadow"), this.dictionary_0[str], this.dictionary_4[str], Color.White, 0f, Vector2.Zero, (float) 0.5f, SpriteEffects.None, Depth.float_21);
                }
            }
        }

        public void method_4()
        {
            int x = 0x10;
            foreach (string str in this.dictionary_0.Keys)
            {
                this.dictionary_3[str] = new Rectangle(x, (base.DashBoard.heigth - 0x20) - 6, 0x20, 0x20);
                this.dictionary_4[str] = new Vector2((float) ((x + 0x20) + 8), (float) ((base.DashBoard.heigth - 0x20) - 12));
                x += (40 + ((int) (base.getFont("TextsShadow").MeasureString(this.dictionary_0[str]).X * 0.5))) + 0x10;
            }
        }

        public void AppendControllerIcon(string string_5, string string_6)
        {
            this.dictionary_0.Add(string_5, string_6);
            this.method_4();
        }

        public void method_6(string string_5)
        {
            this.dictionary_0.Remove(string_5);
            this.method_4();
        }

        public void clearDictionaries()
        {
            this.dictionary_0.Clear();
            this.dictionary_3.Clear();
            this.dictionary_4.Clear();
        }

        
    }
}

