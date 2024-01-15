using System;
using System.Collections.Generic;
using UI.Shared;
using UnityEngine;

namespace Global
{
    public static class PlayerPrefsToJSON
    {
        public static string GetPlayerPrefsInJSON()
        {
            return JsonUtility.ToJson(new PlayerPrefsInJSON());
        }

        public static void SetPlayerPrefsByJSON(string data)
        {
            JsonUtility.FromJson<PlayerPrefsInJSON>(data).SetPlayerPrefs();
        }
    }
}