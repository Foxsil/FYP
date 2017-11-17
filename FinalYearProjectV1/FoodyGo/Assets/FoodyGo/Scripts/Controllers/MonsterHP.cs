using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using packt.FoodyGO.Mapping;
using packt.FoodyGO.Services;
using packt.FoodyGO.Database;

namespace packt.FoodyGO.Controllers
{
    public class MonsterHP : MonoBehaviour
    {
        public Monster MonsterUIInfo { get; set; }

        //public float m_StartingHealth = 100f;               // The amount of health each tank starts with.
        protected Slider m_Slider;                             // The slider to represent how much health the tank currently has.
        protected Image m_FillImage;                           // The image component of the slider.
        private Color m_FullHealthColor = Color.green;       // The color the health bar will be when on full health.
        private Color m_ZeroHealthColor = Color.red;         // The color the health bar will be when on no health.
        protected Text HPText, MonsterNameText, MonsterLvText;
        //public GameObject m_ExplosionPrefab;                // A prefab that will be instantiated in Awake, then used whenever the tank dies.


        //private AudioSource m_ExplosionAudio;               // The audio source to play when the tank explodes.
        //private ParticleSystem m_ExplosionParticles;        // The particle system the will play when the tank is destroyed.
        private float m_CurrentHealth;                      // How much health the tank currently has.
        private bool m_Dead;                                // Has the tank been reduced beyond zero health yet?


        //private void Awake()
        //{
        //    Instantiate the explosion prefab and get a reference to the particle system on it.
        //    m_ExplosionParticles = Instantiate(m_ExplosionPrefab).GetComponent<ParticleSystem>();

        //    Get a reference to the audio source on the instantiated prefab.
        //   m_ExplosionAudio = m_ExplosionParticles.GetComponent<AudioSource>();

        //    Disable the prefab so it can be activated when it's required.
        //    m_ExplosionParticles.gameObject.SetActive(false);
        //}


        private void Start()
        {
            SetupMonster();
        }


        //public void OnEnable()
        //{
        //    SetupMonster();
        //}


        private void SetupMonster()
        {
            if (MonsterUIInfo != null)
            {
                m_CurrentHealth = MonsterUIInfo.HP;
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
                //OnDeath();
            }
        }


        private void SetHealthUI()
        {
            // Set the slider's value appropriately.
            m_Slider.value = m_CurrentHealth / MonsterUIInfo.HP;

            HPText.text = m_CurrentHealth.ToString();

            MonsterNameText.text = MonsterUIInfo.Name.ToString();

            MonsterLvText.text = MonsterUIInfo.Level.ToString();

            // Interpolate the color of the bar between the choosen colours based on the current percentage of the starting health.
            m_FillImage.color = Color.Lerp(m_ZeroHealthColor, m_FullHealthColor, m_CurrentHealth / MonsterUIInfo.HP);
        }


        public void Update()
        {


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

                        TakeDamage(1);
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

        }

        //private void OnDeath()
        //{
        //    // Set the flag so that this function is only called once.
        //    m_Dead = true;

        //    // Move the instantiated explosion prefab to the tank's position and turn it on.
        //    m_ExplosionParticles.transform.position = transform.position;
        //    m_ExplosionParticles.gameObject.SetActive(true);

        //    // Play the particle system of the tank exploding.
        //    m_ExplosionParticles.Play();

        //    // Play the tank explosion sound effect.
        //    m_ExplosionAudio.Play();

        //    // Turn the tank off.
        //    gameObject.SetActive(false);
        //}
    }
