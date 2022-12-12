namespace OrderSystem
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    using System;
    public enum ClientState
    {
        WaitMenu,
        WaitFood,
        Eating,
        Pay
    }

    public class ClientItemView : MonoBehaviour
    {
        private Text text = null;
        private Image image = null;
        public ClientItem client = null;
        public IList<Action<object>> actionList = new List<Action<object>>();
        public void Awake()
        {
            text = transform.Find("Id").GetComponent<Text>();
            image = transform.GetComponent<Image>();
        }
        public void InitClient(ClientItem client)
        {
            this.client = client;
            UpdateState();
        }
        private void UpdateState()
        {
            if (client == null)
            {
                return;
            }
            Color color = Color.white;
            switch (this.client.state)
            {
                case ClientState.WaitMenu:
                    color = Color.green;
                    break;
                case ClientState.WaitFood:
                    color = Color.yellow;
                    break;
                case ClientState.Eating:
                    color = Color.red;
                    StartCoroutine(eatting());
                    break;
                case ClientState.Pay:
                    StartCoroutine(AddGuest());
                    break;
            }
            image.color = color;
            text.text = client.ToString();
        }
        IEnumerator WaitingMenu(float time = 4)
        {
            yield return new WaitForSeconds(time);
            actionList[(int)client.state].Invoke(client);
        }
        /// <summary>
        /// 来客人了
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        IEnumerator AddGuest(float time = 4)
        {
            yield return new WaitForSeconds(time);
            actionList[0].Invoke(client);
        }
        private IEnumerator eatting(float time = 5)
        {
            Debug.Log(client.id + "号桌客人正在就餐");
            yield return new WaitForSeconds(time);
            Debug.Log(client.id + "客人离开饭店");
            client.state++;
            actionList[1].Invoke(client);
        }
    }
}
