using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CCRE_Automaton;

namespace GUI
{
    public partial class Form1 : Form
    {
        public Automaton finiteAutomaton;

        public Form1()
        {
            InitializeComponent();
            finiteAutomaton = new Automaton();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //not sure
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            //states
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            //transitions
        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {
            //symbols
        }

        private void textBox15_TextChanged(object sender, EventArgs e)
        {
            //intial
        }

        private void textBox16_TextChanged(object sender, EventArgs e)
        {
            //accepting
        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {
            //user input
        }

        private void textBox12_TextChanged(object sender, EventArgs e)
        {
            //movement
        }

        //transition movement
        private void button2_Click(object sender, EventArgs e)
        {
            //Console.WriteLine("What is the input? Enter for example, if 0 and 1 are the symbols your input without brackets could be [0,0,1,1,0,1,0,1].");
            //string input = Console.ReadLine(); //read input

            textBox12.Clear();
            textBox19.Clear();
            textBox17.Clear();

            //Console.Clear();
            string currentState = finiteAutomaton.Initial.getState();
            string[] inputTransitions = textBox11.Text.Split(',');
            bool flag = false;

            //Console.WriteLine("Input: " + input);
            //Console.WriteLine("Transitions: ");

            foreach (string currentSymbol in inputTransitions)  //determine if input is approppiate according to transitions
            {
                flag = false;
                foreach (Transition x in finiteAutomaton.Transitions)
                {
                    if (x.getSourceString() == currentState && currentSymbol == x.getSymbolString())
                    {
                        currentState = x.getDestinationString();

                        //Console.WriteLine(x.getSourceString() + "--" + x.getSymbolString() + "-->" + x.getDestinationString()); //display each transition
                        textBox12.AppendText(x.getSourceString() + "--" + x.getSymbolString() + "-->" + x.getDestinationString() + "\r\n");


                        flag = true; //set flag to true if transitions is accepted
                        break;
                    }
                }

                if (!flag) break; //if the current transition was not accepted, continue display error

            }

            if (!flag) //if a transition was not found, display error
            {
                // Console.WriteLine("Transition error.");
                textBox19.AppendText("Transition error\r\n");
            }

            else
            {
                flag = false;
                foreach (State x in finiteAutomaton.Accepting) //determine if input ends on an accepting state
                {
                    if (x.getState() == currentState)
                    {
                        //Console.WriteLine("Final accepting state: " + currentState);
                        textBox12.AppendText("Final accepting state: " + "currentState\r\n");
                        flag = true;
                        break;
                    }
                }

                if (!flag) //if a transition was not found, break the loop
                {
                    //Console.WriteLine("Accepting state error.");
                    textBox19.AppendText("Accepting state error\r\n");
                }
            }

            if (flag) textBox17.AppendText("Accepted");//Console.WriteLine("Automaton has been accepted the input."); //display acceptance result
            else textBox17.AppendText("NOT Accepted");//Console.WriteLine("Automaton has NOT been accepted the input.");
        }

        //upload current automaton in GUI to data
        private void button1_Click(object sender, EventArgs e)
        {
            textBox12.Text = string.Empty;
            textBox11.Text = string.Empty;
            textBox17.Text = string.Empty;
            textBox19.Text = string.Empty;
            Automaton tmpAutomaton = new Automaton();
            tmpAutomaton = Data.userInput(textBox8.Text, textBox9.Text, textBox15.Text, textBox16.Text, textBox10.Text, textBox11.Text);
            bool flag = true;

            if (!ErrorCheck.checkInitial(tmpAutomaton))
            {
                textBox19.AppendText("Initial state not found within automaton states");
                flag = false;
            }
            if (!ErrorCheck.checkAccepting(tmpAutomaton))
            {
                textBox19.AppendText("Accepting state not found within automaton states");
                flag = false;
            }
            if (!ErrorCheck.checkTransitions(tmpAutomaton))
            {
                textBox19.AppendText("Transition element not found within automaton states or alphabet");
                flag = false;
            }

            if (flag) finiteAutomaton = tmpAutomaton;
        }

        private void newFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox8.Text = textBox9.Text = textBox10.Text = textBox16.Text = textBox15.Text = textBox17.Text = textBox19.Text = string.Empty;
        }

        //load from file
        private void loadFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox19.Text = string.Empty;
            OpenFileDialog loadFileDialog1 = new OpenFileDialog();

            loadFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            loadFileDialog1.FilterIndex = 2;
            loadFileDialog1.RestoreDirectory = true;

            textBox8.Clear();
            textBox10.Clear();
            textBox9.Clear();
            textBox15.Clear();
            textBox16.Clear();
            textBox19.Clear();
            textBox11.Clear();
            textBox17.Clear();

            if (loadFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string xmlLocation = loadFileDialog1.FileName;

                Automaton tmpAutomaton = new Automaton();
                tmpAutomaton = Data.xmlInput(xmlLocation);
                bool flag = true;

                if (!ErrorCheck.checkInitial(tmpAutomaton))
                {
                    textBox19.AppendText("Initial state not found within automaton states");
                    flag = false;
                }
                if (!ErrorCheck.checkAccepting(tmpAutomaton))
                {
                    textBox19.AppendText("Accepting state not found within automaton states");
                    flag = false;
                }
                if (!ErrorCheck.checkTransitions(tmpAutomaton))
                {
                    textBox19.AppendText("Transition element not found within automaton states or alphabet");
                    flag = false;
                }

                if (flag == true)
                {
                    finiteAutomaton = tmpAutomaton;
                }
                else
                {
                    return;
                }

                foreach (State x in finiteAutomaton.States) textBox8.AppendText(x.getState() + "\r\n");
                foreach (Symbol x in finiteAutomaton.Alphabet) textBox9.AppendText(x.getSymbol() + "\r\n");
                foreach (Transition x in finiteAutomaton.Transitions) textBox10.AppendText(x.getSourceString() + ";" + x.getSymbolString() + ";" + x.getDestinationString() + "\r\n");
                textBox15.AppendText(finiteAutomaton.Initial.getState());
                foreach (State x in finiteAutomaton.Accepting) textBox16.AppendText(x.getState() + "\r\n");
                if(finiteAutomaton.Input != null) textBox11.AppendText(finiteAutomaton.Input);

            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox18_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox14_TextChanged(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void textBox17_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox19_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox20_TextChanged(object sender, EventArgs e)
        {

        }

        private void saveFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //save 
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Data.xmlOutput(finiteAutomaton, saveFileDialog1.FileName);
            }
        }
    }
}
