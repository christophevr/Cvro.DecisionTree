# DecisionTree
[![Build status](https://ci.appveyor.com/api/projects/status/607lif1gkfydoy9i?svg=true)](https://ci.appveyor.com/project/christophevr/decisiontree)

An intuitive, lightweight decision tree with built-in visualization to [DOT language](https://en.wikipedia.org/wiki/DOT_language)

## Code sample
```
// Build decision tree
var employeeBonusCalculator = new DecisionBuilder<EmployeeInfo, decimal>()
    .WithTest(employee => employee.YearsEmployed < 5)
    .WithPositiveResult(employee => employee.YearsEmployed * 100M)
    .WithNegativeQuery(negativeQuery => negativeQuery
        .WithTest(employee => employee.YearsEmployed < 10)
        .WithPositiveResult(employee => employee.YearsEmployed * 200M)
        .WithNegativeResult(employee => employee.YearsEmployed * 300M))
    .Build();

// Create input
var bob = new EmployeeInfo {YearsEmployed = 7};

// Evaluate tree for output
decimal bonusForBob = employeeBonusCalculator.Evaluate(bob); // Result: 1,400
```

## Visualization
Renders a decision tree to DOT language. Can be visualized with a library like [Viz.js](http://viz-js.com/) or [Webgraphviz](http://www.webgraphviz.com/)
```
string render = employeeBonusCalculator.RenderToString();
```
Image render:
![Diagram](https://i.imgur.com/rExuhZN.png)

## Path tracing
Can return a trace of the path followed
```
var result = employeeBonusCalculator.EvaluateWithPath(bob);
decimal bonusForBob = result.Result;
DecisionPath path = result.DecisionPath;

// Visualize path
string render = employeeBonusCalculator.RenderToString(path);
```
Image render:
![Diagram](https://i.imgur.com/JKe90sT.png)
