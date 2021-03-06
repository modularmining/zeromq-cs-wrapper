﻿namespace ZeroMQ
{
    using System;
    using System.Diagnostics;
    using System.Threading;

    // TODO: Refactor and use compiled expressions?
    internal static class ExecuteExtensions
    {
       
        public static TResult WithTimeout<T1, T2, TResult>(this ZmqSocket socket, Func<T1, T2, TResult> method, T1 arg1, T2 arg2, TimeSpan timeout)
        {
            if ((int)timeout.TotalMilliseconds < 1)
            {
                return method(arg1, arg2);
            }

            TResult receiveResult;

            var timer = Stopwatch.StartNew();

            do
            {
                receiveResult = method(arg1, arg2);

                if (socket.ReceiveStatus != ReceiveStatus.TryAgain)
                {
                    break;
                }
                
                Thread.Sleep(1);
            }
            while (timer.Elapsed <= timeout);

            return receiveResult;
        }

        public static TResult WithTimeout<T1, T2, T3, TResult>(this ZmqSocket socket, Func<T1, T2, T3, TResult> method, T1 arg1, T2 arg2, T3 arg3, TimeSpan timeout)
        {
            if ((int)timeout.TotalMilliseconds < 1)
            {
                return method(arg1, arg2, arg3);
            }

            TResult receiveResult;

            var timer = Stopwatch.StartNew();

            do
            {
                receiveResult = method(arg1, arg2, arg3);

                if (socket.ReceiveStatus != ReceiveStatus.TryAgain)
                {
                    break;
                }

                Thread.Sleep(1);
            }
            while (timer.Elapsed <= timeout);

            return receiveResult;
        }      
    }
}
