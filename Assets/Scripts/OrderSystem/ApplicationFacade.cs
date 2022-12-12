namespace OrderSystem
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using PureMVC.Interfaces;
    using PureMVC.Patterns;

    public class ApplicationFacade : Facade
    {
        public new static IFacade Instance
        {
            get
            {
                if(null == m_instance)
                {
                    lock (m_staticSyncRoot)
                    {
                        if (null == m_instance)
                        {
                            Debug.Log("构造Facade,此处会自动构造View、Controller和Model");
                            m_instance = new ApplicationFacade();
                        }
                    }
                }
                return m_instance;
            }
        }
        public void StartUp(MainUI mainUI)
        {
            Debug.Log("启动程序");
            SengNotification(OrderSystemEvent.STARTUP, mainUI);
        }
        protected override void InitializeFacade()
        {
            base.InitializeFacade();
        }
        protected override void InitializeView()
        {
            base.InitializeView();
        }
        protected override void InitializeController()
        {
            base.InitializeController();
            RegisterCommand(OrderSystemEvent.STARTUP, typeof(StartUpCommand));
        }
        protected override void InitializeModel()
        {
            base.InitializeModel();
        }
    }
}
