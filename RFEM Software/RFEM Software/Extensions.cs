using System.Collections.Generic;
using System.Windows.Media;

public static class Extensions
{
    /// <summary>
    /// This extension method was taken from stack overflow. It allows the visual tree to be searched
    /// for all children controls. It is used to find all of the context menus in a form.
    /// </summary>
    /// <param name="parent"></param>
    /// <param name="recurse"></param>
    /// <returns></returns>
    public static IEnumerable<Visual> GetChildren(this Visual parent, bool recurse = true)
    {
        if (parent != null)
        {
            int count = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < count; i++)
            {
                // Retrieve child visual at specified index value.
                var child = VisualTreeHelper.GetChild(parent, i) as Visual;

                if (child != null)
                {
                    yield return child;

                    if (recurse)
                    {
                        foreach (var grandChild in child.GetChildren(true))
                        {
                            yield return grandChild;
                        }
                    }
                }
            }
        }
    }
}
