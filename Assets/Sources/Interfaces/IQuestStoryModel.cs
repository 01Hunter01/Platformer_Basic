using System;

namespace Plarformer
{
    public interface IQuestStoryModel : IDisposable
    {
        bool IsDone { get; }
    }
}
