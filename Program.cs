using System;
using System.CodeDom.Compiler;
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

            G.write("Please enter your password: (type 'forgot' to view password)");
            string pass = Specielized.Encript_input("*");

            if(pass.ToLower() == "forgot") {
                Console.Clear();
                G.write("Enter your password reset token:");
                string token = Specielized.Encript_input("*");
                if(token == File.ReadAllText(Info.password_reset_path))
                {
                    G.write("\nYour reset password is:");
                    File.WriteAllText(Info.password_path, Generate());
                    G.write(File.ReadAllText(Info.password_path));
                } else {
                    G.write("Incorrect");
                    Thread.Sleep(500);
                    Console.ReadKey();
                }
            } else if(pass == File.ReadAllText(Info.password_path)) {
                Info info = new Info(File.ReadAllText(Info.name_path), pass, int.Parse(File.ReadAllText(Info.positives_path)), int.Parse(File.ReadAllText(Info.negatives_path)), File.ReadAllText(Info.password_reset_path));
                Timetable timetable = new Timetable(File.ReadAllText(Timetable.form_path), File.ReadAllText(Timetable.monday_path).Split(":"), File.ReadAllText(Timetable.tuesday_path).Split(":"), File.ReadAllText(Timetable.wednesday_path).Split(":"), File.ReadAllText(Timetable.thursday_path).Split(":"), File.ReadAllText(Timetable.friday_path).Split(":"));
                home:
                Console.Clear();
                if(settings.Name_or_master)
                {
                    G.write($"Welcome {info.Name},");
                } else {
                    G.write("Welcome Master,");
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
                G.write("\nWould you like to enter a command? Y/N");
                string command = G.read().ToUpper();
                if(command == "Y")
                {
                    command_choice:
                    Console.Clear();
                    G.write("Enter the command number:");
                    G.write("1)    Add positives/credits,");
                    G.write("2)    Add negatives,");
                    G.write("3)    Change lesson timetable,");
                    G.write("4)    Settings,");
                    G.write("5)    Back to home screen.");
                    string choice = G.read();
                    switch(choice)
                    {
                        case "1":
                        start_command1:
                        Console.Clear();
                        G.write("How many positives/credits have you gotten? :)");
                        string middle = Console.ReadLine();
                        if(int.TryParse(middle, out int i1))
                        {
                            G.write("That is not a number.");
                            Thread.Sleep(500);
                            Console.ReadKey();
                            goto start_command1;
                        }
                        int many1 = i1;
                        File.WriteAllText(Info.positives_path, (info.Positives + many1).ToString());
                        info.Positives = int.Parse(File.ReadAllText(Info.positives_path));
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
                        if(int.TryParse(middle, out int i2))
                        {
                            G.write("That is not a number.");
                            Thread.Sleep(500);
                            Console.ReadKey();
                            goto start_command2;
                        }
                        int many2 = i2;
                        File.WriteAllText(Info.positives_path, (info.Positives + many2).ToString());
                        info.Positives = int.Parse(File.ReadAllText(Info.positives_path));
                        G.write("Done");
                        another2:
                        G.write("Would you like to enter another command? Y/N");
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
                        start_command3:
                        Console.Clear();
                        G.write("What day do you want to change something in? \nYou can type the first 3 letters of that day (excluding if you type 'form' to change your form room)");
                        string day = Console.ReadLine();
                        start_command3_1:
                        G.write("What number period do you want to change? \n(type 'lunch' for a lunch time club or type 'home' for a home time club)");
                        string period2 = Console.ReadLine();
                        switch(day.ToLower())
                        {
                            case "monday":
                            case "mon":
                            int o = 0;
                            if(int.TryParse(period2, out o) || period2.ToLower() == "lunch" || period2.ToLower() == "home")
                            {
                                timetable = Change_timetable(timetable, "mon", period2);
                            } else {
                                G.write("\nPeriod is not a number");
                                Thread.Sleep(500);
                                Console.SetCursorPosition(0, Console.CursorTop - 4);
                                G.write(new string(' ', Console.WindowWidth), false);
                                Console.SetCursorPosition(0, Console.CursorTop);
                                goto start_command3_1;
                            }
                            break;

                            case "tuesday":
                            case "tue":
                            o = 0;
                            if(int.TryParse(period2, out o) || period2.ToLower() == "lunch" || period2.ToLower() == "home")
                            {
                                timetable = Change_timetable(timetable, "tue", period2);
                            } else {
                                G.write("\nPeriod is not a number");
                                Thread.Sleep(500);
                                Console.SetCursorPosition(0, Console.CursorTop - 4);
                                G.write(new string(' ', Console.WindowWidth), false);
                                Console.SetCursorPosition(0, Console.CursorTop);
                                goto start_command3_1;
                            }
                            break;
                            
                            case "wednesday":
                            case "wed":
                            o = 0;
                            if(int.TryParse(period2, out o) || period2.ToLower() == "lunch" || period2.ToLower() == "home")
                            {
                                timetable = Change_timetable(timetable, "wed", period2);
                            } else {
                                G.write("\nPeriod is not a number");
                                Thread.Sleep(500);
                                Console.SetCursorPosition(0, Console.CursorTop - 4);
                                G.write(new string(' ', Console.WindowWidth), false);
                                Console.SetCursorPosition(0, Console.CursorTop);
                                goto start_command3_1;
                            }
                            break;

                            case "thursday":
                            case "thu":
                            o = 0;
                            if(int.TryParse(period2, out o) || period2.ToLower() == "lunch" || period2.ToLower() == "home")
                            {
                                timetable = Change_timetable(timetable, "thu", period2);
                            } else {
                                G.write("\nPeriod is not a number");
                                Thread.Sleep(500);
                                Console.SetCursorPosition(0, Console.CursorTop - 4);
                                G.write(new string(' ', Console.WindowWidth), false);
                                Console.SetCursorPosition(0, Console.CursorTop);
                                goto start_command3_1;
                            }
                            break;

                            case "friday":
                            case "fri":
                            o = 0;
                            if(int.TryParse(period2, out o) || period2.ToLower() == "lunch" || period2.ToLower() == "home")
                            {
                                timetable = Change_timetable(timetable, "fri", period2);
                            } else {
                                G.write("\nPeriod is not a number");
                                Thread.Sleep(500);
                                Console.SetCursorPosition(0, Console.CursorTop - 4);
                                G.write(new string(' ', Console.WindowWidth), false);
                                Console.SetCursorPosition(0, Console.CursorTop);
                                goto start_command3_1;
                            }
                            break;

                            case "form":
                            timetable = Change_form(timetable);
                            break;

                            default:
                            G.write("Invalid");
                            Thread.Sleep(500);
                            goto start_command3;
                        }
                        break;

                        case "4":
                        start_command4:
                        Console.Clear();
                        G.write("Welcome to settings!");
                        G.write("What would you like to do?");
                        if(settings.Fast_load)
                        {
                            G.write("1)    Fast loading: ", false);
                            Console.BackgroundColor = ConsoleColor.White;
                            Console.ForegroundColor = ConsoleColor.Black;
                            G.write("|ON []|");
                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.ForegroundColor = ConsoleColor.White;
                        } else {
                            G.write("1)    Fast loading: |[] OFF|");
                        }
                        if(settings.Name_or_master)
                        {
                            G.write("2)    Be called by your name, not 'Master': ", false);
                            Console.BackgroundColor = ConsoleColor.White;
                            Console.ForegroundColor = ConsoleColor.Black;
                            G.write("|ON []|");
                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.ForegroundColor = ConsoleColor.White;
                        } else
                        {
                            G.write("2)    Be called by your name, not 'Master': |[] OFF|");
                        }
                        G.write("3)    Change your password");
                        G.write("4)    Generate password reset token");
                        G.write("5)    Clear positives/negatives");
                        G.write("6)    Exit Settings");
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
                            start_command4_1:
                            Console.Clear();
                            G.write("Enter your old password");
                            string old = Specielized.Encript_input("*");
                            if(old == info.Password)
                            {
                                new_pass_start:
                                G.write("\nEnter new password:");
                                string new_pass = Specielized.Encript_input("*");
                                G.write("\nRenter new password:");
                                if(new_pass != Specielized.Encript_input("*"))
                                {
                                    G.write("\nPasswords do not match");
                                    Thread.Sleep(500);
                                    G.write("Press any key to continue");
                                    Console.ReadKey();
                                    Console.SetCursorPosition(0, Console.CursorTop - 8);
                                    G.write(new string(' ', Console.WindowWidth), false);
                                    Console.SetCursorPosition(0, Console.CursorTop);
                                    goto new_pass_start;
                                }
                                File.WriteAllText(Info.password_path, new_pass);
                                info.Password = File.ReadAllText(Info.password_path);
                                G.write("\nDone");
                                Console.ReadKey();
                                goto start_command4;
                            } else {
                                G.write("\nWrong Password");
                                Thread.Sleep(500);
                                Console.ReadKey();
                                goto start_command4_1;
                            }

                            case "4":
                            File.WriteAllText(Info.password_reset_path, rand.Next(0000, 1000).ToString());
                            info.Password_reset_token = File.ReadAllText(Info.password_reset_path);
                            G.write("Your password reset token is:");
                            G.write(info.Password_reset_token);
                            break;

                            case "5":
                            start_command4_2:
                            Console.Clear();
                            G.write("\nWould you like to clear Positives(p) or Negatives(n)?");
                            string clear = Console.ReadLine();
                            if (clear.ToLower() == "p")
                            {

                            } else if (clear.ToLower() == "n") {

                            } else {
                                G.write("Invalid");
                                Thread.Sleep(500);
                                G.write("Press any key to continue");
                                Console.ReadKey();
                                goto start_command4_2;
                            }
                            break;

                            case "6":
                            break;

                            default:
                            G.write("Invalid");
                            Thread.Sleep(500);
                            Console.ReadKey();
                            goto start_command4;
                        }
                        break;

                        case "5":
                        goto home;

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
            } else {
                atempt_num--;
                G.write("\nWrong Password");
                G.write($"{atempt_num} atempts left.");
                Thread.Sleep(500);
                G.write("Press any key to try again");
                Console.ReadKey();
                if(atempt_num != 0)
                {
                    goto start;
                }
            }
            G.write("\nPress any key to exit:");
            Console.ReadKey();
        }

        public static string Generate()
        {
            Random rand = new Random();
            string s = "";
            for(int i = 0; i <= 3; i++)
            {
                Letters middle = (Letters)rand.Next(1, 63);
                switch(middle)
                {
                    case Letters.n0:
                    s += "0";
                    break;
                    case Letters.n1:
                    s += "1";
                    break;
                    case Letters.n2:
                    s += "2";
                    break;
                    case Letters.n3:
                    s += "3";
                    break;
                    case Letters.n4:
                    s += "4";
                    break;
                    case Letters.n5:
                    s += "5";
                    break;
                    case Letters.n6:
                    s += "6";
                    break;
                    case Letters.n7:
                    s += "7";
                    break;
                    case Letters.n8:
                    s += "8";
                    break;
                    case Letters.n9:
                    s += "9";
                    break;

                    default:
                    s += middle.ToString();
                    break;
                }
            }
            return s;
        }

        enum Letters
        {
            A = 1,
            B,
            C,
            D,
            E,
            F,
            G,
            H,
            I,
            J,
            K,
            L,
            M,
            N,
            O,
            P,
            Q,
            R,
            S,
            T,
            U,
            V,
            W,
            X,
            Y,
            Z,
            a,
            b,
            c,
            d,
            e,
            f,
            g,
            h,
            i,
            j,
            k,
            l,
            m,
            n,
            o,
            p,
            q,
            r,
            s,
            t,
            u,
            v,
            w,
            x,
            y,
            z,
            n0,
            n1,
            n2,
            n3,
            n4,
            n5,
            n6,
            n7,
            n8,
            n9
        }

        public static Timetable Change_timetable(Timetable timetable, string day, string period)
        {
            G.write("What lesson/club are you changing it to?");
            string change = Console.ReadLine();
            G.write("What room is it?");
            change = change + " " + Console.ReadLine();
            string time = day + "_p" + period;
            switch(time)
            {
                case "mon_p1":
                File.WriteAllText(Timetable.monday_path, $"{change}:{timetable.Mon_p2}:{timetable.Mon_p3}:{timetable.Mon_p4}:{timetable.Mon_lunch}:{timetable.Mon_p5}:{timetable.Mon_p6}:{timetable.Mon_home}");
                break;
                case "mon_p2":
                File.WriteAllText(Timetable.monday_path, $"{timetable.Mon_p1}:{change}:{timetable.Mon_p3}:{timetable.Mon_p4}:{timetable.Mon_lunch}:{timetable.Mon_p5}:{timetable.Mon_p6}:{timetable.Mon_home}");
                break;
                case "mon_p3":
                File.WriteAllText(Timetable.monday_path, $"{timetable.Mon_p1}:{timetable.Mon_p2}:{change}:{timetable.Mon_p4}:{timetable.Mon_lunch}:{timetable.Mon_p5}:{timetable.Mon_p6}:{timetable.Mon_home}");
                break;
                case "mon_p4":
                File.WriteAllText(Timetable.monday_path, $"{timetable.Mon_p1}:{timetable.Mon_p2}:{timetable.Mon_p3}:{change}:{timetable.Mon_lunch}:{timetable.Mon_p5}:{timetable.Mon_p6}:{timetable.Mon_home}");
                break;
                case "mon_plunch":
                File.WriteAllText(Timetable.monday_path, $"{timetable.Mon_p1}:{timetable.Mon_p2}:{timetable.Mon_p3}:{timetable.Mon_p4}:{change}:{timetable.Mon_p5}:{timetable.Mon_p6}:{timetable.Mon_home}");
                break;
                case "mon_p5":
                File.WriteAllText(Timetable.monday_path, $"{timetable.Mon_p1}:{timetable.Mon_p2}:{timetable.Mon_p3}:{timetable.Mon_p4}:{timetable.Mon_lunch}:{change}:{timetable.Mon_p6}:{timetable.Mon_home}");
                break;
                case "mon_p6":
                File.WriteAllText(Timetable.monday_path, $"{timetable.Mon_p1}:{timetable.Mon_p2}:{timetable.Mon_p3}:{timetable.Mon_p4}:{timetable.Mon_lunch}:{timetable.Mon_p5}:{change}:{timetable.Mon_home}");
                break;
                case "mon_phome":
                File.WriteAllText(Timetable.monday_path, $"{timetable.Mon_p1}:{timetable.Mon_p2}:{timetable.Mon_p3}:{timetable.Mon_p4}:{timetable.Mon_lunch}:{timetable.Mon_p5}:{timetable.Mon_p6}:{change}");
                break;

                case "tue_p1":
                File.WriteAllText(Timetable.tuesday_path, $"{change}:{timetable.Tue_p2}:{timetable.Tue_p3}:{timetable.Tue_p4}:{timetable.Tue_lunch}:{timetable.Tue_p5}:{timetable.Tue_p6}:{timetable.Tue_home}");
                break;
                case "tue_p2":
                File.WriteAllText(Timetable.tuesday_path, $"{timetable.Tue_p1}:{change}:{timetable.Tue_p3}:{timetable.Tue_p4}:{timetable.Tue_lunch}:{timetable.Tue_p5}:{timetable.Tue_p6}:{timetable.Tue_home}");
                break;
                case "tue_p3":
                File.WriteAllText(Timetable.tuesday_path, $"{timetable.Tue_p1}:{timetable.Tue_p2}:{change}:{timetable.Tue_p4}:{timetable.Tue_lunch}:{timetable.Tue_p5}:{timetable.Tue_p6}:{timetable.Tue_home}");
                break;
                case "tue_p4":
                File.WriteAllText(Timetable.tuesday_path, $"{timetable.Tue_p1}:{timetable.Tue_p2}:{timetable.Tue_p3}:{change}:{timetable.Tue_lunch}:{timetable.Tue_p5}:{timetable.Tue_p6}:{timetable.Tue_home}");
                break;
                case "tue_plunch":
                File.WriteAllText(Timetable.tuesday_path, $"{timetable.Tue_p1}:{timetable.Tue_p2}:{timetable.Tue_p3}:{timetable.Tue_p4}:{change}:{timetable.Tue_p5}:{timetable.Tue_p6}:{timetable.Tue_home}");
                break;
                case "tue_p5":
                File.WriteAllText(Timetable.tuesday_path, $"{timetable.Tue_p1}:{timetable.Tue_p2}:{timetable.Tue_p3}:{timetable.Tue_p4}:{timetable.Tue_lunch}:{change}:{timetable.Tue_p6}:{timetable.Tue_home}");
                break;
                case "tue_p6":
                File.WriteAllText(Timetable.tuesday_path, $"{timetable.Tue_p1}:{timetable.Tue_p2}:{timetable.Tue_p3}:{timetable.Tue_p4}:{timetable.Tue_lunch}:{timetable.Tue_p5}:{change}:{timetable.Tue_home}");
                break;
                case "tue_phome":
                File.WriteAllText(Timetable.tuesday_path, $"{timetable.Tue_p1}:{timetable.Tue_p2}:{timetable.Tue_p3}:{timetable.Tue_p4}:{timetable.Tue_lunch}:{timetable.Tue_p5}:{timetable.Tue_p6}:{change}");
                break;

                case "wed_p1":
                File.WriteAllText(Timetable.wednesday_path, $"{change}:{timetable.Wed_p2}:{timetable.Wed_p3}:{timetable.Wed_p4}:{timetable.Wed_lunch}:{timetable.Wed_p5}:{timetable.Wed_p6}:{timetable.Wed_home}");
                break;
                case "wed_p2":
                File.WriteAllText(Timetable.wednesday_path, $"{timetable.Wed_p1}:{change}:{timetable.Wed_p3}:{timetable.Wed_p4}:{timetable.Wed_lunch}:{timetable.Wed_p5}:{timetable.Wed_p6}:{timetable.Wed_home}");
                break;
                case "wed_p3":
                File.WriteAllText(Timetable.wednesday_path, $"{timetable.Wed_p1}:{timetable.Wed_p2}:{change}:{timetable.Wed_p4}:{timetable.Wed_lunch}:{timetable.Wed_p5}:{timetable.Wed_p6}:{timetable.Wed_home}");
                break;
                case "wed_p4":
                File.WriteAllText(Timetable.wednesday_path, $"{timetable.Wed_p1}:{timetable.Wed_p2}:{timetable.Wed_p3}:{change}:{timetable.Wed_lunch}:{timetable.Wed_p5}:{timetable.Wed_p6}:{timetable.Wed_home}");
                break;
                case "wed_plunch":
                File.WriteAllText(Timetable.wednesday_path, $"{timetable.Wed_p1}:{timetable.Wed_p2}:{timetable.Wed_p3}:{timetable.Wed_p4}:{change}:{timetable.Wed_p5}:{timetable.Wed_p6}:{timetable.Wed_home}");
                break;
                case "wed_p5":
                File.WriteAllText(Timetable.wednesday_path, $"{timetable.Wed_p1}:{timetable.Wed_p2}:{timetable.Wed_p3}:{timetable.Wed_p4}:{timetable.Wed_lunch}:{change}:{timetable.Wed_p6}:{timetable.Wed_home}");
                break;
                case "wed_p6":
                File.WriteAllText(Timetable.wednesday_path, $"{timetable.Wed_p1}:{timetable.Wed_p2}:{timetable.Wed_p3}:{timetable.Wed_p4}:{timetable.Wed_lunch}:{timetable.Wed_p5}:{change}:{timetable.Wed_home}");
                break;
                case "wed_phome":
                File.WriteAllText(Timetable.wednesday_path, $"{timetable.Wed_p1}:{timetable.Wed_p2}:{timetable.Wed_p3}:{timetable.Wed_p4}:{timetable.Wed_lunch}:{timetable.Wed_p5}:{timetable.Wed_p6}:{change}");
                break;

                case "thu_p1":
                File.WriteAllText(Timetable.thursday_path, $"{change}:{timetable.Thu_p2}:{timetable.Thu_p3}:{timetable.Thu_p4}:{timetable.Thu_lunch}:{timetable.Thu_p5}:{timetable.Thu_p6}:{timetable.Thu_home}");
                break;
                case "thu_p2":
                File.WriteAllText(Timetable.thursday_path, $"{timetable.Thu_p1}:{change}:{timetable.Thu_p3}:{timetable.Thu_p4}:{timetable.Thu_lunch}:{timetable.Thu_p5}:{timetable.Thu_p6}:{timetable.Thu_home}");
                break;
                case "thu_p3":
                File.WriteAllText(Timetable.thursday_path, $"{timetable.Thu_p1}:{timetable.Thu_p2}:{change}:{timetable.Thu_p4}:{timetable.Thu_lunch}:{timetable.Thu_p5}:{timetable.Thu_p6}:{timetable.Thu_home}");
                break;
                case "thu_p4":
                File.WriteAllText(Timetable.thursday_path, $"{timetable.Thu_p1}:{timetable.Thu_p2}:{timetable.Thu_p3}:{change}:{timetable.Thu_lunch}:{timetable.Thu_p5}:{timetable.Thu_p6}:{timetable.Thu_home}");
                break;
                case "thu_plunch":
                File.WriteAllText(Timetable.thursday_path, $"{timetable.Thu_p1}:{timetable.Thu_p2}:{timetable.Thu_p3}:{timetable.Thu_p4}:{change}:{timetable.Thu_p5}:{timetable.Thu_p6}:{timetable.Thu_home}");
                break;
                case "thu_p5":
                File.WriteAllText(Timetable.thursday_path, $"{timetable.Thu_p1}:{timetable.Thu_p2}:{timetable.Thu_p3}:{timetable.Thu_p4}:{timetable.Thu_lunch}:{change}:{timetable.Thu_p6}:{timetable.Thu_home}");
                break;
                case "thu_p6":
                File.WriteAllText(Timetable.thursday_path, $"{timetable.Thu_p1}:{timetable.Thu_p2}:{timetable.Thu_p3}:{timetable.Thu_p4}:{timetable.Thu_lunch}:{timetable.Thu_p5}:{change}:{timetable.Thu_home}");
                break;
                case "thu_phome":
                File.WriteAllText(Timetable.thursday_path, $"{timetable.Thu_p1}:{timetable.Thu_p2}:{timetable.Thu_p3}:{timetable.Thu_p4}:{timetable.Thu_lunch}:{timetable.Thu_p5}:{timetable.Thu_p6}:{change}");
                break;

                case "fri_p1":
                File.WriteAllText(Timetable.friday_path, $"{change}:{timetable.Fri_p2}:{timetable.Fri_p3}:{timetable.Fri_p4}:{timetable.Fri_lunch}:{timetable.Fri_p5}:{timetable.Fri_p6}:{timetable.Fri_home}");
                break;
                case "fri_p2":
                File.WriteAllText(Timetable.friday_path, $"{timetable.Fri_p1}:{change}:{timetable.Fri_p3}:{timetable.Fri_p4}:{timetable.Fri_lunch}:{timetable.Fri_p5}:{timetable.Fri_p6}:{timetable.Fri_home}");
                break;
                case "fri_p3":
                File.WriteAllText(Timetable.friday_path, $"{timetable.Fri_p1}:{timetable.Fri_p2}:{change}:{timetable.Fri_p4}:{timetable.Fri_lunch}:{timetable.Fri_p5}:{timetable.Fri_p6}:{timetable.Fri_home}");
                break;
                case "fri_p4":
                File.WriteAllText(Timetable.friday_path, $"{timetable.Fri_p1}:{timetable.Fri_p2}:{timetable.Fri_p3}:{change}:{timetable.Fri_lunch}:{timetable.Fri_p5}:{timetable.Fri_p6}:{timetable.Fri_home}");
                break;
                case "fri_plunch":
                File.WriteAllText(Timetable.friday_path, $"{timetable.Fri_p1}:{timetable.Fri_p2}:{timetable.Fri_p3}:{timetable.Fri_p4}:{change}:{timetable.Fri_p5}:{timetable.Fri_p6}:{timetable.Fri_home}");
                break;
                case "fri_p5":
                File.WriteAllText(Timetable.friday_path, $"{timetable.Fri_p1}:{timetable.Fri_p2}:{timetable.Fri_p3}:{timetable.Fri_p4}:{timetable.Fri_lunch}:{change}:{timetable.Fri_p6}:{timetable.Fri_home}");
                break;
                case "fri_p6":
                File.WriteAllText(Timetable.friday_path, $"{timetable.Fri_p1}:{timetable.Fri_p2}:{timetable.Fri_p3}:{timetable.Fri_p4}:{timetable.Fri_lunch}:{timetable.Fri_p5}:{change}:{timetable.Fri_home}");
                break;
                case "fri_phome":
                File.WriteAllText(Timetable.friday_path, $"{timetable.Fri_p1}:{timetable.Fri_p2}:{timetable.Fri_p3}:{timetable.Fri_p4}:{timetable.Fri_lunch}:{timetable.Fri_p5}:{timetable.Fri_p6}:{change}");
                break;
            }
            timetable = new Timetable(File.ReadAllText(Timetable.form_path), File.ReadAllText(Timetable.monday_path).Split(":"), File.ReadAllText(Timetable.tuesday_path).Split(":"), File.ReadAllText(Timetable.wednesday_path).Split(":"), File.ReadAllText(Timetable.thursday_path).Split(":"), File.ReadAllText(Timetable.friday_path).Split(":"));
            return timetable;
        }
        public static Timetable Change_form(Timetable timetable)
        {
            G.write("What room is your new form?");
            string new_room = Console.ReadLine();
            File.WriteAllText(Timetable.form_path, $" Form {new_room}");
            timetable.Form = new_room;
            return timetable;
        }
    }

    public class Info
    {
        public string? Name { get; set; }
        public string? Password { get; set; }
        public int? Positives { get; set; }
        public int? Negatives { get; set; }
        public string? Password_reset_token { get; set; }

        public static string password_path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "Info", "Password.txt");
        public static string name_path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "Info", "Name.txt");
        public static string positives_path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "Info", "Positives.txt");
        public static string negatives_path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "Info", "Negatives.txt");
        public static string password_reset_path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "Info", "Password_reset.txt");

        public Info(string name, string password, int positives, int negatives, string password_reset_token)
        {
            Name = name;
            Password = password;
            Positives = positives;
            Negatives = negatives;
            Password_reset_token = password_reset_token;
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

        public static string form_path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "Lessons", "Form.txt");
        public static string monday_path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "Lessons", "Monday.txt");
        public static string tuesday_path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "Lessons", "Tuesday.txt");
        public static string wednesday_path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "Lessons", "Wednesday.txt");
        public static string thursday_path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "Lessons", "Thursday.txt");
        public static string friday_path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "Lessons", "Friday.txt");

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