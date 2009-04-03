﻿namespace VGMToolbox.forms
{
    partial class Nsf_Nsfe2NsfM3uForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.grpSourceFiles = new System.Windows.Forms.GroupBox();
            this.lblDragNDrop = new System.Windows.Forms.Label();
            this.tbNSF_nsfe2m3uSource = new System.Windows.Forms.TextBox();
            this.grpOptions = new System.Windows.Forms.GroupBox();
            this.cbNSFE_OneM3uPerTrack = new System.Windows.Forms.CheckBox();
            this.pnlLabels.SuspendLayout();
            this.pnlTitle.SuspendLayout();
            this.pnlButtons.SuspendLayout();
            this.grpSourceFiles.SuspendLayout();
            this.grpOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlLabels
            // 
            this.pnlLabels.Location = new System.Drawing.Point(0, 486);
            this.pnlLabels.Size = new System.Drawing.Size(716, 19);
            // 
            // pnlTitle
            // 
            this.pnlTitle.Size = new System.Drawing.Size(716, 20);
            // 
            // tbOutput
            // 
            this.tbOutput.Location = new System.Drawing.Point(0, 409);
            this.tbOutput.Size = new System.Drawing.Size(716, 77);
            // 
            // pnlButtons
            // 
            this.pnlButtons.Location = new System.Drawing.Point(0, 389);
            this.pnlButtons.Size = new System.Drawing.Size(716, 20);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(656, 0);
            // 
            // btnDoTask
            // 
            this.btnDoTask.Location = new System.Drawing.Point(596, 0);
            // 
            // grpSourceFiles
            // 
            this.grpSourceFiles.Controls.Add(this.lblDragNDrop);
            this.grpSourceFiles.Controls.Add(this.tbNSF_nsfe2m3uSource);
            this.grpSourceFiles.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpSourceFiles.Location = new System.Drawing.Point(0, 23);
            this.grpSourceFiles.Name = "grpSourceFiles";
            this.grpSourceFiles.Size = new System.Drawing.Size(716, 61);
            this.grpSourceFiles.TabIndex = 5;
            this.grpSourceFiles.TabStop = false;
            this.grpSourceFiles.Text = "Files";
            // 
            // lblDragNDrop
            // 
            this.lblDragNDrop.AutoSize = true;
            this.lblDragNDrop.Location = new System.Drawing.Point(6, 42);
            this.lblDragNDrop.Name = "lblDragNDrop";
            this.lblDragNDrop.Size = new System.Drawing.Size(171, 13);
            this.lblDragNDrop.TabIndex = 5;
            this.lblDragNDrop.Text = "Drag and Drop folders or files here.";
            // 
            // tbNSF_nsfe2m3uSource
            // 
            this.tbNSF_nsfe2m3uSource.AllowDrop = true;
            this.tbNSF_nsfe2m3uSource.Location = new System.Drawing.Point(6, 19);
            this.tbNSF_nsfe2m3uSource.Name = "tbNSF_nsfe2m3uSource";
            this.tbNSF_nsfe2m3uSource.Size = new System.Drawing.Size(259, 20);
            this.tbNSF_nsfe2m3uSource.TabIndex = 0;
            this.tbNSF_nsfe2m3uSource.DragDrop += new System.Windows.Forms.DragEventHandler(this.tbNSF_nsfe2m3uSource_DragDrop);
            this.tbNSF_nsfe2m3uSource.DragEnter += new System.Windows.Forms.DragEventHandler(this.doDragEnter);
            // 
            // grpOptions
            // 
            this.grpOptions.Controls.Add(this.cbNSFE_OneM3uPerTrack);
            this.grpOptions.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpOptions.Location = new System.Drawing.Point(0, 84);
            this.grpOptions.Name = "grpOptions";
            this.grpOptions.Size = new System.Drawing.Size(716, 41);
            this.grpOptions.TabIndex = 7;
            this.grpOptions.TabStop = false;
            this.grpOptions.Text = "Options";
            // 
            // cbNSFE_OneM3uPerTrack
            // 
            this.cbNSFE_OneM3uPerTrack.AutoSize = true;
            this.cbNSFE_OneM3uPerTrack.Location = new System.Drawing.Point(6, 19);
            this.cbNSFE_OneM3uPerTrack.Name = "cbNSFE_OneM3uPerTrack";
            this.cbNSFE_OneM3uPerTrack.Size = new System.Drawing.Size(177, 17);
            this.cbNSFE_OneM3uPerTrack.TabIndex = 0;
            this.cbNSFE_OneM3uPerTrack.Text = "Output additional .m3u per track";
            this.cbNSFE_OneM3uPerTrack.UseVisualStyleBackColor = true;
            // 
            // Nsf_Nsfe2NsfM3uForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(716, 527);
            this.Controls.Add(this.grpOptions);
            this.Controls.Add(this.grpSourceFiles);
            this.Name = "Nsf_Nsfe2NsfM3uForm";
            this.Text = "Nsf_Nsfe2NsfM3uForm";
            this.Controls.SetChildIndex(this.pnlLabels, 0);
            this.Controls.SetChildIndex(this.tbOutput, 0);
            this.Controls.SetChildIndex(this.pnlTitle, 0);
            this.Controls.SetChildIndex(this.pnlButtons, 0);
            this.Controls.SetChildIndex(this.grpSourceFiles, 0);
            this.Controls.SetChildIndex(this.grpOptions, 0);
            this.pnlLabels.ResumeLayout(false);
            this.pnlLabels.PerformLayout();
            this.pnlTitle.ResumeLayout(false);
            this.pnlTitle.PerformLayout();
            this.pnlButtons.ResumeLayout(false);
            this.pnlButtons.PerformLayout();
            this.grpSourceFiles.ResumeLayout(false);
            this.grpSourceFiles.PerformLayout();
            this.grpOptions.ResumeLayout(false);
            this.grpOptions.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpSourceFiles;
        private System.Windows.Forms.Label lblDragNDrop;
        private System.Windows.Forms.TextBox tbNSF_nsfe2m3uSource;
        private System.Windows.Forms.GroupBox grpOptions;
        private System.Windows.Forms.CheckBox cbNSFE_OneM3uPerTrack;
    }
}