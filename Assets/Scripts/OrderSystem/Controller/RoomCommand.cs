namespace OrderSystem
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using PureMVC.Interfaces;
    using PureMVC.Patterns;

    public class RoomCommand : SimpleCommand
    {
        public override void Execute(INotification notification)
        {
            RoomDate room = notification.Body as RoomDate;
            RoomProxy roomProxy = Facade.RetrieveProxy(RoomProxy.NAME) as RoomProxy;
            switch (notification.Type)
            {
                case "busy":
                    Debug.Log("收到客人信息");
                    roomProxy.ChangeRoomState(room.roomItem);
                    break;
                case "pay":
                    roomProxy.Remove(room.roomItem);
                    break;

            }
        }
    }
}
