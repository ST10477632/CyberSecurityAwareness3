using Google.Apis.Admin.Directory.directory_v1.Data;
using MassTransit.Courier.Contracts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CyberSecurityAwareness3
{
    public partial class MainWindow : Window
    {
        // Part 3
        private string pendingTaskTitle = string.Empty;
        private string pendingTaskDesc = string.Empty;
        private bool awaitingReminder = false;
        private DatabaseHelper db = new DatabaseHelper();


        //Part 1 and 2
        private ArrayList reply = new ArrayList();
        private ArrayList ignore = new ArrayList();

        private string username = string.Empty;
        private string lastTopic = string.Empty;
        private string favouriteTopic = string.Empty;

        private QuizManager quiz = new QuizManager();
        private ActivityLog activityLog = new ActivityLog();

        public MainWindow()
        {
            InitializeComponent();
            new respond(reply, ignore);
            db.InitialiseDatabase();
            PlayVoiceGreeting();
        }

        // Part 1: Voice greeting
        private void PlayVoiceGreeting()
        {
            try
            {
                string wavPath = System.IO.Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory, "greeting.wav");
                if (File.Exists(wavPath))
                    new SoundPlayer(wavPath).Play();
            }
            catch { }
        }

        // ══════════════════════════════════════════════════════════════════════
        //  SEND

        private void send(object sender, RoutedEventArgs e)
        {
            string questions = question.Text.ToString().Trim();

            if (!string.IsNullOrEmpty(questions))
                AppendMessage(username, questions, Brushes.DodgerBlue, Brushes.White);

            if (string.IsNullOrEmpty(questions))
            {
                AppendMessage("CyberBot", "Please enter a message before sending.",
                              Brushes.LimeGreen, Brushes.OrangeRed);
                return;
            }

            string input = questions.ToLower();

            // Awaiting reminder
            if (awaitingReminder)
            {
                HandleReminderResponse(input, questions);
                question.Clear();
                return;
            }

            // Quiz active
            if (quiz.IsActive)
            {
                HandleQuizAnswer(input);
                question.Clear();
                return;
            }

            // Task 4: Activity log
            if (input.Contains("show activity log") || input.Contains("what have you done") ||
                input.Contains("show log") || input.Contains("activity log"))
            {
                AppendMessage("CyberBot", activityLog.GetRecentLog(),
                              Brushes.LimeGreen, Brushes.LimeGreen);
                activityLog.AddEntry("User viewed the activity log.");
                question.Clear();
                return;
            }

            // Task 2: Start quiz
            if (input.Contains("start quiz") || input.Contains("quiz") ||
                input.Contains("test me") || input.Contains("play quiz"))
            {
                StartQuiz();
                question.Clear();
                return;
            }

            // Task 2: Stop quiz
            if (input.Contains("stop quiz") || input.Contains("end quiz") ||
                input.Contains("quit quiz"))
            {
                quiz.StopQuiz();
                AppendMessage("CyberBot",
                    "Quiz stopped. Type 'start quiz' anytime to try again.",
                    Brushes.LimeGreen, Brushes.LimeGreen);
                activityLog.AddEntry("Quiz stopped by user.");
                question.Clear();
                return;
            }

            // Task 1: View tasks
            if (input.Contains("view task") || input.Contains("show task") ||
                input.Contains("list task") || input.Contains("my task"))
            {
                ViewTasks();
                question.Clear();
                return;
            }

            // Task 1 + Task 3 NLP: Add task
            if (input.Contains("add task") || input.Contains("create task") ||
                input.Contains("new task") || input.Contains("remind me to") ||
                input.Contains("set a reminder") || input.Contains("remember to"))
            {
                HandleAddTask(input, questions);
                question.Clear();
                return;
            }

            // Task 1: Complete task
            if (input.Contains("complete task") || input.Contains("done task") ||
                input.Contains("finish task") || input.Contains("mark task"))
            {
                HandleCompleteTask(input);
                question.Clear();
                return;
            }

            // Task 1: Delete task
            if (input.Contains("delete task") || input.Contains("remove task"))
            {
                HandleDeleteTask(input);
                question.Clear();
                return;
            }

            // Part 2: Capture name
            if (input.StartsWith("my name is ") || input.StartsWith("i am ") ||
                input.StartsWith("i'm "))
            {
                string[] parts = questions.Split(' ');
                string newName = parts[parts.Length - 1];
                username = newName;
                AppendMessage("CyberBot", $"Got it! I'll remember your name is {newName}.",
                              Brushes.LimeGreen, Brushes.LimeGreen);
                activityLog.AddEntry($"User name set to: {newName}");
                question.Clear();
                return;
            }

            // Part 2: Favourite topic
            if (input.Contains("interested in") || input.Contains("i like") ||
                input.Contains("favourite topic"))
            {
                string[] topicKeywords = { "password", "phishing", "scam", "privacy",
                                           "malware", "ransomware", "firewall", "vpn",
                                           "2fa", "update" };
                bool topicFound = false;
                foreach (string kw in topicKeywords)
                {
                    if (input.Contains(kw))
                    {
                        string favouriteTopic = kw;
                        File.AppendAllText("interested_topic.txt", username + ": " + kw + "\n");
                        AppendMessage("CyberBot",
                            $"Great! I'll remember that you're interested in {kw}. " +
                            "It's a crucial part of staying safe online.",
                            Brushes.LimeGreen, Brushes.LimeGreen);
                        activityLog.AddEntry($"User interest saved: {kw}");
                        topicFound = true;
                        break;
                    }
                }
                if (!topicFound)
                    AppendMessage("CyberBot",
                        "Please mention a cybersecurity topic (e.g. passwords, phishing, vpn).",
                        Brushes.LimeGreen, Brushes.OrangeRed);
                question.Clear();
                return;
            }

            // Part 2: Memory recall
            if (input.Contains("what do you remember") || input.Contains("know about me"))
            {
                string memory = $"Here is what I remember about you:\n• Name: {username}";
                string favouriteTopic = null;
                if (!string.IsNullOrEmpty(favouriteTopic))
                    memory += $"\n• Favourite topic: {favouriteTopic}";
                string lastTopic = null;
                if (!string.IsNullOrEmpty(lastTopic))
                    memory += $"\n• Last topic discussed: {lastTopic}";
                AppendMessage("CyberBot", memory, Brushes.LimeGreen, Brushes.LimeGreen);
                question.Clear();
                return;
            }

            // Part 2: Conversation flow
            if (input.Contains("tell me more") || input.Contains("another tip") ||
                input.Contains("explain more") || input.Contains("go on") ||
                input.Contains("continue"))
            {
                string lastTopic = null;
                if (!string.IsNullOrEmpty(lastTopic))
                {
                    string followUp = GetRandomAnswer(lastTopic);
                    if (!string.IsNullOrEmpty(followUp))
                    {
                        AppendMessage("CyberBot", followUp,
                                      Brushes.LimeGreen, Brushes.LimeGreen);
                        activityLog.AddEntry($"Follow-up tip given on: {lastTopic}");
                        question.Clear();
                        return;
                    }
                }
                AppendMessage("CyberBot",
                    "Could you remind me which topic? (e.g. password, phishing, vpn)",
                    Brushes.LimeGreen, Brushes.OrangeRed);
                question.Clear();
                return;
            }

            // Goodbye
            if (input.Contains("bye") || input.Contains("goodbye") || input.Contains("exit"))
            {
                string farewell = string.IsNullOrEmpty(username)
                    ? "Goodbye!" : $"Goodbye, {username}!";
                AppendMessage("CyberBot", farewell + " Stay safe online!",
                              Brushes.LimeGreen, Brushes.LimeGreen);
                activityLog.AddEntry("User ended the session.");
                question.Clear();
                return;
            }

            // Part 2: Sentiment + keyword matching
            string sentimentPrefix = DetectSentiment(input);
            string[] words = questions.Split(' ');
            bool found = false;
            string message = string.Empty;
            Random indexer = new Random();
            ArrayList per_word = new ArrayList();
            ArrayList answers_found = new ArrayList();

            foreach (string word in words)
            {
                if (!ignore.Contains(word.ToLower()))
                {
                    per_word.Clear();
                    found = false;

                    foreach (string answer in reply)
                    {
                        if (answer.Contains(word.ToLower()))
                        {
                            found = true;
                            per_word.Add(answer);
                        }
                    }

                    if (found && per_word.Count > 0)
                    {
                        int indexing = indexer.Next(0, per_word.Count);
                        string chosen = per_word[indexing].ToString();
                        if (!answers_found.Contains(chosen))
                            answers_found.Add(chosen);
                        string lastTopic = chosen.Split(' ')[0];
                    }
                }
            }

            if (answers_found.Count > 0)
            {
                if (!string.IsNullOrEmpty(sentimentPrefix))
                    message += sentimentPrefix + "\n";

                foreach (string per_answer in answers_found)
                    message += per_answer + "\n";

                string favouriteTopic = null;
                if (!string.IsNullOrEmpty(favouriteTopic) && !message.Contains(favouriteTopic))
                    message += $"\n(As someone interested in {favouriteTopic}, stay extra vigilant!)";

                AppendMessage("CyberBot", message.Trim(),
                              Brushes.LimeGreen, Brushes.LimeGreen);
                object lastTopic = null;
                activityLog.AddEntry($"NLP keyword response given. Topic: {lastTopic}");
                chats.ScrollIntoView(chats.Items[chats.Items.Count - 1]);
            }
            else
            {
                AppendMessage("CyberBot",
                    sentimentPrefix +
                    "I didn't quite understand that. Could you rephrase? " +
                    "Try: 'add task', 'start quiz', 'show activity log', or ask about " +
                    "passwords, phishing, scams, privacy, malware, vpn, 2fa, or firewall.",
                    Brushes.LimeGreen, Brushes.OrangeRed);
            }

            question.Clear();
        }

        // ══════════════════════════════════════════════════════════════════════
        //  TASK 1 — Task Assistant
        // ══════════════════════════════════════════════════════════════════════
        private void HandleAddTask(string input, string original)
        {
            string title = string.Empty;
            string desc = string.Empty;

            if (input.Contains("remind me to"))
            {
                int idx = input.IndexOf("remind me to") + "remind me to".Length;
                title = original.Substring(idx).Trim();
                desc = "Reminder: " + title;
            }
            else if (input.Contains("remember to"))
            {
                int idx = input.IndexOf("remember to") + "remember to".Length;
                title = original.Substring(idx).Trim();
                desc = "Task: " + title;
            }
            else if (input.Contains("add task"))
            {
                int idx = input.IndexOf("add task") + "add task".Length;
                title = original.Substring(idx).Trim();
                if (string.IsNullOrEmpty(title)) title = "New cybersecurity task";
                desc = "Task added: " + title;
            }
            else if (input.Contains("set a reminder"))
            {
                int idx = input.IndexOf("set a reminder") + "set a reminder".Length;
                title = original.Substring(idx).Trim();
                if (string.IsNullOrEmpty(title)) title = "Cybersecurity reminder";
                desc = "Reminder: " + title;
            }
            else
            {
                title = original;
                desc = "Cybersecurity task: " + original;
            }

            pendingTaskTitle = title;
            pendingTaskDesc = desc;
            awaitingReminder = true;

            AppendMessage("CyberBot",
                $"Task added with the description \"{desc}\".\n" +
                "Would you like a reminder? (e.g. 'Yes, remind me in 3 days' or 'No')",
                Brushes.LimeGreen, Brushes.LimeGreen);
        }

        private void HandleReminderResponse(string input, string original)
        {
            string reminder = string.Empty;

            if (input.Contains("no") && !input.Contains("yes"))
            {
                AppendMessage("CyberBot",
                    $"Got it! Task '{pendingTaskTitle}' saved with no reminder.",
                    Brushes.LimeGreen, Brushes.LimeGreen);
            }
            else
            {
                if (input.Contains("tomorrow"))
                    reminder = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
                else if (input.Contains("week"))
                    reminder = DateTime.Now.AddDays(7).ToString("yyyy-MM-dd");
                else if (input.Contains("day"))
                {
                    string[] parts = input.Split(' ');
                    foreach (string p in parts)
                    {
                        if (int.TryParse(p, out int days))
                        {
                            reminder = DateTime.Now.AddDays(days).ToString("yyyy-MM-dd");
                            break;
                        }
                    }
                    if (string.IsNullOrEmpty(reminder))
                        reminder = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
                }
                else
                    reminder = original.Trim();

                AppendMessage("CyberBot",
                    $"Got it! I'll remind you about '{pendingTaskTitle}' on {reminder}.",
                    Brushes.LimeGreen, Brushes.LimeGreen);
            }

            bool saved = db.AddTask(pendingTaskTitle, pendingTaskDesc, reminder);
            if (!saved)
                AppendMessage("CyberBot",
                    "Note: Could not save to database. Check your MySQL connection.",
                    Brushes.LimeGreen, Brushes.OrangeRed);

            activityLog.AddEntry($"Task added: '{pendingTaskTitle}'" +
                (string.IsNullOrEmpty(reminder) ? "" : $" (Reminder: {reminder})"));

            awaitingReminder = false;
            pendingTaskTitle = string.Empty;
            pendingTaskDesc = string.Empty;
        }

        private void ViewTasks()
        {
            ArrayList tasks = db.GetAllTasks();
            if (tasks.Count == 0)
            {
                AppendMessage("CyberBot",
                    "You have no tasks yet. Try: 'add task - Enable two-factor authentication'.",
                    Brushes.LimeGreen, Brushes.LimeGreen);
                return;
            }
            string output = "Here are your cybersecurity tasks:\n";
            foreach (TaskItem t in tasks)
                output += t.ToString() + "\n";
            output += "\nTo complete: 'complete task 1'  |  To delete: 'delete task 1'";
            AppendMessage("CyberBot", output, Brushes.LimeGreen, Brushes.LimeGreen);
            activityLog.AddEntry("User viewed all tasks.");
        }

        private void HandleCompleteTask(string input)
        {
            int id = ExtractNumber(input);
            if (id > 0)
            {
                if (db.CompleteTask(id))
                {
                    AppendMessage("CyberBot", $"Task #{id} marked as completed. Well done!",
                                  Brushes.LimeGreen, Brushes.LimeGreen);
                    activityLog.AddEntry($"Task #{id} marked as completed.");
                }
                else
                    AppendMessage("CyberBot",
                        $"Could not complete task #{id}. Check the task number.",
                        Brushes.LimeGreen, Brushes.OrangeRed);
            }
            else
                AppendMessage("CyberBot",
                    "Please specify a task number. E.g. 'complete task 2'",
                    Brushes.LimeGreen, Brushes.OrangeRed);
        }

        private void HandleDeleteTask(string input)
        {
            int id = ExtractNumber(input);
            if (id > 0)
            {
                if (db.DeleteTask(id))
                {
                    AppendMessage("CyberBot", $"Task #{id} has been deleted.",
                                  Brushes.LimeGreen, Brushes.LimeGreen);
                    activityLog.AddEntry($"Task #{id} deleted.");
                }
                else
                    AppendMessage("CyberBot",
                        $"Could not delete task #{id}. Check the task number.",
                        Brushes.LimeGreen, Brushes.OrangeRed);
            }
            else
                AppendMessage("CyberBot",
                    "Please specify a task number. E.g. 'delete task 2'",
                    Brushes.LimeGreen, Brushes.OrangeRed);
        }

        // ══════════════════════════════════════════════════════════════════════
        //  TASK 2 — Quiz
        // ══════════════════════════════════════════════════════════════════════
        private void StartQuiz()
        {
            quiz.StartQuiz();
            activityLog.AddEntry("Quiz started.");
            AppendMessage("CyberBot",
                "🎮 Welcome to the Cybersecurity Quiz!\n" +
                "For multiple choice type: A, B, C or D\n" +
                "For True/False type: True or False\n" +
                "Type 'stop quiz' anytime to exit.\n",
                Brushes.LimeGreen, Brushes.LimeGreen);
            ShowQuizQuestion();
        }

        private void ShowQuizQuestion()
        {
            QuizQuestion q = quiz.GetCurrentQuestion();
            if (q == null) return;

            string display = $"Question:\n{q.Question}\n";
            if (!q.IsTrueFalse && q.Options != null)
                foreach (string opt in q.Options)
                    display += opt + "\n";
            else
                display += "Answer: True or False";

            AppendMessage("CyberBot", display, Brushes.Gold, Brushes.Yellow);
        }

        private void HandleQuizAnswer(string input)
        {
            string answer = input.Trim().ToUpper();
            if (answer == "TRUE" || answer == "T") answer = "True";
            if (answer == "FALSE" || answer == "F") answer = "False";

            string feedback = quiz.SubmitAnswer(answer);
            if (string.IsNullOrEmpty(feedback))
            {
                AppendMessage("CyberBot",
                    "Please type A, B, C, D, True or False.",
                    Brushes.LimeGreen, Brushes.OrangeRed);
                return;
            }

            AppendMessage("CyberBot", feedback, Brushes.LimeGreen, Brushes.LimeGreen);

            if (quiz.IsFinished)
                activityLog.AddEntry($"Quiz completed. Score: {quiz.Score}/{quiz.TotalQuestions}");
            else if (quiz.IsActive)
                ShowQuizQuestion();
        }

        // ══════════════════════════════════════════════════════════════════════
        //  SHARED HELPERS
        // ══════════════════════════════════════════════════════════════════════
        private string DetectSentiment(string input)
        {
            string[] worriedWords = { "worried", "scared", "afraid", "anxious", "nervous", "panic" };
            string[] frustratedWords = { "frustrated", "annoyed", "angry", "upset", "overwhelmed" };
            string[] confusedWords = { "confused", "lost", "dont understand", "don't understand" };
            string[] curiousWords = { "curious", "wonder", "keen" };
            string[] happyWords = { "happy", "great", "awesome", "fantastic", "thanks" };

            foreach (string w in worriedWords)
                if (input.Contains(w))
                    return "It's completely understandable to feel that way. You're taking the right step. ";
            foreach (string w in frustratedWords)
                if (input.Contains(w))
                    return "I hear you — this can be tricky. Let me help you through it. ";
            foreach (string w in confusedWords)
                if (input.Contains(w))
                    return "No worries! Let me break it down simply for you. ";
            foreach (string w in curiousWords)
                if (input.Contains(w))
                    return "Great curiosity! Here's what you need to know: ";
            foreach (string w in happyWords)
                if (input.Contains(w))
                    return "Glad to hear it! Here's what you need to know: ";

            return string.Empty;
        }

        private string GetRandomAnswer(string keyword)
        {
            ArrayList matches = new ArrayList();
            foreach (string answer in reply)
                if (answer.StartsWith(keyword))
                    matches.Add(answer);
            if (matches.Count == 0) return string.Empty;
            return matches[new Random().Next(0, matches.Count)].ToString();
        }

        private int ExtractNumber(string input)
        {
            foreach (string p in input.Split(' '))
                if (int.TryParse(p, out int num))
                    return num;
            return -1;
        }


        private void submit_name(object sender, RoutedEventArgs e)
        {
            string filename = "user_names.txt";
            if (!File.Exists(filename))
                File.AppendAllText(filename, "auto_create\n");

            string name = user_name.Text.ToString().Trim();
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Please enter your name before continuing.");
                return;
            }



            bool found = check_name(name);
            username = name;

            if (!found)
            {
                File.AppendAllText(filename, name + "\n");
                AppendMessage("CyberBot",
                    $"Hey {name}, welcome to CyberBot!\n\n" +
                    "Here is what I can do:\n" +
                    "• Ask about passwords, phishing, scams, privacy, malware, vpn, 2fa\n" +
                    "• Type 'add task' to manage cybersecurity tasks\n" +
                    "• Type 'start quiz' to test your knowledge\n" +
                    "• Type 'show activity log' to see recent actions",
                    Brushes.LimeGreen, Brushes.LimeGreen);
                activityLog.AddEntry($"New user registered: {name}");
            }
            else
            {
                AppendMessage("CyberBot",
                    $"Welcome back, {name}! How can I help you stay safe online today?",
                    Brushes.LimeGreen, Brushes.LimeGreen);
                activityLog.AddEntry($"Returning user logged in: {name}");
            }

            name_grid.Visibility = Visibility.Hidden;
            chats_grid.Visibility = Visibility.Visible;
        }

        // ── Check name ─────────────────────────────────────────────────────────
        private bool check_name(string name)
        {
            string[] names = File.ReadAllLines("user_names.txt");
            foreach (string n in names)
                if (n.ToLower() == name.ToLower())
                    return true;
            return false;
        }

        private void AppendMessage(string name, string message,
                                   Brush nameBrush, Brush messageBrush)
        {
            chats.Items.Add(new TextBlock
            {
                TextWrapping = TextWrapping.Wrap,
                Margin = new Thickness(0, 4, 0, 4),
                Inlines =
                {
                    new Run { Text = name + " : ",  Foreground = nameBrush,
                              FontWeight = FontWeights.Bold },
                    new Run { Text = message,        Foreground = messageBrush }
                }
            });

            if (chats.Items.Count > 0)
                chats.ScrollIntoView(chats.Items[chats.Items.Count - 1]);
        }
    }
}
