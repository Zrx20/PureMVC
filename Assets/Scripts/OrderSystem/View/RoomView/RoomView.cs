namespace OrderSystem
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Events;

    public class RoomView : MonoBehaviour
    {
        public UnityAction<RoomItem> callRoom = null;
        private ObjectPool<RoomItemView> objectPool = null;
        private List<RoomItemView> rooms = new List<RoomItemView>();
        private Transform parent = null;
        public UnityAction<Order> order = null;
        public UnityAction Pay = null;
        private void Awake()
        {
            parent = this.transform.Find("Content");
            var prefab = Resources.Load<GameObject>("Prefabs/UI/RoomItem");
            objectPool = new ObjectPool<RoomItemView>(prefab, "RoomPool");
        }
        public void UpdateRoom(IList<RoomItem> rooms, IList<Action<object>> objs)
        {
            for (int i = 0; i < this.rooms.Count; i++)
            {
                objectPool.Push(this.rooms[i]);
            }
            this.rooms.AddRange(objectPool.Pop(rooms.Count));
            for (int i = 0; i < this.rooms.Count; i++)
            {
                var room = this.rooms[i];
                room.transform.SetParent(parent);
                room.InitClient(rooms[i]);
                room.actionList = objs;
            }
        }
        public void UpdateState(RoomItem room)
        {
            for (int i = 0; i < rooms.Count; i++)
            {
                if (rooms[i].roomItem.id.Equals(room.id))
                {
                    rooms[i].InitClient(room);
                }
            }
        }
        public void RefrshClient(IList<RoomItem> Reclients)
        {
            for (int i = 0; i < Reclients.Count; i++)
            {
                UpdateState(Reclients[i]);
            }
        }
    }
}
