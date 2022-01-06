using System.Collections.Generic;
using ChromaControl.Abstractions;
using LightFX;


namespace ChromaControl.Providers.LightFX
{
    public class LightFXDevice : IDevice
    {
        public string Name => _device;

        public IEnumerable<IDeviceLight> Lights => _lights;

        public int NumberOfLights;

        private readonly uint _deviceIndex;

        private readonly LightFXController _device;

        private readonly List<LightFXDeviceLight> _lights;

        internal LightFXDevice(LightFXController device, uint deviceIndex)
        {
            _deviceIndex = deviceIndex;
            _device = device;
            _lights = new List<LightFXDeviceLight>();

            uint numLights;
            _device.LFX_GetNumLights(_deviceIndex, out numLights);

            for (uint lightIndex = 0; lightIndex < numLights; lightIndex++)
            {
                LFX_ColorStruct light;
                _device.LFX_GetLightColor(_deviceIndex, lightIndex, out light);
                _lights.Add(new LightFXDeviceLight(light));
            }

        }

        public void ApplyLights()
        {
            if (NumberOfLights > 0)
            {

            }
        }

    }
}
