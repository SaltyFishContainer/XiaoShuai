using System;
using System.Collections;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Lingdar77.Functional
{
    public class Flow
    {
        public delegate void delegateNoParam();
        public static IEnumerator SetTimeout(MonoBehaviour context, delegateNoParam func, float seconds)
        {
            IEnumerator setTimeout()
            {
                yield return new WaitForSeconds(seconds);
                func?.Invoke();
            }
            var enumerator = setTimeout();
            context.StartCoroutine(enumerator);
            return enumerator;
        }
        public static IEnumerator SetInterval(MonoBehaviour context, delegateNoParam func, float seconds)
        {
            IEnumerator setInterval()
            {
                while (true)
                {
                    yield return new WaitForSeconds(seconds);
                    func?.Invoke();
                }

            }
            var enumerator = setInterval();
            context.StartCoroutine(enumerator);
            return enumerator;
        }
        public static CancellationTokenSource SetTimeout(Action func, int milliseconds = 300)
        {
            CancellationTokenSource cancelTokenSource = null;

            cancelTokenSource = new CancellationTokenSource();
            Task.Delay(milliseconds, cancelTokenSource.Token)
                   .ContinueWith(t =>
                   {
                       if (t.IsCompletedSuccessfully)
                       {
                           func();
                       }
                   }, TaskScheduler.Default);
            return cancelTokenSource;
        }
        public static Action<T> SetTimeout<T>(Action<T> func, int milliseconds = 300)
        {
            return arg =>
            {
                Task.Delay(milliseconds)
                   .ContinueWith(t =>
                   {
                       if (t.IsCompletedSuccessfully)
                       {
                           func(arg);
                       }
                   }, TaskScheduler.Default);
            };
        }
        public static CancellationTokenSource SetInterval(Action func, int milliseconds = 300)
        {
            CancellationTokenSource cancelTokenSource = null;

            cancelTokenSource = new CancellationTokenSource();
            Action loop = null;
            loop = () =>
            {
                Task.Delay(milliseconds, cancelTokenSource.Token)
                   .ContinueWith(t =>
                   {
                       if (t.IsCompletedSuccessfully)
                       {
                           func();
                           loop();
                       }
                   }, TaskScheduler.Default);
            };
            loop();
            return cancelTokenSource;
        }
        public static Action<T> Debounce<T>(Action<T> func, int milliseconds = 300)
        {
            CancellationTokenSource cancelTokenSource = null;

            return arg =>
            {
                cancelTokenSource?.Cancel();
                cancelTokenSource = new CancellationTokenSource();

                Task.Delay(milliseconds, cancelTokenSource.Token)
                    .ContinueWith(t =>
                    {
                        if (t.IsCompletedSuccessfully)
                        {
                            func(arg);
                        }
                    }, TaskScheduler.Default);
            };
        }
        public static Action Debounce(Action func, int milliseconds = 300)
        {
            CancellationTokenSource cancelTokenSource = null;
            return () =>
            {
                cancelTokenSource?.Cancel();
                cancelTokenSource = new CancellationTokenSource();

                Task.Delay(milliseconds, cancelTokenSource.Token)
                    .ContinueWith(t =>
                    {
                        if (t.IsCompletedSuccessfully)
                        {
                            func();
                        }
                    }, TaskScheduler.Default);
            };
        }
        public static Action<T> Throttle<T>(Action<T> func, int milliseconds = 300)
        {
            return arg =>
            {
                bool timer = false;
                if (!timer)
                {
                    func(arg);
                    timer = true;
                    Task.Delay(milliseconds)
                    .ContinueWith(t =>
                    {
                        if (t.IsCompletedSuccessfully)
                        {
                            timer = false;
                        }
                    });
                }
            };
        }
        public static Action Throttle(Action func, int milliseconds = 300)
        {
            return () =>
            {
                bool timer = false;
                if (!timer)
                {
                    func();
                    timer = true;
                    Task.Delay(milliseconds)
                    .ContinueWith(t =>
                    {
                        if (t.IsCompletedSuccessfully)
                        {
                            timer = false;
                        }
                    });
                }
            };
        }
    }
}