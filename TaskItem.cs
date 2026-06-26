using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Data.SqlClient;
using System.Windows;

namespace CyberSecurityAwareness3
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Reminder { get; set; }
        public bool IsCompleted { get; set; }

        public override string ToString()
        {
            string status = IsCompleted ? "[DONE]" : "[PENDING]";
            string rem = string.IsNullOrEmpty(Reminder)
                                ? "No reminder"
                                : "Reminder: " + Reminder;
            return $"{status} #{Id} — {Title} | {rem}";
        }
    }

    // ── Database helper ────────────────────────────────────────────────────────
    public class DatabaseHelper
    {
        private string connectionString =
            "Server=localhost;Database=cyberbot_db;Uid=root;Pwd=;";

        public void InitialiseDatabase()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = @"
                        create table tasks (
                            id           INT AUTO_INCREMENT PRIMARY KEY,
                            title        VARCHAR(200) NOT NULL,
                            description  TEXT,
                            reminder     VARCHAR(200),
                            is_completed TINYINT(1)  DEFAULT 0,
                            created_at   DATETIME    DEFAULT CURRENT_TIMESTAMP
                        );";
                    new SqlCommand(sql, conn).ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("DB Error: " + ex.Message);
            }
        }

        public bool AddTask(string title, string description, string reminder)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = "INSERT INTO tasks (title, description, reminder) " +
                                 "VALUES (@title, @desc, @reminder)";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@title", title);
                    cmd.Parameters.AddWithValue("@desc", description);
                    cmd.Parameters.AddWithValue("@reminder", reminder);
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            catch { return false; }
        }

        public ArrayList GetAllTasks()
        {
            ArrayList tasks = new ArrayList();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = "SELECT id, title, description, reminder, is_completed FROM tasks";
                    MySqlDataReader reader = new MySqlCommand(sql, conn).ExecuteReader();
                    while (reader.Read())
                    {
                        tasks.Add(new TaskItem
                        {
                            Id = reader.GetInt32("id"),
                            Title = reader.GetString("title"),
                            Description = reader.GetString("description"),
                            Reminder = reader.IsDBNull(reader.GetOrdinal("reminder"))
                                              ? "" : reader.GetString("reminder"),
                            IsCompleted = reader.GetInt32("is_completed") == 1
                        });
                    }
                }
            }
            catch { }
            return tasks;
        }

        public bool CompleteTask(int id)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = "UPDATE tasks SET is_completed = 1 WHERE id = @id";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            catch { return false; }
        }

        public bool DeleteTask(int id)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = "DELETE FROM tasks WHERE id = @id";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            catch { return false; }
        }
    }

    // ── Quiz question model ────────────────────────────────────────────────────
    public class QuizQuestion
    {
        public string Question { get; set; }
        public string[] Options { get; set; }
        public string Answer { get; set; }
        public string Explanation { get; set; }
        public bool IsTrueFalse { get; set; }
    }

    // ── Quiz manager ───────────────────────────────────────────────────────────
    public class QuizManager
    {
        private ArrayList _questions = new ArrayList();
        private int _current = 0;
        public int Score { get; private set; } = 0;
        public bool IsActive { get; private set; } = false;
        public bool IsFinished => _current >= _questions.Count;
        public int TotalQuestions => _questions.Count;

        public QuizManager() { LoadQuestions(); }

        private void LoadQuestions()
        {
            _questions.Add(new QuizQuestion
            {
                Question = "What should you do if you receive an email asking for your password?",
                Options = new[] { "A) Reply with your password",
                                      "B) Delete the email",
                                      "C) Report the email as phishing",
                                      "D) Ignore it" },
                Answer = "C",
                Explanation = "Reporting phishing emails helps prevent scams and alerts your provider.",
                IsTrueFalse = false
            });

            _questions.Add(new QuizQuestion
            {
                Question = "Which of the following is the strongest password?",
                Options = new[] { "A) password123",
                                      "B) John1990",
                                      "C) qwerty",
                                      "D) #Tz!9mK@2vL" },
                Answer = "D",
                Explanation = "A strong password uses uppercase, lowercase, numbers and symbols.",
                IsTrueFalse = false
            });

            _questions.Add(new QuizQuestion
            {
                Question = "What does 2FA stand for?",
                Options = new[] { "A) Two-Factor Authentication",
                                      "B) Two-Firewall Access",
                                      "C) Twice-Fixed Algorithm",
                                      "D) Two-File Analyser" },
                Answer = "A",
                Explanation = "Two-Factor Authentication adds an extra verification step beyond just a password.",
                IsTrueFalse = false
            });

            _questions.Add(new QuizQuestion
            {
                Question = "What is phishing?",
                Options = new[] { "A) A type of antivirus software",
                                      "B) A way to speed up your internet",
                                      "C) A scam using fake messages to steal information",
                                      "D) A method to encrypt files" },
                Answer = "C",
                Explanation = "Phishing tricks users into revealing sensitive data through fake emails or websites.",
                IsTrueFalse = false
            });

            _questions.Add(new QuizQuestion
            {
                Question = "Which is safest to use on public Wi-Fi?",
                Options = new[] { "A) Your banking app without protection",
                                      "B) A VPN",
                                      "C) HTTP websites",
                                      "D) Sharing your hotspot" },
                Answer = "B",
                Explanation = "A VPN encrypts your traffic on public Wi-Fi, keeping your data safe.",
                IsTrueFalse = false
            });

            _questions.Add(new QuizQuestion
            {
                Question = "How often should you update your passwords?",
                Options = new[] { "A) Never — one password is enough",
                                      "B) Every few months or after a breach",
                                      "C) Only when you forget them",
                                      "D) Once a year" },
                Answer = "B",
                Explanation = "Regularly updating passwords reduces the risk of long-term unauthorised access.",
                IsTrueFalse = false
            });

            _questions.Add(new QuizQuestion
            {
                Question = "What does a firewall do?",
                Options = new[] { "A) Speeds up your internet connection",
                                      "B) Stores your passwords securely",
                                      "C) Monitors and controls network traffic based on security rules",
                                      "D) Removes viruses from your device" },
                Answer = "C",
                Explanation = "A firewall acts as a barrier between trusted and untrusted networks.",
                IsTrueFalse = false
            });

            _questions.Add(new QuizQuestion
            {
                Question = "What is ransomware?",
                Options = new[] { "A) Software that speeds up your PC",
                                      "B) Malware that locks files and demands payment",
                                      "C) A type of antivirus",
                                      "D) A network monitoring tool" },
                Answer = "B",
                Explanation = "Ransomware encrypts your files and demands payment — never pay the ransom.",
                IsTrueFalse = false
            });

            _questions.Add(new QuizQuestion
            {
                Question = "TRUE or FALSE: You should use the same password for all accounts.",
                Options = null,
                Answer = "False",
                Explanation = "Reusing passwords is dangerous — if one account is breached, all others are at risk.",
                IsTrueFalse = true
            });

            _questions.Add(new QuizQuestion
            {
                Question = "TRUE or FALSE: Clicking links from unknown senders is safe if your antivirus is on.",
                Options = null,
                Answer = "False",
                Explanation = "Antivirus cannot catch every threat. Never click links from unknown senders.",
                IsTrueFalse = true
            });

            _questions.Add(new QuizQuestion
            {
                Question = "TRUE or FALSE: A VPN can help protect your privacy on public Wi-Fi.",
                Options = null,
                Answer = "True",
                Explanation = "A VPN encrypts your internet traffic, making it harder for others to spy on you.",
                IsTrueFalse = true
            });

            _questions.Add(new QuizQuestion
            {
                Question = "TRUE or FALSE: Software updates should be avoided because they slow your device down.",
                Options = null,
                Answer = "False",
                Explanation = "Updates patch security vulnerabilities. Always keep your software up to date.",
                IsTrueFalse = true
            });
        }

        public void StartQuiz()
        {
            _current = 0;
            Score = 0;
            IsActive = true;
        }

        public void StopQuiz() { IsActive = false; }

        public QuizQuestion GetCurrentQuestion()
        {
            if (_current < _questions.Count)
                return (QuizQuestion)_questions[_current];
            return null;
        }

        public string SubmitAnswer(string userAnswer)
        {
            if (!IsActive) return string.Empty;

            QuizQuestion q = GetCurrentQuestion();
            if (q == null) return string.Empty;

            bool correct = userAnswer.Trim().ToLower() == q.Answer.ToLower();
            if (correct) Score++;

            string feedback = correct
                ? "✅ Correct! " + q.Explanation
                : $"❌ Wrong! The correct answer was: {q.Answer}. {q.Explanation}";

            _current++;

            if (_current >= _questions.Count)
            {
                IsActive = false;
                string grade = Score >= 10 ? "🏆 Great job! You're a cybersecurity pro!" :
                               Score >= 7 ? "👍 Good effort! Keep practising." :
                                             "📚 Keep learning to stay safe online!";
                feedback += $"\n\n--- Quiz Complete! ---\nYour score: {Score}/{TotalQuestions}\n{grade}";
            }

            return feedback;
        }
    }

    // ── Activity log ───────────────────────────────────────────────────────────
    public class ActivityLog
    {
        private ArrayList _log = new ArrayList();

        public void AddEntry(string description)
        {
            _log.Add($"[{DateTime.Now:HH:mm:ss}] {description}");
        }

        public string GetRecentLog()
        {
            if (_log.Count == 0)
                return "No actions have been recorded yet.";

            int start = _log.Count > 10 ? _log.Count - 10 : 0;
            string result = "Here's a summary of recent actions:\n";
            int number = 1;

            for (int i = start; i < _log.Count; i++)
            {
                result += $"{number}. {_log[i]}\n";
                number++;
            }
            return result;
        }

        public int Count => _log.Count;
    }

    // ── ASCII Art ──────────────────────────────────────────── ADD IT HERE ──
    internal class AsciiArt
    {
        public static string Get()
        {
            return @"
                    ==================================================
                       ____       _                 _   _             
                      / ___|  ___| |__   ___   ___ | |_(_) ___  _ __  
                      \___ \ / __| '_ \ / _ \ / _ \| __| |/ _ \| '_  \ 
                       ___) | (__| | | | (_) | (_) | |_| | (_) | | | |
                      |____/ \___|_| |_|\___/ \___/ \__|_|\___/|_| |_|
                        ****** CYBERSECURITY AWARENESS BOT ******
                    ==================================================";
        }
    }
}