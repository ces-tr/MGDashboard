using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace MGDash {

    public sealed class FileChooser : DialogScreen {

        private Color color_3;
        private Color color_4;
        private Color color_5;
        public DirectoryInfo directoryInfo_0;
        public Func<bool> bindfunc1;
        public Func<bool> bindfunc_setDefaultImage;
        
        private static Func<DirectoryInfo, bool> func_2;
        public int fl_pointer;
        private int int_5;
        private int int_6;
        private int int_7;
        private int int_8;
        private List<FileSystemInfo> fileList;
        private Rectangle rectangle_1;
        private Rectangle rectangle_2;
        private Rectangle rectangle_3;
        private Rectangle rectangle_4;
        private Rectangle rectangle_5;
        private Rectangle[] rectangle_6;
        public string[] fileTypes;
        public string string_3;
        private Vector2 vector2_2;
        private Vector2[] vector2_3;

        public FileChooser(Game game_0, string string_4, string[] filetypes, bool usedefault) : base(game_0)
        {
            this.fileList = new List<FileSystemInfo>();
            this.string_3 = string_4;
            this.directoryInfo_0 = new DirectoryInfo(string_4);
            this.fileTypes = filetypes;
            base.Text = "Select a file (" + string.Join(", ",filetypes) + ")";
            base.float_0 = 0.8f;
            base.append("", ButtonControl.Options.Cancel, null);
            if (usedefault) {
                base.append("Use default", ButtonControl.Options.Adhoc, new Func<bool>(this.defaultImage));
            }
            base.append("open", ButtonControl.Options.Accept, new Func<bool>(this.openDirectory));
            base.int_1 = 0;
            base.buttonListIndex = 0;
            this.setfileList();
            this.vector2_2 = new Vector2(16f, 0f);
            this.rectangle_1 = new Rectangle(0, 140, 0, 0x44);
            this.rectangle_2 = new Rectangle(0, 140, 0, 0);
            this.rectangle_3 = new Rectangle(0, 0, 0, 50);
            this.rectangle_4 = new Rectangle(0, 0xd0, 15, 0);
            this.rectangle_5 = new Rectangle(0, 0, 15, 0);
            base.vector2_0 = new Vector2(16f, 0f);
            base.vector2_1 = new Vector2(16f, 0f);
            base.bool_5 = true;
            this.int_8 = 0;
        }

        public override void CalculateBounds(bool bool_6)
        {
            this.vector2_0.Y = (int) (80f - (base.getFont("TextsLight").MeasureString(base.Title).Y * base.float_0));
            this.vector2_1.Y = (int) (140f - ((base.getFont("TextsLight").MeasureString(base.Text).Y * 0.9f) * base.float_0));
            this.vector2_2.Y = 170f - (((base.getFont("TextsLight").MeasureString(this.directoryInfo_0.Name).Y / 2f) * 0.9f) * base.float_0);
            
            if (bool_6) {
                this.int_8 = (((base.DashBoard.heigth - 280) / 50) * 50) + 0x12;
                this.rectangle_1.Width = base.DashBoard.width;
                this.rectangle_2.Width = base.DashBoard.width;
                this.rectangle_2.Height = this.int_8;
                this.rectangle_4.X = base.DashBoard.width - 15;
                this.rectangle_4.Height = this.int_8 - 0x44;
            }
            this.rectangle_3.Y = 0xd0 + ((this.fl_pointer - this.int_5) * 50);
            if (bool_6) {
                this.rectangle_3.Width = base.DashBoard.width;
            }
            int num = (int) Math.Max((double) 10.0, (double) (((double) ((this.int_8 - 0x44) * this.int_7)) / ((double) (this.fileList.Count - 1))));
            int num2 = (int) Math.Min((double) ((140 + this.int_8) - num), Math.Round((double) (208.0 + (((double) ((this.int_8 - 0x44) * this.int_5)) / ((double) (this.fileList.Count - 1))))));
            if (Math.Abs((int) ((num2 + num) - (this.int_8 + 140))) < 3) {
                num2 = (this.int_8 + 140) - num;
            }
            if (bool_6) {
                this.rectangle_5.X = base.DashBoard.width - 15;
            }
            this.rectangle_5.Y = num2;
            this.rectangle_5.Height = num;
            base.y_position = ((this.rectangle_2.Bottom + base.DashBoard.heigth) / 2) - (ButtonControl.backgroundheigth / 2);
            this.int_7 = ((int) (((double) (this.rectangle_2.Height - 0x44)) / 50.0)) - 1;
            this.int_6 = Math.Min((int) (this.int_5 + this.int_7), (int) (this.fileList.Count - 1));
            
            for (int i = 0; i < this.fileList.Count; i++) {
                this.vector2_3[i].Y = 210 + ((i - this.int_5) * 50);
                this.rectangle_6[i].Y = ((210 + ((i - this.int_5) * 50)) - 0x10) + ((int)(((double)(base.getFont("Texts").MeasureString("a").Y * 0.55f)) / 2.0));
            }
            base.CalculateBounds(bool_6);
        }

        public override void CalculateColors()
        {
            base.color_0 = Color.FromNonPremultiplied(0xff, 0xff, 0xff, base.int_2);
            this.color_3 = Color.FromNonPremultiplied(0xff, 0xff, 0xff, (int) (base.int_2 * 0.8f));
            base.color_1 = Color.FromNonPremultiplied(120, 120, 120, base.int_2);
            base.color_2 = Color.FromNonPremultiplied(50, 50, 50, base.int_2);
            this.color_4 = Color.FromNonPremultiplied(0, 0, 0, base.int_2 / 2);
            this.color_5 = Color.FromNonPremultiplied(0x1f, 160, 200, base.int_2);
            base.CalculateColors();
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            if (base.spriteBatch != null) {

                bool flag = this.int_7 < this.fileList.Count;
                base.spriteBatch.Draw(base.getTexture("white"), this.rectangle_2, null, this.color_4, 0f, Vector2.Zero, SpriteEffects.None, Depth.float_10);
                base.spriteBatch.Draw(base.getTexture("white"), this.rectangle_1, null, this.color_4, 0f, Vector2.Zero, SpriteEffects.None, Depth.float_8);
                base.spriteBatch.DrawString(base.getFont("TextsLight"), this.directoryInfo_0.Name, this.vector2_2, base.color_0, 0f, Vector2.Zero, (float) (0.9f * base.float_0), SpriteEffects.None, Depth.float_7);
                for (int i = this.int_5; i <= this.int_6; i++)
                {
                    FileSystemInfo info = this.fileList[i];
                    Texture2D texture = this.getFileTexture(info);
                    
                    string text = (this.method_12(info) ? "Up one level to " : "") + info.Name;
                    SpriteFont font = base.getFont("TextsLight");
                        if ((info is DirectoryInfo) && ((((DirectoryInfo) info).Parent == null) || this.method_12(info)))
                        {
                            font = base.getFont("Texts");
                        }
                    base.spriteBatch.DrawString(font, text, this.vector2_3[i], base.color_0, 0f, Vector2.Zero, (float) 0.55f, SpriteEffects.None, Depth.float_7);
                    Rectangle? sourceRectangle = null;
                    base.spriteBatch.Draw(texture, this.rectangle_6[i], sourceRectangle, base.color_0, 0f, Vector2.Zero, SpriteEffects.None, Depth.float_7);
                }
                base.spriteBatch.Draw(base.getTexture("white"), this.rectangle_3, null, this.color_5, 0f, Vector2.Zero, SpriteEffects.None, Depth.float_8);
                if (flag)
                {
                    base.spriteBatch.Draw(base.getTexture("white"), this.rectangle_4, null, base.color_2, 0f, Vector2.Zero, SpriteEffects.None, Depth.float_6);
                    base.spriteBatch.Draw(base.getTexture("white"), this.rectangle_5, null, base.color_1, 0f, Vector2.Zero, SpriteEffects.None, Depth.float_5);
                }
                base.Draw(gameTime);
            }
        }

        public override int GetPriority() {
            return Priority.int_2;
        }

        public Texture2D dirTexture(DirectoryInfo directoryInfo_1) {
            if (directoryInfo_1.Parent == null) {
                return base.getTexture("drive");
            }
            if (this.method_12(directoryInfo_1)) {
                return base.getTexture("folder_in");
            }
            return base.getTexture("folder");
        }

        public Texture2D fileTexture(FileInfo fileInfo_0) {
            string str;
            if (((str = fileInfo_0.Extension.ToLower()) != null) && (str == ".lnk")) {
                return base.getTexture("link_file");
            }
            return base.getTexture("document_empty");
        }

        private bool method_12(FileSystemInfo fileSystemInfo_0) {
            return ((this.directoryInfo_0.Parent != null) && fileSystemInfo_0.FullName.Equals(this.directoryInfo_0.Parent.FullName));
        }

        public bool openDirectory() {
           if (this.fileList[this.fl_pointer] is DirectoryInfo) {
                this.directoryInfo_0 = (DirectoryInfo) this.fileList[this.fl_pointer];
                this.setfileList();
            }
            else {
                this.Hide();
                if (this.bindfunc1 != null)
                {
                    this.bindfunc1();
                }
            }
            return true;
        }

        public bool defaultImage() {
            this.Hide();
            if (this.bindfunc1 != null) {
                this.bindfunc_setDefaultImage();
            }
            return true;
        }

        
        private bool method_15(FileInfo fileInfo_0) {
            return (this.fileTypes.Contains<string>(fileInfo_0.Extension.ToLower()) && ((fileInfo_0.Attributes & FileAttributes.Hidden) == 0));
        }

        private void setfileList() {
            Func<FileInfo, bool> predicate = null;
            try {
                this.fileList.Clear();
                foreach (DriveInfo info in DriveInfo.GetDrives()) {
                    if (info.DriveType == DriveType.Fixed) {
                        this.fileList.Add(info.RootDirectory);
                    }
                }
                if ((this.directoryInfo_0.Parent != null) && (this.directoryInfo_0.Parent.Parent != null)) {
                    this.fileList.Add(this.directoryInfo_0.Parent);
                }
                if (func_2 == null)
                {
                    func_2 = new Func<DirectoryInfo, bool>(FileChooser.smethod_0);
                }
                this.fileList.AddRange(this.directoryInfo_0.GetDirectories().Where<DirectoryInfo>(func_2));
                if (predicate == null) {
                    predicate = new Func<FileInfo, bool>(this.method_15);
                }
                this.fileList.AddRange(this.directoryInfo_0.EnumerateFiles().Where<FileInfo>(predicate));
                this.fl_pointer = 0;
                this.int_5 = 0;
                base.setTitle = true;
                this.vector2_3 = new Vector2[this.fileList.Count];
                this.rectangle_6 = new Rectangle[this.fileList.Count];
                for (int i = 0; i < this.fileList.Count; i++) {
                    this.vector2_3[i] = new Vector2(64f, 0f);
                    this.rectangle_6[i] = new Rectangle(0x10, 0, 0x20, 0x20);
                }
            }
            catch {
                this.directoryInfo_0 = new DirectoryInfo(this.string_3);
                this.setfileList();
            }
        }

        public FileInfo getfileInfo()
        {
            return (FileInfo) this.fileList[this.fl_pointer];
        }

        public Texture2D getFileTexture(FileSystemInfo fileInfo)
        {
            if (fileInfo is FileInfo)
            {
                return this.fileTexture((FileInfo) fileInfo);
            }
            return this.dirTexture((DirectoryInfo) fileInfo);
        }

        public override bool OnControllerAccept()
        {
            if (((base.buttons.Count > 0) && (base.buttons[base.buttonListIndex] != null)) && (base.buttons[base.buttonListIndex].bindFunction != null))
            {
                base.buttons[base.buttonListIndex].bindFunction();
            }
            return true;
        }

        public override bool OnControllerBack() {
            if (this.directoryInfo_0.Parent != null) {
                this.directoryInfo_0 = this.directoryInfo_0.Parent;
                this.setfileList();
                base.setTitle = true;
            }
            return true;
        }

        public override bool OnControllerDown() {
            if (this.fl_pointer < (this.fileList.Count - 1)) {
                this.fl_pointer++;
                if (this.fl_pointer > this.int_6)
                {
                    this.int_5++;
                }
                base.setTitle = true;
            }
            return true;
        }

        public override bool OnControllerLeft() {
            base.OnControllerLeft();
            return true;
        }

        public override bool OnControllerRight() {
            base.OnControllerRight();
            return true;
        }

        public override bool OnControllerUp() {
            if (this.fl_pointer > 0) {
                this.fl_pointer--;
                if (this.fl_pointer < this.int_5) {
                    this.int_5--;
                }
                base.setTitle = true;
            }
            return true;
        }

        public override void Show() {
            this.directoryInfo_0 = new DirectoryInfo(this.string_3);
            this.setfileList();
            base.Show();
        }

        private static bool smethod_0(DirectoryInfo directoryInfo_1) {
            return ((directoryInfo_1.Attributes & FileAttributes.Hidden) == 0);
        }

    }
}

