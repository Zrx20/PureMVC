namespace OrderSystem
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using PureMVC.Interfaces;
    using PureMVC.Patterns;

    public class RoomRoomCommand : SimpleCommand
    {
        public override void Execute(INotification notification)
        {
            RoomItem room = notification.Body as RoomItem;
            RoomProxy roomProxy = Facade.RetrieveProxy(RoomProxy.NAME) as RoomProxy;
            switch (notification.Type)
            {
                case "pay":
                    roomProxy.Remove(room);
                    break;

            }
        }
    }
}
