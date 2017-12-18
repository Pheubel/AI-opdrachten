#include <iostream>
#include <fstream>
#include <string>
#include <vector>

// to compile/run:
// g++ main.cpp -o binaryquestions;./binaryquestions

struct Node {

	std::string text;
	bool answer;
	int yes;
	int no;

	Node(std::string, int, int);
	Node(std::string);

};

Node::Node (std::string i) {

	answer = true;
	text = i;

}

Node::Node (std::string i, int y, int n) {

	answer = false;
	text = i;
	yes = y;
	no = n;

}

std::vector<Node*> list;

void loadData () {

	std::fstream file;
	std::ifstream fileChecker;

	fileChecker.open("data.txt");

	if (!fileChecker.good()) {

		file.open("data.txt", std::ios_base::in | std::ios_base::out | std::ios_base::app);

		file << "0?2,1,is it alive?\n";
		file << "1:cookie\n";
		file << "2?3,4,does it have 4 legs?\n";
		file << "3:cat\n";
		file << "4:ant\n";

	} else {

		file.open("data.txt", std::ios_base::in | std::ios_base::out | std::ios_base::app);

	}

	file.clear();
	file.seekg(file.beg);

	std::string line = "";
	int numLines = 0;

	while (getline(file, line)) numLines++;

	list.resize(numLines);

	file.clear();
	file.seekg(file.beg);

	Node* node;

	while (getline(file, line)) {

		for (int a = 0; a < line.size(); a++) {

			if (line[a] == '?') {

				int id = atoi(line.substr(0, a).c_str());
				int yes = -1;
				int no = -1;

				for (int b = a + 1; b < line.size(); b++) {

					if (yes == -1) {

						if (line[b] == ',') {

							yes = atoi(line.substr(a + 1, b).c_str());
							a = b;

						}

					} else if (line[b] == ',') {

						no = atoi(line.substr(a + 1, b).c_str());
						a = b;

						node = new Node(line.substr(a + 1, line.size()), yes, no);

						list[id] = node;

					}

				}

				break;

			} else if (line[a] == ':') {

				node = new Node(line.substr(a + 1, line.size()));

				list[atoi(line.substr(0, a).c_str())] = node;

				break;

			}

		}

	}

	file.close();

}

void saveToFile () {

	std::ofstream file;

	file.open("data.txt", std::ofstream::out | std::ofstream::trunc);

	for (int a = 0; a < list.size(); a++) {

		if (list[a]->answer) {

			file << a << ':' << list[a]->text << '\n';

		} else {

			file << a << '?' << list[a]->yes << ',' << list[a]->no << ',' << list[a]->text << '\n';

		}

	}

	file.close();

}

int main (int argc, char const *argv[]) {

	loadData();

	int current = 0;
	Node* node;
	std::string extraN = "aeuio";

	while (1) {

		bool addN = false;

		if (list[current]->answer) {

			for (int a = 0; a < extraN.size(); a++) {

				if (extraN[a] == list[current]->text[0]) {

					addN = true;
					break;

				}

			}

			std::cout << "is it a" << (addN ? "n " : " ") << list[current]->text << "?\n";

		} else {

			std::cout << list[current]->text << "\n";

		}

		std::string in;

		getline(std::cin, in);

		if (in == "exit" || in == "quit" || in == "q") break;

		if (list[current]->answer) {

			if (in == "yes" || in == "y") {

				std::cout << "i knew it. play again?\n";

				getline(std::cin, in);

				if (in == "exit" || in == "quit" || in == "q") break;
				if (in == "n" || in == "no" || in == "nope") break;

				current = 0;

			} else {

				std::cout << "what was it?\n";

				getline(std::cin, in);

				if (in == "exit" || in == "quit" || in == "q") break;

				node = new Node(in);

				list.resize(list.size() + 2);

				list[list.size() - 2] = node;

				std::cout << "i knew that, i was testing you.\n";
				std::cout << "what question could differentiate between a" << (addN ? "n " : " ");
				std::cout << list[current]->text << " and " << node->text << "?\n";
				std::cout << "(yes should yield " << node->text << ")\n";

				getline(std::cin, in);

				if (in == "exit" || in == "quit" || in == "q") break;

				std::string currentEnd = list[current]->text;

				node = new Node(in, list.size() - 2, list.size() - 1);

				list[current] = node;

				node = new Node(currentEnd);

				list[list.size() - 1] = node;

				current = 0;
				std::cout << "\nplay again?\n";

				saveToFile();

				getline(std::cin, in);

				if (in == "exit" || in == "quit" || in == "q") break;
				if (in == "n" || in == "no" || in == "nope") break;

				current = 0;

			}

		} else {

			if (in == "yes" || in == "y" || in == "aye") {

				current = list[current]->yes;

			} else if (in == "no" || in == "n" || in == "nope") {

				current = list[current]->no;

			}

		}

	}
	//
	// Node a ("ant");
	// Node b ("cookie");
	// Node c ("is it alive?", &a, &b);


	return 0;

}
