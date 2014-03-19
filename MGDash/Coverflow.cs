namespace MGDash
{
    using MGDash.Sources.Model;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Management;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows.Forms;
    using System.Text;

    public sealed class CoverFlow : BaseClassDrawable, IControllerEvents
    {
        private bool right;
        private bool left;
        private bool bool_6;
        private double pulsation;
        public GameDetails gameDetails;
        public GamesSection gamesSection;
        public CategorySlide CategorySlide;
        public Rate Rate;
        private int CenterCoverIndex;
        public Box CoverflowMiddleBox;
        public List<Box> CoverFlowItems;
        public List<Box> GameList;
        private List<int> CategorySlideList;

        public Predicate<Box> findGames;
        private Rectangle[] rectangle_1;
        private string notfoundTitle;
        private Vector2 TitlePosition;
        private Vector2 BoxPosition;
        private Vector2 vector2_2;
        public int PMLeft, PMRight;

        public List<int> Processes;

        public CoverFlow(Game _dashboard, GamesSection _gamesSection) : base(_dashboard)
        {
            this.gamesSection = _gamesSection;
            this.CoverFlowItems = new List<Box>();
            this.GameList = new List<Box>();
            this.Rate = new Rate(_dashboard);
            this.gameDetails = new GameDetails(_dashboard);
            
            foreach (VideoGame game in base.DashBoard.Settings.gameList) {
                
                this.GameList.Add(new Box(base.DashBoard, game, _gamesSection, this.gameDetails));
                
            }
            this.CoverFlowItems.AddRange(this.GameList);

            this.CategorySlide = new CategorySlide(base.DashBoard);
            this.BoxPosition = new Vector2();
            this.TitlePosition = new Vector2();
            this.vector2_2 = new Vector2();
            this.rectangle_1 = new Rectangle[5];
            this.notfoundTitle = "";

        }

        public override void CalculateBounds(bool bool_7)
        {
            if (this.CoverFlowItems.Count > 0) {

                PMLeft = Math.Max((this.CenterCoverIndex - (base.DashBoard.width / 0x90/*0x120*/)) + 1, 0);
                PMRight = Math.Min((this.CenterCoverIndex + (base.DashBoard.width / 0x90/*0x120*/)) + 1, this.CoverFlowItems.Count);
                int Pcenter = PMRight / 2;
                
                for (int i = 0; i < this.CoverFlowItems.Count; i++) {

                    this.CoverFlowItems[i].bool_5 = false;
                    
                    if ((i >= PMLeft) && (i < PMRight)) {
                        this.BoxPosition.X = ((((base.DashBoard.width / 2) + (i * 0x90/*0x120*/)) + ((float)this.pulsation)) - 144) + 16;
                        this.BoxPosition.Y = 128f + Math.Max((float) 0f, (float) ((((int) (base.DashBoard.heigth * 0.6f)) - (this.CoverFlowItems[i].vector2_1.Y + 80f)) + 16f));
                        this.CoverFlowItems[i].Position = this.BoxPosition;

                        this.CoverFlowItems[i].Visible = true;
                        this.CoverFlowItems[i].Enabled = true;
                    } 
                    else {
                        this.CoverFlowItems[i].Visible = false;
                        this.CoverFlowItems[i].Enabled = false;
                    }
                }
                
                this.TitlePosition.X = (base.DashBoard.width / 2) - ((base.getFont("TextsShadow").MeasureString(this.CoverflowMiddleBox.VideoGame.name).X * 0.9f) / 2);
                
                if (bool_7) {
                    this.TitlePosition.Y = (((int) (base.DashBoard.heigth * 0.65f)) + 100) - 10;
                    
                    for (int j = 0; j < 5; j++){
                        this.rectangle_1[j].X = (base.DashBoard.width / 2) + ((int) ((j - 2.5) * 52.0));
                        this.rectangle_1[j].Y = ((int) (base.DashBoard.heigth * 0.65f)) + 160;
                        this.rectangle_1[j].Width = 0x30;
                        this.rectangle_1[j].Height = 0x30;
                    }
                }
            }
            else {
                switch (this.gamesSection.options) {
                    case GamesSection.GamesMenu.All:
                        this.notfoundTitle = "Add your games at www.gamecher.com";
                        break;

                    case GamesSection.GamesMenu.Favorites:
                        this.notfoundTitle = "You don't have any favorite game";
                        break;

                    case GamesSection.GamesMenu.Installed:
                        this.notfoundTitle = "You don't have any linked game";
                        break;
                }
                this.vector2_2.X = (base.DashBoard.width / 2) - ((base.getFont("TextsShadow").MeasureString(this.notfoundTitle).X * 0.9f) / 2f);
                this.vector2_2.Y = base.DashBoard.heigth / 2;
            }
        }

        public override void Update(GameTime gameTime) {

            if (this.CoverFlowItems.Count > 0) {
                
                if (this.right) {
                    this.pulsation -= 24;//32.0;

                    this.right = !((this.pulsation % 144/*288.0*/) == 0.0);
                    base.setTitle = true;
                    //Console.WriteLine("*right" + this.pulse + "  " + this.pulse % 160/*288.0*/);
                }
                if (this.left) {

                    this.pulsation += 24;// 32.0;
                    this.left = !((this.pulsation % 144/*288.0*/) == 0.0);
                    base.setTitle = true;
                    //Console.WriteLine("*left" + this.pulse + "  " + this.pulse % 160/*288.0*/);
                }

                int nextposition = (int)(-this.pulsation / 144/*288.0*/);

                //UPdat moving covers
                if (this.CenterCoverIndex < nextposition) {

                    this.CoverflowMiddleBox.Case.align = Case3D.Align.Left;
                }
                else if (this.CenterCoverIndex > nextposition) {

                    this.CoverflowMiddleBox.Case.align = Case3D.Align.Rigth;
                }
                else if (this.CoverflowMiddleBox.Case.align != Case3D.Align.Center) {
                    this.CoverflowMiddleBox.Case.align = Case3D.Align.Center;
                    
                    //Console.WriteLine("*"+this.centerCover.videoGame.name);

                }

                this.CenterCoverIndex = nextposition;
                this.CoverflowMiddleBox = this.CoverFlowItems[this.CenterCoverIndex];
                this.CoverflowMiddleBox.Case.leftside = true;

                if ((this.CenterCoverIndex >= 0) && (this.CenterCoverIndex < this.CoverFlowItems.Count)) {
                    this.CoverflowMiddleBox.bool_5 = true;
                }
            }
         base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime) {
            
            if (base.spriteBatch != null) {
                
                if (this.CoverFlowItems.Count > 0) {

                    base.spriteBatch.DrawString(base.getFont("TextsShadow"), this.CoverflowMiddleBox.VideoGame.name, this.TitlePosition, Color.White, 0f, Vector2.Zero, (float) 0.9f, SpriteEffects.None, Depth.float_41);
                    
                    for (int i = 0; i < 5; i++) {
                        Rectangle? sourceRectangle = null;
                        base.spriteBatch.Draw((i < this.CoverflowMiddleBox.VideoGame.rating) ? base.getTexture("star_white") : base.getTexture("star_gray"), this.rectangle_1[i], sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, Depth.float_41);
                    }
                }
                else {
                    base.spriteBatch.DrawString(base.getFont("TextsShadow"), this.notfoundTitle, this.vector2_2, Color.White, 0f, Vector2.Zero, (float)0.9f, SpriteEffects.None, Depth.float_41);
                }
            }

        base.Draw(gameTime);
        }

        public int GetPriority() {
            return Priority.int_6;
        }

        public override void Hide() {
            base.Hide();
            this.bool_6 = true;
            
            foreach (Box cover in this.CoverFlowItems) {
                base.DashBoard.Components.Remove(cover);
            }
        }

        private int getProcess(int int_1) {
            using (ManagementObject obj2 = new ManagementObject("win32_process.handle='" + int_1.ToString() + "'"))
            {
                obj2.Get();
                return Convert.ToInt32(obj2["ParentProcessId"]);
            }
        }

       public void HandleSomethingHappened()
        {
            MessageBox.Show("ha terminado");
        }

        public bool launchGame()
        {
            if (this.CoverflowMiddleBox != null)
            {
                //Form form = (Form)Control.FromHandle(base.Game.Window.Handle);
                //form.Visible = false;
                bool fullscreen = false;
                if (base.GraphicsDevice.PresentationParameters.IsFullScreen)
                {
                    base.DashBoard.toggleFullScreen();
                    fullscreen = true;
                }

                try
                {
                    List<int> processes = this.getProcesslist();
                    FileInfo fileInfo = new FileInfo(this.CoverflowMiddleBox.VideoGame.path);
                    
                    ProcessStartInfo processStartInfo = new ProcessStartInfo(fileInfo.FullName)
                    {
                        WorkingDirectory = fileInfo.Directory.FullName
                  
                    };
                    Process processById;// = Process.Start(processStartInfo);

                    ThreadPool.QueueUserWorkItem(delegate
                    {
                        //processById.Exited += new EventHandler(HandleSomethingHappened);
                        processById = Process.Start(processStartInfo);
                        //processById.WaitForExit();
                        HandleSomethingHappened();
                        

                        
                    });
                    
                    
                    if (fileInfo.Extension.Equals(".url"))
                    {
                        int num = 0;
                        bool flag1 = false;
                        do
                        {
                            List<int> nums1 = this.getProcesslist();
                            if (nums1.Count != processes.Count)
                            {
                                List<int> nums2 = new List<int>();
                                nums2.AddRange(nums1);
                                try
                                {
                                    nums2.RemoveAll((int int_0) => processes.Contains(int_0));
                                    nums2.RemoveAll((int int_1) => Process.GetProcessById(int_1).ProcessName.Equals("Steam"));
                                    nums2.RemoveAll((int int_1) => Process.GetProcessById(int_1).ProcessName.Equals("x64launcher"));
                                    nums2.RemoveAll((int int_1) => Process.GetProcessById(int_1).ProcessName.Equals("GameOverlayUI"));
                                    nums2.RemoveAll((int int_1) => Process.GetProcessById(int_1).ProcessName.Equals("SteamService"));
                                    nums2.RemoveAll((int int_1) => Process.GetProcessById(int_1).ProcessName.Equals("WmiPrvSE"));
                                    bool count = nums2.Count > 0;
                                    flag1 = count;
                                    if (count)
                                    {
                                        processById = Process.GetProcessById(nums2[0]);
                                    }
                                }
                                catch
                                {
                                    Console.WriteLine("noo");
                                }
                            }
                            num++;
                            Thread.Sleep(50);
                            if (flag1)
                            {
                                //goto Label0;
                                break;
                            }
                        }
                        while (num < 2400);
                    }
                //Label0:
                    //if (processById == null) {
                        //MessageBox.Show("The launcher couldn't wait for the game to end. Game might not be supported.");
                    //}
                    //else {
                    //    processById.WaitForInputIdle();
                     //   processById.WaitForExit();
                    //}
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                finally
                {
                    //form.Visible = true;
                    if (fullscreen && !base.GraphicsDevice.PresentationParameters.IsFullScreen)
                    {
                        base.DashBoard.toggleFullScreen();
                    }
                    //form.Visible = true;
                    //form.Activate();
                }
            }
            return true;
        }

        public List<int> getProcesslist()
        {
           /* List<int> list = new List<int>();
            foreach (Process process in Process.GetProcesses()) { list.Add(process.Id);} return list;*/
            
            List<int> nums = new List<int>();
            Process[] processes = Process.GetProcesses();
            for (int i = 0; i < (int)processes.Length; i++)
            {
                nums.Add(processes[i].Id);
            }
            return nums;

        }

        public bool useDefaultCover() {
            this.CoverflowMiddleBox.changeImage();
            return true;
        }

        public bool method_14() {
            this.CoverflowMiddleBox.method_8(this.gamesSection.FileChooser2.getfileInfo());
            return true;
        }

        public void method_4() {
            
            this.CoverFlowItems.Clear();
            this.CoverFlowItems.AddRange(this.GameList.FindAll(this.findGames));
            
            if (this.CoverFlowItems.Count > 0) {
                
                this.setCenterCover();
                foreach (Box cover in this.CoverFlowItems) {
                    cover.int_5 = 0;
                }
               
                this.CoverflowMiddleBox.int_5 = Box.int_0;
            }
            this.method_7();
            this.CategorySlide.Init();
        }

        private void setCenterCover() {
            
            this.CenterCoverIndex =  this.CoverFlowItems.Count / 2;
            this.CoverflowMiddleBox = (this.CoverFlowItems.Count > 0) ? this.CoverFlowItems[this.CenterCoverIndex] : null;
            this.pulsation = -this.CenterCoverIndex * 0x90/*0x120*/;
            this.AlignCovers();
            //this.gamesSection.setVideo(this.centerCover.videoGame.name);
            
        }

        public void method_6(int int_1) {
            
            Predicate<Box> match = null;
            this.idCat = int_1;
                                
            foreach (Box class2 in this.GameList) {
                class2.Hide();
            }
            
            this.CoverFlowItems.Clear();
            
            if (this.idCat != -1) {
                if (match == null) {
                    match = new Predicate<Box>(this.findCategory);
                }
                this.CoverFlowItems.AddRange(this.GameList.FindAll(match).FindAll(this.findGames));
            }
            else {
                this.CoverFlowItems.AddRange(this.GameList.FindAll(this.findGames));
            }
            
            this.setCenterCover();
            
            foreach(Box cover in this.CoverFlowItems) {
                cover.int_5 = 0;
                cover.Show();
            }
           
            this.method_7();
            base.setTitle = true;
        }

        public void method_7() {
            this.CategorySlideList = new List<int>();
            this.CategorySlideList.Add(-1);
            
            foreach (Box cover in this.GameList.FindAll(this.findGames)) {
                
                foreach (int idCat in cover.VideoGame.categories)
                {
                    if (!this.CategorySlideList.Contains(idCat))
                    {
                        this.CategorySlideList.Add(idCat);
                    }
                }
            }
           
         this.CategorySlide.list_0 = this.CategorySlideList;
        }

        public bool method_8() {
            if (!this.left) {
                return this.right;
            }
            return true;
        }

        private List<int> method_9(int int_1) {
            
            Process[] processes = Process.GetProcesses();
            List<int> list = new List<int>();
            
            for (int i = 0; i < processes.Length; i++) {
                
                if (this.getProcess(processes[i].Id) == int_1){
                    list.Add(processes[i].Id);
                }
            }
         return list;
        }

        public bool OnControllerAccept() {
            if (!this.method_8() && (this.CoverFlowItems.Count > 0)) {
                
                if (this.CoverflowMiddleBox.VideoGame.linked)
                {
                    this.gamesSection.MsglaunchGame.Title = this.CoverflowMiddleBox.VideoGame.name;
                    this.gamesSection.MsglaunchGame.Show();
                }
                else
                {
                    this.gamesSection.MsglinkGame.Title = this.CoverflowMiddleBox.VideoGame.name;
                    this.gamesSection.MsglinkGame.Show();
                }
            }
            return true;
        }

        public bool OnControllerBack() {
            this.CategorySlide.bool_3 = false;
            base.DashBoard.initializeComponents.titleBar.clearDictionaries();
            base.DashBoard.initializeComponents.titleBar.AppendControllerIcon("A", "open");
            base.DashBoard.initializeComponents.titleBar.AppendControllerIcon("B", "back");
            return true;
        }

        public bool OnControllerDetails() {
            if (!this.method_8() && (this.CoverflowMiddleBox != null)) {
                this.CoverflowMiddleBox.GameOptions.Show();
                base.DashBoard.initializeComponents.titleBar.clearDictionaries();
                base.DashBoard.initializeComponents.titleBar.AppendControllerIcon("A", "accept");
                base.DashBoard.initializeComponents.titleBar.AppendControllerIcon("B", "close details");
            }
            return true;
        }

        public bool OnControllerDown() {
            if (!this.method_8() && !this.CategorySlide.method_5())
            {
                this.CategorySlide.int_3 = 100;
                if (this.CategorySlide.CatListCount < (this.CategorySlideList.Count - 1))
                {
                    this.CategorySlide.bool_5 = true;
                    this.method_6(this.CategorySlideList[this.CategorySlide.CatListCount + 1]);
                }
            }
            return true;
        }

        public bool OnControllerLeft() {
            if (!this.method_8() && (this.CenterCoverIndex > 0))
            {
                this.left = true;
            }
            return true;
        }

        public bool OnControllerRight() {
            if (!this.method_8() && (this.CenterCoverIndex < (this.CoverFlowItems.Count - 1))) {
                this.right = true;
            }
            return true;
        }

        public bool OnControllerSort() {
            return true;
        }

        public bool OnControllerUp() {
            if (!this.method_8() && !this.CategorySlide.method_5())
            {
                this.CategorySlide.int_3 = 100;
                if (this.CategorySlide.CatListCount > 0)
                {
                    this.CategorySlide.bool_4 = true;
                    this.method_6(this.CategorySlideList[this.CategorySlide.CatListCount - 1]);
                }
            }
            return true;
        }

        public override void Show() {
            if (this.bool_6) {
                this.method_4();
                this.bool_6 = false;
            }
            base.DashBoard.initializeComponents.titleBar.clearDictionaries();
            base.DashBoard.initializeComponents.titleBar.AppendControllerIcon("A", "launch");
            base.DashBoard.initializeComponents.titleBar.AppendControllerIcon("B", "back");
            base.DashBoard.initializeComponents.titleBar.AppendControllerIcon("Y", "details");
            base.DashBoard.initializeComponents.titleBar.AppendControllerIcon("DPADV", "change category");
            
            base.Show();
        }

        public void AlignCovers() {

            int total = this.CoverFlowItems.Count;
            
            for (int i = 0; i < total; i++) {
                this.CoverFlowItems[i].Case.align =  i <= (total / 2) ? Case3D.Align.Left : Case3D.Align.Rigth;
            }
        }
        
        
            public int idCat;

            public bool findCategory(Box cover) {
                return cover.VideoGame.categories.Contains(this.idCat);
            }
        
        
            public bool findProcess(int int_0) {
                return this.Processes.Contains(int_0);
            }
       
    }
}

