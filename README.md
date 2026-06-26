# CyberSecurityAwareness3

=============================================================
        CyberBot — Cybersecurity Awareness Chatbot
        PROG6221 — Portfolio of Evidence (POE)
        Part 1, Part 2 and Part 3
=============================================================

-------------------------------------------------------------
STUDENT INFORMATION
-------------------------------------------------------------
Name        : George
Subject     : PROG6221
Project     : Cybersecurity Awareness Chatbot
Parts       : Part 1, Part 2, Part 3

-------------------------------------------------------------
PROJECT DESCRIPTION
-------------------------------------------------------------
CyberBot is a WPF-based Cybersecurity Awareness Chatbot
built in C# using .NET 6. The chatbot educates users about
cybersecurity topics such as passwords, phishing, scams,
privacy, malware, ransomware, firewalls, VPNs and more.

The project was built across 3 parts:

Part 1 — Console-based chatbot with voice greeting and
         ASCII art display

Part 2 — WPF GUI chatbot with keyword recognition, random
         responses, conversation flow, memory and recall,
         sentiment detection and error handling

Part 3 — Enhanced WPF GUI with task assistant (MySQL),
         cybersecurity quiz, NLP simulation and
         activity log

-------------------------------------------------------------
FEATURES
-------------------------------------------------------------

PART 1 FEATURES
---------------
- Voice greeting played on startup (george.wav)
- ASCII art displayed on the login screen
- Basic chatbot responses in console

PART 2 FEATURES
---------------
- WPF Graphical User Interface
- Keyword recognition for cybersecurity topics:
  - password, phishing, scam, privacy, malware,
    ransomware, firewall, vpn, 2fa, update
- Random responses — 3 responses per keyword
  selected randomly using ArrayList and Random
- Conversation flow — follow-up phrases like
  "tell me more" or "another tip" continue the
  last topic without restarting
- Memory and recall:
  - Remembers user name
  - Remembers favourite cybersecurity topic
  - Saves interests to interested_topic.txt
  - Recalls stored info on request
- Sentiment detection:
  - Worried   → reassurance response
  - Curious   → enthusiastic response
  - Frustrated→ calm, helpful response
  - Confused  → simplified explanation
  - Happy     → positive acknowledgement
  - Sad       → supportive response
  - Angry     → calm resolution response
- Error handling — default fallback for unknown input
- User name stored in user_names.txt
- Returning users get a welcome back message

PART 3 FEATURES
---------------
- Task Assistant with MySQL database storage:
  - Add cybersecurity tasks with title and description
  - Set optional reminders with date
  - View all tasks
  - Mark tasks as completed
  - Delete tasks
- Cybersecurity Quiz Mini-Game:
  - 12 questions (8 multiple choice + 4 true/false)
  - Covers phishing, passwords, 2FA, VPN, malware,
    ransomware, firewalls, safe browsing
  - Immediate feedback after each answer
  - Final score with grade message
- NLP Simulation:
  - Recognises flexible user phrasing
  - Detects intent from phrases like
    "remind me to update my password" or
    "add a task to enable 2FA"
  - Minimised fallback responses
- Activity Log:
  - Tracks all chatbot actions with timestamps
  - Stores last 10 actions using ArrayList
  - View log by typing "show activity log"
  - Logs: tasks added, quiz started/completed,
    reminders set, user login, topics discussed

-------------------------------------------------------------
PROJECT FILES
-------------------------------------------------------------
CyberSecurityAwareness3/
├── MainWindow.xaml         → GUI layout (XAML)
├── MainWindow.xaml.cs      → All chatbot logic
├── TaskItem.cs             → TaskItem model,
│                             DatabaseHelper (MySQL),
│                             QuizManager,
│                             ActivityLog classes
├── respond.cs              → respond class with all
│                             keyword and sentiment
│                             responses using ArrayList
├── george.wav              → Voice greeting audio file
├── user_names.txt          → Stores registered usernames
│                             (auto-created on first run)
├── interested_topic.txt    → Stores user topic interests
│                             (auto-created on first run)
└── README.txt              → This file

-------------------------------------------------------------
DATABASE SETUP (MySQL)
-------------------------------------------------------------
1. Open MySQL Workbench
2. Run the following SQL:

   CREATE DATABASE cyberbot_db;

   USE cyberbot_db;

  CREATE TABLE tasks (
    id INT IDENTITY(1,1) PRIMARY KEY,
    title VARCHAR(200) NOT NULL,
    description VARCHAR(MAX),
    reminder VARCHAR(200),
    is_completed BIT DEFAULT 0,
    task_dueDate varchar(20)
   );

3. Open TaskItem.cs and update the connection string:

   @"Data source=(localdb)\demo_task;Database=cyberbot_db";

-------------------------------------------------------------
HOW TO RUN THE PROJECT
-------------------------------------------------------------
Requirements:
- Visual Studio 2022
- .NET 6.0 or .NET 8.0
- MySQL Server running locally
- MySql.Data NuGet package installed

Steps:
1. Open CyberSecurityAwareness3.sln in Visual Studio
2. Install NuGet package: MySql.Data
3. Run the SQL script above in MySQL Workbench
4. Make sure george.wav is in the project folder with:
   - Build Action       = Content
   - Copy to Output Dir = Copy if newer
5. Press F5 to run

-------------------------------------------------------------
HOW TO USE THE CHATBOT
-------------------------------------------------------------
COMMAND                        RESULT
------------------------------ ------------------------------
Enter name + click START       Welcome message displayed
tell me about passwords        Password safety tip
I am worried about scams       Empathetic response + tip
I'm interested in phishing     Saves topic to memory
what do you remember           Recalls stored user info
tell me more                   Continues last topic
add task - Enable 2FA          Adds task, asks for reminder
Yes, remind me in 3 days       Saves task with reminder date
remind me to update password   NLP detects and adds task
view tasks                     Shows all tasks from database
complete task 1                Marks task 1 as completed
delete task 1                  Deletes task 1 from database
start quiz                     Starts cybersecurity quiz
A / B / C / D                  Answers multiple choice question
True / False                   Answers true/false question
stop quiz                      Stops the quiz
show activity log              Shows last 10 bot actions
what have you done             Same as show activity log
bye / goodbye                  Ends the session

-------------------------------------------------------------
CYBERSECURITY TOPICS SUPPORTED
-------------------------------------------------------------
- Password safety
- Phishing awareness
- Scam detection
- Online privacy
- Malware protection
- Ransomware prevention
- Firewall explained
- VPN usage
- Two-factor authentication (2FA)
- Software updates

-------------------------------------------------------------
GITHUB SUBMISSION
-------------------------------------------------------------
- Minimum 6 commits with meaningful messages
- Minimum 3 releases tagged in the repository
- Repository includes all source code, README,
  george.wav and all necessary project files

-------------------------------------------------------------
NOTES
-------------------------------------------------------------
- user_names.txt is created automatically on first run
- interested_topic.txt is created automatically on first run
- The database table is created automatically on startup
  if it does not already exist
- The activity log resets when the application closes
  as it is stored in memory using ArrayList
- george.wav must be present for voice greeting to work

=============================================================
                    END OF README
=============================================================
