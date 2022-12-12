namespace OrderSystem
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using PureMVC.Interfaces;
    using PureMVC.Patterns;

    public class MenuMediator : Mediator
    {
        private MenuProxy menuProxy = null;
        public new const string NAME = "MenuMediator";
        public MenuView MenuView
        {
            get { return (MenuView)ViewComonent; }
        }
        public MenuMediator(MenuView view):base(NAME,view)
        {
            MenuView.Submit += order => { SengNotification(OrderSystemEvent.SUBMITMENU, order); };
            MenuView.Cancel += ()=>{ SendNotification(OrderSystemEvent.CANCEL_ORDER); };
        }
        public override void OnRegister()
        {
            base.OnRegister();
            menuProxy = Facade.RetrieveProxy(MenuProxy.NAME) as MenuProxy;
            if (null == menuProxy)
            {
                throw new System.Exception(MenuProxy.NAME + "is null");
            }
            MenuView.UpdateMenu(menuProxy.Mens);
        }
        public override IList<string> ListNotificationInterests()
        {
            IList<string> notifications = new List<string>();
            notifications.Add(OrderSystemEvent.UPMENU);
            notifications.Add(OrderSystemEvent.CANCEL_ORDER);
            notifications.Add(OrderSystemEvent.SUBMITMENU);
            return notifications;
        }
        public override void HandleNotification(INotification notification)
        {
            switch (notification.Name)
            {
                case OrderSystemEvent.UPMENU:
                    Order order = notification.Body as Order;
                    if (null == order)
                    {
                        throw new System.Exception("order is null,plase check it!");
                    }
                    MenuView.UpMenu(order);
                    break;
                case OrderSystemEvent.SUBMITMENU:
                    Order selectedOrder = notification.Body as Order;
                    MenuView.SubmitMenu(selectedOrder);
                    SengNotification(OrderSystemEvent.ORDER, selectedOrder);
                    break;
                case OrderSystemEvent.CANCEL_ORDER:
                    Order order1 = notification.Body as Order;
                    if (order1 == null)
                    {
                        throw new System.Exception("order is null,plase check it!");
                    }
                    MenuView.CancelMenu();
                    SendNotification(OrderCommandEvent.GET_ORDER, order1, "Exit");
                    break;
            }
        }
    }
}
