using Microsoft.Xna.Framework;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Threading;
using System.Windows.Forms;

namespace MGDash
{
	public class Login : Form
	{
		private Button button_0;

		private CheckBox remember;

		private CheckBox offlinemode;

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

		public Login()
		{
			this.Initialize();
			Stream file = Assembly.GetExecutingAssembly().GetManifestResourceStream("Resources.logo.png");
			this.pictureBox_0.BackgroundImage = Image.FromStream(file);
			this.pictureBox_0.BackgroundImageLayout = ImageLayout.Center;
		}

		private void checkBox2_CheckedChanged(object sender, EventArgs e)
		{
			this.password.Enabled = !this.password.Enabled;
			this.username.Enabled = !this.username.Enabled;
			this.remember.Enabled = !this.remember.Enabled;
			this.password.BackColor = (this.password.Enabled ? System.Drawing.Color.FromArgb(255, 255, 255) : System.Drawing.Color.FromArgb(221, 221, 221));
			this.username.BackColor = (this.username.Enabled ? System.Drawing.Color.FromArgb(255, 255, 255) : System.Drawing.Color.FromArgb(221, 221, 221));
			this.password.ResetText();
			this.username.ResetText();
			this.remember.Checked = false;
		}

		public static void CopyStream(Stream input, Stream output)
		{
			byte[] buffer = new byte[8192];
			while (true)
			{
				int num = input.Read(buffer, 0, (int)buffer.Length);
				int bytesRead = num;
				if (num <= 0)
				{
					break;
				}
				output.Write(buffer, 0, bytesRead);
			}
		}

		protected override void Dispose(bool disposing)
		{
			if ((!disposing ? false : this.icontainer_0 != null))
			{
				this.icontainer_0.Dispose();
			}
			base.Dispose(disposing);
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
			((ISupportInitialize)this.pictureBox_0).BeginInit();
			base.SuspendLayout();
			this.panel1.BackColor = System.Drawing.Color.White;
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
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(485, 317);
			this.panel1.TabIndex = 0;
			this.panel2.BackColor = System.Drawing.Color.FromArgb(0, 84, 120);
			this.panel2.Controls.Add(this.pictureBox_0);
			this.panel2.Controls.Add(this.linkLabel_0);
			this.panel2.Controls.Add(this.linkLabel_1);
			this.panel2.Location = new System.Drawing.Point(238, 0);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(247, 317);
			this.panel2.TabIndex = 22;
			this.pictureBox_0.Location = new System.Drawing.Point(17, 39);
			this.pictureBox_0.Name = "logo";
			this.pictureBox_0.Size = new System.Drawing.Size(218, 159);
			this.pictureBox_0.TabIndex = 13;
			this.pictureBox_0.TabStop = false;
			this.linkLabel_0.AutoSize = true;
			this.linkLabel_0.Font = new System.Drawing.Font("Segoe UI", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.linkLabel_0.LinkBehavior = LinkBehavior.NeverUnderline;
			this.linkLabel_0.LinkColor = System.Drawing.Color.White;
			this.linkLabel_0.Location = new System.Drawing.Point(18, 229);
			this.linkLabel_0.Name = "linkLabel1";
			this.linkLabel_0.Size = new System.Drawing.Size(130, 27);
			this.linkLabel_0.TabIndex = 7;
			this.linkLabel_0.TabStop = true;
			this.linkLabel_0.Text = "Need to sign up?";
			this.linkLabel_0.UseCompatibleTextRendering = true;
			this.linkLabel_1.Font = new System.Drawing.Font("Segoe UI", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.linkLabel_1.LinkBehavior = LinkBehavior.NeverUnderline;
			this.linkLabel_1.LinkColor = System.Drawing.Color.White;
			this.linkLabel_1.Location = new System.Drawing.Point(17, 256);
			this.linkLabel_1.Name = "linkLabel2";
			this.linkLabel_1.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
			this.linkLabel_1.Size = new System.Drawing.Size(213, 51);
			this.linkLabel_1.TabIndex = 8;
			this.linkLabel_1.TabStop = true;
			this.linkLabel_1.Text = "Forgot your username or password?";
			this.linkLabel_1.UseCompatibleTextRendering = true;
			this.label_0.AutoSize = true;
			this.label_0.FlatStyle = FlatStyle.Flat;
			this.label_0.Font = new System.Drawing.Font("Segoe UI", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.label_0.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
			this.label_0.Location = new System.Drawing.Point(12, 139);
			this.label_0.Name = "label2";
			this.label_0.Padding = new System.Windows.Forms.Padding(0, 6, 0, 0);
			this.label_0.Size = new System.Drawing.Size(77, 27);
			this.label_0.TabIndex = 20;
			this.label_0.Text = "Password";
			this.remember.AutoSize = true;
			this.remember.FlatStyle = FlatStyle.Flat;
			this.remember.Font = new System.Drawing.Font("Segoe UI", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.remember.ForeColor = System.Drawing.Color.Gray;
			this.remember.Location = new System.Drawing.Point(16, 204);
			this.remember.Name = "checkBox1";
			this.remember.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
			this.remember.Size = new System.Drawing.Size(129, 30);
			this.remember.TabIndex = 17;
			this.remember.Text = "Remember me";
			this.remember.UseCompatibleTextRendering = true;
			this.remember.UseVisualStyleBackColor = true;
			this.offlinemode.AutoSize = true;
			this.offlinemode.FlatStyle = FlatStyle.Flat;
			this.offlinemode.Font = new System.Drawing.Font("Segoe UI", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.offlinemode.ForeColor = System.Drawing.Color.Gray;
			this.offlinemode.Location = new System.Drawing.Point(16, 225);
			this.offlinemode.Name = "offlinemode";
			this.offlinemode.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
			this.offlinemode.Size = new System.Drawing.Size(129, 30);
			this.offlinemode.TabIndex = 17;
			this.offlinemode.Text = "Offline mode";
			this.offlinemode.UseCompatibleTextRendering = true;
			this.offlinemode.UseVisualStyleBackColor = true;
			this.offlinemode.CheckedChanged += new EventHandler(this.checkBox2_CheckedChanged);
			this.label_1.FlatStyle = FlatStyle.Flat;
			this.label_1.Font = new System.Drawing.Font("Segoe UI", 15.75f, FontStyle.Italic, GraphicsUnit.Point, 0);
			this.label_1.ForeColor = System.Drawing.Color.Gray;
			this.label_1.Location = new System.Drawing.Point(12, 39);
			this.label_1.Margin = new System.Windows.Forms.Padding(0);
			this.label_1.Name = "label5";
			this.label_1.Size = new System.Drawing.Size(213, 29);
			this.label_1.TabIndex = 23;
			this.label_1.Text = "Gamecher Mod";
			this.label_1.TextAlign = ContentAlignment.MiddleLeft;
			this.label_1.UseCompatibleTextRendering = true;
			this.password.BorderStyle = BorderStyle.FixedSingle;
			this.password.Font = new System.Drawing.Font("Segoe UI", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.password.ForeColor = SystemColors.WindowText;
			this.password.Location = new System.Drawing.Point(12, 169);
			this.password.Name = "textBox2";
			this.password.PasswordChar = '*';
			this.password.Size = new System.Drawing.Size(213, 29);
			this.password.TabIndex = 16;
			this.password.UseSystemPasswordChar = true;
			this.label_2.AutoSize = true;
			this.label_2.FlatStyle = FlatStyle.Flat;
			this.label_2.Font = new System.Drawing.Font("Segoe UI", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.label_2.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
			this.label_2.Location = new System.Drawing.Point(12, 83);
			this.label_2.Name = "label1";
			this.label_2.Size = new System.Drawing.Size(81, 21);
			this.label_2.TabIndex = 19;
			this.label_2.Text = "Username";
			this.label_3.FlatStyle = FlatStyle.Flat;
			this.label_3.Font = new System.Drawing.Font("Segoe UI", 24f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.label_3.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
			this.label_3.Location = new System.Drawing.Point(9, 9);
			this.label_3.Margin = new System.Windows.Forms.Padding(0);
			this.label_3.Name = "label3";
			this.label_3.Size = new System.Drawing.Size(216, 30);
			this.label_3.TabIndex = 21;
			this.label_3.Text = "MGDashboard";
			this.label_3.TextAlign = ContentAlignment.MiddleLeft;
			this.label_3.UseCompatibleTextRendering = true;
			this.button_0.BackColor = System.Drawing.Color.FromArgb(10, 160, 240);
			this.button_0.FlatAppearance.BorderSize = 0;
			this.button_0.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(11, 176, 255);
			this.button_0.FlatStyle = FlatStyle.Flat;
			this.button_0.Font = new System.Drawing.Font("Segoe UI Light", 18f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.button_0.ForeColor = System.Drawing.Color.White;
			this.button_0.Location = new System.Drawing.Point(12, 256);
			this.button_0.Name = "button1";
			this.button_0.Size = new System.Drawing.Size(213, 47);
			this.button_0.TabIndex = 18;
			this.button_0.Text = "Sign In";
			this.button_0.UseCompatibleTextRendering = true;
			this.button_0.UseVisualStyleBackColor = false;
			this.button_0.Click += new EventHandler(this.signInBtn_Click);
			this.username.BorderStyle = BorderStyle.FixedSingle;
			this.username.Font = new System.Drawing.Font("Segoe UI", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.username.ForeColor = SystemColors.WindowText;
			this.username.Location = new System.Drawing.Point(12, 107);
			this.username.Name = "textBox1";
			this.username.Size = new System.Drawing.Size(213, 29);
			this.username.TabIndex = 15;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(0, 84, 120);
			base.ClientSize = new System.Drawing.Size(485, 316);
			base.Controls.Add(this.panel1);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.Icon = (System.Drawing.Icon)manager.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "LoginForm";
			base.StartPosition = FormStartPosition.CenterScreen;
			base.Load += new EventHandler(this.LoginForm_ONLoad);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			((ISupportInitialize)this.pictureBox_0).EndInit();
			base.ResumeLayout(false);
		}

		private void InitializeComponent()
		{
			base.SuspendLayout();
			base.ClientSize = new System.Drawing.Size(500, 296);
			base.Name = "Login";
			base.ResumeLayout(false);
		}

		public void InitializeDashboard()
		{
			if ((this.HttpConnection.Autenticated ? true : this.offlinemode.Checked))
			{
				base.Close();
				(new Thread(new ThreadStart(this.LoadDashboard))).Start();
			}
		}

		public void LoadDashboard()
		{
			if (!this.offlinemode.Checked)
			{
				(new DashBoard(this.HttpConnection)).Run();
			}
			else
			{
				(new DashBoard()).Run();
			}
		}

		private void login()
		{
			if (this.offlinemode.Checked)
			{
                MessageBox.Show("Offline mode Partially Supported");
				this.InitializeDashboard();
			}
			else
			{
				
					try
					{
						string username = this.username.Text;
						string password = this.password.Text;
						bool remember = this.remember.Checked;
						this.username.Enabled = false;
						this.username.Enabled = false;
						this.remember.Enabled = false;
						this.button_0.Enabled = false;
						if ((username.Trim().Length == 0 ? true : password.Trim().Length == 0))
						{
							MessageBox.Show("Username and Password can't be blank", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
						}
						else
						{
							this.HttpConnection.Auntenticate(username, password, remember);
						}
					}
					catch (Exception exception1)
					{
						Exception exception = exception1;
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
		}

		private void LoginForm_ONLoad(object sender, EventArgs e)
		{
			this.setAplicationDirectories();
			if (this.HttpConnection.isAuntenticated())
			{
				this.InitializeDashboard();
			}
		}

		private void setAplicationDirectories()
		{
			IOException exception;
			try
			{
				string path = string.Concat(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "\\MGDash\\data\\boxart\\original\\front");
				if (!Directory.Exists(path))
				{
					try
					{
						Directory.CreateDirectory(path);
					}
					catch (IOException oException)
					{
						exception = oException;
						MessageBox.Show(string.Concat("There was an error while trying to create your links folder:", exception.Message), "Configuration error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					}
				}
				path = string.Concat(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "\\MGDash\\data\\links");
				if (!Directory.Exists(path))
				{
					try
					{
						Directory.CreateDirectory(path);
					}
					catch (IOException oException1)
					{
						exception = oException1;
						MessageBox.Show(string.Concat("There was an error while trying to create your links folder:", exception.Message), "Configuration error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					}
				}
				path = Environment.GetFolderPath(Environment.SpecialFolder.Personal)+ @"\\MGDash\\data\\config";
				if (!Directory.Exists(path))
				{
					try
					{
						Assembly assembly = Assembly.GetExecutingAssembly();
						Directory.CreateDirectory(path);
						Stream file = assembly.GetManifestResourceStream("Resources.user.json");
						Stream output = File.Create(string.Concat(path, "\\user.json"));
						Login.CopyStream(file, output);
						file = assembly.GetManifestResourceStream("Resources.games.json");
						output = File.Create(string.Concat(path, "\\games.json"));
						Login.CopyStream(file, output);
						file = assembly.GetManifestResourceStream("Resources.categories.json");
						output = File.Create(string.Concat(path, "\\categories.json"));
						Login.CopyStream(file, output);
					}
					catch (IOException oException2)
					{
						exception = oException2;
						MessageBox.Show(string.Concat("There was an error while trying to create your config folder:", exception.Message), "Configuration error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					}
				}
			}
			catch (Exception exception1)
			{
				MessageBox.Show(exception1.StackTrace);
			}
		}

		private void signInBtn_Click(object sender, EventArgs e)
		{
			this.login();
		}
	}
}