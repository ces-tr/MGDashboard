namespace MGDash
{
    using MGDash.Sources.Model;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    
    using System;
    using System.IO;
    using System.Net;
    using System.Threading;

    public sealed class Box : BaseClassDrawable
    {
        public bool coverExists;
        public bool bool_5;
        public bool bool_6;
        private bool bool_7;
        private Color color_0;
        private Color color_1;
        private Color color_2;
        public BoxOptionMenu GameOptions;
        public GameDetails GameDetails;
        public GamesSection GamesSection;
        public static readonly int int_0 = 20;
        private int int_1;
        private int int_2;
        private int int_3;
        private int int_4;
        public int int_5;
        private Rectangle rectangle_1;
        private Rectangle rectangle_2;
        private Rectangle rectangle_3;
        private Rectangle rectangle_4;
        private Rectangle rectangle_5;
        public Texture2D Cover;
        private Vector2 position;
        public Vector2 vector2_1;
        public readonly Vector2 vector2_2;
        public VideoGame VideoGame;
        public Case360 Case;
        public RenderTarget2D caseRender;

        public Box(Game _dashboard, VideoGame _videoGame, GamesSection _games, GameDetails _gdetails)
            : base(_dashboard)
        {
            this.Case = new Case360(_dashboard,this);
            this.vector2_2 = new Vector2(64f, 64f);
            this.VideoGame = _videoGame;
            this.GamesSection = _games;
            this.GameDetails = _gdetails;
            this.GameOptions = new BoxOptionMenu(_dashboard, this);
            this.int_5 = 0;
            this.coverExists = false;
            this.setColors();
            this.method_4();
            this.bool_7 = false;
            this.vector2_1 = new Vector2(256f, 0f);
            this.background = new Rectangle();
            this.rectangle_3 = new Rectangle(0, 0, 0, 1);
            this.rectangle_4 = new Rectangle(0, 0, 0, 1);
            this.rectangle_2 = new Rectangle();
            this.rectangle_1 = new Rectangle(0, 0, 0x80, 0x80);
            this.rectangle_5 = new Rectangle();

        }

        protected override void LoadContent() {
            this.setCover();
            this.caseRender = new RenderTarget2D(base.DashBoard.GraphicsDevice, this.DashBoard.width, base.DashBoard.heigth, false, SurfaceFormat.Color, DepthFormat.Depth24Stencil8);
        }

        public override void CalculateBounds(bool bool_8)
        {
            float num = ((float) this.int_5) / 100f;
            float num2 = (1f + num) - (((float) int_0) / 100f);
            this.background.X = (int)(this.position.X);//- ((this.vector2_1.X * num) / 2));
            this.background.Y = (int) (this.position.Y);// - ((this.vector2_1.Y * num) / 2));
            this.background.Width = 450;//(int) (this.vector2_1.X * (1f + num));
            this.background.Height = 450;//(int) (this.vector2_1.Y * (1f + num));
            this.rectangle_3.Width = this.background.Width;
            this.rectangle_3.X = this.background.X;
            this.rectangle_2 = base.background;
            this.rectangle_2.Inflate(15, 15);
            this.rectangle_5.X = this.background.X + ((int) Math.Round((double) (10f * num2)));
            this.rectangle_5.Y = this.background.Y - ((int) Math.Round((double) (7f * num2)));
            this.rectangle_5.Width = (int) Math.Round((double) (50f * num2));
            this.rectangle_5.Height = (int) Math.Round((double) (77f * num2));
            if (this.int_3 > 0) {
                this.rectangle_1.X = this.background.Center.X;
                if (bool_8)
                {
                    this.rectangle_1.Y = (int) (base.DashBoard.heigth * 0.5f);
                }
            }
            this.GameOptions.CalculateBounds(bool_8);
        }

        public override void CalculateColors() {
            this.int_3 = Math.Max(0, this.int_3 - 5);
            this.color_1 = Color.FromNonPremultiplied(0xff, 0xff, 0xff, this.int_3);
            if (this.Cover != null)
            {
                if (this.int_2 < 0xff)
                {
                    this.int_2 += 5;
                }
                else if (this.int_4 < 0xff)
                {
                    this.int_4 += 5;
                    this.color_2 = Color.FromNonPremultiplied(0, 0, 0, this.int_4);
                }
                else
                {
                    this.color_2 = Color.White;
                }
                int r = (int) ((this.VideoGame.linked ? ((float) 0xff) : ((float) ((int) (0.5f * this.int_2)))) * (1f - (this.GameOptions.float_0 * 0.5f)));
                this.color_0 = Color.FromNonPremultiplied(r, r, r, this.int_2);
            }
        }

        public override void Update(GameTime gameTime) {
            if (this.bool_5) {
                if (this.int_5 < int_0) {
                    this.int_5++;
                    base.setTitle = true;
                }
            }
            else if (this.int_5 > 0) {
                this.int_5--;
                base.setTitle = true;
            }
            if ((this.Cover != null) && (this.int_4 < 0xff)) {
                base.bool_1 = true;
            }
            if (this.GameOptions.method_4()) {
                base.bool_1 = ((this.GameOptions.int_0 % 100) != 0) || base.bool_1;
            }
            if (this.bool_6 || (this.bool_5 && this.GameOptions.method_4())) {
                this.GameOptions.Update(gameTime);
            }
            if (this.int_3 > 0) {
                this.int_1 += 8;
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime) {
            
            if (base.spriteBatch != null) {
                if (this.Cover != null) {
                    //if (base.dashBoard.config.user.shadows_for_cover) {
                        //base.spriteBatch.Draw(base.getTexture("cover_shadow"), this.rectangle_2, null, this.color_2, 0f, Vector2.Zero, SpriteEffects.None, Floats.float_49);
                    //}
                    
                    base.spriteBatch.Draw((Texture2D)this.caseRender, base.background, null, this.color_0, 0f, Vector2.Zero, SpriteEffects.None, this.Case.align != Case360.Align.Center ?   Depth.float_49 : Depth.float_48);
                    
                    //if (base.dashBoard.config.user.glow_for_covers) {
                        //base.spriteBatch.Draw(base.getTexture("cover_glow"), base.background, null, this.color_0, 0f, Vector2.Zero, SpriteEffects.None, Floats.float_47);
                    //}
                    //if (this.videoGame.favorite) {
                        //base.spriteBatch.Draw(base.getTexture("favorite"), this.rectangle_5, null, this.color_0, 0f, Vector2.Zero, SpriteEffects.None, Floats.float_42);
                    //}
                    //if (base.dashBoard.config.user.reflection_for_covers) {
                    //    for (int i = 0; i < (this.background.Height * 0.25); i++)
                    //    {
                    //        this.rectangle_3.Y = i + this.background.Bottom;
                    //        this.rectangle_4.Y = (int) (this.coverImage.Height * (1.0 - ((1.0 / ((double) this.background.Height)) * i)));
                    //        Color color = Color.FromNonPremultiplied(0xff, 0xff, 0xff, (int) ((this.int_2 * (1.0 - (((double) i) / (this.background.Height * 0.25)))) * 0.6));
                            //base.spriteBatch.Draw(this.portada, this.rectangle_3, new Rectangle?(this.rectangle_4), color, 0f, Vector2.Zero, SpriteEffects.None, Floats.float_50 - (this.bool_5 ? 0.001f : 0f));
                    //    }
                    //}
                }
                if ((this.Cover == null) || (this.int_3 > 0))
                {
                    base.spriteBatch.Draw(base.getTexture("loading_circle"), this.rectangle_1, null, this.color_1, MathHelper.ToRadians((float) this.int_1), this.vector2_2, SpriteEffects.None, Depth.float_48);
                }
                if (this.bool_6 || (this.bool_5 && this.GameOptions.method_4()))
                {
                    this.GameOptions.Draw(gameTime);
                }
            }

           

            base.Draw(gameTime);
        }

        public override void Hide()
        {
            base.Hide();
            this.bool_7 = true;
        }

        

        public void method_4()
        {
            base.Visible = false;
            base.Enabled = false;
            this.bool_5 = false;
            this.bool_6 = false;
            this.VideoGame.checklinked();
        }

        public void setColors()
        {
            this.int_1 = 0;
            this.int_3 = 0xff;
            this.color_1 = Color.White;
            this.int_2 = 0;
            this.color_0 = Color.Transparent;
            this.int_4 = 0;
            this.color_2 = Color.Transparent;
        }

        public void setCover()
        {
            new Thread(new ThreadStart(this.loadImage)) { IsBackground = true }.Start();
        }

        public bool changeImage()
        {
            try
            {
                FileInfo info = new FileInfo(this.VideoGame.getCover());
                if (info.Exists)
                {
                    info.Delete();
                    info = null;
                    this.Cover = null;
                }
                this.setCover();
            }
            catch
            {
            }
            return true;
        }

        public bool method_8(FileInfo _fileInfo)
        {
            try {
                _fileInfo.CopyTo(this.VideoGame.getCover(), true);
                this.setCover();
            }
            catch {
            }
            return true;
        }

        private void loadImage()
        {
            try
            {
                if (!new FileInfo(this.VideoGame.getCover()).Exists)
                {
                    this.coverExists = true;
                    this.GamesSection.Coverflow.setTitle = true;
                    this.setColors();
                    new WebClient().DownloadFile(this.VideoGame.method_0(), this.VideoGame.getCover());
                }
                FileStream stream = System.IO.File.OpenRead(this.VideoGame.getCover());
                this.Cover = Texture2D.FromStream(base.GraphicsDevice, stream);
                stream.Close();
               
            }
            catch
            {
                this.Cover = base.getTexture("no_cover");
            }
            finally
            {

                this.coverExists = false;
                this.vector2_1.X = 256f;
                this.vector2_1.Y = 450;//(int) ((this.vector2_1.X / ((float) this.coverImage.Width)) * this.coverImage.Height);
                this.GamesSection.Coverflow.setTitle = true;
                this.rectangle_4.Width = this.Cover.Width;
                
            }
        }

        public override void Show()
        {
            if (this.bool_7)
            {
                this.method_4();
                this.bool_7 = false;
            }
            this.GamesSection.Coverflow.setTitle = true;
            base.Show();
        }

       

      

        public Vector2 Position
        {
            get
            {
                return this.position;
            }
            set
            {
                this.position = value;
                base.setTitle = true;
                this.GameOptions.setTitle = true;
            }
        }
    }
}

