using System;
using System.Linq;
using Nucle.Cloud;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardUI : MonoBehaviour
{
    public GameObject[] entries;

    public void OnEnable()
    {
            for (int i = 0; i < entries.Length; i++)
            {
                 var e = entries[i];
                 e.SetActive(false);
            }

            GetLeaderboard();
    }

    public async void GetLeaderboard()
    {
        try
        {
            var presetId = NucleSettings.Instance.GetPresetId("points");
            var orderType = Nucle.Cloud.orderType.HighToLow;
            var skip = 0;
            var take = 10;

            var result = await Variable.GetList( NucleSettings.Instance.GetUserToken(),presetId,skip,take,orderType,null);
            if (result!=null)
            {
                for (int i = 0; i < result.list.Count; i++)
                {
                    var ent = result.list[i];

                    var e = entries[i];
                    e.SetActive(true);
                    e.transform.Find("Score").GetComponent<Text>().text = ent.value.ToString();
                    var id = ent.userId;
                    var user = await User.GetById( NucleSettings.Instance.GetUserToken(), id);
                    e.transform.Find("UserName").GetComponent<Text>().text = user.userName;

                    entries[i] = e;
                    e.SetActive(true);
                }
            }
        }
        catch (Exception ) { UIManager.Instance.DisconnectedUI(); }
    }
}
