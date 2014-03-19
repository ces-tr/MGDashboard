namespace MGDash
{
   
    using Newtonsoft.Json.Linq;
    using System;
    using System.IO;
    using System.Net;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Text;
    using MGDash.Sources.Model;

    public sealed class HttpConnection {

        public bool Autenticated;
        
        public CookieCollection CookieCollection;
        public HttpWebResponse HttpWebResponse;
        public static readonly string baseurl = "http://www.gamecher.com/";
        private string CookieLocation;
        private string CookiePath;

        public HttpConnection() {
            this.Autenticated = false;
            this.HttpWebResponse = null;
            this.CookieCollection = new CookieCollection();
            this.CookieLocation = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"Gamecher\");
            this.CookiePath = Path.Combine(this.CookieLocation, "session.dat");
        }

        public HttpConnection(CookieCollection _cookieCollection, bool bool_1) : this() {
            this.CookieCollection = _cookieCollection;
            this.Autenticated = bool_1;
        }

        public void Auntenticate(string username, string password, bool remember) {

            string json = @"{""username"":""" + username + @""",""password"":""" + password + @""",""remember"":""" + remember.ToString().ToLower() + @"""}";
            try {
                this.Autenticated = JObject.Parse(this.POST("/sessions.json", JObject.Parse(json))).Value<bool>("success");
            }
            catch (Exception e){
                this.Autenticated = false;
                throw new Exception("An unexpected error ocurred.\r\nCheck your internet connection.");
            }

            if (!this.Autenticated) {
                throw new Exception("Username or password invalid");
            }
            if (remember) {
                this.setCookieContainer();
            }
        }

        public string GET(string string_4) {
            return this.HttpRequest(string_4, null, "GET");
        }

        public void DeleteCookie() {
            try {
                FileInfo info = new FileInfo(this.CookiePath);
                if (info != null) {
                    info.Delete();
                }
            }
            catch {
            }
        }

        public void SaveCookie(string string_4, CookieContainer cookieContainer_0) {
            
            Stream serializationStream = System.IO.File.Create(string_4);
            try {
                new BinaryFormatter().Serialize(serializationStream, cookieContainer_0);
            }
            catch {
            }
            finally {
                if (serializationStream != null) {
                    serializationStream.Dispose();
                }
            }
        }

        public CookieContainer ReadCookie() {
            CookieContainer container;
            try {
                using (Stream stream = System.IO.File.Open(this.CookiePath, FileMode.Open)) {
                    BinaryFormatter formatter = new BinaryFormatter();
                    container = (CookieContainer) formatter.Deserialize(stream);
                }
            }
            catch {
                container = null;
            }
            return container;
        }

        
        public string POST(string string_4, JObject jobject_0) {
            return this.HttpRequest(string_4, jobject_0, "POST");
        }

        public string PUT(string url, JObject jobject_0) {
            return this.HttpRequest(url, jobject_0, "PUT");
        }

        private string HttpRequest(string url, JObject JSParameters, string method) {
            try {
                string str = "";
                HttpWebRequest request = (HttpWebRequest) WebRequest.Create(baseurl + url);
                request.CookieContainer = new CookieContainer();
                this.addCookies(request);
                
                if (method.Equals("PUT") || method.Equals("POST")) {
                    this.setParameters(request, JSParameters, method);
                }
                //request.GetRequestStream().Flush(); //request.GetRequestStream().Close();
                HttpWebResponse response = (HttpWebResponse) request.GetResponse();
                this.HttpWebResponse = response;
                str = new StreamReader(response.GetResponseStream()).ReadToEnd();
                this.setCookieCollection(response);
                return str;
            }
            catch {
            }
            return null;
        }

        private void addCookies(HttpWebRequest httpWebRequest) {

            if ((this.CookieCollection != null) && (this.CookieCollection.Count > 0)) {
                httpWebRequest.CookieContainer.Add(this.CookieCollection);
            }
        }

        private void setParameters(HttpWebRequest httpWebRequest, JObject jobject_0, string method) {
            
            string str = jobject_0.ToString();
            byte[] bytes = Encoding.UTF8.GetBytes(str.ToCharArray());
            httpWebRequest.ContentLength = bytes.Length;
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = method;
            httpWebRequest.GetRequestStream().Write(bytes, 0, bytes.Length);
        }

        private void setCookieCollection(HttpWebResponse _httpWebResponse) {
            
            if (_httpWebResponse.Cookies.Count > 0) {
                if (this.CookieCollection == null) {
                    this.CookieCollection = new CookieCollection();
                }
                this.CookieCollection.Add(_httpWebResponse.Cookies);
            }
        }

        public bool isAuntenticated() {

            CookieContainer container = this.ReadCookie();
            if (container == null) {
                this.DeleteCookie();
                return false;
            }
            this.CookieCollection.Add(container.GetCookies(new Uri(baseurl)));
            if (this.CookieCollection.Count == 0) {
                return false;
            }
            try {
                User user = JObject.Parse(this.GET("/api/user.json")).ToObject<User>();
                this.Autenticated = user != null;
            }
            catch {
                this.Autenticated = false;
            }
            finally {
                if (!this.Autenticated) {
                    this.DeleteCookie();
                }
            }
            return this.Autenticated;
        }

        public void setCookieContainer()
        {
            if (!Directory.Exists(this.CookieLocation))
            {
                Directory.CreateDirectory(this.CookieLocation);
            }
            CookieContainer container = new CookieContainer();
            container.Add(this.CookieCollection);
            
            this.SaveCookie(this.CookiePath, container);
        }
    }
}

