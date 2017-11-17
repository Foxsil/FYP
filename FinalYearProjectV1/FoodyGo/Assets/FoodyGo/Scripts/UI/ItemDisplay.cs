using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using packt.FoodyGO.Services;
using packt.FoodyGO.Database;
using packt.FoodyGO.Managers;
using UnityEngine.UI;

namespace packt.FoodyGO.UI
{
    public class ItemDisplay : MonoBehaviour
    {
        public GameObject[] itemButtonsList;

        public Toggle toggle;

        //private bool showOrHide;

        public void MyListener(bool value)
        {
            if (value)
            {
                ShowButton();
            }
            else
            {
                HideButton();
            }

        }

        public void Update()
        {
            toggle.onValueChanged.AddListener((AROn) => {
                   MyListener(AROn);
                });
        }

        public void ShowButton()
        {
            foreach (var g in itemButtonsList)
            {
                g.SetActive(true);
            }
        }

        public void HideButton()
        {
            foreach (var g in itemButtonsList)
            {
                g.SetActive(false);
            }
        }


    }
}