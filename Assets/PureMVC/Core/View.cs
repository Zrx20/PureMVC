namespace PureMVC.Core
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using PureMVC.Interfaces;
    using PureMVC.Patterns;

    public class View : IView
    {
        protected static volatile IView m_instance;
        protected IDictionary<string, IMediator> m_mediatorMap = new Dictionary<string, IMediator>();
        protected IDictionary<string, IList<IObserver>> m_observerMap = new Dictionary<string, IList<IObserver>>();
        protected static readonly object m_staticSyncRoot = new object();
        protected readonly object m_syncRoot = new object();
        protected View()
        {
            this.InitializeView();
        }
        protected virtual void InitializeView()
        {

        }
        public bool HasMediator(string mediatorName)
        {
            lock (this.m_syncRoot)
            {
                return this.m_mediatorMap.ContainsKey(mediatorName);
            }
        }

        public void NotifyObservers(INotification note)
        {
            IList<IObserver> list = null;
            lock (this.m_syncRoot)
            {
                if (this.m_observerMap.ContainsKey(note.Name))
                {
                    IList<IObserver> collection = this.m_observerMap[note.Name];
                    list = new List<IObserver>(collection);
                }
            }
            if (list != null)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].NotifyObserver(note);
                }
            }
        }

        public void RegisterMediator(IMediator mediator)
        {
            lock (this.m_syncRoot)
            {
                if (this.m_mediatorMap.ContainsKey(mediator.MediatorName))
                {
                    return;
                }
                this.m_mediatorMap[mediator.MediatorName] = mediator;
                IList<string> list = mediator.ListNotificationInterests();
                if (list.Count > 0)
                {
                    IObserver observer = new Observer("handleNotification", mediator);
                    for (int i = 0; i < list.Count; i++)
                    {
                        //将方法名注册给观察者
                        this.RegisterObserver(list[i].ToString(), observer);
                    }
                }
            }
            mediator.OnRegister();
        }

        public void RegisterObserver(string notificationName, IObserver observer)
        {
            lock (this.m_syncRoot)
            {
                if (!this.m_observerMap.ContainsKey(notificationName))
                {
                    this.m_observerMap[notificationName] = new List<IObserver>();
                }
                this.m_observerMap[notificationName].Add(observer);
            }
        }

        public IMediator RemoveMediator(string mediatorName)
        {
            IMediator notifyContext = null;
            lock (this.m_syncRoot)
            {
                if (!this.m_mediatorMap.ContainsKey(mediatorName))
                {
                    return null;
                }
                notifyContext = this.m_mediatorMap[mediatorName];
                IList<string> list = notifyContext.ListNotificationInterests();
                for (int i = 0; i < list.Count; i++)
                {
                    this.RemoveObserver(list[i], notifyContext);
                }
                this.m_mediatorMap.Remove(mediatorName);
            }
            if (notifyContext != null)
            {
                notifyContext.OnRemove();
            }
            return notifyContext;
        }

        public void RemoveObserver(string notificationName, object notifyContext)
        {
            lock (this.m_syncRoot)
            {
                if (this.m_observerMap.ContainsKey(notificationName))
                {
                    IList<IObserver> list = this.m_observerMap[notificationName];
                    for (int i = 0; i < list.Count; i++)
                    {
                        if (list[i].CompareNotifyContext(notifyContext))
                        {
                            list.RemoveAt(i);
                            break;
                        }
                    }
                    if (list.Count == 0)
                    {
                        this.m_observerMap.Remove(notificationName);
                    }
                }
            }
        }

        public IMediator RetrieveMediator(string mediatorName)
        {
            lock (this.m_syncRoot)
            {
                if (!this.m_mediatorMap.ContainsKey(mediatorName))
                {
                    return null;
                }
                return this.m_mediatorMap[mediatorName];
            }
        }
        public static IView Instance
        {
            get
            {
                if (m_instance == null)
                {
                    lock (m_staticSyncRoot)
                    {
                        if (m_instance == null)
                        {
                            m_instance = new View();
                        }
                    }
                }
                return m_instance;
            }
        }
    }
}
