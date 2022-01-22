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

        public int NumberOfLights => _lights.Count;

        private readonly int _deviceIndex;

        private readonly string _description;

        private readonly LightFXController _sdk;

        private readonly List<LightFXDeviceLight> _lights;

        internal LightFXDevice(LightFXController device, int deviceIndex)
        {
            _deviceIndex = deviceIndex;
            _sdk = device;
            _lights = new List<LightFXDeviceLight>();

            LFX_Result result = _sdk.LFX_Initialize();

            if (result == LFX_Result.LFX_SUCCESS)
            {
                if (_deviceIndex == 0)
                    _description = "Alienware 34 Curved Monitor";
                else
                    _description = "Alienware AuroraR5 Chassis";

                //_description = _sdk.LFX_GetDeviceDescription(deviceIndex);

                int numLights = _sdk.LFX_GetNumLights(deviceIndex);

                for (int lightIndex = 0; lightIndex < numLights; lightIndex++)
                {
                    _lights.Add(new LightFXDeviceLight(lightIndex));
                }
            }

        }

        public void ApplyLights()
        {
            if (NumberOfLights > 0)
            {

                foreach (LightFXDeviceLight light in Lights)
                {
                //   LFX_Result result = _sdk.LFX_SetLightColor(_deviceIndex, light.index, light.Color);
                    LFX_Position pos = _sdk.LFX_GetLightLocation(_deviceIndex, light.index);
                    LFX_Result result = _sdk.LFX_Light(LFX_Position.LFX_All, light.Color);
                }
                _sdk.LFX_Update();
            }
        }

    }
}
