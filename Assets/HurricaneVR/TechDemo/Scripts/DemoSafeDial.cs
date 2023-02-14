using HurricaneVR.Framework.Components;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace HurricaneVR.TechDemo.Scripts
{
    public enum SafeDialState
    {
        CamOne,
        CamTwo,
        CamThree,
        Unlocked
    }

    public class DemoSafeDial : HVRRotationTracker
    {
        public TextMeshPro NumberLabel;
        public TextMeshPro DebugLabel;
        public bool DisplayDebug;

        public int First = 30;
        public int Second = 15;
        public int Third = 01;

        public int CurrentNumber;

        public float CamDistance;
        public float PreviousDistance;

        public float Tolerance = 40f;

        public float LowerBound = 0f;
        public float UpperBound = 0f;

        public int AccuracyAllowance = 1;

        public UnityEvent Unlocked = new UnityEvent();

        private SafeDialState _state;

        public SafeDialState State
        {
            get { return _state; }
            set
            {
                _state = value;
                ComputeBounds();
            }
        }

        public int NumberOfRotations
        {
            get
            {
                return ((int)Mathf.Abs(CamDistance)) / 355;
            }
        }

        protected override void Start()
        {
            base.Start();
            ResetLockState(SafeDialState.CamOne);

            if (DebugLabel)
            {
                DebugLabel.text = $"Code:{First},{Second},{Third}\r\n Dist: {CamDistance:f0}\r\nState: {State}\r\nTolerance: {Tolerance:f0}\r\nL_Bound: {LowerBound:f0}\r\nU_Bound: {UpperBound:f0}";
            }
        }

        private void ComputeBounds()
        {
            switch (State)
            {
                case SafeDialState.CamOne:
                    LowerBound = 0f;
                    UpperBound = 1080f;
                    break;
                case SafeDialState.CamTwo:
                    LowerBound = -360f - (360 - Second * StepSize);
                    UpperBound = 0f + Tolerance;
                    break;
                case SafeDialState.CamThree:

                    if (Third < Second)
                    {
                        UpperBound = (Steps - Second + Third) * StepSize;
                    }
                    else
                    {
                        UpperBound = (Third - Second) * StepSize;
                    }

                    LowerBound = 0f;

                    break;
                case SafeDialState.Unlocked:
                    break;
            }

            LowerBound -= Tolerance;
            UpperBound += Tolerance;
        }

        protected override void Update()
        {
            base.Update();
        }

        public bool IsFirstInRange => CurrentNumber >= First - AccuracyAllowance && CurrentNumber <= First + AccuracyAllowance;
        public bool IsSecondInRange => CurrentNumber >= Second - AccuracyAllowance && CurrentNumber <= Second + AccuracyAllowance;
        public bool IsThirdInRange => CurrentNumber >= Third - AccuracyAllowance && CurrentNumber <= Third + AccuracyAllowance;


        public void ResetLockState(SafeDialState state)
        {
            State = state;
            CamDistance = 0f;
            PreviousDistance = 0f;
        }

        protected override void OnStepChanged(int step, bool raiseEvents)
        {
            base.OnStepChanged(step, raiseEvents);

            CurrentNumber = step;

            if (NumberLabel)
            {
                NumberLabel.text = CurrentNumber.ToString("n0");
            }

            if (DebugLabel)
            {
                DebugLabel.text = $"Code:{First},{Second},{Third}\r\n Dist: {CamDistance:f0}\r\nState: {State}\r\nTolerance: {Tolerance:f0}\r\nL_Bound: {LowerBound:f0}\r\nU_Bound: {UpperBound:f0}";
            }
        }

        protected override void OnAngleChanged(float angle, float delta)
        {
            CamDistance += delta;

            if (CamDistance < LowerBound)
            {
                ResetLockState(SafeDialState.CamOne);
            }
            else if (CamDistance > UpperBound && State != SafeDialState.CamOne)
            {
                if (State == SafeDialState.CamTwo)
                {
                    CamDistance = 1080f;
                    State = SafeDialState.CamOne;
                }
                else
                {
                    ResetLockState(SafeDialState.CamOne);
                }
            }

            if (State == SafeDialState.CamOne && NumberOfRotations >= 3 && IsFirstInRange)
            {
                ResetLockState(SafeDialState.CamTwo);
            }
            else if (State == SafeDialState.CamTwo && NumberOfRotations == 1 && IsSecondInRange)
            {
                ResetLockState(SafeDialState.CamThree);
            }
            else if (State == SafeDialState.CamThree && IsThirdInRange)
            {
                State = SafeDialState.Unlocked;
                Unlocked.Invoke();
            }

        }
    }
}