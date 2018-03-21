# DecisionTree
An intuitive, lightweight decision tree with built-in visualization

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