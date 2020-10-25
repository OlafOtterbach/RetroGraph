using RetroGraph.HiddenLine.Model;
using System.Collections.Generic;

namespace RetroGraph.HiddenLine.Services
{
    public static class LineClipping
    {
        public static IEnumerable<LineHL> Clipp(this IEnumerable<LineHL> lines, double left, double right, double bottom, double top)
        {
            var clippedLines = lines.ClippAtLeft(left).ClippAtRight(right).ClippAtBottom(bottom).ClippAtTop(top);
            return clippedLines;
        }

        private static IEnumerable<LineHL> ClippAtLeft(this IEnumerable<LineHL> lines, double left)
        {
            foreach (var line in lines)
            {
                (bool success, LineHL clippedLine) = line.ClippAtLeft(left);
                if (success)
                {
                    yield return clippedLine;
                }
            }
        }

        private static (bool, LineHL) ClippAtLeft(this LineHL line, double left)
        {
            var start = line.Start;
            var end = line.End;
            if ((start.X < left) && (end.X < left))
            {
                return (false, null);
            }

            if ((start.X >= left) && (end.X >= left))
            {
                return (true, line);
            }
            else
            {
                if (start.X > end.X)
                {
                    var help = start;
                    start = end;
                    end = help;
                }
                double qx = (left - start.X) / (end.X - start.X);
                double deltay = qx * (end.Y - start.Y);
                var clippedStart = new PointHL(left, start.Y + deltay);
                return (true, new LineHL { Start = clippedStart, End = end, Edge = line.Edge });
            }
        }

        private static IEnumerable<LineHL> ClippAtRight(this IEnumerable<LineHL> lines, double right)
        {
            foreach (var line in lines)
            {
                var start = line.Start;
                var end = line.End;
                if ((start.X <= right) || (end.X <= right))
                {
                    if ((start.X <= right) && (end.X <= right))
                    {
                        yield return line;
                    }
                    else
                    {
                        if (start.X > end.X)
                        {
                            var help = start;
                            start = end;
                            end = help;
                        }
                        double qx = (right - start.X) / (end.X - start.X);
                        double deltay = qx * (end.Y - start.Y);
                        var clippedEnd = new PointHL(right, start.Y + deltay);
                        yield return new LineHL { Start = start, End = clippedEnd, Edge = line.Edge };
                    }
                }
            }
        }

        private static IEnumerable<LineHL> ClippAtBottom(this IEnumerable<LineHL> lines, double bottom)
        {
            foreach (var line in lines)
            {
                var start = line.Start;
                var end = line.End;
                if ((start.Y >= bottom) || (end.Y >= bottom))
                {
                    if ((start.Y >= bottom) && (end.Y >= bottom))
                    {
                        yield return line;
                    }
                    else
                    {
                        if (start.Y > end.Y)
                        {
                            var help = start;
                            start = end;
                            end = help;
                        }
                        double qy = (bottom - start.Y) / (end.Y - start.Y);
                        double deltax = qy * (end.X - start.X);
                        var clippedStart = new PointHL(start.X + deltax, bottom);
                        yield return new LineHL { Start = clippedStart, End = end, Edge = line.Edge };
                    }
                }
            }
        }

        private static IEnumerable<LineHL> ClippAtTop(this IEnumerable<LineHL> lines, double top)
        {
            foreach (var line in lines)
            {
                var start = line.Start;
                var end = line.End;
                if ((start.Y <= top) || (end.Y <= top))
                {
                    if ((start.Y <= top) && (end.Y <= top))
                    {
                        yield return line;
                    }
                    else
                    {
                        if (start.Y > end.Y)
                        {
                            var help = start;
                            start = end;
                            end = help;
                        }
                        double qy = (top - start.Y) / (end.Y - start.Y);
                        double deltax = qy * (end.X - start.X);
                        var clippedEnd = new PointHL(start.X + deltax, top);
                        yield return new LineHL { Start = start, End = clippedEnd, Edge = line.Edge };
                    }
                }
            }
        }

    }
}
