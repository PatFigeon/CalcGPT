#include <iostream> //R/W
#include <string> //Gives string support
#include <functional> //Gives operation support
#include <cmath> //What do you think?
#include <unordered_set> //Hashset
#include <random> //Funnily enough...
#include <thread> //Defines what a thread even is
#include <chrono> //Time

using namespace std;

float NumSelect()
{
    float inputNumber;
    try
    {
        cin >> inputNumber;
        if (cin.fail())
        {
            throw runtime_error("Invalid input");
        }
    }
    catch (const exception& e)
    {
        cout << "That's prolly not a number vro, try again." << endl;
        cin.clear();
        cin.ignore(10000, '\n');
        inputNumber = NumSelect();
    }
    return inputNumber;
}
int ChooseOperator(char operationParam)
{
    int operationSelect;
    char operation = operationParam;

    switch (operation)
    {
        default:
            cout << "That's not one of the operators vro, try again." << endl;
            cin >> operation;
            operationSelect = ChooseOperator(operation);
            break;
        case '+':
            operationSelect = 0;
            break;
        case '-':
            operationSelect = 1;
            break;
        case '*':
            operationSelect = 2;
            break;
        case '/':
            operationSelect = 3;
            break;
    }
    return operationSelect;
}
int GetUniqueGuess(float numIn, unordered_set<int>& history)
{
    int limit = static_cast<int>(floor(numIn));
    static random_device rd;
    static mt19937 gen(rd()); //Uses some random generation algorithm mt19937
    uniform_int_distribution<int> distrib(limit-20, limit+20);

    while (true)
    {
        int guess = distrib(gen);
        
        if (history.count(guess) == 0)
        {
            history.insert(guess); //If the guess is unique, add to set and return, otherwise recurse
            return guess;
        }
    }
}
int main()
{
    cout << "Yooooooo welcome to the calc (short for calculator). The answer should be close enough. Enter the first number:" << endl;
    float number1 = NumSelect();
    cout << "Which operation we doing? Let's have it" << endl;
    char operation;
    cin >> operation;
    int operationSelect = ChooseOperator(operation);
    cout << "Chuck in that second number then broseph:" << endl;
    float number2 = NumSelect();

    //Account for /0
    while (operationSelect == 2 && number2 == 0)
    {
        cout << "I see what you're doing there you rascal. Let's not divide by zero please. Put in a different divisor" << endl;
        number2 = NumSelect();
    }
    //Arbitrary input limit to spite the user
    if (number1 > 100 || number2 > 100 || number1 < -100 || number2 < -100)
    {
        cout << "I don't have that many fingers. I ain't doing allat";
        main();
    }

    int answer;

    switch (operationSelect)
    {
        case 0:
            answer = number1 + number2;
            break;
        case 1:
            answer = number1 - number2;
            break;
        case 2:
            answer = number1 * number2;
            break;
        case 3:
            answer = number1 / number2;
            break;
    }

    //Truncate that shit
    int trueAnswer = trunc(answer);

    cout << "Thinking..." << endl;

    unordered_set<int> previousGuesses;
    int guess;
    for (int i = 0; i < 25; i++)
    {
        guess = GetUniqueGuess(answer, previousGuesses);
        cout << "Generating guess..." << endl;
        cout << guess << endl;
        this_thread::sleep_for(chrono::milliseconds(500));
        if (guess == trueAnswer)
        {
            cout << "Finally, took a little bit but here's your answer :P\n";
            cout << "Your answer is prolly around the neighbourhood of " << guess << "." << endl;
            break;
        }
    }
    if (guess != trueAnswer)
    {
        cout << "Welp, I tried but it's too hard. Time to give up." << endl;
    }
    cout << "Do you want to run the calc again? y/n" << endl;
    char restart;
    cin >> restart;
    if (restart == 'y')
    {
        main();
    }
    else
    {
        exit(0);
    }
    return 0;
}