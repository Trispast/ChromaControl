using ChromaControl.Providers.LightFX;
using LightFXsdk;
using System.Drawing;
using System.Text;

// See https://aka.ms/new-console-template for more information



var lightFX = new LightFXController();

var result = lightFX.LFX_Initialize();
if (result == LFX_Result.LFX_SUCCESS)
{
    lightFX.LFX_Reset();

    int numDevs = lightFX.LFX_GetNumDevices();

    for (int devIndex = 0; devIndex < numDevs; devIndex++)
    {
        int numLights = lightFX.LFX_GetNumLights(devIndex);

        var green = new LFX_ColorStruct(255, 0, 255, 255);
        var red = new LFX_ColorStruct(255, 0, 255, 255);
        for (int lightIndex = 0; lightIndex < numLights; lightIndex++)
            lightFX.LFX_SetLightColor(devIndex, lightIndex, lightIndex % 2 == 0 ? red : green);
    }

    for (int devIndex = 0; devIndex < numDevs; devIndex++)
    {
        string temp;
        temp = lightFX.LFX_GetDeviceDescription(devIndex);
        if (result != LFX_Result.LFX_SUCCESS)
            continue;

        Console.WriteLine(string.Format(temp));

        int numLights = lightFX.LFX_GetNumLights(devIndex);
        for (int lightIndex = 0; lightIndex < numLights; lightIndex++)
        {
            temp = lightFX.LFX_GetLightDescription(devIndex, lightIndex);
            if (result != LFX_Result.LFX_SUCCESS)
                continue;

            LFX_ColorStruct color;
            color = lightFX.LFX_GetLightColor(devIndex, lightIndex);
            if (result != LFX_Result.LFX_SUCCESS)
                continue;

            Console.WriteLine(string.Format("\tLight: {0} \tDescription: {1} \tColor: {2}", lightIndex, temp, color));
        }
    }

    lightFX.LFX_Update();
    Console.WriteLine("Done.\r\rPress ENTER key to finish ...");
    Console.ReadLine();
    lightFX.LFX_Release();

    var lightFx2 = new LightFXDeviceProvider();

    lightFx2.Initialize();

    var numDevs2 =lightFx2.Devices.Count();

    foreach (var device in lightFx2.Devices)
    {
        int numLights2 = device.NumberOfLights;

        var green = new LFX_ColorStruct(255, 0, 255, 255);
        var red = new LFX_ColorStruct(255, 255, 0, 255);
        foreach (var light in device.Lights)
        {
            light.Color = Color.Red;
        }
        device.ApplyLights();
    }

    Console.WriteLine("Done.\r\rPress ENTER key to finish ...");
    Console.ReadLine();
    lightFx2.ReleaseControl();
}
else
{
    switch (result)
    {
        case LFX_Result.LFX_ERROR_NODEVS:
            Console.WriteLine("There is not AlienFX device available.");
            break;
        default:
            Console.WriteLine("There was an error initializing the AlienFX device.");
            break;
    }
}
