// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using ChromaControl.Abstractions;
using LightFX;

namespace ChromaControl.Providers.LightFX
{
    internal class LightFXDeviceProvider : IDeviceProvider
    {
        public string Name => "LightFX";

        public IEnumerable<IDevice> Devices => _devices;

        private readonly List<LightFXDevice> _devices;

        private LightFXController _sdk => new LightFXController();

        public LightFXDeviceProvider()
        {
            _devices = new List<LightFXDevice>();
        }

        public void Initialize()
        {
           // PerformHealthCheck();

            Thread.Sleep(30000);

            LFX_Result result = _sdk.LFX_Initialize();

            if (result == LFX_Result.LFX_Success)
            {
                uint devicecount;
                _sdk.LFX_GetNumDevices(out devicecount);

                for (int i = 0; i < ((int)devicecount); i++)
                {

                    _devices.Add(new LightFXDevice(i));
                }

            }
            _sdk.LFX_Release();
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

        }

        public void ReleaseControl()
        {

        }
    }
}
