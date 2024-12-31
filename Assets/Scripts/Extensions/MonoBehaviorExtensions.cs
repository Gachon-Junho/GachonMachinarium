using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class MonoBehaviorExtensions
{
    public static void LoadSceneAsync(this MonoBehaviour mono, string sceneName)
    {
        mono.StartCoroutine(loadScene());

        IEnumerator loadScene()
        {
            var loadingTask = SceneManager.LoadSceneAsync(sceneName);

            while (!loadingTask.isDone)
                yield return null;
        }
    }

    public static Coroutine StartDelayedCoroutine(this MonoBehaviour mono, IEnumerator routine, float timeUntilStart)
    {
        var cor = mono.StartCoroutine(startDelayed());
        
        IEnumerator startDelayed()
        {
            yield return new WaitForSeconds(timeUntilStart);

            cor = mono.StartCoroutine(routine);
        }

        return cor;
    }

    public static Coroutine StartDelayedSchedule(this MonoBehaviour mono, Action action, float timeUntilStart)
    {
        var cor = mono.StartCoroutine(startDelayed());
        
        IEnumerator startDelayed()
        {
            yield return new WaitForSeconds(timeUntilStart);

            action?.Invoke();
        }

        return cor;
    }

    public static bool IsVisibleInCamera(this MonoBehaviour mono, Camera camera, float boundOffset = 0)
    {
        var cameraPos = camera.WorldToViewportPoint(mono.transform.position);

        if (cameraPos.y > 1 + boundOffset || cameraPos.y < 0 - boundOffset || cameraPos.x > 1 + boundOffset || cameraPos.x < 0 - boundOffset)
            return false;

        return true;
    }

    public static IEnumerator CheckVisibility(this MonoBehaviour mono, Camera camera, float boundOffset = 0, Action onBecameVisible = null, Action onBecameInvisible = null)
    {
        var isVisible = mono.IsVisibleInCamera(camera, boundOffset);
        var lastVisibility = isVisible;

        while (true)
        {
            isVisible = mono.IsVisibleInCamera(camera, boundOffset);

            if (lastVisibility != isVisible)
            {
                if (isVisible) onBecameVisible?.Invoke();
                else onBecameInvisible?.Invoke();
            }

            lastVisibility = isVisible;
            
            yield return null;
        }
    }

    public static Vector3 DynamicScreenToWorldPoint(this Camera camera, Vector3 position)
    {
        return camera.ScreenToWorldPoint(position + new Vector3(0, 0, camera.transform.position.z) - camera.transform.position * 2);
    }

    public static void ScaleTo(this MonoBehaviour mono, Vector3 scale, double duration = 0, Easing easing = Easing.None)
    {
        mono.StartCoroutine(transformLoop(scale, Time.time, Time.time + duration));

        IEnumerator transformLoop(Vector3 to, double startTime, double endTime)
        {
            var start = mono.transform.localScale;
                
            while (Time.time < endTime)
            {
                mono.transform.localScale = Interpolation.ValueAt(Time.time, start, to, startTime, endTime, new EasingFunction(easing));

                yield return null;
            }
        }
    }
    
    public static void MoveTo(this MonoBehaviour mono, Vector3 position, double duration = 0, Easing easing = Easing.None)
    {
        mono.StartCoroutine(transformLoop(position, Time.time, Time.time + duration));

        IEnumerator transformLoop(Vector3 to, double startTime, double endTime)
        {
            var start = mono.transform.position;
                
            while (Time.time < endTime)
            {
                mono.transform.position = Interpolation.ValueAt(Time.time, start, to, startTime, endTime, new EasingFunction(easing));

                yield return null;
            }
        }
    }
    
    public static void MoveToX(this MonoBehaviour mono, float x, double duration = 0, Easing easing = Easing.None)
    {
        mono.StartCoroutine(transformLoop(x, Time.time, Time.time + duration));

        IEnumerator transformLoop(float to, double startTime, double endTime)
        {
            var start = mono.transform.position.x;
                
            while (Time.time < endTime)
            {
                mono.transform.position = new Vector3(Interpolation.ValueAt(Time.time, start, to, startTime, endTime, new EasingFunction(easing)), 
                    mono.transform.position.y,
                    mono.transform.position.z);
                
                yield return null;
            }
        }
    }
    
    public static void MoveToY(this MonoBehaviour mono, float y, double duration = 0, Easing easing = Easing.None)
    {
        mono.StartCoroutine(transformLoop(y, Time.time, Time.time + duration));

        IEnumerator transformLoop(float to, double startTime, double endTime)
        {
            var start = mono.transform.position.y;
                
            while (Time.time < endTime)
            {
                mono.transform.position = new Vector3(mono.transform.position.x, 
                    Interpolation.ValueAt(Time.time, start, to, startTime, endTime, new EasingFunction(easing)),
                    mono.transform.position.z);
                yield return null;
            }
        }
    }
    
    public static void MoveToZ(this MonoBehaviour mono, float z, double duration = 0, Easing easing = Easing.None)
    {
        mono.StartCoroutine(transformLoop(z, Time.time, Time.time + duration));

        IEnumerator transformLoop(float to, double startTime, double endTime)
        {
            var start = mono.transform.position.z;
                
            while (Time.time < endTime)
            {
                mono.transform.position = new Vector3(mono.transform.position.x, 
                                                        mono.transform.position.y,
                                                        Interpolation.ValueAt(Time.time, start, to, startTime, endTime, new EasingFunction(easing)));

                yield return null;
            }
        }
    }
    
    
    
    public static void RectMoveTo(this MonoBehaviour mono, Vector3 position, double duration = 0, Easing easing = Easing.None)
    {
        var transform = (mono as IHasRectTransform)?.RectTransform;

        if (transform == null)
            return;
        
        mono.StartCoroutine(transformLoop(position, Time.time, Time.time + duration));

        IEnumerator transformLoop(Vector3 to, double startTime, double endTime)
        {
            var start = transform.anchoredPosition;
                
            while (Time.time < endTime)
            {
                transform.anchoredPosition = Interpolation.ValueAt(Time.time, (Vector3)start, to, startTime, endTime, new EasingFunction(easing));
                yield return null;
            }
        }
    }

    public static void RotateTo(this MonoBehaviour mono, Vector3 rotation, double duration = 0, Easing easing = Easing.None)
    {
        mono.StartCoroutine(transformLoop(rotation, Time.time, Time.time + duration));

        IEnumerator transformLoop(Vector3 to, double startTime, double endTime)
        {
            var start = mono.transform.rotation.eulerAngles;
                
            while (Time.time < endTime)
            {
                mono.transform.rotation = Quaternion.Euler(Interpolation.ValueAt(Time.time, start, to, startTime, endTime, new EasingFunction(easing)));
                
                yield return null;
            }
        }
    }
    
    public static void RotateTo(this Transform transform, Vector3 rotation, double duration = 0, Easing easing = Easing.None)
    {
        var mono = ProxyMonoBehavior.Current;
        
        mono.StartCoroutine(transformLoop(rotation, Time.time, Time.time + duration));

        IEnumerator transformLoop(Vector3 to, double startTime, double endTime)
        {
            var start = transform.rotation.eulerAngles;
                
            while (Time.time < endTime)
            {
                transform.rotation = Quaternion.Euler(Interpolation.ValueAt(Time.time, start, to, startTime, endTime, new EasingFunction(easing)));
                
                yield return null;
            }
        }
    }
}