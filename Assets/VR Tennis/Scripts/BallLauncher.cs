using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace SparkVR
{
    public class BallLauncher : MonoBehaviour
    {
        /// <summary>
        /// detected the ball to remove.
        /// </summary>
        int DetectedBallToRemoveFrequency = 300;
        public Transform TopPoint;
        public Transform TargetPoint;
        public Transform StartPoint;
        public Transform StartPointOnGround;
        public Rigidbody Ball;
        public Racket racket;
        float startTime;
        GameMode gameMode;
#if STEAM_VR
        SteamVR_TestController InputComp;
#endif
        List<TennisBall> BallsLaunched = new List<TennisBall>();
        int CurrentFrameCount;
        // Use this for initialization
        void Start()
        {
#if STEAM_VR
            InputComp = FindObjectOfType<SteamVR_TestController>();
#endif
            gameMode = FindObjectOfType<GameMode>();
            startTime = Time.time;
        }


        // Update is called once per frame
        void Update()
        {
#if STEAM_VR
            if(InputComp.TriggerDown)
            {
                SpawnBall();
            }

            if(InputComp.PadPressDown)
            {
                gameMode.ResetPos();
            }
#endif
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SpawnBall();
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                gameMode.ResetPos();
            }

            CurrentFrameCount++;

            if(CurrentFrameCount % DetectedBallToRemoveFrequency == 0)
            {
                RemoveUnusedBall();
            }
        }

        void SpawnBall()
        {
            GameObject go = Instantiate(Ball.gameObject);
            go.transform.position = racket.SelfTrans.position + Vector3.up;
            TennisBall ball = go.GetComponent<TennisBall>();
            ball.RigidBody.velocity = Vector3.up * 6;

            gameMode.StartServe();
            racket.SetBall(ball);
            gameMode.SetBall(ball);
            BallsLaunched.Add(ball);
        }

        private void RemoveUnusedBall()
        {
            List<TennisBall> ballToRemove = new List<TennisBall>();

            for(int i = 0;i < BallsLaunched.Count;++i)
            {
                TennisBall ball = BallsLaunched[i];

                if(!ball.Playing)
                {
                    ballToRemove.Add(ball);
                }
            }

            if(ballToRemove.Count > 0)
            {
                for(int i = 0;i < ballToRemove.Count;++i)
                {
                    BallsLaunched.Remove(ballToRemove[i]);
                    Destroy(ballToRemove[i].gameObject);
                }

                ballToRemove.Clear();
            }
        }

    }

}