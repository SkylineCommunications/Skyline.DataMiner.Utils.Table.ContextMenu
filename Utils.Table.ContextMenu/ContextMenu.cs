namespace Skyline.DataMiner.Utils.Table.ContextMenu
{
	using System;
	using System.Linq;

	using Skyline.DataMiner.Scripting;

	public abstract class ContextMenu<TEnum> where TEnum : struct, Enum
	{
		protected ContextMenu(SLProtocol protocol, object contextMenuData, int tablePid)
		{
			if (protocol == null)
			{
				throw new ArgumentNullException(nameof(protocol));
			}

			if (tablePid < 0)
			{
				throw new ArgumentException(nameof(tablePid) + " is < 0.", nameof(tablePid));
			}

			Protocol = protocol;
			TablePid = tablePid;

			InitializeContextMenuData(contextMenuData);
		}

		public int TablePid { get; private set; }

		public string UserGuid { get; private set; }

		public string ActionRaw { get; private set; }

		public string[] Data { get; private set; }

		protected SLProtocol Protocol { get; }

		protected TEnum Action { get; private set; }

		public abstract void ProcessContextMenuAction();

		private void InitializeContextMenuData(object contextMenuData)
		{
			if (contextMenuData == null)
			{
				throw new ArgumentNullException(nameof(contextMenuData));
			}

			if (!(contextMenuData is string[] allData))
			{
				throw new ArgumentException(nameof(contextMenuData) + " is not a string[].", nameof(contextMenuData));
			}

			if (allData.Length < 2)
			{
				throw new ArgumentException(
					nameof(contextMenuData) + " doesn't have the expected length. Expected Length '2 or more' - Actual Length '" + allData.Length + "'.",
					nameof(contextMenuData));
			}

			UserGuid = allData[0];

			ActionRaw = allData[1];
			if (!Enum.TryParse(ActionRaw, out TEnum actionTemp))
			{
				throw new ArgumentException(nameof(contextMenuData) + "[1] is not a" + nameof(TEnum), nameof(contextMenuData));
			}

			Action = actionTemp;
			Data = allData.Skip(2).ToArray();
		}
	}
}
