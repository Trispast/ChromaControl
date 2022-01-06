// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using ChromaControl.Abstractions;
using LightFX;
using Microsoft.Extensions.Logging;

namespace ChromaControl.Providers.LightFX
{
    internal class LightFXDeviceProvider : IDeviceProvider
    {
        public string Name => "LightFX";

        public IEnumerable<IDevice> Devices => _devices;

        private readonly List<LightFXDevice> _devices;

        public void Initialize()
        {
            PerformHealthCheck();

        }

        public void PerformHealthCheck()
        {


        }

        public void RequestControl()
        {

        }

        public void ReleaseControl()
        {

        }
    }
}
