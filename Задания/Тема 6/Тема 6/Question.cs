using System;
using System.Collections.Generic;

namespace SPB_Quiz
{
    public class AnswerOption
    {
        public string Text { get; set; }
        public bool IsCorrect { get; set; }

        public override string ToString()
        {
            return Text; 
        }
    }

    public class QuestionModel
    {
        public string Text { get; set; }
        public string ImagePath { get; set; }
        public string Hint { get; set; }
        public List<AnswerOption> Answers { get; set; }

        public QuestionModel()
        {
            Answers = new List<AnswerOption>();
        }
    }
}