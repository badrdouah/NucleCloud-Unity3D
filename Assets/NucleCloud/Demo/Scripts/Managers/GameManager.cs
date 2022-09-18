using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Nucle.Cloud;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int Coins = 0;
 
    private static GameManager _instance = null;
    public static GameManager Instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

 
    public async Task<string> GetPoints()
    {
        var presetId = NucleSettings.Instance.GetPresetId("points");
        
        try
        {
            var point = await Variable.Get( NucleSettings.Instance.GetUserToken(), presetId);
            return point == null ? "0" : point.value;
        }
        catch (Exception)
        {
            UIManager.Instance.DisconnectedUI();
            return null;
        }

    }

    public async Task SetPoints(int value)
    {
        var presetId = NucleSettings.Instance.GetPresetId("points");
        var val = value.ToString();
        
        try
        {
            await Variable.Update(NucleSettings.Instance.GetUserToken(),presetId, val);
        }
        catch (Exception) { UIManager.Instance.DisconnectedUI(); }
    }
}
