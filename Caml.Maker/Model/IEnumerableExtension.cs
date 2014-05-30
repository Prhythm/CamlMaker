using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Caml.Maker.Model
{
    public enum ActionResult : int
    {
        Continue = 0, Break = -1, None = 1
    }

    public static class GenericEachExtension
    {
        public static IEnumerable<T> Each<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (T t in source)
            {
                action.Invoke(t);
            }
            return source;
        }

        public static IEnumerable<T> Each<T>(this IEnumerable<T> source, Func<T, ActionResult> action)
        {
            foreach (T t in source)
            {
                switch (action.Invoke(t))
                {
                    case ActionResult.Break:
                        return source;
                    case ActionResult.Continue:
                        continue;
                    case ActionResult.None:
                    default:
                        break;
                }
            }
            return source;
        }

        public static IEnumerable<T> Each<T>(this IEnumerable<T> source, Action<T, int> action)
        {
            int i = 0;
            foreach (T t in source)
            {
                action.Invoke(t, i);
                i++;
            }
            return source;
        }

        public static IEnumerable<T> Each<T>(this IEnumerable<T> source, Func<T, int, ActionResult> action)
        {
            int i = 0;
            foreach (T t in source)
            {
                switch (action.Invoke(t, i))
                {
                    case ActionResult.Break:
                        return source;
                    case ActionResult.Continue:
                        continue;
                    case ActionResult.None:
                    default:
                        break;
                }
                i++;
            }
            return source;
        }
    }

    public static class EachExtension
    {
        public static IEnumerable Each<T>(this IEnumerable source, Action<T> action)
        {
            foreach (T t in source)
            {
                action.Invoke(t);
            }
            return source;
        }

        public static IEnumerable Each<T>(this IEnumerable source, Func<T, ActionResult> action)
        {
            foreach (T t in source)
            {
                switch (action.Invoke(t))
                {
                    case ActionResult.Break:
                        return source;
                    case ActionResult.Continue:
                        continue;
                    case ActionResult.None:
                    default:
                        break;
                }
            }
            return source;
        }

        public static IEnumerable Each<T>(this IEnumerable source, Action<T, int> action)
        {
            int i = 0;
            foreach (T t in source)
            {
                action.Invoke(t, i);
                i++;
            }
            return source;
        }

        public static IEnumerable Each<T>(this IEnumerable source, Func<T, int, ActionResult> action)
        {
            int i = 0;
            foreach (T t in source)
            {
                switch (action.Invoke(t, i))
                {
                    case ActionResult.Break:
                        return source;
                    case ActionResult.Continue:
                        continue;
                    case ActionResult.None:
                    default:
                        break;
                }
                i++;
            }
            return source;
        }

        public static IEnumerable Each(this IEnumerable source, Action<object> action)
        {
            foreach (object t in source)
            {
                action.Invoke(t);
            }
            return source;
        }

        public static IEnumerable Each(this IEnumerable source, Func<object, ActionResult> action)
        {
            foreach (object t in source)
            {
                switch (action.Invoke(t))
                {
                    case ActionResult.Break:
                        return source;
                    case ActionResult.Continue:
                        continue;
                    case ActionResult.None:
                    default:
                        break;
                }
            }
            return source;
        }

        public static IEnumerable Each(this IEnumerable source, Action<object, int> action)
        {
            int i = 0;
            foreach (object t in source)
            {
                action.Invoke(t, i);
                i++;
            }
            return source;
        }

        public static IEnumerable Each(this IEnumerable source, Func<object, int, ActionResult> action)
        {
            int i = 0;
            foreach (object t in source)
            {
                switch (action.Invoke(t, i))
                {
                    case ActionResult.Break:
                        return source;
                    case ActionResult.Continue:
                        continue;
                    case ActionResult.None:
                    default:
                        break;
                }
                i++;
            }
            return source;
        }
    }
}
