using UnityEngine;

namespace MyNamespace
{
    public interface IQuestModel
    {
        bool TryComplete(GameObject player);
    }
}
