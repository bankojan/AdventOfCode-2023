var lines = File.ReadAllLines(".\\Input.txt");

SolvePart2();

void SolvePart1()
{
    var (tiles, startTile) = ParseTiles(lines);

    var currentTiles = new Stack<Tile>();
    currentTiles.Push(startTile);
    var processStartTile = false;

    Tile currentTile = null;

    while (currentTiles.Any())
    {
        var previousTile = currentTile is null ? null : currentTile with { };
        currentTile = currentTiles.Pop();

        if(currentTile.Row == startTile.Row &&  currentTile.Column == startTile.Column)
        {
            if (!processStartTile)
            {
                processStartTile = true;
            }
            else
            {
                continue;
            }      
        }

        var leftTile = (currentTile.Column - 1) >= 0 ? tiles[currentTile.Row][currentTile.Column - 1] : null;
        var rightTile = (currentTile.Column + 1) < tiles[currentTile.Row].Count ? tiles[currentTile.Row][currentTile.Column + 1] : null;

        var upTile = (currentTile.Row - 1) >= 0 ? tiles[currentTile.Row - 1][currentTile.Column] : null;
        var downTile = (currentTile.Row + 1) < tiles.Count ? tiles[currentTile.Row + 1][currentTile.Column] : null;

        var left = leftTile is not null && currentTile.ConnectsWith(Direction.Left, leftTile) && (previousTile is null || leftTile.Row != previousTile.Row || leftTile.Column != previousTile.Column);
        var right = rightTile is not null && currentTile.ConnectsWith(Direction.Right, rightTile) && (previousTile is null || rightTile.Row != previousTile.Row || rightTile.Column != previousTile.Column);
        var up = upTile is not null && currentTile.ConnectsWith(Direction.Up, upTile) && (previousTile is null || upTile.Row != previousTile.Row || upTile.Column != previousTile.Column);
        var down = downTile is not null && currentTile.ConnectsWith(Direction.Down, downTile) && (previousTile is null || downTile.Row != previousTile.Row || downTile.Column != previousTile.Column);

        if (left)
        {
            var newTile = leftTile with { Distance = int.Min(leftTile.Distance, currentTile.Distance + 1) };
            tiles[currentTile.Row][currentTile.Column - 1] = newTile;

            currentTiles.Push(newTile);
        }
        if (right)
        {
            var newTile = rightTile with { Distance = int.Min(rightTile.Distance, currentTile.Distance + 1) };
            tiles[currentTile.Row][currentTile.Column + 1] = newTile;

            currentTiles.Push(newTile);
        }
        if (up)
        {
            var newTile = upTile with { Distance = int.Min(upTile.Distance, currentTile.Distance + 1) };
            tiles[currentTile.Row - 1][currentTile.Column] = newTile;

            currentTiles.Push(newTile);
        }
        if (down)
        {
            var newTile = downTile with { Distance = int.Min(downTile.Distance, currentTile.Distance + 1) };
            tiles[currentTile.Row + 1][currentTile.Column] = newTile;

            currentTiles.Push(newTile);
        }
    }

    var maxDistance = tiles.SelectMany(t => t).Where(t => t.Distance != int.MaxValue).Max(t => t.Distance);
    Console.WriteLine($"Part 1 solution: {maxDistance}");
}

void SolvePart2()
{
    var (tiles, startTile) = ParseTiles(lines);

    var currentTiles = new Stack<Tile>();
    currentTiles.Push(startTile);
    var processStartTile = false;

    Tile currentTile = null;

    while (currentTiles.Any())
    {
        var previousTile = currentTile is null ? null : currentTile with { };
        currentTile = currentTiles.Pop();

        if (currentTile.Row == startTile.Row && currentTile.Column == startTile.Column)
        {
            if (!processStartTile)
            {
                processStartTile = true;
            }
            else
            {
                continue;
            }
        }

        var leftTile = (currentTile.Column - 1) >= 0 ? tiles[currentTile.Row][currentTile.Column - 1] : null;
        var rightTile = (currentTile.Column + 1) < tiles[currentTile.Row].Count ? tiles[currentTile.Row][currentTile.Column + 1] : null;

        var upTile = (currentTile.Row - 1) >= 0 ? tiles[currentTile.Row - 1][currentTile.Column] : null;
        var downTile = (currentTile.Row + 1) < tiles.Count ? tiles[currentTile.Row + 1][currentTile.Column] : null;

        var left = leftTile is not null && currentTile.ConnectsWith(Direction.Left, leftTile) && (previousTile is null || leftTile.Row != previousTile.Row || leftTile.Column != previousTile.Column);
        var right = rightTile is not null && currentTile.ConnectsWith(Direction.Right, rightTile) && (previousTile is null || rightTile.Row != previousTile.Row || rightTile.Column != previousTile.Column);
        var up = upTile is not null && currentTile.ConnectsWith(Direction.Up, upTile) && (previousTile is null || upTile.Row != previousTile.Row || upTile.Column != previousTile.Column);
        var down = downTile is not null && currentTile.ConnectsWith(Direction.Down, downTile) && (previousTile is null || downTile.Row != previousTile.Row || downTile.Column != previousTile.Column);

        if (left)
        {
            var newTile = leftTile with { Distance = int.Min(leftTile.Distance, currentTile.Distance + 1) };
            tiles[currentTile.Row][currentTile.Column - 1] = newTile;

            currentTiles.Push(newTile);
        }
        if (right)
        {
            var newTile = rightTile with { Distance = int.Min(rightTile.Distance, currentTile.Distance + 1) };
            tiles[currentTile.Row][currentTile.Column + 1] = newTile;

            currentTiles.Push(newTile);
        }
        if (up)
        {
            var newTile = upTile with { Distance = int.Min(upTile.Distance, currentTile.Distance + 1) };
            tiles[currentTile.Row - 1][currentTile.Column] = newTile;

            currentTiles.Push(newTile);
        }
        if (down)
        {
            var newTile = downTile with { Distance = int.Min(downTile.Distance, currentTile.Distance + 1) };
            tiles[currentTile.Row + 1][currentTile.Column] = newTile;

            currentTiles.Push(newTile);
        }
    }

    for (int i = 0; i < tiles.Count; i++)
    {
        for (int j = 0; j < tiles[i].Count; j++)
        {
            var txt = tiles[i][j].Distance == -1 || tiles[i][j].Distance == int.MaxValue ? "O" : "P";
            if (tiles[i][j].Row == 4 && tiles[i][j].Column == 44)
            {
                txt = "S";
            }
            Console.Write($" {txt} ");
        }
        Console.WriteLine();
    }

    var nonLoopTiles = tiles
        .SelectMany(t => t)
        .Where(t => t.Value == '.' || t.Distance == int.MaxValue)
        .ToList();

    var tilePools = new List<List<Tile>>();

    while (true)
    {
        if (nonLoopTiles.Count == 0)
        {
            break;
        }
        var currentSearchTiles = new Stack<Tile>();
        currentSearchTiles.Push(nonLoopTiles.First());
        nonLoopTiles.RemoveAt(0);

        var pool = new List<Tile>();

        while (currentSearchTiles.Any())
        {
            var currentSearchTile = currentSearchTiles.Pop();
            pool.Add(currentSearchTile);

            var leftTile = (currentSearchTile.Column - 1) >= 0 ? tiles[currentSearchTile.Row][currentSearchTile.Column - 1] : null;
            var rightTile = (currentSearchTile.Column + 1) < tiles[currentSearchTile.Row].Count ? tiles[currentSearchTile.Row][currentSearchTile.Column + 1] : null;

            var upTile = (currentSearchTile.Row - 1) >= 0 ? tiles[currentSearchTile.Row - 1][currentSearchTile.Column] : null;
            var downTile = (currentSearchTile.Row + 1) < tiles.Count ? tiles[currentSearchTile.Row + 1][currentSearchTile.Column] : null;

            if (leftTile is not null && nonLoopTiles.Any(t => t.Row == leftTile.Row && t.Column == leftTile.Column))
            {
                nonLoopTiles.Remove(leftTile);
                currentSearchTiles.Push(leftTile);
            }
            if (rightTile is not null && nonLoopTiles.Any(t => t.Row == rightTile.Row && t.Column == rightTile.Column))
            {
                nonLoopTiles.Remove(rightTile);
                currentSearchTiles.Push(rightTile);
            }
            if (upTile is not null && nonLoopTiles.Any(t => t.Row == upTile.Row && t.Column == upTile.Column))
            {
                nonLoopTiles.Remove(upTile);
                currentSearchTiles.Push(upTile);
            }
            if (downTile is not null && nonLoopTiles.Any(t => t.Row == downTile.Row && t.Column == downTile.Column))
            {
                nonLoopTiles.Remove(downTile);
                currentSearchTiles.Push(downTile);
            }
        }

        tilePools.Add(pool);
    }

    var loopTiles = tiles
        .SelectMany(t => t)
        .Where(t => t.Distance != -1 && t.Distance != int.MaxValue)
        .ToList();
    nonLoopTiles = tiles
        .SelectMany(t => t)
        .Where(t => t.Value == '.' || t.Distance == int.MaxValue)
        .ToList();

    tiles[startTile.Row][startTile.Column] = startTile with { };

    var topLeft = loopTiles.OrderBy(t => t.Row).ThenBy(t => t.Column).First();

    var current = loopTiles.First(t => t.Row == topLeft.Row && t.Column == topLeft.Column);
    var start = current.StartDirection();

    current = tiles[start.x][start.y];
    var from = start.dir;

    var enclosedTiles = new HashSet<Tile>();

    while (current.Row != topLeft.Row || current.Column != topLeft.Column)
    {
        var nextPos = current.NextClockWise(from);

        var enclosed = from switch
        {
            Direction.Left => (current.Row + 1, current.Column),
            Direction.Right => (current.Row - 1, current.Column),
            Direction.Up => (current.Row, current.Column - 1),
            Direction.Down => (current.Row, current.Column + 1)
        };

        var enclosedTile = nonLoopTiles.FirstOrDefault(t => t.Row == enclosed.Item1 && t.Column == enclosed.Item2);

        if(enclosedTile != null)
        {
            enclosedTiles.Add(enclosedTile);
        }

        current = tiles[nextPos.x][nextPos.y];
        from = nextPos.dir;
    }

    var enclosedPools = tilePools
        .Where(p => p.Any(t => enclosedTiles.Contains(t)));

    var result = enclosedPools.Sum(t => t.Count);
    Console.WriteLine($"Part 2 solution: {result}");
}

bool IsBorderTile(Tile tile, int maxRow, int maxColumn)
{
    return tile.Row == 0 || 
        tile.Column == 0 || 
        tile.Row == maxRow || 
        tile.Column == maxColumn;
}

(List<List<Tile>> tiles, Tile startTile) ParseTiles(string[] lines)
{
    var tiles = new List<List<Tile>>();
    Tile startTile = null;

    for (int i = 0; i < lines.Length; i++)
    {
        var row = new List<Tile>();
        for (int j = 0; j < lines[i].Length; j++)
        {
            var tile = new Tile(i, j, lines[i][j], lines[i][j] != '.' ? int.MaxValue : -1);
            if (tile.Value == 'S')
            {
                startTile = tile with { Distance = 0 };
                row.Add(startTile);
            }
            else
            {
                row.Add(tile);
            }
            
        }

        tiles.Add(row);
    }

    var leftTile = (startTile.Column - 1) >= 0 ? tiles[startTile.Row][startTile.Column - 1] : null;
    var rightTile = (startTile.Column + 1) < tiles[startTile.Row].Count ? tiles[startTile.Row][startTile.Column + 1] : null;

    var upTile = (startTile.Row - 1) >= 0 ? tiles[startTile.Row - 1][startTile.Column] : null;
    var downTile = (startTile.Row + 1) < tiles.Count ? tiles[startTile.Row + 1][startTile.Column] : null;


    var left = leftTile is not null && startTile.CanMoveTo(leftTile);
    var right = rightTile is not null && startTile.CanMoveTo(rightTile);
    var up = upTile is not null && startTile.CanMoveTo(upTile);
    var down = downTile is not null && startTile.CanMoveTo(downTile);

    if (left && right)
    {
        startTile = startTile with { Value = '-', Distance = 0 };
    }
    else if (up && down)
    {
        startTile = startTile with { Value = '|', Distance = 0 };
    }
    else if (up && right)
    {
        startTile = startTile with { Value = 'L', Distance = 0 };
    }
    else if (up && left)
    {
        startTile = startTile with { Value = 'J', Distance = 0 };
    }
    else if (down && right)
    {
        startTile = startTile with { Value = 'F', Distance = 0 };
    }
    else if(down && left)
    {
        startTile = startTile with { Value = '7', Distance = 0 };
    }

    return (tiles, startTile ?? new(0, 0, ' ', 0));
}



record Tile(int Row, int Column, char Value, int Distance)
{
    readonly HashSet<char> _up = ['|', 'J', 'L'];
    readonly HashSet<char> _down = ['|', 'F', '7'];
    readonly HashSet<char> _left = ['J', '7', '-'];
    readonly HashSet<char> _right = ['L', 'F', '-'];

    public bool ConnectsWith(Direction direction, Tile other)
    {
        if(direction == Direction.Up)
        {
            return _up.Contains(Value) && _down.Contains(other.Value);
        }
        if(direction == Direction.Down)
        {
            return _down.Contains(Value) && _up.Contains(other.Value);
        }
        if(direction == Direction.Left)
        {
            return _left.Contains(Value) && _right.Contains(other.Value);
        }
        if (direction == Direction.Right)
        {
            return _right.Contains(Value) && _left.Contains(other.Value);
        }

        return false;
    }

    public (int x, int y, Direction dir) StartDirection()
    {
        if (Value == '|')
        {
            return (Row - 1, Column, Direction.Down);
        }
        if (Value == '-')
        {
            return (Row, Column + 1, Direction.Left);
        }
        if (Value == 'L')
        {
            return (Row - 1, Column, Direction.Down);
        }
        if (Value == 'J')
        {
            return (Row, Column - 1, Direction.Right);
        }
        if (Value == '7')
        {
            return (Row + 1, Column, Direction.Up);
        }
        if (Value == 'F')
        {
            return (Row, Column + 1, Direction.Left);
        }

        return default;
    }

    public (int x, int y, Direction dir) NextClockWise(Direction from)
    {
        if(Value == '|')
        {
            return from == Direction.Up ?
               (Row + 1, Column, Direction.Up) :
               (Row - 1, Column, Direction.Down);
        }
        if(Value == '-')
        {
            return from == Direction.Left ?
                (Row, Column + 1, Direction.Left) :
                (Row, Column - 1, Direction.Right);
        }
        if(Value == 'L')
        {
            return from == Direction.Up ?
                (Row, Column + 1, Direction.Left) :
                (Row - 1, Column, Direction.Down);
        }
        if(Value == 'J')
        {
            return from == Direction.Up ?
                (Row, Column - 1, Direction.Right) :
                (Row - 1, Column, Direction.Down);
        }
        if(Value == '7')
        {
            return from == Direction.Down ?
                (Row, Column - 1, Direction.Right) :
                (Row + 1, Column, Direction.Up);
        }
        if(Value == 'F')
        {
            return from == Direction.Down ?
                (Row, Column + 1, Direction.Left) :
                (Row + 1, Column, Direction.Up);
        }
        return default;
    }

    public bool IsPipe(List<Tile> loopTiles, Direction direction)
    {
        if((direction == Direction.Up || direction == Direction.Down) && (_up.Contains(Value) || _down.Contains(Value)))
        {
            var removed = loopTiles
                .RemoveAll(t => Math.Abs(Column - t.Column) == 1 && Row == t.Row && (_up.Contains(t.Value) || _down.Contains(t.Value)));

            return removed > 0;
        }
        if ((direction == Direction.Left || direction == Direction.Right) && (_left.Contains(Value) || _right.Contains(Value)))
        {
            var removed = loopTiles.RemoveAll(t => Math.Abs(Row - t.Row) == 1 && (_left.Contains(t.Value) || _right.Contains(t.Value)));

            return removed > 0;
        }
        return false;
    }

    public bool CanMoveTo(Tile other)
    {
        if(other == null) return false;

        if(other.Value == '.')
        {
            return false;
        }

        if(other.Value == '-')
        {
            return Math.Abs(other.Column - Column) == 1;
        }
        if(other.Value == '|')
        {
            return Math.Abs(other.Row - Row) == 1;
        }
        if(other.Value == 'L')
        {
            return (other.Row - Row) == 1 || 
                (Column - other.Column) == 1;
        }
        if(other.Value == 'J')
        {
            return (other.Row - Row) == 1 ||
                (other.Column - Column) == 1;
        }
        if(other.Value == 'F')
        {
            return (Row - other.Row) == 1 ||
                (Column - other.Column) == 1;
        }
        if(other.Value == '7')
        {
            return (Row - other.Row) == 1 ||
                (other.Column - Column) == 1;
        }

        return false;
    }
}

enum Direction
{
    None = 0,
    Up = 1,
    Down = 2,
    Left = 3,
    Right = 4,
}



//var groundTiles = tiles
//        .SelectMany(t => t)
//        .Where(t => t.Value == '.' || t.Distance == int.MaxValue)
//        .ToList();

//var loopTiles = tiles
//    .SelectMany(t => t)
//    .Where(t => t.Distance != -1 && t.Distance < int.MaxValue)
//    .ToList();

//var allLoopTiles = tiles
//    .SelectMany(t => t)
//    .Where(t => t.Distance != -1 && t.Distance < int.MaxValue)
//    .ToList();

//for (int i = 0; i < tiles.Count; i++)
//{
//    for (int j = 0; j < tiles[i].Count; j++)
//    {
//        Console.Write(" " + (tiles[i][j].Distance == int.MaxValue || tiles[i][j].Distance == -1 ? "O" : tiles[i][j].Value) + " ");
//    }
//    Console.WriteLine();
//}

//var tilePools = new List<List<Tile>>();

//while (true)
//{
//    if (groundTiles.Count == 0)
//    {
//        break;
//    }
//    var currentSearchTiles = new Stack<(Tile tile, Direction direction)>();
//    currentSearchTiles.Push((groundTiles.First(), Direction.None));

//    var pool = new List<Tile>();
//    var isEnclosed = true;

//    while (currentSearchTiles.Any())
//    {
//        var currentSearchTile = currentSearchTiles.Pop();
//        groundTiles.Remove(currentSearchTile.tile);

//        if (isEnclosed && IsBorderTile(currentSearchTile.tile, tiles.Count - 1, tiles[0].Count - 1))
//        {
//            isEnclosed = false;
//        }

//        if (currentSearchTile.direction == Direction.None)
//        {
//            pool.Add(currentSearchTile.tile);
//        }

//        var leftTile = (currentSearchTile.tile.Column - 1) >= 0 ? tiles[currentSearchTile.tile.Row][currentSearchTile.tile.Column - 1] : null;
//        var rightTile = (currentSearchTile.tile.Column + 1) < tiles[currentSearchTile.tile.Row].Count ? tiles[currentSearchTile.tile.Row][currentSearchTile.tile.Column + 1] : null;

//        var upTile = (currentSearchTile.tile.Row - 1) >= 0 ? tiles[currentSearchTile.tile.Row - 1][currentSearchTile.tile.Column] : null;
//        var downTile = (currentSearchTile.tile.Row + 1) < tiles.Count ? tiles[currentSearchTile.tile.Row + 1][currentSearchTile.tile.Column] : null;

//        if (leftTile is not null)
//        {
//            var isGround = groundTiles.Any(t => t.Row == leftTile.Row && t.Column == leftTile.Column);
//            var isPipe = leftTile.IsPipe(loopTiles, Direction.Left);

//            if (isPipe && (currentSearchTile.direction == Direction.None || currentSearchTile.direction == Direction.Left))
//            {
//                loopTiles.Remove(leftTile);
//                currentSearchTiles.Push((leftTile, Direction.Left));
//            }
//            else if (isGround && (currentSearchTile.direction == Direction.None || currentSearchTile.direction == Direction.Left))
//            {
//                currentSearchTiles.Push((leftTile, Direction.None));
//            }
//        }
//        if (rightTile is not null)
//        {
//            var isGround = groundTiles.Any(t => t.Row == rightTile.Row && t.Column == rightTile.Column);
//            var isPipe = rightTile.IsPipe(loopTiles, Direction.Right);

//            if (isPipe && (currentSearchTile.direction == Direction.None || currentSearchTile.direction == Direction.Right))
//            {
//                loopTiles.Remove(rightTile);
//                currentSearchTiles.Push((rightTile, Direction.Right));
//            }
//            else if (isGround && (currentSearchTile.direction == Direction.None || currentSearchTile.direction == Direction.Right))
//            {
//                currentSearchTiles.Push((rightTile, Direction.None));
//            }
//        }
//        if (upTile is not null)
//        {
//            var isGround = groundTiles.Any(t => t.Row == upTile.Row && t.Column == upTile.Column);
//            var isPipe = upTile.IsPipe(loopTiles, Direction.Up);

//            if (isPipe && (currentSearchTile.direction == Direction.None || currentSearchTile.direction == Direction.Up))
//            {
//                loopTiles.Remove(upTile);
//                currentSearchTiles.Push((upTile, Direction.Up));
//            }
//            else if (isGround && (currentSearchTile.direction == Direction.None || currentSearchTile.direction == Direction.Up))
//            {
//                currentSearchTiles.Push((upTile, Direction.None));
//            }
//        }
//        if (downTile is not null)
//        {
//            var isGround = groundTiles.Any(t => t.Row == downTile.Row && t.Column == downTile.Column);
//            var isPipe = downTile.IsPipe(loopTiles, Direction.Down);

//            if (isPipe && (currentSearchTile.direction == Direction.None || currentSearchTile.direction == Direction.Down))
//            {
//                loopTiles.Remove(downTile);
//                currentSearchTiles.Push((downTile, Direction.Down));
//            }
//            else if (isGround && (currentSearchTile.direction == Direction.None || currentSearchTile.direction == Direction.Down))
//            {
//                currentSearchTiles.Push((downTile, Direction.None));
//            }
//        }
//    }

//    if (isEnclosed)
//    {
//        tilePools.Add(pool.Distinct().ToList());
//    }
//}

//var c = tilePools.Count;
//var d = tilePools.Sum(p => p.Count);