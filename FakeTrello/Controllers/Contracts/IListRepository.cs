using FakeTrello.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeTrello.Controllers.Contracts
{
    interface IListRepository
    {
        // add
        void AddList(string name, Board board);
        void AddList(string name, int boardId);

        // read method
        List GetList(int listId);
        List<List> GetListsFromBoard(int boardId); // List of Trello Lists

        // delete
        bool RemoveList(int listId);
    }
}
