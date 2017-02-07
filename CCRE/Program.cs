using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
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
        public State(string newName = "")
        {
            state = newName;
        }

        public State(State copy)
        {
            state = copy.state;
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
        public Symbol(string newSymbol = "")
        {
            symbol = newSymbol;
        }

        public Symbol(Symbol copy)
        {
            symbol = copy.symbol;
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
        public Transition(string newSource = "", string newDestination = "", string newSymbol = "")
        {
            source = new State(newSource);
            destination = new State(newDestination);
            symbol = new Symbol(newSymbol);

        }

        public Transition(Transition copy)
        {
            source = copy.source;
            destination = copy.destination;
            symbol = copy.symbol;
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

    public class Automaton
    {
        public List<State> States;
        public List<Symbol> Alphabet;
        public State Initial;
        public List<State> Accepting;
        public List<Transition> Transitions;
        public string Input;

        public Automaton()
        {
            States = new List<State>();
            Alphabet = new List<Symbol>();
            Initial = new State();
            Accepting = new List<State>();
            Transitions = new List<Transition>();
            Input = null;
        }

        public Automaton(Automaton copy)
        {
            States = copy.States;
            Alphabet = copy.Alphabet;
            Initial = copy.Initial;
            Transitions = copy.Transitions;
            Input = copy.Input;
        }

        //description: displays all attibutes of the automaton
        //input: none
        //output: none
        //notes:
        public void displayAutomaton()
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
        public bool isAccepeted(string input)
        {
            //Console.WriteLine("What is the input? Enter for example, if 0 and 1 are the symbols your input without brackets could be [0,0,1,1,0,1,0,1].");
            //string input = Console.ReadLine(); //read input

            //Console.Clear();
            string currentState = Initial.getState();
            string[] inputTransitions = input.Split(',');
            bool flag = false;

            //Console.WriteLine("Input: " + input);
            //Console.WriteLine("Transitions: ");

            foreach (string currentSymbol in inputTransitions)  //determine if input is approppiate according to transitions
            {
                flag = false;
                foreach (Transition x in Transitions)
                {
                    if (x.getSourceString() == currentState && currentSymbol == x.getSymbolString())
                    {
                        currentState = x.getDestinationString();

                        //Console.WriteLine(x.getSourceString() + "--" + x.getSymbolString() + "-->" + x.getDestinationString()); //display each transition

                        flag = true; //set flag to true if transitions is accepted
                        break;
                    }
                }

                if (!flag) break; //if the current transition was not accepted, continue display error

            }

            if (!flag) //if a transition was not found, display error
            {
               // Console.WriteLine("Transition error.");
            }

            else
            {
                flag = false;
                foreach (State x in Accepting) //determine if input ends on an accepting state
                {
                    if (x.getState() == currentState)
                    {
                        //Console.WriteLine("Final accepting state: " + currentState);
                        flag = true;
                        break;
                    }
                }

                if (!flag) //if a transition was not found, break the loop
                {
                    //Console.WriteLine("Accepting state error.");
                }
            }

            if (flag) return true;//Console.WriteLine("Automaton has been accepted the input."); //display acceptance result
            else //Console.WriteLine("Automaton has NOT been accepted the input.");
                return false;
        }
    }

    public static class ErrorCheck
    {
        //description: checks for initial state error
        //input: Automaton
        //output: boolean
        //note:
        public static bool checkInitial(Automaton finiteAutomaton)
        {
            if (finiteAutomaton.States.Contains(finiteAutomaton.Initial)) return true; //check if initial state exists within states

            else
            {
                //Console.WriteLine("Initial state not found within automaton states.");
                return false;
            }
        }

        //description: checks for accepting states error
        //input: Automaton
        //output: boolean
        //note:
        public static bool checkAccepting(Automaton finiteAutomaton)
        {
            foreach (State x in finiteAutomaton.Accepting) //check if all accepting states exists within states
            {
                if (finiteAutomaton.States.Contains(x)) continue;

                else
                {
                    //Console.WriteLine("Accepting state not found within automaton states.");
                    return false;
                }
            }

            return true;
        }

        //description: checks for transitions error
        //input: Automaton
        //output: boolean
        //note:
        public static bool checkTransitions(Automaton finiteAutomaton)
        {
            foreach (Transition x in finiteAutomaton.Transitions) //check if all transitions exists within transitions
            {
                if (finiteAutomaton.States.Contains(x.getSource()) &&
                    finiteAutomaton.Alphabet.Contains(x.getSymbol()) &&
                    finiteAutomaton.States.Contains(x.getDestination())) continue;

                else
                {
                    //Console.WriteLine("Transition element not found within automaton states or alphabet.");
                    return false;
                }
            }

            return true;
        }

        //description: checks for all errors
        //input: Automaton
        //output: boolean
        //note:
        public static bool checkAll(Automaton finiteAutomaton)
        {
            if (!checkInitial(finiteAutomaton) ||
                !checkAccepting(finiteAutomaton) ||
                !checkTransitions(finiteAutomaton))
            {
               // Console.WriteLine("Automaton data has been deleted due to error(s).");

                finiteAutomaton.States.Clear();
                finiteAutomaton.Alphabet.Clear();
                finiteAutomaton.Initial.setState(string.Empty);
                finiteAutomaton.Accepting.Clear();
                finiteAutomaton.Transitions.Clear();

                return false;
            }

            return true;
        }
    }

    public static class Data
    {
        //description: imports automaton data from xml file
        //input: Automaton
        //output: none
        //notes
        public static Automaton xmlInput(string xmlLocation)
        {
            //Console.WriteLine("Enter path to XML file: "); //prompt user for file name
            //string xmlLocation = Console.ReadLine();

            //Console.Clear();

            Automaton tmpAutomaton = new Automaton();



            XDocument xmlFile = null;

            xmlFile = XDocument.Load(xmlLocation); //use xmlLocation to open the file for xmlFile
            var result = from q in xmlFile.Descendants("xml")
                         select new
                         {
                             states = q.Element("States"),
                             alpha = q.Element("Alphabet"),
                             initial = q.Element("Initial"),
                             accept = q.Element("Accepting"),
                             trans = q.Element("Transitions")
                         };
            foreach (var thing in result)
            {
                string s8 = (string)thing.states;
                string[] s = s8.Split(',');
                foreach (string x in s)                                                                                      //add all states to the Automaton's states
                    tmpAutomaton.States.Add(new State(x));


                s8 = (string)thing.alpha;
                s = s8.Split(',');
                foreach (string x in s)                                                                                      //add all symbols to the Automaton's alphabet
                    tmpAutomaton.Alphabet.Add(new Symbol(x));


                tmpAutomaton.Initial.setState((string)thing.initial);

                s8 = (string)thing.accept;
                s = s8.Split(',');
                foreach (string x in s)
                    tmpAutomaton.Accepting.Add(new State(x));

                s8 = (string)thing.trans;
                s = s8.Split(',');
                foreach (string x in s)                                                                                      //parse and add all transitions to the Automaton's transitions
                {
                    string[] temp = x.Split(';');
                    if (x != string.Empty)
                    {
                        tmpAutomaton.Transitions.Add(new Transition(temp[0], temp[2], temp[1]));
                    }
                }
                //if (ErrorCheck.checkAll(tmpAutomaton))   //check for data erros
                //{
                //Console.WriteLine("Automaton has been imported.");

                //}

                //return finiteAutomaton;
            }

            return tmpAutomaton;
        }

        //description: scans automaton data from user
        //input: Automaton
        //output: none
        //notes
        public static Automaton userInput(string states, string alphabet, string initial, string accepting, string transition, string input)
        {
            Automaton tmpAutomaton = new Automaton();

            char[] delim = { '\n', '\r'};

            //Console.WriteLine("What are the state names? (ex:A,B,C).");         //prompt user for states
            //string line = Console.ReadLine();
            //string[] list = line.Split(',');                                    //parse string by ',' into list
            string[] list = states.Split(delim);
            foreach (string x in list)                                          //add all states to the Automaton's states
                if(x != "") tmpAutomaton.States.Add(new State(x));

            //Console.WriteLine("What are the alphabet symbols? (ex:0,1,2).");    //prompt user for alphabet
            //line = Console.ReadLine();
            // list = line.Split(',');                                             //parse string by ',' into list
            list = alphabet.Split(delim);
            foreach (string x in list)                                          //add all symbols to the Automaton's alphabet
                if (x != "") tmpAutomaton.Alphabet.Add(new Symbol(x));

            //Console.WriteLine("What is the intial state? (ex:A).");             //prompt user for initial state
            //line = Console.ReadLine();
            tmpAutomaton.Initial.setState(initial);                                //add all states to the Automaton's states

            //Console.WriteLine("What are the accepting states? (ex:B,C).");      //prompt user for accepting states
            //line = Console.ReadLine();
            //list = line.Split(',');                                             //parse string by ',' into list
            list = accepting.Split(delim);
            foreach (string x in list)                                          //add all states to the Automaton's accepting states
                if (x != "") tmpAutomaton.Accepting.Add(new State(x));

            //Console.WriteLine("What are the transitions? (ex:A;0;B,B;1;C).");   //prompt user for transitions
            //line = Console.ReadLine();
            //list = line.Split(',');                                             //parse string by ',' into list
            list = transition.Split(delim);
            foreach (string x in list)                                          //parse and add all transitions to the Automaton's transitions                       
            {
                if (x != "")
                {
                    string[] temp = x.Split(';');
                    tmpAutomaton.Transitions.Add(new Transition(temp[0], temp[2], temp[1]));
                }
            }

            //Console.WriteLine("What is a single run? (ex:0,0,1,0,1,1.");   //prompt user for transitions
            //line = Console.ReadLine();
            tmpAutomaton.Input = input;                                //add all states to the Automaton's states


            //Console.Clear();

            //if (ErrorCheck.checkAll(tmpAutomaton))   //check for data erros
            //{
                //Console.WriteLine("Automaton has been imported.");
                return tmpAutomaton;
           // }

            //finiteAutomaton.displayAutomaton();
            //return finiteAutomaton;
        }

        //description: exports automaton data to xml file
        //input: Automaton
        //output: none
        //notes
        public static void xmlOutput(Automaton finiteAutomaton, string xmlLocation)
        {

            using (FileStream fileStream = new FileStream(xmlLocation, FileMode.Create))
            using (StreamWriter sw = new StreamWriter(fileStream))
            using (XmlTextWriter writer = new XmlTextWriter(sw))
            {
                writer.Formatting = Formatting.Indented;
                writer.Indentation = 4;
                writer.WriteStartElement("xml");
                string s = "";
                foreach (var x in finiteAutomaton.States)
                {
                    s += x.getState() + ',';

                }
                writer.WriteElementString("States", s);
                s = finiteAutomaton.Initial.getState();
                writer.WriteElementString("Initial", s);
                s = string.Empty;
                foreach (var x in finiteAutomaton.Accepting)
                {
                    s += x.getState() + ',';
                }

                writer.WriteElementString("Accepting", s);

                s = string.Empty;
                foreach (var x in finiteAutomaton.Transitions)
                {
                    s += x.getSourceString() + ";" + x.getSymbolString() + ";" + x.getDestinationString() + ',';
                }

                writer.WriteElementString("Transitions", s);

                s = string.Empty;
                foreach (var x in finiteAutomaton.Alphabet)
                {
                    s += x.getSymbol() + ',';
                }

                writer.WriteElementString("Alphabet", s);

                //Console.WriteLine("Enter name for XML file: "); //prompt user for file name
                //string xmlLocation = Console.ReadLine();

                //Console.Clear();

                //create new XDocument Variable with general tags
                //add each state to FSA->States->

                /*

                //add each accepting state to FSA->Accepting

                //add each transition to FSA->Transitions
                foreach (var x in finiteAutomaton.Transitions)
                {
                    xmlFile.Element("FSA").Element("Transitions").Add(new XElement("Transition", (
                                                                        x.getSourceString() + ";" +
                                                                        x.getSymbolString() + ";" +
                                                                        x.getDestinationString())));
                }

                //save the file with user specified name without special format
                xmlFile.Save(xmlLocation, SaveOptions.None);
                */
                //Console.WriteLine("Automaton has been exported.");
            }
        }
    }

    public class Program
    {

        static void Main()
        {
            Automaton finiteAutomaton = new Automaton();

            do                                                              //infinite loop
            {

                Console.WriteLine("Select an option:");                     //menu
                Console.WriteLine(" 1. Define new automaton from file");
                Console.WriteLine(" 2. Define new automaton from input");
                Console.WriteLine(" 3. File export");
                Console.WriteLine(" 4. Display automaton");
                Console.WriteLine(" 5. Run input from user");
                Console.WriteLine(" 6. Run input from file");
                Console.WriteLine(" 7. Exit");
                string option = Console.ReadLine();

                Console.Clear();

                switch (option)
                {
                    case "1":
                        //Data.xmlInput(finiteAutomaton);    //import from xml file
                        break;
                    case "2":
                        //Data.userInput(finiteAutomaton);   //scan from user input
                        break;
                    case "3":
                        //Data.xmlOutput(finiteAutomaton);
                        break;
                    case "4":
                        //finiteAutomaton.displayAutomaton();
                        break;
                    case "5":
                        //finiteAutomaton.userAccepeted(); //check if automaton accepts a user input run
                        break;
                    case "6":

                        //finiteAutomaton.fileAccepeted(); //check if automaton accepts file input run(s)
                        break;
                    case "7":

                        //Console.WriteLine("Closing Program");
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