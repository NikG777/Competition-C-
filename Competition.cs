using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Competitorspace;

namespace Competitionspace
{
    class Competition
    {
        // Количество участников
        private int _countUsers;

        // Количество победителей
        private int _countWinners;

        // Счётчик номера участника
        private static int _numberUser;

        // Название конкурса
        private string _nameCompetition;

        // Возможность регистрации пользователей
        private int _regUsers;

        // Итог конкурса
        private int _itogCompetition;

        // Проведен ли конкурс
        private int _finish;

        // Список участников 
        private List<Competitor> _users;

        public List<Competitor> GetUsersList()
        {
            return _users;
        }

        // Конструктор по умолчанию
        public Competition()
        {
            _nameCompetition = "Произвольный конкурс";
            _countUsers = 5;
            _countWinners = 1;
            _numberUser = 1;
            _finish = 0;
            _regUsers = 0;
            _itogCompetition = 0;
            _users = new List<Competitor>();
        }

        // Метод вывода списка участников в виде текста ( для файла при сохранении готового конкурса)
        public string GetTextUser(bool separatorSpace)
        {
            string text = "";
            string separator = ";";
            if(separatorSpace == true)
            {
                separator = " ";
            }
            foreach (var user in _users)
            {
                text += user.GetIndex() + separator + user.GetName() + separator + user.GetBall() + "\r\n";
            }
            return text;
        }

        // Получить количество участников 
        public int GetCountUsers()
        {
            return _countUsers;
        }

        // Получить количество победителей 
        public int GetCountWinners()
        {
            return _countWinners;
        }

        // Получение id последнего пользователя
        public int GetNumberUser()
        {
            return _numberUser;
        }

        // Получить значения о возможности регистрации участников
        public int GetRegUsers()
        {
            return _regUsers;
        }

        // Установить значения о не возможности регистрации участников
        public void SetRegUsers(int i)
        {
            _regUsers = i;
        }

        // Получить значения о подвидении итога
        public int GetItogCompetition()
        {
            return _itogCompetition;
        }

        // Установить значения о подвидении итога
        public void SetItogCompetition(int i)
        {
            _itogCompetition = i;
        }

        // Получение значение о завершение конкурса
        public int GetFinish()
        {
            return _finish;
        }

        // Увеличение счетчика id последнего пользователя
        public void IncrementNumberUser()
        {
            _numberUser = GetNumberUser() + 1;
        }


        // Получить название конкурса
        public string GetCompetition()
        {
            return _nameCompetition;
        }

        // Установка названия конкурса
        public void SetNameCompetition(string nameCompetition)
        {
            this._nameCompetition = nameCompetition;
        }

        // Установка количество участников
        public void SetCountUsers(int countUsers)
        {
            this._countUsers = countUsers;
        }

        // Установка количество победителей
        public void SetCountWinners(int countWinners)
        {
            this._countWinners = countWinners;
        }

        // Завершение конкурса
        public void SetFinish(int i)
        {
            _finish = i;
        }

        // Сортировка участников по баллам
        public void SortUsers()
        {
            //_users = _users.OrderByDescending(obj => obj.GetBall()).ToList();
          //  int startindex = 0;
            int endIndex = _users.Count - 1;
            BubSort();
        }
        public void BubSort()
        {
            Competitor temp;
            for (int i = 0; i < _users.Count; i++)
            {
                for (int j = 1; j < (_users.Count - i); j++)
                {

                    if (_users[j - 1].GetBall() < _users[j].GetBall())
                    {
                        temp = _users[j - 1];
                        _users[j - 1] = _users[j];
                        _users[j] = temp;
                    }

                }
            }
        }

        // Регистрация участника
        public void AddUser(string name, string ball, int index)
        {
            Competitor us = new Competitor();

            us.SetIndex(index); // Добавляем индекс участника 
            us.SetBall(Int32.Parse(ball));
            us.SetName(name);
            _users.Add(us);
            StreamWriter sw = new StreamWriter(_nameCompetition + ".txt", true);
            sw.WriteLine(index + ";" + name + ";" + ball); // Записываем индекс, имя, балл                                            
            sw.Close();
            // Увеличиваем ID последнего пользователя
            IncrementNumberUser();
        }

        // Установить балл участинику
        public void SetBallUser(int user, int ball)
        {
            foreach (var us in _users)
            {
                if(us.GetIndex() == user)
                {
                    us.SetBall(ball);
                }
            }
        }

        // Изменение настроек файла конкурса, регистрации пользователей завершена
        public void SetFinishRegUsers()
        {
            File.WriteAllText(_nameCompetition + ".txt", SettingsCompetition() + "\r\n" + GetTextUser(false));
        }

        // Что они делают эти два?
        public string AllUsers()
        {
            string gg = "";
            foreach (var user in _users)
            {
                gg += user.GetName() + ", " + user.GetBall() + "\r\n";
            }
            return gg;
        }

        public string Mess()
        {
            return _nameCompetition + _countUsers + _countWinners;
        }


        // -------------------------------------------
        // Создание основного файла конкурса
        public void SetTxt()
        {
            if (!System.IO.File.Exists(_nameCompetition + ".txt"))
            {

                // Настройки файла (первая строка) >>>
                /* 
                 * Название конкурса (Супер конкурс)
                 * Количество участников (11)
                 * Количество победителей (1)
                 * Завершена ли регистрация участников (1 да)
                 * Завершен ли конкурс (1 да)
                 * Подвели ли итоги конкурса (1 да)
                */

                StreamWriter sww = new StreamWriter(_nameCompetition + ".txt");
                sww.WriteLine(GetCompetition() + ";" +
                              GetCountUsers() + ";" +
                              GetCountWinners() + ";" +
                              0 + ";" +
                              0 + ";" +
                              0); // Записываем индекс, имя, балл 
                sww.Close();
            }
        }

        // Считывание директории в которой находится программа  
        public DirectoryInfo GetTxts()
        {
            DirectoryInfo dir = new DirectoryInfo(Directory.GetCurrentDirectory());
            return dir;
        }

        // Получение индекса последнего участника
        public int GetIndexUser(string line)
        {
            StreamReader sr = new StreamReader(_nameCompetition + ".txt");
            try
            {
                string[] word;
                word = sr.ReadLine().Split(';');
                sr.Close();
                return Convert.ToInt32(word[1]);
            }
            catch (NullReferenceException)
            {
                sr.Close();
                return 0;
            }
        }

        // Настройки конкурса из файла
        public void IncludeSettingsInFiles(string fileName)
        {
            StreamReader sr = new StreamReader(fileName + ".txt");
            try
            {
                string[] word;
                word = sr.ReadLine().Split(';');
                sr.Close();

                SetNameCompetition(word[0]);
                SetCountUsers(Convert.ToInt32(word[1]));
                SetCountWinners(Convert.ToInt32(word[2]));
                SetRegUsers(Convert.ToInt32(word[3]));
                SetFinish(Convert.ToInt32(word[4]));
                SetItogCompetition(Convert.ToInt32(word[5]));
            }
            catch (NullReferenceException)
            {
                sr.Close();
            }
        }

        // Какое количество участников уже участвует
        public int GetCountUsersNow()
        {
            string[] lines = File.ReadAllLines(_nameCompetition + ".txt");
            return lines.Length - 1;
        }

        // Вывести текущие настройки конкурса
        public string SettingsCompetition()
        {
            return 
                GetCompetition() +  ";" + 
                GetCountUsers() +   ";" + 
                GetCountWinners() + ";" + 
                GetRegUsers() +     ";" +
                GetFinish() +       ";" +
                GetItogCompetition();
        }

        // Вывести настройки конкурса
        public string SettingsLastCompetition()
        {
            return GetCompetition() + ";" + GetCountUsers() + ";" + GetCountWinners() + ";1;1;1";
        }

        // Перезаписываем файл с итоговыми данными в отдельную директорию 
        public void SetItogFile()
        {
            string path = GetTxts().ToString();
            string subpath = @"Завершенные конкурсы";
            DirectoryInfo dirInfo = new DirectoryInfo(path);
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }
            dirInfo.CreateSubdirectory(subpath);
            File.WriteAllLines(_nameCompetition + ".txt", GetWinners(), Encoding.Default);
            //var re = File.ReadAllLines(GetCompetition() + ".txt", Encoding.Default).Where(s =>!s.Contains(_nameCompetition));
           // File.WriteAllLines(GetCompetition() + ".txt", re, Encoding.Default);    
            File.Move( path + @"\" + _nameCompetition + ".txt", path + @"\" + subpath + @"\"+ _nameCompetition + ".txt");
        }

        // Вывести список участников из файла
        public void SetAllUserFromFile(string line)
        {
            StreamReader sr = new StreamReader(_nameCompetition + ".txt");
            int x = 0;
            using (sr)
            {
                if (!sr.EndOfStream)
                    sr.ReadLine();
                while (!sr.EndOfStream)
                {
                    if(x > 0)
                    {
                        Competitor us = new Competitor();
                        string[] word;
                        word = sr.ReadLine().Split(';');
                        ///make new checks on s beacuse you getting substrings and indexes that may not exist///
                        //e.g
                        if (word.Length >= 1)
                        {
                            us.SetIndex(Convert.ToInt32(word[0])); // Добавляем индекс участника 
                            us.SetBall(Convert.ToInt32(word[2]));
                            us.SetName(word[1]);
                            _users.Add(us);
                        }
                    }
                    ++x;
                }
                sr.Close();
            }
            sr.Close();
        }

        // Удаление пользователя 
        public void DeleteUser(string user)
        {
            StreamReader sr = new StreamReader(GetCompetition() + ".txt");
            List<string> lines = new List<string>();
            string be = sr.ReadLine();
            sr.Close();
            var re = File.ReadAllLines(GetCompetition() + ".txt", Encoding.Default).Where(s => s != be && !s.Contains(user));
            int i = re.ToList().Count();
            lines.Insert(0, be);
            lines.InsertRange(1, re.ToList());
            File.WriteAllLines(GetCompetition() + ".txt", lines, Encoding.Default);
        }
        // Запись победителей в файлик
        public List<string> GetWinners()
        {
            string sr = GetTextUser(true);
            List<string> lst = new List<string>(sr.Split('\n'));
            int p = GetCountWinners();
            for(int i = 0; i < GetCountWinners(); i++)
            {
                lst[i] = lst[i] +" : " + (i+1) + " - Победитель"; 
            }
            return lst;
        }
    }
}
