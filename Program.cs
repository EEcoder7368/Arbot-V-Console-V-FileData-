using System;
using System.Collections.Generic;
using System.IO;
using Raylib_cs;

namespace Arbot__V_Console___V_FileData_
{
    public class Window
    {
        static void Program()
        {
            Settings.settings(File.ReadAllText(Settings.Settings_path).Split(":"));
            Random rand = new Random();
            write("Welcome to Arbot!");
            if(!Settings.Fast_load)
            {
                for(int i = 0; i <= 100; i++)
                {
                    switch(i % 4)
                    {
                        case 0:
                        write("Loading...");
                        break;

                        case 1:
                        write("Loading..");
                        break;

                        case 2:
                        write("Loading.");
                        break;

                        case 3:
                        write("Loading");
                        break;
                    }
                    write($"{i}%");
                    if(i != 100)
                    {
                        Thread.Sleep(rand.Next(30, 150));
                        back(3);
                    }
                }
                write("\n--Finished--");
            }
            write("Press any key to continue");
            read_key();
            int atempt_num = 3;
            start:
            clear();

            write("Please enter your password: (type 'forgot' to view password)");
            string pass = read('*');

            if(pass.ToLower() == "forgot") {
                clear();
                write("Enter your password reset token:");
                string token = read('*');
                if(token == File.ReadAllText(Info.password_reset_path))
                {
                    write("\nYour reset password is:");
                    File.WriteAllText(Info.password_path, Generate());
                    write(File.ReadAllText(Info.password_path));
                    write("\n It is recomended to generate a new password reset token after use.");
                } else {
                    write("\nIncorrect");
                    Thread.Sleep(500);
                    read_key();
                }
            } else if(pass == File.ReadAllText(Info.password_path)) {
                Info.info(File.ReadAllText(Info.name_path), pass, int.Parse(File.ReadAllText(Info.positives_path)), int.Parse(File.ReadAllText(Info.negatives_path)), File.ReadAllText(Info.password_reset_path));
                Timetable.timetable(File.ReadAllText(Timetable.form_path), File.ReadAllText(Timetable.monday_path).Split(":"), File.ReadAllText(Timetable.tuesday_path).Split(":"), File.ReadAllText(Timetable.wednesday_path).Split(":"), File.ReadAllText(Timetable.thursday_path).Split(":"), File.ReadAllText(Timetable.friday_path).Split(":"));
                home:
                clear();
                if(Settings.Name_or_master)
                {
                    write($"Welcome {Info.Name},");
                } else {
                    write("Welcome Master,");
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
                bool school_day = today_weekday != DayOfWeek.Saturday && today_weekday != DayOfWeek.Sunday;

                if (current_time_obj >= TimeSpan.Parse("08:40:00") && current_time_obj < TimeSpan.Parse("09:10:00"))
                {
                    period = "Formtime";
                    lesson_num = 0;
                    next_bell = monday_logic("09:10:00");
                }
                else if (current_time_obj >= TimeSpan.Parse("09:10:00") && current_time_obj < TimeSpan.Parse("10:00:00"))
                {
                    period = "1";
                    lesson_num = 1;
                    next_bell = monday_logic("10:00:00");
                }
                else if (current_time_obj >= TimeSpan.Parse("10:00:00") && current_time_obj < TimeSpan.Parse("10:50:00"))
                {
                    period = "2";
                    lesson_num = 2;
                    next_bell = monday_logic("10:50:00");
                }
                else if (current_time_obj >= TimeSpan.Parse("10:50:00") && current_time_obj < TimeSpan.Parse("11:10:00"))
                {
                    period = "Breaktime";
                    lesson_num = 3;
                    next_bell = monday_logic("11:10:00");
                }
                else if (current_time_obj >= TimeSpan.Parse("11:10:00") && current_time_obj < TimeSpan.Parse("12:00:00"))
                {
                    period = "3";
                    lesson_num = 4;
                    next_bell = monday_logic("12:00:00");
                }
                else if (current_time_obj >= TimeSpan.Parse("12:00:00") && current_time_obj < TimeSpan.Parse("12:50:00"))
                {
                    period = "4";
                    lesson_num = 5;
                    next_bell = monday_logic("12:50:00");
                }
                else if (current_time_obj >= TimeSpan.Parse("12:50:00") && current_time_obj < TimeSpan.Parse("13:30:00"))
                {
                    period = "Lunchtime";
                    lesson_num = 6;
                    next_bell = "13:30:00";
                }
                else if (current_time_obj >= TimeSpan.Parse("13:30:00") && current_time_obj < TimeSpan.Parse("14:20:00"))
                {
                    period = "5";
                    lesson_num = 7;
                    next_bell = "14:20:00";
                }
                else if (current_time_obj >= TimeSpan.Parse("14:20:00") && current_time_obj < TimeSpan.Parse("15:10:00"))
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
                    lessons = new List<string> { Timetable.Form, Timetable.Mon_p1, Timetable.Mon_p2, "Breaktime", Timetable.Mon_p3, Timetable.Mon_p4, Timetable.Mon_lunch, Timetable.Mon_p5, Timetable.Mon_p6, Timetable.Mon_home };
                    break;

                    case DayOfWeek.Tuesday:
                    lessons = new List<string> { Timetable.Form, Timetable.Tue_p1, Timetable.Tue_p2, "Breaktime", Timetable.Tue_p3, Timetable.Tue_p4, Timetable.Tue_lunch, Timetable.Tue_p5, Timetable.Tue_p6, Timetable.Tue_home };
                    break;

                    case DayOfWeek.Wednesday:
                    lessons = new List<string> { Timetable.Form, Timetable.Wed_p1, Timetable.Wed_p2, "Breaktime", Timetable.Wed_p3, Timetable.Wed_p4, Timetable.Wed_lunch, Timetable.Wed_p5, Timetable.Wed_p6, Timetable.Wed_home };
                    break;

                    case DayOfWeek.Thursday:
                    lessons = new List<string> { Timetable.Form, Timetable.Thu_p1, Timetable.Thu_p2, "Breaktime", Timetable.Thu_p3, Timetable.Thu_p4, Timetable.Thu_lunch, Timetable.Thu_p5, Timetable.Thu_p6, Timetable.Thu_home };
                    break;

                    case DayOfWeek.Friday:
                    lessons = new List<string> { Timetable.Form, Timetable.Fri_p1, Timetable.Fri_p2, "Breaktime", Timetable.Fri_p3, Timetable.Fri_p4, Timetable.Fri_lunch, Timetable.Fri_p5, Timetable.Fri_p6, Timetable.Fri_home };
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

                if (school_day)
                {
                    if (calculation >= TimeSpan.Zero)
                    {
                        write($"The day is {today_weekday} and we are in period {period}, the current lesson is {current_lesson} and the next lesson is {next_lesson}. The next bell is at {next_bell} and it is in {calculation} hours, minutes and seconds respectively.");
                    }
                    else
                    {
                        write($"The day is {today_weekday} and there are no lessons on.");
                    }
                }
                else
                {
                    write($"The day is {today_weekday} and there are no lessons on.");
                }

                write("");
                write($"You have: {Info.Positives} positive(s),");
                write($"You have: {Info.Negatives} negative(s),");

                enter_command:
                write("\nWould you like to enter a command? Y/N");
                string command = read(null).ToUpper();
                if(command == "Y")
                {
                    command_choice:
                    clear();
                    write("If you encounter any bugs please email hyper.games.company@gmail.com. \n(with a screen shot of the bug and how to replicate it)");
                    write("Enter the command number:");
                    write("1)    Add positives/credits,");
                    write("2)    Add negatives,");
                    write("3)    Change lesson Timetable,");
                    write("4)    Settings,");
                    write("5)    Back to home screen,");
                    write("6)    Exit.");
                    string choice = read(null);
                    switch(choice)
                    {
                        case "1":
                        start_command1:
                        clear();
                        write("How many positives/credits have you gained? :)");
                        string middle = read(null);
                        if(!int.TryParse(middle, out int i1))
                        {
                            write("That is not a number.");
                            Thread.Sleep(500);
                            read_key();
                            goto start_command1;
                        }
                        int many1 = i1;
                        File.WriteAllText(Info.positives_path, (Info.Positives + many1).ToString());
                        Info.Positives = int.Parse(File.ReadAllText(Info.positives_path));
                        write("Done");
                        write("Press any key");
                        read_key();
                        goto command_choice;

                        case "2":
                        start_command2:
                        clear();
                        write("How many negatives have you ained? :(");
                        middle = read(null);
                        if(!int.TryParse(middle, out int i2))
                        {
                            write("That is not a number.");
                            Thread.Sleep(500);
                            read_key();
                            goto start_command2;
                        }
                        int many2 = i2;
                        File.WriteAllText(Info.negatives_path, (Info.Negatives + many2).ToString());
                        Info.Negatives = int.Parse(File.ReadAllText(Info.negatives_path));
                        write("Done");
                        write("Press any key");
                        read_key();
                        goto command_choice;

                        case "3":
                        start_command3:
                        clear();
                        write("What day do you want to change something in? \nYou can type the first 3 letters of that day (excluding if you type 'form' to change your form room)");
                        string day = read(null);
                        start_command3_1:
                        write("What number period do you want to change? \n(type 'lunch' for a lunch time club or type 'home' for a home time club)");
                        string period2 = read(null);
                        switch(day.ToLower())
                        {
                            case "monday":
                            case "mon":
                            int o = 0;
                            if(int.TryParse(period2, out o) || period2.ToLower() == "lunch" || period2.ToLower() == "home")
                            {
                                Change_timetable("mon", period2);
                            } else {
                                write("\nPeriod is not a number");
                                Thread.Sleep(500);
                                back(4);
                                goto start_command3_1;
                            }
                            break;

                            case "tuesday":
                            case "tue":
                            o = 0;
                            if(int.TryParse(period2, out o) || period2.ToLower() == "lunch" || period2.ToLower() == "home")
                            {
                                Change_timetable("tue", period2);
                            } else {
                                write("\nPeriod is not a number");
                                Thread.Sleep(500);
                                back(4);
                                goto start_command3_1;
                            }
                            break;
                            
                            case "wednesday":
                            case "wed":
                            o = 0;
                            if(int.TryParse(period2, out o) || period2.ToLower() == "lunch" || period2.ToLower() == "home")
                            {
                                Change_timetable("wed", period2);
                            } else {
                                write("\nPeriod is not a number");
                                Thread.Sleep(500);
                                back(4);
                                goto start_command3_1;
                            }
                            break;

                            case "thursday":
                            case "thu":
                            o = 0;
                            if(int.TryParse(period2, out o) || period2.ToLower() == "lunch" || period2.ToLower() == "home")
                            {
                                Change_timetable("thu", period2);
                            } else {
                                write("\nPeriod is not a number");
                                Thread.Sleep(500);
                                back(4);
                                goto start_command3_1;
                            }
                            break;

                            case "friday":
                            case "fri":
                            o = 0;
                            if(int.TryParse(period2, out o) || period2.ToLower() == "lunch" || period2.ToLower() == "home")
                            {
                                Change_timetable("fri", period2);
                            } else {
                                write("\nPeriod is not a number");
                                Thread.Sleep(500);
                                back(4);
                                goto start_command3_1;
                            }
                            break;

                            case "form":
                            Change_form();
                            break;

                            default:
                            write("Invalid");
                            Thread.Sleep(500);
                            goto start_command3;
                        }
                        write("Press any key");
                        read_key();
                        goto command_choice;

                        case "4":
                        start_command4:
                        clear();
                        write("Welcome to Settings!");
                        write("What would you like to do?");
                        if(Settings.Fast_load)
                        {
                            write("1)    Fast loading: ", false);
                            Inverse();
                            write("|ON []|");
                            Inverse();
                        } else {
                            write("1)    Fast loading: |[] OFF|");
                        }
                        if(Settings.Name_or_master)
                        {
                            write("2)    Be called by your name, not 'Master': ", false);
                            Inverse();
                            write("|ON []|");
                            Inverse();
                        } else {
                            write("2)    Be called by your name, not 'Master': |[] OFF|");
                        }
                        if(Settings.Dark_mode)
                        {
                            write("3)    Dark mode: ", false);
                            Inverse();
                            write("|ON []|");
                            Inverse();
                        } else {
                            write("3)    Dark mode: |[] OFF|");
                        }
                        write("4)    Change your password");
                        write("5)    Change your name");
                        write("6)    Generate password reset token");
                        write("7)    Clear positives/negatives");
                        write("8)    Exit Settings");
                        string choice2 = read(null);
                        switch(choice2)
                        {
                            case "1":
                            Settings.Fast_load = !Settings.Fast_load;
                            File.WriteAllText(Settings.Settings_path, $"{Settings.Fast_load.ToString()}:{Settings.Name_or_master.ToString()}:{Settings.Dark_mode.ToString()}");
                            goto start_command4;
                            
                            case "2":
                            Settings.Name_or_master = !Settings.Name_or_master;
                            File.WriteAllText(Settings.Settings_path, $"{Settings.Fast_load.ToString()}:{Settings.Name_or_master.ToString()}:{Settings.Dark_mode.ToString()}");
                            goto start_command4;

                            case "3":
                            Inverse();
                            Settings.Dark_mode = !Settings.Dark_mode;
                            File.WriteAllText(Settings.Settings_path, $"{Settings.Fast_load.ToString()}:{Settings.Name_or_master.ToString()}:{Settings.Dark_mode.ToString()}");
                            goto start_command4;

                            case "4":
                            start_command4_1:
                            Window.clear();
                            write("Enter your old password");
                            string old = read('*');
                            if(old == Info.Password)
                            {
                                new_pass_start:
                                write("\nEnter new password:");
                                string new_pass = read('*');
                                write("\nRe-enter new password:");
                                if(new_pass != read('*'))
                                {
                                    write("\nPasswords do not match");
                                    Thread.Sleep(500);
                                    write("Press any key to continue");
                                    read_key();
                                    back(8);
                                    goto new_pass_start;
                                }
                                File.WriteAllText(Info.password_path, new_pass);
                                Info.Password = File.ReadAllText(Info.password_path);
                                write("\nDone");
                                read_key();
                                goto start_command4;
                            } else {
                                write("\nWrong Password");
                                Thread.Sleep(500);
                                read_key();
                                goto start_command4_1;
                            }

                            case "5":
                            Window.clear();
                            write("Enter your new name");
                            File.WriteAllText(Info.name_path, read(null));
                            Info.Name = File.ReadAllText(Info.name_path);
                            write("Done");
                            write("Press any key to continue");
                            read_key();
                            goto start_command4;

                            case "6":
                            string r = rand.Next(0000, 10000).ToString();
                            while(r.Length < 4)
                            {
                                r = "0" + r;
                            }
                            File.WriteAllText(Info.password_reset_path, r);
                            Info.Password_reset_token = File.ReadAllText(Info.password_reset_path);
                            write("Your password reset token is:");
                            write(Info.Password_reset_token);
                            write("Press any key to continue");
                            read_key();
                            goto start_command4;

                            case "7":
                            start_command4_2:
                            Window.clear();
                            write("\nWould you like to clear Positives(p) or Negatives(n)?");
                            string clear = read(null);
                            if (clear.ToLower() == "p")
                            {
                                File.WriteAllText(Info.positives_path, "0");
                                Info.Positives = 0;
                                write("Done");
                                write("Press any key to continue");
                                read_key();
                            } else if (clear.ToLower() == "n") {
                                File.WriteAllText(Info.negatives_path, "0");
                                Info.Negatives = 0;
                                write("Done");
                                write("Press any key to continue");
                                read_key();
                            } else {
                                write("Invalid");
                                Thread.Sleep(500);
                                write("Press any key to continue");
                                read_key();
                                goto start_command4_2;
                            }
                            goto start_command4;

                            case "8":
                            goto command_choice;

                            default:
                            write("Invalid");
                            Thread.Sleep(500);
                            read_key();
                            goto start_command4;
                        }

                        case "5":
                        goto home;

                        case "6":
                        break;

                        default:
                        write("\nThat is not a number, pick off the list.");
                        Thread.Sleep(500);
                        read_key();
                        goto command_choice;
                    }
                } else if(command != "Y" && command != "N") {
                    write("Invalid");
                    Thread.Sleep(500);
                    read_key();
                    back(3);
                    goto enter_command;
                }
            } else {
                atempt_num--;
                write("\nWrong Password");
                write($"{atempt_num} atempts left.");
                Thread.Sleep(500);
                write("Press any key to try again");
                read_key();
                if(atempt_num != 0)
                {
                    goto start;
                }
            }

            clear();
            write("Press any key to exit...");
            read_key();
        }

        public static string monday_logic(string next_bell)
        {
            if(DateTime.Now.DayOfWeek == DayOfWeek.Monday) { next_bell = (TimeSpan.Parse(next_bell).Add(TimeSpan.Parse("00:10:00"))).ToString(); }
            return next_bell;
        }

        public static string Generate()
        {
            Random rand = new Random();
            string s = "";
            for(int i = 0; i <= 3; i++)
            {
                Letters middle = (Letters)rand.Next(1, 65);
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

                    case Letters.Underscore:
                    s += "_";
                    break;
                    case Letters.Dash:
                    s += "-";
                    break;
                    case Letters.Exclamation:
                    s += "!";
                    break;
                    case Letters.Question:
                    s += "?";
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
            n9,
            Underscore,
            Dash,
            Exclamation,
            Question
        }

        public static void Change_timetable(string day, string period)
        {
            write("What lesson/club are you changing it to?");
            string change = read(null);
            write("What room is it?");
            change = change + " " + read(null);
            string time = day + "_p" + period;
            switch(time)
            {
                case "mon_p1":
                File.WriteAllText(Timetable.monday_path, $"{change}:{Timetable.Mon_p2}:{Timetable.Mon_p3}:{Timetable.Mon_p4}:{Timetable.Mon_lunch}:{Timetable.Mon_p5}:{Timetable.Mon_p6}:{Timetable.Mon_home}");
                break;
                case "mon_p2":
                File.WriteAllText(Timetable.monday_path, $"{Timetable.Mon_p1}:{change}:{Timetable.Mon_p3}:{Timetable.Mon_p4}:{Timetable.Mon_lunch}:{Timetable.Mon_p5}:{Timetable.Mon_p6}:{Timetable.Mon_home}");
                break;
                case "mon_p3":
                File.WriteAllText(Timetable.monday_path, $"{Timetable.Mon_p1}:{Timetable.Mon_p2}:{change}:{Timetable.Mon_p4}:{Timetable.Mon_lunch}:{Timetable.Mon_p5}:{Timetable.Mon_p6}:{Timetable.Mon_home}");
                break;
                case "mon_p4":
                File.WriteAllText(Timetable.monday_path, $"{Timetable.Mon_p1}:{Timetable.Mon_p2}:{Timetable.Mon_p3}:{change}:{Timetable.Mon_lunch}:{Timetable.Mon_p5}:{Timetable.Mon_p6}:{Timetable.Mon_home}");
                break;
                case "mon_plunch":
                File.WriteAllText(Timetable.monday_path, $"{Timetable.Mon_p1}:{Timetable.Mon_p2}:{Timetable.Mon_p3}:{Timetable.Mon_p4}:{change}:{Timetable.Mon_p5}:{Timetable.Mon_p6}:{Timetable.Mon_home}");
                break;
                case "mon_p5":
                File.WriteAllText(Timetable.monday_path, $"{Timetable.Mon_p1}:{Timetable.Mon_p2}:{Timetable.Mon_p3}:{Timetable.Mon_p4}:{Timetable.Mon_lunch}:{change}:{Timetable.Mon_p6}:{Timetable.Mon_home}");
                break;
                case "mon_p6":
                File.WriteAllText(Timetable.monday_path, $"{Timetable.Mon_p1}:{Timetable.Mon_p2}:{Timetable.Mon_p3}:{Timetable.Mon_p4}:{Timetable.Mon_lunch}:{Timetable.Mon_p5}:{change}:{Timetable.Mon_home}");
                break;
                case "mon_phome":
                File.WriteAllText(Timetable.monday_path, $"{Timetable.Mon_p1}:{Timetable.Mon_p2}:{Timetable.Mon_p3}:{Timetable.Mon_p4}:{Timetable.Mon_lunch}:{Timetable.Mon_p5}:{Timetable.Mon_p6}:{change}");
                break;

                case "tue_p1":
                File.WriteAllText(Timetable.tuesday_path, $"{change}:{Timetable.Tue_p2}:{Timetable.Tue_p3}:{Timetable.Tue_p4}:{Timetable.Tue_lunch}:{Timetable.Tue_p5}:{Timetable.Tue_p6}:{Timetable.Tue_home}");
                break;
                case "tue_p2":
                File.WriteAllText(Timetable.tuesday_path, $"{Timetable.Tue_p1}:{change}:{Timetable.Tue_p3}:{Timetable.Tue_p4}:{Timetable.Tue_lunch}:{Timetable.Tue_p5}:{Timetable.Tue_p6}:{Timetable.Tue_home}");
                break;
                case "tue_p3":
                File.WriteAllText(Timetable.tuesday_path, $"{Timetable.Tue_p1}:{Timetable.Tue_p2}:{change}:{Timetable.Tue_p4}:{Timetable.Tue_lunch}:{Timetable.Tue_p5}:{Timetable.Tue_p6}:{Timetable.Tue_home}");
                break;
                case "tue_p4":
                File.WriteAllText(Timetable.tuesday_path, $"{Timetable.Tue_p1}:{Timetable.Tue_p2}:{Timetable.Tue_p3}:{change}:{Timetable.Tue_lunch}:{Timetable.Tue_p5}:{Timetable.Tue_p6}:{Timetable.Tue_home}");
                break;
                case "tue_plunch":
                File.WriteAllText(Timetable.tuesday_path, $"{Timetable.Tue_p1}:{Timetable.Tue_p2}:{Timetable.Tue_p3}:{Timetable.Tue_p4}:{change}:{Timetable.Tue_p5}:{Timetable.Tue_p6}:{Timetable.Tue_home}");
                break;
                case "tue_p5":
                File.WriteAllText(Timetable.tuesday_path, $"{Timetable.Tue_p1}:{Timetable.Tue_p2}:{Timetable.Tue_p3}:{Timetable.Tue_p4}:{Timetable.Tue_lunch}:{change}:{Timetable.Tue_p6}:{Timetable.Tue_home}");
                break;
                case "tue_p6":
                File.WriteAllText(Timetable.tuesday_path, $"{Timetable.Tue_p1}:{Timetable.Tue_p2}:{Timetable.Tue_p3}:{Timetable.Tue_p4}:{Timetable.Tue_lunch}:{Timetable.Tue_p5}:{change}:{Timetable.Tue_home}");
                break;
                case "tue_phome":
                File.WriteAllText(Timetable.tuesday_path, $"{Timetable.Tue_p1}:{Timetable.Tue_p2}:{Timetable.Tue_p3}:{Timetable.Tue_p4}:{Timetable.Tue_lunch}:{Timetable.Tue_p5}:{Timetable.Tue_p6}:{change}");
                break;

                case "wed_p1":
                File.WriteAllText(Timetable.wednesday_path, $"{change}:{Timetable.Wed_p2}:{Timetable.Wed_p3}:{Timetable.Wed_p4}:{Timetable.Wed_lunch}:{Timetable.Wed_p5}:{Timetable.Wed_p6}:{Timetable.Wed_home}");
                break;
                case "wed_p2":
                File.WriteAllText(Timetable.wednesday_path, $"{Timetable.Wed_p1}:{change}:{Timetable.Wed_p3}:{Timetable.Wed_p4}:{Timetable.Wed_lunch}:{Timetable.Wed_p5}:{Timetable.Wed_p6}:{Timetable.Wed_home}");
                break;
                case "wed_p3":
                File.WriteAllText(Timetable.wednesday_path, $"{Timetable.Wed_p1}:{Timetable.Wed_p2}:{change}:{Timetable.Wed_p4}:{Timetable.Wed_lunch}:{Timetable.Wed_p5}:{Timetable.Wed_p6}:{Timetable.Wed_home}");
                break;
                case "wed_p4":
                File.WriteAllText(Timetable.wednesday_path, $"{Timetable.Wed_p1}:{Timetable.Wed_p2}:{Timetable.Wed_p3}:{change}:{Timetable.Wed_lunch}:{Timetable.Wed_p5}:{Timetable.Wed_p6}:{Timetable.Wed_home}");
                break;
                case "wed_plunch":
                File.WriteAllText(Timetable.wednesday_path, $"{Timetable.Wed_p1}:{Timetable.Wed_p2}:{Timetable.Wed_p3}:{Timetable.Wed_p4}:{change}:{Timetable.Wed_p5}:{Timetable.Wed_p6}:{Timetable.Wed_home}");
                break;
                case "wed_p5":
                File.WriteAllText(Timetable.wednesday_path, $"{Timetable.Wed_p1}:{Timetable.Wed_p2}:{Timetable.Wed_p3}:{Timetable.Wed_p4}:{Timetable.Wed_lunch}:{change}:{Timetable.Wed_p6}:{Timetable.Wed_home}");
                break;
                case "wed_p6":
                File.WriteAllText(Timetable.wednesday_path, $"{Timetable.Wed_p1}:{Timetable.Wed_p2}:{Timetable.Wed_p3}:{Timetable.Wed_p4}:{Timetable.Wed_lunch}:{Timetable.Wed_p5}:{change}:{Timetable.Wed_home}");
                break;
                case "wed_phome":
                File.WriteAllText(Timetable.wednesday_path, $"{Timetable.Wed_p1}:{Timetable.Wed_p2}:{Timetable.Wed_p3}:{Timetable.Wed_p4}:{Timetable.Wed_lunch}:{Timetable.Wed_p5}:{Timetable.Wed_p6}:{change}");
                break;

                case "thu_p1":
                File.WriteAllText(Timetable.thursday_path, $"{change}:{Timetable.Thu_p2}:{Timetable.Thu_p3}:{Timetable.Thu_p4}:{Timetable.Thu_lunch}:{Timetable.Thu_p5}:{Timetable.Thu_p6}:{Timetable.Thu_home}");
                break;
                case "thu_p2":
                File.WriteAllText(Timetable.thursday_path, $"{Timetable.Thu_p1}:{change}:{Timetable.Thu_p3}:{Timetable.Thu_p4}:{Timetable.Thu_lunch}:{Timetable.Thu_p5}:{Timetable.Thu_p6}:{Timetable.Thu_home}");
                break;
                case "thu_p3":
                File.WriteAllText(Timetable.thursday_path, $"{Timetable.Thu_p1}:{Timetable.Thu_p2}:{change}:{Timetable.Thu_p4}:{Timetable.Thu_lunch}:{Timetable.Thu_p5}:{Timetable.Thu_p6}:{Timetable.Thu_home}");
                break;
                case "thu_p4":
                File.WriteAllText(Timetable.thursday_path, $"{Timetable.Thu_p1}:{Timetable.Thu_p2}:{Timetable.Thu_p3}:{change}:{Timetable.Thu_lunch}:{Timetable.Thu_p5}:{Timetable.Thu_p6}:{Timetable.Thu_home}");
                break;
                case "thu_plunch":
                File.WriteAllText(Timetable.thursday_path, $"{Timetable.Thu_p1}:{Timetable.Thu_p2}:{Timetable.Thu_p3}:{Timetable.Thu_p4}:{change}:{Timetable.Thu_p5}:{Timetable.Thu_p6}:{Timetable.Thu_home}");
                break;
                case "thu_p5":
                File.WriteAllText(Timetable.thursday_path, $"{Timetable.Thu_p1}:{Timetable.Thu_p2}:{Timetable.Thu_p3}:{Timetable.Thu_p4}:{Timetable.Thu_lunch}:{change}:{Timetable.Thu_p6}:{Timetable.Thu_home}");
                break;
                case "thu_p6":
                File.WriteAllText(Timetable.thursday_path, $"{Timetable.Thu_p1}:{Timetable.Thu_p2}:{Timetable.Thu_p3}:{Timetable.Thu_p4}:{Timetable.Thu_lunch}:{Timetable.Thu_p5}:{change}:{Timetable.Thu_home}");
                break;
                case "thu_phome":
                File.WriteAllText(Timetable.thursday_path, $"{Timetable.Thu_p1}:{Timetable.Thu_p2}:{Timetable.Thu_p3}:{Timetable.Thu_p4}:{Timetable.Thu_lunch}:{Timetable.Thu_p5}:{Timetable.Thu_p6}:{change}");
                break;

                case "fri_p1":
                File.WriteAllText(Timetable.friday_path, $"{change}:{Timetable.Fri_p2}:{Timetable.Fri_p3}:{Timetable.Fri_p4}:{Timetable.Fri_lunch}:{Timetable.Fri_p5}:{Timetable.Fri_p6}:{Timetable.Fri_home}");
                break;
                case "fri_p2":
                File.WriteAllText(Timetable.friday_path, $"{Timetable.Fri_p1}:{change}:{Timetable.Fri_p3}:{Timetable.Fri_p4}:{Timetable.Fri_lunch}:{Timetable.Fri_p5}:{Timetable.Fri_p6}:{Timetable.Fri_home}");
                break;
                case "fri_p3":
                File.WriteAllText(Timetable.friday_path, $"{Timetable.Fri_p1}:{Timetable.Fri_p2}:{change}:{Timetable.Fri_p4}:{Timetable.Fri_lunch}:{Timetable.Fri_p5}:{Timetable.Fri_p6}:{Timetable.Fri_home}");
                break;
                case "fri_p4":
                File.WriteAllText(Timetable.friday_path, $"{Timetable.Fri_p1}:{Timetable.Fri_p2}:{Timetable.Fri_p3}:{change}:{Timetable.Fri_lunch}:{Timetable.Fri_p5}:{Timetable.Fri_p6}:{Timetable.Fri_home}");
                break;
                case "fri_plunch":
                File.WriteAllText(Timetable.friday_path, $"{Timetable.Fri_p1}:{Timetable.Fri_p2}:{Timetable.Fri_p3}:{Timetable.Fri_p4}:{change}:{Timetable.Fri_p5}:{Timetable.Fri_p6}:{Timetable.Fri_home}");
                break;
                case "fri_p5":
                File.WriteAllText(Timetable.friday_path, $"{Timetable.Fri_p1}:{Timetable.Fri_p2}:{Timetable.Fri_p3}:{Timetable.Fri_p4}:{Timetable.Fri_lunch}:{change}:{Timetable.Fri_p6}:{Timetable.Fri_home}");
                break;
                case "fri_p6":
                File.WriteAllText(Timetable.friday_path, $"{Timetable.Fri_p1}:{Timetable.Fri_p2}:{Timetable.Fri_p3}:{Timetable.Fri_p4}:{Timetable.Fri_lunch}:{Timetable.Fri_p5}:{change}:{Timetable.Fri_home}");
                break;
                case "fri_phome":
                File.WriteAllText(Timetable.friday_path, $"{Timetable.Fri_p1}:{Timetable.Fri_p2}:{Timetable.Fri_p3}:{Timetable.Fri_p4}:{Timetable.Fri_lunch}:{Timetable.Fri_p5}:{Timetable.Fri_p6}:{change}");
                break;
            }
            Timetable.timetable(File.ReadAllText(Timetable.form_path), File.ReadAllText(Timetable.monday_path).Split(":"), File.ReadAllText(Timetable.tuesday_path).Split(":"), File.ReadAllText(Timetable.wednesday_path).Split(":"), File.ReadAllText(Timetable.thursday_path).Split(":"), File.ReadAllText(Timetable.friday_path).Split(":"));
        }

        public static void Change_form()
        {
            write("What room is your new form?");
            string new_room = read(null).Trim();
            File.WriteAllText(Timetable.form_path, new_room);
            Timetable.Form = new_room;
        }

        public static Raylib_cs.Color BackgroundColour { get; set; }
        public static Raylib_cs.Color ForegroundColour { get; set; }

        private static volatile string text = string.Empty;
        public static string Text { get { return text; } set { text = value; } }

        public static void write(string what_to_write, bool new_line = true)
        {
            if(new_line)
            {
                Text = Text + what_to_write + "\n";
            } else {
                Text = Text + what_to_write;
            }
        }

        public static KeyboardKey GetKeyPressed()
        {
            KeyboardKey key;
            for(int i = 1; i <= 125; i++)
            {
                if(Raylib.IsKeyPressed((KeyboardKey)i))
                {
                    key = (KeyboardKey)i;
                    return key;
                }
            }
            return KeyboardKey.Null;
        }

        public static string read(char? replace)
        {
            string output = "";
            int key = 0;

            do {
                key = Raylib.GetCharPressed();
                if(key >= 32 && key <= 125)
                {
                    if(replace != null)
                    {
                        output += replace;
                        Text += replace;
                    } else {
                        output += (char)key;
                        Text += (char)key;
                    }
                }
                if(Raylib.IsKeyPressed(KeyboardKey.Backspace) && output.Length > 0)
                {
                    output = output.Substring(0, output.Length - 1);
                    Text = Text.Substring(0, Text.Length - 1);
                }
            } while(Raylib.IsKeyPressed(KeyboardKey.Enter) == false);
            Text += "\n";
            
            return output;
        }
        
        public static KeyboardKey read_key()
        {
            KeyboardKey key;
            do {
                key = (KeyboardKey)Raylib.GetKeyPressed();
            } while(key == 0);
            Text += key.ToString();
            return key;
        }

        public static void clear()
        {
            Text = "";
        }

        public static void Inverse()
        {
            if (BackgroundColour == Raylib_cs.Color.White)
            {
                BackgroundColour = Raylib_cs.Color.Black;
            } else
            {
                BackgroundColour = Raylib_cs.Color.White;
            }
            if (ForegroundColour == Raylib_cs.Color.White)
            {
                ForegroundColour = Raylib_cs.Color.Black;
            } else
            {
                ForegroundColour = Raylib_cs.Color.White;
            }
        }

        public static void back(int how_much_back)
        {
            string[] line_count = Text.Split('\n');
            int length_back = line_count.Length - how_much_back;
            if(length_back < 0) { Text = ""; ArgumentOutOfRangeException.ThrowIfZero(length_back); return; }
            Text = "";
            for(int i = 0; i < length_back; i++)
            {
                Text += (line_count[i] + "\n");
            }
        }

        static void Main(string[] args)
        {
            if (args.Any(argument => string.Equals(argument, "--web", StringComparison.OrdinalIgnoreCase)))
            {
                ArbotWebHost.Run(args);
                return;
            }

#if WINDOWS
            ArbotDesktop.Run(args);
#endif
            return;
        }
    }

    public class Info
    {
        public static string Name { get; set; } = string.Empty;
        public static string Password { get; set; } = string.Empty;
        public static int Positives { get; set; }
        public static int Negatives { get; set; }
        public static string Password_reset_token { get; set; } = string.Empty;

        public static string password_path = GetLocalPasswordPath();
        public static string name_path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "Info", "Name.txt");
        public static string positives_path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "Info", "Positives.txt");
        public static string negatives_path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "Info", "Negatives.txt");
        public static string password_reset_path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "Info", "Password_reset.txt");

        private static string GetLocalPasswordPath()
        {
            var localDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Arbot");
            Directory.CreateDirectory(localDirectory);
            var localPath = Path.Combine(localDirectory, "Password.txt");
            var defaultPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "Info", "Password.txt");
            if (!File.Exists(localPath)) File.Copy(defaultPath, localPath);
            return localPath;
        }

        public static void info(string name, string password, int positives, int negatives, string password_reset_token)
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
        public static string Form { get; set; } = string.Empty;
        public static string Mon_p1 { get; set; } = string.Empty;
        public static string Mon_p2 { get; set; } = string.Empty;
        public static string Mon_p3 { get; set; } = string.Empty;
        public static string Mon_p4 { get; set; } = string.Empty;
        public static string Mon_lunch { get; set; } = string.Empty;
        public static string Mon_p5 { get; set; } = string.Empty;
        public static string Mon_p6 { get; set; } = string.Empty;
        public static string Mon_home { get; set; } = string.Empty;

        public static string Tue_p1 { get; set; } = string.Empty;
        public static string Tue_p2 { get; set; } = string.Empty;
        public static string Tue_p3 { get; set; } = string.Empty;
        public static string Tue_p4 { get; set; } = string.Empty;
        public static string Tue_lunch { get; set; } = string.Empty;
        public static string Tue_p5 { get; set; } = string.Empty;
        public static string Tue_p6 { get; set; } = string.Empty;
        public static string Tue_home { get; set; } = string.Empty;

        public static string Wed_p1 { get; set; } = string.Empty;
        public static string Wed_p2 { get; set; } = string.Empty;
        public static string Wed_p3 { get; set; } = string.Empty;
        public static string Wed_p4 { get; set; } = string.Empty;
        public static string Wed_lunch { get; set; } = string.Empty;
        public static string Wed_p5 { get; set; } = string.Empty;
        public static string Wed_p6 { get; set; } = string.Empty;
        public static string Wed_home { get; set; } = string.Empty;

        public static string Thu_p1 { get; set; } = string.Empty;
        public static string Thu_p2 { get; set; } = string.Empty;
        public static string Thu_p3 { get; set; } = string.Empty;
        public static string Thu_p4 { get; set; } = string.Empty;
        public static string Thu_lunch { get; set; } = string.Empty;
        public static string Thu_p5 { get; set; } = string.Empty;
        public static string Thu_p6 { get; set; } = string.Empty;
        public static string Thu_home { get; set; } = string.Empty;

        public static string Fri_p1 { get; set; } = string.Empty;
        public static string Fri_p2 { get; set; } = string.Empty;
        public static string Fri_p3 { get; set; } = string.Empty;
        public static string Fri_p4 { get; set; } = string.Empty;
        public static string Fri_lunch { get; set; } = string.Empty;
        public static string Fri_p5 { get; set; } = string.Empty;
        public static string Fri_p6 { get; set; } = string.Empty;
        public static string Fri_home { get; set; } = string.Empty;

        public static string form_path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "Lessons", "Form.txt");
        public static string monday_path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "Lessons", "Monday.txt");
        public static string tuesday_path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "Lessons", "Tuesday.txt");
        public static string wednesday_path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "Lessons", "Wednesday.txt");
        public static string thursday_path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "Lessons", "Thursday.txt");
        public static string friday_path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "Lessons", "Friday.txt");

        public static void timetable(string form, string[] mon, string[] tue, string[] wed, string[] thu, string[] fri)
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
        public static bool Fast_load { get; set; }
        public static bool Name_or_master { get; set; }
        
        private static volatile bool dark_mode;
        public static bool Dark_mode { get { return dark_mode; } set { dark_mode = value; } }

        public static string Settings_path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "Info", "Settings.txt");

        public static void settings(string[] set)
        {
            Fast_load = bool.Parse(set[0]);
            Name_or_master = bool.Parse(set[1]);
            Dark_mode = bool.Parse(set[2]);
        }
    }
}
