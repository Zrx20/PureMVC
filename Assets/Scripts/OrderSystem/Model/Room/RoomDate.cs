namespace OrderSystem
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class RoomDate 
    {
        public int id { get; set; }
        public RoomItem roomItem { get; set; }
        public RoomDate(RoomItem roomItem)
        {
            this.roomItem = roomItem;
        }
        public override string ToString()
        {
            return roomItem.id + "客房" + roomItem.num + "人入住";
        }
    }
}
