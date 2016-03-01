using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace RF
{

    public class ThreadClass
    {
        // delegates used to call MainForm functions from worker thread
        public delegate void DelegateDoInLoop(int s);
        public delegate void DelegateThreadFinished();
        // worker thread
        public Thread thread;

        // events used to stop worker thread
        public ManualResetEvent eventStopThread;
        public ManualResetEvent eventThreadStopped;

        // Delegate instances used to cal user interface functions 
        // from worker thread:
        public DelegateDoInLoop delegateDoInLoop;
        public DelegateThreadFinished delegateThreadFinished;

        public ThreadClass(String name,object m_form, Action<int> doInLoop, Action threadFinished)
        {
            this.name = name;
            this.form = m_form;

            // initialize delegates
            delegateDoInLoop = new DelegateDoInLoop(doInLoop);
            delegateThreadFinished = new DelegateThreadFinished(threadFinished);

            // initialize events
            eventStopThread = new ManualResetEvent(false);
            eventThreadStopped = new ManualResetEvent(false);
        }

        public bool Start(Action target)
        {
            Boolean result = false;
            // reset events
            eventStopThread.Reset();
            eventThreadStopped.Reset();

            // create worker thread instance
            thread = new Thread(new ThreadStart(target));

            thread.Name = this.name;	// looks nice in Output window

            thread.Start();
            result = true;
            return result;
        }


        //// Function runs in worker thread and emulates long process.
        //public void Run()
        //{
        //    int i;
        //    String s;

        //    for (i = 1; i <= 10; i++)
        //    {
        //        // make step
        //        s = "Step number " + i.ToString() + " executed";

        //        Thread.Sleep(400);

        //        // Make synchronous call to main form.
        //        // MainForm.AddString function runs in main thread.
        //        // To make asynchronous call use BeginInvoke
        //        //m_form.Invoke(m_form.m_DelegateAddString, new Object[] { s });
        //        m_form.Invoke(m_DelegateAddString, new Object[] { s });


        //        // check if thread is cancelled
        //        //if (m_EventStop.WaitOne(0, true))
        //        if (m_EventStopThread.WaitOne(0, true))
        //        {
        //            // clean-up operations may be placed here
        //            // ...

        //            // inform main thread that this thread stopped
        //            //m_EventStopped.Set();
        //            m_EventThreadStopped.Set();

        //            return;
        //        }
        //    }

        //    // Make asynchronous call to main form
        //    // to inform it that thread finished
        //    //m_form.Invoke(m_form.m_DelegateThreadFinished, null);
        //    m_form.Invoke(m_DelegateThreadFinished, null);
        //}

        public string name { get; set; }

        public object form { get; set; }


        public class Worker
        {
            public event EventHandler<ProgressChangedArgs> ProgressChanged;

            protected void OnProgressChanged(ProgressChangedArgs e)
            {
                if (ProgressChanged != null)
                {
                    ProgressChanged(this, e);
                }
                else
                {
                    // do not know what to do;
                }
            }

            public void StartWork()
            {
                try
                {
                    Thread.Sleep(100);
                    OnProgressChanged(new ProgressChangedArgs("Progress Changed"));
                    Thread.Sleep(100);
                }
                catch (Exception ex)
                {
                }
            }
        }


        public class ProgressChangedArgs : EventArgs
        {
            public string Progress { get; private set; }
            public ProgressChangedArgs(string progress)
            {
                Progress = progress;
            }
        }
    }
}
