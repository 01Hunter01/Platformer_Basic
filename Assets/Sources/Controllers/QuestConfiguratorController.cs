using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public sealed class QuestConfiguratorController
    {
        private QuestObjectView _singleQuestView;
        private QuestController _singleQuestController;
        private QuestStoryConfig[] _questStoryConfigs;
        private QuestObjectView[] _storyQuestsView;
        private QuestCoinModel _questCoinModel;

        private List<IQuestStory> _questStoryList;
        private InteractiveObjectView _player;

        private Dictionary<QuestType, Func<IQuestModel>> _questFactory = new Dictionary<QuestType, Func<IQuestModel>>(10);

        private Dictionary<StoryType, Func<List<IQuest>, IQuestStory>> _questStoryFactory =
            new Dictionary<StoryType, Func<List<IQuest>, IQuestStory>>(10);

        public QuestConfiguratorController(QuestView questView, InteractiveObjectView player)
        {
            _singleQuestView = questView.singleQuest;
            _questStoryConfigs = questView.storyConfigs;
            _storyQuestsView = questView.questObjects;
            _questCoinModel = new QuestCoinModel();
            _player = player;
        }
        
        
        public void Initialization()
        {
            _singleQuestController = new QuestController(_player, _questCoinModel, _singleQuestView);
            _singleQuestController.Reset();
            
            _questFactory.Add(QuestType.Coins, ()=>new QuestCoinModel());
            _questStoryFactory.Add(StoryType.Common, questCollection => new QuestStoryController(questCollection));

            _questStoryList = new List<IQuestStory>();

            foreach (QuestStoryConfig config in _questStoryConfigs)
            {
                _questStoryList.Add(CreateQuestStory(config));
                Debug.Log("Add Story");
            }
        }

        private IQuestStory CreateQuestStory(QuestStoryConfig config)
        {
            List<IQuest> quests = new List<IQuest>();
            foreach (QuestConfig item in config.questConfigs)
            {
                IQuest quest = CreateQuest(item);
                if(quest == null) continue;
                quests.Add(quest);
                Debug.Log("Add Quest");
            }

            return _questStoryFactory[config.type].Invoke(quests);
        }

        private IQuest CreateQuest(QuestConfig config)
        {
            int questID = config.id;
            QuestObjectView questView = _storyQuestsView.FirstOrDefault(value => value.id == config.id);

            if (questView == null)
            {
                Debug.Log("No View");
                return null;
            }

            if (_questFactory.TryGetValue(config.type, out var factory))
            {
                IQuestModel questModel = factory.Invoke();
                return new QuestController(_player, questModel, questView);
            }
            Debug.Log("No Model");
            return null;
        }
    }
}
