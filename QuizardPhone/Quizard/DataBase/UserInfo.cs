using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Quizard.DataBase
{
    class UserInfo
    {
        User QuizUser;
        List<Sets> QuizSets;
        List<Cards> QuizCards;

        public UserInfo()
        {
            QuizUser = new User();
            QuizSets = new List<Sets>();
            QuizCards = new List<Cards>();
        }

        public UserInfo(User m_User, List<Sets> m_Sets, List<Cards> m_Cards)
        {
            QuizUser = m_User;
            QuizSets = m_Sets;
            QuizCards = m_Cards;
        }

        #region Accessors & Mutators
        public User GetUser()
        {
            return QuizUser;
        }
        public List<Sets> GetSets()
        {
            return QuizSets;
        }
        public List<Cards> GetCards()
        {
            return QuizCards;
        }
        public void SetUser(User m_User)
        {
            QuizUser = m_User;
        }
        public void SetQuizSets(List<Sets> m_Sets)
        {
            QuizSets = m_Sets;
        }
        public void SetCards(List<Cards> m_Cards)
        {
            QuizCards = m_Cards;
        }
        #endregion

        //Methods
        public void AddSet(Sets m_Set)
        {
            QuizSets.Add(m_Set);
        }
        public void RemoveSet(Sets m_Set)
        {
            QuizSets.Remove(m_Set);
        }
        public void AddCard(Cards m_Card)
        {
            QuizCards.Add(m_Card);
        }
        public void RemoveCard(Cards m_Card)
        {
            QuizCards.Remove(m_Card);
        }
    }
}