# LogicBuilder.Workflow.Activities.Rules

[![Build Status](https://github.com/BpsLogicBuilder/LogicBuilder.Workflow.Activities.Rules/actions/workflows/ci.yml/badge.svg)](https://github.com/BpsLogicBuilder/LogicBuilder.Workflow.Activities.Rules/actions/workflows/ci.yml)
[![CodeQL](https://github.com/BpsLogicBuilder/LogicBuilder.Workflow.Activities.Rules/actions/workflows/github-code-scanning/codeql/badge.svg)](https://github.com/BpsLogicBuilder/LogicBuilder.Workflow.Activities.Rules/actions/workflows/github-code-scanning/codeql)
[![codecov](https://codecov.io/gh/BpsLogicBuilder/LogicBuilder.Workflow.Activities.Rules/graph/badge.svg?token=4LC956TUIF)](https://codecov.io/gh/BpsLogicBuilder/LogicBuilder.Workflow.Activities.Rules)
[![NuGet](https://img.shields.io/nuget/v/LogicBuilder.Workflow.Activities.Rules.svg)](https://www.nuget.org/packages/LogicBuilder.Workflow.Activities.Rules)

A powerful, forward-chaining rules engine for .NET that enables dynamic business rule evaluation and execution without recompilation. This library provides a flexible framework for defining, validating, and executing business rules using CodeDOM expressions.

## Overview

LogicBuilder.Workflow.Activities.Rules is a .NET rules engine that allows you to:
- Define business rules declaratively using CodeDOM expressions
- Validate rules against target types at design-time or runtime
- Execute rules with forward-chaining and re-evaluation support
- Serialize/deserialize rule sets for storage and versioning
- Support complex conditions and actions including method calls, property access, and object creation

## Core Components

### RuleEngine

The `RuleEngine` class is the execution engine that processes validated rule sets against target objects. It handles:
- Rule preprocessing and analysis
- Forward-chaining execution with configurable chaining behavior
- Rule priority management
- Re-evaluation of rules when object state changes


### RuleSet

The `RuleSet` class represents a collection of rules that can be executed together. Features include:
- Named rule collections with optional descriptions
- Configurable chaining behavior (Full, Sequential, None)
- Rule validation against target types
- Serialization support for persistence
- Cloning capabilities for rule set management

**Properties:**
- `Name` - Unique identifier for the rule set
- `Description` - Optional documentation
- `Rules` - Collection of Rule objects
- `ChainingBehavior` - Controls how rules trigger re-evaluation

**Chaining Behaviors:**
- `Full` - Rules can trigger re-evaluation of all other rules
- `Sequential` - Rules execute in priority order without re-evaluation
- `None` - No forward chaining

### RuleValidation

The `RuleValidation` class validates rule expressions against target types and provides type resolution services. It:
- Validates rule conditions and actions at design-time or runtime
- Resolves types, methods, properties, and fields
- Manages type conversions and implicit/explicit operators
- Supports extension methods (C# 3.0+)
- Provides detailed validation errors with line information

**Key Capabilities:**
- **Type Resolution**: Resolves types from assemblies including generic types
- **Method Resolution**: Finds best-match methods considering overloads, optional parameters, and params arrays
- **Property/Field Resolution**: Validates member access including indexers
- **Conversion Validation**: Checks implicit and explicit type conversions
- **Access Validation**: Enforces visibility rules (public, internal, private)


## Supported Expression Types

The library supports a rich set of CodeDOM expressions:
- **Binary operations**: Comparison, arithmetic, logical operators
- **Property/field access**: Including nested properties
- **Method invocation**: Instance and static methods
- **Array indexers**: Multi-dimensional array access
- **Object creation**: Constructor calls with parameters
- **Type casting**: Explicit type conversions
- **Generic types**: Full support for generic type arguments
- **Collection initializers**: Array and list initialization

## Advanced Features

### Forward Chaining
Rules can trigger re-evaluation of other rules when they modify object state, enabling complex rule dependencies and cascading logic.

### Update Actions
Use `RuleUpdateAction` to explicitly mark properties as updated, forcing re-evaluation of dependent rules:

rule.ThenActions.Add(new RuleUpdateAction("this/PropertyName"));

## Target Frameworks

- .NET Standard 2.0

## Use Cases

This library is ideal for:
- Business rule engines requiring dynamic rule modification
- Policy-based systems with versioned rule sets
- Complex validation scenarios with cascading rules
- Decision automation systems
- Workflow rule evaluation
- Configurable business logic without code deployment



