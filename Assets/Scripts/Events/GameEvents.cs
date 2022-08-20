using System;
using UnityEngine;

public static partial class GameEvents
{
    public static event Action OnTapToPlay;

    public static event Action CameraZoomActionCompleted;

    public static event Action<Transform> ImageSelected;
}

public static partial class GameEvents
{
    public static void InvokeOnTapToPlay() => OnTapToPlay?.Invoke();

    public static void InvokeOnCameraZoomActionCompleted() => CameraZoomActionCompleted?.Invoke();

    public static void InvokeOnImageSelected(Transform obj) => ImageSelected?.Invoke(obj);
}


