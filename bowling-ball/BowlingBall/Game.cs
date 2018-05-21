using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace BowlingGame
{
 

    public class BowlingGame
    {
       
        public int GetScore(ArrayList lstFrames)
        {
            int Score = 0;
            foreach (object fr in lstFrames)
            {
                Frame frAbs = (Frame)fr;
                if (frAbs.GetFrameType() == FrameType.NormalFrame)
                {
                    ActualFrame Absfr = (ActualFrame)fr;
                    Score = Score + Absfr.GetScore();
                }
                else if (frAbs.GetFrameType() == FrameType.SpareFrame)
                {
                    SpareFrame Absfr = (SpareFrame)fr;
                    Score = Score + Absfr.GetScore();
                }
                else if (frAbs.GetFrameType() == FrameType.StrikeFrame)
                {
                    StrikeFrame Absfr = (StrikeFrame)fr;
                    Score = Score + Absfr.GetScore();
                }
            }
            return Score;
        }

        public void AddRoll(int noOfPins, ArrayList lstFrames)
        {
            bool _isLastFrameCompleted = false;
            if (lstFrames.Count != 0)
            {
                if (lstFrames.Count >= 2)
                {
                    Frame objSecondLastFrame = (Frame)lstFrames[lstFrames.Count - 2];
                    if (objSecondLastFrame.GetFrameType() == FrameType.StrikeFrame)
                    {
                        StrikeFrame objStrikeFrame = (StrikeFrame)lstFrames[lstFrames.Count - 2];
                        if (objStrikeFrame.isSecondBonusRollSet()==false)
                        objStrikeFrame.SetSecondBonusRollPoint(noOfPins);
                    }
                }

                Frame objLastFrame = (Frame)lstFrames[lstFrames.Count - 1];
                if (objLastFrame.IsCompleted())
                {
                    _isLastFrameCompleted = true;
                    if (objLastFrame.GetFrameType() == FrameType.SpareFrame)
                    {
                        SpareFrame objSpareFrame = (SpareFrame)lstFrames[lstFrames.Count - 1];
                        objSpareFrame.SetBonusRollPoint(noOfPins);
                    }
                    if (objLastFrame.GetFrameType() == FrameType.StrikeFrame)
                    {
                        StrikeFrame objStrikeFrame = (StrikeFrame)lstFrames[lstFrames.Count - 1];
                        if (objStrikeFrame.isFirstBonusRollSet() && objStrikeFrame.isSecondBonusRollSet()==false)
                        {
                            objStrikeFrame.SetSecondBonusRollPoint(noOfPins);
                        }
                        else
                        {
                            objStrikeFrame.SetFirstBonusRollPoint(noOfPins);
                        }
                    }
                    
                }
                else
                {
                    ActualFrame objAbsLastFrame = (ActualFrame)lstFrames[lstFrames.Count - 1];
                    if (objAbsLastFrame.WillSpareFrame(noOfPins))
                    {
                        SpareFrame objsprFrame = new SpareFrame(objAbsLastFrame.GetFrameIndex(), objAbsLastFrame.GetFirstRollPin());
                        objsprFrame.AddSecondRollPins(noOfPins);
                        lstFrames[lstFrames.Count - 1] = objsprFrame;
                    }
                    else
                    {
                        objAbsLastFrame.AddSecondRollPins(noOfPins);
                    }
                }
            }
            if (lstFrames.Count < 10 && (lstFrames.Count == 0 || _isLastFrameCompleted))
            {
                int FrameIndex = lstFrames.Count + 1;
                Frame objFrame;
                if (noOfPins == 10)
                {
                    objFrame = new StrikeFrame(FrameIndex, noOfPins);
                }
                else
                {
                    objFrame = new ActualFrame(FrameIndex, noOfPins);
                }
                lstFrames.Add(objFrame);
            }

        }
    }
    public enum FrameType { NormalFrame = 0, SpareFrame = 1, StrikeFrame = 2 }

    interface IFrame
    {
        void AddFirstRollPins(int noOfpins);
        FrameType GetFrameType();
        bool isLastFrame();
        int GetFrameIndex();

        int GetScore();

        bool IsCompleted();

        bool IsBonusRollSet();
    }

    interface IActualFrame : IFrame
    {
        void AddSecondRollPins(int noOfpins);
    }

    public class Frame : IFrame
    {
        protected int FrameIndex;
        protected int _firstRollPins = 0;
        protected bool _isFrameComplete = false;
        protected FrameType _frameType;
        protected bool _isBonusRollSet = true;
        public Frame()
        {
            _frameType = FrameType.NormalFrame;
            FrameIndex = 0;
        }
        public Frame(int frameIndex, int noOfPins)
        {
            FrameIndex = frameIndex;
            _frameType = FrameType.NormalFrame;
            _firstRollPins = noOfPins;
            if (noOfPins == 10)
            {
                _frameType = FrameType.StrikeFrame;
                _isFrameComplete = true;
                _isBonusRollSet = false;
            }
        }

        public bool IsCompleted()
        {
            return _isFrameComplete;
        }
        public void AddFirstRollPins(int noOfpins)
        {
            _firstRollPins = noOfpins;
            if (noOfpins == 10)
            {
                _frameType = FrameType.StrikeFrame;
                _isFrameComplete = true;
                _isBonusRollSet = false;
            }
        }

        public FrameType GetFrameType()
        {
            return _frameType;
        }

        public bool isLastFrame()
        {
            return FrameIndex == 10;
        }

        public int GetFrameIndex()
        {
            return FrameIndex;
        }

        public virtual int GetScore()
        {
            return _firstRollPins;
        }

        public virtual bool IsBonusRollSet()
        {
            return _isBonusRollSet;
        }
    }

    public class ActualFrame : Frame, IActualFrame
    {

        protected int _secondRollPins = 0;

        public ActualFrame(int frameIndex, int noOfPins)
            : base(frameIndex, noOfPins)
        {
        }

        public void AddSecondRollPins(int noOfPins)
        {
            _secondRollPins = noOfPins;
            _isFrameComplete = true;
            if ((_firstRollPins + _secondRollPins) == 10)
            {
                _frameType = FrameType.SpareFrame;
                _isBonusRollSet = false;
            }
        }
        public override int GetScore()
        {
            return _firstRollPins + _secondRollPins;
        }

        public bool WillSpareFrame(int noOfPins)
        {
            return _firstRollPins + noOfPins == 10;
        }
        public int GetFirstRollPin()
        {
            return _firstRollPins;
        }
    }

    public class SpareFrame : ActualFrame
    {
        public int _bonusRoll = 0;
        public SpareFrame(int frameIndex, int noOfPins)
            : base(frameIndex, noOfPins)
        {
        }
        public void SetBonusRollPoint(int BonusRoll)
        {
            _bonusRoll = BonusRoll;
            _isBonusRollSet = true;
        }
        public override int GetScore()
        {
            return _firstRollPins + _secondRollPins + _bonusRoll;
        }      

    }

    public class StrikeFrame : Frame
    {
        public int _firstBonusRoll = 0;
        public int _secondBonusRoll = 0;
        public StrikeFrame(int frameIndex, int noOfPins) : base(frameIndex, noOfPins) { }
        public void SetFirstBonusRollPoint(int BonusRoll)
        {
            _firstBonusRoll = BonusRoll;
        }
        public void SetSecondBonusRollPoint(int BonusRoll)
        {
            _secondBonusRoll = BonusRoll;
            _isBonusRollSet = true;
        }
        public override int GetScore()
        {
            return _firstRollPins + _firstBonusRoll + _secondBonusRoll;
        }

        public bool isFirstBonusRollSet()
        {
            return _firstBonusRoll != 0;
        }
        public bool isSecondBonusRollSet()
        {
            return _secondBonusRoll != 0;
        }
    }
}
