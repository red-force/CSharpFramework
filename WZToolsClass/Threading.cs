using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RF
{
    public partial class GlobalClass
    {
#if DEBUG
        public partial class Thread
        {
            public Thread()
            {
            }

            public partial class Timer
            {
                public bool TimerCanceled = false;
                private System.Threading.Timer _timer;
                private System.Threading.TimerCallback TimerCallback;
                // public Object stateObject = null;

                public class StateObject
                {
                    public System.Threading.Timer Timer;

                }

                public Timer(System.Threading.TimerCallback timerCallback, int dueTime = 1000, int period = 2000)
                {
                    this.TimerCallback = timerCallback;
                    // Create a timer that calls a procedure every 2 seconds. 
                    // Note: There is no Start method; the timer starts running as soon as  
                    // the instance is created.
                    StateObject stateObject = new StateObject();
                    System.Threading.Timer TimerItem =
                        new System.Threading.Timer(this.TimerCallback, state: stateObject, dueTime: dueTime, period: period);
                    (stateObject as StateObject).Timer = TimerItem;
                    this._timer = TimerItem;
                }

                public void Dispose()
                {
                    this._timer.Dispose();
                }


            }

            public class RunTimer
            {
                private class StateObjClass
                {
                    // Used to hold parameters for calls to TimerTask. 
                    public int SomeValue;
                    public System.Threading.Timer TimerReference;
                    public bool TimerCanceled;
                }

                public void Run()
                {
                    StateObjClass StateObj = new StateObjClass();
                    StateObj.TimerCanceled = false;
                    StateObj.SomeValue = 1;
                    System.Threading.TimerCallback TimerDelegate =
                        new System.Threading.TimerCallback(TimerTask);

                    // Create a timer that calls a procedure every 2 seconds. 
                    // Note: There is no Start method; the timer starts running as soon as  
                    // the instance is created.
                    System.Threading.Timer TimerItem =
                        new System.Threading.Timer(TimerDelegate, StateObj, 2000, 2000);

                    // Save a reference for Dispose.
                    StateObj.TimerReference = TimerItem;

                    // Run for ten loops. 
                    while (StateObj.SomeValue < 10)
                    {
                        // Wait one second.
                        System.Threading.Thread.Sleep(1000);
                    }

                    // Request Dispose of the timer object.
                    StateObj.TimerCanceled = true;
                }

                private void TimerTask(object StateObj)
                {
                    StateObjClass State = (StateObjClass)StateObj;
                    // Use the interlocked class to increment the counter variable.
                    System.Threading.Interlocked.Increment(ref State.SomeValue);
                    System.Diagnostics.Debug.WriteLine("Launched new thread  " + DateTime.Now.ToString());
                    if (State.TimerCanceled)
                    // Dispose Requested.
                    {
                        State.TimerReference.Dispose();
                        System.Diagnostics.Debug.WriteLine("Done  " + DateTime.Now.ToString());
                    }
                }
            }

            public delegate void workerFunctionDelegate();
            private Worker _worker;
            private System.Threading.Thread _workerThread;
            public System.Threading.Thread WorkerThread
            {
                get { return _workerThread; }
                set { _workerThread = value; }
            }

            private System.Windows.Forms.Control _formControl;

            public System.Windows.Forms.Control FormControl
            {
                get { return _formControl; }
                set { _formControl = value; }
            }
            // private System.Web.UI.Control _webControl;


            public Thread(String threadName = "workerThread", Delegate workerFunc= null, System.Windows.Forms.Control formControl = null, int sleepTime = 100)
            {
                this.Name = threadName;
                this.WorkerFunc = workerFunc;
                this.FormControl = formControl;
                this.SleepTime = sleepTime;
            }

            public void Start(){
                _worker = new Worker();
                _worker.ProgressChanged += new EventHandler<GlobalClass.Thread.Worker.ProgressChangedArgs>(this.OnWorkerProgressChanged);
                //foreach (System.Diagnostics.ProcessThread pt in System.Diagnostics.Process.GetCurrentProcess().Threads) // Stop the thread with the same name;
                //{
                    
                //    if (pt.Name == this.Name)
                //    {
                //        if (pt.IsAlive)
                //        {
                //            pt.Interrupt();
                //            pt.Abort();
                //        }
                //        else
                //        {
                //        }
                //    }
                //}
                WorkerThread = new System.Threading.Thread(new System.Threading.ThreadStart(_worker.StartWork));
                WorkerThread.Name = this.Name;
                WorkerThread.IsBackground = true;
                WorkerThread.Start();
            }

            private void OnWorkerProgressChanged(object sender, GlobalClass.Thread.Worker.ProgressChangedArgs e)
            {
                try
                {
                    //cross thread - so you don't get the cross theading exception
                    if (this.FormControl.InvokeRequired)
                    {
                        this.FormControl.BeginInvoke((System.Windows.Forms.MethodInvoker)delegate
                        {
                            OnWorkerProgressChanged(sender, e);
                        });
                        return;
                    }

                    //change control
                    //this.label1.Text += Dict.translate("MESSAGE", "In Reading");//" " + e.Progress;

                    //workerFunctionDelegate w = workerFunction;
                    //w.BeginInvoke(Convert.ToInt32(order.ticketCount), null, null);

                    if (null != this.WorkerFunc)
                    {
                        System.Threading.Thread.Sleep(this.SleepTime);
                        this.FormControl.BeginInvoke(this.WorkerFunc);
                    }
                    else { }
                }
                catch (Exception ex)
                {
                }
            }

            public partial class Worker
            {
                #region fold Worker\ ProgressChangedArgs
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
                        System.Threading.Thread.Sleep(100);
                        OnProgressChanged(new ProgressChangedArgs("Progress Changed"));
                        System.Threading.Thread.Sleep(100);
                    }
                    catch (Exception ex)
                    {
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

                #endregion
            }

            public Delegate WorkerFunc { get; set; }

            public string Name { get; set; }

            private int _sleepTime = 100;

            public int SleepTime
            {
                get { return _sleepTime; }
                set { _sleepTime = value; }
            }
        }
#endif
    }
}
