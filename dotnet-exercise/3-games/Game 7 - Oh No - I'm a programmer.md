# Oh, No, I'm a Programmer, Need a Problem to Solve

## 1. Web Scraper for a News Site 📰

Write a program that scrapes all the article headlines and links from the homepage of a news website (e.g., BBC News or a similar site). Save the data into a CSV file with columns: `headline`, `url`.

Why it's a good exercise: Introduces working with HTTP requests, external libraries (HtmlAgilityPack or AngleSharp in C#), and parsing messy real-world HTML.

## 2. Weather Data Aggregator with Caching ☁️

Build a small service that fetches current weather data for a list of cities from a public API (e.g., Open-Meteo or NOAA), caches the responses in memory for a short time (e.g., 5–10 minutes), and exposes endpoints like:

- `GET /weather?city=London`
- `GET /weather/stats` (e.g., min/max temperature across cached cities)
Include retry logic, basic error handling, and graceful degradation when the upstream API fails.

Why it's a good exercise: Teaches API consumption, JSON parsing with System.Text.Json, in-memory caching (IMemoryCache), rate limiting awareness, error handling patterns, and lightweight service design without being too advanced.

## 3. Sudoku Solver 🔢

Create a program that can solve any valid 9×9 Sudoku puzzle. Input: 2D array (0 for empty). Output: solved board.

Why it's a good exercise: Demonstrates backtracking, recursion, constraint reasoning, and efficient 2D array manipulation.

## 4. Pathfinding Algorithm Visualizer 🗺️

Implement A* or Dijkstra's algorithm on a 2D grid. Provide a simple UI showing start, goal, obstacles, and animate the exploration and final shortest path.

Why it's a good exercise: Teaches graphs, priority queues (PriorityQueue<T, TPriority>), heuristics, and makes abstract algorithms tangible through visualization.

## 5. Markdown to HTML Converter 📄

Convert a basic Markdown file to HTML supporting:

- Headings (`#`, `##`)
- Bold (`**text**`)
- Italic (`*text*`)
- Unordered lists (`- item`)

Why it's a good exercise: Practices text parsing, regular expressions (Regex), state handling, and file I/O.

## 6. Concurrent File Downloader ⚡

Given a list of file URLs (e.g., images), download them concurrently using async tasks. Show elapsed time vs sequential.

Why it's a good exercise: Introduces async/await, Task.WhenAll, HttpClient patterns, I/O-bound optimization, and performance measurement.

## 7. Simple In-Memory Key-Value Store 💾

Support commands:

```
SET key value
GET key
DELETE key
SAVE      # optional: persist to file
LOAD      # optional: restore from file
```

Why it's a good exercise: Reinforces Dictionary<TKey, TValue>, serialization (System.Text.Json), simple protocol design, and REPL-style input handling.

## 8. Client-Server Chat Application 💬

Create a multi-client chat system using TCP sockets or SignalR. Server broadcasts each received message to all connected clients.

Why it's a good exercise: Covers TcpClient/TcpListener or SignalR hubs, async programming, message framing, and basic protocol design.

## 9. Plagiarism Checker 🔍

Compare two text files and compute similarity using Jaccard similarity on unique word sets: `J(A,B) = |A ∩ B| / |A ∪ B|`.

Why it's a good exercise: Practices tokenization, HashSet operations (Intersect, Union), file I/O, and applying a mathematical measure to text analysis.

## 10. Building a Simple URL Shortener 🔗

Provide:

- Short code generation
- Storage of short → original mapping
- Redirect endpoint

Why it's a good exercise: Combines ASP.NET Core Minimal API design, Dictionary or database persistence, string manipulation, and HTTP redirection semantics.
