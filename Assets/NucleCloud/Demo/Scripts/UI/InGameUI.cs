using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;


public class InGameUI : MonoBehaviour
{
    public Text points;

    private void Awake()
    {
        this.GetComponent<GraphicRaycaster>().enabled = false;
        points.gameObject.SetActive(false);
    }

    async void OnEnable()
    {
        points.text = await GameManager.Instance.GetPoints();
        points.gameObject.SetActive(true);
        this.GetComponent<GraphicRaycaster>().enabled = true;

    }

    public void AddCoin()
    {
        var coin =  Int32.Parse(points.text);
        int newCoinsValue = coin+1;
        points.text = newCoinsValue.ToString();
    }


    public async  void SaveChanges()
    {
        var coin = Int32.Parse(points.text);
        points.text = coin.ToString();
       await  GameManager.Instance.SetPoints(coin) ;
    }
}
