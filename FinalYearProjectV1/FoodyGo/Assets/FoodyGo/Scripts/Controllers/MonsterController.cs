using UnityEngine;
using System.Collections;
using packt.FoodyGO.Mapping;
using packt.FoodyGO.Services;
using packt.FoodyGO.Database;

namespace packt.FoodyGO.Controllers
{
    public class MonsterController : MonoBehaviour
    {
        public MapLocation location;
        public MonsterService monsterService;
        public MonsterSpawnLocation monsterSpawnLocation;
        public Animation anim;
        public float animationSpeed = 0.1f;
        public float monsterWarmRate = .0001f;
        public Monster currentMonster { get;  set; }

        public GameObject camera;
        private Transform target;

        // Use this for initialization
        void Start()
        {
            //anim = GetComponent<Animation>();
            //anim["Idle"].speed = animationSpeed;


        }

        private void OnEnable()
        {
            //anim = GetComponent<Animation>();
        }

        // Update is called once per frame
        void Update()
        {
            //if (animationSpeed == 0)
            //{
            //    //monster is frozen solid
            //    anim["Idle"].speed = 0;
            //    return;
            //}
            ////if monster is moving they will warm up a bit
            //animationSpeed = Mathf.Clamp01(animationSpeed + monsterWarmRate);

            if (target != null)
            {
                camera = GameObject.Find("Main Camera");

                target = camera.transform;

                transform.LookAt(target);
            }

        }

    }
}
