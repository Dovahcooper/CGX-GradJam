using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Runtime.InteropServices;

namespace XBOX
{
    public struct Stick
    {
        public float xAxis, yAxis;
    }

    public struct Triggers
    {
        public float lTrig, rTrig;
    }

    public enum Buttons
    {
        A = 0x5800,
        B = 0x5801,
        X = 0x5802,
        Y = 0x5803,
        RB = 0x5804,
        LB = 0x5805,
        LTrig = 0x5806,
        RTrig = 0x5807,

        DPadUp = 0x5810,
        DPadDown = 0x5811,
        DPadLeft = 0x5812,
        DPadRight = 0x5813,
        Start = 0x5814,
        Select = 0x5815,
        L3 = 0x5816,
        R3 = 0x5817,

        LS_Up = 0x5820,
        LS_Down = 0x5821,
        LS_Right = 0x5822,
        LS_Left = 0x5823,
        LS_Up_Left = 0x5824,
        LS_Up_Right = 0x5825,
        LS_Down_Right = 0x5826,
        LS_Down_Left = 0x5827,
    }

    public class XInput : MonoBehaviour
    {
        #region IMPORT_METHODS
        const string dllName = "XInput1_4Wrapper";

        [DllImport(dllName)]
        public static extern void initDLL();

        [DllImport(dllName)]
        public static extern bool GetConnected(int _index = 0);

        [DllImport(dllName)]
        public static extern bool DownloadPackets(int num_controllers = 1);

        [DllImport(dllName)]
        public static extern void UpdateController(int _index = 0);

        [DllImport(dllName)]
        public static extern bool GetKeyPressed(int _index, int _button);

        [DllImport(dllName)]
        public static extern bool GetKeyReleased(int _index, int _button);

        //bool LIB_API GetKeyHeld(int _index, int _button);

        [DllImport(dllName)]
        public static extern Stick GetLeftStick(int _index = 0);

        [DllImport(dllName)]
        public static extern Stick GetRightStick(int _index = 0);

        [DllImport(dllName)]
        public static extern Triggers GetTriggers(int _index = 0);

        [DllImport(dllName)]
        public static extern bool SetVibration(int _index = 0, float l_motor = 0.0f, float r_motor = 0.0f);

        [DllImport(dllName)]
        public static extern void cleanDLL();
        #endregion

        public Buttons[] allButtons =
        {
            Buttons.A,
            Buttons.B,
            Buttons.X,
            Buttons.Y,
            Buttons.RB,
            Buttons.LB,
            Buttons.LTrig,
            Buttons.RTrig,

            Buttons.DPadUp,
            Buttons.DPadDown,
            Buttons.DPadLeft,
            Buttons.DPadRight,
            Buttons.Start,
            Buttons.Select,
            Buttons.L3,
            Buttons.R3,

            Buttons.LS_Up,
            Buttons.LS_Down,
            Buttons.LS_Right,
            Buttons.LS_Left,
            Buttons.LS_Up_Left,
            Buttons.LS_Up_Right,
            Buttons.LS_Down_Right,
            Buttons.LS_Down_Left,
        };

        [SerializeField]
        public UnityEvent aPress;

        [SerializeField]
        public UnityEvent xPress;

        // Start is called before the first frame update
        void Start()
        {
            initDLL();
            DownloadPackets(1);
        }

        // Update is called once per frame
        void Update()
        {
            DownloadPackets(1);
            UpdateController(0);

            //for(int i = 0; i < allButtons.Length; i++)
            //{
            //    if(XInput.GetKeyPressed(0, (int)allButtons[i]))
            //    {
            //        Debug.Log(allButtons[i] + " was pressed");
            //    }
            //}
            if (GetConnected(0))
            {
                if(GetKeyPressed(0, (int)Buttons.A))
                {
                    aPress.Invoke();
                }
                if(GetKeyPressed(0, (int)Buttons.X))
                {
                    xPress.Invoke();
                }
            }
        }

        private void OnDestroy()
        {
            cleanDLL();
        }
    }
}