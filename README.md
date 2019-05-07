# SqlParser

## Background and Purpose

This project was created as part of an assignment. The application takes a text file containing SQL scripts and performs some basic parsing into an abstract syntax tree (AST).

## Limitations
- A select set of tokens were used for parsing (see `example.sql`)
- While some automated tests were created, they are definitely not comprehensive

## Setup
- Clone this repo
- Navigate to the `SqlParser` (root) directory
- Run: `dotnet restore`

## Running the console application
```bash
dotnet run --project SqlParser.Console "example.sql"
```

## Running the tests
```bash
dotnet test
```
