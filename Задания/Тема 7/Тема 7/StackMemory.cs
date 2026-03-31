using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[Serializable]
public class StackMemory
{
    private readonly int _stackDepth;
    private readonly List<byte[]> _list = new List<byte[]>();
    private int _currentIndex = -1;

    public StackMemory(int depth)
    {
        _stackDepth = depth;
        if (depth < 1) _stackDepth = 1;
    }

    /// <summary>
    /// Сохранить текущее состояние списка фигур в стек
    /// </summary>
    public void Push(List<Figure> figures)
    {
        if (_currentIndex < _list.Count - 1)
            _list.RemoveRange(_currentIndex + 1, _list.Count - _currentIndex - 1);

        if (_list.Count >= _stackDepth)
            _list.RemoveAt(0);
        else
            _currentIndex++;

        using (var ms = new MemoryStream())
        {
            var formatter = new BinaryFormatter();
            try
            {
                formatter.Serialize(ms, figures);
                _list.Add(ms.ToArray());
            }
            catch (Exception)
            {
                _currentIndex--;
            }
        }
    }

    /// <summary>
    /// Отменить действие (Undo)
    /// </summary>
    public List<Figure> Undo(List<Figure> currentFigures)
    {
        if (_currentIndex > 0)
        {
            _currentIndex--;
            return Deserialize(_list[_currentIndex]);
        }
        return currentFigures;
    }

    /// <summary>
    /// Вернуть действие (Redo)
    /// </summary>
    public List<Figure> Redo(List<Figure> currentFigures)
    {
        if (_currentIndex < _list.Count - 1)
        {
            _currentIndex++;
            return Deserialize(_list[_currentIndex]);
        }
        return currentFigures;
    }

    private List<Figure> Deserialize(byte[] data)
    {
        using (var ms = new MemoryStream(data))
        {
            var formatter = new BinaryFormatter();
            return (List<Figure>)formatter.Deserialize(ms);
        }
    }

    public void Clear()
    {
        _list.Clear();
        _currentIndex = -1;
    }
}