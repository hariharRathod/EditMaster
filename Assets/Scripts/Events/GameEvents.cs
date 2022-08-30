using System;
using GestureRecognizer;
using UnityEngine;

public static partial class GameEvents
{
    public static event Action OnTapToPlay;

    public static event Action CameraZoomActionCompleted;

    public static event Action<Transform> ImageSelected,EraserUsed;

    public static event Action CutToolSelected,SelectToolSelected,EraserToolSelected,BackgroundChangeToolSelected;

    public static event Action<Transform, GesturePattern> MyDrawableAreaIsOn;

    public static event Action CutNotAccurate, CutDoneAccurately;

    public static event Action ImageNotSelectedMessage;

    public static event Action<int> ActivateNextTool;

    public static event Action<GameObject> PicturePrefabInstantiateDone;




}

public static partial class GameEvents
{
    public static void InvokeOnTapToPlay() => OnTapToPlay?.Invoke();

    public static void InvokeOnCameraZoomActionCompleted() => CameraZoomActionCompleted?.Invoke();

    public static void InvokeOnImageSelected(Transform obj) => ImageSelected?.Invoke(obj);

    public static void InvokeOnEraserUsed(Transform obj) => EraserUsed?.Invoke(obj);

    public static void InvokeOnCutToolSelected() => CutToolSelected?.Invoke();

    public static void InvokeOnSelectToolSelected() => SelectToolSelected?.Invoke();

    public static void InvokeOnEraserToolSelected() => EraserToolSelected?.Invoke();

    public static void InvokeOnMyDrawableAreaIsOn(Transform arg1, GesturePattern arg2) => MyDrawableAreaIsOn?.Invoke(arg1, arg2);

    public static void InvokeOnCutNotAccurate() => CutNotAccurate?.Invoke();

    public static void InvokeOnCutDoneAccurately() => CutDoneAccurately?.Invoke();

    public static void InvokeOnImageNotSelectedMessage() => ImageNotSelectedMessage?.Invoke();

    public static void InvokeOnBackgroundChangeToolSelected() => BackgroundChangeToolSelected?.Invoke();

    public static void InvokeOnActivateNextTool(int obj) => ActivateNextTool?.Invoke(obj);

    public static void InvokeOnPicturePrefabInstantiateDone(GameObject obj) => PicturePrefabInstantiateDone?.Invoke(obj);
}


