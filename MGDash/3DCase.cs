using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MGDash.Sources.Model;

namespace MGDash
{
    public class Case3D : BaseClassDrawable 
    {
        private Model _model;
        private Matrix[] _boneTransforms;
        public int coverflowposition = 0;
        public bool leftside,rigthside,center = false;
        public Matrix view, proj, worldbones, scale;
        
        double CurAnglePosition = 0;
        
        public Box Box;
        public int savestate=0;
        
        public Align align;
        public float ZAxisTranslation = 0;
        
        public float RAlignDegrees = -MathHelper.ToRadians(70f);
        public float LAlignDegrees = MathHelper.ToRadians(70f);
        
        public Case3D(Game game, Box _cover) : base(game) {
            
            this.Box = _cover;
        }

        protected override void LoadContent() {
           
            this._model = base.getModel("case3D");
            this.initialize();  
        }
        
        public void initialize() {
            
            this._boneTransforms = new Matrix[this._model.Bones.Count];
            this.view = Matrix.CreateLookAt(new Vector3(0, 4, 12), new Vector3(0, 4, -5), Vector3.Up);
            this.proj = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4,
                                       1.0f,//GraphicsDevice.Viewport.Width / GraphicsDevice.Viewport.Height,
                                       1f, 1000.0f);
            this.scale = Matrix.CreateScale(0.5f);
            
        }

        public override void Update(GameTime gameTime) {
          
            // Calculate the new position of the forks.
            //float time = (float)gameTime.TotalGameTime.TotalSeconds;
            //float sine = MathHelper.Clamp((float)(Math.Sin(angle) * Math.Sin(time)), 0, 1) * 100f;
            //float cosine = MathHelper.Clamp((float)(Math.Cos(angle) * Math.Sin(time)), 0, 1) * 100f;
            
            switch(this.align) {
           
                case (Align.Rigth  ) :
                    this.ZAxisTranslation -= (this.ZAxisTranslation > 0) ?  0.1f :0f;
                    
                    if (this.CurAnglePosition >= RAlignDegrees)
                    {
                        this.worldbones = Matrix.CreateRotationY((float)this.CurAnglePosition) * Matrix.CreateTranslation(new Vector3(0f, 0f, this.ZAxisTranslation));
                        this.CurAnglePosition -= 0.065;
                      
                    }
                    
                return;
            
                case (Align.Left) :
                    this.ZAxisTranslation -= (this.ZAxisTranslation > 0) ? 0.1f : 0f;
                        if (this.CurAnglePosition <= LAlignDegrees)
                        {
                            this.worldbones = Matrix.CreateRotationY((float)this.CurAnglePosition) * Matrix.CreateTranslation(new Vector3(0f, 0f, this.ZAxisTranslation));
                            this.CurAnglePosition += 0.065;
                            
                        }
            
                    return;
                
                default : //align to center
                    this.ZAxisTranslation += (this.ZAxisTranslation > 2f) ? 0f : 0.1f;

                    if (this.CurAnglePosition > 0.1) {

                        this.worldbones = Matrix.CreateRotationY((float)this.CurAnglePosition) * Matrix.CreateTranslation(new Vector3(0f, 0f, this.ZAxisTranslation));
                        this.CurAnglePosition -= 0.065;
                    }
                    else if (CurAnglePosition < -0.1) {

                        this.worldbones = Matrix.CreateRotationY((float)this.CurAnglePosition) * Matrix.CreateTranslation(new Vector3(0f, 0f, this.ZAxisTranslation));
                        this.CurAnglePosition += 0.065;
                    }
                    else {
                        this.CurAnglePosition = 0;
                        //this.translateval += (this.translateval > 1.0f) ? 0f : 0.1f;
                        this.worldbones = Matrix.CreateRotationY((float)this.CurAnglePosition) * Matrix.CreateTranslation(new Vector3(0f, 0f, this.ZAxisTranslation));  //translate case in Z Axis
                        
                    }
                    return;
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            this._model.CopyAbsoluteBoneTransformsTo(_boneTransforms);
            base.GraphicsDevice.SetRenderTarget(Box.caseRender);
            base.GraphicsDevice.Clear(Color.Transparent);
            base.GraphicsDevice.BlendState = BlendState.AlphaBlend;
            //GraphicsDevice.RasterizerState = RasterizerState.CullCounterClockwise; //backface culling
            base.GraphicsDevice.DepthStencilState = DepthStencilState.Default; 

            foreach (ModelMesh mesh in this._model.Meshes) {
              foreach (BasicEffect mesheffect in mesh.Effects)
                {
                    mesheffect.Parameters["Texture"].SetValue(this.Box.Cover);
                    mesheffect.EnableDefaultLighting();

                    mesheffect.View = view;
                    mesheffect.Projection = proj;
                    mesheffect.World = scale * _boneTransforms[mesh.ParentBone.Index] * worldbones; 

                   mesheffect.AmbientLightColor = new Vector3(0.5f, 0.5f, 0.5f);
                    //mesheffect.DiffuseColor = new Vector3(1.0f, 1.0f, 1.0f);
                    //mesheffect.SpecularColor = new Vector3(0.25f, 0.25f, 0.25f);
                    //mesheffect.SpecularPower = 5.0f;
                    //basicEffect.Alpha = 1.0f;

                    mesheffect.LightingEnabled = false;
                    if (mesheffect.LightingEnabled) {
                        mesheffect.DirectionalLight0.Enabled = false; // enable each light individually
                        if (mesheffect.DirectionalLight0.Enabled) {
                            // x direction
                            mesheffect.DirectionalLight0.DiffuseColor = new Vector3(0.5f, 0.5f, 0.5f); // range is 0 to 1
                            mesheffect.DirectionalLight0.Direction = Vector3.Normalize(new Vector3(1, 0, 0));
                            // points from the light to the origin of the scene
                            mesheffect.DirectionalLight0.SpecularColor = Vector3.Normalize(new Vector3(0.8f, 0.8f, 0.8f));
                            
                        }

                        mesheffect.DirectionalLight1.Enabled = true;
                        if (mesheffect.DirectionalLight1.Enabled) {
                            // y direction
                            mesheffect.DirectionalLight1.DiffuseColor = new Vector3(0.9f, 0.9f, 0.9f);
                            mesheffect.DirectionalLight1.Direction = Vector3.Normalize(new Vector3(0, 0, -1f));
                            mesheffect.DirectionalLight1.SpecularColor = Vector3.Normalize(new Vector3(0.5f, 0.5f, 0.5f));
                        }

                        mesheffect.DirectionalLight2.Enabled = false;
                        if (mesheffect.DirectionalLight2.Enabled) {
                            // z direction
                            mesheffect.DirectionalLight2.DiffuseColor = new Vector3(0, 0, 0.5f);
                            mesheffect.DirectionalLight2.Direction = Vector3.Normalize(new Vector3(0, 0, -1));
                            mesheffect.DirectionalLight2.SpecularColor = Vector3.One;
                        }
                    }

                }
               
                mesh.Draw();
            }
            
            base.GraphicsDevice.SetRenderTarget(null);
            base.GraphicsDevice.SetRenderTarget(base.DashBoard.renderTarget2D);
            
            base.Draw(gameTime);
        }

        public enum Align {
            Left,Rigth,Center

        };

        
    }
}

