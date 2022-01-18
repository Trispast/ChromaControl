// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Drawing;
using ChromaControl.Abstractions;
using LightFXsdk;

namespace ChromaControl.Providers.LightFX
{
    public class LightFXDeviceLight : IDeviceLight
    {
        public Color Color { get => GetColor(); set => SetColor(value); }

        public int index;
        internal LFX_ColorStruct _deviceLight;

        internal LightFXDeviceLight(int _index)
        {
            index = _index;
        }
        private Color GetColor()
        {
            return Color.FromArgb(_deviceLight.brightness, _deviceLight.red, _deviceLight.green, _deviceLight.blue);
        }

        private void SetColor(Color value)
        {
            _deviceLight.brightness = value.A;
            _deviceLight.red = value.R;
            _deviceLight.green = value.G;
            _deviceLight.blue = value.B;
        }

        private void SetColor(LFX_ColorStruct value)
        {
            _deviceLight = value;
        }
    }
}
