using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfApp {
    public partial class Window1 : Window {
        
        public class Question {
            public string Text { get; set; }
            public List<string> Answers { get; set; }
            public int CorrectAnswerIndex { get; set; }
        }

        private List<Question> questions;
        private int currentQuestionIndex = 0;
        private int score = 0;
        private List<int> userAnswers = new List<int>();

        public Window1()
        {
            InitializeComponent();
            InitializeQuestions();
            LoadQuestion(currentQuestionIndex);
            UpdateButtons();
        }

        private void InitializeQuestions()
        {
            questions = new List<Question>
            {
                new Question
                {
                    Text = "1. В каком году началась Великая Отечественная война?",
                    Answers = new List<string> { "1939", "1941", "1943", "1945" },
                    CorrectAnswerIndex = 1
                },
                new Question
                {
                    Text = "2. Кто был первым русским царём?",
                    Answers = new List<string> { "Иван III", "Иван IV (Грозный)", "Пётр I", "Алексей Михайлович" },
                    CorrectAnswerIndex = 1
                },
                new Question
                {
                    Text = "3. В каком году произошла Куликовская битва?",
                    Answers = new List<string> { "1240", "1380", "1480", "1552" },
                    CorrectAnswerIndex = 1
                },
                new Question
                {
                    Text = "4. Какой город был столицей Древней Руси?",
                    Answers = new List<string> { "Москва", "Киев", "Новгород", "Владимир" },
                    CorrectAnswerIndex = 1
                },
                new Question
                {
                    Text = "5. Кто написал роман 'Война и мир'?",
                    Answers = new List<string> { "Ф. Достоевский", "А. Пушкин", "Л. Толстой", "Н. Гоголь" },
                    CorrectAnswerIndex = 2
                },
                new Question
                {
                    Text = "6. В каком году произошла Октябрьская революция?",
                    Answers = new List<string> { "1905", "1914", "1917", "1922" },
                    CorrectAnswerIndex = 2
                },
                new Question
                {
                    Text = "7. Кто был первым президентом России?",
                    Answers = new List<string> { "В. Ленин", "И. Сталин", "Б. Ельцин", "М. Горбачёв" },
                    CorrectAnswerIndex = 2
                },
                new Question
                {
                    Text = "8. Какой век называют 'Золотым веком' русской культуры?",
                    Answers = new List<string> { "XVII век", "XVIII век", "XIX век", "XX век" },
                    CorrectAnswerIndex = 2
                },
                new Question
                {
                    Text = "9. Кто победил в Ледовом побоище?",
                    Answers = new List<string> { "Шведы", "Немцы", "Русские", "Монголы" },
                    CorrectAnswerIndex = 2
                },
                new Question
                {
                    Text = "10. Как звали первого космонавта?",
                    Answers = new List<string> { "Юрий Гагарин", "Алексей Леонов", "Валентина Терешкова", "Сергей Королёв" },
                    CorrectAnswerIndex = 0
                },
                new Question
                {
                    Text = "11. Когда был основан Санкт-Петербург?",
                    Answers = new List<string> { "1682", "1703", "1721", "1755" },
                    CorrectAnswerIndex = 1
                },
                new Question
                {
                    Text = "12. Кто написал 'Слово о полку Игореве'?",
                    Answers = new List<string> { "Неизвестный автор", "Монах Нестор", "Князь Владимир", "Митрополит Иларион" },
                    CorrectAnswerIndex = 0
                },
                new Question
                {
                    Text = "13. Какой год считается началом распада СССР?",
                    Answers = new List<string> { "1985", "1989", "1991", "1993" },
                    CorrectAnswerIndex = 2
                },
                new Question
                {
                    Text = "14. Кто такой Александр Невский?",
                    Answers = new List<string> { "Князь", "Царь", "Император", "Полководец" },
                    CorrectAnswerIndex = 0
                },
                new Question
                {
                    Text = "15. В каком году закончилась Вторая мировая война?",
                    Answers = new List<string> { "1943", "1944", "1945", "1946" },
                    CorrectAnswerIndex = 2
                }
            };

            for (int i = 0; i < questions.Count; i++)
            {
                userAnswers.Add(-1); 
            }
        }

        private void LoadQuestion(int index)
        {
            if (index < 0 || index >= questions.Count) return;

            var question = questions[index];

            QuestionText.Text = question.Text;
            QuestionNumberText.Text = $"Вопрос {index + 1} из {questions.Count}";

            AnswersPanel.Children.Clear();

            for (int i = 0; i < question.Answers.Count; i++)
            {
                var radioButton = new RadioButton
                {
                    Content = question.Answers[i],
                    FontSize = 16,
                    Margin = new Thickness(0, 5, 0, 5),
                    Tag = i,
                    GroupName = "Answers"
                };

                if (userAnswers[index] == i)
                {
                    radioButton.IsChecked = true;
                }

                radioButton.Checked += RadioButton_Checked;
                AnswersPanel.Children.Add(radioButton);
            }

            UpdateButtons();
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is RadioButton radioButton)
            {
                userAnswers[currentQuestionIndex] = (int)radioButton.Tag;
            }
        }

        private void UpdateButtons()
        {
            PrevButton.IsEnabled = currentQuestionIndex > 0;
            NextButton.IsEnabled = currentQuestionIndex < questions.Count - 1;
        }

        private void PrevButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentQuestionIndex > 0)
            {
                currentQuestionIndex--;
                LoadQuestion(currentQuestionIndex);
            }
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentQuestionIndex < questions.Count - 1)
            {
                currentQuestionIndex++;
                LoadQuestion(currentQuestionIndex);
            }
        }

        private void FinishButton_Click(object sender, RoutedEventArgs e)
        {
            score = 0;
            for (int i = 0; i < questions.Count; i++)
            {
                if (userAnswers[i] == questions[i].CorrectAnswerIndex)
                {
                    score++;
                }
            }

            MessageBox.Show($"Правильных ответов: {score}", "Результат");
        }
    }

}