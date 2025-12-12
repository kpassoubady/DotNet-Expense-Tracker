# .NET Exercise: Personal Expense Tracker

A comprehensive GitHub Copilot Mastery Bootcamp for building a full-stack ASP.NET Core 10.0 web application.

## Overview

Build a **Personal Expense Tracker** while mastering AI-assisted development with GitHub Copilot. This 1-day intensive course covers full-stack .NET development with progressive Copilot skill building.

## Quick Start

```bash
# 1. Verify prerequisites
dotnet --version       # Should be 10.0+
dotnet ef --version    # EF Core tools

# 2. Start with backend setup
# Open: 1-setup/day1-backend-setup.md
```

## Directory Structure

```
dotnet-exercise/
├── README.md                              # This file
├── 0-DotNet-Personal-Expense-Tracker-Breakout.md  # Main course overview
├── 0-copilot-mastery-guide.md             # Copilot reference guide
├── 1-setup/                               # Setup instructions
│   ├── day1-backend-setup.md              # Backend prerequisites (10-15 min)
│   └── day1-frontend-setup.md             # Frontend prerequisites (5 min)
├── 2-breakout/                            # Session exercises
│   ├── 1-Day1-Session1-Models-DbContext.md      # 45 min - EF Core entities
│   ├── 2-Day1-Session2-Services-Business-Logic.md # 45 min - Service layer
│   ├── 3-Day1-Session3-REST-APIs-Controllers.md   # 45 min - REST APIs
│   └── 4-Day1-Session4-Web-Interface-Razor.md     # 45 min - Razor Pages UI
└── 3-games/                               # Interactive Copilot games
    ├── README.md                          # Games overview
    └── Game 1-16                          # 16 interactive exercises
```

## Learning Path

| Session       | Duration | Topics                   | Copilot Skills                       |
|---------------|----------|--------------------------|--------------------------------------|
| **Setup**     | 15 min   | Environment verification | -                                    |
| **Session 1** | 45 min   | Models & DbContext       | Context selection, code generation   |
| **Session 2** | 45 min   | Services & Testing       | Pattern recognition, test generation |
| **Session 3** | 45 min   | REST APIs                | Chat Interface, agents               |
| **Session 4** | 45 min   | Razor Pages UI           | Multi-language coordination          |

## Technology Stack

- **Framework**: ASP.NET Core 10.0, Entity Framework Core 10.0
- **Database**: SQLite (development)
- **Frontend**: Razor Pages, Bootstrap 5.3, Chart.js
- **Validation**: FluentValidation
- **Testing**: xUnit, Moq, FluentAssertions

## Prerequisites

- .NET 10 SDK
- Visual Studio 2022 or VS Code with C# Dev Kit
- GitHub Copilot extension (authenticated)
- Git

## What You'll Build

A complete expense tracking application with:
- EF Core data layer with relationships
- Service layer with business logic
- RESTful API endpoints
- Interactive web UI with charts
- 15+ unit tests

## Getting Started

1. **Read the overview**: [0-DotNet-Personal-Expense-Tracker-Breakout.md](0-DotNet-Personal-Expense-Tracker-Breakout.md)
2. **Complete setup**: [1-setup/day1-backend-setup.md](1-setup/day1-backend-setup.md)
3. **Follow sessions in order**: Start with Session 1 in `2-breakout/`
4. **Practice with games**: Explore interactive exercises in `3-games/`

## 🎮 Interactive Games

The `3-games/` directory contains 16 interactive exercises to reinforce Copilot skills:

| Category                | Games       | Focus                     |
|-------------------------|-------------|---------------------------|
| **Prompt Engineering**  | 1, 3, 8     | Writing effective prompts |
| **Code Generation**     | 2, 6, 7, 10 | From basic to complex     |
| **Critical Evaluation** | 4, 5, 9     | Reviewing AI output       |
| **Copilot Interface**   | 11, 12      | Context & commands        |
| **Docs & Testing**      | 13, 14      | XML docs, xUnit           |
| **Advanced**            | 15, 16      | Git, agents               |

## License

Part of the GitHub Copilot United training materials.
