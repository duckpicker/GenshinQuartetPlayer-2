using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data.Entity;
using System.IO;

namespace GenshinQuartetPlayer2
{
    class Database
    {
        static string dbscript = @"CREATE TABLE IF NOT EXISTS playlist_entries(
            id        INTEGER PRIMARY KEY AUTOINCREMENT,
            name      varchar,
            file_path varchar
            );";

        SQLiteConnection _connection;

        public Database()
        {
            _connection = CreateConnection().GetAwaiter().GetResult();
            InitialiseDatabase(_connection);
        }

        ~Database()
        {
            _connection.Dispose();
        }

        public async Task<SQLiteConnection> CreateConnection()
        {
            SQLiteConnection connection;

            connection = new SQLiteConnection("Data Source=database.db; Version = 3; New = True; ");

            await connection.OpenAsync();

            return connection;
        }

        async void InitialiseDatabase(SQLiteConnection connection)
        {
            var command = connection.CreateCommand();
            command.CommandText = dbscript;
            await command.ExecuteNonQueryAsync();
            await CheckAllEntriesAsync();
        }

        public async Task<List<PlaylistEntry>> GetAllEntries()
        {
            var command = _connection.CreateCommand();
            command.CommandText = "select * from playlist_entries";
            var reader = await command.ExecuteReaderAsync();
            List<PlaylistEntry> entries = new List<PlaylistEntry>();
            while (reader.Read())
            {
                PlaylistEntry entry = new PlaylistEntry()
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    FilePath = reader.GetString(2),
                };
                entries.Add(entry);
            }
            return entries;
        }

        public async void AddEntry(string name, string path)
        {
            var command = _connection.CreateCommand();
            command.CommandText = "SELECT * FROM playlist_entries WHERE name = @name AND file_path = @path";
            command.Parameters.AddWithValue("@name", name);
            command.Parameters.AddWithValue("@path", path);
            var result = await command.ExecuteReaderAsync();

            if (!result.Read())
            {
                command = _connection.CreateCommand();
                command.CommandText = $"insert into playlist_entries (name, file_path) values (:name, :path)";
                command.Parameters.AddWithValue("name", name);
                command.Parameters.AddWithValue("path", path);
                await command.ExecuteNonQueryAsync();
            }
        }

        async Task CheckAllEntriesAsync()
        {
            var command = _connection.CreateCommand();
            command.CommandText = "select * from playlist_entries";
            var reader = await command.ExecuteReaderAsync();

            while (reader.Read())
            {
                if (!File.Exists(reader.GetString(2)))
                {
                    await DeleteEntryAsync(reader.GetInt32(0));
                }
            }
        }

        public async Task DeleteEntryAsync(int id)
        {
            var command = _connection.CreateCommand();
            command.CommandText = $"delete from playlist_entries where id = '{id}'";
            await command.ExecuteNonQueryAsync();
        }
    }
}
