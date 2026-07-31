using System;
using System.Collections.Generic;
using System.IO;
using Easy_mode;

namespace Arbot__V_Console___V_FileData_
{
    public class Program
    {
        static void Main(string[] args)
        {
            string settings_path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "Info", "Settings.txt");
            Settings settings = new Settings(File.ReadAllText(settings_path).Split(":"));
            Random rand = new Random();
            G.write("Welcome to Arbot!");
            if(!settings.Fast_load)
            {
                Console.CursorVisible = false;
                for(int i = 0; i <= 100; i++)
                {
                    switch(i % 4)
                    {
                        case 0:
                        G.write("Loading...");
                        break;

                        case 1:
                        G.write("Loading..");
                        break;

                        case 2:
                        G.write("Loading.");
                        break;

                        case 3:
                        G.write("Loading");
                        break;
                    }
                    G.write($"{i}%");
                    if(i != 100)
                    {
                        Thread.Sleep(rand.Next(30, 150));
                        Console.SetCursorPosition(0, Console.CursorTop - 2);
                        G.write(new string(' ', Console.WindowWidth), false);
                        Console.SetCursorPosition(0, Console.CursorTop);
                    }
                }
                Console.CursorVisible = true;
                G.write("\n--Finished--");
            }
            G.write("Press any key to continue");
            Console.ReadKey();
            int atempt_num = 3;
            start:
            Console.Clear();

            string password_path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "Info", "Password.txt");
            string name_path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "Info", "Name.txt");
            string positives_path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "Info", "Positives.txt");
            string negatives_path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "Info", "Negatives.txt");
            string form_path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "Lessons", "Form.txt");
            string monday_path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "Lessons", "Monday.txt");
            string tuesday_path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "Lessons", "Tuesday.txt");
            string wednesday_path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "Lessons", "Wednesday.txt");
            string thursday_path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "Lessons", "Thursday.txt");
            string friday_path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "Lessons", "Friday.txt");
            G.write("Please enter your password:");
            string pass = Specielized.Encript_input("*");
            if(pass == File.ReadAllText(password_path))
            {
                home:
                Info info = new Info(File.ReadAllText(name_path), pass, int.Parse(File.ReadAllText(positives_path)), int.Parse(File.ReadAllText(negatives_path)));
                Timetable timetable = new Timetable(File.ReadAllText(form_path), File.ReadAllText(monday_path).Split(":"), File.ReadAllText(tuesday_path).Split(":"), File.ReadAllText(wednesday_path).Split(":"), File.ReadAllText(thursday_path).Split(":"), File.ReadAllText(friday_path).Split(":"));
                Console.Clear();
                if(settings.Name_or_master)
                {
                    G.write($"Welcome {info.Name},");
                } else {
                    G.write("Welcome Master");
                }

                List<string> lessons = new List<string>();
                DateTime now = DateTime.Now;
                TimeSpan current_time_obj = now.TimeOfDay;
                DateTime today_date_obj = now.Date;
                DayOfWeek today_weekday = today_date_obj.DayOfWeek;
                string period;
                int lesson_num = 0;
                string next_bell;
                TimeSpan calculation;
                TimeSpan cal2;

                if (current_time_obj > TimeSpan.Parse("08:40:00") && current_time_obj < TimeSpan.Parse("09:10:00"))
                {
                    period = "Formtime";
                    lesson_num = 0;
                    next_bell = "09:10:00";
                }
                else if (current_time_obj > TimeSpan.Parse("09:10:00") && current_time_obj < TimeSpan.Parse("10:00:00"))
                {
                    period = "1";
                    lesson_num = 1;
                    next_bell = "10:00:00";
                }
                else if (current_time_obj > TimeSpan.Parse("10:00:00") && current_time_obj < TimeSpan.Parse("10:50:00"))
                {
                    period = "2";
                    lesson_num = 2;
                    next_bell = "10:50:00";
                }
                else if (current_time_obj > TimeSpan.Parse("10:50:00") && current_time_obj < TimeSpan.Parse("11:10:00"))
                {
                    period = "Breaktime";
                    lesson_num = 3;
                    next_bell = "11:10:00";
                }
                else if (current_time_obj > TimeSpan.Parse("11:10:00") && current_time_obj < TimeSpan.Parse("12:00:00"))
                {
                    period = "3";
                    lesson_num = 4;
                    next_bell = "12:00:00";
                }
                else if (current_time_obj > TimeSpan.Parse("12:00:00") && current_time_obj < TimeSpan.Parse("12:50:00"))
                {
                    period = "4";
                    lesson_num = 5;
                    next_bell = "12:50:00";
                }
                else if (current_time_obj > TimeSpan.Parse("12:50:00") && current_time_obj < TimeSpan.Parse("13:30:00"))
                {
                    period = "Lunchtime";
                    lesson_num = 6;
                    next_bell = "13:30:00";
                }
                else if (current_time_obj > TimeSpan.Parse("13:30:00") && current_time_obj < TimeSpan.Parse("14:20:00"))
                {
                    period = "5";
                    lesson_num = 7;
                    next_bell = "14:20:00";
                }
                else if (current_time_obj > TimeSpan.Parse("14:20:00") && current_time_obj < TimeSpan.Parse("15:10:00"))
                {
                    period = "6";
                    lesson_num = 8;
                    next_bell = "15:10:00";
                }
                else
                {
                    period = "Hometime";
                    next_bell = "08:40:00";
                }

                switch (today_weekday)
                {
                    case DayOfWeek.Monday:
                        lessons = new List<string> { timetable.Form, timetable.Mon_p1, timetable.Mon_p2, "Breaktime", timetable.Mon_p3, timetable.Mon_p4, timetable.Mon_lunch, timetable.Mon_p5, timetable.Mon_p6, timetable.Mon_home };
                        break;

                    case DayOfWeek.Tuesday:
                        lessons = new List<string> { timetable.Form, timetable.Tue_p1, timetable.Tue_p2, "Breaktime", timetable.Tue_p3, timetable.Tue_p4, timetable.Tue_lunch, timetable.Tue_p5, timetable.Tue_p6, timetable.Tue_home };
                        break;

                    case DayOfWeek.Wednesday:
                        lessons = new List<string> { timetable.Form, timetable.Wed_p1, timetable.Wed_p2, "Breaktime", timetable.Wed_p3, timetable.Wed_p4, timetable.Wed_lunch, timetable.Wed_p5, timetable.Wed_p6, timetable.Wed_home };
                        break;

                    case DayOfWeek.Thursday:
                        lessons = new List<string> { timetable.Form, timetable.Thu_p1, timetable.Thu_p2, "Breaktime", timetable.Thu_p3, timetable.Thu_p4, timetable.Thu_lunch, timetable.Thu_p5, timetable.Thu_p6, timetable.Thu_home };
                        break;

                    case DayOfWeek.Friday:
                        lessons = new List<string> { timetable.Form, timetable.Fri_p1, timetable.Fri_p2, "Breaktime", timetable.Fri_p3, timetable.Fri_p4, timetable.Fri_lunch, timetable.Fri_p5, timetable.Fri_p6, timetable.Fri_home };
                        break;

                    case DayOfWeek.Saturday:
                        lessons = new List<string> { " ", " ", " ", " ", " ", " ", " ", " ", " ", " " };
                        break;

                    case DayOfWeek.Sunday:
                        lessons = new List<string> { " ", " ", " ", " ", " ", " ", " ", " ", " ", " " };
                        break;
                }
            
                string current_lesson = lessons[lesson_num];
                string next_lesson = lessons[lesson_num + 1];

                calculation = TimeSpan.Parse(next_bell).Subtract(current_time_obj);
                cal2 = calculation + calculation;

                if (today_weekday != DayOfWeek.Saturday && today_weekday != DayOfWeek.Sunday)
                {
                    if (cal2 >= TimeSpan.Parse("00:00:00"))
                    {
                        Console.WriteLine($"The day is {today_weekday} and we are in the {period}^th period of the day and the current lesson is {current_lesson} and the next lesson is {next_lesson}. The next bell is at {next_bell} and it is in {calculation} hours, minutes and seconds respectively.");
                    }
                }

                if (cal2 <= TimeSpan.Parse("00:00:00") || today_weekday == DayOfWeek.Saturday || today_weekday == DayOfWeek.Sunday)
                {
                    Console.WriteLine($"The day is {today_weekday} and there are no lessons on.");
                }

                Console.WriteLine("");
                Console.WriteLine($"You have: {info.Positives} positive(s),");
                Console.WriteLine($"You have: {info.Negatives} negative(s),");

                enter_command:
                G.write("\nWould you like to enter a command? Y/N (not case sensitive)");
                string command = G.read().ToUpper();
                if(command == "Y")
                {
                    command_choice:
                    Console.Clear();
                    G.write("Enter the command number:");
                    G.write("1)    Add positives/credits,");
                    G.write("2)    Add negatives,");
                    G.write("3)    Change lesson timetable.");
                    G.write("4)    Settings");
                    string choice = G.read();
                    switch(choice)
                    {
                        case "1":
                        start_command1:
                        Console.Clear();
                        G.write("How many positives/credits have you gotten? :)");
                        string middle = Console.ReadLine();
                        if(middle != "1" && middle != "2" && middle != "3")
                        {
                            G.write("That is not a number.");
                            Thread.Sleep(500);
                            Console.ReadKey();
                            goto start_command1;
                        }
                        int many = int.Parse(middle);
                        File.WriteAllText(positives_path, (info.Positives + many).ToString());
                        info.Positives = int.Parse(File.ReadAllText(positives_path));
                        G.write("Done");
                        another:
                        G.write("Would you like to enter another command? Y/N (not case sensitive)");
                        string choice_back = Console.ReadLine().ToUpper();
                        if(choice_back == "Y")
                        {
                            goto command_choice;
                        } else if(choice_back != "N") {
                            G.write("Invaild");
                            Thread.Sleep(500);
                            Console.ReadKey();
                            Console.SetCursorPosition(0, Console.CursorTop - 3);
                            G.write(new string(' ', Console.WindowWidth), false);
                            Console.SetCursorPosition(0, Console.CursorTop);
                            goto another;
                        }
                        break;

                        case "2":
                        start_command2:
                        Console.Clear();
                        G.write("How many negatives have you gotten? :(");
                        middle = Console.ReadLine();
                        if(middle != "1" && middle != "2" && middle != "3")
                        {
                            G.write("That is not a number.");
                            Thread.Sleep(500);
                            Console.ReadKey();
                            goto start_command2;
                        }
                        many = int.Parse(middle);
                        File.WriteAllText(positives_path, (info.Positives + many).ToString());
                        info.Positives = int.Parse(File.ReadAllText(positives_path));
                        G.write("Done");
                        another2:
                        G.write("Would you like to enter another command? Y/N (not case sensitive)");
                        choice_back = Console.ReadLine().ToUpper();
                        if(choice_back == "Y")
                        {
                            goto command_choice;
                        } else if(choice_back != "N") {
                            G.write("Invaild");
                            Thread.Sleep(500);
                            Console.ReadKey();
                            Console.SetCursorPosition(0, Console.CursorTop - 3);
                            G.write(new string(' ', Console.WindowWidth), false);
                            Console.SetCursorPosition(0, Console.CursorTop);
                            goto another2;
                        }
                        break;

                        case "3":
                        Console.Clear();
                        break;

                        case "4":
                        start_command4:
                        Console.Clear();
                        G.write("Welcome to settings!");
                        G.write("What would you like to do?");
                        if(settings.Fast_load)
                        {
                            G.write("1)    Fast loading: ");
                            Console.BackgroundColor = ConsoleColor.White;
                            Console.ForegroundColor = ConsoleColor.Black;
                            G.write("|ON []|", false);
                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.ForegroundColor = ConsoleColor.White;
                        } else {
                            G.write("1)    Fast loading: |[] OFF|");
                        }
                        if(settings.Name_or_master)
                        {
                            G.write("2)    Be called by your name, not 'Master': ");
                            Console.BackgroundColor = ConsoleColor.White;
                            Console.ForegroundColor = ConsoleColor.Black;
                            G.write("|ON []|", false);
                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.ForegroundColor = ConsoleColor.White;
                        } else
                        {
                            G.write("2)     Be called by your name, not 'Master': |[] OFF|");
                        }
                        G.write("3)    Exit Settings");
                        string choice2 = G.read();
                        switch(choice2)
                        {
                            case "1":
                            settings.Fast_load = !settings.Fast_load;
                            File.WriteAllText(settings_path, $"{settings.Fast_load.ToString()}:{settings.Name_or_master.ToString()}");
                            goto start_command4;
                            
                            case "2":
                            settings.Name_or_master = !settings.Name_or_master;
                            File.WriteAllText(settings_path, $"{settings.Fast_load.ToString()}:{settings.Name_or_master.ToString()}");
                            goto start_command4;

                            case "3":
                            break;

                            default:
                            G.write("Invalid");
                            Thread.Sleep(500);
                            Console.ReadKey();
                            goto start_command4;
                        }
                        break;

                        default:
                        G.write("\nThat is not a number, pick off the list.");
                        Thread.Sleep(500);
                        Console.ReadKey();
                        goto command_choice;
                    }
                } else if(command != "Y" && command != "N") {
                    G.write("Invalid");
                    Thread.Sleep(500);
                    Console.ReadKey();
                    Console.SetCursorPosition(0, Console.CursorTop - 3);
                    G.write(new string(' ', Console.WindowWidth), false);
                    Console.SetCursorPosition(0, Console.CursorTop);
                    goto enter_command;
                }

                G.write("\nPress any key to exit:");
                Console.ReadKey();
            } else {
                atempt_num--;
                G.write("\nWrong Password");
                 G.write($"{atempt_num} atempts left.");
                Console.ReadKey();
                if(atempt_num != 0)
                {
                    goto start;
                }
            }
        }
    }

    public class Info
    {
        public string? Name { get; set; }
        public string? Password { get; set; }
        public int? Positives { get; set; }
        public int? Negatives { get; set; }

        public Info(string name, string password, int positives, int negatives)
        {
            Name = name;
            Password = password;
            Positives = positives;
            Negatives = negatives;
        }
    }

    public class Timetable
    {
        public string? Form { get; set; }
        public string? Mon_p1 { get; set; }
        public string? Mon_p2 { get; set; }
        public string? Mon_p3 { get; set; }
        public string? Mon_p4 { get; set; }
        public string? Mon_lunch { get; set; }
        public string? Mon_p5 { get; set; }
        public string? Mon_p6 { get; set; }
        public string? Mon_home { get; set; }

        public string? Tue_p1 { get; set; }
        public string? Tue_p2 { get; set; }
        public string? Tue_p3 { get; set; }
        public string? Tue_p4 { get; set; }
        public string? Tue_lunch { get; set; }
        public string? Tue_p5 { get; set; }
        public string? Tue_p6 { get; set; }
        public string? Tue_home { get; set; }

        public string? Wed_p1 { get; set; }
        public string? Wed_p2 { get; set; }
        public string? Wed_p3 { get; set; }
        public string? Wed_p4 { get; set; }
        public string? Wed_lunch { get; set; }
        public string? Wed_p5 { get; set; }
        public string? Wed_p6 { get; set; }
        public string? Wed_home { get; set; }

        public string? Thu_p1 { get; set; }
        public string? Thu_p2 { get; set; }
        public string? Thu_p3 { get; set; }
        public string? Thu_p4 { get; set; }
        public string? Thu_lunch { get; set; }
        public string? Thu_p5 { get; set; }
        public string? Thu_p6 { get; set; }
        public string? Thu_home { get; set; }

        public string? Fri_p1 { get; set; }
        public string? Fri_p2 { get; set; }
        public string? Fri_p3 { get; set; }
        public string? Fri_p4 { get; set; }
        public string? Fri_lunch { get; set; }
        public string? Fri_p5 { get; set; }
        public string? Fri_p6 { get; set; }
        public string? Fri_home { get; set; }

        public Timetable(string form, string[] mon, string[] tue, string[] wed, string[] thu, string[] fri)
        {
            Form = form;
            Mon_p1 = mon[0];
            Mon_p2 = mon[1];
            Mon_p3 = mon[2];
            Mon_p4 = mon[3];
            Mon_lunch = mon[4];
            Mon_p5 = mon[5];
            Mon_p6 = mon[6];
            Mon_home = mon[7];

            Tue_p1 = tue[0];
            Tue_p2 = tue[1];
            Tue_p3 = tue[2];
            Tue_p4 = tue[3];
            Tue_lunch = tue[4];
            Tue_p5 = tue[5];
            Tue_p6 = tue[6];
            Tue_home = tue[7];

            Wed_p1 = wed[0];
            Wed_p2 = wed[1];
            Wed_p3 = wed[2];
            Wed_p4 = wed[3];
            Wed_lunch = wed[4];
            Wed_p5 = wed[5];
            Wed_p6 = wed[6];
            Wed_home = wed[7];

            Thu_p1 = thu[0];
            Thu_p2 = thu[1];
            Thu_p3 = thu[2];
            Thu_p4 = thu[3];
            Thu_lunch = thu[4];
            Thu_p5 = thu[5];
            Thu_p6 = thu[6];
            Thu_home = thu[7];

            Fri_p1 = fri[0];
            Fri_p2 = fri[1];
            Fri_p3 = fri[2];
            Fri_p4 = fri[3];
            Fri_lunch = fri[4];
            Fri_p5 = fri[5];
            Fri_p6 = fri[6];
            Fri_home = fri[7];
        }
    }
    public class Settings
    {
        public bool Fast_load { get; set; }
        public bool Name_or_master { get; set; }
        public Settings(string[] set)
        {
            Fast_load = bool.Parse(set[0]);
            Name_or_master = bool.Parse(set[1]);
        }
    }
}