using UnityEngine;
using SQLite;
using System.IO;
using System.Collections.Generic;

public class DatabaseManager : MonoBehaviour
{
    public static DatabaseManager Instance;

    private SQLiteConnection connection;
    private string dbPath;

    public SQLiteConnection Connection => connection;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            dbPath = Path.Combine(Application.persistentDataPath, "gameDatabase.db");
            connection = new SQLiteConnection(dbPath);

            CreateTables();
            InitializeTestDeck();   // <-- TAMBAHAN DI SINI

            Debug.Log("Database Path: " + dbPath);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void CreateTables()
    {
        connection.CreateTable<InventoryItem>();
        connection.CreateTable<OwnedCard>();
        connection.CreateTable<SelectedDeck>();
        connection.CreateTable<OwnedArtifact>();
        connection.CreateTable<SelectedArtifact>();
    }

    // =============================
    // TEST DATA (UNTUK SEKARANG)
    // =============================
    void InitializeTestDeck()
    {
        var existingDeck = GetDeck();

        if (existingDeck.Count == 0)
        {
            Debug.Log("Initializing test deck...");

            AddCardToDeck("card_fireball");
            AddCardToDeck("card_heal");
            AddCardToDeck("card_lightning");
            AddCardToDeck("card_nature");
        }
    }

    private void OnApplicationQuit()
    {
        connection?.Close();
    }

    // =============================
    // DECK CRUD
    // =============================

    public void AddCardToDeck(string cardId)
    {
        connection.Insert(new SelectedDeck
        {
            CardId = cardId
        });

        Debug.Log("Card added to deck: " + cardId);
    }

    public List<SelectedDeck> GetDeck()
    {
        return connection.Table<SelectedDeck>().ToList();
    }

    public void ClearDeck()
    {
        connection.DeleteAll<SelectedDeck>();
        Debug.Log("Deck cleared");
    }
}
