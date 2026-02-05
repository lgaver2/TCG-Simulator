using System;
using System.Collections.Generic;
using CardEnum;
using StaticUtils;
using UnityEngine;

[Serializable]
public class TargetSpecification
{
    public List<CardType> cardType;
    public List<CardLocation> cardLocation;
    public List<CostSpecification> cardCost;
}

[Serializable]
public class CostSpecification
{
    public int cost;
    public ComparisonOperator op;
}