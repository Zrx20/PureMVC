namespace PureMVC.Interfaces
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public interface ICommand 
    {
        //执行通知事件
        void Execute(INotification notification);
    }
}
