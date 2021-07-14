using System;
using System.IO;

namespace BirthdayCall
{
    class Program
    {
        static void Main(string[] args)
        {
            //Задание реализовано в одном классе, одном методе. Сама программа - один большой оператор case спрятаный в цикле while из которого
            //конечно же выход есть. файл в который пишутся данные - находится в папке приложения ...BirthdayCall\bin\Debug\net5.0\DataBase.txt
            //пользование программой - крайне простое, вводишь цифры, потом если надо буквы. Ошибки могут возникнуть при не корректном вводе даты
            //и при использовании в имени символа "|" т.к. он разделитель полей в файле БД.  enjoy)
            int a;a = 0; 
            string writePath = @"DataBase.txt";
            Console.WriteLine("Вас приветствует поздравлятор");
            Console.WriteLine("Для начала работы введите цифру нужного пункта меню. ДР - день рождения.");
            while (a==0) {
                int caseSwitch;
                Console.WriteLine("Меню");
                Console.WriteLine("1)Показать список ближайших ДР");
                Console.WriteLine("2)Показать список всех ДР");
                Console.WriteLine("3)Добавить новую запись о ДР");
                Console.WriteLine("4)Редактировать запись о ДР");
                Console.WriteLine("5)Удалить запись");
                Console.WriteLine("6)Выход");
                caseSwitch = Convert.ToInt32(Console.ReadLine());
                switch (caseSwitch)
                {
                    case 1:
                        Console.WriteLine("Ближайшие дни рождения(до которых не более 7 дней):");
                        //string Date2 = Convert.ToString(DateTime.Today.Month);
                        int month = Convert.ToInt32(DateTime.Today.Month);
                        int day = Convert.ToInt32(DateTime.Today.Day);
                        
                        using (StreamReader sr = new StreamReader(writePath, System.Text.Encoding.Default))
                        {
                            string line;
                            while ((line = sr.ReadLine()) != null)
                            {
                                int line_day = Convert.ToInt32(line.Substring(line.IndexOf("|")+4,2));
                                int line_month = Convert.ToInt32(line.Substring(line.IndexOf("|") + 7, 2));
                                if (Math.Abs(month-line_month)==0 & Math.Abs(day -line_day)<=7 & line_day-day>=0)
                                {
                                    Console.WriteLine(line);
                                }
                                if (Math.Abs(month - line_month)== 11 & Math.Abs(day - line_day) >= 24)
                                {
                                    Console.WriteLine(line);
                                }

                            }
                        }

                        break;
                    case 2:
                        Console.WriteLine("Содержимое файла базы данных:");
                        using (StreamReader sr = new StreamReader(writePath, System.Text.Encoding.Default))
                        {
                            string line;
                            while ((line = sr.ReadLine()) != null)
                            {
                                Console.WriteLine(line);
                            }
                        }
                        
                        break;
                    case 3:
                        Console.WriteLine("Для выхода введите -1");int pass = 0; 
                        while (pass!=-1) {
                            Console.WriteLine("Введите ФИО:"); string name = Console.ReadLine();
                            Console.WriteLine("Введите дату рождения:"); string date = Console.ReadLine();
                            string text = "Имя:"+name +"|ДР:"+ date;
                            if (name == "-1") break;
                            try
                            {
                                using (StreamWriter sw = new StreamWriter(writePath, true, System.Text.Encoding.Default))
                                { sw.WriteLineAsync(text);  }
                            }
                            catch (Exception e) { Console.WriteLine(e.Message); }
                            
                        }
                        break;
                    case 4:
                        Console.WriteLine("введите номер записи которую хотите редактировать:");
                        int number=Convert.ToInt32(Console.ReadLine());
                        int cheker = 0;
                        string theText = "";
                        using (StreamReader sr = new StreamReader(writePath, System.Text.Encoding.Default))
                        {
                            string line;
                            while ((line = sr.ReadLine()) != null)
                            {
                                theText = theText + line+ '\n';
                                if (cheker==number) {
                                    string newSTR = line;
                                    Console.WriteLine("текущая строка:"+newSTR);
                                    Console.Write("введите новую фамилию:");
                                    string newnewSTR = Console.ReadLine();
                                    newnewSTR += "|ДР:";
                                    Console.Write("введите новую дату:");
                                    newnewSTR += Console.ReadLine();
                                    theText = theText.Substring(0,theText.Length-line.Length) + "Имя:"+newnewSTR+ '\n';
                                }
                                cheker++; 
                            }
                        }
                        try
                        {using (StreamWriter sw = new StreamWriter(writePath, false))
                            {
                                sw.Write(theText);
                            }
                        }
                        catch (Exception e){ Console.WriteLine(e.Message);}

                        break;
                    case 5:
                        Console.WriteLine("введите номер записи которую хотите удалить:");
                        int number2 = Convert.ToInt32(Console.ReadLine());
                        int cheker2 = 0;
                        string theText2="";
                        using (StreamReader sr = new StreamReader(writePath, System.Text.Encoding.Default))
                        {
                            string line;
                            while ((line = sr.ReadLine()) != null)
                            {
                                theText2 = theText2 + line+ '\n';
                                if (cheker2 == number2)
                                {
                                   theText2 = theText2.Substring(0, theText2.Length - line.Length);
                                }
                                cheker2++;
                            }
                        }
                        try
                        {
                            using (StreamWriter sw = new StreamWriter(writePath, false))
                            {
                                sw.Write(theText2);
                            }
                        }
                        catch (Exception e) { Console.WriteLine(e.Message); }
                        break;
                    case 6:
                        a = 1;
                        break;
                    default:
                        Console.WriteLine("Введен не корректный символ!");
                        break;
                }


            }

            
        }
    }
}
