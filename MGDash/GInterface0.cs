namespace MGDash
{
    using System;

    public interface IControllerEvents
    {
        int GetPriority();
        bool OnControllerAccept();
        bool OnControllerBack();
        bool OnControllerDetails();
        bool OnControllerDown();
        bool OnControllerLeft();
        bool OnControllerRight();
        bool OnControllerSort();
        bool OnControllerUp();
    }
}

