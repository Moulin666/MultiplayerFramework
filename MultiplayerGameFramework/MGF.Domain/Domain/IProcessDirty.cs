using System.ComponentModel;

namespace MGF.Domain
{
    public interface IProcessDirty : INotifyPropertyChanged
    {
        bool IsNew { get; }

        bool IsDirty { get; }

        bool IsDeleted { get; }
    }
}
