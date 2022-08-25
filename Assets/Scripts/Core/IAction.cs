namespace Game.Core
{
    public interface IAction
    {
        /// <summary>
        /// Method to cancel current action so transition to other is inmediate
        /// </summary>
        void Cancel();
    }
}