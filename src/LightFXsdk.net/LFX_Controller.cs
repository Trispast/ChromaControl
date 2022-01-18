using System.Drawing;
using System.Text;
using System.Runtime.InteropServices;
using System.Security;
public struct LFX_ColorStruct
{
    public byte blue;
    public byte green;
    public byte red;
    public byte brightness;

    public LFX_ColorStruct(byte brightness, byte red, byte green, byte blue)
    {
        this.brightness = brightness;
        this.red = red;
        this.green = green;
        this.blue = blue;
    }

    public LFX_ColorStruct(Color color)
    {
        this.brightness = color.A;
        this.red = color.R;
        this.green = color.G;
        this.blue = color.B;
    }
};


public enum LFX_Result
{
    LFX_SUCCESS = 0,
    LFX_FAILURE = 1,
    LFX_ERROR_NOINIT = 2,
    LFX_ERROR_NODEVS = 3,
    LFX_ERROR_NOLIGHTS = 4,
    LFX_ERROR_BUFFSIZE = 5
}

public enum LFX_DeviceType
{
    LFX_DevType_Unknown = 0,
    LFX_DevType_Notebook = 1,
    LFX_DevType_Desktop = 2,
    LFX_DevType_Server = 3,
    LFX_DevType_Display = 4,
    LFX_DevType_Mouse = 5,
    LFX_DevType_Keyboard = 6,
    LFX_DevType_Gamepad = 7,
    LFX_DevType_Speaker = 8,
    LFX_DevType_Custom = 254,
    LFX_DevType_Other = 255
}

public enum LFX_Position
{
    LFX_All = LFX_All_Front | LFX_Middle_Lower_Left | LFX_Middle_Lower_Center | LFX_Middle_Lower_Right | LFX_Middle_Middle_Left | LFX_Middle_Middle_Center | LFX_Middle_Middle_Right | LFX_Middle_Upper_Left | LFX_Middle_Upper_Center | LFX_Middle_Upper_Right | LFX_All_Rear,
    LFX_All_Front = LFX_Front_Lower_Center | LFX_Front_Lower_Left | LFX_Front_Lower_Right | LFX_Front_Middle_Center | LFX_Front_Middle_Left | LFX_Front_Middle_Right | LFX_Front_Upper_Center | LFX_Front_Upper_Left | LFX_Front_Upper_Right,
    LFX_All_Left = LFX_Front_Lower_Left | LFX_Front_Middle_Left | LFX_Front_Upper_Left | LFX_Middle_Lower_Left | LFX_Middle_Middle_Left | LFX_Middle_Upper_Left | LFX_Rear_Lower_Left | LFX_Rear_Middle_Left | LFX_Rear_Upper_Left,
    LFX_All_Lower = LFX_Front_Lower_Center | LFX_Front_Lower_Left | LFX_Front_Lower_Right | LFX_Middle_Lower_Center | LFX_Middle_Lower_Left | LFX_Middle_Lower_Right | LFX_Rear_Lower_Center | LFX_Rear_Lower_Left | LFX_Rear_Lower_Right,
    LFX_All_Rear = LFX_Rear_Lower_Center | LFX_Rear_Lower_Left | LFX_Rear_Lower_Right | LFX_Rear_Middle_Center | LFX_Rear_Middle_Left | LFX_Rear_Middle_Right | LFX_Rear_Upper_Center | LFX_Rear_Upper_Left | LFX_Rear_Upper_Right,
    LFX_All_Right = LFX_Front_Lower_Right | LFX_Front_Middle_Right | LFX_Front_Upper_Right | LFX_Middle_Lower_Right | LFX_Middle_Middle_Right | LFX_Middle_Upper_Right | LFX_Rear_Lower_Right | LFX_Rear_Middle_Right | LFX_Rear_Upper_Right,
    LFX_All_Upper = LFX_Front_Upper_Center | LFX_Front_Upper_Left | LFX_Front_Upper_Right | LFX_Middle_Upper_Center | LFX_Middle_Upper_Left | LFX_Middle_Upper_Right | LFX_Rear_Upper_Center | LFX_Rear_Upper_Left | LFX_Rear_Upper_Right,
    LFX_Front_Lower_Center = 2,
    LFX_Front_Lower_Left = 1,
    LFX_Front_Lower_Right = 4,
    LFX_Front_Middle_Center = 16,
    LFX_Front_Middle_Left = 8,
    LFX_Front_Middle_Right = 32,
    LFX_Front_Upper_Center = 128,
    LFX_Front_Upper_Left = 64,
    LFX_Front_Upper_Right = 256,
    LFX_Middle_Lower_Center = 1024,
    LFX_Middle_Lower_Left = 512,
    LFX_Middle_Lower_Right = 2048,
    LFX_Middle_Middle_Center = 8192,
    LFX_Middle_Middle_Left = 4096,
    LFX_Middle_Middle_Right = 16384,
    LFX_Middle_Upper_Center = 65536,
    LFX_Middle_Upper_Left = 32768,
    LFX_Middle_Upper_Right = 131072,
    LFX_Rear_Lower_Center = 524288,
    LFX_Rear_Lower_Left = 262144,
    LFX_Rear_Lower_Right = 1048576,
    LFX_Rear_Middle_Center = 4194304,
    LFX_Rear_Middle_Left = 2097152,
    LFX_Rear_Middle_Right = 8388608,
    LFX_Rear_Upper_Center = 33554432,
    LFX_Rear_Upper_Left = 16777216,
    LFX_Rear_Upper_Right = 67108864,
}

public enum LFX_ActionEnum
{
    Morph = 1,
    Pulse = 2,
    Color = 3
}

namespace LightFXsdk
{
    public class LightFXController
    {
        [SuppressUnmanagedCodeSecurity]
        [DllImport("LightFX.dll", EntryPoint = "LFX_Initialize", CallingConvention = CallingConvention.Cdecl, SetLastError = false)]
        private static extern LFX_Result LFX_Initialize_Native();

        [SuppressUnmanagedCodeSecurity]
        [DllImport("LightFX.dll", EntryPoint = "LFX_Release", CallingConvention = CallingConvention.Cdecl, SetLastError = false)]
        private static extern LFX_Result LFX_Release_Native();

        [SuppressUnmanagedCodeSecurity]
        [DllImport("LightFX.dll", EntryPoint = "LFX_Reset", CallingConvention = CallingConvention.Cdecl, SetLastError = false)]
        private static extern LFX_Result LFX_Reset_Native();

        [SuppressUnmanagedCodeSecurity]
        [DllImport("LightFX.dll", EntryPoint = "LFX_Update", CallingConvention = CallingConvention.Cdecl, SetLastError = false)]
        private static extern LFX_Result LFX_Update_Native();

        [SuppressUnmanagedCodeSecurity]
        [DllImport("LightFX.dll", EntryPoint = "LFX_UpdateDefault", CallingConvention = CallingConvention.Cdecl, SetLastError = false)]
        private static extern LFX_Result LFX_UpdateDefault_Native();

        [SuppressUnmanagedCodeSecurity]
        [DllImport("LightFX.dll", EntryPoint = "LFX_GetNumDevices", CallingConvention = CallingConvention.Cdecl, SetLastError = false)]
        private static extern LFX_Result LFX_GetNumDevices_Native(out uint numDevices);

        [SuppressUnmanagedCodeSecurity]
        [DllImport("LightFX.dll", EntryPoint = "LFX_GetDeviceDescription",  CallingConvention = CallingConvention.Cdecl, SetLastError = false)]
        private static extern LFX_Result LFX_GetDeviceDescription_Native(uint devIndex, out StringBuilder devDesc, uint devDescSize, out LFX_DeviceType devType);

        [SuppressUnmanagedCodeSecurity]
        [DllImport("LightFX.dll", EntryPoint = "LFX_GetNumLights", CallingConvention = CallingConvention.Cdecl, SetLastError = false)]
        private static extern LFX_Result LFX_GetNumLights_Native(uint devIndex, out uint numLights);

        [SuppressUnmanagedCodeSecurity]
        [DllImport("LightFX.dll", EntryPoint = "LFX_GetLightDescription", CallingConvention = CallingConvention.Cdecl, SetLastError = false)]
        private static extern LFX_Result LFX_GetLightDescription_Native(uint devIndex, uint lightIndex, out StringBuilder lightDesc, uint lightDescSize);

        [SuppressUnmanagedCodeSecurity]
        [DllImport("LightFX.dll", EntryPoint = "LFX_GetLightPosition", CallingConvention = CallingConvention.Cdecl, SetLastError = false)]
        private static extern LFX_Result LFX_GetLightPosition_Native(uint devIndex, uint lightIndex, out LFX_Position lightLoc);

        [SuppressUnmanagedCodeSecurity]
        [DllImport("LightFX.dll", EntryPoint = "LFX_GetLightColor", CallingConvention = CallingConvention.Cdecl, SetLastError = false)]
        private static extern LFX_Result LFX_GetLightColor_Native(uint devIndex, uint lightIndex, out LFX_ColorStruct lightCol);

        [SuppressUnmanagedCodeSecurity]
        [DllImport("LightFX.dll", EntryPoint = "LFX_SetLightColor", CallingConvention = CallingConvention.Cdecl, SetLastError = false)]
        private static extern LFX_Result LFX_SetLightColor_Native(uint devIndex, uint lightIndex, ref LFX_ColorStruct lightCol);


        public LFX_Result LFX_Initialize()
        {
            return LFX_Initialize_Native();
        }
        public LFX_Result LFX_Release()
        {
            return LFX_Release_Native();
        }

        public LFX_Result LFX_Reset()
        {
            return LFX_Reset_Native();
        }

        public LFX_Result LFX_Update()
        {
            return LFX_Update_Native();
        }

        public LFX_Result LFX_UpdateDefault()
        {
            return LFX_UpdateDefault_Native();
        }

        public int LFX_GetNumDevices()
        {
            uint numDevices;
            var result = LFX_GetNumDevices_Native(out numDevices);
            if (result == LFX_Result.LFX_SUCCESS)
                return (int)numDevices;
            else
                return 0;
        }

        public string LFX_GetDeviceDescription(int devIndex)
        {
            uint len = 255;
            LFX_Result result = LFX_GetDeviceDescription_Native((uint)devIndex, out StringBuilder devDesc, len, out LFX_DeviceType type);
            if (result == LFX_Result.LFX_SUCCESS)
                return devDesc.ToString();
            else
                return string.Empty;

        }

        public int LFX_GetNumLights(int devIndex)
        {
            uint numLights;
            LFX_Result result = LFX_GetNumLights_Native((uint)devIndex, out numLights);
            if (result == LFX_Result.LFX_SUCCESS)
                return (int)numLights;
            else
                return 0;
        }

        public string LFX_GetLightDescription(int devIndex, int lightIndex)
        {
            uint len = 255;
            LFX_Result result = LFX_GetLightDescription_Native((uint)devIndex, (uint)lightIndex, out StringBuilder lightDesc, len);
            if (result == LFX_Result.LFX_SUCCESS)
                return lightDesc.ToString();
            else
                return string.Empty;
        }

        public LFX_Position LFX_GetLightPosition(int devIndex, int lightIndex)
        { return LFX_Position.LFX_All; }

        public LFX_ColorStruct LFX_GetLightColor(int devIndex, int lightIndex)
        {
            LFX_Result result = LFX_GetLightColor_Native((uint)devIndex, (uint)lightIndex, out LFX_ColorStruct lightCol);
            return lightCol;
        }

        public LFX_Result LFX_SetLightColor(int devIndex, int lightIndex, LFX_ColorStruct lightCol)
        {
            LFX_Result result = LFX_SetLightColor_Native((uint)(devIndex),(uint)lightIndex, ref lightCol);
            return result;
        }

    };

}
