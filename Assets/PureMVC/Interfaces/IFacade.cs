
namespace PureMVC.Interfaces
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public interface IFacade:INotifier
    {
        //负责注册、注销、恢复、和判断是否存在Command、Proxy、Mediator
        bool HasCammand(string notificationName);
        bool HasMediator(string mediatorName);
        bool HasProxy(string proxyName);
        void RegisterCommand(string notificationName, Type commandType);
        void RegisterMediator(IMediator mediator);
        void RegisterProxy(IProxy proxy);
        void RemoveCommand(string notificationName);
        IMediator RemoveMeditator(string mediatorName);
        IProxy RemoveProxy(string proxyName);
        IMediator RetieveMediator(string mediatorName);
        IProxy RetrieveProxy(string proxyName);
        //通知观察者
        void NotifyObservers(INotification note);
    }
}

