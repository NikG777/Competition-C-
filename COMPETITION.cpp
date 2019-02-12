
#include <string>
#include <list>

using namespace Competitionspace
{

int Competition::_numberUser = 0;

	std::list<Competitor*> Competition::GetUsersList()
	{
		return _users;
	}

	Competition::Competition()
	{
		_nameCompetition = L"Произвольный конкурс";
		_countUsers = 5;
		_countWinners = 1;
		_numberUser = 1;
		_finish = 0;
		_regUsers = 0;
		_itogCompetition = 0;
		_users = std::list<Competitor*>();
	}

	std::wstring Competition::GetTextUser(bool separatorSpace)
	{
		std::wstring text = L"";
		std::wstring separator = L";";
		if (separatorSpace == true)
		{
			separator = L" ";
		}
		for (auto user : _users)
		{
			text += user->GetIndex() + separator + user->GetName() + separator + user->GetBall() + L"\r\n";
		}
		return text;
	}

	int Competition::GetCountUsers()
	{
		return _countUsers;
	}

	int Competition::GetCountWinners()
	{
		return _countWinners;
	}

	int Competition::GetNumberUser()
	{
		return _numberUser;
	}

	int Competition::GetRegUsers()
	{
		return _regUsers;
	}

	void Competition::SetRegUsers(int i)
	{
		_regUsers = i;
	}

	int Competition::GetItogCompetition()
	{
		return _itogCompetition;
	}

	void Competition::SetItogCompetition(int i)
	{
		_itogCompetition = i;
	}

	int Competition::GetFinish()
	{
		return _finish;
	}

	void Competition::IncrementNumberUser()
	{
		_numberUser = GetNumberUser() + 1;
	}

	std::wstring Competition::GetCompetition()
	{
		return _nameCompetition;
	}

	void Competition::SetNameCompetition(const std::wstring &nameCompetition)
	{
		this->_nameCompetition = nameCompetition;
	}

	void Competition::SetCountUsers(int countUsers)
	{
		this->_countUsers = countUsers;
	}

	void Competition::SetCountWinners(int countWinners)
	{
		this->_countWinners = countWinners;
	}

	void Competition::SetFinish(int i)
	{
		_finish = i;
	}

	void Competition::SortUsers()
	{
		//_users = _users.OrderByDescending(obj => obj.GetBall()).ToList();
	  //  int startindex = 0;
		int endIndex = _users.size() - 1;
		BubSort();
	}

	void Competition::BubSort()
	{
		Competitor *temp;
		for (int i = 0; i < _users.size(); i++)
		{
			for (int j = 1; j < (_users.size() - i); j++)
			{

				if (_users[j - 1]->GetBall() < _users[j]->GetBall())
				{
					temp = _users[j - 1];
					_users[j - 1] = _users[j];
					_users[j] = temp;
				}

			}
		}
	}

	void Competition::AddUser(const std::wstring &name, const std::wstring &ball, int index)
	{
		Competitor *us = new Competitor();
		us->SetIndex(index); // Добавляем индекс участника
		us->SetBall(std::stoi(ball));
		us->SetName(name);
		_users.push_back(us);
		StreamWriter *sw = new StreamWriter(_nameCompetition + L".txt", true);
		sw->WriteLine(std::to_wstring(index) + L";" + name + L";" + ball); // Записываем индекс, имя, балл
		sw->Close();
		// Увеличиваем ID последнего пользователя
		IncrementNumberUser();
		delete sw;
	}

	void Competition::SetBallUser(int user, int ball)
	{
		for (auto us : _users)
		{
			if (us->GetIndex() == user)
			{
				us->SetBall(ball);
			}
		}
	}

	void Competition::SetFinishRegUsers()
	{
		File::WriteAllText(_nameCompetition + L".txt", SettingsCompetition() + L"\r\n" + GetTextUser(false));
	}

	std::wstring Competition::AllUsers()
	{
		std::wstring gg = L"";
		for (auto user : _users)
		{
			gg += user->GetName() + L", " + user->GetBall() + L"\r\n";
		}
		return gg;
	}

	std::wstring Competition::Mess()
	{
		return _nameCompetition + std::to_wstring(_countUsers) + std::to_wstring(_countWinners);
	}

	void Competition::SetTxt()
	{
		if (!FileSystem::fileExists(_nameCompetition + L".txt"))
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

			StreamWriter *sww = new StreamWriter(_nameCompetition + L".txt");
			sww->WriteLine(GetCompetition() + L";" + std::to_wstring(GetCountUsers()) + L";" + std::to_wstring(GetCountWinners()) + L";" + std::to_wstring(0) + L";" + std::to_wstring(0) + L";" + std::to_wstring(0)); // Записываем индекс, имя, балл
			sww->Close();

			delete sww;
		}
	}

	DirectoryInfo *Competition::GetTxts()
	{
		DirectoryInfo *dir = new DirectoryInfo(FileSystem::getCurrentDirectory());

	}



int <missing_class_definition>::GetIndexUser(const std::wstring &line)
{
	StreamReader *sr = new StreamReader(_nameCompetition + L".txt");
	try
	{
		std::list<std::wstring> word;
		word = StringHelper::split(sr->ReadLine(), L';');
		sr->Close();

		delete sr;
		return std::stoi(word[1]);
	}
	catch (const NullReferenceException &e1)
	{
		sr->Close();

		delete sr;
		return 0;
	}

	delete sr;
}

void <missing_class_definition>::IncludeSettingsInFiles(const std::wstring &fileName)
{
	StreamReader *sr = new StreamReader(fileName + L".txt");
	try
	{
		std::list<std::wstring> word;
		word = StringHelper::split(sr->ReadLine(), L';');
		sr->Close();

		SetNameCompetition(word[0]);
		SetCountUsers(std::stoi(word[1]));
		SetCountWinners(std::stoi(word[2]));
		SetRegUsers(std::stoi(word[3]));
		SetFinish(std::stoi(word[4]));
		SetItogCompetition(std::stoi(word[5]));
	}
	catch (const NullReferenceException &e1)
	{
		sr->Close();
	}

	delete sr;
}

int <missing_class_definition>::GetCountUsersNow()
{
	std::list<std::wstring> lines = File::ReadAllLines(_nameCompetition + L".txt");
	return lines.size() - 1;
}

std::wstring <missing_class_definition>::SettingsCompetition()
{
	return GetCompetition() + L";" + GetCountUsers() + L";" + GetCountWinners() + L";" + GetRegUsers() + L";" + GetFinish() + L";" + GetItogCompetition();
}

std::wstring <missing_class_definition>::SettingsLastCompetition()
{
	return GetCompetition() + L";" + GetCountUsers() + L";" + GetCountWinners() + L";1;1;1";
}

void <missing_class_definition>::SetItogFile()
{
	std::wstring path = GetTxts().ToString();
	std::wstring subpath = LR"(Завершенные конкурсы)";
	DirectoryInfo *dirInfo = new DirectoryInfo(path);
	if (!dirInfo->Exists)
	{
		dirInfo->Create();
	}
	dirInfo->CreateSubdirectory(subpath);
	File::WriteAllLines(_nameCompetition + L".txt", GetWinners(), Encoding::Default);
	//var re = File.ReadAllLines(GetCompetition() + ".txt", Encoding.Default).Where(s =>!s.Contains(_nameCompetition));
   // File.WriteAllLines(GetCompetition() + ".txt", re, Encoding.Default);    
	FileSystem::renamePath(path + LR"(\)" + _nameCompetition + L".txt", path + LR"(\)" + subpath + LR"(\)" + _nameCompetition + L".txt");

	delete dirInfo;
}

void <missing_class_definition>::SetAllUserFromFile(const std::wstring &line)
{
	StreamReader *sr = new StreamReader(_nameCompetition + L".txt");
	int x = 0;
 using (sr)
	{
		if (!sr->EndOfStream)
		{
			sr->ReadLine();
		}
		while (!sr->EndOfStream)
		{
			if (x > 0)
			{
				Competitor *us = new Competitor();
				std::list<std::wstring> word;
				word = StringHelper::split(sr->ReadLine(), L';');
				///make new checks on s beacuse you getting substrings and indexes that may not exist///
				//e.g
				if (word.size() >= 1)
				{
					us->SetIndex(std::stoi(word[0])); // Добавляем индекс участника
					us->SetBall(std::stoi(word[2]));
					us->SetName(word[1]);
					_users->Add(us);
				}

			}
			++x;
		}
		sr->Close();
	}
	sr->Close();

	delete sr;
}

void <missing_class_definition>::DeleteUser(const std::wstring &user)
{
	StreamReader *sr = new StreamReader(GetCompetition() + L".txt");
	std::list<std::wstring> lines;
	std::wstring be = sr->ReadLine();
	sr->Close();
	auto re = File::ReadAllLines(GetCompetition() + L".txt", Encoding::Default).Where([&] (std::any s)
	{
	delete sr;
		return s != be && !s->Contains(user);
	});
	int i = re->ToList()->Count();
	lines.Insert(0, be);
	lines.InsertRange(1, re->ToList());
	File::WriteAllLines(GetCompetition() + L".txt", lines, Encoding::Default);

	delete sr;
}

std::list<std::wstring> <missing_class_definition>::GetWinners()
{
	std::wstring sr = GetTextUser(true);
	std::list<std::wstring> lst(StringHelper::split(sr, L'\n'));
	int p = GetCountWinners();
	for (int i = 0; i < GetCountWinners(); i++)
	{
		lst[i] = lst[i] + L" : " + std::to_wstring(i + 1) + L" - Победитель";
	}
	return lst;
}

