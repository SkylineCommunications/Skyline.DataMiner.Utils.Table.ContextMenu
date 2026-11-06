namespace Skyline.DataMiner.Utils.Table.ContextMenu
{
	using System;
	using System.Linq;

	using Skyline.DataMiner.Scripting;

	public class ContextMenuTableManagerBasic : ContextMenuTableManagerBase
	{
		private readonly int autoIncPid = -1;

		public ContextMenuTableManagerBasic(SLProtocol protocol, object contextMenuData, int tablePid)
			: base(protocol, contextMenuData, tablePid)
		{
		}

		public ContextMenuTableManagerBasic(SLProtocol protocol, object contextMenuData, int tablePid, int autoIncPid)
			: this(protocol, contextMenuData, tablePid)
		{
			if (autoIncPid < 0)
			{
				throw new ArgumentException(autoIncPid + " is < 0.", nameof(autoIncPid));
			}

			this.autoIncPid = autoIncPid;
		}

		protected override void AddItem()
		{
			// Prepare
			object[] rowData;
			if (autoIncPid > -1)
			{
				int autoIncKey = Convert.ToInt32(Protocol.GetParameter(autoIncPid)) + 1;
				rowData = new object[] { Convert.ToString(autoIncKey) }.Concat(Data).ToArray();
			}
			else
			{
				rowData = Data.ToArray<object>();
			}

			// Sanity Check
			if (Protocol.Exists(TablePid, (string)rowData[0]))
			{
				Protocol.Log("QA" + Protocol.QActionID + "|AddItem|Table '" + TablePid + "' : Row with key '" + rowData[0] + "' already exists.", LogType.Error, LogLevel.NoLogging);
				return;
			}

			////Protocol.Log("QA" + Protocol.QActionID + "|AddItem|Table '" + TablePid + "' : " + String.Join(";", rowData), LogType.DebugInfo, LogLevel.NoLogging);

			// Add
			Protocol.AddRow(TablePid, rowData);
			if (autoIncPid > -1)
			{
				Protocol.SetParameter(autoIncPid, rowData[0]);
			}
		}

		protected override void DeleteItems()
		{
			////Protocol.Log("QA" + Protocol.QActionID + "|DeleteItems|Table '" + TablePid + "' : " + String.Join(";", Data), LogType.DebugInfo, LogLevel.NoLogging);

			Protocol.DeleteRow(TablePid, Data);
		}

		protected override void EditItem()
		{
			string rowKey = Data[Data.Length - 1];
			string[] rowData = Data.Take(Data.Length - 1).ToArray();
			////Protocol.Log("QA" + Protocol.QActionID + "|EditItemEditItem|Table '" + TablePid + "' - rowKey '" + rowKey + "' : " + String.Join(";", rowData), LogType.DebugInfo, LogLevel.NoLogging);

			object[] setRowData = new string[] { null }.Concat(rowData).ToArray<object>();
			Protocol.SetRow(TablePid, rowKey, setRowData);
		}
	}
}
