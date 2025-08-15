using Microsoft.ML.OnnxRuntime;
using System;
using System.IO;
using cli.Options;
using LiteDB;
using System.Runtime.Serialization.Formatters;

static void PrintHeader()
{
    Console.WriteLine("This is SemantiCode CLI.");
}

static int Fail(string message)
{
    Console.WriteLine($"Error: {message}");
    return 1;
}

static int Search(string[] args) 
{ 
    var options = ParseSearch(args);

    if (options.Query == null)
    {
        return Fail("Query is required.");
    }

    if (options.K.HasValue && options.K.Value <= 0)
    {
        return Fail("K must be a positive integer.");
    }



    

    Console.WriteLine("command: search");
    Console.WriteLine($"query: {(options.Query ?? "missing")}");
    Console.WriteLine($"k: {(options.K.HasValue ? options.K.Value.ToString() : "missing")}");

    return 0;
}

static int Index(string[] args)
{
    var options = ParseIndex(args);

    if (options.Path == null)
    {
        return Fail("Path is required.");
    }

    if (!Directory.Exists(options.Path) && !File.Exists(options.Path))
    {
        return Fail($"Path '{options.Path}' does not exist.");
    }

    Console.WriteLine("command: index");
    Console.WriteLine($"path: {(options.Path ?? "missing")}");

    return 0;
}

static IndexOptions ParseIndex(string[] args)
{ 
    string? path = null;
    if(args.Length >= 2)
    {
        path = args[1];
        return new IndexOptions { Path = path };
    }

    return new IndexOptions { Path = path};
}

static SearchOptions ParseSearch(string[] args)
{
    string? query = null;
    int? k = null;

    int i = 1;
    if (i < args.Length  && !args[i].StartsWith("-"))
    {
        query = args[i];
        i++;
    }

    while (i < args.Length)
    {
       var a = args[i];
        if (a == "-k" || a == "--k") {
            if (i + 1 < args.Length && int.TryParse(args[i + 1], out var parsed)) { 
                k = parsed;
                i += 2;
                continue;
            }
            else
            {
                k = null;
                i++;
                continue;
            }
        }
        else
        {
            i++;
        }
    }

    return new SearchOptions { Query = query, K = k};
}


static void help()
{
    Console.WriteLine("Usage: semanticode <command> [options]");
    Console.WriteLine("Commands:");
    Console.WriteLine("  index <path>     Index the code at the specified path.");
    Console.WriteLine("  search <query> [-k <number>]  Search for the query with optional k parameter.");
    Console.WriteLine("Options:");
    Console.WriteLine("  -k, -k <number> Specify the number of results to return.");
}
static bool isHelp(string[] s)
{ 

    foreach (var arg in s)
    {
        if (arg == "-h" || arg == "--help" || arg == "help")
        {
            return true;
        }
        
    }
    return false;

}

PrintHeader();

if (args.Length == 0 || isHelp(args))
{
    help();
    return;
}

var command = args[0].ToLowerInvariant();
switch (command)
{
    case "index":
        Environment.Exit(Index(args));
        break;
    case "search":
        Environment.Exit(Search(args));
        break;
    default:
        Console.WriteLine($"Unknown command: {command}");
        Environment.Exit(1);
        break;
}
