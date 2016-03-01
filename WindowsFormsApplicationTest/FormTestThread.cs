using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace WindowsFormsApplicationTest
{
    public partial class FormTestThread : Form
    {
        public FormTestThread()
        {
            InitializeComponent();
        }

        private void FormTestThread_Load(object sender, EventArgs e)
        {
            // // 1.
        //    System.Threading.TimerCallback TimerDelegate =
        //        new System.Threading.TimerCallback(TimerTask);
            //HQ.GlobalClass.Thread.Timer t = new HQ.GlobalClass.Thread.Timer(TimerDelegate);

            // // 2.
            //HQ.GlobalClass.Thread.Timer t = new HQ.GlobalClass.Thread.Timer(delegate(object stateObject)
            //{
            //    System.Diagnostics.Debug.WriteLine("+");
            //    //label1.Text += "+";
            //    //if (label1.Text.Length > 10)
            //    //{
            //    //    (stateObject as HQ.GlobalClass.Thread.Timer.StateObject).Timer.Dispose();
            //    //}
            //});

            // // 3.
            //HQ.GlobalClass.Thread.RunTimer rt = new HQ.GlobalClass.Thread.RunTimer();
            //rt.Run();

            // // 4.
            //Thread thread = new Thread(DoWork);
            //thread.Start();

            // // 5.
            //ThreadStart threadDelegate = new ThreadStart(DoWork);
            //Thread newThread = new Thread(threadDelegate);
            //newThread.Start();

            // // 6.
#if DEBUG
            RF.GlobalClass.Thread.workerFunctionDelegate w = delegate() {
                label1.Text += "+";
            };
            RF.GlobalClass.Thread hqgct = new RF.GlobalClass.Thread("A",w, this);
            hqgct.Start();
#endif
        }
        public void DoWork()
        {
            System.Diagnostics.Debug.WriteLine("+");
            label1.Text += "+";
            Console.WriteLine("Static thread procedure.");
        }
        public int Data;
        public void DoMoreWork()
        {
            System.Diagnostics.Debug.WriteLine("+");
            Console.WriteLine("Instance thread procedure. Data={0}", Data);
        }
        class Work
        {
            public static void DoWork()
            {
                System.Diagnostics.Debug.WriteLine("+");
                Console.WriteLine("Static thread procedure.");
            }
            public int Data;
            public void DoMoreWork()
            {
                System.Diagnostics.Debug.WriteLine("+");
                Console.WriteLine("Instance thread procedure. Data={0}", Data);
            }
        }


        public void TimerTask(object stateObject)
        {
#if DEBUG
            System.Diagnostics.Debug.WriteLine("+");
            label1.Text += "+";
            if (label1.Text.Length > 10)
            {
                (stateObject as RF.GlobalClass.Thread.Timer.StateObject).Timer.Dispose();
            }
#endif
        }
    }
}
