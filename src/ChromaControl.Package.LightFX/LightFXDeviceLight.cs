// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Drawing;
using ChromaControl.Abstractions;
using LightFX;

namespace ChromaControl.Providers.LightFX
{
    internal class LightFXDeviceLight : IDeviceLight
    {
        public Color Color { get => GetColor(); set => SetColor(value); }

        internal LFX_ColorStruct _deviceLight;

        internal LightFXDeviceLight(LFX_ColorStruct deviceLight)
        {
            _deviceLight = deviceLight;
        }
        private Color GetColor()
        {
            return Color.FromArgb(_deviceLight.red, _deviceLight.green, _deviceLight.blue);
        }

        private void SetColor(Color value)
        {
            _deviceLight.red = value.R;
            _deviceLight.green = value.G;
            _deviceLight.blue = value.B;
        }
    }
}
