using System.Collections.Generic;
using System.Text;
using ChromaControl.Abstractions;
using LightFXsdk;


namespace ChromaControl.Providers.LightFX
{
    public class LightFXDevice : IDevice
    {
        public string Name => _description;

        public IEnumerable<IDeviceLight> Lights => _lights;

        public int NumberOfLights => 0;

        private readonly int _deviceIndex;

        private readonly string _description;

        private readonly LightFXController _device;

        private readonly List<LightFXDeviceLight> _lights;

        internal LightFXDevice(LightFXController device, int deviceIndex)
        {
            _deviceIndex = deviceIndex;
            _device = device;
            _lights = new List<LightFXDeviceLight>();

            LFX_Result result = _device.LFX_Initialize();

            if (result == LFX_Result.LFX_SUCCESS)
            {
                if (_deviceIndex == 0)
                    _description = "Alienware 34 Curved Monitor";
                else
                    _description = "Alienware AuroraR5 Chassis";
                //_description = _device.LFX_GetDeviceDescription(_deviceIndex);

                int numLights = _device.LFX_GetNumLights(_deviceIndex);

                for (int lightIndex = 0; lightIndex < numLights; lightIndex++)
                {
                    LFX_ColorStruct light = _device.LFX_GetLightColor(_deviceIndex, lightIndex);
                    _lights.Add(new LightFXDeviceLight(light));
                }
            }

        }

        public void ApplyLights()
        {
            if (NumberOfLights > 0)
            {
                _device.LFX_Update();
            }
        }

    }
}
