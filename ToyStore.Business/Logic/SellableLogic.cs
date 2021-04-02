using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using ToyStore.Models.Abstracts;
using ToyStore.Models.DBModels;
using ToyStore.Repository.Models;

namespace ToyStore.Business.Logic
{
    public class SellableLogic
    {
        private readonly ToyRepository _toyRepository;
        public SellableLogic(ToyRepository toyRepository)
        {
            this._toyRepository = toyRepository;
        }
        public List<SellableStack> GetSellableStacksWithTag(Tag tag)
        {
            return _toyRepository.GetSellableStacksByTag(tag);
        }

        public List<Tag> GetAvailableTags()
        {
            return _toyRepository.GetAvailableTags().ToList();
        }

        public List<SellableStack> GetAllSellables()
        {
            var sellableStacks = _toyRepository.GetAllSellableStacks();
            // System.Console.WriteLine(SerializeSellableStackList(sellableStacks));
            return sellableStacks;
        }

        public object GetStackPageDetails(Guid SellableStackId)
        {
            var stack = GetSellableStackById(SellableStackId);
            var alternates = _toyRepository.GetSellableStacksWithItem(stack.Item.SellableId);
            return new { Stack = stack, Alternatives = alternates };
        }

        public SellableStack GetSellableStackById(Guid Id)
        {
            return _toyRepository.GetSellableStackByIdDb(Id);
        }

        public int TestMethod(int i)
        {
            i = i + 1;
            i = i + 1;
            i = i + 1;
            i = i + 1;
            return i;
        }

        public string SerializeSellableStackList(List<SellableStack> sellableStackList)
        {

            string serialized = "[\n";

            int stackCounter = sellableStackList.Count - 1;
            sellableStackList.ForEach(sellableStack =>
            {
                serialized += " {\n";
                serialized += "  \"Item\": {\n";
                // starting the item
                // serialized += "\t\t\"SellableName\": \"" + sellableStack.Item.SellableName + "\",\n";
                serialized += GetJsonProperty(3, "SellableName", sellableStack.Item.SellableName);
                serialized += GetJsonProperty(3, "SellablePrice", sellableStack.Item.SellablePrice.ToString(), vQuotations: false);
                serialized += GetJsonProperty(3, "SellableImagePath", sellableStack.Item.SellableImagePath);
                if (sellableStack.Item.Products != null && sellableStack.Item.Products.Count > 0)
                {
                    var sellable = sellableStack.Item;
                    serialized += "   \"Products\": [\n";
                    int productCount = sellable.Products.Count - 1;
                    sellable.Products.ForEach(product =>
                    {
                        serialized += "    {\n";
                        serialized += GetJsonProperty(5, "SellableName", product.SellableName);
                        serialized += GetJsonProperty(5, "SellablePrice", product.SellablePrice.ToString(), vQuotations: false);
                        serialized += GetJsonProperty(5, "SellableImagePath", product.SellableImagePath);
                        serialized += "     \"TagList\": [\n";
                        int tagCounter = product.Tags.Count - 1;
                        product.Tags.ForEach(tag =>
                        {
                            serialized += "      {\n";
                            serialized += GetJsonProperty(7, "TagName", tag.TagName, comma: false);
                            serialized += "      }";
                            if (tagCounter <= 0)
                            {
                                serialized += "\n";
                            }
                            else
                            {
                                serialized += ",\n";
                            }
                            tagCounter--;
                        });
                        serialized += "     ]\n";
                        serialized += "    }";
                        if (productCount <= 0)
                        {
                            serialized += "\n";
                        }
                        else
                        {
                            serialized += ",\n";
                        }
                        productCount--;
                    });
                    // closing product list
                    serialized += "   ],\n";
                }
                serialized += "   \"TagList\": [\n";
                int tagCounter = sellableStack.Item.Tags.Count - 1;
                sellableStack.Item.Tags.ForEach(tag =>
                {
                    serialized += "    {\n";
                    serialized += GetJsonProperty(5, "TagName", tag.TagName, comma: false);
                    serialized += "    }";
                    if (tagCounter <= 0)
                    {
                        serialized += "\n";
                    }
                    else
                    {
                        serialized += ",\n";
                    }
                    tagCounter--;
                });
                //ending tag list
                serialized += "   ]\n";

                // ending item
                serialized += "  },\n";
                serialized += GetJsonProperty(2, "Count", sellableStack.Count.ToString(), vQuotations: false, false);

                // ending sellablestack
                serialized += " }";

                if (stackCounter <= 0)
                {
                    serialized += "\n";
                }
                else
                {
                    serialized += ",\n";
                }
                stackCounter--;
            });
            // ending stack list
            serialized += "]";

            return serialized;
        }

        public List<Customer> GetCustomersForSellableWId(Guid id)
        {
            return _toyRepository.GetCustomersWhoBoughtStack(id);
        }

        public object GetAnyStackWithSellableId(Guid sellableId)
        {
            var stack = _toyRepository.GetStackwithSellableId(sellableId);
            var alternates = _toyRepository.GetSellableStacksWithItem(stack.Item.SellableId);
            return new { Stack = stack, Alternatives = alternates };
        }

        private string GetJsonProperty(int depth, string Name, string value, bool vQuotations = true, bool comma = true)
        {
            // string d = "";
            // for (int i = 0; i < depth; i++)
            // {
            //     d += "\t";
            // }
            string d = new string(' ', depth);
            string stringProp = d;
            stringProp += "\"" + Name + "\": ";
            if (vQuotations)
            {
                stringProp += "\"" + value + "\"";
            }
            else
            {
                stringProp += value;
            }
            if (comma)
            {
                stringProp += ",\n";
            }
            else
            {
                stringProp += "\n";
            }
            return stringProp;
        }

        public void seedDB()
        {
            _toyRepository.SeedDB();
        }
    }
}