using System.Collections;
using System.Net.NetworkInformation;

namespace CyberSecurityAwareness3
{
    public class respond
    {
        public respond(ArrayList reply, ArrayList ignore)
        {
            answers(reply);
            words(ignore);
        }

        private void words(ArrayList ignoring)
        {
            ignoring.Add("a"); ignoring.Add("about"); ignoring.Add("above");
            ignoring.Add("across"); ignoring.Add("after"); ignoring.Add("afterwards");
            ignoring.Add("again"); ignoring.Add("against"); ignoring.Add("all");
            ignoring.Add("almost"); ignoring.Add("alone"); ignoring.Add("along");
            ignoring.Add("already"); ignoring.Add("also"); ignoring.Add("although");
            ignoring.Add("always"); ignoring.Add("am"); ignoring.Add("among");
            ignoring.Add("amongst"); ignoring.Add("amount"); ignoring.Add("an");
            ignoring.Add("and"); ignoring.Add("another"); ignoring.Add("any");
            ignoring.Add("anyhow"); ignoring.Add("anyone"); ignoring.Add("anything");
            ignoring.Add("anyway"); ignoring.Add("anywhere"); ignoring.Add("are");
            ignoring.Add("around"); ignoring.Add("as"); ignoring.Add("at");
            ignoring.Add("back"); ignoring.Add("be"); ignoring.Add("became");
            ignoring.Add("because"); ignoring.Add("become"); ignoring.Add("becomes");
            ignoring.Add("becoming"); ignoring.Add("been"); ignoring.Add("before");
            ignoring.Add("beforehand"); ignoring.Add("behind"); ignoring.Add("being");
            ignoring.Add("below"); ignoring.Add("beside"); ignoring.Add("besides");
            ignoring.Add("between"); ignoring.Add("beyond"); ignoring.Add("both");
            ignoring.Add("but"); ignoring.Add("by"); ignoring.Add("can");
            ignoring.Add("cannot"); ignoring.Add("could"); ignoring.Add("did");
            ignoring.Add("do"); ignoring.Add("does"); ignoring.Add("doing");
            ignoring.Add("done"); ignoring.Add("down"); ignoring.Add("during");
            ignoring.Add("each"); ignoring.Add("either"); ignoring.Add("else");
            ignoring.Add("elsewhere"); ignoring.Add("enough"); ignoring.Add("etc");
            ignoring.Add("even"); ignoring.Add("ever"); ignoring.Add("every");
            ignoring.Add("everyone"); ignoring.Add("everything"); ignoring.Add("everywhere");
            ignoring.Add("except"); ignoring.Add("few"); ignoring.Add("first");
            ignoring.Add("for"); ignoring.Add("former"); ignoring.Add("formerly");
            ignoring.Add("from"); ignoring.Add("further"); ignoring.Add("had");
            ignoring.Add("has"); ignoring.Add("have"); ignoring.Add("having");
            ignoring.Add("he"); ignoring.Add("hence"); ignoring.Add("her");
            ignoring.Add("here"); ignoring.Add("hereafter"); ignoring.Add("hereby");
            ignoring.Add("herein"); ignoring.Add("hereupon"); ignoring.Add("hers");
            ignoring.Add("herself"); ignoring.Add("him"); ignoring.Add("himself");
            ignoring.Add("his"); ignoring.Add("how"); ignoring.Add("however");
            ignoring.Add("i"); ignoring.Add("if"); ignoring.Add("in");
            ignoring.Add("indeed"); ignoring.Add("inside"); ignoring.Add("instead");
            ignoring.Add("into"); ignoring.Add("is"); ignoring.Add("it");
            ignoring.Add("its"); ignoring.Add("itself"); ignoring.Add("last");
            ignoring.Add("later"); ignoring.Add("latter"); ignoring.Add("latterly");
            ignoring.Add("least"); ignoring.Add("less"); ignoring.Add("lot");
            ignoring.Add("many"); ignoring.Add("may"); ignoring.Add("me");
            ignoring.Add("meanwhile"); ignoring.Add("might"); ignoring.Add("moreover");
            ignoring.Add("most"); ignoring.Add("mostly"); ignoring.Add("much");
            ignoring.Add("must"); ignoring.Add("my"); ignoring.Add("myself");
            ignoring.Add("namely"); ignoring.Add("neither"); ignoring.Add("never");
            ignoring.Add("nevertheless"); ignoring.Add("next"); ignoring.Add("no");
            ignoring.Add("nobody"); ignoring.Add("none"); ignoring.Add("noone");
            ignoring.Add("nor"); ignoring.Add("not"); ignoring.Add("nothing");
            ignoring.Add("now"); ignoring.Add("nowhere"); ignoring.Add("of");
            ignoring.Add("off"); ignoring.Add("often"); ignoring.Add("on");
            ignoring.Add("once"); ignoring.Add("one"); ignoring.Add("only");
            ignoring.Add("or"); ignoring.Add("other"); ignoring.Add("others");
            ignoring.Add("otherwise"); ignoring.Add("ought"); ignoring.Add("our");
            ignoring.Add("ours"); ignoring.Add("ourselves"); ignoring.Add("out");
            ignoring.Add("outside"); ignoring.Add("over"); ignoring.Add("own");
            ignoring.Add("part"); ignoring.Add("per"); ignoring.Add("perhaps");
            ignoring.Add("please"); ignoring.Add("put"); ignoring.Add("rather");
            ignoring.Add("re"); ignoring.Add("same"); ignoring.Add("see");
            ignoring.Add("seem"); ignoring.Add("seemed"); ignoring.Add("seeming");
            ignoring.Add("seems"); ignoring.Add("several"); ignoring.Add("she");
            ignoring.Add("should"); ignoring.Add("show"); ignoring.Add("side");
            ignoring.Add("since"); ignoring.Add("so"); ignoring.Add("some");
            ignoring.Add("somehow"); ignoring.Add("someone"); ignoring.Add("something");
            ignoring.Add("sometime"); ignoring.Add("sometimes"); ignoring.Add("somewhere");
            ignoring.Add("still"); ignoring.Add("such"); ignoring.Add("take");
            ignoring.Add("than"); ignoring.Add("that"); ignoring.Add("the");
            ignoring.Add("their"); ignoring.Add("theirs"); ignoring.Add("them");
            ignoring.Add("themselves"); ignoring.Add("then"); ignoring.Add("thence");
            ignoring.Add("there"); ignoring.Add("thereafter"); ignoring.Add("thereby");
            ignoring.Add("therefore"); ignoring.Add("therein"); ignoring.Add("thereupon");
            ignoring.Add("these"); ignoring.Add("they"); ignoring.Add("this");
            ignoring.Add("those"); ignoring.Add("though"); ignoring.Add("through");
            ignoring.Add("throughout"); ignoring.Add("thru"); ignoring.Add("thus");
            ignoring.Add("to"); ignoring.Add("together"); ignoring.Add("too");
            ignoring.Add("toward"); ignoring.Add("towards"); ignoring.Add("under");
            ignoring.Add("unless"); ignoring.Add("until"); ignoring.Add("up");
            ignoring.Add("upon"); ignoring.Add("us"); ignoring.Add("used");
            ignoring.Add("very"); ignoring.Add("via"); ignoring.Add("was");
            ignoring.Add("we"); ignoring.Add("well"); ignoring.Add("were");
            ignoring.Add("what"); ignoring.Add("whatever"); ignoring.Add("when");
            ignoring.Add("whence"); ignoring.Add("whenever"); ignoring.Add("where");
            ignoring.Add("whereafter"); ignoring.Add("whereas"); ignoring.Add("whereby");
            ignoring.Add("wherein"); ignoring.Add("whereupon"); ignoring.Add("wherever");
            ignoring.Add("whether"); ignoring.Add("which"); ignoring.Add("while");
            ignoring.Add("whither"); ignoring.Add("who"); ignoring.Add("whoever");
            ignoring.Add("whole"); ignoring.Add("whom"); ignoring.Add("whose");
            ignoring.Add("why"); ignoring.Add("will"); ignoring.Add("with");
            ignoring.Add("within"); ignoring.Add("without"); ignoring.Add("would");
            ignoring.Add("yes"); ignoring.Add("yet"); ignoring.Add("you");
            ignoring.Add("your"); ignoring.Add("yours"); ignoring.Add("yourself");
            ignoring.Add("yourselves");
        }

        public void answers(ArrayList add_answers)
        {
            // Greetings
            add_answers.Add("hello i'm doing well, thanks for asking! how are you doing today?");
            add_answers.Add("hello great to hear from you! how can i help you today?");
            add_answers.Add("hello welcome! i'm cyberbot, your cybersecurity assistant.");
            add_answers.Add("hi hey there! what cybersecurity topic can i help you with?");
            add_answers.Add("hi glad you're here! ask me anything about staying safe online.");
            add_answers.Add("hey hi! what would you like to know about cybersecurity?");
            add_answers.Add("greeting i'm doing well, thanks for asking! how are you doing today?");
            add_answers.Add("greeting i'm great today! how can i help you?");
            add_answers.Add("greeting doing good! hope you are also doing well today?");

            // Purpose
            add_answers.Add("purpose my purpose is to educate you on how to stay safe online.");
            add_answers.Add("purpose i help users understand online safety and digital protection.");
            add_answers.Add("purpose i assist with cybersecurity awareness and safety guidance.");

            // Password
            add_answers.Add("password make sure to use strong, unique passwords for each account. avoid using personal details in your passwords.");
            add_answers.Add("password a good password is at least 12 characters long and includes uppercase letters, numbers, and symbols.");
            add_answers.Add("password never reuse the same password across multiple accounts. consider using a password manager.");

            // Scam
            add_answers.Add("scam be cautious of emails asking for personal information. scammers often disguise themselves as trusted organisations.");
            add_answers.Add("scam if an offer sounds too good to be true, it almost certainly is. always verify before responding.");
            add_answers.Add("scam scammers often create urgency to pressure you into acting quickly. take your time and verify.");

            // Privacy
            add_answers.Add("privacy review your social media privacy settings regularly to control who can see your information.");
            add_answers.Add("privacy be careful about what personal information you share online — once it is out there, it is hard to take back.");
            add_answers.Add("privacy use privacy-focused browsers like firefox or brave and avoid sharing details on unsecured websites.");

            // Phishing
            add_answers.Add("phishing phishing is when attackers send fake emails pretending to be a trusted source to steal your information.");
            add_answers.Add("phishing always check the sender's actual email address carefully — small spelling differences reveal fake emails.");
            add_answers.Add("phishing hover over links before clicking to preview the real destination url.");

            // Malware
            add_answers.Add("malware malware is malicious software designed to damage or gain unauthorised access to your device.");
            add_answers.Add("malware keep your antivirus software up to date and run regular scans to detect malware early.");
            add_answers.Add("malware only download software from official, trusted sources. pirated software often comes bundled with malware.");

            // Ransomware
            add_answers.Add("ransomware ransomware is a type of malware that locks your files and demands payment to restore them.");
            add_answers.Add("ransomware back up your important files regularly to an offline drive or trusted cloud service.");
            add_answers.Add("ransomware never pay the ransom — there is no guarantee your files will be restored.");

            // Firewall
            add_answers.Add("firewall a firewall monitors and controls incoming and outgoing network traffic based on security rules.");
            add_answers.Add("firewall always keep your firewall enabled — it acts as the first line of defence between your device and the internet.");
            add_answers.Add("firewall both hardware and software firewalls work together to provide the best protection.");

            // VPN
            add_answers.Add("vpn a vpn encrypts your internet traffic, making it much harder for others to spy on your online activity.");
            add_answers.Add("vpn always use a vpn when connecting to public wi-fi in places like cafes, airports, or hotels.");
            add_answers.Add("vpn choose a reputable, paid vpn service — free vpns often collect and sell your browsing data.");

            // Hacked
            add_answers.Add("hacked immediately change your password and log out of all devices if you think your account has been hacked.");
            add_answers.Add("hacked contact the platform's support team right away if your account has been compromised.");
            add_answers.Add("hacked enable two-factor authentication on all your accounts to prevent future unauthorised access.");

            // Fraud
            add_answers.Add("fraud contact your bank immediately if you notice any suspicious or unauthorised transactions.");
            add_answers.Add("fraud report suspected financial fraud to your bank and the relevant authorities as soon as possible.");
            add_answers.Add("fraud monitor your bank accounts and credit reports regularly to catch fraud early.");

            // 2FA
            add_answers.Add("2fa two-factor authentication adds an extra layer of security to your accounts.");
            add_answers.Add("2fa even if your password is stolen, 2fa can stop attackers from logging in.");
            add_answers.Add("2fa use an authenticator app instead of sms for stronger two-factor authentication.");

            // Update
            add_answers.Add("update software updates patch security vulnerabilities — always keep your software updated.");
            add_answers.Add("update enable automatic updates to ensure your device is always protected.");
            add_answers.Add("update outdated software is one of the most common ways attackers gain access to systems.");

            // Cybersecurity
            add_answers.Add("cybersecurity cybersecurity is about protecting systems and networks from digital threats.");
            add_answers.Add("cybersecurity it involves protecting devices and online accounts from attacks.");
            add_answers.Add("cybersecurity it focuses on securing digital information and systems.");

            // Sentiment: Worried
            add_answers.Add("worried it's completely understandable to feel worried. i'm here to help you stay safe.");
            add_answers.Add("worried don't panic, most cybersecurity issues can be resolved quickly.");
            add_answers.Add("worried i understand your concern. let's make sure your information is safe.");

            // Sentiment: Curious
            add_answers.Add("curious that's great! curiosity is the first step to staying safe online.");
            add_answers.Add("curious i love that you're keen to learn! ask me anything about cybersecurity.");
            add_answers.Add("curious great question! the more you know, the better protected you are online.");

            // Sentiment: Frustrated
            add_answers.Add("frustrated i understand you're frustrated. let's work through the issue step by step.");
            add_answers.Add("frustrated it's okay to feel frustrated when things aren't working. i'm here to help.");
            add_answers.Add("frustrated take a breath — we'll fix this together. what seems to be the problem?");

            // Sentiment: Confused
            add_answers.Add("confused that's okay, confusion is completely normal. i'll explain it clearly.");
            add_answers.Add("confused let me break it down step by step so it makes more sense.");
            add_answers.Add("confused no worries at all — i'm here to help you understand it better.");

            // Sentiment: Happy
            add_answers.Add("happy that's great to hear! i'm glad things are going well for you.");
            add_answers.Add("happy awesome! positivity is always good. let me know if you need anything.");
            add_answers.Add("happy i'm happy for you! feel free to ask me anything.");

            // Sentiment: Sad
            add_answers.Add("sad i'm sorry you're feeling this way. i'm here for you if you need help.");
            add_answers.Add("sad that sounds tough. take things one step at a time.");
            add_answers.Add("sad i hope things improve soon. you can talk to me anytime.");

            // Sentiment: Angry
            add_answers.Add("angry i understand you're angry. let's try to solve the issue together calmly.");
            add_answers.Add("angry it's okay to feel angry, but i'm here to help you fix the problem.");
            add_answers.Add("angry take your time — i'm not going anywhere. let's sort this out together.");

            // Follow-up triggers
            add_answers.Add("tell always be cautious when sharing personal information online.");
            add_answers.Add("give use two-factor authentication wherever possible for extra protection.");
            add_answers.Add("another keep all your devices and apps updated to protect against the latest threats.");
            add_answers.Add("explain cybersecurity is the practice of protecting your devices and data from digital attacks.");

            // Interested
            add_answers.Add("interested great! i will remember that topic for you. it is an important part of staying safe online.");
        }
    }
}