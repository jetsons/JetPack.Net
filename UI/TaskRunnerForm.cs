#if !STANDARD

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Jetsons.JetPack {

	/// <summary>
	/// Displays a dialog that runs a sequence of tasks in the background and displays its status.
	/// </summary>
	public class TaskRunnerForm : Form {
		
		#region Task Runner

		public string TaskTitle;
		public BackgroundWorker worker;
		public int TotalSteps;
		public int CurrentStep;
		public Func<TaskRunnerForm, string> TaskDelegate;
		public bool AutoClose = false;
		public bool AllDone = false;

		public void Init(string dialogTitle, int totalSteps, Func<TaskRunnerForm, string> task) {

			this.TaskTitle = dialogTitle;
			this.TotalSteps = totalSteps;
			this.Text = dialogTitle;
			this.LblTitle.Text = dialogTitle;
			this.ProgressBar.Minimum = 0;
			this.ProgressBar.Maximum = TotalSteps;
			this.ProgressBar.Value = 0;
			this.TaskDelegate = task;

			UpdateProgress();
		}

		public void Start() {
			worker = new BackgroundWorker();
			worker.DoWork += Worker_DoWork;
			worker.RunWorkerAsync();
		}
		/// <summary>
		/// Log an extra progress event into the list. You can call this at any time from your delegate.
		/// </summary>
		/// <param name="text"></param>
		public void AddEvent(string text) {

			// add item to list
			this.BeginInvoke((Action)(() => {
				AddItem(CurrentStep + " of " + TotalSteps + " - " + text);
			}));

		}
		/// <summary>
		/// Change the dialog's title
		/// </summary>
		/// <param name="text"></param>
		public void SetTitle(string text) {

			// change the form title
			this.BeginInvoke((Action)(() => {
				this.LblTitle.Text = text;
			}));

		}

		private void Worker_DoWork(object sender, DoWorkEventArgs e) {

			// per step
			for (int i = 0; i < TotalSteps; i++) {

				// execute the work delegate
				CurrentStep = i;
				AllDone = i == (TotalSteps - 1);
				var text = TaskDelegate(this);

				// exit if null returned
				if (text == null) {
					this.BeginInvoke((Action)(() => {
						Close();
					}));
					return;
				}

				this.BeginInvoke((Action)(() => {

					// update the progress bar
					UpdateProgress();

					// add item to list
					AddItem(CurrentStep + " of " + TotalSteps + " - " + text);

				}));


			}

			// when all done set the progress bar to 100%
			CurrentStep = TotalSteps;

			this.BeginInvoke((Action)(async () => {

				// update the progress bar
				UpdateProgress();

				// add item to list
				AddItem(TaskTitle + " completed!");

				// create a timer to destroy this window
				if (AutoClose) {
					await System.Threading.Tasks.Task.Delay(5000);
					this.Close();
				}

			}));

		}


		#endregion

		#region Windows Form UI

		private ProgressBar ProgressBar;
		private Label LblTitle;
		private Label LblProgress;
		private ListBox LstProgress;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		public TaskRunnerForm() {
			InitializeComponent();
		}
		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.ProgressBar = new ProgressBar();
			this.LblTitle = new Label();
			this.LblProgress = new Label();
			this.LstProgress = new ListBox();
			this.SuspendLayout();
			// 
			// ProgressBar
			// 
			this.ProgressBar.Anchor = ((AnchorStyles)(((AnchorStyles.Top | AnchorStyles.Left)
			| AnchorStyles.Right)));
			this.ProgressBar.Location = new Point(14, 107);
			this.ProgressBar.Margin = new Padding(3, 4, 3, 4);
			this.ProgressBar.Name = "ProgressBar";
			this.ProgressBar.Size = new Size(579, 30);
			this.ProgressBar.TabIndex = 0;
			// 
			// LblTitle
			// 
			this.LblTitle.AutoSize = true;
			this.LblTitle.Font = new Font("Segoe UI", 21.75F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
			this.LblTitle.Location = new Point(12, 20);
			this.LblTitle.Name = "LblTitle";
			this.LblTitle.Size = new Size(94, 40);
			this.LblTitle.TabIndex = 1;
			this.LblTitle.Text = "label1";
			// 
			// LblProgress
			// 
			this.LblProgress.AutoSize = true;
			this.LblProgress.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
			this.LblProgress.Location = new Point(14, 67);
			this.LblProgress.Name = "LblProgress";
			this.LblProgress.Size = new Size(63, 25);
			this.LblProgress.TabIndex = 2;
			this.LblProgress.Text = "label2";
			// 
			// LstProgress
			// 
			this.LstProgress.Anchor = ((AnchorStyles)((((AnchorStyles.Top | AnchorStyles.Bottom)
			| AnchorStyles.Left)
			| AnchorStyles.Right)));
			this.LstProgress.FormattingEnabled = true;
			this.LstProgress.ItemHeight = 17;
			this.LstProgress.Location = new Point(14, 144);
			this.LstProgress.Name = "LstProgress";
			this.LstProgress.Size = new Size(581, 344);
			this.LstProgress.TabIndex = 3;
			// 
			// AsyncTaskManager
			// 
			this.StartPosition = FormStartPosition.CenterScreen;
			this.AutoScaleDimensions = new SizeF(7F, 17F);
			this.AutoScaleMode = AutoScaleMode.Font;
			this.ClientSize = new Size(607, 497);
			this.Controls.Add(this.LstProgress);
			this.Controls.Add(this.LblProgress);
			this.Controls.Add(this.LblTitle);
			this.Controls.Add(this.ProgressBar);
			this.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
			this.Margin = new Padding(3, 4, 3, 4);
			this.Name = "AsyncTaskManager";
			this.Text = "Task";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		#endregion

		#region Progress UI

		private void UpdateProgress() {
			int percent = (int)Math.Round((((double)CurrentStep - 1) / ((double)TotalSteps - 1)) * 100);
			if (CurrentStep < TotalSteps) {
				this.LblProgress.Text = percent + "% complete - " + (CurrentStep - 1) + " of " + TotalSteps + " done";
			}
			else {
				this.LblProgress.Text = percent + "% complete";
			}
			this.ProgressBar.Value = CurrentStep > TotalSteps ? TotalSteps : CurrentStep;
		}

		private void AddItem(string text) {
			LstProgress.Items.Add(text);
			LstProgress.SelectedIndex = LstProgress.Items.Count - 1;
		}

		#endregion

	}
	
}

#endif
