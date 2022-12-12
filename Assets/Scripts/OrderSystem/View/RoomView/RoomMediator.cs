namespace OrderSystem
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using PureMVC.Patterns;
    using PureMVC.Interfaces;
    using System;

    public class RoomMediator : Mediator
    {
        private RoomProxy roomProxy = null;
        public new const string NAME = "RoomMediator";
        public RoomView View
        {
            get { return (RoomView)base.ViewComonent; }
        }
        public RoomMediator(RoomView view) : base(NAME, view)
        {

        }
        public override void OnRegister()
        {
            base.OnRegister();
            roomProxy = Facade.RetrieveProxy(RoomProxy.NAME) as RoomProxy;
            if (null == roomProxy)
            {
                throw new System.Exception("获取" + ClientProxy.NAME + "代理失败");
            }
            IList<Action<object>> actionList = new List<Action<object>>()
            {
                item => SengNotification(OrderSystemEvent.invoicong,item),
            };
            View.UpdateRoom(roomProxy.Mens,actionList);
        }
        public override IList<string> ListNotificationInterests()
        {
            IList<string> notifications = new List<string>();
            notifications.Add(OrderCommandEvent.wehaveavisitor);
            notifications.Add(OrderSystemEvent.SomeOneRoom);
            notifications.Add(OrderSystemEvent.invoicong);
            return notifications;
        }
        public override void HandleNotification(INotification notification)
        {
            switch (notification.Name)
            {
                case OrderCommandEvent.wehaveavisitor:
                    RoomDate roomDate = notification.Body as RoomDate;
                    if (null == roomDate)
                    {
                        throw new Exception("RoomData is null,please check it!");
                    }
                    SendNotification(OrderCommandEvent.Lookingfornemptyroom, roomDate, "busy");
                    break;
                case OrderSystemEvent.SomeOneRoom:
                    RoomItem roomItem = notification.Body as RoomItem;
                    if (null == roomItem)
                    {
                        throw new Exception("获取" + RoomProxy.NAME + "代理失败");
                    }
                    View.UpdateState(roomItem);
                    break;
                case OrderSystemEvent.invoicong:
                    RoomItem item = notification.Body as RoomItem;
                    SendNotification(OrderCommandEvent.Roombill, item, "pay");
                    break;
            }
        }
    }
}
