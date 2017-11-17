using UnityEngine;
using System.Collections;
using packt.FoodyGO.Services;
using packt.FoodyGO.Database;
using packt.FoodyGO.Managers;
using UnityEngine.UI;
using System.Collections.Generic;


namespace packt.FoodyGO.Controllers
{
    public class CatchSceneController : MonoBehaviour
    {
        public Transform frozenParticlePrefab;
        //public MonsterController monster;
        public GameObject[] battleSceneList;
        public GameObject[] winnningSceneList;
        public GameObject[] escapeSceneList;

        public Transform monsterDeadParticlePrefab;

        public Transform escapeParticlePrefab;
        public float monsterChanceEscape;
        public float monsterWarmRate;
        public bool catching;
        public Monster currentMonster { get; set; }
        private GameObject spawnMonster;

        public List<GameObject> monstersPrefab;
        public GameObject camera;
        private Transform target;


        protected Slider m_Slider;                             // The slider to represent how much health the tank currently has.
        protected Image m_FillImage;                           // The image component of the slider.
        private Color m_FullHealthColor = Color.green;       // The color the health bar will be when on full health.
        private Color m_ZeroHealthColor = Color.red;         // The color the health bar will be when on no health.
        protected Text HPText, MonsterNameText, MonsterLvText;
        public Text exitText, escapeText, escapeTimeText;
        private float timeRemaining;
        public string[] escapeWord = new string[2]; 

        private float m_CurrentHealth;                      // How much health the tank currently has.
        private bool m_Dead, displayTime, escapeDisplayTime;                                // Has the tank been reduced beyond zero health yet?

        void Start()
        {
            displayTime = false;
            escapeDisplayTime = false;

            target = camera.transform;

            SetupMonster();

            if (currentMonster != null)
            {
                spawnMonster = Instantiate(monstersPrefab[currentMonster.PrefabID], new Vector3(0, 0, -2.5f), Quaternion.identity) as GameObject;

                spawnMonster.gameObject.transform.parent = transform;

                //var controller = spawnMonster.gameObject.AddComponent<MonsterHP>();

                //controller.MonsterUIInfo = currentMonster;

                if (currentMonster.PrefabID <= 3)
                {
                    spawnMonster.transform.localScale = new Vector3(5, 5, 5);
                }
                else
                {
                    spawnMonster.transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
                }

                //spawnMonster.transform.localScale = new Vector3(5, 5, 5);
            }
            else
            {
                foreach (var g in battleSceneList)
                {
                    g.SetActive(false);
                }
            }

            if (target != null && currentMonster != null)
            {
                target = camera.transform;

                spawnMonster.transform.LookAt(target);


                print(currentMonster);

                monsterChanceEscape = currentMonster.Power * currentMonster.Level;
                monsterWarmRate = .0001f * currentMonster.Power;
                catching = true;

                StartCoroutine(CheckEscape());
            }
        }

        IEnumerator CheckEscape()
        {
            while (!m_Dead)
            {
                yield return new WaitForSeconds(30);
                if (Random.Range(0, 100) < monsterChanceEscape && !m_Dead)
                {
                    m_Dead = true;

                    StartCoroutine(escapeSummaryScene(1));

                    //Destroy(spawnMonster);
                    //var escape = Instantiate(escapeParticlePrefab);
                    //escape.parent = transform;
                    //Destroy(escape.gameObject, 2);
                    //foreach (var g in battleSceneList)
                    //{
                    //    g.SetActive(false);
                    //}

                    //foreach (var g in escapeSceneList)
                    //{
                    //    g.SetActive(true);
                    //}

                    //StartCoroutine(CloseScene(2));
                }
            }
        }

        public void EscapeBattle()
        {
            //foreach (var g in battleSceneList)
            //{
            //    g.SetActive(false);
            //}
            //Destroy(spawnMonster);
            //var escape = Instantiate(escapeParticlePrefab);
            //escape.parent = transform;
            //Destroy(escape.gameObject, 2);



            //StartCoroutine(CloseScene(2));


            StartCoroutine(escapeSummaryScene(0));

        }

        public void ExitBattle()
        {
            foreach (var g in winnningSceneList)
            {
                g.SetActive(false);
            }
            StartCoroutine(CloseScene(0.5f));
        }

        IEnumerator CloseScene(float delayDuration)
        {
            //delay 5 seconds before closing scene
            yield return new WaitForSeconds(delayDuration);
            StopAllCoroutines();
            GameManager.Instance.CloseMe(this);
        }


        IEnumerator escapeSummaryScene(int x)
        {
            timeRemaining = 3.0f;

            Destroy(spawnMonster);
            var escape = Instantiate(escapeParticlePrefab);
            escape.parent = transform;
            Destroy(escape.gameObject, 2);
            foreach (var g in battleSceneList)
            {
                g.SetActive(false);
            }

            foreach (var g in escapeSceneList)
            {
                g.SetActive(true);
            }

            if (x == 0)
            {
                escapeText.text = escapeWord[0].ToString();
            }
            else if (x == 1)
            {
                escapeText.text = escapeWord[1].ToString();
            }

            escapeDisplayTime = true;

            yield return new WaitForSeconds(3);

            escapeDisplayTime = false;

            StartCoroutine(CloseScene(0f));
        }

        IEnumerator winningSummaryScene()
        {
            yield return new WaitForSeconds(1);

            timeRemaining = 10.0f;

            foreach (var g in winnningSceneList)
            {
                g.SetActive(true);
            }

            displayTime = true;

            yield return new WaitForSeconds(10);

            displayTime = false;

            StartCoroutine(CloseScene(0f));
        }

        public void ResetScene()
        {
            foreach (var g in battleSceneList)
            {
                g.SetActive(true);
            }
            foreach (var g in winnningSceneList)
            {
                g.SetActive(false);
            }
            foreach (var g in escapeSceneList)
            {
                g.SetActive(false);
            }

            //monster.animationSpeed = 0.1f;
            Start();
        }


        public void showTimeRemaining()
        {
            if (displayTime || escapeDisplayTime)
            {
                if (timeRemaining > 0)
                {
                    timeRemaining -= Time.deltaTime;

                    if (displayTime)
                    {
                        exitText.text = "Returning to map in " + Mathf.Round(timeRemaining).ToString();
                    }
                    else if (escapeDisplayTime)
                    {
                        escapeTimeText.text = "Returning to map in " + Mathf.Round(timeRemaining).ToString();
                    }
                }
                else
                {     
                    if (displayTime)
                    {
                        exitText.text = "Returning to map in 0";
                    }
                    else if (escapeDisplayTime)
                    {
                        escapeTimeText.text = "Returning to map in 0";
                    }

                }
            }
            else
                return;
        }

        //public void OnMonsterHit(GameObject go, Collision collision)
        //{
        //    monster = go.GetComponent<MonsterController>();            

        //    if (monster != null)
        //    {
        //        monster.monsterWarmRate = monsterWarmRate;
        //        print("Monster hit");
        //        var animSpeedReduction = Mathf.Sqrt(collision.relativeVelocity.magnitude) / 10;
        //        monster.animationSpeed = Mathf.Clamp01(monster.animationSpeed - animSpeedReduction);
        //        if (monster.animationSpeed == 0)
        //        {
        //            print("Monster FROZEN");
        //            //save the monster in the player inventory
        //            InventoryService.Instance.CreateMonster(monsterProps);

        //            //updated code needed to store particle
        //            //set parent and then destroy object after a delay
        //            var frozen = Instantiate(frozenParticlePrefab);
        //            frozen.parent = transform;
        //            Destroy(frozen.gameObject, 5);

        //            foreach(var g in frozenDisableList)
        //            {
        //                g.SetActive(false);
        //            }
        //            foreach(var g in frozenEnableList)
        //            {
        //                g.SetActive(true);
        //            }
        //            StartCoroutine(CloseScene());
        //        }
        //    }
        //}


  


        private void SetupMonster()
        {
            if (currentMonster != null)
            {
                m_CurrentHealth = currentMonster.HP;
                m_Dead = false;

                m_Slider = GameObject.Find("MonsterHPSlider").GetComponent<Slider>();
                m_FillImage = GameObject.Find("HealthBar").GetComponent<Image>();
                HPText = GameObject.Find("Monster HP").GetComponent<Text>();
                MonsterNameText = GameObject.Find("MonsterNameText").GetComponent<Text>();
                MonsterLvText = GameObject.Find("MonsterLVText").GetComponent<Text>();

                // Update the health slider's value and color.
                SetHealthUI();
            }
            else
                return;
        }

        public void TakeDamage(float amount)
        {
            // Reduce current health by the amount of damage done.
            m_CurrentHealth -= amount;

            // Change the UI elements appropriately.
            SetHealthUI();

            // If the current health is at or below zero and it has not yet been registered, call OnDeath.
            if (m_CurrentHealth <= 0f && !m_Dead)
            {
                OnDeath();

               StartCoroutine(winningSummaryScene());
            }
        }


        private void SetHealthUI()
        {
            // Set the slider's value appropriately.
            m_Slider.value = m_CurrentHealth / currentMonster.HP;

            HPText.text = m_CurrentHealth.ToString();

            MonsterNameText.text = currentMonster.Name.ToString();

            MonsterLvText.text = currentMonster.Level.ToString();

            // Interpolate the color of the bar between the choosen colours based on the current percentage of the starting health.
            m_FillImage.color = Color.Lerp(m_ZeroHealthColor, m_FullHealthColor, m_CurrentHealth / currentMonster.HP);
        }


        public void Update()
        {

            showTimeRemaining();

            if (Input.GetMouseButtonDown(0) && m_CurrentHealth > 0)
            {

                RaycastHit hitInfo = new RaycastHit();

                bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);

                if (hit)
                {
                    Debug.Log("Hit " + hitInfo.transform.gameObject.name);
                    if (hitInfo.transform.gameObject.tag == "Monster") //&& hitInfo.transform.gameObject.tag != "Canvas")
                    {
                        Debug.Log("It's working!");

                        TakeDamage(10);
                    }
                    else
                    {
                        Debug.Log("nopz");
                    }
                }
                else
                {
                    Debug.Log("No hit");
                }


            }



        }



        private void OnDeath()
        {
            // Set the flag so that this function is only called once.
            m_Dead = true;

            // Move the instantiated explosion prefab to the tank's position and turn it on.
            monsterDeadParticlePrefab.transform.position = spawnMonster.transform.position;
            monsterDeadParticlePrefab.gameObject.SetActive(true);

            Destroy(spawnMonster);


            foreach (var g in battleSceneList)
            {
                g.SetActive(false);
            }

            var particleEffect = Instantiate(monsterDeadParticlePrefab);
            particleEffect.parent = transform;
            Destroy(particleEffect.gameObject, 2);

            // Play the particle system of the tank exploding.
            //monsterDeadParticlePrefab.Play();

            // Play the tank explosion sound effect.
            //m_ExplosionAudio.Play();

            // Turn the tank off.
            //gameObject.SetActive(false);
            
        }


     


    }
}

