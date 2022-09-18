using System;
using System.Collections.Generic;
using UnityEngine;

namespace Nucle.Cloud
{
    [CreateAssetMenu(fileName = "Nucle", menuName = "Nucle/Settings", order = 1)]
    public class NucleSettings : SingeltonScriptableObject<NucleSettings>
    {
        public string projectId;
        public List<PresetEditorModel> presets = new List<PresetEditorModel>();
        public UserModel user;

        public string GetPresetId(string key)
        {
            for (int i = 0; i < presets.Count; i++)
            {
                var preset = presets[i];
                var filtredKey = key.ToUpper().Trim();
                var filtredPresetKey = preset.key.ToUpper().Trim();

                if (filtredPresetKey == filtredKey)
                {
                    return preset.id;
                }
            }
            return null;
        }

        public string GetUserToken()
        {
            return PlayerPrefs.GetString("UserToken"); ;
        }

        public void SetUserToken(string userToken)
        {
            PlayerPrefs.SetString("UserToken", userToken);
        }

        public bool IsUserLoggedIn()
        {
            return !string.IsNullOrWhiteSpace(GetUserToken());
        }

        public void Logout()
        {
            NucleSettings.Instance.user = null;
            NucleSettings.Instance.SetUserToken(null);
        }
    }

    [Serializable]
    public class PresetEditorModel
    {
        public string id;
        public string key;
    }
}
