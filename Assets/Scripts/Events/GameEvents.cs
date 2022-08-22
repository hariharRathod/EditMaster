using System;
using UnityEngine;

public static partial class GameEvents
{
    public static event Action OnTapToPlay;

    public static event Action CameraZoomActionCompleted;

    public static event Action<Transform> ImageSelected,EraserUsed,MyDrawableAreaIsOn;

    public static event Action CutToolSelected,SelectToolSelected,EraserToolSelected;


}

public static partial class GameEvents
{
    public static void InvokeOnTapToPlay() => OnTapToPlay?.Invoke();

    public static void InvokeOnCameraZoomActionCompleted() => CameraZoomActionCompleted?.Invoke();

    public static void InvokeOnImageSelected(Transform obj) => ImageSelected?.Invoke(obj);

    public static void InvokeOnEraserUsed(Transform obj) => EraserUsed?.Invoke(obj);

    public static void InvokeOnCutToolSelected() => CutToolSelected?.Invoke();

    public static void InvokeOnMyDrawableAreaIsOn(Transform obj) => MyDrawableAreaIsOn?.Invoke(obj);

    public static void InvokeOnSelectToolSelected() => SelectToolSelected?.Invoke();

    public static void InvokeOnEraserToolSelected() => EraserToolSelected?.Invoke();
}


