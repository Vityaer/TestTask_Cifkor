using Assets.Scripts.Common;
using Newtonsoft.Json;
using System.IO;

namespace Common
{

    public class TextUtils
    {
        public static string DictionariesPath
        {
            get
            {
                return "Assets/Saves";
            }
        }
        public static string GetTextFromLocalStorage<T>()
        {
            var path = GetConfigPath<T>();
            if (!File.Exists(path))
            {
                File.Create(path).Close();
            }
            var text = File.ReadAllText(path);
            return text;
        }

        public static string GetConfigPath<T>()
        {
            var path = Path.Combine(DictionariesPath, $"{typeof(T).Name}.json");
            return path;
        }

        public static T Load<T>() where T : new()
        {
            var json = GetTextFromLocalStorage<T>();
            var result = JsonConvert.DeserializeObject<T>(json, ProjectConstants.Common.SerializerSettings);

            if (result == null)
            {
                result = new T();
            }

            return result;
        }
    }
}
