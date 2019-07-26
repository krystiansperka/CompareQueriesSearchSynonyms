using System;
using System.Collections;
using System.Collections.Generic;

/*
Given a list of synonym pairs, write a function to determine if some pairs of queries are synonyms. 
Two queries are synonyms if you can match them (considering synonyms), word by word in order.
=================================
Example:

Synonyms:
[(approval, popularity), (rate, percentage), (fatality, casualty)]

Queries:
[("approval rate", "popularity percentage"), ("car fatality rate", "car casualty percentage"), ("approval rate", "popularity ratings")]

Expected Output: [True, True, False]
*/

namespace Findsynonyms
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var example = new Example();
            example.LoadData();
            example.LoadDictionary();
            example.DetermineSynonyms();
            example.DetermineSynonymsWithDictionary();

        }
    }

    public class PairOfSynonyms
    {
        public string Value1 { get; set; }
        public string Value2 { get; set; }
    }

    public class PairOfQueries
    {
        public string Query1 { get; set; }
        public string Query2 { get; set; }
    }



    public class Example
    {
        // Create a list of pairs
        public List<PairOfSynonyms> listOfSynonims = new List<PairOfSynonyms>();

        // Create a list of queries
        public List<PairOfQueries> listOfQueries = new List<PairOfQueries>();

        // Create list of results
        public List<bool> listOfResults = new List<bool>();

        // Define delimiter like { ' ', ',', '.', ':', '\t' };
        public char[] delimiterChars = { ' ' };

        //Dictionary of Synonyms
        public Dictionary<string, string> dictionaryOfSynonyms = new Dictionary<string, string>();

        public void LoadData()
        {
            // Add Synonyms to list
            listOfSynonims.Add(new PairOfSynonyms() { Value1 = "approval", Value2 = "popularity" });
            listOfSynonims.Add(new PairOfSynonyms() { Value1 = "rate", Value2 = "percentage" });
            listOfSynonims.Add(new PairOfSynonyms() { Value1 = "fatality", Value2 = "casualty" });
            //.....


            // Add Queries to list
            listOfQueries.Add(new PairOfQueries() { Query1 = "approval rate", Query2 = "popularity percentage" });
            listOfQueries.Add(new PairOfQueries() { Query1 = "car fatality rate", Query2 = "car casualty percentage" });
            listOfQueries.Add(new PairOfQueries() { Query1 = "approval rate", Query2 = "popularity ratings" });
            //.....

        }

        public void LoadDictionary()
        {
            foreach (PairOfSynonyms pairOfSynonyms in listOfSynonims)
            {
                if (!dictionaryOfSynonyms.ContainsKey(pairOfSynonyms.Value1))
                {
                    dictionaryOfSynonyms.Add(pairOfSynonyms.Value1, pairOfSynonyms.Value2);
                }

                if (!dictionaryOfSynonyms.ContainsKey(pairOfSynonyms.Value2))
                {
                    dictionaryOfSynonyms.Add(pairOfSynonyms.Value2, pairOfSynonyms.Value1);
                }
            }
        }


        public List<bool> DetermineSynonyms()
        {
            foreach (PairOfQueries pairOfQueries in listOfQueries)
            {
                string[] wordsInQuery1 = pairOfQueries.Query1.Split(delimiterChars);
                string[] wordsInQuery2 = pairOfQueries.Query2.Split(delimiterChars);
                bool result = false;
                int countWordsInQuery = 0;

                foreach (var word in wordsInQuery1)
                {
                    var word2 = wordsInQuery2[countWordsInQuery];
                    result = false;

                    if (word == word2) result = true;
                    else
                    {
                        foreach (PairOfSynonyms pairOfSynonyms in listOfSynonims)
                        {
                            if ((word == pairOfSynonyms.Value1 | word == pairOfSynonyms.Value2) & (word2 == pairOfSynonyms.Value1 | word2 == pairOfSynonyms.Value2))
                            {
                                result = true;
                                break; 
                            }
                        }
                       
                    }

                    if (result == false) break; 
                    countWordsInQuery++;
                }

                listOfResults.Add(result);

            }

            return listOfResults;
        }

        public List<bool> DetermineSynonymsWithDictionary()
        {
            foreach (PairOfQueries pairOfQueries in listOfQueries)
            {
                string[] wordsInQuery1 = pairOfQueries.Query1.Split(delimiterChars);
                string[] wordsInQuery2 = pairOfQueries.Query2.Split(delimiterChars);
                bool result = false;
                int countWordsInQuery = 0;

                foreach (var word in wordsInQuery1)
                {
                    var word2 = wordsInQuery2[countWordsInQuery];
                    result = false;

                    if (word == word2) result = true;
                    else
                    {
                        if (dictionaryOfSynonyms.TryGetValue(word, out string wordFromDict))
                        {
                            if (word2 == wordFromDict) result = true;
                        }

                    }

                    if (result == false) break;
                    countWordsInQuery++;
                }

                listOfResults.Add(result);

            }

            return listOfResults;
        }
    }
}
