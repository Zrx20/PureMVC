namespace OrderSystem
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using PureMVC.Interfaces;
    using PureMVC.Patterns;

    internal class StartUpCommand : SimpleCommand
    {
        public override void Execute(INotification notification)
        {
            //菜单代理
            MenuProxy menuProxy = new MenuProxy();
            Facade.RegisterProxy(menuProxy);

            //客户端代理
            ClientProxy clientProxy = new ClientProxy();
            Facade.RegisterProxy(clientProxy);

            //服务员代理
            WaiterProxy waitProxy = new WaiterProxy();
            Facade.RegisterProxy(waitProxy);

            //厨师代理
            CookProxy cookProxy = new CookProxy();
            Facade.RegisterProxy(cookProxy);

            RoomProxy roomProxy = new RoomProxy();
            Facade.RegisterProxy(roomProxy);

            OrderProxy orderProxy = new OrderProxy();
            Facade.RegisterProxy(orderProxy);

            MainUI mainUI = notification.Body as MainUI;
            if (null == mainUI)
            {
                throw new System.Exception("程序启动失败...");
            }
            Facade.RegisterMediator(new MenuMediator(mainUI.MenuView));
            Facade.RegisterMediator(new ClientMediator(mainUI.ClientView));
            Facade.RegisterMediator(new WaiterMediator(mainUI.WaiterView));
            Facade.RegisterMediator(new CookMediator(mainUI.CookView));
            Facade.RegisterMediator(new RoomMediator(mainUI.RoomView));

            Facade.RegisterCommand(OrderCommandEvent.GUEST_BE_AWAY, typeof(GuestBeAwayCommand));
            Facade.RegisterCommand(OrderCommandEvent.GET_ORDER, typeof(GetAndExitOrderCommand));
            Facade.RegisterCommand(OrderCommandEvent.CookCooking, typeof(CookCommand));
            Facade.RegisterCommand(OrderCommandEvent.selectWaiter, typeof(WaiterCommand));
            Facade.RegisterCommand(OrderCommandEvent.ChangeClientState, typeof(ClientChangeStateCommand));
            Facade.RegisterCommand(OrderCommandEvent.Lookingfornemptyroom, typeof(RoomCommand));
            Facade.RegisterCommand(OrderCommandEvent.Roombill, typeof(RoomRoomCommand));
        }
    }
}
