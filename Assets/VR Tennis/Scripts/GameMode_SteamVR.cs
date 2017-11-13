using UnityEngine;
using System.Collections;
namespace SparkVR
{
    public class GameMode_SteamVR : GameMode
    {
        // Use this for initialization
        void Start()
        {
        }

        public override void ResetPos()
        {
            base.ResetPos();
        }

        public override void StartServe()
        {
            base.StartServe();
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
        }

        protected override void HandleServe()
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

                if (currentBallHeight > 2.5f && servePower > 8f)
                {
                    serveDir.y = 0f;
                    serveDir.x = Mathf.Clamp(serveDir.x, -3f, -1f);
                    serveDir.z = Mathf.Clamp(Random.Range(22f, 30f), 22f, 30f);
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

        protected override void HandleYourTurn()
        {
            Vector3 currentSwipeDir = Vector3.zero;

            float racketPower = GamerRacket.CalculateSwipePowerAndDir(out currentSwipeDir);

            if (CachedPower.Count < 25)
            {
                CachedPower.Add(racketPower);
                CachedSwipeDir.Add(currentSwipeDir);
            }
            else
            {
                CachedPower.RemoveAt(0);
                CachedPower.Add(racketPower);
                CachedSwipeDir.RemoveAt(0);
                CachedSwipeDir.Add(currentSwipeDir);
            }

            Vector3 BallPos = CurrentBall.SelfTrans.position;
            BallVirtialPosition = BallPos + Vector3.forward * 2f;

            BallPos.y = 0f;
            Vector3 PlayerPos = Gamer.SelfTrans.position;
            PlayerPos.y = 0f;

            float playerDistFromBall = Vector3.Distance(PlayerPos, BallPos);
            bool isInHitRange = false;

            if (playerDistFromBall > 2.5f && playerDistFromBall < 3.5f)
            {
                // the ball enter tips range
                CurrentBall.SetColor(Color.green);
            }
            else if (playerDistFromBall < 2.5f && playerDistFromBall > 0.5f && BallPos.z > PlayerPos.z)
            {
                // the ball enter hit range
                CurrentBall.SetColor(Color.red);
                isInHitRange = true;
            }
            else
            {
                // the ball in normal fly state.
                CurrentBall.SetColor(Color.white);
            }

            bool canHitNow = false;

            float distBetweenBallAndRacket = Vector3.Distance(BallVirtialPosition, GamerRacket.DetectedTrans.position);

            if (distBetweenBallAndRacket > PreDistBetweenBallAndRacket)
            {
                canHitNow = true;
            }

            PreDistBetweenBallAndRacket = distBetweenBallAndRacket;

            // racket hit the ball
            if (isInHitRange && racketPower > 5f && canHitNow)
            {
                Vector3 fixSwipeDir;
                float fixPower = GetMaxPower(out fixSwipeDir);
                if (fixPower < racketPower)
                    fixPower = racketPower;

                Vector3 finalDir = currentSwipeDir;

                Debug.DrawRay(CurrentBall.SelfTrans.position, finalDir, Color.red, 10f);

                // make the fly direction in a property range.
                Rect restrict = CalculateBoundary(Gamer.SelfTrans.position);
                //finalDir.z += 3f;// fixed z axis
                finalDir.y += 1.5f;// fixed y axis
                finalDir.y = Mathf.Clamp(finalDir.y, -2f, 8f);
                finalDir.x = Mathf.Clamp(finalDir.x, restrict.xMin, restrict.xMax);
                // z direction we just use the power instead of finalDir.z
                finalDir.z = fixPower;
                CurrentBall.RigidBody.velocity = finalDir;
                CurrentBall.OnHit();

                // calculate the destination point of the tennis ball.
                AICurrentDestPosition = BezierHelper.GetTargetPoint(CurrentBall.RigidBody, CurrentBall.SelfTrans.position, finalDir, 30);
                if (AICurrentDestPosition.z > 0)
                {
                    // let AI player move to the destination point.
                    //AIPlayer.MoveToPosition(AIPlayer.SelfTrans.position, AICurrentDestPosition + Vector3.forward * 2.5f, 8f);
                    AIPlayer.MoveToPosition(AICurrentDestPosition + Vector3.forward * 2.5f, 1f);
                    GamerRacket.ResetRacket();
                }

                CachedPower.Clear();
                CachedSwipeDir.Clear();
                CurrentState = eState.OthersTurn;
            }
        }

        protected override void HandleOthersTurn()
        {
            base.HandleOthersTurn();
        }

        protected override void Update()
        {
            GamerRacket.HandleSteamRacket();
        }
    }

}
