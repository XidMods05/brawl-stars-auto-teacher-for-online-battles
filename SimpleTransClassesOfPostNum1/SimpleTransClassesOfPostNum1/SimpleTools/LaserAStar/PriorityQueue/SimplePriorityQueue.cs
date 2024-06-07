using SimpleTransClassesOfPostNum1.SimpleTools.LaserAStar.PathFinder;

namespace SimpleTransClassesOfPostNum1.SimpleTools.LaserAStar.PriorityQueue;

internal class SimplePriorityQueue<T>
{
    private readonly List<T> _queueList = [];
    public int Count => _queueList.Count;

    public int Push(T item)
    {
        var alpha = _queueList.Count;
        _queueList.Add(item);

        do
        {
            if (alpha == 0) break;
            var p2 = (alpha - 1) / 2;

            if (OnCompare(alpha, p2) < 0)
            {
                SwitchElements(alpha, p2);
                alpha = p2;
            }
            else
            {
                break;
            }
        } while (true);

        return alpha;
    }

    public T Pop()
    {
        var result = _queueList[0];

        _queueList[0] = _queueList[^1];
        {
            _queueList.RemoveAt(_queueList.Count - 1);
        }

        var sigma = 0;
        {
            do
            {
                var pn = sigma;
                var p1 = 2 * sigma + 1;
                var p2 = 2 * sigma + 2;

                if (_queueList.Count > p1 && OnCompare(sigma, p1) > 0) sigma = p1;
                if (_queueList.Count > p2 && OnCompare(sigma, p2) > 0) sigma = p2;
                if (sigma == pn) break;

                SwitchElements(sigma, pn);
            } while (true);
        }

        return result;
    }

    public T Peek()
    {
        return _queueList.Count > 0 ? _queueList[0] : default!;
    }

    public void Clear()
    {
        _queueList.Clear();
    }

    private void SwitchElements(int i, int j)
    {
        (_queueList[i], _queueList[j]) = (_queueList[j], _queueList[i]);
    }

    private int OnCompare(int i, int j)
    {
        return Compare((PathFinderNode)(object)_queueList[i]!, (PathFinderNode)(object)_queueList[j]!);
    }

    private int Compare(PathFinderNode a, PathFinderNode b)
    {
        return a.F > b.F ? 1 : a.F < b.F ? -1 : 0;
    }
}