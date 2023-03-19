using UnityEngine;

namespace Platformer
{
    [CreateAssetMenu(fileName = "QuestConfig", menuName = "Configs / QuestSystem / QuestConfig", order = 1)]
    public sealed class QuestConfig : ScriptableObject
    {
        public int id;
        public QuestType type;
    }
}
