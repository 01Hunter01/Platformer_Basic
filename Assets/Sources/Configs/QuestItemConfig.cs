using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    [CreateAssetMenu(fileName = "QuestItemConfig", menuName = "Configs / QuestSystem / QuestItemConfig", order = 1)]
    public sealed class QuestItemConfig : ScriptableObject
    {
        public int questID;
        public List<int> questItemID;
    }
}
