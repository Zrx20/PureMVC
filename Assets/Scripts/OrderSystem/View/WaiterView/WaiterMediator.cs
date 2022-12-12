namespace OrderSystem
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using PureMVC.Interfaces;
    using PureMVC.Patterns;

    internal class WaiterMediator : Mediator
    {
        private WaiterProxy waiterProxy = null;
        public new const string NAME = "WaiterMediator";
        public WaiterView WaiterView
        {
            get { return (WaiterView)base.ViewComonent; }
        }

        private OrderProxy orderProxy = null;

        public WaiterMediator(WaiterView view) : base(NAME, view)
        {
            WaiterView.CallWaiter += () => { SendNotification(OrderSystemEvent.CALL_WAITER); };
            WaiterView.order += data => { SengNotification(OrderSystemEvent.ORDER, data); };
            WaiterView.Pay += () => { SendNotification(OrderSystemEvent.PAY); };
            WaiterView.CallCook += () => { SendNotification(OrderSystemEvent.CALL_COOK); };
            WaiterView.ServerFood += item => { SendNotification(OrderCommandEvent.selectWaiter,item,"WANSHI"); };
        }

        public override void OnRegister()
        {
            base.OnRegister();
            waiterProxy = Facade.RetrieveProxy(WaiterProxy.NAME) as WaiterProxy;
            orderProxy = Facade.RetrieveProxy(OrderProxy.NAME) as OrderProxy;
            if (null == waiterProxy)
                throw new System.Exception(WaiterProxy.NAME + "is null,please check it!");
            if (null == orderProxy)
                throw new System.Exception(OrderProxy.NAME + "is null,please check it!");
            WaiterView.UpdateWaiter(waiterProxy.Waiters);
        }

        public override IList<string> ListNotificationInterests()
        {
            IList<string> notifications = new List<string>();
            notifications.Add(OrderSystemEvent.CALL_WAITER);
            notifications.Add(OrderSystemEvent.ORDER);
            notifications.Add(OrderSystemEvent.GET_PAY);
            notifications.Add(OrderSystemEvent.FOOD_TO_CLIENT);
            notifications.Add(OrderSystemEvent.ResfrshWarite);
            return notifications;
        }
        public override void HandleNotification(INotification notification)
        {
            switch (notification.Name)
            {
                case OrderSystemEvent.CALL_WAITER:
                    ClientItem client = notification.Body as ClientItem;
                    SendNotification(OrderCommandEvent.GET_ORDER, client, "Get");
                    break;
                case OrderSystemEvent.ORDER:
                    SengNotification(OrderSystemEvent.CALL_COOK, notification.Body);
                    break;
                case OrderSystemEvent.GET_PAY:
                    Debug.Log(" 服务员拿到顾客的付款 ");
                    ClientItem item = notification.Body as ClientItem;
                    SendNotification(OrderCommandEvent.GUEST_BE_AWAY, item, "Remove");
                    break;
                case OrderSystemEvent.FOOD_TO_CLIENT:
                    Debug.Log(" 服务员上菜 ");
                    WaiterItem waiteritem = notification.Body as WaiterItem;
                    SendNotification(OrderCommandEvent.ChangeClientState, waiteritem.order, "Eating");
                    SengNotification(OrderSystemEvent.PAY, waiteritem);
                    break;
                case OrderSystemEvent.ResfrshWarite:
                    waiterProxy = Facade.RetrieveProxy(WaiterProxy.NAME) as WaiterProxy;
                    WaiterView.Move(waiterProxy.Waiters);
                    break;
            }
        }

        private WaiterItem GetIdleWaiter()
        {
            foreach (WaiterItem waiter in waiterProxy.Waiters)
                if (waiter.state.Equals((int)E_WaiterState.Idle))
                    return waiter;
            Debug.LogWarning("暂无空闲服务员请稍等..");
            return null;
        }
    }
}
