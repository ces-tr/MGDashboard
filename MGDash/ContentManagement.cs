using System;
using System.IO;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace MGDash {

    public sealed class ContentManagement : BaseClassDrawable {
        public Dictionary<string, SpriteFont> Fonts;
        public Dictionary<string, Texture2D> Textures;
        public Dictionary<string, Texture2D> Images;
        public Dictionary<string, Model> Models;
        public Dictionary<string, Video> Videos;
        public DashBoard dashboard;

        public ContentManagement(Game _dashboard) : base(_dashboard) {
        }

        protected override void LoadContent() {
            this.Textures = this.LoadContent<Texture2D>(base.Game.Content, "Textures");
            this.Fonts = this.LoadContent<SpriteFont>(base.Game.Content, "Fonts");
            
            foreach (SpriteFont font in this.Fonts.Values) {
                font.DefaultCharacter = '~';
            }
            this.Images = new Dictionary<string, Texture2D>();
            ///**** SET FONTS SPACING////
            base.getFont("TextsShadow").Spacing = -3f;
            base.getFont("TextsFullShadow").Spacing = -32f;
            base.getFont("TextsLight").Spacing = 2.5f;
            base.getFont("Texts").Spacing = 3f;
            base.getFont("Titles").Spacing = 4f;
            base.getFont("TitlesShadow").Spacing = -15f;
            base.getFont("TitlesBig").Spacing = -4f;
            
            ///**** SET MONOGAMECHER IMAGES////
            this.setImage("", "user_pic");
            this.setImage(base.DashBoard.Settings.user.user_bg, "user_bg");
            this.setImage(base.DashBoard.Settings.user.games_bg, "games_bg");
            this.setImage(base.DashBoard.Settings.user.games_menu_bg, "games_menu_bg");
            this.setImage(base.DashBoard.Settings.user.music_menu_bg, "music_menu_bg");
            this.setImage(base.DashBoard.Settings.user.videos_menu_bg, "videos_menu_bg");
            this.setImage(base.DashBoard.Settings.user.social_menu_bg, "social_menu_bg");
            this.setImage(base.DashBoard.Settings.user.options_menu_bg, "options_menu_bg");
            
            //*****SET 3D MODELS******
            this.Models = new Dictionary<string, Model>();
            this.Models = this.LoadContent<Model>(base.Game.Content, "Models");
            
            //LOAD VIDEOS
            //this.Videos = new Dictionary<string, Video>();
            //this.Videos = this.LoadContent<Video>(base.Game.Content, "Videos");
            
         }

        private Dictionary<string, T> LoadContent<T>(ContentManager contentManager, string contentfolder) {
            DirectoryInfo info = new DirectoryInfo(contentManager.RootDirectory + @"\" + contentfolder);
            if (!info.Exists) {
                throw new DirectoryNotFoundException();
            }
            Dictionary<string, T> dictionary = new Dictionary<string, T>();
            
            foreach (FileInfo info2 in info.GetFiles("*.*")) {
               string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(info2.Name);
               dictionary[fileNameWithoutExtension] = contentManager.Load<T>(contentfolder + "/" + fileNameWithoutExtension);
              
            }
            return dictionary;
        }

        public bool setImage(string userimage, string textureimage) {
            try {
                if (userimage != null) {
                    using (FileStream stream = File.OpenRead(userimage)) {
                        this.Images[textureimage] = Texture2D.FromStream(base.GraphicsDevice, stream);
                        stream.Close();
                        return true;
                    }
                }
                throw new Exception("dashboard background path can't be null");
            }
            catch {
                this.Images[textureimage] = base.getTexture(textureimage);
            }
        
            return true;
        }

        public void setModel(){
        }
    }
}

