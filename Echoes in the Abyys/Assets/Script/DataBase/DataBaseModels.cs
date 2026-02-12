using SQLite;

#region INVENTORY

public class InventoryItem
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public string ItemId { get; set; }
    public int Quantity { get; set; }
}

#endregion


#region CARD SYSTEM

public class OwnedCard
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public string CardId { get; set; }
    public bool Owned { get; set; }
}

public class SelectedDeck
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public string CardId { get; set; }
}

#endregion


#region ARTIFACT SYSTEM

public class OwnedArtifact
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public string ArtifactId { get; set; }
    public bool Unlocked { get; set; }
}

public class SelectedArtifact
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public string ArtifactId { get; set; }
}

#endregion
