using System.Collections.Generic;
using System.Text;
using ChromaControl.Abstractions;
using LightFX;


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

        internal LightFXDevice(int deviceIndex)
        {
            _deviceIndex = deviceIndex;
            _device = new LightFXController();
            _lights = new List<LightFXDeviceLight>();

            LFX_Result result = _device.LFX_Initialize();

            if (result == LFX_Result.LFX_Success)
            {
                StringBuilder description;
                LFX_DeviceType type;

                _device.LFX_GetDeviceDescription((uint)_deviceIndex, out description, 255, out type);
                _description = description.ToString();

                uint numLights;
                _device.LFX_GetNumLights((uint)_deviceIndex, out numLights);

                for (uint lightIndex = 0; lightIndex < numLights; lightIndex++)
                {
                    LFX_ColorStruct light;
                    _device.LFX_GetLightColor((uint)_deviceIndex, lightIndex, out light);
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
