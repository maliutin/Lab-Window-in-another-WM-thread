using System;
using System.Threading;
using System.Windows.Forms;

namespace WindowsFormsLab01
{
    class SecondWindowsThread : IDisposable
    {
        private Thread thread;
        private SynchronizationContext ctx;
        private ManualResetEvent mre;

        public SecondWindowsThread()
        {
            this.mre = new ManualResetEvent(false);
            try
            {
                this.thread = new Thread(
                    () =>
                    {
                        Application.Idle += this.Initialize;
                        Application.Run();
                    }
                );
                thread.IsBackground = true;
                thread.SetApartmentState(ApartmentState.STA);
                thread.Name = "WaitDialog";
                thread.Start();
                this.mre.WaitOne();
            }
            finally
            {
                this.mre.Dispose();
            }
        }

        private void Initialize(Object sender, EventArgs e)
        {
            this.ctx = SynchronizationContext.Current;
            this.mre.Set();
            Application.Idle -= this.Initialize;
        }

        #region IDisposable Support
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (this.ctx != null)
                    {
                        this.ctx.Send((_) => Application.ExitThread(), null);
                        this.ctx = null;
                    }
                }
                disposedValue = true;
            }
        }

        void IDisposable.Dispose()
        {
            Dispose(true);
        }
        #endregion

        public SynchronizationContext SynchronizationContext
        {
            get { return this.ctx; }
        }

    }
}
