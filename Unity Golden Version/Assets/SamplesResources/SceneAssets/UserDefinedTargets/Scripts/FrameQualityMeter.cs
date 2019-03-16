/*===============================================================================
Copyright (c) 2017 PTC Inc. All Rights Reserved.

Vuforia is a trademark of PTC Inc., registered in the United States and other 
countries.
===============================================================================*/
using UnityEngine;
using UnityEngine.UI;

public class FrameQualityMeter : MonoBehaviour
{
    public Image[] LowMedHigh;
    internal Color32[] colorScheme = new Color32[4];

    void SetMeter(Color low, Color med, Color high)
    {
        if (LowMedHigh.Length == 3)
        {
            if (LowMedHigh[0])
                LowMedHigh[0].color = low;
            if (LowMedHigh[1])
                LowMedHigh[1].color = med;
            if (LowMedHigh[2])
                LowMedHigh[2].color = high;
        }

        colorScheme[0] = new Color32(26, 26, 29, 255);
        colorScheme[1] = new Color32(111, 34, 50, 255);
        colorScheme[2] = new Color32(149, 7, 64, 255);
        colorScheme[3] = new Color32(195, 7, 63, 255);
    }

    public void SetQuality(Vuforia.ImageTargetBuilder.FrameQuality quality)
    {
        switch (quality)
        {
            case (Vuforia.ImageTargetBuilder.FrameQuality.FRAME_QUALITY_NONE):
                SetMeter(colorScheme[0], colorScheme[0], colorScheme[0]);
                break;
            case (Vuforia.ImageTargetBuilder.FrameQuality.FRAME_QUALITY_LOW):
                SetMeter(colorScheme[1], colorScheme[0], colorScheme[0]);
                break;
            case (Vuforia.ImageTargetBuilder.FrameQuality.FRAME_QUALITY_MEDIUM):
                SetMeter(colorScheme[1], colorScheme[2], colorScheme[0]);
                break;
            case (Vuforia.ImageTargetBuilder.FrameQuality.FRAME_QUALITY_HIGH):
                SetMeter(colorScheme[1], colorScheme[2], colorScheme[3]);
                break;
        }
    }
}
