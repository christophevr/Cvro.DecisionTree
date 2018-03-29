# DecisionTree
[![Build status](https://ci.appveyor.com/api/projects/status/607lif1gkfydoy9i?svg=true)](https://ci.appveyor.com/project/christophevr/decisiontree)

An intuitive, lightweight decision tree with built-in visualization to [DOT language](https://en.wikipedia.org/wiki/DOT_language)

Code sample:
```
var decisionTree = new DecisionQuery<EmployeeInfo, BonusCalculation>
{
	Test = employee => employee.YearsEmployed < 5,
	Positive = new DecisionResult<EmployeeInfo, BonusCalculation> { CreateResult = employee => new BonusCalculation { Bonus = employee.YearsEmployed * 100 } },
	Negative = new DecisionQuery<EmployeeInfo, BonusCalculation>
	{
		Test = employee => employee.YearsEmployed < 10,
		Positive = new DecisionResult<EmployeeInfo, BonusCalculation> { CreateResult = employee => new BonusCalculation { Bonus = employee.YearsEmployed * 200M } },
		Negative = new DecisionResult<EmployeeInfo, BonusCalculation> { CreateResult = employee => new BonusCalculation { Bonus = employee.YearsEmployed * 300M } }
	}
};

var graph = decisionTree.RenderToString();
```

Image render:
![Diagram](https://i.imgur.com/j7KHMt8.png)
