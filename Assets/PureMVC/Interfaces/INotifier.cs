namespace PureMVC.Interfaces
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public interface INotifier
    {
        void SendNotification(string notificationName);
        void SengNotification(string notificationName, object boby);
        void SendNotification(string notificationName, object boby, string type);
    }
}

