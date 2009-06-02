﻿using System;
using System.ComponentModel;
using System.IO;

using VGMToolbox.format.util;
using VGMToolbox.plugin;

namespace VGMToolbox.tools.xsf
{
    class Time2sfWorker : BackgroundWorker, IVgmtBackgroundWorker
    {
        private int fileCount = 0;
        private int maxFiles = 0;
        private VGMToolbox.util.ProgressStruct progressStruct;

        public struct Time2sfStruct
        {
            public string PathTo2sf;
            public string PathToSdat;
            public bool DoSingleLoop;
        }

        public Time2sfWorker()
        {
            this.progressStruct = new VGMToolbox.util.ProgressStruct();
            this.fileCount = 0;
            this.maxFiles = 0;
            
            WorkerReportsProgress = true;
            WorkerSupportsCancellation = true;
        }

        private void time2sfs(Time2sfStruct pTime2sfStruct, DoWorkEventArgs e)
        {
            if (!CancellationPending)
            {
                string outputMessages;
                
                XsfUtil.Time2sfStruct t = new XsfUtil.Time2sfStruct();
                t.Mini2sfDirectory = pTime2sfStruct.PathTo2sf;
                t.SdatPath = pTime2sfStruct.PathToSdat;
                t.DoSingleLoop = pTime2sfStruct.DoSingleLoop;

                try
                {
                    XsfUtil.Time2sfFolder(t, out outputMessages);

                    this.progressStruct.Clear();

                    if (!String.IsNullOrEmpty(outputMessages))
                    {
                        this.progressStruct.ErrorMessage = String.Format("ERROR: {0}",
                            outputMessages) + Environment.NewLine;
                    }

                    ReportProgress(100, this.progressStruct);
                }
                catch(Exception _e)
                {
                    this.progressStruct.Clear();
                    this.progressStruct.ErrorMessage = String.Format("ERROR: {0}",
                        _e.Message) + Environment.NewLine;
                    ReportProgress(0, this.progressStruct);
                }                
            }
            else
            {
                e.Cancel = true;
                return;
            }
        }

        protected override void OnDoWork(DoWorkEventArgs e)
        {
            Time2sfStruct time2sfStruct = (Time2sfStruct) e.Argument;
            time2sfStruct.PathTo2sf = Path.GetFullPath(time2sfStruct.PathTo2sf);
            time2sfStruct.PathToSdat = Path.GetFullPath(time2sfStruct.PathToSdat);
            
            this.time2sfs(time2sfStruct, e);
        }    
    }
}
