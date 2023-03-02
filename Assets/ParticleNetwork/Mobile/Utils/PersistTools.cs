using Newtonsoft.Json;
using Network.Particle.Scripts.Model;
using UnityEngine;

namespace Network.Particle.Scripts.Utils
{
    public class PersistTools
    {
        public static void SaveUserInfo(string json)
        {
            PlayerPrefs.SetString("UserInfo", json);
        }

        public static UserInfo GetUserInfo()
        {
            string json = PlayerPrefs.GetString("UserInfo", "");
            return JsonConvert.DeserializeObject<UserInfo>(json);
        }

        public static string GetUserInfoJson()
        {
            string json = PlayerPrefs.GetString("UserInfo", "");
            return json;
        }

        public static void Clear()
        {
            PlayerPrefs.DeleteKey("UserInfo");
        }
    }
}