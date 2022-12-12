namespace OrderSystem
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using PureMVC.Patterns;

    public class MenuProxy : Proxy
    {
        public new const string NAME = "MenuProxy";
        public IList<MenuItem> Mens
        {
            get { return (IList<MenuItem>)base.Data; }
        }
        public MenuProxy():base(NAME,new List<MenuItem>())
        {
            AddMenu(new MenuItem(1, "小龙虾", 99, true));
            AddMenu(new MenuItem(2, "米饭", 5, true));
            AddMenu(new MenuItem(3, "土豆牛肉", 49, true));
            AddMenu(new MenuItem(4, "豆腐", 13, false));
            AddMenu(new MenuItem(5, "蛋汤", 9, true));
            AddMenu(new MenuItem(6, "小炒肉", 29, true));
            AddMenu(new MenuItem(7, "驴肉火烧", 9, false));
            AddMenu(new MenuItem(8, "火锅", 119, true));
            AddMenu(new MenuItem(9, "包子", 5, true));
            AddMenu(new MenuItem(10, "馒头", 2, true));
        }
        public override void OnRegister()
        {
            base.OnRegister();
        }
        public void AddMenu(MenuItem item)
        {
            if (!Mens.Contains(item))
            {
                Mens.Add(item);
            }
        }
        public void Remove(MenuItem item)
        {
            if (Mens.Contains(item))
            {
                Mens.Remove(item);
            }
        }
        public void OutOfStock(MenuItem item)
        {
            foreach (MenuItem menuItem in Mens)
            {
                if (menuItem.id == item.id)
                {
                    menuItem.instock = false;
                    return;
                }
            }
        }
    }
}
