namespace OrderSystem
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using PureMVC.Interfaces;
    using PureMVC.Patterns;

    public class CookMediator : Mediator
    {
        private CookProxy cookProxy = null;
        public new const string NAME = "CookMediator";
        public CookView CookView
        {
            get { return (CookView)base.ViewComonent; }
        }

        public CookMediator(CookView view) : base(NAME, view)
        {
            CookView.CallCook += () => { SendNotification(OrderSystemEvent.CALL_COOK); };
            CookView.ServerFood += item => { SengNotification(OrderSystemEvent.SERVER_FOOD,item); };
        }

        public override void OnRegister()
        {
            base.OnRegister();
            cookProxy = Facade.RetrieveProxy(CookProxy.NAME) as CookProxy;
            if (null == cookProxy)
                throw new System.Exception(CookProxy.NAME + "is null.");
            CookView.UpdateCook(cookProxy.Cooks);
        }

        public override IList<string> ListNotificationInterests()
        {
            IList<string> notifications = new List<string>();
            notifications.Add(OrderSystemEvent.CALL_COOK);
            notifications.Add(OrderSystemEvent.SERVER_FOOD);
            notifications.Add(OrderSystemEvent.ResfrshCook);
            return notifications;
        }

        public override void HandleNotification(INotification notification)
        {
            switch (notification.Name)
            {
                case OrderSystemEvent.CALL_COOK:
                    Order order = notification.Body as Order;
                    if (null == order)
                        throw new System.Exception("order is null ,please check it.");
                    Debug.Log("厨师接收到前台的订单,开始炒菜:" + order.names);
                    SengNotification(OrderCommandEvent.CookCooking, order);
                    break;
                case OrderSystemEvent.SERVER_FOOD:
                    Debug.Log("厨师通知服务员上菜");
                    CookItem cook = notification.Body as CookItem;
                    SendNotification(OrderSystemEvent.ResfrshCook);
                    SendNotification(OrderCommandEvent.selectWaiter, cook.cookOrder, "SERVING");
                    cook.cookOrder = null;
                    break;
                case OrderSystemEvent.ResfrshCook:
                    cookProxy = Facade.RetrieveProxy(CookProxy.NAME) as CookProxy;
                    if (null == cookProxy)
                        throw new System.Exception(CookProxy.NAME + "is null.");
                    CookView.ResfrshCook(cookProxy.Cooks);
                    break;
            }
        }
    }
}
