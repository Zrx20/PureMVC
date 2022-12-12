namespace PureMVC.Patterns
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using PureMVC.Interfaces;
    using System;

    public class Notifier : INotifier
    {
        private IFacade m_facade = PureMVC.Patterns.Facade.Instance;
        public void SendNotification(string notificationName)
        {
            this.m_facade.SendNotification(notificationName);
        }

        public void SendNotification(string notificationName, object boby, string type)
        {
            this.m_facade.SendNotification(notificationName, boby, type);
        }

        public void SengNotification(string notificationName, object boby)
        {
            this.m_facade.SengNotification(notificationName, boby);
        }
        protected IFacade Facade
        {
            get
            {
                return this.m_facade;
            }
        }
    }
}
