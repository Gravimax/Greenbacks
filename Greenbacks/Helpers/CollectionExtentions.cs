﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Greenbacks
{
    public static class CollectionExtentions
    {
        /// <summary>
        /// Sends an IEnumerable collection to a ObservableCollection.
        /// </summary>
        /// <typeparam name="T">Object of type T.</typeparam>
        /// <param name="data">The IEnumerable collection to convert.</param>
        /// <returns>ObservableCollection</returns>
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> data)
        {
            ObservableCollection<T> dataToReturn = new ObservableCollection<T>();

            foreach (T t in data)
            {
                dataToReturn.Add(t);
            }

            return dataToReturn;
        }

        /// <summary>
        /// Adds all items from a new collection into the existing collection.
        /// </summary>
        /// <typeparam name="T">Object of type T</typeparam>
        /// <param name="collection">The collection.</param>
        /// <param name="newCollection">Collection of new objects.</param>
        /// <returns>Collection with added items</returns>
        public static ObservableCollection<T> AddRange<T>(this ObservableCollection<T> collection, ObservableCollection<T> newCollection)
        {
            if (newCollection != null)
            {
                foreach (var item in newCollection)
                {
                    collection.Add(item);
                }
            }

            return collection;
        }

        /// <summary>
        /// Adds all items from a new collection into the existing collection.
        /// </summary>
        /// <typeparam name="T">Object of type T</typeparam>
        /// <param name="collection">The collection.</param>
        /// <param name="newCollection">Collection of new objects.</param>
        /// <returns>Collection with added items</returns>
        public static ObservableCollection<T> AddRange<T>(this ObservableCollection<T> collection, IEnumerable<T> newCollection)
        {
            if (newCollection != null)
            {
                foreach (var item in newCollection)
                {
                    collection.Add(item);
                }
            }

            return collection;
        }

        /// <summary>
        /// Removes all the objects from an Observable Collection that match the condition.
        /// </summary>
        /// <typeparam name="T">Object of type T</typeparam>
        /// <param name="coll">The collection.</param>
        /// <param name="condition">The condition.</param>
        /// <returns>Collection without the desired items.</returns>
        public static ObservableCollection<T> RemoveAll<T>(this ObservableCollection<T> collection, Func<T, bool> condition)
        {
            var itemsToRemove = collection.Where(condition).ToList();

            foreach (var itemToRemove in itemsToRemove)
            {
                collection.Remove(itemToRemove);
            }

            return collection;
        }

        /// <summary>
        /// Sorts the specified collection.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="collection">The collection.</param>
        /// <param name="keySelector">The key selector.</param>
        /// <param name="direction">The direction.</param>
        /// <returns>Sorted collection</returns>
        public static ObservableCollection<T> Sort<T, TKey>(this ObservableCollection<T> collection, Func<T, TKey> keySelector, System.ComponentModel.ListSortDirection direction = System.ComponentModel.ListSortDirection.Ascending)
        {
            var sortedItems = collection.ToList();

            switch (direction)
            {
                case System.ComponentModel.ListSortDirection.Ascending:
                    sortedItems.OrderBy(keySelector);
                    break;
                case System.ComponentModel.ListSortDirection.Descending:
                    sortedItems.OrderByDescending(keySelector);
                    break;
            }

            return sortedItems.ToObservableCollection();
        }

        /// <summary>
        /// Sorts the specified collection.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="collection">The collection.</param>
        /// <param name="keySelector">The key selector.</param>
        /// <param name="comparer">The comparer.</param>
        /// <param name="direction">The direction.</param>
        /// <returns>Sorted collection</returns>
        public static ObservableCollection<T> Sort<T, TKey>(this ObservableCollection<T> collection, Func<T, TKey> keySelector, IComparer<TKey> comparer, System.ComponentModel.ListSortDirection direction = System.ComponentModel.ListSortDirection.Ascending)
        {
            var sortedItems = collection.ToList();

            switch (direction)
            {
                case System.ComponentModel.ListSortDirection.Ascending:
                    sortedItems.OrderBy(keySelector, comparer);
                    break;
                case System.ComponentModel.ListSortDirection.Descending:
                    sortedItems.OrderByDescending(keySelector, comparer);
                    break;
            }

            return sortedItems.ToObservableCollection();
        }
    }
}
