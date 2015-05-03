using Database;
using Interfaces;

namespace PicTureen.Services
{
    public class ContextProvider: IContextProvider
    {
        private readonly ISettingsService _settingsService;
        private readonly IDbHelper _dbHelper;
        private ImagesDbContext _context;
        public ContextProvider(ISettingsService settingsService, IDbHelper dbHelper)
        {
            _settingsService = settingsService;
            _dbHelper = dbHelper;
        }

        public ImagesDbContext GetDbContext()
        {
            if (_context == null)
            {
                _dbHelper.CreateTables();
                _context = new ImagesDbContext(_dbHelper);
            }
            return _context;
        }
    }
}
