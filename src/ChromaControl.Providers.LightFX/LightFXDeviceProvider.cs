// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using LightFXsdk;
using System;
using System.Diagnostics;
using ChromaControl.Abstractions;


namespace ChromaControl.Providers.LightFX
{
    public class LightFXDeviceProvider : IDeviceProvider
    {
        public string Name => "Corsair";

        public IEnumerable<IDevice> Devices => _devices;

        private readonly List<LightFXDevice> _devices;

        private readonly LightFXController _sdk;

        private LFX_Result _result;

        public LightFXDeviceProvider()
        {
            _devices = new List<LightFXDevice>();
            _sdk = new LightFXController();
            _result = _sdk.LFX_Initialize();
        }

        public void Initialize()
        {
            PerformHealthCheck();

            //Thread.Sleep(30000);
            //RequestControl();

            if (_result == LFX_Result.LFX_SUCCESS)
            {
                int devicecount;
                devicecount = _sdk.LFX_GetNumDevices();

                for (int i = 0; i < devicecount; i++)
                {

                    _devices.Add(new LightFXDevice(_sdk, (int)i));
                }

            }

        }

        public void PerformHealthCheck()
        {
            var LightFXServiceRunning = Process.GetProcessesByName("AlienFXWindowsService").Length != 0;

            while (!LightFXServiceRunning)
            {
                Thread.Sleep(1000);
                LightFXServiceRunning = Process.GetProcessesByName("AlienFXWindowsService").Length != 0;
            }

        }

        public void RequestControl()
        {
            _sdk.LFX_Initialize();
        }

        public void ReleaseControl()
        {
            _result = _sdk.LFX_Release();
        }
    }
}
