using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace OrcaMDF.RawCore
{
	public class RawDataFile : IDisposable
	{
		private readonly Stream stream;
		private readonly long fileSize;

        private void logException(Exception ex, String extrainfo = "", Boolean silent = true)
        {
            if (ex != null)
            {
                File.AppendAllText("ErrorLog.txt",
                    DateTime.Now +
                    Environment.NewLine +
                    "----------" +
                    Environment.NewLine +
                    ex +
                    Environment.NewLine +
                    Environment.NewLine +
                    extrainfo);
            }
            else
            {
                File.AppendAllText("ErrorLog.txt",
                    DateTime.Now +
                    Environment.NewLine +
                    Environment.NewLine +
                    extrainfo);
            }

            if (silent != true)
            {
                string msg =
                    "An exception has occurred:" + Environment.NewLine +
                    ex.Message + Environment.NewLine +
                    Environment.NewLine +
                    "To help improve OrcaMDF, I would appreciate if you would send the ErrorLog.txt file to me at mark@improve.dk" + Environment.NewLine +
                    Environment.NewLine +
                    "The error log does not contain any sensitive information, feel free to check it to be 100% certain. The ErrorLog.txt file is located in the same directory as the OrcaMDF Studio application." +
                    Environment.NewLine + extrainfo;

                MessageBox.Show(msg, "Uh oh!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public int PageCount
		{
			get { return (int)(fileSize / 8192); }
		}

		public RawDataFile(string filePath)
		{
			stream = File.OpenRead(filePath);
			fileSize = new FileInfo(filePath).Length;
		}

		public RawPage GetPage(int pageID)
		{
			return new RawPage(pageID, GetPageBytes(pageID));
		}

        public RawPage GetPage(int pageID, ref bool lastpage)
        {
            return new RawPage(pageID, GetPageBytes(pageID, ref lastpage));
        }

        public byte[] GetPageBytes(int pageID)
		{
            var bytes = new byte[8192];
            try
            {
                stream.Seek(pageID * 8192, SeekOrigin.Begin);

                stream.Read(bytes, 0, 8192);

                return bytes;
            } catch (Exception ex)
            {
                logException(ex);
                return bytes;
            }
		}

        public byte[] GetPageBytes(long pageID, ref bool lastpage)
        {
            var bytes = new byte[8192];
            try
            {
                stream.Seek(pageID * 8192L, SeekOrigin.Begin);

                stream.Read(bytes, 0, 8192);
                lastpage = false;

                return bytes;
            }
            catch (Exception ex)
            {
                logException(ex);
                lastpage = false;
                return bytes;
            }
        }

        /// <summary>
        /// Returns all the pages in the database that can be successfully parsed. Note that these may
        /// still be corrupt, but they're at least structurally valid.
        /// </summary>
        public IEnumerable<RawPage> BestEffortPages
		{
			get
			{
				for (int i = 0; i < PageCount; i++)
				{
					RawPage page = null;

					try
					{
                        bool lastpage = false;
						page = GetPage(i, ref lastpage);
                        if (lastpage == true) { break; }
					}
					catch
					{ }

					if (page != null)
						yield return page;
				}
			}
		}

		/// <summary>
		/// Returns all the pages in the database
		/// </summary>
		public IEnumerable<RawPage> Pages
		{
			get
			{
                for (int i = 0; i < PageCount; i++)
                {
                    bool lastpage = false;
                    RawPage page = GetPage(i, ref lastpage);
                    yield return page;
                    if (lastpage == true) { break; }
                }
			}
		}

		public void Dispose()
		{
			if (stream != null)
				stream.Dispose();
		}
	}
}