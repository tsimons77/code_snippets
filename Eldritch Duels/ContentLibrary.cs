﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace eldritch.cards 
{
    [System.Serializable]
    public class ContentLibrary : MonoBehaviour
    {
        #region cards
        [SerializeField]
        public List<Card> _cards = new List<Card>();
        public void AddCard(Card card)
        {
            for(int i = 0; i < _cards.Count; i++)
            {
                if(_cards[i].CardName == card.CardName)
                {
                    _cards[i] = card;
                    return;
                }
            }
            if(_cards.Count == 0)
            {
                _cards.Add(card);
            }
            for (int i = 0; i < _cards.Count; i++)
            {
                if (_cards[i].CardID > card.CardID)
                {
                    _cards.Insert(i, card);
                    return;
                }
            }
            _cards.Add(card);
            

        }

        public Card GetCard(int cardID) //get a card by id
        {
            foreach(Card c in _cards) //Unity does not save states of hashmaps so a list was required
            {
                if(c.CardID == cardID)
                {
                    return c;
                }
            }
            return null;
        }
        public Card GetCard(string cardName) //get card by name
        {
            foreach (Card c in _cards)  //Unity does not save states of hashmaps so a list was required
            {
                
                if (c.CardName.Trim().Equals(cardName.Trim()))
                {
                    Debug.Log("Return card");
                    return c;
                }
            }
            return null;
        }



        public List<Card> GetAllCards()
        {
            return _cards;
        }
        public void RemoveCard(string cardName) //remove a card 
        {
            for(int i = 0; i < _cards.Count; i++)  //Unity does not save states of hashmaps so a list was required
            {
                if (_cards[i].CardName.Equals(cardName))
                {
                    _cards.RemoveAt(i);
                    return;
                }
            }
        }
        public int GetNextID() //get next available ID
        {
            if(_cards.Count == 0)
            {
                return 0;
            }
            for(int i = 0; i < _cards.Count-1; i++)
            {
                if(_cards[i].CardID - _cards[i+1].CardID > 1)
                {
                    return _cards[i].CardID + 1;
                }
            }
            return _cards[_cards.Count - 1].CardID + 1;
        }
        #endregion

        #region crafting
        public List<CraftingRecipe> recipes = new List<CraftingRecipe>();
        public void AddRecipe(CraftingRecipe recipe)
        {
            for(int i = 0; i < recipes.Count;i++)
            {
                if(recipe.BaseCard.Equals(recipes[i].BaseCard) && recipe.FodderCard.Equals(recipes[i].FodderCard))
                {
                    recipes[i] = recipe;
                    return;
                }
            }
            recipes.Add(recipe);
        }
        public void AddRecipe(string baseCard, string fodderCard, string resultCard, int craftCost)
        {
            CraftingRecipe cr;
            cr.BaseCard = baseCard;
            cr.FodderCard = fodderCard;
            cr.ResultCard = resultCard;
            cr.CraftCost = craftCost;
            AddRecipe(cr);
        }
        public CraftingRecipe PreviewCraft(string baseCard, string fodderCard)
        {
            foreach(CraftingRecipe cr in this.recipes)
            {
                if(cr.BaseCard.Equals(baseCard) && cr.FodderCard.Equals(fodderCard))
                {
                    return cr;
                }
            }
            
            
            return CraftingRecipe.Empty;
        }
        public void RemoveRecipe(CraftingRecipe cr)
        {
            for(int i = 0; i < recipes.Count; i++)
            {
                if(cr.BaseCard.Equals(recipes[i].BaseCard) && cr.FodderCard.Equals(recipes[i].FodderCard))
                {
                    recipes.RemoveAt(i);
                }
            }
        }
        public List<CraftingRecipe> GetAllRecipes()
        {
            return recipes;
        }


        #endregion
    }
}
