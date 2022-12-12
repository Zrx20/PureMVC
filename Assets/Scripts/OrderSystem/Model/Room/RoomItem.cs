namespace OrderSystem
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public enum RoomState
    {
        idle,
        busy,
        pay
    }
    public class RoomItem
    {
        public int id { get; set; }
        public int num { get; set; }
        public RoomState state { get; set; }
        public RoomItem(int id,int num, RoomState state)
        {
            this.id = id;
            this.num = num;
            this.state = state;
        }
        public override string ToString()
        {
            return id + "号房间\n" + num +"个人" +"\n"+ ResultState(state);
        }
        private string ResultState(RoomState roomState)
        {
            if (roomState.Equals(RoomState.idle))
            {
                return "空房间";
            }
            if (roomState.Equals(RoomState.busy))
            {
                return "有人";
            }
            return "人走了";
        }
    }
}