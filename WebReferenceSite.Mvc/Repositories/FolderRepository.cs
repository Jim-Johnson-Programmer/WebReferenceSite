using System.Collections.Generic;
using WebReferenceSite.Mvc.Models.RepositoryModels;
using Dapper;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Linq;
using Microsoft.Extensions.Logging;
using WebReferenceSite.Mvc.Repositories.DapperWrapper;

namespace WebReferenceSite.Mvc.Repositories
{
    public interface IFolderRepository
    {
        int CreateFolder(Folder folder);
        bool UpdateFolder(Folder folder);
        List<Folder> GetAllRows();
        List<Folder> GetFolderChildFolders(string parentFolderId);
        List<Folder> GetFoldersFromIdToRoot(string folderId);
        Folder GetFolderByFolderId(string folderId);
        List<Folder> GetFoldersByFolderNamePortion(string folderName);
    }

    public class FolderRepository : IFolderRepository
    {
        //private string _sqlConnectionString = string.Empty;
        private ILoggerFactory _loggerFactory;
        private ILogger<FolderRepository> _logger;
        private IDbExecutorFactory _dbExecutorFactory;
        //private IDbExecutor _dapperWrapper;

        public FolderRepository(IDbExecutorFactory dbExecutorFactory, ILogger<FolderRepository> logger)
        {
            _logger = logger;
            //_dapperWrapper = dbExecutor;
            _dbExecutorFactory = dbExecutorFactory;
        }

        //public FolderRepository(ILogger<FolderRepository> logger, string connectionString)
        //{
        //    _sqlConnectionString = connectionString;
        //    //_loggerFactory = loggerFactory;
        //    //_logger = loggerFactory.CreateLogger<FolderRepository>();
        //    _logger = logger;
        //    _dapperWrapper = new SqlExecutor(new SqlConnection(_sqlConnectionString));
        //}

        //public FolderRepository(IDbExecutor sqlExecutorWrapper, ILoggerFactory loggerFactory, string connectionString)
        //{
        //    _sqlConnectionString = connectionString;
        //    _loggerFactory = loggerFactory;
        //    _logger = loggerFactory.CreateLogger<FolderRepository>();
        //    _dapperWrapper = sqlExecutorWrapper;
        //}

        public List<Folder> GetAllRows()
        {
            List<Folder> folders = new List<Folder>();
            //SqlConnection connection = new SqlConnection(_sqlConnectionString);

            //try
            //{                
            //    folders = connection.Query<Folder>("SELECT FolderId,FolderName,ParentFolderId,ParentFolderName,CreatedOn,UpdatedOn,CreatedBy,UpdatedBy FROM dbo.Folders").AsList<Folder>();
            //    _logger.LogTrace("FolderService retrieved {0} rows for folder grid", folders.Count());
            //}
            //catch (System.Exception ex)
            //{
            //    _logger.LogError("Error trying to get ALL folder rows. Error message={0}", ex, ex.Message);
            //}
            //finally
            //{
            //    connection.Close();
            //    connection.Dispose();
            //}

            return folders;
        }

        public List<Folder> GetFolderChildFolders(string parentFolderId)
        {
            List<Folder> folders = new List<Folder>();
            IDbExecutor dbExecutor = _dbExecutorFactory.CreateExecutor();

            try
            {
                folders = dbExecutor.Query<Folder>("SELECT FolderId,FolderName,ParentFolderId,ParentFolderName,CreatedOn,UpdatedOn,CreatedBy,UpdatedBy FROM dbo.Folders where ParentFolderId=" + parentFolderId,
                    new { parentFolderId})
                    .AsList<Folder>();
                _logger.LogTrace("FolderService retrieved {0} rows for folder grid", folders.Count());
            }
            catch (System.Exception ex)
            {
                _logger.LogError("Error trying to get ALL folder rows. Error message={0}", ex, ex.Message);
            }
            finally
            {
                dbExecutor.Dispose();
            }

            return folders;
        }

        public Folder GetFolderByFolderId(string folderId)
        {
            Folder folder = new Folder();
            //SqlConnection connection = new SqlConnection(_sqlConnectionString);
            IDbExecutor dbExecutor = _dbExecutorFactory.CreateExecutor();

            try
            {  
                folder = dbExecutor.Query<Folder>("SELECT FolderId,FolderName,ParentFolderId,ParentFolderName,CreatedOn,UpdatedOn,CreatedBy,UpdatedBy FROM dbo.Folders WHERE FolderId=" + folderId, new { folderId }).FirstOrDefault();
                _logger.LogTrace("FolderService retrieved folder with id={0} for folder grid", folder.FolderId);
            }
            catch (System.Exception ex)
            {
                _logger.LogError("Error trying to get folder. Error message={0}", ex, ex.Message);
            }
            finally
            {
                dbExecutor.Dispose();
            }

            return folder;
        }

        public List<Folder> GetFoldersByFolderNamePortion(string folderName)
        {
            List<Folder> folderList = new List<Folder>();
            IDbExecutor dbExecutor = _dbExecutorFactory.CreateExecutor();

            try
            {
                folderList = dbExecutor.Query<Folder>("SELECT FolderId,FolderName,ParentFolderId,ParentFolderName,CreatedOn,UpdatedOn,CreatedBy,UpdatedBy FROM dbo.Folders WHERE FolderName=@FolderName", 
                    new { FolderName = new DbString { Value = folderName, IsFixedLength = false, IsAnsi = true }}).ToList();
                int folderCount = folderList!=null?folderList.Count:0;
                _logger.LogInformation("FolderService retrieved folders with folderName results count={folderCount}", folderCount);
            }
            catch (System.Exception ex)
            {
                _logger.LogError("Error trying to get folder. Error message={0}", ex, ex.Message);
            }
            finally
            {
                dbExecutor.Dispose();
            }

            return folderList;
        }

        public List<Folder> GetFoldersFromIdToRoot(string folderId)
        {
            List<Folder> folderList = new List<Folder>();
            //SqlConnection connection = new SqlConnection(_sqlConnectionString);
            IDbExecutor dbExecutor = _dbExecutorFactory.CreateExecutor();

            try
            {
                string sqlString =  "with recursionTable as ( ";
                sqlString += " select a.FolderId, a.FolderName, a.ParentFolderId, a.ParentFolderName, a.CreatedOn, a.UpdatedOn, a.CreatedBy, a.UpdatedBy from folders a ";
                sqlString += " where a.FolderId=@FolderId ";
                sqlString += " union all ";
                sqlString += " select b.FolderId, b.FolderName, b.ParentFolderId, b.ParentFolderName, b.CreatedOn, b.UpdatedOn, b.CreatedBy, b.UpdatedBy  from folders b ";
                sqlString += "inner join recursionTable c on c.ParentFolderId = b.FolderId";
                sqlString += ")";
                sqlString += "select rc.FolderId, rc.FolderName, rc.ParentFolderId, rc.ParentFolderName, rc.CreatedOn, rc.UpdatedOn, rc.CreatedBy, rc.UpdatedBy from recursionTable rc order by rc.FolderId;";


                folderList = dbExecutor.Query<Folder>(sqlString, new { FolderId=folderId }).ToList();
                _logger.LogTrace("FolderService retrieved all folders from id={0} crawling up to ROOT",  
                                    string.Join("/", folderList.Select(t => t.FolderId).ToArray()));
            }
            catch (System.Exception ex)
            {
                _logger.LogError("Error trying to get folders between folderid{0} to ROOT. Error message={1}", folderId, ex.Message);
            }
            finally
            {
                dbExecutor.Dispose();
            }

            return folderList;
        }

        public int CreateFolder(Folder folder)
        {
            int newFolderId = 0;
            //SqlConnection connection = new SqlConnection(_sqlConnectionString);
            
            //try
            //{
            //    //int rowsAffected = connection.Execute(sqlQuery, folder);

            //    string sqlQuery = "INSERT INTO [dbo].[Folders]([FolderName],[ParentFolderId],[ParentFolderName],[CreatedOn],[UpdatedOn],[CreatedBy],[UpdatedBy]) Values (";
            //    sqlQuery += "@FolderName,@ParentFolderId,@ParentFolderName,@CreatedOn,@UpdatedOn,@CreatedBy,@UpdatedBy); ";
            //    sqlQuery += "SELECT CAST(SCOPE_IDENTITY() as int); ";
            //    //newFolderId = _dapperWrapper.QueryFirstOrDefault<int>(sqlQuery, folder);

            //    string  sqlAfterQuery = "Select * FROM [dbo].[Folders] WHERE FolderId = @NewFolderId";
            //    //Folder insertedFolder = _dapperWrapper.QueryFirstOrDefault<Folder>(sqlAfterQuery, new { @NewFolderId = newFolderId });
            //    if (insertedFolder != null) throw new System.Exception(string.Format("Insert failed for folderName={0} and parentId={1}", folder.FolderName, folder.ParentFolderId));
                
            //    _logger.LogTrace("FolderService ADDED folder with id={0} to database", folder.FolderId);
            //}
            //catch (System.Exception ex)
            //{
            //    _logger.LogError("Error trying to ADD folder. Error message={0}", ex, ex.Message);
            //    newFolderId = -1;
            //}
            //finally
            //{
            //    connection.Close();
            //    connection.Dispose();
            //}

            return newFolderId;
        }

        public bool UpdateFolder(Folder folder)
        {
            bool status = true;
            //SqlConnection connection = new SqlConnection(_sqlConnectionString);
            //string sqlQuery = "INSERT INTO [dbo].[Folders]([FolderName],[ParentFolderId],[ParentFolderName],[CreatedOn],[UpdatedOn],[CreatedBy],[UpdatedBy]) Values (";
            //sqlQuery += "@FolderName,@ParentFolderId,@ParentFolderName,@CreatedOn,@UpdatedOn,@CreatedBy,@UpdatedBy)";

            //try
            //{
            //    int rowsAffected = connection.Execute(sqlQuery, folder);
            //    _logger.LogTrace("FolderService ADDED folder with id={0} to database", folder.FolderId);
            //}
            //catch (System.Exception ex)
            //{
            //    _logger.LogError("Error trying to ADD folder. Error message={0}", ex, ex.Message);
            //    status = false;
            //}
            //finally
            //{
            //    connection.Close();
            //    connection.Dispose();
            //}

            return status;
        }
    }
}
