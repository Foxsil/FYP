using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SQLite4Unity3d;
using UnityEngine.UI;
using packt.FoodyGO.Services;
using System;
using packt.FoodyGO.Database;

namespace packt.FoodyGO.UI
{

    public class PlayerInfo : MonoBehaviour
    {

        //protected Text levelText;
        //protected Text expText;
        //protected Text playerNameText;
        //protected RawImage Image;
        //private Monster monster;

        public Text levelText;
        public Text expText;
        public Text playerNameText;

        // Use this for initialization
        void Start()
        {
            //levelText = transform.Find("Level_Text").gameObject.GetComponent<Text>();
            //expText = transform.Find("Exp_Text").gameObject.GetComponent<Text>();            
            //playerNameText = transform.Find("Player_Name_Text").gameObject.GetComponent<Text>();
            //Image = transform.Find("RawImage").gameObject.GetComponent<RawImage>();

            //monster = value as Monster;
            //if (monster == null) return;

            //TopText.text = "CP " + (monster.Power * monster.Level).ToString();
            //BottomText.text = monster.Name;

            var player = InventoryService.Instance.ReadPlayer(1);
            expText.text = player.Experience.ToString();
            levelText.text = player.Level.ToString();
            playerNameText.text = player.Id.ToString();
        }

        private void OnEnable()
        {
            //updateInfo();
        }

        private void updateInfo()
        {
            var player = InventoryService.Instance.ReadPlayer(1);

            if (player.Experience.ToString() != expText.text)
                expText.text = player.Experience.ToString();
            if (player.Level.ToString() != levelText.text)
                levelText.text = player.Level.ToString();

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
