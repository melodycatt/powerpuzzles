/*using System;
using System.Linq;

namespace Scripts.Utility
{
    public struct TruthTable
    {
        public bool[][] inputs;
        public bool[][] outputs;

        bool[][][] table;
        public TruthTable(int inputs, int outputs)
        {
            this.inputs = new bool[(int)Math.Pow(2, inputs)][];
            this.outputs = new bool[(int)Math.Pow(2, inputs)][];
            table = new bool[2][][];
            table[0] = this.inputs;
            table[1] = this.outputs;
        }

        public TruthTable(int inputs, params Func<bool[], bool>[] testers)
        {
            this.inputs = new bool[(int)Math.Pow(2, inputs)][];
            this.outputs = new bool[(int)Math.Pow(2, inputs)][];
            table = new bool[2][][];
            table[0] = this.inputs;
            table[1] = this.outputs;
            for (int i = 0; i < (int)Math.Pow(2, inputs); i++)
            {
                string b = Convert.ToString(i, 2);
                this.inputs[i] = b.Split("").Select(a => a == "1").ToArray();
                for (int j = 0; j < inputs; j++)
                {
                }
                for (int f = 0; f < testers.Length; f++)
                {
                    this.outputs[i][f] = testers[f](this.inputs[i]);
                }
            }
        }
    }
}*/