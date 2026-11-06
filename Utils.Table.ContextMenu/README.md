# Skyline.DataMiner.Utils.Table.ContextMenu

## About

Skyline.DataMiner.Utils.Table.ContextMenu is a NuGet package in the public [nuget store](https://www.nuget.org/) that contain assemblies that assist in developing connectors that want to use ContextMenu's on tables.

The NuGet contains a class called ContextMenuTableManagerBasic allowing the basic actions on a table:

- AddItem
- DeleteItems
- EditItems

It also provides the ability to override the base class and create your own actions in addition to or instead of the basic actions.

> Documentation: [Table Context Menu](https://docs.dataminer.services/develop/devguide/Connector/UIComponentsCustomTableContextMenu.html)

## Requirements

The "DataMiner Integration Studio" Visual Studio extension is required for development of connectors and Automation scripts using NuGets.

See [Installing DataMiner Integration Studio](https://aka.dataminer.services/DisInstallation)

> [!IMPORTANT]
> NuGets are mandatory to be installed with PackageReferences. DIS was redesigned to work with PackageReferences and be future-proof.
>
> For more information on how to migrate from packages.config to PackageReferences, see [docs.microsoft.com](https://docs.microsoft.com/en-us/nuget/consume-packages/migrate-packages-config-to-package-reference).

## Getting started

Create your own class extending ContextMenuTableManagerBase or use the existing ContextMenuTableManagerBasic class.

```cs
public static void Run(SLProtocol protocol, object contextMenuData)
{
   ContextMenuTableManagerBasic contextMenu = new ContextMenuTableManagerBasic(
      protocol,
      contextMenuData,
      Parameter.managedtable2_autoinc);
      contextMenu.ProcessContextMenuAction();
}
```
