using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using System;
using System.Text.RegularExpressions;
using UnityEngine.U2D;
using UnityEngine.UI;

public class Robot : MonoBehaviour
{
    public TextMeshProUGUI tmp;
    public bool next = true;
    private readonly System.Random rand = new();
    private int tutorialIndex = 0;
    private int tableIndex = 0;
    public List<GameObject> CurrentComps = new();
    public Dictionary<string, GameObject> Components = new();
    public bool hinting = false;
    public int hintI = -1;
    public Button hint;
    private readonly string[] tutorials = new string[] {
        "Hello! Welcome to L0G1CA!\n\nThis is a game about electrical logic. You'll be solving puzzles by connecting basic logic 'gates' to create the right outputs.\n(press the right arrow to continue, and the left arrow to go back)~True",
        "Lets go over the terminology:\nA logic gate is something that takes some input(s), tests it based on some logic, and gives corresponding output(s)\nA high signal just means something is on. This is represented as a 1 in binary, and in the game it is represented in green\nA low signal is the opposite - its a 0 in binary, and grey or black in game~True",
        //"Finally, there are truth tables. These are how you can know what the end goal of the puzzle is.\nThey include a column for each input, and a column for each output. With these columns they show what outputs you should get for each combination of high and low outputs.\nSometimes, truth tables will have columns for what the last values of the inputs were.~True",
        "Lets get right in!\n\n(Whenever I stop talking, you can still press the arrows to move on in the tutorial. When there is a ... that means there is more to what I'm saying, it just won't fit on the screen, so I put it on the next part of the tutorial)~True",
        "BREAK ^~OR$2,1",
        "This is an OR gate.\nIt takes two inputs, which will be those two 'L's on the right (L stands for Low), and gives 1 output.~False",
        "The output is high when either or both of the inputs are high. Don't mistake this for an XOR (exclusive or) gate, which ONLY turns on if 1 of its inputs is high.~False",
        "If you ever forget what a gate does, hold CTRL and hover over it!~False",
        "Firstly, lets move the gate to a better place so we can see it well - maybe the center of the screen. To do that, just hold shift and drag the gate where you want to put it.~False",
        "To demonstrate the gate, wire each input on the right to one of the inputs on the OR gate (which are the white circles, or terminals, on the right side of the gate)...~False",
        "...You do this by clicking on the terminals that are on the inputs, and then dragging to the input you want to connect to.~False",
        "Then, drag from the output of the OR gate (the terminal on the left side of it) to the Output component on the left side of the screen.~False",
        "When wiring, you always start a wire from an output terminal (usually on the left side of a component), and end it at an input terminal (usually on the right side). You can even wire a gate to its own input!~False",
        "Now you can play around by clicking on each of the inputs to turn them on or off, and seeing what the output is!~False",
        "Once you understand the OR gate, click on the button in the top right.\nThat button is the test button - when you click it, I will test your circuit to see if it gives the right outputs for the puzzle...~False",
        "...For now, all you need is the basic wires I told you to make before.~False",
        "Ok, so now we know how this all works, lets look at some other gates. I'm going to give you 2 inputs and 1 output, and you can go wild. You won't need to test your circuit here.~True",
        "BREAK $2,1",
        "I've given you access to all the gates, you just need to buy them.\nYou might've been wondering what that number is in the top left. Those are your bits! You use them to buy logic gates to put in your circuit.\nOnce you run out of bits though, you can't get them back unless you reset - so be careful!~False",
        "To buy gates, press B to open the shop, and B again to close it. Click on the icon for the gate you want, and then click wherever you want to spawn it.~False",
        "~False",
        "I have one final challenge for you - make an on button and off button for the output.\nThe first input (the on button) should make the output go on (high), which should then stay on even when the first input turns off (becomes low) again.\nThe output should then only turn off when the second input (the off button) is on (high), and stay off (low) until the on button is pressed again.\n\nThis circuit actually has a real name - a set-reset latch (or SR latch) - and it's pretty common! The first input is the set button, and the second is the reset button.~True", //\nI'll also give you exactly enough bits to solve the challenge in the most expensive way.
        "A good way to think of the solution to a puzzle is to think of the logic in English terms.\n\nAn example would be \"I want this output to go on when this input is on, and this input is not on.\" If you look closely, I used the words AND and NOT. Thats how I can figure out I need an AND gate and a NOT gate.\n\nIf you can't find gates in your logic, try thinking about synonyms you've used. You might say \"I want this one to turn on UNLESS this one is on.\" You can rephrase that to replace UNLESS with NOT.",
        "If you cant figure this one out, there's a hint button in the top right. When you click that button, I'll give you a hint, and wait until you press space to go away. The hint button will go grey while you read the hint, and stay grey once you've gone through every hint~True",
        "BREAK ^$2,1"
    };

    [SerializeField]
    private string[] hints = new string[] {
        "For the set button, try wiring the input to an OR gate, and then wiring the output of that OR gate back to its own input.\nThat way, once the OR gate receives a high input, it will power itself forever.\nNext, find a way to include the reset input so that it breaks that infinite loop.",
        "To break the loop, add some gates before the OR gate connects back to itself, so it doesn't directly connect to itself anymore. That way, you can interrupt the high signal with the reset input before it gets back to power the OR gate again.",
        "To make the reset button interrupt the high signal, first wire the OR gate to an input of an AND gate. Next make sure the other input is high UNLESS the reset input is high.",
        "To make the other input of the AND gate stay high unless the reset input is high, just wire the reset input to a NOT gate, and wire that to the AND gate."
    };

    public List<int> bits = new() {
        1000,
        1000
    };
    public List<Dictionary<bool[], bool[]>> tables = new List<Dictionary<bool[], bool[]>>()
    {
        new () {
            { 
                new bool[] { false, false}, 
                new bool[] { false } 
            },
            {
                new bool[] { false, true },
                new bool[] { true }
            },
            {
                new bool[] { true, false },
                new bool[] { true }
            },
            {
                new bool[] { true, true },
                new bool[] { true }
            },
        },
        new () {
            { 
                new bool[] { false, false}, 
                new bool[] { false } 
            },
            {
                new bool[] { true, false },
                new bool[] { true }
            },
            {
                new bool[] { false, false },
                new bool[] { true }
            },
            {
                new bool[] { false, true },
                new bool[] { false }
            },
            {
                new bool[] { false, false },
                new bool[] { false }
            },
        }
    };


    // Start is called before the first frame update
    void Start()
    {
        tmp = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        Camera.main.GetComponent<CameraUtil>().TutorialPause = true;
        Components.Add("OR", Resources.Load<GameObject>("OR"));
        Components.Add("AND", Resources.Load<GameObject>("AND"));
        Components.Add("XOR", Resources.Load<GameObject>("XOR"));
        Components.Add("NOT", Resources.Load<GameObject>("NOT"));
        Components.Add("NOR", Resources.Load<GameObject>("NOR"));
        Components.Add("NAND", Resources.Load<GameObject>("NAND"));
        Components.Add("XNOR", Resources.Load<GameObject>("XNOR"));
    }

    public void Hint() {
        hinting = true;
        hintI++;
        hint.interactable = false;
        transform.localScale = new(0.25f, 0.25f, 0.25f);
        GetComponent<RectTransform>().anchoredPosition = new(-139, 0); 
        tmp.transform.localScale = new(2, 2, 2);
        tmp.GetComponent<RectTransform>().anchoredPosition = new(-2480, 0);
        tmp.margin = new(0,0, -960, 0);
        tmp.text = hints[hintI]; 
    }

    // Update is called once per frame
    void Update()
    {
        if (hinting) {
            if (Input.GetKeyDown(KeyCode.Space)) {
                hinting = false;
                if (hintI < hints.Count() - 1) {
                    hint.interactable = true;
                    tmp.text = "";
                }
            }
        }
        else 
        {
            if (next) {
                next = false;
                if (tmp.text != tutorials[tutorialIndex] && tutorials[tutorialIndex][0..5] != "BREAK") {
                    tmp.text = tutorials[tutorialIndex];
                    tmp.text = Regex.Replace(tmp.text, @"~((?:True)|(?:False))$", "", RegexOptions.Multiline);
                    transform.Find("Block").gameObject.SetActive(Convert.ToBoolean(Regex.Match(tutorials[tutorialIndex], @"~((?:True)|(?:False))$", RegexOptions.Multiline).Groups[1].Value));
                    if (Camera.main.GetComponent<CameraUtil>().TutorialPause == false && Convert.ToBoolean(Regex.Match(tutorials[tutorialIndex], @"~((?:True)|(?:False))$", RegexOptions.Multiline).Groups[1].Value)) {
                        foreach(GameObject i in CurrentComps) {
                            Destroy(i);
                        }
                        CurrentComps.Clear();
                        print(GetComponent<RectTransform>().anchoredPosition);
                        Camera.main.GetComponent<CameraUtil>().ClearPuts();
                        transform.localScale = new(0.5f, 0.5f, 0.5f);
                        GetComponent<RectTransform>().anchoredPosition = new(-278, 0); 
                        tmp.transform.localScale = new(1, 1, 1);
                        tmp.GetComponent<RectTransform>().anchoredPosition = new(-1000, 500);
                        tmp.margin = new(0,0, -780, 0);
                    } else if (Camera.main.GetComponent<CameraUtil>().TutorialPause == true && !Convert.ToBoolean(Regex.Match(tutorials[tutorialIndex], @"~((?:True)|(?:False))$", RegexOptions.Multiline).Groups[1].Value)) {
                        print(GetComponent<RectTransform>().anchoredPosition);
                        transform.localScale = new(0.25f, 0.25f, 0.25f);
                        GetComponent<RectTransform>().anchoredPosition = new(-139, 0);
                        tmp.transform.localScale = new(2, 2, 2);
                        tmp.GetComponent<RectTransform>().anchoredPosition = new(-2480, 0);
                        tmp.margin = new(0,0, -960, 0);
                    }
                    Camera.main.GetComponent<CameraUtil>().TutorialPause = Convert.ToBoolean(Regex.Match(tutorials[tutorialIndex], @"~((?:True)|(?:False))$", RegexOptions.Multiline).Groups[1].Value);
                } else if (tmp.text != tutorials[tutorialIndex] && tutorials[tutorialIndex][0..5] == "BREAK") {
                    transform.localScale = new(0.25f, 0.25f, 0.25f);
                    GetComponent<RectTransform>().anchoredPosition = new(-139, 0); 
                    tmp.transform.localScale = new(2, 2, 2);
                    tmp.GetComponent<RectTransform>().anchoredPosition = new(-2480, 0);
                    tmp.margin = new(0,0, -960, 0);
                    tmp.text = ""; 
                    Camera.main.GetComponent<CameraUtil>().TutorialPause = false;
                    Match components = Regex.Match(tutorials[tutorialIndex], @"(?<=~)((.*),?)+(?=\$)");
                    Match inOut = Regex.Match(tutorials[tutorialIndex], @"(?<=\$)(?:(.*),?)+$");
                    Match Carrot = Regex.Match(tutorials[tutorialIndex], @"\^");
                    if (components.Success) {
                        string[] splitComponents = components.Value.Split(',');
                        foreach(string comp in splitComponents) {
                            GameObject temp = Instantiate(Components[comp]);
                            temp.GetComponent<holdable>().shop = Camera.main.GetComponent<CameraUtil>().shop;
                            CurrentComps.Add(temp);
                        }
                    }
                    if (inOut.Success) {
                        string[] splitInOut = inOut.Value.Split(',');
                        Camera.main.GetComponent<CameraUtil>().ResetPuts(int.Parse(splitInOut[0]), int.Parse(splitInOut[1]));
                    }
                    if (Carrot.Success && tableIndex != tables.Count) {
                        Camera.main.GetComponent<CameraUtil>().table.Table = tables[tableIndex];
                        Camera.main.GetComponent<CameraUtil>().bits = bits[tableIndex];
                        hint.interactable = true;
                        tableIndex ++;
                    };
                    transform.Find("Block").gameObject.SetActive(false);
                }
            }
            if (Input.GetKeyDown(KeyCode.RightArrow)) {
                tutorialIndex++;
                next = true;
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
                Match Carrot = Regex.Match(tutorials[tutorialIndex], @"\^");
                if (Carrot.Success && tableIndex != tables.Count) {
                    hint.interactable = true;
                    tableIndex --;
                };
                tutorialIndex--;
                next = true;
            }
        }
    }
}
