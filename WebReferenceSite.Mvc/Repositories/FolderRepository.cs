using System.Collections.Generic;
using WebReferenceSite.Mvc.Models.RepositoryModels;
using Dapper;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Linq;

namespace WebReferenceSite.Mvc.Repositories
{
    public class FolderRepository
    {
        private string _sqlConnectionString = string.Empty;
        public FolderRepository(string connectionString)
        {
            _sqlConnectionString = connectionString;
        }
        public List<Folder> GetAllRows() 
        {
            List<Folder> folders = new List<Folder>();
            
            SqlConnection connection = new SqlConnection(_sqlConnectionString);
            
            folders = connection.Query<Folder>("SELECT FolderId,FolderName,ParentFolderId,ParentFolderName,CreatedOn,UpdatedOn,CreatedBy,UpdatedBy FROM dbo.Folders").AsList<Folder>();
            
            connection.Close();
            connection.Dispose();
            
            return folders;
        }

        public Folder GetFolder(int folderId)
        {
            Folder folder = new Folder();
            SqlConnection connection = new SqlConnection(_sqlConnectionString);

            folder = connection.Query<Folder>("SELECT FolderId,FolderName,ParentFolderId,ParentFolderName,CreatedOn,UpdatedOn,CreatedBy,UpdatedBy FROM dbo.Folders WHERE FolderId =" + folderId, new { folderId }).FirstOrDefault();

            connection.Close();
            connection.Dispose();
            return folder;
        }
        public bool AddRow(Folder folder)
        {
            SqlConnection connection = new SqlConnection(_sqlConnectionString);
            string sqlQuery = "INSERT INTO [dbo].[Folders]([FolderName],[ParentFolderId],[ParentFolderName],[CreatedOn],[UpdatedOn],[CreatedBy],[UpdatedBy]) Values (";
            sqlQuery += "@FolderName,@ParentFolderId,@ParentFolderName,@CreatedOn,@UpdatedOn,@CreatedBy,@UpdatedBy)";

            int rowsAffected = connection.Execute(sqlQuery, folder);

            connection.Close();
            connection.Dispose();

            return true;
        }
        
        public bool UpdateRow(Folder folders)
        {
            return true;
        }
    }
}
