
using Newtonsoft.Json;
using System.IO;

public class PersistInformation
{
    public enum INFO_TYPE {
        PROFILE,
        AUTHENTICATION,
        PROGRESS
    }

    public static void PersistData(INFO_TYPE pType, object pContent)
    {
        PersistJSON(pType.ToString(), JsonConvert.SerializeObject( pContent ));
    }

    public static T LoadData<T>(INFO_TYPE pType) where T : new()
    {
        string pContents = LoadJSON(pType.ToString().ToUpper());

        if (!string.IsNullOrEmpty(pContents))
        {
            return JsonConvert.DeserializeObject<T>(pContents);
        }else
        {
            return new T();
        }
    }

    private static void PersistJSON(string pNameFile, string pContent)
    {
        string tPath = UnityEngine.Application.persistentDataPath + "/" + pNameFile + ".json";

        File.WriteAllText(tPath, pContent);
    }

    private static string LoadJSON(string pNameFile)
    {
        string tPath = UnityEngine.Application.persistentDataPath + "/" + pNameFile + ".json";

        if (File.Exists(tPath))
            return File.ReadAllText(tPath);

        return null;
    }
}