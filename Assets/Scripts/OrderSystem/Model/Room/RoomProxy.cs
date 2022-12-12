namespace OrderSystem
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using PureMVC.Interfaces;
    using PureMVC.Patterns;

    public class RoomProxy : Proxy
    {
        public new const string NAME = "RoomProxy";
        public IList<RoomItem> Mens
        {
            get { return (IList<RoomItem>)base.Data; }
        }
        public RoomProxy():base(NAME,new List<RoomItem>())
        {
            AddMenu(new RoomItem(1, 0, 0));
            AddMenu(new RoomItem(2, 0, 0));
            AddMenu(new RoomItem(3, 0, 0));
            AddMenu(new RoomItem(4, 0, 0));
        }
        public void AddMenu(RoomItem item)
        {
            if (!Mens.Contains(item))
            {
                Mens.Add(item);
            }
            UpdateRoom(item);
        }
        public void Remove(RoomItem item)
        {
            for (int i = 0; i < Mens.Count; i++)
            {
                if (Mens[i].id == item.id)
                {
                    Mens[i].state = RoomState.idle;
                    SengNotification(OrderSystemEvent.SomeOneRoom, Mens[i]);
                    return;
                }
            }
        }
        public void ChangeRoomState(RoomItem item)
        {
            SengNotification(OrderSystemEvent.SomeOneRoom, item);
        }
        private void UpdateRoom(RoomItem item)
        {
            for (int i = 0; i < Mens.Count; i++)
            {
                if (Mens[i].id == item.id)
                {
                    Mens[i] = item;
                    Mens[i].state = item.state;
                    Mens[i].num = item.num;
                    SengNotification(OrderSystemEvent.SomeOneRoom, Mens[i]);
                    return;
                }
            }
        }
    }
}
