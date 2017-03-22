using FakeTrello.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeTrello.Controllers.Contracts
{
    interface IBoardManager
    {
        void AddBoard(string name, ApplicationUser owner);
        void EditBoardName(int boardId, string newname);
        bool RemoveBoard(int boardId);
    }
}
