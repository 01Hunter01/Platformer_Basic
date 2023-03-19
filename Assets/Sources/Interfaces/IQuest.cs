using System;

namespace Plarformer
{
    public interface IQuest : IDisposable
    {
        event EventHandler<IQuest> QuestCompleted;
        bool IsComleted { get; }
        void Reset();
    }
}
