using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace SparkVR
{
    public class GameMode : MonoBehaviour
    {
        protected enum eState
        {
            YouServe,// You Serve
            OhtersServe,// AI Serve
            YourTurn,// Your turn
            OthersTurn,// Others turn (AI)
            Idle,
        }

        public Transform RacketRootTrans;
        public Racket GamerRacket;
        public Player AIPlayer;
        public Transform EndPoint;
        public Player Gamer;
        protected TennisBall CurrentBall;
        protected eState CurrentState;
        protected float PreDistBetweenBallAndRacket = float.MaxValue;
        protected Vector3 AICurrentDestPosition = Vector3.zero;
        protected Vector3 BallVirtialPosition;
        protected List<float> CachedPower = new List<float>();
        protected List<Vector3> CachedSwipeDir = new List<Vector3>();

        public virtual void ResetPos()
        {
            GamerRacket.ResetRacket();
            RacketRootTrans.position = new Vector3(0.33f, 1f, -12.5f);
            Gamer.SelfTrans.position = new Vector3(0f, 0f, -13f);
            CurrentState = eState.Idle;
        }

        public void SetBall(TennisBall ball)
        {
            if (CurrentBall != null)
                CurrentBall.Playing = false;
            CurrentBall = ball;
            CurrentBall.Playing = true;
        }

        public virtual void StartServe()
        {
            GamerRacket.ResetRacket();
            CurrentState = eState.YouServe;
        }

        // Update is called once per frame
        protected virtual void Update()
        {

        }

        protected virtual void FixedUpdate()
        {
            HandleGameProcess();
        }

        /// <summary>
        /// Game Main Process
        /// </summary>
        protected void HandleGameProcess()
        {
            if (CurrentState == eState.YouServe)
            {
                // Handle Serve
                HandleServe();
            }
            else if (CurrentState == eState.YourTurn)
            {
                // your turn
                HandleYourTurn();
            }
            else if (CurrentState == eState.OthersTurn)
            {
                // other's turn
                HandleOthersTurn();
            }
            else
            {
                // Idle
            }
        }

        /// <summary>
        /// handle Serve
        /// </summary>
        protected virtual void HandleServe()
        {
            if (CurrentBall != null)
            {
                Vector3 serveDir = Vector3.zero;
                float servePower = GamerRacket.CalculateSwipePowerAndDir(out serveDir);
                float currentBallHeight = CurrentBall.SelfTrans.position.y;
                Vector3 racketWantedPos = RacketRootTrans.position;
                racketWantedPos.y = currentBallHeight - 0.5f;
                racketWantedPos.y = Mathf.Clamp(racketWantedPos.y, 1f, 3f);
                RacketRootTrans.position = racketWantedPos;

                if (currentBallHeight > 3.5f)
                {
                    serveDir.y = 0f;
                    serveDir.x = Mathf.Clamp(serveDir.x, -3f, -1f);
                    serveDir.z = Mathf.Clamp(Random.Range(18f, 25f), 18f, 25f);
                    CurrentBall.RigidBody.velocity = serveDir;
                    CurrentBall.OnHit();

                    // calculate the destination of the tennis ball.
                    AICurrentDestPosition = BezierHelper.GetTargetPoint(CurrentBall.RigidBody, CurrentBall.SelfTrans.position, serveDir, 30);

                    // AI move to target position
                    AIPlayer.MoveToPosition(AICurrentDestPosition + Vector3.forward * 2.5f, 1f);
                    GamerRacket.ResetRacket();
                    racketWantedPos.y = 1f;
                    RacketRootTrans.position = racketWantedPos;
                    // turn to others turn
                    CurrentState = eState.OthersTurn;
                }
            }
        }

        /// <summary>
        /// handle your turn
        /// </summary>
        protected virtual void HandleYourTurn()
        {

        }

        /// <summary>
        /// Handle Others turn(AI)
        /// </summary>
        protected virtual void HandleOthersTurn()
        {
            if (CurrentBall == null)
                return;

            Vector3 BallPos = CurrentBall.SelfTrans.position;
            Vector3 AIPos = AIPlayer.SelfTrans.position;
            float ret = Vector3.Distance(BallPos, AIPos);
            float distFromDestPoint = Vector3.Distance(AIPos, AICurrentDestPosition);

            if (ret < 2f)
            {
                // calculate hit direction
                Vector3 wantedTargetPos = Random.insideUnitSphere;
                wantedTargetPos.y = 0f;
                wantedTargetPos = wantedTargetPos.normalized * 2f;
                wantedTargetPos += EndPoint.position;
                wantedTargetPos.y = 0;

                // calculate the wanted velocity of the tennis ball's rigidbody.use newton s low
                Vector3 flyDir = BezierHelper.CalulateStartVelocity(1.2f, wantedTargetPos, CurrentBall.SelfTrans.position);
                CurrentBall.RigidBody.velocity = flyDir;

                Vector3 left = (Random.Range(-1f, 1f) >= 0 ? 0.8f : -0.8f) * Vector3.right;
                // move the racket to the destination
                Vector3 racketWantedPos = wantedTargetPos + Vector3.ProjectOnPlane(flyDir, Vector3.up).normalized * 2f;
                racketWantedPos.y = RacketRootTrans.position.y;
                RacketRootTrans.position = racketWantedPos;

                // Player move to destination point
                Vector3 playerWantedPos = racketWantedPos + Vector3.back + left;
                playerWantedPos.y = Gamer.SelfTrans.position.y;
                Gamer.MoveToPosition(Gamer.SelfTrans.position, playerWantedPos, 4f);

                CurrentBall.OnHit();
                GamerRacket.ResetRacket();
                PreDistBetweenBallAndRacket = float.MaxValue;
                CurrentState = eState.YourTurn;
            }
        }

        protected float GetMaxPower(out Vector3 swipeDir)
        {
            int wantedIndex = -1;

            swipeDir = Vector3.zero;

            float tmpPower = 0;

            for (int i = 0; i < CachedPower.Count; ++i)
            {
                if (CachedPower[i] > tmpPower)
                {
                    tmpPower = CachedPower[i];
                    wantedIndex = i;
                }
            }

            if (wantedIndex >= 0)
                swipeDir = CachedSwipeDir[wantedIndex];

            return tmpPower;
        }

        /// <summary>
        /// Calculate boundary
        /// </summary>
        /// <param name="PlayerPosition"></param>
        /// <returns></returns>
        protected Rect CalculateBoundary(Vector3 PlayerPosition)
        {
            // Ground Boundary
            // x -5 , 5
            // z -12,12

            Rect rect = new Rect();
            rect.xMin = -4 - PlayerPosition.x;
            rect.xMax = 4 - PlayerPosition.x;
            rect.yMin = -8 - PlayerPosition.z;
            rect.yMax = 8 - PlayerPosition.z;

            return rect;
        }

        private Vector3[] GizmosPoint;

        void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.color = Color.red;
            Gizmos.DrawLine(Gamer.SelfTrans.position + Vector3.up, Gamer.SelfTrans.position + Vector3.forward * 0.5f + Vector3.up);
            Gizmos.color = Color.green;
            Gizmos.DrawLine(Gamer.SelfTrans.position + Vector3.up * 0.9f, Gamer.SelfTrans.position + Vector3.forward + Vector3.up * 0.9f);
        }
    }
}