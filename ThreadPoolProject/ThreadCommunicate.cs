using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadPoolProject
{
    public class ThreadCommunicate
    {

        /// <summary>
        ///  实现两个线程的相互通信
        /// </summary>
        public static void CommunicateThread()
        {
            EventWaitHandle handleA = new AutoResetEvent(false);
            EventWaitHandle handleB = new AutoResetEvent(false);

            ThreadPool.QueueUserWorkItem(t =>
            {
                Console.WriteLine("A:我是A,我已经开始运行了");
                Thread.Sleep(2000);
                Console.WriteLine("A:我想睡觉了,B你先跑跑吧。");
                EventWaitHandle.SignalAndWait(handleB, handleA);
                Console.WriteLine("A:开始工作ing");
                Thread.Sleep(3000);
                Console.WriteLine("A:这个有点难，问下B");
                EventWaitHandle.SignalAndWait(handleB, handleA);
                Console.WriteLine("A:不错，今天任务搞定，我也闪人了。");

            });

            ThreadPool.QueueUserWorkItem(t =>
            {
                handleB.WaitOne();
                Console.WriteLine("B:我是B,我已经顶替A开始运行了。");
                Thread.Sleep(5000);
                Console.WriteLine("B:我的事情已经做完了，该让A搞搞了，休息一会。");
                EventWaitHandle.SignalAndWait(handleA, handleB);
                Console.WriteLine("B:hi,A我搞定了，下班了。");
                handleA.Set();

            });



            EventWaitHandle manualEvent = new ManualResetEvent(false);

            ThreadPool.QueueUserWorkItem(t =>
            {
                int i = 0;
                while (true)
                {
                    manualEvent.WaitOne();  //ManualResetEvent的Set()方法，让事件的终止状态永远为true,让这里一直能执行。
                    i++;                    //而AutoResetEvent的Set()方法，初始化让这里执行一次，然后再次执行时是非终止的。将阻塞原有线程的执行
                    Console.WriteLine("#" + i.ToString());
                    Thread.Sleep(1000);
                }

            });

            manualEvent.Set();

            manualEvent.Reset();

        }


        /// <summary>
        /// 控制并行线程的执行
        /// </summary>
        public static void SemaphoreTest()
        {
            Semaphore semaphore = new Semaphore(0, 3);

            for (int i = 0; i < 8; i++)
            {
                ThreadPool.QueueUserWorkItem(t =>
                {
                    semaphore.WaitOne();
                    Console.WriteLine("\t第：" + ((int)t).ToString() + "线程开始运行...");
                }, i);
            }

            ThreadPool.QueueUserWorkItem(t =>
            {
                for (int i = 0; i < 3; i++)
                {
                    Console.WriteLine("第:" + (i + 1).ToString() + "批线程开始执行...");
                    semaphore.Release(3);
                    Thread.Sleep(5000);
                }

            });

        }

    }
}
