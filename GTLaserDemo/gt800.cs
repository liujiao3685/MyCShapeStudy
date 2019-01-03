using System.Runtime.InteropServices;

namespace gt800
{
    public class mc
    {
        public const short PRF_MAP_MAX = 2;
        public const short ENC_MAP_MAX = 2;

        public const short STEP_DIR = 0;
        public const short STEP_PULSE = 1;

        public const short MC_NONE = -1;

        public const short MC_LIMIT_POSITIVE = 0;
        public const short MC_LIMIT_NEGATIVE = 1;
        public const short MC_ALARM = 2;
        public const short MC_HOME = 3;
        public const short MC_GPI = 4;

        public const short MC_ENABLE = 10;
        public const short MC_CLEAR = 11;
        public const short MC_GPO = 12;

        public const short MC_DAC = 20;
        public const short MC_STEP = 21;
        public const short MC_PULSE = 22;
        public const short MC_ENCODER = 23;

        public const short MC_AXIS = 30;
        public const short MC_PROFILE = 31;
        public const short MC_CONTROL = 32;

        public const short CAPTURE_HOME = 1;
        public const short CAPTURE_INDEX = 2;

        public const short PT_MODE_STATIC = 0;
        public const short PT_MODE_DYNAMIC = 1;


        public const short PT_SEGMENT_NORMAL = 0;
        public const short PT_SEGMENT_EVEN = 1;
        public const short PT_SEGMENT_STOP = 2;

        public const short GEAR_MASTER_ENCODER = 1;
        public const short GEAR_MASTER_PROFILE = 2;
        public const short GEAR_MASTER_AXIS = 3;

        public const short FOLLOW_MASTER_ENCODER = 1;
        public const short FOLLOW_MASTER_PROFILE = 2;
        public const short FOLLOW_MASTER_AXIS = 3;

        public const short FOLLOW_EVENT_START = 1;
        public const short FOLLOW_EVENT_PASS = 2;

        public const short FOLLOW_SEGMENT_NORMAL = 0;
        public const short FOLLOW_SEGMENT_EVEN = 1;
        public const short FOLLOW_SEGMENT_STOP = 2;

        public struct TTrapPrm
        {
            public double acc;
            public double dec;
            public double velStart;
            public short smoothTime;
        }

        public struct TJogPrm
        {
            public double acc;
            public double dec;
            public double smooth;
        }

        public struct TPid
        {
            public double kp;
            public double ki;
            public double kd;
            public double kvff;
            public double kaff;
            public short offset;

            public int errorIntegralLimit;
            public int integralLimit;
            public int derivativeLimit;
            public short limit;
        }

        public struct TThreadStatus
        {
            public short link;
            public uint address;
            public short size;
            public uint page;
            public short delay;
            public short priority;
            public short ptr;
            public short status;
            public short error;
            public short[] result;
            public short breakpoint;
            public short period;
            public short count;
            public short function;
        }

        [DllImport("gt800.dll")]
        public static extern short GT_Open();
        [DllImport("gt800.dll")]
        public static extern short GT_Close();

        [DllImport("gt800.dll")]
        public static extern short GT_LoadConfig(System.Text.StringBuilder pFile);
        [DllImport("gt800.dll")]
        public static extern short GT_SaveConfig(System.Text.StringBuilder pFile);

        [DllImport("gt800.dll")]
        public static extern short GT_Retain();

        [DllImport("gt800.dll")]
        public static extern short GT_SetDo(short doType, int value);
        [DllImport("gt800.dll")]
        public static extern short GT_SetDoBit(short doType, short doIndex, short value);
        [DllImport("gt800.dll")]
        public static extern short GT_GetDo(short doType, out int pValue);
        [DllImport("gt800.dll")]
        public static extern short GT_GetDi(short diType, out int pValue);

        [DllImport("gt800.dll")]
        public static extern short GT_SetDac(short dac, short value);
        [DllImport("gt800.dll")]

        public static extern short GT_SetEncPos(short encoder, int encPos);
        [DllImport("gt800.dll")]
        public static extern short GT_GetEncPos(short encoder, out double pValue, short count, ref uint pClock);
        [DllImport("gt800.dll")]
        public static extern short GT_GetEncVel(short encoder, out double pValue, short count, out uint pClock);

        [DllImport("gt800.dll")]
        public static extern short GT_SetCaptureMode(short encoder, short mode);
        [DllImport("gt800.dll")]
        public static extern short GT_GetCaptureMode(short encoder, out short pMode, short count, out uint pClock);
        [DllImport("gt800.dll")]
        public static extern short GT_GetCapture(out int pStatus, out int pPos, out uint pClock);

        [DllImport("gt800.dll")]
        public static extern short GT_Reset(short mode);

        [DllImport("gt800.dll")]
        public static extern short GT_GetClock(out uint pClock, out uint pLoop);

        [DllImport("gt800.dll")]
        public static extern short GT_GetSts(short axis, out int pSts, short count, out uint pClock);
        [DllImport("gt800.dll")]
        public static extern short GT_ClrSts(short axis);
        [DllImport("gt800.dll")]
        public static extern short GT_AxisOn(short axis);
        [DllImport("gt800.dll")]
        public static extern short GT_AxisOff(short axis);
        [DllImport("gt800.dll")]
        public static extern short GT_Stop(int mask, int option);
        [DllImport("gt800.dll")]
        public static extern short GT_SetPrfPos(short profile, int prfPos);
        [DllImport("gt800.dll")]
        public static extern short GT_SynchAxisPos(int mask);

        [DllImport("gt800.dll")]
        public static extern short GT_SetSoftLimit(short axis, int positive, int negative);
        [DllImport("gt800.dll")]
        public static extern short GT_GetSoftLimit(short axis, out int pPositive, out int pNegative);
        [DllImport("gt800.dll")]
        public static extern short GT_SetAxisBand(short axis, int band, int time);
        [DllImport("gt800.dll")]
        public static extern short GT_GetAxisBand(short axis, out int pBand, out int pTime);

        [DllImport("gt800.dll")]
        public static extern short GT_GetPrfPos(short profile, out double pValue, short count, out uint pClock);
        [DllImport("gt800.dll")]
        public static extern short GT_GetPrfVel(short profile, out double pValue, short count, out uint pClock);
        [DllImport("gt800.dll")]
        public static extern short GT_GetPrfAcc(short profile, out double pValue, short count, out uint pClock);
        [DllImport("gt800.dll")]
        public static extern short GT_GetPrfMode(short profile, out int pValue, short count, out uint pClock);

        [DllImport("gt800.dll")]
        public static extern short GT_GetAxisPrfPos(short axis, out double pValue, short count, out uint pClock);
        [DllImport("gt800.dll")]
        public static extern short GT_GetAxisPrfVel(short axis, out double pValue, short count, out uint pClock);
        [DllImport("gt800.dll")]
        public static extern short GT_GetAxisPrfAcc(short axis, out double pValue, short count, out uint pClock);
        [DllImport("gt800.dll")]
        public static extern short GT_GetAxisEncPos(short axis, out double pValue, short count, out uint pClock);
        [DllImport("gt800.dll")]
        public static extern short GT_GetAxisEncVel(short axis, out double pValue, short count, out uint pClock);
        [DllImport("gt800.dll")]
        public static extern short GT_GetAxisEncAcc(short axis, out double pValue, short count, out uint pClock);
        [DllImport("gt800.dll")]
        public static extern short GT_GetAxisError(short axis, out double pValue, short count, out uint pClock);

        [DllImport("gt800.dll")]
        public static extern short GT_SetControlFilter(short control, short index);
        [DllImport("gt800.dll")]
        public static extern short GT_GetControlFilter(short control, out short pIndex);

        [DllImport("gt800.dll")]
        public static extern short GT_SetPid(short control, short index, ref TPid pPid);
        [DllImport("gt800.dll")]
        public static extern short GT_GetPid(short control, short index, out TPid pPid);

        [DllImport("gt800.dll")]
        public static extern short GT_Update(int mask);
        [DllImport("gt800.dll")]
        public static extern short GT_SetPos(short profile, int pos);
        [DllImport("gt800.dll")]
        public static extern short GT_GetPos(short profile, out int pPos);
        [DllImport("gt800.dll")]
        public static extern short GT_SetVel(short profile, double vel);
        [DllImport("gt800.dll")]
        public static extern short GT_GetVel(short profile, out double pVel);

        [DllImport("gt800.dll")]
        public static extern short GT_PrfTrap(short profile);
        [DllImport("gt800.dll")]
        public static extern short GT_SetTrapPrm(short profile, ref TTrapPrm pPrm);
        [DllImport("gt800.dll")]
        public static extern short GT_GetTrapPrm(short profile, out TTrapPrm pPrm);

        [DllImport("gt800.dll")]
        public static extern short GT_PrfJog(short profile);
        [DllImport("gt800.dll")]
        public static extern short GT_SetJogPrm(short profile, ref TJogPrm pPrm);
        [DllImport("gt800.dll")]
        public static extern short GT_GetJogPrm(short profile, out TJogPrm pPrm);

        [DllImport("gt800.dll")]
        public static extern short GT_PrfPt(short profile, short mode);
        [DllImport("gt800.dll")]
        public static extern short GT_SetPtLoop(short profile, int loop);
        [DllImport("gt800.dll")]
        public static extern short GT_GetPtLoop(short profile, out int pLoop);
        [DllImport("gt800.dll")]
        public static extern short GT_PtSpace(short profile, out short pSpace, short fifo);
        [DllImport("gt800.dll")]
        public static extern short GT_PtData(short profile, double pos, int time, short type, short fifo);
        [DllImport("gt800.dll")]
        public static extern short GT_PtClear(short profile, short fifo);
        [DllImport("gt800.dll")]
        public static extern short GT_PtStart(int mask, int option);

        [DllImport("gt800.dll")]
        public static extern short GT_PrfGear(short profile, short dir);
        [DllImport("gt800.dll")]
        public static extern short GT_SetGearMaster(short profile, short masterType, short masterIndex, short masterItem);
        [DllImport("gt800.dll")]
        public static extern short GT_GetGearMaster(short profile, out short pMasterType, out short pMasterIndex, out short pMasterItem);
        [DllImport("gt800.dll")]
        public static extern short GT_SetGearRatio(short profile, int masterEven, int slaveEven, int masterSlope);
        [DllImport("gt800.dll")]
        public static extern short GT_GetGearRatio(short profile, out int pMaster, out int pSlave, out int pSlope);
        [DllImport("gt800.dll")]
        public static extern short GT_GearStart(int mask);

        [DllImport("gt800.dll")]
        public static extern short GT_PrfFollow(short profile, short dir);
        [DllImport("gt800.dll")]
        public static extern short GT_SetFollowMaster(short profile, short masterType, short masterIndex, short masterItem);
        [DllImport("gt800.dll")]
        public static extern short GT_GetFollowMaster(short profile, out short pMasterType, out short pMasterIndex, out short pMasterItem);
        [DllImport("gt800.dll")]
        public static extern short GT_SetFollowLoop(short profile, short loop);
        [DllImport("gt800.dll")]
        public static extern short GT_GetFollowLoop(short profile, out short pLoop);
        [DllImport("gt800.dll")]
        public static extern short GT_SetFollowEvent(short profile, short myevent, short masterDir, int pos);
        [DllImport("gt800.dll")]
        public static extern short GT_GetFollowEvent(short profile, out short pEvent, out short pMasterDir, out int pPos);
        [DllImport("gt800.dll")]
        public static extern short GT_FollowSpace(short profile, out short pSpace);
        [DllImport("gt800.dll")]
        public static extern short GT_FollowData(short profile, int masterSegment, double slaveSegment, short type);
        [DllImport("gt800.dll")]
        public static extern short GT_FollowClear(short profile);
        [DllImport("gt800.dll")]
        public static extern short GT_FollowStart(int mask);

        [DllImport("gt800.dll")]
        public static extern short GT_Bind(short thread, short function, short page);
        [DllImport("gt800.dll")]
        public static extern short GT_StopThread(short thread);
        [DllImport("gt800.dll")]
        public static extern short GT_RunThread(short thread);
        [DllImport("gt800.dll")]
        public static extern short GT_PauseThread(short thread);
        [DllImport("gt800.dll")]
        public static extern short GT_GetThread(short thread, out TThreadStatus pThread);
    }
}
