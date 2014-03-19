namespace MGDash.Sources.Model
{
    using Microsoft.CSharp.RuntimeBinder;
    
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;
    using IWshRuntimeLibrary;

    public sealed class VideoGame  {
        public string box_art_back;
        public string box_art_front;
        public List<int> categories;
        public string coop;
        public string description;
        public string developer;
        public string esrb;
        public bool favorite;
        public int id;
        public bool linked;
        private readonly string local_cover_base_path = (Environment.GetFolderPath(Environment.SpecialFolder.Personal).Replace('\\', '/') + "/MGDash/data/");
        private readonly string local_shortcut_path = (Environment.GetFolderPath(Environment.SpecialFolder.Personal).Replace('\\', '/') + "/MGDash/data/links/");
        public string name;
        public string path;
        public string players;
        public string publisher;
        public int rating;
        public string release_date;
        private readonly string remote_cover_base_url = "http://thegamesdb.net/banners/";
        public string youtube;

        public Settings Settings;

        public string method_0()
        {
            return (this.remote_cover_base_url + this.box_art_front);
        }

        public string getCover()
        {
            if (this.box_art_front != null)
            {
                return (this.local_cover_base_path + this.box_art_front).Replace('\\', '/');
            }
            return string.Concat(new object[] { this.local_cover_base_path, "boxart/original/front/cover_", this.id, ".png" }).Replace('\\', '/');
        }

        public string getShortcutLocation(string string_0)
        {
            Console.WriteLine(string.Concat(new object[] { this.local_shortcut_path, "shortcut_", this.id, string_0 }).Replace('\\', '/'));
            return string.Concat(new object[] { this.local_shortcut_path, "shortcut_", this.id, string_0 }).Replace('\\', '/');
        }

        public string category(string string_0)
        {
            if (this.categories.Count <= 0) {
                return "Uncategorized";
            }
            string str = "";
            foreach (int num in this.categories)
            {
                string str2 = str;
                str = str2 + this.Settings.Categories[num].name + " " + string_0 + " ";
            }
            return str.Substring(0, str.Length - 3);
        }

        private bool createShortcutold(FileInfo fileInfo) {
            
            try {
                if (!fileInfo.Extension.Equals(".lnk") && !fileInfo.Extension.Equals(".url"))
                {
                    GInterface3 interface2 = (GInterface3)Activator.CreateInstance(System.Type.GetTypeFromCLSID(new Guid("72C24DD5-D70A-438B-8A42-98424B88AFB8")));
                    if (callSite_0 == null)
                    {
                        callSite_0 = CallSite<Func<CallSite, object, IWshShortcut>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof(IWshShortcut), typeof(VideoGame)));
                    }
                    IWshShortcut interface3 = callSite_0.Target(callSite_0, interface2.CreateShortcut(this.getShortcutLocation(".lnk")));
                    interface3.TargetPath = fileInfo.FullName;
                    interface3.WorkingDirectory = fileInfo.Directory.FullName;
                    interface3.Description = "MGDash generated link";
                    interface3.Save();
                    return true;
                }
                fileInfo.CopyTo(this.getShortcutLocation(fileInfo.Extension), true);
                return true;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.StackTrace);
                this.path = null;
                return false;
            }
        }


        private bool createShortcut(FileInfo fileInfo_0)
        {
            WshShell shell = new WshShell();
            bool flag;
            try
            {
                if (fileInfo_0.Extension.Equals(".lnk") || fileInfo_0.Extension.Equals(".url"))
                {
                    fileInfo_0.CopyTo(this.getShortcutLocation(fileInfo_0.Extension), true);
                    flag = true;
                }
                else
                {
                    
                    IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(this.getShortcutLocation(".lnk"));
                    shortcut.TargetPath = fileInfo_0.FullName;
                    shortcut.WorkingDirectory = fileInfo_0.Directory.FullName;
                    shortcut.Description = "MGDash generated link";
                    shortcut.Save();
                    flag = true;
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                this.path = null;
                flag = false;
            }
            return flag;
        }

        public void checklinked()
        {
            if (this.path != null) {
                this.linked = new FileInfo(this.path).Exists;
            }
            else {
                this.linked = false;
            }
        }

        public void link(FileInfo fileInfo)
        {
            if (this.createShortcut(fileInfo)) {
                this.Settings.saveLink(this, this.getShortcutLocation(!fileInfo.Extension.Equals(".url") ? ".lnk" : ".url"));
            }
            this.checklinked();
        }

        public void unlink() {
            
            this.Settings.unlink(this);
            this.checklinked();
        }

        public void rate(int int_0) {
            this.Settings.rate(this, int_0);
        }

        public void setfavorite(bool bool_0) {
            this.Settings.setfavorite(this, bool_0);
        }

        public static CallSite<Func<CallSite, object, IWshShortcut>> callSite_0;
        
    }
}

