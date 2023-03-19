using System;

namespace Platformer
{
    public interface IQuest : IDisposable
    {
        event EventHandler<IQuest> QuestCompleted;
        bool IsCompleted { get; }
        void Reset();
    }
}
