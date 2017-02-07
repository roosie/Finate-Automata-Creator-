using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace CCRE_Automaton
{
    public class State
    {
        private string state = null;

        //description: default constructor
        //input: new string state
        //output: none
        //notes:
        public State(string newName = "stateDefault")
        {
            state = newName;
        }

        //description: get state
        //input: none
        //output: string state
        //notes:
        public string getState()
        {
            return state;
        }

        //description: set state
        //input: new string state
        //output: none
        //notes:
        public void setState(string newState)
        {
            state = newState;
        }


        //description: overrides States = operator
        //input: States object
        //output: boolean
        //notes:
        public override bool Equals(object obj)
        {
            if (state == ((State)obj).state)
            {
                return true;
            }
            return false;
        }
    }

    public class Symbol
    {
        private string symbol = null;

        //description: default constructor
        //input: new string symbol
        //output: none
        //notes:
        public Symbol(string newSymbol = "symbolDefault")
        {
            symbol = newSymbol;
        }

        //description: get symbol
        //input: none
        //output: string symbol
        //notes:
        public string getSymbol()
        {
            return symbol;
        }

        //description: set symbol
        //input: new string symbol
        //output: none
        //notes:
        public void setSymbol(string newSymbol)
        {
            symbol = newSymbol;
        }

        //description: overrides Symbol = operator
        //input: Symbol object
        //output: boolean
        //notes:
        public override bool Equals(object obj)
        {
            if (symbol == ((Symbol)obj).symbol)
            {
                return true;
            }
            return false;
        }
    }

    public class Transition
    {
        private State source = null;
        private State destination = null;
        private Symbol symbol = null;

        //description: default constructor
        //input: new string source, string destination, string symbol
        //output: none
        //notes:
        public Transition(string newSource = "sourceDefault", string newDestination = "destinationDefault", string newSymbol = "symbolDefault")
        {
            source = new State(newSource);
            destination = new State(newDestination);
            symbol = new Symbol(newSymbol);

        }

        //description: get source
        //input: none
        //output: string source
        //notes:
        public string getSourceString()
        {
            return source.getState();
        }

        //description: set source
        //input: new string source
        //output: none
        //notes:
        public void setSourceString(string newSource)
        {
            source.setState(newSource);
        }

        //description: get destination
        //input: none
        //output: string destination
        //notes:
        public string getDestinationString()
        {
            return destination.getState();
        }


        //description: set destination
        //input: new string destination
        //output: none
        //notes:
        public void setDestinationString(string newDestination)
        {
            destination.setState(newDestination);
        }

        //description: get symbol
        //input: none
        //output: string symbol
        //notes:
        public string getSymbolString()
        {
            return symbol.getSymbol();
        }

        //description: set symbol
        //input: new string symbol
        //output: none
        //notes:
        public void setSymbolString(string newSymbol)
        {
            symbol.setSymbol(newSymbol);
        }

        //description: set transition
        //input: new string source, string destination, string symbol
        //output: none
        //notes: uses previous setter functions
        public void setTransition(string newSource, string newDestination, string newSymbol)
        {
            setSourceString(newSource);
            setDestinationString(newDestination);
            setSymbolString(newSymbol);
        }

        //description: get source
        //input: none
        //output: state source
        //notes:
        public State getSource()
        {
            return source;
        }

        //description: get symbol
        //input: none
        //output: symbol symbol
        //notes:
        public Symbol getSymbol()
        {
            return symbol;
        }

        //description: get destination
        //input: none
        //output: state destination
        //notes:
        public State getDestination()
        {
            return destination;
        }

        //description: overrides Transitions == operator
        //input: Transitions object
        //output: boolean
        //notes:
        public override bool Equals(object obj)
        {
            if (source != ((Transition)obj).source)
            {
                return false;
            }
            if (symbol != ((Transition)obj).symbol)
            {
                return false;
            }
            if (destination != ((Transition)obj).destination)
            {
                return false;
            }
            return true;
        }
    }

    public static class Automaton
    {
        public static List<State> States = new List<State>();
        public static List<Symbol> Alphabet = new List<Symbol>();
        public static State Initial = new State();
        public static List<State> Accepting = new List<State>();
        public static List<Transition> Transitions = new List<Transition>();

        //description: displays all attibutes of the automaton
        //input: none
        //output: none
        //notes:
        public static void displayAutomaton()
        {
            Console.Write("States: ");
            foreach (State x in States) Console.Write(x.getState() + ", ");     //display all states in states list

            Console.Write("\nAlphabet: ");
            foreach (Symbol x in Alphabet) Console.Write(x.getSymbol() + ", "); //display all symbols in alphabet list

            Console.Write("\nInitial State: " + Initial.getState());

            Console.Write("\nAccepting States: ");
            foreach (State x in Accepting) Console.Write(x.getState() + ", ");  //display all states in accepting states list

            Console.Write("\nTransition Set: ");                                //display tranistion set list as coded
            foreach (Transition x in Transitions) Console.Write(x.getSourceString() + ";" +
                                                                x.getSymbolString() + ";" +
                                                                x.getDestinationString() + ", ");

            Console.WriteLine();
        }

        //description: displays if an input is accepted, if not what error was found first
        //input: none
        //output: none
        //notes:
        public static void isAccepted()
        {
            Console.WriteLine("What is the input? Enter for example, if 0 and 1 are the symbols your input without brackets could be [0,0,1,1,0,1,0,1].");
            string input = Console.ReadLine(); //read input

            Console.Clear();
            string currentState = Initial.getState();
            string[] inputTransitions = input.Split(',');
            bool flag = false;

            Console.WriteLine("Input: " + input);
            Console.WriteLine("Transitions: ");

            foreach (string currentSymbol in inputTransitions)  //determine if input is approppiate according to transitions
            {
                flag = false;
                foreach (Transition x in Transitions)
                {
                    if (x.getSourceString() == currentState && currentSymbol == x.getSymbolString())
                    {
                        currentState = x.getDestinationString();

                        Console.WriteLine(x.getSourceString() + "--" + x.getSymbolString() + "-->" + x.getDestinationString()); //display each transition

                        flag = true; //set flag to true if transitions is accepted
                        break;
                    }
                }

                if (!flag) break; //if the current transition was not accepted, continue display error

            }

            if (!flag) //if a transition was not found, display error
            {
                Console.WriteLine("Transition error.");
            }

            else
            {
                flag = false;
                foreach (State x in Accepting) //determine if input ends on an accepting state
                {
                    if (x.getState() == currentState)
                    {
                        Console.WriteLine("Final accepting state: " + currentState);
                        flag = true;
                        break;
                    }
                }

                if (!flag) //if a transition was not found, break the loop
                {
                    Console.WriteLine("Accepting state error.");
                }
            }

            if (flag) Console.WriteLine("Automaton has been accepted the input."); //display acceptance result
            else Console.WriteLine("Automaton has NOT been accepted the input.");
            return;
        }
    }

    public class ErrorCheck
    {
        public ErrorCheck()
        {

        }

        //description: checks for initial state error
        //input: none
        //output: boolean
        //note:
        private bool checkInitial()
        {
            if (Automaton.States.Contains(Automaton.Initial)) return true; //check if initial state exists within states

            else
            {
                Console.WriteLine("Initial state not found within automaton states.");
                return false;
            }
        }

        //description: checks for accepting states error
        //input: none
        //output: boolean
        //note:
        private bool checkAccepting()
        {
            foreach (State x in Automaton.Accepting) //check if all accepting states exists within states
            {
                if (Automaton.States.Contains(x)) continue;

                else
                {
                    Console.WriteLine("Accepting state not found within automaton states.");
                    return false;
                }
            }

            return true;
        }

        //description: checks for transitions error
        //input: none
        //output: boolean
        //note:
        private bool checkTransitions()
        {
            foreach (Transition x in Automaton.Transitions) //check if all transitions exists within transitions
            {
                if (Automaton.States.Contains(x.getSource()) &&
                    Automaton.Alphabet.Contains(x.getSymbol()) &&
                    Automaton.States.Contains(x.getDestination())) continue;

                else
                {
                    Console.WriteLine("Transition element not found within automaton states or alphabet.");
                    return false;
                }
            }

            return true;
        }

        //description: checks for all errors
        //input: none
        //output: boolean
        //note:
        public bool checkAll()
        {
            if (!checkInitial() ||
                !checkAccepting() ||
                !checkTransitions())
            {
                Console.WriteLine("Automaton data has been deleted due to error(s).");

                Automaton.States.Clear();
                Automaton.Alphabet.Clear();
                Automaton.Initial.setState(string.Empty);
                Automaton.Accepting.Clear();
                Automaton.Transitions.Clear();

                return false;
            }

            return true;
        }
    }

    public class Interface
    {
        public Interface()
        {

        }

        //description: imports automaton data from xml file
        //input: none
        //output: none
        //notes
        public void xmlInput()
        {
            Console.WriteLine("Enter path to XML file: "); //prompt user for file name
            string xmlLocation = Console.ReadLine();

            Console.Clear();

            Automaton.States.Clear();       //delete existing automaton data
            Automaton.Alphabet.Clear();
            Automaton.Initial.setState("");
            Automaton.Accepting.Clear();
            Automaton.Transitions.Clear();


            XDocument xmlFile = XDocument.Load(xmlLocation); //use xmlLocation to open the file for xmlFile

            var list = xmlFile.Root.Elements("States").Elements("State").Select(element => element.Value).ToList();         //create a list of the elements tagged with state within the states tag
            foreach (string x in list)                                                                                      //add all states to the Automaton's states
                Automaton.States.Add(new State(x));

            list = xmlFile.Root.Elements("Alphabet").Elements("Symbol").Select(element => element.Value).ToList();          //create a list of the elements tagged with symbol within the alphabet tag
            foreach (string x in list)                                                                                      //add all symbols to the Automaton's alphabet
                Automaton.Alphabet.Add(new Symbol(x));

            list = xmlFile.Root.Elements("Initial").Elements("State").Select(element => element.Value).ToList();            //create a list of the elements tagged with state within the initial tag
            Automaton.Initial.setState(list[0]);                                                                            //add the state to the Automaton's initial state

            list = xmlFile.Root.Elements("Accepting").Elements("State").Select(element => element.Value).ToList();          //create a list of the elements tagged with state within the Accepting tag
            foreach (string x in list)                                                                                       //add all states to the Automaton's accepting states
                Automaton.Accepting.Add(new State(x));

            list = xmlFile.Root.Elements("Transitions").Elements("Transition").Select(element => element.Value).ToList();   //create a list of the elements tagged with transition within the transitions tag
            foreach (string x in list)                                                                                      //parse and add all transitions to the Automaton's transitions
            {
                string[] temp = x.Split(';');

                Automaton.Transitions.Add(new Transition(temp[0], temp[2], temp[1]));
            }
        }

        //description: scans automaton data from user
        //input: none
        //output: none
        //notes
        public void userInput()
        {
            Automaton.States.Clear();       //delete existing automaton data
            Automaton.Alphabet.Clear();
            Automaton.Initial.setState("");
            Automaton.Accepting.Clear();
            Automaton.Transitions.Clear();

            Console.WriteLine("What are the state names? (ex:A,B,C).");         //prompt user for states
            string line = Console.ReadLine();
            string[] list = line.Split(',');                                    //parse string by ',' into list
            foreach (string x in list)                                          //add all states to the Automaton's states
                Automaton.States.Add(new State(x));

            Console.WriteLine("What are the alphabet symbols? (ex:0,1,2).");    //prompt user for alphabet
            line = Console.ReadLine();
            list = line.Split(',');                                             //parse string by ',' into list
            foreach (string x in list)                                          //add all symbols to the Automaton's alphabet
                Automaton.Alphabet.Add(new Symbol(x));

            Console.WriteLine("What is the intial state? (ex:A).");             //prompt user for initial state
            line = Console.ReadLine();
            list[0] = line;
            Automaton.Initial.setState(list[0]);                                //add all states to the Automaton's states

            Console.WriteLine("What are the accepting states? (ex:B,C).");      //prompt user for accepting states
            line = Console.ReadLine();
            list = line.Split(',');                                             //parse string by ',' into list
            foreach (string x in list)                                          //add all states to the Automaton's accepting states
                Automaton.Accepting.Add(new State(x));

            Console.WriteLine("What are the transitions? (ex:A;0;B,B;1;C).");   //prompt user for transitions
            line = Console.ReadLine();
            list = line.Split(',');                                             //parse string by ',' into list
            foreach (string x in list)                                          //parse and add all transitions to the Automaton's transitions                       
            {
                string[] temp = x.Split(';');
                Automaton.Transitions.Add(new Transition(temp[0], temp[2], temp[1]));
            }

            Console.Clear();
        }

        //description: exports automaton data to xml file
        //input: none
        //output: none
        //notes
        public void xmlOutput()
        {
            Console.WriteLine("Enter name for XML file: "); //prompt user for file name
            string xmlLocation = Console.ReadLine();

            Console.Clear();

            //create new XDocument Variable with general tags
            XDocument xmlFile = new XDocument(new XElement("FSA",
                                                new XElement("States"),
                                                new XElement("Alphabet"),
                                                new XElement("Initial"),
                                                new XElement("Accepting"),
                                                new XElement("Transitions")));

            //add each state to FSA->States->
            foreach (var x in Automaton.States)
            {
                xmlFile.Element("FSA").Element("States").Add(new XElement("State", x.getState()));
            }

            //add each symbol to FSA->States->
            foreach (var x in Automaton.Alphabet)
            {
                xmlFile.Element("FSA").Element("Alphabet").Add(new XElement("Symbol", x.getSymbol()));
            }

            //add initial state to FSA->Initial
            xmlFile.Element("FSA").Element("Initial").Add(new XElement("State", Automaton.Initial.getState()));

            //add each accepting state to FSA->Accepting
            foreach (var x in Automaton.Accepting)
            {
                xmlFile.Element("FSA").Element("Accepting").Add(new XElement("State", x.getState()));
            }

            //add each transition to FSA->Transitions
            foreach (var x in Automaton.Transitions)
            {
                xmlFile.Element("FSA").Element("Transitions").Add(new XElement("Transition", (
                                                                    x.getSourceString() + ";" +
                                                                    x.getSymbolString() + ";" +
                                                                    x.getDestinationString())));
            }

            //save the file with user specified name without special format
            xmlFile.Save(xmlLocation, SaveOptions.None);
        }
    }

    public static class Program
    {

        static void Main()
        {
            Interface data = new Interface();    //declared an instances of class for functions
            ErrorCheck check = new ErrorCheck();

            do                                                              //infinite loop
            {

                Console.WriteLine("Select an option:");                     //menu
                Console.WriteLine(" 1. File defined automaton");
                Console.WriteLine(" 2. User defined automaton");
                Console.WriteLine(" 3. File export");
                Console.WriteLine(" 4. Display");
                Console.WriteLine(" 5. Run input");
                Console.WriteLine(" 6. Exit");
                string option = Console.ReadLine();

                Console.Clear();

                switch (option)
                {
                    case "1":
                        data.xmlInput();    //import from xml file
                        check.checkAll();   //check for data erros

                        Console.WriteLine("Automaton has been imported.");
                        Automaton.displayAutomaton();
                        break;
                    case "2":
                        data.userInput();   //scan from user input
                        check.checkAll();   //check for data erros

                        Console.WriteLine("Automaton has been entered.");
                        Automaton.displayAutomaton();
                        break;
                    case "3":
                        data.xmlOutput();

                        Console.WriteLine("Automaton has been exported.");
                        break;
                    case "4":

                        Automaton.displayAutomaton();
                        break;
                    case "5":

                        Automaton.isAccepted(); //check if automaton accepts a user input run
                        break;
                    case "6":

                        Console.WriteLine("Closing Program");
                        return;

                    default:
                        break;
                }

                Console.ReadLine();
                Console.Clear();
            } while (true);
        }
    }
}