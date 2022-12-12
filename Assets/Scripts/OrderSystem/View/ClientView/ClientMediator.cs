namespace OrderSystem
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using PureMVC.Interfaces;
    using PureMVC.Patterns;
    using System;

    public class ClientMediator : Mediator
    {
        private ClientProxy clientProxy = null;
        public new const string NAME = "ClientMediator";
        private ClientView View
        {
            get { return (ClientView)ViewComonent; }
        }
        public ClientMediator(ClientView view):base(NAME,view)
        {
            view.CallWaiter += data => { SengNotification(OrderSystemEvent.CALL_WAITER, data); };
            view.Order += data => { SengNotification(OrderSystemEvent.ORDER, data); };
            view.Pay += () => { SendNotification(OrderSystemEvent.PAY); };
        }
        public override void OnRegister()
        {
            base.OnRegister();
            clientProxy = Facade.RetrieveProxy(ClientProxy.NAME) as ClientProxy;
            if (null == clientProxy)
            {
                throw new System.Exception("获取" + ClientProxy.NAME + "代理失败");
            }
            IList<Action<object>> actionList = new List<Action<object>>()
            {
                item => SendNotification(OrderCommandEvent.GUEST_BE_AWAY,item,"Add"),
                item => SengNotification(OrderSystemEvent.GET_PAY,item),
            };
            View.UpdateClient(clientProxy.Clients,actionList);
        }
        public override IList<string> ListNotificationInterests()
        {
            IList<string> notifications = new List<string>();
            notifications.Add(OrderSystemEvent.CALL_WAITER);
            notifications.Add(OrderSystemEvent.ORDER);
            notifications.Add(OrderSystemEvent.PAY);
            notifications.Add(OrderSystemEvent.REFRESH);
            return notifications;
        }
        public override void HandleNotification(INotification notification)
        {
            switch (notification.Name)
            {
                case OrderSystemEvent.CALL_WAITER:
                    ClientItem client = notification.Body as ClientItem;
                    if (null == client)
                        throw new System.Exception("对应桌号顾客不存在，请核对！");
                    Debug.Log(client.id + " 号桌顾客呼叫服务员 , 索要菜单 ");
                    break;
                case OrderSystemEvent.ORDER:
                    Order order1 = notification.Body as Order;
                    if (null == order1)
                        throw new System.Exception("order1 is null ,please check it!");
                    SendNotification(OrderCommandEvent.ChangeClientState, order1, "WaitFood");
                    Debug.Log("1111111");
                    break;
                case OrderSystemEvent.PAY:
                    //Order finishOrder = notification.Body as Order;
                    //if (null == finishOrder)
                    //    throw new System.Exception("finishOrder is null ,please check it!");
                    //finishOrder.client.state++;
                    //View.UpdateState(finishOrder.client);
                    //SengNotification(OrderSystemEvent.GET_PAY, finishOrder);
                    break;
                case OrderSystemEvent.REFRESH:
                    ClientItem clientItem = notification.Body as ClientItem;
                    if (null == clientProxy)
                        throw new Exception("获取" + ClientProxy.NAME + "代理失败");
                    View.UpdateState(clientItem);
                    break;
            }
        }
    }

}