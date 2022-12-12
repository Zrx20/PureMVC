using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PureMVC.Interfaces;
using PureMVC.Patterns;
using UnityEngine.UI;
using OrderSystem;
using System;

public class RoomItemView : MonoBehaviour
{
    private Text text = null;
    private Image image = null;
    public RoomItem roomItem = null;
    public IList<Action<object>> actionList = new List<Action<object>>();
    private void Awake()
    {
        text = transform.Find("Id").GetComponent<Text>();
        image = transform.GetComponent<Image>();
    }
    public void InitClient(RoomItem room)
    {
        this.roomItem = room;
        UpdateState();
    }
    private void UpdateState()
    {
        if (roomItem == null)
        {
            return;
        }
        Color color = Color.white;
        switch (this.roomItem.state)
        {
            case RoomState.idle:
                color = Color.white;
                
                break;
            case RoomState.busy:
                color = Color.red;
                StartCoroutine(Goroom());
                break;
        }
        image.color = color;
        text.text = roomItem.ToString();
    }
    private IEnumerator Goroom(float time = 3)
    {
        Debug.Log(roomItem.id + "号桌客人已经入驻");
        yield return new WaitForSeconds(time);
        roomItem.state--;
        Debug.Log(roomItem.id + "客人离开客房");
        actionList[0].Invoke(roomItem);
    }
}
