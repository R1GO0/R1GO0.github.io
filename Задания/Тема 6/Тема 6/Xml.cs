using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace SPB_Quiz
{
    public class XmlDataManager
    {
        private string filePath;

        public XmlDataManager(string fileName)
        {
            filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
            if (!File.Exists(filePath))
                CreateDefaultXml();
        }

        private void CreateDefaultXml()
        {
            XDocument doc = new XDocument(
                new XElement("Quiz", new XAttribute("title", "СПб Викторина"),
                    new XElement("Topic", new XAttribute("name", "Новая тема"),
                        new XElement("Level", new XAttribute("id", 1),
                            new XElement("Question", new XAttribute("text", "Пример"),
                                new XElement("Answer", "Ответ"))))));
            doc.Save(filePath);
        }

        public List<string> GetTopics()
        {
            XDocument doc = XDocument.Load(filePath);
            return doc.Root.Elements("Topic")
                         .Select(t => t.Attribute("name").Value)
                         .ToList();
        }

        public bool HasNextLevel(string topicName, int currentLevelId)
        {
            XDocument doc = XDocument.Load(filePath);
            var topic = doc.Root.Elements("Topic")
                              .FirstOrDefault(t => t.Attribute("name").Value == topicName);

            if (topic == null) return false;

            var nextLevel = topic.Elements("Level")
                                 .FirstOrDefault(l => (int)l.Attribute("id") == currentLevelId + 1);

            return nextLevel != null && nextLevel.Elements("Question").Any();
        }
        public List<QuestionModel> GetQuestions(string topicName, int levelId)
        {
            XDocument doc = XDocument.Load(filePath);
            var questions = new List<QuestionModel>();

            var topic = doc.Root.Elements("Topic")
                              .FirstOrDefault(t => t.Attribute("name").Value == topicName);

            if (topic == null) return questions;

            var level = topic.Elements("Level")
                             .FirstOrDefault(l => (int)l.Attribute("id") == levelId);

            if (level == null) return questions;

            foreach (var qElem in level.Elements("Question"))
            {
                var q = new QuestionModel
                {
                    Text = qElem.Attribute("text").Value,
                    ImagePath = qElem.Attribute("image")?.Value ?? "",
                    Hint = qElem.Attribute("hint")?.Value ?? "Нет подсказки"
                };

                foreach (var aElem in qElem.Elements("Answer"))
                {
                    q.Answers.Add(new AnswerOption
                    {
                        Text = aElem.Value,
                        IsCorrect = (aElem.Attribute("right")?.Value == "yes")
                    });
                }
                questions.Add(q);
            }
            return questions;
        }

        public void AddQuestion(string topic, int level, QuestionModel q)
        {
            XDocument doc = XDocument.Load(filePath);
            var topicElem = doc.Root.Elements("Topic")
                                  .FirstOrDefault(t => t.Attribute("name").Value == topic);

            if (topicElem == null)
            {
                topicElem = new XElement("Topic", new XAttribute("name", topic));
                doc.Root.Add(topicElem);
            }

            var levelElem = topicElem.Elements("Level")
                                     .FirstOrDefault(l => (int)l.Attribute("id") == level);

            if (levelElem == null)
            {
                levelElem = new XElement("Level", new XAttribute("id", level));
                topicElem.Add(levelElem);
            }

            XElement qElem = new XElement("Question",
                new XAttribute("text", q.Text),
                new XAttribute("image", q.ImagePath),
                new XAttribute("hint", q.Hint));

            foreach (var ans in q.Answers)
            {
                qElem.Add(new XElement("Answer",
                    new XAttribute("right", ans.IsCorrect ? "yes" : "no"),
                    ans.Text));
            }

            levelElem.Add(qElem);
            doc.Save(filePath);
        }
    }
}