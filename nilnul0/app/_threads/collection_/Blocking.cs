using System;
using System.Collections.Concurrent;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nilnul._app_._TEST_.nilnul0.app._threads.collection_
{

	public class ConcurrentPriorityQueue<T> : IProducerConsumerCollection<T>
	{
		private object key = new object();
		private SortedSet<Tuple<T, int>> set;

		private Func<T, int> prioritySelector;

		public ConcurrentPriorityQueue(Func<T, int> prioritySelector, IComparer<T> comparer = null)
		{
			this.prioritySelector = prioritySelector;
			set = new SortedSet<Tuple<T, int>>(
				new MyComparer<T>(comparer ?? Comparer<T>.Default));
		}

		private class MyComparer<T> : IComparer<Tuple<T, int>>
		{
			private IComparer<T> comparer;
			public MyComparer(IComparer<T> comparer)
			{
				this.comparer = comparer;
			}
			public int Compare(Tuple<T, int> first, Tuple<T, int> second)
			{
				var returnValue = first.Item2.CompareTo(second.Item2);
				if (returnValue == 0)
					returnValue = comparer.Compare(first.Item1, second.Item1);
				return returnValue;
			}
		}

		public bool TryAdd(T item)
		{
			lock (key)
			{
				return set.Add(Tuple.Create(item, prioritySelector(item)));
			}
		}

		public bool TryTake(out T item)
		{
			lock (key)
			{
				if (set.Count > 0)
				{
					var first = set.First();
					item = first.Item1;
					return set.Remove(first);
				}
				else
				{
					item = default(T);
					return false;
				}
			}
		}

		public bool ChangePriority(T item, int oldPriority, int newPriority)
		{
			lock (key)
			{
				if (set.Remove(Tuple.Create(item, oldPriority)))
				{
					return set.Add(Tuple.Create(item, newPriority));
				}
				else
					return false;
			}
		}

		/// <summary>
		/// with the lock, this method is safe even when this is wrapped inner of a <see cref="BlockingCollection{T}"/>
		/// </summary>
		/// <param name="item"></param>
		/// <returns></returns>

		public bool ChangePriority(T item)
		{
			lock (key)
			{
				var result = set.FirstOrDefault(pair => object.Equals(pair.Item1, item));

				if (object.Equals(result.Item1, item))
				{
					return ChangePriority(item, result.Item2, prioritySelector(item));
				}
				else
				{
					return false;
				}
			}
		}

		public void CopyTo(T[] array, int index)
		{
			lock (key)
			{
				foreach (var item in set.Select(pair => pair.Item1))
				{
					array[index++] = item;
				}
			}
		}

		public T[] ToArray()
		{
			lock (key)
			{
				return set.Select(pair => pair.Item1).ToArray();
			}
		}
		public void CopyTo(Array array, int index)
		{
			lock (key)
			{
				foreach (var item in set.Select(pair => pair.Item1))
				{
					array.SetValue(item, index++);
				}
			}
		}

		public IEnumerator<T> GetEnumerator()
		{
			return ToArray().AsEnumerable().GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}


		public int Count
		{
			get { lock (key) { return set.Count; } }
		}

		public bool IsSynchronized
		{
			get { return true; }
		}

		public object SyncRoot
		{
			get { return key; }
		}
	}
}
/*
  https://stackoverflow.com/questions/14941027/prioritized-queues-in-task-parallel-library

As for actual implementation, they're not efficiently implemented, I just lock around everything. Creating more efficient implementations would prevent the use of SortedSet as a priority queue, and re-implementing one of those that can be effectively accessed concurrently is not going to be that easy.

In order to change the priority of an item you'll need to remove the item from the set and then add it again, and to find it without iterating the whole set you'd need to know the old priority as well as the new priority.

Once you have an IProducerConsumerCollection<T> instance, which the above object is, you can use it as the internal backing object of a BlockingCollection<T> in order to have an easier to use user interface.

Instead of var first = set.First(); I would prefer var first = set.Min. The Min property should be slightly more efficient. – 
Theodor Zoulias
 Apr 15 at 2:19
*/
/*
 https://learn.microsoft.com/en-us/dotnet/api/system.collections.concurrent.iproducerconsumercollection-1?f1url=%3FappId%3DDev16IDEF1%26l%3DEN-US%26k%3Dk(System.Collections.Concurrent.IProducerConsumerCollection%25601)%3Bk(SolutionItemsProject)%3Bk(TargetFrameworkMoniker-.NETFramework%2CVersion%253Dv4.8.1)%3Bk(DevLang-csharp)%26rd%3Dtrue&view=net-7.0
 */