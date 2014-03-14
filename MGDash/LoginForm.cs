namespace MGDash
{
    
    using System;
    
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Threading;
    using System.Windows.Forms;
    using System.Reflection;
    //using MonoGamecher.Properties;
   

    public class Login : Form
    {
        private Button button_0;
        private CheckBox remember, offlinemode;
        public HttpConnect HttpConnection = new HttpConnect();
        private IContainer icontainer_0;
        private Label label_0;
        private Label label_1;
        private Label label_2;
        private Label label_3;
        private LinkLabel linkLabel_0;
        private LinkLabel linkLabel_1;
        private Panel panel1;
        private Panel panel2;
        private PictureBox pictureBox_0;
        private TextBox password;
        private TextBox username;
        public Version version_0;

        //delegate void DType();

        public Login()
        {
            this.Initialize();
            
            var assembly = Assembly.GetExecutingAssembly();

            /*foreach (var resourceName in assembly.GetManifestResourceNames()) {
                Console.WriteLine(resourceName);
            }*/
           
            System.IO.Stream file = assembly.GetManifestResourceStream("Resources.logo.png");
            this.pictureBox_0.BackgroundImage = Image.FromStream(file);

            this.pictureBox_0.BackgroundImageLayout = ImageLayout.Center;
        }

        private void signInBtn_Click(object sender, EventArgs e)
        {
            this.login();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(disposing);
        }

        private void LoginForm_ONLoad(object sender, EventArgs e)
        {
            this.setAplicationDirectories();
            
            if (this.HttpConnection.isAuntenticated())
            {
                this.InitializeDashboard();
                return;
            }
            //else {
               // MessageBox.Show("Server unreachable. Check your internet connection or", "Connection timeout", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            //}
        }

        private void setAplicationDirectories() {
            
            try {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + @"\Gamecher\data\boxart\original\front";
                if (!Directory.Exists(path)) {
                    try {
                        Directory.CreateDirectory(path);
                    }
                    catch (IOException exception) {
                        MessageBox.Show("There was an error while trying to create your links folder:" + exception.Message, "Configuration error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
                }
                path = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + @"\Gamecher\data\links";
                if (!Directory.Exists(path)) {
                    try {
                        Directory.CreateDirectory(path);
                    }
                    catch (IOException exception2) {
                        MessageBox.Show("There was an error while trying to create your links folder:" + exception2.Message, "Configuration error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
                }
            }
            catch (Exception exception3) {
                MessageBox.Show(exception3.StackTrace);
            }
        }

       public void InitializeDashboard()
        {
            if (this.HttpConnection.Autenticated || offlinemode.Checked==true)
            {
                base.Close();
                new Thread(new ThreadStart(this.LoadDashboard)).Start();
                //(new DType(this.initDashboard)).BeginInvoke(null,null);

            }
             
        }

       public void LoadDashboard()
        {
            new DashBoard(this.HttpConnection).Run();
        }
        
        private void login()
       {
           if (offlinemode.Checked == false)
           {

                   try {
                        string username = this.username.Text;
                        string password = this.password.Text;
                        bool remember = this.remember.Checked;
                        this.username.Enabled = false;
                        this.username.Enabled = false;
                        this.remember.Enabled = false;
                        this.button_0.Enabled = false;
                
                        if ((username.Trim().Length != 0) && (password.Trim().Length != 0)) {
                    
                            this.HttpConnection.Auntenticate(username, password, remember);
                        }
                        else {
                            MessageBox.Show("Username and Password can't be blank", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        }
                        
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show(exception.Message, "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
                    finally
                    {
                        this.username.Enabled = true;
                        this.username.Enabled = true;
                        this.remember.Enabled = true;
                        this.button_0.Enabled = true;
                        this.InitializeDashboard();
                    }

            }
           else{
               MessageBox.Show("Offline mode not supported yet");
               InitializeDashboard();
           }
        }

        private void Initialize()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(Login));
            this.panel1 = new Panel();
            this.panel2 = new Panel();

            this.pictureBox_0 = new PictureBox();
            this.linkLabel_0 = new LinkLabel();
            this.linkLabel_1 = new LinkLabel();
            this.label_0 = new Label();
            this.remember = new CheckBox();
            this.offlinemode = new CheckBox();
            this.label_1 = new Label();
            this.password = new TextBox();
            this.label_2 = new Label();
            this.label_3 = new Label();
            this.button_0 = new Button();
            
            this.username = new TextBox();

            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((ISupportInitialize) this.pictureBox_0).BeginInit();
            base.SuspendLayout();

            this.panel1.BackColor = Color.White;
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.label_0);
            this.panel1.Controls.Add(this.remember);
            this.panel1.Controls.Add(this.label_1);
            this.panel1.Controls.Add(this.password);
            this.panel1.Controls.Add(this.label_2);
            this.panel1.Controls.Add(this.label_3);
            this.panel1.Controls.Add(this.button_0);
            this.panel1.Controls.Add(this.offlinemode);
            this.panel1.Controls.Add(this.username);

            this.panel1.Location = new Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x1e5, 0x13d);
            this.panel1.TabIndex = 0;

            this.panel2.BackColor = Color.FromArgb(0, 0x54, 120);
            this.panel2.Controls.Add(this.pictureBox_0);
            this.panel2.Controls.Add(this.linkLabel_0);
            this.panel2.Controls.Add(this.linkLabel_1);
            this.panel2.Location = new Point(0xee, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new Size(0xf7, 0x13d);
            this.panel2.TabIndex = 0x16;

            this.pictureBox_0.Location = new Point(0x11, 0x27);
            this.pictureBox_0.Name = "logo";
            this.pictureBox_0.Size = new Size(0xda, 0x9f);
            this.pictureBox_0.TabIndex = 13;
            this.pictureBox_0.TabStop = false;
            this.linkLabel_0.AutoSize = true;
            this.linkLabel_0.Font = new Font("Segoe UI", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.linkLabel_0.LinkBehavior = LinkBehavior.NeverUnderline;
            this.linkLabel_0.LinkColor = Color.White;
            this.linkLabel_0.Location = new Point(0x12, 0xe5);
            this.linkLabel_0.Name = "linkLabel1";
            this.linkLabel_0.Size = new Size(130, 0x1b);
            this.linkLabel_0.TabIndex = 7;
            this.linkLabel_0.TabStop = true;
            this.linkLabel_0.Text = "Need to sign up?";
            this.linkLabel_0.UseCompatibleTextRendering = true;
            this.linkLabel_1.Font = new Font("Segoe UI", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.linkLabel_1.LinkBehavior = LinkBehavior.NeverUnderline;
            this.linkLabel_1.LinkColor = Color.White;
            this.linkLabel_1.Location = new Point(0x11, 0x100);
            this.linkLabel_1.Name = "linkLabel2";
            this.linkLabel_1.Padding = new Padding(0, 2, 0, 0);
            this.linkLabel_1.Size = new Size(0xd5, 0x33);
            this.linkLabel_1.TabIndex = 8;
            this.linkLabel_1.TabStop = true;
            this.linkLabel_1.Text = "Forgot your username or password?";
            this.linkLabel_1.UseCompatibleTextRendering = true;
            this.label_0.AutoSize = true;
            this.label_0.FlatStyle = FlatStyle.Flat;
            this.label_0.Font = new Font("Segoe UI", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.label_0.ForeColor = Color.FromArgb(0x40, 0x40, 0x40);
            this.label_0.Location = new Point(12, 0x8b);
            this.label_0.Name = "label2";
            this.label_0.Padding = new Padding(0, 6, 0, 0);
            this.label_0.Size = new Size(0x4d, 0x1b);
            this.label_0.TabIndex = 20;
            this.label_0.Text = "Password";
            
            this.remember.AutoSize = true;
            this.remember.FlatStyle = FlatStyle.Flat;
            this.remember.Font = new Font("Segoe UI", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.remember.ForeColor = Color.Gray;
            this.remember.Location = new Point(0x10, 0xcc);
            this.remember.Name = "checkBox1";
            this.remember.Padding = new Padding(0, 2, 0, 0);
            this.remember.Size = new Size(0x81, 30);
            this.remember.TabIndex = 0x11;
            this.remember.Text = "Remember me";
            this.remember.UseCompatibleTextRendering = true;
            this.remember.UseVisualStyleBackColor = true;

            this.offlinemode.AutoSize = true;
            this.offlinemode.FlatStyle = FlatStyle.Flat;
            this.offlinemode.Font = new Font("Segoe UI", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.offlinemode.ForeColor = Color.Gray;
            this.offlinemode.Location = new Point(0x10, 0xE1);
            this.offlinemode.Name = "offlinemode";
            this.offlinemode.Padding = new Padding(0, 2, 0, 0);
            this.offlinemode.Size = new Size(0x81, 30);
            this.offlinemode.TabIndex = 0x11;
            this.offlinemode.Text = "Offline mode";
            this.offlinemode.UseCompatibleTextRendering = true;
            this.offlinemode.UseVisualStyleBackColor = true;
            this.offlinemode.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);

            this.label_1.FlatStyle = FlatStyle.Flat;
            this.label_1.Font = new Font("Segoe UI", 15.75f, FontStyle.Italic, GraphicsUnit.Point, 0);
            this.label_1.ForeColor = Color.Gray;
            this.label_1.Location = new Point(12, 0x27);
            this.label_1.Margin = new Padding(0);
            this.label_1.Name = "label5";
            this.label_1.Size = new Size(0xd5, 0x1d);
            this.label_1.TabIndex = 0x17;
            this.label_1.Text = "Gamecher Mod";
            this.label_1.TextAlign = ContentAlignment.MiddleLeft;
            this.label_1.UseCompatibleTextRendering = true;
            this.password.BorderStyle = BorderStyle.FixedSingle;
            this.password.Font = new Font("Segoe UI", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.password.ForeColor = SystemColors.WindowText;
            this.password.Location = new Point(12, 0xa9);
            this.password.Name = "textBox2";
            this.password.PasswordChar = '*';
            this.password.Size = new Size(0xd5, 0x1d);
            this.password.TabIndex = 0x10;
            this.password.UseSystemPasswordChar = true;
            this.label_2.AutoSize = true;
            this.label_2.FlatStyle = FlatStyle.Flat;
            this.label_2.Font = new Font("Segoe UI", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.label_2.ForeColor = Color.FromArgb(0x40, 0x40, 0x40);
            this.label_2.Location = new Point(12, 0x53);
            this.label_2.Name = "label1";
            this.label_2.Size = new Size(0x51, 0x15);
            this.label_2.TabIndex = 0x13;
            this.label_2.Text = "Username";
            this.label_3.FlatStyle = FlatStyle.Flat;
            this.label_3.Font = new Font("Segoe UI", 24f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.label_3.ForeColor = Color.FromArgb(0x40, 0x40, 0x40);
            this.label_3.Location = new Point(9, 9);
            this.label_3.Margin = new Padding(0);
            this.label_3.Name = "label3";
            this.label_3.Size = new Size(0xd8, 30);
            this.label_3.TabIndex = 0x15;
            this.label_3.Text = "MGDashboard";
            this.label_3.TextAlign = ContentAlignment.MiddleLeft;
            this.label_3.UseCompatibleTextRendering = true;
            
            this.button_0.BackColor = Color.FromArgb(10, 160, 240);
            this.button_0.FlatAppearance.BorderSize = 0;
            this.button_0.FlatAppearance.MouseOverBackColor = Color.FromArgb(11, 0xb0, 0xff);
            this.button_0.FlatStyle = FlatStyle.Flat;
            this.button_0.Font = new Font("Segoe UI Light", 18f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.button_0.ForeColor = Color.White;
            this.button_0.Location = new Point(12, 0x100);
            this.button_0.Name = "button1";
            this.button_0.Size = new Size(0xd5, 0x2f);
            this.button_0.TabIndex = 0x12;
            this.button_0.Text = "Sign In";
            this.button_0.UseCompatibleTextRendering = true;
            this.button_0.UseVisualStyleBackColor = false;
            this.button_0.Click += new EventHandler(this.signInBtn_Click);
            
            this.username.BorderStyle = BorderStyle.FixedSingle;
            this.username.Font = new Font("Segoe UI", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.username.ForeColor = SystemColors.WindowText;
            this.username.Location = new Point(12, 0x6b);
            this.username.Name = "textBox1";
            this.username.Size = new Size(0xd5, 0x1d);
            this.username.TabIndex = 15;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.FromArgb(0, 0x54, 120);
            base.ClientSize = new Size(0x1e5, 0x13c);
            base.Controls.Add(this.panel1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.Icon = (Icon)manager.GetObject("$this.Icon");
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "LoginForm";
            base.StartPosition = FormStartPosition.CenterScreen;
            base.Load += new EventHandler(this.LoginForm_ONLoad);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((ISupportInitialize) this.pictureBox_0).EndInit();
            base.ResumeLayout(false);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // Login
            // 
            this.ClientSize = new System.Drawing.Size(500, 296);
            this.Name = "Login";
            this.ResumeLayout(false);

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            password.Enabled = !(password.Enabled);
            username.Enabled = !username.Enabled;
            remember.Enabled = !remember.Enabled;
            password.BackColor = (password.Enabled) ? Color.FromArgb(0xff, 0xff, 0xFf) :  Color.FromArgb(0xDD, 0xdd, 0xdd);
            username.BackColor = (username.Enabled) ? Color.FromArgb(0xff, 0xff, 0xFf) : Color.FromArgb(0xDD, 0xdd, 0xdd);
            password.ResetText();
            username.ResetText();
            remember.Checked = false;
        }
    }
}

