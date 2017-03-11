using FakeTrello.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeTrello.DAL
{
    interface IRepository
    {
        // List of methods to help deliver features
        // Create
        void AddBoard(string name, ApplicationUser owner);
        void AddList(string name, Board board);
        void AddList(string name, int boardId);
        void AddCard(string name, List list);
        void AddCard(int listId, int ownerId);

        // Read
        List<Card> GetCardsFromList(int listId);
        List<Card> GetCardFromBoard(int boardId);
        Card GetCard(int cardId);
        List GetList(int listId);
        List<List> GetListFromBoard(int boardId); // List of Trello Lists
        List<Board> GetBoardFromUser(string userId);
        Board GetBoard(int boardId);
        List<ApplicationUser> GetCardAttendees(int cardId);
       
        // Update
        bool AttachedUser(int userId, int cardId);
        bool MoveCard(int cardId, int oldListId, int newListId);
        bool CopyCard(int cardId, int newListId, int newOwnerId);

        // Delete
        void RemoveBoard(int boardId);
        void RemoveList(int listId);
        bool RemoveCard(int cardId);
    }
}
