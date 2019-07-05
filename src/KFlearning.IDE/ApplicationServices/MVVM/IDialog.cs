using MahApps.Metro.Controls.Dialogs;

namespace KFlearning.IDE.ApplicationServices
{
    public interface IDialog
    {
        BaseMetroDialog DialogInstance { get; set; }
        MessageDialogResult DialogResult { get; set; }
        object State { get; set; }
    }
}
