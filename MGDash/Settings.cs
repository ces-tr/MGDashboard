using MGDash.Sources.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;

namespace MGDash
{
	public sealed class Settings
	{
		public Dictionary<int, Category> Categories;

		private DashBoard dashboard;

		public HttpConnection HttpConnection;

		public List<VideoGame> gameList;

		public User user;

		public Settings(DashBoard _dashboard, ref HttpConnection _httpconn)
		{
			this.dashboard = _dashboard;
			this.HttpConnection = _httpconn;
			this.setUserData();
			this.setGameData();
			this.setCategoryData();
		}

		public Settings(DashBoard _dashboard)
		{
			this.dashboard = _dashboard;
			this.setOfflineUserData();
			this.setOfflineGameData();
			this.setOfflineCategoryData();
		}

		public void saveLink(VideoGame videoGame, string gamepath)
		{
			try
			{
				if (JObject.Parse(this.HttpConnection.PUT(string.Concat("/api/user/games/", videoGame.id, ".json"), JObject.Parse(string.Concat("{'collection' : {'path' : '", gamepath, "'} }")))).Value<bool>("success"))
				{
					videoGame.path = gamepath;
				}
			}
			catch
			{
                if (gamepath != null) videoGame.path = gamepath; //Offline mode 
			}
		}

		public void saveUser(User user_1)
		{
			try
			{
				if (JObject.Parse(this.HttpConnection.PUT("/api/user.json", JObject.Parse(string.Concat("{'user' : ", JObject.FromObject(user_1).ToString(), " }")))).Value<bool>("success"))
				{
					this.user = user_1;
				}
			}
			catch
			{
			}
		}

		public void method_4(string string_0, object object_0)
		{
			try
			{
				string str = (object_0 is bool ? object_0.ToString().ToLower() : object_0.ToString());
				HttpConnection httpConnection = this.HttpConnection;
				string[] string0 = new string[] { "{'user' : {'", string_0, "' : ", str, "} }" };
				if (JObject.Parse(httpConnection.PUT("/api/user.json", JObject.Parse(string.Concat(string0)))).Value<bool>("success"))
				{
					JObject obj3 = JObject.FromObject(this.user);
					obj3[string_0] = object_0.ToString();
					this.user = obj3.ToObject<User>();
				}
			}
			catch
			{
			}
		}

		public void rate(VideoGame videoGame_0, int int_0)
		{
			try
			{
				if (JObject.Parse(this.HttpConnection.PUT(string.Concat("/api/user/games/", videoGame_0.id, ".json"), JObject.Parse(string.Concat("{'collection' : {'rating' : ", int_0, "} }")))).Value<bool>("success"))
				{
					videoGame_0.rating = int_0;
				}
			}
			catch
			{
			}
		}

		private void setCategoryData()
		{
			try
			{
				JArray array = JArray.Parse(this.HttpConnection.GET("/api/categories.json"));
				this.Categories = new Dictionary<int, Category>();
				foreach (JObject obj2 in (IEnumerable<JToken>)array)
				{
					Category category = obj2.ToObject<Category>();
					this.Categories.Add(category.id, category);
				}
			}
			catch
			{
			}
		}

		public void setfavorite(VideoGame _videoGame, bool bool_0)
		{
			try
			{
				if (JObject.Parse(this.HttpConnection.PUT(string.Concat("/api/user/games/", _videoGame.id, ".json"), JObject.Parse(string.Concat("{'collection' : {'favorite' : ", bool_0.ToString().ToLower(), "} }")))).Value<bool>("success"))
				{
					_videoGame.favorite = bool_0;
				}
			}
			catch
			{
			}
		}

		private void setGameData()
		{
			try
			{
				JArray array = JArray.Parse(this.HttpConnection.GET("/api/user/games.json"));
				this.gameList = new List<VideoGame>();
				foreach (JObject obj2 in (IEnumerable<JToken>)array)
				{
					this.gameList.Add(obj2.ToObject<VideoGame>());
				}
				foreach (VideoGame game in this.gameList)
				{
					game.Settings = this;
				}
			}
			catch
			{
			}
		}

		private void setOfflineCategoryData()
		{
			try
			{
				StreamReader reader = File.OpenText(string.Concat(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "\\MGDash\\data\\config\\categories.json"));
				JArray array = JArray.Parse(reader.ReadToEnd());
				this.Categories = new Dictionary<int, Category>();
				foreach (JObject obj2 in (IEnumerable<JToken>)array)
				{
					Category category = obj2.ToObject<Category>();
					this.Categories.Add(category.id, category);
				}
			}
			catch
			{
				throw new NotImplementedException();
			}
		}

		private void setOfflineGameData()
		{
			try
			{
				StreamReader reader = File.OpenText(Environment.GetFolderPath(Environment.SpecialFolder.Personal)+ @"\\MGDash\\data\\config\\games.json");
				JArray array = JArray.Parse(reader.ReadToEnd());
				this.gameList = new List<VideoGame>();
				foreach (JObject obj2 in (IEnumerable<JToken>)array)
				{
					this.gameList.Add(obj2.ToObject<VideoGame>());
				}
				foreach (VideoGame game in this.gameList)
				{
					game.Settings = this;
				}
			}
			catch (Exception exception)
			{
				throw new NotImplementedException("Games Config file not loaded");
			}
		}

		public void setOfflineUserData()
		{
			try
			{
				StreamReader reader = File.OpenText(string.Concat(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "\\MGDash\\data\\config\\user.json"));
				this.user = JObject.Parse(reader.ReadToEnd()).ToObject<User>();
			}
			catch
			{
				throw new NotImplementedException("no se cargo el archivo de configuracion de usuario");
			}
		}

		public void setUserData()
		{
			try
			{
				this.user = JObject.Parse(this.HttpConnection.GET("/api/user.json")).ToObject<User>();
			}
			catch
			{
			}
		}

		public void unlink(VideoGame videoGame)
		{
			try
			{
				if (JObject.Parse(this.HttpConnection.PUT(string.Concat("/api/user/games/", videoGame.id, ".json"), JObject.Parse("{'collection' : {'path' : null} }"))).Value<bool>("success"))
				{
					videoGame.path = null;
				}
			}
			catch
			{
			}
		}
	}
}