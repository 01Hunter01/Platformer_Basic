using System;

namespace Platformer
{
    public interface IQuestStoryModel : IDisposable
    {
        bool IsDone { get; }
    }
}
