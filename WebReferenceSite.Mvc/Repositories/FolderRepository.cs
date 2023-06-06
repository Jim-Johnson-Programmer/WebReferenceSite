using Dapper;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using WebReferenceSite.Mvc.Models.RepositoryModels;
using WebReferenceSite.Mvc.Repositories.DapperWrapper;

namespace WebReferenceSite.Mvc.Repositories
{

    public class FolderRepository : IFolderRepository
    {
        private string _sqlConnectionString = string.Empty;
        private ILoggerFactory _loggerFactory;
        private ILogger<FolderRepository> _logger;
        private IDbExecutorFactory _dbExecutorFactory;
        private ILoggerFactory loggerFactory;

        public FolderRepository(string connectionString ,IDbExecutorFactory dbExecutorFactory, ILoggerFactory loggerFactory)
        {
            _sqlConnectionString = connectionString;
            _dbExecutorFactory = dbExecutorFactory;
            _logger = loggerFactory.CreateLogger<FolderRepository>();
        }

        public List<Folder> GetAllRows()
        {
            List<Folder> folders = new List<Folder>();
            IDbExecutor dbExecutor = _dbExecutorFactory.CreateExecutor();

            try
            {
                folders = dbExecutor.Query<Folder>("SELECT FolderId,FolderName,ParentFolderId,ParentFolderName,CreatedOn,UpdatedOn,CreatedBy,UpdatedBy FROM dbo.Folders").AsList<Folder>();
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
            IDbExecutor dbExecutor = _dbExecutorFactory.CreateExecutor();

            try
            {
                string sqlQuery = "INSERT INTO [dbo].[Folders]([FolderName],[ParentFolderId],[ParentFolderName],[CreatedOn],[UpdatedOn],[CreatedBy],[UpdatedBy]) Values (";
                sqlQuery += "@FolderName,@ParentFolderId,@ParentFolderName,GETDATE(),GETDATE(),SYSTEM_USER,SYSTEM_USER); ";
                sqlQuery += "SELECT CAST(SCOPE_IDENTITY() as int); ";
                newFolderId = dbExecutor.Execute(sqlQuery, new { FolderName = folder.FolderName, ParentFolderId = folder.ParentFolderId, ParentFolderName = folder.ParentFolderName });

                string sqlAfterQuery = "Select * FROM [dbo].[Folders] WHERE FolderId = @NewFolderId";
                Folder insertedFolder = dbExecutor.Query<Folder>(sqlAfterQuery, new { @NewFolderId = newFolderId }).FirstOrDefault();
                if (insertedFolder != null) throw new System.Exception(string.Format("Insert failed for folderName={0} and parentId={1}", folder.FolderName, folder.ParentFolderId));

                _logger.LogTrace("FolderService ADDED folder with id={0} to database", folder.FolderId);
            }
            catch (System.Exception ex)
            {
                newFolderId = -1;
                _logger.LogError("Error trying to ADD folder. Error message={0}", ex.Message);                
            }
            finally
            {
                dbExecutor.Dispose();
            }

            return newFolderId;
        }

        public bool UpdateFolder(Folder folder)
        {
            bool status = true;
            int newFolderId;
            IDbExecutor dbExecutor = _dbExecutorFactory.CreateExecutor();

            try
            {
                string sqlQuery = "UPDATE [dbo].[Folders] SET [FolderName] = @FolderName ,[UpdatedOn] = GETDATE() ,[UpdatedBy] = SYSTEM_USER WHERE FolderId = @FolderId;";
                newFolderId = dbExecutor.Execute(sqlQuery, new { FolderName = folder.FolderName, FolderId=folder.FolderId});
                _logger.LogTrace("FolderService UPDATED folder with id={0} in database", folder.FolderId);
            }
            catch (System.Exception ex)
            {
                status = false;
                _logger.LogError("Error trying to UPDATE folder. Error message={0}", ex.Message);
            }
            finally
            {
                dbExecutor.Dispose();
            }

            return status;
        }
    }
}
