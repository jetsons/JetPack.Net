using System;
using System.Windows.Forms;

namespace Jetsons.JetPack {

	/// <summary>
	/// Displays dialogs that run tasks in the background and displays their status.
	/// </summary>
	public static class BackgroundTasks {
		
		/// <summary>
		/// Displays a dialog that runs a single task in the background, while displaying a progress bar and a detailed list of progress events.
		/// This class runs a delegate once. You need to call NextStep to move the progress bar ahead.
		/// To log extra progress events you can call AddEvent at any time from your delegate.
		/// </summary>
		public static TaskStatusForm ShowStatusDialog(string dialogTitle, int totalSteps, Action<TaskStatusForm> task) {
			var manager = new TaskStatusForm();
			manager.Show();
			manager.Init(dialogTitle, totalSteps, task);
			manager.Start();
			return manager;
		}

		/// <summary>
		/// Displays a dialog that runs a sequence of tasks in the background, while displaying a progress bar and a detailed list of progress events.
		/// This class runs a delegate N times. Returning a string will cause a progress event to get logged in the list.
		/// Returning null will cause the process to be aborted.
		/// To log extra progress events you can call AddEvent at any time from your delegate.
		/// </summary>
		public static TaskRunnerForm ShowRunnerDialog(string dialogTitle, int totalSteps, Func<TaskRunnerForm, string> task) {
			var manager = new TaskRunnerForm();
			manager.Show();
			manager.Init(dialogTitle, totalSteps, task);
			manager.Start();
			return manager;
		}


	}
}