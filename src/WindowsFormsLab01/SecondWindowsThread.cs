using System;
using System.Threading;
using System.Windows.Forms;

namespace WindowsFormsLab01
{
    class SecondWindowsThread : IDisposable
    {
        private SynchronizationContext ctx;

        public SecondWindowsThread()
        {
            ManualResetEvent mre = new ManualResetEvent(false);
            try
            {
                Thread thread = new Thread(
                    () =>
                    {
                        this.ctx = new WindowsFormsSynchronizationContext();
                        mre.Set();
                        Application.Run();
                    }
                );
                thread.IsBackground = true;
                thread.SetApartmentState(ApartmentState.STA);
                thread.Start();
                mre.WaitOne();
            }
            finally
            {
                mre.Dispose();
            }
        }

        #region IDisposable Support
        private bool disposedValue = false;

        public void Dispose()
        {
            if (disposedValue) return;

            if (this.ctx != null)
            {
                this.ctx.Send((_) => Application.ExitThread(), null);
                this.ctx = null;
            }
            disposedValue = true;
        }

        #endregion

        public SynchronizationContext SynchronizationContext
        {
            get
            {
                if (this.disposedValue) throw new ObjectDisposedException(nameof(SynchronizationContext));
                return this.ctx;
            }
        }

    }
}
