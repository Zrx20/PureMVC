namespace OrderSystem
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using PureMVC.Patterns;

    public class ClientProxy : Proxy
    {
        public new const string NAME = "ClientProxy";
        public RoomItem roomitem = null;
        public IList<ClientItem> Clients
        {
            get { return (IList<ClientItem>)base.Data; }
        }
        public ClientProxy():base(NAME,new List<ClientItem>())
        {

        }
        public override void OnRegister()
        {
            AddClient(new ClientItem(1, 2, 0));
            AddClient(new ClientItem(2, 1, 0));
            AddClient(new ClientItem(3, 4, 0));
            AddClient(new ClientItem(4, 5, 0));
            AddClient(new ClientItem(5, 12, 0));
        }
        public void AddClient(ClientItem item)
        {
            if (Clients.Count<5)
            {
                Clients.Add(item);
            }
            UpdateClient(item);
           
        }
        public void RemoveClient(ClientItem item)
        {
            for (int i = 0; i < Clients.Count; i++)
            {
                if (Clients[i].id == item.id)
                {
                    Clients[i].state = ClientState.Pay;
                    SengNotification(OrderSystemEvent.REFRESH, Clients[i]);
                  
                    return;
                }
            }
        }
        public void ChangeClientState(ClientItem item, ClientState stateIndex)
        {
            item.state = stateIndex;
            SengNotification(OrderSystemEvent.REFRESH, item);
        }
        public void DeleteClient(ClientItem item)
        {
            for (int i = 0; i < Clients.Count; i++)
            {
                if (Clients[i].id == item.id)
                {
                    Clients[i].state = ClientState.Pay;
                    SengNotification(OrderSystemEvent.REFRESH, Clients[i]);
                    
                    int room = 1;
                    if (room == 1)
                    {
                        roomitem = new RoomItem(Clients[i].id, Clients[i].population, RoomState.busy);
                        SengNotification(OrderCommandEvent.wehaveavisitor, new RoomDate(roomitem));
                    }
                }
            }
        }
        public void UpdateClient(ClientItem item)
        {
            for (int i = 0; i < Clients.Count; i++)
            {
                if (Clients[i].id == item.id)
                {
                    Clients[i] = item;
                    Clients[i].state = item.state;
                    Clients[i].population = item.population;
                    SengNotification(OrderSystemEvent.REFRESH, Clients[i]);
                    return;
                }
            }
        }
    }
}
