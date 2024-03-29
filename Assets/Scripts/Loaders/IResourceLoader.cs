using System.Collections.Generic;

public interface IResourceLoader
{
    public List<TItem> LoadItems<TItem>() where TItem : Item;

    public List<Item> LoadAllItems();

    public TUIItem LoadUIItem<TUIItem>() where TUIItem : UIItem;
}
