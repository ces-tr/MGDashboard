using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System;
using System.Runtime.CompilerServices;

namespace MGDash
{
    public sealed class GamesSection : BaseClassDrawable {
        
        public CoverFlow Coverflow;
        public Message MsglaunchGame;
        public Message MsglinkGame;
        public Message MsgChangeCover;
        public FileChooser FileChooser1;
        public FileChooser FileChooser2;
        public GamesMenu options;
        //Video video;
        //public VideoPlayer videoplayer;
        Model TvBack;
        public Matrix[] _boneTransforms;
        Matrix view, proj, wbones;
        BasicEffect tvtexture;

        public GamesSection(Game _dashboard) : base(_dashboard) {

            this.Coverflow = new CoverFlow(_dashboard, this);
            
            this.MsglaunchGame = new Message(_dashboard, true, false);
            this.MsglaunchGame.append("Launch", ButtonControl.Options.Accept, new Func<bool>(this.Coverflow.launchGame));
            this.MsglaunchGame.Text = "Do you want to launch this game?";
           
            this.MsglinkGame = new Message(_dashboard, false, true);
            this.MsglinkGame.Text = "Is not linked to any file";
            this.MsglinkGame.append("Link", ButtonControl.Options.Adhoc, new Func<bool>(this.linkGame));
            
            this.MsgChangeCover = new Message(_dashboard, true, false);
            this.MsgChangeCover.Text = "Select an option to change box art";
            this.MsgChangeCover.append("Download default", ButtonControl.Options.Adhoc, new Func<bool>(this.Coverflow.useDefaultCover));
            this.MsgChangeCover.append("Choose file", ButtonControl.Options.Adhoc, new Func<bool>(this.editGameboxart));
            
            this.FileChooser1 = new FileChooser(_dashboard, Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), new string[] { ".lnk", ".exe", ".bat", ".url" }, false);
            this.FileChooser2 = new FileChooser(_dashboard, Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), new string[] { ".png", ".jpg", ".jpeg" }, false);

           
            //this.videoplayer = new VideoPlayer();
            //this.videoplayer.IsLooped = true;

           
        }

        protected override void LoadContent() {
            //this.video = this.dashboard.Content.Load<Video>(@"Videos\test");
            this.TvBack = base.getModel("tvback");
            
            this.tvtexture = (BasicEffect)this.TvBack.Meshes[195].MeshParts[0].Effect;
            this._boneTransforms = new Matrix[this.TvBack.Bones.Count];
            
            view = Matrix.CreateLookAt(new Vector3(0, 2.5f, 7), new Vector3(0, 2.7f, -5), Vector3.Up);
            //view = Matrix.CreateLookAt(new Vector3(4.5F, 3.5f, 5.5f), new Vector3(-1F, 2f, -2), Vector3.Up);
            proj = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4,
                                       1.0f, //GraphicsDevice.Viewport.Width / GraphicsDevice.Viewport.Height,
                                       1f, 1000.0f);
            this.wbones = Matrix.CreateRotationY(-MathHelper.ToRadians(12.5f)) * Matrix.CreateRotationZ(MathHelper.ToRadians(1.0f));
            /*
            this.tvbackrender = new RenderTarget2D(this.dashBoard.GraphicsDevice, this.dashBoard.width, this.dashBoard.heigth, false, SurfaceFormat.Color, DepthFormat.Depth24Stencil8);
            TvBacktexture = new Texture2D(GraphicsDevice, videoplayer.GetTexture().Width, videoplayer.GetTexture().Height, false, SurfaceFormat.Color);
            PresentationParameters pp = GraphicsDevice.PresentationParameters;
            this.tvbackrender = new RenderTarget2D(GraphicsDevice, pp.BackBufferWidth, pp.BackBufferHeight, false, pp.BackBufferFormat, pp.DepthStencilFormat, pp.MultiSampleCount,
                                   RenderTargetUsage.DiscardContents);
            foreach (ModelMesh mesh in this.TvBack.Meshes){
              foreach (ModelMeshPart meshpart in mesh.MeshParts){   
              BasicEffect effect = (BasicEffect)meshpart.Effect;
                }
            }*/

        }

        public override void Update(GameTime gameTime) {

            
            float time = (float)gameTime.TotalGameTime.TotalSeconds;
            //float sine = MathHelper.Clamp((float)(Math.Sin(time)), 0, 1);//* 100f;
            //this.wbones = Matrix.CreateRotationY(time * 0.5f);
           // if (videoplayer.State == MediaState.Stopped) {
           //     videoplayer.Play(video);
           // }
            
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime) {
  
            this.TvBack.CopyAbsoluteBoneTransformsTo(_boneTransforms);
            GraphicsDevice.Clear(Color.White);
            GraphicsDevice.BlendState = BlendState.AlphaBlend;
            GraphicsDevice.SamplerStates[0] = SamplerState.LinearWrap;
            base.GraphicsDevice.DepthStencilState = DepthStencilState.Default;

            foreach (ModelMesh mesh in this.TvBack.Meshes) {
                foreach (BasicEffect mesheffect in mesh.Effects) {
                    mesheffect.View = view;
                    mesheffect.Projection = proj;
                    mesheffect.World = _boneTransforms[mesh.ParentBone.Index];// *wbones;
                    mesheffect.EnableDefaultLighting();
                    
                }
            mesh.Draw();
            }
            try{
                tvtexture.Texture = Coverflow.CoverflowMiddleBox.Cover;//videoplayer.GetTexture();
            }
                catch{}
           base.Draw(gameTime);
        }

        public void checkGames(GamesMenu _gamesOptions) {
            
            this.options = _gamesOptions;
            
            switch (_gamesOptions) {
                case GamesMenu.All:

                    this.Coverflow.findGames = (Box Box) => true;
                    return;

                case GamesMenu.Favorites:

                    this.Coverflow.findGames = (Box Box) => Box.VideoGame.favorite;
                    return;

                case GamesMenu.Installed:
                    
                    this.Coverflow.findGames = (Box Box) => Box.VideoGame.linked;
                    return;
            }
        }

        public bool linkGame() {
            this.FileChooser1.Title = "Linking " + this.Coverflow.CoverflowMiddleBox.VideoGame.name;
            this.FileChooser1.bindfunc1 = new Func<bool>(this.Coverflow.CoverflowMiddleBox.GameOptions.link);
            this.FileChooser1.Show();
            return true;
        }

        public bool editGameboxart() {
            this.FileChooser2.Title = "Editing box art for " + this.Coverflow.CoverflowMiddleBox.VideoGame.name;
            this.FileChooser2.bindfunc1 = new Func<bool>(this.Coverflow.method_14);
            this.FileChooser2.Show();
            return true;
        }

        public void setVideo(string videoname) {
            //try {
                //this.videoplayer.Stop();
                //this.video = base.getVideo(videoname);//this.dashboard.Content.Load<Video>(@"Videos\test");
                //Console.WriteLine(videoname);
            //}
            //catch(Exception){}
        }

        public enum GamesMenu {
            All,
            Favorites,
            Installed
        }
    }
}

