using System.Collections;
using System.Collections.Generic;
using OrderSystem;
using PureMVC.Interfaces;
using PureMVC.Patterns;
using UnityEngine;

public class GetAndExitOrderCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        OrderProxy orderProxy = PureMVC.Patterns.Facade.Instance.RetrieveProxy("OrderProxy") as OrderProxy;
        MenuProxy menuProxy = PureMVC.Patterns.Facade.Instance.RetrieveProxy("MenuProxy") as MenuProxy;
        Debug.Log(notification.Type);
        if (notification.Type == "Get")
        {
            Order order = new Order(notification.Body as ClientItem, menuProxy.Mens);
            orderProxy.AddOrder(order);
            SengNotification(OrderSystemEvent.UPMENU, order);
        }
        else if (notification.Type == "Exit")
        {
            Order order = new Order(notification.Body as ClientItem, menuProxy.Mens);
            orderProxy.RemoveOrder(order);
        }
    }
}

