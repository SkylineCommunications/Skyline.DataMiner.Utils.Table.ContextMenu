namespace Skyline.DataMiner.Utils.Table.ContextMenu
{
	using Skyline.DataMiner.Scripting;

	public enum Action
	{
		AddItem = 1,
		DuplicateItem = 2,
		EditItem = 10,
		DeleteSelectedRows = 20,
	}

	public abstract class ContextMenuTableManagerBase : ContextMenu<Action>
	{
		protected ContextMenuTableManagerBase(SLProtocol protocol, object contextMenuData, int tablePid)
			: base(protocol, contextMenuData, tablePid)
		{
		}

		public override void ProcessContextMenuAction()
		{
			switch (Action)
			{
				case Action.AddItem:
				case Action.DuplicateItem:
					// DuplicateItem is exactly the same as AddItem.
					// It just provides user-friendliness by setting default values in all dependency parameters.
					AddItem();
					break;
				case Action.EditItem:
					EditItem();
					break;
				case Action.DeleteSelectedRows:
					DeleteItems();
					break;
				default:
					Protocol.Log("QA" + Protocol.QActionID + "|ContextMenuTableManagerBase|Process|Unexpected ContextMenu value '" + ActionRaw + "'", LogType.Error, LogLevel.NoLogging);
					return;
			}
		}

		protected abstract void AddItem();

		protected abstract void EditItem();

		protected abstract void DeleteItems();
	}
}
