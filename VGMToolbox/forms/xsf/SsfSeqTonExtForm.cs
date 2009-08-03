﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using VGMToolbox.plugin;
using VGMToolbox.tools.xsf;

namespace VGMToolbox.forms.xsf
{
    public partial class SsfSeqTonExtForm : AVgmtForm
    {
        public SsfSeqTonExtForm(TreeNode pTreeNode)
            : base(pTreeNode)
        {
            // set title
            this.lblTitle.Text = ConfigurationSettings.AppSettings["Form_SeqextTonextFE_Title"];
            this.tbOutput.Text = ConfigurationSettings.AppSettings["Form_SeqextTonextFE_IntroText"];

            // hide the DoTask button since this is a drag and drop form
            this.btnDoTask.Hide();

            InitializeComponent();

            // messages
            this.BackgroundWorker = new SsfSeqTonExtractorWorker();
            this.BeginMessage = ConfigurationSettings.AppSettings["Form_SeqextTonextFE_MessageBegin"];
            this.CompleteMessage = ConfigurationSettings.AppSettings["Form_SeqextTonextFE_MessageComplete"];
            this.CancelMessage = ConfigurationSettings.AppSettings["Form_SeqextTonextFE_MessageCancel"];

            this.grpSource.Text =
                ConfigurationSettings.AppSettings["Form_SeqextTonextFE_GroupSource"];
            this.lblDragNDrop.Text =
                ConfigurationSettings.AppSettings["Form_SeqextTonextFE_LblDragNDrop"];
            this.cbExtractToSubfolder.Text =
                ConfigurationSettings.AppSettings["Form_SeqextTonextFE_CheckBoxExtractToSubfolder"];
            this.lblAuthor.Text =
                ConfigurationSettings.AppSettings["Form_SeqextTonextFE_LblAuthor"];
        }

        private void tbSsfSqTonExtSource_DragDrop(object sender, DragEventArgs e)
        {
            string[] s = (string[])e.Data.GetData(DataFormats.FileDrop, false);

            SsfSeqTonExtractorWorker.SsfSeqTonExtractorStruct stexStruct = new SsfSeqTonExtractorWorker.SsfSeqTonExtractorStruct();
            stexStruct.SourcePaths = s;
            stexStruct.extractToSubFolder = cbExtractToSubfolder.Checked;

            base.backgroundWorker_Execute(stexStruct);
        }
        protected override void doDragEnter(object sender, DragEventArgs e)
        {
            base.doDragEnter(sender, e);
        }
    }
}
