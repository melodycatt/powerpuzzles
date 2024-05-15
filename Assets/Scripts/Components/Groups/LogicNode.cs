using System;
using System.Collections.Generic;
public class LogicNode
{
    public List<LogicNode> connections = new();
    public bool value;
    public List<bool> inputs;
    public Func<List<bool>, bool> Logic;

    public LogicNode(int nInputs, Func<List<bool>, bool> logic)
    {
        inputs = new(nInputs);
        Logic = logic;
    }

    public virtual void Return()
    {
        value = Logic(inputs);
        connections.ForEach(item => item.inputs.Add(value));
    }
}

