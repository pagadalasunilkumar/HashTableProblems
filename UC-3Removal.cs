using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashTableProjects
{
    internal class UC_3Removal
    {
        class MyMapNode<TKey, TValue>
        {
            public TKey Key { get; set; }
            public TValue Value { get; set; }
        }

        class MyHashTable<TKey, TValue>
        {
            private LinkedList<MyMapNode<TKey, TValue>>[] buckets;
            private int size;

            public MyHashTable(int size)
            {
                this.size = size;
                this.buckets = new LinkedList<MyMapNode<TKey, TValue>>[size];
            }

            private int GetBucketIndex(TKey key)
            {
                int hash = key.GetHashCode();
                int index = hash % size;
                return Math.Abs(index);
            }

            public TValue Get(TKey key)
            {
                int index = GetBucketIndex(key);
                var linkedList = buckets[index];
                if (linkedList != null)
                {
                    foreach (var node in linkedList)
                    {
                        if (node.Key.Equals(key))
                            return node.Value;
                    }
                }
                return default(TValue);
            }

            public void Add(TKey key, TValue value)
            {
                int index = GetBucketIndex(key);
                var linkedList = buckets[index];
                if (linkedList == null)
                {
                    linkedList = new LinkedList<MyMapNode<TKey, TValue>>();
                    buckets[index] = linkedList;
                }
                foreach (var node in linkedList)
                {
                    if (node.Key.Equals(key))
                    {
                        node.Value = value;
                        return;
                    }
                }
                var newNode = new MyMapNode<TKey, TValue> { Key = key, Value = value };
                linkedList.AddLast(newNode);
            }

            public void Remove(TKey key)
            {
                int index = GetBucketIndex(key);
                var linkedList = buckets[index];
                if (linkedList != null)
                {
                    var node = linkedList.First;
                    while (node != null)
                    {
                        if (node.Value.Key.Equals(key))
                        {
                            linkedList.Remove(node);
                            return;
                        }
                        node = node.Next;
                    }
                }
            }
        }

        class Program
        {
            static void Main()
            {
                string phrase = "Paranoids are not paranoid because they are paranoid but because they keep putting themselves deliberately into paranoid avoidable situations";
                string[] words = phrase.Split(' ');

                MyHashTable<string, int> wordFrequency = new MyHashTable<string, int>(words.Length);

                foreach (string word in words)
                {
                    int frequency = wordFrequency.Get(word);
                    wordFrequency.Add(word, frequency + 1);
                }

                wordFrequency.Remove("avoidable");

                foreach (var bucket in wordFrequency)
                {
                    if (bucket != null)
                    {
                        foreach (var node in bucket)
                        {
                            Console.WriteLine($"Word: {node.Key}, Frequency: {node.Value}");
                        }
                    }
                }
            }
        }
    }
}
