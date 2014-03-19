namespace MGDash
{
    using Microsoft.Xna.Framework;
    using Newtonsoft.Json.Linq;
    using MGDash.Sources.Model;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public sealed class OptionsSection : BaseClassDrawable, IControllerEvents
    {
        private bool bool_4;
        private bool bool_5;
        private Color[] color_0;
        public double double_0;
        public static int int_0 = 800;
        private int int_1;
        private List<Configuration> ConfigList;
        public string[] titles;
        private StringBuilder stringBuilder_0;
        private StringBuilder stringBuilder_1;

        public OptionsSection(Game game_0) : base(game_0)
        {
            this.titles = new string[] { "General", "User", "Games", "Main menu" };
            this.color_0 = new Color[] { Color.FromNonPremultiplied(0x80, 0x80, 0x80, 0xff), Color.FromNonPremultiplied(0x2c, 0x5d, 0x7f, 0xff), Color.FromNonPremultiplied(15, 140, 30, 0xff), Color.FromNonPremultiplied(0x80, 0x80, 0x80, 0xff) };
            this.ConfigList = new List<Configuration>();
            for (int i = 0; i < this.titles.Length; i++)
            {
                this.ConfigList.Add(new Configuration(game_0, this, this.titles[i], this.color_0[i]));
            }
            this.double_0 = 0.0;
            this.int_1 = 0;
            this.ConfigList[0].bool_4 = true;
            this.bool_5 = false;
            this.bool_4 = false;
        }

        public override void CalculateBounds(bool bool_6)
        {
            this.method_5(bool_6);
        }

        public override void Draw(GameTime gameTime)
        {
            if (base.spriteBatch != null)
            {
                foreach (Configuration class2 in this.ConfigList)
                {
                    class2.Draw(gameTime);
                }
            }
            base.Draw(gameTime);
        }

        public int GetPriority()
        {
            return Priority.int_8;
        }

        public void method_4()
        {
            this.stringBuilder_0 = new StringBuilder(JObject.FromObject(base.DashBoard.Settings.user).ToString());
            this.stringBuilder_1 = new StringBuilder();
            this.stringBuilder_1.Append(this.stringBuilder_0);
            
            foreach (Configuration class2 in this.ConfigList)
            {
                class2.list_0.Clear();
            }
            this.ConfigList[0].list_0.Add(new SwitchButtonControl(base.DashBoard, Depth.float_54, "Use fullscreen", this.stringBuilder_0, "fullscreen"));
            this.ConfigList[0].list_0.Add(new SwitchButtonControl(base.DashBoard, Depth.float_54, "Always on top", this.stringBuilder_0, "always_on_top"));
            this.ConfigList[0].list_0.Add(new SwitchButtonControl(base.DashBoard, Depth.float_54, "Start with system", this.stringBuilder_0, "load_on_start"));
            this.ConfigList[0].list_0.Add(new SwitchButtonControl(base.DashBoard, Depth.float_54, "Use blur effects", this.stringBuilder_0, "blur_effect"));
            this.ConfigList[0].list_0.Add(new SwitchButtonControl(base.DashBoard, Depth.float_54, "Show key hints", this.stringBuilder_0, "show_hints"));
            
            this.ConfigList[1].list_0.Add(new BGImageButtonControl(base.DashBoard, Depth.float_54, "Dashboard background", this.stringBuilder_0, "user_bg"));
            
            this.ConfigList[2].list_0.Add(new BGImageButtonControl(base.DashBoard, Depth.float_54, "Games list background", this.stringBuilder_0, "games_bg"));
            this.ConfigList[2].list_0.Add(new SwitchButtonControl(base.DashBoard, Depth.float_54, "Shadows for covers", this.stringBuilder_0, "shadows_for_cover"));
            this.ConfigList[2].list_0.Add(new SwitchButtonControl(base.DashBoard, Depth.float_54, "Glow for covers", this.stringBuilder_0, "glow_for_covers"));
            this.ConfigList[2].list_0.Add(new SwitchButtonControl(base.DashBoard, Depth.float_54, "Reflections for covers", this.stringBuilder_0, "reflection_for_covers"));
            
            this.ConfigList[3].list_0.Add(new BGImageButtonControl(base.DashBoard, Depth.float_54, "Games menu background", this.stringBuilder_0, "games_menu_bg"));
            this.ConfigList[3].list_0.Add(new BGImageButtonControl(base.DashBoard, Depth.float_54, "Music menu background", this.stringBuilder_0, "music_menu_bg"));
            this.ConfigList[3].list_0.Add(new BGImageButtonControl(base.DashBoard, Depth.float_54, "Video menu background", this.stringBuilder_0, "videos_menu_bg"));
            this.ConfigList[3].list_0.Add(new BGImageButtonControl(base.DashBoard, Depth.float_54, "Social menu background", this.stringBuilder_0, "social_menu_bg"));
            this.ConfigList[3].list_0.Add(new BGImageButtonControl(base.DashBoard, Depth.float_54, "Options menu background", this.stringBuilder_0, "options_menu_bg"));
            
            for (int i = 0; i < this.ConfigList.Count; i++)
            {
                if (this.ConfigList[i].list_0.Count > 0)
                {
                    this.ConfigList[i].list_0[0].bool_4 = true;
                    this.ConfigList[i].int_0 = 0;
                }
            }
        }

        public void method_5(bool bool_6)
        {
            this.ConfigList[0].background.X = (int) this.double_0;
            this.ConfigList[0].CalculateBounds(bool_6);
            for (int i = 1; i < this.ConfigList.Count; i++)
            {
                this.ConfigList[i].background.X = this.ConfigList[i - 1].background.Right;
                this.ConfigList[i].CalculateBounds(bool_6);
            }
        }

        public bool method_6()
        {
            if (!this.bool_5)
            {
                return this.bool_4;
            }
            return true;
        }

        public bool OnControllerAccept()
        {
            this.ConfigList[this.int_1].OnControllerAccept();
            return true;
        }

        public bool OnControllerBack()
        {
            base.DashBoard.initializeComponents.titleBar.clearDictionaries();
            base.DashBoard.initializeComponents.titleBar.AppendControllerIcon("A", "open");
            base.DashBoard.initializeComponents.titleBar.AppendControllerIcon("B", "back");
            if (!this.stringBuilder_1.Equals(this.stringBuilder_0))
            {
                base.DashBoard.Settings.saveUser(JObject.Parse(this.stringBuilder_0.ToString()).ToObject<User>());
                base.DashBoard.method_5(base.DashBoard.Settings.user.load_on_start);
                if (base.DashBoard.Settings.user.fullscreen != base.DashBoard.graphicsDeviceManager.IsFullScreen)
                {
                    Message dlg_WindowMode = new Message(base.DashBoard, false, false)
                    {
                        Title = "Window mode has changed",
                        Text = "It will be applied after restarting the Application"
                    };
                    dlg_WindowMode.append("Close", ButtonControl.Options.Back, null);
                    dlg_WindowMode.Show();
                }
            }
            return true;
        }

        public bool OnControllerDetails()
        {
            return true;
        }

        public bool OnControllerDown()
        {
            this.ConfigList[this.int_1].OnControllerDown();
            return true;
        }

        public bool OnControllerLeft()
        {
            if ((this.int_1 > 0) && !this.method_6())
            {
                this.ConfigList[this.int_1].bool_4 = false;
                this.int_1--;
                this.ConfigList[this.int_1].bool_4 = true;
                this.bool_5 = true;
            }
            return true;
        }

        public bool OnControllerMenu()
        {
            return true;
        }

        public bool OnControllerRight()
        {
            if ((this.int_1 < (this.titles.Length - 1)) && !this.method_6())
            {
                this.ConfigList[this.int_1].bool_4 = false;
                this.int_1++;
                this.ConfigList[this.int_1].bool_4 = true;
                this.bool_4 = true;
            }
            return true;
        }

        public bool OnControllerSort()
        {
            return true;
        }

        public bool OnControllerUp()
        {
            this.ConfigList[this.int_1].OnControllerUp();
            return true;
        }

        public override void Show()
        {
            base.DashBoard.initializeComponents.titleBar.clearDictionaries();
            this.method_4();
            base.Show();
        }

        public override void Update(GameTime gameTime)
        {
            this.bool_4 = this.bool_4 && (this.ConfigList[this.int_1].background.Right > base.DashBoard.width);
            this.bool_5 = this.bool_5 && (this.ConfigList[this.int_1].background.X < 0);
            if (this.bool_4)
            {
                this.double_0 -= Math.Min(20, this.ConfigList[this.int_1].background.Right - base.DashBoard.width);
                base.setTitle = true;
            }
            if (this.bool_5)
            {
                this.double_0 += Math.Min(20, Math.Abs(this.ConfigList[this.int_1].background.X));
                base.setTitle = true;
            }
            foreach (Configuration class2 in this.ConfigList)
            {
                class2.Update(gameTime);
            }
            base.Update(gameTime);
        }
    }
}

